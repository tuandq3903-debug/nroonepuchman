using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x020000FD RID: 253
	public class Cout
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x000BC610 File Offset: 0x000BA810
		public static void println(string s)
		{
			if (mSystem.isTest)
			{
				Debug.Log(((Cout.count % 2 != 0) ? "***--- " : ">>>--- ") + s);
				Cout.count++;
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000BC645 File Offset: 0x000BA845
		public static void Log(string str)
		{
			if (mSystem.isTest)
			{
				Debug.Log(str);
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000BC654 File Offset: 0x000BA854
		public static void LogError(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000BC663 File Offset: 0x000BA863
		public static void LogError2(string str)
		{
			bool isTest = mSystem.isTest;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000BC654 File Offset: 0x000BA854
		public static void LogError3(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000BC66B File Offset: 0x000BA86B
		public static void LogWarning(string str)
		{
			if (mSystem.isTest)
			{
				Debug.LogWarning(str);
			}
		}

		// Token: 0x040015C3 RID: 5571
		public static int count;
	}
}
