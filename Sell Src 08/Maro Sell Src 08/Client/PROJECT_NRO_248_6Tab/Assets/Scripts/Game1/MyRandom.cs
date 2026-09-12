using System;

namespace Game1
{
	// Token: 0x020004B6 RID: 1206
	public class MyRandom
	{
		// Token: 0x0600358F RID: 13711 RVA: 0x003447CD File Offset: 0x003429CD
		public MyRandom()
		{
			this.r = new Random();
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x003447E0 File Offset: 0x003429E0
		public int nextInt()
		{
			return this.r.Next();
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x003447ED File Offset: 0x003429ED
		public int nextInt(int a)
		{
			return this.r.Next(a);
		}

		// Token: 0x0400695C RID: 26972
		public Random r;
	}
}
