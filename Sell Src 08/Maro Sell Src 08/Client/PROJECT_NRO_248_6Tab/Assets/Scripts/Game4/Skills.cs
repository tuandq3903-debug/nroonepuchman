using System;

namespace Game4
{
	// Token: 0x0200025D RID: 605
	public class Skills
	{
		// Token: 0x06001B46 RID: 6982 RVA: 0x001B02CF File Offset: 0x001AE4CF
		public static void add(Skill skill)
		{
			Skills.skills.put(skill.skillId, skill);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x001B02E7 File Offset: 0x001AE4E7
		public static Skill get(short skillId)
		{
			return (Skill)Skills.skills.get(skillId);
		}

		// Token: 0x0400352C RID: 13612
		public static MyHashTable skills = new MyHashTable();
	}
}
