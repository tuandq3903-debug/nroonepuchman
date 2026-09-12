using System;

namespace Game4
{
	// Token: 0x020001F7 RID: 503
	public interface IMessageHandler
	{
		// Token: 0x060016B9 RID: 5817
		void onMessage(Message message);

		// Token: 0x060016BA RID: 5818
		void onConnectionFail(bool isMain);

		// Token: 0x060016BB RID: 5819
		void onDisconnected(bool isMain);

		// Token: 0x060016BC RID: 5820
		void onConnectOK(bool isMain);
	}
}
