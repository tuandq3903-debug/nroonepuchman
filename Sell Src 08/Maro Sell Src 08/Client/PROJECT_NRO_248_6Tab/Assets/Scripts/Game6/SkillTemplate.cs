using System;

namespace Game6
{
	// Token: 0x020000AE RID: 174
	public class SkillTemplate
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x0008615A File Offset: 0x0008435A
		public bool isBuffToPlayer()
		{
			return this.type == 2;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00086168 File Offset: 0x00084368
		public bool isUseAlone()
		{
			return this.type == 3;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00086176 File Offset: 0x00084376
		public bool isAttackSkill()
		{
			return this.type == 1;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00086184 File Offset: 0x00084384
		public bool isSkillSpec()
		{
			return this.type == 4;
		}

		// Token: 0x0400102F RID: 4143
		public sbyte id;

		// Token: 0x04001030 RID: 4144
		public int classId;

		// Token: 0x04001031 RID: 4145
		public string name;

		// Token: 0x04001032 RID: 4146
		public int maxPoint;

		// Token: 0x04001033 RID: 4147
		public int manaUseType;

		// Token: 0x04001034 RID: 4148
		public int type;

		// Token: 0x04001035 RID: 4149
		public int iconId;

		// Token: 0x04001036 RID: 4150
		public string[] description;

		// Token: 0x04001037 RID: 4151
		public Skill[] skills;

		// Token: 0x04001038 RID: 4152
		public string damInfo;
	}
}
