using System;

namespace Game5.Assets.src.g
{
	// Token: 0x020001AD RID: 429
	public class ClientInput : mScreen, IActionListener
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x001282DC File Offset: 0x001264DC
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

		// Token: 0x0600131F RID: 4895 RVA: 0x001284F2 File Offset: 0x001266F2
		public static ClientInput gI()
		{
			if (ClientInput.instance == null)
			{
				ClientInput.instance = new ClientInput();
			}
			return ClientInput.instance;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0012850A File Offset: 0x0012670A
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00128519 File Offset: 0x00126719
		public void setInput(int type, string title)
		{
			this.nTf = type;
			this.init(title);
			this.switchToMe();
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00128530 File Offset: 0x00126730
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

		// Token: 0x06001323 RID: 4899 RVA: 0x001285D0 File Offset: 0x001267D0
		public override void update()
		{
			GameScr.gI().update();
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i].update();
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00128608 File Offset: 0x00126808
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

		// Token: 0x06001325 RID: 4901 RVA: 0x00128650 File Offset: 0x00126850
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

		// Token: 0x06001326 RID: 4902 RVA: 0x00128789 File Offset: 0x00126989
		public void clearScreen()
		{
			ClientInput.instance = null;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00128794 File Offset: 0x00126994
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

		// Token: 0x040024B8 RID: 9400
		public static ClientInput instance;

		// Token: 0x040024B9 RID: 9401
		public TField[] tf;

		// Token: 0x040024BA RID: 9402
		private int x;

		// Token: 0x040024BB RID: 9403
		private int y;

		// Token: 0x040024BC RID: 9404
		private int w;

		// Token: 0x040024BD RID: 9405
		private int h;

		// Token: 0x040024BE RID: 9406
		private string[] strPaint;

		// Token: 0x040024BF RID: 9407
		private int focus;

		// Token: 0x040024C0 RID: 9408
		private int nTf;
	}
}
