using System;

namespace Game2
{
	// Token: 0x020003B9 RID: 953
	public class ItemOption
	{
		// Token: 0x06002A52 RID: 10834 RVA: 0x0000237F File Offset: 0x0000057F
		public ItemOption()
		{
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x0029B354 File Offset: 0x00299554
		public bool IsValidOption()
		{
			return this != null && this.optionTemplate != null && this.optionTemplate.id != 21 && this.optionTemplate.id != 200 && this.optionTemplate.id != 72 && this.optionTemplate.id != 57 && this.optionTemplate.id != 58 && this.optionTemplate.id != 34 && this.optionTemplate.id != 35 && this.optionTemplate.id != 36 && this.optionTemplate.id != 102 && this.optionTemplate.id != 107;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x0029B414 File Offset: 0x00299614
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

		// Token: 0x06002A55 RID: 10837 RVA: 0x0029B462 File Offset: 0x00299662
		public string getOptionString()
		{
			return NinjaUtil.Replace(this.optionTemplate.name, "#", this.param.ToString() + string.Empty);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x0029B48E File Offset: 0x0029968E
		public string getOptiongColor()
		{
			return NinjaUtil.Replace(this.optionTemplate.name, "$", string.Empty);
		}

		// Token: 0x04005213 RID: 21011
		public int param;

		// Token: 0x04005214 RID: 21012
		public sbyte active;

		// Token: 0x04005215 RID: 21013
		public sbyte activeCard;

		// Token: 0x04005216 RID: 21014
		public ItemOptionTemplate optionTemplate;
	}
}
