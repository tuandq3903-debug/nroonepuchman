using System;
using System.Collections.Generic;

namespace Game1
{
	// Token: 0x0200044A RID: 1098
	public class AutoLogin
	{
		// Token: 0x06003079 RID: 12409 RVA: 0x002EC35E File Offset: 0x002EA55E
		public AutoLogin()
		{
			this.lastTimeWait = mSystem.currentTimeMillis();
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x002EC37C File Offset: 0x002EA57C
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

		// Token: 0x04005CD3 RID: 23763
		public bool waitToNextLogin;

		// Token: 0x04005CD4 RID: 23764
		public long lastTimeWait;

		// Token: 0x04005CD5 RID: 23765
		public bool hasSetUserPass;

		// Token: 0x04005CD6 RID: 23766
		public string accAutoLogin = "";
	}
}
