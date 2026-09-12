using System;

namespace Game6
{
	// Token: 0x020000C1 RID: 193
	public class Timer
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x0008E044 File Offset: 0x0008C244
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

		// Token: 0x0400118B RID: 4491
		public static int idAction;

		// Token: 0x0400118C RID: 4492
		public static long timeExecute;

		// Token: 0x0400118D RID: 4493
		public static bool isON;
	}
}
