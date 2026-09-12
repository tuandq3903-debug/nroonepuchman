using System;

namespace Game3
{
	// Token: 0x0200029E RID: 670
	public class BgItem
	{
		// Token: 0x06001D65 RID: 7525 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void clearHashTable()
		{
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x001C4920 File Offset: 0x001C2B20
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

		// Token: 0x06001D67 RID: 7527 RVA: 0x001C4960 File Offset: 0x001C2B60
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

		// Token: 0x06001D68 RID: 7528 RVA: 0x001C49A4 File Offset: 0x001C2BA4
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

		// Token: 0x06001D69 RID: 7529 RVA: 0x001C49D8 File Offset: 0x001C2BD8
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

		// Token: 0x06001D6A RID: 7530 RVA: 0x001C4B2C File Offset: 0x001C2D2C
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

		// Token: 0x04003831 RID: 14385
		public int id;

		// Token: 0x04003832 RID: 14386
		public int trans;

		// Token: 0x04003833 RID: 14387
		public short idImage;

		// Token: 0x04003834 RID: 14388
		public int x;

		// Token: 0x04003835 RID: 14389
		public int y;

		// Token: 0x04003836 RID: 14390
		public int dx;

		// Token: 0x04003837 RID: 14391
		public int dy;

		// Token: 0x04003838 RID: 14392
		public sbyte layer;

		// Token: 0x04003839 RID: 14393
		public int[] tileX;

		// Token: 0x0400383A RID: 14394
		public int[] tileY;

		// Token: 0x0400383B RID: 14395
		public static MyHashTable imgNew = new MyHashTable();

		// Token: 0x0400383C RID: 14396
		public static MyVector vKeysNew = new MyVector();

		// Token: 0x0400383D RID: 14397
		public static MyVector vKeysLast = new MyVector();

		// Token: 0x0400383E RID: 14398
		public int transX;

		// Token: 0x0400383F RID: 14399
		public int transY;

		// Token: 0x04003840 RID: 14400
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

		// Token: 0x04003841 RID: 14401
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

		// Token: 0x04003842 RID: 14402
		public static sbyte[] newSmallVersion;
	}
}
