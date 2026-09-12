using System;

namespace Game6
{
	// Token: 0x02000066 RID: 102
	public class Math
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0004A2A9 File Offset: 0x000484A9
		public static int min(int x, int y)
		{
			if (x < y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0004A2B4 File Offset: 0x000484B4
		public static int pow(int data, int x)
		{
			int num = 1;
			for (int i = 0; i < x; i++)
			{
				num *= data;
			}
			return num;
		}
	}
}
