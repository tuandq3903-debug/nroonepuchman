using System;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000044 RID: 68
	public class Image
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00043DC1 File Offset: 0x00041FC1
		public static Image createImage(string filename)
		{
			return Image.__createImage(filename);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00043DC9 File Offset: 0x00041FC9
		public static Image createImage(byte[] imageData)
		{
			return Image.__createImage(imageData);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00043DD1 File Offset: 0x00041FD1
		public static Image createImage(int w, int h)
		{
			return Image.__createImage(w, h);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00043DDC File Offset: 0x00041FDC
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

		// Token: 0x06000359 RID: 857 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00043E2C File Offset: 0x0004202C
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

		// Token: 0x0600035B RID: 859 RVA: 0x00043E88 File Offset: 0x00042088
		public static Color setColorFromRBG(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00043ED4 File Offset: 0x000420D4
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

		// Token: 0x0600035D RID: 861 RVA: 0x00043FB0 File Offset: 0x000421B0
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

		// Token: 0x0600035E RID: 862 RVA: 0x00044018 File Offset: 0x00042218
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

		// Token: 0x0600035F RID: 863 RVA: 0x000440A0 File Offset: 0x000422A0
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

		// Token: 0x06000360 RID: 864 RVA: 0x0004414D File Offset: 0x0004234D
		private static Image __createEmptyImage()
		{
			return new Image();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00044154 File Offset: 0x00042354
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

		// Token: 0x06000362 RID: 866 RVA: 0x00044189 File Offset: 0x00042389
		public int getWidth()
		{
			return this.w / mGraphics.zoomLevel;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00044197 File Offset: 0x00042397
		public int getHeight()
		{
			return this.h / mGraphics.zoomLevel;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000441A5 File Offset: 0x000423A5
		private static void setTextureQuality(Image img)
		{
			Image.setTextureQuality(img.texture);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000441B2 File Offset: 0x000423B2
		public static void setTextureQuality(Texture2D texture)
		{
			texture.anisoLevel = 0;
			texture.filterMode = FilterMode.Point;
			texture.mipMapBias = 0f;
			texture.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000441D4 File Offset: 0x000423D4
		public int getRealImageWidth()
		{
			return this.w;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000441DC File Offset: 0x000423DC
		public int getRealImageHeight()
		{
			return this.h;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000441E4 File Offset: 0x000423E4
		public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
		{
			Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
			for (int i = 0; i < pixels.Length; i++)
			{
				data[i] = mGraphics.getIntByColor(pixels[i]);
			}
		}

		// Token: 0x040006F3 RID: 1779
		private const int INTERVAL = 5;

		// Token: 0x040006F4 RID: 1780
		private const int MAXTIME = 500;

		// Token: 0x040006F5 RID: 1781
		public Texture2D texture = new Texture2D(1, 1);

		// Token: 0x040006F6 RID: 1782
		public static Image imgTemp;

		// Token: 0x040006F7 RID: 1783
		public static string filenametemp;

		// Token: 0x040006F8 RID: 1784
		public static byte[] datatemp;

		// Token: 0x040006F9 RID: 1785
		public static Image imgSrcTemp;

		// Token: 0x040006FA RID: 1786
		public static int xtemp;

		// Token: 0x040006FB RID: 1787
		public static int ytemp;

		// Token: 0x040006FC RID: 1788
		public static int wtemp;

		// Token: 0x040006FD RID: 1789
		public static int htemp;

		// Token: 0x040006FE RID: 1790
		public static int transformtemp;

		// Token: 0x040006FF RID: 1791
		public int w;

		// Token: 0x04000700 RID: 1792
		public int h;

		// Token: 0x04000701 RID: 1793
		public static int status;

		// Token: 0x04000702 RID: 1794
		public Color colorBlend = Color.black;
	}
}
