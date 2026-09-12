using System;

namespace Game3.Assets.src.g
{
	// Token: 0x0200035D RID: 861
	public class ClientInput : mScreen, IActionListener
	{
		// Token: 0x06002666 RID: 9830 RVA: 0x00252424 File Offset: 0x00250624
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

		// Token: 0x06002667 RID: 9831 RVA: 0x0025263A File Offset: 0x0025083A
		public static ClientInput gI()
		{
			if (ClientInput.instance == null)
			{
				ClientInput.instance = new ClientInput();
			}
			return ClientInput.instance;
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x00252652 File Offset: 0x00250852
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00252661 File Offset: 0x00250861
		public void setInput(int type, string title)
		{
			this.nTf = type;
			this.init(title);
			this.switchToMe();
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x00252678 File Offset: 0x00250878
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

		// Token: 0x0600266B RID: 9835 RVA: 0x00252718 File Offset: 0x00250918
		public override void update()
		{
			GameScr.gI().update();
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i].update();
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00252750 File Offset: 0x00250950
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

		// Token: 0x0600266D RID: 9837 RVA: 0x00252798 File Offset: 0x00250998
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

		// Token: 0x0600266E RID: 9838 RVA: 0x002528D1 File Offset: 0x00250AD1
		public void clearScreen()
		{
			ClientInput.instance = null;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x002528DC File Offset: 0x00250ADC
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

		// Token: 0x040049B6 RID: 18870
		public static ClientInput instance;

		// Token: 0x040049B7 RID: 18871
		public TField[] tf;

		// Token: 0x040049B8 RID: 18872
		private int x;

		// Token: 0x040049B9 RID: 18873
		private int y;

		// Token: 0x040049BA RID: 18874
		private int w;

		// Token: 0x040049BB RID: 18875
		private int h;

		// Token: 0x040049BC RID: 18876
		private string[] strPaint;

		// Token: 0x040049BD RID: 18877
		private int focus;

		// Token: 0x040049BE RID: 18878
		private int nTf;
	}
}
