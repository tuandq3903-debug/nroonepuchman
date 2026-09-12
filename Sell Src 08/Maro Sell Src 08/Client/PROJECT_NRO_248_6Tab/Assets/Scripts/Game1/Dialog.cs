using System;

namespace Game1
{
	// Token: 0x02000463 RID: 1123
	public abstract class Dialog
	{
		// Token: 0x0600320C RID: 12812 RVA: 0x00313534 File Offset: 0x00311734
		public virtual void paint(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintz.paintTabSoft(g);
			GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x00313590 File Offset: 0x00311790
		public virtual void keyPress(int keyCode)
		{
			if (keyCode > -22)
			{
				if (keyCode != -21)
				{
					switch (keyCode)
					{
					case -7:
						goto IL_A9;
					case -6:
						goto IL_96;
					case -5:
						break;
					case -4:
					case -3:
						return;
					case -2:
						goto IL_6F;
					case -1:
						goto IL_48;
					default:
						if (keyCode != 10)
						{
							return;
						}
						break;
					}
					GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] = true;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
					return;
				}
				IL_96:
				GameCanvas.keyHold[12] = true;
				GameCanvas.keyPressed[12] = true;
				return;
			}
			if (keyCode == -39)
			{
				goto IL_6F;
			}
			if (keyCode != -38)
			{
				if (keyCode != -22)
				{
					return;
				}
				goto IL_A9;
			}
			IL_48:
			GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
			GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
			return;
			IL_6F:
			GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
			GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
			return;
			IL_A9:
			GameCanvas.keyHold[13] = true;
			GameCanvas.keyPressed[13] = true;
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x00313680 File Offset: 0x00311880
		public virtual void update()
		{
			if (this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center)))
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				GameCanvas.isPointerClick = false;
				mScreen.keyTouch = -1;
				GameCanvas.isPointerJustRelease = false;
				if (this.center != null)
				{
					this.center.performAction();
				}
				mScreen.keyTouch = -1;
			}
			if (this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left)))
			{
				GameCanvas.keyPressed[12] = false;
				GameCanvas.isPointerClick = false;
				mScreen.keyTouch = -1;
				GameCanvas.isPointerJustRelease = false;
				if (this.left != null)
				{
					this.left.performAction();
				}
				mScreen.keyTouch = -1;
			}
			if (this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
			{
				GameCanvas.keyPressed[13] = false;
				GameCanvas.isPointerClick = false;
				GameCanvas.isPointerJustRelease = false;
				mScreen.keyTouch = -1;
				if (this.right != null)
				{
					this.right.performAction();
				}
				mScreen.keyTouch = -1;
			}
			GameCanvas.clearKeyPressed();
			GameCanvas.clearKeyHold();
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void show()
		{
		}

		// Token: 0x04006014 RID: 24596
		public Command left;

		// Token: 0x04006015 RID: 24597
		public Command center;

		// Token: 0x04006016 RID: 24598
		public Command right;

		// Token: 0x04006017 RID: 24599
		private int lenCaption;
	}
}
