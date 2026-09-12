using System;

namespace Game2
{
	// Token: 0x020003BD RID: 957
	public class ItemTime
	{
		// Token: 0x06002A5C RID: 10844 RVA: 0x0029B569 File Offset: 0x00299769
		public ItemTime()
		{
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0029B57C File Offset: 0x0029977C
		public ItemTime(short idIcon, int s)
		{
			this.idIcon = idIcon;
			this.minute = s / 60;
			this.second = s % 60;
			this.time = s;
			this.coutTime = s;
			this.curr = (this.last = mSystem.currentTimeMillis());
			this.isPaint_coolDownBar = (idIcon == 14);
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0029B5E0 File Offset: 0x002997E0
		public void initTimeText(sbyte id, string text, int time)
		{
			if (time == -1)
			{
				this.dontClear = true;
			}
			else
			{
				this.dontClear = false;
			}
			this.isText = true;
			this.minute = time / 60;
			this.second = time % 60;
			this.idIcon = (short)id;
			this.time = time;
			this.coutTime = time;
			this.text = text;
			this.curr = (this.last = mSystem.currentTimeMillis());
			this.isPaint_coolDownBar = (this.idIcon == 14);
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0029B65C File Offset: 0x0029985C
		public static bool isExistItem(int id)
		{
			for (int i = 0; i < Char.vItemTime.size(); i++)
			{
				if ((int)((ItemTime)Char.vItemTime.elementAt(i)).idIcon == id)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0029B69C File Offset: 0x0029989C
		public static ItemTime getMessageById(int id)
		{
			for (int i = 0; i < GameScr.textTime.size(); i++)
			{
				ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
				if ((int)itemTime.idIcon == id)
				{
					return itemTime;
				}
			}
			return null;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0029B6DC File Offset: 0x002998DC
		public static bool isExistMessage(int id)
		{
			for (int i = 0; i < GameScr.textTime.size(); i++)
			{
				if ((int)((ItemTime)GameScr.textTime.elementAt(i)).idIcon == id)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0029B71C File Offset: 0x0029991C
		public static ItemTime getItemById(int id)
		{
			for (int i = 0; i < Char.vItemTime.size(); i++)
			{
				ItemTime itemTime = (ItemTime)Char.vItemTime.elementAt(i);
				if ((int)itemTime.idIcon == id)
				{
					return itemTime;
				}
			}
			return null;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0029B75C File Offset: 0x0029995C
		public void initTime(int time)
		{
			this.minute = time / 60;
			this.second = time % 60;
			this.coutTime = time;
			this.curr = (this.last = mSystem.currentTimeMillis());
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x0029B798 File Offset: 0x00299998
		public void paint(mGraphics g, int x, int y)
		{
			SmallImage.drawSmallImage(g, (int)this.idIcon, x, y, 0, 3);
			string empty = string.Empty;
			empty = this.minute.ToString() + "'";
			if (this.minute == 0)
			{
				empty = this.second.ToString() + "s";
			}
			mFont.tahoma_7b_white.drawString(g, empty, x, y + 15, 2, mFont.tahoma_7b_dark);
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x0029B808 File Offset: 0x00299A08
		public void paintText(mGraphics g, int x, int y)
		{
			if (this.isPaint_coolDownBar)
			{
				if (Char.myCharz() != null)
				{
					int num = 80;
					int x2 = GameCanvas.w / 2 - num / 2;
					int y2 = GameCanvas.h - 80;
					g.setColor(8421504);
					g.fillRect(x2, y2, num, 2);
					g.setColor(16777215);
					if (this.per > 0)
					{
						g.fillRect(x2, y2, num * this.per / 100, 2);
					}
				}
				return;
			}
			string empty = string.Empty;
			empty = this.minute.ToString() + "'";
			if (this.minute < 1)
			{
				empty = this.second.ToString() + "s";
			}
			if (this.minute < 0)
			{
				empty = string.Empty;
			}
			if (this.dontClear)
			{
				empty = string.Empty;
			}
			mFont.tahoma_7b_white.drawString(g, this.text + " " + empty, x, y + 15, 0, mFont.tahoma_7b_dark);
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0029B8FC File Offset: 0x00299AFC
		public void update()
		{
			this.curr = mSystem.currentTimeMillis();
			if (this.curr - this.last >= 1000L)
			{
				this.last = mSystem.currentTimeMillis();
				this.second--;
				this.coutTime--;
				if (this.second <= 0)
				{
					this.second = 60;
					this.minute--;
				}
				if (this.time > 0)
				{
					this.per = this.coutTime * 100 / this.time;
				}
			}
			if (this.minute < 0 && !this.isText)
			{
				Char.vItemTime.removeElement(this);
			}
			if (this.minute < 0 && this.isText && !this.dontClear)
			{
				GameScr.textTime.removeElement(this);
			}
		}

		// Token: 0x04005228 RID: 21032
		public short idIcon;

		// Token: 0x04005229 RID: 21033
		public int second;

		// Token: 0x0400522A RID: 21034
		public int minute;

		// Token: 0x0400522B RID: 21035
		private long curr;

		// Token: 0x0400522C RID: 21036
		private long last;

		// Token: 0x0400522D RID: 21037
		private bool isText;

		// Token: 0x0400522E RID: 21038
		private bool dontClear;

		// Token: 0x0400522F RID: 21039
		private string text;

		// Token: 0x04005230 RID: 21040
		private bool isPaint_coolDownBar;

		// Token: 0x04005231 RID: 21041
		public int time;

		// Token: 0x04005232 RID: 21042
		public int coutTime;

		// Token: 0x04005233 RID: 21043
		private int per = 100;
	}
}
