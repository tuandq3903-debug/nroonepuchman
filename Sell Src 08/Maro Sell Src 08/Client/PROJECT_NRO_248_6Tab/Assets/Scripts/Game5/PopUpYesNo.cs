using System;

namespace Game5
{
	// Token: 0x0200016C RID: 364
	public class PopUpYesNo : IActionListener
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x00110D2C File Offset: 0x0010EF2C
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

		// Token: 0x0600107F RID: 4223 RVA: 0x00110E2C File Offset: 0x0010F02C
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

		// Token: 0x06001080 RID: 4224 RVA: 0x00110FB8 File Offset: 0x0010F1B8
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

		// Token: 0x06001081 RID: 4225 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x04002187 RID: 8583
		public Command cmdYes;

		// Token: 0x04002188 RID: 8584
		public Command cmdNo;

		// Token: 0x04002189 RID: 8585
		public string[] info;

		// Token: 0x0400218A RID: 8586
		private int X;

		// Token: 0x0400218B RID: 8587
		private int Y;

		// Token: 0x0400218C RID: 8588
		private int W = 120;

		// Token: 0x0400218D RID: 8589
		private int H;

		// Token: 0x0400218E RID: 8590
		private int dem;

		// Token: 0x0400218F RID: 8591
		private long last;

		// Token: 0x04002190 RID: 8592
		private long curr;
	}
}
