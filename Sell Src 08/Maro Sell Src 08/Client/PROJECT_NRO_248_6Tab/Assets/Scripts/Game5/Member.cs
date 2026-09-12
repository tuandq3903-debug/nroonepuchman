using System;

namespace Game5
{
	// Token: 0x0200013F RID: 319
	public class Member
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x000DF450 File Offset: 0x000DD650
		public static string getRole(int r)
		{
			string result;
			switch (r)
			{
			case 0:
				result = mResources.clan_leader;
				break;
			case 1:
				result = mResources.clan_coleader;
				break;
			case 2:
				result = mResources.member;
				break;
			default:
				result = string.Empty;
				break;
			}
			return result;
		}

		// Token: 0x04001B5E RID: 7006
		public int ID;

		// Token: 0x04001B5F RID: 7007
		public short head;

		// Token: 0x04001B60 RID: 7008
		public short headICON = -1;

		// Token: 0x04001B61 RID: 7009
		public short leg;

		// Token: 0x04001B62 RID: 7010
		public short body;

		// Token: 0x04001B63 RID: 7011
		public string name;

		// Token: 0x04001B64 RID: 7012
		public sbyte role;

		// Token: 0x04001B65 RID: 7013
		public string powerPoint;

		// Token: 0x04001B66 RID: 7014
		public int donate;

		// Token: 0x04001B67 RID: 7015
		public int receive_donate;

		// Token: 0x04001B68 RID: 7016
		public int curClanPoint;

		// Token: 0x04001B69 RID: 7017
		public int clanPoint;

		// Token: 0x04001B6A RID: 7018
		public int lastRequest;

		// Token: 0x04001B6B RID: 7019
		public string joinTime;
	}
}
