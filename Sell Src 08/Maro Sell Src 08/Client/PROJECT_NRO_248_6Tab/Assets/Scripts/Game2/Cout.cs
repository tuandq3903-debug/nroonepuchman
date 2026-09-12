using System;
using UnityEngine;

namespace Game2
{
	// Token: 0x02000385 RID: 901
	public class Cout
	{
		// Token: 0x0600282E RID: 10286 RVA: 0x0027B7FC File Offset: 0x002799FC
		public static void println(string s)
		{
			if (mSystem.isTest)
			{
				Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
				Cout.count++;
			}
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0027B831 File Offset: 0x00279A31
		public static void Log(string str)
		{
			if (mSystem.isTest)
			{
				Debug.Log(str);
			}
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0027B840 File Offset: 0x00279A40
		public static void LogError(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x0027B84F File Offset: 0x00279A4F
		public static void LogError2(string str)
		{
			bool isTest = mSystem.isTest;
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0027B840 File Offset: 0x00279A40
		public static void LogError3(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0027B857 File Offset: 0x00279A57
		public static void LogWarning(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogWarning(str);
			}
		}

		// Token: 0x04004D40 RID: 19776
		public static int count;
	}
}
