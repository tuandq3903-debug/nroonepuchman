using System;

namespace Game5
{
	// Token: 0x0200019B RID: 411
	public class TransportScr : mScreen, IActionListener
	{
		// Token: 0x06001278 RID: 4728 RVA: 0x001231C4 File Offset: 0x001213C4
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

		// Token: 0x06001279 RID: 4729 RVA: 0x0012329E File Offset: 0x0012149E
		public static TransportScr gI()
		{
			if (TransportScr.instance == null)
			{
				TransportScr.instance = new TransportScr();
			}
			return TransportScr.instance;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x001232B8 File Offset: 0x001214B8
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

		// Token: 0x0600127B RID: 4731 RVA: 0x00123340 File Offset: 0x00121540
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

		// Token: 0x0600127C RID: 4732 RVA: 0x00123468 File Offset: 0x00121668
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

		// Token: 0x0600127D RID: 4733 RVA: 0x0012363E File Offset: 0x0012183E
		public override void updateKey()
		{
			base.updateKey();
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00123648 File Offset: 0x00121848
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

		// Token: 0x04002416 RID: 9238
		public static TransportScr instance;

		// Token: 0x04002417 RID: 9239
		public static Image ship;

		// Token: 0x04002418 RID: 9240
		public static Image taungam;

		// Token: 0x04002419 RID: 9241
		public sbyte type;

		// Token: 0x0400241A RID: 9242
		public int speed = 5;

		// Token: 0x0400241B RID: 9243
		public int[] posX;

		// Token: 0x0400241C RID: 9244
		public int[] posY;

		// Token: 0x0400241D RID: 9245
		public int[] posX2;

		// Token: 0x0400241E RID: 9246
		public int[] posY2;

		// Token: 0x0400241F RID: 9247
		private int cmx;

		// Token: 0x04002420 RID: 9248
		private int n = 20;

		// Token: 0x04002421 RID: 9249
		public short time;

		// Token: 0x04002422 RID: 9250
		public short maxTime;

		// Token: 0x04002423 RID: 9251
		public long last;

		// Token: 0x04002424 RID: 9252
		public long curr;

		// Token: 0x04002425 RID: 9253
		private bool isSpeed;

		// Token: 0x04002426 RID: 9254
		private bool transNow;

		// Token: 0x04002427 RID: 9255
		private int currSpeed;
	}
}
