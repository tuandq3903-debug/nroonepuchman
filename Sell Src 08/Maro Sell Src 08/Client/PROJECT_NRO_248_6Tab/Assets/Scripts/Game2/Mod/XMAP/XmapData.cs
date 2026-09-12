using System;
using System.Collections.Generic;
using System.Text;

namespace Game2.Mod.XMAP
{
	// Token: 0x02000432 RID: 1074
	public class XmapData
	{
		// Token: 0x06002FCE RID: 12238 RVA: 0x002E5754 File Offset: 0x002E3954
		private XmapData()
		{
			this.GroupMaps = new List<GroupMap>();
			this.MyLinkMaps = null;
			this.IsLoading = false;
			this.IsLoadingCapsule = false;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x002E59E1 File Offset: 0x002E3BE1
		public static XmapData GI()
		{
			if (XmapData.Instance == null)
			{
				XmapData.Instance = new XmapData();
			}
			return XmapData.Instance;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x002E59F9 File Offset: 0x002E3BF9
		public void LoadLinkMaps()
		{
			this.IsLoading = true;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x002E5A04 File Offset: 0x002E3C04
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

		// Token: 0x06002FD2 RID: 12242 RVA: 0x002E5A6C File Offset: 0x002E3C6C
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

		// Token: 0x06002FD3 RID: 12243 RVA: 0x002E5F2C File Offset: 0x002E412C
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

		// Token: 0x06002FD4 RID: 12244 RVA: 0x002E5FE4 File Offset: 0x002E41E4
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

		// Token: 0x06002FD5 RID: 12245 RVA: 0x002E604A File Offset: 0x002E424A
		private void LoadLinkMapBase()
		{
			this.MyLinkMaps = new Dictionary<int, List<MapNext>>();
			this.LoadLinkMapsFromFile();
			this.LoadLinkMapsAutoWaypointFromFile();
			this.LoadLinkMapsHome();
			this.LoadLinkMapSieuThi();
			this.LoadLinkMapToCold();
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x002E6078 File Offset: 0x002E4278
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

		// Token: 0x06002FD7 RID: 12247 RVA: 0x002E6158 File Offset: 0x002E4358
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

		// Token: 0x06002FD8 RID: 12248 RVA: 0x002E6248 File Offset: 0x002E4448
		private void LoadLinkMapsHome()
		{
			int cgender = Char.myCharz().cgender;
			int num = 21 + cgender;
			int num2 = 7 * cgender;
			this.LoadLinkMap(num2, num, TypeMapNext.AutoWaypoint, null);
			this.LoadLinkMap(num, num2, TypeMapNext.AutoWaypoint, null);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x002E6280 File Offset: 0x002E4480
		private void LoadLinkMapSieuThi()
		{
			int cgender = Char.myCharz().cgender;
			int idMapNext = 24 + cgender;
			int[] array = new int[2];
			array[0] = 10;
			int[] info = array;
			this.LoadLinkMap(84, idMapNext, TypeMapNext.NpcMenu, info);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x002E62B4 File Offset: 0x002E44B4
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

		// Token: 0x06002FDB RID: 12251 RVA: 0x002E62EC File Offset: 0x002E44EC
		public List<MapNext> GetMapNexts(int idMap)
		{
			if (this.CanGetMapNexts(idMap))
			{
				return this.MyLinkMaps[idMap];
			}
			return null;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x002E6305 File Offset: 0x002E4505
		public bool CanGetMapNexts(int idMap)
		{
			return this.MyLinkMaps.ContainsKey(idMap);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x002E6314 File Offset: 0x002E4514
		private void LoadLinkMap(int idMapStart, int idMapNext, TypeMapNext type, int[] info)
		{
			this.AddKeyLinkMaps(idMapStart);
			MapNext item = new MapNext(idMapNext, type, info);
			this.MyLinkMaps[idMapStart].Add(item);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x002E6345 File Offset: 0x002E4545
		private void AddKeyLinkMaps(int idMap)
		{
			if (!this.MyLinkMaps.ContainsKey(idMap))
			{
				this.MyLinkMaps.Add(idMap, new List<MapNext>());
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x002E6366 File Offset: 0x002E4566
		private bool IsWaitInfoMapTrans()
		{
			return !AutoXmap.IsShowPanelMapTrans;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000920B0 File Offset: 0x000902B0
		public static int GetIdMapFromPanelXmap(string mapName)
		{
			return int.Parse(mapName.Split(new char[]
			{
				'['
			})[1].Trim(']'));
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x002E6370 File Offset: 0x002E4570
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

		// Token: 0x06002FE2 RID: 12258 RVA: 0x002E3A11 File Offset: 0x002E1C11
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

		// Token: 0x06002FE3 RID: 12259 RVA: 0x002E6421 File Offset: 0x002E4621
		public static int GetPosWaypointY(Waypoint waypoint)
		{
			return (int)waypoint.maxY;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x002E6429 File Offset: 0x002E4629
		public static bool IsMyCharDie()
		{
			return Char.myCharz().statusMe == 14 || Char.myCharz().cHP <= 0L;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x002E644C File Offset: 0x002E464C
		public static bool CanNextMap()
		{
			return !Char.isLoadingMap && !Char.ischangingMap && !Controller.isStopReadMessage;
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x002E6468 File Offset: 0x002E4668
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

		// Token: 0x06002FE7 RID: 12263 RVA: 0x002E6510 File Offset: 0x002E4710
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

		// Token: 0x06002FE8 RID: 12264 RVA: 0x002E655C File Offset: 0x002E475C
		private static bool CanUseCapsuleNormal()
		{
			return !XmapData.IsMyCharDie() && AutoXmap.IsUseCapsuleNormal && XmapData.HasItemCapsuleNormal();
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x002E6574 File Offset: 0x002E4774
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

		// Token: 0x06002FEA RID: 12266 RVA: 0x002E65B6 File Offset: 0x002E47B6
		private static bool CanUseCapsuleVip()
		{
			return !XmapData.IsMyCharDie() && AutoXmap.IsUseCapsuleVip && XmapData.HasItemCapsuleVip();
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x002E65D0 File Offset: 0x002E47D0
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

		// Token: 0x04005C08 RID: 23560
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

		// Token: 0x04005C09 RID: 23561
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

		// Token: 0x04005C0A RID: 23562
		public List<GroupMap> GroupMaps;

		// Token: 0x04005C0B RID: 23563
		public Dictionary<int, List<MapNext>> MyLinkMaps;

		// Token: 0x04005C0C RID: 23564
		public bool IsLoading;

		// Token: 0x04005C0D RID: 23565
		private bool IsLoadingCapsule;

		// Token: 0x04005C0E RID: 23566
		private static XmapData Instance;
	}
}
