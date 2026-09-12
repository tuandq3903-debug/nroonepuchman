using System;

namespace Game5
{
	// Token: 0x02000111 RID: 273
	public class FireWorkEff
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x000C49B8 File Offset: 0x000C2BB8
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

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000C4A24 File Offset: 0x000C2C24
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

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000C4AA8 File Offset: 0x000C2CA8
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

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000C4B30 File Offset: 0x000C2D30
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

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000C4B80 File Offset: 0x000C2D80
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

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000C499D File Offset: 0x000C2B9D
		public static long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x040016D6 RID: 5846
		private static int w;

		// Token: 0x040016D7 RID: 5847
		private static int h;

		// Token: 0x040016D8 RID: 5848
		private static MyRandom r = new MyRandom();

		// Token: 0x040016D9 RID: 5849
		private static MyVector mg = new MyVector();

		// Token: 0x040016DA RID: 5850
		private static int f = 17;

		// Token: 0x040016DB RID: 5851
		private static int x;

		// Token: 0x040016DC RID: 5852
		private static int y;

		// Token: 0x040016DD RID: 5853
		private static int ag;

		// Token: 0x040016DE RID: 5854
		private static int x0;

		// Token: 0x040016DF RID: 5855
		private static int y0;

		// Token: 0x040016E0 RID: 5856
		private static int t;

		// Token: 0x040016E1 RID: 5857
		private static int v;

		// Token: 0x040016E2 RID: 5858
		private static int ymax = 269;

		// Token: 0x040016E3 RID: 5859
		private static float a;

		// Token: 0x040016E4 RID: 5860
		private static int[] mang_x = new int[3];

		// Token: 0x040016E5 RID: 5861
		private static int[] mang_y = new int[3];

		// Token: 0x040016E6 RID: 5862
		private static bool st = false;

		// Token: 0x040016E7 RID: 5863
		private static long last = 0L;

		// Token: 0x040016E8 RID: 5864
		private static long delay = 150L;
	}
}
