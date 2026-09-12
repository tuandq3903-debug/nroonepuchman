using System;

namespace Game3
{
	// Token: 0x020002DE RID: 734
	public struct ItemAutoFilter
	{
		// Token: 0x0600209B RID: 8347 RVA: 0x002059CE File Offset: 0x00203BCE
		public ItemAutoFilter(int iconID, int id, string name)
		{
			this.iconID = iconID;
			this.id = id;
			this.name = name;
		}

		// Token: 0x04003F70 RID: 16240
		public int iconID;

		// Token: 0x04003F71 RID: 16241
		public int id;

		// Token: 0x04003F72 RID: 16242
		public string name;
	}
}
