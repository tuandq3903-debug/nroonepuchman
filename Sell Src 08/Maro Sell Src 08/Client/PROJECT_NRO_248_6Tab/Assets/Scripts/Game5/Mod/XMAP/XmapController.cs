using System;
using System.Collections.Generic;

namespace Game5.Mod.XMAP
{
	// Token: 0x020001A9 RID: 425
	public class XmapController : IActionListener
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x00125D5C File Offset: 0x00123F5C
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

		// Token: 0x060012CC RID: 4812 RVA: 0x00125EBC File Offset: 0x001240BC
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				XmapController.ShowPanelXmap((List<int>)p);
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00125ECD File Offset: 0x001240CD
		private static void Wait(int time)
		{
			XmapController.IsWait = true;
			XmapController.TimeStartWait = mSystem.currentTimeMillis();
			XmapController.TimeWait = (long)time;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00125EE6 File Offset: 0x001240E6
		private static bool IsWaiting()
		{
			if (XmapController.IsWait && mSystem.currentTimeMillis() - XmapController.TimeStartWait >= XmapController.TimeWait)
			{
				XmapController.IsWait = false;
			}
			return XmapController.IsWait;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00125F0C File Offset: 0x0012410C
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

		// Token: 0x060012D0 RID: 4816 RVA: 0x00125F98 File Offset: 0x00124198
		public static string GetMapName(int id)
		{
			string result = TileMap.mapName;
			if (id >= 0 && id < XmapController.mapNames.Length && XmapController.mapNames[id] != null && XmapController.mapNames[id] != string.Empty)
			{
				result = XmapController.mapNames[id];
			}
			return result;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00125FE0 File Offset: 0x001241E0
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

		// Token: 0x060012D2 RID: 4818 RVA: 0x00126083 File Offset: 0x00124283
		public static void StartRunToMapId(int idMap)
		{
			XmapController.IdMapEnd = idMap;
			AutoXmap.IsXmapRunning = true;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00126091 File Offset: 0x00124291
		public static void FinishXmap()
		{
			AutoXmap.IsXmapRunning = false;
			XmapController.IsNextMapFailed = false;
			XmapData.GI().MyLinkMaps = null;
			XmapController.WayXmap = null;
			GameCanvas.panel.hide();
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x001260BA File Offset: 0x001242BA
		public static void SaveIdMapCapsuleReturn()
		{
			AutoXmap.IdMapCapsuleReturn = TileMap.mapID;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x001260C8 File Offset: 0x001242C8
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

		// Token: 0x060012D6 RID: 4822 RVA: 0x0012614C File Offset: 0x0012434C
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

		// Token: 0x060012D7 RID: 4823 RVA: 0x001261A0 File Offset: 0x001243A0
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

		// Token: 0x060012D8 RID: 4824 RVA: 0x001261F4 File Offset: 0x001243F4
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

		// Token: 0x060012D9 RID: 4825 RVA: 0x001263E4 File Offset: 0x001245E4
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

		// Token: 0x060012DA RID: 4826 RVA: 0x00126458 File Offset: 0x00124658
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

		// Token: 0x060012DB RID: 4827 RVA: 0x001264AF File Offset: 0x001246AF
		private static void NextMapPosition(MapNext mapNext)
		{
			GameCanvas.startOKDlg("Đây là lỗi, vui lòng báo cáo lại với ADMIN");
			InfoDlg.hide();
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x001264C0 File Offset: 0x001246C0
		private static void NextMapCapsule(MapNext mapNext)
		{
			XmapController.SaveIdMapCapsuleReturn();
			int selected = mapNext.Info[0];
			Service.gI().requestMapSelect(selected);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x001264E6 File Offset: 0x001246E6
		public static void UseCapsuleNormal()
		{
			AutoXmap.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, (sbyte)ModFunc.GI().FindItemIndex(193), -1);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0012650B File Offset: 0x0012470B
		public static void UseCapsuleVip()
		{
			AutoXmap.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, (sbyte)ModFunc.GI().FindItemIndex(194), -1);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00126530 File Offset: 0x00124730
		public static void HideInfoDlg()
		{
			InfoDlg.hide();
		}

		// Token: 0x0400247E RID: 9342
		private static readonly string listMap = "Làng Aru,Đồi hoa cúc,Thung lũng tre,Rừng nấm,Rừng xương,Đảo Kamê,Đông Karin,Làng Mori,Đồi nấm tím,Thị trấn Moori,Thung lũng Namếc,Thung lũng Maima,Vực maima,Đảo Guru,Làng Kakarot,Đồi hoang,Làng Plant,Rừng nguyên sinh,Rừng thông Xayda,Thành phố Vegeta,Vách núi đen,Nhà Gôhan,Nhà Moori,Nhà Broly,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Rừng Bamboo,Rừng dương xỉ,Nam Kamê,Đảo Bulông,Núi hoa vàng,Núi hoa tím,Nam Guru,Đông Nam Guru,Rừng cọ,Rừng đá,Thung lũng đen,Bờ vực đen,Vách núi Aru,Vách núi Moori,Vực Plant,Vách núi Aru,Vách núi Moori,Vách núi Kakarot,Thần điện,Tháp Karin,Rừng Karin,Hành tinh Kaio,Phòng tập thời gian,Thánh địa Kaio,Đấu trường,Đại hội võ thuật,Tường thành 1,Tầng 3,Tầng 1,Tầng 2,Tầng 4,Tường thành 2,Tường thành 3,Trại độc nhãn 1,Trại độc nhãn 2,Trại độc nhãn 3,Trại lính Fide,Núi dây leo,Núi cây quỷ,Trại qủy già,Vực chết,Thung lũng Nappa,Vực cấm,Núi Appule,Căn cứ Raspberry,Thung lũng Raspberry,Thung lũng chết,Đồi cây Fide,Khe núi tử thần,Núi đá,Rừng đá,Lãnh  địa Fize,Núi khỉ đỏ,Núi khỉ vàng,Hang quỷ chim,Núi khỉ đen,Hang khỉ đen,Siêu Thị,Hành tinh M-2,Hành tinh Polaris,Hành tinh Cretaceous,Hành tinh Monmaasu,Hành tinh Rudeeze,Hành tinh Gelbo,Hành tinh Tigere,Thành phố phía đông,Thành phố phía nam,Đảo Balê,95,Cao nguyên,Thành phố phía bắc,Ngọn núi phía bắc,Thung lũng phía bắc,Thị trấn Ginder,101,Nhà Bunma,Võ đài Xên bọ hung,Sân sau siêu thị,Cánh đồng tuyết,Rừng tuyết,Núi tuyết,Dòng sông băng,Rừng băng,Hang băng,Đông Nam Karin,Võ đài Hạt Mít,Đại hội võ thuật,Cổng phi thuyền,Phòng chờ,Thánh địa Kaio,Cửa Ải 1,Cửa Ải 2,Cửa Ải 3,Phòng chỉ huy,Đấu trường,Ngũ Hành Sơn,Ngũ Hành Sơn,Ngũ Hành Sơn,Võ đài Bang,Thành phố Santa,Cổng phi thuyền,Bụng Mabư,Đại hội võ thuật,Đại hội võ thuật Vũ Trụ,Hành Tinh Yardart,Hành Tinh Yardart 2,Hành Tinh Yardart 3,Đại hội võ thuật Vũ Trụ 6-7,Động hải tặc,Hang Bạch Tuộc,Động kho báu,Cảng hải tặc,Hành tinh Potaufeu,Hang động Potaufeu,Con đường rắn độc,Con đường rắn độc,Con đường rắn độc,Hoang mạc,Võ Đài Siêu Cấp,Tây Karin,Sa mạc,Lâu đài Lychee,Thành phố Santa,Lôi Đài,Hành tinh bóng tối,Vùng đất băng giá,Lãnh địa bang hội,Hành tinh Bill,Hành tinh ngục tù,Tây thánh địa,Đông thánh Địa,Bắc thánh địa,Nam thánh Địa,Khu hang động,Bìa rừng nguyên thủy,Rừng nguyên thủy,Làng Plant nguyên thủy,Tranh ngọc Namếc";

		// Token: 0x0400247F RID: 9343
		public static readonly string[] mapNames = XmapController.listMap.Split(new char[]
		{
			','
		});

		// Token: 0x04002480 RID: 9344
		public static int VuDangMapNext;

		// Token: 0x04002481 RID: 9345
		private static int step;

		// Token: 0x04002482 RID: 9346
		private static readonly XmapController _Instance = new XmapController();

		// Token: 0x04002483 RID: 9347
		public static int IdMapEnd;

		// Token: 0x04002484 RID: 9348
		private static List<int> WayXmap;

		// Token: 0x04002485 RID: 9349
		private static int IndexWay;

		// Token: 0x04002486 RID: 9350
		private static bool IsNextMapFailed;

		// Token: 0x04002487 RID: 9351
		private static bool IsWait;

		// Token: 0x04002488 RID: 9352
		private static long TimeStartWait;

		// Token: 0x04002489 RID: 9353
		private static long TimeWait;

		// Token: 0x0400248A RID: 9354
		private static bool IsWaitNextMap;
	}
}
