using System;

namespace Game3
{
	// Token: 0x020002D2 RID: 722
	public class InfoDlg
	{
		// Token: 0x0600206F RID: 8303 RVA: 0x00204193 File Offset: 0x00202393
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

		// Token: 0x06002070 RID: 8304 RVA: 0x002041B0 File Offset: 0x002023B0
		public static void showWait()
		{
			InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
			InfoDlg.isLock = true;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x002041C8 File Offset: 0x002023C8
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

		// Token: 0x06002072 RID: 8306 RVA: 0x002042BB File Offset: 0x002024BB
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

		// Token: 0x06002073 RID: 8307 RVA: 0x002042DD File Offset: 0x002024DD
		public static void hide()
		{
			InfoDlg.title = string.Empty;
			InfoDlg.subtitke = null;
			InfoDlg.isLock = false;
			InfoDlg.delay = 0;
			InfoDlg.isShow = false;
		}

		// Token: 0x04003EA5 RID: 16037
		public static bool isShow;

		// Token: 0x04003EA6 RID: 16038
		private static string title;

		// Token: 0x04003EA7 RID: 16039
		private static string subtitke;

		// Token: 0x04003EA8 RID: 16040
		public static int delay;

		// Token: 0x04003EA9 RID: 16041
		public static bool isLock;
	}
}
