using System;

namespace Game5
{
	// Token: 0x02000158 RID: 344
	public class MyStream
	{
		// Token: 0x06000F16 RID: 3862 RVA: 0x000F0840 File Offset: 0x000EEA40
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
