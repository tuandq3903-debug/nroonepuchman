using System;

namespace Game5
{
	// Token: 0x0200012E RID: 302
	public struct ItemAutoFilter
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x000DB886 File Offset: 0x000D9A86
		public ItemAutoFilter(int iconID, int id, string name)
		{
			this.iconID = iconID;
			this.id = id;
			this.name = name;
		}

		// Token: 0x04001A72 RID: 6770
		public int iconID;

		// Token: 0x04001A73 RID: 6771
		public int id;

		// Token: 0x04001A74 RID: 6772
		public string name;
	}
}
