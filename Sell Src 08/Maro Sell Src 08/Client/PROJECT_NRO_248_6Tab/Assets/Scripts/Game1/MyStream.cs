using System;

namespace Game1
{
	// Token: 0x020004B8 RID: 1208
	public class MyStream
	{
		// Token: 0x060035A6 RID: 13734 RVA: 0x00344AD0 File Offset: 0x00342CD0
		public static DataInputStream readFile(string path)
		{
			path = Main.res + path;
			DataInputStream result;
			try
			{
				result = DataInputStream.getResourceAsStream(path);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
	}
}
