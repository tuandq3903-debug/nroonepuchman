using System;
using System.Collections;

namespace Game4
{
	// Token: 0x0200022C RID: 556
	public class MyHashTable
	{
		// Token: 0x06001899 RID: 6297 RVA: 0x00185000 File Offset: 0x00183200
		public object get(object k)
		{
			return this.h[k];
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0018500E File Offset: 0x0018320E
		public void clear()
		{
			this.h.Clear();
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0018501B File Offset: 0x0018321B
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.h.GetEnumerator();
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00185028 File Offset: 0x00183228
		public int size()
		{
			return this.h.Count;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00185035 File Offset: 0x00183235
		public void put(object k, object v)
		{
			if (this.h.ContainsKey(k))
			{
				this.h.Remove(k);
			}
			this.h.Add(k, v);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0018505E File Offset: 0x0018325E
		public void remove(object k)
		{
			this.h.Remove(k);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0018506C File Offset: 0x0018326C
		public bool containsKey(object key)
		{
			return this.h.ContainsKey(key);
		}

		// Token: 0x040031DD RID: 12765
		public Hashtable h = new Hashtable();
	}
}
