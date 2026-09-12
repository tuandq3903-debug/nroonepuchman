using System;

namespace Game3
{
	// Token: 0x020002E4 RID: 740
	public class ItemTemplates
	{
		// Token: 0x060020B5 RID: 8373 RVA: 0x0020648A File Offset: 0x0020468A
		public static void add(ItemTemplate it)
		{
			ItemTemplates.itemTemplates.put(it.id, it);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x002064A2 File Offset: 0x002046A2
		public static ItemTemplate get(short id)
		{
			return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
		}

		// Token: 0x04003FA8 RID: 16296
		public static MyHashTable itemTemplates = new MyHashTable();
	}
}
