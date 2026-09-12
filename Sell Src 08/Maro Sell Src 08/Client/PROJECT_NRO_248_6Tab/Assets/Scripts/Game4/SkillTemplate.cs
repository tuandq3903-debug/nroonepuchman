using System;

namespace Game4
{
	// Token: 0x0200025E RID: 606
	public class SkillTemplate
	{
		// Token: 0x06001B49 RID: 6985 RVA: 0x001B030A File Offset: 0x001AE50A
		public bool isBuffToPlayer()
		{
			return this.type == 2;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x001B0318 File Offset: 0x001AE518
		public bool isUseAlone()
		{
			return this.type == 3;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x001B0326 File Offset: 0x001AE526
		public bool isAttackSkill()
		{
			return this.type == 1;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x001B0334 File Offset: 0x001AE534
		public bool isSkillSpec()
		{
			return this.type == 4;
		}

		// Token: 0x0400352D RID: 13613
		public sbyte id;

		// Token: 0x0400352E RID: 13614
		public int classId;

		// Token: 0x0400352F RID: 13615
		public string name;

		// Token: 0x04003530 RID: 13616
		public int maxPoint;

		// Token: 0x04003531 RID: 13617
		public int manaUseType;

		// Token: 0x04003532 RID: 13618
		public int type;

		// Token: 0x04003533 RID: 13619
		public int iconId;

		// Token: 0x04003534 RID: 13620
		public string[] description;

		// Token: 0x04003535 RID: 13621
		public Skill[] skills;

		// Token: 0x04003536 RID: 13622
		public string damInfo;
	}
}
