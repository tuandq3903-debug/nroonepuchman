using System;

namespace Game6
{
	// Token: 0x02000080 RID: 128
	public class MyStream
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x0005B6D8 File Offset: 0x000598D8
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
