using System;

namespace Game5.Assets.src.g
{
	// Token: 0x020001B1 RID: 433
	public class PetFollow
	{
		// Token: 0x06001337 RID: 4919 RVA: 0x0012917B File Offset: 0x0012737B
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x001291B9 File Offset: 0x001273B9
		public void SetImg(int fimg, int[] frameNew, int wimg, int himg)
		{
			if (fimg >= 1)
			{
				this.fimg = fimg;
				this.frame = frameNew;
				this.wimg = wimg;
				this.himg = himg;
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x001291DC File Offset: 0x001273DC
		public void paint(mGraphics g)
		{
			int w = 32;
			int h = 32;
			int num = (GameCanvas.gameTick % 10 > 5) ? 1 : 0;
			if (this.fimg > 0)
			{
				w = this.wimg;
				h = this.himg;
				num = 0;
			}
			SmallImage.drawSmallImage(g, (int)this.smallID, this.f, this.cmx, this.cmy + 3 + num, w, h, (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00129250 File Offset: 0x00127450
		public void update()
		{
			this.moveCamera();
			if (GameCanvas.gameTick % 3 == 0)
			{
				this.f = this.frame[this.count];
				this.count++;
			}
			if (this.count >= this.frame.Length)
			{
				this.count = 0;
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x001292A4 File Offset: 0x001274A4
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 1);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x001292CC File Offset: 0x001274CC
		public void moveCamera()
		{
			if (this.cmy != this.cmtoY)
			{
				this.cmvy = this.cmtoY - this.cmy << 2;
				this.cmdy += this.cmvy;
				this.cmy += this.cmdy >> 4;
				this.cmdy &= 15;
			}
			if (this.cmx != this.cmtoX)
			{
				this.cmvx = this.cmtoX - this.cmx << 2;
				this.cmdx += this.cmvx;
				this.cmx += this.cmdx >> 4;
				this.cmdx &= 15;
			}
		}

		// Token: 0x040024DD RID: 9437
		public short smallID;

		// Token: 0x040024DE RID: 9438
		public Info info = new Info();

		// Token: 0x040024DF RID: 9439
		public int dir;

		// Token: 0x040024E0 RID: 9440
		public int f;

		// Token: 0x040024E1 RID: 9441
		public int tF;

		// Token: 0x040024E2 RID: 9442
		public int cmtoY;

		// Token: 0x040024E3 RID: 9443
		public int cmy;

		// Token: 0x040024E4 RID: 9444
		public int cmdy;

		// Token: 0x040024E5 RID: 9445
		public int cmvy;

		// Token: 0x040024E6 RID: 9446
		public int cmyLim;

		// Token: 0x040024E7 RID: 9447
		public int cmtoX;

		// Token: 0x040024E8 RID: 9448
		public int cmx;

		// Token: 0x040024E9 RID: 9449
		public int cmdx;

		// Token: 0x040024EA RID: 9450
		public int cmvx;

		// Token: 0x040024EB RID: 9451
		public int cmxLim;

		// Token: 0x040024EC RID: 9452
		public int fimg = -1;

		// Token: 0x040024ED RID: 9453
		public int wimg;

		// Token: 0x040024EE RID: 9454
		public int himg;

		// Token: 0x040024EF RID: 9455
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x040024F0 RID: 9456
		private int count;
	}
}
