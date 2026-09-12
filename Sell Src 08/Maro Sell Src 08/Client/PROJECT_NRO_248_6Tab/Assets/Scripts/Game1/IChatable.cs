using System;

namespace Game1
{
	// Token: 0x0200047B RID: 1147
	public interface IChatable
	{
		// Token: 0x06003387 RID: 13191
		void onChatFromMe(string text, string to);

		// Token: 0x06003388 RID: 13192
		void onCancelChat();
	}
}
