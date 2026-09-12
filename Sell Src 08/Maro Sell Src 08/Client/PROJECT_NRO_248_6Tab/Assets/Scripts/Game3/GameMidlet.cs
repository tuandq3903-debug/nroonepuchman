using System;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002C6 RID: 710
	public class GameMidlet
	{
		// Token: 0x06001F8E RID: 8078 RVA: 0x001F4652 File Offset: 0x001F2852
		public GameMidlet()
		{
			this.initGame();
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x001F4660 File Offset: 0x001F2860
		public void initGame()
		{
			GameMidlet.instance = this;
			MotherCanvas.instance = new MotherCanvas();
			Session_ME.gI().setHandler(Controller.gI());
			Session_ME2.gI().setHandler(Controller.gI());
			Session_ME2.isMainSession = false;
			GameMidlet.instance = this;
			GameMidlet.gameCanvas = new GameCanvas();
			GameMidlet.gameCanvas.start();
			SplashScr.LoadImg();
			SplashScr.loadSplashScr();
			GameCanvas.currentScreen = new SplashScr();
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x001F46CF File Offset: 0x001F28CF
		public void exit()
		{
			if (Main.typeClient == 6)
			{
				mSystem.exitWP();
				return;
			}
			GameCanvas.bRun = false;
			mSystem.gcc();
			this.notifyDestroyed();
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x001F46F0 File Offset: 0x001F28F0
		public void notifyDestroyed()
		{
			Main.exit();
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000353AB File Offset: 0x000335AB
		public void platformRequest(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x04003CB6 RID: 15542
		public static string IP = "";

		// Token: 0x04003CB7 RID: 15543
		public static int PORT = 14445;

		// Token: 0x04003CB8 RID: 15544
		public static string IP2;

		// Token: 0x04003CB9 RID: 15545
		public static int PORT2;

		// Token: 0x04003CBA RID: 15546
		public static sbyte PROVIDER;

		// Token: 0x04003CBB RID: 15547
		public static int LANGUAGE;

		// Token: 0x04003CBC RID: 15548
		public static string VERSION = (Rms.loadRMSString("version") == null) ? "2.4.8" : Rms.loadRMSString("version");

		// Token: 0x04003CBD RID: 15549
		public static GameCanvas gameCanvas;

		// Token: 0x04003CBE RID: 15550
		public static GameMidlet instance;

		// Token: 0x04003CBF RID: 15551
		public static bool isConnect2;

		// Token: 0x04003CC0 RID: 15552
		public static bool isBackWindowsPhone;
	}
}
