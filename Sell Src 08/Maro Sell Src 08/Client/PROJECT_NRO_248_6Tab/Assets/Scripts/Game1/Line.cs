using System;

namespace Game1
{
	// Token: 0x02000498 RID: 1176
	public class Line
	{
		// Token: 0x0600340D RID: 13325 RVA: 0x00330B29 File Offset: 0x0032ED29
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

		// Token: 0x0600340E RID: 13326 RVA: 0x00330B60 File Offset: 0x0032ED60
		public void update()
		{
			this.x0 += this.vx;
			this.x1 += this.vx;
			this.y0 += this.vy;
			this.y1 += this.vy;
			this.f++;
		}

		// Token: 0x040064C7 RID: 25799
		public int x0;

		// Token: 0x040064C8 RID: 25800
		public int y0;

		// Token: 0x040064C9 RID: 25801
		public int x1;

		// Token: 0x040064CA RID: 25802
		public int y1;

		// Token: 0x040064CB RID: 25803
		public int vx;

		// Token: 0x040064CC RID: 25804
		public int vy;

		// Token: 0x040064CD RID: 25805
		public int f;

		// Token: 0x040064CE RID: 25806
		public int type;

		// Token: 0x040064CF RID: 25807
		public bool is2Line;
	}
}
