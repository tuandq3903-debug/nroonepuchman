using System;

namespace Game5
{
	// Token: 0x020000FA RID: 250
	public class Command
	{
		// Token: 0x06000B11 RID: 2833 RVA: 0x000AC8A4 File Offset: 0x000AAAA4
		public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000AC90C File Offset: 0x000AAB0C
		public Command(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000AC94A File Offset: 0x000AAB4A
		public Command()
		{
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000AC97C File Offset: 0x000AAB7C
		public Command(string caption, IActionListener actionListener, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000AC9D4 File Offset: 0x000AABD4
		public Command(string caption, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.p = p;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000ACA24 File Offset: 0x000AAC24
		public Command(string caption, int action)
		{
			this.caption = caption;
			this.idAction = action;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000ACA64 File Offset: 0x000AAC64
		public Command(string caption, int action, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000ACABC File Offset: 0x000AACBC
		public void perform(string str)
		{
			if (this.actionChat != null)
			{
				this.actionChat(str);
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000ACAD4 File Offset: 0x000AACD4
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

		// Token: 0x06000B1A RID: 2842 RVA: 0x000ACB7C File Offset: 0x000AAD7C
		public void setType()
		{
			this.type = 1;
			this.w = 160;
			this.hw = 80;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000ACB98 File Offset: 0x000AAD98
		public void setTypeDelete()
		{
			this.type = 2;
			this.w = 50;
			this.hw = 28;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000ACBB4 File Offset: 0x000AADB4
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

		// Token: 0x06000B1D RID: 2845 RVA: 0x000ACEBC File Offset: 0x000AB0BC
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

		// Token: 0x06000B1E RID: 2846 RVA: 0x000ACF2C File Offset: 0x000AB12C
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

		// Token: 0x0400159D RID: 5533
		public bool isDisplay;

		// Token: 0x0400159E RID: 5534
		public ActionChat actionChat;

		// Token: 0x0400159F RID: 5535
		public string caption;

		// Token: 0x040015A0 RID: 5536
		public string[] subCaption;

		// Token: 0x040015A1 RID: 5537
		public IActionListener actionListener;

		// Token: 0x040015A2 RID: 5538
		public int idAction;

		// Token: 0x040015A3 RID: 5539
		public bool isPlaySoundButton = true;

		// Token: 0x040015A4 RID: 5540
		public Image img;

		// Token: 0x040015A5 RID: 5541
		public Image img2;

		// Token: 0x040015A6 RID: 5542
		public Image imgFocus;

		// Token: 0x040015A7 RID: 5543
		public int x;

		// Token: 0x040015A8 RID: 5544
		public int y;

		// Token: 0x040015A9 RID: 5545
		public int w = mScreen.cmdW;

		// Token: 0x040015AA RID: 5546
		public int h = mScreen.cmdH;

		// Token: 0x040015AB RID: 5547
		public int hw;

		// Token: 0x040015AC RID: 5548
		private int lenCaption;

		// Token: 0x040015AD RID: 5549
		public bool isFocus;

		// Token: 0x040015AE RID: 5550
		public object p;

		// Token: 0x040015AF RID: 5551
		public int type;

		// Token: 0x040015B0 RID: 5552
		public string caption2 = string.Empty;

		// Token: 0x040015B1 RID: 5553
		public static Image btn0left;

		// Token: 0x040015B2 RID: 5554
		public static Image btn0mid;

		// Token: 0x040015B3 RID: 5555
		public static Image btn0right;

		// Token: 0x040015B4 RID: 5556
		public static Image btn1left;

		// Token: 0x040015B5 RID: 5557
		public static Image btn1mid;

		// Token: 0x040015B6 RID: 5558
		public static Image btn1right;

		// Token: 0x040015B7 RID: 5559
		public bool cmdClosePanel;

		// Token: 0x040015B8 RID: 5560
		public Action action;
	}
}
