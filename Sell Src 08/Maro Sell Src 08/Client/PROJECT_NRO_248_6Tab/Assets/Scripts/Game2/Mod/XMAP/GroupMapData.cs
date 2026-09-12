using System;
using System.Collections.Generic;

namespace Game2.Mod.XMAP
{
	// Token: 0x02000429 RID: 1065
	internal class GroupMapData
	{
		// Token: 0x06002F7E RID: 12158 RVA: 0x002E36B5 File Offset: 0x002E18B5
		public GroupMapData(List<int> var1, string var2)
		{
			this.datamap = var1;
			this.mapname = var2;
		}

		// Token: 0x04005BC4 RID: 23492
		public List<int> datamap;

		// Token: 0x04005BC5 RID: 23493
		public string mapname;
	}
}
