using System;

namespace Game1
{
	// Token: 0x0200046D RID: 1133
	public class EffectPanel : Effect2
	{
		// Token: 0x06003248 RID: 12872 RVA: 0x003155FC File Offset: 0x003137FC
		public static void addServerEffect(int id, int cx, int cy, int loopCount)
		{
			EffectPanel effectPanel = new EffectPanel();
			effectPanel.eff = GameScr.efs[id - 1];
			effectPanel.x = cx;
			effectPanel.y = cy;
			effectPanel.loopCount = (short)loopCount;
			Effect2.vEffect3.addElement(effectPanel);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x00315640 File Offset: 0x00313840
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

		// Token: 0x0600324A RID: 12874 RVA: 0x00315748 File Offset: 0x00313948
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

		// Token: 0x0400607D RID: 24701
		public EffectCharPaint eff;

		// Token: 0x0400607E RID: 24702
		private int i0;

		// Token: 0x0400607F RID: 24703
		private int dx0;

		// Token: 0x04006080 RID: 24704
		private int dy0;

		// Token: 0x04006081 RID: 24705
		private int x;

		// Token: 0x04006082 RID: 24706
		private int y;

		// Token: 0x04006083 RID: 24707
		private Char c;

		// Token: 0x04006084 RID: 24708
		private Mob m;

		// Token: 0x04006085 RID: 24709
		private short loopCount;

		// Token: 0x04006086 RID: 24710
		private long endTime;

		// Token: 0x04006087 RID: 24711
		private int trans;
	}
}
