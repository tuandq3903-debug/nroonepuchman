using System;

namespace Game5
{
	// Token: 0x02000133 RID: 307
	public class ItemTemplate
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x000DC2C0 File Offset: 0x000DA4C0
		public ItemTemplate(short templateID, sbyte type, sbyte gender, string name, string description, sbyte level, int strRequire, short iconID, short part, bool isUpToUp)
		{
			this.id = templateID;
			this.type = type;
			this.gender = gender;
			this.name = name;
			this.name = Res.changeString(this.name);
			this.description = description;
			this.description = Res.changeString(this.description);
			this.level = level;
			this.strRequire = strRequire;
			this.iconID = iconID;
			this.part = part;
			this.isUpToUp = isUpToUp;
		}

		// Token: 0x04001A9D RID: 6813
		public short id;

		// Token: 0x04001A9E RID: 6814
		public sbyte type;

		// Token: 0x04001A9F RID: 6815
		public sbyte gender;

		// Token: 0x04001AA0 RID: 6816
		public string name;

		// Token: 0x04001AA1 RID: 6817
		public string[] subName;

		// Token: 0x04001AA2 RID: 6818
		public string description;

		// Token: 0x04001AA3 RID: 6819
		public sbyte level;

		// Token: 0x04001AA4 RID: 6820
		public short iconID;

		// Token: 0x04001AA5 RID: 6821
		public short part;

		// Token: 0x04001AA6 RID: 6822
		public bool isUpToUp;

		// Token: 0x04001AA7 RID: 6823
		public int w;

		// Token: 0x04001AA8 RID: 6824
		public int h;

		// Token: 0x04001AA9 RID: 6825
		public int strRequire;
	}
}
