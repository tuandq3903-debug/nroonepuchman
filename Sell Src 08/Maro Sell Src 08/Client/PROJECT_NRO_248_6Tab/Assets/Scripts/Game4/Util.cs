using System;

namespace Game4
{
	// Token: 0x02000274 RID: 628
	public static class Util
	{
		// Token: 0x06001C23 RID: 7203 RVA: 0x001B874E File Offset: 0x001B694E
		public static bool CanDoWithTime(long lastTime, long waitTime)
		{
			return mSystem.currentTimeMillis() - lastTime > waitTime;
		}
	}
}
