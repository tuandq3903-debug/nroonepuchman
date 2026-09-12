using System;

namespace Game1
{
	// Token: 0x020004CD RID: 1229
	public class Position
	{
		// Token: 0x06003713 RID: 14099 RVA: 0x00365353 File Offset: 0x00363553
		public Position()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x00365369 File Offset: 0x00363569
		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x04006B8D RID: 27533
		public int x;

		// Token: 0x04006B8E RID: 27534
		public int y;

		// Token: 0x04006B8F RID: 27535
		public int anchor;

		// Token: 0x04006B90 RID: 27536
		public int g;

		// Token: 0x04006B91 RID: 27537
		public int v;

		// Token: 0x04006B92 RID: 27538
		public int w;

		// Token: 0x04006B93 RID: 27539
		public int h;

		// Token: 0x04006B94 RID: 27540
		public int color;

		// Token: 0x04006B95 RID: 27541
		public int limitY;

		// Token: 0x04006B96 RID: 27542
		public Layer layer;

		// Token: 0x04006B97 RID: 27543
		public short yTo;

		// Token: 0x04006B98 RID: 27544
		public short xTo;

		// Token: 0x04006B99 RID: 27545
		public short distant;
	}
}
