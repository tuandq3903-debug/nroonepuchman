using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game4
{
	// Token: 0x02000249 RID: 585
	public class ScaleGUI
	{
		// Token: 0x06001A63 RID: 6755 RVA: 0x001A7F10 File Offset: 0x001A6110
		public static void initScaleGUI()
		{
			Cout.println("Init Scale GUI: Screen.w=" + Screen.width.ToString() + " Screen.h=" + Screen.height.ToString());
			ScaleGUI.WIDTH = (float)Screen.width;
			ScaleGUI.HEIGHT = (float)Screen.height;
			ScaleGUI.scaleScreen = false;
			int width = Screen.width;
		}

		// Token: 0x0400345A RID: 13402
		public static bool scaleScreen;

		// Token: 0x0400345B RID: 13403
		public static float WIDTH;

		// Token: 0x0400345C RID: 13404
		public static float HEIGHT;

		// Token: 0x0400345D RID: 13405
		private static List<Matrix4x4> stack = new List<Matrix4x4>();
	}
}
