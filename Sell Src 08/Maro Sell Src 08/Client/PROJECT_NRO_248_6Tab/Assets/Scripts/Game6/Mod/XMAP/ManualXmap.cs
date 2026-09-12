using System;
using System.Text;

namespace Game6.Mod.XMAP
{
	// Token: 0x020000CA RID: 202
	public class ManualXmap
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x0008F3C7 File Offset: 0x0008D5C7
		public static ManualXmap GI()
		{
			if (ManualXmap._Instance == null)
			{
				ManualXmap._Instance = new ManualXmap();
			}
			return ManualXmap._Instance;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0008F3DF File Offset: 0x0008D5DF
		public void LoadMapLeft()
		{
			ManualXmap.LoadMap(0);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0008F3E7 File Offset: 0x0008D5E7
		public void LoadMapCenter()
		{
			ManualXmap.LoadMap(1);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0008F3EF File Offset: 0x0008D5EF
		public void LoadMapRight()
		{
			ManualXmap.LoadMap(2);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0008F3F8 File Offset: 0x0008D5F8
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

		// Token: 0x060008F4 RID: 2292 RVA: 0x0008F5D4 File Offset: 0x0008D7D4
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

		// Token: 0x060008F5 RID: 2293 RVA: 0x0008F610 File Offset: 0x0008D810
		private static void ResetSavedWaypoints()
		{
			ManualXmap.wayPointMapLeft = new int[2];
			ManualXmap.wayPointMapCenter = new int[2];
			ManualXmap.wayPointMapRight = new int[2];
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0008F633 File Offset: 0x0008D833
		private static bool IsNRDMap(int mapID)
		{
			return mapID >= 85 && mapID <= 91;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0008F644 File Offset: 0x0008D844
		private static bool IsKarinMap(int mapID)
		{
			return mapID >= 45 && mapID <= 47;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0008F658 File Offset: 0x0008D858
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

		// Token: 0x060008F9 RID: 2297 RVA: 0x0008F6C0 File Offset: 0x0008D8C0
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

		// Token: 0x060008FA RID: 2298 RVA: 0x0008F731 File Offset: 0x0008D931
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

		// Token: 0x060008FB RID: 2299 RVA: 0x0008F764 File Offset: 0x0008D964
		private static bool ShouldRequestChangeMap(int position, Waypoint waypoint)
		{
			if (position == 1 || TileMap.vGo.size() == 1)
			{
				return true;
			}
			Char myChar = Char.myCharz();
			return myChar.isInEnterOfflinePoint() != null || myChar.isInEnterOnlinePoint() != null;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0008F7A0 File Offset: 0x0008D9A0
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

		// Token: 0x060008FD RID: 2301 RVA: 0x0008F810 File Offset: 0x0008DA10
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

		// Token: 0x060008FE RID: 2302 RVA: 0x0008F850 File Offset: 0x0008DA50
		private static bool IsLeftWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực cấm") || (TileMap.mapID == 73 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực chết") || (TileMap.mapID == 110 && ManualXmap.GetTextPopup(waypoint.popup) == "Rừng tuyết") || waypoint.maxX < 60;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0008F8CD File Offset: 0x0008DACD
		private static bool IsCenterWaypoint(Waypoint waypoint)
		{
			return TileMap.mapID != 27 && (ManualXmap.IsSpecialCenterMap(waypoint) || ((int)waypoint.minX < TileMap.pxw - 60 && waypoint.maxX >= 60));
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0008F903 File Offset: 0x0008DB03
		private static bool IsRightWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Căn cứ Raspberry") || (int)waypoint.minX > TileMap.pxw - 60;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0008F938 File Offset: 0x0008DB38
		private static bool IsSpecialCenterMap(Waypoint waypoint)
		{
			string popupText = ManualXmap.GetTextPopup(waypoint.popup);
			int mapID = TileMap.mapID;
			return ((mapID == 106 || mapID == 107) && popupText == "Hang băng") || ((mapID == 105 || mapID == 108) && popupText == "Rừng băng") || (mapID == 109 && popupText == "Cánh đồng tuyết");
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0008F99C File Offset: 0x0008DB9C
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

		// Token: 0x06000903 RID: 2307 RVA: 0x0008F9E8 File Offset: 0x0008DBE8
		public static void RequestChangeMap(Waypoint waypoint)
		{
			if (waypoint.isOffline)
			{
				Service.gI().getMapOffline();
				return;
			}
			Service.gI().requestChangeMap();
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0008FA08 File Offset: 0x0008DC08
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

		// Token: 0x040011CA RID: 4554
		public static ManualXmap _Instance;

		// Token: 0x040011CB RID: 4555
		private static int[] wayPointMapLeft;

		// Token: 0x040011CC RID: 4556
		private static int[] wayPointMapCenter;

		// Token: 0x040011CD RID: 4557
		private static int[] wayPointMapRight;
	}
}
