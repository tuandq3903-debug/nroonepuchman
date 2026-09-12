using System;

namespace Game4
{
	// Token: 0x02000224 RID: 548
	public class MoneyCharge : mScreen, IActionListener
	{
		// Token: 0x06001855 RID: 6229 RVA: 0x00181C08 File Offset: 0x0017FE08
		public MoneyCharge()
		{
			this.w = GameCanvas.w - 20;
			if (this.w > 320)
			{
				this.w = 320;
			}
			this.strPaint = mFont.tahoma_7b_green2.splitFontArray(mResources.pay_card, this.w - 20);
			this.x = (GameCanvas.w - this.w) / 2;
			this.y = GameCanvas.h - 150 - (this.strPaint.Length - 1) * 20;
			this.h = 110 + (this.strPaint.Length - 1) * 20;
			this.yP = this.y;
			this.tfSerial = new TField();
			this.tfSerial.name = mResources.SERI_NUM;
			this.tfSerial.x = this.x + 10;
			this.tfSerial.y = this.y + 35 + (this.strPaint.Length - 1) * 20;
			this.yt = this.tfSerial.y;
			this.tfSerial.width = this.w - 20;
			this.tfSerial.height = mScreen.ITEM_HEIGHT + 2;
			if (GameCanvas.isTouch)
			{
				this.tfSerial.isFocus = false;
			}
			else
			{
				this.tfSerial.isFocus = true;
			}
			this.tfSerial.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				this.tfSerial.showSubTextField = false;
			}
			if (Main.isIPhone)
			{
				this.tfSerial.isPaintMouse = false;
			}
			if (!GameCanvas.isTouch)
			{
				this.right = this.tfSerial.cmdClear;
			}
			this.tfCode = new TField();
			this.tfCode.name = mResources.CARD_CODE;
			this.tfCode.x = this.x + 10;
			this.tfCode.y = this.tfSerial.y + 35;
			this.tfCode.width = this.w - 20;
			this.tfCode.height = mScreen.ITEM_HEIGHT + 2;
			this.tfCode.isFocus = false;
			this.tfCode.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				this.tfCode.showSubTextField = false;
			}
			if (Main.isIPhone)
			{
				this.tfCode.isPaintMouse = false;
			}
			this.left = new Command(mResources.CLOSE, this, 1, null);
			this.center = new Command(mResources.pay_card2, this, 2, null);
			if (GameCanvas.isTouch)
			{
				this.center.x = GameCanvas.w / 2 + 18;
				this.left.x = GameCanvas.w / 2 - 85;
				this.center.y = (this.left.y = this.y + this.h + 5);
			}
			this.freeAreaHeight = this.tfSerial.y - (4 * this.tfSerial.height - 10);
			this.yP = this.tfSerial.y;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00181F20 File Offset: 0x00180120
		public static MoneyCharge gI()
		{
			if (MoneyCharge.instance == null)
			{
				MoneyCharge.instance = new MoneyCharge();
			}
			return MoneyCharge.instance;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00181F38 File Offset: 0x00180138
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000034B9 File Offset: 0x000016B9
		public void updateTfWhenOpenKb()
		{
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00181F48 File Offset: 0x00180148
		public override void paint(mGraphics g)
		{
			GameScr.gI().paint(g);
			PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
			for (int i = 0; i < this.strPaint.Length; i++)
			{
				mFont.tahoma_7b_green2.drawString(g, this.strPaint[i], GameCanvas.w / 2, this.y + 15 + i * 20, mFont.CENTER);
			}
			this.tfSerial.paint(g);
			this.tfCode.paint(g);
			base.paint(g);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00181FDF File Offset: 0x001801DF
		public override void update()
		{
			GameScr.gI().update();
			this.tfSerial.update();
			this.tfCode.update();
			if (Main.isWindowsPhone)
			{
				this.updateTfWhenOpenKb();
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0018200E File Offset: 0x0018020E
		public override void keyPress(int keyCode)
		{
			if (this.tfSerial.isFocus)
			{
				this.tfSerial.keyPressed(keyCode);
			}
			else if (this.tfCode.isFocus)
			{
				this.tfCode.keyPressed(keyCode);
			}
			base.keyPress(keyCode);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00182050 File Offset: 0x00180250
		public override void updateKey()
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				this.focus--;
				if (this.focus < 0)
				{
					this.focus = 1;
				}
			}
			else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				this.focus++;
				if (this.focus > 1)
				{
					this.focus = 1;
				}
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				GameCanvas.clearKeyPressed();
				if (this.focus == 1)
				{
					this.tfSerial.isFocus = false;
					this.tfCode.isFocus = true;
					if (!GameCanvas.isTouch)
					{
						this.right = this.tfCode.cmdClear;
					}
				}
				else if (this.focus == 0)
				{
					this.tfSerial.isFocus = true;
					this.tfCode.isFocus = false;
					if (!GameCanvas.isTouch)
					{
						this.right = this.tfSerial.cmdClear;
					}
				}
				else
				{
					this.tfSerial.isFocus = false;
					this.tfCode.isFocus = false;
				}
			}
			if (GameCanvas.isPointerJustRelease)
			{
				if (GameCanvas.isPointerHoldIn(this.tfSerial.x, this.tfSerial.y, this.tfSerial.width, this.tfSerial.height))
				{
					this.focus = 0;
				}
				else if (GameCanvas.isPointerHoldIn(this.tfCode.x, this.tfCode.y, this.tfCode.width, this.tfCode.height))
				{
					this.focus = 1;
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00182208 File Offset: 0x00180408
		public void clearScreen()
		{
			MoneyCharge.instance = null;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00182210 File Offset: 0x00180410
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				GameScr.instance.switchToMe();
				this.clearScreen();
			}
			if (idAction == 2)
			{
				if (this.tfSerial.getText() == null || this.tfSerial.getText().Equals(string.Empty))
				{
					GameCanvas.startOKDlg(mResources.serial_blank);
					return;
				}
				if (this.tfCode.getText() == null || this.tfCode.getText().Equals(string.Empty))
				{
					GameCanvas.startOKDlg(mResources.card_code_blank);
					return;
				}
				Service.gI().sendCardInfo(this.tfSerial.getText(), this.tfCode.getText());
				GameScr.instance.switchToMe();
				this.clearScreen();
			}
		}

		// Token: 0x04002FD8 RID: 12248
		public static MoneyCharge instance;

		// Token: 0x04002FD9 RID: 12249
		public TField tfSerial;

		// Token: 0x04002FDA RID: 12250
		public TField tfCode;

		// Token: 0x04002FDB RID: 12251
		private int x;

		// Token: 0x04002FDC RID: 12252
		private int y;

		// Token: 0x04002FDD RID: 12253
		private int w;

		// Token: 0x04002FDE RID: 12254
		private int h;

		// Token: 0x04002FDF RID: 12255
		private string[] strPaint;

		// Token: 0x04002FE0 RID: 12256
		private int focus;

		// Token: 0x04002FE1 RID: 12257
		private int yt;

		// Token: 0x04002FE2 RID: 12258
		private int freeAreaHeight;

		// Token: 0x04002FE3 RID: 12259
		private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

		// Token: 0x04002FE4 RID: 12260
		private int yP;
	}
}
