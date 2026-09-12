using System;
using System.Text;

namespace Game1.Mod.XMAP
{
	// Token: 0x02000502 RID: 1282
	public class ManualXmap
	{
		// Token: 0x06003923 RID: 14627 RVA: 0x0037876F File Offset: 0x0037696F
		public static ManualXmap GI()
		{
			if (ManualXmap._Instance == null)
			{
				ManualXmap._Instance = new ManualXmap();
			}
			return ManualXmap._Instance;
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x00378787 File Offset: 0x00376987
		public void LoadMapLeft()
		{
			ManualXmap.LoadMap(0);
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x0037878F File Offset: 0x0037698F
		public void LoadMapCenter()
		{
			ManualXmap.LoadMap(1);
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x00378797 File Offset: 0x00376997
		public void LoadMapRight()
		{
			ManualXmap.LoadMap(2);
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x003787A0 File Offset: 0x003769A0
		private static void LoadWaypointsInMap()
		{
			ManualXmap.ResetSavedWaypoints();
			int num = TileMap.vGo.size();
			if (num != 2)
			{
				for (int i = 0; i < num; i++)
				{
					Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
					if (waypoint.maxX < 60)
					{
						ManualXmap.wayPointMapLeft[0] = (int)(waypoint.minX + 15);
						ManualXmap.wayPointMapLeft[1] = (int)waypoint.maxY;
					}
					else if ((int)waypoint.maxX > TileMap.pxw - 60)
					{
						ManualXmap.wayPointMapRight[0] = (int)(waypoint.maxX - 15);
						ManualXmap.wayPointMapRight[1] = (int)waypoint.maxY;
					}
					else
					{
						ManualXmap.wayPointMapCenter[0] = (int)(waypoint.minX + 15);
						ManualXmap.wayPointMapCenter[1] = (int)waypoint.maxY;
					}
				}
				return;
			}
			Waypoint waypoint2 = (Waypoint)TileMap.vGo.elementAt(0);
			Waypoint waypoint3 = (Waypoint)TileMap.vGo.elementAt(1);
			if ((waypoint2.maxX < 60 && waypoint3.maxX < 60) || ((int)waypoint2.minX > TileMap.pxw - 60 && (int)waypoint3.minX > TileMap.pxw - 60))
			{
				ManualXmap.wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
				ManualXmap.wayPointMapLeft[1] = (int)waypoint2.maxY;
				ManualXmap.wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
				ManualXmap.wayPointMapRight[1] = (int)waypoint3.maxY;
				return;
			}
			if (waypoint2.maxX < waypoint3.maxX)
			{
				ManualXmap.wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
				ManualXmap.wayPointMapLeft[1] = (int)waypoint2.maxY;
				ManualXmap.wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
				ManualXmap.wayPointMapRight[1] = (int)waypoint3.maxY;
				return;
			}
			ManualXmap.wayPointMapLeft[0] = (int)(waypoint3.minX + 15);
			ManualXmap.wayPointMapLeft[1] = (int)waypoint3.maxY;
			ManualXmap.wayPointMapRight[0] = (int)(waypoint2.maxX - 15);
			ManualXmap.wayPointMapRight[1] = (int)waypoint2.maxY;
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x0037897C File Offset: 0x00376B7C
		private static int GetYGround(int x)
		{
			int num = 50;
			int i = 0;
			while (i < 30)
			{
				i++;
				num += 24;
				if (TileMap.tileTypeAt(x, num, 2))
				{
					if (num % 24 != 0)
					{
						num -= num % 24;
						break;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x003789B8 File Offset: 0x00376BB8
		private static void ResetSavedWaypoints()
		{
			ManualXmap.wayPointMapLeft = new int[2];
			ManualXmap.wayPointMapCenter = new int[2];
			ManualXmap.wayPointMapRight = new int[2];
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x0008F633 File Offset: 0x0008D833
		private static bool IsNRDMap(int mapID)
		{
			return mapID >= 85 && mapID <= 91;
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x0008F644 File Offset: 0x0008D844
		private static bool IsKarinMap(int mapID)
		{
			return mapID >= 45 && mapID <= 47;
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x003789DC File Offset: 0x00376BDC
		public static void LoadMap(int position)
		{
			if (ManualXmap.IsKarinMap(TileMap.mapID))
			{
				return;
			}
			if (ManualXmap.IsNRDMap(TileMap.mapID))
			{
				ManualXmap.TeleportInNRDMap(position);
				return;
			}
			Waypoint waypoint = ManualXmap.FindWaypoint(position);
			if (waypoint != null)
			{
				int targetX = ManualXmap.GetTargetX(waypoint);
				ModFunc.GI().MoveTo(targetX, (int)waypoint.maxY);
				if (ManualXmap.ShouldRequestChangeMap(position, waypoint))
				{
					ManualXmap.RequestChangeMap(waypoint);
					return;
				}
			}
			else
			{
				ManualXmap.MoveToDefaultPosition(position);
			}
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x00378A44 File Offset: 0x00376C44
		private static void MoveToDefaultPosition(int position)
		{
			switch (position)
			{
			case 0:
				ModFunc.GI().MoveTo(60, ManualXmap.GetYGround(60));
				return;
			case 1:
				ModFunc.GI().MoveTo(TileMap.pxw / 2, ManualXmap.GetYGround(TileMap.pxw / 2));
				return;
			case 2:
				ModFunc.GI().MoveTo(TileMap.pxw - 60, ManualXmap.GetYGround(TileMap.pxw - 60));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x00378AB5 File Offset: 0x00376CB5
		private static int GetTargetX(Waypoint waypoint)
		{
			if (waypoint.maxX < 60)
			{
				return 15;
			}
			if ((int)waypoint.minX > TileMap.pxw - 60)
			{
				return TileMap.pxw - 15;
			}
			return (int)(waypoint.minX + 30);
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x00378AE8 File Offset: 0x00376CE8
		private static bool ShouldRequestChangeMap(int position, Waypoint waypoint)
		{
			if (position == 1 || TileMap.vGo.size() == 1)
			{
				return true;
			}
			Char myChar = Char.myCharz();
			return myChar.isInEnterOfflinePoint() != null || myChar.isInEnterOnlinePoint() != null;
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x00378B24 File Offset: 0x00376D24
		public static Waypoint FindWaypoint(int position)
		{
			if (TileMap.vGo.size() == 0)
			{
				return null;
			}
			if (TileMap.vGo.size() == 1)
			{
				return (Waypoint)TileMap.vGo.elementAt(0);
			}
			ManualXmap.LoadWaypointsInMap();
			for (int i = 0; i < TileMap.vGo.size(); i++)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				if (ManualXmap.IsMatchingWaypoint(waypoint, position))
				{
					return waypoint;
				}
			}
			return null;
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x00378B94 File Offset: 0x00376D94
		private static bool IsMatchingWaypoint(Waypoint waypoint, int position)
		{
			bool result;
			switch (position)
			{
			case 0:
				result = ManualXmap.IsLeftWaypoint(waypoint);
				break;
			case 1:
				result = ManualXmap.IsCenterWaypoint(waypoint);
				break;
			case 2:
				result = ManualXmap.IsRightWaypoint(waypoint);
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x00378BD4 File Offset: 0x00376DD4
		private static bool IsLeftWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực cấm") || (TileMap.mapID == 73 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực chết") || (TileMap.mapID == 110 && ManualXmap.GetTextPopup(waypoint.popup) == "Rừng tuyết") || waypoint.maxX < 60;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00378C51 File Offset: 0x00376E51
		private static bool IsCenterWaypoint(Waypoint waypoint)
		{
			return TileMap.mapID != 27 && (ManualXmap.IsSpecialCenterMap(waypoint) || ((int)waypoint.minX < TileMap.pxw - 60 && waypoint.maxX >= 60));
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x00378C87 File Offset: 0x00376E87
		private static bool IsRightWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Căn cứ Raspberry") || (int)waypoint.minX > TileMap.pxw - 60;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x00378CBC File Offset: 0x00376EBC
		private static bool IsSpecialCenterMap(Waypoint waypoint)
		{
			string popupText = ManualXmap.GetTextPopup(waypoint.popup);
			int mapID = TileMap.mapID;
			return ((mapID == 106 || mapID == 107) && popupText == "Hang băng") || ((mapID == 105 || mapID == 108) && popupText == "Rừng băng") || (mapID == 109 && popupText == "Cánh đồng tuyết");
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x00378D20 File Offset: 0x00376F20
		public static string GetTextPopup(PopUp popUp)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < popUp.says.Length; i++)
			{
				stringBuilder.Append(popUp.says[i]);
				stringBuilder.Append(" ");
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x00378D6C File Offset: 0x00376F6C
		public static void RequestChangeMap(Waypoint waypoint)
		{
			if (waypoint.isOffline)
			{
				Service.gI().getMapOffline();
				return;
			}
			Service.gI().requestChangeMap();
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x00378D8C File Offset: 0x00376F8C
		private static void TeleportInNRDMap(int position)
		{
			if (position == 0)
			{
				ModFunc.GI().MoveTo(60, ManualXmap.GetYGround(60));
				return;
			}
			if (position != 2)
			{
				ModFunc.GI().MoveTo(TileMap.pxw - 60, ManualXmap.GetYGround(TileMap.pxw - 60));
				return;
			}
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId >= 30 && npc.template.npcTemplateId <= 36)
				{
					Char.myCharz().npcFocus = npc;
					ModFunc.GI().MoveTo(npc.cx, npc.cy - 3);
					return;
				}
			}
		}

		// Token: 0x04006E45 RID: 28229
		public static ManualXmap _Instance;

		// Token: 0x04006E46 RID: 28230
		private static int[] wayPointMapLeft;

		// Token: 0x04006E47 RID: 28231
		private static int[] wayPointMapCenter;

		// Token: 0x04006E48 RID: 28232
		private static int[] wayPointMapRight;
	}
}
