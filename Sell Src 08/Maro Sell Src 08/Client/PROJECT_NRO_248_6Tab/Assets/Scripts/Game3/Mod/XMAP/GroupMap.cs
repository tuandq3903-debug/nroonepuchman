using System;
using System.Collections.Generic;

namespace Game3.Mod.XMAP
{
	// Token: 0x02000350 RID: 848
	public struct GroupMap
	{
		// Token: 0x060025D9 RID: 9689 RVA: 0x0024E601 File Offset: 0x0024C801
		public GroupMap(string nameGroup, List<int> idMaps)
		{
			this.NameGroup = nameGroup;
			this.IdMaps = idMaps;
		}

		// Token: 0x04004943 RID: 18755
		public string NameGroup;

		// Token: 0x04004944 RID: 18756
		public List<int> IdMaps;
	}
}
