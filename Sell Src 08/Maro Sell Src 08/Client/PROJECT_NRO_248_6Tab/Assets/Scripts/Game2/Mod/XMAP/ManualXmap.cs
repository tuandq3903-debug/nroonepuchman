using System;
using System.Text;

namespace Game2.Mod.XMAP
{
	// Token: 0x0200042A RID: 1066
	public class ManualXmap
	{
		// Token: 0x06002F7F RID: 12159 RVA: 0x002E36CB File Offset: 0x002E18CB
		public static ManualXmap GI()
		{
			if (ManualXmap._Instance == null)
			{
				ManualXmap._Instance = new ManualXmap();
			}
			return ManualXmap._Instance;
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x002E36E3 File Offset: 0x002E18E3
		public void LoadMapLeft()
		{
			ManualXmap.LoadMap(0);
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x002E36EB File Offset: 0x002E18EB
		public void LoadMapCenter()
		{
			ManualXmap.LoadMap(1);
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x002E36F3 File Offset: 0x002E18F3
		public void LoadMapRight()
		{
			ManualXmap.LoadMap(2);
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x002E36FC File Offset: 0x002E18FC
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

		// Token: 0x06002F84 RID: 12164 RVA: 0x002E38D8 File Offset: 0x002E1AD8
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

		// Token: 0x06002F85 RID: 12165 RVA: 0x002E3914 File Offset: 0x002E1B14
		private static void ResetSavedWaypoints()
		{
			ManualXmap.wayPointMapLeft = new int[2];
			ManualXmap.wayPointMapCenter = new int[2];
			ManualXmap.wayPointMapRight = new int[2];
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x0008F633 File Offset: 0x0008D833
		private static bool IsNRDMap(int mapID)
		{
			return mapID >= 85 && mapID <= 91;
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x0008F644 File Offset: 0x0008D844
		private static bool IsKarinMap(int mapID)
		{
			return mapID >= 45 && mapID <= 47;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x002E3938 File Offset: 0x002E1B38
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

		// Token: 0x06002F89 RID: 12169 RVA: 0x002E39A0 File Offset: 0x002E1BA0
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

		// Token: 0x06002F8A RID: 12170 RVA: 0x002E3A11 File Offset: 0x002E1C11
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

		// Token: 0x06002F8B RID: 12171 RVA: 0x002E3A44 File Offset: 0x002E1C44
		private static bool ShouldRequestChangeMap(int position, Waypoint waypoint)
		{
			if (position == 1 || TileMap.vGo.size() == 1)
			{
				return true;
			}
			Char myChar = Char.myCharz();
			return myChar.isInEnterOfflinePoint() != null || myChar.isInEnterOnlinePoint() != null;
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x002E3A80 File Offset: 0x002E1C80
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

		// Token: 0x06002F8D RID: 12173 RVA: 0x002E3AF0 File Offset: 0x002E1CF0
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

		// Token: 0x06002F8E RID: 12174 RVA: 0x002E3B30 File Offset: 0x002E1D30
		private static bool IsLeftWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực cấm") || (TileMap.mapID == 73 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực chết") || (TileMap.mapID == 110 && ManualXmap.GetTextPopup(waypoint.popup) == "Rừng tuyết") || waypoint.maxX < 60;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x002E3BAD File Offset: 0x002E1DAD
		private static bool IsCenterWaypoint(Waypoint waypoint)
		{
			return TileMap.mapID != 27 && (ManualXmap.IsSpecialCenterMap(waypoint) || ((int)waypoint.minX < TileMap.pxw - 60 && waypoint.maxX >= 60));
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x002E3BE3 File Offset: 0x002E1DE3
		private static bool IsRightWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Căn cứ Raspberry") || (int)waypoint.minX > TileMap.pxw - 60;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x002E3C18 File Offset: 0x002E1E18
		private static bool IsSpecialCenterMap(Waypoint waypoint)
		{
			string popupText = ManualXmap.GetTextPopup(waypoint.popup);
			int mapID = TileMap.mapID;
			return ((mapID == 106 || mapID == 107) && popupText == "Hang băng") || ((mapID == 105 || mapID == 108) && popupText == "Rừng băng") || (mapID == 109 && popupText == "Cánh đồng tuyết");
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x002E3C7C File Offset: 0x002E1E7C
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

		// Token: 0x06002F93 RID: 12179 RVA: 0x002E3CC8 File Offset: 0x002E1EC8
		public static void RequestChangeMap(Waypoint waypoint)
		{
			if (waypoint.isOffline)
			{
				Service.gI().getMapOffline();
				return;
			}
			Service.gI().requestChangeMap();
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x002E3CE8 File Offset: 0x002E1EE8
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

		// Token: 0x04005BC6 RID: 23494
		public static ManualXmap _Instance;

		// Token: 0x04005BC7 RID: 23495
		private static int[] wayPointMapLeft;

		// Token: 0x04005BC8 RID: 23496
		private static int[] wayPointMapCenter;

		// Token: 0x04005BC9 RID: 23497
		private static int[] wayPointMapRight;
	}
}
