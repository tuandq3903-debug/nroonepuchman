using System;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002CC RID: 716
	public class Image
	{
		// Token: 0x06002041 RID: 8257 RVA: 0x002030E5 File Offset: 0x002012E5
		public static Image createImage(string filename)
		{
			return Image.__createImage(filename);
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x002030ED File Offset: 0x002012ED
		public static Image createImage(byte[] imageData)
		{
			return Image.__createImage(imageData);
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x002030F5 File Offset: 0x002012F5
		public static Image createImage(int w, int h)
		{
			return Image.__createImage(w, h);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00203100 File Offset: 0x00201300
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

		// Token: 0x06002045 RID: 8261 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00203140 File Offset: 0x00201340
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

		// Token: 0x06002047 RID: 8263 RVA: 0x0020319C File Offset: 0x0020139C
		public static Color setColorFromRBG(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x002031E8 File Offset: 0x002013E8
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

		// Token: 0x06002049 RID: 8265 RVA: 0x002032C4 File Offset: 0x002014C4
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

		// Token: 0x0600204A RID: 8266 RVA: 0x0020332C File Offset: 0x0020152C
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

		// Token: 0x0600204B RID: 8267 RVA: 0x002033B4 File Offset: 0x002015B4
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

		// Token: 0x0600204C RID: 8268 RVA: 0x00203461 File Offset: 0x00201661
		private static Image __createEmptyImage()
		{
			return new Image();
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00203468 File Offset: 0x00201668
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

		// Token: 0x0600204E RID: 8270 RVA: 0x0020349D File Offset: 0x0020169D
		public int getWidth()
		{
			return this.w / mGraphics.zoomLevel;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x002034AB File Offset: 0x002016AB
		public int getHeight()
		{
			return this.h / mGraphics.zoomLevel;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x002034B9 File Offset: 0x002016B9
		private static void setTextureQuality(Image img)
		{
			Image.setTextureQuality(img.texture);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000441B2 File Offset: 0x000423B2
		public static void setTextureQuality(Texture2D texture)
		{
			texture.anisoLevel = 0;
			texture.filterMode = FilterMode.Point;
			texture.mipMapBias = 0f;
			texture.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x002034C6 File Offset: 0x002016C6
		public int getRealImageWidth()
		{
			return this.w;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x002034CE File Offset: 0x002016CE
		public int getRealImageHeight()
		{
			return this.h;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x002034D8 File Offset: 0x002016D8
		public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
		{
			Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
			for (int i = 0; i < pixels.Length; i++)
			{
				data[i] = mGraphics.getIntByColor(pixels[i]);
			}
		}

		// Token: 0x04003E70 RID: 15984
		private const int INTERVAL = 5;

		// Token: 0x04003E71 RID: 15985
		private const int MAXTIME = 500;

		// Token: 0x04003E72 RID: 15986
		public Texture2D texture = new Texture2D(1, 1);

		// Token: 0x04003E73 RID: 15987
		public static Image imgTemp;

		// Token: 0x04003E74 RID: 15988
		public static string filenametemp;

		// Token: 0x04003E75 RID: 15989
		public static byte[] datatemp;

		// Token: 0x04003E76 RID: 15990
		public static Image imgSrcTemp;

		// Token: 0x04003E77 RID: 15991
		public static int xtemp;

		// Token: 0x04003E78 RID: 15992
		public static int ytemp;

		// Token: 0x04003E79 RID: 15993
		public static int wtemp;

		// Token: 0x04003E7A RID: 15994
		public static int htemp;

		// Token: 0x04003E7B RID: 15995
		public static int transformtemp;

		// Token: 0x04003E7C RID: 15996
		public int w;

		// Token: 0x04003E7D RID: 15997
		public int h;

		// Token: 0x04003E7E RID: 15998
		public static int status;

		// Token: 0x04003E7F RID: 15999
		public Color colorBlend = Color.black;
	}
}
