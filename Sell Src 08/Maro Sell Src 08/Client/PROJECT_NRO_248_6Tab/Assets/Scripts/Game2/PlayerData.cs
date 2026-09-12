using System;

namespace Game2
{
	// Token: 0x020003F1 RID: 1009
	public class PlayerData
	{
		// Token: 0x06002D56 RID: 11606 RVA: 0x002CF1F5 File Offset: 0x002CD3F5
		public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
		{
			this.playerID = playerID;
			this.name = name;
			this.head = head;
			this.body = body;
			this.leg = leg;
			this.powpoint = ppoint;
		}

		// Token: 0x040058B3 RID: 22707
		public int playerID;

		// Token: 0x040058B4 RID: 22708
		public string name;

		// Token: 0x040058B5 RID: 22709
		public short head;

		// Token: 0x040058B6 RID: 22710
		public short body;

		// Token: 0x040058B7 RID: 22711
		public short leg;

		// Token: 0x040058B8 RID: 22712
		public long powpoint;
	}
}
