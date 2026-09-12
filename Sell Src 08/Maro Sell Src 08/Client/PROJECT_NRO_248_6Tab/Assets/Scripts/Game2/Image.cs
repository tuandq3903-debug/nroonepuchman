using System;
using UnityEngine;

namespace Game2
{
	// Token: 0x020003A4 RID: 932
	public class Image
	{
		// Token: 0x060029E5 RID: 10725 RVA: 0x00298189 File Offset: 0x00296389
		public static Image createImage(string filename)
		{
			return Image.__createImage(filename);
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x00298191 File Offset: 0x00296391
		public static Image createImage(byte[] imageData)
		{
			return Image.__createImage(imageData);
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x00298199 File Offset: 0x00296399
		public static Image createImage(int w, int h)
		{
			return Image.__createImage(w, h);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x002981A4 File Offset: 0x002963A4
		public static Image createImage(sbyte[] imageData, int offset, int lenght)
		{
			if (offset + lenght > imageData.Length)
			{
				return null;
			}
			byte[] array = new byte[lenght];
			for (int i = 0; i < lenght; i++)
			{
				array[i] = Image.convertSbyteToByte(imageData[i + offset]);
			}
			return Image.createImage(array);
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x002981E4 File Offset: 0x002963E4
		public static Image createRGBImage(int[] rbg, int w, int h, bool bl)
		{
			Image image = Image.createImage(w, h);
			Color[] array = new Color[rbg.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Image.setColorFromRBG(rbg[i]);
			}
			image.texture.SetPixels(0, 0, w, h, array);
			image.texture.Apply();
			return image;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00298240 File Offset: 0x00296440
		public static Color setColorFromRBG(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x0029828C File Offset: 0x0029648C
		public static void update()
		{
			if (Image.status == 2)
			{
				Image.status = 1;
				Image.imgTemp = Image.__createEmptyImage();
				Image.status = 0;
				return;
			}
			if (Image.status == 3)
			{
				Image.status = 1;
				Image.imgTemp = Image.__createImage(Image.filenametemp);
				Image.status = 0;
				return;
			}
			if (Image.status == 4)
			{
				Image.status = 1;
				Image.imgTemp = Image.__createImage(Image.datatemp);
				Image.status = 0;
				return;
			}
			if (Image.status == 5)
			{
				Image.status = 1;
				Image.imgTemp = Image.__createImage(Image.imgSrcTemp, Image.xtemp, Image.ytemp, Image.wtemp, Image.htemp, Image.transformtemp);
				Image.status = 0;
				return;
			}
			if (Image.status == 6)
			{
				Image.status = 1;
				Image.imgTemp = Image.__createImage(Image.wtemp, Image.htemp);
				Image.status = 0;
			}
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00298368 File Offset: 0x00296568
		private static Image __createImage(string filename)
		{
			Image image = new Image();
			Texture2D texture2D = Resources.Load(filename) as Texture2D;
			if (texture2D == null)
			{
				throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
			}
			image.texture = texture2D;
			image.w = image.texture.width;
			image.h = image.texture.height;
			Image.setTextureQuality(image);
			return image;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x002983D0 File Offset: 0x002965D0
		private static Image __createImage(byte[] imageData)
		{
			if (imageData == null || imageData.Length == 0)
			{
				Cout.LogError("Create Image from byte array fail");
				return null;
			}
			Image image = new Image();
			try
			{
				image.texture.LoadImage(imageData);
				image.w = image.texture.width;
				image.h = image.texture.height;
				Image.setTextureQuality(image);
			}
			catch (Exception)
			{
				Cout.LogError("CREAT IMAGE FROM ARRAY FAIL \n" + Environment.StackTrace);
			}
			return image;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x00298458 File Offset: 0x00296658
		private static Image __createImage(Image src, int x, int y, int w, int h, int transform)
		{
			Image image = new Image();
			image.texture = new Texture2D(w, h);
			y = src.texture.height - y - h;
			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < h; j++)
				{
					int num = i;
					if (transform == 2)
					{
						num = w - i;
					}
					int num2 = j;
					image.texture.SetPixel(i, j, src.texture.GetPixel(x + num, y + num2));
				}
			}
			image.texture.Apply();
			image.w = image.texture.width;
			image.h = image.texture.height;
			Image.setTextureQuality(image);
			return image;
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x00298505 File Offset: 0x00296705
		private static Image __createEmptyImage()
		{
			return new Image();
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x0029850C File Offset: 0x0029670C
		public static Image __createImage(int w, int h)
		{
			Image image = new Image();
			image.texture = new Texture2D(w, h, TextureFormat.RGBA32, false);
			Image.setTextureQuality(image);
			image.w = w;
			image.h = h;
			image.texture.Apply();
			return image;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00298541 File Offset: 0x00296741
		public int getWidth()
		{
			return this.w / mGraphics.zoomLevel;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x0029854F File Offset: 0x0029674F
		public int getHeight()
		{
			return this.h / mGraphics.zoomLevel;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0029855D File Offset: 0x0029675D
		private static void setTextureQuality(Image img)
		{
			Image.setTextureQuality(img.texture);
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000441B2 File Offset: 0x000423B2
		public static void setTextureQuality(Texture2D texture)
		{
			texture.anisoLevel = 0;
			texture.filterMode = FilterMode.Point;
			texture.mipMapBias = 0f;
			texture.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0029856A File Offset: 0x0029676A
		public int getRealImageWidth()
		{
			return this.w;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x00298572 File Offset: 0x00296772
		public int getRealImageHeight()
		{
			return this.h;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x0029857C File Offset: 0x0029677C
		public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
		{
			Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
			for (int i = 0; i < pixels.Length; i++)
			{
				data[i] = mGraphics.getIntByColor(pixels[i]);
			}
		}

		// Token: 0x040050EF RID: 20719
		private const int INTERVAL = 5;

		// Token: 0x040050F0 RID: 20720
		private const int MAXTIME = 500;

		// Token: 0x040050F1 RID: 20721
		public Texture2D texture = new Texture2D(1, 1);

		// Token: 0x040050F2 RID: 20722
		public static Image imgTemp;

		// Token: 0x040050F3 RID: 20723
		public static string filenametemp;

		// Token: 0x040050F4 RID: 20724
		public static byte[] datatemp;

		// Token: 0x040050F5 RID: 20725
		public static Image imgSrcTemp;

		// Token: 0x040050F6 RID: 20726
		public static int xtemp;

		// Token: 0x040050F7 RID: 20727
		public static int ytemp;

		// Token: 0x040050F8 RID: 20728
		public static int wtemp;

		// Token: 0x040050F9 RID: 20729
		public static int htemp;

		// Token: 0x040050FA RID: 20730
		public static int transformtemp;

		// Token: 0x040050FB RID: 20731
		public int w;

		// Token: 0x040050FC RID: 20732
		public int h;

		// Token: 0x040050FD RID: 20733
		public static int status;

		// Token: 0x040050FE RID: 20734
		public Color colorBlend = Color.black;
	}
}
