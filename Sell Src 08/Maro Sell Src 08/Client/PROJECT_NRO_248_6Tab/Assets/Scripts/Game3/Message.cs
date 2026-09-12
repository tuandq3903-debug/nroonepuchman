using System;

namespace Game3
{
	// Token: 0x020002F1 RID: 753
	public class Message
	{
		// Token: 0x0600210B RID: 8459 RVA: 0x0020A5BE File Offset: 0x002087BE
		public Message(int command)
		{
			this.command = (sbyte)command;
			this.dos = new myWriter();
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0020A5D9 File Offset: 0x002087D9
		public Message(sbyte command)
		{
			this.command = command;
			this.dos = new myWriter();
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x0020A5F3 File Offset: 0x002087F3
		public Message(sbyte command, sbyte[] data)
		{
			this.command = command;
			this.dis = new myReader(data);
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0020A60E File Offset: 0x0020880E
		public sbyte[] getData()
		{
			return this.dos.getData();
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0020A61B File Offset: 0x0020881B
		public myReader reader()
		{
			return this.dis;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0020A623 File Offset: 0x00208823
		public myWriter writer()
		{
			return this.dos;
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0020A62B File Offset: 0x0020882B
		public int readInt3Byte()
		{
			return this.dis.readInt();
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000034B9 File Offset: 0x000016B9
		public void cleanup()
		{
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0020A638 File Offset: 0x00208838
		public long readLong()
		{
			if (ModFunc.isReadInt)
			{
				return (long)this.dis.readInt();
			}
			return this.dis.readLong();
		}

		// Token: 0x04004090 RID: 16528
		public sbyte command;

		// Token: 0x04004091 RID: 16529
		private myReader dis;

		// Token: 0x04004092 RID: 16530
		private myWriter dos;
	}
}
