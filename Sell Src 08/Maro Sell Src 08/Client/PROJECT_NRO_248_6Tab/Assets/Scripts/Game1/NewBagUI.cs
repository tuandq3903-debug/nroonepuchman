using System;

namespace Game1
{
	// Token: 0x020004BD RID: 1213
	public class NewBagUI : IActionListener
	{
		// Token: 0x060035C9 RID: 13769 RVA: 0x00344F32 File Offset: 0x00343132
		public static NewBagUI GI()
		{
			NewBagUI result;
			if ((result = NewBagUI.instance) == null)
			{
				result = (NewBagUI.instance = new NewBagUI());
			}
			return result;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x00344F48 File Offset: 0x00343148
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

		// Token: 0x060035CB RID: 13771 RVA: 0x00344F85 File Offset: 0x00343185
		public void Paint(mGraphics g)
		{
			if (this.isShow)
			{
				PopUp.paintPopUp(g, 40, 40, GameCanvas.w - 80, GameCanvas.h - 80, -1, true);
				this.cmdClose.paint(g);
			}
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x00344FB7 File Offset: 0x003431B7
		public void perform(int idAction, object p)
		{
			if (idAction == 0)
			{
				this.isShow = false;
			}
		}

		// Token: 0x04006968 RID: 26984
		private static NewBagUI instance;

		// Token: 0x04006969 RID: 26985
		public bool isShow;

		// Token: 0x0400696A RID: 26986
		private Command cmdClose;
	}
}
