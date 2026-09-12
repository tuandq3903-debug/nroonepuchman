using System;
using System.Collections;

namespace Game6
{
	// Token: 0x02000081 RID: 129
	public class MyVector
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x0005B714 File Offset: 0x00059914
		public MyVector()
		{
			this.a = new ArrayList();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0005B714 File Offset: 0x00059914
		public MyVector(string s)
		{
			this.a = new ArrayList();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0005B727 File Offset: 0x00059927
		public void addElement(object o)
		{
			this.a.Add(o);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0005B736 File Offset: 0x00059936
		public bool contains(object o)
		{
			return this.a.Contains(o);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0005B749 File Offset: 0x00059949
		public int size()
		{
			if (this.a == null)
			{
				return 0;
			}
			return this.a.Count;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0005B760 File Offset: 0x00059960
		public object elementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				return this.a[index];
			}
			return null;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0005B782 File Offset: 0x00059982
		public void setElementAt(object obj, int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a[index] = obj;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0005B7A3 File Offset: 0x000599A3
		public int indexOf(object o)
		{
			return this.a.IndexOf(o);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0005B7B1 File Offset: 0x000599B1
		public void removeElementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a.RemoveAt(index);
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0005B7D1 File Offset: 0x000599D1
		public void removeElement(object o)
		{
			this.a.Remove(o);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0005B7DF File Offset: 0x000599DF
		public void removeAllElements()
		{
			this.a.Clear();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0005B7EC File Offset: 0x000599EC
		public void insertElementAt(object o, int i)
		{
			this.a.Insert(i, o);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0005B7FB File Offset: 0x000599FB
		public object firstElement()
		{
			return this.a[0];
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0005B809 File Offset: 0x00059A09
		public object lastElement()
		{
			return this.a[this.a.Count - 1];
		}

		// Token: 0x04000CE5 RID: 3301
		private ArrayList a;
	}
}
