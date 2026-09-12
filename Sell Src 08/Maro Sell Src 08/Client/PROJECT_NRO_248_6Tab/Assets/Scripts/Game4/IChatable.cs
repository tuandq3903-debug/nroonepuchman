using System;

namespace Game4
{
	// Token: 0x020001F3 RID: 499
	public interface IChatable
	{
		// Token: 0x0600169B RID: 5787
		void onChatFromMe(string text, string to);

		// Token: 0x0600169C RID: 5788
		void onCancelChat();
	}
}
