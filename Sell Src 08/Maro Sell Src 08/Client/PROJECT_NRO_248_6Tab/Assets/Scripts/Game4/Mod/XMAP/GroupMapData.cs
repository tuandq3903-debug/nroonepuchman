using System;
using System.Collections.Generic;

namespace Game4.Mod.XMAP
{
	// Token: 0x02000279 RID: 633
	internal class GroupMapData
	{
		// Token: 0x06001C36 RID: 7222 RVA: 0x001B956D File Offset: 0x001B776D
		public GroupMapData(List<int> var1, string var2)
		{
			this.datamap = var1;
			this.mapname = var2;
		}

		// Token: 0x040036C6 RID: 14022
		public List<int> datamap;

		// Token: 0x040036C7 RID: 14023
		public string mapname;
	}
}
