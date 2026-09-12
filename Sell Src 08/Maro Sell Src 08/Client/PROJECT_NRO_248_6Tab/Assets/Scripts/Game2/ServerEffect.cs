using System;

namespace Game2
{
	// Token: 0x020003FC RID: 1020
	public class ServerEffect : Effect2
	{
		// Token: 0x06002DB6 RID: 11702 RVA: 0x002D2C24 File Offset: 0x002D0E24
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

		// Token: 0x06002DB7 RID: 11703 RVA: 0x002D2C68 File Offset: 0x002D0E68
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

		// Token: 0x06002DB8 RID: 11704 RVA: 0x002D2CB4 File Offset: 0x002D0EB4
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

		// Token: 0x06002DB9 RID: 11705 RVA: 0x002D2CF0 File Offset: 0x002D0EF0
		public static void addServerEffect(int id, Char c, int loopCount)
		{
			ServerEffect serverEffect = new ServerEffect();
			serverEffect.eff = GameScr.efs[id - 1];
			serverEffect.c = c;
			serverEffect.loopCount = (short)loopCount;
			Effect2.vEffect2.addElement(serverEffect);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x002D2D2C File Offset: 0x002D0F2C
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

		// Token: 0x06002DBB RID: 11707 RVA: 0x002D2E40 File Offset: 0x002D1040
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

		// Token: 0x04005979 RID: 22905
		public EffectCharPaint eff;

		// Token: 0x0400597A RID: 22906
		private int i0;

		// Token: 0x0400597B RID: 22907
		private int dx0;

		// Token: 0x0400597C RID: 22908
		private int dy0;

		// Token: 0x0400597D RID: 22909
		private int x;

		// Token: 0x0400597E RID: 22910
		private int y;

		// Token: 0x0400597F RID: 22911
		private Char c;

		// Token: 0x04005980 RID: 22912
		private Mob m;

		// Token: 0x04005981 RID: 22913
		private short loopCount;

		// Token: 0x04005982 RID: 22914
		private long endTime;

		// Token: 0x04005983 RID: 22915
		private int trans;
	}
}
