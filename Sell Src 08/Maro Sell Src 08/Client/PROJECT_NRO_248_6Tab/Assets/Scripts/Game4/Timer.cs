using System;

namespace Game4
{
	// Token: 0x02000271 RID: 625
	public class Timer
	{
		// Token: 0x06001C1A RID: 7194 RVA: 0x001B8200 File Offset: 0x001B6400
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

		// Token: 0x04003689 RID: 13961
		public static int idAction;

		// Token: 0x0400368A RID: 13962
		public static long timeExecute;

		// Token: 0x0400368B RID: 13963
		public static bool isON;
	}
}
