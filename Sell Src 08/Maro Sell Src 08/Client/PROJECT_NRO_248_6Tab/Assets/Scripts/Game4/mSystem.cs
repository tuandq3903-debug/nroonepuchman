using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x0200022B RID: 555
	public class mSystem
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void AddIpTest()
		{
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00184704 File Offset: 0x00182904
		public static void resetCurInapp()
		{
			mSystem.curINAPP = 0;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0018470C File Offset: 0x0018290C
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

		// Token: 0x06001884 RID: 6276 RVA: 0x00184AC8 File Offset: 0x00182CC8
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

		// Token: 0x06001885 RID: 6277 RVA: 0x00184C58 File Offset: 0x00182E58
		public static void callHotlinePC()
		{
			Application.OpenURL(ServerListScreen.linkweb);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineJava()
		{
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineIphone()
		{
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineWindowsPhone()
		{
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void closeBanner()
		{
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void createAdmob()
		{
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void checkAdComlete()
		{
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00184C64 File Offset: 0x00182E64
		public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
		{
			g.fillRect(x, y, w + 10, h, 0, 90);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00184C78 File Offset: 0x00182E78
		public static long currentTimeMillis()
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0005AA93 File Offset: 0x00058C93
		public static void println(object str)
		{
			Debug.Log(str);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0005AA9B File Offset: 0x00058C9B
		public static void gcc()
		{
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00184CB3 File Offset: 0x00182EB3
		public static void onConnectOK()
		{
			Controller.isConnectOK = true;
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00184CBB File Offset: 0x00182EBB
		public static void onConnectionFail()
		{
			Controller.isConnectionFail = true;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00184CC3 File Offset: 0x00182EC3
		public static void onDisconnected()
		{
			Controller.isDisconnected = true;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void exitWP()
		{
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00184CCC File Offset: 0x00182ECC
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

		// Token: 0x06001895 RID: 6293 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void endKey()
		{
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00184F90 File Offset: 0x00183190
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

		// Token: 0x06001897 RID: 6295 RVA: 0x00184FEA File Offset: 0x001831EA
		public static Image loadImage(string path)
		{
			return GameCanvas.loadImage(path);
		}

		// Token: 0x040031D6 RID: 12758
		public static bool isTest;

		// Token: 0x040031D7 RID: 12759
		public static string strAdmob;

		// Token: 0x040031D8 RID: 12760
		public static string publicID;

		// Token: 0x040031D9 RID: 12761
		public static string android_pack;

		// Token: 0x040031DA RID: 12762
		public static int clientType = 4;

		// Token: 0x040031DB RID: 12763
		public static sbyte curINAPP;

		// Token: 0x040031DC RID: 12764
		public static sbyte maxINAPP = 5;
	}
}
