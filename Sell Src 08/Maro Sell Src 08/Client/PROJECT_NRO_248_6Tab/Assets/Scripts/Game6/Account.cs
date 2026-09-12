using System;

namespace Game6
{
	// Token: 0x02000005 RID: 5
	public struct Account
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000222A File Offset: 0x0000042A
		public Account(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000223A File Offset: 0x0000043A
		public string getUsername()
		{
			return this.username;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002242 File Offset: 0x00000442
		public string getPassword()
		{
			return this.password;
		}

		// Token: 0x04000012 RID: 18
		private string username;

		// Token: 0x04000013 RID: 19
		private string password;
	}
}
