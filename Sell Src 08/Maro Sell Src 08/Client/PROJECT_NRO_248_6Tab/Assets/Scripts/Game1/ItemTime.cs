using System;

namespace Game1
{
	// Token: 0x02000495 RID: 1173
	public class ItemTime
	{
		// Token: 0x06003400 RID: 13312 RVA: 0x0033060D File Offset: 0x0032E80D
		public ItemTime()
		{
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x00330620 File Offset: 0x0032E820
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

		// Token: 0x06003402 RID: 13314 RVA: 0x00330684 File Offset: 0x0032E884
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

		// Token: 0x06003403 RID: 13315 RVA: 0x00330700 File Offset: 0x0032E900
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

		// Token: 0x06003404 RID: 13316 RVA: 0x00330740 File Offset: 0x0032E940
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

		// Token: 0x06003405 RID: 13317 RVA: 0x00330780 File Offset: 0x0032E980
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

		// Token: 0x06003406 RID: 13318 RVA: 0x003307C0 File Offset: 0x0032E9C0
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

		// Token: 0x06003407 RID: 13319 RVA: 0x00330800 File Offset: 0x0032EA00
		public void initTime(int time)
		{
			this.minute = time / 60;
			this.second = time % 60;
			this.coutTime = time;
			this.curr = (this.last = mSystem.currentTimeMillis());
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x0033083C File Offset: 0x0032EA3C
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

		// Token: 0x06003409 RID: 13321 RVA: 0x003308AC File Offset: 0x0032EAAC
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

		// Token: 0x0600340A RID: 13322 RVA: 0x003309A0 File Offset: 0x0032EBA0
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

		// Token: 0x040064A7 RID: 25767
		public short idIcon;

		// Token: 0x040064A8 RID: 25768
		public int second;

		// Token: 0x040064A9 RID: 25769
		public int minute;

		// Token: 0x040064AA RID: 25770
		private long curr;

		// Token: 0x040064AB RID: 25771
		private long last;

		// Token: 0x040064AC RID: 25772
		private bool isText;

		// Token: 0x040064AD RID: 25773
		private bool dontClear;

		// Token: 0x040064AE RID: 25774
		private string text;

		// Token: 0x040064AF RID: 25775
		private bool isPaint_coolDownBar;

		// Token: 0x040064B0 RID: 25776
		public int time;

		// Token: 0x040064B1 RID: 25777
		public int coutTime;

		// Token: 0x040064B2 RID: 25778
		private int per = 100;
	}
}
