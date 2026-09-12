using System;

namespace Game6
{
	// Token: 0x02000093 RID: 147
	public class PopUp
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x0007B248 File Offset: 0x00079448
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

		// Token: 0x060006CF RID: 1743 RVA: 0x0007B3D4 File Offset: 0x000795D4
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

		// Token: 0x060006D0 RID: 1744 RVA: 0x0007B424 File Offset: 0x00079624
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

		// Token: 0x060006D1 RID: 1745 RVA: 0x0007B5AB File Offset: 0x000797AB
		public static void addPopUp(PopUp p)
		{
			PopUp.vPopups.addElement(p);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0007B5B8 File Offset: 0x000797B8
		public void paintClipPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isFocus)
		{
			if (color == 1)
			{
				g.fillRect(x, y, w, h, 16777215, 90);
				return;
			}
			g.fillRect(x, y, w, h, 0, 77);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0007B5E4 File Offset: 0x000797E4
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

		// Token: 0x060006D4 RID: 1748 RVA: 0x0007B83C File Offset: 0x00079A3C
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

		// Token: 0x060006D5 RID: 1749 RVA: 0x0007B914 File Offset: 0x00079B14
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

		// Token: 0x060006D6 RID: 1750 RVA: 0x0007BB4E File Offset: 0x00079D4E
		public void doClick(int timeDelay)
		{
			this.timeDelay = timeDelay;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0007BB58 File Offset: 0x00079D58
		public static void paintAll(mGraphics g)
		{
			for (int i = 0; i < PopUp.vPopups.size(); i++)
			{
				((PopUp)PopUp.vPopups.elementAt(i)).paint(g);
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0007BB90 File Offset: 0x00079D90
		public static void updateAll()
		{
			for (int i = 0; i < PopUp.vPopups.size(); i++)
			{
				((PopUp)PopUp.vPopups.elementAt(i)).update();
			}
		}

		// Token: 0x04000EF7 RID: 3831
		public static MyVector vPopups = new MyVector();

		// Token: 0x04000EF8 RID: 3832
		public int sayWidth;

		// Token: 0x04000EF9 RID: 3833
		public int sayRun;

		// Token: 0x04000EFA RID: 3834
		public string[] says;

		// Token: 0x04000EFB RID: 3835
		public int cx;

		// Token: 0x04000EFC RID: 3836
		public int cy;

		// Token: 0x04000EFD RID: 3837
		public int cw;

		// Token: 0x04000EFE RID: 3838
		public int ch;

		// Token: 0x04000EFF RID: 3839
		public bool isWayPoint;

		// Token: 0x04000F00 RID: 3840
		public int tDelay;

		// Token: 0x04000F01 RID: 3841
		private int timeDelay;

		// Token: 0x04000F02 RID: 3842
		public Command command;

		// Token: 0x04000F03 RID: 3843
		public bool isPaint = true;

		// Token: 0x04000F04 RID: 3844
		public bool isHide;

		// Token: 0x04000F05 RID: 3845
		public static Image goc;

		// Token: 0x04000F06 RID: 3846
		public static Image imgPopUp;

		// Token: 0x04000F07 RID: 3847
		public static Image imgPopUp2;
	}
}
