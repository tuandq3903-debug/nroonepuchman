using System;

namespace Game3
{
	// Token: 0x020002CE RID: 718
	public interface IMapObject
	{
		// Token: 0x06002057 RID: 8279
		int getX();

		// Token: 0x06002058 RID: 8280
		int getY();

		// Token: 0x06002059 RID: 8281
		int getW();

		// Token: 0x0600205A RID: 8282
		int getH();

		// Token: 0x0600205B RID: 8283
		void stopMoving();

		// Token: 0x0600205C RID: 8284
		bool isInvisible();
	}
}
