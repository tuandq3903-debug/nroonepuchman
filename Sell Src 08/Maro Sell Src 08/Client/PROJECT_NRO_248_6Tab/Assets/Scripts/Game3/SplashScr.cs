using System;

namespace Game3
{
	// Token: 0x0200033B RID: 827
	public class SplashScr : mScreen
	{
		// Token: 0x0600254C RID: 9548 RVA: 0x00247084 File Offset: 0x00245284
		public SplashScr()
		{
			SplashScr.instance = this;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x00247092 File Offset: 0x00245292
		public static void loadSplashScr()
		{
			SplashScr.splashScrStat = 0;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x0024709C File Offset: 0x0024529C
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

		// Token: 0x0600254F RID: 9551 RVA: 0x00247250 File Offset: 0x00245450
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

		// Token: 0x06002550 RID: 9552 RVA: 0x00247390 File Offset: 0x00245590
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

		// Token: 0x06002551 RID: 9553 RVA: 0x002474FA File Offset: 0x002456FA
		public static void LoadImg()
		{
			ModFunc.LoadLogoImages();
			ModFunc.LoadTickImages();
			ModFunc.LoadLogoGif();
			ModFunc.LoadImgMenuChat();
			ModFunc.LoadLogoGifMenu();
		}

		// Token: 0x04004821 RID: 18465
		public static int splashScrStat;

		// Token: 0x04004822 RID: 18466
		private bool isCheckConnect;

		// Token: 0x04004823 RID: 18467
		public static int nData = -1;

		// Token: 0x04004824 RID: 18468
		public static int maxData = -1;

		// Token: 0x04004825 RID: 18469
		public static SplashScr instance;
	}
}
