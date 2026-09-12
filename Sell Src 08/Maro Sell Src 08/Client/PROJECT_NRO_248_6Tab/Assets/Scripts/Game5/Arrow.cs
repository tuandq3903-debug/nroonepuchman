using System;

namespace Game5
{
	// Token: 0x020000E3 RID: 227
	public class Arrow
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x000975D0 File Offset: 0x000957D0
		public Arrow(Char charBelong, Arrowpaint arrp)
		{
			this.charBelong = charBelong;
			this.arrp = arrp;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000975E8 File Offset: 0x000957E8
		public void update()
		{
			if (this.charBelong.mobFocus == null && this.charBelong.charFocus == null)
			{
				this.endMe();
				return;
			}
			if (this.charBelong.mobFocus != null)
			{
				this.axTo = this.charBelong.mobFocus.x;
				this.ayTo = this.charBelong.mobFocus.y - this.charBelong.mobFocus.h / 4;
			}
			else if (this.charBelong.charFocus != null)
			{
				this.axTo = this.charBelong.charFocus.cx;
				this.ayTo = this.charBelong.charFocus.cy - this.charBelong.charFocus.ch / 4;
			}
			int num = this.axTo - this.ax;
			int num2 = this.ayTo - this.ay;
			int num3 = 5;
			int num4 = 4;
			if (num + num2 < 60)
			{
				num4 = 3;
			}
			else if (num + num2 < 30)
			{
				num4 = 2;
			}
			if (this.ax != this.axTo)
			{
				if (num > 0 && num < num3)
				{
					this.ax = this.axTo;
				}
				else if (num < 0 && num > -num3)
				{
					this.ax = this.axTo;
				}
				else
				{
					this.avx = this.axTo - this.ax << 2;
					this.adx += this.avx;
					this.ax += this.adx >> num4;
					this.adx &= 15;
				}
			}
			if (this.ay != this.ayTo)
			{
				if (num2 > 0 && num2 < num3)
				{
					this.ay = this.ayTo;
				}
				else if (num2 < 0 && num2 > -num3)
				{
					this.ay = this.ayTo;
				}
				else
				{
					this.avy = this.ayTo - this.ay << 2;
					this.ady += this.avy;
					this.ay += this.ady >> num4;
					this.ady &= 15;
				}
			}
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			if (this.charBelong.mobFocus != null)
			{
				num5 = this.axTo - this.charBelong.mobFocus.w / 4;
				num7 = this.axTo + this.charBelong.mobFocus.w / 4;
				num6 = this.ayTo - this.charBelong.mobFocus.h / 4;
				num8 = this.ayTo + this.charBelong.mobFocus.h / 4;
			}
			else if (this.charBelong.charFocus != null)
			{
				num5 = this.axTo - this.charBelong.charFocus.cw / 4;
				num7 = this.axTo + this.charBelong.charFocus.cw / 4;
				num6 = this.ayTo - this.charBelong.charFocus.ch / 4;
				num8 = this.ayTo + this.charBelong.charFocus.ch / 4;
			}
			if (this.life > 0)
			{
				this.life--;
			}
			if (this.life == 0 || (this.ax >= num5 && this.ax <= num7 && this.ay >= num6 && this.ay <= num8))
			{
				this.endMe();
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00097948 File Offset: 0x00095B48
		private void endMe()
		{
			this.charBelong.arr = null;
			this.ax = (this.ay = (this.axTo = (this.ayTo = (this.avx = (this.avy = (this.adx = (this.ady = 0)))))));
			this.charBelong.setAttack();
			if (this.charBelong.me)
			{
				this.charBelong.saveLoadPreviousSkill();
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000979CC File Offset: 0x00095BCC
		public void paint(mGraphics g)
		{
			int dx = this.axTo - this.ax;
			int num = this.ayTo - this.ay;
			int num2 = Arrow.findDirIndexFromAngle(Res.angle(dx, -num));
			SmallImage.drawSmallImage(g, this.arrp.imgId[(int)Arrow.FRAME[num2]], this.ax, this.ay, Arrow.TRANSFORM[num2], mGraphics.VCENTER | mGraphics.HCENTER);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00097A38 File Offset: 0x00095C38
		public static int findDirIndexFromAngle(int angle)
		{
			int i = 0;
			while (i < Arrow.ARROWINDEX.Length - 1)
			{
				if (angle >= Arrow.ARROWINDEX[i] && angle <= Arrow.ARROWINDEX[i + 1])
				{
					if (i >= 16)
					{
						return 0;
					}
					return i;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x040012A7 RID: 4775
		public int life;

		// Token: 0x040012A8 RID: 4776
		public int ax;

		// Token: 0x040012A9 RID: 4777
		public int ay;

		// Token: 0x040012AA RID: 4778
		public int axTo;

		// Token: 0x040012AB RID: 4779
		public int ayTo;

		// Token: 0x040012AC RID: 4780
		public int avx;

		// Token: 0x040012AD RID: 4781
		public int avy;

		// Token: 0x040012AE RID: 4782
		public int adx;

		// Token: 0x040012AF RID: 4783
		public int ady;

		// Token: 0x040012B0 RID: 4784
		public Char charBelong;

		// Token: 0x040012B1 RID: 4785
		public Arrowpaint arrp;

		// Token: 0x040012B2 RID: 4786
		public static sbyte[] FRAME = new sbyte[]
		{
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0
		};

		// Token: 0x040012B3 RID: 4787
		public static int[] ARROWINDEX = new int[]
		{
			0,
			15,
			37,
			52,
			75,
			105,
			127,
			142,
			165,
			195,
			217,
			232,
			255,
			285,
			307,
			322,
			345,
			370
		};

		// Token: 0x040012B4 RID: 4788
		public static int[] TRANSFORM = new int[]
		{
			0,
			0,
			0,
			7,
			6,
			6,
			6,
			2,
			2,
			3,
			3,
			4,
			5,
			5,
			5,
			1
		};
	}
}
