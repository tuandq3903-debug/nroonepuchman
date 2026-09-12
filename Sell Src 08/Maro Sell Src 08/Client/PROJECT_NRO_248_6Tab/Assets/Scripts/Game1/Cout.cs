using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x0200045D RID: 1117
	public class Cout
	{
		// Token: 0x060031D2 RID: 12754 RVA: 0x003108A0 File Offset: 0x0030EAA0
		public static void println(string s)
		{
			if (mSystem.isTest)
			{
				Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
				Cout.count++;
			}
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x003108D5 File Offset: 0x0030EAD5
		public static void Log(string str)
		{
			if (mSystem.isTest)
			{
				Debug.Log(str);
			}
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x003108E4 File Offset: 0x0030EAE4
		public static void LogError(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x003108F3 File Offset: 0x0030EAF3
		public static void LogError2(string str)
		{
			bool isTest = mSystem.isTest;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x003108E4 File Offset: 0x0030EAE4
		public static void LogError3(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x003108FB File Offset: 0x0030EAFB
		public static void LogWarning(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogWarning(str);
			}
		}

		// Token: 0x04005FBF RID: 24511
		public static int count;
	}
}
