using System;

namespace Game1
{
	// Token: 0x0200047E RID: 1150
	public interface IMapObject
	{
		// Token: 0x0600339F RID: 13215
		int getX();

		// Token: 0x060033A0 RID: 13216
		int getY();

		// Token: 0x060033A1 RID: 13217
		int getW();

		// Token: 0x060033A2 RID: 13218
		int getH();

		// Token: 0x060033A3 RID: 13219
		void stopMoving();

		// Token: 0x060033A4 RID: 13220
		bool isInvisible();
	}
}
