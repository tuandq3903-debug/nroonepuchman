using System;

namespace Game5
{
	// Token: 0x0200010D RID: 269
	public class EffectPanel : Effect2
	{
		// Token: 0x06000BB8 RID: 3000 RVA: 0x000C136C File Offset: 0x000BF56C
		public static void addServerEffect(int id, int cx, int cy, int loopCount)
		{
			EffectPanel effectPanel = new EffectPanel();
			effectPanel.eff = GameScr.efs[id - 1];
			effectPanel.x = cx;
			effectPanel.y = cy;
			effectPanel.loopCount = (short)loopCount;
			Effect2.vEffect3.addElement(effectPanel);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000C13B0 File Offset: 0x000BF5B0
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

		// Token: 0x06000BBA RID: 3002 RVA: 0x000C14B8 File Offset: 0x000BF6B8
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

		// Token: 0x04001681 RID: 5761
		public EffectCharPaint eff;

		// Token: 0x04001682 RID: 5762
		private int i0;

		// Token: 0x04001683 RID: 5763
		private int dx0;

		// Token: 0x04001684 RID: 5764
		private int dy0;

		// Token: 0x04001685 RID: 5765
		private int x;

		// Token: 0x04001686 RID: 5766
		private int y;

		// Token: 0x04001687 RID: 5767
		private Char c;

		// Token: 0x04001688 RID: 5768
		private Mob m;

		// Token: 0x04001689 RID: 5769
		private short loopCount;

		// Token: 0x0400168A RID: 5770
		private long endTime;

		// Token: 0x0400168B RID: 5771
		private int trans;
	}
}
