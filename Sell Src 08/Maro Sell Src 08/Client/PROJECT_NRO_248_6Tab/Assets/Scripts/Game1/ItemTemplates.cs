using System;

namespace Game1
{
	// Token: 0x02000494 RID: 1172
	public class ItemTemplates
	{
		// Token: 0x060033FD RID: 13309 RVA: 0x003305D2 File Offset: 0x0032E7D2
		public static void add(ItemTemplate it)
		{
			ItemTemplates.itemTemplates.put(it.id, it);
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x003305EA File Offset: 0x0032E7EA
		public static ItemTemplate get(short id)
		{
			return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
		}

		// Token: 0x040064A6 RID: 25766
		public static MyHashTable itemTemplates = new MyHashTable();
	}
}
