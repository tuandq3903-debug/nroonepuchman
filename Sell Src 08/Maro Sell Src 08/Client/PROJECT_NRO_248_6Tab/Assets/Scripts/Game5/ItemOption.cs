using System;

namespace Game5
{
	// Token: 0x02000131 RID: 305
	public class ItemOption
	{
		// Token: 0x06000D66 RID: 3430 RVA: 0x0000237F File Offset: 0x0000057F
		public ItemOption()
		{
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000DC168 File Offset: 0x000DA368
		public bool IsValidOption()
		{
			return this != null && this.optionTemplate != null && this.optionTemplate.id != 21 && this.optionTemplate.id != 200 && this.optionTemplate.id != 72 && this.optionTemplate.id != 57 && this.optionTemplate.id != 58 && this.optionTemplate.id != 34 && this.optionTemplate.id != 35 && this.optionTemplate.id != 36 && this.optionTemplate.id != 102 && this.optionTemplate.id != 107;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000DC228 File Offset: 0x000DA428
		public ItemOption(int optionTemplateId, int param)
		{
			if (optionTemplateId == 22)
			{
				optionTemplateId = 6;
				param *= 1000;
			}
			if (optionTemplateId == 23)
			{
				optionTemplateId = 7;
				param *= 1000;
			}
			this.param = param;
			this.optionTemplate = GameScr.gI().iOptionTemplates[optionTemplateId];
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000DC276 File Offset: 0x000DA476
		public string getOptionString()
		{
			return NinjaUtil.Replace(this.optionTemplate.name, "#", this.param.ToString() + string.Empty);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000DC2A2 File Offset: 0x000DA4A2
		public string getOptiongColor()
		{
			return NinjaUtil.Replace(this.optionTemplate.name, "$", string.Empty);
		}

		// Token: 0x04001A96 RID: 6806
		public int param;

		// Token: 0x04001A97 RID: 6807
		public sbyte active;

		// Token: 0x04001A98 RID: 6808
		public sbyte activeCard;

		// Token: 0x04001A99 RID: 6809
		public ItemOptionTemplate optionTemplate;
	}
}
