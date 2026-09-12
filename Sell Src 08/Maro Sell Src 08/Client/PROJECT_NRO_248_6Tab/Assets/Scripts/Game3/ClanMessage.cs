using System;

namespace Game3
{
	// Token: 0x020002A8 RID: 680
	public class ClanMessage : IActionListener
	{
		// Token: 0x06001E52 RID: 7762 RVA: 0x001D66F4 File Offset: 0x001D48F4
		public static void addMessage(ClanMessage cm, int index, bool upToTop)
		{
			int i = 0;
			while (i < ClanMessage.vMessage.size())
			{
				ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
				if (clanMessage.id == cm.id)
				{
					ClanMessage.vMessage.removeElement(clanMessage);
					if (!upToTop)
					{
						ClanMessage.vMessage.insertElementAt(cm, i);
						return;
					}
					ClanMessage.vMessage.insertElementAt(cm, 0);
					return;
				}
				else
				{
					if (clanMessage.maxCap != 0 && clanMessage.recieve == clanMessage.maxCap)
					{
						ClanMessage.vMessage.removeElement(clanMessage);
					}
					i++;
				}
			}
			if (index == -1)
			{
				ClanMessage.vMessage.addElement(cm);
			}
			else
			{
				ClanMessage.vMessage.insertElementAt(cm, 0);
			}
			if (ClanMessage.vMessage.size() > 20)
			{
				ClanMessage.vMessage.removeElementAt(ClanMessage.vMessage.size() - 1);
			}
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x001D67C0 File Offset: 0x001D49C0
		public void paint(mGraphics g, int x, int y)
		{
			mFont mFont2 = mFont.tahoma_7b_dark;
			if (this.role == 0)
			{
				mFont2 = mFont.tahoma_7b_red;
			}
			else if (this.role == 1)
			{
				mFont2 = mFont.tahoma_7b_green;
			}
			else if (this.role == 2)
			{
				mFont2 = mFont.tahoma_7b_green2;
			}
			if (this.type == 0)
			{
				mFont2.drawString(g, this.playerName, x + 3, y + 1, 0);
				if (this.color == 0)
				{
					mFont.tahoma_7_grey.drawString(g, this.chat[0] + ((this.chat.Length <= 1) ? string.Empty : "..."), x + 3, y + 11, 0);
				}
				else
				{
					mFont.tahoma_7_red.drawString(g, this.chat[0] + ((this.chat.Length <= 1) ? string.Empty : "..."), x + 3, y + 11, 0);
				}
				mFont.tahoma_7_grey.drawString(g, NinjaUtil.getTimeAgo((long)this.timeAgo) + " " + mResources.ago, x + GameCanvas.panel.wScroll - 3, y + 1, mFont.RIGHT);
			}
			if (this.type == 1)
			{
				mFont2.drawString(g, string.Concat(new string[]
				{
					this.playerName,
					" (",
					this.recieve.ToString(),
					"/",
					this.maxCap.ToString(),
					")"
				}), x + 3, y + 1, 0);
				mFont.tahoma_7_blue.drawString(g, string.Concat(new string[]
				{
					mResources.request_pea,
					" ",
					NinjaUtil.getTimeAgo((long)this.timeAgo),
					" ",
					mResources.ago
				}), x + 3, y + 11, 0);
			}
			if (this.type == 2)
			{
				mFont2.drawString(g, this.playerName, x + 3, y + 1, 0);
				mFont.tahoma_7_blue.drawString(g, mResources.request_join_clan, x + 3, y + 11, 0);
			}
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x001D69BA File Offset: 0x001D4BBA
		public void update()
		{
			if (this.time != 0L)
			{
				this.timeAgo = (int)(mSystem.currentTimeMillis() / 1000L - this.time);
			}
		}

		// Token: 0x04003A8B RID: 14987
		public int id;

		// Token: 0x04003A8C RID: 14988
		public int type;

		// Token: 0x04003A8D RID: 14989
		public int playerId;

		// Token: 0x04003A8E RID: 14990
		public string playerName;

		// Token: 0x04003A8F RID: 14991
		public long time;

		// Token: 0x04003A90 RID: 14992
		public int headId;

		// Token: 0x04003A91 RID: 14993
		public string[] chat;

		// Token: 0x04003A92 RID: 14994
		public sbyte color;

		// Token: 0x04003A93 RID: 14995
		public sbyte role;

		// Token: 0x04003A94 RID: 14996
		private int timeAgo;

		// Token: 0x04003A95 RID: 14997
		public int recieve;

		// Token: 0x04003A96 RID: 14998
		public int maxCap;

		// Token: 0x04003A97 RID: 14999
		public string[] option;

		// Token: 0x04003A98 RID: 15000
		public static MyVector vMessage = new MyVector();
	}
}
