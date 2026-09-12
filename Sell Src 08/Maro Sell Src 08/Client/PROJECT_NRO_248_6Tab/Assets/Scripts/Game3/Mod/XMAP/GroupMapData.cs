using System;
using System.Collections.Generic;

namespace Game3.Mod.XMAP
{
	// Token: 0x02000351 RID: 849
	internal class GroupMapData
	{
		// Token: 0x060025DA RID: 9690 RVA: 0x0024E611 File Offset: 0x0024C811
		public GroupMapData(List<int> var1, string var2)
		{
			this.datamap = var1;
			this.mapname = var2;
		}

		// Token: 0x04004945 RID: 18757
		public List<int> datamap;

		// Token: 0x04004946 RID: 18758
		public string mapname;
	}
}
