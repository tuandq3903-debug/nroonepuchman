using System;
using System.Collections.Generic;

namespace Game3
{
	// Token: 0x0200029A RID: 666
	public class AutoLogin
	{
		// Token: 0x06001D31 RID: 7473 RVA: 0x001C2216 File Offset: 0x001C0416
		public AutoLogin()
		{
			this.lastTimeWait = mSystem.currentTimeMillis();
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x001C2234 File Offset: 0x001C0434
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

		// Token: 0x040037D5 RID: 14293
		public bool waitToNextLogin;

		// Token: 0x040037D6 RID: 14294
		public long lastTimeWait;

		// Token: 0x040037D7 RID: 14295
		public bool hasSetUserPass;

		// Token: 0x040037D8 RID: 14296
		public string accAutoLogin = "";
	}
}
