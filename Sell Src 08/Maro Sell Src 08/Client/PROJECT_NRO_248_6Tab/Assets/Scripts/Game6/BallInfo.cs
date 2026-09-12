using System;

namespace Game6
{
	// Token: 0x02000015 RID: 21
	public class BallInfo
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00005538 File Offset: 0x00003738
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

		// Token: 0x040000A8 RID: 168
		public int x;

		// Token: 0x040000A9 RID: 169
		public int y;

		// Token: 0x040000AA RID: 170
		public int xTo = -999;

		// Token: 0x040000AB RID: 171
		public int yTo = -999;

		// Token: 0x040000AC RID: 172
		public int count;

		// Token: 0x040000AD RID: 173
		public int vy;

		// Token: 0x040000AE RID: 174
		public int vx;

		// Token: 0x040000AF RID: 175
		public int dir;

		// Token: 0x040000B0 RID: 176
		public int idImg;

		// Token: 0x040000B1 RID: 177
		public bool isPaint = true;

		// Token: 0x040000B2 RID: 178
		public bool isDone;

		// Token: 0x040000B3 RID: 179
		public bool isSetImg;

		// Token: 0x040000B4 RID: 180
		public Char cFocus;
	}
}
