using System;

namespace Game3
{
	// Token: 0x020002CF RID: 719
	public interface IMessageHandler
	{
		// Token: 0x0600205D RID: 8285
		void onMessage(Message message);

		// Token: 0x0600205E RID: 8286
		void onConnectionFail(bool isMain);

		// Token: 0x0600205F RID: 8287
		void onDisconnected(bool isMain);

		// Token: 0x06002060 RID: 8288
		void onConnectOK(bool isMain);
	}
}
