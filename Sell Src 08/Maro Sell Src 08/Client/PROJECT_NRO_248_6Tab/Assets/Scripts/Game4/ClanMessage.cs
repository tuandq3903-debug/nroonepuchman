using System;

namespace Game4
{
	// Token: 0x020001D0 RID: 464
	public class ClanMessage : IActionListener
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x00141650 File Offset: 0x0013F850
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

		// Token: 0x060014AF RID: 5295 RVA: 0x0014171C File Offset: 0x0013F91C
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

		// Token: 0x060014B0 RID: 5296 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00141916 File Offset: 0x0013FB16
		public void update()
		{
			if (this.time != 0L)
			{
				this.timeAgo = (int)(mSystem.currentTimeMillis() / 1000L - this.time);
			}
		}

		// Token: 0x0400280C RID: 10252
		public int id;

		// Token: 0x0400280D RID: 10253
		public int type;

		// Token: 0x0400280E RID: 10254
		public int playerId;

		// Token: 0x0400280F RID: 10255
		public string playerName;

		// Token: 0x04002810 RID: 10256
		public long time;

		// Token: 0x04002811 RID: 10257
		public int headId;

		// Token: 0x04002812 RID: 10258
		public string[] chat;

		// Token: 0x04002813 RID: 10259
		public sbyte color;

		// Token: 0x04002814 RID: 10260
		public sbyte role;

		// Token: 0x04002815 RID: 10261
		private int timeAgo;

		// Token: 0x04002816 RID: 10262
		public int recieve;

		// Token: 0x04002817 RID: 10263
		public int maxCap;

		// Token: 0x04002818 RID: 10264
		public string[] option;

		// Token: 0x04002819 RID: 10265
		public static MyVector vMessage = new MyVector();
	}
}
