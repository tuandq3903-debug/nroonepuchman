using System;

namespace Game1
{
	// Token: 0x02000482 RID: 1154
	public class InfoDlg
	{
		// Token: 0x060033B7 RID: 13239 RVA: 0x0032E2DB File Offset: 0x0032C4DB
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

		// Token: 0x060033B8 RID: 13240 RVA: 0x0032E2F8 File Offset: 0x0032C4F8
		public static void showWait()
		{
			InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
			InfoDlg.isLock = true;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x0032E310 File Offset: 0x0032C510
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

		// Token: 0x060033BA RID: 13242 RVA: 0x0032E403 File Offset: 0x0032C603
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

		// Token: 0x060033BB RID: 13243 RVA: 0x0032E425 File Offset: 0x0032C625
		public static void hide()
		{
			InfoDlg.title = string.Empty;
			InfoDlg.subtitke = null;
			InfoDlg.isLock = false;
			InfoDlg.delay = 0;
			InfoDlg.isShow = false;
		}

		// Token: 0x040063A3 RID: 25507
		public static bool isShow;

		// Token: 0x040063A4 RID: 25508
		private static string title;

		// Token: 0x040063A5 RID: 25509
		private static string subtitke;

		// Token: 0x040063A6 RID: 25510
		public static int delay;

		// Token: 0x040063A7 RID: 25511
		public static bool isLock;
	}
}
