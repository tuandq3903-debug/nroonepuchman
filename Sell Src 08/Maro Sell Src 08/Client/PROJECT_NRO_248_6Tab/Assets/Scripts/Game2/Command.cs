using System;

namespace Game2
{
	// Token: 0x02000382 RID: 898
	public class Command
	{
		// Token: 0x060027FD RID: 10237 RVA: 0x0026BA90 File Offset: 0x00269C90
		public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
			this.x = x;
			this.y = y;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x0026BAF8 File Offset: 0x00269CF8
		public Command(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x0026BB36 File Offset: 0x00269D36
		public Command()
		{
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0026BB68 File Offset: 0x00269D68
		public Command(string caption, IActionListener actionListener, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0026BBC0 File Offset: 0x00269DC0
		public Command(string caption, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.p = p;
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0026BC10 File Offset: 0x00269E10
		public Command(string caption, int action)
		{
			this.caption = caption;
			this.idAction = action;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0026BC50 File Offset: 0x00269E50
		public Command(string caption, int action, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.x = x;
			this.y = y;
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0026BCA8 File Offset: 0x00269EA8
		public void perform(string str)
		{
			if (this.actionChat != null)
			{
				this.actionChat(str);
			}
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0026BCC0 File Offset: 0x00269EC0
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

		// Token: 0x06002806 RID: 10246 RVA: 0x0026BD68 File Offset: 0x00269F68
		public void setType()
		{
			this.type = 1;
			this.w = 160;
			this.hw = 80;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x0026BD84 File Offset: 0x00269F84
		public void setTypeDelete()
		{
			this.type = 2;
			this.w = 50;
			this.hw = 28;
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x0026BDA0 File Offset: 0x00269FA0
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

		// Token: 0x06002809 RID: 10249 RVA: 0x0026C0A8 File Offset: 0x0026A2A8
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

		// Token: 0x0600280A RID: 10250 RVA: 0x0026C118 File Offset: 0x0026A318
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

		// Token: 0x04004D1A RID: 19738
		public bool isDisplay;

		// Token: 0x04004D1B RID: 19739
		public ActionChat actionChat;

		// Token: 0x04004D1C RID: 19740
		public string caption;

		// Token: 0x04004D1D RID: 19741
		public string[] subCaption;

		// Token: 0x04004D1E RID: 19742
		public IActionListener actionListener;

		// Token: 0x04004D1F RID: 19743
		public int idAction;

		// Token: 0x04004D20 RID: 19744
		public bool isPlaySoundButton = true;

		// Token: 0x04004D21 RID: 19745
		public Image img;

		// Token: 0x04004D22 RID: 19746
		public Image img2;

		// Token: 0x04004D23 RID: 19747
		public Image imgFocus;

		// Token: 0x04004D24 RID: 19748
		public int x;

		// Token: 0x04004D25 RID: 19749
		public int y;

		// Token: 0x04004D26 RID: 19750
		public int w = mScreen.cmdW;

		// Token: 0x04004D27 RID: 19751
		public int h = mScreen.cmdH;

		// Token: 0x04004D28 RID: 19752
		public int hw;

		// Token: 0x04004D29 RID: 19753
		private int lenCaption;

		// Token: 0x04004D2A RID: 19754
		public bool isFocus;

		// Token: 0x04004D2B RID: 19755
		public object p;

		// Token: 0x04004D2C RID: 19756
		public int type;

		// Token: 0x04004D2D RID: 19757
		public string caption2 = string.Empty;

		// Token: 0x04004D2E RID: 19758
		public static Image btn0left;

		// Token: 0x04004D2F RID: 19759
		public static Image btn0mid;

		// Token: 0x04004D30 RID: 19760
		public static Image btn0right;

		// Token: 0x04004D31 RID: 19761
		public static Image btn1left;

		// Token: 0x04004D32 RID: 19762
		public static Image btn1mid;

		// Token: 0x04004D33 RID: 19763
		public static Image btn1right;

		// Token: 0x04004D34 RID: 19764
		public bool cmdClosePanel;

		// Token: 0x04004D35 RID: 19765
		public Action action;
	}
}
