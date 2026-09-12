using System;

namespace Game2
{
	// Token: 0x020003E4 RID: 996
	internal class Net
	{
		// Token: 0x06002C24 RID: 11300 RVA: 0x002AFE7C File Offset: 0x002AE07C
		public static void connectHTTP2(string link, Command h)
		{
			Net.h = h;
			if (link != null)
			{
				h.perform(link);
			}
		}

		// Token: 0x040056E8 RID: 22248
		public static Command h;
	}
}
