using System;
using System.Collections.Generic;

namespace Game4.Mod.XMAP
{
	// Token: 0x02000278 RID: 632
	public struct GroupMap
	{
		// Token: 0x06001C35 RID: 7221 RVA: 0x001B955D File Offset: 0x001B775D
		public GroupMap(string nameGroup, List<int> idMaps)
		{
			this.NameGroup = nameGroup;
			this.IdMaps = idMaps;
		}

		// Token: 0x040036C4 RID: 14020
		public string NameGroup;

		// Token: 0x040036C5 RID: 14021
		public List<int> IdMaps;
	}
}
