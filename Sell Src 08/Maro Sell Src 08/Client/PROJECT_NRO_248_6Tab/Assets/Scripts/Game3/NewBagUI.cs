using System;

namespace Game3
{
	// Token: 0x0200030D RID: 781
	public class NewBagUI : IActionListener
	{
		// Token: 0x06002281 RID: 8833 RVA: 0x0021ADEA File Offset: 0x00218FEA
		public static NewBagUI GI()
		{
			NewBagUI result;
			if ((result = NewBagUI.instance) == null)
			{
				result = (NewBagUI.instance = new NewBagUI());
			}
			return result;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0021AE00 File Offset: 0x00219000
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

		// Token: 0x06002283 RID: 8835 RVA: 0x0021AE3D File Offset: 0x0021903D
		public void Paint(mGraphics g)
		{
			if (this.isShow)
			{
				PopUp.paintPopUp(g, 40, 40, GameCanvas.w - 80, GameCanvas.h - 80, -1, true);
				this.cmdClose.paint(g);
			}
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0021AE6F File Offset: 0x0021906F
		public void perform(int idAction, object p)
		{
			if (idAction == 0)
			{
				this.isShow = false;
			}
		}

		// Token: 0x0400446A RID: 17514
		private static NewBagUI instance;

		// Token: 0x0400446B RID: 17515
		public bool isShow;

		// Token: 0x0400446C RID: 17516
		private Command cmdClose;
	}
}
