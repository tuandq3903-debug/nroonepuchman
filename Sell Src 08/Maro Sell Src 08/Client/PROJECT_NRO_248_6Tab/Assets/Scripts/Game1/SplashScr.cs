using System;

namespace Game1
{
	// Token: 0x020004EB RID: 1259
	public class SplashScr : mScreen
	{
		// Token: 0x06003894 RID: 14484 RVA: 0x003711CC File Offset: 0x0036F3CC
		public SplashScr()
		{
			SplashScr.instance = this;
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x003711DA File Offset: 0x0036F3DA
		public static void loadSplashScr()
		{
			SplashScr.splashScrStat = 0;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x003711E4 File Offset: 0x0036F3E4
		public override void update()
		{
			ServerListScreen.updateDeleteData();
			if (SplashScr.splashScrStat == 30 && !this.isCheckConnect)
			{
				this.isCheckConnect = true;
				if (Rms.loadRMSInt("serverchat") != -1)
				{
					GameScr.isPaintChatVip = (Rms.loadRMSInt("serverchat") == 0);
				}
				if (Rms.loadRMSInt("isPlaySound") != -1)
				{
					GameCanvas.isPlaySound = (Rms.loadRMSInt("isPlaySound") == 1);
				}
				if (GameCanvas.isPlaySound)
				{
					SoundMn.gI().loadSound(TileMap.mapID);
				}
				SoundMn.gI().getStrOption();
				if (Rms.loadRMSInt("svselect") == -1)
				{
					string[] array = Res.split(ServerListScreen.linkDefault.Trim(), ",", 0);
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
					}
					GameCanvas.serverScr.switchToMe();
				}
				else
				{
					ServerListScreen.LoadIP();
				}
			}
			SplashScr.splashScrStat++;
			if (SplashScr.splashScrStat >= 150)
			{
				if (Session_ME.gI().isConnected())
				{
					ServerListScreen.loadScreen = true;
					GameCanvas.serverScreen.switchToMe();
					return;
				}
				mSystem.onDisconnected();
			}
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x00371398 File Offset: 0x0036F598
		public static void loadIP()
		{
			if (Rms.loadRMSInt("svselect") == -1)
			{
				int num = 0;
				if (mResources.language > 0)
				{
					for (int i = 0; i < (int)mResources.language; i++)
					{
						num += ServerListScreen.lengthServer[i];
					}
				}
				if (ServerListScreen.serverPriority == -1)
				{
					ServerListScreen.ipSelect = num + Res.random(0, ServerListScreen.lengthServer[(int)mResources.language]);
				}
				else
				{
					ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
				}
				Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
				GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
				GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
				mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
				LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
				GameCanvas.connect();
				return;
			}
			ServerListScreen.ipSelect = Rms.loadRMSInt("svselect");
			if (ServerListScreen.ipSelect > ServerListScreen.nameServer.Length - 1)
			{
				ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
				Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
			}
			GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
			GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			GameCanvas.connect();
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x003714D8 File Offset: 0x0036F6D8
		public override void paint(mGraphics g)
		{
			if (ModFunc.imgLogoBig != null && SplashScr.splashScrStat < 30)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				ModFunc.PaintLogoGif(g, GameCanvas.w / 2, GameCanvas.h / 2, 3);
			}
			if (SplashScr.nData != -1)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				ModFunc.PaintLogoGif(g, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
				if (ServerListScreen.cmdDeleteRMS != null)
				{
					mFont.tahoma_7_white.drawStringBorder(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
				}
				GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
				mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + (SplashScr.nData * 100 / SplashScr.maxData).ToString() + "%", GameCanvas.w / 2, GameCanvas.h / 2, 2);
				return;
			}
			if (SplashScr.splashScrStat >= 30)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.hh, g);
				if (ServerListScreen.cmdDeleteRMS != null)
				{
					mFont.tahoma_7_white.drawStringBorder(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
				}
			}
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x00371642 File Offset: 0x0036F842
		public static void LoadImg()
		{
			ModFunc.LoadLogoImages();
			ModFunc.LoadTickImages();
			ModFunc.LoadLogoGif();
			ModFunc.LoadImgMenuChat();
			ModFunc.LoadLogoGifMenu();
		}

		// Token: 0x04006D1F RID: 27935
		public static int splashScrStat;

		// Token: 0x04006D20 RID: 27936
		private bool isCheckConnect;

		// Token: 0x04006D21 RID: 27937
		public static int nData = -1;

		// Token: 0x04006D22 RID: 27938
		public static int maxData = -1;

		// Token: 0x04006D23 RID: 27939
		public static SplashScr instance;
	}
}
