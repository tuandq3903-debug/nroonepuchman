using System;

namespace Game1
{
	// Token: 0x020004D4 RID: 1236
	public class ServerEffect : Effect2
	{
		// Token: 0x0600375A RID: 14170 RVA: 0x00367CC8 File Offset: 0x00365EC8
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

		// Token: 0x0600375B RID: 14171 RVA: 0x00367D0C File Offset: 0x00365F0C
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

		// Token: 0x0600375C RID: 14172 RVA: 0x00367D58 File Offset: 0x00365F58
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

		// Token: 0x0600375D RID: 14173 RVA: 0x00367D94 File Offset: 0x00365F94
		public static void addServerEffect(int id, Char c, int loopCount)
		{
			ServerEffect serverEffect = new ServerEffect();
			serverEffect.eff = GameScr.efs[id - 1];
			serverEffect.c = c;
			serverEffect.loopCount = (short)loopCount;
			Effect2.vEffect2.addElement(serverEffect);
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x00367DD0 File Offset: 0x00365FD0
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

		// Token: 0x0600375F RID: 14175 RVA: 0x00367EE4 File Offset: 0x003660E4
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

		// Token: 0x04006BF8 RID: 27640
		public EffectCharPaint eff;

		// Token: 0x04006BF9 RID: 27641
		private int i0;

		// Token: 0x04006BFA RID: 27642
		private int dx0;

		// Token: 0x04006BFB RID: 27643
		private int dy0;

		// Token: 0x04006BFC RID: 27644
		private int x;

		// Token: 0x04006BFD RID: 27645
		private int y;

		// Token: 0x04006BFE RID: 27646
		private Char c;

		// Token: 0x04006BFF RID: 27647
		private Mob m;

		// Token: 0x04006C00 RID: 27648
		private short loopCount;

		// Token: 0x04006C01 RID: 27649
		private long endTime;

		// Token: 0x04006C02 RID: 27650
		private int trans;
	}
}
