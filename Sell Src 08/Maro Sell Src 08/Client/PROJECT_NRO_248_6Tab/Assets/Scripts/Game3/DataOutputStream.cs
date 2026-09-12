using System;

namespace Game3
{
	// Token: 0x020002B2 RID: 690
	public class DataOutputStream
	{
		// Token: 0x06001EBD RID: 7869 RVA: 0x001E9373 File Offset: 0x001E7573
		public DataOutputStream()
		{
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x001E9386 File Offset: 0x001E7586
		public DataOutputStream(int len)
		{
			this.w = new myWriter(len);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x001E93A5 File Offset: 0x001E75A5
		public void writeShort(short i)
		{
			this.w.writeShort(i);
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x001E93B3 File Offset: 0x001E75B3
		public sbyte[] toByteArray()
		{
			return this.w.getData();
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x001E93C0 File Offset: 0x001E75C0
		public void close()
		{
			this.w.Close();
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x001E93CD File Offset: 0x001E75CD
		public void writeByte(sbyte b)
		{
			this.w.writeByte(b);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x001E93DB File Offset: 0x001E75DB
		public void writeUTF(string name)
		{
			this.w.writeUTF(name);
		}

		// Token: 0x04003B15 RID: 15125
		private myWriter w = new myWriter();
	}
}
