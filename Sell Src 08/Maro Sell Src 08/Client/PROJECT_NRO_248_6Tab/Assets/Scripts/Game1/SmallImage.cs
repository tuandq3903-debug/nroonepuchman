using System;
using System.Collections.Generic;
using Game1.Assets.src.e;

namespace Game1
{
	// Token: 0x020004E8 RID: 1256
	public class SmallImage
	{
		// Token: 0x0600383B RID: 14395 RVA: 0x0036F544 File Offset: 0x0036D744
		public SmallImage()
		{
			this.readImage();
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x0036F554 File Offset: 0x0036D754
		public static void loadBigRMS()
		{
			if (SmallImage.imgbig == null)
			{
				SmallImage.imgbig = new Image[]
				{
					GameCanvas.loadImageRMS("/img/Big0.png"),
					GameCanvas.loadImageRMS("/img/Big1.png"),
					GameCanvas.loadImageRMS("/img/Big2.png"),
					GameCanvas.loadImageRMS("/img/Big3.png"),
					GameCanvas.loadImageRMS("/img/Big4.png")
				};
			}
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x0036F5B4 File Offset: 0x0036D7B4
		public static void loadBigImage()
		{
			SmallImage.imgEmpty = Image.createRGBImage(new int[1], 1, 1, true);
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x0036F5C9 File Offset: 0x0036D7C9
		public static void init()
		{
			SmallImage.instance = null;
			SmallImage.instance = new SmallImage();
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x0036F5DC File Offset: 0x0036D7DC
		public void readImage()
		{
			int num = 0;
			try
			{
				DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NR_image"));
				short num2 = dataInputStream.readShort();
				SmallImage.smallImg = new int[(int)num2][];
				for (int i = 0; i < SmallImage.smallImg.Length; i++)
				{
					SmallImage.smallImg[i] = new int[5];
				}
				for (int j = 0; j < (int)num2; j++)
				{
					num++;
					SmallImage.smallImg[j][0] = dataInputStream.readUnsignedByte();
					SmallImage.smallImg[j][1] = (int)dataInputStream.readShort();
					SmallImage.smallImg[j][2] = (int)dataInputStream.readShort();
					SmallImage.smallImg[j][3] = (int)dataInputStream.readShort();
					SmallImage.smallImg[j][4] = (int)dataInputStream.readShort();
				}
			}
			catch (Exception ex)
			{
				Cout.LogError3("Loi readImage: " + ex.ToString() + "i= " + num.ToString());
			}
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void clearHastable()
		{
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x0036F6C8 File Offset: 0x0036D8C8
		public static void createImage(int id)
		{
			if (id < 0 || SmallImage.imgNew == null || id >= SmallImage.imgNew.Length)
			{
				return;
			}
			if (mGraphics.zoomLevel == 1)
			{
				Image image = GameCanvas.loadImage("/SmallImage/Small" + id.ToString() + ".png");
				if (image != null)
				{
					SmallImage.imgNew[id] = new Small(image, id);
					return;
				}
				SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
				Service.gI().requestIcon(id);
				return;
			}
			else
			{
				Image image2 = GameCanvas.loadImage("/SmallImage/Small" + id.ToString() + ".png");
				if (image2 != null)
				{
					SmallImage.imgNew[id] = new Small(image2, id);
					return;
				}
				bool flag = false;
				if (SmallImage.imageRaw.ContainsKey(id))
				{
					Image img = null;
					SmallImage.imageRaw.TryGetValue(id, out img);
					if (img != null)
					{
						SmallImage.imgNew[id] = new Small(img, id);
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
					Service.gI().requestIcon(id);
				}
				return;
			}
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x0036F7B0 File Offset: 0x0036D9B0
		public static void drawSmallImage(mGraphics g, int id, int x, int y, int transform, int anchor)
		{
			if (id < 0 || SmallImage.imgNew == null || id >= SmallImage.imgNew.Length)
			{
				return;
			}
			if (SmallImage.imgbig != null)
			{
				if (SmallImage.smallImg != null)
				{
					if (id < 0 || id >= SmallImage.smallImg.Length || SmallImage.smallImg[id] == null || id >= SmallImage.imgNew.Length || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
					{
						Small small2 = SmallImage.imgNew[id];
						if (small2 == null)
						{
							SmallImage.createImage(id);
							return;
						}
						small2.paint(g, transform, x, y, anchor);
						return;
					}
					else if (SmallImage.imgbig[SmallImage.smallImg[id][0]] != null)
					{
						g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], SmallImage.smallImg[id][1], SmallImage.smallImg[id][2], SmallImage.smallImg[id][3], SmallImage.smallImg[id][4], transform, x, y, anchor);
						return;
					}
				}
				else if (GameCanvas.currentScreen != GameScr.gI())
				{
					Small small3 = SmallImage.imgNew[id];
					if (small3 == null)
					{
						SmallImage.createImage(id);
						return;
					}
					small3.paint(g, transform, x, y, anchor);
				}
				return;
			}
			Small small4 = SmallImage.imgNew[id];
			if (small4 == null)
			{
				SmallImage.createImage(id);
				return;
			}
			g.drawRegion(small4, 0, 0, mGraphics.getImageWidth(small4.img), mGraphics.getImageHeight(small4.img), transform, x, y, anchor);
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x0036F8EC File Offset: 0x0036DAEC
		public static void drawSmallImage(mGraphics g, int id, int f, int x, int y, int w, int h, int transform, int anchor)
		{
			if (id < 0 || SmallImage.imgNew == null || id >= SmallImage.imgNew.Length)
			{
				return;
			}
			if (SmallImage.imgbig == null)
			{
				Small small = SmallImage.imgNew[id];
				if (small == null)
				{
					SmallImage.createImage(id);
					return;
				}
				g.drawRegion(small.img, 0, f * w, w, h, transform, x, y, anchor);
				return;
			}
			else
			{
				if (SmallImage.smallImg == null)
				{
					if (GameCanvas.currentScreen != GameScr.gI())
					{
						Small small2 = SmallImage.imgNew[id];
						if (small2 == null)
						{
							SmallImage.createImage(id);
							return;
						}
						small2.paint(g, transform, f, x, y, w, h, anchor);
					}
					return;
				}
				if (id < 0 || id >= SmallImage.smallImg.Length || SmallImage.smallImg[id] == null || id >= SmallImage.imgNew.Length || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
				{
					Small small3 = SmallImage.imgNew[id];
					if (small3 == null)
					{
						SmallImage.createImage(id);
						return;
					}
					small3.paint(g, transform, f, x, y, w, h, anchor);
					return;
				}
				else
				{
					if (SmallImage.smallImg[id][0] != 4 && SmallImage.imgbig[SmallImage.smallImg[id][0]] != null)
					{
						g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], 0, f * w, w, h, transform, x, y, anchor);
						return;
					}
					Small small4 = SmallImage.imgNew[id];
					if (small4 == null)
					{
						SmallImage.createImage(id);
						return;
					}
					small4.paint(g, transform, f, x, y, w, h, anchor);
					return;
				}
			}
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x0036FA4C File Offset: 0x0036DC4C
		public static void update()
		{
			int num = 0;
			if (GameCanvas.gameTick % 1000 != 0)
			{
				return;
			}
			for (int i = 0; i < SmallImage.imgNew.Length; i++)
			{
				if (SmallImage.imgNew[i] != null)
				{
					num++;
					SmallImage.imgNew[i].update();
					SmallImage.smallCount++;
				}
			}
			if (num > 200 && GameCanvas.lowGraphic)
			{
				SmallImage.imgNew = new Small[(int)SmallImage.maxSmall];
			}
		}

		// Token: 0x04006CB7 RID: 27831
		public static int[][] smallImg;

		// Token: 0x04006CB8 RID: 27832
		public static SmallImage instance;

		// Token: 0x04006CB9 RID: 27833
		public static Image[] imgbig;

		// Token: 0x04006CBA RID: 27834
		public static Small[] imgNew;

		// Token: 0x04006CBB RID: 27835
		public static MyVector vKeys = new MyVector();

		// Token: 0x04006CBC RID: 27836
		public static Image imgEmpty = null;

		// Token: 0x04006CBD RID: 27837
		public static sbyte[] newSmallVersion;

		// Token: 0x04006CBE RID: 27838
		public static int smallCount;

		// Token: 0x04006CBF RID: 27839
		public static short maxSmall;

		// Token: 0x04006CC0 RID: 27840
		public static Dictionary<int, Image> imageRaw = new Dictionary<int, Image>();
	}
}
