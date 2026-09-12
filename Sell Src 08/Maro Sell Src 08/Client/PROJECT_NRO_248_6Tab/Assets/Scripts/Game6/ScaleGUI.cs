using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000099 RID: 153
	public class ScaleGUI
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x0007DD58 File Offset: 0x0007BF58
		public static void initScaleGUI()
		{
			Cout.println("Init Scale GUI: Screen.w=" + Screen.width.ToString() + " Screen.h=" + Screen.height.ToString());
			ScaleGUI.WIDTH = (float)Screen.width;
			ScaleGUI.HEIGHT = (float)Screen.height;
			ScaleGUI.scaleScreen = false;
			int width = Screen.width;
		}

		// Token: 0x04000F5C RID: 3932
		public static bool scaleScreen;

		// Token: 0x04000F5D RID: 3933
		public static float WIDTH;

		// Token: 0x04000F5E RID: 3934
		public static float HEIGHT;

		// Token: 0x04000F5F RID: 3935
		private static List<Matrix4x4> stack = new List<Matrix4x4>();
	}
}
