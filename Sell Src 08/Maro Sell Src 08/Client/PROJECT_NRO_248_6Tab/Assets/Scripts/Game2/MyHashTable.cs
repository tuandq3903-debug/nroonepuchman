using System;
using System.Collections;

namespace Game2
{
	// Token: 0x020003DC RID: 988
	public class MyHashTable
	{
		// Token: 0x06002BE1 RID: 11233 RVA: 0x002AF148 File Offset: 0x002AD348
		public object get(object k)
		{
			return this.h[k];
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x002AF156 File Offset: 0x002AD356
		public void clear()
		{
			this.h.Clear();
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x002AF163 File Offset: 0x002AD363
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.h.GetEnumerator();
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x002AF170 File Offset: 0x002AD370
		public int size()
		{
			return this.h.Count;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x002AF17D File Offset: 0x002AD37D
		public void put(object k, object v)
		{
			if (this.h.ContainsKey(k))
			{
				this.h.Remove(k);
			}
			this.h.Add(k, v);
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x002AF1A6 File Offset: 0x002AD3A6
		public void remove(object k)
		{
			this.h.Remove(k);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x002AF1B4 File Offset: 0x002AD3B4
		public bool containsKey(object key)
		{
			return this.h.ContainsKey(key);
		}

		// Token: 0x040056DB RID: 22235
		public Hashtable h = new Hashtable();
	}
}
