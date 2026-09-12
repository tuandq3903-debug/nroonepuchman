using System;
using System.Collections;

namespace Game6
{
	// Token: 0x0200007C RID: 124
	public class MyHashTable
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x0005ADF4 File Offset: 0x00058FF4
		public object get(object k)
		{
			return this.h[k];
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0005AE02 File Offset: 0x00059002
		public void clear()
		{
			this.h.Clear();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0005AE0F File Offset: 0x0005900F
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.h.GetEnumerator();
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0005AE1C File Offset: 0x0005901C
		public int size()
		{
			return this.h.Count;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0005AE29 File Offset: 0x00059029
		public void put(object k, object v)
		{
			if (this.h.ContainsKey(k))
			{
				this.h.Remove(k);
			}
			this.h.Add(k, v);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0005AE52 File Offset: 0x00059052
		public void remove(object k)
		{
			this.h.Remove(k);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0005AE60 File Offset: 0x00059060
		public bool containsKey(object key)
		{
			return this.h.ContainsKey(key);
		}

		// Token: 0x04000CDF RID: 3295
		public Hashtable h = new Hashtable();
	}
}
