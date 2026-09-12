using System;

namespace Game4.Assets.src.g
{
	// Token: 0x02000289 RID: 649
	public class PetFollow
	{
		// Token: 0x06001CDB RID: 7387 RVA: 0x001BE21F File Offset: 0x001BC41F
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x001BE25D File Offset: 0x001BC45D
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

		// Token: 0x06001CDD RID: 7389 RVA: 0x001BE280 File Offset: 0x001BC480
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

		// Token: 0x06001CDE RID: 7390 RVA: 0x001BE2F4 File Offset: 0x001BC4F4
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

		// Token: 0x06001CDF RID: 7391 RVA: 0x001BE348 File Offset: 0x001BC548
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 1);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x001BE370 File Offset: 0x001BC570
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

		// Token: 0x0400375C RID: 14172
		public short smallID;

		// Token: 0x0400375D RID: 14173
		public Info info = new Info();

		// Token: 0x0400375E RID: 14174
		public int dir;

		// Token: 0x0400375F RID: 14175
		public int f;

		// Token: 0x04003760 RID: 14176
		public int tF;

		// Token: 0x04003761 RID: 14177
		public int cmtoY;

		// Token: 0x04003762 RID: 14178
		public int cmy;

		// Token: 0x04003763 RID: 14179
		public int cmdy;

		// Token: 0x04003764 RID: 14180
		public int cmvy;

		// Token: 0x04003765 RID: 14181
		public int cmyLim;

		// Token: 0x04003766 RID: 14182
		public int cmtoX;

		// Token: 0x04003767 RID: 14183
		public int cmx;

		// Token: 0x04003768 RID: 14184
		public int cmdx;

		// Token: 0x04003769 RID: 14185
		public int cmvx;

		// Token: 0x0400376A RID: 14186
		public int cmxLim;

		// Token: 0x0400376B RID: 14187
		public int fimg = -1;

		// Token: 0x0400376C RID: 14188
		public int wimg;

		// Token: 0x0400376D RID: 14189
		public int himg;

		// Token: 0x0400376E RID: 14190
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x0400376F RID: 14191
		private int count;
	}
}
