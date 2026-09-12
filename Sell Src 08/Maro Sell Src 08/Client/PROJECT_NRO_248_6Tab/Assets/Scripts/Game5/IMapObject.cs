using System;

namespace Game5
{
	// Token: 0x0200011E RID: 286
	public interface IMapObject
	{
		// Token: 0x06000D0F RID: 3343
		int getX();

		// Token: 0x06000D10 RID: 3344
		int getY();

		// Token: 0x06000D11 RID: 3345
		int getW();

		// Token: 0x06000D12 RID: 3346
		int getH();

		// Token: 0x06000D13 RID: 3347
		void stopMoving();

		// Token: 0x06000D14 RID: 3348
		bool isInvisible();
	}
}
