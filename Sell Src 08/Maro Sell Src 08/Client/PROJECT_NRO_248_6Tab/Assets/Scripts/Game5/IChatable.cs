using System;

namespace Game5
{
	// Token: 0x0200011B RID: 283
	public interface IChatable
	{
		// Token: 0x06000CF7 RID: 3319
		void onChatFromMe(string text, string to);

		// Token: 0x06000CF8 RID: 3320
		void onCancelChat();
	}
}
