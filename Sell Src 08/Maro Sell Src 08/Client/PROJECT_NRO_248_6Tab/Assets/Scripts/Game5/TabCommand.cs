using System;

namespace Game5
{
	// Token: 0x0200018F RID: 399
	public class TabCommand
	{
		// Token: 0x06001217 RID: 4631 RVA: 0x0011F49B File Offset: 0x0011D69B
		public TabCommand(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0011F4C1 File Offset: 0x0011D6C1
		public static void loadBG()
		{
			TabCommand.menu = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
			TabCommand.menu1 = GameCanvas.loadImage("/mainImage/myTexture2dnutf.png");
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0011F4E4 File Offset: 0x0011D6E4
		public void paint(mGraphics g)
		{
			g.drawImage(this.isFocus ? TabCommand.menu1 : TabCommand.menu, this.x, this.y);
			mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + this.w / 2, this.y + this.h / 2 - mFont.tahoma_7b_dark.getHeight() / 2, 3);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0011F554 File Offset: 0x0011D754
		public bool isPointerInside()
		{
			this.isFocus = false;
			if (GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h))
			{
				if (GameCanvas.isPointerDown)
				{
					this.isFocus = true;
				}
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0011F5A6 File Offset: 0x0011D7A6
		public void Invoke()
		{
			GameCanvas.clearAllPointerEvent();
			Action action = this.action;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0400234A RID: 9034
		private static Image menu;

		// Token: 0x0400234B RID: 9035
		private static Image menu1;

		// Token: 0x0400234C RID: 9036
		public string caption;

		// Token: 0x0400234D RID: 9037
		public Action action;

		// Token: 0x0400234E RID: 9038
		public int x;

		// Token: 0x0400234F RID: 9039
		public int y;

		// Token: 0x04002350 RID: 9040
		public int w = 32;

		// Token: 0x04002351 RID: 9041
		public int h = 32;

		// Token: 0x04002352 RID: 9042
		private bool isFocus;
	}
}
