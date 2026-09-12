using System;

namespace Game2.Assets.src.g
{
	// Token: 0x02000439 RID: 1081
	public class PetFollow
	{
		// Token: 0x06003023 RID: 12323 RVA: 0x002E8367 File Offset: 0x002E6567
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x002E83A5 File Offset: 0x002E65A5
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

		// Token: 0x06003025 RID: 12325 RVA: 0x002E83C8 File Offset: 0x002E65C8
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

		// Token: 0x06003026 RID: 12326 RVA: 0x002E843C File Offset: 0x002E663C
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

		// Token: 0x06003027 RID: 12327 RVA: 0x002E8490 File Offset: 0x002E6690
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 1);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x002E84B8 File Offset: 0x002E66B8
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

		// Token: 0x04005C5A RID: 23642
		public short smallID;

		// Token: 0x04005C5B RID: 23643
		public Info info = new Info();

		// Token: 0x04005C5C RID: 23644
		public int dir;

		// Token: 0x04005C5D RID: 23645
		public int f;

		// Token: 0x04005C5E RID: 23646
		public int tF;

		// Token: 0x04005C5F RID: 23647
		public int cmtoY;

		// Token: 0x04005C60 RID: 23648
		public int cmy;

		// Token: 0x04005C61 RID: 23649
		public int cmdy;

		// Token: 0x04005C62 RID: 23650
		public int cmvy;

		// Token: 0x04005C63 RID: 23651
		public int cmyLim;

		// Token: 0x04005C64 RID: 23652
		public int cmtoX;

		// Token: 0x04005C65 RID: 23653
		public int cmx;

		// Token: 0x04005C66 RID: 23654
		public int cmdx;

		// Token: 0x04005C67 RID: 23655
		public int cmvx;

		// Token: 0x04005C68 RID: 23656
		public int cmxLim;

		// Token: 0x04005C69 RID: 23657
		public int fimg = -1;

		// Token: 0x04005C6A RID: 23658
		public int wimg;

		// Token: 0x04005C6B RID: 23659
		public int himg;

		// Token: 0x04005C6C RID: 23660
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x04005C6D RID: 23661
		private int count;
	}
}
