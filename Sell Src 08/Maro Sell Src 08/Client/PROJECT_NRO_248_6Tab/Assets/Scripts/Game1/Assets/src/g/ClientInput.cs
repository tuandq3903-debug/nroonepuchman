using System;

namespace Game1.Assets.src.g
{
	// Token: 0x0200050D RID: 1293
	public class ClientInput : mScreen, IActionListener
	{
		// Token: 0x060039AE RID: 14766 RVA: 0x0037C56C File Offset: 0x0037A76C
		private void init(string t)
		{
			this.w = GameCanvas.w - 20;
			if (this.w > 320)
			{
				this.w = 320;
			}
			this.strPaint = mFont.tahoma_7b_dark.splitFontArray(t, this.w - 20);
			this.x = (GameCanvas.w - this.w) / 2;
			this.tf = new TField[this.nTf];
			this.h = this.tf.Length * 35 + (this.strPaint.Length - 1) * 20 + 40;
			this.y = GameCanvas.h - this.h - 40 - (this.strPaint.Length - 1) * 20;
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i] = new TField();
				this.tf[i].name = string.Empty;
				this.tf[i].x = this.x + 10;
				this.tf[i].y = this.y + 35 + (this.strPaint.Length - 1) * 20 + i * 35;
				this.tf[i].width = this.w - 20;
				this.tf[i].height = mScreen.ITEM_HEIGHT + 2;
				if (GameCanvas.isTouch)
				{
					this.tf[0].isFocus = false;
				}
				else
				{
					this.tf[0].isFocus = true;
				}
				if (!GameCanvas.isTouch)
				{
					this.right = this.tf[0].cmdClear;
				}
			}
			this.left = new Command(mResources.CLOSE, this, 1, null);
			this.center = new Command(mResources.OK, this, 2, null);
			if (GameCanvas.isTouch)
			{
				this.center.x = GameCanvas.w / 2 + 18;
				this.left.x = GameCanvas.w / 2 - 85;
				this.center.y = (this.left.y = this.y + this.h + 5);
			}
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x0037C782 File Offset: 0x0037A982
		public static ClientInput gI()
		{
			if (ClientInput.instance == null)
			{
				ClientInput.instance = new ClientInput();
			}
			return ClientInput.instance;
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x0037C79A File Offset: 0x0037A99A
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x0037C7A9 File Offset: 0x0037A9A9
		public void setInput(int type, string title)
		{
			this.nTf = type;
			this.init(title);
			this.switchToMe();
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x0037C7C0 File Offset: 0x0037A9C0
		public override void paint(mGraphics g)
		{
			GameScr.gI().paint(g);
			PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
			for (int i = 0; i < this.strPaint.Length; i++)
			{
				mFont.tahoma_7b_green2.drawString(g, this.strPaint[i], GameCanvas.w / 2, this.y + 15 + i * 20, mFont.CENTER);
			}
			for (int j = 0; j < this.tf.Length; j++)
			{
				this.tf[j].paint(g);
			}
			base.paint(g);
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x0037C860 File Offset: 0x0037AA60
		public override void update()
		{
			GameScr.gI().update();
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i].update();
			}
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x0037C898 File Offset: 0x0037AA98
		public override void keyPress(int keyCode)
		{
			for (int i = 0; i < this.tf.Length; i++)
			{
				if (this.tf[i].isFocus)
				{
					this.tf[i].keyPressed(keyCode);
					break;
				}
			}
			base.keyPress(keyCode);
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x0037C8E0 File Offset: 0x0037AAE0
		public override void updateKey()
		{
			if (GameCanvas.keyPressed[2])
			{
				this.focus--;
				if (this.focus < 0)
				{
					this.focus = this.tf.Length - 1;
				}
			}
			else if (GameCanvas.keyPressed[8])
			{
				this.focus++;
				if (this.focus > this.tf.Length - 1)
				{
					this.focus = 0;
				}
			}
			if (GameCanvas.keyPressed[2] || GameCanvas.keyPressed[8])
			{
				GameCanvas.clearKeyPressed();
				for (int i = 0; i < this.tf.Length; i++)
				{
					if (this.focus == i)
					{
						this.tf[i].isFocus = true;
						if (!GameCanvas.isTouch)
						{
							this.right = this.tf[i].cmdClear;
						}
					}
					else
					{
						this.tf[i].isFocus = false;
					}
					if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerHoldIn(this.tf[i].x, this.tf[i].y, this.tf[i].width, this.tf[i].height))
					{
						this.focus = i;
						break;
					}
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x0037CA19 File Offset: 0x0037AC19
		public void clearScreen()
		{
			ClientInput.instance = null;
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x0037CA24 File Offset: 0x0037AC24
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				GameScr.instance.switchToMe();
				this.clearScreen();
			}
			if (idAction != 2)
			{
				return;
			}
			for (int i = 0; i < this.tf.Length; i++)
			{
				if (this.tf[i].getText() == null || this.tf[i].getText().Equals(string.Empty))
				{
					GameCanvas.startOKDlg(mResources.vuilongnhapduthongtin);
					return;
				}
			}
			Service.gI().sendClientInput(this.tf);
			GameScr.instance.switchToMe();
		}

		// Token: 0x04006EB4 RID: 28340
		public static ClientInput instance;

		// Token: 0x04006EB5 RID: 28341
		public TField[] tf;

		// Token: 0x04006EB6 RID: 28342
		private int x;

		// Token: 0x04006EB7 RID: 28343
		private int y;

		// Token: 0x04006EB8 RID: 28344
		private int w;

		// Token: 0x04006EB9 RID: 28345
		private int h;

		// Token: 0x04006EBA RID: 28346
		private string[] strPaint;

		// Token: 0x04006EBB RID: 28347
		private int focus;

		// Token: 0x04006EBC RID: 28348
		private int nTf;
	}
}
