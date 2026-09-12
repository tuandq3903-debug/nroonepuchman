using System;

namespace Game6
{
	// Token: 0x02000091 RID: 145
	public class PlayerData
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x0007AEB1 File Offset: 0x000790B1
		public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
		{
			this.playerID = playerID;
			this.name = name;
			this.head = head;
			this.body = body;
			this.leg = leg;
			this.powpoint = ppoint;
		}

		// Token: 0x04000EB7 RID: 3767
		public int playerID;

		// Token: 0x04000EB8 RID: 3768
		public string name;

		// Token: 0x04000EB9 RID: 3769
		public short head;

		// Token: 0x04000EBA RID: 3770
		public short body;

		// Token: 0x04000EBB RID: 3771
		public short leg;

		// Token: 0x04000EBC RID: 3772
		public long powpoint;
	}
}
