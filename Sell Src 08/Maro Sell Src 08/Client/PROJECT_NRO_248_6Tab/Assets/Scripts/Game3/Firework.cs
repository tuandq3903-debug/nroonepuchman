using System;

namespace Game3
{
	// Token: 0x020002C0 RID: 704
	public class Firework
	{
		// Token: 0x06001F33 RID: 7987 RVA: 0x001EE89C File Offset: 0x001ECA9C
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

		// Token: 0x06001F34 RID: 7988 RVA: 0x001EE958 File Offset: 0x001ECB58
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

		// Token: 0x06001F35 RID: 7989 RVA: 0x001EEA6C File Offset: 0x001ECC6C
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

		// Token: 0x06001F36 RID: 7990 RVA: 0x001EEAE5 File Offset: 0x001ECCE5
		public long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x001EEAEC File Offset: 0x001ECCEC
		public void Drawline(mGraphics g, int x, int y, int color)
		{
			g.setColor(color);
			g.fillRect(x, y, 1, 2);
		}

		// Token: 0x04003BC4 RID: 15300
		public int w;

		// Token: 0x04003BC5 RID: 15301
		public int h;

		// Token: 0x04003BC6 RID: 15302
		public int v;

		// Token: 0x04003BC7 RID: 15303
		public int x0;

		// Token: 0x04003BC8 RID: 15304
		public int x;

		// Token: 0x04003BC9 RID: 15305
		public int y;

		// Token: 0x04003BCA RID: 15306
		public int y0;

		// Token: 0x04003BCB RID: 15307
		public int angle;

		// Token: 0x04003BCC RID: 15308
		public int t;

		// Token: 0x04003BCD RID: 15309
		public int cl = 16711680;

		// Token: 0x04003BCE RID: 15310
		private float a;

		// Token: 0x04003BCF RID: 15311
		private long last;

		// Token: 0x04003BD0 RID: 15312
		private long delay = 150L;

		// Token: 0x04003BD1 RID: 15313
		private bool act = true;

		// Token: 0x04003BD2 RID: 15314
		private int[] arr_x = new int[2];

		// Token: 0x04003BD3 RID: 15315
		private int[] arr_y = new int[2];
	}
}
