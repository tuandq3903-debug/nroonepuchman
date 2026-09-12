using System;

namespace Game2
{
	// Token: 0x020003CD RID: 973
	public class MobCapcha
	{
		// Token: 0x06002B2C RID: 11052 RVA: 0x002A4EAC File Offset: 0x002A30AC
		public static void init()
		{
			MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x002A4EC0 File Offset: 0x002A30C0
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

		// Token: 0x06002B2E RID: 11054 RVA: 0x002A50AC File Offset: 0x002A32AC
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

		// Token: 0x04005402 RID: 21506
		public static Image imgMob;

		// Token: 0x04005403 RID: 21507
		public static int cmtoY;

		// Token: 0x04005404 RID: 21508
		public static int cmy;

		// Token: 0x04005405 RID: 21509
		public static int cmdy;

		// Token: 0x04005406 RID: 21510
		public static int cmvy;

		// Token: 0x04005407 RID: 21511
		public static int cmtoX;

		// Token: 0x04005408 RID: 21512
		public static int cmx;

		// Token: 0x04005409 RID: 21513
		public static int cmdx;

		// Token: 0x0400540A RID: 21514
		public static int cmvx;

		// Token: 0x0400540B RID: 21515
		public static bool explode;

		// Token: 0x0400540C RID: 21516
		public static int delay;

		// Token: 0x0400540D RID: 21517
		public static bool isCreateMob;

		// Token: 0x0400540E RID: 21518
		public static int tF;

		// Token: 0x0400540F RID: 21519
		public static int f;

		// Token: 0x04005410 RID: 21520
		public static int dir;

		// Token: 0x04005411 RID: 21521
		public static bool isAttack;
	}
}
