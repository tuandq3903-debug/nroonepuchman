using System;

namespace Game2
{
	// Token: 0x020003E0 RID: 992
	public class MyStream
	{
		// Token: 0x06002C02 RID: 11266 RVA: 0x002AFA2C File Offset: 0x002ADC2C
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
