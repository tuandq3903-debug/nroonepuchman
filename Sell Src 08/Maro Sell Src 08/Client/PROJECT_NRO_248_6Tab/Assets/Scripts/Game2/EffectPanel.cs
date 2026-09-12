using System;

namespace Game2
{
	// Token: 0x02000395 RID: 917
	public class EffectPanel : Effect2
	{
		// Token: 0x060028A4 RID: 10404 RVA: 0x00280558 File Offset: 0x0027E758
		public static void addServerEffect(int id, int cx, int cy, int loopCount)
		{
			EffectPanel effectPanel = new EffectPanel();
			effectPanel.eff = GameScr.efs[id - 1];
			effectPanel.x = cx;
			effectPanel.y = cy;
			effectPanel.loopCount = (short)loopCount;
			Effect2.vEffect3.addElement(effectPanel);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x0028059C File Offset: 0x0027E79C
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

		// Token: 0x060028A6 RID: 10406 RVA: 0x002806A4 File Offset: 0x0027E8A4
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

		// Token: 0x04004DFE RID: 19966
		public EffectCharPaint eff;

		// Token: 0x04004DFF RID: 19967
		private int i0;

		// Token: 0x04004E00 RID: 19968
		private int dx0;

		// Token: 0x04004E01 RID: 19969
		private int dy0;

		// Token: 0x04004E02 RID: 19970
		private int x;

		// Token: 0x04004E03 RID: 19971
		private int y;

		// Token: 0x04004E04 RID: 19972
		private Char c;

		// Token: 0x04004E05 RID: 19973
		private Mob m;

		// Token: 0x04004E06 RID: 19974
		private short loopCount;

		// Token: 0x04004E07 RID: 19975
		private long endTime;

		// Token: 0x04004E08 RID: 19976
		private int trans;
	}
}
