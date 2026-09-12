using System;
using UnityEngine;

namespace Game6
{
	// Token: 0x0200007B RID: 123
	public class mSystem
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void AddIpTest()
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0005A4E4 File Offset: 0x000586E4
		public static void resetCurInapp()
		{
			mSystem.curINAPP = 0;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0005A4EC File Offset: 0x000586EC
		public static string getTimeCountDown(long timeStart, int secondCount, bool isOnlySecond, bool isShortText)
		{
			string result = string.Empty;
			long num = (timeStart + (long)(secondCount * 1000) - mSystem.currentTimeMillis()) / 1000L;
			if (num <= 0L)
			{
				return string.Empty;
			}
			long num2 = 0L;
			long num3 = 0L;
			long num4 = num / 60L;
			long num5 = num;
			if (isOnlySecond)
			{
				return num5.ToString() + string.Empty;
			}
			if (num >= 86400L)
			{
				num2 = num / 86400L;
				num3 = num % 86400L / 3600L;
			}
			else if (num >= 3600L)
			{
				num3 = num / 3600L;
				num4 = num % 3600L / 60L;
			}
			else if (num >= 60L)
			{
				num4 = num / 60L;
				num5 = num % 60L;
			}
			else
			{
				num5 = num;
			}
			if (isShortText)
			{
				if (num2 > 0L)
				{
					return num2.ToString() + "d";
				}
				if (num3 > 0L)
				{
					return num3.ToString() + "h";
				}
				if (num4 > 0L)
				{
					return num4.ToString() + "m";
				}
				if (num5 > 0L)
				{
					return num5.ToString() + "s";
				}
			}
			if (num2 > 0L)
			{
				if (num2 >= 10L)
				{
					result = ((num3 < 1L) ? (num2.ToString() + "d") : ((num3 >= 10L) ? (num2.ToString() + "d" + num3.ToString() + "h") : (num2.ToString() + "d0" + num3.ToString() + "h")));
				}
				else if (num2 < 10L)
				{
					result = ((num3 < 1L) ? (num2.ToString() + "d") : ((num3 >= 10L) ? (num2.ToString() + "d" + num3.ToString() + "h") : (num2.ToString() + "d0" + num3.ToString() + "h")));
				}
			}
			else if (num3 > 0L)
			{
				if (num3 >= 10L)
				{
					result = ((num4 < 1L) ? (num3.ToString() + "h") : ((num4 >= 10L) ? (num3.ToString() + "h" + num4.ToString() + "m") : (num3.ToString() + "h0" + num4.ToString() + "m")));
				}
				else if (num3 < 10L)
				{
					result = ((num4 < 1L) ? (num3.ToString() + "h") : ((num4 >= 10L) ? (num3.ToString() + "h" + num4.ToString() + "m") : (num3.ToString() + "h0" + num4.ToString() + "m")));
				}
			}
			else if (num4 > 0L)
			{
				if (num4 >= 10L)
				{
					if (num5 >= 10L)
					{
						result = num4.ToString() + "m" + num5.ToString() + string.Empty;
					}
					else if (num5 < 10L)
					{
						result = num4.ToString() + "m0" + num5.ToString() + string.Empty;
					}
				}
				else if (num4 < 10L)
				{
					if (num5 >= 10L)
					{
						result = num4.ToString() + "m" + num5.ToString() + string.Empty;
					}
					else if (num5 < 10L)
					{
						result = num4.ToString() + "m0" + num5.ToString() + string.Empty;
					}
				}
			}
			else
			{
				result = ((num5 >= 10L) ? (num5.ToString() + string.Empty) : ("0" + num5.ToString() + string.Empty));
			}
			return result;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0005A8A8 File Offset: 0x00058AA8
		public static string numberTostring(long number)
		{
			string text = string.Empty + number.ToString();
			bool flag = false;
			try
			{
				string empty = string.Empty;
				if (number < 0L)
				{
					flag = true;
					number = -number;
					text = string.Empty + number.ToString();
				}
				int num;
				if (number >= 1000000000L)
				{
					empty = "b";
					number /= 1000000000L;
					num = (string.Empty + number.ToString()).Length;
				}
				else if (number >= 1000000L)
				{
					empty = "m";
					number /= 1000000L;
					num = (string.Empty + number.ToString()).Length;
				}
				else if (number < 1000L)
				{
					if (flag)
					{
						return "-" + text;
					}
					return text;
				}
				else
				{
					empty = "k";
					number /= 1000L;
					num = (string.Empty + number.ToString()).Length;
				}
				int num2 = int.Parse(text.Substring(num, 2));
				text = ((num2 == 0) ? (text.Substring(0, num) + empty) : ((num2 % 10 != 0) ? (text.Substring(0, num) + "," + text.Substring(num, 2) + empty) : (text.Substring(0, num) + "," + text.Substring(num, 1) + empty)));
			}
			catch (Exception)
			{
			}
			if (flag)
			{
				return "-" + text;
			}
			return text;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0005AA38 File Offset: 0x00058C38
		public static void callHotlinePC()
		{
			Application.OpenURL(ServerListScreen.linkweb);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineJava()
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineIphone()
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineWindowsPhone()
		{
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void closeBanner()
		{
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void createAdmob()
		{
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void checkAdComlete()
		{
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0005AA44 File Offset: 0x00058C44
		public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
		{
			g.fillRect(x, y, w + 10, h, 0, 90);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0005AA58 File Offset: 0x00058C58
		public static long currentTimeMillis()
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0005AA93 File Offset: 0x00058C93
		public static void println(object str)
		{
			Debug.Log(str);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0005AA9B File Offset: 0x00058C9B
		public static void gcc()
		{
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0005AAA8 File Offset: 0x00058CA8
		public static void onConnectOK()
		{
			Controller.isConnectOK = true;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0005AAB0 File Offset: 0x00058CB0
		public static void onConnectionFail()
		{
			Controller.isConnectionFail = true;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0005AAB8 File Offset: 0x00058CB8
		public static void onDisconnected()
		{
			Controller.isDisconnected = true;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void exitWP()
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0005AAC0 File Offset: 0x00058CC0
		public static void paintFlyText(mGraphics g)
		{
			for (int i = 0; i < 5; i++)
			{
				if (GameScr.flyTextState[i] != -1 && GameCanvas.isPaint(GameScr.flyTextX[i], GameScr.flyTextY[i]))
				{
					if (GameScr.flyTextColor[i] == mFont.RED)
					{
						mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.YELLOW)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.GREEN)
					{
						mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.FATAL)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.FATAL_ME)
					{
						mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.MISS)
					{
						mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.tahoma_7_grey);
					}
					else if (GameScr.flyTextColor[i] == mFont.ORANGE)
					{
						mFont.bigNumber_orange.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.ADDMONEY)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.MISS_ME)
					{
						mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.HP)
					{
						mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.MP)
					{
						mFont.bigNumber_blue.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
				}
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void endKey()
		{
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0005AD84 File Offset: 0x00058F84
		public static FrameImage getFraImage(string nameImg)
		{
			FrameImage result = null;
			MainImage mainImage = null;
			if (mainImage == null)
			{
				mainImage = ImgByName.getImagePath(nameImg, ImgByName.hashImagePath);
			}
			if (mainImage.img != null)
			{
				int num = mainImage.img.getHeight() / (int)mainImage.nFrame;
				if (num < 1)
				{
					num = 1;
				}
				result = new FrameImage(mainImage.img, mainImage.img.getWidth(), num);
			}
			return result;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0005ADDE File Offset: 0x00058FDE
		public static Image loadImage(string path)
		{
			return GameCanvas.loadImage(path);
		}

		// Token: 0x04000CD8 RID: 3288
		public static bool isTest;

		// Token: 0x04000CD9 RID: 3289
		public static string strAdmob;

		// Token: 0x04000CDA RID: 3290
		public static string publicID;

		// Token: 0x04000CDB RID: 3291
		public static string android_pack;

		// Token: 0x04000CDC RID: 3292
		public static int clientType = 4;

		// Token: 0x04000CDD RID: 3293
		public static sbyte curINAPP;

		// Token: 0x04000CDE RID: 3294
		public static sbyte maxINAPP = 5;
	}
}
