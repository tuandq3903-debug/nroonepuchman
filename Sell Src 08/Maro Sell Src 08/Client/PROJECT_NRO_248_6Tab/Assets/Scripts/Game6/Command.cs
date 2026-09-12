using System;

namespace Game6
{
	// Token: 0x02000022 RID: 34
	public class Command
	{
		// Token: 0x0600016D RID: 365 RVA: 0x000176D8 File Offset: 0x000158D8
		public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
			this.x = x;
			this.y = y;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00017740 File Offset: 0x00015940
		public Command(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0001777E File Offset: 0x0001597E
		public Command()
		{
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000177B0 File Offset: 0x000159B0
		public Command(string caption, IActionListener actionListener, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00017808 File Offset: 0x00015A08
		public Command(string caption, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.p = p;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00017858 File Offset: 0x00015A58
		public Command(string caption, int action)
		{
			this.caption = caption;
			this.idAction = action;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00017898 File Offset: 0x00015A98
		public Command(string caption, int action, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000178F0 File Offset: 0x00015AF0
		public void perform(string str)
		{
			if (this.actionChat != null)
			{
				this.actionChat(str);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00017908 File Offset: 0x00015B08
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

		// Token: 0x06000176 RID: 374 RVA: 0x000179B0 File Offset: 0x00015BB0
		public void setType()
		{
			this.type = 1;
			this.w = 160;
			this.hw = 80;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000179CC File Offset: 0x00015BCC
		public void setTypeDelete()
		{
			this.type = 2;
			this.w = 50;
			this.hw = 28;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000179E8 File Offset: 0x00015BE8
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

		// Token: 0x06000179 RID: 377 RVA: 0x00017CF0 File Offset: 0x00015EF0
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

		// Token: 0x0600017A RID: 378 RVA: 0x00017D60 File Offset: 0x00015F60
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

		// Token: 0x0400031F RID: 799
		public bool isDisplay;

		// Token: 0x04000320 RID: 800
		public ActionChat actionChat;

		// Token: 0x04000321 RID: 801
		public string caption;

		// Token: 0x04000322 RID: 802
		public string[] subCaption;

		// Token: 0x04000323 RID: 803
		public IActionListener actionListener;

		// Token: 0x04000324 RID: 804
		public int idAction;

		// Token: 0x04000325 RID: 805
		public bool isPlaySoundButton = true;

		// Token: 0x04000326 RID: 806
		public Image img;

		// Token: 0x04000327 RID: 807
		public Image img2;

		// Token: 0x04000328 RID: 808
		public Image imgFocus;

		// Token: 0x04000329 RID: 809
		public int x;

		// Token: 0x0400032A RID: 810
		public int y;

		// Token: 0x0400032B RID: 811
		public int w = mScreen.cmdW;

		// Token: 0x0400032C RID: 812
		public int h = mScreen.cmdH;

		// Token: 0x0400032D RID: 813
		public int hw;

		// Token: 0x0400032E RID: 814
		private int lenCaption;

		// Token: 0x0400032F RID: 815
		public bool isFocus;

		// Token: 0x04000330 RID: 816
		public object p;

		// Token: 0x04000331 RID: 817
		public int type;

		// Token: 0x04000332 RID: 818
		public string caption2 = string.Empty;

		// Token: 0x04000333 RID: 819
		public static Image btn0left;

		// Token: 0x04000334 RID: 820
		public static Image btn0mid;

		// Token: 0x04000335 RID: 821
		public static Image btn0right;

		// Token: 0x04000336 RID: 822
		public static Image btn1left;

		// Token: 0x04000337 RID: 823
		public static Image btn1mid;

		// Token: 0x04000338 RID: 824
		public static Image btn1right;

		// Token: 0x04000339 RID: 825
		public bool cmdClosePanel;

		// Token: 0x0400033A RID: 826
		public Action action;
	}
}
