using System;

namespace Game3
{
	// Token: 0x020002C2 RID: 706
	public class FireWorkMn
	{
		// Token: 0x06001F3F RID: 7999 RVA: 0x001EEE38 File Offset: 0x001ED038
		public FireWorkMn(int x, int y, int goc, int n)
		{
			this.x = x;
			this.y = y;
			this.goc = goc;
			this.n = n;
			for (int i = 0; i < n; i++)
			{
				this.fw.addElement(new Firework(x, y, Math.abs(this.rd.nextInt() % 8) + 3, i * goc, this.color[Math.abs(this.rd.nextInt() % this.color.Length)]));
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x001EEF00 File Offset: 0x001ED100
		public void paint(mGraphics g)
		{
			for (int i = 0; i < this.fw.size(); i++)
			{
				Firework firework = (Firework)this.fw.elementAt(i);
				if (firework.y < -200)
				{
					this.fw.removeElementAt(i);
				}
				firework.paint(g);
			}
		}

		// Token: 0x04003BE7 RID: 15335
		private int x;

		// Token: 0x04003BE8 RID: 15336
		private int y;

		// Token: 0x04003BE9 RID: 15337
		private int goc = 1;

		// Token: 0x04003BEA RID: 15338
		private int n = 360;

		// Token: 0x04003BEB RID: 15339
		private MyRandom rd = new MyRandom();

		// Token: 0x04003BEC RID: 15340
		private MyVector fw = new MyVector();

		// Token: 0x04003BED RID: 15341
		private int[] color = new int[]
		{
			16711680,
			16776960,
			65280,
			16777215,
			255,
			65535,
			15790320,
			12632256
		};
	}
}
