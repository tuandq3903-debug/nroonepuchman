using System;

namespace Game5.Assets.src.g
{
	// Token: 0x020001B2 RID: 434
	public class RegisterScreen : mScreen, IActionListener
	{
		// Token: 0x0600133D RID: 4925 RVA: 0x00129390 File Offset: 0x00127590
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

		// Token: 0x0600133E RID: 4926 RVA: 0x0012961C File Offset: 0x0012781C
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

		// Token: 0x0600133F RID: 4927 RVA: 0x00129650 File Offset: 0x00127850
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

		// Token: 0x06001340 RID: 4928 RVA: 0x00129758 File Offset: 0x00127958
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

		// Token: 0x06001341 RID: 4929 RVA: 0x000DD9DE File Offset: 0x000DBBDE
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x001297C0 File Offset: 0x001279C0
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

		// Token: 0x06001343 RID: 4931 RVA: 0x00129A18 File Offset: 0x00127C18
		private void turnOffFocus()
		{
			this.tfPassword.isFocus = false;
			this.tfUsername.isFocus = false;
			this.tfMaGioiThieu.isFocus = false;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00129A40 File Offset: 0x00127C40
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

		// Token: 0x06001345 RID: 4933 RVA: 0x00129A94 File Offset: 0x00127C94
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

		// Token: 0x06001346 RID: 4934 RVA: 0x00129CB8 File Offset: 0x00127EB8
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

		// Token: 0x040024F1 RID: 9457
		public TField tfPassword;

		// Token: 0x040024F2 RID: 9458
		public TField tfUsername;

		// Token: 0x040024F3 RID: 9459
		public TField tfMaGioiThieu;

		// Token: 0x040024F4 RID: 9460
		private int focus;

		// Token: 0x040024F5 RID: 9461
		private readonly Command cmdExit;

		// Token: 0x040024F6 RID: 9462
		private readonly Command cmdOK;

		// Token: 0x040024F7 RID: 9463
		public static string serverName;

		// Token: 0x040024F8 RID: 9464
		public static Image imgTitle;

		// Token: 0x040024F9 RID: 9465
		public int plX;

		// Token: 0x040024FA RID: 9466
		public int plY;

		// Token: 0x040024FB RID: 9467
		public int lY;

		// Token: 0x040024FC RID: 9468
		public int lX;

		// Token: 0x040024FD RID: 9469
		public int logoDes;

		// Token: 0x040024FE RID: 9470
		public int lineX;

		// Token: 0x040024FF RID: 9471
		public int lineY;

		// Token: 0x04002500 RID: 9472
		public static int[] bgId = new int[]
		{
			0,
			8,
			2,
			6,
			9
		};

		// Token: 0x04002501 RID: 9473
		public static bool isTryGetIPFromWap;

		// Token: 0x04002502 RID: 9474
		public static short timeLogin;

		// Token: 0x04002503 RID: 9475
		public static long lastTimeLogin;

		// Token: 0x04002504 RID: 9476
		public static long currTimeLogin;

		// Token: 0x04002505 RID: 9477
		private int xLog;

		// Token: 0x04002506 RID: 9478
		private int yLog;

		// Token: 0x04002507 RID: 9479
		private readonly int v = 2;

		// Token: 0x04002508 RID: 9480
		private int g;

		// Token: 0x04002509 RID: 9481
		private int ylogo = -40;

		// Token: 0x0400250A RID: 9482
		private int dir = 1;
	}
}
