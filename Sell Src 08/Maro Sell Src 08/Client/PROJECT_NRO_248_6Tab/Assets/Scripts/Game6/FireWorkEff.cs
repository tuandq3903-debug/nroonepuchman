using System;

namespace Game6
{
	// Token: 0x02000039 RID: 57
	public class FireWorkEff
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0002F7AC File Offset: 0x0002D9AC
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

		// Token: 0x0600024D RID: 589 RVA: 0x0002F818 File Offset: 0x0002DA18
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

		// Token: 0x0600024E RID: 590 RVA: 0x0002F89C File Offset: 0x0002DA9C
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

		// Token: 0x0600024F RID: 591 RVA: 0x0002F924 File Offset: 0x0002DB24
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

		// Token: 0x06000250 RID: 592 RVA: 0x0002F974 File Offset: 0x0002DB74
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

		// Token: 0x06000251 RID: 593 RVA: 0x0002F791 File Offset: 0x0002D991
		public static long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x04000457 RID: 1111
		private static int w;

		// Token: 0x04000458 RID: 1112
		private static int h;

		// Token: 0x04000459 RID: 1113
		private static MyRandom r = new MyRandom();

		// Token: 0x0400045A RID: 1114
		private static MyVector mg = new MyVector();

		// Token: 0x0400045B RID: 1115
		private static int f = 17;

		// Token: 0x0400045C RID: 1116
		private static int x;

		// Token: 0x0400045D RID: 1117
		private static int y;

		// Token: 0x0400045E RID: 1118
		private static int ag;

		// Token: 0x0400045F RID: 1119
		private static int x0;

		// Token: 0x04000460 RID: 1120
		private static int y0;

		// Token: 0x04000461 RID: 1121
		private static int t;

		// Token: 0x04000462 RID: 1122
		private static int v;

		// Token: 0x04000463 RID: 1123
		private static int ymax = 269;

		// Token: 0x04000464 RID: 1124
		private static float a;

		// Token: 0x04000465 RID: 1125
		private static int[] mang_x = new int[3];

		// Token: 0x04000466 RID: 1126
		private static int[] mang_y = new int[3];

		// Token: 0x04000467 RID: 1127
		private static bool st = false;

		// Token: 0x04000468 RID: 1128
		private static long last = 0L;

		// Token: 0x04000469 RID: 1129
		private static long delay = 150L;
	}
}
