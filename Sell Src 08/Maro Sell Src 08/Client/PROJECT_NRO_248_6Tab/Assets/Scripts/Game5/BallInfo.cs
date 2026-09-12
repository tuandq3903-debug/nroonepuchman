using System;

namespace Game5
{
	// Token: 0x020000ED RID: 237
	public class BallInfo
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x0009A724 File Offset: 0x00098924
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

		// Token: 0x04001326 RID: 4902
		public int x;

		// Token: 0x04001327 RID: 4903
		public int y;

		// Token: 0x04001328 RID: 4904
		public int xTo = -999;

		// Token: 0x04001329 RID: 4905
		public int yTo = -999;

		// Token: 0x0400132A RID: 4906
		public int count;

		// Token: 0x0400132B RID: 4907
		public int vy;

		// Token: 0x0400132C RID: 4908
		public int vx;

		// Token: 0x0400132D RID: 4909
		public int dir;

		// Token: 0x0400132E RID: 4910
		public int idImg;

		// Token: 0x0400132F RID: 4911
		public bool isPaint = true;

		// Token: 0x04001330 RID: 4912
		public bool isDone;

		// Token: 0x04001331 RID: 4913
		public bool isSetImg;

		// Token: 0x04001332 RID: 4914
		public Char cFocus;
	}
}
