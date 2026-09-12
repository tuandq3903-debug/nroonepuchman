using System;

namespace Game4
{
	// Token: 0x0200020B RID: 523
	public class ItemTemplate
	{
		// Token: 0x06001710 RID: 5904 RVA: 0x00171364 File Offset: 0x0016F564
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

		// Token: 0x04002D1C RID: 11548
		public short id;

		// Token: 0x04002D1D RID: 11549
		public sbyte type;

		// Token: 0x04002D1E RID: 11550
		public sbyte gender;

		// Token: 0x04002D1F RID: 11551
		public string name;

		// Token: 0x04002D20 RID: 11552
		public string[] subName;

		// Token: 0x04002D21 RID: 11553
		public string description;

		// Token: 0x04002D22 RID: 11554
		public sbyte level;

		// Token: 0x04002D23 RID: 11555
		public short iconID;

		// Token: 0x04002D24 RID: 11556
		public short part;

		// Token: 0x04002D25 RID: 11557
		public bool isUpToUp;

		// Token: 0x04002D26 RID: 11558
		public int w;

		// Token: 0x04002D27 RID: 11559
		public int h;

		// Token: 0x04002D28 RID: 11560
		public int strRequire;
	}
}
