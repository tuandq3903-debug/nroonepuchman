using System;

namespace Game4
{
	// Token: 0x020001F6 RID: 502
	public interface IMapObject
	{
		// Token: 0x060016B3 RID: 5811
		int getX();

		// Token: 0x060016B4 RID: 5812
		int getY();

		// Token: 0x060016B5 RID: 5813
		int getW();

		// Token: 0x060016B6 RID: 5814
		int getH();

		// Token: 0x060016B7 RID: 5815
		void stopMoving();

		// Token: 0x060016B8 RID: 5816
		bool isInvisible();
	}
}
