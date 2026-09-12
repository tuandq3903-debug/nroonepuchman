using System;

namespace Game6
{
	// Token: 0x02000094 RID: 148
	public class PopUpYesNo : IActionListener
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x0007BBD4 File Offset: 0x00079DD4
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

		// Token: 0x060006DB RID: 1755 RVA: 0x0007BCD4 File Offset: 0x00079ED4
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

		// Token: 0x060006DC RID: 1756 RVA: 0x0007BE60 File Offset: 0x0007A060
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

		// Token: 0x060006DD RID: 1757 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x04000F08 RID: 3848
		public Command cmdYes;

		// Token: 0x04000F09 RID: 3849
		public Command cmdNo;

		// Token: 0x04000F0A RID: 3850
		public string[] info;

		// Token: 0x04000F0B RID: 3851
		private int X;

		// Token: 0x04000F0C RID: 3852
		private int Y;

		// Token: 0x04000F0D RID: 3853
		private int W = 120;

		// Token: 0x04000F0E RID: 3854
		private int H;

		// Token: 0x04000F0F RID: 3855
		private int dem;

		// Token: 0x04000F10 RID: 3856
		private long last;

		// Token: 0x04000F11 RID: 3857
		private long curr;
	}
}
