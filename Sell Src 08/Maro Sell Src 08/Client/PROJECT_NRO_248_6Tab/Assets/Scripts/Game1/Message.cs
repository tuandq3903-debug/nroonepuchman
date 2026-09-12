using System;

namespace Game1
{
	// Token: 0x020004A1 RID: 1185
	public class Message
	{
		// Token: 0x06003453 RID: 13395 RVA: 0x00334706 File Offset: 0x00332906
		public Message(int command)
		{
			this.command = (sbyte)command;
			this.dos = new myWriter();
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x00334721 File Offset: 0x00332921
		public Message(sbyte command)
		{
			this.command = command;
			this.dos = new myWriter();
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x0033473B File Offset: 0x0033293B
		public Message(sbyte command, sbyte[] data)
		{
			this.command = command;
			this.dis = new myReader(data);
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x00334756 File Offset: 0x00332956
		public sbyte[] getData()
		{
			return this.dos.getData();
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x00334763 File Offset: 0x00332963
		public myReader reader()
		{
			return this.dis;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x0033476B File Offset: 0x0033296B
		public myWriter writer()
		{
			return this.dos;
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x00334773 File Offset: 0x00332973
		public int readInt3Byte()
		{
			return this.dis.readInt();
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000034B9 File Offset: 0x000016B9
		public void cleanup()
		{
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x00334780 File Offset: 0x00332980
		public long readLong()
		{
			if (ModFunc.isReadInt)
			{
				return (long)this.dis.readInt();
			}
			return this.dis.readLong();
		}

		// Token: 0x0400658E RID: 25998
		public sbyte command;

		// Token: 0x0400658F RID: 25999
		private myReader dis;

		// Token: 0x04006590 RID: 26000
		private myWriter dos;
	}
}
