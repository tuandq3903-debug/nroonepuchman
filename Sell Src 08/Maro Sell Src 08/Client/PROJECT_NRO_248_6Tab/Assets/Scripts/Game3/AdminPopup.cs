using System;
using UnityEngine;

namespace Game3
{
	// Token: 0x02000290 RID: 656
	public class AdminPopup : IActionListener
	{
		// Token: 0x06001CFB RID: 7419 RVA: 0x001C1594 File Offset: 0x001BF794
		public static AdminPopup gI()
		{
			if (AdminPopup.instance == null)
			{
				AdminPopup.instance = new AdminPopup();
			}
			return AdminPopup.instance;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x001C15AC File Offset: 0x001BF7AC
		public AdminPopup()
		{
			this.w = 320;
			this.h = GameCanvas.h / 2;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2 - 20;
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x001C1614 File Offset: 0x001BF814
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

		// Token: 0x06001CFE RID: 7422 RVA: 0x00002378 File Offset: 0x00000578
		public void perform(int idAction, object p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04003797 RID: 14231
		private static AdminPopup instance;

		// Token: 0x04003798 RID: 14232
		private int size;

		// Token: 0x04003799 RID: 14233
		private int iconID;

		// Token: 0x0400379A RID: 14234
		private bool isShow;

		// Token: 0x0400379B RID: 14235
		private Scroll scr = new Scroll();

		// Token: 0x0400379C RID: 14236
		private int x;

		// Token: 0x0400379D RID: 14237
		private int y;

		// Token: 0x0400379E RID: 14238
		private int w;

		// Token: 0x0400379F RID: 14239
		private int h;
	}
}
