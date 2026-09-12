using System;

namespace Game4
{
	// Token: 0x02000273 RID: 627
	public class TransportScr : mScreen, IActionListener
	{
		// Token: 0x06001C1C RID: 7196 RVA: 0x001B8268 File Offset: 0x001B6468
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

		// Token: 0x06001C1D RID: 7197 RVA: 0x001B8342 File Offset: 0x001B6542
		public static TransportScr gI()
		{
			if (TransportScr.instance == null)
			{
				TransportScr.instance = new TransportScr();
			}
			return TransportScr.instance;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x001B835C File Offset: 0x001B655C
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

		// Token: 0x06001C1F RID: 7199 RVA: 0x001B83E4 File Offset: 0x001B65E4
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

		// Token: 0x06001C20 RID: 7200 RVA: 0x001B850C File Offset: 0x001B670C
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

		// Token: 0x06001C21 RID: 7201 RVA: 0x001B86E2 File Offset: 0x001B68E2
		public override void updateKey()
		{
			base.updateKey();
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x001B86EC File Offset: 0x001B68EC
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

		// Token: 0x04003695 RID: 13973
		public static TransportScr instance;

		// Token: 0x04003696 RID: 13974
		public static Image ship;

		// Token: 0x04003697 RID: 13975
		public static Image taungam;

		// Token: 0x04003698 RID: 13976
		public sbyte type;

		// Token: 0x04003699 RID: 13977
		public int speed = 5;

		// Token: 0x0400369A RID: 13978
		public int[] posX;

		// Token: 0x0400369B RID: 13979
		public int[] posY;

		// Token: 0x0400369C RID: 13980
		public int[] posX2;

		// Token: 0x0400369D RID: 13981
		public int[] posY2;

		// Token: 0x0400369E RID: 13982
		private int cmx;

		// Token: 0x0400369F RID: 13983
		private int n = 20;

		// Token: 0x040036A0 RID: 13984
		public short time;

		// Token: 0x040036A1 RID: 13985
		public short maxTime;

		// Token: 0x040036A2 RID: 13986
		public long last;

		// Token: 0x040036A3 RID: 13987
		public long curr;

		// Token: 0x040036A4 RID: 13988
		private bool isSpeed;

		// Token: 0x040036A5 RID: 13989
		private bool transNow;

		// Token: 0x040036A6 RID: 13990
		private int currSpeed;
	}
}
