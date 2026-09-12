using System;

namespace Game4
{
	// Token: 0x020001BA RID: 442
	public class ArrayCast
	{
		// Token: 0x0600135C RID: 4956 RVA: 0x0012C61C File Offset: 0x0012A81C
		public static sbyte[] cast(byte[] data)
		{
			sbyte[] array = new sbyte[data.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (sbyte)data[i];
			}
			return array;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0012C648 File Offset: 0x0012A848
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
