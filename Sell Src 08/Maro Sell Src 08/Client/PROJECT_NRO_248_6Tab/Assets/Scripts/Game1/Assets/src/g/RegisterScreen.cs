using System;

namespace Game1.Assets.src.g
{
	// Token: 0x02000512 RID: 1298
	public class RegisterScreen : mScreen, IActionListener
	{
		// Token: 0x060039CD RID: 14797 RVA: 0x0037D620 File Offset: 0x0037B820
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

		// Token: 0x060039CE RID: 14798 RVA: 0x0037D8AC File Offset: 0x0037BAAC
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

		// Token: 0x060039CF RID: 14799 RVA: 0x0037D8E0 File Offset: 0x0037BAE0
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

		// Token: 0x060039D0 RID: 14800 RVA: 0x0037D9E8 File Offset: 0x0037BBE8
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

		// Token: 0x060039D1 RID: 14801 RVA: 0x00331C6E File Offset: 0x0032FE6E
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x0037DA50 File Offset: 0x0037BC50
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

		// Token: 0x060039D3 RID: 14803 RVA: 0x0037DCA8 File Offset: 0x0037BEA8
		private void turnOffFocus()
		{
			this.tfPassword.isFocus = false;
			this.tfUsername.isFocus = false;
			this.tfMaGioiThieu.isFocus = false;
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x0037DCD0 File Offset: 0x0037BED0
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

		// Token: 0x060039D5 RID: 14805 RVA: 0x0037DD24 File Offset: 0x0037BF24
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

		// Token: 0x060039D6 RID: 14806 RVA: 0x0037DF48 File Offset: 0x0037C148
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

		// Token: 0x04006EED RID: 28397
		public TField tfPassword;

		// Token: 0x04006EEE RID: 28398
		public TField tfUsername;

		// Token: 0x04006EEF RID: 28399
		public TField tfMaGioiThieu;

		// Token: 0x04006EF0 RID: 28400
		private int focus;

		// Token: 0x04006EF1 RID: 28401
		private readonly Command cmdExit;

		// Token: 0x04006EF2 RID: 28402
		private readonly Command cmdOK;

		// Token: 0x04006EF3 RID: 28403
		public static string serverName;

		// Token: 0x04006EF4 RID: 28404
		public static Image imgTitle;

		// Token: 0x04006EF5 RID: 28405
		public int plX;

		// Token: 0x04006EF6 RID: 28406
		public int plY;

		// Token: 0x04006EF7 RID: 28407
		public int lY;

		// Token: 0x04006EF8 RID: 28408
		public int lX;

		// Token: 0x04006EF9 RID: 28409
		public int logoDes;

		// Token: 0x04006EFA RID: 28410
		public int lineX;

		// Token: 0x04006EFB RID: 28411
		public int lineY;

		// Token: 0x04006EFC RID: 28412
		public static int[] bgId = new int[]
		{
			0,
			8,
			2,
			6,
			9
		};

		// Token: 0x04006EFD RID: 28413
		public static bool isTryGetIPFromWap;

		// Token: 0x04006EFE RID: 28414
		public static short timeLogin;

		// Token: 0x04006EFF RID: 28415
		public static long lastTimeLogin;

		// Token: 0x04006F00 RID: 28416
		public static long currTimeLogin;

		// Token: 0x04006F01 RID: 28417
		private int xLog;

		// Token: 0x04006F02 RID: 28418
		private int yLog;

		// Token: 0x04006F03 RID: 28419
		private readonly int v = 2;

		// Token: 0x04006F04 RID: 28420
		private int g;

		// Token: 0x04006F05 RID: 28421
		private int ylogo = -40;

		// Token: 0x04006F06 RID: 28422
		private int dir = 1;
	}
}
