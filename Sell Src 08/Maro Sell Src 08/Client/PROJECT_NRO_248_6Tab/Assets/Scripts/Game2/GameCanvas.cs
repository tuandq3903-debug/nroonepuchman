using System;
using Game2.Assets.src.g;
using UnityEngine;

namespace Game2
{
	// Token: 0x0200039D RID: 925
	public class GameCanvas : IActionListener
	{
		// Token: 0x060028E9 RID: 10473 RVA: 0x00284140 File Offset: 0x00282340
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

		// Token: 0x060028EA RID: 10474 RVA: 0x0002FDFA File Offset: 0x0002DFFA
		public static string getPlatformName()
		{
			return "Pc platform xxx";
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x002841F4 File Offset: 0x002823F4
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

		// Token: 0x060028EC RID: 10476 RVA: 0x002844DD File Offset: 0x002826DD
		public static GameCanvas gI()
		{
			return GameCanvas.instance;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x002844E4 File Offset: 0x002826E4
		public void initPaint()
		{
			GameCanvas.paintz = new Paint();
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x002844F0 File Offset: 0x002826F0
		public static void closeKeyBoard()
		{
			mGraphics.addYWhenOpenKeyBoard = 0;
			GameCanvas.timeOpenKeyBoard = 0;
			Main.closeKeyBoard();
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x00284504 File Offset: 0x00282704
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

		// Token: 0x060028F0 RID: 10480 RVA: 0x00284BB0 File Offset: 0x00282DB0
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

		// Token: 0x060028F1 RID: 10481 RVA: 0x00284C14 File Offset: 0x00282E14
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

		// Token: 0x060028F2 RID: 10482 RVA: 0x00284CD0 File Offset: 0x00282ED0
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

		// Token: 0x060028F3 RID: 10483 RVA: 0x00284E40 File Offset: 0x00283040
		public static bool isWaiting()
		{
			return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || Char.isLoadingMap || LoginScr.isContinueToLogin;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x00284E77 File Offset: 0x00283077
		public static void connect()
		{
			if (!Session_ME.gI().isConnected())
			{
				Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x00284E9C File Offset: 0x0028309C
		public static void connect2()
		{
			if (!Session_ME2.gI().isConnected())
			{
				Res.outz("IP2= " + GameMidlet.IP2 + " PORT 2= " + GameMidlet.PORT2.ToString());
				Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x00284EEC File Offset: 0x002830EC
		public static void resetTrans(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x00284F14 File Offset: 0x00283114
		public static void resetTransGameScr(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.translate(0, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.translate(-GameScr.cmx, -GameScr.cmy);
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000034B9 File Offset: 0x000016B9
		public void start()
		{
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void debug(string s, int type)
		{
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x00284F64 File Offset: 0x00283164
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

		// Token: 0x060028FB RID: 10491 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void paintCloud(mGraphics g)
		{
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void updateBG()
		{
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x00285128 File Offset: 0x00283328
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

		// Token: 0x060028FE RID: 10494 RVA: 0x0028517C File Offset: 0x0028337C
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

		// Token: 0x060028FF RID: 10495 RVA: 0x00285598 File Offset: 0x00283798
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

		// Token: 0x06002900 RID: 10496 RVA: 0x0028562B File Offset: 0x0028382B
		public static void drawSun2(mGraphics g)
		{
			if (GameCanvas.imgSun2 != null)
			{
				g.drawImage(GameCanvas.imgSun2, GameCanvas.sunX2, GameCanvas.sunY2, 0);
			}
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0028564A File Offset: 0x0028384A
		public static bool isHDVersion()
		{
			return mGraphics.zoomLevel > 1;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x00285658 File Offset: 0x00283858
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

		// Token: 0x06002903 RID: 10499 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void resetBg()
		{
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x00286080 File Offset: 0x00284280
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

		// Token: 0x06002905 RID: 10501 RVA: 0x00286748 File Offset: 0x00284948
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

		// Token: 0x06002906 RID: 10502 RVA: 0x00287788 File Offset: 0x00285988
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

		// Token: 0x06002907 RID: 10503 RVA: 0x002877C4 File Offset: 0x002859C4
		public void keyPressedz(int keyCode)
		{
			GameCanvas.lastTimePress = mSystem.currentTimeMillis();
			if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32 || keyCode == 31)
			{
				GameCanvas.keyAsciiPress = keyCode;
			}
			this.mapKeyPress(keyCode);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x00287814 File Offset: 0x00285A14
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

		// Token: 0x06002909 RID: 10505 RVA: 0x00287D0C File Offset: 0x00285F0C
		public void keyReleasedz(int keyCode)
		{
			GameCanvas.keyAsciiPress = 0;
			this.mapKeyRelease(keyCode);
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x00287D1C File Offset: 0x00285F1C
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

		// Token: 0x0600290B RID: 10507 RVA: 0x002880C9 File Offset: 0x002862C9
		public void pointerMouse(int x, int y)
		{
			GameCanvas.pxMouse = x;
			GameCanvas.pyMouse = y;
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x002880D7 File Offset: 0x002862D7
		public void scrollMouse(int a)
		{
			GameCanvas.pXYScrollMouse = a;
			if (GameCanvas.panel != null && GameCanvas.panel.isShow)
			{
				GameCanvas.panel.updateScroolMouse(a);
			}
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00288100 File Offset: 0x00286300
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

		// Token: 0x0600290E RID: 10510 RVA: 0x00288178 File Offset: 0x00286378
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

		// Token: 0x0600290F RID: 10511 RVA: 0x002881D1 File Offset: 0x002863D1
		public void pointerReleased(int x, int y)
		{
			GameCanvas.isPointerDown = false;
			GameCanvas.isPointerJustRelease = true;
			GameCanvas.isPointerMove = false;
			mScreen.keyTouch = -1;
			GameCanvas.px = x;
			GameCanvas.py = y;
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x002881F7 File Offset: 0x002863F7
		public static bool isPointerHoldIn(int x, int y, int w, int h)
		{
			return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x00288230 File Offset: 0x00286430
		public static bool isMouseFocus(int x, int y, int w, int h)
		{
			return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0028825C File Offset: 0x0028645C
		public static void clearKeyPressed()
		{
			for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
			{
				GameCanvas.keyPressed[i] = false;
			}
			GameCanvas.isPointerJustRelease = false;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0028828C File Offset: 0x0028648C
		public static void clearKeyHold()
		{
			for (int i = 0; i < GameCanvas.keyHold.Length; i++)
			{
				GameCanvas.keyHold[i] = false;
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x002882B4 File Offset: 0x002864B4
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

		// Token: 0x06002915 RID: 10517 RVA: 0x00288360 File Offset: 0x00286560
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

		// Token: 0x06002916 RID: 10518 RVA: 0x002887A8 File Offset: 0x002869A8
		public static void endDlg()
		{
			if (GameCanvas.inputDlg != null)
			{
				GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
			}
			GameCanvas.currentDialog = null;
			InfoDlg.hide();
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x002887D0 File Offset: 0x002869D0
		public static void startOKDlg(string info)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x00288803 File Offset: 0x00286A03
		public static void startWaitDlg(string info)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
			GameCanvas.msgdlg.isWait = true;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x00288803 File Offset: 0x00286A03
		public static void startOKDlg(string info, bool isError)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
			GameCanvas.msgdlg.isWait = true;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00288841 File Offset: 0x00286A41
		public static void startWaitDlg()
		{
			GameCanvas.closeKeyBoard();
			Char.isLoadingMap = true;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x0028884E File Offset: 0x00286A4E
		public static void startOK(string info, int actionID, object p)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
			GameCanvas.msgdlg.show();
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x00288880 File Offset: 0x00286A80
		public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
			GameCanvas.msgdlg.show();
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x002888DB File Offset: 0x00286ADB
		public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
			GameCanvas.msgdlg.show();
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x002888FA File Offset: 0x00286AFA
		public static void startserverThongBao(string msgSv)
		{
			GameCanvas.thongBaoTest = msgSv;
			GameCanvas.xThongBaoTranslate = GameCanvas.w - 60;
			GameCanvas.dir_ = -1;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x00006D67 File Offset: 0x00004F67
		public static bool isGetResourceFromServer()
		{
			return true;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x00288918 File Offset: 0x00286B18
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

		// Token: 0x06002921 RID: 10529 RVA: 0x002889CC File Offset: 0x00286BCC
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

		// Token: 0x06002922 RID: 10530 RVA: 0x00288A10 File Offset: 0x00286C10
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

		// Token: 0x06002923 RID: 10531 RVA: 0x00288A68 File Offset: 0x00286C68
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

		// Token: 0x06002924 RID: 10532 RVA: 0x00288ABC File Offset: 0x00286CBC
		public static Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
		{
			RenderTexture rt = RenderTexture.active = new RenderTexture(targetX, targetY, 24);
			Graphics.Blit(texture2D, rt);
			Texture2D texture2D2 = new Texture2D(targetX, targetY);
			texture2D2.ReadPixels(new Rect(0f, 0f, (float)targetX, (float)targetY), 0, 0);
			texture2D2.Apply();
			return texture2D2;
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x00288B08 File Offset: 0x00286D08
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

		// Token: 0x06002926 RID: 10534 RVA: 0x00288B78 File Offset: 0x00286D78
		public static string cutPng(string str)
		{
			string result = str;
			if (str.Contains(".png"))
			{
				result = str.Replace(".png", string.Empty);
			}
			return result;
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x00288BA8 File Offset: 0x00286DA8
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

		// Token: 0x06002928 RID: 10536 RVA: 0x00288BF0 File Offset: 0x00286DF0
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

		// Token: 0x06002929 RID: 10537 RVA: 0x00288C7C File Offset: 0x00286E7C
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

		// Token: 0x0600292A RID: 10538 RVA: 0x00288D05 File Offset: 0x00286F05
		public static bool isPaint(int x, int y)
		{
			return x >= GameScr.cmx && x <= GameScr.cmx + GameScr.gW && y >= GameScr.cmy && y <= GameScr.cmy + GameScr.gH + 30;
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00288D40 File Offset: 0x00286F40
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

		// Token: 0x0600292C RID: 10540 RVA: 0x00288E04 File Offset: 0x00287004
		public static void paintShukiren(int x, int y, mGraphics g)
		{
			g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x00288E37 File Offset: 0x00287037
		public void resetToLoginScrz()
		{
			this.resetToLoginScr = true;
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x002881F7 File Offset: 0x002863F7
		public static bool isPointer(int x, int y, int w, int h)
		{
			return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x00288E40 File Offset: 0x00287040
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

		// Token: 0x06002930 RID: 10544 RVA: 0x00289570 File Offset: 0x00287770
		public static void clearAllPointerEvent()
		{
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerDown = false;
			GameCanvas.isPointerJustDown = false;
			GameCanvas.isPointerJustRelease = false;
			GameScr.gI().lastSingleClick = 0L;
			GameScr.gI().isPointerDowning = false;
		}

		// Token: 0x04004E78 RID: 20088
		public static bool isLoadRes = false;

		// Token: 0x04004E79 RID: 20089
		public static long timeNow = 0L;

		// Token: 0x04004E7A RID: 20090
		public static bool open3Hour;

		// Token: 0x04004E7B RID: 20091
		public static bool lowGraphic = false;

		// Token: 0x04004E7C RID: 20092
		public static bool serverchat = false;

		// Token: 0x04004E7D RID: 20093
		public static bool isMoveNumberPad = true;

		// Token: 0x04004E7E RID: 20094
		public static bool isLoading;

		// Token: 0x04004E7F RID: 20095
		public static bool isTouch = false;

		// Token: 0x04004E80 RID: 20096
		public static bool isTouchControl;

		// Token: 0x04004E81 RID: 20097
		public static bool isTouchControlSmallScreen;

		// Token: 0x04004E82 RID: 20098
		public static bool isTouchControlLargeScreen;

		// Token: 0x04004E83 RID: 20099
		public static bool isConnectFail;

		// Token: 0x04004E84 RID: 20100
		public static GameCanvas instance;

		// Token: 0x04004E85 RID: 20101
		public static bool bRun;

		// Token: 0x04004E86 RID: 20102
		public static bool[] keyPressed = new bool[30];

		// Token: 0x04004E87 RID: 20103
		public static bool[] keyReleased = new bool[30];

		// Token: 0x04004E88 RID: 20104
		public static bool[] keyHold = new bool[30];

		// Token: 0x04004E89 RID: 20105
		public static bool isPointerDown;

		// Token: 0x04004E8A RID: 20106
		public static bool isPointerClick;

		// Token: 0x04004E8B RID: 20107
		public static bool isPointerJustRelease;

		// Token: 0x04004E8C RID: 20108
		public static bool isPointerMove;

		// Token: 0x04004E8D RID: 20109
		public static int px;

		// Token: 0x04004E8E RID: 20110
		public static int py;

		// Token: 0x04004E8F RID: 20111
		public static int pxFirst;

		// Token: 0x04004E90 RID: 20112
		public static int pyFirst;

		// Token: 0x04004E91 RID: 20113
		public static int pxLast;

		// Token: 0x04004E92 RID: 20114
		public static int pyLast;

		// Token: 0x04004E93 RID: 20115
		public static int pxMouse;

		// Token: 0x04004E94 RID: 20116
		public static int pyMouse;

		// Token: 0x04004E95 RID: 20117
		public static Position[] arrPos = new Position[4];

		// Token: 0x04004E96 RID: 20118
		public static int gameTick;

		// Token: 0x04004E97 RID: 20119
		public static int taskTick;

		// Token: 0x04004E98 RID: 20120
		public static bool isEff1;

		// Token: 0x04004E99 RID: 20121
		public static bool isEff2;

		// Token: 0x04004E9A RID: 20122
		public static long timeTickEff1;

		// Token: 0x04004E9B RID: 20123
		public static long timeTickEff2;

		// Token: 0x04004E9C RID: 20124
		public static int w;

		// Token: 0x04004E9D RID: 20125
		public static int h;

		// Token: 0x04004E9E RID: 20126
		public static int hw;

		// Token: 0x04004E9F RID: 20127
		public static int hh;

		// Token: 0x04004EA0 RID: 20128
		public static int wd3;

		// Token: 0x04004EA1 RID: 20129
		public static int hd3;

		// Token: 0x04004EA2 RID: 20130
		public static int w2d3;

		// Token: 0x04004EA3 RID: 20131
		public static int h2d3;

		// Token: 0x04004EA4 RID: 20132
		public static int w3d4;

		// Token: 0x04004EA5 RID: 20133
		public static int h3d4;

		// Token: 0x04004EA6 RID: 20134
		public static int wd6;

		// Token: 0x04004EA7 RID: 20135
		public static int hd6;

		// Token: 0x04004EA8 RID: 20136
		public static mScreen currentScreen;

		// Token: 0x04004EA9 RID: 20137
		public static Menu menu = new Menu();

		// Token: 0x04004EAA RID: 20138
		public static Panel panel;

		// Token: 0x04004EAB RID: 20139
		public static Panel panel2;

		// Token: 0x04004EAC RID: 20140
		public static ChooseCharScr chooseCharScr;

		// Token: 0x04004EAD RID: 20141
		public static LoginScr loginScr;

		// Token: 0x04004EAE RID: 20142
		public static AddressScr adsScr;

		// Token: 0x04004EAF RID: 20143
		public static RegisterScreen registerScr;

		// Token: 0x04004EB0 RID: 20144
		public static Dialog currentDialog;

		// Token: 0x04004EB1 RID: 20145
		public static MsgDlg msgdlg;

		// Token: 0x04004EB2 RID: 20146
		public static InputDlg inputDlg;

		// Token: 0x04004EB3 RID: 20147
		public static MyVector currentPopup = new MyVector();

		// Token: 0x04004EB4 RID: 20148
		public static int requestLoseCount;

		// Token: 0x04004EB5 RID: 20149
		public static MyVector listPoint;

		// Token: 0x04004EB6 RID: 20150
		public static Paint paintz;

		// Token: 0x04004EB7 RID: 20151
		public static bool isGetResFromServer;

		// Token: 0x04004EB8 RID: 20152
		public static Image[] imgBG;

		// Token: 0x04004EB9 RID: 20153
		public static int skyColor;

		// Token: 0x04004EBA RID: 20154
		public static int curPos = 0;

		// Token: 0x04004EBB RID: 20155
		public static int[] bgW;

		// Token: 0x04004EBC RID: 20156
		public static int[] bgH;

		// Token: 0x04004EBD RID: 20157
		public static int planet = 0;

		// Token: 0x04004EBE RID: 20158
		private mGraphics g = new mGraphics();

		// Token: 0x04004EBF RID: 20159
		public static MyHashTable danhHieu = new MyHashTable();

		// Token: 0x04004EC0 RID: 20160
		public static MyVector messageServer = new MyVector(string.Empty);

		// Token: 0x04004EC1 RID: 20161
		public static bool isPlaySound = true;

		// Token: 0x04004EC2 RID: 20162
		private static int clearOldData;

		// Token: 0x04004EC3 RID: 20163
		public static int timeOpenKeyBoard;

		// Token: 0x04004EC4 RID: 20164
		public static bool isFocusPanel2;

		// Token: 0x04004EC5 RID: 20165
		public static int fps = 0;

		// Token: 0x04004EC6 RID: 20166
		public static int max;

		// Token: 0x04004EC7 RID: 20167
		public static int up;

		// Token: 0x04004EC8 RID: 20168
		public static int upmax;

		// Token: 0x04004EC9 RID: 20169
		private long timefps = mSystem.currentTimeMillis() + 1000L;

		// Token: 0x04004ECA RID: 20170
		private long timeup = mSystem.currentTimeMillis() + 1000L;

		// Token: 0x04004ECB RID: 20171
		private static int dir_ = -1;

		// Token: 0x04004ECC RID: 20172
		private int tickWaitThongBao;

		// Token: 0x04004ECD RID: 20173
		public bool isPaintCarret;

		// Token: 0x04004ECE RID: 20174
		public static MyVector debugUpdate;

		// Token: 0x04004ECF RID: 20175
		public static MyVector debugPaint;

		// Token: 0x04004ED0 RID: 20176
		public static MyVector debugSession;

		// Token: 0x04004ED1 RID: 20177
		private static bool isShowErrorForm = false;

		// Token: 0x04004ED2 RID: 20178
		public static bool paintBG;

		// Token: 0x04004ED3 RID: 20179
		public static int gsskyHeight;

		// Token: 0x04004ED4 RID: 20180
		public static int gsgreenField1Y;

		// Token: 0x04004ED5 RID: 20181
		public static int gsgreenField2Y;

		// Token: 0x04004ED6 RID: 20182
		public static int gshouseY;

		// Token: 0x04004ED7 RID: 20183
		public static int gsmountainY;

		// Token: 0x04004ED8 RID: 20184
		public static int bgLayer0y;

		// Token: 0x04004ED9 RID: 20185
		public static int bgLayer1y;

		// Token: 0x04004EDA RID: 20186
		public static Image imgCloud;

		// Token: 0x04004EDB RID: 20187
		public static Image imgSun;

		// Token: 0x04004EDC RID: 20188
		public static Image imgSun2;

		// Token: 0x04004EDD RID: 20189
		public static Image imgClear;

		// Token: 0x04004EDE RID: 20190
		public static Image[] imgBorder = new Image[3];

		// Token: 0x04004EDF RID: 20191
		public static Image[] imgSunSpec = new Image[3];

		// Token: 0x04004EE0 RID: 20192
		public static int borderConnerW;

		// Token: 0x04004EE1 RID: 20193
		public static int borderConnerH;

		// Token: 0x04004EE2 RID: 20194
		public static int borderCenterW;

		// Token: 0x04004EE3 RID: 20195
		public static int borderCenterH;

		// Token: 0x04004EE4 RID: 20196
		public static int[] cloudX;

		// Token: 0x04004EE5 RID: 20197
		public static int[] cloudY;

		// Token: 0x04004EE6 RID: 20198
		public static int sunX;

		// Token: 0x04004EE7 RID: 20199
		public static int sunY;

		// Token: 0x04004EE8 RID: 20200
		public static int sunX2;

		// Token: 0x04004EE9 RID: 20201
		public static int sunY2;

		// Token: 0x04004EEA RID: 20202
		public static int[] layerSpeed;

		// Token: 0x04004EEB RID: 20203
		public static int[] moveX;

		// Token: 0x04004EEC RID: 20204
		public static int[] moveXSpeed;

		// Token: 0x04004EED RID: 20205
		public static bool isBoltEff;

		// Token: 0x04004EEE RID: 20206
		public static bool boltActive;

		// Token: 0x04004EEF RID: 20207
		public static int tBolt;

		// Token: 0x04004EF0 RID: 20208
		public static Image imgBgIOS;

		// Token: 0x04004EF1 RID: 20209
		public static int typeBg = -1;

		// Token: 0x04004EF2 RID: 20210
		public static int transY;

		// Token: 0x04004EF3 RID: 20211
		public static int[] yb = new int[5];

		// Token: 0x04004EF4 RID: 20212
		public static int[] colorTop;

		// Token: 0x04004EF5 RID: 20213
		public static int[] colorBotton;

		// Token: 0x04004EF6 RID: 20214
		public static int yb1;

		// Token: 0x04004EF7 RID: 20215
		public static int yb2;

		// Token: 0x04004EF8 RID: 20216
		public static int yb3;

		// Token: 0x04004EF9 RID: 20217
		public static int nBg = 0;

		// Token: 0x04004EFA RID: 20218
		public static int lastBg = -1;

		// Token: 0x04004EFB RID: 20219
		public static int[] bgRain = new int[]
		{
			1,
			4,
			11
		};

		// Token: 0x04004EFC RID: 20220
		public static int[] bgRainFont = new int[]
		{
			-1
		};

		// Token: 0x04004EFD RID: 20221
		public static Image imgCaycot;

		// Token: 0x04004EFE RID: 20222
		public static Image tam;

		// Token: 0x04004EFF RID: 20223
		public static int typeBackGround = -1;

		// Token: 0x04004F00 RID: 20224
		public static int saveIDBg = -10;

		// Token: 0x04004F01 RID: 20225
		public static bool isLoadBGok;

		// Token: 0x04004F02 RID: 20226
		private static long lastTimePress = 0L;

		// Token: 0x04004F03 RID: 20227
		public static int keyAsciiPress;

		// Token: 0x04004F04 RID: 20228
		public static int pXYScrollMouse;

		// Token: 0x04004F05 RID: 20229
		private static Image imgSignal;

		// Token: 0x04004F06 RID: 20230
		public static MyVector flyTexts = new MyVector();

		// Token: 0x04004F07 RID: 20231
		public int longTime;

		// Token: 0x04004F08 RID: 20232
		public static long timeBreakLoading;

		// Token: 0x04004F09 RID: 20233
		private static string thongBaoTest;

		// Token: 0x04004F0A RID: 20234
		public static int xThongBaoTranslate = GameCanvas.w - 60;

		// Token: 0x04004F0B RID: 20235
		public static bool isPointerJustDown = false;

		// Token: 0x04004F0C RID: 20236
		private int count = 1;

		// Token: 0x04004F0D RID: 20237
		public static bool csWait;

		// Token: 0x04004F0E RID: 20238
		public static MyRandom r = new MyRandom();

		// Token: 0x04004F0F RID: 20239
		public static bool isBlackScreen;

		// Token: 0x04004F10 RID: 20240
		public static int[] bgSpeed;

		// Token: 0x04004F11 RID: 20241
		public static int cmdBarX;

		// Token: 0x04004F12 RID: 20242
		public static int cmdBarY;

		// Token: 0x04004F13 RID: 20243
		public static int cmdBarW;

		// Token: 0x04004F14 RID: 20244
		public static int cmdBarH;

		// Token: 0x04004F15 RID: 20245
		public static int cmdBarLeftW;

		// Token: 0x04004F16 RID: 20246
		public static int cmdBarRightW;

		// Token: 0x04004F17 RID: 20247
		public static int cmdBarCenterW;

		// Token: 0x04004F18 RID: 20248
		public static int hpBarX;

		// Token: 0x04004F19 RID: 20249
		public static int hpBarY;

		// Token: 0x04004F1A RID: 20250
		public static int hpBarW;

		// Token: 0x04004F1B RID: 20251
		public static int expBarW;

		// Token: 0x04004F1C RID: 20252
		public static int lvPosX;

		// Token: 0x04004F1D RID: 20253
		public static int moneyPosX;

		// Token: 0x04004F1E RID: 20254
		public static int hpBarH;

		// Token: 0x04004F1F RID: 20255
		public static int girlHPBarY;

		// Token: 0x04004F20 RID: 20256
		public int timeOut;

		// Token: 0x04004F21 RID: 20257
		public int[] dustX;

		// Token: 0x04004F22 RID: 20258
		public int[] dustY;

		// Token: 0x04004F23 RID: 20259
		public int[] dustState;

		// Token: 0x04004F24 RID: 20260
		public static int[] wsX;

		// Token: 0x04004F25 RID: 20261
		public static int[] wsY;

		// Token: 0x04004F26 RID: 20262
		public static int[] wsState;

		// Token: 0x04004F27 RID: 20263
		public static int[] wsF;

		// Token: 0x04004F28 RID: 20264
		public static Image[] imgWS;

		// Token: 0x04004F29 RID: 20265
		public static Image imgShuriken;

		// Token: 0x04004F2A RID: 20266
		public static Image[][] imgDust;

		// Token: 0x04004F2B RID: 20267
		public static bool isResume;

		// Token: 0x04004F2C RID: 20268
		public static ServerListScreen serverScreen;

		// Token: 0x04004F2D RID: 20269
		public static ServerScr serverScr;

		// Token: 0x04004F2E RID: 20270
		public bool resetToLoginScr;

		// Token: 0x04004F2F RID: 20271
		public static long TIMEOUT;

		// Token: 0x04004F30 RID: 20272
		public static int timeLoading = 15;

		// Token: 0x04004F31 RID: 20273
		public static string acc;

		// Token: 0x04004F32 RID: 20274
		public static string pass;

		// Token: 0x04004F33 RID: 20275
		public static int server = -1;

		// Token: 0x04004F34 RID: 20276
		private static long time;
	}
}
