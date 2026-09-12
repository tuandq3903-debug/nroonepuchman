using System;

namespace Game2
{
	// Token: 0x020003D7 RID: 983
	public class MovePoint
	{
		// Token: 0x06002BB5 RID: 11189 RVA: 0x002ACCBF File Offset: 0x002AAEBF
		public MovePoint(int xEnd, int yEnd, int act, int dir)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.dir = dir;
			this.status = act;
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x002ACCE4 File Offset: 0x002AAEE4
		public MovePoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
		}

		// Token: 0x040054FD RID: 21757
		public int xEnd;

		// Token: 0x040054FE RID: 21758
		public int yEnd;

		// Token: 0x040054FF RID: 21759
		public int dir;

		// Token: 0x04005500 RID: 21760
		public int cvx;

		// Token: 0x04005501 RID: 21761
		public int cvy;

		// Token: 0x04005502 RID: 21762
		public int status;
	}
}
