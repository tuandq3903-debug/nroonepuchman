using System;

namespace Game3
{
	// Token: 0x02000292 RID: 658
	public class ArrayCast
	{
		// Token: 0x06001D00 RID: 7424 RVA: 0x001C16C0 File Offset: 0x001BF8C0
		public static sbyte[] cast(byte[] data)
		{
			sbyte[] array = new sbyte[data.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (sbyte)data[i];
			}
			return array;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x001C16EC File Offset: 0x001BF8EC
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
