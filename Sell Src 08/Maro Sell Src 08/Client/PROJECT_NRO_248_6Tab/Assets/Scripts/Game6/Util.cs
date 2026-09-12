using System;

namespace Game6
{
	// Token: 0x020000C4 RID: 196
	public static class Util
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x0008E592 File Offset: 0x0008C792
		public static bool CanDoWithTime(long lastTime, long waitTime)
		{
			return mSystem.currentTimeMillis() - lastTime > waitTime;
		}
	}
}
