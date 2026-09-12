using System;
using System.Collections.Generic;

namespace Game5.Mod.XMAP
{
	// Token: 0x020001A1 RID: 417
	internal class GroupMapData
	{
		// Token: 0x06001292 RID: 4754 RVA: 0x001244C9 File Offset: 0x001226C9
		public GroupMapData(List<int> var1, string var2)
		{
			this.datamap = var1;
			this.mapname = var2;
		}

		// Token: 0x04002447 RID: 9287
		public List<int> datamap;

		// Token: 0x04002448 RID: 9288
		public string mapname;
	}
}
