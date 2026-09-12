using System;

namespace Game2
{
	// Token: 0x020003D9 RID: 985
	public class mScreen
	{
		// Token: 0x06002BB9 RID: 11193 RVA: 0x002AE116 File Offset: 0x002AC316
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

		// Token: 0x06002BBA RID: 11194 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void unLoad()
		{
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void keyPress(int keyCode)
		{
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x002AE13C File Offset: 0x002AC33C
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

		// Token: 0x06002BBE RID: 11198 RVA: 0x002AE278 File Offset: 0x002AC478
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

		// Token: 0x06002BBF RID: 11199 RVA: 0x002AE4C0 File Offset: 0x002AC6C0
		public virtual void paint(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
			if ((!ChatTextField.gI().isShow || !Main.isPC) && GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu)
			{
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
		}

		// Token: 0x040056C5 RID: 22213
		public Command left;

		// Token: 0x040056C6 RID: 22214
		public Command center;

		// Token: 0x040056C7 RID: 22215
		public Command right;

		// Token: 0x040056C8 RID: 22216
		public Command cmdClose;

		// Token: 0x040056C9 RID: 22217
		public static int ITEM_HEIGHT;

		// Token: 0x040056CA RID: 22218
		public static int yOpenKeyBoard = 100;

		// Token: 0x040056CB RID: 22219
		public static int cmdW = 68;

		// Token: 0x040056CC RID: 22220
		public static int cmdH = 26;

		// Token: 0x040056CD RID: 22221
		public static int keyTouch = -1;

		// Token: 0x040056CE RID: 22222
		public static int keyMouse = -1;
	}
}
