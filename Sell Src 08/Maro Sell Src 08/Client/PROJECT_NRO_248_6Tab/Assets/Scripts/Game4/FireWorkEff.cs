using System;

namespace Game4
{
	// Token: 0x020001E9 RID: 489
	public class FireWorkEff
	{
		// Token: 0x06001594 RID: 5524 RVA: 0x00159A5C File Offset: 0x00157C5C
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

		// Token: 0x06001595 RID: 5525 RVA: 0x00159AC8 File Offset: 0x00157CC8
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

		// Token: 0x06001596 RID: 5526 RVA: 0x00159B4C File Offset: 0x00157D4C
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

		// Token: 0x06001597 RID: 5527 RVA: 0x00159BD4 File Offset: 0x00157DD4
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

		// Token: 0x06001598 RID: 5528 RVA: 0x00159C24 File Offset: 0x00157E24
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

		// Token: 0x06001599 RID: 5529 RVA: 0x00159A41 File Offset: 0x00157C41
		public static long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x04002955 RID: 10581
		private static int w;

		// Token: 0x04002956 RID: 10582
		private static int h;

		// Token: 0x04002957 RID: 10583
		private static MyRandom r = new MyRandom();

		// Token: 0x04002958 RID: 10584
		private static MyVector mg = new MyVector();

		// Token: 0x04002959 RID: 10585
		private static int f = 17;

		// Token: 0x0400295A RID: 10586
		private static int x;

		// Token: 0x0400295B RID: 10587
		private static int y;

		// Token: 0x0400295C RID: 10588
		private static int ag;

		// Token: 0x0400295D RID: 10589
		private static int x0;

		// Token: 0x0400295E RID: 10590
		private static int y0;

		// Token: 0x0400295F RID: 10591
		private static int t;

		// Token: 0x04002960 RID: 10592
		private static int v;

		// Token: 0x04002961 RID: 10593
		private static int ymax = 269;

		// Token: 0x04002962 RID: 10594
		private static float a;

		// Token: 0x04002963 RID: 10595
		private static int[] mang_x = new int[3];

		// Token: 0x04002964 RID: 10596
		private static int[] mang_y = new int[3];

		// Token: 0x04002965 RID: 10597
		private static bool st = false;

		// Token: 0x04002966 RID: 10598
		private static long last = 0L;

		// Token: 0x04002967 RID: 10599
		private static long delay = 150L;
	}
}
