using System;
using System.Collections;

namespace Game4
{
	// Token: 0x02000231 RID: 561
	public class MyVector
	{
		// Token: 0x060018BB RID: 6331 RVA: 0x00185920 File Offset: 0x00183B20
		public MyVector()
		{
			this.a = new ArrayList();
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00185920 File Offset: 0x00183B20
		public MyVector(string s)
		{
			this.a = new ArrayList();
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00185933 File Offset: 0x00183B33
		public void addElement(object o)
		{
			this.a.Add(o);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00185942 File Offset: 0x00183B42
		public bool contains(object o)
		{
			return this.a.Contains(o);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00185955 File Offset: 0x00183B55
		public int size()
		{
			if (this.a == null)
			{
				return 0;
			}
			return this.a.Count;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0018596C File Offset: 0x00183B6C
		public object elementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				return this.a[index];
			}
			return null;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0018598E File Offset: 0x00183B8E
		public void setElementAt(object obj, int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a[index] = obj;
			}
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x001859AF File Offset: 0x00183BAF
		public int indexOf(object o)
		{
			return this.a.IndexOf(o);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x001859BD File Offset: 0x00183BBD
		public void removeElementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a.RemoveAt(index);
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x001859DD File Offset: 0x00183BDD
		public void removeElement(object o)
		{
			this.a.Remove(o);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x001859EB File Offset: 0x00183BEB
		public void removeAllElements()
		{
			this.a.Clear();
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x001859F8 File Offset: 0x00183BF8
		public void insertElementAt(object o, int i)
		{
			this.a.Insert(i, o);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00185A07 File Offset: 0x00183C07
		public object firstElement()
		{
			return this.a[0];
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00185A15 File Offset: 0x00183C15
		public object lastElement()
		{
			return this.a[this.a.Count - 1];
		}

		// Token: 0x040031E3 RID: 12771
		private ArrayList a;
	}
}
