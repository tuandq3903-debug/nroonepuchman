using System;
using System.Collections.Generic;

namespace Game2.Mod.XMAP
{
	// Token: 0x02000431 RID: 1073
	public class XmapController : IActionListener
	{
		// Token: 0x06002FB7 RID: 12215 RVA: 0x002E4F48 File Offset: 0x002E3148
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

		// Token: 0x06002FB8 RID: 12216 RVA: 0x002E50A8 File Offset: 0x002E32A8
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				XmapController.ShowPanelXmap((List<int>)p);
			}
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x002E50B9 File Offset: 0x002E32B9
		private static void Wait(int time)
		{
			XmapController.IsWait = true;
			XmapController.TimeStartWait = mSystem.currentTimeMillis();
			XmapController.TimeWait = (long)time;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x002E50D2 File Offset: 0x002E32D2
		private static bool IsWaiting()
		{
			if (XmapController.IsWait && mSystem.currentTimeMillis() - XmapController.TimeStartWait >= XmapController.TimeWait)
			{
				XmapController.IsWait = false;
			}
			return XmapController.IsWait;
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x002E50F8 File Offset: 0x002E32F8
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

		// Token: 0x06002FBC RID: 12220 RVA: 0x002E5184 File Offset: 0x002E3384
		public static string GetMapName(int id)
		{
			string result = TileMap.mapName;
			if (id >= 0 && id < XmapController.mapNames.Length && XmapController.mapNames[id] != null && XmapController.mapNames[id] != string.Empty)
			{
				result = XmapController.mapNames[id];
			}
			return result;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x002E51CC File Offset: 0x002E33CC
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

		// Token: 0x06002FBE RID: 12222 RVA: 0x002E526F File Offset: 0x002E346F
		public static void StartRunToMapId(int idMap)
		{
			XmapController.IdMapEnd = idMap;
			AutoXmap.IsXmapRunning = true;
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x002E527D File Offset: 0x002E347D
		public static void FinishXmap()
		{
			AutoXmap.IsXmapRunning = false;
			XmapController.IsNextMapFailed = false;
			XmapData.GI().MyLinkMaps = null;
			XmapController.WayXmap = null;
			GameCanvas.panel.hide();
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x002E52A6 File Offset: 0x002E34A6
		public static void SaveIdMapCapsuleReturn()
		{
			AutoXmap.IdMapCapsuleReturn = TileMap.mapID;
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x002E52B4 File Offset: 0x002E34B4
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

		// Token: 0x06002FC2 RID: 12226 RVA: 0x002E5338 File Offset: 0x002E3538
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

		// Token: 0x06002FC3 RID: 12227 RVA: 0x002E538C File Offset: 0x002E358C
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

		// Token: 0x06002FC4 RID: 12228 RVA: 0x002E53E0 File Offset: 0x002E35E0
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

		// Token: 0x06002FC5 RID: 12229 RVA: 0x002E55D0 File Offset: 0x002E37D0
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

		// Token: 0x06002FC6 RID: 12230 RVA: 0x002E5644 File Offset: 0x002E3844
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

		// Token: 0x06002FC7 RID: 12231 RVA: 0x002E569B File Offset: 0x002E389B
		private static void NextMapPosition(MapNext mapNext)
		{
			GameCanvas.startOKDlg("Đây là lỗi, vui lòng báo cáo lại với ADMIN");
			InfoDlg.hide();
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x002E56AC File Offset: 0x002E38AC
		private static void NextMapCapsule(MapNext mapNext)
		{
			XmapController.SaveIdMapCapsuleReturn();
			int selected = mapNext.Info[0];
			Service.gI().requestMapSelect(selected);
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x002E56D2 File Offset: 0x002E38D2
		public static void UseCapsuleNormal()
		{
			AutoXmap.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, (sbyte)ModFunc.GI().FindItemIndex(193), -1);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x002E56F7 File Offset: 0x002E38F7
		public static void UseCapsuleVip()
		{
			AutoXmap.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, (sbyte)ModFunc.GI().FindItemIndex(194), -1);
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x002E571C File Offset: 0x002E391C
		public static void HideInfoDlg()
		{
			InfoDlg.hide();
		}

		// Token: 0x04005BFB RID: 23547
		private static readonly string listMap = "Làng Aru,Đồi hoa cúc,Thung lũng tre,Rừng nấm,Rừng xương,Đảo Kamê,Đông Karin,Làng Mori,Đồi nấm tím,Thị trấn Moori,Thung lũng Namếc,Thung lũng Maima,Vực maima,Đảo Guru,Làng Kakarot,Đồi hoang,Làng Plant,Rừng nguyên sinh,Rừng thông Xayda,Thành phố Vegeta,Vách núi đen,Nhà Gôhan,Nhà Moori,Nhà Broly,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Rừng Bamboo,Rừng dương xỉ,Nam Kamê,Đảo Bulông,Núi hoa vàng,Núi hoa tím,Nam Guru,Đông Nam Guru,Rừng cọ,Rừng đá,Thung lũng đen,Bờ vực đen,Vách núi Aru,Vách núi Moori,Vực Plant,Vách núi Aru,Vách núi Moori,Vách núi Kakarot,Thần điện,Tháp Karin,Rừng Karin,Hành tinh Kaio,Phòng tập thời gian,Thánh địa Kaio,Đấu trường,Đại hội võ thuật,Tường thành 1,Tầng 3,Tầng 1,Tầng 2,Tầng 4,Tường thành 2,Tường thành 3,Trại độc nhãn 1,Trại độc nhãn 2,Trại độc nhãn 3,Trại lính Fide,Núi dây leo,Núi cây quỷ,Trại qủy già,Vực chết,Thung lũng Nappa,Vực cấm,Núi Appule,Căn cứ Raspberry,Thung lũng Raspberry,Thung lũng chết,Đồi cây Fide,Khe núi tử thần,Núi đá,Rừng đá,Lãnh  địa Fize,Núi khỉ đỏ,Núi khỉ vàng,Hang quỷ chim,Núi khỉ đen,Hang khỉ đen,Siêu Thị,Hành tinh M-2,Hành tinh Polaris,Hành tinh Cretaceous,Hành tinh Monmaasu,Hành tinh Rudeeze,Hành tinh Gelbo,Hành tinh Tigere,Thành phố phía đông,Thành phố phía nam,Đảo Balê,95,Cao nguyên,Thành phố phía bắc,Ngọn núi phía bắc,Thung lũng phía bắc,Thị trấn Ginder,101,Nhà Bunma,Võ đài Xên bọ hung,Sân sau siêu thị,Cánh đồng tuyết,Rừng tuyết,Núi tuyết,Dòng sông băng,Rừng băng,Hang băng,Đông Nam Karin,Võ đài Hạt Mít,Đại hội võ thuật,Cổng phi thuyền,Phòng chờ,Thánh địa Kaio,Cửa Ải 1,Cửa Ải 2,Cửa Ải 3,Phòng chỉ huy,Đấu trường,Ngũ Hành Sơn,Ngũ Hành Sơn,Ngũ Hành Sơn,Võ đài Bang,Thành phố Santa,Cổng phi thuyền,Bụng Mabư,Đại hội võ thuật,Đại hội võ thuật Vũ Trụ,Hành Tinh Yardart,Hành Tinh Yardart 2,Hành Tinh Yardart 3,Đại hội võ thuật Vũ Trụ 6-7,Động hải tặc,Hang Bạch Tuộc,Động kho báu,Cảng hải tặc,Hành tinh Potaufeu,Hang động Potaufeu,Con đường rắn độc,Con đường rắn độc,Con đường rắn độc,Hoang mạc,Võ Đài Siêu Cấp,Tây Karin,Sa mạc,Lâu đài Lychee,Thành phố Santa,Lôi Đài,Hành tinh bóng tối,Vùng đất băng giá,Lãnh địa bang hội,Hành tinh Bill,Hành tinh ngục tù,Tây thánh địa,Đông thánh Địa,Bắc thánh địa,Nam thánh Địa,Khu hang động,Bìa rừng nguyên thủy,Rừng nguyên thủy,Làng Plant nguyên thủy,Tranh ngọc Namếc";

		// Token: 0x04005BFC RID: 23548
		public static readonly string[] mapNames = XmapController.listMap.Split(new char[]
		{
			','
		});

		// Token: 0x04005BFD RID: 23549
		public static int VuDangMapNext;

		// Token: 0x04005BFE RID: 23550
		private static int step;

		// Token: 0x04005BFF RID: 23551
		private static readonly XmapController _Instance = new XmapController();

		// Token: 0x04005C00 RID: 23552
		public static int IdMapEnd;

		// Token: 0x04005C01 RID: 23553
		private static List<int> WayXmap;

		// Token: 0x04005C02 RID: 23554
		private static int IndexWay;

		// Token: 0x04005C03 RID: 23555
		private static bool IsNextMapFailed;

		// Token: 0x04005C04 RID: 23556
		private static bool IsWait;

		// Token: 0x04005C05 RID: 23557
		private static long TimeStartWait;

		// Token: 0x04005C06 RID: 23558
		private static long TimeWait;

		// Token: 0x04005C07 RID: 23559
		private static bool IsWaitNextMap;
	}
}
