using System;

namespace Game4
{
	// Token: 0x02000219 RID: 537
	public class Message
	{
		// Token: 0x06001767 RID: 5991 RVA: 0x0017551A File Offset: 0x0017371A
		public Message(int command)
		{
			this.command = (sbyte)command;
			this.dos = new myWriter();
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00175535 File Offset: 0x00173735
		public Message(sbyte command)
		{
			this.command = command;
			this.dos = new myWriter();
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0017554F File Offset: 0x0017374F
		public Message(sbyte command, sbyte[] data)
		{
			this.command = command;
			this.dis = new myReader(data);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0017556A File Offset: 0x0017376A
		public sbyte[] getData()
		{
			return this.dos.getData();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00175577 File Offset: 0x00173777
		public myReader reader()
		{
			return this.dis;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0017557F File Offset: 0x0017377F
		public myWriter writer()
		{
			return this.dos;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00175587 File Offset: 0x00173787
		public int readInt3Byte()
		{
			return this.dis.readInt();
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000034B9 File Offset: 0x000016B9
		public void cleanup()
		{
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00175594 File Offset: 0x00173794
		public long readLong()
		{
			if (ModFunc.isReadInt)
			{
				return (long)this.dis.readInt();
			}
			return this.dis.readLong();
		}

		// Token: 0x04002E11 RID: 11793
		public sbyte command;

		// Token: 0x04002E12 RID: 11794
		private myReader dis;

		// Token: 0x04002E13 RID: 11795
		private myWriter dos;
	}
}
