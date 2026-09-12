using System;

namespace Game2
{
	// Token: 0x0200040E RID: 1038
	public class SkillTemplate
	{
		// Token: 0x06002E91 RID: 11921 RVA: 0x002DA452 File Offset: 0x002D8652
		public bool isBuffToPlayer()
		{
			return this.type == 2;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x002DA460 File Offset: 0x002D8660
		public bool isUseAlone()
		{
			return this.type == 3;
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x002DA46E File Offset: 0x002D866E
		public bool isAttackSkill()
		{
			return this.type == 1;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x002DA47C File Offset: 0x002D867C
		public bool isSkillSpec()
		{
			return this.type == 4;
		}

		// Token: 0x04005A2B RID: 23083
		public sbyte id;

		// Token: 0x04005A2C RID: 23084
		public int classId;

		// Token: 0x04005A2D RID: 23085
		public string name;

		// Token: 0x04005A2E RID: 23086
		public int maxPoint;

		// Token: 0x04005A2F RID: 23087
		public int manaUseType;

		// Token: 0x04005A30 RID: 23088
		public int type;

		// Token: 0x04005A31 RID: 23089
		public int iconId;

		// Token: 0x04005A32 RID: 23090
		public string[] description;

		// Token: 0x04005A33 RID: 23091
		public Skill[] skills;

		// Token: 0x04005A34 RID: 23092
		public string damInfo;
	}
}
