using System;
using System.Collections.Generic;
using System.Threading;

namespace Game2.Mod.XMAP
{
	// Token: 0x0200042C RID: 1068
	public class PickMob
	{
		// Token: 0x06002F97 RID: 12183 RVA: 0x002E3DB1 File Offset: 0x002E1FB1
		public static void Update()
		{
			PickMobController.Update();
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x002E3DB8 File Offset: 0x002E1FB8
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

		// Token: 0x04005BCD RID: 23501
		private static readonly sbyte[] IdSkillsBase = new sbyte[]
		{
			0,
			2,
			17,
			4,
			13
		};

		// Token: 0x04005BCE RID: 23502
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

		// Token: 0x04005BCF RID: 23503
		public static bool tanSat = false;

		// Token: 0x04005BD0 RID: 23504
		public static bool tsPlayer = false;

		// Token: 0x04005BD1 RID: 23505
		public static bool neSieuQuai = false;

		// Token: 0x04005BD2 RID: 23506
		public static bool vuotDiaHinh = true;

		// Token: 0x04005BD3 RID: 23507
		public static bool telePem = true;

		// Token: 0x04005BD4 RID: 23508
		public static bool isGoBack;

		// Token: 0x04005BD5 RID: 23509
		public static int mapGoback;

		// Token: 0x04005BD6 RID: 23510
		public static int zoneGoback;

		// Token: 0x04005BD7 RID: 23511
		public static int xGoback;

		// Token: 0x04005BD8 RID: 23512
		public static int yGoback;

		// Token: 0x04005BD9 RID: 23513
		public static List<int> IdMobsTanSat = new List<int>();

		// Token: 0x04005BDA RID: 23514
		public static List<int> TypeMobsTanSat = new List<int>();

		// Token: 0x04005BDB RID: 23515
		public static List<sbyte> IdSkillsTanSat = new List<sbyte>(PickMob.IdSkillsBase);

		// Token: 0x04005BDC RID: 23516
		public static bool IsAutoPickItems = true;

		// Token: 0x04005BDD RID: 23517
		public static bool IsPickItemsAll = true;

		// Token: 0x04005BDE RID: 23518
		public static bool IsPickItemsDis = false;

		// Token: 0x04005BDF RID: 23519
		public static bool IsLimitTimesPickItem = true;

		// Token: 0x04005BE0 RID: 23520
		public static int TimesAutoPickItemMax = 20;

		// Token: 0x04005BE1 RID: 23521
		public static List<short> IdItemPicks = new List<short>();

		// Token: 0x04005BE2 RID: 23522
		public static List<short> IdItemBlocks = new List<short>(PickMob.IdItemBlockBase);

		// Token: 0x04005BE3 RID: 23523
		public static List<sbyte> TypeItemPicks = new List<sbyte>();

		// Token: 0x04005BE4 RID: 23524
		public static List<sbyte> TypeItemBlock = new List<sbyte>();

		// Token: 0x04005BE5 RID: 23525
		public static int HpBuff = 0;

		// Token: 0x04005BE6 RID: 23526
		public static int MpBuff = 0;
	}
}
