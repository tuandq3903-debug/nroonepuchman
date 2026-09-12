using System;
using System.Collections.Generic;

namespace Game6
{
	// Token: 0x02000012 RID: 18
	public class AutoLogin
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002EDE File Offset: 0x000010DE
		public AutoLogin()
		{
			this.lastTimeWait = mSystem.currentTimeMillis();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002EFC File Offset: 0x000010FC
		public Account GetAccWithUsername(List<Account> accounts)
		{
			foreach (Account acc in accounts)
			{
				if (acc.getUsername().Equals(this.accAutoLogin))
				{
					return acc;
				}
			}
			return new Account("", "");
		}

		// Token: 0x04000059 RID: 89
		public bool waitToNextLogin;

		// Token: 0x0400005A RID: 90
		public long lastTimeWait;

		// Token: 0x0400005B RID: 91
		public bool hasSetUserPass;

		// Token: 0x0400005C RID: 92
		public string accAutoLogin = "";
	}
}
