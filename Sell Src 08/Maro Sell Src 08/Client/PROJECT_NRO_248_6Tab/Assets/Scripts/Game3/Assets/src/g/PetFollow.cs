using System;

namespace Game3.Assets.src.g
{
	// Token: 0x02000361 RID: 865
	public class PetFollow
	{
		// Token: 0x0600267F RID: 9855 RVA: 0x002532C3 File Offset: 0x002514C3
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x00253301 File Offset: 0x00251501
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

		// Token: 0x06002681 RID: 9857 RVA: 0x00253324 File Offset: 0x00251524
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

		// Token: 0x06002682 RID: 9858 RVA: 0x00253398 File Offset: 0x00251598
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

		// Token: 0x06002683 RID: 9859 RVA: 0x002533EC File Offset: 0x002515EC
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 1);
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x00253414 File Offset: 0x00251614
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

		// Token: 0x040049DB RID: 18907
		public short smallID;

		// Token: 0x040049DC RID: 18908
		public Info info = new Info();

		// Token: 0x040049DD RID: 18909
		public int dir;

		// Token: 0x040049DE RID: 18910
		public int f;

		// Token: 0x040049DF RID: 18911
		public int tF;

		// Token: 0x040049E0 RID: 18912
		public int cmtoY;

		// Token: 0x040049E1 RID: 18913
		public int cmy;

		// Token: 0x040049E2 RID: 18914
		public int cmdy;

		// Token: 0x040049E3 RID: 18915
		public int cmvy;

		// Token: 0x040049E4 RID: 18916
		public int cmyLim;

		// Token: 0x040049E5 RID: 18917
		public int cmtoX;

		// Token: 0x040049E6 RID: 18918
		public int cmx;

		// Token: 0x040049E7 RID: 18919
		public int cmdx;

		// Token: 0x040049E8 RID: 18920
		public int cmvx;

		// Token: 0x040049E9 RID: 18921
		public int cmxLim;

		// Token: 0x040049EA RID: 18922
		public int fimg = -1;

		// Token: 0x040049EB RID: 18923
		public int wimg;

		// Token: 0x040049EC RID: 18924
		public int himg;

		// Token: 0x040049ED RID: 18925
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x040049EE RID: 18926
		private int count;
	}
}
