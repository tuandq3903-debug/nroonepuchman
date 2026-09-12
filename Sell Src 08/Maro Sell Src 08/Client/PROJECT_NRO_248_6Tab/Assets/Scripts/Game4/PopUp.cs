using System;

namespace Game4
{
	// Token: 0x02000243 RID: 579
	public class PopUp
	{
		// Token: 0x06001A16 RID: 6678 RVA: 0x001A5444 File Offset: 0x001A3644
		public PopUp(string info, int x, int y)
		{
			this.sayWidth = 100;
			if (info.Length < 10)
			{
				this.sayWidth = 60;
			}
			if (GameCanvas.w == 128)
			{
				this.sayWidth = 128;
			}
			this.says = mFont.tahoma_7b_dark.splitFontArray(info, this.sayWidth - 10);
			this.sayRun = 7;
			this.cx = x - this.sayWidth / 2 - 1;
			this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
			this.cw = this.sayWidth + 2;
			this.ch = (this.says.Length + 1) * 12 + 1;
			while (this.cw % 10 != 0)
			{
				this.cw++;
			}
			while (this.ch % 10 != 0)
			{
				this.ch++;
			}
			if (x >= 0 && x <= 24)
			{
				this.cx += this.cw / 2 + 30;
			}
			if (x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24)
			{
				this.cx -= this.cw / 2 + 6;
			}
			while (this.cx <= 30)
			{
				this.cx += 2;
			}
			while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
			{
				this.cx -= 2;
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x001A55D0 File Offset: 0x001A37D0
		public static void loadBg()
		{
			if (PopUp.goc == null)
			{
				PopUp.goc = GameCanvas.loadImage("/mainImage/myTexture2dbd3.png");
			}
			if (PopUp.imgPopUp == null)
			{
				PopUp.imgPopUp = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup.png");
			}
			if (PopUp.imgPopUp2 == null)
			{
				PopUp.imgPopUp2 = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup2.png");
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x001A5620 File Offset: 0x001A3820
		public void updateXYWH(string[] info, int x, int y)
		{
			this.sayWidth = 0;
			for (int i = 0; i < info.Length; i++)
			{
				if (this.sayWidth < mFont.tahoma_7b_dark.getWidth(info[i]))
				{
					this.sayWidth = mFont.tahoma_7b_dark.getWidth(info[i]);
				}
			}
			this.sayWidth += 20;
			this.says = info;
			this.sayRun = 7;
			this.cx = x - this.sayWidth / 2 - 1;
			this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
			this.cw = this.sayWidth + 2;
			this.ch = (this.says.Length + 1) * 12 + 1;
			while (this.cw % 10 != 0)
			{
				this.cw++;
			}
			while (this.ch % 10 != 0)
			{
				this.ch++;
			}
			if (x >= 0 && x <= 24)
			{
				this.cx += this.cw / 2 + 30;
			}
			if (x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24)
			{
				this.cx -= this.cw / 2 + 6;
			}
			while (this.cx <= 30)
			{
				this.cx += 2;
			}
			while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
			{
				this.cx -= 2;
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x001A57A7 File Offset: 0x001A39A7
		public static void addPopUp(PopUp p)
		{
			PopUp.vPopups.addElement(p);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x001A57B4 File Offset: 0x001A39B4
		public void paintClipPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isFocus)
		{
			if (color == 1)
			{
				g.fillRect(x, y, w, h, 16777215, 90);
				return;
			}
			g.fillRect(x, y, w, h, 0, 77);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x001A57E0 File Offset: 0x001A39E0
		public static void paintPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isButton)
		{
			if (!isButton)
			{
				g.setColor(0);
				g.fillRect(x + 6, y, w - 14 + 1, h);
				g.fillRect(x, y + 6, w, h - 12 + 1);
				g.setColor(color);
				g.fillRect(x + 6, y + 1, w - 12, h - 2);
				g.fillRect(x + 1, y + 6, w - 2, h - 12);
				g.drawRegion(PopUp.goc, 0, 0, 7, 6, 0, x, y, 0);
				g.drawRegion(PopUp.goc, 0, 0, 7, 6, 2, x + w - 7, y, 0);
				g.drawRegion(PopUp.goc, 0, 0, 7, 6, 1, x, y + h - 6, 0);
				g.drawRegion(PopUp.goc, 0, 0, 7, 6, 3, x + w - 7, y + h - 6, 0);
				return;
			}
			Image arg = (color != 1) ? PopUp.imgPopUp : PopUp.imgPopUp2;
			g.drawRegion(arg, 0, 0, 10, 10, 0, x, y, 0);
			g.drawRegion(arg, 0, 20, 10, 10, 0, x + w - 10, y, 0);
			g.drawRegion(arg, 0, 50, 10, 10, 0, x, y + h - 10, 0);
			g.drawRegion(arg, 0, 70, 10, 10, 0, x + w - 10, y + h - 10, 0);
			int num = ((w - 20) % 10 != 0) ? ((w - 20) / 10 + 1) : ((w - 20) / 10);
			int num2 = ((h - 20) % 10 != 0) ? ((h - 20) / 10 + 1) : ((h - 20) / 10);
			for (int i = 0; i < num; i++)
			{
				g.drawRegion(arg, 0, 10, 10, 10, 0, x + 10 + i * 10, y, 0);
			}
			for (int j = 0; j < num2; j++)
			{
				g.drawRegion(arg, 0, 30, 10, 10, 0, x, y + 10 + j * 10, 0);
			}
			for (int k = 0; k < num; k++)
			{
				g.drawRegion(arg, 0, 60, 10, 10, 0, x + 10 + k * 10, y + h - 10, 0);
			}
			for (int l = 0; l < num2; l++)
			{
				g.drawRegion(arg, 0, 40, 10, 10, 0, x + w - 10, y + 10 + l * 10, 0);
			}
			g.setColor((color != 1) ? 16770503 : 12052656);
			g.fillRect(x + 10, y + 10, w - 20, h - 20);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x001A5A38 File Offset: 0x001A3C38
		public void paint(mGraphics g)
		{
			if (this.isPaint && this.says != null && ChatPopup.currChatPopup == null && !this.isHide)
			{
				this.paintClipPopUp(g, this.cx, this.cy - GameCanvas.transY, this.cw, this.ch, (this.timeDelay != 0) ? 1 : 0, true);
				for (int i = 0; i < this.says.Length; i++)
				{
					((this.timeDelay != 0) ? mFont.tahoma_7b_green2 : mFont.tahoma_7b_white).drawString(g, this.says[i], this.cx + this.cw / 2, this.cy + (this.ch / 2 - this.says.Length * 12 / 2) + i * 12 - GameCanvas.transY, 2);
				}
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x001A5B10 File Offset: 0x001A3D10
		private void update()
		{
			if (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId == 0)
			{
				if (this.cx + this.cw >= GameScr.cmx && this.cx <= GameCanvas.w + GameScr.cmx && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy)
				{
					this.isHide = false;
				}
				else
				{
					this.isHide = true;
				}
			}
			if (Char.myCharz().taskMaint == null || (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId != 0))
			{
				if (this.cx + this.cw / 2 >= Char.myCharz().cx - 100 && this.cx + this.cw / 2 <= Char.myCharz().cx + 100 && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy)
				{
					this.isHide = false;
				}
				else
				{
					this.isHide = true;
				}
			}
			if (this.timeDelay > 0)
			{
				this.timeDelay--;
				if (this.timeDelay == 0 && this.command != null)
				{
					this.command.performAction();
				}
			}
			if (!this.isWayPoint)
			{
				return;
			}
			if (Char.myCharz().taskMaint != null)
			{
				if (Char.myCharz().taskMaint.taskId == 0)
				{
					if (Char.myCharz().taskMaint.index == 0)
					{
						this.isPaint = false;
					}
					if (Char.myCharz().taskMaint.index == 1)
					{
						this.isPaint = true;
					}
					if (Char.myCharz().taskMaint.index > 1 && Char.myCharz().taskMaint.index < 6)
					{
						this.isPaint = false;
						return;
					}
				}
				else if (!this.isPaint)
				{
					this.tDelay++;
					if (this.tDelay == 50)
					{
						this.isPaint = true;
						return;
					}
				}
			}
			else if (!this.isPaint)
			{
				Hint.isPaint = false;
				this.tDelay++;
				if (this.tDelay == 50)
				{
					this.isPaint = true;
					Hint.isPaint = true;
				}
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x001A5D4A File Offset: 0x001A3F4A
		public void doClick(int timeDelay)
		{
			this.timeDelay = timeDelay;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x001A5D54 File Offset: 0x001A3F54
		public static void paintAll(mGraphics g)
		{
			for (int i = 0; i < PopUp.vPopups.size(); i++)
			{
				((PopUp)PopUp.vPopups.elementAt(i)).paint(g);
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x001A5D8C File Offset: 0x001A3F8C
		public static void updateAll()
		{
			for (int i = 0; i < PopUp.vPopups.size(); i++)
			{
				((PopUp)PopUp.vPopups.elementAt(i)).update();
			}
		}

		// Token: 0x040033F5 RID: 13301
		public static MyVector vPopups = new MyVector();

		// Token: 0x040033F6 RID: 13302
		public int sayWidth;

		// Token: 0x040033F7 RID: 13303
		public int sayRun;

		// Token: 0x040033F8 RID: 13304
		public string[] says;

		// Token: 0x040033F9 RID: 13305
		public int cx;

		// Token: 0x040033FA RID: 13306
		public int cy;

		// Token: 0x040033FB RID: 13307
		public int cw;

		// Token: 0x040033FC RID: 13308
		public int ch;

		// Token: 0x040033FD RID: 13309
		public bool isWayPoint;

		// Token: 0x040033FE RID: 13310
		public int tDelay;

		// Token: 0x040033FF RID: 13311
		private int timeDelay;

		// Token: 0x04003400 RID: 13312
		public Command command;

		// Token: 0x04003401 RID: 13313
		public bool isPaint = true;

		// Token: 0x04003402 RID: 13314
		public bool isHide;

		// Token: 0x04003403 RID: 13315
		public static Image goc;

		// Token: 0x04003404 RID: 13316
		public static Image imgPopUp;

		// Token: 0x04003405 RID: 13317
		public static Image imgPopUp2;
	}
}
