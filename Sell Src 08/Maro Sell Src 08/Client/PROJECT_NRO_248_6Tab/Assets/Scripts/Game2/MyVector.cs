using System;
using System.Collections;

namespace Game2
{
	// Token: 0x020003E1 RID: 993
	public class MyVector
	{
		// Token: 0x06002C03 RID: 11267 RVA: 0x002AFA68 File Offset: 0x002ADC68
		public MyVector()
		{
			this.a = new ArrayList();
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x002AFA68 File Offset: 0x002ADC68
		public MyVector(string s)
		{
			this.a = new ArrayList();
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x002AFA7B File Offset: 0x002ADC7B
		public void addElement(object o)
		{
			this.a.Add(o);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x002AFA8A File Offset: 0x002ADC8A
		public bool contains(object o)
		{
			return this.a.Contains(o);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x002AFA9D File Offset: 0x002ADC9D
		public int size()
		{
			if (this.a == null)
			{
				return 0;
			}
			return this.a.Count;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x002AFAB4 File Offset: 0x002ADCB4
		public object elementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				return this.a[index];
			}
			return null;
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x002AFAD6 File Offset: 0x002ADCD6
		public void setElementAt(object obj, int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a[index] = obj;
			}
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x002AFAF7 File Offset: 0x002ADCF7
		public int indexOf(object o)
		{
			return this.a.IndexOf(o);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x002AFB05 File Offset: 0x002ADD05
		public void removeElementAt(int index)
		{
			if (index > -1 && index < this.a.Count)
			{
				this.a.RemoveAt(index);
			}
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x002AFB25 File Offset: 0x002ADD25
		public void removeElement(object o)
		{
			this.a.Remove(o);
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x002AFB33 File Offset: 0x002ADD33
		public void removeAllElements()
		{
			this.a.Clear();
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x002AFB40 File Offset: 0x002ADD40
		public void insertElementAt(object o, int i)
		{
			this.a.Insert(i, o);
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x002AFB4F File Offset: 0x002ADD4F
		public object firstElement()
		{
			return this.a[0];
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x002AFB5D File Offset: 0x002ADD5D
		public object lastElement()
		{
			return this.a[this.a.Count - 1];
		}

		// Token: 0x040056E1 RID: 22241
		private ArrayList a;
	}
}
