using System;

namespace Game3
{
	// Token: 0x02000308 RID: 776
	public class MyStream
	{
		// Token: 0x0600225E RID: 8798 RVA: 0x0021A988 File Offset: 0x00218B88
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
