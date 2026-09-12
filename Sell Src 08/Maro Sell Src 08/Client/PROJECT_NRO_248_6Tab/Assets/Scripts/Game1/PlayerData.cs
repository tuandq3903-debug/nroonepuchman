using System;

namespace Game1
{
	// Token: 0x020004C9 RID: 1225
	public class PlayerData
	{
		// Token: 0x060036FA RID: 14074 RVA: 0x00364299 File Offset: 0x00362499
		public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
		{
			this.playerID = playerID;
			this.name = name;
			this.head = head;
			this.body = body;
			this.leg = leg;
			this.powpoint = ppoint;
		}

		// Token: 0x04006B32 RID: 27442
		public int playerID;

		// Token: 0x04006B33 RID: 27443
		public string name;

		// Token: 0x04006B34 RID: 27444
		public short head;

		// Token: 0x04006B35 RID: 27445
		public short body;

		// Token: 0x04006B36 RID: 27446
		public short leg;

		// Token: 0x04006B37 RID: 27447
		public long powpoint;
	}
}
