using System;

namespace Game1
{
	// Token: 0x02000442 RID: 1090
	public class ArrayCast
	{
		// Token: 0x06003048 RID: 12360 RVA: 0x002EB808 File Offset: 0x002E9A08
		public static sbyte[] cast(byte[] data)
		{
			sbyte[] array = new sbyte[data.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (sbyte)data[i];
			}
			return array;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x002EB834 File Offset: 0x002E9A34
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
