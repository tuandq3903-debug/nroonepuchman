using System;

namespace Game2
{
	// Token: 0x020003A3 RID: 931
	public interface IChatable
	{
		// Token: 0x060029E3 RID: 10723
		void onChatFromMe(string text, string to);

		// Token: 0x060029E4 RID: 10724
		void onCancelChat();
	}
}
