using System;
using System.Collections.Generic;

namespace Game4
{
	// Token: 0x020001C2 RID: 450
	public class AutoLogin
	{
		// Token: 0x0600138D RID: 5005 RVA: 0x0012D172 File Offset: 0x0012B372
		public AutoLogin()
		{
			this.lastTimeWait = mSystem.currentTimeMillis();
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0012D190 File Offset: 0x0012B390
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

		// Token: 0x04002556 RID: 9558
		public bool waitToNextLogin;

		// Token: 0x04002557 RID: 9559
		public long lastTimeWait;

		// Token: 0x04002558 RID: 9560
		public bool hasSetUserPass;

		// Token: 0x04002559 RID: 9561
		public string accAutoLogin = "";
	}
}
