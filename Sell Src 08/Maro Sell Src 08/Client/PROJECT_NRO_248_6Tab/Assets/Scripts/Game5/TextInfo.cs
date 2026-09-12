using System;

namespace Game5
{
	// Token: 0x02000196 RID: 406
	public class TextInfo
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x00120506 File Offset: 0x0011E706
		public static void reset()
		{
			TextInfo.dx = 0;
			TextInfo.tx = 0;
			TextInfo.isBack = false;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0012051C File Offset: 0x0011E71C
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

		// Token: 0x04002381 RID: 9089
		public static int dx;

		// Token: 0x04002382 RID: 9090
		public static int tx;

		// Token: 0x04002383 RID: 9091
		public static int wStr;

		// Token: 0x04002384 RID: 9092
		public static bool isBack;

		// Token: 0x04002385 RID: 9093
		public static string laststring = string.Empty;
	}
}
