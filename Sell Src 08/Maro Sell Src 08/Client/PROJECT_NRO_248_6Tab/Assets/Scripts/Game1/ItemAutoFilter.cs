using System;

namespace Game1
{
	// Token: 0x0200048E RID: 1166
	public struct ItemAutoFilter
	{
		// Token: 0x060033E3 RID: 13283 RVA: 0x0032FB16 File Offset: 0x0032DD16
		public ItemAutoFilter(int iconID, int id, string name)
		{
			this.iconID = iconID;
			this.id = id;
			this.name = name;
		}

		// Token: 0x0400646E RID: 25710
		public int iconID;

		// Token: 0x0400646F RID: 25711
		public int id;

		// Token: 0x04006470 RID: 25712
		public string name;
	}
}
