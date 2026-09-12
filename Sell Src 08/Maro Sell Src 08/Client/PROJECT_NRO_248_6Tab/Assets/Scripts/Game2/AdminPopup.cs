using System;
using UnityEngine;

namespace Game2
{
	// Token: 0x02000368 RID: 872
	public class AdminPopup : IActionListener
	{
		// Token: 0x0600269F RID: 9887 RVA: 0x00256638 File Offset: 0x00254838
		public static AdminPopup gI()
		{
			if (AdminPopup.instance == null)
			{
				AdminPopup.instance = new AdminPopup();
			}
			return AdminPopup.instance;
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x00256650 File Offset: 0x00254850
		public AdminPopup()
		{
			this.w = 320;
			this.h = GameCanvas.h / 2;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2 - 20;
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x002566B8 File Offset: 0x002548B8
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

		// Token: 0x060026A2 RID: 9890 RVA: 0x00002378 File Offset: 0x00000578
		public void perform(int idAction, object p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04004A16 RID: 18966
		private static AdminPopup instance;

		// Token: 0x04004A17 RID: 18967
		private int size;

		// Token: 0x04004A18 RID: 18968
		private int iconID;

		// Token: 0x04004A19 RID: 18969
		private bool isShow;

		// Token: 0x04004A1A RID: 18970
		private Scroll scr = new Scroll();

		// Token: 0x04004A1B RID: 18971
		private int x;

		// Token: 0x04004A1C RID: 18972
		private int y;

		// Token: 0x04004A1D RID: 18973
		private int w;

		// Token: 0x04004A1E RID: 18974
		private int h;
	}
}
