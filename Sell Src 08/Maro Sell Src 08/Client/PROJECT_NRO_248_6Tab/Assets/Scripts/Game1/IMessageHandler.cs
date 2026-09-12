using System;

namespace Game1
{
	// Token: 0x0200047F RID: 1151
	public interface IMessageHandler
	{
		// Token: 0x060033A5 RID: 13221
		void onMessage(Message message);

		// Token: 0x060033A6 RID: 13222
		void onConnectionFail(bool isMain);

		// Token: 0x060033A7 RID: 13223
		void onDisconnected(bool isMain);

		// Token: 0x060033A8 RID: 13224
		void onConnectOK(bool isMain);
	}
}
