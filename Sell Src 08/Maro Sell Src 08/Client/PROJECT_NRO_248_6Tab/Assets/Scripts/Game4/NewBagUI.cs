using System;

namespace Game4
{
	// Token: 0x02000235 RID: 565
	public class NewBagUI : IActionListener
	{
		// Token: 0x060018DD RID: 6365 RVA: 0x00185D46 File Offset: 0x00183F46
		public static NewBagUI GI()
		{
			NewBagUI result;
			if ((result = NewBagUI.instance) == null)
			{
				result = (NewBagUI.instance = new NewBagUI());
			}
			return result;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00185D5C File Offset: 0x00183F5C
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

		// Token: 0x060018DF RID: 6367 RVA: 0x00185D99 File Offset: 0x00183F99
		public void Paint(mGraphics g)
		{
			if (this.isShow)
			{
				PopUp.paintPopUp(g, 40, 40, GameCanvas.w - 80, GameCanvas.h - 80, -1, true);
				this.cmdClose.paint(g);
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00185DCB File Offset: 0x00183FCB
		public void perform(int idAction, object p)
		{
			if (idAction == 0)
			{
				this.isShow = false;
			}
		}

		// Token: 0x040031EB RID: 12779
		private static NewBagUI instance;

		// Token: 0x040031EC RID: 12780
		public bool isShow;

		// Token: 0x040031ED RID: 12781
		private Command cmdClose;
	}
}
