using System;

namespace Game2
{
	// Token: 0x020003A7 RID: 935
	public interface IMessageHandler
	{
		// Token: 0x06002A01 RID: 10753
		void onMessage(Message message);

		// Token: 0x06002A02 RID: 10754
		void onConnectionFail(bool isMain);

		// Token: 0x06002A03 RID: 10755
		void onDisconnected(bool isMain);

		// Token: 0x06002A04 RID: 10756
		void onConnectOK(bool isMain);
	}
}
