using System;

namespace Game1
{
	// Token: 0x020004F6 RID: 1270
	public class TextInfo
	{
		// Token: 0x060038C8 RID: 14536 RVA: 0x00374796 File Offset: 0x00372996
		public static void reset()
		{
			TextInfo.dx = 0;
			TextInfo.tx = 0;
			TextInfo.isBack = false;
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x003747AC File Offset: 0x003729AC
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

		// Token: 0x04006D7D RID: 28029
		public static int dx;

		// Token: 0x04006D7E RID: 28030
		public static int tx;

		// Token: 0x04006D7F RID: 28031
		public static int wStr;

		// Token: 0x04006D80 RID: 28032
		public static bool isBack;

		// Token: 0x04006D81 RID: 28033
		public static string laststring = string.Empty;
	}
}
