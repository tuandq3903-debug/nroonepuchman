using System;

namespace Game3
{
	// Token: 0x02000324 RID: 804
	public class ServerEffect : Effect2
	{
		// Token: 0x06002412 RID: 9234 RVA: 0x0023DB80 File Offset: 0x0023BD80
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

		// Token: 0x06002413 RID: 9235 RVA: 0x0023DBC4 File Offset: 0x0023BDC4
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

		// Token: 0x06002414 RID: 9236 RVA: 0x0023DC10 File Offset: 0x0023BE10
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

		// Token: 0x06002415 RID: 9237 RVA: 0x0023DC4C File Offset: 0x0023BE4C
		public static void addServerEffect(int id, Char c, int loopCount)
		{
			ServerEffect serverEffect = new ServerEffect();
			serverEffect.eff = GameScr.efs[id - 1];
			serverEffect.c = c;
			serverEffect.loopCount = (short)loopCount;
			Effect2.vEffect2.addElement(serverEffect);
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x0023DC88 File Offset: 0x0023BE88
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

		// Token: 0x06002417 RID: 9239 RVA: 0x0023DD9C File Offset: 0x0023BF9C
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

		// Token: 0x040046FA RID: 18170
		public EffectCharPaint eff;

		// Token: 0x040046FB RID: 18171
		private int i0;

		// Token: 0x040046FC RID: 18172
		private int dx0;

		// Token: 0x040046FD RID: 18173
		private int dy0;

		// Token: 0x040046FE RID: 18174
		private int x;

		// Token: 0x040046FF RID: 18175
		private int y;

		// Token: 0x04004700 RID: 18176
		private Char c;

		// Token: 0x04004701 RID: 18177
		private Mob m;

		// Token: 0x04004702 RID: 18178
		private short loopCount;

		// Token: 0x04004703 RID: 18179
		private long endTime;

		// Token: 0x04004704 RID: 18180
		private int trans;
	}
}
