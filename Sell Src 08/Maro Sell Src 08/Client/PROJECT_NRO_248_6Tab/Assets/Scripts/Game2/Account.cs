using System;

namespace Game2
{
	// Token: 0x02000365 RID: 869
	public struct Account
	{
		// Token: 0x0600269A RID: 9882 RVA: 0x00256618 File Offset: 0x00254818
		public Account(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x00256628 File Offset: 0x00254828
		public string getUsername()
		{
			return this.username;
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x00256630 File Offset: 0x00254830
		public string getPassword()
		{
			return this.password;
		}

		// Token: 0x04004A0D RID: 18957
		private string username;

		// Token: 0x04004A0E RID: 18958
		private string password;
	}
}
