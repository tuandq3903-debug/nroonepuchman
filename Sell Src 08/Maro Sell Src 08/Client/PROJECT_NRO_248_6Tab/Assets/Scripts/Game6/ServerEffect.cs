using System;

namespace Game6
{
	// Token: 0x0200009C RID: 156
	public class ServerEffect : Effect2
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x0007E924 File Offset: 0x0007CB24
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

		// Token: 0x06000727 RID: 1831 RVA: 0x0007E968 File Offset: 0x0007CB68
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

		// Token: 0x06000728 RID: 1832 RVA: 0x0007E9B4 File Offset: 0x0007CBB4
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

		// Token: 0x06000729 RID: 1833 RVA: 0x0007E9F0 File Offset: 0x0007CBF0
		public static void addServerEffect(int id, Char c, int loopCount)
		{
			ServerEffect serverEffect = new ServerEffect();
			serverEffect.eff = GameScr.efs[id - 1];
			serverEffect.c = c;
			serverEffect.loopCount = (short)loopCount;
			Effect2.vEffect2.addElement(serverEffect);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0007EA2C File Offset: 0x0007CC2C
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

		// Token: 0x0600072B RID: 1835 RVA: 0x0007EB40 File Offset: 0x0007CD40
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

		// Token: 0x04000F7D RID: 3965
		public EffectCharPaint eff;

		// Token: 0x04000F7E RID: 3966
		private int i0;

		// Token: 0x04000F7F RID: 3967
		private int dx0;

		// Token: 0x04000F80 RID: 3968
		private int dy0;

		// Token: 0x04000F81 RID: 3969
		private int x;

		// Token: 0x04000F82 RID: 3970
		private int y;

		// Token: 0x04000F83 RID: 3971
		private Char c;

		// Token: 0x04000F84 RID: 3972
		private Mob m;

		// Token: 0x04000F85 RID: 3973
		private short loopCount;

		// Token: 0x04000F86 RID: 3974
		private long endTime;

		// Token: 0x04000F87 RID: 3975
		private int trans;
	}
}
