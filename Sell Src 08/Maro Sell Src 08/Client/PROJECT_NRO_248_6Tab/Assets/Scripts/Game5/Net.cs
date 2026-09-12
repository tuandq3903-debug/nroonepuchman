using System;

namespace Game5
{
	// Token: 0x0200015C RID: 348
	internal class Net
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x000F0C90 File Offset: 0x000EEE90
		public static void connectHTTP2(string link, Command h)
		{
			Net.h = h;
			if (link != null)
			{
				h.perform(link);
			}
		}

		// Token: 0x04001F6B RID: 8043
		public static Command h;
	}
}
