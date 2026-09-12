using System;

namespace Game1
{
	// Token: 0x020004C2 RID: 1218
	public class Paint
	{
		// Token: 0x060035FB RID: 13819 RVA: 0x003474F0 File Offset: 0x003456F0
		public static void loadbg()
		{
			for (int i = 0; i < Paint.goc.Length; i++)
			{
				Paint.goc[i] = GameCanvas.loadImage("/mainImage/myTexture2dgoc" + (i + 1).ToString() + ".png");
			}
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x00347538 File Offset: 0x00345738
		public void paintCmdBar(mGraphics g, Command left, Command center, Command right)
		{
			mFont mFont2 = (!GameCanvas.isTouch) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_dark;
			int num = 3;
			if (left != null)
			{
				Paint.lenCaption = mFont2.getWidth(left.caption);
				if (Paint.lenCaption > 0)
				{
					if (left.x >= 0 && left.y > 0)
					{
						left.paint(g);
					}
					else
					{
						g.drawImage((mScreen.keyTouch != 0) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, 1, GameCanvas.h - mScreen.cmdH - 1, 0);
						mFont2.drawString(g, left.caption, 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
					}
				}
			}
			if (center != null)
			{
				Paint.lenCaption = mFont2.getWidth(center.caption);
				if (Paint.lenCaption > 0)
				{
					if (center.x > 0 && center.y > 0)
					{
						center.paint(g);
					}
					else
					{
						g.drawImage((mScreen.keyTouch != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.hw - 35, GameCanvas.h - mScreen.cmdH - 1, 0);
						mFont2.drawString(g, center.caption, GameCanvas.hw, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
					}
				}
			}
			if (right == null)
			{
				return;
			}
			Paint.lenCaption = mFont2.getWidth(right.caption);
			if (Paint.lenCaption > 0)
			{
				if (right.x > 0 && right.y > 0)
				{
					right.paint(g);
					return;
				}
				g.drawImage((mScreen.keyTouch != 2) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH - 1, 0);
				mFont2.drawString(g, right.caption, GameCanvas.w - 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
			}
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000034B9 File Offset: 0x000016B9
		public void paintTabSoft(mGraphics g)
		{
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x003476F8 File Offset: 0x003458F8
		public void paintPopUp(int x, int y, int w, int h, mGraphics g)
		{
			g.setColor(9340251);
			g.drawRect(x + 18, y, (w - 36) / 2 - 32, h);
			g.drawRect(x + 18 + (w - 36) / 2 + 32, y, (w - 36) / 2 - 22, h);
			g.drawRect(x, y + 8, w, h - 17);
			g.setColor(Paint.COLORBACKGROUND);
			g.fillRect(x + 18, y + 3, (w - 36) / 2 - 32, h - 4);
			g.fillRect(x + 18 + (w - 36) / 2 + 31, y + 3, (w - 38) / 2 - 22, h - 4);
			g.fillRect(x + 1, y + 6, w - 1, h - 11);
			g.setColor(14667919);
			g.fillRect(x + 18, y + 1, (w - 36) / 2 - 32, 2);
			g.fillRect(x + 18 + (w - 36) / 2 + 32, y + 1, (w - 36) / 2 - 12, 2);
			g.fillRect(x + 18, y + h - 2, (w - 36) / 2 - 31, 2);
			g.fillRect(x + 18 + (w - 36) / 2 + 32, y + h - 2, (w - 36) / 2 - 31, 2);
			g.fillRect(x + 1, y + 11, 2, h - 18);
			g.fillRect(x + w - 2, y + 11, 2, h - 18);
			g.drawImage(Paint.goc[0], x - 3, y - 2, mGraphics.TOP | mGraphics.LEFT);
			g.drawImage(Paint.goc[2], x + w + 3, y - 2, StaticObj.TOP_RIGHT);
			g.drawImage(Paint.goc[1], x - 3, y + h + 3, StaticObj.BOTTOM_LEFT);
			g.drawImage(Paint.goc[3], x + w + 4, y + h + 2, StaticObj.BOTTOM_RIGHT);
			g.drawImage(Paint.goc[4], x + w / 2, y, StaticObj.TOP_CENTER);
			g.drawImage(Paint.goc[5], x + w / 2, y + h + 1, StaticObj.BOTTOM_HCENTER);
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x00347916 File Offset: 0x00345B16
		public void paintFrameSimple(int x, int y, int w, int h, mGraphics g)
		{
			g.setColor(6702080);
			g.fillRect(x, y, w, h);
			g.setColor(14338484);
			g.fillRect(x + 1, y + 1, w - 2, h - 2);
		}

		// Token: 0x040069A1 RID: 27041
		public static int COLORBACKGROUND = 15787715;

		// Token: 0x040069A2 RID: 27042
		public static int COLORLIGHT = 16383818;

		// Token: 0x040069A3 RID: 27043
		public static int COLORDARK = 3937280;

		// Token: 0x040069A4 RID: 27044
		public static int COLORBORDER = 15224576;

		// Token: 0x040069A5 RID: 27045
		public static int COLORFOCUS = 16777215;

		// Token: 0x040069A6 RID: 27046
		public static Image imgBg;

		// Token: 0x040069A7 RID: 27047
		public static Image imgLogo;

		// Token: 0x040069A8 RID: 27048
		public static Image imgLB;

		// Token: 0x040069A9 RID: 27049
		public static Image imgLT;

		// Token: 0x040069AA RID: 27050
		public static Image imgRB;

		// Token: 0x040069AB RID: 27051
		public static Image imgRT;

		// Token: 0x040069AC RID: 27052
		public static Image imgChuong;

		// Token: 0x040069AD RID: 27053
		public static Image imgSelectBoard;

		// Token: 0x040069AE RID: 27054
		public static Image imgtoiSmall;

		// Token: 0x040069AF RID: 27055
		public static Image imgTayTren;

		// Token: 0x040069B0 RID: 27056
		public static Image imgTayDuoi;

		// Token: 0x040069B1 RID: 27057
		public static Image[] imgTick = new Image[2];

		// Token: 0x040069B2 RID: 27058
		public static Image[] imgMsg = new Image[2];

		// Token: 0x040069B3 RID: 27059
		public static Image[] goc = new Image[6];

		// Token: 0x040069B4 RID: 27060
		public static int hTab = 24;

		// Token: 0x040069B5 RID: 27061
		public static int lenCaption = 0;

		// Token: 0x040069B6 RID: 27062
		public int[] color = new int[]
		{
			15970400,
			13479911,
			2250052,
			16374659,
			15906669,
			12931125,
			3108954
		};

		// Token: 0x040069B7 RID: 27063
		public static Image imgCheck = GameCanvas.loadImage("/mainImage/myTexture2dcheck.png");
	}
}
