using System;

namespace Game6
{
	// Token: 0x02000095 RID: 149
	public class Position
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0007BF6B File Offset: 0x0007A16B
		public Position()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0007BF81 File Offset: 0x0007A181
		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x04000F12 RID: 3858
		public int x;

		// Token: 0x04000F13 RID: 3859
		public int y;

		// Token: 0x04000F14 RID: 3860
		public int anchor;

		// Token: 0x04000F15 RID: 3861
		public int g;

		// Token: 0x04000F16 RID: 3862
		public int v;

		// Token: 0x04000F17 RID: 3863
		public int w;

		// Token: 0x04000F18 RID: 3864
		public int h;

		// Token: 0x04000F19 RID: 3865
		public int color;

		// Token: 0x04000F1A RID: 3866
		public int limitY;

		// Token: 0x04000F1B RID: 3867
		public Layer layer;

		// Token: 0x04000F1C RID: 3868
		public short yTo;

		// Token: 0x04000F1D RID: 3869
		public short xTo;

		// Token: 0x04000F1E RID: 3870
		public short distant;
	}
}
