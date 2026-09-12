using System;

namespace Game5
{
	// Token: 0x02000102 RID: 258
	public class DataOutputStream
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x000BF22B File Offset: 0x000BD42B
		public DataOutputStream()
		{
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000BF23E File Offset: 0x000BD43E
		public DataOutputStream(int len)
		{
			this.w = new myWriter(len);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000BF25D File Offset: 0x000BD45D
		public void writeShort(short i)
		{
			this.w.writeShort(i);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000BF26B File Offset: 0x000BD46B
		public sbyte[] toByteArray()
		{
			return this.w.getData();
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000BF278 File Offset: 0x000BD478
		public void close()
		{
			this.w.Close();
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000BF285 File Offset: 0x000BD485
		public void writeByte(sbyte b)
		{
			this.w.writeByte(b);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000BF293 File Offset: 0x000BD493
		public void writeUTF(string name)
		{
			this.w.writeUTF(name);
		}

		// Token: 0x04001617 RID: 5655
		private myWriter w = new myWriter();
	}
}
