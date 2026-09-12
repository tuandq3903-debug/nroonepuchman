using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x02000139 RID: 313
	public class LoginScr : mScreen, IActionListener
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x000DC938 File Offset: 0x000DAB38
		public LoginScr()
		{
			this.yLog = GameCanvas.hh - 30;
			TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
			if (TileMap.bgID == 5 || TileMap.bgID == 6)
			{
				TileMap.bgID = 4;
			}
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameScr.cmy = 200;
			Main.closeKeyBoard();
			if (GameCanvas.h > 200)
			{
				this.defYL = GameCanvas.hh - 80;
			}
			else
			{
				this.defYL = GameCanvas.hh - 65;
			}
			this.resetLogo();
			this.wC = ((GameCanvas.w < 200) ? 140 : 160);
			this.yt = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;
			if (GameCanvas.h <= 160)
			{
				this.yt = 20;
			}
			this.tfUser = new TField();
			this.tfUser.y = GameCanvas.hh - mScreen.ITEM_HEIGHT - 9;
			this.tfUser.width = this.wC;
			this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
			this.tfUser.isFocus = true;
			this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
			this.tfUser.name = mResources.phone;
			this.tfPass = new TField();
			this.tfPass.y = GameCanvas.hh - 4;
			this.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
			this.tfPass.width = this.wC;
			this.tfPass.height = mScreen.ITEM_HEIGHT + 2;
			this.yt += 35;
			this.isCheck = true;
			int num4 = Rms.loadRMSInt("check");
			if (num4 != 1)
			{
				if (num4 == 2)
				{
					this.isCheck = false;
				}
			}
			else
			{
				this.isCheck = true;
			}
			this.tfUser.setText(Rms.loadRMSString("acc"));
			this.tfPass.setText(Rms.loadRMSString("pass"));
			this.focus = 0;
			this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
			this.cmdCheck = new Command(mResources.remember, this, 2001, null);
			this.cmdRes = new Command(mResources.register, this, 2002, null);
			this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
			this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
			this.freeAreaHeight = this.tfUser.y - 2 * this.tfUser.height;
			if (GameCanvas.isTouch)
			{
				this.cmdLogin.x = GameCanvas.w / 2 + 8;
				this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
				if (GameCanvas.h >= 200)
				{
					this.cmdLogin.y = this.yLog + 110;
					this.cmdMenu.y = this.yLog + 110;
				}
				this.cmdBackFromRegister.x = GameCanvas.w / 2 + 3;
				this.cmdBackFromRegister.y = this.yLog + 110;
				this.cmdRes.x = GameCanvas.w / 2 - 84;
				this.cmdRes.y = this.cmdMenu.y;
			}
			this.wP = 170;
			this.hP = ((!this.isRes) ? 100 : 110);
			this.xP = GameCanvas.hw - this.wP / 2;
			this.yP = this.tfUser.y - 15;
			int num2 = 4;
			int num3 = num2 * 32 + 23 + 33;
			if (num3 >= GameCanvas.w)
			{
				num2--;
				num3 = num2 * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num3 / 2;
			this.yLog = GameCanvas.hh - 30;
			this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
			this.tfUser.x = this.xLog + 10;
			this.tfUser.y = this.yLog + 20;
			this.cmdOK = new Command(mResources.OK, this, 2008, null);
			this.cmdOK.x = GameCanvas.w / 2 - 84;
			this.cmdOK.y = this.cmdLogin.y;
			ModFunc.cmdAccManager = new Command(ModFunc.strAccManager, ModFunc.GI(), 101, null);
			ModFunc.cmdAccManager.x = GameCanvas.w / 2 + 3;
			ModFunc.cmdAccManager.y = this.cmdLogin.y;
			this.center = this.cmdOK;
			this.left = ModFunc.cmdAccManager;
			ModFunc.cmdCloseAccManager = new Command(mResources.CLOSE, ModFunc.GI(), 104, null);
			ModFunc.GI().LoadAcc();
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000DCEA0 File Offset: 0x000DB0A0
		public override void switchToMe()
		{
			TabController.updateCharName(string.Empty, 1);
			this.isRegistering = false;
			SoundMn.gI().stopAll();
			this.tfUser.isFocus = true;
			this.tfPass.isFocus = false;
			if (GameCanvas.isTouch)
			{
				this.tfUser.isFocus = false;
			}
			GameCanvas.loadBG(1);
			base.switchToMe();
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000DCF00 File Offset: 0x000DB100
		public void setUserPass()
		{
			string text = Rms.loadRMSString("acc");
			if (text != null && !text.Equals(string.Empty))
			{
				this.tfUser.setText(text);
			}
			string text2 = Rms.loadRMSString("pass");
			if (text2 != null && !text2.Equals(string.Empty))
			{
				this.tfPass.setText(text2);
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x000034B9 File Offset: 0x000016B9
		public void updateTfWhenOpenKb()
		{
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000DCF5C File Offset: 0x000DB15C
		protected void doMenu()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
			if (!this.isLogin2)
			{
				myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
			}
			myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
			myVector.addElement(new Command(mResources.website, this, 1005, null));
			if (Main.isPC)
			{
				myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
			}
			GameCanvas.menu.startAt(myVector, 0);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000DD004 File Offset: 0x000DB204
		protected void doRegister()
		{
			if (this.tfUser.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.userBlank);
				return;
			}
			this.tfUser.getText().ToCharArray();
			if (this.tfPass.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.passwordBlank);
				return;
			}
			if (this.tfUser.getText().Length < 5)
			{
				GameCanvas.startOKDlg(mResources.accTooShort);
				return;
			}
			int num = 0;
			string text = null;
			if (mResources.language == 2)
			{
				if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
				{
					text = mResources.emailInvalid;
				}
				num = 0;
			}
			else
			{
				try
				{
					long.Parse(this.tfUser.getText());
					if (this.tfUser.getText().Length < 8 || this.tfUser.getText().Length > 12 || (!this.tfUser.getText().StartsWith("0") && !this.tfUser.getText().StartsWith("84")))
					{
						text = mResources.phoneInvalid;
					}
					num = 1;
				}
				catch (Exception)
				{
					if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
					{
						text = mResources.emailInvalid;
					}
					num = 0;
				}
			}
			if (text != null)
			{
				GameCanvas.startOKDlg(text);
			}
			else
			{
				GameCanvas.msgdlg.setInfo(string.Concat(new string[]
				{
					mResources.plsCheckAcc,
					(num != 1) ? (mResources.email + ": ") : (mResources.phone + ": "),
					this.tfUser.getText(),
					"\n",
					mResources.password,
					": ",
					this.tfPass.getText()
				}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
			}
			GameCanvas.currentDialog = GameCanvas.msgdlg;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000DD240 File Offset: 0x000DB440
		protected void doRegister(string user)
		{
			this.isFAQ = false;
			GameCanvas.startWaitDlg(mResources.CONNECTING);
			GameCanvas.connect();
			GameCanvas.startWaitDlg(mResources.REGISTERING);
			this.passRe = this.tfPass.getText();
			Service.gI().requestRegister(user, this.tfPass.getText(), Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()), Rms.loadRMSString("passAo" + ServerListScreen.ipSelect.ToString()), GameMidlet.VERSION);
			Rms.saveRMSString("acc", user);
			Rms.saveRMSString("pass", this.tfPass.getText());
			this.t = 20;
			this.isRegistering = true;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000DD2FC File Offset: 0x000DB4FC
		public void doLogin()
		{
			string text = Rms.loadRMSString("acc");
			string text2 = Rms.loadRMSString("pass");
			if (text != null && !text.Equals(string.Empty))
			{
				this.isLogin2 = false;
			}
			else if (Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()).Equals(string.Empty))
			{
				this.isLogin2 = true;
			}
			else
			{
				this.isLogin2 = false;
			}
			if ((text == null || text.Equals(string.Empty)) && this.isLogin2)
			{
				text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
				text2 = "a";
			}
			if (text == null || text2 == null || GameMidlet.VERSION == null || text.Equals(string.Empty))
			{
				return;
			}
			if (text2.Equals(string.Empty))
			{
				this.focus = 1;
				this.tfUser.isFocus = false;
				this.tfPass.isFocus = true;
				if (!GameCanvas.isTouch)
				{
					this.right = this.tfPass.cmdClear;
				}
				return;
			}
			if (!Session_ME.gI().isConnected())
			{
				GameCanvas.connect();
			}
			Service.gI().login(text, text2, GameMidlet.VERSION, (sbyte)(isLogin2 ? 1 : 0));
			if (Session_ME.connected)
			{
				GameCanvas.startWaitDlg();
			}
			else
			{
				GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
			}
			this.focus = 0;
			if (!this.isLogin2)
			{
				this.actRegisterLeft();
			}
			GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
			if (ModFunc.isAutoLogin)
			{
				ModFunc.dangLogin = false;
				Controller.isConnectionFail = false;
				Controller.isDisconnected = false;
			}
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000DD4A8 File Offset: 0x000DB6A8
		public void savePass()
		{
			if (this.isCheck)
			{
				Rms.saveRMSInt("check", 1);
				Rms.saveRMSString("acc", this.tfUser.getText().ToLower().Trim());
				Rms.saveRMSString("pass", this.tfPass.getText().ToLower().Trim());
				return;
			}
			Rms.saveRMSInt("check", 2);
			Rms.saveRMSString("acc", string.Empty);
			Rms.saveRMSString("pass", string.Empty);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000DD530 File Offset: 0x000DB730
		public override void update()
		{
			if (ModFunc.AutoLogin())
			{
				return;
			}
			if (Main.isWindowsPhone && this.isRegistering)
			{
				if (this.t < 0)
				{
					GameCanvas.endDlg();
					Session_ME.gI().close();
					GameCanvas.serverScreen.switchToMe();
					this.isRegistering = false;
				}
				else
				{
					this.t--;
				}
			}
			if (LoginScr.timeLogin > 0)
			{
				GameCanvas.startWaitDlg();
				LoginScr.currTimeLogin = mSystem.currentTimeMillis();
				if (LoginScr.currTimeLogin - LoginScr.lastTimeLogin >= 1000L)
				{
					LoginScr.timeLogin -= 1;
					if (LoginScr.timeLogin == 0)
					{
						GameCanvas.loginScr.doLogin();
					}
					LoginScr.lastTimeLogin = LoginScr.currTimeLogin;
				}
			}
			if (this.isLogin2 && !this.isRes)
			{
				this.tfUser.name = mResources.phone;
				this.tfPass.name = mResources.password;
				this.tfUser.isPaintCarret = false;
				this.tfPass.isPaintCarret = false;
				this.tfUser.update();
				this.tfPass.update();
			}
			else
			{
				this.tfUser.name = mResources.phone;
				this.tfPass.name = mResources.password;
				this.tfUser.update();
				this.tfPass.update();
			}
			if (TouchScreenKeyboard.visible)
			{
				mGraphics.addYWhenOpenKeyBoard = 50;
			}
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				((Effect2)Effect2.vEffect2.elementAt(i)).update();
			}
			if (LoginScr.isUpdateAll && !LoginScr.isUpdateData && !LoginScr.isUpdateItem && !LoginScr.isUpdateMap && !LoginScr.isUpdateSkill)
			{
				LoginScr.isUpdateAll = false;
				mSystem.gcc();
				Service.gI().finishUpdate();
			}
			GameScr.cmx++;
			if (GameScr.cmx > GameCanvas.w * 3 + 100)
			{
				GameScr.cmx = 100;
			}
			if (ChatPopup.currChatPopup != null)
			{
				return;
			}
			this.updateLogo();
			if (this.g >= 0)
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
			if (this.tipid >= 0 && GameCanvas.gameTick % 100 == 0)
			{
				this.doChangeTip();
			}
			if (this.isLogin2 && !this.isRes)
			{
				this.tfUser.isPaintCarret = false;
				this.tfPass.isPaintCarret = false;
				this.tfUser.update();
				this.tfPass.update();
			}
			else
			{
				this.tfUser.name = mResources.phone;
				this.tfPass.name = mResources.password;
				this.tfUser.update();
				this.tfPass.update();
			}
			if (GameCanvas.isTouch)
			{
				if (this.isRes)
				{
					this.center = this.cmdRes;
					this.left = this.cmdBackFromRegister;
				}
				else
				{
					this.center = this.cmdOK;
					this.left = ModFunc.cmdAccManager;
				}
			}
			else if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = ModFunc.cmdAccManager;
			}
			if (!Main.isPC && !TouchScreenKeyboard.visible && !Main.isMiniApp && !Main.isWindowsPhone)
			{
				string text3 = this.tfUser.getText().ToLower().Trim();
				string text2 = this.tfPass.getText().ToLower().Trim();
				if (!text3.Equals(string.Empty) && !text2.Equals(string.Empty))
				{
					this.doLogin();
				}
				Main.isMiniApp = true;
			}
			this.updateTfWhenOpenKb();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000DD914 File Offset: 0x000DBB14
		private void doChangeTip()
		{
			this.tipid++;
			if (this.tipid >= mResources.tips.Length)
			{
				this.tipid = 0;
			}
			if (GameCanvas.currentDialog == GameCanvas.msgdlg && GameCanvas.msgdlg.isWait)
			{
				GameCanvas.msgdlg.setInfo(mResources.tips[this.tipid]);
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000DD973 File Offset: 0x000DBB73
		public void updateLogo()
		{
			if (this.defYL != this.yL)
			{
				this.yL += this.defYL - this.yL >> 1;
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000DD99F File Offset: 0x000DBB9F
		public override void keyPress(int keyCode)
		{
			if (this.tfUser.isFocus)
			{
				this.tfUser.keyPressed(keyCode);
			}
			else if (this.tfPass.isFocus)
			{
				this.tfPass.keyPressed(keyCode);
			}
			base.keyPress(keyCode);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000DD9DE File Offset: 0x000DBBDE
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000DD9E8 File Offset: 0x000DBBE8
		public override void paint(mGraphics g)
		{
			GameCanvas.paintBGGameScr(g);
			int num = this.tfUser.y - 70;
			if (GameCanvas.h <= 220)
			{
				num += 5;
			}
			mFont.tahoma_7_white.drawStringBorder(g, "v" + GameMidlet.VERSION, GameCanvas.w - 2, 20, 1, mFont.tahoma_7_grey);
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				mFont.tahoma_7_white.drawStringBorder(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			}
			else
			{
				mFont.tahoma_7_white.drawStringBorder(g, ServerListScreen.linkweb, GameCanvas.w - 2, 2, 1, mFont.tahoma_7_grey);
			}
			if (ChatPopup.currChatPopup != null || ChatPopup.serverChatPopUp != null)
			{
				return;
			}
			if (GameCanvas.currentDialog == null)
			{
				if (ModFunc.isOpenAccMAnager)
				{
					int h = 180;
					int w = (GameCanvas.w < 200) ? 160 : 260;
					int xPop = (GameCanvas.w - w) / 2;
					int yPop = (GameCanvas.h - h) / 2;
					PopUp.paintPopUp(g, xPop, yPop, w, h, -1, true);
					this.left = null;
					this.center = null;
					int yStart = yPop - 15;
					for (int i = 0; i < ModFunc.GI().cmdsChooseAcc.Count; i++)
					{
						ModFunc.GI().cmdsChooseAcc[i].x = xPop + 20;
						ModFunc.GI().cmdsChooseAcc[i].y = yStart;
						ModFunc.GI().cmdsChooseAcc[i].paint(g);
						ModFunc.GI().cmdsDelAcc[i].x = xPop + 190;
						ModFunc.GI().cmdsDelAcc[i].y = yStart;
						ModFunc.GI().cmdsDelAcc[i].paint(g);
						yStart += 30;
					}
					ModFunc.cmdCloseAccManager.x = (GameCanvas.w - ModFunc.cmdCloseAccManager.w) / 2;
					ModFunc.cmdCloseAccManager.y = yPop + h + 10;
					ModFunc.cmdCloseAccManager.paint(g);
					if (GameCanvas.h > 160 && ModFunc.imgLogoBig != null)
					{
						ModFunc.PaintLogoGif(g, GameCanvas.hw, yPop - 20, 3);
					}
				}
				else
				{
					int h2 = 105;
					int w2 = (GameCanvas.w < 200) ? 160 : 180;
					PopUp.paintPopUp(g, this.xLog, this.yLog - 10, w2, h2, -1, true);
					if (GameCanvas.h > 160 && ModFunc.imgLogoBig != null)
					{
						ModFunc.PaintLogoGif(g, GameCanvas.hw, num, 3);
					}
					int num2 = 4;
					int num3 = num2 * 32 + 23 + 33;
					if (num3 >= GameCanvas.w)
					{
						num2--;
						num3 = num2 * 32 + 23 + 33;
					}
					this.xLog = GameCanvas.w / 2 - num3 / 2;
					this.tfUser.x = this.xLog + 10;
					this.tfUser.y = this.yLog + 20;
					this.tfPass.x = this.xLog + 10;
					this.tfPass.y = this.yLog + 55;
					this.tfUser.paint(g);
					this.tfPass.paint(g);
					if (GameCanvas.w < 176)
					{
						mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
						mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfPass.x - 35, this.tfPass.y + 7, 0);
						mFont.tahoma_7b_green2.drawString(g, mResources.server + ":" + LoginScr.serverName, GameCanvas.w / 2, this.tfPass.y + 32, 2);
					}
				}
			}
			base.paint(g);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000DDDF0 File Offset: 0x000DBFF0
		public override void updateKey()
		{
			if (LoginScr.isContinueToLogin)
			{
				return;
			}
			if (!GameCanvas.isTouch)
			{
				if (this.tfUser.isFocus)
				{
					this.right = this.tfUser.cmdClear;
				}
				else
				{
					this.right = this.tfPass.cmdClear;
				}
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				this.focus--;
				if (this.focus < 0)
				{
					this.focus = 1;
				}
			}
			else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
			{
				this.focus++;
				if (this.focus > 1)
				{
					this.focus = 0;
				}
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
			{
				GameCanvas.clearKeyPressed();
				if (!this.isLogin2 || this.isRes)
				{
					if (this.focus == 1)
					{
						this.tfUser.isFocus = false;
						this.tfPass.isFocus = true;
					}
					else if (this.focus == 0)
					{
						this.tfUser.isFocus = true;
						this.tfPass.isFocus = false;
					}
					else
					{
						this.tfUser.isFocus = false;
						this.tfPass.isFocus = false;
					}
				}
			}
			if (GameCanvas.isTouch)
			{
				if (this.isRes)
				{
					this.center = this.cmdRes;
					this.left = this.cmdBackFromRegister;
				}
				else
				{
					this.center = this.cmdOK;
					this.left = ModFunc.cmdAccManager;
				}
			}
			else if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = ModFunc.cmdAccManager;
			}
			if (GameCanvas.isPointerJustRelease && (!this.isLogin2 || this.isRes) && !ModFunc.isOpenAccMAnager)
			{
				if (GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height))
				{
					this.focus = 0;
				}
				else if (GameCanvas.isPointerHoldIn(this.tfPass.x, this.tfPass.y, this.tfPass.width, this.tfPass.height))
				{
					this.focus = 1;
				}
			}
			if (Main.isPC && GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && this.right != null)
			{
				this.right.performAction();
			}
			if (ModFunc.isOpenAccMAnager)
			{
				foreach (Command cmd in ModFunc.GI().cmdsChooseAcc)
				{
					if (cmd.isPointerPressInside())
					{
						cmd.performAction();
					}
				}
				foreach (Command cmd2 in ModFunc.GI().cmdsDelAcc)
				{
					if (cmd2.isPointerPressInside())
					{
						cmd2.performAction();
					}
				}
				if (ModFunc.cmdCloseAccManager.isPointerPressInside())
				{
					ModFunc.cmdCloseAccManager.performAction();
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000DE15C File Offset: 0x000DC35C
		public void resetLogo()
		{
			this.yL = -50;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000DE168 File Offset: 0x000DC368
		public void perform(int idAction, object p)
		{
			if (idAction <= 2008)
			{
				if (idAction != 13)
				{
					switch (idAction)
					{
						case 1000:
							try
							{
								GameMidlet.instance.platformRequest((string)p);
							}
							catch (Exception)
							{
							}
							GameCanvas.endDlg();
							return;
						case 1001:
							GameCanvas.endDlg();
							this.isRes = false;
							return;
						case 1002:
							{
								GameCanvas.startWaitDlg();
								string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
								if (text == null || text.Equals(string.Empty))
								{
									Service.gI().login2(string.Empty);
									return;
								}
								GameCanvas.loginScr.isLogin2 = true;
								GameCanvas.connect();
								Service.gI().setClientType();
								Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
								return;
							}
						case 1003:
							this.doMenu();
							return;
						case 1004:
							ServerListScreen.doUpdateServer();
							GameCanvas.serverScreen.switchToMe();
							return;
						default:
							switch (idAction)
							{
								case 2001:
									if (this.isCheck)
									{
										this.isCheck = false;
										return;
									}
									this.isCheck = true;
									return;
								case 2002:
									this.doRegister();
									return;
								case 2003:
									this.doMenu();
									return;
								case 2004:
									this.actRegister();
									return;
								case 2005:
								case 2006:
								case 2007:
									break;
								case 2008:
									{
										string user = this.tfUser.getText().Trim();
										string pass = this.tfPass.getText().Trim();
										ModFunc.GI().AddAccount(user, pass);
										Rms.saveRMSString("acc", user);
										Rms.saveRMSString("pass", pass);
										if (ServerListScreen.loadScreen)
										{
											GameCanvas.serverScreen.switchToMe();
											return;
										}
										GameCanvas.serverScreen.show2();
										return;
									}
								default:
									return;
							}
							break;
					}
				}
				else
				{
					switch (mSystem.clientType)
					{
						case 1:
							mSystem.callHotlineJava();
							return;
						case 2:
							break;
						case 3:
						case 5:
							mSystem.callHotlineIphone();
							return;
						case 4:
							mSystem.callHotlinePC();
							return;
						case 6:
							mSystem.callHotlineWindowsPhone();
							return;
						default:
							return;
					}
				}
			}
			else if (idAction <= 10021)
			{
				if (idAction != 4000)
				{
					if (idAction != 10021)
					{
						return;
					}
					this.actRegisterLeft();
					return;
				}
				else
				{
					this.doRegister(this.tfUser.getText());
				}
			}
			else
			{
				if (idAction == 10041)
				{
					Rms.saveRMSInt("lowGraphic", 0);
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
					return;
				}
				if (idAction != 10042)
				{
					return;
				}
				Rms.saveRMSInt("lowGraphic", 1);
				GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				return;
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000DE3E8 File Offset: 0x000DC5E8
		public void actRegisterLeft()
		{
			if (this.isLogin2)
			{
				this.doLogin();
				return;
			}
			this.isRes = false;
			this.tfPass.isFocus = false;
			this.tfUser.isFocus = true;
			this.left = this.cmdMenu;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000DE424 File Offset: 0x000DC624
		public void actRegister()
		{
			GameCanvas.endDlg();
			this.isRes = true;
			this.tfPass.isFocus = false;
			this.tfUser.isFocus = true;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x000DE44C File Offset: 0x000DC64C
		public void backToRegister()
		{
			GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
			ServerListScreen.countDieConnect = 0;
			if (GameCanvas.loginScr.isLogin2)
			{
				GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
				return;
			}
			if (Main.isWindowsPhone)
			{
				GameMidlet.isBackWindowsPhone = true;
			}
			GameCanvas.instance.resetToLoginScr = false;
			GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
		}

		// Token: 0x04001AD4 RID: 6868
		public TField tfUser;

		// Token: 0x04001AD5 RID: 6869
		public TField tfPass;

		// Token: 0x04001AD6 RID: 6870
		public static bool isContinueToLogin = false;

		// Token: 0x04001AD7 RID: 6871
		private int focus;

		// Token: 0x04001AD8 RID: 6872
		private int wC;

		// Token: 0x04001AD9 RID: 6873
		private int yL;

		// Token: 0x04001ADA RID: 6874
		private int defYL;

		// Token: 0x04001ADB RID: 6875
		public bool isCheck;

		// Token: 0x04001ADC RID: 6876
		public bool isRes;

		// Token: 0x04001ADD RID: 6877
		public Command cmdLogin;

		// Token: 0x04001ADE RID: 6878
		public Command cmdCheck;

		// Token: 0x04001ADF RID: 6879
		public Command cmdRes;

		// Token: 0x04001AE0 RID: 6880
		public Command cmdMenu;

		// Token: 0x04001AE1 RID: 6881
		public Command cmdBackFromRegister;

		// Token: 0x04001AE2 RID: 6882
		public string listFAQ = string.Empty;

		// Token: 0x04001AE3 RID: 6883
		public string titleFAQ;

		// Token: 0x04001AE4 RID: 6884
		public string subtitleFAQ;

		// Token: 0x04001AE5 RID: 6885
		private string numSupport = string.Empty;

		// Token: 0x04001AE6 RID: 6886
		public static bool isLocal = false;

		// Token: 0x04001AE7 RID: 6887
		public static bool isUpdateAll;

		// Token: 0x04001AE8 RID: 6888
		public static bool isUpdateData;

		// Token: 0x04001AE9 RID: 6889
		public static bool isUpdateMap;

		// Token: 0x04001AEA RID: 6890
		public static bool isUpdateSkill;

		// Token: 0x04001AEB RID: 6891
		public static bool isUpdateItem;

		// Token: 0x04001AEC RID: 6892
		public static string serverName;

		// Token: 0x04001AED RID: 6893
		public int plX;

		// Token: 0x04001AEE RID: 6894
		public int plY;

		// Token: 0x04001AEF RID: 6895
		public int lY;

		// Token: 0x04001AF0 RID: 6896
		public int lX;

		// Token: 0x04001AF1 RID: 6897
		public int logoDes;

		// Token: 0x04001AF2 RID: 6898
		public int lineX;

		// Token: 0x04001AF3 RID: 6899
		public int lineY;

		// Token: 0x04001AF4 RID: 6900
		public static int[] bgId = new int[]
		{
			0,
			8,
			2,
			6,
			9
		};

		// Token: 0x04001AF5 RID: 6901
		public static bool isTryGetIPFromWap;

		// Token: 0x04001AF6 RID: 6902
		public static short timeLogin;

		// Token: 0x04001AF7 RID: 6903
		public static long lastTimeLogin;

		// Token: 0x04001AF8 RID: 6904
		public static long currTimeLogin;

		// Token: 0x04001AF9 RID: 6905
		private int yt;

		// Token: 0x04001AFA RID: 6906
		private Command cmdSelect;

		// Token: 0x04001AFB RID: 6907
		private Command cmdOK;

		// Token: 0x04001AFC RID: 6908
		private int xLog;

		// Token: 0x04001AFD RID: 6909
		private int yLog;

		// Token: 0x04001AFE RID: 6910
		public static GameMidlet m;

		// Token: 0x04001AFF RID: 6911
		private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

		// Token: 0x04001B00 RID: 6912
		private int freeAreaHeight;

		// Token: 0x04001B01 RID: 6913
		private int xP;

		// Token: 0x04001B02 RID: 6914
		private int yP;

		// Token: 0x04001B03 RID: 6915
		private int wP;

		// Token: 0x04001B04 RID: 6916
		private int hP;

		// Token: 0x04001B05 RID: 6917
		private int t = 20;

		// Token: 0x04001B06 RID: 6918
		private bool isRegistering;

		// Token: 0x04001B07 RID: 6919
		private string passRe = string.Empty;

		// Token: 0x04001B08 RID: 6920
		public bool isFAQ;

		// Token: 0x04001B09 RID: 6921
		private int tipid = -1;

		// Token: 0x04001B0A RID: 6922
		public bool isLogin2;

		// Token: 0x04001B0B RID: 6923
		private int v = 2;

		// Token: 0x04001B0C RID: 6924
		private int g;

		// Token: 0x04001B0D RID: 6925
		private int ylogo = -40;

		// Token: 0x04001B0E RID: 6926
		private int dir = 1;

		// Token: 0x04001B0F RID: 6927
		public static bool isLoggingIn;
	}
}
