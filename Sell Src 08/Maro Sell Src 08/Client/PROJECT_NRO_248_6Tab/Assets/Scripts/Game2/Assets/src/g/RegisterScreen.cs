using System;

namespace Game2.Assets.src.g
{
	// Token: 0x0200043A RID: 1082
	public class RegisterScreen : mScreen, IActionListener
	{
		// Token: 0x06003029 RID: 12329 RVA: 0x002E857C File Offset: 0x002E677C
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

		// Token: 0x0600302A RID: 12330 RVA: 0x002E8808 File Offset: 0x002E6A08
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

		// Token: 0x0600302B RID: 12331 RVA: 0x002E883C File Offset: 0x002E6A3C
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

		// Token: 0x0600302C RID: 12332 RVA: 0x002E8944 File Offset: 0x002E6B44
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

		// Token: 0x0600302D RID: 12333 RVA: 0x0029CBCA File Offset: 0x0029ADCA
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x002E89AC File Offset: 0x002E6BAC
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

		// Token: 0x0600302F RID: 12335 RVA: 0x002E8C04 File Offset: 0x002E6E04
		private void turnOffFocus()
		{
			this.tfPassword.isFocus = false;
			this.tfUsername.isFocus = false;
			this.tfMaGioiThieu.isFocus = false;
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x002E8C2C File Offset: 0x002E6E2C
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

		// Token: 0x06003031 RID: 12337 RVA: 0x002E8C80 File Offset: 0x002E6E80
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

		// Token: 0x06003032 RID: 12338 RVA: 0x002E8EA4 File Offset: 0x002E70A4
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

		// Token: 0x04005C6E RID: 23662
		public TField tfPassword;

		// Token: 0x04005C6F RID: 23663
		public TField tfUsername;

		// Token: 0x04005C70 RID: 23664
		public TField tfMaGioiThieu;

		// Token: 0x04005C71 RID: 23665
		private int focus;

		// Token: 0x04005C72 RID: 23666
		private readonly Command cmdExit;

		// Token: 0x04005C73 RID: 23667
		private readonly Command cmdOK;

		// Token: 0x04005C74 RID: 23668
		public static string serverName;

		// Token: 0x04005C75 RID: 23669
		public static Image imgTitle;

		// Token: 0x04005C76 RID: 23670
		public int plX;

		// Token: 0x04005C77 RID: 23671
		public int plY;

		// Token: 0x04005C78 RID: 23672
		public int lY;

		// Token: 0x04005C79 RID: 23673
		public int lX;

		// Token: 0x04005C7A RID: 23674
		public int logoDes;

		// Token: 0x04005C7B RID: 23675
		public int lineX;

		// Token: 0x04005C7C RID: 23676
		public int lineY;

		// Token: 0x04005C7D RID: 23677
		public static int[] bgId = new int[]
		{
			0,
			8,
			2,
			6,
			9
		};

		// Token: 0x04005C7E RID: 23678
		public static bool isTryGetIPFromWap;

		// Token: 0x04005C7F RID: 23679
		public static short timeLogin;

		// Token: 0x04005C80 RID: 23680
		public static long lastTimeLogin;

		// Token: 0x04005C81 RID: 23681
		public static long currTimeLogin;

		// Token: 0x04005C82 RID: 23682
		private int xLog;

		// Token: 0x04005C83 RID: 23683
		private int yLog;

		// Token: 0x04005C84 RID: 23684
		private readonly int v = 2;

		// Token: 0x04005C85 RID: 23685
		private int g;

		// Token: 0x04005C86 RID: 23686
		private int ylogo = -40;

		// Token: 0x04005C87 RID: 23687
		private int dir = 1;
	}
}
