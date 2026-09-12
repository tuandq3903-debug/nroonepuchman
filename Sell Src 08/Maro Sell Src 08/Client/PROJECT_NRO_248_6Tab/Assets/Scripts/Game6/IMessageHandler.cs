using System;

namespace Game6
{
	// Token: 0x02000047 RID: 71
	public interface IMessageHandler
	{
		// Token: 0x06000371 RID: 881
		void onMessage(Message message);

		// Token: 0x06000372 RID: 882
		void onConnectionFail(bool isMain);

		// Token: 0x06000373 RID: 883
		void onDisconnected(bool isMain);

		// Token: 0x06000374 RID: 884
		void onConnectOK(bool isMain);
	}
}
