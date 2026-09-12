using System;

namespace Game4
{
	// Token: 0x02000209 RID: 521
	public class ItemOption
	{
		// Token: 0x0600170A RID: 5898 RVA: 0x0000237F File Offset: 0x0000057F
		public ItemOption()
		{
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0017120C File Offset: 0x0016F40C
		public bool IsValidOption()
		{
			return this != null && this.optionTemplate != null && this.optionTemplate.id != 21 && this.optionTemplate.id != 200 && this.optionTemplate.id != 72 && this.optionTemplate.id != 57 && this.optionTemplate.id != 58 && this.optionTemplate.id != 34 && this.optionTemplate.id != 35 && this.optionTemplate.id != 36 && this.optionTemplate.id != 102 && this.optionTemplate.id != 107;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x001712CC File Offset: 0x0016F4CC
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

		// Token: 0x0600170D RID: 5901 RVA: 0x0017131A File Offset: 0x0016F51A
		public string getOptionString()
		{
			if (this.param > 0 && !this.optionTemplate.name.Contains("#") && this.optionTemplate.id >= 136 && this.optionTemplate.id <= 144)
			{
				string optionName = this.optionTemplate.name.Replace("$", string.Empty);
				if (this.optionTemplate.id == 137 || this.optionTemplate.id == 139) return optionName.Replace("#", this.param.ToString());
				return optionName.Replace("+ sát thương", "+" + this.param.ToString() + "% sát thương").Replace("+sát thương", "+" + this.param.ToString() + "% sát thương").Replace("+ HP", "+" + this.param.ToString() + "% HP").Replace("+HP", "+" + this.param.ToString() + "% HP").Replace("+ KI", "+" + this.param.ToString() + "% KI").Replace("+KI", "+" + this.param.ToString() + "% KI");
			}
			return NinjaUtil.Replace(this.optionTemplate.name, "#", this.param.ToString() + string.Empty);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00171346 File Offset: 0x0016F546
		public string getOptiongColor()
		{
			return NinjaUtil.Replace(this.optionTemplate.name, "$", string.Empty);
		}

		// Token: 0x04002D15 RID: 11541
		public int param;

		// Token: 0x04002D16 RID: 11542
		public sbyte active;

		// Token: 0x04002D17 RID: 11543
		public sbyte activeCard;

		// Token: 0x04002D18 RID: 11544
		public ItemOptionTemplate optionTemplate;
	}
}
