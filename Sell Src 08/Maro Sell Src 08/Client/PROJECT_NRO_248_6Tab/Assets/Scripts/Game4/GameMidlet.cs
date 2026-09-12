using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x020001EE RID: 494
	public class GameMidlet
	{
		// Token: 0x060015EA RID: 5610 RVA: 0x0015F5AE File Offset: 0x0015D7AE
		public GameMidlet()
		{
			this.initGame();
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0015F5BC File Offset: 0x0015D7BC
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

		// Token: 0x060015EC RID: 5612 RVA: 0x0015F62B File Offset: 0x0015D82B
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

		// Token: 0x060015ED RID: 5613 RVA: 0x0015F64C File Offset: 0x0015D84C
		public void notifyDestroyed()
		{
			Main.exit();
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000353AB File Offset: 0x000335AB
		public void platformRequest(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x04002A37 RID: 10807
		public static string IP = "";

		// Token: 0x04002A38 RID: 10808
		public static int PORT = 14445;

		// Token: 0x04002A39 RID: 10809
		public static string IP2;

		// Token: 0x04002A3A RID: 10810
		public static int PORT2;

		// Token: 0x04002A3B RID: 10811
		public static sbyte PROVIDER;

		// Token: 0x04002A3C RID: 10812
		public static int LANGUAGE;

		// Token: 0x04002A3D RID: 10813
		public static string VERSION = (Rms.loadRMSString("version") == null) ? "2.4.8" : Rms.loadRMSString("version");

		// Token: 0x04002A3E RID: 10814
		public static GameCanvas gameCanvas;

		// Token: 0x04002A3F RID: 10815
		public static GameMidlet instance;

		// Token: 0x04002A40 RID: 10816
		public static bool isConnect2;

		// Token: 0x04002A41 RID: 10817
		public static bool isBackWindowsPhone;
	}
}
