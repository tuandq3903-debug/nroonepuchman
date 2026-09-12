using System;

namespace Game2
{
	// Token: 0x0200037D RID: 893
	public class ChooseCharScr : mScreen, IActionListener
	{
		// Token: 0x060027EA RID: 10218 RVA: 0x0026AFFF File Offset: 0x002691FF
		public override void switchToMe()
		{
			ServerListScreen.isWait = false;
			Char.isLoadingMap = false;
			LoginScr.isContinueToLogin = false;
			ServerListScreen.waitToLogin = false;
			GameScr.gI().initSelectChar();
			base.switchToMe();
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x0026B02C File Offset: 0x0026922C
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

		// Token: 0x060027EC RID: 10220 RVA: 0x0026B0E8 File Offset: 0x002692E8
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

		// Token: 0x060027ED RID: 10221 RVA: 0x0026B4F8 File Offset: 0x002696F8
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

		// Token: 0x060027EE RID: 10222 RVA: 0x0026B5E0 File Offset: 0x002697E0
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

		// Token: 0x04004CEE RID: 19694
		public Command[] vc_players;

		// Token: 0x04004CEF RID: 19695
		public static PlayerData[] playerData;

		// Token: 0x04004CF0 RID: 19696
		private int cf;

		// Token: 0x04004CF1 RID: 19697
		private int[] cx = new int[]
		{
			GameCanvas.w / 2 - 100,
			GameCanvas.w / 2 - 100
		};

		// Token: 0x04004CF2 RID: 19698
		private int focus;

		// Token: 0x04004CF3 RID: 19699
		private int[] cy = new int[2];

		// Token: 0x04004CF4 RID: 19700
		private int[] rectPanel = new int[]
		{
			GameCanvas.w / 2 - 150,
			GameCanvas.h / 2 - 100,
			300,
			200
		};

		// Token: 0x04004CF5 RID: 19701
		private int offsetY = -35;

		// Token: 0x04004CF6 RID: 19702
		private int offsetX = -35;
	}
}
