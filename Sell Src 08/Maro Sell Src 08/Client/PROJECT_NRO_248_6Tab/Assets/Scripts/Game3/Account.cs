using System;

namespace Game3
{
	// Token: 0x0200028D RID: 653
	public struct Account
	{
		// Token: 0x06001CF6 RID: 7414 RVA: 0x001C1574 File Offset: 0x001BF774
		public Account(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x001C1584 File Offset: 0x001BF784
		public string getUsername()
		{
			return this.username;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x001C158C File Offset: 0x001BF78C
		public string getPassword()
		{
			return this.password;
		}

		// Token: 0x0400378E RID: 14222
		private string username;

		// Token: 0x0400378F RID: 14223
		private string password;
	}
}
