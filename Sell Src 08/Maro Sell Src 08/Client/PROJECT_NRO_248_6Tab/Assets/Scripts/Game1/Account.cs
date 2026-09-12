using System;

namespace Game1
{
	// Token: 0x0200043D RID: 1085
	public struct Account
	{
		// Token: 0x0600303E RID: 12350 RVA: 0x002EB6BC File Offset: 0x002E98BC
		public Account(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x002EB6CC File Offset: 0x002E98CC
		public string getUsername()
		{
			return this.username;
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x002EB6D4 File Offset: 0x002E98D4
		public string getPassword()
		{
			return this.password;
		}

		// Token: 0x04005C8C RID: 23692
		private string username;

		// Token: 0x04005C8D RID: 23693
		private string password;
	}
}
