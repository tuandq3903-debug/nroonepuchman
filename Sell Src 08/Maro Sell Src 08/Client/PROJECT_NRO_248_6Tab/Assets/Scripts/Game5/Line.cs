using System;

namespace Game5
{
	// Token: 0x02000138 RID: 312
	public class Line
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x000DC899 File Offset: 0x000DAA99
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

		// Token: 0x06000D7E RID: 3454 RVA: 0x000DC8D0 File Offset: 0x000DAAD0
		public void update()
		{
			this.x0 += this.vx;
			this.x1 += this.vx;
			this.y0 += this.vy;
			this.y1 += this.vy;
			this.f++;
		}

		// Token: 0x04001ACB RID: 6859
		public int x0;

		// Token: 0x04001ACC RID: 6860
		public int y0;

		// Token: 0x04001ACD RID: 6861
		public int x1;

		// Token: 0x04001ACE RID: 6862
		public int y1;

		// Token: 0x04001ACF RID: 6863
		public int vx;

		// Token: 0x04001AD0 RID: 6864
		public int vy;

		// Token: 0x04001AD1 RID: 6865
		public int f;

		// Token: 0x04001AD2 RID: 6866
		public int type;

		// Token: 0x04001AD3 RID: 6867
		public bool is2Line;
	}
}
