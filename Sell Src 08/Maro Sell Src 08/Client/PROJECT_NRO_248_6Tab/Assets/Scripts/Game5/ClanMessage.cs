using System;

namespace Game5
{
	// Token: 0x020000F8 RID: 248
	public class ClanMessage : IActionListener
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x000AC5AC File Offset: 0x000AA7AC
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

		// Token: 0x06000B0B RID: 2827 RVA: 0x000AC678 File Offset: 0x000AA878
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

		// Token: 0x06000B0C RID: 2828 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000AC872 File Offset: 0x000AAA72
		public void update()
		{
			if (this.time != 0L)
			{
				this.timeAgo = (int)(mSystem.currentTimeMillis() / 1000L - this.time);
			}
		}

		// Token: 0x0400158D RID: 5517
		public int id;

		// Token: 0x0400158E RID: 5518
		public int type;

		// Token: 0x0400158F RID: 5519
		public int playerId;

		// Token: 0x04001590 RID: 5520
		public string playerName;

		// Token: 0x04001591 RID: 5521
		public long time;

		// Token: 0x04001592 RID: 5522
		public int headId;

		// Token: 0x04001593 RID: 5523
		public string[] chat;

		// Token: 0x04001594 RID: 5524
		public sbyte color;

		// Token: 0x04001595 RID: 5525
		public sbyte role;

		// Token: 0x04001596 RID: 5526
		private int timeAgo;

		// Token: 0x04001597 RID: 5527
		public int recieve;

		// Token: 0x04001598 RID: 5528
		public int maxCap;

		// Token: 0x04001599 RID: 5529
		public string[] option;

		// Token: 0x0400159A RID: 5530
		public static MyVector vMessage = new MyVector();
	}
}
