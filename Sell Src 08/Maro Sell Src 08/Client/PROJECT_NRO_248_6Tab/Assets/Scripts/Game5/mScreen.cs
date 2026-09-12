using System;

namespace Game5
{
	// Token: 0x02000151 RID: 337
	public class mScreen
	{
		// Token: 0x06000ECD RID: 3789 RVA: 0x000EEF2A File Offset: 0x000ED12A
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

		// Token: 0x06000ECE RID: 3790 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void unLoad()
		{
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void keyPress(int keyCode)
		{
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000EEF50 File Offset: 0x000ED150
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

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000EF08C File Offset: 0x000ED28C
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

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000EF2D4 File Offset: 0x000ED4D4
		public virtual void paint(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
			if ((!ChatTextField.gI().isShow || !Main.isPC) && GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu)
			{
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
		}

		// Token: 0x04001F48 RID: 8008
		public Command left;

		// Token: 0x04001F49 RID: 8009
		public Command center;

		// Token: 0x04001F4A RID: 8010
		public Command right;

		// Token: 0x04001F4B RID: 8011
		public Command cmdClose;

		// Token: 0x04001F4C RID: 8012
		public static int ITEM_HEIGHT;

		// Token: 0x04001F4D RID: 8013
		public static int yOpenKeyBoard = 100;

		// Token: 0x04001F4E RID: 8014
		public static int cmdW = 68;

		// Token: 0x04001F4F RID: 8015
		public static int cmdH = 26;

		// Token: 0x04001F50 RID: 8016
		public static int keyTouch = -1;

		// Token: 0x04001F51 RID: 8017
		public static int keyMouse = -1;
	}
}
