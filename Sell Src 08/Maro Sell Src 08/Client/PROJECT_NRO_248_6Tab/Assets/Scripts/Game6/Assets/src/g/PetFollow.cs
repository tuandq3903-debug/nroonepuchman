using System;

namespace Game6.Assets.src.g
{
	// Token: 0x020000D9 RID: 217
	public class PetFollow
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x000940D7 File Offset: 0x000922D7
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00094115 File Offset: 0x00092315
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

		// Token: 0x06000995 RID: 2453 RVA: 0x00094138 File Offset: 0x00092338
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

		// Token: 0x06000996 RID: 2454 RVA: 0x000941AC File Offset: 0x000923AC
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

		// Token: 0x06000997 RID: 2455 RVA: 0x00094200 File Offset: 0x00092400
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 1);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00094228 File Offset: 0x00092428
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

		// Token: 0x0400125E RID: 4702
		public short smallID;

		// Token: 0x0400125F RID: 4703
		public Info info = new Info();

		// Token: 0x04001260 RID: 4704
		public int dir;

		// Token: 0x04001261 RID: 4705
		public int f;

		// Token: 0x04001262 RID: 4706
		public int tF;

		// Token: 0x04001263 RID: 4707
		public int cmtoY;

		// Token: 0x04001264 RID: 4708
		public int cmy;

		// Token: 0x04001265 RID: 4709
		public int cmdy;

		// Token: 0x04001266 RID: 4710
		public int cmvy;

		// Token: 0x04001267 RID: 4711
		public int cmyLim;

		// Token: 0x04001268 RID: 4712
		public int cmtoX;

		// Token: 0x04001269 RID: 4713
		public int cmx;

		// Token: 0x0400126A RID: 4714
		public int cmdx;

		// Token: 0x0400126B RID: 4715
		public int cmvx;

		// Token: 0x0400126C RID: 4716
		public int cmxLim;

		// Token: 0x0400126D RID: 4717
		public int fimg = -1;

		// Token: 0x0400126E RID: 4718
		public int wimg;

		// Token: 0x0400126F RID: 4719
		public int himg;

		// Token: 0x04001270 RID: 4720
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x04001271 RID: 4721
		private int count;
	}
}
