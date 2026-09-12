using System;

namespace Game5
{
	// Token: 0x02000135 RID: 309
	public class ItemTime
	{
		// Token: 0x06000D70 RID: 3440 RVA: 0x000DC37D File Offset: 0x000DA57D
		public ItemTime()
		{
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000DC390 File Offset: 0x000DA590
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

		// Token: 0x06000D72 RID: 3442 RVA: 0x000DC3F4 File Offset: 0x000DA5F4
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

		// Token: 0x06000D73 RID: 3443 RVA: 0x000DC470 File Offset: 0x000DA670
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

		// Token: 0x06000D74 RID: 3444 RVA: 0x000DC4B0 File Offset: 0x000DA6B0
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

		// Token: 0x06000D75 RID: 3445 RVA: 0x000DC4F0 File Offset: 0x000DA6F0
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

		// Token: 0x06000D76 RID: 3446 RVA: 0x000DC530 File Offset: 0x000DA730
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

		// Token: 0x06000D77 RID: 3447 RVA: 0x000DC570 File Offset: 0x000DA770
		public void initTime(int time)
		{
			this.minute = time / 60;
			this.second = time % 60;
			this.coutTime = time;
			this.curr = (this.last = mSystem.currentTimeMillis());
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x000DC5AC File Offset: 0x000DA7AC
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

		// Token: 0x06000D79 RID: 3449 RVA: 0x000DC61C File Offset: 0x000DA81C
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

		// Token: 0x06000D7A RID: 3450 RVA: 0x000DC710 File Offset: 0x000DA910
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

		// Token: 0x04001AAB RID: 6827
		public short idIcon;

		// Token: 0x04001AAC RID: 6828
		public int second;

		// Token: 0x04001AAD RID: 6829
		public int minute;

		// Token: 0x04001AAE RID: 6830
		private long curr;

		// Token: 0x04001AAF RID: 6831
		private long last;

		// Token: 0x04001AB0 RID: 6832
		private bool isText;

		// Token: 0x04001AB1 RID: 6833
		private bool dontClear;

		// Token: 0x04001AB2 RID: 6834
		private string text;

		// Token: 0x04001AB3 RID: 6835
		private bool isPaint_coolDownBar;

		// Token: 0x04001AB4 RID: 6836
		public int time;

		// Token: 0x04001AB5 RID: 6837
		public int coutTime;

		// Token: 0x04001AB6 RID: 6838
		private int per = 100;
	}
}
