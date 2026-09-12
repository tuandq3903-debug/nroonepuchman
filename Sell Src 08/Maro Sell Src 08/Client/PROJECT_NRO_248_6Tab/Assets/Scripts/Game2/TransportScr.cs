using System;

namespace Game2
{
	// Token: 0x02000423 RID: 1059
	public class TransportScr : mScreen, IActionListener
	{
		// Token: 0x06002F64 RID: 12132 RVA: 0x002E23B0 File Offset: 0x002E05B0
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

		// Token: 0x06002F65 RID: 12133 RVA: 0x002E248A File Offset: 0x002E068A
		public static TransportScr gI()
		{
			if (TransportScr.instance == null)
			{
				TransportScr.instance = new TransportScr();
			}
			return TransportScr.instance;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x002E24A4 File Offset: 0x002E06A4
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

		// Token: 0x06002F67 RID: 12135 RVA: 0x002E252C File Offset: 0x002E072C
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

		// Token: 0x06002F68 RID: 12136 RVA: 0x002E2654 File Offset: 0x002E0854
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

		// Token: 0x06002F69 RID: 12137 RVA: 0x002E282A File Offset: 0x002E0A2A
		public override void updateKey()
		{
			base.updateKey();
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x002E2834 File Offset: 0x002E0A34
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

		// Token: 0x04005B93 RID: 23443
		public static TransportScr instance;

		// Token: 0x04005B94 RID: 23444
		public static Image ship;

		// Token: 0x04005B95 RID: 23445
		public static Image taungam;

		// Token: 0x04005B96 RID: 23446
		public sbyte type;

		// Token: 0x04005B97 RID: 23447
		public int speed = 5;

		// Token: 0x04005B98 RID: 23448
		public int[] posX;

		// Token: 0x04005B99 RID: 23449
		public int[] posY;

		// Token: 0x04005B9A RID: 23450
		public int[] posX2;

		// Token: 0x04005B9B RID: 23451
		public int[] posY2;

		// Token: 0x04005B9C RID: 23452
		private int cmx;

		// Token: 0x04005B9D RID: 23453
		private int n = 20;

		// Token: 0x04005B9E RID: 23454
		public short time;

		// Token: 0x04005B9F RID: 23455
		public short maxTime;

		// Token: 0x04005BA0 RID: 23456
		public long last;

		// Token: 0x04005BA1 RID: 23457
		public long curr;

		// Token: 0x04005BA2 RID: 23458
		private bool isSpeed;

		// Token: 0x04005BA3 RID: 23459
		private bool transNow;

		// Token: 0x04005BA4 RID: 23460
		private int currSpeed;
	}
}
