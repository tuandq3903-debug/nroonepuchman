using System;

namespace Game5
{
	// Token: 0x02000110 RID: 272
	public class Firework
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x000C4754 File Offset: 0x000C2954
		public Firework(int x0, int y0, int v, int angle, int cl)
		{
			this.y0 = y0;
			this.x0 = x0;
			this.a = 1f;
			this.v = v;
			this.angle = angle;
			this.w = GameCanvas.w;
			this.h = GameCanvas.h;
			this.last = this.time();
			for (int i = 0; i < 2; i++)
			{
				this.arr_x[i] = x0;
				this.arr_y[i] = y0;
			}
			this.cl = cl;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000C4810 File Offset: 0x000C2A10
		public void preDraw()
		{
			if (this.time() - this.last >= this.delay)
			{
				this.t++;
				this.last = this.time();
				this.arr_x[1] = this.arr_x[0];
				this.arr_y[1] = this.arr_y[0];
				this.arr_x[0] = this.x;
				this.arr_y[0] = this.y;
				this.x = Res.cos((int)((double)this.angle * 3.141592653589793 / 180.0)) * this.v * this.t + this.x0;
				this.y = (int)((float)(this.v * Res.sin((int)((double)this.angle * 3.141592653589793 / 180.0)) * this.t) - this.a * (float)this.t * (float)this.t / 2f) + this.y0;
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000C4924 File Offset: 0x000C2B24
		public void paint(mGraphics g)
		{
			this.Drawline(g, this.w - this.x, this.h - this.y, this.cl);
			for (int i = 0; i < 2; i++)
			{
				this.Drawline(g, this.w - this.arr_x[i], this.h - this.arr_y[i], this.cl);
			}
			if (this.act)
			{
				this.preDraw();
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000C499D File Offset: 0x000C2B9D
		public long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000C49A4 File Offset: 0x000C2BA4
		public void Drawline(mGraphics g, int x, int y, int color)
		{
			g.setColor(color);
			g.fillRect(x, y, 1, 2);
		}

		// Token: 0x040016C6 RID: 5830
		public int w;

		// Token: 0x040016C7 RID: 5831
		public int h;

		// Token: 0x040016C8 RID: 5832
		public int v;

		// Token: 0x040016C9 RID: 5833
		public int x0;

		// Token: 0x040016CA RID: 5834
		public int x;

		// Token: 0x040016CB RID: 5835
		public int y;

		// Token: 0x040016CC RID: 5836
		public int y0;

		// Token: 0x040016CD RID: 5837
		public int angle;

		// Token: 0x040016CE RID: 5838
		public int t;

		// Token: 0x040016CF RID: 5839
		public int cl = 16711680;

		// Token: 0x040016D0 RID: 5840
		private float a;

		// Token: 0x040016D1 RID: 5841
		private long last;

		// Token: 0x040016D2 RID: 5842
		private long delay = 150L;

		// Token: 0x040016D3 RID: 5843
		private bool act = true;

		// Token: 0x040016D4 RID: 5844
		private int[] arr_x = new int[2];

		// Token: 0x040016D5 RID: 5845
		private int[] arr_y = new int[2];
	}
}
