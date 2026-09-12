using System;

namespace Game6
{
	// Token: 0x0200007E RID: 126
	public class MyRandom
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x0005B3D5 File Offset: 0x000595D5
		public MyRandom()
		{
			this.r = new Random();
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0005B3E8 File Offset: 0x000595E8
		public int nextInt()
		{
			return this.r.Next();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0005B3F5 File Offset: 0x000595F5
		public int nextInt(int a)
		{
			return this.r.Next(a);
		}

		// Token: 0x04000CE1 RID: 3297
		public Random r;
	}
}
