using System;
using System.Collections.Generic;

namespace Game5
{
	// Token: 0x020000EA RID: 234
	public class AutoLogin
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x000980CE File Offset: 0x000962CE
		public AutoLogin()
		{
			this.lastTimeWait = mSystem.currentTimeMillis();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000980EC File Offset: 0x000962EC
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

		// Token: 0x040012D7 RID: 4823
		public bool waitToNextLogin;

		// Token: 0x040012D8 RID: 4824
		public long lastTimeWait;

		// Token: 0x040012D9 RID: 4825
		public bool hasSetUserPass;

		// Token: 0x040012DA RID: 4826
		public string accAutoLogin = "";
	}
}
