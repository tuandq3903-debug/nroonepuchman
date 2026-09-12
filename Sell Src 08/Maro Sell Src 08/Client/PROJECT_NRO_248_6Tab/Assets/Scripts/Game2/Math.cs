using System;

namespace Game2
{
	// Token: 0x020003C6 RID: 966
	public class Math
	{
		// Token: 0x06002A9D RID: 10909 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x0004A2A9 File Offset: 0x000484A9
		public static int min(int x, int y)
		{
			if (x < y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0029E61C File Offset: 0x0029C81C
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
