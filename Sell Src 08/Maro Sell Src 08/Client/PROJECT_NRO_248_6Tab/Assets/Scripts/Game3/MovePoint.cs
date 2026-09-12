using System;

namespace Game3
{
	// Token: 0x020002FF RID: 767
	public class MovePoint
	{
		// Token: 0x06002211 RID: 8721 RVA: 0x00217C1B File Offset: 0x00215E1B
		public MovePoint(int xEnd, int yEnd, int act, int dir)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.dir = dir;
			this.status = act;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00217C40 File Offset: 0x00215E40
		public MovePoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
		}

		// Token: 0x0400427E RID: 17022
		public int xEnd;

		// Token: 0x0400427F RID: 17023
		public int yEnd;

		// Token: 0x04004280 RID: 17024
		public int dir;

		// Token: 0x04004281 RID: 17025
		public int cvx;

		// Token: 0x04004282 RID: 17026
		public int cvy;

		// Token: 0x04004283 RID: 17027
		public int status;
	}
}
