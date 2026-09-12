using System;

namespace Game2
{
	// Token: 0x020003D4 RID: 980
	public class MoneyCharge : mScreen, IActionListener
	{
		// Token: 0x06002B9D RID: 11165 RVA: 0x002ABD50 File Offset: 0x002A9F50
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

		// Token: 0x06002B9E RID: 11166 RVA: 0x002AC068 File Offset: 0x002AA268
		public static MoneyCharge gI()
		{
			if (MoneyCharge.instance == null)
			{
				MoneyCharge.instance = new MoneyCharge();
			}
			return MoneyCharge.instance;
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x002AC080 File Offset: 0x002AA280
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000034B9 File Offset: 0x000016B9
		public void updateTfWhenOpenKb()
		{
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x002AC090 File Offset: 0x002AA290
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

		// Token: 0x06002BA2 RID: 11170 RVA: 0x002AC127 File Offset: 0x002AA327
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

		// Token: 0x06002BA3 RID: 11171 RVA: 0x002AC156 File Offset: 0x002AA356
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

		// Token: 0x06002BA4 RID: 11172 RVA: 0x002AC198 File Offset: 0x002AA398
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

		// Token: 0x06002BA5 RID: 11173 RVA: 0x002AC350 File Offset: 0x002AA550
		public void clearScreen()
		{
			MoneyCharge.instance = null;
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x002AC358 File Offset: 0x002AA558
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

		// Token: 0x040054D6 RID: 21718
		public static MoneyCharge instance;

		// Token: 0x040054D7 RID: 21719
		public TField tfSerial;

		// Token: 0x040054D8 RID: 21720
		public TField tfCode;

		// Token: 0x040054D9 RID: 21721
		private int x;

		// Token: 0x040054DA RID: 21722
		private int y;

		// Token: 0x040054DB RID: 21723
		private int w;

		// Token: 0x040054DC RID: 21724
		private int h;

		// Token: 0x040054DD RID: 21725
		private string[] strPaint;

		// Token: 0x040054DE RID: 21726
		private int focus;

		// Token: 0x040054DF RID: 21727
		private int yt;

		// Token: 0x040054E0 RID: 21728
		private int freeAreaHeight;

		// Token: 0x040054E1 RID: 21729
		private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

		// Token: 0x040054E2 RID: 21730
		private int yP;
	}
}
