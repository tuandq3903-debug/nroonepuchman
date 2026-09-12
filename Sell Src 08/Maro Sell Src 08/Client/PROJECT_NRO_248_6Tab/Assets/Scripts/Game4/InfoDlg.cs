using System;

namespace Game4
{
	// Token: 0x020001FA RID: 506
	public class InfoDlg
	{
		// Token: 0x060016CB RID: 5835 RVA: 0x0016F0EF File Offset: 0x0016D2EF
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

		// Token: 0x060016CC RID: 5836 RVA: 0x0016F10C File Offset: 0x0016D30C
		public static void showWait()
		{
			InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
			InfoDlg.isLock = true;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0016F124 File Offset: 0x0016D324
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

		// Token: 0x060016CE RID: 5838 RVA: 0x0016F217 File Offset: 0x0016D417
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

		// Token: 0x060016CF RID: 5839 RVA: 0x0016F239 File Offset: 0x0016D439
		public static void hide()
		{
			InfoDlg.title = string.Empty;
			InfoDlg.subtitke = null;
			InfoDlg.isLock = false;
			InfoDlg.delay = 0;
			InfoDlg.isShow = false;
		}

		// Token: 0x04002C26 RID: 11302
		public static bool isShow;

		// Token: 0x04002C27 RID: 11303
		private static string title;

		// Token: 0x04002C28 RID: 11304
		private static string subtitke;

		// Token: 0x04002C29 RID: 11305
		public static int delay;

		// Token: 0x04002C2A RID: 11306
		public static bool isLock;
	}
}
