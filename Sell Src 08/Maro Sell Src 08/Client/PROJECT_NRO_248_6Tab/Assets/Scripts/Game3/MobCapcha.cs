using System;

namespace Game3
{
	// Token: 0x020002F5 RID: 757
	public class MobCapcha
	{
		// Token: 0x06002188 RID: 8584 RVA: 0x0020FE08 File Offset: 0x0020E008
		public static void init()
		{
			MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0020FE1C File Offset: 0x0020E01C
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

		// Token: 0x0600218A RID: 8586 RVA: 0x00210008 File Offset: 0x0020E208
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

		// Token: 0x04004183 RID: 16771
		public static Image imgMob;

		// Token: 0x04004184 RID: 16772
		public static int cmtoY;

		// Token: 0x04004185 RID: 16773
		public static int cmy;

		// Token: 0x04004186 RID: 16774
		public static int cmdy;

		// Token: 0x04004187 RID: 16775
		public static int cmvy;

		// Token: 0x04004188 RID: 16776
		public static int cmtoX;

		// Token: 0x04004189 RID: 16777
		public static int cmx;

		// Token: 0x0400418A RID: 16778
		public static int cmdx;

		// Token: 0x0400418B RID: 16779
		public static int cmvx;

		// Token: 0x0400418C RID: 16780
		public static bool explode;

		// Token: 0x0400418D RID: 16781
		public static int delay;

		// Token: 0x0400418E RID: 16782
		public static bool isCreateMob;

		// Token: 0x0400418F RID: 16783
		public static int tF;

		// Token: 0x04004190 RID: 16784
		public static int f;

		// Token: 0x04004191 RID: 16785
		public static int dir;

		// Token: 0x04004192 RID: 16786
		public static bool isAttack;
	}
}
