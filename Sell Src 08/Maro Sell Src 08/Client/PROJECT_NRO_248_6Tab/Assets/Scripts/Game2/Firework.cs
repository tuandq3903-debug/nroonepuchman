using System;

namespace Game2
{
	// Token: 0x02000398 RID: 920
	public class Firework
	{
		// Token: 0x060028D7 RID: 10455 RVA: 0x00283940 File Offset: 0x00281B40
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

		// Token: 0x060028D8 RID: 10456 RVA: 0x002839FC File Offset: 0x00281BFC
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

		// Token: 0x060028D9 RID: 10457 RVA: 0x00283B10 File Offset: 0x00281D10
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

		// Token: 0x060028DA RID: 10458 RVA: 0x00283B89 File Offset: 0x00281D89
		public long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x00283B90 File Offset: 0x00281D90
		public void Drawline(mGraphics g, int x, int y, int color)
		{
			g.setColor(color);
			g.fillRect(x, y, 1, 2);
		}

		// Token: 0x04004E43 RID: 20035
		public int w;

		// Token: 0x04004E44 RID: 20036
		public int h;

		// Token: 0x04004E45 RID: 20037
		public int v;

		// Token: 0x04004E46 RID: 20038
		public int x0;

		// Token: 0x04004E47 RID: 20039
		public int x;

		// Token: 0x04004E48 RID: 20040
		public int y;

		// Token: 0x04004E49 RID: 20041
		public int y0;

		// Token: 0x04004E4A RID: 20042
		public int angle;

		// Token: 0x04004E4B RID: 20043
		public int t;

		// Token: 0x04004E4C RID: 20044
		public int cl = 16711680;

		// Token: 0x04004E4D RID: 20045
		private float a;

		// Token: 0x04004E4E RID: 20046
		private long last;

		// Token: 0x04004E4F RID: 20047
		private long delay = 150L;

		// Token: 0x04004E50 RID: 20048
		private bool act = true;

		// Token: 0x04004E51 RID: 20049
		private int[] arr_x = new int[2];

		// Token: 0x04004E52 RID: 20050
		private int[] arr_y = new int[2];
	}
}
