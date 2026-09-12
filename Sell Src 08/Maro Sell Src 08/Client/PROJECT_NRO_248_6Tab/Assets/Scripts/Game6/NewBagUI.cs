using System;

namespace Game6
{
	// Token: 0x02000085 RID: 133
	public class NewBagUI : IActionListener
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x0005BB3A File Offset: 0x00059D3A
		public static NewBagUI GI()
		{
			NewBagUI result;
			if ((result = NewBagUI.instance) == null)
			{
				result = (NewBagUI.instance = new NewBagUI());
			}
			return result;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0005BB50 File Offset: 0x00059D50
		public NewBagUI()
		{
			this.cmdClose = new Command(string.Empty, this, 0, null)
			{
				x = 40,
				y = 40,
				img = Panel.imgX,
				cmdClosePanel = true
			};
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0005BB8D File Offset: 0x00059D8D
		public void Paint(mGraphics g)
		{
			if (this.isShow)
			{
				PopUp.paintPopUp(g, 40, 40, GameCanvas.w - 80, GameCanvas.h - 80, -1, true);
				this.cmdClose.paint(g);
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0005BBBF File Offset: 0x00059DBF
		public void perform(int idAction, object p)
		{
			if (idAction == 0)
			{
				this.isShow = false;
			}
		}

		// Token: 0x04000CED RID: 3309
		private static NewBagUI instance;

		// Token: 0x04000CEE RID: 3310
		public bool isShow;

		// Token: 0x04000CEF RID: 3311
		private Command cmdClose;
	}
}
