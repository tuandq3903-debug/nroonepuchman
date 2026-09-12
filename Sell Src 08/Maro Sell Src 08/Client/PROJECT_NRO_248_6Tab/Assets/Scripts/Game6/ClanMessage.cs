using System;

namespace Game6
{
	// Token: 0x02000020 RID: 32
	public class ClanMessage : IActionListener
	{
		// Token: 0x06000166 RID: 358 RVA: 0x000173E0 File Offset: 0x000155E0
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

		// Token: 0x06000167 RID: 359 RVA: 0x000174AC File Offset: 0x000156AC
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

		// Token: 0x06000168 RID: 360 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000176A6 File Offset: 0x000158A6
		public void update()
		{
			if (this.time != 0L)
			{
				this.timeAgo = (int)(mSystem.currentTimeMillis() / 1000L - this.time);
			}
		}

		// Token: 0x0400030F RID: 783
		public int id;

		// Token: 0x04000310 RID: 784
		public int type;

		// Token: 0x04000311 RID: 785
		public int playerId;

		// Token: 0x04000312 RID: 786
		public string playerName;

		// Token: 0x04000313 RID: 787
		public long time;

		// Token: 0x04000314 RID: 788
		public int headId;

		// Token: 0x04000315 RID: 789
		public string[] chat;

		// Token: 0x04000316 RID: 790
		public sbyte color;

		// Token: 0x04000317 RID: 791
		public sbyte role;

		// Token: 0x04000318 RID: 792
		private int timeAgo;

		// Token: 0x04000319 RID: 793
		public int recieve;

		// Token: 0x0400031A RID: 794
		public int maxCap;

		// Token: 0x0400031B RID: 795
		public string[] option;

		// Token: 0x0400031C RID: 796
		public static MyVector vMessage = new MyVector();
	}
}
