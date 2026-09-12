using System;

namespace Game2
{
	// Token: 0x020003F5 RID: 1013
	public class Position
	{
		// Token: 0x06002D6F RID: 11631 RVA: 0x002D02AF File Offset: 0x002CE4AF
		public Position()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x002D02C5 File Offset: 0x002CE4C5
		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x0400590E RID: 22798
		public int x;

		// Token: 0x0400590F RID: 22799
		public int y;

		// Token: 0x04005910 RID: 22800
		public int anchor;

		// Token: 0x04005911 RID: 22801
		public int g;

		// Token: 0x04005912 RID: 22802
		public int v;

		// Token: 0x04005913 RID: 22803
		public int w;

		// Token: 0x04005914 RID: 22804
		public int h;

		// Token: 0x04005915 RID: 22805
		public int color;

		// Token: 0x04005916 RID: 22806
		public int limitY;

		// Token: 0x04005917 RID: 22807
		public Layer layer;

		// Token: 0x04005918 RID: 22808
		public short yTo;

		// Token: 0x04005919 RID: 22809
		public short xTo;

		// Token: 0x0400591A RID: 22810
		public short distant;
	}
}
