using System;

namespace Game6
{
	// Token: 0x020000B7 RID: 183
	public class TabCommand
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x0008A38F File Offset: 0x0008858F
		public TabCommand(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0008A3B5 File Offset: 0x000885B5
		public static void loadBG()
		{
			TabCommand.menu = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
			TabCommand.menu1 = GameCanvas.loadImage("/mainImage/myTexture2dnutf.png");
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0008A3D8 File Offset: 0x000885D8
		public void paint(mGraphics g)
		{
			g.drawImage(this.isFocus ? TabCommand.menu1 : TabCommand.menu, this.x, this.y);
			mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + this.w / 2, this.y + this.h / 2 - mFont.tahoma_7b_dark.getHeight() / 2, 3);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0008A448 File Offset: 0x00088648
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

		// Token: 0x06000877 RID: 2167 RVA: 0x0008A49A File Offset: 0x0008869A
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

		// Token: 0x040010CB RID: 4299
		private static Image menu;

		// Token: 0x040010CC RID: 4300
		private static Image menu1;

		// Token: 0x040010CD RID: 4301
		public string caption;

		// Token: 0x040010CE RID: 4302
		public Action action;

		// Token: 0x040010CF RID: 4303
		public int x;

		// Token: 0x040010D0 RID: 4304
		public int y;

		// Token: 0x040010D1 RID: 4305
		public int w = 32;

		// Token: 0x040010D2 RID: 4306
		public int h = 32;

		// Token: 0x040010D3 RID: 4307
		private bool isFocus;
	}
}
