using System;

namespace Game2
{
	// Token: 0x02000424 RID: 1060
	public static class Util
	{
		// Token: 0x06002F6B RID: 12139 RVA: 0x002E2896 File Offset: 0x002E0A96
		public static bool CanDoWithTime(long lastTime, long waitTime)
		{
			return mSystem.currentTimeMillis() - lastTime > waitTime;
		}
	}
}
