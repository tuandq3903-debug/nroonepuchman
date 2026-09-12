using System;

namespace Game3
{
	// Token: 0x020002E1 RID: 737
	public class ItemOption
	{
		// Token: 0x060020AE RID: 8366 RVA: 0x0000237F File Offset: 0x0000057F
		public ItemOption()
		{
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x002062B0 File Offset: 0x002044B0
		public bool IsValidOption()
		{
			return this != null && this.optionTemplate != null && this.optionTemplate.id != 21 && this.optionTemplate.id != 200 && this.optionTemplate.id != 72 && this.optionTemplate.id != 57 && this.optionTemplate.id != 58 && this.optionTemplate.id != 34 && this.optionTemplate.id != 35 && this.optionTemplate.id != 36 && this.optionTemplate.id != 102 && this.optionTemplate.id != 107;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00206370 File Offset: 0x00204570
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

		// Token: 0x060020B1 RID: 8369 RVA: 0x002063BE File Offset: 0x002045BE
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

		// Token: 0x060020B2 RID: 8370 RVA: 0x002063EA File Offset: 0x002045EA
		public string getOptiongColor()
		{
			return NinjaUtil.Replace(this.optionTemplate.name, "$", string.Empty);
		}

		// Token: 0x04003F94 RID: 16276
		public int param;

		// Token: 0x04003F95 RID: 16277
		public sbyte active;

		// Token: 0x04003F96 RID: 16278
		public sbyte activeCard;

		// Token: 0x04003F97 RID: 16279
		public ItemOptionTemplate optionTemplate;
	}
}
