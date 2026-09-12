using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x0200024D RID: 589
	public class ServerListScreen : mScreen, IActionListener
	{
		// Token: 0x06001A75 RID: 6773 RVA: 0x001A8DE0 File Offset: 0x001A6FE0
		public ServerListScreen()
		{
			int num = 4;
			if (num * 32 + 23 + 33 >= GameCanvas.w)
			{
				num--;
			}
			this.initCommand();
			if (!GameCanvas.isTouch)
			{
				ServerListScreen.selected = 0;
				this.processInput();
			}
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameScr.cmy = 200;
			Command command = new Command();
			command.actionChat = delegate (string str)
			{
				string text = str;
				string text2 = str;
				if (text == null)
				{
					text = ServerListScreen.linkDefault;
					return;
				}
				if (text == null && text2 != null)
				{
					if (text2.Equals(string.Empty) || text2.Length < 20)
					{
						text2 = ServerListScreen.linkDefault;
					}
					ServerListScreen.GetServerList(text2);
				}
				if (text != null && text2 == null)
				{
					if (text.Equals(string.Empty) || text.Length < 20)
					{
						text = ServerListScreen.linkDefault;
					}
					ServerListScreen.GetServerList(text);
				}
				if (text != null && text2 != null)
				{
					if (text.Length > text2.Length)
					{
						ServerListScreen.GetServerList(text);
						return;
					}
					ServerListScreen.GetServerList(text2);
				}
			};
			ServerListScreen.cmdUpdateServer = command;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x001A8E74 File Offset: 0x001A7074
		public static void createDeleteRMS()
		{
			if (ServerListScreen.cmdDeleteRMS == null)
			{
				if (GameCanvas.serverScreen == null)
				{
					GameCanvas.serverScreen = new ServerListScreen();
				}
				ServerListScreen.cmdDeleteRMS = new Command(string.Empty, GameCanvas.serverScreen, 14, null);
				ServerListScreen.cmdDeleteRMS.x = GameCanvas.w - 78;
				ServerListScreen.cmdDeleteRMS.y = GameCanvas.h - 26;
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x001A8ED4 File Offset: 0x001A70D4
		private void initCommand()
		{
			this.nCmdPlay = 0;
			string text = Rms.loadRMSString("acc");
			Rms.loadRMSString("pass");
			if (text == null)
			{
				if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null)
				{
					this.nCmdPlay = 1;
				}
			}
			else if (text.Equals(string.Empty))
			{
				if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null)
				{
					this.nCmdPlay = 1;
				}
			}
			else
			{
				this.nCmdPlay = 1;
			}
			this.cmd = new Command[(Main.isIPhone || mGraphics.zoomLevel <= 1) ? (4 + this.nCmdPlay) : (3 + this.nCmdPlay)];
			int num = GameCanvas.hh - 15 * this.cmd.Length + 28;
			for (int i = 0; i < this.cmd.Length; i++)
			{
				switch (i)
				{
					case 0:
						this.cmd[0] = new Command(string.Empty, this, 3, null);
						if (text == null)
						{
							this.cmd[0].caption = mResources.playNew;
							if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null)
							{
								this.cmd[0].caption = mResources.choitiep;
							}
						}
						else if (text.Equals(string.Empty))
						{
							this.cmd[0].caption = mResources.playNew;
							if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null)
							{
								this.cmd[0].caption = mResources.choitiep;
							}
						}
						else
						{
							this.cmd[0].caption = mResources.playAcc + ": " + ((!text.Contains(',')) ? text : "");
							if (this.cmd[0].caption.Length > 23)
							{
								this.cmd[0].caption = this.cmd[0].caption.Substring(0, 23);
								Command command = this.cmd[0];
								command.caption += "...";
							}
						}
						break;
					case 1:
						if (this.nCmdPlay == 1)
						{
							this.cmd[1] = new Command(string.Empty, this, 10100, null);
							this.cmd[1].caption = mResources.playNew;
						}
						else
						{
							this.cmd[1] = new Command(mResources.change_account, this, 7, null);
						}
						break;
					case 2:
						if (this.nCmdPlay == 1)
						{
							this.cmd[2] = new Command(mResources.change_account, this, 7, null);
						}
						else
						{
							this.cmd[2] = new Command(string.Empty, this, 17, null);
						}
						break;
					case 3:
						if (this.nCmdPlay == 1)
						{
							this.cmd[3] = new Command(string.Empty, this, 17, null);
						}
						else
						{
							this.cmd[3] = new Command(mResources.option, this, 8, null);
						}
						break;
					case 4:
						this.cmd[4] = new Command(mResources.option, this, 8, null);
						break;
				}
				this.cmd[i].y = num;
				this.cmd[i].setType();
				this.cmd[i].x = (GameCanvas.w - this.cmd[i].w) / 2;
				num += 30;
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x001A922E File Offset: 0x001A742E
		public static void doUpdateServer()
		{
			if (ServerListScreen.cmdUpdateServer == null && GameCanvas.serverScreen == null)
			{
				GameCanvas.serverScreen = new ServerListScreen();
			}
			Net.connectHTTP2(ServerListScreen.linkDefault, ServerListScreen.cmdUpdateServer);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x001A9258 File Offset: 0x001A7458
		public static void GetServerList(string str)
		{
			ServerListScreen.lengthServer = new int[3];
			string[] array = Res.split(str.Trim(), ",", 0);
			Res.outz("tem leng= " + array.Length.ToString());
			mResources.loadLanguague(sbyte.Parse(array[array.Length - 2]));
			ServerListScreen.nameServer = new string[array.Length - 2];
			ServerListScreen.address = new string[array.Length - 2];
			ServerListScreen.port = new short[array.Length - 2];
			ServerListScreen.language = new sbyte[array.Length - 2];
			ServerListScreen.hasConnected = new bool[2];
			for (int i = 0; i < array.Length - 2; i++)
			{
				string[] array2 = Res.split(array[i].Trim(), ":", 0);
				ServerListScreen.nameServer[i] = array2[0];
				ServerListScreen.address[i] = array2[1];
				ServerListScreen.port[i] = short.Parse(array2[2]);
				ServerListScreen.language[i] = sbyte.Parse(array2[3].Trim());
				ServerListScreen.lengthServer[(int)ServerListScreen.language[i]]++;
			}
			ServerListScreen.serverPriority = sbyte.Parse(array[array.Length - 1]);
			ServerListScreen.SaveIP();
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x001A937C File Offset: 0x001A757C
		public override void paint(mGraphics g)
		{
			if (!ServerListScreen.loadScreen)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			}
			else
			{
				GameCanvas.paintBGGameScr(g);
			}
			int num2 = 2;
			mFont.tahoma_7_white.drawStringBorder(g, string.Concat(new string[]
			{
				"v",
				GameMidlet.VERSION,
				" (x",
				mGraphics.zoomLevel.ToString(),
				")"
			}), GameCanvas.w - 2, num2, 1, mFont.tahoma_7_grey);
			string empty = string.Empty;
			empty = ((ServerListScreen.testConnect != 0) ? (empty + ServerListScreen.nameServer[ServerListScreen.ipSelect] + " connected") : (empty + ServerListScreen.nameServer[ServerListScreen.ipSelect] + " disconnect"));
			if (mSystem.isTest)
			{
				mFont.tahoma_7_white.drawString(g, empty, GameCanvas.w - 2, num2 + 15 + 15, 1, mFont.tahoma_7_grey);
			}
			if (!ServerListScreen.isGetData || ServerListScreen.loadScreen)
			{
				if (mSystem.clientType == 1 && !GameCanvas.isTouch)
				{
					mFont.tahoma_7_white.drawStringBorder(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
				}
				else
				{
					mFont.tahoma_7_white.drawStringBorder(g, ServerListScreen.linkweb, GameCanvas.w - 2, num2 + 20, 1, mFont.tahoma_7_grey);
				}
			}
			else
			{
				mFont.tahoma_7_white.drawStringBorder(g, ServerListScreen.linkweb, GameCanvas.w - 2, num2 + 20, 1, mFont.tahoma_7_grey);
			}
			int w = GameCanvas.w;
			if (ServerListScreen.cmdDeleteRMS != null)
			{
				mFont.tahoma_7_white.drawStringBorder(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			}
			if (GameCanvas.currentDialog == null)
			{
				if (!ServerListScreen.loadScreen)
				{
					if (!ServerListScreen.bigOk)
					{
						GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), ServerListScreen.bg);
						ModFunc.PaintLogoGif(g, GameCanvas.hw, GameCanvas.hh - 32, 3);
						if (!ServerListScreen.isGetData)
						{
							mFont.tahoma_7b_white.drawString(g, mResources.taidulieudechoi, GameCanvas.hw, GameCanvas.hh + 24, 2);
							Command command = ServerListScreen.cmdDownload;
							if (command != null)
							{
								command.paint(g);
							}
						}
						else
						{
							Command command2 = ServerListScreen.cmdDownload;
							if (command2 != null)
							{
								command2.paint(g);
							}
							GameScr.paintOngMauPercent(GameScr.frBarPow20, GameScr.frBarPow21, GameScr.frBarPow22, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, 100f, g);
							GameScr.paintOngMauPercent(GameScr.frBarPow0, GameScr.frBarPow1, GameScr.frBarPow2, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, (float)ServerListScreen.percent, g);
						}
					}
				}
				else
				{
					int num3 = GameCanvas.hh - 15 * this.cmd.Length - 15;
					if (num3 < 25)
					{
						num3 = 25;
					}
					if (ModFunc.imgLogoBig != null)
					{
						ModFunc.PaintLogoGif(g, GameCanvas.hw, num3, 3);
					}
					for (int i = 0; i < this.cmd.Length; i++)
					{
						this.cmd[i].paint(g);
					}
					g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
					if (ServerListScreen.testConnect == -1)
					{
						if (GameCanvas.gameTick % 20 > 10)
						{
							g.drawRegion(GameScr.imgRoomStat, 0, 14, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(this.cmd[2 + this.nCmdPlay].caption) >> 1) - 10, this.cmd[2 + this.nCmdPlay].y + 10, 0);
						}
					}
					else
					{
						g.drawRegion(GameScr.imgRoomStat, 0, ServerListScreen.testConnect * 7, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(this.cmd[2 + this.nCmdPlay].caption) >> 1) - 10, this.cmd[2 + this.nCmdPlay].y + 9, 0);
					}
				}
			}
			base.paint(g);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x001A975C File Offset: 0x001A795C
		public void selectServer()
		{
			ServerListScreen.flagServer = 100;
			GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
			Session_ME.gI().close();
			GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
			GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
			GameMidlet.LANGUAGE = (int)ServerListScreen.language[ServerListScreen.ipSelect];
			Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
			if (ServerListScreen.language[ServerListScreen.ipSelect] != mResources.language)
			{
				mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
			}
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			this.initCommand();
			ServerListScreen.loadScreen = true;
			ServerListScreen.countDieConnect = 0;
			ServerListScreen.testConnect = -1;
			ServerListScreen.isAutoConect = true;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x001A9814 File Offset: 0x001A7A14
		public override void update()
		{
			if (ModFunc.AutoLogin())
			{
				return;
			}
			if (ServerListScreen.waitToLogin)
			{
				ServerListScreen.tWaitToLogin++;
				if (ServerListScreen.tWaitToLogin == 50)
				{
					GameCanvas.serverScreen.selectServer();
				}
				if (ServerListScreen.tWaitToLogin == 100)
				{
					if (GameCanvas.loginScr == null)
					{
						GameCanvas.loginScr = new LoginScr();
					}
					GameCanvas.loginScr.doLogin();
					Service.gI().finishUpdate();
					ServerListScreen.waitToLogin = false;
				}
			}
			if (ServerListScreen.flagServer > 0)
			{
				ServerListScreen.flagServer--;
				if (ServerListScreen.flagServer == 0)
				{
					GameCanvas.endDlg();
				}
				if (ServerListScreen.testConnect == 2)
				{
					ServerListScreen.flagServer = 0;
					GameCanvas.endDlg();
				}
			}
			if (ServerListScreen.flagServer <= 0 && ServerListScreen.isAutoConect)
			{
				ServerListScreen.countDieConnect++;
				if (ServerListScreen.countDieConnect > 100000)
				{
					ServerListScreen.countDieConnect = 0;
				}
			}
			for (int i = 0; i < this.cmd.Length; i++)
			{
				if (i == ServerListScreen.selected)
				{
					this.cmd[i].isFocus = true;
				}
				else
				{
					this.cmd[i].isFocus = false;
				}
			}
			GameScr.cmx++;
			if (!ServerListScreen.loadScreen && (ServerListScreen.bigOk || ServerListScreen.percent == 100))
			{
				ServerListScreen.cmdDownload = null;
			}
			base.update();
			if (Char.isLoadingMap || !ServerListScreen.loadScreen || !ServerListScreen.isAutoConect || GameCanvas.currentScreen != this || ServerListScreen.testConnect == 2)
			{
				return;
			}
			if (ServerListScreen.countDieConnect < 5)
			{
				if (ServerListScreen.flagServer <= 0)
				{
					ServerListScreen.flagServer = 100;
					GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
					GameCanvas.connect();
					return;
				}
			}
			else if (!Session_ME.gI().isConnected())
			{
				if (ServerListScreen.flagServer <= 0)
				{
					Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 18, null);
					Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 19, null);
					GameCanvas.startYesNoDlg(mResources.maychutathoacmatsong + ". " + mResources.confirmChangeServer, cmdYes, cmdNo);
					ServerListScreen.flagServer = 100;
					return;
				}
			}
			else if (ServerListScreen.flagServer <= 0)
			{
				ServerListScreen.countDieConnect = 0;
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x001A9A06 File Offset: 0x001A7C06
		private void processInput()
		{
			if (ServerListScreen.loadScreen)
			{
				this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
				return;
			}
			this.center = ServerListScreen.cmdDownload;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x001A9A3E File Offset: 0x001A7C3E
		public static void updateDeleteData()
		{
			if (ServerListScreen.cmdDeleteRMS != null && ServerListScreen.cmdDeleteRMS.isPointerPressInside())
			{
				ServerListScreen.cmdDeleteRMS.performAction();
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x001A9A60 File Offset: 0x001A7C60
		public override void updateKey()
		{
			if (GameCanvas.isTouch)
			{
				ServerListScreen.updateDeleteData();
				if (!ServerListScreen.loadScreen)
				{
					if (ServerListScreen.cmdDownload != null && ServerListScreen.cmdDownload.isPointerPressInside())
					{
						ServerListScreen.cmdDownload.performAction();
					}
					base.updateKey();
					return;
				}
				for (int i = 0; i < this.cmd.Length; i++)
				{
					if (this.cmd[i] != null && this.cmd[i].isPointerPressInside())
					{
						if (ServerListScreen.testConnect == -1 || ServerListScreen.testConnect == 0)
						{
							if (this.cmd[i].caption.IndexOf(mResources.server) != -1)
							{
								this.cmd[i].performAction();
							}
						}
						else
						{
							this.cmd[i].performAction();
						}
					}
				}
			}
			else if (ServerListScreen.loadScreen)
			{
				if (GameCanvas.keyPressed[8])
				{
					int num = (mGraphics.zoomLevel <= 1) ? 4 : 2;
					GameCanvas.keyPressed[8] = false;
					ServerListScreen.selected++;
					if (ServerListScreen.selected > num)
					{
						ServerListScreen.selected = 0;
					}
					this.processInput();
				}
				if (GameCanvas.keyPressed[2])
				{
					int num2 = (mGraphics.zoomLevel <= 1) ? 4 : 2;
					GameCanvas.keyPressed[2] = false;
					ServerListScreen.selected--;
					if (ServerListScreen.selected < 0)
					{
						ServerListScreen.selected = num2;
					}
					this.processInput();
				}
			}
			if (!ServerListScreen.isWait)
			{
				base.updateKey();
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x001A9BB0 File Offset: 0x001A7DB0
		public static void SaveIP()
		{
			try
			{
				Rms.saveRMSString("NRlink2", ModFunc.EncodeStringToByteArrayString(ServerListScreen.linkDefault, "69"));
				SplashScr.loadIP();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x001A9BF0 File Offset: 0x001A7DF0
		public static void SaveIPNew(string ip)
		{
			try
			{
				Rms.saveRMSString("NRlink2", ModFunc.EncodeStringToByteArrayString(ip, "69"));
				SplashScr.loadIP();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x001A9C2C File Offset: 0x001A7E2C
		public static void LoadIP()
		{
			try
			{
				if (ServerListScreen.isMultiSever)
				{
					if (string.IsNullOrEmpty(ServerListScreen.ListIP))
					{
						ServerListScreen.GetServerList(ServerListScreen.linkDefault);
						return;
					}
					ServerListScreen.lengthServer = new int[3];
					mResources.loadLanguague(0);
					string[] serverList = ServerListScreen.ListIP.Split(',', StringSplitOptions.None);
					if (serverList.Length <= 1)
					{
						ServerListScreen.GetServerList(ServerListScreen.linkDefault);
						return;
					}
					int serverLength = serverList.Length;
					ServerListScreen.nameServer = new string[serverLength];
					ServerListScreen.address = new string[serverLength];
					ServerListScreen.port = new short[serverLength];
					ServerListScreen.language = new sbyte[serverLength];
					for (int i = 0; i < serverLength; i++)
					{
						if (!string.IsNullOrEmpty(serverList[i]))
						{
							string[] serverInfo = serverList[i].Trim().Split(':', StringSplitOptions.None);
							if (serverInfo.Length < 3)
							{
								mSystem.println(string.Format("Invalid server data at index {0}", i));
							}
							else
							{
								try
								{
									ServerListScreen.nameServer[i] = serverInfo[0];
									ServerListScreen.address[i] = serverInfo[1];
									ServerListScreen.port[i] = short.Parse(serverInfo[2]);
									ServerListScreen.language[i] = 0;
									ServerListScreen.lengthServer[(int)ServerListScreen.language[i]]++;
								}
								catch (Exception ex)
								{
									mSystem.println(string.Format("Error parsing server {0}: {1}", i, ex.Message));
								}
							}
						}
					}
				}
				else
				{
					string encryptedData = Rms.loadRMSString("NRlink2");
					if (string.IsNullOrEmpty(encryptedData))
					{
						ServerListScreen.GetServerList(ServerListScreen.linkDefault);
						return;
					}
					string decryptedData = ModFunc.DecodeByteArrayString(encryptedData, "69");
					if (string.IsNullOrEmpty(decryptedData))
					{
						ServerListScreen.GetServerList(ServerListScreen.linkDefault);
						return;
					}
					ServerListScreen.lengthServer = new int[3];
					mResources.loadLanguague(0);
					string[] serverList2 = decryptedData.Split(":0", StringSplitOptions.None);
					if (serverList2.Length <= 1)
					{
						ServerListScreen.GetServerList(ServerListScreen.linkDefault);
						return;
					}
					int serverLength2 = serverList2.Length - 1;
					ServerListScreen.nameServer = new string[serverLength2];
					ServerListScreen.address = new string[serverLength2];
					ServerListScreen.port = new short[serverLength2];
					ServerListScreen.language = new sbyte[serverLength2];
					for (int j = 0; j < serverLength2; j++)
					{
						string[] serverInfo2 = serverList2[j].Trim(':').Trim(',').Split(':', StringSplitOptions.None);
						if (serverInfo2.Length < 3)
						{
							mSystem.println(string.Format("Invalid server data at index {0}", j));
						}
						else
						{
							try
							{
								ServerListScreen.nameServer[j] = serverInfo2[0];
								ServerListScreen.address[j] = serverInfo2[1];
								ServerListScreen.port[j] = short.Parse(serverInfo2[2]);
								ServerListScreen.language[j] = 0;
								ServerListScreen.lengthServer[(int)ServerListScreen.language[j]]++;
							}
							catch (Exception ex2)
							{
								mSystem.println(string.Format("Error parsing server {0}: {1}", j, ex2.Message));
							}
						}
					}
				}
				if (ServerListScreen.lengthServer[0] == 0)
				{
					mSystem.println("No valid servers found");
					ServerListScreen.GetServerList(ServerListScreen.linkDefault);
				}
				else
				{
					int lastServer = Rms.loadRMSInt("svselect");
					if (lastServer >= 0 && lastServer < ServerListScreen.nameServer.Length)
					{
						ServerListScreen.ipSelect = lastServer;
						ServerListScreen.serverPriority = (sbyte)lastServer;
					}
					else
					{
						ServerListScreen.ipSelect = 0;
						ServerListScreen.serverPriority = 0;
					}
					SplashScr.loadIP();
				}
			}
			catch (Exception ex3)
			{
				mSystem.println("LoadIP Error: " + ex3.Message);
				ServerListScreen.GetServerList(ServerListScreen.linkDefault);
				try
				{
					ServerListScreen.SaveIPNew(ServerListScreen.linkDefault);
				}
				catch (Exception ex4)
				{
					mSystem.println("SaveIP Error: " + ex4.Message);
				}
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x001AA000 File Offset: 0x001A8200
		public override void switchToMe()
		{
			TabController.updateCharName(string.Empty, 1);
			EffectManager.remove();
			GameScr.cmy = 0;
			GameScr.cmx = 0;
			this.initCommand();
			ServerListScreen.isWait = false;
			GameCanvas.loginScr = null;
			string text = Rms.loadRMSString("ResVersion");
			if (((text == null || !(text != string.Empty)) ? -1 : int.Parse(text)) > 0)
			{
				ServerListScreen.loadScreen = true;
				GameCanvas.loadBG(1);
			}
			ServerListScreen.bigOk = true;
			this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
			this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
			this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
			if (this.cmd.Length == 5 + this.nCmdPlay)
			{
				this.cmd[4 + this.nCmdPlay].caption = mResources.option;
			}
			Char.isLoadingMap = false;
			ModFunc.startAutoItem = false;
			mSystem.resetCurInapp();
			base.switchToMe();
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x001AA120 File Offset: 0x001A8320
		public void switchToMe2()
		{
			GameScr.cmy = 0;
			GameScr.cmx = 0;
			this.initCommand();
			ServerListScreen.isWait = false;
			GameCanvas.loginScr = null;
			string text = Rms.loadRMSString("ResVersion");
			if (((text == null || !(text != string.Empty)) ? -1 : int.Parse(text)) > 0)
			{
				ServerListScreen.loadScreen = true;
				GameCanvas.loadBG(1);
			}
			ServerListScreen.bigOk = true;
			this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
			this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
			this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
			if (this.cmd.Length == 5 + this.nCmdPlay)
			{
				this.cmd[4 + this.nCmdPlay].caption = mResources.option;
			}
			mSystem.resetCurInapp();
			base.switchToMe();
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x001AA224 File Offset: 0x001A8424
		public void cancel()
		{
			if (GameCanvas.serverScreen == null)
			{
				GameCanvas.serverScreen = new ServerListScreen();
			}
			ServerListScreen.demPercent = 0;
			ServerListScreen.percent = 0;
			ServerListScreen.stopDownload = true;
			GameCanvas.serverScreen.show2();
			ServerListScreen.isGetData = false;
			ServerListScreen.cmdDownload.isFocus = true;
			this.center = new Command(string.Empty, this, 2, null);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x001AA284 File Offset: 0x001A8484
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
				case 1:
				case 4:
					Session_ME.gI().close();
					ServerListScreen.isAutoConect = false;
					ServerListScreen.countDieConnect = 0;
					ServerListScreen.loadScreen = true;
					ServerListScreen.testConnect = 0;
					ServerListScreen.isGetData = false;
					Rms.clearAll();
					this.switchToMe();
					return;
				case 2:
					ServerListScreen.stopDownload = false;
					ServerListScreen.cmdDownload = new Command(mResources.huy, this, 4, null);
					ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
					ServerListScreen.cmdDownload.y = GameCanvas.hh + 65;
					this.right = null;
					if (!GameCanvas.isTouch)
					{
						ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
						ServerListScreen.cmdDownload.y = GameCanvas.h - mScreen.cmdH - 1;
					}
					this.center = new Command(string.Empty, this, 4, null);
					if (!ServerListScreen.isGetData)
					{
						Service.gI().updateData();
						Service.gI().getResource(1, null);
						if (!GameCanvas.isTouch)
						{
							ServerListScreen.cmdDownload.isFocus = true;
							this.center = new Command(string.Empty, this, 4, null);
						}
						ServerListScreen.isGetData = true;
					}
					return;
				case 3:
					{
						if (GameCanvas.loginScr == null)
						{
							GameCanvas.loginScr = new LoginScr();
						}
						GameCanvas.loginScr.switchToMe();
						bool flag3 = Rms.loadRMSString("acc") != null && !Rms.loadRMSString("acc").Equals(string.Empty);
						bool flag2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()).Equals(string.Empty);
						if (!flag3 && !flag2)
						{
							GameCanvas.connect();
							string text3 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
							if (text3 == null || text3.Equals(string.Empty))
							{
								Service.gI().login2(string.Empty);
							}
							else
							{
								GameCanvas.loginScr.isLogin2 = true;
								GameCanvas.connect();
								Service.gI().setClientType();
								Service.gI().login(text3, string.Empty, GameMidlet.VERSION, 1);
							}
							if (Session_ME.connected)
							{
								GameCanvas.startWaitDlg();
							}
							else
							{
								GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
							}
						}
						else
						{
							GameCanvas.loginScr.doLogin();
						}
						LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
						return;
					}
				case 5:
					ServerListScreen.doUpdateServer();
					if (ServerListScreen.nameServer.Length != 1)
					{
						MyVector myVector = new MyVector(string.Empty);
						for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
						{
							myVector.addElement(new Command(ServerListScreen.nameServer[i], this, 6, null));
						}
						GameCanvas.menu.startAt(myVector, 0);
						if (!GameCanvas.isTouch)
						{
							GameCanvas.menu.menuSelectedItem = ServerListScreen.ipSelect;
						}
					}
					return;
				case 6:
					ServerListScreen.ipSelect = GameCanvas.menu.menuSelectedItem;
					this.selectServer();
					return;
				case 7:
					if (GameCanvas.loginScr == null)
					{
						GameCanvas.loginScr = new LoginScr();
					}
					GameCanvas.loginScr.switchToMe();
					return;
				case 8:
					break;
				case 9:
					Rms.saveRMSInt("lowGraphic", 1);
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
					return;
				case 10:
					Rms.saveRMSInt("lowGraphic", 0);
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
					return;
				case 11:
					{
						if (GameCanvas.loginScr == null)
						{
							GameCanvas.loginScr = new LoginScr();
						}
						GameCanvas.loginScr.switchToMe();
						string text4 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
						if (text4 == null || text4.Equals(string.Empty))
						{
							Service.gI().login2(string.Empty);
						}
						else
						{
							GameCanvas.loginScr.isLogin2 = true;
							GameCanvas.connect();
							Service.gI().setClientType();
							Service.gI().login(text4, string.Empty, GameMidlet.VERSION, 1);
						}
						GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
						return;
					}
				case 12:
					GameMidlet.instance.exit();
					return;
				case 13:
					if (!ServerListScreen.isGetData || ServerListScreen.loadScreen)
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
								break;
							case 6:
								mSystem.callHotlineWindowsPhone();
								return;
							default:
								return;
						}
						return;
					}
					break;
				default:
					if (idAction == 1000)
					{
						GameCanvas.connect();
						return;
					}
					if (idAction == 10100)
					{
						if (GameCanvas.loginScr == null)
						{
							GameCanvas.loginScr = new LoginScr();
						}
						GameCanvas.loginScr.switchToMe();
						GameCanvas.connect();
						Service.gI().login2(string.Empty);
						GameCanvas.startWaitDlg();
						LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
						return;
					}
					break;
			}
			switch (idAction)
			{
				case 14:
					{
						Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 15, null);
						Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 16, null);
						GameCanvas.startYesNoDlg(mResources.deletaDataNote, cmdYes, cmdNo);
						return;
					}
				case 15:
					Rms.clearAll();
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
					return;
				case 16:
					InfoDlg.hide();
					GameCanvas.currentDialog = null;
					return;
				case 17:
					if (GameCanvas.serverScr == null)
					{
						GameCanvas.serverScr = new ServerScr();
					}
					GameCanvas.serverScr.switchToMe();
					return;
				case 18:
					GameCanvas.endDlg();
					InfoDlg.hide();
					if (GameCanvas.serverScr == null)
					{
						GameCanvas.serverScr = new ServerScr();
					}
					GameCanvas.serverScr.switchToMe();
					return;
				case 19:
					if (mSystem.clientType == 1)
					{
						InfoDlg.hide();
						GameCanvas.currentDialog = null;
						return;
					}
					ServerListScreen.countDieConnect = 0;
					ServerListScreen.testConnect = 0;
					ServerListScreen.isAutoConect = true;
					return;
				default:
					return;
			}
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x001AA818 File Offset: 0x001A8A18
		public void init()
		{
			if (!ServerListScreen.loadScreen)
			{
				ServerListScreen.cmdDownload = new Command(mResources.taidulieu, this, 2, null);
				ServerListScreen.cmdDownload.isFocus = true;
				ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
				ServerListScreen.cmdDownload.y = GameCanvas.hh + 45;
				if (ServerListScreen.cmdDownload.y > GameCanvas.h - 26)
				{
					ServerListScreen.cmdDownload.y = GameCanvas.h - 26;
				}
				ServerListScreen.cmdDownload.performAction();
			}
			if (!GameCanvas.isTouch)
			{
				ServerListScreen.selected = 0;
				this.processInput();
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x001AA8B8 File Offset: 0x001A8AB8
		public void show2()
		{
			GameScr.cmx = 0;
			GameScr.cmy = 0;
			this.initCommand();
			ServerListScreen.loadScreen = false;
			ServerListScreen.percent = 0;
			ServerListScreen.bigOk = false;
			ServerListScreen.isGetData = false;
			ServerListScreen.p = 0;
			ServerListScreen.demPercent = 0;
			ServerListScreen.strWait = mResources.PLEASEWAIT;
			Char.isLoadingMap = false;
			this.init();
			base.switchToMe();
		}

		// Token: 0x04003486 RID: 13446
		public static string[] nameServer;

		// Token: 0x04003487 RID: 13447
		public static string[] address;

		// Token: 0x04003488 RID: 13448
		public static sbyte serverPriority;

		// Token: 0x04003489 RID: 13449
		public static bool[] hasConnected;

		// Token: 0x0400348A RID: 13450
		public static short[] port;

		// Token: 0x0400348B RID: 13451
		public static int selected;

		// Token: 0x0400348C RID: 13452
		public static bool isWait;

		// Token: 0x0400348D RID: 13453
		public static Command cmdUpdateServer;

		// Token: 0x0400348E RID: 13454
		public static sbyte[] language;

		// Token: 0x0400348F RID: 13455
		private Command[] cmd;

		// Token: 0x04003490 RID: 13456
		private int nCmdPlay;

		// Token: 0x04003491 RID: 13457
		public static string keyDecryptString;

		// Token: 0x04003492 RID: 13458
		public static Command cmdDeleteRMS;

		// Token: 0x04003493 RID: 13459
		public static bool isMultiSever = false;

		// Token: 0x04003494 RID: 13460
		public static string ListIP = "NRO:127.0.0.1:14445:0,0,0";

		// Token: 0x04003495 RID: 13461
		public static string linkDefault = "NRO:127.0.0.1:14445:0,0,0";

		// Token: 0x04003496 RID: 13462
		public const sbyte languageVersion = 2;

		// Token: 0x04003497 RID: 13463
		public new int keyTouch = -1;

		// Token: 0x04003498 RID: 13464
		public static bool stopDownload;

		// Token: 0x04003499 RID: 13465
		public static string linkweb = ModFunc.homeUrl;

		// Token: 0x0400349A RID: 13466
		public static int countDieConnect;

		// Token: 0x0400349B RID: 13467
		public static bool waitToLogin;

		// Token: 0x0400349C RID: 13468
		public static int tWaitToLogin;

		// Token: 0x0400349D RID: 13469
		public static int[] lengthServer = new int[3];

		// Token: 0x0400349E RID: 13470
		public static int ipSelect;

		// Token: 0x0400349F RID: 13471
		public static int flagServer;

		// Token: 0x040034A0 RID: 13472
		public static bool bigOk;

		// Token: 0x040034A1 RID: 13473
		public static int percent;

		// Token: 0x040034A2 RID: 13474
		public static string strWait;

		// Token: 0x040034A3 RID: 13475
		public static int nBig;

		// Token: 0x040034A4 RID: 13476
		public static int nBg;

		// Token: 0x040034A5 RID: 13477
		public static int demPercent;

		// Token: 0x040034A6 RID: 13478
		public static int maxBg;

		// Token: 0x040034A7 RID: 13479
		public static bool isGetData = false;

		// Token: 0x040034A8 RID: 13480
		public static Command cmdDownload;

		// Token: 0x040034A9 RID: 13481
		private Command cmdStart;

		// Token: 0x040034AA RID: 13482
		public string dataSize;

		// Token: 0x040034AB RID: 13483
		public static int p;

		// Token: 0x040034AC RID: 13484
		public static int testConnect;

		// Token: 0x040034AD RID: 13485
		public static bool loadScreen;

		// Token: 0x040034AE RID: 13486
		public static bool isAutoConect = true;

		// Token: 0x040034AF RID: 13487
		public static Texture2D bg = Resources.Load("res/bg/bg") as Texture2D;
	}
}
