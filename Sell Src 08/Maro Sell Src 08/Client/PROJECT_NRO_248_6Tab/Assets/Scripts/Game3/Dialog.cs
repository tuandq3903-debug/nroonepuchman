using System;

namespace Game3
{
	// Token: 0x020002B3 RID: 691
	public abstract class Dialog
	{
		// Token: 0x06001EC4 RID: 7876 RVA: 0x001E93EC File Offset: 0x001E75EC
		public virtual void paint(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintz.paintTabSoft(g);
			GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x001E9448 File Offset: 0x001E7648
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

		// Token: 0x06001EC6 RID: 7878 RVA: 0x001E9538 File Offset: 0x001E7738
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

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void show()
		{
		}

		// Token: 0x04003B16 RID: 15126
		public Command left;

		// Token: 0x04003B17 RID: 15127
		public Command center;

		// Token: 0x04003B18 RID: 15128
		public Command right;

		// Token: 0x04003B19 RID: 15129
		private int lenCaption;
	}
}
