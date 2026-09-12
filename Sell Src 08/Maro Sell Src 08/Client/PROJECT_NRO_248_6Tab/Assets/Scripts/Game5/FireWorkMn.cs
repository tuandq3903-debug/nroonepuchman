using System;

namespace Game5
{
	// Token: 0x02000112 RID: 274
	public class FireWorkMn
	{
		// Token: 0x06000BF7 RID: 3063 RVA: 0x000C4CF0 File Offset: 0x000C2EF0
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

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000C4DB8 File Offset: 0x000C2FB8
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

		// Token: 0x040016E9 RID: 5865
		private int x;

		// Token: 0x040016EA RID: 5866
		private int y;

		// Token: 0x040016EB RID: 5867
		private int goc = 1;

		// Token: 0x040016EC RID: 5868
		private int n = 360;

		// Token: 0x040016ED RID: 5869
		private MyRandom rd = new MyRandom();

		// Token: 0x040016EE RID: 5870
		private MyVector fw = new MyVector();

		// Token: 0x040016EF RID: 5871
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
