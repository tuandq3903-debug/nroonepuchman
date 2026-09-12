using System;

namespace Game5
{
	// Token: 0x02000199 RID: 409
	public class Timer
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x0012315C File Offset: 0x0012135C
		public static void update()
		{
			long num = mSystem.currentTimeMillis();
			if (!Timer.isON || num <= Timer.timeExecute)
			{
				return;
			}
			Timer.isON = false;
			try
			{
				if (Timer.idAction > 0)
				{
					GameScr.gI().actionPerform(Timer.idAction, null);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0400240A RID: 9226
		public static int idAction;

		// Token: 0x0400240B RID: 9227
		public static long timeExecute;

		// Token: 0x0400240C RID: 9228
		public static bool isON;
	}
}
