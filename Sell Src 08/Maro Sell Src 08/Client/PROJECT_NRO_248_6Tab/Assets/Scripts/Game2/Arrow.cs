using System;

namespace Game2
{
	// Token: 0x0200036B RID: 875
	public class Arrow
	{
		// Token: 0x060026A6 RID: 9894 RVA: 0x002567BC File Offset: 0x002549BC
		public Arrow(Char charBelong, Arrowpaint arrp)
		{
			this.charBelong = charBelong;
			this.arrp = arrp;
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x002567D4 File Offset: 0x002549D4
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

		// Token: 0x060026A8 RID: 9896 RVA: 0x00256B34 File Offset: 0x00254D34
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

		// Token: 0x060026A9 RID: 9897 RVA: 0x00256BB8 File Offset: 0x00254DB8
		public void paint(mGraphics g)
		{
			int dx = this.axTo - this.ax;
			int num = this.ayTo - this.ay;
			int num2 = Arrow.findDirIndexFromAngle(Res.angle(dx, -num));
			SmallImage.drawSmallImage(g, this.arrp.imgId[(int)Arrow.FRAME[num2]], this.ax, this.ay, Arrow.TRANSFORM[num2], mGraphics.VCENTER | mGraphics.HCENTER);
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x00256C24 File Offset: 0x00254E24
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

		// Token: 0x04004A24 RID: 18980
		public int life;

		// Token: 0x04004A25 RID: 18981
		public int ax;

		// Token: 0x04004A26 RID: 18982
		public int ay;

		// Token: 0x04004A27 RID: 18983
		public int axTo;

		// Token: 0x04004A28 RID: 18984
		public int ayTo;

		// Token: 0x04004A29 RID: 18985
		public int avx;

		// Token: 0x04004A2A RID: 18986
		public int avy;

		// Token: 0x04004A2B RID: 18987
		public int adx;

		// Token: 0x04004A2C RID: 18988
		public int ady;

		// Token: 0x04004A2D RID: 18989
		public Char charBelong;

		// Token: 0x04004A2E RID: 18990
		public Arrowpaint arrp;

		// Token: 0x04004A2F RID: 18991
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

		// Token: 0x04004A30 RID: 18992
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

		// Token: 0x04004A31 RID: 18993
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
