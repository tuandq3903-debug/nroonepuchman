using System;
using System.Collections.Generic;

namespace Game1.Mod.XMAP
{
	// Token: 0x02000501 RID: 1281
	internal class GroupMapData
	{
		// Token: 0x06003922 RID: 14626 RVA: 0x00378759 File Offset: 0x00376959
		public GroupMapData(List<int> var1, string var2)
		{
			this.datamap = var1;
			this.mapname = var2;
		}

		// Token: 0x04006E43 RID: 28227
		public List<int> datamap;

		// Token: 0x04006E44 RID: 28228
		public string mapname;
	}
}
