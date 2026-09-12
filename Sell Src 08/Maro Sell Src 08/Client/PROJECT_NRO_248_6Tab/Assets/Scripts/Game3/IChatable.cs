using System;

namespace Game3
{
	// Token: 0x020002CB RID: 715
	public interface IChatable
	{
		// Token: 0x0600203F RID: 8255
		void onChatFromMe(string text, string to);

		// Token: 0x06002040 RID: 8256
		void onCancelChat();
	}
}
