using System;

namespace Game5
{
	// Token: 0x02000134 RID: 308
	public class ItemTemplates
	{
		// Token: 0x06000D6D RID: 3437 RVA: 0x000DC342 File Offset: 0x000DA542
		public static void add(ItemTemplate it)
		{
			ItemTemplates.itemTemplates.put(it.id, it);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x000DC35A File Offset: 0x000DA55A
		public static ItemTemplate get(short id)
		{
			return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
		}

		// Token: 0x04001AAA RID: 6826
		public static MyHashTable itemTemplates = new MyHashTable();
	}
}
