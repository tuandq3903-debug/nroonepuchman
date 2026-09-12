using System;

namespace Game4
{
	// Token: 0x020001EA RID: 490
	public class FireWorkMn
	{
		// Token: 0x0600159B RID: 5531 RVA: 0x00159D94 File Offset: 0x00157F94
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

		// Token: 0x0600159C RID: 5532 RVA: 0x00159E5C File Offset: 0x0015805C
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

		// Token: 0x04002968 RID: 10600
		private int x;

		// Token: 0x04002969 RID: 10601
		private int y;

		// Token: 0x0400296A RID: 10602
		private int goc = 1;

		// Token: 0x0400296B RID: 10603
		private int n = 360;

		// Token: 0x0400296C RID: 10604
		private MyRandom rd = new MyRandom();

		// Token: 0x0400296D RID: 10605
		private MyVector fw = new MyVector();

		// Token: 0x0400296E RID: 10606
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
