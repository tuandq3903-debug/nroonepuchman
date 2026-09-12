using System;

namespace Game1
{
	// Token: 0x0200044E RID: 1102
	public class BgItem
	{
		// Token: 0x060030AD RID: 12461 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void clearHashTable()
		{
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x002EEA68 File Offset: 0x002ECC68
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

		// Token: 0x060030AF RID: 12463 RVA: 0x002EEAA8 File Offset: 0x002ECCA8
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

		// Token: 0x060030B0 RID: 12464 RVA: 0x002EEAEC File Offset: 0x002ECCEC
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

		// Token: 0x060030B1 RID: 12465 RVA: 0x002EEB20 File Offset: 0x002ECD20
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

		// Token: 0x060030B2 RID: 12466 RVA: 0x002EEC74 File Offset: 0x002ECE74
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

		// Token: 0x04005D2F RID: 23855
		public int id;

		// Token: 0x04005D30 RID: 23856
		public int trans;

		// Token: 0x04005D31 RID: 23857
		public short idImage;

		// Token: 0x04005D32 RID: 23858
		public int x;

		// Token: 0x04005D33 RID: 23859
		public int y;

		// Token: 0x04005D34 RID: 23860
		public int dx;

		// Token: 0x04005D35 RID: 23861
		public int dy;

		// Token: 0x04005D36 RID: 23862
		public sbyte layer;

		// Token: 0x04005D37 RID: 23863
		public int[] tileX;

		// Token: 0x04005D38 RID: 23864
		public int[] tileY;

		// Token: 0x04005D39 RID: 23865
		public static MyHashTable imgNew = new MyHashTable();

		// Token: 0x04005D3A RID: 23866
		public static MyVector vKeysNew = new MyVector();

		// Token: 0x04005D3B RID: 23867
		public static MyVector vKeysLast = new MyVector();

		// Token: 0x04005D3C RID: 23868
		public int transX;

		// Token: 0x04005D3D RID: 23869
		public int transY;

		// Token: 0x04005D3E RID: 23870
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

		// Token: 0x04005D3F RID: 23871
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

		// Token: 0x04005D40 RID: 23872
		public static sbyte[] newSmallVersion;
	}
}
