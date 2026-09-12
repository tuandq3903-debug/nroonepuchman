using System;

namespace Game5
{
	// Token: 0x020000E4 RID: 228
	public class Arrowpaint
	{
		// Token: 0x040012B5 RID: 4789
		public int id;

		// Token: 0x040012B6 RID: 4790
		public int life;

		// Token: 0x040012B7 RID: 4791
		public int ax;

		// Token: 0x040012B8 RID: 4792
		public int ay;

		// Token: 0x040012B9 RID: 4793
		public int axTo;

		// Token: 0x040012BA RID: 4794
		public int ayTo;

		// Token: 0x040012BB RID: 4795
		public int avx;

		// Token: 0x040012BC RID: 4796
		public int avy;

		// Token: 0x040012BD RID: 4797
		public int adx;

		// Token: 0x040012BE RID: 4798
		public int ady;

		// Token: 0x040012BF RID: 4799
		public Char charBelong;

		// Token: 0x040012C0 RID: 4800
		public int[] imgId = new int[3];

		// Token: 0x040012C1 RID: 4801
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

		// Token: 0x040012C2 RID: 4802
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

		// Token: 0x040012C3 RID: 4803
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
