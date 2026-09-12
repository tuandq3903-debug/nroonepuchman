using System;

namespace Game6
{
	// Token: 0x020000AD RID: 173
	public class Skills
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x0008611F File Offset: 0x0008431F
		public static void add(Skill skill)
		{
			Skills.skills.put(skill.skillId, skill);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00086137 File Offset: 0x00084337
		public static Skill get(short skillId)
		{
			return (Skill)Skills.skills.get(skillId);
		}

		// Token: 0x0400102E RID: 4142
		public static MyHashTable skills = new MyHashTable();
	}
}
