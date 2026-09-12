using System;

namespace Game2
{
	// Token: 0x02000399 RID: 921
	public class FireWorkEff
	{
		// Token: 0x060028DC RID: 10460 RVA: 0x00283BA4 File Offset: 0x00281DA4
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

		// Token: 0x060028DD RID: 10461 RVA: 0x00283C10 File Offset: 0x00281E10
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

		// Token: 0x060028DE RID: 10462 RVA: 0x00283C94 File Offset: 0x00281E94
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

		// Token: 0x060028DF RID: 10463 RVA: 0x00283D1C File Offset: 0x00281F1C
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

		// Token: 0x060028E0 RID: 10464 RVA: 0x00283D6C File Offset: 0x00281F6C
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

		// Token: 0x060028E1 RID: 10465 RVA: 0x00283B89 File Offset: 0x00281D89
		public static long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x04004E53 RID: 20051
		private static int w;

		// Token: 0x04004E54 RID: 20052
		private static int h;

		// Token: 0x04004E55 RID: 20053
		private static MyRandom r = new MyRandom();

		// Token: 0x04004E56 RID: 20054
		private static MyVector mg = new MyVector();

		// Token: 0x04004E57 RID: 20055
		private static int f = 17;

		// Token: 0x04004E58 RID: 20056
		private static int x;

		// Token: 0x04004E59 RID: 20057
		private static int y;

		// Token: 0x04004E5A RID: 20058
		private static int ag;

		// Token: 0x04004E5B RID: 20059
		private static int x0;

		// Token: 0x04004E5C RID: 20060
		private static int y0;

		// Token: 0x04004E5D RID: 20061
		private static int t;

		// Token: 0x04004E5E RID: 20062
		private static int v;

		// Token: 0x04004E5F RID: 20063
		private static int ymax = 269;

		// Token: 0x04004E60 RID: 20064
		private static float a;

		// Token: 0x04004E61 RID: 20065
		private static int[] mang_x = new int[3];

		// Token: 0x04004E62 RID: 20066
		private static int[] mang_y = new int[3];

		// Token: 0x04004E63 RID: 20067
		private static bool st = false;

		// Token: 0x04004E64 RID: 20068
		private static long last = 0L;

		// Token: 0x04004E65 RID: 20069
		private static long delay = 150L;
	}
}
