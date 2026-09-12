using System;

namespace Game1
{
	// Token: 0x0200045A RID: 1114
	public class Command
	{
		// Token: 0x060031A1 RID: 12705 RVA: 0x00300B34 File Offset: 0x002FED34
		public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
			this.x = x;
			this.y = y;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x00300B9C File Offset: 0x002FED9C
		public Command(string caption, Action action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x00300BDA File Offset: 0x002FEDDA
		public Command()
		{
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x00300C0C File Offset: 0x002FEE0C
		public Command(string caption, IActionListener actionListener, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.actionListener = actionListener;
			this.p = p;
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x00300C64 File Offset: 0x002FEE64
		public Command(string caption, int action, object p)
		{
			this.caption = caption;
			this.idAction = action;
			this.p = p;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x00300CB4 File Offset: 0x002FEEB4
		public Command(string caption, int action)
		{
			this.caption = caption;
			this.idAction = action;
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x00300CF4 File Offset: 0x002FEEF4
		public Command(string caption, int action, int x, int y)
		{
			this.caption = caption;
			this.idAction = action;
			this.x = x;
			this.y = y;
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x00300D4C File Offset: 0x002FEF4C
		public void perform(string str)
		{
			if (this.actionChat != null)
			{
				this.actionChat(str);
			}
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x00300D64 File Offset: 0x002FEF64
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

		// Token: 0x060031AA RID: 12714 RVA: 0x00300E0C File Offset: 0x002FF00C
		public void setType()
		{
			this.type = 1;
			this.w = 160;
			this.hw = 80;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x00300E28 File Offset: 0x002FF028
		public void setTypeDelete()
		{
			this.type = 2;
			this.w = 50;
			this.hw = 28;
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x00300E44 File Offset: 0x002FF044
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

		// Token: 0x060031AD RID: 12717 RVA: 0x0030114C File Offset: 0x002FF34C
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

		// Token: 0x060031AE RID: 12718 RVA: 0x003011BC File Offset: 0x002FF3BC
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

		// Token: 0x04005F99 RID: 24473
		public bool isDisplay;

		// Token: 0x04005F9A RID: 24474
		public ActionChat actionChat;

		// Token: 0x04005F9B RID: 24475
		public string caption;

		// Token: 0x04005F9C RID: 24476
		public string[] subCaption;

		// Token: 0x04005F9D RID: 24477
		public IActionListener actionListener;

		// Token: 0x04005F9E RID: 24478
		public int idAction;

		// Token: 0x04005F9F RID: 24479
		public bool isPlaySoundButton = true;

		// Token: 0x04005FA0 RID: 24480
		public Image img;

		// Token: 0x04005FA1 RID: 24481
		public Image img2;

		// Token: 0x04005FA2 RID: 24482
		public Image imgFocus;

		// Token: 0x04005FA3 RID: 24483
		public int x;

		// Token: 0x04005FA4 RID: 24484
		public int y;

		// Token: 0x04005FA5 RID: 24485
		public int w = mScreen.cmdW;

		// Token: 0x04005FA6 RID: 24486
		public int h = mScreen.cmdH;

		// Token: 0x04005FA7 RID: 24487
		public int hw;

		// Token: 0x04005FA8 RID: 24488
		private int lenCaption;

		// Token: 0x04005FA9 RID: 24489
		public bool isFocus;

		// Token: 0x04005FAA RID: 24490
		public object p;

		// Token: 0x04005FAB RID: 24491
		public int type;

		// Token: 0x04005FAC RID: 24492
		public string caption2 = string.Empty;

		// Token: 0x04005FAD RID: 24493
		public static Image btn0left;

		// Token: 0x04005FAE RID: 24494
		public static Image btn0mid;

		// Token: 0x04005FAF RID: 24495
		public static Image btn0right;

		// Token: 0x04005FB0 RID: 24496
		public static Image btn1left;

		// Token: 0x04005FB1 RID: 24497
		public static Image btn1mid;

		// Token: 0x04005FB2 RID: 24498
		public static Image btn1right;

		// Token: 0x04005FB3 RID: 24499
		public bool cmdClosePanel;

		// Token: 0x04005FB4 RID: 24500
		public Action action;
	}
}
