using System;

namespace Game3
{
	// Token: 0x0200030C RID: 780
	internal class Net
	{
		// Token: 0x06002280 RID: 8832 RVA: 0x0021ADD8 File Offset: 0x00218FD8
		public static void connectHTTP2(string link, Command h)
		{
			Net.h = h;
			if (link != null)
			{
				h.perform(link);
			}
		}

		// Token: 0x04004469 RID: 17513
		public static Command h;
	}
}
