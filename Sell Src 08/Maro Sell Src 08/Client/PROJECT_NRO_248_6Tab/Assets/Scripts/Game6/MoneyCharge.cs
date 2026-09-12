using System;

namespace Game6
{
	// Token: 0x02000074 RID: 116
	public class MoneyCharge : mScreen, IActionListener
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x000579E8 File Offset: 0x00055BE8
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

		// Token: 0x0600050E RID: 1294 RVA: 0x00057D00 File Offset: 0x00055F00
		public static MoneyCharge gI()
		{
			if (MoneyCharge.instance == null)
			{
				MoneyCharge.instance = new MoneyCharge();
			}
			return MoneyCharge.instance;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00057D18 File Offset: 0x00055F18
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000034B9 File Offset: 0x000016B9
		public void updateTfWhenOpenKb()
		{
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00057D28 File Offset: 0x00055F28
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

		// Token: 0x06000512 RID: 1298 RVA: 0x00057DBF File Offset: 0x00055FBF
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

		// Token: 0x06000513 RID: 1299 RVA: 0x00057DEE File Offset: 0x00055FEE
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

		// Token: 0x06000514 RID: 1300 RVA: 0x00057E30 File Offset: 0x00056030
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

		// Token: 0x06000515 RID: 1301 RVA: 0x00057FE8 File Offset: 0x000561E8
		public void clearScreen()
		{
			MoneyCharge.instance = null;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00057FF0 File Offset: 0x000561F0
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

		// Token: 0x04000ADA RID: 2778
		public static MoneyCharge instance;

		// Token: 0x04000ADB RID: 2779
		public TField tfSerial;

		// Token: 0x04000ADC RID: 2780
		public TField tfCode;

		// Token: 0x04000ADD RID: 2781
		private int x;

		// Token: 0x04000ADE RID: 2782
		private int y;

		// Token: 0x04000ADF RID: 2783
		private int w;

		// Token: 0x04000AE0 RID: 2784
		private int h;

		// Token: 0x04000AE1 RID: 2785
		private string[] strPaint;

		// Token: 0x04000AE2 RID: 2786
		private int focus;

		// Token: 0x04000AE3 RID: 2787
		private int yt;

		// Token: 0x04000AE4 RID: 2788
		private int freeAreaHeight;

		// Token: 0x04000AE5 RID: 2789
		private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

		// Token: 0x04000AE6 RID: 2790
		private int yP;
	}
}
