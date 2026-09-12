using System;

namespace Game1.Assets.src.g
{
	// Token: 0x02000511 RID: 1297
	public class PetFollow
	{
		// Token: 0x060039C7 RID: 14791 RVA: 0x0037D40B File Offset: 0x0037B60B
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x0037D449 File Offset: 0x0037B649
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

		// Token: 0x060039C9 RID: 14793 RVA: 0x0037D46C File Offset: 0x0037B66C
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

		// Token: 0x060039CA RID: 14794 RVA: 0x0037D4E0 File Offset: 0x0037B6E0
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

		// Token: 0x060039CB RID: 14795 RVA: 0x0037D534 File Offset: 0x0037B734
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 1);
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x0037D55C File Offset: 0x0037B75C
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

		// Token: 0x04006ED9 RID: 28377
		public short smallID;

		// Token: 0x04006EDA RID: 28378
		public Info info = new Info();

		// Token: 0x04006EDB RID: 28379
		public int dir;

		// Token: 0x04006EDC RID: 28380
		public int f;

		// Token: 0x04006EDD RID: 28381
		public int tF;

		// Token: 0x04006EDE RID: 28382
		public int cmtoY;

		// Token: 0x04006EDF RID: 28383
		public int cmy;

		// Token: 0x04006EE0 RID: 28384
		public int cmdy;

		// Token: 0x04006EE1 RID: 28385
		public int cmvy;

		// Token: 0x04006EE2 RID: 28386
		public int cmyLim;

		// Token: 0x04006EE3 RID: 28387
		public int cmtoX;

		// Token: 0x04006EE4 RID: 28388
		public int cmx;

		// Token: 0x04006EE5 RID: 28389
		public int cmdx;

		// Token: 0x04006EE6 RID: 28390
		public int cmvx;

		// Token: 0x04006EE7 RID: 28391
		public int cmxLim;

		// Token: 0x04006EE8 RID: 28392
		public int fimg = -1;

		// Token: 0x04006EE9 RID: 28393
		public int wimg;

		// Token: 0x04006EEA RID: 28394
		public int himg;

		// Token: 0x04006EEB RID: 28395
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x04006EEC RID: 28396
		private int count;
	}
}
