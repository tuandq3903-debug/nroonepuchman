using System;

namespace Game3
{
	// Token: 0x020002AA RID: 682
	public class Command
	{
		// Token: 0x06001E59 RID: 7769 RVA: 0x001D69EC File Offset: 0x001D4BEC
		public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x001D6A54 File Offset: 0x001D4C54
		public Command(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x001D6A92 File Offset: 0x001D4C92
		public Command()
		{
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x001D6AC4 File Offset: 0x001D4CC4
		public Command(string caption, IActionListener actionListener, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x001D6B1C File Offset: 0x001D4D1C
		public Command(string caption, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.p = p;
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x001D6B6C File Offset: 0x001D4D6C
		public Command(string caption, int action)
		{
			this.caption = caption;
			this.idAction = action;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x001D6BAC File Offset: 0x001D4DAC
		public Command(string caption, int action, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x001D6C04 File Offset: 0x001D4E04
		public void perform(string str)
		{
			if (this.actionChat != null)
			{
				this.actionChat(str);
			}
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x001D6C1C File Offset: 0x001D4E1C
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

		// Token: 0x06001E62 RID: 7778 RVA: 0x001D6CC4 File Offset: 0x001D4EC4
		public void setType()
		{
			this.type = 1;
			this.w = 160;
			this.hw = 80;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x001D6CE0 File Offset: 0x001D4EE0
		public void setTypeDelete()
		{
			this.type = 2;
			this.w = 50;
			this.hw = 28;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x001D6CFC File Offset: 0x001D4EFC
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

		// Token: 0x06001E65 RID: 7781 RVA: 0x001D7004 File Offset: 0x001D5204
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

		// Token: 0x06001E66 RID: 7782 RVA: 0x001D7074 File Offset: 0x001D5274
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

		// Token: 0x04003A9B RID: 15003
		public bool isDisplay;

		// Token: 0x04003A9C RID: 15004
		public ActionChat actionChat;

		// Token: 0x04003A9D RID: 15005
		public string caption;

		// Token: 0x04003A9E RID: 15006
		public string[] subCaption;

		// Token: 0x04003A9F RID: 15007
		public IActionListener actionListener;

		// Token: 0x04003AA0 RID: 15008
		public int idAction;

		// Token: 0x04003AA1 RID: 15009
		public bool isPlaySoundButton = true;

		// Token: 0x04003AA2 RID: 15010
		public Image img;

		// Token: 0x04003AA3 RID: 15011
		public Image img2;

		// Token: 0x04003AA4 RID: 15012
		public Image imgFocus;

		// Token: 0x04003AA5 RID: 15013
		public int x;

		// Token: 0x04003AA6 RID: 15014
		public int y;

		// Token: 0x04003AA7 RID: 15015
		public int w = mScreen.cmdW;

		// Token: 0x04003AA8 RID: 15016
		public int h = mScreen.cmdH;

		// Token: 0x04003AA9 RID: 15017
		public int hw;

		// Token: 0x04003AAA RID: 15018
		private int lenCaption;

		// Token: 0x04003AAB RID: 15019
		public bool isFocus;

		// Token: 0x04003AAC RID: 15020
		public object p;

		// Token: 0x04003AAD RID: 15021
		public int type;

		// Token: 0x04003AAE RID: 15022
		public string caption2 = string.Empty;

		// Token: 0x04003AAF RID: 15023
		public static Image btn0left;

		// Token: 0x04003AB0 RID: 15024
		public static Image btn0mid;

		// Token: 0x04003AB1 RID: 15025
		public static Image btn0right;

		// Token: 0x04003AB2 RID: 15026
		public static Image btn1left;

		// Token: 0x04003AB3 RID: 15027
		public static Image btn1mid;

		// Token: 0x04003AB4 RID: 15028
		public static Image btn1right;

		// Token: 0x04003AB5 RID: 15029
		public bool cmdClosePanel;

		// Token: 0x04003AB6 RID: 15030
		public Action action;
	}
}
