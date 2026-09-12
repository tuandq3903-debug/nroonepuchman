using System;

namespace Game6
{
	// Token: 0x02000035 RID: 53
	public class EffectPanel : Effect2
	{
		// Token: 0x06000214 RID: 532 RVA: 0x0002C160 File Offset: 0x0002A360
		public static void addServerEffect(int id, int cx, int cy, int loopCount)
		{
			EffectPanel effectPanel = new EffectPanel();
			effectPanel.eff = GameScr.efs[id - 1];
			effectPanel.x = cx;
			effectPanel.y = cy;
			effectPanel.loopCount = (short)loopCount;
			Effect2.vEffect3.addElement(effectPanel);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0002C1A4 File Offset: 0x0002A3A4
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
				SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.i0].idImg, num, num2, this.trans, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0002C2AC File Offset: 0x0002A4AC
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
					Effect2.vEffect3.removeElement(this);
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
						Effect2.vEffect3.removeElement(this);
					}
					else
					{
						this.i0 = 0;
					}
				}
			}
			if (GameCanvas.gameTick % 11 == 0 && this.c != null && this.c != Char.myCharz() && !GameScr.vCharInMap.contains(this.c))
			{
				Effect2.vEffect3.removeElement(this);
			}
		}

		// Token: 0x04000402 RID: 1026
		public EffectCharPaint eff;

		// Token: 0x04000403 RID: 1027
		private int i0;

		// Token: 0x04000404 RID: 1028
		private int dx0;

		// Token: 0x04000405 RID: 1029
		private int dy0;

		// Token: 0x04000406 RID: 1030
		private int x;

		// Token: 0x04000407 RID: 1031
		private int y;

		// Token: 0x04000408 RID: 1032
		private Char c;

		// Token: 0x04000409 RID: 1033
		private Mob m;

		// Token: 0x0400040A RID: 1034
		private short loopCount;

		// Token: 0x0400040B RID: 1035
		private long endTime;

		// Token: 0x0400040C RID: 1036
		private int trans;
	}
}
