using System;

namespace Game4
{
	// Token: 0x020001DA RID: 474
	public class DataOutputStream
	{
		// Token: 0x06001519 RID: 5401 RVA: 0x001542CF File Offset: 0x001524CF
		public DataOutputStream()
		{
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x001542E2 File Offset: 0x001524E2
		public DataOutputStream(int len)
		{
			this.w = new myWriter(len);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00154301 File Offset: 0x00152501
		public void writeShort(short i)
		{
			this.w.writeShort(i);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0015430F File Offset: 0x0015250F
		public sbyte[] toByteArray()
		{
			return this.w.getData();
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0015431C File Offset: 0x0015251C
		public void close()
		{
			this.w.Close();
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00154329 File Offset: 0x00152529
		public void writeByte(sbyte b)
		{
			this.w.writeByte(b);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00154337 File Offset: 0x00152537
		public void writeUTF(string name)
		{
			this.w.writeUTF(name);
		}

		// Token: 0x04002896 RID: 10390
		private myWriter w = new myWriter();
	}
}
