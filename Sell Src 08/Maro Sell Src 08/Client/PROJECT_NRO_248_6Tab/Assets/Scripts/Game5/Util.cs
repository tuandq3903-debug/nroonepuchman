using System;

namespace Game5
{
	// Token: 0x0200019C RID: 412
	public static class Util
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x001236AA File Offset: 0x001218AA
		public static bool CanDoWithTime(long lastTime, long waitTime)
		{
			return mSystem.currentTimeMillis() - lastTime > waitTime;
		}
	}
}
