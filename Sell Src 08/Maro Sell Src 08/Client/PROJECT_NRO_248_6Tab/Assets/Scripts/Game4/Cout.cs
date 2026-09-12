using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x020001D5 RID: 469
	public class Cout
	{
		// Token: 0x060014E6 RID: 5350 RVA: 0x001516B4 File Offset: 0x0014F8B4
		public static void println(string s)
		{
			if (mSystem.isTest)
			{
				Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
				Cout.count++;
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x001516E9 File Offset: 0x0014F8E9
		public static void Log(string str)
		{
			if (mSystem.isTest)
			{
				Debug.Log(str);
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x001516F8 File Offset: 0x0014F8F8
		public static void LogError(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00151707 File Offset: 0x0014F907
		public static void LogError2(string str)
		{
			bool isTest = mSystem.isTest;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x001516F8 File Offset: 0x0014F8F8
		public static void LogError3(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0015170F File Offset: 0x0014F90F
		public static void LogWarning(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogWarning(str);
			}
		}

		// Token: 0x04002842 RID: 10306
		public static int count;
	}
}
