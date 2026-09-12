using System;

namespace Game4
{
	// Token: 0x0200024C RID: 588
	public class ServerEffect : Effect2
	{
		// Token: 0x06001A6E RID: 6766 RVA: 0x001A8ADC File Offset: 0x001A6CDC
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

		// Token: 0x06001A6F RID: 6767 RVA: 0x001A8B20 File Offset: 0x001A6D20
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

		// Token: 0x06001A70 RID: 6768 RVA: 0x001A8B6C File Offset: 0x001A6D6C
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

		// Token: 0x06001A71 RID: 6769 RVA: 0x001A8BA8 File Offset: 0x001A6DA8
		public static void addServerEffect(int id, Char c, int loopCount)
		{
			ServerEffect serverEffect = new ServerEffect();
			serverEffect.eff = GameScr.efs[id - 1];
			serverEffect.c = c;
			serverEffect.loopCount = (short)loopCount;
			Effect2.vEffect2.addElement(serverEffect);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x001A8BE4 File Offset: 0x001A6DE4
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

		// Token: 0x06001A73 RID: 6771 RVA: 0x001A8CF8 File Offset: 0x001A6EF8
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

		// Token: 0x0400347B RID: 13435
		public EffectCharPaint eff;

		// Token: 0x0400347C RID: 13436
		private int i0;

		// Token: 0x0400347D RID: 13437
		private int dx0;

		// Token: 0x0400347E RID: 13438
		private int dy0;

		// Token: 0x0400347F RID: 13439
		private int x;

		// Token: 0x04003480 RID: 13440
		private int y;

		// Token: 0x04003481 RID: 13441
		private Char c;

		// Token: 0x04003482 RID: 13442
		private Mob m;

		// Token: 0x04003483 RID: 13443
		private short loopCount;

		// Token: 0x04003484 RID: 13444
		private long endTime;

		// Token: 0x04003485 RID: 13445
		private int trans;
	}
}
