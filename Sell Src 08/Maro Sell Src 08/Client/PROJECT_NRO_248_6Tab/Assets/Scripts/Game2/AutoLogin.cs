using System;
using System.Collections.Generic;

namespace Game2
{
	// Token: 0x02000372 RID: 882
	public class AutoLogin
	{
		// Token: 0x060026D5 RID: 9941 RVA: 0x002572BA File Offset: 0x002554BA
		public AutoLogin()
		{
			this.lastTimeWait = mSystem.currentTimeMillis();
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x002572D8 File Offset: 0x002554D8
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

		// Token: 0x04004A54 RID: 19028
		public bool waitToNextLogin;

		// Token: 0x04004A55 RID: 19029
		public long lastTimeWait;

		// Token: 0x04004A56 RID: 19030
		public bool hasSetUserPass;

		// Token: 0x04004A57 RID: 19031
		public string accAutoLogin = "";
	}
}
