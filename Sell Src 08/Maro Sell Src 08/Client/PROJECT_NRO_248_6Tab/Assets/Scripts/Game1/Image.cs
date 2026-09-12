using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x0200047C RID: 1148
	public class Image
	{
		// Token: 0x06003389 RID: 13193 RVA: 0x0032D22D File Offset: 0x0032B42D
		public static Image createImage(string filename)
		{
			return Image.__createImage(filename);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x0032D235 File Offset: 0x0032B435
		public static Image createImage(byte[] imageData)
		{
			return Image.__createImage(imageData);
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x0032D23D File Offset: 0x0032B43D
		public static Image createImage(int w, int h)
		{
			return Image.__createImage(w, h);
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x0032D248 File Offset: 0x0032B448
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

		// Token: 0x0600338D RID: 13197 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x0032D288 File Offset: 0x0032B488
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

		// Token: 0x0600338F RID: 13199 RVA: 0x0032D2E4 File Offset: 0x0032B4E4
		public static Color setColorFromRBG(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x0032D330 File Offset: 0x0032B530
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

		// Token: 0x06003391 RID: 13201 RVA: 0x0032D40C File Offset: 0x0032B60C
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

		// Token: 0x06003392 RID: 13202 RVA: 0x0032D474 File Offset: 0x0032B674
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

		// Token: 0x06003393 RID: 13203 RVA: 0x0032D4FC File Offset: 0x0032B6FC
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

		// Token: 0x06003394 RID: 13204 RVA: 0x0032D5A9 File Offset: 0x0032B7A9
		private static Image __createEmptyImage()
		{
			return new Image();
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x0032D5B0 File Offset: 0x0032B7B0
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

		// Token: 0x06003396 RID: 13206 RVA: 0x0032D5E5 File Offset: 0x0032B7E5
		public int getWidth()
		{
			return this.w / mGraphics.zoomLevel;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x0032D5F3 File Offset: 0x0032B7F3
		public int getHeight()
		{
			return this.h / mGraphics.zoomLevel;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x0032D601 File Offset: 0x0032B801
		private static void setTextureQuality(Image img)
		{
			Image.setTextureQuality(img.texture);
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000441B2 File Offset: 0x000423B2
		public static void setTextureQuality(Texture2D texture)
		{
			texture.anisoLevel = 0;
			texture.filterMode = FilterMode.Point;
			texture.mipMapBias = 0f;
			texture.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x0032D60E File Offset: 0x0032B80E
		public int getRealImageWidth()
		{
			return this.w;
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x0032D616 File Offset: 0x0032B816
		public int getRealImageHeight()
		{
			return this.h;
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x0032D620 File Offset: 0x0032B820
		public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
		{
			Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
			for (int i = 0; i < pixels.Length; i++)
			{
				data[i] = mGraphics.getIntByColor(pixels[i]);
			}
		}

		// Token: 0x0400636E RID: 25454
		private const int INTERVAL = 5;

		// Token: 0x0400636F RID: 25455
		private const int MAXTIME = 500;

		// Token: 0x04006370 RID: 25456
		public Texture2D texture = new Texture2D(1, 1);

		// Token: 0x04006371 RID: 25457
		public static Image imgTemp;

		// Token: 0x04006372 RID: 25458
		public static string filenametemp;

		// Token: 0x04006373 RID: 25459
		public static byte[] datatemp;

		// Token: 0x04006374 RID: 25460
		public static Image imgSrcTemp;

		// Token: 0x04006375 RID: 25461
		public static int xtemp;

		// Token: 0x04006376 RID: 25462
		public static int ytemp;

		// Token: 0x04006377 RID: 25463
		public static int wtemp;

		// Token: 0x04006378 RID: 25464
		public static int htemp;

		// Token: 0x04006379 RID: 25465
		public static int transformtemp;

		// Token: 0x0400637A RID: 25466
		public int w;

		// Token: 0x0400637B RID: 25467
		public int h;

		// Token: 0x0400637C RID: 25468
		public static int status;

		// Token: 0x0400637D RID: 25469
		public Color colorBlend = Color.black;
	}
}
