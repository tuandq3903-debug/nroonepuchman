using System;

namespace Game2
{
	// Token: 0x02000376 RID: 886
	public class BgItem
	{
		// Token: 0x06002709 RID: 9993 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void clearHashTable()
		{
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x002599C4 File Offset: 0x00257BC4
		public static bool isExistKeyNews(string keyNew)
		{
			for (int i = 0; i < BgItem.vKeysNew.size(); i++)
			{
				if (((string)BgItem.vKeysNew.elementAt(i)).Equals(keyNew))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x00259A04 File Offset: 0x00257C04
		public bool isNotBlend()
		{
			if (mGraphics.zoomLevel == 1)
			{
				return true;
			}
			if (TileMap.isInAirMap())
			{
				return true;
			}
			for (int i = 0; i < BgItem.idNotBlend.Length; i++)
			{
				if ((int)this.idImage == BgItem.idNotBlend[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x00259A48 File Offset: 0x00257C48
		public bool isMiniBg()
		{
			for (int i = 0; i < BgItem.isMiniBgz.Length; i++)
			{
				if ((int)this.idImage == BgItem.isMiniBgz[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x00259A7C File Offset: 0x00257C7C
		public void changeColor()
		{
			if (this.isNotBlend() || this.layer == 2 || this.layer == 4 || BgItem.imgNew.containsKey(this.idImage.ToString() + "blend" + this.layer.ToString()))
			{
				return;
			}
			Image image = (Image)BgItem.imgNew.get(this.idImage.ToString() + string.Empty);
			if (image != null && image.getRealImageWidth() > 4)
			{
				sbyte[] array = Rms.loadRMS(string.Concat(new string[]
				{
					"x",
					mGraphics.zoomLevel.ToString(),
					"blend",
					this.idImage.ToString(),
					"layer",
					this.layer.ToString()
				}));
				if (array == null)
				{
					BgItem.imgNew.put(this.idImage.ToString() + "blend" + this.layer.ToString(), BgItemMn.blendImage(image, (int)this.layer, (int)this.idImage));
					return;
				}
				Image v = Image.createImage(array, 0, array.Length);
				BgItem.imgNew.put(this.idImage.ToString() + "blend" + this.layer.ToString(), v);
			}
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x00259BD0 File Offset: 0x00257DD0
		public void paint(mGraphics g)
		{
			if (ModFunc.GiamDungLuong || Char.isLoadingMap || (this.idImage == 279 && GameScr.gI().tMabuEff >= 110))
			{
				return;
			}
			int cmx = GameScr.cmx;
			int cmy = GameScr.cmy;
			Image image = (this.layer == 2 || this.layer == 4) ? ((Image)BgItem.imgNew.get(this.idImage.ToString() + string.Empty)) : (this.isNotBlend() ? ((Image)BgItem.imgNew.get(this.idImage.ToString() + string.Empty)) : ((Image)BgItem.imgNew.get(this.idImage.ToString() + "blend" + this.layer.ToString())));
			if (image == null || this.idImage == 96)
			{
				return;
			}
			if (this.layer == 4)
			{
				this.transX = -cmx / 2 + 100;
			}
			if (this.idImage == 28 && this.layer == 3)
			{
				this.transX = -cmx / 3 + 200;
			}
			if ((this.idImage == 67 || this.idImage == 68 || this.idImage == 69 || this.idImage == 70) && this.layer == 3)
			{
				this.transX = -cmx / 3 + 200;
			}
			if (this.isMiniBg() && this.layer < 4)
			{
				this.transX = -(cmx >> 4) + 50;
				this.transY = (cmy >> 5) - 15;
			}
			int num = this.x + this.dx + this.transX;
			int num2 = this.y + this.dy + this.transY;
			if (this.x + this.dx + image.getWidth() + this.transX >= cmx && this.x + this.dx + this.transX <= cmx + GameCanvas.w && this.y + this.dy + this.transY + image.getHeight() >= cmy && this.y + this.dy + this.transY <= cmy + GameCanvas.h)
			{
				g.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), this.trans, this.x + this.dx + this.transX, this.y + this.dy + this.transY, 0);
				if (this.idImage == 11 && TileMap.mapID != 122)
				{
					g.setClip(num, num2 + 24, 48, 14);
					for (int i = 0; i < 2; i++)
					{
						g.drawRegion(TileMap.imgWaterflow, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, num + i * 24, num2 + 24, 0);
					}
					g.setClip(GameScr.cmx, GameScr.cmy, GameScr.gW, GameScr.gH);
				}
			}
			if (TileMap.isDoubleMap() && this.idImage > 137 && this.idImage != 156 && this.idImage != 159 && this.idImage != 157 && this.idImage != 165 && this.idImage != 167 && this.idImage != 168 && this.idImage != 169 && this.idImage != 170 && this.idImage != 238 && TileMap.pxw - (this.x + this.dx + this.transX) >= cmx && TileMap.pxw - (this.x + this.dx + this.transX + image.getWidth()) <= cmx + GameCanvas.w && this.y + this.dy + this.transY + image.getHeight() >= cmy && this.y + this.dy + this.transY <= cmy + GameCanvas.h && (this.idImage < 241 || this.idImage >= 266))
			{
				g.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 2, TileMap.pxw - (this.x + this.dx + this.transX), this.y + this.dy + this.transY, StaticObj.TOP_RIGHT);
			}
		}

		// Token: 0x04004AB0 RID: 19120
		public int id;

		// Token: 0x04004AB1 RID: 19121
		public int trans;

		// Token: 0x04004AB2 RID: 19122
		public short idImage;

		// Token: 0x04004AB3 RID: 19123
		public int x;

		// Token: 0x04004AB4 RID: 19124
		public int y;

		// Token: 0x04004AB5 RID: 19125
		public int dx;

		// Token: 0x04004AB6 RID: 19126
		public int dy;

		// Token: 0x04004AB7 RID: 19127
		public sbyte layer;

		// Token: 0x04004AB8 RID: 19128
		public int[] tileX;

		// Token: 0x04004AB9 RID: 19129
		public int[] tileY;

		// Token: 0x04004ABA RID: 19130
		public static MyHashTable imgNew = new MyHashTable();

		// Token: 0x04004ABB RID: 19131
		public static MyVector vKeysNew = new MyVector();

		// Token: 0x04004ABC RID: 19132
		public static MyVector vKeysLast = new MyVector();

		// Token: 0x04004ABD RID: 19133
		public int transX;

		// Token: 0x04004ABE RID: 19134
		public int transY;

		// Token: 0x04004ABF RID: 19135
		public static int[] idNotBlend = new int[]
		{
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			91,
			92,
			95,
			144,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			117,
			118,
			119,
			120,
			121,
			122,
			123,
			124,
			125,
			126,
			127,
			132,
			133,
			134,
			139,
			140,
			141,
			142,
			143,
			144,
			145,
			146,
			147,
			171,
			121,
			122,
			229,
			218
		};

		// Token: 0x04004AC0 RID: 19136
		public static int[] isMiniBgz = new int[]
		{
			79,
			80,
			81,
			85,
			86,
			90,
			91,
			92,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108
		};

		// Token: 0x04004AC1 RID: 19137
		public static sbyte[] newSmallVersion;
	}
}
