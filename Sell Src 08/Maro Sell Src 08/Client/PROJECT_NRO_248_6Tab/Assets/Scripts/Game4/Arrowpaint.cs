using System;

namespace Game4
{
	// Token: 0x020001BC RID: 444
	public class Arrowpaint
	{
		// Token: 0x04002534 RID: 9524
		public int id;

		// Token: 0x04002535 RID: 9525
		public int life;

		// Token: 0x04002536 RID: 9526
		public int ax;

		// Token: 0x04002537 RID: 9527
		public int ay;

		// Token: 0x04002538 RID: 9528
		public int axTo;

		// Token: 0x04002539 RID: 9529
		public int ayTo;

		// Token: 0x0400253A RID: 9530
		public int avx;

		// Token: 0x0400253B RID: 9531
		public int avy;

		// Token: 0x0400253C RID: 9532
		public int adx;

		// Token: 0x0400253D RID: 9533
		public int ady;

		// Token: 0x0400253E RID: 9534
		public Char charBelong;

		// Token: 0x0400253F RID: 9535
		public int[] imgId = new int[3];

		// Token: 0x04002540 RID: 9536
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

		// Token: 0x04002541 RID: 9537
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

		// Token: 0x04002542 RID: 9538
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
