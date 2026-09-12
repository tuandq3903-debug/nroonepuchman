using System;

namespace Game6
{
	// Token: 0x0200000C RID: 12
	public class Arrowpaint
	{
		// Token: 0x04000037 RID: 55
		public int id;

		// Token: 0x04000038 RID: 56
		public int life;

		// Token: 0x04000039 RID: 57
		public int ax;

		// Token: 0x0400003A RID: 58
		public int ay;

		// Token: 0x0400003B RID: 59
		public int axTo;

		// Token: 0x0400003C RID: 60
		public int ayTo;

		// Token: 0x0400003D RID: 61
		public int avx;

		// Token: 0x0400003E RID: 62
		public int avy;

		// Token: 0x0400003F RID: 63
		public int adx;

		// Token: 0x04000040 RID: 64
		public int ady;

		// Token: 0x04000041 RID: 65
		public Char charBelong;

		// Token: 0x04000042 RID: 66
		public int[] imgId = new int[3];

		// Token: 0x04000043 RID: 67
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

		// Token: 0x04000044 RID: 68
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

		// Token: 0x04000045 RID: 69
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
