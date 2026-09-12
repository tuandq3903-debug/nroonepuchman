using System;
using System.Text;

namespace Game3.Mod.XMAP
{
	// Token: 0x02000352 RID: 850
	public class ManualXmap
	{
		// Token: 0x060025DB RID: 9691 RVA: 0x0024E627 File Offset: 0x0024C827
		public static ManualXmap GI()
		{
			if (ManualXmap._Instance == null)
			{
				ManualXmap._Instance = new ManualXmap();
			}
			return ManualXmap._Instance;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0024E63F File Offset: 0x0024C83F
		public void LoadMapLeft()
		{
			ManualXmap.LoadMap(0);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0024E647 File Offset: 0x0024C847
		public void LoadMapCenter()
		{
			ManualXmap.LoadMap(1);
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0024E64F File Offset: 0x0024C84F
		public void LoadMapRight()
		{
			ManualXmap.LoadMap(2);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0024E658 File Offset: 0x0024C858
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

		// Token: 0x060025E0 RID: 9696 RVA: 0x0024E834 File Offset: 0x0024CA34
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

		// Token: 0x060025E1 RID: 9697 RVA: 0x0024E870 File Offset: 0x0024CA70
		private static void ResetSavedWaypoints()
		{
			ManualXmap.wayPointMapLeft = new int[2];
			ManualXmap.wayPointMapCenter = new int[2];
			ManualXmap.wayPointMapRight = new int[2];
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0008F633 File Offset: 0x0008D833
		private static bool IsNRDMap(int mapID)
		{
			return mapID >= 85 && mapID <= 91;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0008F644 File Offset: 0x0008D844
		private static bool IsKarinMap(int mapID)
		{
			return mapID >= 45 && mapID <= 47;
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0024E894 File Offset: 0x0024CA94
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

		// Token: 0x060025E5 RID: 9701 RVA: 0x0024E8FC File Offset: 0x0024CAFC
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

		// Token: 0x060025E6 RID: 9702 RVA: 0x0024E96D File Offset: 0x0024CB6D
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

		// Token: 0x060025E7 RID: 9703 RVA: 0x0024E9A0 File Offset: 0x0024CBA0
		private static bool ShouldRequestChangeMap(int position, Waypoint waypoint)
		{
			if (position == 1 || TileMap.vGo.size() == 1)
			{
				return true;
			}
			Char myChar = Char.myCharz();
			return myChar.isInEnterOfflinePoint() != null || myChar.isInEnterOnlinePoint() != null;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0024E9DC File Offset: 0x0024CBDC
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

		// Token: 0x060025E9 RID: 9705 RVA: 0x0024EA4C File Offset: 0x0024CC4C
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

		// Token: 0x060025EA RID: 9706 RVA: 0x0024EA8C File Offset: 0x0024CC8C
		private static bool IsLeftWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực cấm") || (TileMap.mapID == 73 && ManualXmap.GetTextPopup(waypoint.popup) == "Vực chết") || (TileMap.mapID == 110 && ManualXmap.GetTextPopup(waypoint.popup) == "Rừng tuyết") || waypoint.maxX < 60;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x0024EB09 File Offset: 0x0024CD09
		private static bool IsCenterWaypoint(Waypoint waypoint)
		{
			return TileMap.mapID != 27 && (ManualXmap.IsSpecialCenterMap(waypoint) || ((int)waypoint.minX < TileMap.pxw - 60 && waypoint.maxX >= 60));
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x0024EB3F File Offset: 0x0024CD3F
		private static bool IsRightWaypoint(Waypoint waypoint)
		{
			return (TileMap.mapID == 70 && ManualXmap.GetTextPopup(waypoint.popup) == "Căn cứ Raspberry") || (int)waypoint.minX > TileMap.pxw - 60;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0024EB74 File Offset: 0x0024CD74
		private static bool IsSpecialCenterMap(Waypoint waypoint)
		{
			string popupText = ManualXmap.GetTextPopup(waypoint.popup);
			int mapID = TileMap.mapID;
			return ((mapID == 106 || mapID == 107) && popupText == "Hang băng") || ((mapID == 105 || mapID == 108) && popupText == "Rừng băng") || (mapID == 109 && popupText == "Cánh đồng tuyết");
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0024EBD8 File Offset: 0x0024CDD8
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

		// Token: 0x060025EF RID: 9711 RVA: 0x0024EC24 File Offset: 0x0024CE24
		public static void RequestChangeMap(Waypoint waypoint)
		{
			if (waypoint.isOffline)
			{
				Service.gI().getMapOffline();
				return;
			}
			Service.gI().requestChangeMap();
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x0024EC44 File Offset: 0x0024CE44
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

		// Token: 0x04004947 RID: 18759
		public static ManualXmap _Instance;

		// Token: 0x04004948 RID: 18760
		private static int[] wayPointMapLeft;

		// Token: 0x04004949 RID: 18761
		private static int[] wayPointMapCenter;

		// Token: 0x0400494A RID: 18762
		private static int[] wayPointMapRight;
	}
}
