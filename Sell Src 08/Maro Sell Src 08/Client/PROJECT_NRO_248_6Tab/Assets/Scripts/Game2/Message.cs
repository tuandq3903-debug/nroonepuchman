using System;

namespace Game2
{
	// Token: 0x020003C9 RID: 969
	public class Message
	{
		// Token: 0x06002AAF RID: 10927 RVA: 0x0029F662 File Offset: 0x0029D862
		public Message(int command)
		{
			this.command = (sbyte)command;
			this.dos = new myWriter();
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0029F67D File Offset: 0x0029D87D
		public Message(sbyte command)
		{
			this.command = command;
			this.dos = new myWriter();
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0029F697 File Offset: 0x0029D897
		public Message(sbyte command, sbyte[] data)
		{
			this.command = command;
			this.dis = new myReader(data);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x0029F6B2 File Offset: 0x0029D8B2
		public sbyte[] getData()
		{
			return this.dos.getData();
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x0029F6BF File Offset: 0x0029D8BF
		public myReader reader()
		{
			return this.dis;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x0029F6C7 File Offset: 0x0029D8C7
		public myWriter writer()
		{
			return this.dos;
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x0029F6CF File Offset: 0x0029D8CF
		public int readInt3Byte()
		{
			return this.dis.readInt();
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000034B9 File Offset: 0x000016B9
		public void cleanup()
		{
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x0029F6DC File Offset: 0x0029D8DC
		public long readLong()
		{
			if (ModFunc.isReadInt)
			{
				return (long)this.dis.readInt();
			}
			return this.dis.readLong();
		}

		// Token: 0x0400530F RID: 21263
		public sbyte command;

		// Token: 0x04005310 RID: 21264
		private myReader dis;

		// Token: 0x04005311 RID: 21265
		private myWriter dos;
	}
}
