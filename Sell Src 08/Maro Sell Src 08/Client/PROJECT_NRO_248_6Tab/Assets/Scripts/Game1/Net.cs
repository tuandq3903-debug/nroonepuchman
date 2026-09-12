using System;

namespace Game1
{
	// Token: 0x020004BC RID: 1212
	internal class Net
	{
		// Token: 0x060035C8 RID: 13768 RVA: 0x00344F20 File Offset: 0x00343120
		public static void connectHTTP2(string link, Command h)
		{
			Net.h = h;
			if (link != null)
			{
				h.perform(link);
			}
		}

		// Token: 0x04006967 RID: 26983
		public static Command h;
	}
}
