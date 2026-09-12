using System;

namespace Game1
{
	// Token: 0x020004FB RID: 1275
	public class TransportScr : mScreen, IActionListener
	{
		// Token: 0x06003908 RID: 14600 RVA: 0x00377454 File Offset: 0x00375654
		public TransportScr()
		{
			this.posX = new int[this.n];
			this.posY = new int[this.n];
			for (int i = 0; i < this.n; i++)
			{
				this.posX[i] = Res.random(0, GameCanvas.w);
				this.posY[i] = i * (GameCanvas.h / this.n);
			}
			this.posX2 = new int[this.n];
			this.posY2 = new int[this.n];
			for (int j = 0; j < this.n; j++)
			{
				this.posX2[j] = Res.random(0, GameCanvas.w);
				this.posY2[j] = j * (GameCanvas.h / this.n);
			}
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x0037752E File Offset: 0x0037572E
		public static TransportScr gI()
		{
			if (TransportScr.instance == null)
			{
				TransportScr.instance = new TransportScr();
			}
			return TransportScr.instance;
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x00377548 File Offset: 0x00375748
		public override void switchToMe()
		{
			if (TransportScr.ship == null)
			{
				TransportScr.ship = GameCanvas.loadImage("/mainImage/myTexture2dfutherShip.png");
			}
			if (TransportScr.taungam == null)
			{
				TransportScr.taungam = GameCanvas.loadImage("/mainImage/taungam.png");
			}
			this.isSpeed = false;
			this.transNow = false;
			if (Char.myCharz().checkLuong() > 0 && this.type == 0)
			{
				this.center = new Command(mResources.faster, this, 1, null);
			}
			else
			{
				this.center = null;
			}
			this.currSpeed = 0;
			base.switchToMe();
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x003775D0 File Offset: 0x003757D0
		public override void paint(mGraphics g)
		{
			g.setColor((this.type != 0) ? 3056895 : 0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			for (int i = 0; i < this.n; i++)
			{
				g.setColor((this.type != 0) ? 11140863 : 14802654);
				g.fillRect(this.posX[i], this.posY[i], 10, 2);
			}
			if (this.type == 0)
			{
				g.drawRegion(TransportScr.ship, 0, 0, 72, 95, 7, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
			}
			if (this.type == 1)
			{
				g.drawRegion(TransportScr.taungam, 0, 0, 144, 79, 2, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
			}
			for (int j = 0; j < this.n; j++)
			{
				g.setColor((this.type != 0) ? 7536127 : 14935011);
				g.fillRect(this.posX2[j], this.posY2[j], 18, 3);
			}
			base.paint(g);
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x003776F8 File Offset: 0x003758F8
		public override void update()
		{
			if (this.type == 0)
			{
				if (!this.isSpeed)
				{
					this.currSpeed = GameCanvas.w / 2 * (int)this.time / (int)this.maxTime;
				}
			}
			else
			{
				this.currSpeed += 2;
			}
			Controller.isStopReadMessage = false;
			this.cmx = (((GameCanvas.w / 2 + this.cmx) / 2 + this.cmx) / 2 + this.cmx) / 2;
			if (this.type == 1)
			{
				this.cmx = 0;
			}
			for (int i = 0; i < this.n; i++)
			{
				this.posX[i] -= this.speed / 2;
				if (this.posX[i] < -20)
				{
					this.posX[i] = GameCanvas.w;
				}
			}
			for (int j = 0; j < this.n; j++)
			{
				this.posX2[j] -= this.speed;
				if (this.posX2[j] < -20)
				{
					this.posX2[j] = GameCanvas.w;
				}
			}
			if (GameCanvas.gameTick % 3 == 0)
			{
				this.speed += ((!this.isSpeed) ? 1 : 2);
			}
			if (this.speed > ((!this.isSpeed) ? 25 : 80))
			{
				this.speed = ((!this.isSpeed) ? 25 : 80);
			}
			this.curr = mSystem.currentTimeMillis();
			if (this.curr - this.last >= 1000L)
			{
				this.time += 1;
				this.last = this.curr;
			}
			if (this.isSpeed)
			{
				this.currSpeed += 3;
			}
			if (this.currSpeed >= GameCanvas.w / 2 + 30 && !this.transNow)
			{
				this.transNow = true;
				Service.gI().transportNow();
			}
			base.update();
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x003778CE File Offset: 0x00375ACE
		public override void updateKey()
		{
			base.updateKey();
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x003778D8 File Offset: 0x00375AD8
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				GameCanvas.startYesNoDlg(mResources.fasterQuestion, new Command(mResources.YES, this, 2, null), new Command(mResources.NO, this, 3, null));
			}
			if (idAction == 2 && Char.myCharz().checkLuong() > 0)
			{
				this.isSpeed = true;
				GameCanvas.endDlg();
				this.center = null;
			}
			if (idAction == 3)
			{
				GameCanvas.endDlg();
			}
		}

		// Token: 0x04006E12 RID: 28178
		public static TransportScr instance;

		// Token: 0x04006E13 RID: 28179
		public static Image ship;

		// Token: 0x04006E14 RID: 28180
		public static Image taungam;

		// Token: 0x04006E15 RID: 28181
		public sbyte type;

		// Token: 0x04006E16 RID: 28182
		public int speed = 5;

		// Token: 0x04006E17 RID: 28183
		public int[] posX;

		// Token: 0x04006E18 RID: 28184
		public int[] posY;

		// Token: 0x04006E19 RID: 28185
		public int[] posX2;

		// Token: 0x04006E1A RID: 28186
		public int[] posY2;

		// Token: 0x04006E1B RID: 28187
		private int cmx;

		// Token: 0x04006E1C RID: 28188
		private int n = 20;

		// Token: 0x04006E1D RID: 28189
		public short time;

		// Token: 0x04006E1E RID: 28190
		public short maxTime;

		// Token: 0x04006E1F RID: 28191
		public long last;

		// Token: 0x04006E20 RID: 28192
		public long curr;

		// Token: 0x04006E21 RID: 28193
		private bool isSpeed;

		// Token: 0x04006E22 RID: 28194
		private bool transNow;

		// Token: 0x04006E23 RID: 28195
		private int currSpeed;
	}
}
