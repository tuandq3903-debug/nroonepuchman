using System;

namespace Game3
{
	// Token: 0x020002E5 RID: 741
	public class ItemTime
	{
		// Token: 0x060020B8 RID: 8376 RVA: 0x002064C5 File Offset: 0x002046C5
		public ItemTime()
		{
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x002064D8 File Offset: 0x002046D8
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

		// Token: 0x060020BA RID: 8378 RVA: 0x0020653C File Offset: 0x0020473C
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

		// Token: 0x060020BB RID: 8379 RVA: 0x002065B8 File Offset: 0x002047B8
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

		// Token: 0x060020BC RID: 8380 RVA: 0x002065F8 File Offset: 0x002047F8
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

		// Token: 0x060020BD RID: 8381 RVA: 0x00206638 File Offset: 0x00204838
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

		// Token: 0x060020BE RID: 8382 RVA: 0x00206678 File Offset: 0x00204878
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

		// Token: 0x060020BF RID: 8383 RVA: 0x002066B8 File Offset: 0x002048B8
		public void initTime(int time)
		{
			this.minute = time / 60;
			this.second = time % 60;
			this.coutTime = time;
			this.curr = (this.last = mSystem.currentTimeMillis());
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x002066F4 File Offset: 0x002048F4
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

		// Token: 0x060020C1 RID: 8385 RVA: 0x00206764 File Offset: 0x00204964
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

		// Token: 0x060020C2 RID: 8386 RVA: 0x00206858 File Offset: 0x00204A58
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

		// Token: 0x04003FA9 RID: 16297
		public short idIcon;

		// Token: 0x04003FAA RID: 16298
		public int second;

		// Token: 0x04003FAB RID: 16299
		public int minute;

		// Token: 0x04003FAC RID: 16300
		private long curr;

		// Token: 0x04003FAD RID: 16301
		private long last;

		// Token: 0x04003FAE RID: 16302
		private bool isText;

		// Token: 0x04003FAF RID: 16303
		private bool dontClear;

		// Token: 0x04003FB0 RID: 16304
		private string text;

		// Token: 0x04003FB1 RID: 16305
		private bool isPaint_coolDownBar;

		// Token: 0x04003FB2 RID: 16306
		public int time;

		// Token: 0x04003FB3 RID: 16307
		public int coutTime;

		// Token: 0x04003FB4 RID: 16308
		private int per = 100;
	}
}
