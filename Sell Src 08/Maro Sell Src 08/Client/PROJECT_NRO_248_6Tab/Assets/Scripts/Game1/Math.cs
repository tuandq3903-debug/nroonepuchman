using System;

namespace Game1
{
	// Token: 0x0200049E RID: 1182
	public class Math
	{
		// Token: 0x06003441 RID: 13377 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x0004A2A9 File Offset: 0x000484A9
		public static int min(int x, int y)
		{
			if (x < y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x003336C0 File Offset: 0x003318C0
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
