using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x02000476 RID: 1142
	public class GameMidlet
	{
		// Token: 0x060032D6 RID: 13014 RVA: 0x0031E79A File Offset: 0x0031C99A
		public GameMidlet()
		{
			this.initGame();
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x0031E7A8 File Offset: 0x0031C9A8
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

		// Token: 0x060032D8 RID: 13016 RVA: 0x0031E817 File Offset: 0x0031CA17
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

		// Token: 0x060032D9 RID: 13017 RVA: 0x0031E838 File Offset: 0x0031CA38
		public void notifyDestroyed()
		{
			Main.exit();
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000353AB File Offset: 0x000335AB
		public void platformRequest(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x040061B4 RID: 25012
		public static string IP = "";

		// Token: 0x040061B5 RID: 25013
		public static int PORT = 14445;

		// Token: 0x040061B6 RID: 25014
		public static string IP2;

		// Token: 0x040061B7 RID: 25015
		public static int PORT2;

		// Token: 0x040061B8 RID: 25016
		public static sbyte PROVIDER;

		// Token: 0x040061B9 RID: 25017
		public static int LANGUAGE;

		// Token: 0x040061BA RID: 25018
		public static string VERSION = (Rms.loadRMSString("version") == null) ? "2.4.8" : Rms.loadRMSString("version");

		// Token: 0x040061BB RID: 25019
		public static GameCanvas gameCanvas;

		// Token: 0x040061BC RID: 25020
		public static GameMidlet instance;

		// Token: 0x040061BD RID: 25021
		public static bool isConnect2;

		// Token: 0x040061BE RID: 25022
		public static bool isBackWindowsPhone;
	}
}
