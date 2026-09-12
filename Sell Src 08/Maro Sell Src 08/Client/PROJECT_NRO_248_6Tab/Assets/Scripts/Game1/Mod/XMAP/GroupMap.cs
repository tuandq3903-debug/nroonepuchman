using System;
using System.Collections.Generic;

namespace Game1.Mod.XMAP
{
	// Token: 0x02000500 RID: 1280
	public struct GroupMap
	{
		// Token: 0x06003921 RID: 14625 RVA: 0x00378749 File Offset: 0x00376949
		public GroupMap(string nameGroup, List<int> idMaps)
		{
			this.NameGroup = nameGroup;
			this.IdMaps = idMaps;
		}

		// Token: 0x04006E41 RID: 28225
		public string NameGroup;

		// Token: 0x04006E42 RID: 28226
		public List<int> IdMaps;
	}
}
