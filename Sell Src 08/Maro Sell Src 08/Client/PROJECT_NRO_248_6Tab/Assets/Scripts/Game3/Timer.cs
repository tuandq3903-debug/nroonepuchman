using System;

namespace Game3
{
	// Token: 0x02000349 RID: 841
	public class Timer
	{
		// Token: 0x060025BE RID: 9662 RVA: 0x0024D2A4 File Offset: 0x0024B4A4
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

		// Token: 0x04004908 RID: 18696
		public static int idAction;

		// Token: 0x04004909 RID: 18697
		public static long timeExecute;

		// Token: 0x0400490A RID: 18698
		public static bool isON;
	}
}
