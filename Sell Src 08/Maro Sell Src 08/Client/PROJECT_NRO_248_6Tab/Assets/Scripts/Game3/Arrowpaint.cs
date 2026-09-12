using System;

namespace Game3
{
	// Token: 0x02000294 RID: 660
	public class Arrowpaint
	{
		// Token: 0x040037B3 RID: 14259
		public int id;

		// Token: 0x040037B4 RID: 14260
		public int life;

		// Token: 0x040037B5 RID: 14261
		public int ax;

		// Token: 0x040037B6 RID: 14262
		public int ay;

		// Token: 0x040037B7 RID: 14263
		public int axTo;

		// Token: 0x040037B8 RID: 14264
		public int ayTo;

		// Token: 0x040037B9 RID: 14265
		public int avx;

		// Token: 0x040037BA RID: 14266
		public int avy;

		// Token: 0x040037BB RID: 14267
		public int adx;

		// Token: 0x040037BC RID: 14268
		public int ady;

		// Token: 0x040037BD RID: 14269
		public Char charBelong;

		// Token: 0x040037BE RID: 14270
		public int[] imgId = new int[3];

		// Token: 0x040037BF RID: 14271
		public static sbyte[] FRAME = new sbyte[]
		{
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0
		};

		// Token: 0x040037C0 RID: 14272
		public static int[] ARROWINDEX = new int[]
		{
			0,
			15,
			37,
			52,
			75,
			105,
			127,
			142,
			165,
			195,
			217,
			232,
			255,
			285,
			307,
			322,
			345,
			370
		};

		// Token: 0x040037C1 RID: 14273
		public static int[] TRANSFORM = new int[]
		{
			0,
			0,
			0,
			7,
			6,
			6,
			6,
			2,
			2,
			3,
			3,
			4,
			5,
			5,
			5,
			1
		};
	}
}
