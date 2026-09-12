using System;

namespace Game6
{
	// Token: 0x0200006D RID: 109
	public class MobCapcha
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00050B44 File Offset: 0x0004ED44
		public static void init()
		{
			MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00050B58 File Offset: 0x0004ED58
		public static void paint(mGraphics g, int x, int y)
		{
			if (!MobCapcha.isAttack)
			{
				if (GameCanvas.gameTick % 3 == 0)
				{
					if (Char.myCharz().cdir == 1)
					{
						MobCapcha.cmtoX = x - 20 - GameScr.cmx;
					}
					if (Char.myCharz().cdir == -1)
					{
						MobCapcha.cmtoX = x + 20 - GameScr.cmx;
					}
				}
				MobCapcha.cmtoY = Char.myCharz().cy - 40 - GameScr.cmy;
			}
			else
			{
				MobCapcha.delay++;
				if (MobCapcha.delay == 5)
				{
					MobCapcha.isAttack = false;
					MobCapcha.delay = 0;
				}
				MobCapcha.cmtoX = x - GameScr.cmx;
				MobCapcha.cmtoY = y - GameScr.cmy;
			}
			if (MobCapcha.cmx > x - GameScr.cmx)
			{
				MobCapcha.dir = -1;
			}
			else
			{
				MobCapcha.dir = 1;
			}
			g.drawImage(GameScr.imgCapcha, MobCapcha.cmx, MobCapcha.cmy - 40, 3);
			PopUp.paintPopUp(g, MobCapcha.cmx - 25, MobCapcha.cmy - 70, 50, 20, 16777215, false);
			mFont.tahoma_7b_dark.drawString(g, GameScr.gI().keyInput, MobCapcha.cmx, MobCapcha.cmy - 65, 2);
			if (MobCapcha.isCreateMob)
			{
				MobCapcha.isCreateMob = false;
				EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
			}
			if (MobCapcha.explode)
			{
				MobCapcha.explode = false;
				EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
				GameScr.gI().mobCapcha = null;
				MobCapcha.cmtoX = -GameScr.cmx;
				MobCapcha.cmtoY = -GameScr.cmy;
			}
			g.drawRegion(MobCapcha.imgMob, 0, MobCapcha.f * 40, 40, 40, (MobCapcha.dir != 1) ? 2 : 0, MobCapcha.cmx, MobCapcha.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 3);
			MobCapcha.moveCamera();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00050D44 File Offset: 0x0004EF44
		public static void moveCamera()
		{
			if (MobCapcha.cmy != MobCapcha.cmtoY)
			{
				MobCapcha.cmvy = MobCapcha.cmtoY - MobCapcha.cmy << 2;
				MobCapcha.cmdy += MobCapcha.cmvy;
				MobCapcha.cmy += MobCapcha.cmdy >> 4;
				MobCapcha.cmdy &= 15;
			}
			if (MobCapcha.cmx != MobCapcha.cmtoX)
			{
				MobCapcha.cmvx = MobCapcha.cmtoX - MobCapcha.cmx << 2;
				MobCapcha.cmdx += MobCapcha.cmvx;
				MobCapcha.cmx += MobCapcha.cmdx >> 4;
				MobCapcha.cmdx &= 15;
			}
			MobCapcha.tF++;
			if (MobCapcha.tF == 5)
			{
				MobCapcha.tF = 0;
				MobCapcha.f++;
				if (MobCapcha.f > 2)
				{
					MobCapcha.f = 0;
				}
			}
		}

		// Token: 0x04000A06 RID: 2566
		public static Image imgMob;

		// Token: 0x04000A07 RID: 2567
		public static int cmtoY;

		// Token: 0x04000A08 RID: 2568
		public static int cmy;

		// Token: 0x04000A09 RID: 2569
		public static int cmdy;

		// Token: 0x04000A0A RID: 2570
		public static int cmvy;

		// Token: 0x04000A0B RID: 2571
		public static int cmtoX;

		// Token: 0x04000A0C RID: 2572
		public static int cmx;

		// Token: 0x04000A0D RID: 2573
		public static int cmdx;

		// Token: 0x04000A0E RID: 2574
		public static int cmvx;

		// Token: 0x04000A0F RID: 2575
		public static bool explode;

		// Token: 0x04000A10 RID: 2576
		public static int delay;

		// Token: 0x04000A11 RID: 2577
		public static bool isCreateMob;

		// Token: 0x04000A12 RID: 2578
		public static int tF;

		// Token: 0x04000A13 RID: 2579
		public static int f;

		// Token: 0x04000A14 RID: 2580
		public static int dir;

		// Token: 0x04000A15 RID: 2581
		public static bool isAttack;
	}
}
