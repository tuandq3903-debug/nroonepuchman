using System;
using System.Collections.Generic;

namespace Game6.Mod.XMAP
{
	// Token: 0x020000C8 RID: 200
	public struct GroupMap
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0008F3A1 File Offset: 0x0008D5A1
		public GroupMap(string nameGroup, List<int> idMaps)
		{
			this.NameGroup = nameGroup;
			this.IdMaps = idMaps;
		}

		// Token: 0x040011C6 RID: 4550
		public string NameGroup;

		// Token: 0x040011C7 RID: 4551
		public List<int> IdMaps;
	}
}
