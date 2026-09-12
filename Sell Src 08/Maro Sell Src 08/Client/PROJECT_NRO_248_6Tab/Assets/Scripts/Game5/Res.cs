using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x0200016F RID: 367
	public class Res
	{
		// Token: 0x06001097 RID: 4247 RVA: 0x001124BC File Offset: 0x001106BC
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

		// Token: 0x06001098 RID: 4248 RVA: 0x00112530 File Offset: 0x00110730
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

		// Token: 0x06001099 RID: 4249 RVA: 0x001125A0 File Offset: 0x001107A0
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

		// Token: 0x0600109A RID: 4250 RVA: 0x00112610 File Offset: 0x00110810
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

		// Token: 0x0600109B RID: 4251 RVA: 0x00112638 File Offset: 0x00110838
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

		// Token: 0x0600109C RID: 4252 RVA: 0x0007D542 File Offset: 0x0007B742
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

		// Token: 0x0600109D RID: 4253 RVA: 0x000BC645 File Offset: 0x000BA845
		public static void outz(string s)
		{
			if (mSystem.isTest)
			{
				Debug.Log(s);
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000BC654 File Offset: 0x000BA854
		public static void err(string s)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(s);
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void outz2(string s)
		{
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void paintOnScreenDebug(mGraphics g)
		{
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void updateOnScreenDebug()
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0007D563 File Offset: 0x0007B763
		public static string changeString(string str)
		{
			return str;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0005CA84 File Offset: 0x0005AC84
		public static string replace(string _text, string _searchStr, string _replacementStr)
		{
			return _text.Replace(_searchStr, _replacementStr);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0011269A File Offset: 0x0011089A
		public static int random(int a, int b)
		{
			if (a == b)
			{
				return a;
			}
			return a + Res.r.nextInt(b - a);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x001126B1 File Offset: 0x001108B1
		public static int random(int a)
		{
			return Res.r.nextInt(a);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x001126C0 File Offset: 0x001108C0
		public static int random_Am(int a, int b)
		{
			int num = a + Res.r.nextInt(b - a);
			if (Res.random(2) == 0)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x001126EC File Offset: 0x001108EC
		public static int random_Am_0(int a)
		{
			int num;
			for (num = 0; num == 0; num = Res.r.nextInt() % a)
			{
			}
			return num;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0011270E File Offset: 0x0011090E
		public static int distance(int x1, int y1, int x2, int y2)
		{
			return Res.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00112724 File Offset: 0x00110924
		public static int getDistance(int x, int y)
		{
			return Res.sqrt(x * x + y * y);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00112734 File Offset: 0x00110934
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

		// Token: 0x060010AB RID: 4267 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0007D631 File Offset: 0x0007B831
		public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
		{
			return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00112768 File Offset: 0x00110968
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

		// Token: 0x060010AE RID: 4270 RVA: 0x001127B8 File Offset: 0x001109B8
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

		// Token: 0x060010AF RID: 4271 RVA: 0x001128A0 File Offset: 0x00110AA0
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

		// Token: 0x040021D2 RID: 8658
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

		// Token: 0x040021D3 RID: 8659
		private static short[] cosz;

		// Token: 0x040021D4 RID: 8660
		private static int[] tanz;

		// Token: 0x040021D5 RID: 8661
		public static string[] LOG_CAT = new string[]
		{
			"<color=#ff0000ff>[  LOG_CAT  ]</color>",
			"<color=#ff0000ff>[LOG_SESSION]</color>",
			"<color=#ffff00ff>[LOG_SESSION]</color>",
			"<color=#ff0000ff>[LOG_MOBILE ]</color>",
			string.Empty
		};

		// Token: 0x040021D6 RID: 8662
		public static MyVector debug = new MyVector();

		// Token: 0x040021D7 RID: 8663
		public static MyRandom r = new MyRandom();
	}
}
