using System;

namespace Game1
{
	// Token: 0x020004E6 RID: 1254
	public class SkillTemplate
	{
		// Token: 0x06003835 RID: 14389 RVA: 0x0036F4F6 File Offset: 0x0036D6F6
		public bool isBuffToPlayer()
		{
			return this.type == 2;
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x0036F504 File Offset: 0x0036D704
		public bool isUseAlone()
		{
			return this.type == 3;
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x0036F512 File Offset: 0x0036D712
		public bool isAttackSkill()
		{
			return this.type == 1;
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x0036F520 File Offset: 0x0036D720
		public bool isSkillSpec()
		{
			return this.type == 4;
		}

		// Token: 0x04006CAA RID: 27818
		public sbyte id;

		// Token: 0x04006CAB RID: 27819
		public int classId;

		// Token: 0x04006CAC RID: 27820
		public string name;

		// Token: 0x04006CAD RID: 27821
		public int maxPoint;

		// Token: 0x04006CAE RID: 27822
		public int manaUseType;

		// Token: 0x04006CAF RID: 27823
		public int type;

		// Token: 0x04006CB0 RID: 27824
		public int iconId;

		// Token: 0x04006CB1 RID: 27825
		public string[] description;

		// Token: 0x04006CB2 RID: 27826
		public Skill[] skills;

		// Token: 0x04006CB3 RID: 27827
		public string damInfo;
	}
}
