using System;

namespace Game1.Mod
{
	// Token: 0x020004FE RID: 1278
	public class ShowBoss
	{
		// Token: 0x06003912 RID: 14610 RVA: 0x00377CE0 File Offset: 0x00375EE0
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

		// Token: 0x06003913 RID: 14611 RVA: 0x00377EFC File Offset: 0x003760FC
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

		// Token: 0x06003914 RID: 14612 RVA: 0x00377F28 File Offset: 0x00376128
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

		// Token: 0x06003915 RID: 14613 RVA: 0x00378174 File Offset: 0x00376374
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

		// Token: 0x06003916 RID: 14614 RVA: 0x00378270 File Offset: 0x00376470
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

		// Token: 0x06003917 RID: 14615 RVA: 0x003782F8 File Offset: 0x003764F8
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

		// Token: 0x06003918 RID: 14616 RVA: 0x00378394 File Offset: 0x00376594
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

		// Token: 0x06003919 RID: 14617 RVA: 0x003784DC File Offset: 0x003766DC
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

		// Token: 0x04006E2B RID: 28203
		private static int SHOW_TIME = 20000;

		// Token: 0x04006E2C RID: 28204
		private int startX;

		// Token: 0x04006E2D RID: 28205
		private int targetX;

		// Token: 0x04006E2E RID: 28206
		private int currentX;

		// Token: 0x04006E2F RID: 28207
		private long startShowTime;

		// Token: 0x04006E30 RID: 28208
		private bool isShowing;

		// Token: 0x04006E31 RID: 28209
		private bool isDone;

		// Token: 0x04006E32 RID: 28210
		private static int VERTICAL_SPACING = 10;

		// Token: 0x04006E33 RID: 28211
		private int yPos;

		// Token: 0x04006E34 RID: 28212
		private static object lockObject = new object();

		// Token: 0x04006E35 RID: 28213
		public string nameBoss;

		// Token: 0x04006E36 RID: 28214
		public string mapName;

		// Token: 0x04006E37 RID: 28215
		public string playerKill;

		// Token: 0x04006E38 RID: 28216
		public int mapID;

		// Token: 0x04006E39 RID: 28217
		public long time;

		// Token: 0x04006E3A RID: 28218
		public DateTime AppearTime;
	}
}
