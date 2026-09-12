using System;

namespace Game2
{
	// Token: 0x0200038B RID: 907
	public abstract class Dialog
	{
		// Token: 0x06002868 RID: 10344 RVA: 0x0027E490 File Offset: 0x0027C690
		public virtual void paint(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintz.paintTabSoft(g);
			GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x0027E4EC File Offset: 0x0027C6EC
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

		// Token: 0x0600286A RID: 10346 RVA: 0x0027E5DC File Offset: 0x0027C7DC
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

		// Token: 0x0600286B RID: 10347 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void show()
		{
		}

		// Token: 0x04004D95 RID: 19861
		public Command left;

		// Token: 0x04004D96 RID: 19862
		public Command center;

		// Token: 0x04004D97 RID: 19863
		public Command right;

		// Token: 0x04004D98 RID: 19864
		private int lenCaption;
	}
}
