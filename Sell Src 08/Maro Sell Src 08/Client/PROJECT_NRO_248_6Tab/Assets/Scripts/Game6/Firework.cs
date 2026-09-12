using System;

namespace Game6
{
	// Token: 0x02000038 RID: 56
	public class Firework
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0002F548 File Offset: 0x0002D748
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

		// Token: 0x06000248 RID: 584 RVA: 0x0002F604 File Offset: 0x0002D804
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

		// Token: 0x06000249 RID: 585 RVA: 0x0002F718 File Offset: 0x0002D918
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

		// Token: 0x0600024A RID: 586 RVA: 0x0002F791 File Offset: 0x0002D991
		public long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0002F798 File Offset: 0x0002D998
		public void Drawline(mGraphics g, int x, int y, int color)
		{
			g.setColor(color);
			g.fillRect(x, y, 1, 2);
		}

		// Token: 0x04000447 RID: 1095
		public int w;

		// Token: 0x04000448 RID: 1096
		public int h;

		// Token: 0x04000449 RID: 1097
		public int v;

		// Token: 0x0400044A RID: 1098
		public int x0;

		// Token: 0x0400044B RID: 1099
		public int x;

		// Token: 0x0400044C RID: 1100
		public int y;

		// Token: 0x0400044D RID: 1101
		public int y0;

		// Token: 0x0400044E RID: 1102
		public int angle;

		// Token: 0x0400044F RID: 1103
		public int t;

		// Token: 0x04000450 RID: 1104
		public int cl = 16711680;

		// Token: 0x04000451 RID: 1105
		private float a;

		// Token: 0x04000452 RID: 1106
		private long last;

		// Token: 0x04000453 RID: 1107
		private long delay = 150L;

		// Token: 0x04000454 RID: 1108
		private bool act = true;

		// Token: 0x04000455 RID: 1109
		private int[] arr_x = new int[2];

		// Token: 0x04000456 RID: 1110
		private int[] arr_y = new int[2];
	}
}
