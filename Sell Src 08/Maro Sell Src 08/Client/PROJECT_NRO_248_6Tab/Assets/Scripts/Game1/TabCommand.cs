using System;

namespace Game1
{
	// Token: 0x020004EF RID: 1263
	public class TabCommand
	{
		// Token: 0x060038A7 RID: 14503 RVA: 0x0037372B File Offset: 0x0037192B
		public TabCommand(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x00373751 File Offset: 0x00371951
		public static void loadBG()
		{
			TabCommand.menu = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
			TabCommand.menu1 = GameCanvas.loadImage("/mainImage/myTexture2dnutf.png");
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x00373774 File Offset: 0x00371974
		public void paint(mGraphics g)
		{
			g.drawImage(this.isFocus ? TabCommand.menu1 : TabCommand.menu, this.x, this.y);
			mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + this.w / 2, this.y + this.h / 2 - mFont.tahoma_7b_dark.getHeight() / 2, 3);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x003737E4 File Offset: 0x003719E4
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

		// Token: 0x060038AB RID: 14507 RVA: 0x00373836 File Offset: 0x00371A36
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

		// Token: 0x04006D46 RID: 27974
		private static Image menu;

		// Token: 0x04006D47 RID: 27975
		private static Image menu1;

		// Token: 0x04006D48 RID: 27976
		public string caption;

		// Token: 0x04006D49 RID: 27977
		public Action action;

		// Token: 0x04006D4A RID: 27978
		public int x;

		// Token: 0x04006D4B RID: 27979
		public int y;

		// Token: 0x04006D4C RID: 27980
		public int w = 32;

		// Token: 0x04006D4D RID: 27981
		public int h = 32;

		// Token: 0x04006D4E RID: 27982
		private bool isFocus;
	}
}
