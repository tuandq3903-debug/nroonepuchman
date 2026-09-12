using System;

namespace Game5
{
	// Token: 0x02000156 RID: 342
	public class MyRandom
	{
		// Token: 0x06000EFF RID: 3839 RVA: 0x000F053D File Offset: 0x000EE73D
		public MyRandom()
		{
			this.r = new Random();
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x000F0550 File Offset: 0x000EE750
		public int nextInt()
		{
			return this.r.Next();
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x000F055D File Offset: 0x000EE75D
		public int nextInt(int a)
		{
			return this.r.Next(a);
		}

		// Token: 0x04001F60 RID: 8032
		public Random r;
	}
}
