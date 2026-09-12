using System;

namespace Game4
{
	// Token: 0x0200022E RID: 558
	public class MyRandom
	{
		// Token: 0x060018A3 RID: 6307 RVA: 0x001855E1 File Offset: 0x001837E1
		public MyRandom()
		{
			this.r = new Random();
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x001855F4 File Offset: 0x001837F4
		public int nextInt()
		{
			return this.r.Next();
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00185601 File Offset: 0x00183801
		public int nextInt(int a)
		{
			return this.r.Next(a);
		}

		// Token: 0x040031DF RID: 12767
		public Random r;
	}
}
