using System;

namespace Game4
{
	// Token: 0x02000210 RID: 528
	public class Line
	{
		// Token: 0x06001721 RID: 5921 RVA: 0x0017193D File Offset: 0x0016FB3D
		public void setLine(int x0, int y0, int x1, int y1, int vx, int vy, bool is2Line)
		{
			this.x0 = x0;
			this.y0 = y0;
			this.x1 = x1;
			this.y1 = y1;
			this.vx = vx;
			this.vy = vy;
			this.is2Line = is2Line;
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00171974 File Offset: 0x0016FB74
		public void update()
		{
			this.x0 += this.vx;
			this.x1 += this.vx;
			this.y0 += this.vy;
			this.y1 += this.vy;
			this.f++;
		}

		// Token: 0x04002D4A RID: 11594
		public int x0;

		// Token: 0x04002D4B RID: 11595
		public int y0;

		// Token: 0x04002D4C RID: 11596
		public int x1;

		// Token: 0x04002D4D RID: 11597
		public int y1;

		// Token: 0x04002D4E RID: 11598
		public int vx;

		// Token: 0x04002D4F RID: 11599
		public int vy;

		// Token: 0x04002D50 RID: 11600
		public int f;

		// Token: 0x04002D51 RID: 11601
		public int type;

		// Token: 0x04002D52 RID: 11602
		public bool is2Line;
	}
}
