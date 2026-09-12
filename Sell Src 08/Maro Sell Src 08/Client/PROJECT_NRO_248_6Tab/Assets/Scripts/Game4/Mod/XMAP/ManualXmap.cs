using System;
using System.Text;

namespace Game4.Mod.XMAP
{
	// Token: 0x0200027A RID: 634
	public class ManualXmap
	{
		// Token: 0x06001C37 RID: 7223 RVA: 0x001B9583 File Offset: 0x001B7783
		public static ManualXmap GI()
		{
			if (ManualXmap._Instance == null)
			{
				ManualXmap._Instance = new ManualXmap();
			}
			return ManualXmap._Instance;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x001B959B File Offset: 0x001B779B
		public void LoadMapLeft()
		{
			ManualXmap.LoadMap(0);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x001B95A3 File Offset: 0x001B77A3
		public void LoadMapCenter()
		{
			ManualXmap.LoadMap(1);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x001B95AB File Offset: 0x001B77AB
		public void LoadMapRight()
		{
			ManualXmap.LoadMap(2);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x001B95B4 File Offset: 0x001B77B4
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

		// Token: 0x06001C3C RID: 7228 RVA: 0x001B9790 File Offset: 0x001B7990
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

		// Token: 0x06001C3D RID: 7229 RVA: 0x001B97CC File Offset: 0x001B79CC
		private static void ResetSavedWaypoints()
		{
			ManualXmap.wayPointMapLeft = new int[2];
			ManualXmap.wayPointMapCenter = new int[2];
			ManualXmap.wayPointMapRight = new int[2];
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0008F633 File Offset: 0x0008D833
		private static bool IsNRDMap(int mapID)
		{
			return mapID >= 85 && mapID <= 91;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0008F644 File Offset: 0x0008D844
		private static bool IsKarinMap(int mapID)
		{
			return mapID >= 45 && mapID <= 47;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x001B97F0 File Offset: 0x001B79F0
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

		// Token: 0x06001C41 RID: 7233 RVA: 0x001B9858 File Offset: 0x001B7A58
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

		// Token: 0x06001C42 RID: 7234 RVA: 0x001B98C9 File Offset: 0x001B7AC9
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

		// Token: 0x06001C43 RID: 7235 RVA: 0x001B98FC File Offset: 0x001B7AFC
		private static bool ShouldRequestChangeMap(int position, Waypoint waypoint)
		{
			if (position == 1 || TileMap.vGo.size() == 1)
			{
				return true;
			}
			Char myChar = Char.myCharz();
			return myChar.isInEnterOfflinePoint() != null || myChar.isInEnterOnlinePoint() != null;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x001B9938 File Offset: 0x001B7B38
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

		// Token: 0x06001C45 RID: 7237 RVA: 0x001B99A8 File Offset: 0x001B7BA8
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

		// Token: 0x06001C46 RID: 7238 RVA: 0x001B99E8 File Offset: 0x001B7BE8
		private static bool IsLeftWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực cấm") || (TileMap.mapID == 73 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực chết") || (TileMap.mapID == 110 && ManualXmap.GetTextPopup(waypoint.popup) == "Rừng tuyết") || waypoint.maxX < 60;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x001B9A65 File Offset: 0x001B7C65
		private static bool IsCenterWaypoint(Waypoint waypoint)
		{
			return TileMap.mapID != 27 && (ManualXmap.IsSpecialCenterMap(waypoint) || ((int)waypoint.minX < TileMap.pxw - 60 && waypoint.maxX >= 60));
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x001B9A9B File Offset: 0x001B7C9B
		private static bool IsRightWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Căn cứ Raspberry") || (int)waypoint.minX > TileMap.pxw - 60;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x001B9AD0 File Offset: 0x001B7CD0
		private static bool IsSpecialCenterMap(Waypoint waypoint)
		{
			string popupText = ManualXmap.GetTextPopup(waypoint.popup);
			int mapID = TileMap.mapID;
			return ((mapID == 106 || mapID == 107) && popupText == "Hang băng") || ((mapID == 105 || mapID == 108) && popupText == "Rừng băng") || (mapID == 109 && popupText == "Cánh đồng tuyết");
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x001B9B34 File Offset: 0x001B7D34
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

		// Token: 0x06001C4B RID: 7243 RVA: 0x001B9B80 File Offset: 0x001B7D80
		public static void RequestChangeMap(Waypoint waypoint)
		{
			if (waypoint.isOffline)
			{
				Service.gI().getMapOffline();
				return;
			}
			Service.gI().requestChangeMap();
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x001B9BA0 File Offset: 0x001B7DA0
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

		// Token: 0x040036C8 RID: 14024
		public static ManualXmap _Instance;

		// Token: 0x040036C9 RID: 14025
		private static int[] wayPointMapLeft;

		// Token: 0x040036CA RID: 14026
		private static int[] wayPointMapCenter;

		// Token: 0x040036CB RID: 14027
		private static int[] wayPointMapRight;
	}
}
