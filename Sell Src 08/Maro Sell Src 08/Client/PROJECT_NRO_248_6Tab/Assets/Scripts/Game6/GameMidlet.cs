using System;
using UnityEngine;

namespace Game6
{
	// Token: 0x0200003E RID: 62
	public class GameMidlet
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00035306 File Offset: 0x00033506
		public GameMidlet()
		{
			this.initGame();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00035314 File Offset: 0x00033514
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

		// Token: 0x060002A4 RID: 676 RVA: 0x00035383 File Offset: 0x00033583
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

		// Token: 0x060002A5 RID: 677 RVA: 0x000353A4 File Offset: 0x000335A4
		public void notifyDestroyed()
		{
			Main.exit();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000353AB File Offset: 0x000335AB
		public void platformRequest(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x04000539 RID: 1337
		public static string IP = "";

		// Token: 0x0400053A RID: 1338
		public static int PORT = 14445;

		// Token: 0x0400053B RID: 1339
		public static string IP2;

		// Token: 0x0400053C RID: 1340
		public static int PORT2;

		// Token: 0x0400053D RID: 1341
		public static sbyte PROVIDER;

		// Token: 0x0400053E RID: 1342
		public static int LANGUAGE;

		// Token: 0x0400053F RID: 1343
		public static string VERSION = (Rms.loadRMSString("version") == null) ? "2.4.8" : Rms.loadRMSString("version");

		// Token: 0x04000540 RID: 1344
		public static GameCanvas gameCanvas;

		// Token: 0x04000541 RID: 1345
		public static GameMidlet instance;

		// Token: 0x04000542 RID: 1346
		public static bool isConnect2;

		// Token: 0x04000543 RID: 1347
		public static bool isBackWindowsPhone;
	}
}
