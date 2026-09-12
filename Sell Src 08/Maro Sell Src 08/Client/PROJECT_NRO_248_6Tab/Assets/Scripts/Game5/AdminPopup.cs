using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x020000E0 RID: 224
	public class AdminPopup : IActionListener
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x0009744C File Offset: 0x0009564C
		public static AdminPopup gI()
		{
			if (AdminPopup.instance == null)
			{
				AdminPopup.instance = new AdminPopup();
			}
			return AdminPopup.instance;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00097464 File Offset: 0x00095664
		public AdminPopup()
		{
			this.w = 320;
			this.h = GameCanvas.h / 2;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2 - 20;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000974CC File Offset: 0x000956CC
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

		// Token: 0x060009B6 RID: 2486 RVA: 0x00002378 File Offset: 0x00000578
		public void perform(int idAction, object p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001299 RID: 4761
		private static AdminPopup instance;

		// Token: 0x0400129A RID: 4762
		private int size;

		// Token: 0x0400129B RID: 4763
		private int iconID;

		// Token: 0x0400129C RID: 4764
		private bool isShow;

		// Token: 0x0400129D RID: 4765
		private Scroll scr = new Scroll();

		// Token: 0x0400129E RID: 4766
		private int x;

		// Token: 0x0400129F RID: 4767
		private int y;

		// Token: 0x040012A0 RID: 4768
		private int w;

		// Token: 0x040012A1 RID: 4769
		private int h;
	}
}
