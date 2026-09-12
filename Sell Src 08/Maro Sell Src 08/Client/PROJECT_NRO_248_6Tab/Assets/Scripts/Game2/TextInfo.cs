using System;

namespace Game2
{
	// Token: 0x0200041E RID: 1054
	public class TextInfo
	{
		// Token: 0x06002F24 RID: 12068 RVA: 0x002DF6F2 File Offset: 0x002DD8F2
		public static void reset()
		{
			TextInfo.dx = 0;
			TextInfo.tx = 0;
			TextInfo.isBack = false;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x002DF708 File Offset: 0x002DD908
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

		// Token: 0x04005AFE RID: 23294
		public static int dx;

		// Token: 0x04005AFF RID: 23295
		public static int tx;

		// Token: 0x04005B00 RID: 23296
		public static int wStr;

		// Token: 0x04005B01 RID: 23297
		public static bool isBack;

		// Token: 0x04005B02 RID: 23298
		public static string laststring = string.Empty;
	}
}
