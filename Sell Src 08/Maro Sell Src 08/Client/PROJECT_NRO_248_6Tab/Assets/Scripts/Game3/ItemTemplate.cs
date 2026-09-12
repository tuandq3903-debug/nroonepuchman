using System;

namespace Game3
{
	// Token: 0x020002E3 RID: 739
	public class ItemTemplate
	{
		// Token: 0x060020B4 RID: 8372 RVA: 0x00206408 File Offset: 0x00204608
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

		// Token: 0x04003F9B RID: 16283
		public short id;

		// Token: 0x04003F9C RID: 16284
		public sbyte type;

		// Token: 0x04003F9D RID: 16285
		public sbyte gender;

		// Token: 0x04003F9E RID: 16286
		public string name;

		// Token: 0x04003F9F RID: 16287
		public string[] subName;

		// Token: 0x04003FA0 RID: 16288
		public string description;

		// Token: 0x04003FA1 RID: 16289
		public sbyte level;

		// Token: 0x04003FA2 RID: 16290
		public short iconID;

		// Token: 0x04003FA3 RID: 16291
		public short part;

		// Token: 0x04003FA4 RID: 16292
		public bool isUpToUp;

		// Token: 0x04003FA5 RID: 16293
		public int w;

		// Token: 0x04003FA6 RID: 16294
		public int h;

		// Token: 0x04003FA7 RID: 16295
		public int strRequire;
	}
}
