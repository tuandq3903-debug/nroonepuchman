using System;

namespace Game3.Mod
{
	// Token: 0x0200034E RID: 846
	public class ShowBoss
	{
		// Token: 0x060025CA RID: 9674 RVA: 0x0024DB98 File Offset: 0x0024BD98
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

		// Token: 0x060025CB RID: 9675 RVA: 0x0024DDB4 File Offset: 0x0024BFB4
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

		// Token: 0x060025CC RID: 9676 RVA: 0x0024DDE0 File Offset: 0x0024BFE0
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

		// Token: 0x060025CD RID: 9677 RVA: 0x0024E02C File Offset: 0x0024C22C
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

		// Token: 0x060025CE RID: 9678 RVA: 0x0024E128 File Offset: 0x0024C328
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

		// Token: 0x060025CF RID: 9679 RVA: 0x0024E1B0 File Offset: 0x0024C3B0
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

		// Token: 0x060025D0 RID: 9680 RVA: 0x0024E24C File Offset: 0x0024C44C
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

		// Token: 0x060025D1 RID: 9681 RVA: 0x0024E394 File Offset: 0x0024C594
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

		// Token: 0x0400492D RID: 18733
		private static int SHOW_TIME = 20000;

		// Token: 0x0400492E RID: 18734
		private int startX;

		// Token: 0x0400492F RID: 18735
		private int targetX;

		// Token: 0x04004930 RID: 18736
		private int currentX;

		// Token: 0x04004931 RID: 18737
		private long startShowTime;

		// Token: 0x04004932 RID: 18738
		private bool isShowing;

		// Token: 0x04004933 RID: 18739
		private bool isDone;

		// Token: 0x04004934 RID: 18740
		private static int VERTICAL_SPACING = 10;

		// Token: 0x04004935 RID: 18741
		private int yPos;

		// Token: 0x04004936 RID: 18742
		private static object lockObject = new object();

		// Token: 0x04004937 RID: 18743
		public string nameBoss;

		// Token: 0x04004938 RID: 18744
		public string mapName;

		// Token: 0x04004939 RID: 18745
		public string playerKill;

		// Token: 0x0400493A RID: 18746
		public int mapID;

		// Token: 0x0400493B RID: 18747
		public long time;

		// Token: 0x0400493C RID: 18748
		public DateTime AppearTime;
	}
}
