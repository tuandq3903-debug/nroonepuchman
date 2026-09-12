using System;

namespace Game2
{
	// Token: 0x0200038A RID: 906
	public class DataOutputStream
	{
		// Token: 0x06002861 RID: 10337 RVA: 0x0027E417 File Offset: 0x0027C617
		public DataOutputStream()
		{
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x0027E42A File Offset: 0x0027C62A
		public DataOutputStream(int len)
		{
			this.w = new myWriter(len);
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x0027E449 File Offset: 0x0027C649
		public void writeShort(short i)
		{
			this.w.writeShort(i);
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x0027E457 File Offset: 0x0027C657
		public sbyte[] toByteArray()
		{
			return this.w.getData();
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x0027E464 File Offset: 0x0027C664
		public void close()
		{
			this.w.Close();
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0027E471 File Offset: 0x0027C671
		public void writeByte(sbyte b)
		{
			this.w.writeByte(b);
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x0027E47F File Offset: 0x0027C67F
		public void writeUTF(string name)
		{
			this.w.writeUTF(name);
		}

		// Token: 0x04004D94 RID: 19860
		private myWriter w = new myWriter();
	}
}
