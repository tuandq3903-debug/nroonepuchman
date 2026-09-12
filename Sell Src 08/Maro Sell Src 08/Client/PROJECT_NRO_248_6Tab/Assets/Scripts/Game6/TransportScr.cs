using System;

namespace Game6
{
	// Token: 0x020000C3 RID: 195
	public class TransportScr : mScreen, IActionListener
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0008E0AC File Offset: 0x0008C2AC
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

		// Token: 0x060008D5 RID: 2261 RVA: 0x0008E186 File Offset: 0x0008C386
		public static TransportScr gI()
		{
			if (TransportScr.instance == null)
			{
				TransportScr.instance = new TransportScr();
			}
			return TransportScr.instance;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0008E1A0 File Offset: 0x0008C3A0
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

		// Token: 0x060008D7 RID: 2263 RVA: 0x0008E228 File Offset: 0x0008C428
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

		// Token: 0x060008D8 RID: 2264 RVA: 0x0008E350 File Offset: 0x0008C550
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

		// Token: 0x060008D9 RID: 2265 RVA: 0x0008E526 File Offset: 0x0008C726
		public override void updateKey()
		{
			base.updateKey();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0008E530 File Offset: 0x0008C730
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

		// Token: 0x04001197 RID: 4503
		public static TransportScr instance;

		// Token: 0x04001198 RID: 4504
		public static Image ship;

		// Token: 0x04001199 RID: 4505
		public static Image taungam;

		// Token: 0x0400119A RID: 4506
		public sbyte type;

		// Token: 0x0400119B RID: 4507
		public int speed = 5;

		// Token: 0x0400119C RID: 4508
		public int[] posX;

		// Token: 0x0400119D RID: 4509
		public int[] posY;

		// Token: 0x0400119E RID: 4510
		public int[] posX2;

		// Token: 0x0400119F RID: 4511
		public int[] posY2;

		// Token: 0x040011A0 RID: 4512
		private int cmx;

		// Token: 0x040011A1 RID: 4513
		private int n = 20;

		// Token: 0x040011A2 RID: 4514
		public short time;

		// Token: 0x040011A3 RID: 4515
		public short maxTime;

		// Token: 0x040011A4 RID: 4516
		public long last;

		// Token: 0x040011A5 RID: 4517
		public long curr;

		// Token: 0x040011A6 RID: 4518
		private bool isSpeed;

		// Token: 0x040011A7 RID: 4519
		private bool transNow;

		// Token: 0x040011A8 RID: 4520
		private int currSpeed;
	}
}
