using System;

namespace Game5
{
	// Token: 0x02000169 RID: 361
	public class PlayerData
	{
		// Token: 0x0600106A RID: 4202 RVA: 0x00110009 File Offset: 0x0010E209
		public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
		{
			this.playerID = playerID;
			this.name = name;
			this.head = head;
			this.body = body;
			this.leg = leg;
			this.powpoint = ppoint;
		}

		// Token: 0x04002136 RID: 8502
		public int playerID;

		// Token: 0x04002137 RID: 8503
		public string name;

		// Token: 0x04002138 RID: 8504
		public short head;

		// Token: 0x04002139 RID: 8505
		public short body;

		// Token: 0x0400213A RID: 8506
		public short leg;

		// Token: 0x0400213B RID: 8507
		public long powpoint;
	}
}
