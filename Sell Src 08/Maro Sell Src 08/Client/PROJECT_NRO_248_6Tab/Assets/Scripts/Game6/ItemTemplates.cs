using System;

namespace Game6
{
	// Token: 0x0200005C RID: 92
	public class ItemTemplates
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0004719A File Offset: 0x0004539A
		public static void add(ItemTemplate it)
		{
			ItemTemplates.itemTemplates.put(it.id, it);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000471B2 File Offset: 0x000453B2
		public static ItemTemplate get(short id)
		{
			return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
		}

		// Token: 0x0400082B RID: 2091
		public static MyHashTable itemTemplates = new MyHashTable();
	}
}
