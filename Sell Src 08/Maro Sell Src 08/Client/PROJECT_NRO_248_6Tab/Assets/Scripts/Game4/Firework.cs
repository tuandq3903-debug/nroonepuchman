using System;

namespace Game4
{
	// Token: 0x020001E8 RID: 488
	public class Firework
	{
		// Token: 0x0600158F RID: 5519 RVA: 0x001597F8 File Offset: 0x001579F8
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

		// Token: 0x06001590 RID: 5520 RVA: 0x001598B4 File Offset: 0x00157AB4
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

		// Token: 0x06001591 RID: 5521 RVA: 0x001599C8 File Offset: 0x00157BC8
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

		// Token: 0x06001592 RID: 5522 RVA: 0x00159A41 File Offset: 0x00157C41
		public long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00159A48 File Offset: 0x00157C48
		public void Drawline(mGraphics g, int x, int y, int color)
		{
			g.setColor(color);
			g.fillRect(x, y, 1, 2);
		}

		// Token: 0x04002945 RID: 10565
		public int w;

		// Token: 0x04002946 RID: 10566
		public int h;

		// Token: 0x04002947 RID: 10567
		public int v;

		// Token: 0x04002948 RID: 10568
		public int x0;

		// Token: 0x04002949 RID: 10569
		public int x;

		// Token: 0x0400294A RID: 10570
		public int y;

		// Token: 0x0400294B RID: 10571
		public int y0;

		// Token: 0x0400294C RID: 10572
		public int angle;

		// Token: 0x0400294D RID: 10573
		public int t;

		// Token: 0x0400294E RID: 10574
		public int cl = 16711680;

		// Token: 0x0400294F RID: 10575
		private float a;

		// Token: 0x04002950 RID: 10576
		private long last;

		// Token: 0x04002951 RID: 10577
		private long delay = 150L;

		// Token: 0x04002952 RID: 10578
		private bool act = true;

		// Token: 0x04002953 RID: 10579
		private int[] arr_x = new int[2];

		// Token: 0x04002954 RID: 10580
		private int[] arr_y = new int[2];
	}
}
