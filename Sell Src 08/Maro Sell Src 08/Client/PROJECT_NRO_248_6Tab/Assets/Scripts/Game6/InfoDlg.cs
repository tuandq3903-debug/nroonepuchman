using System;

namespace Game6
{
	// Token: 0x0200004A RID: 74
	public class InfoDlg
	{
		// Token: 0x06000383 RID: 899 RVA: 0x00044E9F File Offset: 0x0004309F
		public static void show(string title, string subtitle, int delay)
		{
			if (title != null)
			{
				InfoDlg.isShow = true;
				InfoDlg.title = title;
				InfoDlg.subtitke = subtitle;
				InfoDlg.delay = delay;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00044EBC File Offset: 0x000430BC
		public static void showWait()
		{
			InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
			InfoDlg.isLock = true;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00044ED4 File Offset: 0x000430D4
		public static void paint(mGraphics g)
		{
			if (InfoDlg.isShow && (!InfoDlg.isLock || InfoDlg.delay <= 4990) && !GameScr.isPaintAlert)
			{
				int num = 10;
				GameCanvas.paintz.paintPopUp(GameCanvas.hw - 75, num, 150, 55, g);
				if (InfoDlg.isLock)
				{
					GameCanvas.paintShukiren(GameCanvas.hw - mFont.tahoma_8b.getWidth(InfoDlg.title) / 2 - 10, num + 28, g);
					mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw + 5, num + 21, 2);
					return;
				}
				if (InfoDlg.subtitke != null)
				{
					mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 13, 2);
					mFont.tahoma_7_green2.drawString(g, InfoDlg.subtitke, GameCanvas.hw, num + 30, 2);
					return;
				}
				mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 21, 2);
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00044FC7 File Offset: 0x000431C7
		public static void update()
		{
			if (InfoDlg.delay > 0)
			{
				InfoDlg.delay--;
				if (InfoDlg.delay == 0)
				{
					InfoDlg.hide();
				}
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00044FE9 File Offset: 0x000431E9
		public static void hide()
		{
			InfoDlg.title = string.Empty;
			InfoDlg.subtitke = null;
			InfoDlg.isLock = false;
			InfoDlg.delay = 0;
			InfoDlg.isShow = false;
		}

		// Token: 0x04000728 RID: 1832
		public static bool isShow;

		// Token: 0x04000729 RID: 1833
		private static string title;

		// Token: 0x0400072A RID: 1834
		private static string subtitke;

		// Token: 0x0400072B RID: 1835
		public static int delay;

		// Token: 0x0400072C RID: 1836
		public static bool isLock;
	}
}
