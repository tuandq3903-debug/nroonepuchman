using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game1
{
	// Token: 0x020004D1 RID: 1233
	public class ScaleGUI
	{
		// Token: 0x0600374F RID: 14159 RVA: 0x003670FC File Offset: 0x003652FC
		public static void initScaleGUI()
		{
			Cout.println("Init Scale GUI: Screen.w=" + Screen.width.ToString() + " Screen.h=" + Screen.height.ToString());
			ScaleGUI.WIDTH = (float)Screen.width;
			ScaleGUI.HEIGHT = (float)Screen.height;
			ScaleGUI.scaleScreen = false;
			int width = Screen.width;
		}

		// Token: 0x04006BD7 RID: 27607
		public static bool scaleScreen;

		// Token: 0x04006BD8 RID: 27608
		public static float WIDTH;

		// Token: 0x04006BD9 RID: 27609
		public static float HEIGHT;

		// Token: 0x04006BDA RID: 27610
		private static List<Matrix4x4> stack = new List<Matrix4x4>();
	}
}
