using System;

namespace Game1
{
	// Token: 0x02000470 RID: 1136
	public class Firework
	{
		// Token: 0x0600327B RID: 12923 RVA: 0x003189E4 File Offset: 0x00316BE4
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

		// Token: 0x0600327C RID: 12924 RVA: 0x00318AA0 File Offset: 0x00316CA0
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

		// Token: 0x0600327D RID: 12925 RVA: 0x00318BB4 File Offset: 0x00316DB4
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

		// Token: 0x0600327E RID: 12926 RVA: 0x00318C2D File Offset: 0x00316E2D
		public long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x00318C34 File Offset: 0x00316E34
		public void Drawline(mGraphics g, int x, int y, int color)
		{
			g.setColor(color);
			g.fillRect(x, y, 1, 2);
		}

		// Token: 0x040060C2 RID: 24770
		public int w;

		// Token: 0x040060C3 RID: 24771
		public int h;

		// Token: 0x040060C4 RID: 24772
		public int v;

		// Token: 0x040060C5 RID: 24773
		public int x0;

		// Token: 0x040060C6 RID: 24774
		public int x;

		// Token: 0x040060C7 RID: 24775
		public int y;

		// Token: 0x040060C8 RID: 24776
		public int y0;

		// Token: 0x040060C9 RID: 24777
		public int angle;

		// Token: 0x040060CA RID: 24778
		public int t;

		// Token: 0x040060CB RID: 24779
		public int cl = 16711680;

		// Token: 0x040060CC RID: 24780
		private float a;

		// Token: 0x040060CD RID: 24781
		private long last;

		// Token: 0x040060CE RID: 24782
		private long delay = 150L;

		// Token: 0x040060CF RID: 24783
		private bool act = true;

		// Token: 0x040060D0 RID: 24784
		private int[] arr_x = new int[2];

		// Token: 0x040060D1 RID: 24785
		private int[] arr_y = new int[2];
	}
}
