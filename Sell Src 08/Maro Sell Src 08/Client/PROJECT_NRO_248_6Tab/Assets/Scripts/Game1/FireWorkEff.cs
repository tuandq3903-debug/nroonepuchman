using System;

namespace Game1
{
	// Token: 0x02000471 RID: 1137
	public class FireWorkEff
	{
		// Token: 0x06003280 RID: 12928 RVA: 0x00318C48 File Offset: 0x00316E48
		public static void preDraw()
		{
			if (FireWorkEff.st)
			{
				FireWorkEff.animate();
			}
			if (FireWorkEff.t > 32 && FireWorkEff.st)
			{
				FireWorkEff.st = false;
				FireWorkEff.mg.removeAllElements();
				FireWorkEff.mg.addElement(new FireWorkMn(Res.random(50, GameCanvas.w - 50), Res.random(GameCanvas.h - 100, GameCanvas.h), 5, 72));
			}
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x00318CB4 File Offset: 0x00316EB4
		public static void paint(mGraphics g)
		{
			FireWorkEff.preDraw();
			g.setColor(0);
			g.fillRect(0, 0, FireWorkEff.w, FireWorkEff.h);
			g.setColor(16711680);
			for (int i = 0; i < FireWorkEff.mg.size(); i++)
			{
				((FireWorkMn)FireWorkEff.mg.elementAt(i)).paint(g);
			}
			if (!FireWorkEff.st)
			{
				FireWorkEff.keyPressed(-(Math.abs(FireWorkEff.r.nextInt() % 3) + 5));
			}
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x00318D38 File Offset: 0x00316F38
		public static void keyPressed(int k)
		{
			if (k == -5 && !FireWorkEff.st)
			{
				FireWorkEff.x0 = FireWorkEff.w / 2;
				FireWorkEff.ag = 80;
				FireWorkEff.st = true;
				FireWorkEff.add();
				return;
			}
			if (k == -7 && !FireWorkEff.st)
			{
				FireWorkEff.ag = 60;
				FireWorkEff.x0 = 0;
				FireWorkEff.st = true;
				FireWorkEff.add();
				return;
			}
			if (k == -6 && !FireWorkEff.st)
			{
				FireWorkEff.ag = 120;
				FireWorkEff.x0 = FireWorkEff.w;
				FireWorkEff.st = true;
				FireWorkEff.add();
			}
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x00318DC0 File Offset: 0x00316FC0
		public static void add()
		{
			FireWorkEff.y0 = 0;
			FireWorkEff.v = 16;
			FireWorkEff.t = 0;
			FireWorkEff.a = 0f;
			for (int i = 0; i < 3; i++)
			{
				FireWorkEff.mang_y[i] = 0;
				FireWorkEff.mang_x[i] = FireWorkEff.x0;
			}
			FireWorkEff.st = true;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x00318E10 File Offset: 0x00317010
		public static void animate()
		{
			FireWorkEff.mang_y[2] = FireWorkEff.mang_y[1];
			FireWorkEff.mang_x[2] = FireWorkEff.mang_x[1];
			FireWorkEff.mang_y[1] = FireWorkEff.mang_y[0];
			FireWorkEff.mang_x[1] = FireWorkEff.mang_x[0];
			FireWorkEff.mang_y[0] = FireWorkEff.y;
			FireWorkEff.mang_x[0] = FireWorkEff.x;
			FireWorkEff.x = Res.cos((int)((double)FireWorkEff.ag * 3.141592653589793 / 180.0)) * FireWorkEff.v * FireWorkEff.t + FireWorkEff.x0;
			FireWorkEff.y = (int)((float)(FireWorkEff.v * Res.sin((int)((double)FireWorkEff.ag * 3.141592653589793 / 180.0)) * FireWorkEff.t) - FireWorkEff.a * (float)FireWorkEff.t * (float)FireWorkEff.t / 2f) + FireWorkEff.y0;
			if (FireWorkEff.time() - FireWorkEff.last >= FireWorkEff.delay)
			{
				FireWorkEff.t++;
				FireWorkEff.last = FireWorkEff.time();
			}
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x00318C2D File Offset: 0x00316E2D
		public static long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x040060D2 RID: 24786
		private static int w;

		// Token: 0x040060D3 RID: 24787
		private static int h;

		// Token: 0x040060D4 RID: 24788
		private static MyRandom r = new MyRandom();

		// Token: 0x040060D5 RID: 24789
		private static MyVector mg = new MyVector();

		// Token: 0x040060D6 RID: 24790
		private static int f = 17;

		// Token: 0x040060D7 RID: 24791
		private static int x;

		// Token: 0x040060D8 RID: 24792
		private static int y;

		// Token: 0x040060D9 RID: 24793
		private static int ag;

		// Token: 0x040060DA RID: 24794
		private static int x0;

		// Token: 0x040060DB RID: 24795
		private static int y0;

		// Token: 0x040060DC RID: 24796
		private static int t;

		// Token: 0x040060DD RID: 24797
		private static int v;

		// Token: 0x040060DE RID: 24798
		private static int ymax = 269;

		// Token: 0x040060DF RID: 24799
		private static float a;

		// Token: 0x040060E0 RID: 24800
		private static int[] mang_x = new int[3];

		// Token: 0x040060E1 RID: 24801
		private static int[] mang_y = new int[3];

		// Token: 0x040060E2 RID: 24802
		private static bool st = false;

		// Token: 0x040060E3 RID: 24803
		private static long last = 0L;

		// Token: 0x040060E4 RID: 24804
		private static long delay = 150L;
	}
}
