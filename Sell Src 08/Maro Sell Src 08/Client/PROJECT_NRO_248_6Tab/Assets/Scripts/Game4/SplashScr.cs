using System;

namespace Game4
{
	// Token: 0x02000263 RID: 611
	public class SplashScr : mScreen
	{
		// Token: 0x06001BA8 RID: 7080 RVA: 0x001B1FE0 File Offset: 0x001B01E0
		public SplashScr()
		{
			SplashScr.instance = this;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x001B1FEE File Offset: 0x001B01EE
		public static void loadSplashScr()
		{
			SplashScr.splashScrStat = 0;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x001B1FF8 File Offset: 0x001B01F8
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

		// Token: 0x06001BAB RID: 7083 RVA: 0x001B21AC File Offset: 0x001B03AC
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

		// Token: 0x06001BAC RID: 7084 RVA: 0x001B22EC File Offset: 0x001B04EC
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

		// Token: 0x06001BAD RID: 7085 RVA: 0x001B2456 File Offset: 0x001B0656
		public static void LoadImg()
		{
			ModFunc.LoadLogoImages();
			ModFunc.LoadTickImages();
			ModFunc.LoadLogoGif();
			ModFunc.LoadImgMenuChat();
			ModFunc.LoadLogoGifMenu();
		}

		// Token: 0x040035A2 RID: 13730
		public static int splashScrStat;

		// Token: 0x040035A3 RID: 13731
		private bool isCheckConnect;

		// Token: 0x040035A4 RID: 13732
		public static int nData = -1;

		// Token: 0x040035A5 RID: 13733
		public static int maxData = -1;

		// Token: 0x040035A6 RID: 13734
		public static SplashScr instance;
	}
}
