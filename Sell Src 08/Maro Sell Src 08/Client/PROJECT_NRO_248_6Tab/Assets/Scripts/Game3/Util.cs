using System;

namespace Game3
{
	// Token: 0x0200034C RID: 844
	public static class Util
	{
		// Token: 0x060025C7 RID: 9671 RVA: 0x0024D7F2 File Offset: 0x0024B9F2
		public static bool CanDoWithTime(long lastTime, long waitTime)
		{
			return mSystem.currentTimeMillis() - lastTime > waitTime;
		}
	}
}
