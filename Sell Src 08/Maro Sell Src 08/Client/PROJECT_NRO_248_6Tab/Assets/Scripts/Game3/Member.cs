using System;

namespace Game3
{
	// Token: 0x020002EF RID: 751
	public class Member
	{
		// Token: 0x060020FC RID: 8444 RVA: 0x00209598 File Offset: 0x00207798
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

		// Token: 0x0400405C RID: 16476
		public int ID;

		// Token: 0x0400405D RID: 16477
		public short head;

		// Token: 0x0400405E RID: 16478
		public short headICON = -1;

		// Token: 0x0400405F RID: 16479
		public short leg;

		// Token: 0x04004060 RID: 16480
		public short body;

		// Token: 0x04004061 RID: 16481
		public string name;

		// Token: 0x04004062 RID: 16482
		public sbyte role;

		// Token: 0x04004063 RID: 16483
		public string powerPoint;

		// Token: 0x04004064 RID: 16484
		public int donate;

		// Token: 0x04004065 RID: 16485
		public int receive_donate;

		// Token: 0x04004066 RID: 16486
		public int curClanPoint;

		// Token: 0x04004067 RID: 16487
		public int clanPoint;

		// Token: 0x04004068 RID: 16488
		public int lastRequest;

		// Token: 0x04004069 RID: 16489
		public string joinTime;
	}
}
