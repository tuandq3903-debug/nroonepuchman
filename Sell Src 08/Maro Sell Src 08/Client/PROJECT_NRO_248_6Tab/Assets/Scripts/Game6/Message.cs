using System;

namespace Game6
{
	// Token: 0x02000069 RID: 105
	public class Message
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0004B2FA File Offset: 0x000494FA
		public Message(int command)
		{
			this.command = (sbyte)command;
			this.dos = new myWriter();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0004B315 File Offset: 0x00049515
		public Message(sbyte command)
		{
			this.command = command;
			this.dos = new myWriter();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0004B32F File Offset: 0x0004952F
		public Message(sbyte command, sbyte[] data)
		{
			this.command = command;
			this.dis = new myReader(data);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0004B34A File Offset: 0x0004954A
		public sbyte[] getData()
		{
			return this.dos.getData();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0004B357 File Offset: 0x00049557
		public myReader reader()
		{
			return this.dis;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0004B35F File Offset: 0x0004955F
		public myWriter writer()
		{
			return this.dos;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0004B367 File Offset: 0x00049567
		public int readInt3Byte()
		{
			return this.dis.readInt();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000034B9 File Offset: 0x000016B9
		public void cleanup()
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0004B374 File Offset: 0x00049574
		public long readLong()
		{
			if (ModFunc.isReadInt)
			{
				return (long)this.dis.readInt();
			}
			return this.dis.readLong();
		}

		// Token: 0x04000913 RID: 2323
		public sbyte command;

		// Token: 0x04000914 RID: 2324
		private myReader dis;

		// Token: 0x04000915 RID: 2325
		private myWriter dos;
	}
}
