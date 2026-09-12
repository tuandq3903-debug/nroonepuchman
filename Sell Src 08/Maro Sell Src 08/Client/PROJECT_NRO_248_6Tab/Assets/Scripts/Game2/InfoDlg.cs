using System;

namespace Game2
{
	// Token: 0x020003AA RID: 938
	public class InfoDlg
	{
		// Token: 0x06002A13 RID: 10771 RVA: 0x00299237 File Offset: 0x00297437
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

		// Token: 0x06002A14 RID: 10772 RVA: 0x00299254 File Offset: 0x00297454
		public static void showWait()
		{
			InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
			InfoDlg.isLock = true;
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x0029926C File Offset: 0x0029746C
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

		// Token: 0x06002A16 RID: 10774 RVA: 0x0029935F File Offset: 0x0029755F
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

		// Token: 0x06002A17 RID: 10775 RVA: 0x00299381 File Offset: 0x00297581
		public static void hide()
		{
			InfoDlg.title = string.Empty;
			InfoDlg.subtitke = null;
			InfoDlg.isLock = false;
			InfoDlg.delay = 0;
			InfoDlg.isShow = false;
		}

		// Token: 0x04005124 RID: 20772
		public static bool isShow;

		// Token: 0x04005125 RID: 20773
		private static string title;

		// Token: 0x04005126 RID: 20774
		private static string subtitke;

		// Token: 0x04005127 RID: 20775
		public static int delay;

		// Token: 0x04005128 RID: 20776
		public static bool isLock;
	}
}
