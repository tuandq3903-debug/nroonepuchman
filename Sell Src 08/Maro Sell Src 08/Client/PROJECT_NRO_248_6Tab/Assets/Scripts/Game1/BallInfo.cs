using System;

namespace Game1
{
	// Token: 0x0200044D RID: 1101
	public class BallInfo
	{
		// Token: 0x060030AB RID: 12459 RVA: 0x002EE9B4 File Offset: 0x002ECBB4
		public void SetChar()
		{
			this.cFocus = new Char();
			this.cFocus.charID = Res.random(-999, -800);
			this.cFocus.head = -1;
			this.cFocus.body = -1;
			this.cFocus.leg = -1;
			this.cFocus.bag = -1;
			this.cFocus.cName = string.Empty;
			this.cFocus.cHP = (this.cFocus.cHPFull = 20L);
		}

		// Token: 0x04005D22 RID: 23842
		public int x;

		// Token: 0x04005D23 RID: 23843
		public int y;

		// Token: 0x04005D24 RID: 23844
		public int xTo = -999;

		// Token: 0x04005D25 RID: 23845
		public int yTo = -999;

		// Token: 0x04005D26 RID: 23846
		public int count;

		// Token: 0x04005D27 RID: 23847
		public int vy;

		// Token: 0x04005D28 RID: 23848
		public int vx;

		// Token: 0x04005D29 RID: 23849
		public int dir;

		// Token: 0x04005D2A RID: 23850
		public int idImg;

		// Token: 0x04005D2B RID: 23851
		public bool isPaint = true;

		// Token: 0x04005D2C RID: 23852
		public bool isDone;

		// Token: 0x04005D2D RID: 23853
		public bool isSetImg;

		// Token: 0x04005D2E RID: 23854
		public Char cFocus;
	}
}
