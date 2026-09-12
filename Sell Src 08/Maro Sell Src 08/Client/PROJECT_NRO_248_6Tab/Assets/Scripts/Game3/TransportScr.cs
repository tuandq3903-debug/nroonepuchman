using System;

namespace Game3
{
	// Token: 0x0200034B RID: 843
	public class TransportScr : mScreen, IActionListener
	{
		// Token: 0x060025C0 RID: 9664 RVA: 0x0024D30C File Offset: 0x0024B50C
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

		// Token: 0x060025C1 RID: 9665 RVA: 0x0024D3E6 File Offset: 0x0024B5E6
		public static TransportScr gI()
		{
			if (TransportScr.instance == null)
			{
				TransportScr.instance = new TransportScr();
			}
			return TransportScr.instance;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x0024D400 File Offset: 0x0024B600
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

		// Token: 0x060025C3 RID: 9667 RVA: 0x0024D488 File Offset: 0x0024B688
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

		// Token: 0x060025C4 RID: 9668 RVA: 0x0024D5B0 File Offset: 0x0024B7B0
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

		// Token: 0x060025C5 RID: 9669 RVA: 0x0024D786 File Offset: 0x0024B986
		public override void updateKey()
		{
			base.updateKey();
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0024D790 File Offset: 0x0024B990
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

		// Token: 0x04004914 RID: 18708
		public static TransportScr instance;

		// Token: 0x04004915 RID: 18709
		public static Image ship;

		// Token: 0x04004916 RID: 18710
		public static Image taungam;

		// Token: 0x04004917 RID: 18711
		public sbyte type;

		// Token: 0x04004918 RID: 18712
		public int speed = 5;

		// Token: 0x04004919 RID: 18713
		public int[] posX;

		// Token: 0x0400491A RID: 18714
		public int[] posY;

		// Token: 0x0400491B RID: 18715
		public int[] posX2;

		// Token: 0x0400491C RID: 18716
		public int[] posY2;

		// Token: 0x0400491D RID: 18717
		private int cmx;

		// Token: 0x0400491E RID: 18718
		private int n = 20;

		// Token: 0x0400491F RID: 18719
		public short time;

		// Token: 0x04004920 RID: 18720
		public short maxTime;

		// Token: 0x04004921 RID: 18721
		public long last;

		// Token: 0x04004922 RID: 18722
		public long curr;

		// Token: 0x04004923 RID: 18723
		private bool isSpeed;

		// Token: 0x04004924 RID: 18724
		private bool transNow;

		// Token: 0x04004925 RID: 18725
		private int currSpeed;
	}
}
