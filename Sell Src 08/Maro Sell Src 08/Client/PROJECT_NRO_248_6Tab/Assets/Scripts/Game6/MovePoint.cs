using System;

namespace Game6
{
	// Token: 0x02000077 RID: 119
	public class MovePoint
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x00058957 File Offset: 0x00056B57
		public MovePoint(int xEnd, int yEnd, int act, int dir)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.dir = dir;
			this.status = act;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0005897C File Offset: 0x00056B7C
		public MovePoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
		}

		// Token: 0x04000B01 RID: 2817
		public int xEnd;

		// Token: 0x04000B02 RID: 2818
		public int yEnd;

		// Token: 0x04000B03 RID: 2819
		public int dir;

		// Token: 0x04000B04 RID: 2820
		public int cvx;

		// Token: 0x04000B05 RID: 2821
		public int cvy;

		// Token: 0x04000B06 RID: 2822
		public int status;
	}
}
