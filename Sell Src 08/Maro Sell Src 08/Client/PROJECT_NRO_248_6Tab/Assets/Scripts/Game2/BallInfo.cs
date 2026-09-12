using System;

namespace Game2
{
	// Token: 0x02000375 RID: 885
	public class BallInfo
	{
		// Token: 0x06002707 RID: 9991 RVA: 0x00259910 File Offset: 0x00257B10
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

		// Token: 0x04004AA3 RID: 19107
		public int x;

		// Token: 0x04004AA4 RID: 19108
		public int y;

		// Token: 0x04004AA5 RID: 19109
		public int xTo = -999;

		// Token: 0x04004AA6 RID: 19110
		public int yTo = -999;

		// Token: 0x04004AA7 RID: 19111
		public int count;

		// Token: 0x04004AA8 RID: 19112
		public int vy;

		// Token: 0x04004AA9 RID: 19113
		public int vx;

		// Token: 0x04004AAA RID: 19114
		public int dir;

		// Token: 0x04004AAB RID: 19115
		public int idImg;

		// Token: 0x04004AAC RID: 19116
		public bool isPaint = true;

		// Token: 0x04004AAD RID: 19117
		public bool isDone;

		// Token: 0x04004AAE RID: 19118
		public bool isSetImg;

		// Token: 0x04004AAF RID: 19119
		public Char cFocus;
	}
}
