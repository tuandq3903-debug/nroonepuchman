using System;

namespace Game4
{
	// Token: 0x02000244 RID: 580
	public class PopUpYesNo : IActionListener
	{
		// Token: 0x06001A22 RID: 6690 RVA: 0x001A5DD0 File Offset: 0x001A3FD0
		public void setPopUp(string info, Command cmdYes, Command cmdNo)
		{
			this.info = new string[]
			{
				info
			};
			this.H = 29;
			this.cmdYes = cmdYes;
			this.cmdNo = cmdNo;
			this.cmdYes.img = (this.cmdNo.img = GameScr.imgNut);
			this.cmdYes.imgFocus = (this.cmdNo.imgFocus = GameScr.imgNutF);
			this.cmdYes.w = mGraphics.getImageWidth(cmdYes.img);
			this.cmdNo.w = mGraphics.getImageWidth(cmdYes.img);
			this.cmdYes.h = mGraphics.getImageHeight(cmdYes.img);
			this.cmdNo.h = mGraphics.getImageHeight(cmdYes.img);
			this.last = mSystem.currentTimeMillis();
			this.dem = this.info[0].Length / 3;
			if (this.dem < 15)
			{
				this.dem = 15;
			}
			TextInfo.reset();
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x001A5ED0 File Offset: 0x001A40D0
		public void paint(mGraphics g)
		{
			PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H + ((!GameCanvas.isTouch) ? 10 : 0), 16777215, false);
			if (this.info != null)
			{
				TextInfo.paint(g, this.info[0], this.X + 5, this.Y + this.H / 2 - ((!GameCanvas.isTouch) ? 6 : 4), this.W - 10, this.H, mFont.tahoma_7);
				if (GameCanvas.isTouch)
				{
					this.cmdYes.paint(g);
					mFont.tahoma_7_yellow.drawString(g, this.dem.ToString() + string.Empty, this.cmdYes.x + this.cmdYes.w / 2, this.cmdYes.y + this.cmdYes.h + 5, 2, mFont.tahoma_7_grey);
					return;
				}
				if (TField.isQwerty)
				{
					mFont.tahoma_7b_blue.drawString(g, mResources.do_accept_qwerty + this.dem.ToString() + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
					return;
				}
				mFont.tahoma_7b_blue.drawString(g, mResources.do_accept + this.dem.ToString() + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x001A605C File Offset: 0x001A425C
		public void update()
		{
			if (this.info != null)
			{
				this.X = GameCanvas.w - 5 - this.W;
				this.Y = 45;
				if (GameCanvas.w - 50 > 155 + this.W)
				{
					this.X = GameCanvas.w - 55 - this.W;
					this.Y = 5;
				}
				this.cmdYes.x = this.X - 35;
				this.cmdYes.y = this.Y;
				this.curr = mSystem.currentTimeMillis();
				Res.outz("curr - last= " + (this.curr - this.last).ToString());
				if (this.curr - this.last >= 1000L)
				{
					this.last = mSystem.currentTimeMillis();
					this.dem--;
				}
				if (this.dem == 0)
				{
					GameScr.gI().popUpYesNo = null;
				}
			}
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x04003406 RID: 13318
		public Command cmdYes;

		// Token: 0x04003407 RID: 13319
		public Command cmdNo;

		// Token: 0x04003408 RID: 13320
		public string[] info;

		// Token: 0x04003409 RID: 13321
		private int X;

		// Token: 0x0400340A RID: 13322
		private int Y;

		// Token: 0x0400340B RID: 13323
		private int W = 120;

		// Token: 0x0400340C RID: 13324
		private int H;

		// Token: 0x0400340D RID: 13325
		private int dem;

		// Token: 0x0400340E RID: 13326
		private long last;

		// Token: 0x0400340F RID: 13327
		private long curr;
	}
}
