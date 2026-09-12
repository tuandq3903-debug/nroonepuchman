using System;

namespace Game2
{
	// Token: 0x020003BC RID: 956
	public class ItemTemplates
	{
		// Token: 0x06002A59 RID: 10841 RVA: 0x0029B52E File Offset: 0x0029972E
		public static void add(ItemTemplate it)
		{
			ItemTemplates.itemTemplates.put(it.id, it);
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0029B546 File Offset: 0x00299746
		public static ItemTemplate get(short id)
		{
			return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
		}

		// Token: 0x04005227 RID: 21031
		public static MyHashTable itemTemplates = new MyHashTable();
	}
}
