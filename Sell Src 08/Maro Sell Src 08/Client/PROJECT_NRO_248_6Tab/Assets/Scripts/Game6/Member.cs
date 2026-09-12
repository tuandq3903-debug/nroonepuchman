using System;

namespace Game6
{
	// Token: 0x02000067 RID: 103
	public class Member
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0004A2D4 File Offset: 0x000484D4
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

		// Token: 0x040008DF RID: 2271
		public int ID;

		// Token: 0x040008E0 RID: 2272
		public short head;

		// Token: 0x040008E1 RID: 2273
		public short headICON = -1;

		// Token: 0x040008E2 RID: 2274
		public short leg;

		// Token: 0x040008E3 RID: 2275
		public short body;

		// Token: 0x040008E4 RID: 2276
		public string name;

		// Token: 0x040008E5 RID: 2277
		public sbyte role;

		// Token: 0x040008E6 RID: 2278
		public string powerPoint;

		// Token: 0x040008E7 RID: 2279
		public int donate;

		// Token: 0x040008E8 RID: 2280
		public int receive_donate;

		// Token: 0x040008E9 RID: 2281
		public int curClanPoint;

		// Token: 0x040008EA RID: 2282
		public int clanPoint;

		// Token: 0x040008EB RID: 2283
		public int lastRequest;

		// Token: 0x040008EC RID: 2284
		public string joinTime;
	}
}
