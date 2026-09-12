using System;
using Game6.Assets.src.g;
using UnityEngine;

namespace Game6
{
	// Token: 0x0200003D RID: 61
	public class GameCanvas : IActionListener
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0002FD48 File Offset: 0x0002DF48
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

		// Token: 0x0600025A RID: 602 RVA: 0x0002FDFA File Offset: 0x0002DFFA
		public static string getPlatformName()
		{
			return "Pc platform xxx";
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0002FE04 File Offset: 0x0002E004
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

		// Token: 0x0600025C RID: 604 RVA: 0x000300ED File Offset: 0x0002E2ED
		public static GameCanvas gI()
		{
			return GameCanvas.instance;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000300F4 File Offset: 0x0002E2F4
		public void initPaint()
		{
			GameCanvas.paintz = new Paint();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00030100 File Offset: 0x0002E300
		public static void closeKeyBoard()
		{
			mGraphics.addYWhenOpenKeyBoard = 0;
			GameCanvas.timeOpenKeyBoard = 0;
			Main.closeKeyBoard();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00030114 File Offset: 0x0002E314
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

		// Token: 0x06000260 RID: 608 RVA: 0x000307C0 File Offset: 0x0002E9C0
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

		// Token: 0x06000261 RID: 609 RVA: 0x00030824 File Offset: 0x0002EA24
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

		// Token: 0x06000262 RID: 610 RVA: 0x000308E0 File Offset: 0x0002EAE0
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

		// Token: 0x06000263 RID: 611 RVA: 0x00030A50 File Offset: 0x0002EC50
		public static bool isWaiting()
		{
			return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || Char.isLoadingMap || LoginScr.isContinueToLogin;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00030A87 File Offset: 0x0002EC87
		public static void connect()
		{
			if (!Session_ME.gI().isConnected())
			{
				Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00030AAC File Offset: 0x0002ECAC
		public static void connect2()
		{
			if (!Session_ME2.gI().isConnected())
			{
				Res.outz("IP2= " + GameMidlet.IP2 + " PORT 2= " + GameMidlet.PORT2.ToString());
				Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00030AFC File Offset: 0x0002ECFC
		public static void resetTrans(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00030B24 File Offset: 0x0002ED24
		public static void resetTransGameScr(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.translate(0, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.translate(-GameScr.cmx, -GameScr.cmy);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000034B9 File Offset: 0x000016B9
		public void start()
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void debug(string s, int type)
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00030B74 File Offset: 0x0002ED74
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

		// Token: 0x0600026B RID: 619 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void paintCloud(mGraphics g)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void updateBG()
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00030D38 File Offset: 0x0002EF38
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

		// Token: 0x0600026E RID: 622 RVA: 0x00030D8C File Offset: 0x0002EF8C
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

		// Token: 0x0600026F RID: 623 RVA: 0x000311A8 File Offset: 0x0002F3A8
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

		// Token: 0x06000270 RID: 624 RVA: 0x0003123B File Offset: 0x0002F43B
		public static void drawSun2(mGraphics g)
		{
			if (GameCanvas.imgSun2 != null)
			{
				g.drawImage(GameCanvas.imgSun2, GameCanvas.sunX2, GameCanvas.sunY2, 0);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0003125A File Offset: 0x0002F45A
		public static bool isHDVersion()
		{
			return mGraphics.zoomLevel > 1;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00031268 File Offset: 0x0002F468
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

		// Token: 0x06000273 RID: 627 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void resetBg()
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00031C90 File Offset: 0x0002FE90
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

		// Token: 0x06000275 RID: 629 RVA: 0x00032358 File Offset: 0x00030558
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

		// Token: 0x06000276 RID: 630 RVA: 0x00033398 File Offset: 0x00031598
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

		// Token: 0x06000277 RID: 631 RVA: 0x000333D4 File Offset: 0x000315D4
		public void keyPressedz(int keyCode)
		{
			GameCanvas.lastTimePress = mSystem.currentTimeMillis();
			if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32 || keyCode == 31)
			{
				GameCanvas.keyAsciiPress = keyCode;
			}
			this.mapKeyPress(keyCode);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00033424 File Offset: 0x00031624
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

		// Token: 0x06000279 RID: 633 RVA: 0x0003391C File Offset: 0x00031B1C
		public void keyReleasedz(int keyCode)
		{
			GameCanvas.keyAsciiPress = 0;
			this.mapKeyRelease(keyCode);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0003392C File Offset: 0x00031B2C
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

		// Token: 0x0600027B RID: 635 RVA: 0x00033CD9 File Offset: 0x00031ED9
		public void pointerMouse(int x, int y)
		{
			GameCanvas.pxMouse = x;
			GameCanvas.pyMouse = y;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00033CE7 File Offset: 0x00031EE7
		public void scrollMouse(int a)
		{
			GameCanvas.pXYScrollMouse = a;
			if (GameCanvas.panel != null && GameCanvas.panel.isShow)
			{
				GameCanvas.panel.updateScroolMouse(a);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00033D10 File Offset: 0x00031F10
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

		// Token: 0x0600027E RID: 638 RVA: 0x00033D88 File Offset: 0x00031F88
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

		// Token: 0x0600027F RID: 639 RVA: 0x00033DE1 File Offset: 0x00031FE1
		public void pointerReleased(int x, int y)
		{
			GameCanvas.isPointerDown = false;
			GameCanvas.isPointerJustRelease = true;
			GameCanvas.isPointerMove = false;
			mScreen.keyTouch = -1;
			GameCanvas.px = x;
			GameCanvas.py = y;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00033E07 File Offset: 0x00032007
		public static bool isPointerHoldIn(int x, int y, int w, int h)
		{
			return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00033E40 File Offset: 0x00032040
		public static bool isMouseFocus(int x, int y, int w, int h)
		{
			return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00033E6C File Offset: 0x0003206C
		public static void clearKeyPressed()
		{
			for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
			{
				GameCanvas.keyPressed[i] = false;
			}
			GameCanvas.isPointerJustRelease = false;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00033E9C File Offset: 0x0003209C
		public static void clearKeyHold()
		{
			for (int i = 0; i < GameCanvas.keyHold.Length; i++)
			{
				GameCanvas.keyHold[i] = false;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00033EC4 File Offset: 0x000320C4
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

		// Token: 0x06000285 RID: 645 RVA: 0x00033F70 File Offset: 0x00032170
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

		// Token: 0x06000286 RID: 646 RVA: 0x000343B8 File Offset: 0x000325B8
		public static void endDlg()
		{
			if (GameCanvas.inputDlg != null)
			{
				GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
			}
			GameCanvas.currentDialog = null;
			InfoDlg.hide();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000343E0 File Offset: 0x000325E0
		public static void startOKDlg(string info)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00034413 File Offset: 0x00032613
		public static void startWaitDlg(string info)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
			GameCanvas.msgdlg.isWait = true;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00034413 File Offset: 0x00032613
		public static void startOKDlg(string info, bool isError)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
			GameCanvas.msgdlg.isWait = true;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00034451 File Offset: 0x00032651
		public static void startWaitDlg()
		{
			GameCanvas.closeKeyBoard();
			Char.isLoadingMap = true;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0003445E File Offset: 0x0003265E
		public static void startOK(string info, int actionID, object p)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
			GameCanvas.msgdlg.show();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00034490 File Offset: 0x00032690
		public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
			GameCanvas.msgdlg.show();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000344EB File Offset: 0x000326EB
		public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
			GameCanvas.msgdlg.show();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0003450A File Offset: 0x0003270A
		public static void startserverThongBao(string msgSv)
		{
			GameCanvas.thongBaoTest = msgSv;
			GameCanvas.xThongBaoTranslate = GameCanvas.w - 60;
			GameCanvas.dir_ = -1;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00006D67 File Offset: 0x00004F67
		public static bool isGetResourceFromServer()
		{
			return true;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00034528 File Offset: 0x00032728
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

		// Token: 0x06000291 RID: 657 RVA: 0x000345DC File Offset: 0x000327DC
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

		// Token: 0x06000292 RID: 658 RVA: 0x00034620 File Offset: 0x00032820
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

		// Token: 0x06000293 RID: 659 RVA: 0x00034678 File Offset: 0x00032878
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

		// Token: 0x06000294 RID: 660 RVA: 0x000346CC File Offset: 0x000328CC
		public static Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
		{
			RenderTexture rt = RenderTexture.active = new RenderTexture(targetX, targetY, 24);
			Graphics.Blit(texture2D, rt);
			Texture2D texture2D2 = new Texture2D(targetX, targetY);
			texture2D2.ReadPixels(new Rect(0f, 0f, (float)targetX, (float)targetY), 0, 0);
			texture2D2.Apply();
			return texture2D2;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00034718 File Offset: 0x00032918
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

		// Token: 0x06000296 RID: 662 RVA: 0x00034788 File Offset: 0x00032988
		public static string cutPng(string str)
		{
			string result = str;
			if (str.Contains(".png"))
			{
				result = str.Replace(".png", string.Empty);
			}
			return result;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000347B8 File Offset: 0x000329B8
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

		// Token: 0x06000298 RID: 664 RVA: 0x00034800 File Offset: 0x00032A00
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

		// Token: 0x06000299 RID: 665 RVA: 0x0003488C File Offset: 0x00032A8C
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

		// Token: 0x0600029A RID: 666 RVA: 0x00034915 File Offset: 0x00032B15
		public static bool isPaint(int x, int y)
		{
			return x >= GameScr.cmx && x <= GameScr.cmx + GameScr.gW && y >= GameScr.cmy && y <= GameScr.cmy + GameScr.gH + 30;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00034950 File Offset: 0x00032B50
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

		// Token: 0x0600029C RID: 668 RVA: 0x00034A14 File Offset: 0x00032C14
		public static void paintShukiren(int x, int y, mGraphics g)
		{
			g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00034A47 File Offset: 0x00032C47
		public void resetToLoginScrz()
		{
			this.resetToLoginScr = true;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00033E07 File Offset: 0x00032007
		public static bool isPointer(int x, int y, int w, int h)
		{
			return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && (GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00034A50 File Offset: 0x00032C50
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

		// Token: 0x060002A0 RID: 672 RVA: 0x00035180 File Offset: 0x00033380
		public static void clearAllPointerEvent()
		{
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerDown = false;
			GameCanvas.isPointerJustDown = false;
			GameCanvas.isPointerJustRelease = false;
			GameScr.gI().lastSingleClick = 0L;
			GameScr.gI().isPointerDowning = false;
		}

		// Token: 0x0400047C RID: 1148
		public static bool isLoadRes = false;

		// Token: 0x0400047D RID: 1149
		public static long timeNow = 0L;

		// Token: 0x0400047E RID: 1150
		public static bool open3Hour;

		// Token: 0x0400047F RID: 1151
		public static bool lowGraphic = false;

		// Token: 0x04000480 RID: 1152
		public static bool serverchat = false;

		// Token: 0x04000481 RID: 1153
		public static bool isMoveNumberPad = true;

		// Token: 0x04000482 RID: 1154
		public static bool isLoading;

		// Token: 0x04000483 RID: 1155
		public static bool isTouch = false;

		// Token: 0x04000484 RID: 1156
		public static bool isTouchControl;

		// Token: 0x04000485 RID: 1157
		public static bool isTouchControlSmallScreen;

		// Token: 0x04000486 RID: 1158
		public static bool isTouchControlLargeScreen;

		// Token: 0x04000487 RID: 1159
		public static bool isConnectFail;

		// Token: 0x04000488 RID: 1160
		public static GameCanvas instance;

		// Token: 0x04000489 RID: 1161
		public static bool bRun;

		// Token: 0x0400048A RID: 1162
		public static bool[] keyPressed = new bool[30];

		// Token: 0x0400048B RID: 1163
		public static bool[] keyReleased = new bool[30];

		// Token: 0x0400048C RID: 1164
		public static bool[] keyHold = new bool[30];

		// Token: 0x0400048D RID: 1165
		public static bool isPointerDown;

		// Token: 0x0400048E RID: 1166
		public static bool isPointerClick;

		// Token: 0x0400048F RID: 1167
		public static bool isPointerJustRelease;

		// Token: 0x04000490 RID: 1168
		public static bool isPointerMove;

		// Token: 0x04000491 RID: 1169
		public static int px;

		// Token: 0x04000492 RID: 1170
		public static int py;

		// Token: 0x04000493 RID: 1171
		public static int pxFirst;

		// Token: 0x04000494 RID: 1172
		public static int pyFirst;

		// Token: 0x04000495 RID: 1173
		public static int pxLast;

		// Token: 0x04000496 RID: 1174
		public static int pyLast;

		// Token: 0x04000497 RID: 1175
		public static int pxMouse;

		// Token: 0x04000498 RID: 1176
		public static int pyMouse;

		// Token: 0x04000499 RID: 1177
		public static Position[] arrPos = new Position[4];

		// Token: 0x0400049A RID: 1178
		public static int gameTick;

		// Token: 0x0400049B RID: 1179
		public static int taskTick;

		// Token: 0x0400049C RID: 1180
		public static bool isEff1;

		// Token: 0x0400049D RID: 1181
		public static bool isEff2;

		// Token: 0x0400049E RID: 1182
		public static long timeTickEff1;

		// Token: 0x0400049F RID: 1183
		public static long timeTickEff2;

		// Token: 0x040004A0 RID: 1184
		public static int w;

		// Token: 0x040004A1 RID: 1185
		public static int h;

		// Token: 0x040004A2 RID: 1186
		public static int hw;

		// Token: 0x040004A3 RID: 1187
		public static int hh;

		// Token: 0x040004A4 RID: 1188
		public static int wd3;

		// Token: 0x040004A5 RID: 1189
		public static int hd3;

		// Token: 0x040004A6 RID: 1190
		public static int w2d3;

		// Token: 0x040004A7 RID: 1191
		public static int h2d3;

		// Token: 0x040004A8 RID: 1192
		public static int w3d4;

		// Token: 0x040004A9 RID: 1193
		public static int h3d4;

		// Token: 0x040004AA RID: 1194
		public static int wd6;

		// Token: 0x040004AB RID: 1195
		public static int hd6;

		// Token: 0x040004AC RID: 1196
		public static mScreen currentScreen;

		// Token: 0x040004AD RID: 1197
		public static Menu menu = new Menu();

		// Token: 0x040004AE RID: 1198
		public static Panel panel;

		// Token: 0x040004AF RID: 1199
		public static Panel panel2;

		// Token: 0x040004B0 RID: 1200
		public static ChooseCharScr chooseCharScr;

		// Token: 0x040004B1 RID: 1201
		public static LoginScr loginScr;

		// Token: 0x040004B2 RID: 1202
		public static AddressScr adsScr;

		// Token: 0x040004B3 RID: 1203
		public static RegisterScreen registerScr;

		// Token: 0x040004B4 RID: 1204
		public static Dialog currentDialog;

		// Token: 0x040004B5 RID: 1205
		public static MsgDlg msgdlg;

		// Token: 0x040004B6 RID: 1206
		public static InputDlg inputDlg;

		// Token: 0x040004B7 RID: 1207
		public static MyVector currentPopup = new MyVector();

		// Token: 0x040004B8 RID: 1208
		public static int requestLoseCount;

		// Token: 0x040004B9 RID: 1209
		public static MyVector listPoint;

		// Token: 0x040004BA RID: 1210
		public static Paint paintz;

		// Token: 0x040004BB RID: 1211
		public static bool isGetResFromServer;

		// Token: 0x040004BC RID: 1212
		public static Image[] imgBG;

		// Token: 0x040004BD RID: 1213
		public static int skyColor;

		// Token: 0x040004BE RID: 1214
		public static int curPos = 0;

		// Token: 0x040004BF RID: 1215
		public static int[] bgW;

		// Token: 0x040004C0 RID: 1216
		public static int[] bgH;

		// Token: 0x040004C1 RID: 1217
		public static int planet = 0;

		// Token: 0x040004C2 RID: 1218
		private mGraphics g = new mGraphics();

		// Token: 0x040004C3 RID: 1219
		public static MyHashTable danhHieu = new MyHashTable();

		// Token: 0x040004C4 RID: 1220
		public static MyVector messageServer = new MyVector(string.Empty);

		// Token: 0x040004C5 RID: 1221
		public static bool isPlaySound = true;

		// Token: 0x040004C6 RID: 1222
		private static int clearOldData;

		// Token: 0x040004C7 RID: 1223
		public static int timeOpenKeyBoard;

		// Token: 0x040004C8 RID: 1224
		public static bool isFocusPanel2;

		// Token: 0x040004C9 RID: 1225
		public static int fps = 0;

		// Token: 0x040004CA RID: 1226
		public static int max;

		// Token: 0x040004CB RID: 1227
		public static int up;

		// Token: 0x040004CC RID: 1228
		public static int upmax;

		// Token: 0x040004CD RID: 1229
		private long timefps = mSystem.currentTimeMillis() + 1000L;

		// Token: 0x040004CE RID: 1230
		private long timeup = mSystem.currentTimeMillis() + 1000L;

		// Token: 0x040004CF RID: 1231
		private static int dir_ = -1;

		// Token: 0x040004D0 RID: 1232
		private int tickWaitThongBao;

		// Token: 0x040004D1 RID: 1233
		public bool isPaintCarret;

		// Token: 0x040004D2 RID: 1234
		public static MyVector debugUpdate;

		// Token: 0x040004D3 RID: 1235
		public static MyVector debugPaint;

		// Token: 0x040004D4 RID: 1236
		public static MyVector debugSession;

		// Token: 0x040004D5 RID: 1237
		private static bool isShowErrorForm = false;

		// Token: 0x040004D6 RID: 1238
		public static bool paintBG;

		// Token: 0x040004D7 RID: 1239
		public static int gsskyHeight;

		// Token: 0x040004D8 RID: 1240
		public static int gsgreenField1Y;

		// Token: 0x040004D9 RID: 1241
		public static int gsgreenField2Y;

		// Token: 0x040004DA RID: 1242
		public static int gshouseY;

		// Token: 0x040004DB RID: 1243
		public static int gsmountainY;

		// Token: 0x040004DC RID: 1244
		public static int bgLayer0y;

		// Token: 0x040004DD RID: 1245
		public static int bgLayer1y;

		// Token: 0x040004DE RID: 1246
		public static Image imgCloud;

		// Token: 0x040004DF RID: 1247
		public static Image imgSun;

		// Token: 0x040004E0 RID: 1248
		public static Image imgSun2;

		// Token: 0x040004E1 RID: 1249
		public static Image imgClear;

		// Token: 0x040004E2 RID: 1250
		public static Image[] imgBorder = new Image[3];

		// Token: 0x040004E3 RID: 1251
		public static Image[] imgSunSpec = new Image[3];

		// Token: 0x040004E4 RID: 1252
		public static int borderConnerW;

		// Token: 0x040004E5 RID: 1253
		public static int borderConnerH;

		// Token: 0x040004E6 RID: 1254
		public static int borderCenterW;

		// Token: 0x040004E7 RID: 1255
		public static int borderCenterH;

		// Token: 0x040004E8 RID: 1256
		public static int[] cloudX;

		// Token: 0x040004E9 RID: 1257
		public static int[] cloudY;

		// Token: 0x040004EA RID: 1258
		public static int sunX;

		// Token: 0x040004EB RID: 1259
		public static int sunY;

		// Token: 0x040004EC RID: 1260
		public static int sunX2;

		// Token: 0x040004ED RID: 1261
		public static int sunY2;

		// Token: 0x040004EE RID: 1262
		public static int[] layerSpeed;

		// Token: 0x040004EF RID: 1263
		public static int[] moveX;

		// Token: 0x040004F0 RID: 1264
		public static int[] moveXSpeed;

		// Token: 0x040004F1 RID: 1265
		public static bool isBoltEff;

		// Token: 0x040004F2 RID: 1266
		public static bool boltActive;

		// Token: 0x040004F3 RID: 1267
		public static int tBolt;

		// Token: 0x040004F4 RID: 1268
		public static Image imgBgIOS;

		// Token: 0x040004F5 RID: 1269
		public static int typeBg = -1;

		// Token: 0x040004F6 RID: 1270
		public static int transY;

		// Token: 0x040004F7 RID: 1271
		public static int[] yb = new int[5];

		// Token: 0x040004F8 RID: 1272
		public static int[] colorTop;

		// Token: 0x040004F9 RID: 1273
		public static int[] colorBotton;

		// Token: 0x040004FA RID: 1274
		public static int yb1;

		// Token: 0x040004FB RID: 1275
		public static int yb2;

		// Token: 0x040004FC RID: 1276
		public static int yb3;

		// Token: 0x040004FD RID: 1277
		public static int nBg = 0;

		// Token: 0x040004FE RID: 1278
		public static int lastBg = -1;

		// Token: 0x040004FF RID: 1279
		public static int[] bgRain = new int[]
		{
			1,
			4,
			11
		};

		// Token: 0x04000500 RID: 1280
		public static int[] bgRainFont = new int[]
		{
			-1
		};

		// Token: 0x04000501 RID: 1281
		public static Image imgCaycot;

		// Token: 0x04000502 RID: 1282
		public static Image tam;

		// Token: 0x04000503 RID: 1283
		public static int typeBackGround = -1;

		// Token: 0x04000504 RID: 1284
		public static int saveIDBg = -10;

		// Token: 0x04000505 RID: 1285
		public static bool isLoadBGok;

		// Token: 0x04000506 RID: 1286
		private static long lastTimePress = 0L;

		// Token: 0x04000507 RID: 1287
		public static int keyAsciiPress;

		// Token: 0x04000508 RID: 1288
		public static int pXYScrollMouse;

		// Token: 0x04000509 RID: 1289
		private static Image imgSignal;

		// Token: 0x0400050A RID: 1290
		public static MyVector flyTexts = new MyVector();

		// Token: 0x0400050B RID: 1291
		public int longTime;

		// Token: 0x0400050C RID: 1292
		public static long timeBreakLoading;

		// Token: 0x0400050D RID: 1293
		private static string thongBaoTest;

		// Token: 0x0400050E RID: 1294
		public static int xThongBaoTranslate = GameCanvas.w - 60;

		// Token: 0x0400050F RID: 1295
		public static bool isPointerJustDown = false;

		// Token: 0x04000510 RID: 1296
		private int count = 1;

		// Token: 0x04000511 RID: 1297
		public static bool csWait;

		// Token: 0x04000512 RID: 1298
		public static MyRandom r = new MyRandom();

		// Token: 0x04000513 RID: 1299
		public static bool isBlackScreen;

		// Token: 0x04000514 RID: 1300
		public static int[] bgSpeed;

		// Token: 0x04000515 RID: 1301
		public static int cmdBarX;

		// Token: 0x04000516 RID: 1302
		public static int cmdBarY;

		// Token: 0x04000517 RID: 1303
		public static int cmdBarW;

		// Token: 0x04000518 RID: 1304
		public static int cmdBarH;

		// Token: 0x04000519 RID: 1305
		public static int cmdBarLeftW;

		// Token: 0x0400051A RID: 1306
		public static int cmdBarRightW;

		// Token: 0x0400051B RID: 1307
		public static int cmdBarCenterW;

		// Token: 0x0400051C RID: 1308
		public static int hpBarX;

		// Token: 0x0400051D RID: 1309
		public static int hpBarY;

		// Token: 0x0400051E RID: 1310
		public static int hpBarW;

		// Token: 0x0400051F RID: 1311
		public static int expBarW;

		// Token: 0x04000520 RID: 1312
		public static int lvPosX;

		// Token: 0x04000521 RID: 1313
		public static int moneyPosX;

		// Token: 0x04000522 RID: 1314
		public static int hpBarH;

		// Token: 0x04000523 RID: 1315
		public static int girlHPBarY;

		// Token: 0x04000524 RID: 1316
		public int timeOut;

		// Token: 0x04000525 RID: 1317
		public int[] dustX;

		// Token: 0x04000526 RID: 1318
		public int[] dustY;

		// Token: 0x04000527 RID: 1319
		public int[] dustState;

		// Token: 0x04000528 RID: 1320
		public static int[] wsX;

		// Token: 0x04000529 RID: 1321
		public static int[] wsY;

		// Token: 0x0400052A RID: 1322
		public static int[] wsState;

		// Token: 0x0400052B RID: 1323
		public static int[] wsF;

		// Token: 0x0400052C RID: 1324
		public static Image[] imgWS;

		// Token: 0x0400052D RID: 1325
		public static Image imgShuriken;

		// Token: 0x0400052E RID: 1326
		public static Image[][] imgDust;

		// Token: 0x0400052F RID: 1327
		public static bool isResume;

		// Token: 0x04000530 RID: 1328
		public static ServerListScreen serverScreen;

		// Token: 0x04000531 RID: 1329
		public static ServerScr serverScr;

		// Token: 0x04000532 RID: 1330
		public bool resetToLoginScr;

		// Token: 0x04000533 RID: 1331
		public static long TIMEOUT;

		// Token: 0x04000534 RID: 1332
		public static int timeLoading = 15;

		// Token: 0x04000535 RID: 1333
		public static string acc;

		// Token: 0x04000536 RID: 1334
		public static string pass;

		// Token: 0x04000537 RID: 1335
		public static int server = -1;

		// Token: 0x04000538 RID: 1336
		private static long time;
	}
}
