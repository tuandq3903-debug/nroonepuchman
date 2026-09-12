using System;
using System.Collections;

namespace Game1
{
	// Token: 0x020004B4 RID: 1204
	public class MyHashTable
	{
		// Token: 0x06003585 RID: 13701 RVA: 0x003441EC File Offset: 0x003423EC
		public object get(object k)
		{
			return this.h[k];
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x003441FA File Offset: 0x003423FA
		public void clear()
		{
			this.h.Clear();
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x00344207 File Offset: 0x00342407
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.h.GetEnumerator();
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x00344214 File Offset: 0x00342414
		public int size()
		{
			return this.h.Count;
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x00344221 File Offset: 0x00342421
		public void put(object k, object v)
		{
			if (this.h.ContainsKey(k))
			{
				this.h.Remove(k);
			}
			this.h.Add(k, v);
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x0034424A File Offset: 0x0034244A
		public void remove(object k)
		{
			this.h.Remove(k);
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x00344258 File Offset: 0x00342458
		public bool containsKey(object key)
		{
			return this.h.ContainsKey(key);
		}

		// Token: 0x0400695A RID: 26970
		public Hashtable h = new Hashtable();
	}
}
