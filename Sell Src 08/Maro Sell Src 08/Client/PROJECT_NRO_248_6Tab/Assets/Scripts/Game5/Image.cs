using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x0200011C RID: 284
	public class Image
	{
		// Token: 0x06000CF9 RID: 3321 RVA: 0x000D8F9D File Offset: 0x000D719D
		public static Image createImage(string filename)
		{
			return Image.__createImage(filename);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000D8FA5 File Offset: 0x000D71A5
		public static Image createImage(byte[] imageData)
		{
			return Image.__createImage(imageData);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000D8FAD File Offset: 0x000D71AD
		public static Image createImage(int w, int h)
		{
			return Image.__createImage(w, h);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000D8FB8 File Offset: 0x000D71B8
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

		// Token: 0x06000CFD RID: 3325 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000D8FF8 File Offset: 0x000D71F8
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

		// Token: 0x06000CFF RID: 3327 RVA: 0x000D9054 File Offset: 0x000D7254
		public static Color setColorFromRBG(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000D90A0 File Offset: 0x000D72A0
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

		// Token: 0x06000D01 RID: 3329 RVA: 0x000D917C File Offset: 0x000D737C
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

		// Token: 0x06000D02 RID: 3330 RVA: 0x000D91E4 File Offset: 0x000D73E4
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

		// Token: 0x06000D03 RID: 3331 RVA: 0x000D926C File Offset: 0x000D746C
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

		// Token: 0x06000D04 RID: 3332 RVA: 0x000D9319 File Offset: 0x000D7519
		private static Image __createEmptyImage()
		{
			return new Image();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000D9320 File Offset: 0x000D7520
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

		// Token: 0x06000D06 RID: 3334 RVA: 0x000D9355 File Offset: 0x000D7555
		public int getWidth()
		{
			return this.w / mGraphics.zoomLevel;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000D9363 File Offset: 0x000D7563
		public int getHeight()
		{
			return this.h / mGraphics.zoomLevel;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000D9371 File Offset: 0x000D7571
		private static void setTextureQuality(Image img)
		{
			Image.setTextureQuality(img.texture);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000441B2 File Offset: 0x000423B2
		public static void setTextureQuality(Texture2D texture)
		{
			texture.anisoLevel = 0;
			texture.filterMode = FilterMode.Point;
			texture.mipMapBias = 0f;
			texture.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000D937E File Offset: 0x000D757E
		public int getRealImageWidth()
		{
			return this.w;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000D9386 File Offset: 0x000D7586
		public int getRealImageHeight()
		{
			return this.h;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000D9390 File Offset: 0x000D7590
		public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
		{
			Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
			for (int i = 0; i < pixels.Length; i++)
			{
				data[i] = mGraphics.getIntByColor(pixels[i]);
			}
		}

		// Token: 0x04001972 RID: 6514
		private const int INTERVAL = 5;

		// Token: 0x04001973 RID: 6515
		private const int MAXTIME = 500;

		// Token: 0x04001974 RID: 6516
		public Texture2D texture = new Texture2D(1, 1);

		// Token: 0x04001975 RID: 6517
		public static Image imgTemp;

		// Token: 0x04001976 RID: 6518
		public static string filenametemp;

		// Token: 0x04001977 RID: 6519
		public static byte[] datatemp;

		// Token: 0x04001978 RID: 6520
		public static Image imgSrcTemp;

		// Token: 0x04001979 RID: 6521
		public static int xtemp;

		// Token: 0x0400197A RID: 6522
		public static int ytemp;

		// Token: 0x0400197B RID: 6523
		public static int wtemp;

		// Token: 0x0400197C RID: 6524
		public static int htemp;

		// Token: 0x0400197D RID: 6525
		public static int transformtemp;

		// Token: 0x0400197E RID: 6526
		public int w;

		// Token: 0x0400197F RID: 6527
		public int h;

		// Token: 0x04001980 RID: 6528
		public static int status;

		// Token: 0x04001981 RID: 6529
		public Color colorBlend = Color.black;
	}
}
