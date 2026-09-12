using System;
using UnityEngine;

namespace Game3
{
	// Token: 0x0200031F RID: 799
	public class Res
	{
		// Token: 0x060023DF RID: 9183 RVA: 0x0023C604 File Offset: 0x0023A804
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

		// Token: 0x060023E0 RID: 9184 RVA: 0x0023C678 File Offset: 0x0023A878
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

		// Token: 0x060023E1 RID: 9185 RVA: 0x0023C6E8 File Offset: 0x0023A8E8
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

		// Token: 0x060023E2 RID: 9186 RVA: 0x0023C758 File Offset: 0x0023A958
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

		// Token: 0x060023E3 RID: 9187 RVA: 0x0023C780 File Offset: 0x0023A980
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

		// Token: 0x060023E4 RID: 9188 RVA: 0x0007D542 File Offset: 0x0007B742
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

		// Token: 0x060023E5 RID: 9189 RVA: 0x001E678D File Offset: 0x001E498D
		public static void outz(string s)
		{
			if (mSystem.isTest)
			{
				Debug.Log(s);
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x001E679C File Offset: 0x001E499C
		public static void err(string s)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(s);
			}
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void outz2(string s)
		{
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void paintOnScreenDebug(mGraphics g)
		{
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void updateOnScreenDebug()
		{
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x0007D563 File Offset: 0x0007B763
		public static string changeString(string str)
		{
			return str;
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x0005CA84 File Offset: 0x0005AC84
		public static string replace(string _text, string _searchStr, string _replacementStr)
		{
			return _text.Replace(_searchStr, _replacementStr);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0023C7E2 File Offset: 0x0023A9E2
		public static int random(int a, int b)
		{
			if (a == b)
			{
				return a;
			}
			return a + Res.r.nextInt(b - a);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x0023C7F9 File Offset: 0x0023A9F9
		public static int random(int a)
		{
			return Res.r.nextInt(a);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x0023C808 File Offset: 0x0023AA08
		public static int random_Am(int a, int b)
		{
			int num = a + Res.r.nextInt(b - a);
			if (Res.random(2) == 0)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x0023C834 File Offset: 0x0023AA34
		public static int random_Am_0(int a)
		{
			int num;
			for (num = 0; num == 0; num = Res.r.nextInt() % a)
			{
			}
			return num;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x0023C856 File Offset: 0x0023AA56
		public static int distance(int x1, int y1, int x2, int y2)
		{
			return Res.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x0023C86C File Offset: 0x0023AA6C
		public static int getDistance(int x, int y)
		{
			return Res.sqrt(x * x + y * y);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0023C87C File Offset: 0x0023AA7C
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

		// Token: 0x060023F3 RID: 9203 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0007D631 File Offset: 0x0007B831
		public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
		{
			return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0023C8B0 File Offset: 0x0023AAB0
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

		// Token: 0x060023F6 RID: 9206 RVA: 0x0023C900 File Offset: 0x0023AB00
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

		// Token: 0x060023F7 RID: 9207 RVA: 0x0023C9E8 File Offset: 0x0023ABE8
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

		// Token: 0x040046D0 RID: 18128
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

		// Token: 0x040046D1 RID: 18129
		private static short[] cosz;

		// Token: 0x040046D2 RID: 18130
		private static int[] tanz;

		// Token: 0x040046D3 RID: 18131
		public static string[] LOG_CAT = new string[]
		{
			"<color=#ff0000ff>[  LOG_CAT  ]</color>",
			"<color=#ff0000ff>[LOG_SESSION]</color>",
			"<color=#ffff00ff>[LOG_SESSION]</color>",
			"<color=#ff0000ff>[LOG_MOBILE ]</color>",
			string.Empty
		};

		// Token: 0x040046D4 RID: 18132
		public static MyVector debug = new MyVector();

		// Token: 0x040046D5 RID: 18133
		public static MyRandom r = new MyRandom();
	}
}
