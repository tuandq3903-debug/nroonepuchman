using System;

namespace Game5
{
	// Token: 0x02000141 RID: 321
	public class Message
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x000E0476 File Offset: 0x000DE676
		public Message(int command)
		{
			this.command = (sbyte)command;
			this.dos = new myWriter();
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000E0491 File Offset: 0x000DE691
		public Message(sbyte command)
		{
			this.command = command;
			this.dos = new myWriter();
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000E04AB File Offset: 0x000DE6AB
		public Message(sbyte command, sbyte[] data)
		{
			this.command = command;
			this.dis = new myReader(data);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000E04C6 File Offset: 0x000DE6C6
		public sbyte[] getData()
		{
			return this.dos.getData();
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x000E04D3 File Offset: 0x000DE6D3
		public myReader reader()
		{
			return this.dis;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x000E04DB File Offset: 0x000DE6DB
		public myWriter writer()
		{
			return this.dos;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000E04E3 File Offset: 0x000DE6E3
		public int readInt3Byte()
		{
			return this.dis.readInt();
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000034B9 File Offset: 0x000016B9
		public void cleanup()
		{
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000E04F0 File Offset: 0x000DE6F0
		public long readLong()
		{
			if (ModFunc.isReadInt)
			{
				return (long)this.dis.readInt();
			}
			return this.dis.readLong();
		}

		// Token: 0x04001B92 RID: 7058
		public sbyte command;

		// Token: 0x04001B93 RID: 7059
		private myReader dis;

		// Token: 0x04001B94 RID: 7060
		private myWriter dos;
	}
}
