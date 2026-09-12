using System;

namespace Game1
{
	// Token: 0x0200049F RID: 1183
	public class Member
	{
		// Token: 0x06003444 RID: 13380 RVA: 0x003336E0 File Offset: 0x003318E0
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

		// Token: 0x0400655A RID: 25946
		public int ID;

		// Token: 0x0400655B RID: 25947
		public short head;

		// Token: 0x0400655C RID: 25948
		public short headICON = -1;

		// Token: 0x0400655D RID: 25949
		public short leg;

		// Token: 0x0400655E RID: 25950
		public short body;

		// Token: 0x0400655F RID: 25951
		public string name;

		// Token: 0x04006560 RID: 25952
		public sbyte role;

		// Token: 0x04006561 RID: 25953
		public string powerPoint;

		// Token: 0x04006562 RID: 25954
		public int donate;

		// Token: 0x04006563 RID: 25955
		public int receive_donate;

		// Token: 0x04006564 RID: 25956
		public int curClanPoint;

		// Token: 0x04006565 RID: 25957
		public int clanPoint;

		// Token: 0x04006566 RID: 25958
		public int lastRequest;

		// Token: 0x04006567 RID: 25959
		public string joinTime;
	}
}
