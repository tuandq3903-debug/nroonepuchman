using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x02000440 RID: 1088
	public class AdminPopup : IActionListener
	{
		// Token: 0x06003043 RID: 12355 RVA: 0x002EB6DC File Offset: 0x002E98DC
		public static AdminPopup gI()
		{
			if (AdminPopup.instance == null)
			{
				AdminPopup.instance = new AdminPopup();
			}
			return AdminPopup.instance;
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x002EB6F4 File Offset: 0x002E98F4
		public AdminPopup()
		{
			this.w = 320;
			this.h = GameCanvas.h / 2;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2 - 20;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x002EB75C File Offset: 0x002E995C
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

		// Token: 0x06003046 RID: 12358 RVA: 0x00002378 File Offset: 0x00000578
		public void perform(int idAction, object p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04005C95 RID: 23701
		private static AdminPopup instance;

		// Token: 0x04005C96 RID: 23702
		private int size;

		// Token: 0x04005C97 RID: 23703
		private int iconID;

		// Token: 0x04005C98 RID: 23704
		private bool isShow;

		// Token: 0x04005C99 RID: 23705
		private Scroll scr = new Scroll();

		// Token: 0x04005C9A RID: 23706
		private int x;

		// Token: 0x04005C9B RID: 23707
		private int y;

		// Token: 0x04005C9C RID: 23708
		private int w;

		// Token: 0x04005C9D RID: 23709
		private int h;
	}
}
