using System;
using System.Collections;

namespace Game3
{
	// Token: 0x02000309 RID: 777
	public class MyVector
	{
		// Token: 0x0600225F RID: 8799 RVA: 0x0021A9C4 File Offset: 0x00218BC4
		public MyVector()
		{
			this.a = new ArrayList();
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0021A9C4 File Offset: 0x00218BC4
		public MyVector(string s)
		{
			this.a = new ArrayList();
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0021A9D7 File Offset: 0x00218BD7
		public void addElement(object o)
		{
			this.a.Add(o);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0021A9E6 File Offset: 0x00218BE6
		public bool contains(object o)
		{
			return this.a.Contains(o);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0021A9F9 File Offset: 0x00218BF9
		public int size()
		{
			if (this.a == null)
			{
				return 0;
			}
			return this.a.Count;
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0021AA10 File Offset: 0x00218C10
		public object elementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				return this.a[index];
			}
			return null;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0021AA32 File Offset: 0x00218C32
		public void setElementAt(object obj, int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a[index] = obj;
			}
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0021AA53 File Offset: 0x00218C53
		public int indexOf(object o)
		{
			return this.a.IndexOf(o);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x0021AA61 File Offset: 0x00218C61
		public void removeElementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a.RemoveAt(index);
			}
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x0021AA81 File Offset: 0x00218C81
		public void removeElement(object o)
		{
			this.a.Remove(o);
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x0021AA8F File Offset: 0x00218C8F
		public void removeAllElements()
		{
			this.a.Clear();
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x0021AA9C File Offset: 0x00218C9C
		public void insertElementAt(object o, int i)
		{
			this.a.Insert(i, o);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0021AAAB File Offset: 0x00218CAB
		public object firstElement()
		{
			return this.a[0];
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0021AAB9 File Offset: 0x00218CB9
		public object lastElement()
		{
			return this.a[this.a.Count - 1];
		}

		// Token: 0x04004462 RID: 17506
		private ArrayList a;
	}
}
