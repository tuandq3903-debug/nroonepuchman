using System;
using System.Collections;

namespace Game5
{
	// Token: 0x02000159 RID: 345
	public class MyVector
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x000F087C File Offset: 0x000EEA7C
		public MyVector()
		{
			this.a = new ArrayList();
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x000F087C File Offset: 0x000EEA7C
		public MyVector(string s)
		{
			this.a = new ArrayList();
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000F088F File Offset: 0x000EEA8F
		public void addElement(object o)
		{
			this.a.Add(o);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x000F089E File Offset: 0x000EEA9E
		public bool contains(object o)
		{
			return this.a.Contains(o);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x000F08B1 File Offset: 0x000EEAB1
		public int size()
		{
			if (this.a == null)
			{
				return 0;
			}
			return this.a.Count;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000F08C8 File Offset: 0x000EEAC8
		public object elementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				return this.a[index];
			}
			return null;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x000F08EA File Offset: 0x000EEAEA
		public void setElementAt(object obj, int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a[index] = obj;
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x000F090B File Offset: 0x000EEB0B
		public int indexOf(object o)
		{
			return this.a.IndexOf(o);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000F0919 File Offset: 0x000EEB19
		public void removeElementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a.RemoveAt(index);
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x000F0939 File Offset: 0x000EEB39
		public void removeElement(object o)
		{
			this.a.Remove(o);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000F0947 File Offset: 0x000EEB47
		public void removeAllElements()
		{
			this.a.Clear();
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000F0954 File Offset: 0x000EEB54
		public void insertElementAt(object o, int i)
		{
			this.a.Insert(i, o);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x000F0963 File Offset: 0x000EEB63
		public object firstElement()
		{
			return this.a[0];
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x000F0971 File Offset: 0x000EEB71
		public object lastElement()
		{
			return this.a[this.a.Count - 1];
		}

		// Token: 0x04001F64 RID: 8036
		private ArrayList a;
	}
}
