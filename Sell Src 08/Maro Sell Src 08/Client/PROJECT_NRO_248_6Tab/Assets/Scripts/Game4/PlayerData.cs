using System;

namespace Game4
{
	// Token: 0x02000241 RID: 577
	public class PlayerData
	{
		// Token: 0x06001A0E RID: 6670 RVA: 0x001A50AD File Offset: 0x001A32AD
		public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
		{
			this.playerID = playerID;
			this.name = name;
			this.head = head;
			this.body = body;
			this.leg = leg;
			this.powpoint = ppoint;
		}

		// Token: 0x040033B5 RID: 13237
		public int playerID;

		// Token: 0x040033B6 RID: 13238
		public string name;

		// Token: 0x040033B7 RID: 13239
		public short head;

		// Token: 0x040033B8 RID: 13240
		public short body;

		// Token: 0x040033B9 RID: 13241
		public short leg;

		// Token: 0x040033BA RID: 13242
		public long powpoint;
	}
}
