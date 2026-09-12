using System;

namespace Game5
{
	// Token: 0x0200014F RID: 335
	public class MovePoint
	{
		// Token: 0x06000EC9 RID: 3785 RVA: 0x000EDAD3 File Offset: 0x000EBCD3
		public MovePoint(int xEnd, int yEnd, int act, int dir)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.dir = dir;
			this.status = act;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000EDAF8 File Offset: 0x000EBCF8
		public MovePoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
		}

		// Token: 0x04001D80 RID: 7552
		public int xEnd;

		// Token: 0x04001D81 RID: 7553
		public int yEnd;

		// Token: 0x04001D82 RID: 7554
		public int dir;

		// Token: 0x04001D83 RID: 7555
		public int cvx;

		// Token: 0x04001D84 RID: 7556
		public int cvy;

		// Token: 0x04001D85 RID: 7557
		public int status;
	}
}
