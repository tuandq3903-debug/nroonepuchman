using System;

namespace Game5
{
	// Token: 0x02000186 RID: 390
	public class SkillTemplate
	{
		// Token: 0x060011A5 RID: 4517 RVA: 0x0011B266 File Offset: 0x00119466
		public bool isBuffToPlayer()
		{
			return this.type == 2;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0011B274 File Offset: 0x00119474
		public bool isUseAlone()
		{
			return this.type == 3;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0011B282 File Offset: 0x00119482
		public bool isAttackSkill()
		{
			return this.type == 1;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0011B290 File Offset: 0x00119490
		public bool isSkillSpec()
		{
			return this.type == 4;
		}

		// Token: 0x040022AE RID: 8878
		public sbyte id;

		// Token: 0x040022AF RID: 8879
		public int classId;

		// Token: 0x040022B0 RID: 8880
		public string name;

		// Token: 0x040022B1 RID: 8881
		public int maxPoint;

		// Token: 0x040022B2 RID: 8882
		public int manaUseType;

		// Token: 0x040022B3 RID: 8883
		public int type;

		// Token: 0x040022B4 RID: 8884
		public int iconId;

		// Token: 0x040022B5 RID: 8885
		public string[] description;

		// Token: 0x040022B6 RID: 8886
		public Skill[] skills;

		// Token: 0x040022B7 RID: 8887
		public string damInfo;
	}
}
