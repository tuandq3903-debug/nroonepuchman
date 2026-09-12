using System;
using System.Collections;

namespace Game1
{
	// Token: 0x020004B9 RID: 1209
	public class MyVector
	{
		// Token: 0x060035A7 RID: 13735 RVA: 0x00344B0C File Offset: 0x00342D0C
		public MyVector()
		{
			this.a = new ArrayList();
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x00344B0C File Offset: 0x00342D0C
		public MyVector(string s)
		{
			this.a = new ArrayList();
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x00344B1F File Offset: 0x00342D1F
		public void addElement(object o)
		{
			this.a.Add(o);
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x00344B2E File Offset: 0x00342D2E
		public bool contains(object o)
		{
			return this.a.Contains(o);
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x00344B41 File Offset: 0x00342D41
		public int size()
		{
			if (this.a == null)
			{
				return 0;
			}
			return this.a.Count;
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x00344B58 File Offset: 0x00342D58
		public object elementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				return this.a[index];
			}
			return null;
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x00344B7A File Offset: 0x00342D7A
		public void setElementAt(object obj, int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a[index] = obj;
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x00344B9B File Offset: 0x00342D9B
		public int indexOf(object o)
		{
			return this.a.IndexOf(o);
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x00344BA9 File Offset: 0x00342DA9
		public void removeElementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a.RemoveAt(index);
			}
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x00344BC9 File Offset: 0x00342DC9
		public void removeElement(object o)
		{
			this.a.Remove(o);
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x00344BD7 File Offset: 0x00342DD7
		public void removeAllElements()
		{
			this.a.Clear();
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x00344BE4 File Offset: 0x00342DE4
		public void insertElementAt(object o, int i)
		{
			this.a.Insert(i, o);
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x00344BF3 File Offset: 0x00342DF3
		public object firstElement()
		{
			return this.a[0];
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x00344C01 File Offset: 0x00342E01
		public object lastElement()
		{
			return this.a[this.a.Count - 1];
		}

		// Token: 0x04006960 RID: 26976
		private ArrayList a;
	}
}
