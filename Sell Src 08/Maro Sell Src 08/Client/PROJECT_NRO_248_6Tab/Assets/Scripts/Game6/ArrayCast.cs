using System;

namespace Game6
{
	// Token: 0x0200000A RID: 10
	public class ArrayCast
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002388 File Offset: 0x00000588
		public static sbyte[] cast(byte[] data)
		{
			sbyte[] array = new sbyte[data.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (sbyte)data[i];
			}
			return array;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023B4 File Offset: 0x000005B4
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
