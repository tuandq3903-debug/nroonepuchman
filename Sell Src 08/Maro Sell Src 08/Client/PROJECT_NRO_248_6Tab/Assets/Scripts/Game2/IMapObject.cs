using System;

namespace Game2
{
	// Token: 0x020003A6 RID: 934
	public interface IMapObject
	{
		// Token: 0x060029FB RID: 10747
		int getX();

		// Token: 0x060029FC RID: 10748
		int getY();

		// Token: 0x060029FD RID: 10749
		int getW();

		// Token: 0x060029FE RID: 10750
		int getH();

		// Token: 0x060029FF RID: 10751
		void stopMoving();

		// Token: 0x06002A00 RID: 10752
		bool isInvisible();
	}
}
