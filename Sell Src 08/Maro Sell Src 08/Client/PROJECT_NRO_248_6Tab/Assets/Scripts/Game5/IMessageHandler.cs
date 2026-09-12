using System;

namespace Game5
{
	// Token: 0x0200011F RID: 287
	public interface IMessageHandler
	{
		// Token: 0x06000D15 RID: 3349
		void onMessage(Message message);

		// Token: 0x06000D16 RID: 3350
		void onConnectionFail(bool isMain);

		// Token: 0x06000D17 RID: 3351
		void onDisconnected(bool isMain);

		// Token: 0x06000D18 RID: 3352
		void onConnectOK(bool isMain);
	}
}
