using System;
using System.Collections.Generic;
using System.Text;

namespace Game3.Mod.XMAP
{
	// Token: 0x0200035A RID: 858
	public class XmapData
	{
		// Token: 0x0600262A RID: 9770 RVA: 0x002506B0 File Offset: 0x0024E8B0
		private XmapData()
		{
			this.GroupMaps = new List<GroupMap>();
			this.MyLinkMaps = null;
			this.IsLoading = false;
			this.IsLoadingCapsule = false;
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0025093D File Offset: 0x0024EB3D
		public static XmapData GI()
		{
			if (XmapData.Instance == null)
			{
				XmapData.Instance = new XmapData();
			}
			return XmapData.Instance;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00250955 File Offset: 0x0024EB55
		public void LoadLinkMaps()
		{
			this.IsLoading = true;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x00250960 File Offset: 0x0024EB60
		public void Update()
		{
			if (this.IsLoadingCapsule)
			{
				if (!this.IsWaitInfoMapTrans())
				{
					this.LoadLinkMapCapsule();
					this.IsLoadingCapsule = false;
					this.IsLoading = false;
				}
				return;
			}
			this.LoadLinkMapBase();
			if (XmapData.CanUseCapsuleVip())
			{
				XmapController.UseCapsuleVip();
				this.IsLoadingCapsule = true;
				return;
			}
			if (XmapData.CanUseCapsuleNormal())
			{
				XmapController.UseCapsuleNormal();
				this.IsLoadingCapsule = true;
				return;
			}
			this.IsLoading = false;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x002509C8 File Offset: 0x0024EBC8
		public void LoadGroupMapsFromFile()
		{
			GroupMapData map = new GroupMapData(new List<int>
			{
				44,
				23,
				14,
				15,
				16,
				17,
				18,
				20,
				19,
				35,
				36,
				37,
				38,
				26,
				52,
				84,
				129
			}, "Xayda");
			GroupMapData map2 = new GroupMapData(new List<int>
			{
				42,
				21,
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				27,
				28,
				29,
				30,
				47,
				46,
				45,
				48,
				50,
				111,
				24,
				53,
				58,
				59,
				60,
				61,
				62,
				55,
				56,
				54,
				57
			}, "Trái Đất");
			GroupMapData map3 = new GroupMapData(new List<int>
			{
				43,
				22,
				7,
				8,
				9,
				11,
				12,
				13,
				10,
				31,
				32,
				33,
				34,
				25
			}, "Namek");
			GroupMapData map4 = new GroupMapData(new List<int>
			{
				68,
				69,
				70,
				71,
				72,
				64,
				65,
				63,
				66,
				67,
				73,
				74,
				75,
				76,
				77,
				81,
				82,
				83,
				79,
				80,
				131,
				132,
				133
			}, "Nappa");
			GroupMapData map5 = new GroupMapData(new List<int>
			{
				102,
				92,
				93,
				94,
				96,
				97,
				98,
				99,
				100,
				103
			}, "Tương Lai");
			GroupMapData map6 = new GroupMapData(new List<int>
			{
				109,
				108,
				107,
				110,
				106,
				105
			}, "Cold");
			GroupMapData map7 = new GroupMapData(new List<int>
			{
				122,
				123,
				124
			}, "Ngũ Hành Sơn");
			this.GroupMaps.Clear();
			try
			{
				this.GroupMaps.Add(new GroupMap(map.mapname, map.datamap));
				this.GroupMaps.Add(new GroupMap(map2.mapname, map2.datamap));
				this.GroupMaps.Add(new GroupMap(map3.mapname, map3.datamap));
				this.GroupMaps.Add(new GroupMap(map4.mapname, map4.datamap));
				this.GroupMaps.Add(new GroupMap(map5.mapname, map5.datamap));
				this.GroupMaps.Add(new GroupMap(map6.mapname, map6.datamap));
				this.GroupMaps.Add(new GroupMap(map7.mapname, map7.datamap));
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
			this.RemoveMapsHomeInGroupMaps();
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00250E88 File Offset: 0x0024F088
		private void RemoveMapsHomeInGroupMaps()
		{
			int cgender = Char.myCharz().cgender;
			foreach (GroupMap groupMap in this.GroupMaps)
			{
				if (cgender != 0)
				{
					if (cgender != 1)
					{
						groupMap.IdMaps.Remove(21);
						groupMap.IdMaps.Remove(22);
					}
					else
					{
						groupMap.IdMaps.Remove(21);
						groupMap.IdMaps.Remove(23);
					}
				}
				else
				{
					groupMap.IdMaps.Remove(22);
					groupMap.IdMaps.Remove(23);
				}
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x00250F40 File Offset: 0x0024F140
		private void LoadLinkMapCapsule()
		{
			this.AddKeyLinkMaps(TileMap.mapID);
			string[] mapNames = GameCanvas.panel.mapNames;
			for (int i = 0; i < mapNames.Length; i++)
			{
				int idMapFromName = XmapData.GetIdMapFromName(mapNames[i]);
				if (idMapFromName != -1)
				{
					int[] info = new int[]
					{
						i
					};
					this.MyLinkMaps[TileMap.mapID].Add(new MapNext(idMapFromName, TypeMapNext.Capsule, info));
				}
			}
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x00250FA6 File Offset: 0x0024F1A6
		private void LoadLinkMapBase()
		{
			this.MyLinkMaps = new Dictionary<int, List<MapNext>>();
			this.LoadLinkMapsFromFile();
			this.LoadLinkMapsAutoWaypointFromFile();
			this.LoadLinkMapsHome();
			this.LoadLinkMapSieuThi();
			this.LoadLinkMapToCold();
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00250FD4 File Offset: 0x0024F1D4
		private void LoadLinkMapsFromFile()
		{
			try
			{
				for (int i = 0; i < this.DataXMap.Length; i++)
				{
					string text;
					if ((text = this.DataXMap[i]) != null)
					{
						text = text.Trim();
						if (!text.StartsWith("#") && !text.Equals(""))
						{
							int[] array = Array.ConvertAll<string, int>(text.Split(new char[]
							{
								' '
							}), (string s) => int.Parse(s));
							int num = array.Length - 3;
							int[] array2 = new int[num];
							Array.Copy(array, 3, array2, 0, num);
							this.LoadLinkMap(array[0], array[1], (TypeMapNext)array[2], array2);
						}
					}
				}
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x002510B4 File Offset: 0x0024F2B4
		private void LoadLinkMapsAutoWaypointFromFile()
		{
			try
			{
				for (int z = 0; z < this.DataWayPoint.Length; z++)
				{
					string text;
					if ((text = this.DataWayPoint[z]) != null)
					{
						text = text.Trim();
						if (!text.StartsWith("#") && !text.Equals(""))
						{
							int[] array = Array.ConvertAll<string, int>(text.Split(new char[]
							{
								' '
							}), (string s) => int.Parse(s));
							for (int i = 0; i < array.Length; i++)
							{
								if (i != 0)
								{
									this.LoadLinkMap(array[i], array[i - 1], TypeMapNext.AutoWaypoint, null);
								}
								if (i != array.Length - 1)
								{
									this.LoadLinkMap(array[i], array[i + 1], TypeMapNext.AutoWaypoint, null);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x002511A4 File Offset: 0x0024F3A4
		private void LoadLinkMapsHome()
		{
			int cgender = Char.myCharz().cgender;
			int num = 21 + cgender;
			int num2 = 7 * cgender;
			this.LoadLinkMap(num2, num, TypeMapNext.AutoWaypoint, null);
			this.LoadLinkMap(num, num2, TypeMapNext.AutoWaypoint, null);
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x002511DC File Offset: 0x0024F3DC
		private void LoadLinkMapSieuThi()
		{
			int cgender = Char.myCharz().cgender;
			int idMapNext = 24 + cgender;
			int[] array = new int[2];
			array[0] = 10;
			int[] info = array;
			this.LoadLinkMap(84, idMapNext, TypeMapNext.NpcMenu, info);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00251210 File Offset: 0x0024F410
		private void LoadLinkMapToCold()
		{
			if (Char.myCharz().taskMaint.taskId > 30)
			{
				int[] array = new int[2];
				array[0] = 12;
				int[] info = array;
				this.LoadLinkMap(19, 109, TypeMapNext.NpcMenu, info);
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00251248 File Offset: 0x0024F448
		public List<MapNext> GetMapNexts(int idMap)
		{
			if (this.CanGetMapNexts(idMap))
			{
				return this.MyLinkMaps[idMap];
			}
			return null;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00251261 File Offset: 0x0024F461
		public bool CanGetMapNexts(int idMap)
		{
			return this.MyLinkMaps.ContainsKey(idMap);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00251270 File Offset: 0x0024F470
		private void LoadLinkMap(int idMapStart, int idMapNext, TypeMapNext type, int[] info)
		{
			this.AddKeyLinkMaps(idMapStart);
			MapNext item = new MapNext(idMapNext, type, info);
			this.MyLinkMaps[idMapStart].Add(item);
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x002512A1 File Offset: 0x0024F4A1
		private void AddKeyLinkMaps(int idMap)
		{
			if (!this.MyLinkMaps.ContainsKey(idMap))
			{
				this.MyLinkMaps.Add(idMap, new List<MapNext>());
			}
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x002512C2 File Offset: 0x0024F4C2
		private bool IsWaitInfoMapTrans()
		{
			return !AutoXmap.IsShowPanelMapTrans;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000920B0 File Offset: 0x000902B0
		public static int GetIdMapFromPanelXmap(string mapName)
		{
			return int.Parse(mapName.Split(new char[]
			{
				'['
			})[1].Trim(']'));
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x002512CC File Offset: 0x0024F4CC
		public static Waypoint FindWaypoint(int idMap)
		{
			for (int i = 0; i < TileMap.vGo.size(); i++)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				if (XmapController.IdMapEnd == 124 && TileMap.mapID == 123)
				{
					for (int j = 0; j < TileMap.vGo.size(); j++)
					{
						Waypoint result = (Waypoint)TileMap.vGo.elementAt(j);
						if (j == TileMap.vGo.size() - 1)
						{
							return result;
						}
					}
				}
				if (XmapData.GetTextPopup(waypoint.popup).Trim().ToLower().Equals(XmapController.GetMapName(idMap).Trim().ToLower()))
				{
					return waypoint;
				}
			}
			return null;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x0024E96D File Offset: 0x0024CB6D
		public static int GetPosWaypointX(Waypoint waypoint)
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

		// Token: 0x0600263F RID: 9791 RVA: 0x0025137D File Offset: 0x0024F57D
		public static int GetPosWaypointY(Waypoint waypoint)
		{
			return (int)waypoint.maxY;
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00251385 File Offset: 0x0024F585
		public static bool IsMyCharDie()
		{
			return Char.myCharz().statusMe == 14 || Char.myCharz().cHP <= 0L;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x002513A8 File Offset: 0x0024F5A8
		public static bool CanNextMap()
		{
			return !Char.isLoadingMap && !Char.ischangingMap && !Controller.isStopReadMessage;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x002513C4 File Offset: 0x0024F5C4
		private static int GetIdMapFromName(string mapName)
		{
			int cgender = Char.myCharz().cgender;
			if (mapName.Equals("Về nhà"))
			{
				return 21 + cgender;
			}
			if (mapName.Equals("Trạm tàu vũ trụ"))
			{
				return 24 + cgender;
			}
			if (mapName.Contains("Về chỗ cũ: "))
			{
				mapName = mapName.Replace("Về chỗ cũ: ", "");
				if (XmapController.GetMapName(AutoXmap.IdMapCapsuleReturn).Equals(mapName))
				{
					return AutoXmap.IdMapCapsuleReturn;
				}
				if (mapName.Equals("Rừng đá"))
				{
					return -1;
				}
			}
			for (int i = 0; i < TileMap.mapNames.Length; i++)
			{
				if (mapName.Equals(XmapController.GetMapName(i)))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x0025146C File Offset: 0x0024F66C
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

		// Token: 0x06002644 RID: 9796 RVA: 0x002514B8 File Offset: 0x0024F6B8
		private static bool CanUseCapsuleNormal()
		{
			return !XmapData.IsMyCharDie() && AutoXmap.IsUseCapsuleNormal && XmapData.HasItemCapsuleNormal();
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x002514D0 File Offset: 0x0024F6D0
		private static bool HasItemCapsuleNormal()
		{
			Item[] arrItemBag = Char.myCharz().arrItemBag;
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				if (arrItemBag[i] != null && arrItemBag[i].template.id == 193)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00251512 File Offset: 0x0024F712
		private static bool CanUseCapsuleVip()
		{
			return !XmapData.IsMyCharDie() && AutoXmap.IsUseCapsuleVip && XmapData.HasItemCapsuleVip();
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0025152C File Offset: 0x0024F72C
		private static bool HasItemCapsuleVip()
		{
			Item[] arrItemBag = Char.myCharz().arrItemBag;
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				if (arrItemBag[i] != null && arrItemBag[i].template.id == 194)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004989 RID: 18825
		public string[] DataXMap = new string[]
		{
			"24 25 1 10 0",
			"25 24 1 11 0",
			"45 48 1 19 3",
			"48 45 1 20 3 0",
			"48 50 1 20 3 1",
			"50 48 1 44 0",
			"24 26 1 10 1",
			"26 24 1 12 0",
			"25 26 1 11 1",
			"26 25 1 12 1",
			"24 84 1 10 2",
			"25 84 1 11 2",
			"26 84 1 12 2",
			"19 68 1 12 1",
			"68 19 1 12 0",
			"80 131 1 60 0",
			"27 102 1 38 1",
			"28 102 1 38 1",
			"29 102 1 38 1",
			"102 24 1 38 1",
			"27 53 1 25 0",
			"52 129 1 23 3",
			"0 149 1 67 3 0",
			"45 48 1 19 3",
			"50 48 1 44 0",
			"139 25 1 63 1",
			"139 26 1 63 2",
			"24 139 1 63 0",
			"139 24 1 63 0",
			"19 126 1 53 0",
			"126 19 1 53 0",
			"24 139 1 63 0",
			"139 24 1 63 0",
			"19 126 1 53 0",
			"126 19 1 53 0",
			"0 122 1 49 0",
			"80 160 1 60 0"
		};

		// Token: 0x0400498A RID: 18826
		public string[] DataWayPoint = new string[]
		{
			"42 0 1 2 3 4 5 6",
			"3 27 28 29 30",
			"2 24",
			"1 47 46 45",
			"5 29",
			"47 111",
			"53 58 59 60 61 62 55 56 54 57",
			"53 27",
			"43 7 8 9 11 12 13 10",
			"11 31 32 33 34",
			"9 25",
			"13 33",
			"52 44 14 15 16 17 18 20 19",
			"17 35 36 37 38",
			"16 26",
			"20 37",
			"68 69 70 71 72 64 65 63 66 67 73 74 75 76 77 81 82 83 79 80",
			"102 92 93 94 96 97 98 99 100 103",
			"109 108 107 110 106",
			"109 105",
			"109 106",
			"106 107",
			"108 105",
			"131 132 133",
			"80 105",
			"160 161 162 163",
			"139 140",
			"149 147 152 151 148",
			"122 123 124"
		};

		// Token: 0x0400498B RID: 18827
		public List<GroupMap> GroupMaps;

		// Token: 0x0400498C RID: 18828
		public Dictionary<int, List<MapNext>> MyLinkMaps;

		// Token: 0x0400498D RID: 18829
		public bool IsLoading;

		// Token: 0x0400498E RID: 18830
		private bool IsLoadingCapsule;

		// Token: 0x0400498F RID: 18831
		private static XmapData Instance;
	}
}
