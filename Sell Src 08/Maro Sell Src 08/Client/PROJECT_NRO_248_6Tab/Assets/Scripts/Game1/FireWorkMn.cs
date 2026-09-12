using System;

namespace Game1
{
	// Token: 0x02000472 RID: 1138
	public class FireWorkMn
	{
		// Token: 0x06003287 RID: 12935 RVA: 0x00318F80 File Offset: 0x00317180
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

		// Token: 0x06003288 RID: 12936 RVA: 0x00319048 File Offset: 0x00317248
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

		// Token: 0x040060E5 RID: 24805
		private int x;

		// Token: 0x040060E6 RID: 24806
		private int y;

		// Token: 0x040060E7 RID: 24807
		private int goc = 1;

		// Token: 0x040060E8 RID: 24808
		private int n = 360;

		// Token: 0x040060E9 RID: 24809
		private MyRandom rd = new MyRandom();

		// Token: 0x040060EA RID: 24810
		private MyVector fw = new MyVector();

		// Token: 0x040060EB RID: 24811
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
