using System;

namespace Game6
{
	// Token: 0x02000087 RID: 135
	public class NinjaUtil
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0005C9DD File Offset: 0x0005ABDD
		public static int randomNumber(int max)
		{
			return new MyRandom().nextInt(max);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0005C9EC File Offset: 0x0005ABEC
		public static sbyte[] readByteArray(Message msg)
		{
			try
			{
				int length = msg.reader().readInt();
				if (length > 1)
				{
					sbyte[] data = new sbyte[length];
					msg.reader().read(ref data);
					return data;
				}
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0005CA3C File Offset: 0x0005AC3C
		public static sbyte[] readByteArray(myReader dos)
		{
			try
			{
				sbyte[] data = new sbyte[dos.readInt()];
				dos.read(ref data);
				return data;
			}
			catch (Exception)
			{
				Cout.LogError("LOI DOC readByteArray dos  NINJAUTIL");
			}
			return null;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0005CA84 File Offset: 0x0005AC84
		public static string Replace(string text, string regex, string replacement)
		{
			return text.Replace(regex, replacement);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0005CA90 File Offset: 0x0005AC90
		public static string getDate(int second)
		{
			long num = (long)second * 1000L;
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan(num * 10000L)).ToUniversalTime();
			int hour = dateTime.Hour;
			int minute = dateTime.Minute;
			int day = dateTime.Day;
			int month = dateTime.Month;
			int year = dateTime.Year;
			return string.Concat(new string[]
			{
				day.ToString(),
				"/",
				month.ToString(),
				"/",
				year.ToString(),
				" ",
				hour.ToString(),
				"h"
			});
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0005CB54 File Offset: 0x0005AD54
		public static string getDate2(long second)
		{
			long num = second + 25200000L;
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan(num * 10000L)).ToUniversalTime();
			int hour = dateTime.Hour;
			int minute = dateTime.Minute;
			return hour.ToString() + "h" + minute.ToString() + "m";
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0005CBC8 File Offset: 0x0005ADC8
		public static string getTime(int timeRemainS)
		{
			int num = 0;
			if (timeRemainS > 60)
			{
				num = timeRemainS / 60;
				timeRemainS %= 60;
			}
			int num2 = 0;
			if (num > 60)
			{
				num2 = num / 60;
				num %= 60;
			}
			int num3 = 0;
			if (num2 > 24)
			{
				num3 = num2 / 24;
				num2 %= 24;
			}
			string empty = string.Empty;
			if (num3 > 0)
			{
				empty += num3.ToString();
				empty += "d";
				return empty + num2.ToString() + "h";
			}
			if (num2 > 0)
			{
				empty += num2.ToString();
				empty += "h";
				return empty + num.ToString() + "'";
			}
			empty = ((num <= 9) ? (empty + "0" + num.ToString()) : (empty + num.ToString()));
			empty += ":";
			if (timeRemainS > 9)
			{
				return empty + timeRemainS.ToString();
			}
			return empty + "0" + timeRemainS.ToString();
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0005CCCC File Offset: 0x0005AECC
		public static string getMoneys(long m)
		{
			string text = string.Empty;
			long num = m / 1000L + 1L;
			int i = 0;
			while ((long)i < num)
			{
				if (m < 1000L)
				{
					text = m.ToString() + text;
					break;
				}
				long num2 = m % 1000L;
				text = ((num2 != 0L) ? ((num2 >= 10L) ? ((num2 >= 100L) ? ("." + num2.ToString() + text) : (".0" + num2.ToString() + text)) : (".00" + num2.ToString() + text)) : (".000" + text));
				m /= 1000L;
				i++;
			}
			return text;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0005CD84 File Offset: 0x0005AF84
		public static string getTimeAgo(long timeRemainS)
		{
			long num = 0L;
			if (timeRemainS > 60L)
			{
				num = timeRemainS / 60L;
			}
			long num2 = 0L;
			if (num > 60L)
			{
				num2 = num / 60L;
				num %= 60L;
			}
			long num3 = 0L;
			if (num2 > 24L)
			{
				num3 = num2 / 24L;
				num2 %= 24L;
			}
			string empty = string.Empty;
			if (num3 > 0L)
			{
				empty += num3.ToString();
				empty += "d";
				return empty + num2.ToString() + "h";
			}
			if (num2 > 0L)
			{
				empty += num2.ToString();
				empty += "h";
				return empty + num.ToString() + "'";
			}
			if (num == 0L)
			{
				num = 1L;
			}
			empty += num.ToString();
			return empty + "ph";
		}
	}
}
