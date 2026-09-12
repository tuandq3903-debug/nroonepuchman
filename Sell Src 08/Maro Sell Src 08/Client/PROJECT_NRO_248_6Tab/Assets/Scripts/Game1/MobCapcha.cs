using System;

namespace Game1
{
	// Token: 0x020004A5 RID: 1189
	public class MobCapcha
	{
		// Token: 0x060034D0 RID: 13520 RVA: 0x00339F50 File Offset: 0x00338150
		public static void init()
		{
			MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x00339F64 File Offset: 0x00338164
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

		// Token: 0x060034D2 RID: 13522 RVA: 0x0033A150 File Offset: 0x00338350
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

		// Token: 0x04006681 RID: 26241
		public static Image imgMob;

		// Token: 0x04006682 RID: 26242
		public static int cmtoY;

		// Token: 0x04006683 RID: 26243
		public static int cmy;

		// Token: 0x04006684 RID: 26244
		public static int cmdy;

		// Token: 0x04006685 RID: 26245
		public static int cmvy;

		// Token: 0x04006686 RID: 26246
		public static int cmtoX;

		// Token: 0x04006687 RID: 26247
		public static int cmx;

		// Token: 0x04006688 RID: 26248
		public static int cmdx;

		// Token: 0x04006689 RID: 26249
		public static int cmvx;

		// Token: 0x0400668A RID: 26250
		public static bool explode;

		// Token: 0x0400668B RID: 26251
		public static int delay;

		// Token: 0x0400668C RID: 26252
		public static bool isCreateMob;

		// Token: 0x0400668D RID: 26253
		public static int tF;

		// Token: 0x0400668E RID: 26254
		public static int f;

		// Token: 0x0400668F RID: 26255
		public static int dir;

		// Token: 0x04006690 RID: 26256
		public static bool isAttack;
	}
}
