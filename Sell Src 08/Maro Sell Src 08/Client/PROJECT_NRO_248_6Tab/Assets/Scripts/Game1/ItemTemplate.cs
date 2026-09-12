using System;

namespace Game1
{
	// Token: 0x02000493 RID: 1171
	public class ItemTemplate
	{
		// Token: 0x060033FC RID: 13308 RVA: 0x00330550 File Offset: 0x0032E750
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

		// Token: 0x04006499 RID: 25753
		public short id;

		// Token: 0x0400649A RID: 25754
		public sbyte type;

		// Token: 0x0400649B RID: 25755
		public sbyte gender;

		// Token: 0x0400649C RID: 25756
		public string name;

		// Token: 0x0400649D RID: 25757
		public string[] subName;

		// Token: 0x0400649E RID: 25758
		public string description;

		// Token: 0x0400649F RID: 25759
		public sbyte level;

		// Token: 0x040064A0 RID: 25760
		public short iconID;

		// Token: 0x040064A1 RID: 25761
		public short part;

		// Token: 0x040064A2 RID: 25762
		public bool isUpToUp;

		// Token: 0x040064A3 RID: 25763
		public int w;

		// Token: 0x040064A4 RID: 25764
		public int h;

		// Token: 0x040064A5 RID: 25765
		public int strRequire;
	}
}
