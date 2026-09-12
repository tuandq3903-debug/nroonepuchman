using System;

namespace Game5
{
	// Token: 0x0200016D RID: 365
	public class Position
	{
		// Token: 0x06001083 RID: 4227 RVA: 0x001110C3 File Offset: 0x0010F2C3
		public Position()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x001110D9 File Offset: 0x0010F2D9
		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x04002191 RID: 8593
		public int x;

		// Token: 0x04002192 RID: 8594
		public int y;

		// Token: 0x04002193 RID: 8595
		public int anchor;

		// Token: 0x04002194 RID: 8596
		public int g;

		// Token: 0x04002195 RID: 8597
		public int v;

		// Token: 0x04002196 RID: 8598
		public int w;

		// Token: 0x04002197 RID: 8599
		public int h;

		// Token: 0x04002198 RID: 8600
		public int color;

		// Token: 0x04002199 RID: 8601
		public int limitY;

		// Token: 0x0400219A RID: 8602
		public Layer layer;

		// Token: 0x0400219B RID: 8603
		public short yTo;

		// Token: 0x0400219C RID: 8604
		public short xTo;

		// Token: 0x0400219D RID: 8605
		public short distant;
	}
}
