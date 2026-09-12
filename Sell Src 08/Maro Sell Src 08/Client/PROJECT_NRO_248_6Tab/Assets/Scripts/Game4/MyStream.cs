using System;

namespace Game4
{
	// Token: 0x02000230 RID: 560
	public class MyStream
	{
		// Token: 0x060018BA RID: 6330 RVA: 0x001858E4 File Offset: 0x00183AE4
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
