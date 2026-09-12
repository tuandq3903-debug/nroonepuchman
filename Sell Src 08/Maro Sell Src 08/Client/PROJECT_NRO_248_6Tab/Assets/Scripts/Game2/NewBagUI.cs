using System;

namespace Game2
{
	// Token: 0x020003E5 RID: 997
	public class NewBagUI : IActionListener
	{
		// Token: 0x06002C25 RID: 11301 RVA: 0x002AFE8E File Offset: 0x002AE08E
		public static NewBagUI GI()
		{
			NewBagUI result;
			if ((result = NewBagUI.instance) == null)
			{
				result = (NewBagUI.instance = new NewBagUI());
			}
			return result;
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x002AFEA4 File Offset: 0x002AE0A4
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

		// Token: 0x06002C27 RID: 11303 RVA: 0x002AFEE1 File Offset: 0x002AE0E1
		public void Paint(mGraphics g)
		{
			if (this.isShow)
			{
				PopUp.paintPopUp(g, 40, 40, GameCanvas.w - 80, GameCanvas.h - 80, -1, true);
				this.cmdClose.paint(g);
			}
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x002AFF13 File Offset: 0x002AE113
		public void perform(int idAction, object p)
		{
			if (idAction == 0)
			{
				this.isShow = false;
			}
		}

		// Token: 0x040056E9 RID: 22249
		private static NewBagUI instance;

		// Token: 0x040056EA RID: 22250
		public bool isShow;

		// Token: 0x040056EB RID: 22251
		private Command cmdClose;
	}
}
