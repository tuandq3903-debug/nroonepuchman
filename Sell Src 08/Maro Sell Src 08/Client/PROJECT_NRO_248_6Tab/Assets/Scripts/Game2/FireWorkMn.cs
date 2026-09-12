using System;

namespace Game2
{
	// Token: 0x0200039A RID: 922
	public class FireWorkMn
	{
		// Token: 0x060028E3 RID: 10467 RVA: 0x00283EDC File Offset: 0x002820DC
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

		// Token: 0x060028E4 RID: 10468 RVA: 0x00283FA4 File Offset: 0x002821A4
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

		// Token: 0x04004E66 RID: 20070
		private int x;

		// Token: 0x04004E67 RID: 20071
		private int y;

		// Token: 0x04004E68 RID: 20072
		private int goc = 1;

		// Token: 0x04004E69 RID: 20073
		private int n = 360;

		// Token: 0x04004E6A RID: 20074
		private MyRandom rd = new MyRandom();

		// Token: 0x04004E6B RID: 20075
		private MyVector fw = new MyVector();

		// Token: 0x04004E6C RID: 20076
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
