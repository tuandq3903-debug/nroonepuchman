using System;

namespace Game4
{
	// Token: 0x0200026E RID: 622
	public class TextInfo
	{
		// Token: 0x06001BDC RID: 7132 RVA: 0x001B55AA File Offset: 0x001B37AA
		public static void reset()
		{
			TextInfo.dx = 0;
			TextInfo.tx = 0;
			TextInfo.isBack = false;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x001B55C0 File Offset: 0x001B37C0
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

		// Token: 0x04003600 RID: 13824
		public static int dx;

		// Token: 0x04003601 RID: 13825
		public static int tx;

		// Token: 0x04003602 RID: 13826
		public static int wStr;

		// Token: 0x04003603 RID: 13827
		public static bool isBack;

		// Token: 0x04003604 RID: 13828
		public static string laststring = string.Empty;
	}
}
