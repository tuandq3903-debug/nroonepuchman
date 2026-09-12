using System;

namespace Game4
{
	// Token: 0x0200023A RID: 570
	public class Paint
	{
		// Token: 0x0600190F RID: 6415 RVA: 0x00188304 File Offset: 0x00186504
		public static void loadbg()
		{
			for (int i = 0; i < Paint.goc.Length; i++)
			{
				Paint.goc[i] = GameCanvas.loadImage("/mainImage/myTexture2dgoc" + (i + 1).ToString() + ".png");
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0018834C File Offset: 0x0018654C
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

		// Token: 0x06001911 RID: 6417 RVA: 0x000034B9 File Offset: 0x000016B9
		public void paintTabSoft(mGraphics g)
		{
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0018850C File Offset: 0x0018670C
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

		// Token: 0x06001913 RID: 6419 RVA: 0x0018872A File Offset: 0x0018692A
		public void paintFrameSimple(int x, int y, int w, int h, mGraphics g)
		{
			g.setColor(6702080);
			g.fillRect(x, y, w, h);
			g.setColor(14338484);
			g.fillRect(x + 1, y + 1, w - 2, h - 2);
		}

		// Token: 0x04003224 RID: 12836
		public static int COLORBACKGROUND = 15787715;

		// Token: 0x04003225 RID: 12837
		public static int COLORLIGHT = 16383818;

		// Token: 0x04003226 RID: 12838
		public static int COLORDARK = 3937280;

		// Token: 0x04003227 RID: 12839
		public static int COLORBORDER = 15224576;

		// Token: 0x04003228 RID: 12840
		public static int COLORFOCUS = 16777215;

		// Token: 0x04003229 RID: 12841
		public static Image imgBg;

		// Token: 0x0400322A RID: 12842
		public static Image imgLogo;

		// Token: 0x0400322B RID: 12843
		public static Image imgLB;

		// Token: 0x0400322C RID: 12844
		public static Image imgLT;

		// Token: 0x0400322D RID: 12845
		public static Image imgRB;

		// Token: 0x0400322E RID: 12846
		public static Image imgRT;

		// Token: 0x0400322F RID: 12847
		public static Image imgChuong;

		// Token: 0x04003230 RID: 12848
		public static Image imgSelectBoard;

		// Token: 0x04003231 RID: 12849
		public static Image imgtoiSmall;

		// Token: 0x04003232 RID: 12850
		public static Image imgTayTren;

		// Token: 0x04003233 RID: 12851
		public static Image imgTayDuoi;

		// Token: 0x04003234 RID: 12852
		public static Image[] imgTick = new Image[2];

		// Token: 0x04003235 RID: 12853
		public static Image[] imgMsg = new Image[2];

		// Token: 0x04003236 RID: 12854
		public static Image[] goc = new Image[6];

		// Token: 0x04003237 RID: 12855
		public static int hTab = 24;

		// Token: 0x04003238 RID: 12856
		public static int lenCaption = 0;

		// Token: 0x04003239 RID: 12857
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

		// Token: 0x0400323A RID: 12858
		public static Image imgCheck = GameCanvas.loadImage("/mainImage/myTexture2dcheck.png");
	}
}
