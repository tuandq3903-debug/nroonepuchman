using System;

namespace Game3
{
	// Token: 0x02000346 RID: 838
	public class TextInfo
	{
		// Token: 0x06002580 RID: 9600 RVA: 0x0024A64E File Offset: 0x0024884E
		public static void reset()
		{
			TextInfo.dx = 0;
			TextInfo.tx = 0;
			TextInfo.isBack = false;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x0024A664 File Offset: 0x00248864
		public static void paint(mGraphics g, string str, int x, int y, int w, int h, mFont f)
		{
			if (TextInfo.wStr != f.getWidth(str) || !TextInfo.laststring.Equals(str))
			{
				TextInfo.laststring = str;
				TextInfo.dx = 0;
				TextInfo.wStr = f.getWidth(str);
				TextInfo.isBack = false;
				TextInfo.tx = 0;
			}
			g.setClip(x, y, w, h);
			if (TextInfo.wStr > w)
			{
				f.drawString(g, str, x - TextInfo.dx, y, 0);
			}
			else
			{
				f.drawString(g, str, x + w / 2, y, 2);
			}
			GameCanvas.resetTrans(g);
			if (TextInfo.wStr <= w)
			{
				return;
			}
			if (!TextInfo.isBack)
			{
				TextInfo.tx++;
				if (TextInfo.tx > 50)
				{
					TextInfo.dx++;
					if (TextInfo.dx >= TextInfo.wStr)
					{
						TextInfo.tx = 0;
						TextInfo.dx = -w + 30;
						TextInfo.isBack = true;
					}
				}
				return;
			}
			if (TextInfo.dx < 0)
			{
				int num = w + TextInfo.dx >> 1;
				TextInfo.dx += num;
			}
			if (TextInfo.dx > 0)
			{
				TextInfo.dx = 0;
			}
			if (TextInfo.dx == 0)
			{
				TextInfo.tx++;
				if (TextInfo.tx == 50)
				{
					TextInfo.tx = 0;
					TextInfo.isBack = false;
				}
			}
		}

		// Token: 0x0400487F RID: 18559
		public static int dx;

		// Token: 0x04004880 RID: 18560
		public static int tx;

		// Token: 0x04004881 RID: 18561
		public static int wStr;

		// Token: 0x04004882 RID: 18562
		public static bool isBack;

		// Token: 0x04004883 RID: 18563
		public static string laststring = string.Empty;
	}
}
