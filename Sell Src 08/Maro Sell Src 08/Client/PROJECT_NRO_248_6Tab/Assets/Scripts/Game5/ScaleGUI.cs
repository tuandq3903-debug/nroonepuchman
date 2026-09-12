using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game5
{
	// Token: 0x02000171 RID: 369
	public class ScaleGUI
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x00112E6C File Offset: 0x0011106C
		public static void initScaleGUI()
		{
			Cout.println("Init Scale GUI: Screen.w=" + Screen.width.ToString() + " Screen.h=" + Screen.height.ToString());
			ScaleGUI.WIDTH = (float)Screen.width;
			ScaleGUI.HEIGHT = (float)Screen.height;
			ScaleGUI.scaleScreen = false;
			int width = Screen.width;
		}

		// Token: 0x040021DB RID: 8667
		public static bool scaleScreen;

		// Token: 0x040021DC RID: 8668
		public static float WIDTH;

		// Token: 0x040021DD RID: 8669
		public static float HEIGHT;

		// Token: 0x040021DE RID: 8670
		private static List<Matrix4x4> stack = new List<Matrix4x4>();
	}
}
