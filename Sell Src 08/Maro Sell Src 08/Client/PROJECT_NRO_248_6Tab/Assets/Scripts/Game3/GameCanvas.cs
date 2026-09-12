using System;
using Game3.Assets.src.g;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002C5 RID: 709
	public class GameCanvas : IActionListener
	{
		// Token: 0x06001F45 RID: 8005 RVA: 0x001EF09C File Offset: 0x001ED29C
		public GameCanvas()
		{
			int num = Rms.loadRMSInt("languageVersion");
			if (num != -1)
			{
				if (num != 2)
				{
					Main.main.doClearRMS();
					Rms.saveRMSInt("languageVersion", 2);
				}
			}
			else
			{
				Rms.saveRMSInt("languageVersion", 2);
			}
			GameCanvas.clearOldData = Rms.loadRMSInt(GameMidlet.VERSION);
			if (GameCanvas.clearOldData != 1)
			{
				Main.main.doClearRMS();
				Rms.saveRMSInt(GameMidlet.VERSION, 1);
			}
			this.initGame();
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0002FDFA File Offset: 0x0002DFFA
		public static string getPlatformName()
		{
			return "Pc platform xxx";
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x001EF150 File Offset: 0x001ED350
		public void initGame()
		{
			MotherCanvas.instance.setChildCanvas(this);
			GameCanvas.w = MotherCanvas.instance.getWidthz();
			GameCanvas.h = MotherCanvas.instance.getHeightz();
			GameCanvas.hw = GameCanvas.w / 2;
			GameCanvas.hh = GameCanvas.h / 2;
			GameCanvas.isTouch = true;
			if (GameCanvas.w >= 240)
			{
				GameCanvas.isTouchControl = true;
			}
			if (GameCanvas.w < 320)
			{
				GameCanvas.isTouchControlSmallScreen = true;
			}
			if (GameCanvas.w >= 320)
			{
				GameCanvas.isTouchControlLargeScreen = true;
			}
			GameCanvas.msgdlg = new MsgDlg();
			if (GameCanvas.h <= 160)
			{
				Paint.hTab = 15;
				mScreen.cmdH = 17;
			}
			GameScr.d = ((GameCanvas.w <= GameCanvas.h) ? GameCanvas.h : GameCanvas.w) + 20;
			GameCanvas.instance = this;
			mFont.init();
			mScreen.ITEM_HEIGHT = mFont.tahoma_8b.getHeight() + 8;
			this.initPaint();
			this.loadDust();
			this.loadWaterSplash();
			GameCanvas.panel = new Panel();
			GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/myTexture2df.png");
			int num = Rms.loadRMSInt("clienttype");
			if (num != -1)
			{
				if (num > 7)
				{
					Rms.saveRMSInt("clienttype", mSystem.clientType);
				}
				else
				{
					mSystem.clientType = num;
				}
			}
			GameCanvas.imgClear = GameCanvas.loadImage("/mainImage/myTexture2der.png");
			GameCanvas.debugUpdate = new MyVector();
			GameCanvas.debugPaint = new MyVector();
			GameCanvas.debugSession = new MyVector();
			for (int i = 0; i < 3; i++)
			{
				GameCanvas.imgBorder[i] = GameCanvas.loadImage("/mainImage/myTexture2dbd" + i.ToString() + ".png");
			}
			GameCanvas.borderConnerW = mGraphics.getImageWidth(GameCanvas.imgBorder[0]);
			GameCanvas.borderConnerH = mGraphics.getImageHeight(GameCanvas.imgBorder[0]);
			GameCanvas.borderCenterW = mGraphics.getImageWidth(GameCanvas.imgBorder[1]);
			GameCanvas.borderCenterH = mGraphics.getImageHeight(GameCanvas.imgBorder[1]);
			Panel.graphics = Rms.loadRMSInt("lowGraphic");
			GameCanvas.lowGraphic = (Rms.loadRMSInt("lowGraphic") == 1);
			GameScr.isPaintChatVip = (Rms.loadRMSInt("serverchat") != 1);
			Char.isPaintAura = (Rms.loadRMSInt("isPaintAura") == 1);
			Char.isPaintAura2 = (Rms.loadRMSInt("isPaintAura2") == 1);
			Res.init();
			SmallImage.loadBigImage();
			Panel.WIDTH_PANEL = 176;
			if (Panel.WIDTH_PANEL > GameCanvas.w)
			{
				Panel.WIDTH_PANEL = GameCanvas.w;
			}
			InfoMe.gI().loadCharId();
			Command.btn0left = GameCanvas.loadImage("/mainImage/btn0left.png");
			Command.btn0mid = GameCanvas.loadImage("/mainImage/btn0mid.png");
			Command.btn0right = GameCanvas.loadImage("/mainImage/btn0right.png");
			Command.btn1left = GameCanvas.loadImage("/mainImage/btn1left.png");
			Command.btn1mid = GameCanvas.loadImage("/mainImage/btn1mid.png");
			Command.btn1right = GameCanvas.loadImage("/mainImage/btn1right.png");
			GameCanvas.serverScreen = new ServerListScreen();
			ServerListScreen.createDeleteRMS();
			GameCanvas.serverScr = new ServerScr();
			GameCanvas.chooseCharScr = new ChooseCharScr();
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x001EF439 File Offset: 0x001ED639
		public static GameCanvas gI()
		{
			return GameCanvas.instance;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x001EF440 File Offset: 0x001ED640
		public void initPaint()
		{
			GameCanvas.paintz = new Paint();
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x001EF44C File Offset: 0x001ED64C
		public static void closeKeyBoard()
		{
			mGraphics.addYWhenOpenKeyBoard = 0;
			GameCanvas.timeOpenKeyBoard = 0;
			Main.closeKeyBoard();
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x001EF460 File Offset: 0x001ED660
		public void update()
		{
			if (mSystem.currentTimeMillis() > this.timefps)
			{
				this.timefps += 1000L;
				GameCanvas.max = GameCanvas.fps;
				GameCanvas.fps = 0;
			}
			GameCanvas.fps++;
			if (GameCanvas.messageServer.size() > 0 && GameCanvas.thongBaoTest == null)
			{
				GameCanvas.startserverThongBao((string)GameCanvas.messageServer.elementAt(0));
				GameCanvas.messageServer.removeElementAt(0);
			}
			if (GameCanvas.gameTick % 5 == 0)
			{
				GameCanvas.timeNow = mSystem.currentTimeMillis();
			}
			Res.updateOnScreenDebug();
			try
			{
				if (TouchScreenKeyboard.visible)
				{
					GameCanvas.timeOpenKeyBoard++;
					if (GameCanvas.timeOpenKeyBoard > ((!Main.isWindowsPhone) ? 10 : 5))
					{
						mGraphics.addYWhenOpenKeyBoard = 94;
					}
				}
				else
				{
					mGraphics.addYWhenOpenKeyBoard = 0;
					GameCanvas.timeOpenKeyBoard = 0;
				}
				GameCanvas.debugUpdate.removeAllElements();
				long num = mSystem.currentTimeMillis();
				if (num - GameCanvas.timeTickEff1 >= 780L && !GameCanvas.isEff1)
				{
					GameCanvas.timeTickEff1 = num;
					GameCanvas.isEff1 = true;
				}
				else
				{
					GameCanvas.isEff1 = false;
				}
				if (num - GameCanvas.timeTickEff2 >= 7800L && !GameCanvas.isEff2)
				{
					GameCanvas.timeTickEff2 = num;
					GameCanvas.isEff2 = true;
				}
				else
				{
					GameCanvas.isEff2 = false;
				}
				if (GameCanvas.taskTick > 0)
				{
					GameCanvas.taskTick--;
				}
				GameCanvas.gameTick++;
				if (GameCanvas.gameTick > 10000)
				{
					if (mSystem.currentTimeMillis() - GameCanvas.lastTimePress > 20000L && GameCanvas.currentScreen == GameCanvas.loginScr)
					{
						GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
					}
					GameCanvas.gameTick = 0;
				}
				if (!TabController.Instance.isPointerHoldInTab() && GameCanvas.currentScreen != null)
				{
					if (ChatPopup.serverChatPopUp != null)
					{
						ChatPopup.serverChatPopUp.update();
						ChatPopup.serverChatPopUp.updateKey();
					}
					else if (ChatPopup.currChatPopup != null)
					{
						ChatPopup.currChatPopup.update();
						ChatPopup.currChatPopup.updateKey();
					}
					else if (GameCanvas.currentDialog != null)
					{
						GameCanvas.currentDialog.update();
					}
					else if (GameCanvas.menu.showMenu)
					{
						GameCanvas.menu.updateMenu();
						GameCanvas.menu.updateMenuKey();
					}
					else if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.update();
						if (GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H))
						{
							GameCanvas.isFocusPanel2 = false;
						}
						if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
						{
							GameCanvas.panel2.update();
							if (GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H))
							{
								GameCanvas.isFocusPanel2 = true;
							}
						}
						if (GameCanvas.panel2 != null)
						{
							if (GameCanvas.isFocusPanel2)
							{
								GameCanvas.panel2.updateKey();
							}
							else
							{
								GameCanvas.panel.updateKey();
							}
						}
						else
						{
							GameCanvas.panel.updateKey();
						}
						if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
						{
							GameCanvas.panel.chatTFUpdateKey();
						}
						else if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
						{
							GameCanvas.panel2.chatTFUpdateKey();
						}
						else if ((GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H) && GameCanvas.panel2 != null) || GameCanvas.panel2 == null)
						{
							GameCanvas.panel.updateKey();
						}
						else if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow && GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H))
						{
							GameCanvas.panel2.updateKey();
						}
						if (GameCanvas.isPointer(GameCanvas.panel.X + GameCanvas.panel.W, GameCanvas.panel.Y, GameCanvas.w - GameCanvas.panel.W * 2, GameCanvas.panel.H) && GameCanvas.isPointerJustRelease && GameCanvas.panel.isDoneCombine)
						{
							GameCanvas.panel.hide();
						}
					}
					if (!GameCanvas.isLoading)
					{
						GameCanvas.currentScreen.update();
					}
					if (!GameCanvas.panel.isShow && ChatPopup.serverChatPopUp == null)
					{
						GameCanvas.currentScreen.updateKey();
					}
					Hint.update();
					SoundMn.gI().update();
				}
				Timer.update();
				InfoDlg.update();
				if (this.resetToLoginScr)
				{
					this.resetToLoginScr = false;
					this.doResetToLoginScr(GameCanvas.serverScreen);
				}
				if (Controller.isConnectOK)
				{
					if (Controller.isMain)
					{
						GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
						GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
						ServerListScreen.testConnect = 2;
						Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
						Service.gI().setClientType();
						Service.gI().androidPack();
					}
					else
					{
						Service.gI().setClientType2();
						Service.gI().androidPack2();
					}
					Controller.isConnectOK = false;
				}
				if (Controller.isDisconnected)
				{
					if (!Controller.isMain)
					{
						if (GameCanvas.currentScreen == GameCanvas.serverScreen && !Service.reciveFromMainSession)
						{
							GameCanvas.serverScreen.cancel();
						}
						if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
						{
							this.onDisconnected();
						}
					}
					else
					{
						this.onDisconnected();
					}
					Controller.isDisconnected = false;
				}
				if (Controller.isConnectionFail)
				{
					Debug.Log("connect fail");
					if (!Controller.isMain)
					{
						if (GameCanvas.currentScreen == GameCanvas.serverScreen && ServerListScreen.isGetData && !Service.reciveFromMainSession)
						{
							ServerListScreen.testConnect = 0;
							GameCanvas.serverScreen.cancel();
						}
						if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
						{
							this.onConnectionFail();
						}
					}
					else if (Session_ME.gI().isCompareIPConnect())
					{
						this.onConnectionFail();
					}
					Controller.isConnectionFail = false;
				}
				if (Main.isResume)
				{
					Main.isResume = false;
					if (GameCanvas.currentDialog != null && GameCanvas.currentDialog.left != null && GameCanvas.currentDialog.left.actionListener != null)
					{
						GameCanvas.currentDialog.left.performAction();
					}
				}
				if (GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr)
				{
					GameCanvas.xThongBaoTranslate += GameCanvas.dir_ * 2;
					if (GameCanvas.xThongBaoTranslate - Panel.imgNew.getWidth() <= 60)
					{
						GameCanvas.dir_ = 0;
						this.tickWaitThongBao++;
						if (this.tickWaitThongBao > 150)
						{
							this.tickWaitThongBao = 0;
							GameCanvas.thongBaoTest = null;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x001EFB0C File Offset: 0x001EDD0C
		public static Image loadEffect(string path)
		{
			path = string.Concat(new string[]
			{
				Main.res,
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/inven",
				path
			});
			Image result = null;
			try
			{
				result = Image.createImage(path);
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x001EFB70 File Offset: 0x001EDD70
		public void onDisconnected()
		{
			if (Controller.isConnectionFail)
			{
				Controller.isConnectionFail = false;
			}
			GameCanvas.isResume = true;
			Session_ME.gI().clearSendingMessage();
			Session_ME2.gI().clearSendingMessage();
			Session_ME.gI().close();
			Session_ME2.gI().close();
			if (Controller.isLoadingData)
			{
				GameCanvas.instance.resetToLoginScrz();
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
				Controller.isDisconnected = false;
				return;
			}
			if (GameCanvas.currentScreen != GameCanvas.serverScreen)
			{
				GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
			}
			else
			{
				GameCanvas.endDlg();
			}
			Char.isLoadingMap = false;
			if (Controller.isMain)
			{
				ServerListScreen.testConnect = 0;
			}
			GameCanvas.instance.resetToLoginScrz();
			GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
			mSystem.endKey();
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x001EFC2C File Offset: 0x001EDE2C
		public void onConnectionFail()
		{
			if (GameCanvas.currentScreen.Equals(SplashScr.instance))
			{
				if (ServerListScreen.hasConnected != null)
				{
					ServerListScreen.GetServerList(ServerListScreen.linkDefault);
					if (!ServerListScreen.hasConnected[0])
					{
						ServerListScreen.hasConnected[0] = true;
						ServerListScreen.ipSelect = 0;
						GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
						Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
						GameCanvas.connect();
						return;
					}
					if (!ServerListScreen.hasConnected[2])
					{
						ServerListScreen.hasConnected[2] = true;
						ServerListScreen.ipSelect = 2;
						GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
						Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
						GameCanvas.connect();
						return;
					}
					GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
				}
				return;
			}
			Session_ME.gI().clearSendingMessage();
			Session_ME2.gI().clearSendingMessage();
			ServerListScreen.isWait = false;
			if (Controller.isLoadingData)
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
				Controller.isConnectionFail = false;
				return;
			}
			GameCanvas.isResume = true;
			LoginScr.isContinueToLogin = false;
			if (GameCanvas.loginScr != null)
			{
				GameCanvas.instance.resetToLoginScrz();
			}
			else
			{
				GameCanvas.loginScr = new LoginScr();
			}
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			if (GameCanvas.currentScreen != GameCanvas.serverScreen)
			{
				ServerListScreen.countDieConnect = 0;
			}
			else
			{
				GameCanvas.endDlg();
				ServerListScreen.loadScreen = true;
				GameCanvas.serverScreen.switchToMe();
			}
			Char.isLoadingMap = false;
			if (Controller.isMain)
			{
				ServerListScreen.testConnect = 0;
			}
			mSystem.endKey();
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x001EFD9C File Offset: 0x001EDF9C
		public static bool isWaiting()
		{
			return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || Char.isLoadingMap || LoginScr.isContinueToLogin;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x001EFDD3 File Offset: 0x001EDFD3
		public static void connect()
		{
			if (!Session_ME.gI().isConnected())
			{
				Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
			}
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x001EFDF8 File Offset: 0x001EDFF8
		public static void connect2()
		{
			if (!Session_ME2.gI().isConnected())
			{
				Res.outz("IP2= " + GameMidlet.IP2 + " PORT 2= " + GameMidlet.PORT2.ToString());
				Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
			}
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x001EFE48 File Offset: 0x001EE048
		public static void resetTrans(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x001EFE70 File Offset: 0x001EE070
		public static void resetTransGameScr(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.translate(0, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.translate(-GameScr.cmx, -GameScr.cmy);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000034B9 File Offset: 0x000016B9
		public void start()
		{
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void debug(string s, int type)
		{
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x001EFEC0 File Offset: 0x001EE0C0
		public void doResetToLoginScr(mScreen screen)
		{
			try
			{
				SoundMn.gI().stopAll();
				LoginScr.isContinueToLogin = false;
				TileMap.lastType = (TileMap.bgType = 0);
				Char.clearMyChar();
				GameScr.clearGameScr();
				GameScr.resetAllvector();
				InfoDlg.hide();
				GameScr.info1.hide();
				GameScr.info2.hide();
				GameScr.info2.cmdChat = null;
				Hint.isShow = false;
				ChatPopup.currChatPopup = null;
				Controller.isStopReadMessage = false;
				GameScr.loadCamera(true, -1, -1);
				GameScr.cmx = 100;
				GameCanvas.panel.currentTabIndex = 0;
				GameCanvas.panel.selected = (GameCanvas.isTouch ? -1 : 0);
				GameCanvas.panel.init();
				GameCanvas.panel2 = null;
				GameScr.isPaint = true;
				ClanMessage.vMessage.removeAllElements();
				GameScr.textTime.removeAllElements();
				GameScr.vClan.removeAllElements();
				GameScr.vFriend.removeAllElements();
				GameScr.vEnemies.removeAllElements();
				TileMap.vCurrItem.removeAllElements();
				BackgroudEffect.vBgEffect.removeAllElements();
				EffecMn.vEff.removeAllElements();
				Effect.newEff.removeAllElements();
				GameCanvas.menu.showMenu = false;
				GameCanvas.panel.vItemCombine.removeAllElements();
				GameCanvas.panel.isShow = false;
				if (GameCanvas.panel.tabIcon != null)
				{
					GameCanvas.panel.tabIcon.isShow = false;
				}
				if (mGraphics.zoomLevel == 1)
				{
					SmallImage.clearHastable();
				}
				Session_ME.gI().close();
				Session_ME2.gI().close();
				screen.switchToMe();
			}
			catch (Exception ex)
			{
				Cout.println("Loi tai doResetToLoginScr " + ex.ToString());
			}
			ServerListScreen.isAutoConect = true;
			ServerListScreen.countDieConnect = 0;
			ServerListScreen.testConnect = -1;
			ServerListScreen.loadScreen = true;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void paintCloud(mGraphics g)
		{
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void updateBG()
		{
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x001F0084 File Offset: 0x001EE284
		public static void fillRect(mGraphics g, int color, int x, int y, int w, int h, int detalY)
		{
			g.setColor(color);
			int cmy = GameScr.cmy;
			if (cmy > GameCanvas.h)
			{
				cmy = GameCanvas.h;
			}
			g.fillRect(x, y - ((detalY != 0) ? (cmy >> detalY) : 0), w, h + ((detalY != 0) ? (cmy >> detalY) : 0));
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x001F00D8 File Offset: 0x001EE2D8
		public static void paintBackgroundtLayer(mGraphics g, int layer, int deltaY, int color1, int color2)
		{
			try
			{
				int num = layer - 1;
				if (num == GameCanvas.imgBG.Length - 1 && (GameScr.gI().isRongThanXuatHien || GameScr.gI().isFireWorks))
				{
					g.setColor(GameScr.gI().mautroi);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					if (GameCanvas.typeBg == 2 || GameCanvas.typeBg == 4 || GameCanvas.typeBg == 7)
					{
						GameCanvas.drawSun1(g);
						GameCanvas.drawSun2(g);
					}
					if (GameScr.gI().isFireWorks && !GameCanvas.lowGraphic)
					{
						FireWorkEff.paint(g);
					}
				}
				else if (GameCanvas.imgBG != null && GameCanvas.imgBG[num] != null)
				{
					if (GameCanvas.moveX[num] != 0)
					{
						GameCanvas.moveX[num] += GameCanvas.moveXSpeed[num];
					}
					int cmy = GameScr.cmy;
					if (cmy > GameCanvas.h)
					{
						cmy = GameCanvas.h;
					}
					if (GameCanvas.layerSpeed[num] != 0)
					{
						for (int i = -((GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]) % GameCanvas.bgW[num]); i < GameScr.gW; i += GameCanvas.bgW[num])
						{
							g.drawImage(GameCanvas.imgBG[num], i, GameCanvas.yb[num] - ((deltaY > 0) ? (cmy >> deltaY) : 0), 0);
						}
					}
					else
					{
						for (int j = 0; j < GameScr.gW; j += GameCanvas.bgW[num])
						{
							g.drawImage(GameCanvas.imgBG[num], j, GameCanvas.yb[num] - ((deltaY > 0) ? (cmy >> deltaY) : 0), 0);
						}
					}
					if (color1 != -1)
					{
						if (num == GameCanvas.nBg - 1)
						{
							GameCanvas.fillRect(g, color1, 0, -(cmy >> deltaY), GameScr.gW, GameCanvas.yb[num], deltaY);
						}
						else
						{
							GameCanvas.fillRect(g, color1, 0, GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1], GameScr.gW, GameCanvas.yb[num] - (GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1]), deltaY);
						}
					}
					if (color2 != -1)
					{
						if (num == 0)
						{
							GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameScr.gH - (GameCanvas.yb[num] + GameCanvas.bgH[num]), deltaY);
						}
						else
						{
							GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameCanvas.yb[num - 1] - (GameCanvas.yb[num] + GameCanvas.bgH[num]) + 80, deltaY);
						}
					}
					if (GameCanvas.currentScreen == GameScr.instance)
					{
						if (layer == 1 && GameCanvas.typeBg == 11)
						{
							g.drawImage(GameCanvas.imgSun2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 400, GameCanvas.yb[0] + 30 - (cmy >> 2), StaticObj.BOTTOM_HCENTER);
						}
						if (layer == 1 && GameCanvas.typeBg == 13)
						{
							g.drawImage(GameCanvas.imgBG[1], -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200, GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
							g.drawRegion(GameCanvas.imgBG[1], 0, 0, GameCanvas.bgW[1], GameCanvas.bgH[1], 2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200 + GameCanvas.bgW[1], GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
						}
						if (layer == 3 && TileMap.mapID == 1)
						{
							for (int k = 0; k < TileMap.pxh / mGraphics.getImageHeight(GameCanvas.imgCaycot); k++)
							{
								g.drawImage(GameCanvas.imgCaycot, -(GameScr.cmx >> GameCanvas.layerSpeed[2]) + 300, k * mGraphics.getImageHeight(GameCanvas.imgCaycot) - (cmy >> 3), 0);
							}
						}
					}
					int x = -(GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]);
					EffecMn.paintBackGroundUnderLayer(g, x, GameCanvas.yb[num] + GameCanvas.bgH[num] - (cmy >> deltaY), num);
				}
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham paint bground: " + ex.ToString());
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x001F04F4 File Offset: 0x001EE6F4
		public static void drawSun1(mGraphics g)
		{
			if (GameCanvas.imgSun != null)
			{
				g.drawImage(GameCanvas.imgSun, GameCanvas.sunX, GameCanvas.sunY, 0);
			}
			if (!GameCanvas.isBoltEff)
			{
				return;
			}
			if (GameCanvas.gameTick % 200 == 0)
			{
				GameCanvas.boltActive = true;
			}
			if (GameCanvas.boltActive)
			{
				GameCanvas.tBolt++;
				if (GameCanvas.tBolt == 10)
				{
					GameCanvas.tBolt = 0;
					GameCanvas.boltActive = false;
				}
				if (GameCanvas.tBolt % 2 == 0)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				}
			}
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x001F0587 File Offset: 0x001EE787
		public static void drawSun2(mGraphics g)
		{
			if (GameCanvas.imgSun2 != null)
			{
				g.drawImage(GameCanvas.imgSun2, GameCanvas.sunX2, GameCanvas.sunY2, 0);
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x001F05A6 File Offset: 0x001EE7A6
		public static bool isHDVersion()
		{
			return mGraphics.zoomLevel > 1;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x001F05B4 File Offset: 0x001EE7B4
		public static void paintBGGameScr(mGraphics g)
		{
			if (!GameCanvas.isLoadBGok)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			}
			if (Char.isLoadingMap)
			{
				return;
			}
			int gW = GameScr.gW;
			int gH = GameScr.gH;
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			if (ModFunc.GiamDungLuong)
			{
				g.setColor(ModFunc.GetColor());
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				return;
			}
			try
			{
				if (GameCanvas.paintBG)
				{
					if (GameCanvas.currentScreen == GameScr.gI())
					{
						if (TileMap.mapID == 137 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 120 || TileMap.isMapDouble)
						{
							g.setColor(0);
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
							return;
						}
						if (TileMap.mapID == 138)
						{
							g.setColor(6776679);
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
							return;
						}
					}
					if (GameCanvas.typeBg == 0)
					{
						GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					}
					else if (GameCanvas.typeBg == 1)
					{
						GameCanvas.paintBackgroundtLayer(g, 4, 6, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, -1);
						GameCanvas.fillRect(g, GameCanvas.colorTop[2], 0, -(GameScr.cmy >> 5), gW, GameCanvas.yb[2], 5);
						GameCanvas.fillRect(g, GameCanvas.colorBotton[2], 0, GameCanvas.yb[2] + GameCanvas.bgH[2] - (GameScr.cmy >> 3), gW, 70, 3);
						GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					}
					else if (GameCanvas.typeBg == 2)
					{
						GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
						GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
						GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
						GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
						GameCanvas.paintCloud(g);
					}
					else if (GameCanvas.typeBg == 3)
					{
						int num = GameScr.cmy - (325 - GameScr.gH23);
						g.translate(0, -num);
						GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien && !GameScr.gI().isFireWorks) ? GameCanvas.colorTop[2] : GameScr.gI().mautroi, 0, num - (GameScr.cmy >> 3), gW, GameCanvas.yb[2] - num + (GameScr.cmy >> 3) + 100, 2);
						GameCanvas.paintBackgroundtLayer(g, 3, 2, -1, GameCanvas.colorBotton[2]);
						GameCanvas.paintBackgroundtLayer(g, 2, 0, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, GameCanvas.colorBotton[0]);
						g.translate(0, -g.getTranslateY());
					}
					else if (GameCanvas.typeBg == 4)
					{
						GameCanvas.paintBackgroundtLayer(g, 4, 7, GameCanvas.colorTop[3], -1);
						GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, (!GameCanvas.isHDVersion()) ? GameCanvas.colorTop[1] : GameCanvas.colorBotton[2]);
						GameCanvas.paintBackgroundtLayer(g, 2, 2, GameCanvas.colorTop[1], GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
					}
					else if (GameCanvas.typeBg == 5)
					{
						GameCanvas.paintBackgroundtLayer(g, 4, 15, GameCanvas.colorTop[3], -1);
						GameCanvas.drawSun1(g);
						g.translate(100, 10);
						GameCanvas.drawSun1(g);
						g.translate(-100, -10);
						GameCanvas.drawSun2(g);
						GameCanvas.paintBackgroundtLayer(g, 3, 10, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 2, 6, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 1, 4, -1, -1);
						g.translate(0, 27);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, -1);
						g.translate(0, 20);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
						g.translate(-g.getTranslateX(), -g.getTranslateY());
					}
					else if (GameCanvas.typeBg == 6)
					{
						GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
						GameCanvas.drawSun1(g);
						GameCanvas.drawSun2(g);
						g.translate(60, 40);
						GameCanvas.drawSun2(g);
						g.translate(-60, -40);
						GameCanvas.paintBackgroundtLayer(g, 4, 7, -1, GameCanvas.colorBotton[3]);
						BackgroudEffect.paintFarAll(g);
						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					}
					else if (GameCanvas.typeBg == 7)
					{
						GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
						GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 2, 4, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 1, 3, -1, GameCanvas.colorBotton[0]);
					}
					else if (GameCanvas.typeBg == 8)
					{
						GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
						GameCanvas.drawSun1(g);
						GameCanvas.drawSun2(g);
						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
						GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
						if (((TileMap.mapID < 92 || TileMap.mapID > 96) && TileMap.mapID != 51 && TileMap.mapID != 52) || GameCanvas.currentScreen == GameCanvas.loginScr)
						{
							GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
						}
					}
					else if (GameCanvas.typeBg == 9)
					{
						GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
						GameCanvas.drawSun1(g);
						GameCanvas.drawSun2(g);
						g.translate(-80, 20);
						GameCanvas.drawSun2(g);
						g.translate(80, -20);
						BackgroudEffect.paintFarAll(g);
						GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, -1);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					}
					else if (GameCanvas.typeBg == 10)
					{
						int num2 = GameScr.cmy - (380 - GameScr.gH23);
						g.translate(0, -num2);
						GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien) ? GameCanvas.colorTop[1] : GameScr.gI().mautroi, 0, num2 - (GameScr.cmy >> 2), gW, GameCanvas.yb[1] - num2 + (GameScr.cmy >> 2) + 100, 2);
						GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
						GameCanvas.drawSun1(g);
						GameCanvas.drawSun2(g);
						GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, -1);
						g.translate(0, -g.getTranslateY());
					}
					else if (GameCanvas.typeBg == 11)
					{
						GameCanvas.paintBackgroundtLayer(g, 3, 6, GameCanvas.colorTop[2], GameCanvas.colorBotton[2]);
						GameCanvas.drawSun1(g);
						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					}
					else if (GameCanvas.typeBg == 12)
					{
						g.setColor(9161471);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, 14417919);
						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, 14417919);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, 14417919);
						GameCanvas.paintCloud(g);
					}
					else if (GameCanvas.typeBg == 13)
					{
						g.setColor(15268088);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						GameCanvas.paintBackgroundtLayer(g, 1, 5, -1, 15268088);
					}
					else if (GameCanvas.typeBg == 15)
					{
						g.setColor(2631752);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					}
					else if (GameCanvas.typeBg == 16)
					{
						GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
						for (int i = 0; i < GameCanvas.imgSunSpec.Length; i++)
						{
							g.drawImage(GameCanvas.imgSunSpec[i], GameCanvas.cloudX[i], GameCanvas.cloudY[i], 33);
						}
						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					}
					else if (GameCanvas.typeBg == 19)
					{
						GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
						GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
						GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
						GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
						GameCanvas.paintCloud(g);
					}
					else
					{
						GameCanvas.fillRect(g, GameCanvas.colorBotton[3], 0, GameCanvas.yb[3] + GameCanvas.bgH[3], GameScr.gW, GameCanvas.yb[2] + GameCanvas.bgH[2], 6);
						GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
						GameCanvas.drawSun1(g);
						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
					}
				}
				else
				{
					g.setColor(2315859);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					if (GameCanvas.tam != null)
					{
						for (int j = -((GameScr.cmx >> 2) % mGraphics.getImageWidth(GameCanvas.tam)); j < GameScr.gW; j += mGraphics.getImageWidth(GameCanvas.tam))
						{
							g.drawImage(GameCanvas.tam, j, (GameScr.cmy >> 3) + GameCanvas.h / 2 - 50, 0);
						}
					}
					g.setColor(5084791);
					g.fillRect(0, (GameScr.cmy >> 3) + GameCanvas.h / 2 - 50 + mGraphics.getImageHeight(GameCanvas.tam), gW, GameCanvas.h);
				}
			}
			catch (Exception)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				g.drawImageScale(ModFunc.imgBg, 0, 0, GameCanvas.w, GameCanvas.h, 0);
			}
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void resetBg()
		{
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x001F0FDC File Offset: 0x001EF1DC
		public static void getYBackground(int typeBg)
		{
			try
			{
				int gH = GameScr.gH23;
				switch (typeBg)
				{
				case 0:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 70;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 20;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 30;
					GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
					goto IL_67A;
				case 1:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 120;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 40;
					GameCanvas.yb[2] = GameCanvas.yb[1] - 90;
					GameCanvas.yb[3] = GameCanvas.yb[2] - 25;
					goto IL_67A;
				case 2:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
					GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
					GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
					goto IL_67A;
				case 3:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 10;
					GameCanvas.yb[1] = GameCanvas.yb[0] + 80;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
					goto IL_67A;
				case 4:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 130;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1];
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 20;
					GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 80;
					goto IL_67A;
				case 5:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 40;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 10;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 15;
					GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
					goto IL_67A;
				case 6:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 30;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 10;
					GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 15;
					GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4] + 15;
					goto IL_67A;
				case 7:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 20;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 15;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 20;
					GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
					goto IL_67A;
				case 8:
					GameCanvas.yb[0] = gH - 103 + 150;
					if (TileMap.mapID == 103)
					{
						GameCanvas.yb[0] -= 100;
					}
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 40;
					GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 10;
					goto IL_67A;
				case 9:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 22;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
					GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3];
					goto IL_67A;
				case 10:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] - 45;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
					goto IL_67A;
				case 11:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 60;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 5;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 15;
					goto IL_67A;
				case 12:
					GameCanvas.yb[0] = gH + 40;
					GameCanvas.yb[1] = GameCanvas.yb[0] - 40;
					GameCanvas.yb[2] = GameCanvas.yb[1] - 40;
					goto IL_67A;
				case 13:
					GameCanvas.yb[0] = gH - 80;
					GameCanvas.yb[1] = GameCanvas.yb[0];
					goto IL_67A;
				case 15:
					GameCanvas.yb[0] = gH - 20;
					GameCanvas.yb[1] = GameCanvas.yb[0] - 80;
					goto IL_67A;
				case 16:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
					GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
					goto IL_67A;
				case 19:
					GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
					GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
					GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
					GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
					GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
					goto IL_67A;
				}
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
				IL_67A:;
			}
			catch (Exception)
			{
				int gH2 = GameScr.gH23;
				for (int i = 0; i < GameCanvas.yb.Length; i++)
				{
					GameCanvas.yb[i] = 1;
				}
			}
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x001F16A4 File Offset: 0x001EF8A4
		public static void loadBG(int typeBG)
		{
			try
			{
				GameCanvas.isLoadBGok = true;
				if (GameCanvas.typeBg == 12)
				{
					BackgroudEffect.yfog = TileMap.pxh - 100;
				}
				else
				{
					BackgroudEffect.yfog = TileMap.pxh - 160;
				}
				BackgroudEffect.clearImage();
				GameCanvas.randomRaintEff(typeBG);
				if ((TileMap.lastBgID != typeBG || TileMap.lastType != TileMap.bgType) && typeBG != -1)
				{
					GameCanvas.transY = 12;
					TileMap.lastBgID = (int)((sbyte)typeBG);
					TileMap.lastType = (int)((sbyte)TileMap.bgType);
					GameCanvas.layerSpeed = new int[]
					{
						1,
						2,
						3,
						7,
						8
					};
					GameCanvas.moveX = new int[5];
					GameCanvas.moveXSpeed = new int[5];
					GameCanvas.typeBg = typeBG;
					GameCanvas.isBoltEff = false;
					GameScr.firstY = GameScr.cmy;
					GameCanvas.imgBG = null;
					GameCanvas.imgCloud = null;
					GameCanvas.imgSun = null;
					GameCanvas.imgCaycot = null;
					GameScr.firstY = -1;
					switch (GameCanvas.typeBg)
					{
					case 0:
						GameCanvas.imgCaycot = GameCanvas.loadImageRMS("/bg/caycot.png");
						GameCanvas.layerSpeed = new int[]
						{
							1,
							3,
							5,
							7
						};
						GameCanvas.nBg = 4;
						if (TileMap.bgType == 2)
						{
							GameCanvas.transY = 8;
							goto IL_31E;
						}
						goto IL_31E;
					case 1:
						GameCanvas.transY = 7;
						GameCanvas.nBg = 4;
						goto IL_31E;
					case 2:
					{
						int[] array = new int[5];
						array[2] = 1;
						GameCanvas.moveX = array;
						int[] array2 = new int[5];
						array2[2] = 2;
						GameCanvas.moveXSpeed = array2;
						GameCanvas.nBg = 5;
						goto IL_31E;
					}
					case 3:
						GameCanvas.nBg = 3;
						goto IL_31E;
					case 4:
					{
						BackgroudEffect.addEffect(3);
						int[] array3 = new int[5];
						array3[1] = 1;
						GameCanvas.moveX = array3;
						int[] array4 = new int[5];
						array4[1] = 1;
						GameCanvas.moveXSpeed = array4;
						GameCanvas.nBg = 4;
						goto IL_31E;
					}
					case 5:
						GameCanvas.nBg = 4;
						goto IL_31E;
					case 6:
					{
						int[] array5 = new int[5];
						array5[0] = 1;
						GameCanvas.moveX = array5;
						int[] array6 = new int[5];
						array6[0] = 2;
						GameCanvas.moveXSpeed = array6;
						GameCanvas.nBg = 5;
						goto IL_31E;
					}
					case 7:
						GameCanvas.nBg = 4;
						goto IL_31E;
					case 8:
						GameCanvas.transY = 8;
						GameCanvas.nBg = 4;
						goto IL_31E;
					case 9:
						BackgroudEffect.addEffect(9);
						GameCanvas.nBg = 4;
						goto IL_31E;
					case 10:
						GameCanvas.nBg = 2;
						goto IL_31E;
					case 11:
						GameCanvas.transY = 7;
						GameCanvas.layerSpeed[2] = 0;
						GameCanvas.nBg = 3;
						goto IL_31E;
					case 12:
					{
						int[] array7 = new int[5];
						array7[0] = 1;
						array7[1] = 1;
						GameCanvas.moveX = array7;
						int[] array8 = new int[5];
						array8[0] = 2;
						array8[1] = 1;
						GameCanvas.moveXSpeed = array8;
						GameCanvas.nBg = 3;
						goto IL_31E;
					}
					case 13:
						GameCanvas.nBg = 2;
						goto IL_31E;
					case 15:
						Res.outz("HELL");
						GameCanvas.nBg = 2;
						goto IL_31E;
					case 16:
						GameCanvas.layerSpeed = new int[]
						{
							1,
							3,
							5,
							7
						};
						GameCanvas.nBg = 4;
						goto IL_31E;
					case 19:
					{
						int[] array9 = new int[5];
						array9[1] = 2;
						array9[2] = 1;
						GameCanvas.moveX = array9;
						int[] array10 = new int[5];
						array10[1] = 2;
						array10[2] = 1;
						GameCanvas.moveXSpeed = array10;
						GameCanvas.nBg = 5;
						goto IL_31E;
					}
					}
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					IL_31E:
					if (typeBG <= 16)
					{
						GameCanvas.skyColor = StaticObj.SKYCOLOR[GameCanvas.typeBg];
					}
					else
					{
						try
						{
							string path = "/bg/b" + GameCanvas.typeBg.ToString() + 3.ToString() + ".png";
							if (TileMap.bgType != 0)
							{
								path = string.Concat(new string[]
								{
									"/bg/b",
									GameCanvas.typeBg.ToString(),
									3.ToString(),
									"-",
									TileMap.bgType.ToString(),
									".png"
								});
							}
							int[] data = new int[1];
							Image image = GameCanvas.loadImageRMS(path);
							image.getRGB(ref data, 0, 1, mGraphics.getRealImageWidth(image) / 2, 0, 1, 1);
							GameCanvas.skyColor = data[0];
						}
						catch (Exception)
						{
							GameCanvas.skyColor = StaticObj.SKYCOLOR[StaticObj.SKYCOLOR.Length - 1];
						}
					}
					GameCanvas.colorTop = new int[StaticObj.SKYCOLOR.Length];
					GameCanvas.colorBotton = new int[StaticObj.SKYCOLOR.Length];
					for (int i = 0; i < StaticObj.SKYCOLOR.Length; i++)
					{
						GameCanvas.colorTop[i] = StaticObj.SKYCOLOR[i];
						GameCanvas.colorBotton[i] = StaticObj.SKYCOLOR[i];
					}
					if (GameCanvas.lowGraphic)
					{
						GameCanvas.tam = GameCanvas.loadImageRMS("/bg/b63.png");
					}
					else
					{
						GameCanvas.imgBG = new Image[GameCanvas.nBg];
						GameCanvas.bgW = new int[GameCanvas.nBg];
						GameCanvas.bgH = new int[GameCanvas.nBg];
						GameCanvas.colorBotton = new int[GameCanvas.nBg];
						GameCanvas.colorTop = new int[GameCanvas.nBg];
						if (TileMap.bgType == 100)
						{
							GameCanvas.imgBG[0] = GameCanvas.loadImageRMS("/bg/b100.png");
							GameCanvas.imgBG[1] = GameCanvas.loadImageRMS("/bg/b100.png");
							GameCanvas.imgBG[2] = GameCanvas.loadImageRMS("/bg/b82-1.png");
							GameCanvas.imgBG[3] = GameCanvas.loadImageRMS("/bg/b93.png");
							for (int j = 0; j < GameCanvas.nBg; j++)
							{
								if (GameCanvas.imgBG[j] != null)
								{
									int[] data2 = new int[1];
									GameCanvas.imgBG[j].getRGB(ref data2, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, 0, 1, 1);
									GameCanvas.colorTop[j] = data2[0];
									data2 = new int[1];
									GameCanvas.imgBG[j].getRGB(ref data2, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[j]) - 1, 1, 1);
									GameCanvas.colorBotton[j] = data2[0];
									GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
									GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
								}
								else if (GameCanvas.nBg > 1)
								{
									GameCanvas.imgBG[j] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg.ToString() + "0.png");
									GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
									GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
								}
							}
						}
						else
						{
							for (int k = 0; k < GameCanvas.nBg; k++)
							{
								string path2 = "/bg/b" + GameCanvas.typeBg.ToString() + k.ToString() + ".png";
								if (TileMap.bgType != 0)
								{
									path2 = string.Concat(new string[]
									{
										"/bg/b",
										GameCanvas.typeBg.ToString(),
										k.ToString(),
										"-",
										TileMap.bgType.ToString(),
										".png"
									});
								}
								GameCanvas.imgBG[k] = GameCanvas.loadImageRMS(path2);
								if (GameCanvas.imgBG[k] != null)
								{
									int[] data3 = new int[1];
									GameCanvas.imgBG[k].getRGB(ref data3, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, 0, 1, 1);
									GameCanvas.colorTop[k] = data3[0];
									data3 = new int[1];
									GameCanvas.imgBG[k].getRGB(ref data3, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[k]) - 1, 1, 1);
									GameCanvas.colorBotton[k] = data3[0];
									GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
									GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
								}
								else if (GameCanvas.nBg > 1)
								{
									GameCanvas.imgBG[k] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg.ToString() + "0.png");
									GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
									GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
								}
							}
						}
						GameCanvas.getYBackground(GameCanvas.typeBg);
						GameCanvas.cloudX = new int[]
						{
							GameScr.gW / 2 - 40,
							GameScr.gW / 2 + 40,
							GameScr.gW / 2 - 100,
							GameScr.gW / 2 - 80,
							GameScr.gW / 2 - 120
						};
						GameCanvas.cloudY = new int[]
						{
							130,
							100,
							150,
							140,
							80
						};
						GameCanvas.imgSunSpec = null;
						if (GameCanvas.typeBg != 0)
						{
							if (GameCanvas.typeBg == 2)
							{
								GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun0.png");
								GameCanvas.sunX = GameScr.gW / 2 + 50;
								GameCanvas.sunY = GameCanvas.yb[4] - 40;
								TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts");
							}
							else if (GameCanvas.typeBg == 19)
							{
								TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/water_flow_32");
							}
							else if (GameCanvas.typeBg == 4)
							{
								GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun2.png");
								GameCanvas.sunX = GameScr.gW / 2 + 30;
								GameCanvas.sunY = GameCanvas.yb[3];
							}
							else if (GameCanvas.typeBg == 7)
							{
								GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun3" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
								GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun4" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
								GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
								GameCanvas.sunY = GameCanvas.yb[3] - 80;
								GameCanvas.sunX2 = GameCanvas.sunX - 100;
								GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
							}
							else if (GameCanvas.typeBg == 6)
							{
								GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun5" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
								GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun6" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
								GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
								GameCanvas.sunY = GameCanvas.yb[4];
								GameCanvas.sunX2 = GameCanvas.sunX - 100;
								GameCanvas.sunY2 = GameCanvas.yb[4] + 20;
							}
							else if (typeBG == 5)
							{
								GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun8" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
								GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun7" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
								GameCanvas.sunX = GameScr.gW / 2 - 50;
								GameCanvas.sunY = GameCanvas.yb[3] + 20;
								GameCanvas.sunX2 = GameScr.gW / 2 + 20;
								GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
							}
							else if (GameCanvas.typeBg == 8 && TileMap.mapID < 90)
							{
								GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun9" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
								GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun10" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
								GameCanvas.sunX = GameScr.gW / 2 - 30;
								GameCanvas.sunY = GameCanvas.yb[3] + 60;
								GameCanvas.sunX2 = GameScr.gW / 2 + 20;
								GameCanvas.sunY2 = GameCanvas.yb[3] + 10;
							}
							else
							{
								switch (typeBG)
								{
								case 9:
									GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun11" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
									GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun12" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
									GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
									GameCanvas.sunY = GameCanvas.yb[4] + 20;
									GameCanvas.sunX2 = GameCanvas.sunX - 80;
									GameCanvas.sunY2 = GameCanvas.yb[4] + 40;
									goto IL_FE0;
								case 10:
									GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun13" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
									GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun14" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
									GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
									GameCanvas.sunY = GameCanvas.yb[1] - 30;
									GameCanvas.sunX2 = GameCanvas.sunX - 80;
									GameCanvas.sunY2 = GameCanvas.yb[1];
									goto IL_FE0;
								case 11:
									GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun15" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
									GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/b113" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
									GameCanvas.sunX = GameScr.gW / 2 - 30;
									GameCanvas.sunY = GameCanvas.yb[2] - 30;
									goto IL_FE0;
								case 12:
									GameCanvas.cloudY = new int[]
									{
										200,
										170,
										220,
										150,
										250
									};
									goto IL_FE0;
								case 16:
									GameCanvas.cloudX = new int[]
									{
										90,
										170,
										250,
										320,
										400,
										450,
										500
									};
									GameCanvas.cloudY = new int[]
									{
										GameCanvas.yb[2] + 5,
										GameCanvas.yb[2] - 20,
										GameCanvas.yb[2] - 50,
										GameCanvas.yb[2] - 30,
										GameCanvas.yb[2] - 50,
										GameCanvas.yb[2],
										GameCanvas.yb[2] - 40
									};
									GameCanvas.imgSunSpec = new Image[7];
									for (int l = 0; l < GameCanvas.imgSunSpec.Length; l++)
									{
										int num = 161;
										if (l == 0 || l == 2 || l == 3 || l == 2 || l == 6)
										{
											num = 160;
										}
										GameCanvas.imgSunSpec[l] = GameCanvas.loadImageRMS("/bg/sun" + num.ToString() + ".png");
									}
									goto IL_FE0;
								case 19:
								{
									int[] array11 = new int[5];
									array11[1] = 2;
									array11[2] = 1;
									GameCanvas.moveX = array11;
									int[] array12 = new int[5];
									array12[1] = 2;
									array12[2] = 1;
									GameCanvas.moveXSpeed = array12;
									GameCanvas.nBg = 5;
									goto IL_FE0;
								}
								}
								GameCanvas.imgCloud = null;
								GameCanvas.imgSun = null;
								GameCanvas.imgSun2 = null;
								GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun" + typeBG.ToString() + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
								GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
								GameCanvas.sunY = GameCanvas.yb[2] - 30;
							}
						}
						IL_FE0:
						GameCanvas.paintBG = false;
						if (!GameCanvas.paintBG)
						{
							GameCanvas.paintBG = true;
						}
					}
				}
			}
			catch (Exception)
			{
				GameCanvas.isLoadBGok = false;
			}
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x001F26E4 File Offset: 0x001F08E4
		private static void randomRaintEff(int typeBG)
		{
			for (int i = 0; i < GameCanvas.bgRain.Length; i++)
			{
				if (typeBG == GameCanvas.bgRain[i] && Res.random(0, 2) == 0)
				{
					BackgroudEffect.addEffect(0);
					return;
				}
			}
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x001F2720 File Offset: 0x001F0920
		public void keyPressedz(int keyCode)
		{
			GameCanvas.lastTimePress = mSystem.currentTimeMillis();
			if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32 || keyCode == 31)
			{
				GameCanvas.keyAsciiPress = keyCode;
			}
			this.mapKeyPress(keyCode);
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x001F2770 File Offset: 0x001F0970
		public void mapKeyPress(int keyCode)
		{
			if (GameCanvas.currentDialog != null)
			{
				GameCanvas.currentDialog.keyPress(keyCode);
				GameCanvas.keyAsciiPress = 0;
				return;
			}
			GameCanvas.currentScreen.keyPress(keyCode);
			if (keyCode <= -22)
			{
				if (keyCode <= -38)
				{
					if (keyCode == -39)
					{
						goto IL_13E;
					}
					if (keyCode != -38)
					{
						return;
					}
				}
				else
				{
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = true;
						GameCanvas.keyPressed[16] = true;
						return;
					}
					if (keyCode != -22)
					{
						return;
					}
					goto IL_350;
				}
			}
			else
			{
				if (keyCode <= -1)
				{
					if (keyCode != -21)
					{
						switch (keyCode)
						{
						case -8:
							GameCanvas.keyHold[14] = true;
							GameCanvas.keyPressed[14] = true;
							return;
						case -7:
							goto IL_350;
						case -6:
							break;
						case -5:
							goto IL_204;
						case -4:
							if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && Char.myCharz().isAttack)
							{
								GameCanvas.clearKeyHold();
								GameCanvas.clearKeyPressed();
								return;
							}
							GameCanvas.keyHold[24] = true;
							GameCanvas.keyPressed[24] = true;
							return;
						case -3:
							if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && Char.myCharz().isAttack)
							{
								GameCanvas.clearKeyHold();
								GameCanvas.clearKeyPressed();
								return;
							}
							GameCanvas.keyHold[23] = true;
							GameCanvas.keyPressed[23] = true;
							return;
						case -2:
							goto IL_13E;
						case -1:
							goto IL_FC;
						default:
							return;
						}
					}
					GameCanvas.keyHold[12] = true;
					GameCanvas.keyPressed[12] = true;
					return;
				}
				if (keyCode != 10)
				{
					switch (keyCode)
					{
					case 35:
						GameCanvas.keyHold[11] = true;
						GameCanvas.keyPressed[11] = true;
						return;
					case 36:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 43:
					case 44:
					case 45:
					case 46:
					case 47:
						break;
					case 42:
						GameCanvas.keyHold[10] = true;
						GameCanvas.keyPressed[10] = true;
						return;
					case 48:
						GameCanvas.keyHold[0] = true;
						GameCanvas.keyPressed[0] = true;
						return;
					case 49:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[1] = true;
							GameCanvas.keyPressed[1] = true;
							return;
						}
						break;
					case 50:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[2] = true;
							GameCanvas.keyPressed[2] = true;
							return;
						}
						break;
					case 51:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[3] = true;
							GameCanvas.keyPressed[3] = true;
							return;
						}
						break;
					case 52:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[4] = true;
							GameCanvas.keyPressed[4] = true;
							return;
						}
						break;
					case 53:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[5] = true;
							GameCanvas.keyPressed[5] = true;
							return;
						}
						break;
					case 54:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[6] = true;
							GameCanvas.keyPressed[6] = true;
							return;
						}
						break;
					case 55:
						GameCanvas.keyHold[7] = true;
						GameCanvas.keyPressed[7] = true;
						return;
					case 56:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[8] = true;
							GameCanvas.keyPressed[8] = true;
							return;
						}
						break;
					case 57:
						GameCanvas.keyHold[9] = true;
						GameCanvas.keyPressed[9] = true;
						return;
					default:
						if (keyCode != 113)
						{
							return;
						}
						GameCanvas.keyHold[17] = true;
						GameCanvas.keyPressed[17] = true;
						break;
					}
					return;
				}
				IL_204:
				if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && Char.myCharz().isAttack)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[25] = true;
				GameCanvas.keyPressed[25] = true;
				GameCanvas.keyHold[15] = true;
				GameCanvas.keyPressed[15] = true;
				return;
			}
			IL_FC:
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[21] = true;
			GameCanvas.keyPressed[21] = true;
			return;
			IL_13E:
			if ((GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[22] = true;
			GameCanvas.keyPressed[22] = true;
			return;
			IL_350:
			GameCanvas.keyHold[13] = true;
			GameCanvas.keyPressed[13] = true;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x001F2C68 File Offset: 0x001F0E68
		public void keyReleasedz(int keyCode)
		{
			GameCanvas.keyAsciiPress = 0;
			this.mapKeyRelease(keyCode);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x001F2C78 File Offset: 0x001F0E78
		public void mapKeyRelease(int keyCode)
		{
			if (keyCode > -22)
			{
				if (keyCode <= -1)
				{
					if (keyCode != -21)
					{
						switch (keyCode)
						{
						case -8:
							GameCanvas.keyHold[14] = false;
							return;
						case -7:
							goto IL_21D;
						case -6:
							break;
						case -5:
							goto IL_100;
						case -4:
							GameCanvas.keyHold[24] = false;
							return;
						case -3:
							GameCanvas.keyHold[23] = false;
							return;
						case -2:
							goto IL_E2;
						case -1:
							goto IL_D8;
						default:
							return;
						}
					}
					GameCanvas.keyHold[12] = false;
					GameCanvas.keyReleased[12] = true;
					return;
				}
				if (keyCode != 10)
				{
					switch (keyCode)
					{
					case 35:
						GameCanvas.keyHold[11] = false;
						GameCanvas.keyReleased[11] = true;
						return;
					case 36:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 43:
					case 44:
					case 45:
					case 46:
					case 47:
						break;
					case 42:
						GameCanvas.keyHold[10] = false;
						GameCanvas.keyReleased[10] = true;
						return;
					case 48:
						GameCanvas.keyHold[0] = false;
						GameCanvas.keyReleased[0] = true;
						return;
					case 49:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[1] = false;
							GameCanvas.keyReleased[1] = true;
							return;
						}
						break;
					case 50:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[2] = false;
							GameCanvas.keyReleased[2] = true;
							return;
						}
						break;
					case 51:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[3] = false;
							GameCanvas.keyReleased[3] = true;
							return;
						}
						break;
					case 52:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[4] = false;
							GameCanvas.keyReleased[4] = true;
							return;
						}
						break;
					case 53:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[5] = false;
							GameCanvas.keyReleased[5] = true;
							return;
						}
						break;
					case 54:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[6] = false;
							GameCanvas.keyReleased[6] = true;
							return;
						}
						break;
					case 55:
						GameCanvas.keyHold[7] = false;
						GameCanvas.keyReleased[7] = true;
						return;
					case 56:
						if (GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow))
						{
							GameCanvas.keyHold[8] = false;
							GameCanvas.keyReleased[8] = true;
							return;
						}
						break;
					case 57:
						GameCanvas.keyHold[9] = false;
						GameCanvas.keyReleased[9] = true;
						return;
					default:
						if (keyCode != 113)
						{
							return;
						}
						GameCanvas.keyHold[17] = false;
						GameCanvas.keyReleased[17] = true;
						break;
					}
					return;
				}
				IL_100:
				GameCanvas.keyHold[25] = false;
				GameCanvas.keyReleased[25] = true;
				GameCanvas.keyHold[15] = true;
				GameCanvas.keyPressed[15] = true;
				return;
			}
			if (keyCode <= -38)
			{
				if (keyCode == -39)
				{
					goto IL_E2;
				}
				if (keyCode != -38)
				{
					return;
				}
			}
			else
			{
				if (keyCode == -26)
				{
					GameCanvas.keyHold[16] = false;
					return;
				}
				if (keyCode != -22)
				{
					return;
				}
				goto IL_21D;
			}
			IL_D8:
			GameCanvas.keyHold[21] = false;
			return;
			IL_E2:
			GameCanvas.keyHold[22] = false;
			return;
			IL_21D:
			GameCanvas.keyHold[13] = false;
			GameCanvas.keyReleased[13] = true;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x001F3025 File Offset: 0x001F1225
		public void pointerMouse(int x, int y)
		{
			GameCanvas.pxMouse = x;
			GameCanvas.pyMouse = y;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x001F3033 File Offset: 0x001F1233
		public void scrollMouse(int a)
		{
			GameCanvas.pXYScrollMouse = a;
			if (GameCanvas.panel != null && GameCanvas.panel.isShow)
			{
				GameCanvas.panel.updateScroolMouse(a);
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x001F305C File Offset: 0x001F125C
		public void pointerDragged(int x, int y)
		{
			if (Res.abs(x - GameCanvas.pxLast) >= 10 || Res.abs(y - GameCanvas.pyLast) >= 10)
			{
				GameCanvas.isPointerClick = false;
				GameCanvas.isPointerDown = true;
				GameCanvas.isPointerMove = true;
			}
			GameCanvas.px = x;
			GameCanvas.py = y;
			GameCanvas.curPos++;
			if (GameCanvas.curPos > 3)
			{
				GameCanvas.curPos = 0;
			}
			GameCanvas.arrPos[GameCanvas.curPos] = new Position(x, y);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x001F30D4 File Offset: 0x001F12D4
		public void pointerPressed(int x, int y)
		{
			GameCanvas.isPointerJustRelease = false;
			GameCanvas.isPointerJustDown = true;
			GameCanvas.isPointerDown = true;
			GameCanvas.isPointerClick = true;
			GameCanvas.isPointerMove = false;
			GameCanvas.lastTimePress = mSystem.currentTimeMillis();
			GameCanvas.pxFirst = x;
			GameCanvas.pyFirst = y;
			GameCanvas.pxLast = x;
			GameCanvas.pyLast = y;
			GameCanvas.px = x;
			GameCanvas.py = y;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x001F312D File Offset: 0x001F132D
		public void pointerReleased(int x, int y)
		{
			GameCanvas.isPointerDown = false;
			GameCanvas.isPointerJustRelease = true;
			GameCanvas.isPointerMove = false;
			mScreen.keyTouch = -1;
			GameCanvas.px = x;
			GameCanvas.py = y;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x001F3153 File Offset: 0x001F1353
		public static bool isPointerHoldIn(int x, int y, int w, int h)
		{
			return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x001F318C File Offset: 0x001F138C
		public static bool isMouseFocus(int x, int y, int w, int h)
		{
			return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x001F31B8 File Offset: 0x001F13B8
		public static void clearKeyPressed()
		{
			for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
			{
				GameCanvas.keyPressed[i] = false;
			}
			GameCanvas.isPointerJustRelease = false;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x001F31E8 File Offset: 0x001F13E8
		public static void clearKeyHold()
		{
			for (int i = 0; i < GameCanvas.keyHold.Length; i++)
			{
				GameCanvas.keyHold[i] = false;
			}
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x001F3210 File Offset: 0x001F1410
		public void paintChangeMap(mGraphics g)
		{
			string empty = string.Empty;
			GameCanvas.resetTrans(g);
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			ModFunc.PaintLogoGif(g, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
			mFont.tahoma_7b_white.drawString(g, mResources.PLEASEWAIT + ((LoginScr.timeLogin <= 0) ? empty : (" " + LoginScr.timeLogin.ToString() + "s")), GameCanvas.w / 2, GameCanvas.h / 2, 2);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x001F32BC File Offset: 0x001F14BC
		public void paint(mGraphics gx)
		{
			try
			{
				GameCanvas.debugPaint.removeAllElements();
				GameCanvas.debug("PA", 1);
				if (GameCanvas.currentScreen != null)
				{
					GameCanvas.currentScreen.paint(this.g);
				}
				GameCanvas.debug("PB", 1);
				this.g.translate(-this.g.getTranslateX(), -this.g.getTranslateY());
				this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				if (GameCanvas.panel != null && GameCanvas.panel.isShow)
				{
					GameCanvas.panel.paint(this.g);
					if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
					{
						GameCanvas.panel2.paint(this.g);
					}
					if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
					{
						GameCanvas.panel.chatTField.paint(this.g);
					}
					if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
					{
						GameCanvas.panel2.chatTField.paint(this.g);
					}
				}
				Res.paintOnScreenDebug(this.g);
				InfoDlg.paint(this.g);
				if (GameCanvas.currentDialog != null)
				{
					GameCanvas.debug("PC", 1);
					GameCanvas.currentDialog.paint(this.g);
				}
				else if (GameCanvas.menu.showMenu)
				{
					GameCanvas.debug("PD", 1);
					GameCanvas.menu.paintMenu(this.g);
				}
				GameScr.info1.paint(this.g);
				GameScr.info2.paint(this.g);
				if (GameScr.gI().popUpYesNo != null)
				{
					GameScr.gI().popUpYesNo.paint(this.g);
				}
				if (ChatPopup.currChatPopup != null)
				{
					ChatPopup.currChatPopup.paint(this.g);
				}
				Hint.paint(this.g);
				if (ChatPopup.serverChatPopUp != null)
				{
					ChatPopup.serverChatPopUp.paint(this.g);
				}
				for (int i = 0; i < Effect2.vEffect2.size(); i++)
				{
					Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
					if (effect is ChatPopup && !effect.Equals(ChatPopup.currChatPopup) && !effect.Equals(ChatPopup.serverChatPopUp))
					{
						effect.paint(this.g);
					}
				}
				if (Char.isLoadingMap || LoginScr.isContinueToLogin || ServerListScreen.waitToLogin || ServerListScreen.isWait)
				{
					this.paintChangeMap(this.g);
					if (GameCanvas.timeLoading > 0 && LoginScr.timeLogin <= 0)
					{
						GameCanvas.startWaitDlg();
						if (mSystem.currentTimeMillis() - GameCanvas.TIMEOUT >= 1000L)
						{
							GameCanvas.timeLoading--;
							Res.outz("[COUNT] == " + GameCanvas.timeLoading.ToString());
							if (GameCanvas.timeLoading == 0)
							{
								GameCanvas.timeLoading = 15;
							}
							GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
						}
					}
					if (mSystem.currentTimeMillis() > GameCanvas.timeBreakLoading)
					{
						GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
						if (GameCanvas.currentScreen != null)
						{
							if (GameCanvas.currentScreen is GameScr)
							{
								GameScr.gI().switchToMe();
							}
							else if (!(GameCanvas.currentScreen is SplashScr) && GameCanvas.currentScreen is LoginScr)
							{
								GameCanvas.gI().resetToLoginScrz();
							}
						}
					}
				}
				GameCanvas.debug("PE", 1);
				GameCanvas.resetTrans(this.g);
				EffecMn.paintLayer4(this.g);
				GameCanvas.resetTrans(this.g);
				int num = GameCanvas.h / 4;
				if (GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr && GameCanvas.thongBaoTest != null)
				{
					this.g.setClip(60, num, GameCanvas.w - 120, mFont.tahoma_7_white.getHeight() + 2);
					mFont.tahoma_7_grey.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num + 1, 0);
					mFont.tahoma_7_yellow.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num, 0);
					this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				}
				TabController.Instance.paint(this.g);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x001F3704 File Offset: 0x001F1904
		public static void endDlg()
		{
			if (GameCanvas.inputDlg != null)
			{
				GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
			}
			GameCanvas.currentDialog = null;
			InfoDlg.hide();
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x001F372C File Offset: 0x001F192C
		public static void startOKDlg(string info)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x001F375F File Offset: 0x001F195F
		public static void startWaitDlg(string info)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
			GameCanvas.msgdlg.isWait = true;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x001F375F File Offset: 0x001F195F
		public static void startOKDlg(string info, bool isError)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
			GameCanvas.msgdlg.isWait = true;
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x001F379D File Offset: 0x001F199D
		public static void startWaitDlg()
		{
			GameCanvas.closeKeyBoard();
			Char.isLoadingMap = true;
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x001F37AA File Offset: 0x001F19AA
		public static void startOK(string info, int actionID, object p)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
			GameCanvas.msgdlg.show();
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x001F37DC File Offset: 0x001F19DC
		public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
			GameCanvas.msgdlg.show();
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x001F3837 File Offset: 0x001F1A37
		public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
			GameCanvas.msgdlg.show();
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x001F3856 File Offset: 0x001F1A56
		public static void startserverThongBao(string msgSv)
		{
			GameCanvas.thongBaoTest = msgSv;
			GameCanvas.xThongBaoTranslate = GameCanvas.w - 60;
			GameCanvas.dir_ = -1;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00006D67 File Offset: 0x00004F67
		public static bool isGetResourceFromServer()
		{
			return true;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x001F3874 File Offset: 0x001F1A74
		public static Image loadImageRMS(string path)
		{
			path = Main.res + "/x" + mGraphics.zoomLevel.ToString() + path;
			path = GameCanvas.cutPng(path);
			Image result = null;
			try
			{
				result = Image.createImage(path);
			}
			catch (Exception ex)
			{
				try
				{
					string[] array = Res.split(path, "/", 0);
					sbyte[] array2 = Rms.loadRMS("x" + mGraphics.zoomLevel.ToString() + array[array.Length - 1]);
					if (array2 != null)
					{
						result = Image.createImage(array2, 0, array2.Length);
					}
				}
				catch (Exception)
				{
					Cout.LogError("Loi ham khong tim thay a: " + ex.ToString());
				}
			}
			return result;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x001F3928 File Offset: 0x001F1B28
		public static Image LoadImageFromRoot(string path)
		{
			path = Main.res + path;
			path = GameCanvas.cutPng(path);
			Image result = null;
			try
			{
				result = Image.createImage(path);
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x001F396C File Offset: 0x001F1B6C
		public static Image loadImage(string path)
		{
			string pathOrigin = path;
			path = Main.res + "/x" + mGraphics.zoomLevel.ToString() + path;
			path = GameCanvas.cutPng(path);
			Image result;
			try
			{
				result = Image.createImage(path);
			}
			catch (Exception)
			{
				result = GameCanvas.loadImageResize(pathOrigin);
			}
			return result;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x001F39C4 File Offset: 0x001F1BC4
		public static Image loadImageResize(string path)
		{
			path = Main.res + "/x4" + path;
			path = GameCanvas.cutPng(path);
			Image result = null;
			try
			{
				result = Image.createImage(path);
				if (mGraphics.zoomLevel != 4)
				{
					GameCanvas.resizeImage(result);
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x001F3A18 File Offset: 0x001F1C18
		public static Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
		{
			RenderTexture rt = RenderTexture.active = new RenderTexture(targetX, targetY, 24);
			Graphics.Blit(texture2D, rt);
			Texture2D texture2D2 = new Texture2D(targetX, targetY);
			texture2D2.ReadPixels(new Rect(0f, 0f, (float)targetX, (float)targetY), 0, 0);
			texture2D2.Apply();
			return texture2D2;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x001F3A64 File Offset: 0x001F1C64
		public static void resizeImage(Image img)
		{
			int num = img.w / 4;
			int hx = img.h / 4;
			int w = num * mGraphics.zoomLevel;
			int h = hx * mGraphics.zoomLevel;
			img.texture = GameCanvas.Resize(img.texture, w, h);
			img.w = img.texture.width;
			img.h = img.texture.height;
			Image.setTextureQuality(img.texture);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x001F3AD4 File Offset: 0x001F1CD4
		public static string cutPng(string str)
		{
			string result = str;
			if (str.Contains(".png"))
			{
				result = str.Replace(".png", string.Empty);
			}
			return result;
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x001F3B04 File Offset: 0x001F1D04
		public bool startDust(int dir, int x, int y)
		{
			if (GameCanvas.lowGraphic)
			{
				return false;
			}
			int num = (dir != 1) ? 1 : 0;
			if (this.dustState[num] != -1)
			{
				return false;
			}
			this.dustState[num] = 0;
			this.dustX[num] = x;
			this.dustY[num] = y;
			return true;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x001F3B4C File Offset: 0x001F1D4C
		public void loadWaterSplash()
		{
			if (!GameCanvas.lowGraphic)
			{
				GameCanvas.imgWS = new Image[3];
				for (int i = 0; i < 3; i++)
				{
					GameCanvas.imgWS[i] = GameCanvas.loadImage("/e/w" + i.ToString() + ".png");
				}
				GameCanvas.wsX = new int[2];
				GameCanvas.wsY = new int[2];
				GameCanvas.wsState = new int[2];
				GameCanvas.wsF = new int[2];
				GameCanvas.wsState[0] = (GameCanvas.wsState[1] = -1);
			}
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x001F3BD8 File Offset: 0x001F1DD8
		public void updateDust()
		{
			if (GameCanvas.lowGraphic)
			{
				return;
			}
			for (int i = 0; i < 2; i++)
			{
				if (this.dustState[i] != -1)
				{
					this.dustState[i]++;
					if (this.dustState[i] >= 5)
					{
						this.dustState[i] = -1;
					}
					if (i == 0)
					{
						this.dustX[i]--;
					}
					else
					{
						this.dustX[i]++;
					}
					this.dustY[i]--;
				}
			}
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x001F3C61 File Offset: 0x001F1E61
		public static bool isPaint(int x, int y)
		{
			return x >= GameScr.cmx && x <= GameScr.cmx + GameScr.gW && y >= GameScr.cmy && y <= GameScr.cmy + GameScr.gH + 30;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x001F3C9C File Offset: 0x001F1E9C
		public void loadDust()
		{
			if (GameCanvas.lowGraphic)
			{
				return;
			}
			if (GameCanvas.imgDust == null)
			{
				GameCanvas.imgDust = new Image[2][];
				for (int i = 0; i < GameCanvas.imgDust.Length; i++)
				{
					GameCanvas.imgDust[i] = new Image[5];
				}
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						GameCanvas.imgDust[j][k] = GameCanvas.loadImage("/e/d" + j.ToString() + k.ToString() + ".png");
					}
				}
			}
			this.dustX = new int[2];
			this.dustY = new int[2];
			this.dustState = new int[2];
			this.dustState[0] = (this.dustState[1] = -1);
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x001F3D60 File Offset: 0x001F1F60
		public static void paintShukiren(int x, int y, mGraphics g)
		{
			g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x001F3D93 File Offset: 0x001F1F93
		public void resetToLoginScrz()
		{
			this.resetToLoginScr = true;
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x001F3153 File Offset: 0x001F1353
		public static bool isPointer(int x, int y, int w, int h)
		{
			return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x001F3D9C File Offset: 0x001F1F9C
		public void perform(int idAction, object p)
		{
			if (idAction > 9999)
			{
				if (idAction <= 101025)
				{
					switch (idAction)
					{
					case 88810:
					{
						int playerMapId = (int)p;
						GameCanvas.endDlg();
						Service.gI().acceptInviteTrade(playerMapId);
						return;
					}
					case 88811:
						GameCanvas.endDlg();
						Service.gI().cancelInviteTrade();
						return;
					case 88812:
					case 88813:
					case 88815:
					case 88816:
					case 88830:
					case 88831:
					case 88832:
					case 88833:
					case 88834:
					case 88835:
					case 88838:
						return;
					case 88814:
					{
						Item[] items = (Item[])p;
						GameCanvas.endDlg();
						Service.gI().crystalCollectLock(items);
						return;
					}
					case 88817:
						ChatPopup.addChatPopup(string.Empty, 1, Char.myCharz().npcFocus);
						Service.gI().menu(Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
						return;
					case 88818:
					{
						short menuId3 = (short)p;
						Service.gI().textBoxId(menuId3, GameCanvas.inputDlg.tfInput.getText());
						GameCanvas.endDlg();
						return;
					}
					case 88819:
					{
						short menuId4 = (short)p;
						Service.gI().menuId(menuId4);
						return;
					}
					case 88820:
					{
						string[] array = (string[])p;
						if (Char.myCharz().npcFocus == null)
						{
							return;
						}
						int menuSelectedItem = GameCanvas.menu.menuSelectedItem;
						if (array.Length > 1)
						{
							MyVector myVector = new MyVector();
							for (int i = 0; i < array.Length - 1; i++)
							{
								myVector.addElement(new Command(array[i + 1], GameCanvas.instance, 88821, menuSelectedItem));
							}
							GameCanvas.menu.startAt(myVector, 3);
							return;
						}
						ChatPopup.addChatPopup(string.Empty, 1, Char.myCharz().npcFocus);
						Service.gI().menu(Char.myCharz().npcFocus.template.npcTemplateId, menuSelectedItem, 0);
						return;
					}
					case 88821:
					{
						int menuId5 = (int)p;
						ChatPopup.addChatPopup(string.Empty, 1, Char.myCharz().npcFocus);
						Service.gI().menu(Char.myCharz().npcFocus.template.npcTemplateId, menuId5, GameCanvas.menu.menuSelectedItem);
						return;
					}
					case 88822:
						ChatPopup.addChatPopup(string.Empty, 1, Char.myCharz().npcFocus);
						Service.gI().menu(Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
						return;
					case 88823:
						GameCanvas.startOKDlg(mResources.SENTMSG);
						return;
					case 88824:
						GameCanvas.startOKDlg(mResources.NOSENDMSG);
						return;
					case 88825:
						GameCanvas.startOKDlg(mResources.sendMsgSuccess, false);
						return;
					case 88826:
						GameCanvas.startOKDlg(mResources.cannotSendMsg, false);
						return;
					case 88827:
						GameCanvas.startOKDlg(mResources.sendGuessMsgSuccess);
						return;
					case 88828:
						GameCanvas.startOKDlg(mResources.sendMsgFail);
						return;
					case 88829:
					{
						string text4 = GameCanvas.inputDlg.tfInput.getText();
						if (!text4.Equals(string.Empty))
						{
							Service.gI().changeName(text4, (int)p);
							InfoDlg.showWait();
							return;
						}
						return;
					}
					case 88836:
						GameCanvas.inputDlg.tfInput.setMaxTextLenght(6);
						GameCanvas.inputDlg.show(mResources.INPUT_PRIVATE_PASS, new Command(mResources.ACCEPT, GameCanvas.instance, 888361, null), TField.INPUT_TYPE_NUMERIC);
						return;
					case 88837:
						break;
					case 88839:
						goto IL_5F8;
					default:
						switch (idAction)
						{
						case 101023:
							Main.numberQuit = 0;
							return;
						case 101024:
							GameCanvas.endDlg();
							return;
						case 101025:
							GameCanvas.endDlg();
							if (ServerListScreen.loadScreen)
							{
								GameCanvas.serverScreen.switchToMe();
								return;
							}
							GameCanvas.serverScreen.show2();
							return;
						default:
							return;
						}
						break;
					}
				}
				else if (idAction != 888361)
				{
					switch (idAction)
					{
					case 888391:
						goto IL_65E;
					case 888392:
						Service.gI().menu(4, GameCanvas.menu.menuSelectedItem, 0);
						return;
					case 888393:
						if (GameCanvas.loginScr == null)
						{
							GameCanvas.loginScr = new LoginScr();
						}
						GameCanvas.loginScr.doLogin();
						Main.closeKeyBoard();
						return;
					case 888394:
						GameCanvas.endDlg();
						return;
					case 888395:
						GameCanvas.endDlg();
						return;
					case 888396:
						GameCanvas.endDlg();
						return;
					case 888397:
					{
						string text8 = (string)p;
						return;
					}
					default:
						return;
					}
				}
				else
				{
					string text5 = GameCanvas.inputDlg.tfInput.getText();
					GameCanvas.endDlg();
					if (text5.Length < 6 || text5.Equals(string.Empty))
					{
						GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
						return;
					}
					try
					{
						Service.gI().activeAccProtect(int.Parse(text5));
						return;
					}
					catch (Exception ex3)
					{
						GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
						Cout.println("Loi tai 888361 Gamescavas " + ex3.ToString());
						return;
					}
				}
				string text6 = GameCanvas.inputDlg.tfInput.getText();
				GameCanvas.endDlg();
				try
				{
					Service.gI().openLockAccProtect(int.Parse(text6.Trim()));
					return;
				}
				catch (Exception ex4)
				{
					Cout.println("Loi tai 88837 " + ex4.ToString());
					return;
				}
				IL_5F8:
				string text7 = GameCanvas.inputDlg.tfInput.getText();
				GameCanvas.endDlg();
				if (text7.Length < 6 || text7.Equals(string.Empty))
				{
					GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
					return;
				}
				try
				{
					GameCanvas.startYesNoDlg(mResources.cancelAccountProtection, 888391, text7, 8882, null);
					return;
				}
				catch (Exception)
				{
					GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
					return;
				}
				IL_65E:
				string s = (string)p;
				GameCanvas.endDlg();
				Service.gI().clearAccProtect(int.Parse(s));
				return;
			}
			if (idAction <= 8889)
			{
				if (idAction == 999)
				{
					mSystem.closeBanner();
					GameCanvas.endDlg();
					return;
				}
				switch (idAction)
				{
				case 8881:
				{
					string url = (string)p;
					try
					{
						GameMidlet.instance.platformRequest(url);
					}
					catch (Exception)
					{
					}
					GameCanvas.currentDialog = null;
					return;
				}
				case 8882:
					InfoDlg.hide();
					GameCanvas.currentDialog = null;
					ServerListScreen.isAutoConect = false;
					ServerListScreen.countDieConnect = 0;
					return;
				case 8883:
					break;
				case 8884:
					GameCanvas.endDlg();
					GameCanvas.loginScr.switchToMe();
					return;
				case 8885:
					GameMidlet.instance.exit();
					return;
				case 8886:
				{
					GameCanvas.endDlg();
					string name = (string)p;
					Service.gI().addFriend(name);
					return;
				}
				case 8887:
				{
					GameCanvas.endDlg();
					int charId = (int)p;
					Service.gI().addPartyAccept(charId);
					return;
				}
				case 8888:
				{
					int charId2 = (int)p;
					Service.gI().addPartyCancel(charId2);
					GameCanvas.endDlg();
					return;
				}
				case 8889:
				{
					string str = (string)p;
					GameCanvas.endDlg();
					Service.gI().acceptPleaseParty(str);
					return;
				}
				default:
					return;
				}
			}
			else if (idAction != 9000)
			{
				if (idAction != 9999)
				{
					return;
				}
				GameCanvas.endDlg();
				GameCanvas.connect();
				Service.gI().setClientType();
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.doLogin();
				return;
			}
			else
			{
				GameCanvas.endDlg();
				ModFunc.imgLogoBig = null;
				SmallImage.loadBigRMS();
				mSystem.gcc();
				ServerListScreen.bigOk = true;
				ServerListScreen.loadScreen = true;
				GameScr.gI().loadGameScr();
				if (GameCanvas.currentScreen != GameCanvas.loginScr)
				{
					GameCanvas.serverScreen.switchToMe2();
					return;
				}
			}
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x001F44CC File Offset: 0x001F26CC
		public static void clearAllPointerEvent()
		{
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerDown = false;
			GameCanvas.isPointerJustDown = false;
			GameCanvas.isPointerJustRelease = false;
			GameScr.gI().lastSingleClick = 0L;
			GameScr.gI().isPointerDowning = false;
		}

		// Token: 0x04003BF9 RID: 15353
		public static bool isLoadRes = false;

		// Token: 0x04003BFA RID: 15354
		public static long timeNow = 0L;

		// Token: 0x04003BFB RID: 15355
		public static bool open3Hour;

		// Token: 0x04003BFC RID: 15356
		public static bool lowGraphic = false;

		// Token: 0x04003BFD RID: 15357
		public static bool serverchat = false;

		// Token: 0x04003BFE RID: 15358
		public static bool isMoveNumberPad = true;

		// Token: 0x04003BFF RID: 15359
		public static bool isLoading;

		// Token: 0x04003C00 RID: 15360
		public static bool isTouch = false;

		// Token: 0x04003C01 RID: 15361
		public static bool isTouchControl;

		// Token: 0x04003C02 RID: 15362
		public static bool isTouchControlSmallScreen;

		// Token: 0x04003C03 RID: 15363
		public static bool isTouchControlLargeScreen;

		// Token: 0x04003C04 RID: 15364
		public static bool isConnectFail;

		// Token: 0x04003C05 RID: 15365
		public static GameCanvas instance;

		// Token: 0x04003C06 RID: 15366
		public static bool bRun;

		// Token: 0x04003C07 RID: 15367
		public static bool[] keyPressed = new bool[30];

		// Token: 0x04003C08 RID: 15368
		public static bool[] keyReleased = new bool[30];

		// Token: 0x04003C09 RID: 15369
		public static bool[] keyHold = new bool[30];

		// Token: 0x04003C0A RID: 15370
		public static bool isPointerDown;

		// Token: 0x04003C0B RID: 15371
		public static bool isPointerClick;

		// Token: 0x04003C0C RID: 15372
		public static bool isPointerJustRelease;

		// Token: 0x04003C0D RID: 15373
		public static bool isPointerMove;

		// Token: 0x04003C0E RID: 15374
		public static int px;

		// Token: 0x04003C0F RID: 15375
		public static int py;

		// Token: 0x04003C10 RID: 15376
		public static int pxFirst;

		// Token: 0x04003C11 RID: 15377
		public static int pyFirst;

		// Token: 0x04003C12 RID: 15378
		public static int pxLast;

		// Token: 0x04003C13 RID: 15379
		public static int pyLast;

		// Token: 0x04003C14 RID: 15380
		public static int pxMouse;

		// Token: 0x04003C15 RID: 15381
		public static int pyMouse;

		// Token: 0x04003C16 RID: 15382
		public static Position[] arrPos = new Position[4];

		// Token: 0x04003C17 RID: 15383
		public static int gameTick;

		// Token: 0x04003C18 RID: 15384
		public static int taskTick;

		// Token: 0x04003C19 RID: 15385
		public static bool isEff1;

		// Token: 0x04003C1A RID: 15386
		public static bool isEff2;

		// Token: 0x04003C1B RID: 15387
		public static long timeTickEff1;

		// Token: 0x04003C1C RID: 15388
		public static long timeTickEff2;

		// Token: 0x04003C1D RID: 15389
		public static int w;

		// Token: 0x04003C1E RID: 15390
		public static int h;

		// Token: 0x04003C1F RID: 15391
		public static int hw;

		// Token: 0x04003C20 RID: 15392
		public static int hh;

		// Token: 0x04003C21 RID: 15393
		public static int wd3;

		// Token: 0x04003C22 RID: 15394
		public static int hd3;

		// Token: 0x04003C23 RID: 15395
		public static int w2d3;

		// Token: 0x04003C24 RID: 15396
		public static int h2d3;

		// Token: 0x04003C25 RID: 15397
		public static int w3d4;

		// Token: 0x04003C26 RID: 15398
		public static int h3d4;

		// Token: 0x04003C27 RID: 15399
		public static int wd6;

		// Token: 0x04003C28 RID: 15400
		public static int hd6;

		// Token: 0x04003C29 RID: 15401
		public static mScreen currentScreen;

		// Token: 0x04003C2A RID: 15402
		public static Menu menu = new Menu();

		// Token: 0x04003C2B RID: 15403
		public static Panel panel;

		// Token: 0x04003C2C RID: 15404
		public static Panel panel2;

		// Token: 0x04003C2D RID: 15405
		public static ChooseCharScr chooseCharScr;

		// Token: 0x04003C2E RID: 15406
		public static LoginScr loginScr;

		// Token: 0x04003C2F RID: 15407
		public static AddressScr adsScr;

		// Token: 0x04003C30 RID: 15408
		public static RegisterScreen registerScr;

		// Token: 0x04003C31 RID: 15409
		public static Dialog currentDialog;

		// Token: 0x04003C32 RID: 15410
		public static MsgDlg msgdlg;

		// Token: 0x04003C33 RID: 15411
		public static InputDlg inputDlg;

		// Token: 0x04003C34 RID: 15412
		public static MyVector currentPopup = new MyVector();

		// Token: 0x04003C35 RID: 15413
		public static int requestLoseCount;

		// Token: 0x04003C36 RID: 15414
		public static MyVector listPoint;

		// Token: 0x04003C37 RID: 15415
		public static Paint paintz;

		// Token: 0x04003C38 RID: 15416
		public static bool isGetResFromServer;

		// Token: 0x04003C39 RID: 15417
		public static Image[] imgBG;

		// Token: 0x04003C3A RID: 15418
		public static int skyColor;

		// Token: 0x04003C3B RID: 15419
		public static int curPos = 0;

		// Token: 0x04003C3C RID: 15420
		public static int[] bgW;

		// Token: 0x04003C3D RID: 15421
		public static int[] bgH;

		// Token: 0x04003C3E RID: 15422
		public static int planet = 0;

		// Token: 0x04003C3F RID: 15423
		private mGraphics g = new mGraphics();

		// Token: 0x04003C40 RID: 15424
		public static MyHashTable danhHieu = new MyHashTable();

		// Token: 0x04003C41 RID: 15425
		public static MyVector messageServer = new MyVector(string.Empty);

		// Token: 0x04003C42 RID: 15426
		public static bool isPlaySound = true;

		// Token: 0x04003C43 RID: 15427
		private static int clearOldData;

		// Token: 0x04003C44 RID: 15428
		public static int timeOpenKeyBoard;

		// Token: 0x04003C45 RID: 15429
		public static bool isFocusPanel2;

		// Token: 0x04003C46 RID: 15430
		public static int fps = 0;

		// Token: 0x04003C47 RID: 15431
		public static int max;

		// Token: 0x04003C48 RID: 15432
		public static int up;

		// Token: 0x04003C49 RID: 15433
		public static int upmax;

		// Token: 0x04003C4A RID: 15434
		private long timefps = mSystem.currentTimeMillis() + 1000L;

		// Token: 0x04003C4B RID: 15435
		private long timeup = mSystem.currentTimeMillis() + 1000L;

		// Token: 0x04003C4C RID: 15436
		private static int dir_ = -1;

		// Token: 0x04003C4D RID: 15437
		private int tickWaitThongBao;

		// Token: 0x04003C4E RID: 15438
		public bool isPaintCarret;

		// Token: 0x04003C4F RID: 15439
		public static MyVector debugUpdate;

		// Token: 0x04003C50 RID: 15440
		public static MyVector debugPaint;

		// Token: 0x04003C51 RID: 15441
		public static MyVector debugSession;

		// Token: 0x04003C52 RID: 15442
		private static bool isShowErrorForm = false;

		// Token: 0x04003C53 RID: 15443
		public static bool paintBG;

		// Token: 0x04003C54 RID: 15444
		public static int gsskyHeight;

		// Token: 0x04003C55 RID: 15445
		public static int gsgreenField1Y;

		// Token: 0x04003C56 RID: 15446
		public static int gsgreenField2Y;

		// Token: 0x04003C57 RID: 15447
		public static int gshouseY;

		// Token: 0x04003C58 RID: 15448
		public static int gsmountainY;

		// Token: 0x04003C59 RID: 15449
		public static int bgLayer0y;

		// Token: 0x04003C5A RID: 15450
		public static int bgLayer1y;

		// Token: 0x04003C5B RID: 15451
		public static Image imgCloud;

		// Token: 0x04003C5C RID: 15452
		public static Image imgSun;

		// Token: 0x04003C5D RID: 15453
		public static Image imgSun2;

		// Token: 0x04003C5E RID: 15454
		public static Image imgClear;

		// Token: 0x04003C5F RID: 15455
		public static Image[] imgBorder = new Image[3];

		// Token: 0x04003C60 RID: 15456
		public static Image[] imgSunSpec = new Image[3];

		// Token: 0x04003C61 RID: 15457
		public static int borderConnerW;

		// Token: 0x04003C62 RID: 15458
		public static int borderConnerH;

		// Token: 0x04003C63 RID: 15459
		public static int borderCenterW;

		// Token: 0x04003C64 RID: 15460
		public static int borderCenterH;

		// Token: 0x04003C65 RID: 15461
		public static int[] cloudX;

		// Token: 0x04003C66 RID: 15462
		public static int[] cloudY;

		// Token: 0x04003C67 RID: 15463
		public static int sunX;

		// Token: 0x04003C68 RID: 15464
		public static int sunY;

		// Token: 0x04003C69 RID: 15465
		public static int sunX2;

		// Token: 0x04003C6A RID: 15466
		public static int sunY2;

		// Token: 0x04003C6B RID: 15467
		public static int[] layerSpeed;

		// Token: 0x04003C6C RID: 15468
		public static int[] moveX;

		// Token: 0x04003C6D RID: 15469
		public static int[] moveXSpeed;

		// Token: 0x04003C6E RID: 15470
		public static bool isBoltEff;

		// Token: 0x04003C6F RID: 15471
		public static bool boltActive;

		// Token: 0x04003C70 RID: 15472
		public static int tBolt;

		// Token: 0x04003C71 RID: 15473
		public static Image imgBgIOS;

		// Token: 0x04003C72 RID: 15474
		public static int typeBg = -1;

		// Token: 0x04003C73 RID: 15475
		public static int transY;

		// Token: 0x04003C74 RID: 15476
		public static int[] yb = new int[5];

		// Token: 0x04003C75 RID: 15477
		public static int[] colorTop;

		// Token: 0x04003C76 RID: 15478
		public static int[] colorBotton;

		// Token: 0x04003C77 RID: 15479
		public static int yb1;

		// Token: 0x04003C78 RID: 15480
		public static int yb2;

		// Token: 0x04003C79 RID: 15481
		public static int yb3;

		// Token: 0x04003C7A RID: 15482
		public static int nBg = 0;

		// Token: 0x04003C7B RID: 15483
		public static int lastBg = -1;

		// Token: 0x04003C7C RID: 15484
		public static int[] bgRain = new int[]
		{
			1,
			4,
			11
		};

		// Token: 0x04003C7D RID: 15485
		public static int[] bgRainFont = new int[]
		{
			-1
		};

		// Token: 0x04003C7E RID: 15486
		public static Image imgCaycot;

		// Token: 0x04003C7F RID: 15487
		public static Image tam;

		// Token: 0x04003C80 RID: 15488
		public static int typeBackGround = -1;

		// Token: 0x04003C81 RID: 15489
		public static int saveIDBg = -10;

		// Token: 0x04003C82 RID: 15490
		public static bool isLoadBGok;

		// Token: 0x04003C83 RID: 15491
		private static long lastTimePress = 0L;

		// Token: 0x04003C84 RID: 15492
		public static int keyAsciiPress;

		// Token: 0x04003C85 RID: 15493
		public static int pXYScrollMouse;

		// Token: 0x04003C86 RID: 15494
		private static Image imgSignal;

		// Token: 0x04003C87 RID: 15495
		public static MyVector flyTexts = new MyVector();

		// Token: 0x04003C88 RID: 15496
		public int longTime;

		// Token: 0x04003C89 RID: 15497
		public static long timeBreakLoading;

		// Token: 0x04003C8A RID: 15498
		private static string thongBaoTest;

		// Token: 0x04003C8B RID: 15499
		public static int xThongBaoTranslate = GameCanvas.w - 60;

		// Token: 0x04003C8C RID: 15500
		public static bool isPointerJustDown = false;

		// Token: 0x04003C8D RID: 15501
		private int count = 1;

		// Token: 0x04003C8E RID: 15502
		public static bool csWait;

		// Token: 0x04003C8F RID: 15503
		public static MyRandom r = new MyRandom();

		// Token: 0x04003C90 RID: 15504
		public static bool isBlackScreen;

		// Token: 0x04003C91 RID: 15505
		public static int[] bgSpeed;

		// Token: 0x04003C92 RID: 15506
		public static int cmdBarX;

		// Token: 0x04003C93 RID: 15507
		public static int cmdBarY;

		// Token: 0x04003C94 RID: 15508
		public static int cmdBarW;

		// Token: 0x04003C95 RID: 15509
		public static int cmdBarH;

		// Token: 0x04003C96 RID: 15510
		public static int cmdBarLeftW;

		// Token: 0x04003C97 RID: 15511
		public static int cmdBarRightW;

		// Token: 0x04003C98 RID: 15512
		public static int cmdBarCenterW;

		// Token: 0x04003C99 RID: 15513
		public static int hpBarX;

		// Token: 0x04003C9A RID: 15514
		public static int hpBarY;

		// Token: 0x04003C9B RID: 15515
		public static int hpBarW;

		// Token: 0x04003C9C RID: 15516
		public static int expBarW;

		// Token: 0x04003C9D RID: 15517
		public static int lvPosX;

		// Token: 0x04003C9E RID: 15518
		public static int moneyPosX;

		// Token: 0x04003C9F RID: 15519
		public static int hpBarH;

		// Token: 0x04003CA0 RID: 15520
		public static int girlHPBarY;

		// Token: 0x04003CA1 RID: 15521
		public int timeOut;

		// Token: 0x04003CA2 RID: 15522
		public int[] dustX;

		// Token: 0x04003CA3 RID: 15523
		public int[] dustY;

		// Token: 0x04003CA4 RID: 15524
		public int[] dustState;

		// Token: 0x04003CA5 RID: 15525
		public static int[] wsX;

		// Token: 0x04003CA6 RID: 15526
		public static int[] wsY;

		// Token: 0x04003CA7 RID: 15527
		public static int[] wsState;

		// Token: 0x04003CA8 RID: 15528
		public static int[] wsF;

		// Token: 0x04003CA9 RID: 15529
		public static Image[] imgWS;

		// Token: 0x04003CAA RID: 15530
		public static Image imgShuriken;

		// Token: 0x04003CAB RID: 15531
		public static Image[][] imgDust;

		// Token: 0x04003CAC RID: 15532
		public static bool isResume;

		// Token: 0x04003CAD RID: 15533
		public static ServerListScreen serverScreen;

		// Token: 0x04003CAE RID: 15534
		public static ServerScr serverScr;

		// Token: 0x04003CAF RID: 15535
		public bool resetToLoginScr;

		// Token: 0x04003CB0 RID: 15536
		public static long TIMEOUT;

		// Token: 0x04003CB1 RID: 15537
		public static int timeLoading = 15;

		// Token: 0x04003CB2 RID: 15538
		public static string acc;

		// Token: 0x04003CB3 RID: 15539
		public static string pass;

		// Token: 0x04003CB4 RID: 15540
		public static int server = -1;

		// Token: 0x04003CB5 RID: 15541
		private static long time;
	}
}
