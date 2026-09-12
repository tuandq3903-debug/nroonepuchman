using System;

namespace Game2
{
	// Token: 0x020003DE RID: 990
	public class MyRandom
	{
		// Token: 0x06002BEB RID: 11243 RVA: 0x002AF729 File Offset: 0x002AD929
		public MyRandom()
		{
			this.r = new Random();
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x002AF73C File Offset: 0x002AD93C
		public int nextInt()
		{
			return this.r.Next();
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x002AF749 File Offset: 0x002AD949
		public int nextInt(int a)
		{
			return this.r.Next(a);
		}

		// Token: 0x040056DD RID: 22237
		public Random r;
	}
}
