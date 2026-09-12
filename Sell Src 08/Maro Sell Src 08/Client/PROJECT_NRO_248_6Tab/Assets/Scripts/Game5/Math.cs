using System;

namespace Game5
{
	// Token: 0x0200013E RID: 318
	public class Math
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0004A2A9 File Offset: 0x000484A9
		public static int min(int x, int y)
		{
			if (x < y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000DF430 File Offset: 0x000DD630
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
