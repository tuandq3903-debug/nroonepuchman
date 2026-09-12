using System;

namespace Game6
{
	// Token: 0x02000059 RID: 89
	public class ItemOption
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x0000237F File Offset: 0x0000057F
		public ItemOption()
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00046FC0 File Offset: 0x000451C0
		public bool IsValidOption()
		{
			return this != null && this.optionTemplate != null && this.optionTemplate.id != 21 && this.optionTemplate.id != 200 && this.optionTemplate.id != 72 && this.optionTemplate.id != 57 && this.optionTemplate.id != 58 && this.optionTemplate.id != 34 && this.optionTemplate.id != 35 && this.optionTemplate.id != 36 && this.optionTemplate.id != 102 && this.optionTemplate.id != 107;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00047080 File Offset: 0x00045280
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

		// Token: 0x060003C5 RID: 965 RVA: 0x000470CE File Offset: 0x000452CE
		public string getOptionString()
		{
			return NinjaUtil.Replace(this.optionTemplate.name, "#", this.param.ToString() + string.Empty);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000470FA File Offset: 0x000452FA
		public string getOptiongColor()
		{
			return NinjaUtil.Replace(this.optionTemplate.name, "$", string.Empty);
		}

		// Token: 0x04000817 RID: 2071
		public int param;

		// Token: 0x04000818 RID: 2072
		public sbyte active;

		// Token: 0x04000819 RID: 2073
		public sbyte activeCard;

		// Token: 0x0400081A RID: 2074
		public ItemOptionTemplate optionTemplate;
	}
}
