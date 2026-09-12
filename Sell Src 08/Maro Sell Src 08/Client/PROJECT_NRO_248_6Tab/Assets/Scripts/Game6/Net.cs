using System;

namespace Game6
{
	// Token: 0x02000084 RID: 132
	internal class Net
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x0005BB28 File Offset: 0x00059D28
		public static void connectHTTP2(string link, Command h)
		{
			Net.h = h;
			if (link != null)
			{
				h.perform(link);
			}
		}

		// Token: 0x04000CEC RID: 3308
		public static Command h;
	}
}
