using System;

namespace Game4
{
	// Token: 0x02000227 RID: 551
	public class MovePoint
	{
		// Token: 0x0600186D RID: 6253 RVA: 0x00182B77 File Offset: 0x00180D77
		public MovePoint(int xEnd, int yEnd, int act, int dir)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.dir = dir;
			this.status = act;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00182B9C File Offset: 0x00180D9C
		public MovePoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
		}

		// Token: 0x04002FFF RID: 12287
		public int xEnd;

		// Token: 0x04003000 RID: 12288
		public int yEnd;

		// Token: 0x04003001 RID: 12289
		public int dir;

		// Token: 0x04003002 RID: 12290
		public int cvx;

		// Token: 0x04003003 RID: 12291
		public int cvy;

		// Token: 0x04003004 RID: 12292
		public int status;
	}
}
