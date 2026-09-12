using System;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002AD RID: 685
	public class Cout
	{
		// Token: 0x06001E8A RID: 7818 RVA: 0x001E6758 File Offset: 0x001E4958
		public static void println(string s)
		{
			if (mSystem.isTest)
			{
				Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
				Cout.count++;
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x001E678D File Offset: 0x001E498D
		public static void Log(string str)
		{
			if (mSystem.isTest)
			{
				Debug.Log(str);
			}
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x001E679C File Offset: 0x001E499C
		public static void LogError(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x001E67AB File Offset: 0x001E49AB
		public static void LogError2(string str)
		{
			bool isTest = mSystem.isTest;
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x001E679C File Offset: 0x001E499C
		public static void LogError3(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x001E67B3 File Offset: 0x001E49B3
		public static void LogWarning(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogWarning(str);
			}
		}

		// Token: 0x04003AC1 RID: 15041
		public static int count;
	}
}
