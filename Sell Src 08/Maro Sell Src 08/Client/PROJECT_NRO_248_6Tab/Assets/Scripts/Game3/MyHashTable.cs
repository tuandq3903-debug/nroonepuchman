using System;
using System.Collections;

namespace Game3
{
	// Token: 0x02000304 RID: 772
	public class MyHashTable
	{
		// Token: 0x0600223D RID: 8765 RVA: 0x0021A0A4 File Offset: 0x002182A4
		public object get(object k)
		{
			return this.h[k];
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0021A0B2 File Offset: 0x002182B2
		public void clear()
		{
			this.h.Clear();
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0021A0BF File Offset: 0x002182BF
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.h.GetEnumerator();
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0021A0CC File Offset: 0x002182CC
		public int size()
		{
			return this.h.Count;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0021A0D9 File Offset: 0x002182D9
		public void put(object k, object v)
		{
			if (this.h.ContainsKey(k))
			{
				this.h.Remove(k);
			}
			this.h.Add(k, v);
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0021A102 File Offset: 0x00218302
		public void remove(object k)
		{
			this.h.Remove(k);
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x0021A110 File Offset: 0x00218310
		public bool containsKey(object key)
		{
			return this.h.ContainsKey(key);
		}

		// Token: 0x0400445C RID: 17500
		public Hashtable h = new Hashtable();
	}
}
