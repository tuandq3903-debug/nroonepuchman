using System;

namespace Game1
{
	// Token: 0x020004F9 RID: 1273
	public class Timer
	{
		// Token: 0x06003906 RID: 14598 RVA: 0x003773EC File Offset: 0x003755EC
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

		// Token: 0x04006E06 RID: 28166
		public static int idAction;

		// Token: 0x04006E07 RID: 28167
		public static long timeExecute;

		// Token: 0x04006E08 RID: 28168
		public static bool isON;
	}
}
