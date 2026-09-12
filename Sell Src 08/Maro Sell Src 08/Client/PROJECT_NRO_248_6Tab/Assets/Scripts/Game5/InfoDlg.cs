using System;

namespace Game5
{
	// Token: 0x02000122 RID: 290
	public class InfoDlg
	{
		// Token: 0x06000D27 RID: 3367 RVA: 0x000DA04B File Offset: 0x000D824B
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

		// Token: 0x06000D28 RID: 3368 RVA: 0x000DA068 File Offset: 0x000D8268
		public static void showWait()
		{
			InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
			InfoDlg.isLock = true;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000DA080 File Offset: 0x000D8280
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

		// Token: 0x06000D2A RID: 3370 RVA: 0x000DA173 File Offset: 0x000D8373
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

		// Token: 0x06000D2B RID: 3371 RVA: 0x000DA195 File Offset: 0x000D8395
		public static void hide()
		{
			InfoDlg.title = string.Empty;
			InfoDlg.subtitke = null;
			InfoDlg.isLock = false;
			InfoDlg.delay = 0;
			InfoDlg.isShow = false;
		}

		// Token: 0x040019A7 RID: 6567
		public static bool isShow;

		// Token: 0x040019A8 RID: 6568
		private static string title;

		// Token: 0x040019A9 RID: 6569
		private static string subtitke;

		// Token: 0x040019AA RID: 6570
		public static int delay;

		// Token: 0x040019AB RID: 6571
		public static bool isLock;
	}
}
