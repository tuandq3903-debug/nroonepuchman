using System;

namespace Game2.Assets.src.g
{
	// Token: 0x02000435 RID: 1077
	public class ClientInput : mScreen, IActionListener
	{
		// Token: 0x0600300A RID: 12298 RVA: 0x002E74C8 File Offset: 0x002E56C8
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

		// Token: 0x0600300B RID: 12299 RVA: 0x002E76DE File Offset: 0x002E58DE
		public static ClientInput gI()
		{
			if (ClientInput.instance == null)
			{
				ClientInput.instance = new ClientInput();
			}
			return ClientInput.instance;
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x002E76F6 File Offset: 0x002E58F6
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x002E7705 File Offset: 0x002E5905
		public void setInput(int type, string title)
		{
			this.nTf = type;
			this.init(title);
			this.switchToMe();
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x002E771C File Offset: 0x002E591C
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

		// Token: 0x0600300F RID: 12303 RVA: 0x002E77BC File Offset: 0x002E59BC
		public override void update()
		{
			GameScr.gI().update();
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i].update();
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x002E77F4 File Offset: 0x002E59F4
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

		// Token: 0x06003011 RID: 12305 RVA: 0x002E783C File Offset: 0x002E5A3C
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

		// Token: 0x06003012 RID: 12306 RVA: 0x002E7975 File Offset: 0x002E5B75
		public void clearScreen()
		{
			ClientInput.instance = null;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x002E7980 File Offset: 0x002E5B80
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

		// Token: 0x04005C35 RID: 23605
		public static ClientInput instance;

		// Token: 0x04005C36 RID: 23606
		public TField[] tf;

		// Token: 0x04005C37 RID: 23607
		private int x;

		// Token: 0x04005C38 RID: 23608
		private int y;

		// Token: 0x04005C39 RID: 23609
		private int w;

		// Token: 0x04005C3A RID: 23610
		private int h;

		// Token: 0x04005C3B RID: 23611
		private string[] strPaint;

		// Token: 0x04005C3C RID: 23612
		private int focus;

		// Token: 0x04005C3D RID: 23613
		private int nTf;
	}
}
