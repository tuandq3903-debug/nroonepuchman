using System;

namespace Game2
{
	// Token: 0x0200036C RID: 876
	public class Arrowpaint
	{
		// Token: 0x04004A32 RID: 18994
		public int id;

		// Token: 0x04004A33 RID: 18995
		public int life;

		// Token: 0x04004A34 RID: 18996
		public int ax;

		// Token: 0x04004A35 RID: 18997
		public int ay;

		// Token: 0x04004A36 RID: 18998
		public int axTo;

		// Token: 0x04004A37 RID: 18999
		public int ayTo;

		// Token: 0x04004A38 RID: 19000
		public int avx;

		// Token: 0x04004A39 RID: 19001
		public int avy;

		// Token: 0x04004A3A RID: 19002
		public int adx;

		// Token: 0x04004A3B RID: 19003
		public int ady;

		// Token: 0x04004A3C RID: 19004
		public Char charBelong;

		// Token: 0x04004A3D RID: 19005
		public int[] imgId = new int[3];

		// Token: 0x04004A3E RID: 19006
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

		// Token: 0x04004A3F RID: 19007
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

		// Token: 0x04004A40 RID: 19008
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
