using System;

namespace Game1
{
	// Token: 0x02000444 RID: 1092
	public class Arrowpaint
	{
		// Token: 0x04005CB1 RID: 23729
		public int id;

		// Token: 0x04005CB2 RID: 23730
		public int life;

		// Token: 0x04005CB3 RID: 23731
		public int ax;

		// Token: 0x04005CB4 RID: 23732
		public int ay;

		// Token: 0x04005CB5 RID: 23733
		public int axTo;

		// Token: 0x04005CB6 RID: 23734
		public int ayTo;

		// Token: 0x04005CB7 RID: 23735
		public int avx;

		// Token: 0x04005CB8 RID: 23736
		public int avy;

		// Token: 0x04005CB9 RID: 23737
		public int adx;

		// Token: 0x04005CBA RID: 23738
		public int ady;

		// Token: 0x04005CBB RID: 23739
		public Char charBelong;

		// Token: 0x04005CBC RID: 23740
		public int[] imgId = new int[3];

		// Token: 0x04005CBD RID: 23741
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

		// Token: 0x04005CBE RID: 23742
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

		// Token: 0x04005CBF RID: 23743
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
