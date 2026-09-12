using System;

namespace Game1
{
	// Token: 0x020004CC RID: 1228
	public class PopUpYesNo : IActionListener
	{
		// Token: 0x0600370E RID: 14094 RVA: 0x00364FBC File Offset: 0x003631BC
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

		// Token: 0x0600370F RID: 14095 RVA: 0x003650BC File Offset: 0x003632BC
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

		// Token: 0x06003710 RID: 14096 RVA: 0x00365248 File Offset: 0x00363448
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

		// Token: 0x06003711 RID: 14097 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x04006B83 RID: 27523
		public Command cmdYes;

		// Token: 0x04006B84 RID: 27524
		public Command cmdNo;

		// Token: 0x04006B85 RID: 27525
		public string[] info;

		// Token: 0x04006B86 RID: 27526
		private int X;

		// Token: 0x04006B87 RID: 27527
		private int Y;

		// Token: 0x04006B88 RID: 27528
		private int W = 120;

		// Token: 0x04006B89 RID: 27529
		private int H;

		// Token: 0x04006B8A RID: 27530
		private int dem;

		// Token: 0x04006B8B RID: 27531
		private long last;

		// Token: 0x04006B8C RID: 27532
		private long curr;
	}
}
