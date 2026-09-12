using System;

namespace Game3
{
	// Token: 0x02000336 RID: 822
	public class SkillTemplate
	{
		// Token: 0x060024ED RID: 9453 RVA: 0x002453AE File Offset: 0x002435AE
		public bool isBuffToPlayer()
		{
			return this.type == 2;
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x002453BC File Offset: 0x002435BC
		public bool isUseAlone()
		{
			return this.type == 3;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x002453CA File Offset: 0x002435CA
		public bool isAttackSkill()
		{
			return this.type == 1;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x002453D8 File Offset: 0x002435D8
		public bool isSkillSpec()
		{
			return this.type == 4;
		}

		// Token: 0x040047AC RID: 18348
		public sbyte id;

		// Token: 0x040047AD RID: 18349
		public int classId;

		// Token: 0x040047AE RID: 18350
		public string name;

		// Token: 0x040047AF RID: 18351
		public int maxPoint;

		// Token: 0x040047B0 RID: 18352
		public int manaUseType;

		// Token: 0x040047B1 RID: 18353
		public int type;

		// Token: 0x040047B2 RID: 18354
		public int iconId;

		// Token: 0x040047B3 RID: 18355
		public string[] description;

		// Token: 0x040047B4 RID: 18356
		public Skill[] skills;

		// Token: 0x040047B5 RID: 18357
		public string damInfo;
	}
}
