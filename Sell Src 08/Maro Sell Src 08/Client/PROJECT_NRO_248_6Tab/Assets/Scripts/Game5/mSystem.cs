using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x02000153 RID: 339
	public class mSystem
	{
		// Token: 0x06000EDD RID: 3805 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void AddIpTest()
		{
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000EF660 File Offset: 0x000ED860
		public static void resetCurInapp()
		{
			mSystem.curINAPP = 0;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000EF668 File Offset: 0x000ED868
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

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000EFA24 File Offset: 0x000EDC24
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

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000EFBB4 File Offset: 0x000EDDB4
		public static void callHotlinePC()
		{
			Application.OpenURL(ServerListScreen.linkweb);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineJava()
		{
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineIphone()
		{
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineWindowsPhone()
		{
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void closeBanner()
		{
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void createAdmob()
		{
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void checkAdComlete()
		{
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000EFBC0 File Offset: 0x000EDDC0
		public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
		{
			g.fillRect(x, y, w + 10, h, 0, 90);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000EFBD4 File Offset: 0x000EDDD4
		public static long currentTimeMillis()
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0005AA93 File Offset: 0x00058C93
		public static void println(object str)
		{
			Debug.Log(str);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0005AA9B File Offset: 0x00058C9B
		public static void gcc()
		{
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000EFC0F File Offset: 0x000EDE0F
		public static void onConnectOK()
		{
			Controller.isConnectOK = true;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000EFC17 File Offset: 0x000EDE17
		public static void onConnectionFail()
		{
			Controller.isConnectionFail = true;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000EFC1F File Offset: 0x000EDE1F
		public static void onDisconnected()
		{
			Controller.isDisconnected = true;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void exitWP()
		{
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000EFC28 File Offset: 0x000EDE28
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

		// Token: 0x06000EF1 RID: 3825 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void endKey()
		{
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000EFEEC File Offset: 0x000EE0EC
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

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000EFF46 File Offset: 0x000EE146
		public static Image loadImage(string path)
		{
			return GameCanvas.loadImage(path);
		}

		// Token: 0x04001F57 RID: 8023
		public static bool isTest;

		// Token: 0x04001F58 RID: 8024
		public static string strAdmob;

		// Token: 0x04001F59 RID: 8025
		public static string publicID;

		// Token: 0x04001F5A RID: 8026
		public static string android_pack;

		// Token: 0x04001F5B RID: 8027
		public static int clientType = 4;

		// Token: 0x04001F5C RID: 8028
		public static sbyte curINAPP;

		// Token: 0x04001F5D RID: 8029
		public static sbyte maxINAPP = 5;
	}
}
