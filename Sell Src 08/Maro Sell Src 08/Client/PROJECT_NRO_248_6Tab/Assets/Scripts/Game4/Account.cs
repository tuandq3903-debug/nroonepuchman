using System;

namespace Game4
{
	// Token: 0x020001B5 RID: 437
	public struct Account
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x0012C4D0 File Offset: 0x0012A6D0
		public Account(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0012C4E0 File Offset: 0x0012A6E0
		public string getUsername()
		{
			return this.username;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0012C4E8 File Offset: 0x0012A6E8
		public string getPassword()
		{
			return this.password;
		}

		// Token: 0x0400250F RID: 9487
		private string username;

		// Token: 0x04002510 RID: 9488
		private string password;
	}
}
