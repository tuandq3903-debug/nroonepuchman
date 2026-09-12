using System;

namespace Game4.Assets.src.g
{
	// Token: 0x0200028A RID: 650
	public class RegisterScreen : mScreen, IActionListener
	{
		// Token: 0x06001CE1 RID: 7393 RVA: 0x001BE434 File Offset: 0x001BC634
		public RegisterScreen()
		{
			this.yLog = 130;
			TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
			if (TileMap.bgID == 5 || TileMap.bgID == 6)
			{
				TileMap.bgID = 4;
			}
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameScr.cmy = 200;
			this.tfUsername = new TField
			{
				width = 220,
				height = mScreen.ITEM_HEIGHT + 2,
				name = "Tên tài khoản",
				isFocus = true
			};
			this.tfPassword = new TField
			{
				width = 220,
				height = mScreen.ITEM_HEIGHT + 2,
				name = "Mật khẩu"
			};
			this.tfPassword.setIputType(TField.INPUT_TYPE_PASSWORD);
			this.tfMaGioiThieu = new TField
			{
				width = 220,
				height = mScreen.ITEM_HEIGHT + 2,
				name = "Mã giới thiệu"
			};
			this.focus = 0;
			int num = 4;
			int num2 = num * 32 + 23 + 33;
			if (num2 >= GameCanvas.w)
			{
				num--;
				num2 = num * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num2 / 2;
			this.yLog = 5;
			this.lY = ((GameCanvas.w < 200) ? (this.tfPassword.y - 30) : (this.yLog - 30));
			this.tfPassword.x = this.xLog + 10;
			this.tfPassword.y = this.yLog + 20;
			this.cmdOK = new Command(mResources.OK, this, 2008, null)
			{
				x = GameCanvas.w / 2 - 40,
				y = GameCanvas.h - 70
			};
			this.cmdExit = new Command("Thoát", this, 1003, null)
			{
				x = GameCanvas.w / 2 - 40,
				y = GameCanvas.h - 40
			};
			if (GameCanvas.w < 250)
			{
				this.cmdOK.x = GameCanvas.w / 2 - 80;
				this.cmdExit.x = GameCanvas.w / 2 + 10;
				this.cmdExit.y = (this.cmdOK.y = GameCanvas.h - 25);
			}
			this.center = this.cmdOK;
			this.left = this.cmdExit;
			RegisterScreen.imgTitle = ModFunc.imgLogoBig;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x001BE6C0 File Offset: 0x001BC8C0
		public new void switchToMe()
		{
			SoundMn.gI().stopAll();
			this.focus = 0;
			if (GameCanvas.isTouch)
			{
				this.tfUsername.isFocus = false;
				this.focus = -1;
			}
			base.switchToMe();
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x001BE6F4 File Offset: 0x001BC8F4
		public override void update()
		{
			this.tfPassword.update();
			this.tfUsername.update();
			this.tfMaGioiThieu.update();
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				((Effect2)Effect2.vEffect2.elementAt(i)).update();
			}
			GameScr.cmx++;
			if (GameScr.cmx > GameCanvas.w * 3 + 100)
			{
				GameScr.cmx = 100;
			}
			if (ChatPopup.currChatPopup == null && this.g >= 0)
			{
				this.ylogo += this.dir * this.g;
				this.g += this.dir * this.v;
				if (this.g <= 0)
				{
					this.dir *= -1;
				}
				if (this.ylogo > 0)
				{
					this.dir *= -1;
					this.g -= 2 * this.v;
				}
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x001BE7FC File Offset: 0x001BC9FC
		public override void keyPress(int keyCode)
		{
			if (this.tfPassword.isFocus)
			{
				this.tfPassword.keyPressed(keyCode);
			}
			else if (this.tfUsername.isFocus)
			{
				this.tfUsername.keyPressed(keyCode);
			}
			else if (this.tfMaGioiThieu.isFocus)
			{
				this.tfMaGioiThieu.keyPressed(keyCode);
			}
			base.keyPress(keyCode);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00172A82 File Offset: 0x00170C82
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x001BE864 File Offset: 0x001BCA64
		public override void paint(mGraphics g)
		{
			GameCanvas.paintBGGameScr(g);
			if (ChatPopup.currChatPopup != null || ChatPopup.serverChatPopUp != null)
			{
				return;
			}
			if (GameCanvas.currentDialog == null)
			{
				this.xLog = (GameCanvas.w - this.tfUsername.width) / 2 - 10;
				int num2 = this.tfUsername.height * 3 + 50;
				if (GameCanvas.w < 260)
				{
					this.xLog = (GameCanvas.w - 240) / 2;
				}
				this.yLog = (GameCanvas.h - num2) / 2;
				PopUp.paintPopUp(g, this.xLog, this.yLog, 240, num2, -1, true);
				if (GameCanvas.h > 160 && RegisterScreen.imgTitle != null)
				{
					g.drawImage(RegisterScreen.imgTitle, GameCanvas.hw, this.tfUsername.y - 40, 3);
				}
				this.tfUsername.x = this.xLog + 10;
				this.tfUsername.y = this.yLog + 15;
				this.tfPassword.x = this.tfUsername.x;
				this.tfPassword.y = this.tfUsername.y + 30;
				this.tfMaGioiThieu.x = this.tfPassword.x;
				this.tfMaGioiThieu.y = this.tfPassword.y + 30;
				this.tfPassword.paint(g);
				this.tfUsername.paint(g);
				this.tfMaGioiThieu.paint(g);
				if (GameCanvas.w < 176)
				{
					mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUsername.x - 35, this.tfUsername.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfPassword.x - 35, this.tfPassword.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.server + ": " + RegisterScreen.serverName, GameCanvas.w / 2, this.tfPassword.y + 32, 2);
				}
			}
			g.setColor(GameCanvas.skyColor);
			g.fillRect(GameCanvas.w - 40, 4, 36, 11);
			GameCanvas.resetTrans(g);
			base.paint(g);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x001BEABC File Offset: 0x001BCCBC
		private void turnOffFocus()
		{
			this.tfPassword.isFocus = false;
			this.tfUsername.isFocus = false;
			this.tfMaGioiThieu.isFocus = false;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x001BEAE4 File Offset: 0x001BCCE4
		private void processFocus()
		{
			this.turnOffFocus();
			switch (this.focus)
			{
			case 0:
				this.tfUsername.isFocus = true;
				return;
			case 1:
				this.tfPassword.isFocus = true;
				return;
			case 2:
				this.tfMaGioiThieu.isFocus = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x001BEB38 File Offset: 0x001BCD38
		public override void updateKey()
		{
			if (!GameCanvas.isTouch)
			{
				if (this.tfPassword.isFocus)
				{
					this.right = this.tfPassword.cmdClear;
				}
				else if (this.tfUsername.isFocus)
				{
					this.right = this.tfUsername.cmdClear;
				}
				else if (this.tfMaGioiThieu.isFocus)
				{
					this.right = this.tfMaGioiThieu.cmdClear;
				}
			}
			if (GameCanvas.keyPressed[21])
			{
				this.focus--;
				if (this.focus < 0)
				{
					this.focus = 2;
				}
				this.processFocus();
			}
			else if (GameCanvas.keyPressed[22])
			{
				this.focus++;
				if (this.focus > 2)
				{
					this.focus = 0;
				}
				this.processFocus();
			}
			if (GameCanvas.keyPressed[21] || GameCanvas.keyPressed[22])
			{
				GameCanvas.clearKeyPressed();
				if (this.focus == 1)
				{
					this.tfUsername.isFocus = false;
					this.tfPassword.isFocus = true;
				}
				else if (this.focus == 0)
				{
					this.tfUsername.isFocus = true;
					this.tfPassword.isFocus = false;
				}
				else
				{
					this.tfUsername.isFocus = false;
					this.tfPassword.isFocus = false;
				}
			}
			if (GameCanvas.isPointerJustRelease)
			{
				if (GameCanvas.isPointerHoldIn(this.tfPassword.x, this.tfPassword.y, this.tfPassword.width, this.tfPassword.height))
				{
					this.focus = 1;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfUsername.x, this.tfUsername.y, this.tfUsername.width, this.tfUsername.height))
				{
					this.focus = 0;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfMaGioiThieu.x, this.tfMaGioiThieu.y, this.tfMaGioiThieu.width, this.tfMaGioiThieu.height))
				{
					this.focus = 2;
					this.processFocus();
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x001BED5C File Offset: 0x001BCF5C
		public void perform(int idAction, object p)
		{
			if (idAction == 1003)
			{
				GameCanvas.serverScreen.switchToMe();
				return;
			}
			if (idAction != 2008)
			{
				return;
			}
			if (this.tfUsername.getText().Equals(string.Empty) || this.tfPassword.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg("Vui lòng điền đầy đủ thông tin");
				return;
			}
			GameCanvas.startOKDlg(mResources.PLEASEWAIT);
			Service.gI().charInfo("1", "1", "1", "1", "1", "1", (this.tfMaGioiThieu.getText().Length > 0) ? this.tfMaGioiThieu.getText() : "-1", this.tfUsername.getText(), this.tfPassword.getText());
		}

		// Token: 0x04003770 RID: 14192
		public TField tfPassword;

		// Token: 0x04003771 RID: 14193
		public TField tfUsername;

		// Token: 0x04003772 RID: 14194
		public TField tfMaGioiThieu;

		// Token: 0x04003773 RID: 14195
		private int focus;

		// Token: 0x04003774 RID: 14196
		private readonly Command cmdExit;

		// Token: 0x04003775 RID: 14197
		private readonly Command cmdOK;

		// Token: 0x04003776 RID: 14198
		public static string serverName;

		// Token: 0x04003777 RID: 14199
		public static Image imgTitle;

		// Token: 0x04003778 RID: 14200
		public int plX;

		// Token: 0x04003779 RID: 14201
		public int plY;

		// Token: 0x0400377A RID: 14202
		public int lY;

		// Token: 0x0400377B RID: 14203
		public int lX;

		// Token: 0x0400377C RID: 14204
		public int logoDes;

		// Token: 0x0400377D RID: 14205
		public int lineX;

		// Token: 0x0400377E RID: 14206
		public int lineY;

		// Token: 0x0400377F RID: 14207
		public static int[] bgId = new int[]
		{
			0,
			8,
			2,
			6,
			9
		};

		// Token: 0x04003780 RID: 14208
		public static bool isTryGetIPFromWap;

		// Token: 0x04003781 RID: 14209
		public static short timeLogin;

		// Token: 0x04003782 RID: 14210
		public static long lastTimeLogin;

		// Token: 0x04003783 RID: 14211
		public static long currTimeLogin;

		// Token: 0x04003784 RID: 14212
		private int xLog;

		// Token: 0x04003785 RID: 14213
		private int yLog;

		// Token: 0x04003786 RID: 14214
		private readonly int v = 2;

		// Token: 0x04003787 RID: 14215
		private int g;

		// Token: 0x04003788 RID: 14216
		private int ylogo = -40;

		// Token: 0x04003789 RID: 14217
		private int dir = 1;
	}
}
