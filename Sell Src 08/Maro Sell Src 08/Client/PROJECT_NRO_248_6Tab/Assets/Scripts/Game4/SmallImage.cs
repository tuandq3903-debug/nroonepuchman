using System;
using System.Collections.Generic;
using Game4.Assets.src.e;

namespace Game4
{
	// Token: 0x02000260 RID: 608
	public class SmallImage
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x001B0358 File Offset: 0x001AE558
		public SmallImage()
		{
			this.readImage();
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x001B0368 File Offset: 0x001AE568
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

		// Token: 0x06001B51 RID: 6993 RVA: 0x001B03C8 File Offset: 0x001AE5C8
		public static void loadBigImage()
		{
			SmallImage.imgEmpty = Image.createRGBImage(new int[1], 1, 1, true);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x001B03DD File Offset: 0x001AE5DD
		public static void init()
		{
			SmallImage.instance = null;
			SmallImage.instance = new SmallImage();
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x001B03F0 File Offset: 0x001AE5F0
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

		// Token: 0x06001B54 RID: 6996 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void clearHastable()
		{
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x001B04DC File Offset: 0x001AE6DC
		public static void createImage(int id)
		{
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

		// Token: 0x06001B56 RID: 6998 RVA: 0x001B05C4 File Offset: 0x001AE7C4
		public static void drawSmallImage(mGraphics g, int id, int x, int y, int transform, int anchor)
		{
			if (SmallImage.imgbig != null)
			{
				if (SmallImage.smallImg != null)
				{
					if (id >= SmallImage.smallImg.Length || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
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

		// Token: 0x06001B57 RID: 6999 RVA: 0x001B0700 File Offset: 0x001AE900
		public static void drawSmallImage(mGraphics g, int id, int f, int x, int y, int w, int h, int transform, int anchor)
		{
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
				if (id >= SmallImage.smallImg.Length || SmallImage.smallImg[id] == null || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
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

		// Token: 0x06001B58 RID: 7000 RVA: 0x001B0860 File Offset: 0x001AEA60
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

		// Token: 0x0400353A RID: 13626
		public static int[][] smallImg;

		// Token: 0x0400353B RID: 13627
		public static SmallImage instance;

		// Token: 0x0400353C RID: 13628
		public static Image[] imgbig;

		// Token: 0x0400353D RID: 13629
		public static Small[] imgNew;

		// Token: 0x0400353E RID: 13630
		public static MyVector vKeys = new MyVector();

		// Token: 0x0400353F RID: 13631
		public static Image imgEmpty = null;

		// Token: 0x04003540 RID: 13632
		public static sbyte[] newSmallVersion;

		// Token: 0x04003541 RID: 13633
		public static int smallCount;

		// Token: 0x04003542 RID: 13634
		public static short maxSmall;

		// Token: 0x04003543 RID: 13635
		public static Dictionary<int, Image> imageRaw = new Dictionary<int, Image>();
	}
}
