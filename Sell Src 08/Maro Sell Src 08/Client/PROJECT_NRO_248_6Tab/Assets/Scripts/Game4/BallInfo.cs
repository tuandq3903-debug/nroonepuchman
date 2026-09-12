using System;

namespace Game4
{
	// Token: 0x020001C5 RID: 453
	public class BallInfo
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x0012F7C8 File Offset: 0x0012D9C8
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

		// Token: 0x040025A5 RID: 9637
		public int x;

		// Token: 0x040025A6 RID: 9638
		public int y;

		// Token: 0x040025A7 RID: 9639
		public int xTo = -999;

		// Token: 0x040025A8 RID: 9640
		public int yTo = -999;

		// Token: 0x040025A9 RID: 9641
		public int count;

		// Token: 0x040025AA RID: 9642
		public int vy;

		// Token: 0x040025AB RID: 9643
		public int vx;

		// Token: 0x040025AC RID: 9644
		public int dir;

		// Token: 0x040025AD RID: 9645
		public int idImg;

		// Token: 0x040025AE RID: 9646
		public bool isPaint = true;

		// Token: 0x040025AF RID: 9647
		public bool isDone;

		// Token: 0x040025B0 RID: 9648
		public bool isSetImg;

		// Token: 0x040025B1 RID: 9649
		public Char cFocus;
	}
}
