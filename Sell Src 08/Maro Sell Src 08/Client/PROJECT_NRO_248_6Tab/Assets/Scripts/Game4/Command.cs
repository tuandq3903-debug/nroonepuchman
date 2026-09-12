using System;

namespace Game4
{
	// Token: 0x020001D2 RID: 466
	public class Command
	{
		// Token: 0x060014B5 RID: 5301 RVA: 0x00141948 File Offset: 0x0013FB48
		public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
			this.x = x;
			this.y = y;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x001419B0 File Offset: 0x0013FBB0
		public Command(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x001419EE File Offset: 0x0013FBEE
		public Command()
		{
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00141A20 File Offset: 0x0013FC20
		public Command(string caption, IActionListener actionListener, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00141A78 File Offset: 0x0013FC78
		public Command(string caption, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.p = p;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00141AC8 File Offset: 0x0013FCC8
		public Command(string caption, int action)
		{
			this.caption = caption;
			this.idAction = action;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00141B08 File Offset: 0x0013FD08
		public Command(string caption, int action, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.x = x;
			this.y = y;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00141B60 File Offset: 0x0013FD60
		public void perform(string str)
		{
			if (this.actionChat != null)
			{
				this.actionChat(str);
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00141B78 File Offset: 0x0013FD78
		public void performAction()
		{
			GameCanvas.clearAllPointerEvent();
			if (this.isPlaySoundButton && ((this.caption != null && !this.caption.Equals(string.Empty) && !this.caption.Equals(mResources.saying)) || this.img != null))
			{
				SoundMn.gI().buttonClick();
			}
			if (this.idAction > 0)
			{
				if (this.actionListener != null)
				{
					this.actionListener.perform(this.idAction, this.p);
				}
				else
				{
					GameScr.gI().actionPerform(this.idAction, this.p);
				}
			}
			Action action = this.action;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00141C20 File Offset: 0x0013FE20
		public void setType()
		{
			this.type = 1;
			this.w = 160;
			this.hw = 80;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00141C3C File Offset: 0x0013FE3C
		public void setTypeDelete()
		{
			this.type = 2;
			this.w = 50;
			this.hw = 28;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00141C58 File Offset: 0x0013FE58
		public void paint(mGraphics g)
		{
			if (this.img != null)
			{
				g.drawImage(this.img, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
				if (this.isFocus)
				{
					if (this.imgFocus == null)
					{
						if (this.cmdClosePanel)
						{
							g.drawImage(ItemMap.imageFlare, this.x + 8, this.y + mGraphics.addYWhenOpenKeyBoard + 8, 3);
						}
						else
						{
							g.drawImage(ItemMap.imageFlare, this.x - (this.img.Equals(GameScr.imgMenu) ? 10 : 0), this.y + mGraphics.addYWhenOpenKeyBoard, 0);
						}
					}
					else
					{
						g.drawImage(this.imgFocus, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
					}
				}
				if (this.caption != "menu" && this.caption != null)
				{
					if (!this.isFocus)
					{
						mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
						return;
					}
					mFont.tahoma_7b_green2.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
				return;
			}
			if (this.caption != string.Empty)
			{
				if (this.type == 1)
				{
					if (!this.isFocus)
					{
						Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 160, g);
					}
					else
					{
						Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 160, g);
					}
				}
				else if (this.type == 2)
				{
					if (!this.isFocus)
					{
						Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, this.w, g);
					}
					else
					{
						Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, this.w, g);
					}
				}
				else if (!this.isFocus)
				{
					Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 76, g);
				}
				else
				{
					Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 76, g);
				}
			}
			int num = (this.type != 1 && this.type != 2) ? (this.x + 38) : (this.x + this.hw);
			if (!this.isFocus)
			{
				mFont.tahoma_7b_dark.drawString(g, this.caption, num, this.y + 7, 2);
				return;
			}
			mFont.tahoma_7b_green2.drawString(g, this.caption, num, this.y + 7, 2);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00141F60 File Offset: 0x00140160
		public static void paintOngMau(Image img0, Image img1, Image img2, int x, int y, int size, mGraphics g)
		{
			for (int i = 10; i <= size - 20; i += 10)
			{
				g.drawImage(img1, x + i, y, 0);
			}
			int num = size % 10;
			if (num > 0)
			{
				g.drawRegion(img1, 0, 0, num, 24, 0, x + size - 10 - num, y, 0);
			}
			g.drawImage(img0, x, y, 0);
			g.drawImage(img2, x + size - 10, y, 0);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00141FD0 File Offset: 0x001401D0
		public bool isPointerPressInside()
		{
			this.isFocus = false;
			if (GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h))
			{
				if (GameCanvas.isPointerDown)
				{
					this.isFocus = true;
				}
				if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400281C RID: 10268
		public bool isDisplay;

		// Token: 0x0400281D RID: 10269
		public ActionChat actionChat;

		// Token: 0x0400281E RID: 10270
		public string caption;

		// Token: 0x0400281F RID: 10271
		public string[] subCaption;

		// Token: 0x04002820 RID: 10272
		public IActionListener actionListener;

		// Token: 0x04002821 RID: 10273
		public int idAction;

		// Token: 0x04002822 RID: 10274
		public bool isPlaySoundButton = true;

		// Token: 0x04002823 RID: 10275
		public Image img;

		// Token: 0x04002824 RID: 10276
		public Image img2;

		// Token: 0x04002825 RID: 10277
		public Image imgFocus;

		// Token: 0x04002826 RID: 10278
		public int x;

		// Token: 0x04002827 RID: 10279
		public int y;

		// Token: 0x04002828 RID: 10280
		public int w = mScreen.cmdW;

		// Token: 0x04002829 RID: 10281
		public int h = mScreen.cmdH;

		// Token: 0x0400282A RID: 10282
		public int hw;

		// Token: 0x0400282B RID: 10283
		private int lenCaption;

		// Token: 0x0400282C RID: 10284
		public bool isFocus;

		// Token: 0x0400282D RID: 10285
		public object p;

		// Token: 0x0400282E RID: 10286
		public int type;

		// Token: 0x0400282F RID: 10287
		public string caption2 = string.Empty;

		// Token: 0x04002830 RID: 10288
		public static Image btn0left;

		// Token: 0x04002831 RID: 10289
		public static Image btn0mid;

		// Token: 0x04002832 RID: 10290
		public static Image btn0right;

		// Token: 0x04002833 RID: 10291
		public static Image btn1left;

		// Token: 0x04002834 RID: 10292
		public static Image btn1mid;

		// Token: 0x04002835 RID: 10293
		public static Image btn1right;

		// Token: 0x04002836 RID: 10294
		public bool cmdClosePanel;

		// Token: 0x04002837 RID: 10295
		public Action action;
	}
}
