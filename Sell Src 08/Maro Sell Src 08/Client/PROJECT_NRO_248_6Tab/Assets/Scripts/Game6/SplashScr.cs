using System;

namespace Game6
{
	// Token: 0x020000B3 RID: 179
	public class SplashScr : mScreen
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x00087E30 File Offset: 0x00086030
		public SplashScr()
		{
			SplashScr.instance = this;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00087E3E File Offset: 0x0008603E
		public static void loadSplashScr()
		{
			SplashScr.splashScrStat = 0;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00087E48 File Offset: 0x00086048
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

		// Token: 0x06000863 RID: 2147 RVA: 0x00087FFC File Offset: 0x000861FC
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

		// Token: 0x06000864 RID: 2148 RVA: 0x0008813C File Offset: 0x0008633C
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

		// Token: 0x06000865 RID: 2149 RVA: 0x000882A6 File Offset: 0x000864A6
		public static void LoadImg()
		{
			ModFunc.LoadLogoImages();
			ModFunc.LoadTickImages();
			ModFunc.LoadLogoGif();
			ModFunc.LoadImgMenuChat();
			ModFunc.LoadLogoGifMenu();
		}

		// Token: 0x040010A4 RID: 4260
		public static int splashScrStat;

		// Token: 0x040010A5 RID: 4261
		private bool isCheckConnect;

		// Token: 0x040010A6 RID: 4262
		public static int nData = -1;

		// Token: 0x040010A7 RID: 4263
		public static int maxData = -1;

		// Token: 0x040010A8 RID: 4264
		public static SplashScr instance;
	}
}
