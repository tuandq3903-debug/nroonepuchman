using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x020001B8 RID: 440
	public class AdminPopup : IActionListener
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x0012C4F0 File Offset: 0x0012A6F0
		public static AdminPopup gI()
		{
			if (AdminPopup.instance == null)
			{
				AdminPopup.instance = new AdminPopup();
			}
			return AdminPopup.instance;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0012C508 File Offset: 0x0012A708
		public AdminPopup()
		{
			this.w = 320;
			this.h = GameCanvas.h / 2;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2 - 20;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0012C570 File Offset: 0x0012A770
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

		// Token: 0x0600135A RID: 4954 RVA: 0x00002378 File Offset: 0x00000578
		public void perform(int idAction, object p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002518 RID: 9496
		private static AdminPopup instance;

		// Token: 0x04002519 RID: 9497
		private int size;

		// Token: 0x0400251A RID: 9498
		private int iconID;

		// Token: 0x0400251B RID: 9499
		private bool isShow;

		// Token: 0x0400251C RID: 9500
		private Scroll scr = new Scroll();

		// Token: 0x0400251D RID: 9501
		private int x;

		// Token: 0x0400251E RID: 9502
		private int y;

		// Token: 0x0400251F RID: 9503
		private int w;

		// Token: 0x04002520 RID: 9504
		private int h;
	}
}
