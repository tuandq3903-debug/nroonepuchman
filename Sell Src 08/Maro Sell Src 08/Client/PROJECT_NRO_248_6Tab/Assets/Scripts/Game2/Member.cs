using System;

namespace Game2
{
	// Token: 0x020003C7 RID: 967
	public class Member
	{
		// Token: 0x06002AA0 RID: 10912 RVA: 0x0029E63C File Offset: 0x0029C83C
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

		// Token: 0x040052DB RID: 21211
		public int ID;

		// Token: 0x040052DC RID: 21212
		public short head;

		// Token: 0x040052DD RID: 21213
		public short headICON = -1;

		// Token: 0x040052DE RID: 21214
		public short leg;

		// Token: 0x040052DF RID: 21215
		public short body;

		// Token: 0x040052E0 RID: 21216
		public string name;

		// Token: 0x040052E1 RID: 21217
		public sbyte role;

		// Token: 0x040052E2 RID: 21218
		public string powerPoint;

		// Token: 0x040052E3 RID: 21219
		public int donate;

		// Token: 0x040052E4 RID: 21220
		public int receive_donate;

		// Token: 0x040052E5 RID: 21221
		public int curClanPoint;

		// Token: 0x040052E6 RID: 21222
		public int clanPoint;

		// Token: 0x040052E7 RID: 21223
		public int lastRequest;

		// Token: 0x040052E8 RID: 21224
		public string joinTime;
	}
}
