using System;

namespace Game1
{
	// Token: 0x020004E5 RID: 1253
	public class Skills
	{
		// Token: 0x06003832 RID: 14386 RVA: 0x0036F4BB File Offset: 0x0036D6BB
		public static void add(Skill skill)
		{
			Skills.skills.put(skill.skillId, skill);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x0036F4D3 File Offset: 0x0036D6D3
		public static Skill get(short skillId)
		{
			return (Skill)Skills.skills.get(skillId);
		}

		// Token: 0x04006CA9 RID: 27817
		public static MyHashTable skills = new MyHashTable();
	}
}
