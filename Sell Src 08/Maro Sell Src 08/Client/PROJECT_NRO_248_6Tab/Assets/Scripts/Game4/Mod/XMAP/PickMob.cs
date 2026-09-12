using System;
using System.Collections.Generic;
using System.Threading;

namespace Game4.Mod.XMAP
{
	// Token: 0x0200027C RID: 636
	public class PickMob
	{
		// Token: 0x06001C4F RID: 7247 RVA: 0x001B9C69 File Offset: 0x001B7E69
		public static void Update()
		{
			PickMobController.Update();
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x001B9C70 File Offset: 0x001B7E70
		public static void GoBack()
		{
			Thread.Sleep(5000);
			if (!GameScr.gI().magicTree.isUpdate && GameScr.gI().magicTree.currPeas > 0 && TileMap.mapID == Char.myCharz().cgender + 21)
			{
				Service.gI().magicTree(1);
				Thread.Sleep(500);
				GameCanvas.gI().keyPressedz(-5);
				Thread.Sleep(1000);
			}
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				Char.myCharz().cx = itemMap.x;
				Service.gI().charMove();
				Thread.Sleep(1000);
				Service.gI().pickItem(itemMap.itemMapID);
				Thread.Sleep(1000);
			}
			XmapController.StartRunToMapId(PickMob.mapGoback);
			while (TileMap.mapID != PickMob.mapGoback)
			{
				Thread.Sleep(200);
			}
			while (TileMap.zoneID != PickMob.zoneGoback)
			{
				Thread.Sleep(1000);
				Service.gI().requestChangeZone(PickMob.zoneGoback, -1);
			}
			Thread.Sleep(2000);
			ModFunc.GI().MoveTo(PickMob.xGoback, PickMob.yGoback);
			GameScr.isAutoPlay = true;
		}

		// Token: 0x040036CF RID: 14031
		private static readonly sbyte[] IdSkillsBase = new sbyte[]
		{
			0,
			2,
			17,
			4,
			13
		};

		// Token: 0x040036D0 RID: 14032
		public static readonly short[] IdItemBlockBase = new short[]
		{
			225,
			353,
			354,
			355,
			356,
			357,
			358,
			359,
			360,
			362
		};

		// Token: 0x040036D1 RID: 14033
		public static bool tanSat = false;

		// Token: 0x040036D2 RID: 14034
		public static bool tsPlayer = false;

		// Token: 0x040036D3 RID: 14035
		public static bool neSieuQuai = false;

		// Token: 0x040036D4 RID: 14036
		public static bool vuotDiaHinh = true;

		// Token: 0x040036D5 RID: 14037
		public static bool telePem = true;

		// Token: 0x040036D6 RID: 14038
		public static bool isGoBack;

		// Token: 0x040036D7 RID: 14039
		public static int mapGoback;

		// Token: 0x040036D8 RID: 14040
		public static int zoneGoback;

		// Token: 0x040036D9 RID: 14041
		public static int xGoback;

		// Token: 0x040036DA RID: 14042
		public static int yGoback;

		// Token: 0x040036DB RID: 14043
		public static List<int> IdMobsTanSat = new List<int>();

		// Token: 0x040036DC RID: 14044
		public static List<int> TypeMobsTanSat = new List<int>();

		// Token: 0x040036DD RID: 14045
		public static List<sbyte> IdSkillsTanSat = new List<sbyte>(PickMob.IdSkillsBase);

		// Token: 0x040036DE RID: 14046
		public static bool IsAutoPickItems = true;

		// Token: 0x040036DF RID: 14047
		public static bool IsPickItemsAll = true;

		// Token: 0x040036E0 RID: 14048
		public static bool IsPickItemsDis = false;

		// Token: 0x040036E1 RID: 14049
		public static bool IsLimitTimesPickItem = true;

		// Token: 0x040036E2 RID: 14050
		public static int TimesAutoPickItemMax = 20;

		// Token: 0x040036E3 RID: 14051
		public static List<short> IdItemPicks = new List<short>();

		// Token: 0x040036E4 RID: 14052
		public static List<short> IdItemBlocks = new List<short>(PickMob.IdItemBlockBase);

		// Token: 0x040036E5 RID: 14053
		public static List<sbyte> TypeItemPicks = new List<sbyte>();

		// Token: 0x040036E6 RID: 14054
		public static List<sbyte> TypeItemBlock = new List<sbyte>();

		// Token: 0x040036E7 RID: 14055
		public static int HpBuff = 0;

		// Token: 0x040036E8 RID: 14056
		public static int MpBuff = 0;
	}
}
