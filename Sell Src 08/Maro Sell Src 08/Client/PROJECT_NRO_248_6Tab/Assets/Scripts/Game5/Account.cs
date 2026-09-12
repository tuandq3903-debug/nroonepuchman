using System;

namespace Game5
{
	// Token: 0x020000DD RID: 221
	public struct Account
	{
		// Token: 0x060009AE RID: 2478 RVA: 0x0009742C File Offset: 0x0009562C
		public Account(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0009743C File Offset: 0x0009563C
		public string getUsername()
		{
			return this.username;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00097444 File Offset: 0x00095644
		public string getPassword()
		{
			return this.password;
		}

		// Token: 0x04001290 RID: 4752
		private string username;

		// Token: 0x04001291 RID: 4753
		private string password;
	}
}
