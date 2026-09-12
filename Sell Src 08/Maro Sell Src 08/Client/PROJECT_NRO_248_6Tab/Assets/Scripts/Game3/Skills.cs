using System;

namespace Game3
{
	// Token: 0x02000335 RID: 821
	public class Skills
	{
		// Token: 0x060024EA RID: 9450 RVA: 0x00245373 File Offset: 0x00243573
		public static void add(Skill skill)
		{
			Skills.skills.put(skill.skillId, skill);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0024538B File Offset: 0x0024358B
		public static Skill get(short skillId)
		{
			return (Skill)Skills.skills.get(skillId);
		}

		// Token: 0x040047AB RID: 18347
		public static MyHashTable skills = new MyHashTable();
	}
}
