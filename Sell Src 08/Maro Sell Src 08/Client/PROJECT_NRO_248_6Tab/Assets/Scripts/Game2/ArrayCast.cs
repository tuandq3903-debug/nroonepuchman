using System;

namespace Game2
{
	// Token: 0x0200036A RID: 874
	public class ArrayCast
	{
		// Token: 0x060026A4 RID: 9892 RVA: 0x00256764 File Offset: 0x00254964
		public static sbyte[] cast(byte[] data)
		{
			sbyte[] array = new sbyte[data.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (sbyte)data[i];
			}
			return array;
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x00256790 File Offset: 0x00254990
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
