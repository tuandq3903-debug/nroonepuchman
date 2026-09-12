using System;

namespace Game4
{
	// Token: 0x02000267 RID: 615
	public class TabCommand
	{
		// Token: 0x06001BBB RID: 7099 RVA: 0x001B453F File Offset: 0x001B273F
		public TabCommand(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x001B4565 File Offset: 0x001B2765
		public static void loadBG()
		{
			TabCommand.menu = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
			TabCommand.menu1 = GameCanvas.loadImage("/mainImage/myTexture2dnutf.png");
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x001B4588 File Offset: 0x001B2788
		public void paint(mGraphics g)
		{
			g.drawImage(this.isFocus ? TabCommand.menu1 : TabCommand.menu, this.x, this.y);
			mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + this.w / 2, this.y + this.h / 2 - mFont.tahoma_7b_dark.getHeight() / 2, 3);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x001B45F8 File Offset: 0x001B27F8
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

		// Token: 0x06001BBF RID: 7103 RVA: 0x001B464A File Offset: 0x001B284A
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

		// Token: 0x040035C9 RID: 13769
		private static Image menu;

		// Token: 0x040035CA RID: 13770
		private static Image menu1;

		// Token: 0x040035CB RID: 13771
		public string caption;

		// Token: 0x040035CC RID: 13772
		public Action action;

		// Token: 0x040035CD RID: 13773
		public int x;

		// Token: 0x040035CE RID: 13774
		public int y;

		// Token: 0x040035CF RID: 13775
		public int w = 32;

		// Token: 0x040035D0 RID: 13776
		public int h = 32;

		// Token: 0x040035D1 RID: 13777
		private bool isFocus;
	}
}
