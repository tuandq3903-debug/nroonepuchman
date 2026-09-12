using System;

namespace Game2
{
	// Token: 0x02000421 RID: 1057
	public class Timer
	{
		// Token: 0x06002F62 RID: 12130 RVA: 0x002E2348 File Offset: 0x002E0548
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

		// Token: 0x04005B87 RID: 23431
		public static int idAction;

		// Token: 0x04005B88 RID: 23432
		public static long timeExecute;

		// Token: 0x04005B89 RID: 23433
		public static bool isON;
	}
}
