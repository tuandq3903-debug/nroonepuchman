using System;

namespace Game4
{
	// Token: 0x020001E5 RID: 485
	public class EffectPanel : Effect2
	{
		// Token: 0x0600155C RID: 5468 RVA: 0x00156410 File Offset: 0x00154610
		public static void addServerEffect(int id, int cx, int cy, int loopCount)
		{
			EffectPanel effectPanel = new EffectPanel();
			effectPanel.eff = GameScr.efs[id - 1];
			effectPanel.x = cx;
			effectPanel.y = cy;
			effectPanel.loopCount = (short)loopCount;
			Effect2.vEffect3.addElement(effectPanel);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00156454 File Offset: 0x00154654
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

		// Token: 0x0600155E RID: 5470 RVA: 0x0015655C File Offset: 0x0015475C
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

		// Token: 0x04002900 RID: 10496
		public EffectCharPaint eff;

		// Token: 0x04002901 RID: 10497
		private int i0;

		// Token: 0x04002902 RID: 10498
		private int dx0;

		// Token: 0x04002903 RID: 10499
		private int dy0;

		// Token: 0x04002904 RID: 10500
		private int x;

		// Token: 0x04002905 RID: 10501
		private int y;

		// Token: 0x04002906 RID: 10502
		private Char c;

		// Token: 0x04002907 RID: 10503
		private Mob m;

		// Token: 0x04002908 RID: 10504
		private short loopCount;

		// Token: 0x04002909 RID: 10505
		private long endTime;

		// Token: 0x0400290A RID: 10506
		private int trans;
	}
}
