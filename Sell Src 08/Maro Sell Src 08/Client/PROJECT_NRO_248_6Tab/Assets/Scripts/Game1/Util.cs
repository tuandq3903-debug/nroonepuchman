using System;

namespace Game1
{
	// Token: 0x020004FC RID: 1276
	public static class Util
	{
		// Token: 0x0600390F RID: 14607 RVA: 0x0037793A File Offset: 0x00375B3A
		public static bool CanDoWithTime(long lastTime, long waitTime)
		{
			return mSystem.currentTimeMillis() - lastTime > waitTime;
		}
	}
}
