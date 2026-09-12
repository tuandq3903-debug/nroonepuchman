using System;

namespace Game6
{
	// Token: 0x0200005B RID: 91
	public class ItemTemplate
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x00047118 File Offset: 0x00045318
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

		// Token: 0x0400081E RID: 2078
		public short id;

		// Token: 0x0400081F RID: 2079
		public sbyte type;

		// Token: 0x04000820 RID: 2080
		public sbyte gender;

		// Token: 0x04000821 RID: 2081
		public string name;

		// Token: 0x04000822 RID: 2082
		public string[] subName;

		// Token: 0x04000823 RID: 2083
		public string description;

		// Token: 0x04000824 RID: 2084
		public sbyte level;

		// Token: 0x04000825 RID: 2085
		public short iconID;

		// Token: 0x04000826 RID: 2086
		public short part;

		// Token: 0x04000827 RID: 2087
		public bool isUpToUp;

		// Token: 0x04000828 RID: 2088
		public int w;

		// Token: 0x04000829 RID: 2089
		public int h;

		// Token: 0x0400082A RID: 2090
		public int strRequire;
	}
}
