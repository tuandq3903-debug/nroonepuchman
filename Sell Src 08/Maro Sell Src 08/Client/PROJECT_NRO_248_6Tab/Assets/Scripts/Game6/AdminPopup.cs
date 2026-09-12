using System;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000008 RID: 8
	public class AdminPopup : IActionListener
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000224A File Offset: 0x0000044A
		public static AdminPopup gI()
		{
			if (AdminPopup.instance == null)
			{
				AdminPopup.instance = new AdminPopup();
			}
			return AdminPopup.instance;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002264 File Offset: 0x00000464
		public AdminPopup()
		{
			this.w = 320;
			this.h = GameCanvas.h / 2;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2 - 20;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022CC File Offset: 0x000004CC
		public void paint(mGraphics g)
		{
			if (!this.isShow)
			{
				return;
			}
			try
			{
				PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, 16777215, false);
				g.translate(0, -this.scr.cmy);
				for (int i = 0; i < this.size; i++)
				{
					SmallImage.drawSmallImage(g, this.iconID, this.x + 5, this.y + 20 * i, 0, 0);
				}
				g.translate(0, this.scr.cmy);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002378 File Offset: 0x00000578
		public void perform(int idAction, object p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400001B RID: 27
		private static AdminPopup instance;

		// Token: 0x0400001C RID: 28
		private int size;

		// Token: 0x0400001D RID: 29
		private int iconID;

		// Token: 0x0400001E RID: 30
		private bool isShow;

		// Token: 0x0400001F RID: 31
		private Scroll scr = new Scroll();

		// Token: 0x04000020 RID: 32
		private int x;

		// Token: 0x04000021 RID: 33
		private int y;

		// Token: 0x04000022 RID: 34
		private int w;

		// Token: 0x04000023 RID: 35
		private int h;
	}
}
