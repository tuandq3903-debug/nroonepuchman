using System;

namespace Game5
{
	// Token: 0x0200015D RID: 349
	public class NewBagUI : IActionListener
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x000F0CA2 File Offset: 0x000EEEA2
		public static NewBagUI GI()
		{
			NewBagUI result;
			if ((result = NewBagUI.instance) == null)
			{
				result = (NewBagUI.instance = new NewBagUI());
			}
			return result;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000F0CB8 File Offset: 0x000EEEB8
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

		// Token: 0x06000F3B RID: 3899 RVA: 0x000F0CF5 File Offset: 0x000EEEF5
		public void Paint(mGraphics g)
		{
			if (this.isShow)
			{
				PopUp.paintPopUp(g, 40, 40, GameCanvas.w - 80, GameCanvas.h - 80, -1, true);
				this.cmdClose.paint(g);
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x000F0D27 File Offset: 0x000EEF27
		public void perform(int idAction, object p)
		{
			if (idAction == 0)
			{
				this.isShow = false;
			}
		}

		// Token: 0x04001F6C RID: 8044
		private static NewBagUI instance;

		// Token: 0x04001F6D RID: 8045
		public bool isShow;

		// Token: 0x04001F6E RID: 8046
		private Command cmdClose;
	}
}
