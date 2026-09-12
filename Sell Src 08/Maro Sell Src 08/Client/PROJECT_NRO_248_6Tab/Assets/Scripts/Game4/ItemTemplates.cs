using System;

namespace Game4
{
	// Token: 0x0200020C RID: 524
	public class ItemTemplates
	{
		// Token: 0x06001711 RID: 5905 RVA: 0x001713E6 File Offset: 0x0016F5E6
		public static void add(ItemTemplate it)
		{
			ItemTemplates.itemTemplates.put(it.id, it);
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x001713FE File Offset: 0x0016F5FE
		public static ItemTemplate get(short id)
		{
			return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
		}

		// Token: 0x04002D29 RID: 11561
		public static MyHashTable itemTemplates = new MyHashTable();
	}
}
