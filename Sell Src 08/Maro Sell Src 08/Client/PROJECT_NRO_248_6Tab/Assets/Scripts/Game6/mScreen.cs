using System;

namespace Game6
{
	// Token: 0x02000079 RID: 121
	public class mScreen
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x00059DAE File Offset: 0x00057FAE
		public virtual void switchToMe()
		{
			GameCanvas.clearKeyPressed();
			GameCanvas.clearKeyHold();
			if (GameCanvas.currentScreen != null)
			{
				GameCanvas.currentScreen.unLoad();
			}
			GameCanvas.currentScreen = this;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void unLoad()
		{
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void keyPress(int keyCode)
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00059DD4 File Offset: 0x00057FD4
		public virtual void updateKey()
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center))
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				mScreen.keyTouch = -1;
				GameCanvas.isPointerJustRelease = false;
				if (this.center != null)
				{
					this.center.performAction();
				}
			}
			if (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left))
			{
				GameCanvas.keyPressed[12] = false;
				mScreen.keyTouch = -1;
				GameCanvas.isPointerJustRelease = false;
				if (ChatTextField.gI().isShow)
				{
					if (ChatTextField.gI().left != null)
					{
						ChatTextField.gI().left.performAction();
					}
				}
				else if (this.left != null)
				{
					this.left.performAction();
				}
			}
			if (!GameCanvas.keyPressed[13] && !mScreen.getCmdPointerLast(GameCanvas.currentScreen.right))
			{
				return;
			}
			GameCanvas.keyPressed[13] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (ChatTextField.gI().isShow)
			{
				if (ChatTextField.gI().right != null)
				{
					ChatTextField.gI().right.performAction();
					return;
				}
			}
			else if (this.right != null)
			{
				this.right.performAction();
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00059F10 File Offset: 0x00058110
		public static bool getCmdPointerLast(Command cmd)
		{
			if (cmd == null)
			{
				return false;
			}
			if (cmd.x >= 0 && cmd.y != 0)
			{
				return cmd.isPointerPressInside();
			}
			if (GameCanvas.currentDialog != null)
			{
				if (GameCanvas.currentDialog.center != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
				{
					mScreen.keyTouch = 1;
					if (cmd == GameCanvas.currentDialog.center && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						return true;
					}
				}
				if (GameCanvas.currentDialog.left != null && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
				{
					mScreen.keyTouch = 0;
					if (cmd == GameCanvas.currentDialog.left && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						return true;
					}
				}
				if (GameCanvas.currentDialog.right != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
				{
					mScreen.keyTouch = 2;
					if ((cmd == GameCanvas.currentDialog.right || cmd == ChatTextField.gI().right) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						return true;
					}
				}
			}
			else
			{
				if (cmd == GameCanvas.currentScreen.left && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
				{
					mScreen.keyTouch = 0;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						return true;
					}
				}
				if (cmd == GameCanvas.currentScreen.right && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
				{
					mScreen.keyTouch = 2;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						return true;
					}
				}
				if ((cmd == GameCanvas.currentScreen.center || ChatPopup.currChatPopup != null) && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
				{
					mScreen.keyTouch = 1;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0005A158 File Offset: 0x00058358
		public virtual void paint(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
			if ((!ChatTextField.gI().isShow || !Main.isPC) && GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu)
			{
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
		}

		// Token: 0x04000CC9 RID: 3273
		public Command left;

		// Token: 0x04000CCA RID: 3274
		public Command center;

		// Token: 0x04000CCB RID: 3275
		public Command right;

		// Token: 0x04000CCC RID: 3276
		public Command cmdClose;

		// Token: 0x04000CCD RID: 3277
		public static int ITEM_HEIGHT;

		// Token: 0x04000CCE RID: 3278
		public static int yOpenKeyBoard = 100;

		// Token: 0x04000CCF RID: 3279
		public static int cmdW = 68;

		// Token: 0x04000CD0 RID: 3280
		public static int cmdH = 26;

		// Token: 0x04000CD1 RID: 3281
		public static int keyTouch = -1;

		// Token: 0x04000CD2 RID: 3282
		public static int keyMouse = -1;
	}
}
