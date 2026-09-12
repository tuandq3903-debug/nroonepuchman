using System;

namespace Game6
{
	// Token: 0x02000046 RID: 70
	public interface IMapObject
	{
		// Token: 0x0600036B RID: 875
		int getX();

		// Token: 0x0600036C RID: 876
		int getY();

		// Token: 0x0600036D RID: 877
		int getW();

		// Token: 0x0600036E RID: 878
		int getH();

		// Token: 0x0600036F RID: 879
		void stopMoving();

		// Token: 0x06000370 RID: 880
		bool isInvisible();
	}
}
