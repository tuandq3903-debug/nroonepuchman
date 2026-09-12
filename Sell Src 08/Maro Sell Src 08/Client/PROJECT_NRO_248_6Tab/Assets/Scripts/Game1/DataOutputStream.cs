using System;

namespace Game1
{
	// Token: 0x02000462 RID: 1122
	public class DataOutputStream
	{
		// Token: 0x06003205 RID: 12805 RVA: 0x003134BB File Offset: 0x003116BB
		public DataOutputStream()
		{
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x003134CE File Offset: 0x003116CE
		public DataOutputStream(int len)
		{
			this.w = new myWriter(len);
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x003134ED File Offset: 0x003116ED
		public void writeShort(short i)
		{
			this.w.writeShort(i);
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x003134FB File Offset: 0x003116FB
		public sbyte[] toByteArray()
		{
			return this.w.getData();
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00313508 File Offset: 0x00311708
		public void close()
		{
			this.w.Close();
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00313515 File Offset: 0x00311715
		public void writeByte(sbyte b)
		{
			this.w.writeByte(b);
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x00313523 File Offset: 0x00311723
		public void writeUTF(string name)
		{
			this.w.writeUTF(name);
		}

		// Token: 0x04006013 RID: 24595
		private myWriter w = new myWriter();
	}
}
