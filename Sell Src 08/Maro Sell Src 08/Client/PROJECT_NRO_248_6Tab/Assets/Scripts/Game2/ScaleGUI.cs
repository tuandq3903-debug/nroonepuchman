using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game2
{
	// Token: 0x020003F9 RID: 1017
	public class ScaleGUI
	{
		// Token: 0x06002DAB RID: 11691 RVA: 0x002D2058 File Offset: 0x002D0258
		public static void initScaleGUI()
		{
			Cout.println("Init Scale GUI: Screen.w=" + Screen.width.ToString() + " Screen.h=" + Screen.height.ToString());
			ScaleGUI.WIDTH = (float)Screen.width;
			ScaleGUI.HEIGHT = (float)Screen.height;
			ScaleGUI.scaleScreen = false;
			int width = Screen.width;
		}

		// Token: 0x04005958 RID: 22872
		public static bool scaleScreen;

		// Token: 0x04005959 RID: 22873
		public static float WIDTH;

		// Token: 0x0400595A RID: 22874
		public static float HEIGHT;

		// Token: 0x0400595B RID: 22875
		private static List<Matrix4x4> stack = new List<Matrix4x4>();
	}
}
