using System;
using System.Collections;

namespace Game5
{
	// Token: 0x02000154 RID: 340
	public class MyHashTable
	{
		// Token: 0x06000EF5 RID: 3829 RVA: 0x000EFF5C File Offset: 0x000EE15C
		public object get(object k)
		{
			return this.h[k];
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000EFF6A File Offset: 0x000EE16A
		public void clear()
		{
			this.h.Clear();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000EFF77 File Offset: 0x000EE177
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.h.GetEnumerator();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000EFF84 File Offset: 0x000EE184
		public int size()
		{
			return this.h.Count;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000EFF91 File Offset: 0x000EE191
		public void put(object k, object v)
		{
			if (this.h.ContainsKey(k))
			{
				this.h.Remove(k);
			}
			this.h.Add(k, v);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000EFFBA File Offset: 0x000EE1BA
		public void remove(object k)
		{
			this.h.Remove(k);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000EFFC8 File Offset: 0x000EE1C8
		public bool containsKey(object key)
		{
			return this.h.ContainsKey(key);
		}

		// Token: 0x04001F5E RID: 8030
		public Hashtable h = new Hashtable();
	}
}
