using System;

namespace Game3
{
	// Token: 0x020002BD RID: 701
	public class EffectPanel : Effect2
	{
		// Token: 0x06001F00 RID: 7936 RVA: 0x001EB4B4 File Offset: 0x001E96B4
		public static void addServerEffect(int id, int cx, int cy, int loopCount)
		{
			EffectPanel effectPanel = new EffectPanel();
			effectPanel.eff = GameScr.efs[id - 1];
			effectPanel.x = cx;
			effectPanel.y = cy;
			effectPanel.loopCount = (short)loopCount;
			Effect2.vEffect3.addElement(effectPanel);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x001EB4F8 File Offset: 0x001E96F8
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

		// Token: 0x06001F02 RID: 7938 RVA: 0x001EB600 File Offset: 0x001E9800
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

		// Token: 0x04003B7F RID: 15231
		public EffectCharPaint eff;

		// Token: 0x04003B80 RID: 15232
		private int i0;

		// Token: 0x04003B81 RID: 15233
		private int dx0;

		// Token: 0x04003B82 RID: 15234
		private int dy0;

		// Token: 0x04003B83 RID: 15235
		private int x;

		// Token: 0x04003B84 RID: 15236
		private int y;

		// Token: 0x04003B85 RID: 15237
		private Char c;

		// Token: 0x04003B86 RID: 15238
		private Mob m;

		// Token: 0x04003B87 RID: 15239
		private short loopCount;

		// Token: 0x04003B88 RID: 15240
		private long endTime;

		// Token: 0x04003B89 RID: 15241
		private int trans;
	}
}
