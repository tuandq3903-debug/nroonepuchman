using System;

namespace Game4
{
	// Token: 0x02000217 RID: 535
	public class Member
	{
		// Token: 0x06001758 RID: 5976 RVA: 0x001744F4 File Offset: 0x001726F4
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

		// Token: 0x04002DDD RID: 11741
		public int ID;

		// Token: 0x04002DDE RID: 11742
		public short head;

		// Token: 0x04002DDF RID: 11743
		public short headICON = -1;

		// Token: 0x04002DE0 RID: 11744
		public short leg;

		// Token: 0x04002DE1 RID: 11745
		public short body;

		// Token: 0x04002DE2 RID: 11746
		public string name;

		// Token: 0x04002DE3 RID: 11747
		public sbyte role;

		// Token: 0x04002DE4 RID: 11748
		public string powerPoint;

		// Token: 0x04002DE5 RID: 11749
		public int donate;

		// Token: 0x04002DE6 RID: 11750
		public int receive_donate;

		// Token: 0x04002DE7 RID: 11751
		public int curClanPoint;

		// Token: 0x04002DE8 RID: 11752
		public int clanPoint;

		// Token: 0x04002DE9 RID: 11753
		public int lastRequest;

		// Token: 0x04002DEA RID: 11754
		public string joinTime;
	}
}
