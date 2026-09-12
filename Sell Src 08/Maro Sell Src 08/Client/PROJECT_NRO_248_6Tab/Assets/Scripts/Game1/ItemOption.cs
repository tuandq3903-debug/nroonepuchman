using System;

namespace Game1
{
	// Token: 0x02000491 RID: 1169
	public class ItemOption
	{
		// Token: 0x060033F6 RID: 13302 RVA: 0x0000237F File Offset: 0x0000057F
		public ItemOption()
		{
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x003303F8 File Offset: 0x0032E5F8
		public bool IsValidOption()
		{
			return this != null && this.optionTemplate != null && this.optionTemplate.id != 21 && this.optionTemplate.id != 200 && this.optionTemplate.id != 72 && this.optionTemplate.id != 57 && this.optionTemplate.id != 58 && this.optionTemplate.id != 34 && this.optionTemplate.id != 35 && this.optionTemplate.id != 36 && this.optionTemplate.id != 102 && this.optionTemplate.id != 107;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x003304B8 File Offset: 0x0032E6B8
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

		// Token: 0x060033F9 RID: 13305 RVA: 0x00330506 File Offset: 0x0032E706
		public string getOptionString()
		{
			if (this.param > 0 && !this.optionTemplate.name.Contains("#") && this.optionTemplate.id >= 136 && this.optionTemplate.id <= 144)
			{
				string optionName = this.optionTemplate.name.Replace("$", string.Empty);
				if (this.optionTemplate.id == 137 || this.optionTemplate.id == 139)
				{
					return optionName.Replace("#", this.param.ToString());
				}
				return optionName.Replace("+ sát thương", "+" + this.param.ToString() + "% sát thương").Replace("+sát thương", "+" + this.param.ToString() + "% sát thương").Replace("+ HP", "+" + this.param.ToString() + "% HP").Replace("+HP", "+" + this.param.ToString() + "% HP").Replace("+ KI", "+" + this.param.ToString() + "% KI").Replace("+KI", "+" + this.param.ToString() + "% KI");
			}
			return NinjaUtil.Replace(this.optionTemplate.name, "#", this.param.ToString() + string.Empty);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x00330532 File Offset: 0x0032E732
		public string getOptiongColor()
		{
			return NinjaUtil.Replace(this.optionTemplate.name, "$", string.Empty);
		}

		// Token: 0x04006492 RID: 25746
		public int param;

		// Token: 0x04006493 RID: 25747
		public sbyte active;

		// Token: 0x04006494 RID: 25748
		public sbyte activeCard;

		// Token: 0x04006495 RID: 25749
		public ItemOptionTemplate optionTemplate;
	}
}
