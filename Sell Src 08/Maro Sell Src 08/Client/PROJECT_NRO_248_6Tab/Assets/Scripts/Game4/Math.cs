using System;

namespace Game4
{
	// Token: 0x02000216 RID: 534
	public class Math
	{
		// Token: 0x06001755 RID: 5973 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0004A2A9 File Offset: 0x000484A9
		public static int min(int x, int y)
		{
			if (x < y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x001744D4 File Offset: 0x001726D4
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
