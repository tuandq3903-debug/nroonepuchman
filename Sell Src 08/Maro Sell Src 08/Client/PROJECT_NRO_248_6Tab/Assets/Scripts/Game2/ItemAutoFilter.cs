using System;

namespace Game2
{
	// Token: 0x020003B6 RID: 950
	public struct ItemAutoFilter
	{
		// Token: 0x06002A3F RID: 10815 RVA: 0x0029AA72 File Offset: 0x00298C72
		public ItemAutoFilter(int iconID, int id, string name)
		{
			this.iconID = iconID;
			this.id = id;
			this.name = name;
		}

		// Token: 0x040051EF RID: 20975
		public int iconID;

		// Token: 0x040051F0 RID: 20976
		public int id;

		// Token: 0x040051F1 RID: 20977
		public string name;
	}
}
