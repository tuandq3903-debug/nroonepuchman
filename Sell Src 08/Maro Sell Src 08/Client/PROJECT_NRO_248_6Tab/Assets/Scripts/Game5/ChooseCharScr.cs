using System;

namespace Game5
{
	// Token: 0x020000F5 RID: 245
	public class ChooseCharScr : mScreen, IActionListener
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x000ABE13 File Offset: 0x000AA013
		public override void switchToMe()
		{
			ServerListScreen.isWait = false;
			Char.isLoadingMap = false;
			LoginScr.isContinueToLogin = false;
			ServerListScreen.waitToLogin = false;
			GameScr.gI().initSelectChar();
			base.switchToMe();
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000ABE40 File Offset: 0x000AA040
		public override void update()
		{
			if (GameCanvas.gameTick % 10 > 2)
			{
				this.cf = 1;
			}
			else
			{
				this.cf = 0;
			}
			for (int i = 0; i < this.vc_players.Length; i++)
			{
				if (this.vc_players[i].isPointerPressInside())
				{
					this.vc_players[i].performAction();
				}
			}
			for (int j = 0; j < this.cx.Length; j++)
			{
				if (GameCanvas.isPointerHoldIn(this.cx[j] + this.offsetX, this.cy[j] + this.offsetY, this.rectPanel[2], 60))
				{
					if (GameCanvas.isPointerDown)
					{
						this.focus = j;
						break;
					}
					if (GameCanvas.isPointerJustRelease)
					{
						bool isPointerClick = GameCanvas.isPointerClick;
					}
				}
			}
			base.update();
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000ABEFC File Offset: 0x000AA0FC
		public override void paint(mGraphics g)
		{
			GameCanvas.paintBGGameScr(g);
			try
			{
				PopUp.paintPopUp(g, this.rectPanel[0] - 10, this.rectPanel[1], this.rectPanel[2] + 20, this.rectPanel[3], 16777215, true);
				if (this.vc_players != null)
				{
					for (int i = 0; i < this.vc_players.Length; i++)
					{
						this.vc_players[i].paint(g);
					}
				}
				if (ChooseCharScr.playerData != null)
				{
					for (int j = 0; j < ChooseCharScr.playerData.Length; j++)
					{
						PopUp.paintPopUp(g, this.cx[j] - 20, this.cy[j] + this.offsetY, this.rectPanel[2], 60, 16777215, false);
						Part part = GameScr.parts[(int)ChooseCharScr.playerData[j].head];
						Part part2 = GameScr.parts[(int)ChooseCharScr.playerData[j].leg];
						Part part3 = GameScr.parts[(int)ChooseCharScr.playerData[j].body];
						SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[this.cf][0][0]].id, this.cx[j] + Char.CharInfo[this.cf][0][1] + (int)part.pi[Char.CharInfo[this.cf][0][0]].dx, this.cy[j] - Char.CharInfo[this.cf][0][2] + (int)part.pi[Char.CharInfo[this.cf][0][0]].dy, 0, 0);
						SmallImage.drawSmallImage(g, (int)part2.pi[Char.CharInfo[this.cf][1][0]].id, this.cx[j] + Char.CharInfo[this.cf][1][1] + (int)part2.pi[Char.CharInfo[this.cf][1][0]].dx, this.cy[j] - Char.CharInfo[this.cf][1][2] + (int)part2.pi[Char.CharInfo[this.cf][1][0]].dy, 0, 0);
						SmallImage.drawSmallImage(g, (int)part3.pi[Char.CharInfo[this.cf][2][0]].id, this.cx[j] + Char.CharInfo[this.cf][2][1] + (int)part3.pi[Char.CharInfo[this.cf][2][0]].dx, this.cy[j] - Char.CharInfo[this.cf][2][2] + (int)part3.pi[Char.CharInfo[this.cf][2][0]].dy, 0, 0);
						if (this.focus == j)
						{
							mFont.tahoma_7b_yellow.drawString(g, ChooseCharScr.playerData[j].name, this.cx[j] + this.rectPanel[2] - 25, this.cy[j] + this.offsetY, 1);
							mFont.tahoma_7b_yellow.drawString(g, mResources.power_point + " " + Res.formatNumber2(ChooseCharScr.playerData[j].powpoint), this.cx[j] + this.rectPanel[2] - 25, this.cy[j] + this.offsetY + mFont.tahoma_7b_yellow.getHeight(), 1);
						}
						else
						{
							mFont.tahoma_7b_dark.drawString(g, ChooseCharScr.playerData[j].name, this.cx[j] + this.rectPanel[2] - 25, this.cy[j] + this.offsetY, 1);
							mFont.tahoma_7b_dark.drawString(g, mResources.power_point + " " + Res.formatNumber2(ChooseCharScr.playerData[j].powpoint), this.cx[j] + this.rectPanel[2] - 25, this.cy[j] + this.offsetY + mFont.tahoma_7b_dark.getHeight(), 1);
						}
					}
				}
			}
			catch (Exception)
			{
			}
			base.paint(g);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000AC30C File Offset: 0x000AA50C
		internal void updateChooseCharacter(byte len)
		{
			this.cx = new int[(int)len];
			this.cy = new int[(int)len];
			for (int i = 0; i < (int)len; i++)
			{
				this.cx[i] = this.rectPanel[0] + 20;
				this.cy[i] = i * 70 + this.rectPanel[1] + 50;
			}
			this.vc_players = new Command[2];
			this.vc_players[1] = new Command("Vào game", this, 1, null, this.rectPanel[0] + this.rectPanel[2] - 80 - 80, this.rectPanel[1] + this.rectPanel[3] - 30);
			this.vc_players[0] = new Command("Trờ ra", this, 2, null, this.rectPanel[0] + this.rectPanel[2] - 80, this.rectPanel[1] + this.rectPanel[3] - 30);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000AC3F4 File Offset: 0x000AA5F4
		public void perform(int idAction, object p)
		{
			if (idAction != 1)
			{
				if (idAction != 2)
				{
					return;
				}
				GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
			}
			else if (this.focus != -1)
			{
				GameCanvas.startWaitDlg();
				Service.gI().finishUpdate(ChooseCharScr.playerData[this.focus].playerID);
				return;
			}
		}

		// Token: 0x04001571 RID: 5489
		public Command[] vc_players;

		// Token: 0x04001572 RID: 5490
		public static PlayerData[] playerData;

		// Token: 0x04001573 RID: 5491
		private int cf;

		// Token: 0x04001574 RID: 5492
		private int[] cx = new int[]
		{
			GameCanvas.w / 2 - 100,
			GameCanvas.w / 2 - 100
		};

		// Token: 0x04001575 RID: 5493
		private int focus;

		// Token: 0x04001576 RID: 5494
		private int[] cy = new int[2];

		// Token: 0x04001577 RID: 5495
		private int[] rectPanel = new int[]
		{
			GameCanvas.w / 2 - 150,
			GameCanvas.h / 2 - 100,
			300,
			200
		};

		// Token: 0x04001578 RID: 5496
		private int offsetY = -35;

		// Token: 0x04001579 RID: 5497
		private int offsetX = -35;
	}
}
