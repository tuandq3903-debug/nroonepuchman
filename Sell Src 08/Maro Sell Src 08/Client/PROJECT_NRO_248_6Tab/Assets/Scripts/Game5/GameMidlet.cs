using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x02000116 RID: 278
	public class GameMidlet
	{
		// Token: 0x06000C46 RID: 3142 RVA: 0x000CA50A File Offset: 0x000C870A
		public GameMidlet()
		{
			this.initGame();
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000CA518 File Offset: 0x000C8718
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

		// Token: 0x06000C48 RID: 3144 RVA: 0x000CA587 File Offset: 0x000C8787
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

		// Token: 0x06000C49 RID: 3145 RVA: 0x000CA5A8 File Offset: 0x000C87A8
		public void notifyDestroyed()
		{
			Main.exit();
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000353AB File Offset: 0x000335AB
		public void platformRequest(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x040017B8 RID: 6072
		public static string IP = "";

		// Token: 0x040017B9 RID: 6073
		public static int PORT = 14445;

		// Token: 0x040017BA RID: 6074
		public static string IP2;

		// Token: 0x040017BB RID: 6075
		public static int PORT2;

		// Token: 0x040017BC RID: 6076
		public static sbyte PROVIDER;

		// Token: 0x040017BD RID: 6077
		public static int LANGUAGE;

		// Token: 0x040017BE RID: 6078
		public static string VERSION = (Rms.loadRMSString("version") == null) ? "2.4.8" : Rms.loadRMSString("version");

		// Token: 0x040017BF RID: 6079
		public static GameCanvas gameCanvas;

		// Token: 0x040017C0 RID: 6080
		public static GameMidlet instance;

		// Token: 0x040017C1 RID: 6081
		public static bool isConnect2;

		// Token: 0x040017C2 RID: 6082
		public static bool isBackWindowsPhone;
	}
}
