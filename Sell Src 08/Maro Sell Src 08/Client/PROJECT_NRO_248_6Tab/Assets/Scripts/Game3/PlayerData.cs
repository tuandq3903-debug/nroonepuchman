using System;

namespace Game3
{
	// Token: 0x02000319 RID: 793
	public class PlayerData
	{
		// Token: 0x060023B2 RID: 9138 RVA: 0x0023A151 File Offset: 0x00238351
		public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
		{
			this.playerID = playerID;
			this.name = name;
			this.head = head;
			this.body = body;
			this.leg = leg;
			this.powpoint = ppoint;
		}

		// Token: 0x04004634 RID: 17972
		public int playerID;

		// Token: 0x04004635 RID: 17973
		public string name;

		// Token: 0x04004636 RID: 17974
		public short head;

		// Token: 0x04004637 RID: 17975
		public short body;

		// Token: 0x04004638 RID: 17976
		public short leg;

		// Token: 0x04004639 RID: 17977
		public long powpoint;
	}
}
