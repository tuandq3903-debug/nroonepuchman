using System;

namespace Game4
{
	// Token: 0x0200021D RID: 541
	public class MobCapcha
	{
		// Token: 0x060017E4 RID: 6116 RVA: 0x0017AD64 File Offset: 0x00178F64
		public static void init()
		{
			MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0017AD78 File Offset: 0x00178F78
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

		// Token: 0x060017E6 RID: 6118 RVA: 0x0017AF64 File Offset: 0x00179164
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

		// Token: 0x04002F04 RID: 12036
		public static Image imgMob;

		// Token: 0x04002F05 RID: 12037
		public static int cmtoY;

		// Token: 0x04002F06 RID: 12038
		public static int cmy;

		// Token: 0x04002F07 RID: 12039
		public static int cmdy;

		// Token: 0x04002F08 RID: 12040
		public static int cmvy;

		// Token: 0x04002F09 RID: 12041
		public static int cmtoX;

		// Token: 0x04002F0A RID: 12042
		public static int cmx;

		// Token: 0x04002F0B RID: 12043
		public static int cmdx;

		// Token: 0x04002F0C RID: 12044
		public static int cmvx;

		// Token: 0x04002F0D RID: 12045
		public static bool explode;

		// Token: 0x04002F0E RID: 12046
		public static int delay;

		// Token: 0x04002F0F RID: 12047
		public static bool isCreateMob;

		// Token: 0x04002F10 RID: 12048
		public static int tF;

		// Token: 0x04002F11 RID: 12049
		public static int f;

		// Token: 0x04002F12 RID: 12050
		public static int dir;

		// Token: 0x04002F13 RID: 12051
		public static bool isAttack;
	}
}
