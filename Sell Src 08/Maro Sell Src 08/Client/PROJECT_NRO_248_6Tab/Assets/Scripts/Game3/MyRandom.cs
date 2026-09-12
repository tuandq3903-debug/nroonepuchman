using System;

namespace Game3
{
	// Token: 0x02000306 RID: 774
	public class MyRandom
	{
		// Token: 0x06002247 RID: 8775 RVA: 0x0021A685 File Offset: 0x00218885
		public MyRandom()
		{
			this.r = new Random();
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0021A698 File Offset: 0x00218898
		public int nextInt()
		{
			return this.r.Next();
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0021A6A5 File Offset: 0x002188A5
		public int nextInt(int a)
		{
			return this.r.Next(a);
		}

		// Token: 0x0400445E RID: 17502
		public Random r;
	}
}
