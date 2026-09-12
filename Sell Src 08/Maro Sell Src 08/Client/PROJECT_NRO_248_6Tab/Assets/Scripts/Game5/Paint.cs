using System;

namespace Game5
{
	// Token: 0x02000162 RID: 354
	public class Paint
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x000F3260 File Offset: 0x000F1460
		public static void loadbg()
		{
			for (int i = 0; i < Paint.goc.Length; i++)
			{
				Paint.goc[i] = GameCanvas.loadImage("/mainImage/myTexture2dgoc" + (i + 1).ToString() + ".png");
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000F32A8 File Offset: 0x000F14A8
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

		// Token: 0x06000F6D RID: 3949 RVA: 0x000034B9 File Offset: 0x000016B9
		public void paintTabSoft(mGraphics g)
		{
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x000F3468 File Offset: 0x000F1668
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

		// Token: 0x06000F6F RID: 3951 RVA: 0x000F3686 File Offset: 0x000F1886
		public void paintFrameSimple(int x, int y, int w, int h, mGraphics g)
		{
			g.setColor(6702080);
			g.fillRect(x, y, w, h);
			g.setColor(14338484);
			g.fillRect(x + 1, y + 1, w - 2, h - 2);
		}

		// Token: 0x04001FA5 RID: 8101
		public static int COLORBACKGROUND = 15787715;

		// Token: 0x04001FA6 RID: 8102
		public static int COLORLIGHT = 16383818;

		// Token: 0x04001FA7 RID: 8103
		public static int COLORDARK = 3937280;

		// Token: 0x04001FA8 RID: 8104
		public static int COLORBORDER = 15224576;

		// Token: 0x04001FA9 RID: 8105
		public static int COLORFOCUS = 16777215;

		// Token: 0x04001FAA RID: 8106
		public static Image imgBg;

		// Token: 0x04001FAB RID: 8107
		public static Image imgLogo;

		// Token: 0x04001FAC RID: 8108
		public static Image imgLB;

		// Token: 0x04001FAD RID: 8109
		public static Image imgLT;

		// Token: 0x04001FAE RID: 8110
		public static Image imgRB;

		// Token: 0x04001FAF RID: 8111
		public static Image imgRT;

		// Token: 0x04001FB0 RID: 8112
		public static Image imgChuong;

		// Token: 0x04001FB1 RID: 8113
		public static Image imgSelectBoard;

		// Token: 0x04001FB2 RID: 8114
		public static Image imgtoiSmall;

		// Token: 0x04001FB3 RID: 8115
		public static Image imgTayTren;

		// Token: 0x04001FB4 RID: 8116
		public static Image imgTayDuoi;

		// Token: 0x04001FB5 RID: 8117
		public static Image[] imgTick = new Image[2];

		// Token: 0x04001FB6 RID: 8118
		public static Image[] imgMsg = new Image[2];

		// Token: 0x04001FB7 RID: 8119
		public static Image[] goc = new Image[6];

		// Token: 0x04001FB8 RID: 8120
		public static int hTab = 24;

		// Token: 0x04001FB9 RID: 8121
		public static int lenCaption = 0;

		// Token: 0x04001FBA RID: 8122
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

		// Token: 0x04001FBB RID: 8123
		public static Image imgCheck = GameCanvas.loadImage("/mainImage/myTexture2dcheck.png");
	}
}
