using System;

namespace Game5
{
	// Token: 0x020000E2 RID: 226
	public class ArrayCast
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x00097578 File Offset: 0x00095778
		public static sbyte[] cast(byte[] data)
		{
			sbyte[] array = new sbyte[data.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (sbyte)data[i];
			}
			return array;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000975A4 File Offset: 0x000957A4
		public static byte[] cast(sbyte[] data)
		{
			byte[] array = new byte[data.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)data[i];
			}
			return array;
		}
	}
}
