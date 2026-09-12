using System;

namespace Game4
{
	// Token: 0x02000234 RID: 564
	internal class Net
	{
		// Token: 0x060018DC RID: 6364 RVA: 0x00185D34 File Offset: 0x00183F34
		public static void connectHTTP2(string link, Command h)
		{
			Net.h = h;
			if (link != null)
			{
				h.perform(link);
			}
		}

		// Token: 0x040031EA RID: 12778
		public static Command h;
	}
}
