using System;

namespace Game1
{
	// Token: 0x020004AF RID: 1199
	public class MovePoint
	{
		// Token: 0x06003559 RID: 13657 RVA: 0x00341D63 File Offset: 0x0033FF63
		public MovePoint(int xEnd, int yEnd, int act, int dir)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.dir = dir;
			this.status = act;
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x00341D88 File Offset: 0x0033FF88
		public MovePoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
		}

		// Token: 0x0400677C RID: 26492
		public int xEnd;

		// Token: 0x0400677D RID: 26493
		public int yEnd;

		// Token: 0x0400677E RID: 26494
		public int dir;

		// Token: 0x0400677F RID: 26495
		public int cvx;

		// Token: 0x04006780 RID: 26496
		public int cvy;

		// Token: 0x04006781 RID: 26497
		public int status;
	}
}
