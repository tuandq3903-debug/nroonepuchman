using System;

namespace Game2
{
	// Token: 0x02000417 RID: 1047
	public class TabCommand
	{
		// Token: 0x06002F03 RID: 12035 RVA: 0x002DE687 File Offset: 0x002DC887
		public TabCommand(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x002DE6AD File Offset: 0x002DC8AD
		public static void loadBG()
		{
			TabCommand.menu = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
			TabCommand.menu1 = GameCanvas.loadImage("/mainImage/myTexture2dnutf.png");
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x002DE6D0 File Offset: 0x002DC8D0
		public void paint(mGraphics g)
		{
			g.drawImage(this.isFocus ? TabCommand.menu1 : TabCommand.menu, this.x, this.y);
			mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + this.w / 2, this.y + this.h / 2 - mFont.tahoma_7b_dark.getHeight() / 2, 3);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x002DE740 File Offset: 0x002DC940
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

		// Token: 0x06002F07 RID: 12039 RVA: 0x002DE792 File Offset: 0x002DC992
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

		// Token: 0x04005AC7 RID: 23239
		private static Image menu;

		// Token: 0x04005AC8 RID: 23240
		private static Image menu1;

		// Token: 0x04005AC9 RID: 23241
		public string caption;

		// Token: 0x04005ACA RID: 23242
		public Action action;

		// Token: 0x04005ACB RID: 23243
		public int x;

		// Token: 0x04005ACC RID: 23244
		public int y;

		// Token: 0x04005ACD RID: 23245
		public int w = 32;

		// Token: 0x04005ACE RID: 23246
		public int h = 32;

		// Token: 0x04005ACF RID: 23247
		private bool isFocus;
	}
}
