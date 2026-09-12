using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game3
{
	// Token: 0x02000321 RID: 801
	public class ScaleGUI
	{
		// Token: 0x06002407 RID: 9223 RVA: 0x0023CFB4 File Offset: 0x0023B1B4
		public static void initScaleGUI()
		{
			Cout.println("Init Scale GUI: Screen.w=" + Screen.width.ToString() + " Screen.h=" + Screen.height.ToString());
			ScaleGUI.WIDTH = (float)Screen.width;
			ScaleGUI.HEIGHT = (float)Screen.height;
			ScaleGUI.scaleScreen = false;
			int width = Screen.width;
		}

		// Token: 0x040046D9 RID: 18137
		public static bool scaleScreen;

		// Token: 0x040046DA RID: 18138
		public static float WIDTH;

		// Token: 0x040046DB RID: 18139
		public static float HEIGHT;

		// Token: 0x040046DC RID: 18140
		private static List<Matrix4x4> stack = new List<Matrix4x4>();
	}
}
