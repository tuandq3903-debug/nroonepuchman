using System;

namespace Game2
{
	// Token: 0x02000380 RID: 896
	public class ClanMessage : IActionListener
	{
		// Token: 0x060027F6 RID: 10230 RVA: 0x0026B798 File Offset: 0x00269998
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

		// Token: 0x060027F7 RID: 10231 RVA: 0x0026B864 File Offset: 0x00269A64
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

		// Token: 0x060027F8 RID: 10232 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x0026BA5E File Offset: 0x00269C5E
		public void update()
		{
			if (this.time != 0L)
			{
				this.timeAgo = (int)(mSystem.currentTimeMillis() / 1000L - this.time);
			}
		}

		// Token: 0x04004D0A RID: 19722
		public int id;

		// Token: 0x04004D0B RID: 19723
		public int type;

		// Token: 0x04004D0C RID: 19724
		public int playerId;

		// Token: 0x04004D0D RID: 19725
		public string playerName;

		// Token: 0x04004D0E RID: 19726
		public long time;

		// Token: 0x04004D0F RID: 19727
		public int headId;

		// Token: 0x04004D10 RID: 19728
		public string[] chat;

		// Token: 0x04004D11 RID: 19729
		public sbyte color;

		// Token: 0x04004D12 RID: 19730
		public sbyte role;

		// Token: 0x04004D13 RID: 19731
		private int timeAgo;

		// Token: 0x04004D14 RID: 19732
		public int recieve;

		// Token: 0x04004D15 RID: 19733
		public int maxCap;

		// Token: 0x04004D16 RID: 19734
		public string[] option;

		// Token: 0x04004D17 RID: 19735
		public static MyVector vMessage = new MyVector();
	}
}
