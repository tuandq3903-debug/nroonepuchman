using System;

namespace Game3
{
	// Token: 0x0200029D RID: 669
	public class BallInfo
	{
		// Token: 0x06001D63 RID: 7523 RVA: 0x001C486C File Offset: 0x001C2A6C
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

		// Token: 0x04003824 RID: 14372
		public int x;

		// Token: 0x04003825 RID: 14373
		public int y;

		// Token: 0x04003826 RID: 14374
		public int xTo = -999;

		// Token: 0x04003827 RID: 14375
		public int yTo = -999;

		// Token: 0x04003828 RID: 14376
		public int count;

		// Token: 0x04003829 RID: 14377
		public int vy;

		// Token: 0x0400382A RID: 14378
		public int vx;

		// Token: 0x0400382B RID: 14379
		public int dir;

		// Token: 0x0400382C RID: 14380
		public int idImg;

		// Token: 0x0400382D RID: 14381
		public bool isPaint = true;

		// Token: 0x0400382E RID: 14382
		public bool isDone;

		// Token: 0x0400382F RID: 14383
		public bool isSetImg;

		// Token: 0x04003830 RID: 14384
		public Char cFocus;
	}
}
