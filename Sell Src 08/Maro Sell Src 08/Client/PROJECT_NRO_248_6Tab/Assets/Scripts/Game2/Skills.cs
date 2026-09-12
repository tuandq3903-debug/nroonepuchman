using System;

namespace Game2
{
	// Token: 0x0200040D RID: 1037
	public class Skills
	{
		// Token: 0x06002E8E RID: 11918 RVA: 0x002DA417 File Offset: 0x002D8617
		public static void add(Skill skill)
		{
			Skills.skills.put(skill.skillId, skill);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x002DA42F File Offset: 0x002D862F
		public static Skill get(short skillId)
		{
			return (Skill)Skills.skills.get(skillId);
		}

		// Token: 0x04005A2A RID: 23082
		public static MyHashTable skills = new MyHashTable();
	}
}
