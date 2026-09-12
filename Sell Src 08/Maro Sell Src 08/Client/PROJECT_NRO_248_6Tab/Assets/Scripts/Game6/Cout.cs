using System;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000025 RID: 37
	public class Cout
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0002744E File Offset: 0x0002564E
		public static void println(string s)
		{
			if (mSystem.isTest)
			{
				Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
				Cout.count++;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00027483 File Offset: 0x00025683
		public static void Log(string str)
		{
			if (mSystem.isTest)
			{
				Debug.Log(str);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00027492 File Offset: 0x00025692
		public static void LogError(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000274A1 File Offset: 0x000256A1
		public static void LogError2(string str)
		{
			bool isTest = mSystem.isTest;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00027492 File Offset: 0x00025692
		public static void LogError3(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000274A9 File Offset: 0x000256A9
		public static void LogWarning(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogWarning(str);
			}
		}

		// Token: 0x04000345 RID: 837
		public static int count;
	}
}
