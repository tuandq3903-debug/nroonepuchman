using System;

namespace Game4
{
	// Token: 0x02000206 RID: 518
	public struct ItemAutoFilter
	{
		// Token: 0x060016F7 RID: 5879 RVA: 0x0017092A File Offset: 0x0016EB2A
		public ItemAutoFilter(int iconID, int id, string name)
		{
			this.iconID = iconID;
			this.id = id;
			this.name = name;
		}

		// Token: 0x04002CF1 RID: 11505
		public int iconID;

		// Token: 0x04002CF2 RID: 11506
		public int id;

		// Token: 0x04002CF3 RID: 11507
		public string name;
	}
}
