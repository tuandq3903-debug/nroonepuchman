using System;

namespace Game2
{
	// Token: 0x020003BB RID: 955
	public class ItemTemplate
	{
		// Token: 0x06002A58 RID: 10840 RVA: 0x0029B4AC File Offset: 0x002996AC
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

		// Token: 0x0400521A RID: 21018
		public short id;

		// Token: 0x0400521B RID: 21019
		public sbyte type;

		// Token: 0x0400521C RID: 21020
		public sbyte gender;

		// Token: 0x0400521D RID: 21021
		public string name;

		// Token: 0x0400521E RID: 21022
		public string[] subName;

		// Token: 0x0400521F RID: 21023
		public string description;

		// Token: 0x04005220 RID: 21024
		public sbyte level;

		// Token: 0x04005221 RID: 21025
		public short iconID;

		// Token: 0x04005222 RID: 21026
		public short part;

		// Token: 0x04005223 RID: 21027
		public bool isUpToUp;

		// Token: 0x04005224 RID: 21028
		public int w;

		// Token: 0x04005225 RID: 21029
		public int h;

		// Token: 0x04005226 RID: 21030
		public int strRequire;
	}
}
