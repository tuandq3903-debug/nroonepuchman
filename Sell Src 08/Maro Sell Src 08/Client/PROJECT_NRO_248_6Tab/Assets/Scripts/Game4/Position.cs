using System;

namespace Game4
{
	// Token: 0x02000245 RID: 581
	public class Position
	{
		// Token: 0x06001A27 RID: 6695 RVA: 0x001A6167 File Offset: 0x001A4367
		public Position()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x001A617D File Offset: 0x001A437D
		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x04003410 RID: 13328
		public int x;

		// Token: 0x04003411 RID: 13329
		public int y;

		// Token: 0x04003412 RID: 13330
		public int anchor;

		// Token: 0x04003413 RID: 13331
		public int g;

		// Token: 0x04003414 RID: 13332
		public int v;

		// Token: 0x04003415 RID: 13333
		public int w;

		// Token: 0x04003416 RID: 13334
		public int h;

		// Token: 0x04003417 RID: 13335
		public int color;

		// Token: 0x04003418 RID: 13336
		public int limitY;

		// Token: 0x04003419 RID: 13337
		public Layer layer;

		// Token: 0x0400341A RID: 13338
		public short yTo;

		// Token: 0x0400341B RID: 13339
		public short xTo;

		// Token: 0x0400341C RID: 13340
		public short distant;
	}
}
