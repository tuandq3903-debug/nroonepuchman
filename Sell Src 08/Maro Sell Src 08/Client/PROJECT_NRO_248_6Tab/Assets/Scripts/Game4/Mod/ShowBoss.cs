using System;

namespace Game4.Mod
{
	// Token: 0x02000276 RID: 630
	public class ShowBoss
	{
		// Token: 0x06001C26 RID: 7206 RVA: 0x001B8AF4 File Offset: 0x001B6CF4
		public ShowBoss(string a)
		{
			if (a.Contains("tiêu diệt"))
			{
				a = a.Replace(" vừa tiêu diệt được", "|");
				a = a.Replace(" mọi người đều ngưỡng mộ", "|");
				a = a.Replace(" -> ", "|");
				a = a.Replace("(Đạo Tôn)", "|");
				a = a.Replace(" Kill Liên Sát ", "|");
				string[] array = a.Split('|', StringSplitOptions.None);
				this.playerKill = array[0].Trim();
				this.nameBoss = array[1].Trim();
				this.mapName = "";
				this.mapID = -1;
				this.AppearTime = DateTime.Now;
				this.time = (long)(this.AppearTime - new DateTime(1970, 1, 1)).TotalSeconds;
				this.startShowTime = mSystem.currentTimeMillis();
				this.startX = -GameCanvas.w;
				this.targetX = 100;
				this.currentX = this.startX;
				this.isShowing = true;
				this.isDone = false;
				object obj = ShowBoss.lockObject;
				lock (obj)
				{
					this.yPos = 65 + ModFunc.killedBossNotif.size() * ShowBoss.VERTICAL_SPACING;
					ModFunc.killedBossNotif.addElement(this);
					return;
				}
			}
			a = a.Replace(a.Substring(0, 5), "|");
			a = a.Replace(" vừa xuất hiện tại", "|");
			a = a.Replace(" khu vực", "|");
			string[] array2 = a.Split('|', StringSplitOptions.None);
			this.nameBoss = array2[1].Trim();
			this.mapName = array2[2].Trim();
			this.mapID = ModFunc.GI().GetMapID(this.mapName);
			this.AppearTime = DateTime.Now;
			this.time = (long)(this.AppearTime - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x001B8D10 File Offset: 0x001B6F10
		public void PaintBoss(mGraphics g, int x, int y, int align)
		{
			if (!string.IsNullOrEmpty(this.playerKill))
			{
				if (ModFunc.notifKillBoss)
				{
					this.PaintKilledBoss(g);
					return;
				}
			}
			else
			{
				this.PaintActiveBoss(g, x, y, align);
			}
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x001B8D3C File Offset: 0x001B6F3C
		private void PaintKilledBoss(mGraphics g)
		{
			if (!this.isShowing)
			{
				return;
			}
			long deltaTime = mSystem.currentTimeMillis() - this.startShowTime;
			if (deltaTime < (long)ShowBoss.SHOW_TIME)
			{
				if (this.currentX < this.targetX)
				{
					this.currentX += 10 * mGraphics.zoomLevel;
					if (this.currentX > this.targetX)
					{
						this.currentX = this.targetX;
					}
				}
			}
			else if (deltaTime >= (long)ShowBoss.SHOW_TIME)
			{
				this.currentX -= 10 * mGraphics.zoomLevel;
				if (this.currentX < -GameCanvas.w)
				{
					this.isShowing = false;
					this.isDone = true;
					ShowBoss.RemoveNotification(this);
					object obj = ShowBoss.lockObject;
					lock (obj)
					{
						for (int i = ModFunc.killedBossNotif.indexOf(this); i < ModFunc.killedBossNotif.size(); i++)
						{
							((ShowBoss)ModFunc.killedBossNotif.elementAt(i)).yPos = 70 + i * ShowBoss.VERTICAL_SPACING;
						}
					}
				}
			}
			if (this.isShowing)
			{
				TimeSpan timeSpan = DateTime.Now.Subtract(this.AppearTime);
				this.GetTimeString(timeSpan);
				string notifText = string.Concat(new string[]
				{
					"[",
					this.nameBoss,
					"]  đã bị [",
					this.playerKill,
					"]  hạ gục"
				});
				int width = mFont.tahoma_7_yellow.getWidth(notifText);
				int maxWidth = GameCanvas.w / 2 - 100;
				if (width > maxWidth)
				{
					string line = "[" + this.nameBoss + "]  đã bị ";
					string line2 = "[" + this.playerKill + "]  hạ gục";
					mFont.tahoma_7_yellow.drawStringBorder(g, line, this.currentX - 97, this.yPos + 8, mFont.LEFT, mFont.tahoma_7_grey);
					mFont.tahoma_7_yellow.drawStringBorder(g, line2, this.currentX - 97, this.yPos + 16, mFont.LEFT, mFont.tahoma_7_grey);
					return;
				}
				mFont.tahoma_7_yellow.drawStringBorder(g, notifText, this.currentX - 97, this.yPos + 12, mFont.LEFT, mFont.tahoma_7_grey);
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x001B8F88 File Offset: 0x001B7188
		private void PaintActiveBoss(mGraphics g, int x, int y, int align)
		{
			g.fillRect(GameCanvas.w - 20, y + 3, 20, 10, 2721889, 90);
			TimeSpan timeSpan = DateTime.Now.Subtract(this.AppearTime);
			string timeAppear = this.GetTimeString(timeSpan);
			mFont mFont = mFont.tahoma_7_yellow;
			if (TileMap.mapName.Trim().ToLower() == this.mapName.Trim().ToLower())
			{
				mFont = mFont.tahoma_7_red;
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					if (((Char)GameScr.vCharInMap.elementAt(i)).cName == this.nameBoss)
					{
						mFont = mFont.tahoma_7b_red;
						break;
					}
				}
			}
			mFont.drawStringBorder(g, string.Concat(new string[]
			{
				this.nameBoss,
				" - ",
				this.mapName,
				" - ",
				timeAppear
			}), x, y, align, mFont.tahoma_7_grey);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x001B9084 File Offset: 0x001B7284
		private string GetTimeString(TimeSpan timeSpan)
		{
			string timeAppear = "";
			int hours = (int)System.Math.Floor((decimal)timeSpan.TotalHours);
			if (hours > 0)
			{
				timeAppear += string.Format("{0}h", hours);
			}
			if (timeSpan.Minutes > 0)
			{
				timeAppear += string.Format("{0}m", timeSpan.Minutes);
			}
			return timeAppear + string.Format("{0}s", timeSpan.Seconds);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x001B910C File Offset: 0x001B730C
		private static void RemoveNotification(ShowBoss notification)
		{
			object obj = ShowBoss.lockObject;
			lock (obj)
			{
				if (!string.IsNullOrEmpty(notification.playerKill))
				{
					int num = ModFunc.killedBossNotif.indexOf(notification);
					ModFunc.killedBossNotif.removeElement(notification);
					for (int i = num; i < ModFunc.killedBossNotif.size(); i++)
					{
						((ShowBoss)ModFunc.killedBossNotif.elementAt(i)).yPos = 65 + i * ShowBoss.VERTICAL_SPACING;
					}
				}
				else
				{
					ModFunc.activeBossNotif.removeElement(notification);
				}
			}
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x001B91A8 File Offset: 0x001B73A8
		public static void UpdateNotifications()
		{
			for (int i = ModFunc.killedBossNotif.size() - 1; i >= 0; i--)
			{
				ShowBoss notification = (ShowBoss)ModFunc.killedBossNotif.elementAt(i);
				if (notification.isDone)
				{
					ShowBoss.RemoveNotification(notification);
					for (int j = i; j < ModFunc.killedBossNotif.size(); j++)
					{
						((ShowBoss)ModFunc.killedBossNotif.elementAt(j)).yPos = 65 + j * ShowBoss.VERTICAL_SPACING;
					}
				}
			}
			for (int i2 = ModFunc.activeBossNotif.size() - 1; i2 >= 0; i2--)
			{
				ShowBoss notification2 = (ShowBoss)ModFunc.activeBossNotif.elementAt(i2);
				if (notification2.isDone)
				{
					ShowBoss.RemoveNotification(notification2);
				}
			}
			if (ModFunc.killedBossNotif.size() <= 5)
			{
				return;
			}
			object obj = ShowBoss.lockObject;
			lock (obj)
			{
				ShowBoss showBoss = (ShowBoss)ModFunc.killedBossNotif.elementAt(0);
				ModFunc.killedBossNotif.removeElementAt(0);
				for (int k = 0; k < ModFunc.killedBossNotif.size(); k++)
				{
					((ShowBoss)ModFunc.killedBossNotif.elementAt(k)).yPos = 65 + k * ShowBoss.VERTICAL_SPACING;
				}
			}
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x001B92F0 File Offset: 0x001B74F0
		public static void HandleChatVip(string chatVip)
		{
			string chatLower = chatVip.Trim().ToLower();
			ShowBoss notification = new ShowBoss(chatVip);
			object obj;
			if (chatLower.Contains("boss") && chatLower.Contains("xuất hiện"))
			{
				obj = ShowBoss.lockObject;
				lock (obj)
				{
					ModFunc.activeBossNotif.addElement(notification);
					if (ModFunc.activeBossNotif.size() > 5)
					{
						ModFunc.activeBossNotif.removeElementAt(0);
					}
					return;
				}
			}
			if (!chatLower.Contains("tiêu diệt"))
			{
				return;
			}
			obj = ShowBoss.lockObject;
			lock (obj)
			{
				if (ModFunc.killedBossNotif.size() >= 5)
				{
					ModFunc.killedBossNotif.removeElementAt(0);
					for (int i = 0; i < ModFunc.killedBossNotif.size(); i++)
					{
						((ShowBoss)ModFunc.killedBossNotif.elementAt(i)).yPos = 65 + i * ShowBoss.VERTICAL_SPACING;
					}
				}
				notification.yPos = 65 + ModFunc.killedBossNotif.size() * ShowBoss.VERTICAL_SPACING;
				ModFunc.killedBossNotif.addElement(notification);
			}
		}

		// Token: 0x040036AE RID: 13998
		private static int SHOW_TIME = 20000;

		// Token: 0x040036AF RID: 13999
		private int startX;

		// Token: 0x040036B0 RID: 14000
		private int targetX;

		// Token: 0x040036B1 RID: 14001
		private int currentX;

		// Token: 0x040036B2 RID: 14002
		private long startShowTime;

		// Token: 0x040036B3 RID: 14003
		private bool isShowing;

		// Token: 0x040036B4 RID: 14004
		private bool isDone;

		// Token: 0x040036B5 RID: 14005
		private static int VERTICAL_SPACING = 10;

		// Token: 0x040036B6 RID: 14006
		private int yPos;

		// Token: 0x040036B7 RID: 14007
		private static object lockObject = new object();

		// Token: 0x040036B8 RID: 14008
		public string nameBoss;

		// Token: 0x040036B9 RID: 14009
		public string mapName;

		// Token: 0x040036BA RID: 14010
		public string playerKill;

		// Token: 0x040036BB RID: 14011
		public int mapID;

		// Token: 0x040036BC RID: 14012
		public long time;

		// Token: 0x040036BD RID: 14013
		public DateTime AppearTime;
	}
}
