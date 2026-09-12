using System;

namespace Game4.Assets.src.g
{
	// Token: 0x02000285 RID: 645
	public class ClientInput : mScreen, IActionListener
	{
		// Token: 0x06001CC2 RID: 7362 RVA: 0x001BD380 File Offset: 0x001BB580
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

		// Token: 0x06001CC3 RID: 7363 RVA: 0x001BD596 File Offset: 0x001BB796
		public static ClientInput gI()
		{
			if (ClientInput.instance == null)
			{
				ClientInput.instance = new ClientInput();
			}
			return ClientInput.instance;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x001BD5AE File Offset: 0x001BB7AE
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x001BD5BD File Offset: 0x001BB7BD
		public void setInput(int type, string title)
		{
			this.nTf = type;
			this.init(title);
			this.switchToMe();
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x001BD5D4 File Offset: 0x001BB7D4
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

		// Token: 0x06001CC7 RID: 7367 RVA: 0x001BD674 File Offset: 0x001BB874
		public override void update()
		{
			GameScr.gI().update();
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i].update();
			}
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x001BD6AC File Offset: 0x001BB8AC
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

		// Token: 0x06001CC9 RID: 7369 RVA: 0x001BD6F4 File Offset: 0x001BB8F4
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

		// Token: 0x06001CCA RID: 7370 RVA: 0x001BD82D File Offset: 0x001BBA2D
		public void clearScreen()
		{
			ClientInput.instance = null;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x001BD838 File Offset: 0x001BBA38
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

		// Token: 0x04003737 RID: 14135
		public static ClientInput instance;

		// Token: 0x04003738 RID: 14136
		public TField[] tf;

		// Token: 0x04003739 RID: 14137
		private int x;

		// Token: 0x0400373A RID: 14138
		private int y;

		// Token: 0x0400373B RID: 14139
		private int w;

		// Token: 0x0400373C RID: 14140
		private int h;

		// Token: 0x0400373D RID: 14141
		private string[] strPaint;

		// Token: 0x0400373E RID: 14142
		private int focus;

		// Token: 0x0400373F RID: 14143
		private int nTf;
	}
}
