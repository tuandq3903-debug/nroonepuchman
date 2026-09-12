using System;
using System.Collections.Generic;

namespace Game5.Mod.XMAP
{
	// Token: 0x020001A0 RID: 416
	public struct GroupMap
	{
		// Token: 0x06001291 RID: 4753 RVA: 0x001244B9 File Offset: 0x001226B9
		public GroupMap(string nameGroup, List<int> idMaps)
		{
			this.NameGroup = nameGroup;
			this.IdMaps = idMaps;
		}

		// Token: 0x04002445 RID: 9285
		public string NameGroup;

		// Token: 0x04002446 RID: 9286
		public List<int> IdMaps;
	}
}
