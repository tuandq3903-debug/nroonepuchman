using System;

namespace Game3
{
	// Token: 0x020002E8 RID: 744
	public class Line
	{
		// Token: 0x060020C5 RID: 8389 RVA: 0x002069E1 File Offset: 0x00204BE1
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

		// Token: 0x060020C6 RID: 8390 RVA: 0x00206A18 File Offset: 0x00204C18
		public void update()
		{
			this.x0 += this.vx;
			this.x1 += this.vx;
			this.y0 += this.vy;
			this.y1 += this.vy;
			this.f++;
		}

		// Token: 0x04003FC9 RID: 16329
		public int x0;

		// Token: 0x04003FCA RID: 16330
		public int y0;

		// Token: 0x04003FCB RID: 16331
		public int x1;

		// Token: 0x04003FCC RID: 16332
		public int y1;

		// Token: 0x04003FCD RID: 16333
		public int vx;

		// Token: 0x04003FCE RID: 16334
		public int vy;

		// Token: 0x04003FCF RID: 16335
		public int f;

		// Token: 0x04003FD0 RID: 16336
		public int type;

		// Token: 0x04003FD1 RID: 16337
		public bool is2Line;
	}
}
