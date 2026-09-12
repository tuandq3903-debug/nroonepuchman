using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x020001F4 RID: 500
	public class Image
	{
		// Token: 0x0600169D RID: 5789 RVA: 0x0016E041 File Offset: 0x0016C241
		public static Image createImage(string filename)
		{
			return Image.__createImage(filename);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0016E049 File Offset: 0x0016C249
		public static Image createImage(byte[] imageData)
		{
			return Image.__createImage(imageData);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0016E051 File Offset: 0x0016C251
		public static Image createImage(int w, int h)
		{
			return Image.__createImage(w, h);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0016E05C File Offset: 0x0016C25C
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

		// Token: 0x060016A1 RID: 5793 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0016E09C File Offset: 0x0016C29C
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

		// Token: 0x060016A3 RID: 5795 RVA: 0x0016E0F8 File Offset: 0x0016C2F8
		public static Color setColorFromRBG(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0016E144 File Offset: 0x0016C344
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

		// Token: 0x060016A5 RID: 5797 RVA: 0x0016E220 File Offset: 0x0016C420
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

		// Token: 0x060016A6 RID: 5798 RVA: 0x0016E288 File Offset: 0x0016C488
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

		// Token: 0x060016A7 RID: 5799 RVA: 0x0016E310 File Offset: 0x0016C510
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

		// Token: 0x060016A8 RID: 5800 RVA: 0x0016E3BD File Offset: 0x0016C5BD
		private static Image __createEmptyImage()
		{
			return new Image();
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0016E3C4 File Offset: 0x0016C5C4
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

		// Token: 0x060016AA RID: 5802 RVA: 0x0016E3F9 File Offset: 0x0016C5F9
		public int getWidth()
		{
			return this.w / mGraphics.zoomLevel;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0016E407 File Offset: 0x0016C607
		public int getHeight()
		{
			return this.h / mGraphics.zoomLevel;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0016E415 File Offset: 0x0016C615
		private static void setTextureQuality(Image img)
		{
			Image.setTextureQuality(img.texture);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000441B2 File Offset: 0x000423B2
		public static void setTextureQuality(Texture2D texture)
		{
			texture.anisoLevel = 0;
			texture.filterMode = FilterMode.Point;
			texture.mipMapBias = 0f;
			texture.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0016E422 File Offset: 0x0016C622
		public int getRealImageWidth()
		{
			return this.w;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0016E42A File Offset: 0x0016C62A
		public int getRealImageHeight()
		{
			return this.h;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0016E434 File Offset: 0x0016C634
		public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
		{
			Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
			for (int i = 0; i < pixels.Length; i++)
			{
				data[i] = mGraphics.getIntByColor(pixels[i]);
			}
		}

		// Token: 0x04002BF1 RID: 11249
		private const int INTERVAL = 5;

		// Token: 0x04002BF2 RID: 11250
		private const int MAXTIME = 500;

		// Token: 0x04002BF3 RID: 11251
		public Texture2D texture = new Texture2D(1, 1);

		// Token: 0x04002BF4 RID: 11252
		public static Image imgTemp;

		// Token: 0x04002BF5 RID: 11253
		public static string filenametemp;

		// Token: 0x04002BF6 RID: 11254
		public static byte[] datatemp;

		// Token: 0x04002BF7 RID: 11255
		public static Image imgSrcTemp;

		// Token: 0x04002BF8 RID: 11256
		public static int xtemp;

		// Token: 0x04002BF9 RID: 11257
		public static int ytemp;

		// Token: 0x04002BFA RID: 11258
		public static int wtemp;

		// Token: 0x04002BFB RID: 11259
		public static int htemp;

		// Token: 0x04002BFC RID: 11260
		public static int transformtemp;

		// Token: 0x04002BFD RID: 11261
		public int w;

		// Token: 0x04002BFE RID: 11262
		public int h;

		// Token: 0x04002BFF RID: 11263
		public static int status;

		// Token: 0x04002C00 RID: 11264
		public Color colorBlend = Color.black;
	}
}
