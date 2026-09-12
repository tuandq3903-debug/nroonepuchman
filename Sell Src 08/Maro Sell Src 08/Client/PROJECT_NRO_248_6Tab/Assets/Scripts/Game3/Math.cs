using System;

namespace Game3
{
	// Token: 0x020002EE RID: 750
	public class Math
	{
		// Token: 0x060020F9 RID: 8441 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0004A2A9 File Offset: 0x000484A9
		public static int min(int x, int y)
		{
			if (x < y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00209578 File Offset: 0x00207778
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
