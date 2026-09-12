using System;

namespace Game3
{
	// Token: 0x0200031D RID: 797
	public class Position
	{
		// Token: 0x060023CB RID: 9163 RVA: 0x0023B20B File Offset: 0x0023940B
		public Position()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x0023B221 File Offset: 0x00239421
		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x0400468F RID: 18063
		public int x;

		// Token: 0x04004690 RID: 18064
		public int y;

		// Token: 0x04004691 RID: 18065
		public int anchor;

		// Token: 0x04004692 RID: 18066
		public int g;

		// Token: 0x04004693 RID: 18067
		public int v;

		// Token: 0x04004694 RID: 18068
		public int w;

		// Token: 0x04004695 RID: 18069
		public int h;

		// Token: 0x04004696 RID: 18070
		public int color;

		// Token: 0x04004697 RID: 18071
		public int limitY;

		// Token: 0x04004698 RID: 18072
		public Layer layer;

		// Token: 0x04004699 RID: 18073
		public short yTo;

		// Token: 0x0400469A RID: 18074
		public short xTo;

		// Token: 0x0400469B RID: 18075
		public short distant;
	}
}
