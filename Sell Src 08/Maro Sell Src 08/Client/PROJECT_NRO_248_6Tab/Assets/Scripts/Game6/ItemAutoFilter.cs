using System;

namespace Game6
{
	// Token: 0x02000056 RID: 86
	public struct ItemAutoFilter
	{
		// Token: 0x060003AF RID: 943 RVA: 0x000466DA File Offset: 0x000448DA
		public ItemAutoFilter(int iconID, int id, string name)
		{
			this.iconID = iconID;
			this.id = id;
			this.name = name;
		}

		// Token: 0x040007F3 RID: 2035
		public int iconID;

		// Token: 0x040007F4 RID: 2036
		public int id;

		// Token: 0x040007F5 RID: 2037
		public string name;
	}
}
