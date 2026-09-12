using System;

namespace Game3
{
	// Token: 0x020002C1 RID: 705
	public class FireWorkEff
	{
		// Token: 0x06001F38 RID: 7992 RVA: 0x001EEB00 File Offset: 0x001ECD00
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

		// Token: 0x06001F39 RID: 7993 RVA: 0x001EEB6C File Offset: 0x001ECD6C
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

		// Token: 0x06001F3A RID: 7994 RVA: 0x001EEBF0 File Offset: 0x001ECDF0
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

		// Token: 0x06001F3B RID: 7995 RVA: 0x001EEC78 File Offset: 0x001ECE78
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

		// Token: 0x06001F3C RID: 7996 RVA: 0x001EECC8 File Offset: 0x001ECEC8
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

		// Token: 0x06001F3D RID: 7997 RVA: 0x001EEAE5 File Offset: 0x001ECCE5
		public static long time()
		{
			return mSystem.currentTimeMillis();
		}

		// Token: 0x04003BD4 RID: 15316
		private static int w;

		// Token: 0x04003BD5 RID: 15317
		private static int h;

		// Token: 0x04003BD6 RID: 15318
		private static MyRandom r = new MyRandom();

		// Token: 0x04003BD7 RID: 15319
		private static MyVector mg = new MyVector();

		// Token: 0x04003BD8 RID: 15320
		private static int f = 17;

		// Token: 0x04003BD9 RID: 15321
		private static int x;

		// Token: 0x04003BDA RID: 15322
		private static int y;

		// Token: 0x04003BDB RID: 15323
		private static int ag;

		// Token: 0x04003BDC RID: 15324
		private static int x0;

		// Token: 0x04003BDD RID: 15325
		private static int y0;

		// Token: 0x04003BDE RID: 15326
		private static int t;

		// Token: 0x04003BDF RID: 15327
		private static int v;

		// Token: 0x04003BE0 RID: 15328
		private static int ymax = 269;

		// Token: 0x04003BE1 RID: 15329
		private static float a;

		// Token: 0x04003BE2 RID: 15330
		private static int[] mang_x = new int[3];

		// Token: 0x04003BE3 RID: 15331
		private static int[] mang_y = new int[3];

		// Token: 0x04003BE4 RID: 15332
		private static bool st = false;

		// Token: 0x04003BE5 RID: 15333
		private static long last = 0L;

		// Token: 0x04003BE6 RID: 15334
		private static long delay = 150L;
	}
}
