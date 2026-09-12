using System;

namespace Game6
{
	// Token: 0x02000060 RID: 96
	public class Line
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x000476F1 File Offset: 0x000458F1
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

		// Token: 0x060003DA RID: 986 RVA: 0x00047728 File Offset: 0x00045928
		public void update()
		{
			this.x0 += this.vx;
			this.x1 += this.vx;
			this.y0 += this.vy;
			this.y1 += this.vy;
			this.f++;
		}

		// Token: 0x0400084C RID: 2124
		public int x0;

		// Token: 0x0400084D RID: 2125
		public int y0;

		// Token: 0x0400084E RID: 2126
		public int x1;

		// Token: 0x0400084F RID: 2127
		public int y1;

		// Token: 0x04000850 RID: 2128
		public int vx;

		// Token: 0x04000851 RID: 2129
		public int vy;

		// Token: 0x04000852 RID: 2130
		public int f;

		// Token: 0x04000853 RID: 2131
		public int type;

		// Token: 0x04000854 RID: 2132
		public bool is2Line;
	}
}
