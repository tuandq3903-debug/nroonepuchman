using System;
using System.Collections.Generic;
using Game2.Assets.src.e;

namespace Game2
{
	// Token: 0x02000410 RID: 1040
	public class SmallImage
	{
		// Token: 0x06002E97 RID: 11927 RVA: 0x002DA4A0 File Offset: 0x002D86A0
		public SmallImage()
		{
			this.readImage();
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x002DA4B0 File Offset: 0x002D86B0
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

		// Token: 0x06002E99 RID: 11929 RVA: 0x002DA510 File Offset: 0x002D8710
		public static void loadBigImage()
		{
			SmallImage.imgEmpty = Image.createRGBImage(new int[1], 1, 1, true);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x002DA525 File Offset: 0x002D8725
		public static void init()
		{
			SmallImage.instance = null;
			SmallImage.instance = new SmallImage();
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x002DA538 File Offset: 0x002D8738
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

		// Token: 0x06002E9C RID: 11932 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void clearHastable()
		{
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x002DA624 File Offset: 0x002D8824
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

		// Token: 0x06002E9E RID: 11934 RVA: 0x002DA70C File Offset: 0x002D890C
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

		// Token: 0x06002E9F RID: 11935 RVA: 0x002DA848 File Offset: 0x002D8A48
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

		// Token: 0x06002EA0 RID: 11936 RVA: 0x002DA9A8 File Offset: 0x002D8BA8
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

		// Token: 0x04005A38 RID: 23096
		public static int[][] smallImg;

		// Token: 0x04005A39 RID: 23097
		public static SmallImage instance;

		// Token: 0x04005A3A RID: 23098
		public static Image[] imgbig;

		// Token: 0x04005A3B RID: 23099
		public static Small[] imgNew;

		// Token: 0x04005A3C RID: 23100
		public static MyVector vKeys = new MyVector();

		// Token: 0x04005A3D RID: 23101
		public static Image imgEmpty = null;

		// Token: 0x04005A3E RID: 23102
		public static sbyte[] newSmallVersion;

		// Token: 0x04005A3F RID: 23103
		public static int smallCount;

		// Token: 0x04005A40 RID: 23104
		public static short maxSmall;

		// Token: 0x04005A41 RID: 23105
		public static Dictionary<int, Image> imageRaw = new Dictionary<int, Image>();
	}
}
