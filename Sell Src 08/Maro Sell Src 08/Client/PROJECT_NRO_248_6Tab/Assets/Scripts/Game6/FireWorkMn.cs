using System;

namespace Game6
{
	// Token: 0x0200003A RID: 58
	public class FireWorkMn
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0002FAE4 File Offset: 0x0002DCE4
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

		// Token: 0x06000254 RID: 596 RVA: 0x0002FBAC File Offset: 0x0002DDAC
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

		// Token: 0x0400046A RID: 1130
		private int x;

		// Token: 0x0400046B RID: 1131
		private int y;

		// Token: 0x0400046C RID: 1132
		private int goc = 1;

		// Token: 0x0400046D RID: 1133
		private int n = 360;

		// Token: 0x0400046E RID: 1134
		private MyRandom rd = new MyRandom();

		// Token: 0x0400046F RID: 1135
		private MyVector fw = new MyVector();

		// Token: 0x04000470 RID: 1136
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
