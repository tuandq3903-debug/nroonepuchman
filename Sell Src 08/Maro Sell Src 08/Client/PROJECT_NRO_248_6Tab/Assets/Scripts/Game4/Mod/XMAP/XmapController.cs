using System;
using System.Collections.Generic;

namespace Game4.Mod.XMAP
{
	// Token: 0x02000281 RID: 641
	public class XmapController : IActionListener
	{
		// Token: 0x06001C6F RID: 7279 RVA: 0x001BAE00 File Offset: 0x001B9000
		public static void Update()
		{
			if (XmapController.IsWaiting() || XmapData.GI().IsLoading)
			{
				return;
			}
			if (XmapController.IsWaitNextMap)
			{
				XmapController.Wait(100);
				XmapController.IsWaitNextMap = false;
				return;
			}
			if (XmapController.IsNextMapFailed)
			{
				XmapData.GI().MyLinkMaps = null;
				XmapController.WayXmap = null;
				XmapController.IsNextMapFailed = false;
				return;
			}
			if (XmapController.WayXmap == null)
			{
				if (XmapData.GI().MyLinkMaps == null)
				{
					XmapData.GI().LoadLinkMaps();
					return;
				}
				XmapController.WayXmap = XmapAlgorithm.FindWay(TileMap.mapID, XmapController.IdMapEnd);
				XmapController.IndexWay = 0;
				if (XmapController.WayXmap == null)
				{
					GameScr.info1.addInfo("Không thể tìm thấy đường đi", 0);
					XmapController.FinishXmap();
					return;
				}
			}
			int mapID = TileMap.mapID;
			List<int> wayXmap = XmapController.WayXmap;
			if (mapID == wayXmap[wayXmap.Count - 1] && !XmapData.IsMyCharDie())
			{
				XmapController.FinishXmap();
				return;
			}
			if (TileMap.mapID == XmapController.WayXmap[XmapController.IndexWay])
			{
				if (XmapData.IsMyCharDie())
				{
					Service.gI().returnTownFromDead();
					XmapController.IsWaitNextMap = (XmapController.IsNextMapFailed = true);
				}
				else if (XmapData.CanNextMap())
				{
					XmapController.NextMap(XmapController.WayXmap[XmapController.IndexWay + 1]);
					XmapController.IsWaitNextMap = true;
				}
				XmapController.Wait(500);
				return;
			}
			if (TileMap.mapID == XmapController.WayXmap[XmapController.IndexWay + 1])
			{
				XmapController.IndexWay++;
				return;
			}
			XmapController.IsNextMapFailed = true;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x001BAF60 File Offset: 0x001B9160
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				XmapController.ShowPanelXmap((List<int>)p);
			}
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x001BAF71 File Offset: 0x001B9171
		private static void Wait(int time)
		{
			XmapController.IsWait = true;
			XmapController.TimeStartWait = mSystem.currentTimeMillis();
			XmapController.TimeWait = (long)time;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x001BAF8A File Offset: 0x001B918A
		private static bool IsWaiting()
		{
			if (XmapController.IsWait && mSystem.currentTimeMillis() - XmapController.TimeStartWait >= XmapController.TimeWait)
			{
				XmapController.IsWait = false;
			}
			return XmapController.IsWait;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x001BAFB0 File Offset: 0x001B91B0
		public static void ShowXmapMenu()
		{
			XmapData.GI().LoadGroupMapsFromFile();
			MyVector myVector = new MyVector();
			foreach (GroupMap groupMap in XmapData.GI().GroupMaps)
			{
				myVector.addElement(new Command(groupMap.NameGroup, XmapController._Instance, 1, groupMap.IdMaps));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x001BB03C File Offset: 0x001B923C
		public static string GetMapName(int id)
		{
			string result = TileMap.mapName;
			if (id >= 0 && id < XmapController.mapNames.Length && XmapController.mapNames[id] != null && XmapController.mapNames[id] != string.Empty)
			{
				result = XmapController.mapNames[id];
			}
			return result;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x001BB084 File Offset: 0x001B9284
		public static void ShowPanelXmap(List<int> idMaps)
		{
			AutoXmap.IsMapTransAsXmap = true;
			int count = idMaps.Count;
			GameCanvas.panel.mapNames = new string[count];
			GameCanvas.panel.planetNames = new string[count];
			for (int i = 0; i < count; i++)
			{
				string str = XmapController.GetMapName(idMaps[i]);
				GameCanvas.panel.mapNames[i] = str + " [" + idMaps[i].ToString() + "]";
				GameCanvas.panel.planetNames[i] = "";
			}
			GameCanvas.panel.setTypeMapTrans();
			GameCanvas.panel.show();
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x001BB127 File Offset: 0x001B9327
		public static void StartRunToMapId(int idMap)
		{
			XmapController.IdMapEnd = idMap;
			AutoXmap.IsXmapRunning = true;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x001BB135 File Offset: 0x001B9335
		public static void FinishXmap()
		{
			AutoXmap.IsXmapRunning = false;
			XmapController.IsNextMapFailed = false;
			XmapData.GI().MyLinkMaps = null;
			XmapController.WayXmap = null;
			GameCanvas.panel.hide();
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x001BB15E File Offset: 0x001B935E
		public static void SaveIdMapCapsuleReturn()
		{
			AutoXmap.IdMapCapsuleReturn = TileMap.mapID;
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x001BB16C File Offset: 0x001B936C
		private static void NextMap(int idMapNext)
		{
			XmapController.VuDangMapNext = idMapNext;
			List<MapNext> mapNexts = XmapData.GI().GetMapNexts(TileMap.mapID);
			if (mapNexts != null)
			{
				foreach (MapNext mapNext in mapNexts)
				{
					if (mapNext.MapID == idMapNext)
					{
						XmapController.NextMap(mapNext);
						return;
					}
				}
			}
			GameScr.info1.addInfo("Lỗi tại dữ liệu", 0);
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x001BB1F0 File Offset: 0x001B93F0
		private static void NextMap(MapNext mapNext)
		{
			switch (mapNext.Type)
			{
			case TypeMapNext.AutoWaypoint:
				XmapController.NextMapAutoWaypoint(mapNext);
				return;
			case TypeMapNext.NpcMenu:
				XmapController.NextMapNpcMenu(mapNext);
				return;
			case TypeMapNext.NpcPanel:
				XmapController.NextMapNpcPanel(mapNext);
				return;
			case TypeMapNext.Position:
				XmapController.NextMapPosition(mapNext);
				return;
			case TypeMapNext.Capsule:
				XmapController.NextMapCapsule(mapNext);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x001BB244 File Offset: 0x001B9444
		private static void NextMapAutoWaypoint(MapNext mapNext)
		{
			Waypoint waypoint = XmapData.FindWaypoint(mapNext.MapID);
			if (waypoint != null)
			{
				int posWaypointX = XmapData.GetPosWaypointX(waypoint);
				int posWaypointY = XmapData.GetPosWaypointY(waypoint);
				ModFunc.GI().MoveTo(posWaypointX, posWaypointY);
				if (Char.myCharz().isInEnterOnlinePoint() != null)
				{
					waypoint.popup.command.performAction();
				}
			}
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x001BB298 File Offset: 0x001B9498
		private static void NextMapNpcMenu(MapNext mapNext)
		{
			int num = mapNext.Info[0];
			if (GameScr.findNPCInMap((short)num) == null)
			{
				XmapController.Fixtl();
				return;
			}
			ModFunc.GI().GotoNpc(num);
			Service.gI().openMenu(num);
			int i = 0;
			while (i < GameCanvas.menu.menuItems.size())
			{
				if (((Command)GameCanvas.menu.menuItems.elementAt(i)).caption.Trim().ToLower().Contains("võ thuật") && XmapController.VuDangMapNext == 129)
				{
					Service.gI().confirmMenu((short)num, (sbyte)i);
					return;
				}
				if (((Command)GameCanvas.menu.menuItems.elementAt(i)).caption.Trim().ToLower().Contains("tương lai") && XmapController.VuDangMapNext >= 92 && XmapController.VuDangMapNext <= 103)
				{
					Service.gI().confirmMenu((short)num, (sbyte)i);
					return;
				}
				if (((Command)GameCanvas.menu.menuItems.elementAt(i)).caption.Trim().ToLower().Contains("yardart") && XmapController.VuDangMapNext >= 131 && XmapController.VuDangMapNext <= 133)
				{
					Service.gI().confirmMenu((short)num, (sbyte)i);
					return;
				}
				if (XmapController.VuDangMapNext >= 161 && XmapController.VuDangMapNext <= 164)
				{
					if (((Command)GameCanvas.menu.menuItems.elementAt(i)).caption.Trim().ToLower().Contains("thực vật"))
					{
						Service.gI().confirmMenu((short)num, (sbyte)i);
						return;
					}
					Service.gI().useItem(0, 1, (sbyte)ModFunc.GI().FindItemIndex(992), -1);
					return;
				}
				else
				{
					i++;
				}
			}
			for (int j = 1; j < mapNext.Info.Length; j++)
			{
				int num2 = mapNext.Info[j];
				Service.gI().confirmMenu((short)num, (sbyte)num2);
			}
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x001BB488 File Offset: 0x001B9688
		private static void Fixtl()
		{
			if (TileMap.mapID == 27)
			{
				XmapController.NextMap(28);
				XmapController.IsWaitNextMap = true;
				XmapController.step = 0;
				return;
			}
			if (TileMap.mapID == 29)
			{
				XmapController.NextMap(28);
				XmapController.IsWaitNextMap = true;
				XmapController.step = 1;
				return;
			}
			if (XmapController.step == 0)
			{
				XmapController.NextMap(29);
				XmapController.IsWaitNextMap = true;
				return;
			}
			if (XmapController.step == 1)
			{
				XmapController.NextMap(27);
				XmapController.IsWaitNextMap = true;
			}
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x001BB4FC File Offset: 0x001B96FC
		private static void NextMapNpcPanel(MapNext mapNext)
		{
			int num = mapNext.Info[0];
			int num2 = mapNext.Info[1];
			int selected = mapNext.Info[2];
			ModFunc.GI().GotoNpc(num);
			Service.gI().openMenu(num);
			Service.gI().confirmMenu((short)num, (sbyte)num2);
			Service.gI().requestMapSelect(selected);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x001BB553 File Offset: 0x001B9753
		private static void NextMapPosition(MapNext mapNext)
		{
			GameCanvas.startOKDlg("Đây là lỗi, vui lòng báo cáo lại với ADMIN");
			InfoDlg.hide();
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x001BB564 File Offset: 0x001B9764
		private static void NextMapCapsule(MapNext mapNext)
		{
			XmapController.SaveIdMapCapsuleReturn();
			int selected = mapNext.Info[0];
			Service.gI().requestMapSelect(selected);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x001BB58A File Offset: 0x001B978A
		public static void UseCapsuleNormal()
		{
			AutoXmap.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, (sbyte)ModFunc.GI().FindItemIndex(193), -1);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x001BB5AF File Offset: 0x001B97AF
		public static void UseCapsuleVip()
		{
			AutoXmap.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, (sbyte)ModFunc.GI().FindItemIndex(194), -1);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x001BB5D4 File Offset: 0x001B97D4
		public static void HideInfoDlg()
		{
			InfoDlg.hide();
		}

		// Token: 0x040036FD RID: 14077
		private static readonly string listMap = "Làng Aru,Đồi hoa cúc,Thung lũng tre,Rừng nấm,Rừng xương,Đảo Kamê,Đông Karin,Làng Mori,Đồi nấm tím,Thị trấn Moori,Thung lũng Namếc,Thung lũng Maima,Vực maima,Đảo Guru,Làng Kakarot,Đồi hoang,Làng Plant,Rừng nguyên sinh,Rừng thông Xayda,Thành phố Vegeta,Vách núi đen,Nhà Gôhan,Nhà Moori,Nhà Broly,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Rừng Bamboo,Rừng dương xỉ,Nam Kamê,Đảo Bulông,Núi hoa vàng,Núi hoa tím,Nam Guru,Đông Nam Guru,Rừng cọ,Rừng đá,Thung lũng đen,Bờ vực đen,Vách núi Aru,Vách núi Moori,Vực Plant,Vách núi Aru,Vách núi Moori,Vách núi Kakarot,Thần điện,Tháp Karin,Rừng Karin,Hành tinh Kaio,Phòng tập thời gian,Thánh địa Kaio,Đấu trường,Đại hội võ thuật,Tường thành 1,Tầng 3,Tầng 1,Tầng 2,Tầng 4,Tường thành 2,Tường thành 3,Trại độc nhãn 1,Trại độc nhãn 2,Trại độc nhãn 3,Trại lính Fide,Núi dây leo,Núi cây quỷ,Trại qủy già,Vực chết,Thung lũng Nappa,Vực cấm,Núi Appule,Căn cứ Raspberry,Thung lũng Raspberry,Thung lũng chết,Đồi cây Fide,Khe núi tử thần,Núi đá,Rừng đá,Lãnh  địa Fize,Núi khỉ đỏ,Núi khỉ vàng,Hang quỷ chim,Núi khỉ đen,Hang khỉ đen,Siêu Thị,Hành tinh M-2,Hành tinh Polaris,Hành tinh Cretaceous,Hành tinh Monmaasu,Hành tinh Rudeeze,Hành tinh Gelbo,Hành tinh Tigere,Thành phố phía đông,Thành phố phía nam,Đảo Balê,95,Cao nguyên,Thành phố phía bắc,Ngọn núi phía bắc,Thung lũng phía bắc,Thị trấn Ginder,101,Nhà Bunma,Võ đài Xên bọ hung,Sân sau siêu thị,Cánh đồng tuyết,Rừng tuyết,Núi tuyết,Dòng sông băng,Rừng băng,Hang băng,Đông Nam Karin,Võ đài Hạt Mít,Đại hội võ thuật,Cổng phi thuyền,Phòng chờ,Thánh địa Kaio,Cửa Ải 1,Cửa Ải 2,Cửa Ải 3,Phòng chỉ huy,Đấu trường,Ngũ Hành Sơn,Ngũ Hành Sơn,Ngũ Hành Sơn,Võ đài Bang,Thành phố Santa,Cổng phi thuyền,Bụng Mabư,Đại hội võ thuật,Đại hội võ thuật Vũ Trụ,Hành Tinh Yardart,Hành Tinh Yardart 2,Hành Tinh Yardart 3,Đại hội võ thuật Vũ Trụ 6-7,Động hải tặc,Hang Bạch Tuộc,Động kho báu,Cảng hải tặc,Hành tinh Potaufeu,Hang động Potaufeu,Con đường rắn độc,Con đường rắn độc,Con đường rắn độc,Hoang mạc,Võ Đài Siêu Cấp,Tây Karin,Sa mạc,Lâu đài Lychee,Thành phố Santa,Lôi Đài,Hành tinh bóng tối,Vùng đất băng giá,Lãnh địa bang hội,Hành tinh Bill,Hành tinh ngục tù,Tây thánh địa,Đông thánh Địa,Bắc thánh địa,Nam thánh Địa,Khu hang động,Bìa rừng nguyên thủy,Rừng nguyên thủy,Làng Plant nguyên thủy,Tranh ngọc Namếc";

		// Token: 0x040036FE RID: 14078
		public static readonly string[] mapNames = XmapController.listMap.Split(new char[]
		{
			','
		});

		// Token: 0x040036FF RID: 14079
		public static int VuDangMapNext;

		// Token: 0x04003700 RID: 14080
		private static int step;

		// Token: 0x04003701 RID: 14081
		private static readonly XmapController _Instance = new XmapController();

		// Token: 0x04003702 RID: 14082
		public static int IdMapEnd;

		// Token: 0x04003703 RID: 14083
		private static List<int> WayXmap;

		// Token: 0x04003704 RID: 14084
		private static int IndexWay;

		// Token: 0x04003705 RID: 14085
		private static bool IsNextMapFailed;

		// Token: 0x04003706 RID: 14086
		private static bool IsWait;

		// Token: 0x04003707 RID: 14087
		private static long TimeStartWait;

		// Token: 0x04003708 RID: 14088
		private static long TimeWait;

		// Token: 0x04003709 RID: 14089
		private static bool IsWaitNextMap;
	}
}
