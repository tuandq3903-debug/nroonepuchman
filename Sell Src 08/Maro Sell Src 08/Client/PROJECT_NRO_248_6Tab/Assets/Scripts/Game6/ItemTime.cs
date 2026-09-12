using System;

namespace Game6
{
	// Token: 0x0200005D RID: 93
	public class ItemTime
	{
		// Token: 0x060003CC RID: 972 RVA: 0x000471D5 File Offset: 0x000453D5
		public ItemTime()
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000471E8 File Offset: 0x000453E8
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

		// Token: 0x060003CE RID: 974 RVA: 0x0004724C File Offset: 0x0004544C
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

		// Token: 0x060003CF RID: 975 RVA: 0x000472C8 File Offset: 0x000454C8
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

		// Token: 0x060003D0 RID: 976 RVA: 0x00047308 File Offset: 0x00045508
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

		// Token: 0x060003D1 RID: 977 RVA: 0x00047348 File Offset: 0x00045548
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

		// Token: 0x060003D2 RID: 978 RVA: 0x00047388 File Offset: 0x00045588
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

		// Token: 0x060003D3 RID: 979 RVA: 0x000473C8 File Offset: 0x000455C8
		public void initTime(int time)
		{
			this.minute = time / 60;
			this.second = time % 60;
			this.coutTime = time;
			this.curr = (this.last = mSystem.currentTimeMillis());
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00047404 File Offset: 0x00045604
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

		// Token: 0x060003D5 RID: 981 RVA: 0x00047474 File Offset: 0x00045674
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

		// Token: 0x060003D6 RID: 982 RVA: 0x00047568 File Offset: 0x00045768
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

		// Token: 0x0400082C RID: 2092
		public short idIcon;

		// Token: 0x0400082D RID: 2093
		public int second;

		// Token: 0x0400082E RID: 2094
		public int minute;

		// Token: 0x0400082F RID: 2095
		private long curr;

		// Token: 0x04000830 RID: 2096
		private long last;

		// Token: 0x04000831 RID: 2097
		private bool isText;

		// Token: 0x04000832 RID: 2098
		private bool dontClear;

		// Token: 0x04000833 RID: 2099
		private string text;

		// Token: 0x04000834 RID: 2100
		private bool isPaint_coolDownBar;

		// Token: 0x04000835 RID: 2101
		public int time;

		// Token: 0x04000836 RID: 2102
		public int coutTime;

		// Token: 0x04000837 RID: 2103
		private int per = 100;
	}
}
