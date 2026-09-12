using System;
using System.Collections.Generic;

namespace Game2.Mod.XMAP
{
	// Token: 0x02000428 RID: 1064
	public struct GroupMap
	{
		// Token: 0x06002F7D RID: 12157 RVA: 0x002E36A5 File Offset: 0x002E18A5
		public GroupMap(string nameGroup, List<int> idMaps)
		{
			this.NameGroup = nameGroup;
			this.IdMaps = idMaps;
		}

		// Token: 0x04005BC2 RID: 23490
		public string NameGroup;

		// Token: 0x04005BC3 RID: 23491
		public List<int> IdMaps;
	}
}
