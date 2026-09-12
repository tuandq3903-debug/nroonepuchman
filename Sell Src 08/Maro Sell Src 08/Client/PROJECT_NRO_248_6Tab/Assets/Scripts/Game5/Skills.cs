using System;

namespace Game5
{
	// Token: 0x02000185 RID: 389
	public class Skills
	{
		// Token: 0x060011A2 RID: 4514 RVA: 0x0011B22B File Offset: 0x0011942B
		public static void add(Skill skill)
		{
			Skills.skills.put(skill.skillId, skill);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0011B243 File Offset: 0x00119443
		public static Skill get(short skillId)
		{
			return (Skill)Skills.skills.get(skillId);
		}

		// Token: 0x040022AD RID: 8877
		public static MyHashTable skills = new MyHashTable();
	}
}
