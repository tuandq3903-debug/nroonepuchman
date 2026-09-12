using System;

namespace Game6
{
	// Token: 0x02000043 RID: 67
	public interface IChatable
	{
		// Token: 0x06000353 RID: 851
		void onChatFromMe(string text, string to);

		// Token: 0x06000354 RID: 852
		void onCancelChat();
	}
}
