using System;

namespace Game5
{
	// Token: 0x0200016B RID: 363
	public class PopUp
	{
		// Token: 0x06001072 RID: 4210 RVA: 0x001103A0 File Offset: 0x0010E5A0
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

		// Token: 0x06001073 RID: 4211 RVA: 0x0011052C File Offset: 0x0010E72C
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

		// Token: 0x06001074 RID: 4212 RVA: 0x0011057C File Offset: 0x0010E77C
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

		// Token: 0x06001075 RID: 4213 RVA: 0x00110703 File Offset: 0x0010E903
		public static void addPopUp(PopUp p)
		{
			PopUp.vPopups.addElement(p);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00110710 File Offset: 0x0010E910
		public void paintClipPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isFocus)
		{
			if (color == 1)
			{
				g.fillRect(x, y, w, h, 16777215, 90);
				return;
			}
			g.fillRect(x, y, w, h, 0, 77);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0011073C File Offset: 0x0010E93C
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

		// Token: 0x06001078 RID: 4216 RVA: 0x00110994 File Offset: 0x0010EB94
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

		// Token: 0x06001079 RID: 4217 RVA: 0x00110A6C File Offset: 0x0010EC6C
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

		// Token: 0x0600107A RID: 4218 RVA: 0x00110CA6 File Offset: 0x0010EEA6
		public void doClick(int timeDelay)
		{
			this.timeDelay = timeDelay;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00110CB0 File Offset: 0x0010EEB0
		public static void paintAll(mGraphics g)
		{
			for (int i = 0; i < PopUp.vPopups.size(); i++)
			{
				((PopUp)PopUp.vPopups.elementAt(i)).paint(g);
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00110CE8 File Offset: 0x0010EEE8
		public static void updateAll()
		{
			for (int i = 0; i < PopUp.vPopups.size(); i++)
			{
				((PopUp)PopUp.vPopups.elementAt(i)).update();
			}
		}

		// Token: 0x04002176 RID: 8566
		public static MyVector vPopups = new MyVector();

		// Token: 0x04002177 RID: 8567
		public int sayWidth;

		// Token: 0x04002178 RID: 8568
		public int sayRun;

		// Token: 0x04002179 RID: 8569
		public string[] says;

		// Token: 0x0400217A RID: 8570
		public int cx;

		// Token: 0x0400217B RID: 8571
		public int cy;

		// Token: 0x0400217C RID: 8572
		public int cw;

		// Token: 0x0400217D RID: 8573
		public int ch;

		// Token: 0x0400217E RID: 8574
		public bool isWayPoint;

		// Token: 0x0400217F RID: 8575
		public int tDelay;

		// Token: 0x04002180 RID: 8576
		private int timeDelay;

		// Token: 0x04002181 RID: 8577
		public Command command;

		// Token: 0x04002182 RID: 8578
		public bool isPaint = true;

		// Token: 0x04002183 RID: 8579
		public bool isHide;

		// Token: 0x04002184 RID: 8580
		public static Image goc;

		// Token: 0x04002185 RID: 8581
		public static Image imgPopUp;

		// Token: 0x04002186 RID: 8582
		public static Image imgPopUp2;
	}
}
