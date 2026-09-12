using System;

namespace Game3
{
	// Token: 0x02000301 RID: 769
	public class mScreen
	{
		// Token: 0x06002215 RID: 8725 RVA: 0x00219072 File Offset: 0x00217272
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

		// Token: 0x06002216 RID: 8726 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void unLoad()
		{
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void keyPress(int keyCode)
		{
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00219098 File Offset: 0x00217298
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

		// Token: 0x0600221A RID: 8730 RVA: 0x002191D4 File Offset: 0x002173D4
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

		// Token: 0x0600221B RID: 8731 RVA: 0x0021941C File Offset: 0x0021761C
		public virtual void paint(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
			if ((!ChatTextField.gI().isShow || !Main.isPC) && GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu)
			{
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
		}

		// Token: 0x04004446 RID: 17478
		public Command left;

		// Token: 0x04004447 RID: 17479
		public Command center;

		// Token: 0x04004448 RID: 17480
		public Command right;

		// Token: 0x04004449 RID: 17481
		public Command cmdClose;

		// Token: 0x0400444A RID: 17482
		public static int ITEM_HEIGHT;

		// Token: 0x0400444B RID: 17483
		public static int yOpenKeyBoard = 100;

		// Token: 0x0400444C RID: 17484
		public static int cmdW = 68;

		// Token: 0x0400444D RID: 17485
		public static int cmdH = 26;

		// Token: 0x0400444E RID: 17486
		public static int keyTouch = -1;

		// Token: 0x0400444F RID: 17487
		public static int keyMouse = -1;
	}
}
