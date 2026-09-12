using System;

namespace Game5
{
	// Token: 0x02000174 RID: 372
	public class ServerEffect : Effect2
	{
		// Token: 0x060010CA RID: 4298 RVA: 0x00113A38 File Offset: 0x00111C38
		public static void addServerEffect(int id, int cx, int cy, int loopCount)
		{
			ServerEffect serverEffect = new ServerEffect
			{
				eff = GameScr.efs[id - 1],
				x = cx,
				y = cy,
				loopCount = (short)loopCount
			};
			Effect2.vEffect2.addElement(serverEffect);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00113A7C File Offset: 0x00111C7C
		public static void addServerEffect(int id, int cx, int cy, int loopCount, int trans)
		{
			ServerEffect serverEffect = new ServerEffect
			{
				eff = GameScr.efs[id - 1],
				x = cx,
				y = cy,
				loopCount = (short)loopCount,
				trans = trans
			};
			Effect2.vEffect2.addElement(serverEffect);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00113AC8 File Offset: 0x00111CC8
		public static void addServerEffect(int id, Mob m, int loopCount)
		{
			ServerEffect serverEffect = new ServerEffect
			{
				eff = GameScr.efs[id - 1],
				m = m,
				loopCount = (short)loopCount
			};
			Effect2.vEffect2.addElement(serverEffect);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00113B04 File Offset: 0x00111D04
		public static void addServerEffect(int id, Char c, int loopCount)
		{
			ServerEffect serverEffect = new ServerEffect();
			serverEffect.eff = GameScr.efs[id - 1];
			serverEffect.c = c;
			serverEffect.loopCount = (short)loopCount;
			Effect2.vEffect2.addElement(serverEffect);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00113B40 File Offset: 0x00111D40
		public override void paint(mGraphics g)
		{
			if (mGraphics.zoomLevel == 1)
			{
				GameScr.countEff++;
			}
			if (GameScr.countEff < 8)
			{
				if (this.c != null)
				{
					this.x = this.c.cx;
					this.y = this.c.cy + GameCanvas.transY;
				}
				if (this.m != null)
				{
					this.x = this.m.x;
					this.y = this.m.y + GameCanvas.transY;
				}
				int num = this.x + this.dx0 + this.eff.arrEfInfo[this.i0].dx;
				int num2 = this.y + this.dy0 + this.eff.arrEfInfo[this.i0].dy;
				if (GameCanvas.isPaint(num, num2))
				{
					SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.i0].idImg, num, num2, this.trans, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00113C54 File Offset: 0x00111E54
		public override void update()
		{
			if (this.endTime != 0L)
			{
				this.i0++;
				if (this.i0 >= this.eff.arrEfInfo.Length)
				{
					this.i0 = 0;
				}
				if (mSystem.currentTimeMillis() - this.endTime > 0L)
				{
					Effect2.vEffect2.removeElement(this);
				}
			}
			else
			{
				this.i0++;
				if (this.i0 >= this.eff.arrEfInfo.Length)
				{
					this.loopCount -= 1;
					if (this.loopCount <= 0)
					{
						Effect2.vEffect2.removeElement(this);
					}
					else
					{
						this.i0 = 0;
					}
				}
			}
			if (GameCanvas.gameTick % 11 == 0 && this.c != null && this.c != Char.myCharz() && !GameScr.vCharInMap.contains(this.c))
			{
				Effect2.vEffect2.removeElement(this);
			}
		}

		// Token: 0x040021FC RID: 8700
		public EffectCharPaint eff;

		// Token: 0x040021FD RID: 8701
		private int i0;

		// Token: 0x040021FE RID: 8702
		private int dx0;

		// Token: 0x040021FF RID: 8703
		private int dy0;

		// Token: 0x04002200 RID: 8704
		private int x;

		// Token: 0x04002201 RID: 8705
		private int y;

		// Token: 0x04002202 RID: 8706
		private Char c;

		// Token: 0x04002203 RID: 8707
		private Mob m;

		// Token: 0x04002204 RID: 8708
		private short loopCount;

		// Token: 0x04002205 RID: 8709
		private long endTime;

		// Token: 0x04002206 RID: 8710
		private int trans;
	}
}
