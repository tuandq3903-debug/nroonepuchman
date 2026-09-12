using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x020004B3 RID: 1203
	public class mSystem
	{
		// Token: 0x0600356D RID: 13677 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void AddIpTest()
		{
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x003438F0 File Offset: 0x00341AF0
		public static void resetCurInapp()
		{
			mSystem.curINAPP = 0;
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x003438F8 File Offset: 0x00341AF8
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

		// Token: 0x06003570 RID: 13680 RVA: 0x00343CB4 File Offset: 0x00341EB4
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

		// Token: 0x06003571 RID: 13681 RVA: 0x00343E44 File Offset: 0x00342044
		public static void callHotlinePC()
		{
			Application.OpenURL(ServerListScreen.linkweb);
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineJava()
		{
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineIphone()
		{
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void callHotlineWindowsPhone()
		{
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void closeBanner()
		{
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void createAdmob()
		{
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void checkAdComlete()
		{
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x00343E50 File Offset: 0x00342050
		public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
		{
			g.fillRect(x, y, w + 10, h, 0, 90);
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00343E64 File Offset: 0x00342064
		public static long currentTimeMillis()
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x0005AA93 File Offset: 0x00058C93
		public static void println(object str)
		{
			Debug.Log(str);
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x0005AA9B File Offset: 0x00058C9B
		public static void gcc()
		{
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x00343E9F File Offset: 0x0034209F
		public static void onConnectOK()
		{
			Controller.isConnectOK = true;
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00343EA7 File Offset: 0x003420A7
		public static void onConnectionFail()
		{
			Controller.isConnectionFail = true;
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x00343EAF File Offset: 0x003420AF
		public static void onDisconnected()
		{
			Controller.isDisconnected = true;
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void exitWP()
		{
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x00343EB8 File Offset: 0x003420B8
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

		// Token: 0x06003581 RID: 13697 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void endKey()
		{
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x0034417C File Offset: 0x0034237C
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

		// Token: 0x06003583 RID: 13699 RVA: 0x003441D6 File Offset: 0x003423D6
		public static Image loadImage(string path)
		{
			return GameCanvas.loadImage(path);
		}

		// Token: 0x04006953 RID: 26963
		public static bool isTest;

		// Token: 0x04006954 RID: 26964
		public static string strAdmob;

		// Token: 0x04006955 RID: 26965
		public static string publicID;

		// Token: 0x04006956 RID: 26966
		public static string android_pack;

		// Token: 0x04006957 RID: 26967
		public static int clientType = 4;

		// Token: 0x04006958 RID: 26968
		public static sbyte curINAPP;

		// Token: 0x04006959 RID: 26969
		public static sbyte maxINAPP = 5;
	}
}
