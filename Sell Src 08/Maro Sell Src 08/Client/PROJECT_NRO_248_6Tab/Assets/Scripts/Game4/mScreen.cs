using System;

namespace Game4
{
	// Token: 0x02000229 RID: 553
	public class mScreen
	{
		// Token: 0x06001871 RID: 6257 RVA: 0x00183FCE File Offset: 0x001821CE
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

		// Token: 0x06001872 RID: 6258 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void unLoad()
		{
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void keyPress(int keyCode)
		{
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00183FF4 File Offset: 0x001821F4
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

		// Token: 0x06001876 RID: 6262 RVA: 0x00184130 File Offset: 0x00182330
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

		// Token: 0x06001877 RID: 6263 RVA: 0x00184378 File Offset: 0x00182578
		public virtual void paint(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
			if ((!ChatTextField.gI().isShow || !Main.isPC) && GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu)
			{
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
		}

		// Token: 0x040031C7 RID: 12743
		public Command left;

		// Token: 0x040031C8 RID: 12744
		public Command center;

		// Token: 0x040031C9 RID: 12745
		public Command right;

		// Token: 0x040031CA RID: 12746
		public Command cmdClose;

		// Token: 0x040031CB RID: 12747
		public static int ITEM_HEIGHT;

		// Token: 0x040031CC RID: 12748
		public static int yOpenKeyBoard = 100;

		// Token: 0x040031CD RID: 12749
		public static int cmdW = 68;

		// Token: 0x040031CE RID: 12750
		public static int cmdH = 26;

		// Token: 0x040031CF RID: 12751
		public static int keyTouch = -1;

		// Token: 0x040031D0 RID: 12752
		public static int keyMouse = -1;
	}
}
