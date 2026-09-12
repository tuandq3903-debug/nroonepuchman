using System;

namespace Game3
{
	// Token: 0x0200033F RID: 831
	public class TabCommand
	{
		// Token: 0x0600255F RID: 9567 RVA: 0x002495E3 File Offset: 0x002477E3
		public TabCommand(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00249609 File Offset: 0x00247809
		public static void loadBG()
		{
			TabCommand.menu = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
			TabCommand.menu1 = GameCanvas.loadImage("/mainImage/myTexture2dnutf.png");
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x0024962C File Offset: 0x0024782C
		public void paint(mGraphics g)
		{
			g.drawImage(this.isFocus ? TabCommand.menu1 : TabCommand.menu, this.x, this.y);
			mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + this.w / 2, this.y + this.h / 2 - mFont.tahoma_7b_dark.getHeight() / 2, 3);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x0024969C File Offset: 0x0024789C
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

		// Token: 0x06002563 RID: 9571 RVA: 0x002496EE File Offset: 0x002478EE
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

		// Token: 0x04004848 RID: 18504
		private static Image menu;

		// Token: 0x04004849 RID: 18505
		private static Image menu1;

		// Token: 0x0400484A RID: 18506
		public string caption;

		// Token: 0x0400484B RID: 18507
		public Action action;

		// Token: 0x0400484C RID: 18508
		public int x;

		// Token: 0x0400484D RID: 18509
		public int y;

		// Token: 0x0400484E RID: 18510
		public int w = 32;

		// Token: 0x0400484F RID: 18511
		public int h = 32;

		// Token: 0x04004850 RID: 18512
		private bool isFocus;
	}
}
