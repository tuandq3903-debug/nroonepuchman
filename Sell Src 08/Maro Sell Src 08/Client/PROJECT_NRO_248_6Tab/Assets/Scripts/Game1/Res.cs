using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x020004CF RID: 1231
	public class Res
	{
		// Token: 0x06003727 RID: 14119 RVA: 0x0036674C File Offset: 0x0036494C
		public static void init()
		{
			Res.cosz = new short[91];
			Res.tanz = new int[91];
			for (int i = 0; i <= 90; i++)
			{
				Res.cosz[i] = Res.sinz[90 - i];
				if (Res.cosz[i] == 0)
				{
					Res.tanz[i] = int.MaxValue;
				}
				else
				{
					Res.tanz[i] = ((int)Res.sinz[i] << 10) / (int)Res.cosz[i];
				}
			}
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x003667C0 File Offset: 0x003649C0
		public static int sin(int a)
		{
			a = Res.fixangle(a);
			if (a >= 0 && a < 90)
			{
				return (int)Res.sinz[a];
			}
			if (a >= 90 && a < 180)
			{
				return (int)Res.sinz[180 - a];
			}
			if (a >= 180 && a < 270)
			{
				return (int)(-(int)Res.sinz[a - 180]);
			}
			return (int)(-(int)Res.sinz[360 - a]);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x00366830 File Offset: 0x00364A30
		public static int cos(int a)
		{
			a = Res.fixangle(a);
			if (a >= 0 && a < 90)
			{
				return (int)Res.cosz[a];
			}
			if (a >= 90 && a < 180)
			{
				return (int)(-(int)Res.cosz[180 - a]);
			}
			if (a >= 180 && a < 270)
			{
				return (int)(-(int)Res.cosz[a - 180]);
			}
			return (int)Res.cosz[360 - a];
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x003668A0 File Offset: 0x00364AA0
		public static int atan(int a)
		{
			for (int i = 0; i <= 90; i++)
			{
				if (Res.tanz[i] >= a)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x003668C8 File Offset: 0x00364AC8
		public static int angle(int dx, int dy)
		{
			int num;
			if (dx != 0)
			{
				num = Res.atan(Math.abs((dy << 10) / dx));
				if (dy >= 0 && dx < 0)
				{
					num = 180 - num;
				}
				if (dy < 0 && dx < 0)
				{
					num = 180 + num;
				}
				if (dy < 0 && dx >= 0)
				{
					num = 360 - num;
				}
			}
			else
			{
				num = ((dy <= 0) ? 270 : 90);
			}
			return num;
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x0007D542 File Offset: 0x0007B742
		public static int fixangle(int angle)
		{
			if (angle >= 360)
			{
				angle -= 360;
			}
			if (angle < 0)
			{
				angle += 360;
			}
			return angle;
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x003108D5 File Offset: 0x0030EAD5
		public static void outz(string s)
		{
			if (mSystem.isTest)
			{
				Debug.Log(s);
			}
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x003108E4 File Offset: 0x0030EAE4
		public static void err(string s)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(s);
			}
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void outz2(string s)
		{
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void paintOnScreenDebug(mGraphics g)
		{
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void updateOnScreenDebug()
		{
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x0007D563 File Offset: 0x0007B763
		public static string changeString(string str)
		{
			return str;
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x0005CA84 File Offset: 0x0005AC84
		public static string replace(string _text, string _searchStr, string _replacementStr)
		{
			return _text.Replace(_searchStr, _replacementStr);
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x0036692A File Offset: 0x00364B2A
		public static int random(int a, int b)
		{
			if (a == b)
			{
				return a;
			}
			return a + Res.r.nextInt(b - a);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x00366941 File Offset: 0x00364B41
		public static int random(int a)
		{
			return Res.r.nextInt(a);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x00366950 File Offset: 0x00364B50
		public static int random_Am(int a, int b)
		{
			int num = a + Res.r.nextInt(b - a);
			if (Res.random(2) == 0)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x0036697C File Offset: 0x00364B7C
		public static int random_Am_0(int a)
		{
			int num;
			for (num = 0; num == 0; num = Res.r.nextInt() % a)
			{
			}
			return num;
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x0036699E File Offset: 0x00364B9E
		public static int distance(int x1, int y1, int x2, int y2)
		{
			return Res.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x003669B4 File Offset: 0x00364BB4
		public static int getDistance(int x, int y)
		{
			return Res.sqrt(x * x + y * y);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x003669C4 File Offset: 0x00364BC4
		public static int sqrt(int a)
		{
			if (a <= 0)
			{
				return 0;
			}
			int num = (a + 1) / 2;
			int num2;
			do
			{
				num2 = num;
				num = num / 2 + a / (2 * num);
			}
			while (Math.abs(num2 - num) > 1);
			return num;
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x0007D631 File Offset: 0x0007B831
		public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
		{
			return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x003669F8 File Offset: 0x00364BF8
		public static string[] split(string original, string separator, int count)
		{
			int num = original.IndexOf(separator);
			string[] array;
			if (num >= 0)
			{
				array = Res.split(original.Substring(num + separator.Length), separator, count + 1);
			}
			else
			{
				array = new string[count + 1];
				num = original.Length;
			}
			array[count] = original.Substring(0, num);
			return array;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x00366A48 File Offset: 0x00364C48
		public static string formatNumber(long number)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			empty = string.Empty;
			if (number >= 1000000000L)
			{
				empty2 = mResources.billion;
				long num = number % 1000000000L / 100000000L;
				number /= 1000000000L;
				empty = number.ToString() + string.Empty;
				if (num > 0L)
				{
					return empty + "," + num.ToString() + empty2;
				}
				return empty + empty2;
			}
			else
			{
				if (number < 1000000L)
				{
					return number.ToString() + string.Empty;
				}
				empty2 = mResources.million;
				long num2 = number % 1000000L / 100000L;
				number /= 1000000L;
				empty = number.ToString() + string.Empty;
				if (num2 > 0L)
				{
					return empty + "," + num2.ToString() + empty2;
				}
				return empty + empty2;
			}
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x00366B30 File Offset: 0x00364D30
		public static string formatNumber2(long number)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			empty = string.Empty;
			if (number >= 1000000000L)
			{
				empty2 = mResources.billion;
				long num = number % 1000000000L / 10000000L;
				number /= 1000000000L;
				empty = number.ToString() + string.Empty;
				if (num >= 10L)
				{
					if (num % 10L == 0L)
					{
						num /= 10L;
					}
					return empty + "," + num.ToString() + empty2;
				}
				if (num > 0L)
				{
					return empty + ",0" + num.ToString() + empty2;
				}
				return empty + empty2;
			}
			else if (number >= 1000000L)
			{
				empty2 = mResources.million;
				long num2 = number % 1000000L / 10000L;
				number /= 1000000L;
				empty = number.ToString() + string.Empty;
				if (num2 >= 10L)
				{
					if (num2 % 10L == 0L)
					{
						num2 /= 10L;
					}
					return empty + "," + num2.ToString() + empty2;
				}
				if (num2 > 0L)
				{
					return empty + ",0" + num2.ToString() + empty2;
				}
				return empty + empty2;
			}
			else
			{
				if (number < 10000L)
				{
					return number.ToString() + string.Empty;
				}
				empty2 = "k";
				long num3 = number % 1000L / 10L;
				number /= 1000L;
				empty = number.ToString() + string.Empty;
				if (num3 >= 10L)
				{
					if (num3 % 10L == 0L)
					{
						num3 /= 10L;
					}
					return empty + "," + num3.ToString() + empty2;
				}
				if (num3 > 0L)
				{
					return empty + ",0" + num3.ToString() + empty2;
				}
				return empty + empty2;
			}
		}

		// Token: 0x04006BCE RID: 27598
		private static short[] sinz = new short[]
		{
			0,
			18,
			36,
			54,
			71,
			89,
			107,
			125,
			143,
			160,
			178,
			195,
			213,
			230,
			248,
			265,
			282,
			299,
			316,
			333,
			350,
			367,
			384,
			400,
			416,
			433,
			449,
			465,
			481,
			496,
			512,
			527,
			543,
			558,
			573,
			587,
			602,
			616,
			630,
			644,
			658,
			672,
			685,
			698,
			711,
			724,
			737,
			749,
			761,
			773,
			784,
			796,
			807,
			818,
			828,
			839,
			849,
			859,
			868,
			878,
			887,
			896,
			904,
			912,
			920,
			928,
			935,
			943,
			949,
			956,
			962,
			968,
			974,
			979,
			984,
			989,
			994,
			998,
			1002,
			1005,
			1008,
			1011,
			1014,
			1016,
			1018,
			1020,
			1022,
			1023,
			1023,
			1024,
			1024
		};

		// Token: 0x04006BCF RID: 27599
		private static short[] cosz;

		// Token: 0x04006BD0 RID: 27600
		private static int[] tanz;

		// Token: 0x04006BD1 RID: 27601
		public static string[] LOG_CAT = new string[]
		{
			"<color=#ff0000ff>[  LOG_CAT  ]</color>",
			"<color=#ff0000ff>[LOG_SESSION]</color>",
			"<color=#ffff00ff>[LOG_SESSION]</color>",
			"<color=#ff0000ff>[LOG_MOBILE ]</color>",
			string.Empty
		};

		// Token: 0x04006BD2 RID: 27602
		public static MyVector debug = new MyVector();

		// Token: 0x04006BD3 RID: 27603
		public static MyRandom r = new MyRandom();
	}
}
