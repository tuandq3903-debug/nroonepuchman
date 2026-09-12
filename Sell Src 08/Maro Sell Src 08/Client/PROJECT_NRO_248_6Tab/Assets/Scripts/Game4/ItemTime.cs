using System;

namespace Game4
{
	// Token: 0x0200020D RID: 525
	public class ItemTime
	{
		// Token: 0x06001714 RID: 5908 RVA: 0x00171421 File Offset: 0x0016F621
		public ItemTime()
		{
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00171434 File Offset: 0x0016F634
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

		// Token: 0x06001716 RID: 5910 RVA: 0x00171498 File Offset: 0x0016F698
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

		// Token: 0x06001717 RID: 5911 RVA: 0x00171514 File Offset: 0x0016F714
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

		// Token: 0x06001718 RID: 5912 RVA: 0x00171554 File Offset: 0x0016F754
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

		// Token: 0x06001719 RID: 5913 RVA: 0x00171594 File Offset: 0x0016F794
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

		// Token: 0x0600171A RID: 5914 RVA: 0x001715D4 File Offset: 0x0016F7D4
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

		// Token: 0x0600171B RID: 5915 RVA: 0x00171614 File Offset: 0x0016F814
		public void initTime(int time)
		{
			this.minute = time / 60;
			this.second = time % 60;
			this.coutTime = time;
			this.curr = (this.last = mSystem.currentTimeMillis());
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00171650 File Offset: 0x0016F850
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

		// Token: 0x0600171D RID: 5917 RVA: 0x001716C0 File Offset: 0x0016F8C0
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

		// Token: 0x0600171E RID: 5918 RVA: 0x001717B4 File Offset: 0x0016F9B4
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

		// Token: 0x04002D2A RID: 11562
		public short idIcon;

		// Token: 0x04002D2B RID: 11563
		public int second;

		// Token: 0x04002D2C RID: 11564
		public int minute;

		// Token: 0x04002D2D RID: 11565
		private long curr;

		// Token: 0x04002D2E RID: 11566
		private long last;

		// Token: 0x04002D2F RID: 11567
		private bool isText;

		// Token: 0x04002D30 RID: 11568
		private bool dontClear;

		// Token: 0x04002D31 RID: 11569
		private string text;

		// Token: 0x04002D32 RID: 11570
		private bool isPaint_coolDownBar;

		// Token: 0x04002D33 RID: 11571
		public int time;

		// Token: 0x04002D34 RID: 11572
		public int coutTime;

		// Token: 0x04002D35 RID: 11573
		private int per = 100;
	}
}
