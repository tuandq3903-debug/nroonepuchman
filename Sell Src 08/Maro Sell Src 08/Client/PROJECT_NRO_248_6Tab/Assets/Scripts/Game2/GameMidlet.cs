using System;
using UnityEngine;

namespace Game2
{
	// Token: 0x0200039E RID: 926
	public class GameMidlet
	{
		// Token: 0x06002932 RID: 10546 RVA: 0x002896F6 File Offset: 0x002878F6
		public GameMidlet()
		{
			this.initGame();
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x00289704 File Offset: 0x00287904
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

		// Token: 0x06002934 RID: 10548 RVA: 0x00289773 File Offset: 0x00287973
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

		// Token: 0x06002935 RID: 10549 RVA: 0x00289794 File Offset: 0x00287994
		public void notifyDestroyed()
		{
			Main.exit();
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000353AB File Offset: 0x000335AB
		public void platformRequest(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x04004F35 RID: 20277
		public static string IP = "";

		// Token: 0x04004F36 RID: 20278
		public static int PORT = 14445;

		// Token: 0x04004F37 RID: 20279
		public static string IP2;

		// Token: 0x04004F38 RID: 20280
		public static int PORT2;

		// Token: 0x04004F39 RID: 20281
		public static sbyte PROVIDER;

		// Token: 0x04004F3A RID: 20282
		public static int LANGUAGE;

		// Token: 0x04004F3B RID: 20283
		public static string VERSION = (Rms.loadRMSString("version") == null) ? "2.4.8" : Rms.loadRMSString("version");

		// Token: 0x04004F3C RID: 20284
		public static GameCanvas gameCanvas;

		// Token: 0x04004F3D RID: 20285
		public static GameMidlet instance;

		// Token: 0x04004F3E RID: 20286
		public static bool isConnect2;

		// Token: 0x04004F3F RID: 20287
		public static bool isBackWindowsPhone;
	}
}
