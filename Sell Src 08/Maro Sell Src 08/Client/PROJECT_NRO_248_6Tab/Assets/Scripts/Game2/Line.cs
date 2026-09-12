using System;

namespace Game2
{
	// Token: 0x020003C0 RID: 960
	public class Line
	{
		// Token: 0x06002A69 RID: 10857 RVA: 0x0029BA85 File Offset: 0x00299C85
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

		// Token: 0x06002A6A RID: 10858 RVA: 0x0029BABC File Offset: 0x00299CBC
		public void update()
		{
			this.x0 += this.vx;
			this.x1 += this.vx;
			this.y0 += this.vy;
			this.y1 += this.vy;
			this.f++;
		}

		// Token: 0x04005248 RID: 21064
		public int x0;

		// Token: 0x04005249 RID: 21065
		public int y0;

		// Token: 0x0400524A RID: 21066
		public int x1;

		// Token: 0x0400524B RID: 21067
		public int y1;

		// Token: 0x0400524C RID: 21068
		public int vx;

		// Token: 0x0400524D RID: 21069
		public int vy;

		// Token: 0x0400524E RID: 21070
		public int f;

		// Token: 0x0400524F RID: 21071
		public int type;

		// Token: 0x04005250 RID: 21072
		public bool is2Line;
	}
}
