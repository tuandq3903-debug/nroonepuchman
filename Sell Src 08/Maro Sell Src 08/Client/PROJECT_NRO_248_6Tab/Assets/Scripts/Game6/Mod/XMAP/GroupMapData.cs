using System;
using System.Collections.Generic;

namespace Game6.Mod.XMAP
{
	// Token: 0x020000C9 RID: 201
	internal class GroupMapData
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0008F3B1 File Offset: 0x0008D5B1
		public GroupMapData(List<int> var1, string var2)
		{
			this.datamap = var1;
			this.mapname = var2;
		}

		// Token: 0x040011C8 RID: 4552
		public List<int> datamap;

		// Token: 0x040011C9 RID: 4553
		public string mapname;
	}
}
