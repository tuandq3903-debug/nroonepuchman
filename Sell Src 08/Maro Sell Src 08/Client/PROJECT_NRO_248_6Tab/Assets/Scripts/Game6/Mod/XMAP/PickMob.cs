using System;
using System.Collections.Generic;
using System.Threading;

namespace Game6.Mod.XMAP
{
	// Token: 0x020000CC RID: 204
	public class PickMob
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x0008FAD1 File Offset: 0x0008DCD1
		public static void Update()
		{
			PickMobController.Update();
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0008FAD8 File Offset: 0x0008DCD8
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

		// Token: 0x040011D1 RID: 4561
		private static readonly sbyte[] IdSkillsBase = new sbyte[]
		{
			0,
			2,
			17,
			4,
			13
		};

		// Token: 0x040011D2 RID: 4562
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

		// Token: 0x040011D3 RID: 4563
		public static bool tanSat = false;

		// Token: 0x040011D4 RID: 4564
		public static bool tsPlayer = false;

		// Token: 0x040011D5 RID: 4565
		public static bool neSieuQuai = false;

		// Token: 0x040011D6 RID: 4566
		public static bool vuotDiaHinh = true;

		// Token: 0x040011D7 RID: 4567
		public static bool telePem = true;

		// Token: 0x040011D8 RID: 4568
		public static bool isGoBack;

		// Token: 0x040011D9 RID: 4569
		public static int mapGoback;

		// Token: 0x040011DA RID: 4570
		public static int zoneGoback;

		// Token: 0x040011DB RID: 4571
		public static int xGoback;

		// Token: 0x040011DC RID: 4572
		public static int yGoback;

		// Token: 0x040011DD RID: 4573
		public static List<int> IdMobsTanSat = new List<int>();

		// Token: 0x040011DE RID: 4574
		public static List<int> TypeMobsTanSat = new List<int>();

		// Token: 0x040011DF RID: 4575
		public static List<sbyte> IdSkillsTanSat = new List<sbyte>(PickMob.IdSkillsBase);

		// Token: 0x040011E0 RID: 4576
		public static bool IsAutoPickItems = true;

		// Token: 0x040011E1 RID: 4577
		public static bool IsPickItemsAll = true;

		// Token: 0x040011E2 RID: 4578
		public static bool IsPickItemsDis = false;

		// Token: 0x040011E3 RID: 4579
		public static bool IsLimitTimesPickItem = true;

		// Token: 0x040011E4 RID: 4580
		public static int TimesAutoPickItemMax = 20;

		// Token: 0x040011E5 RID: 4581
		public static List<short> IdItemPicks = new List<short>();

		// Token: 0x040011E6 RID: 4582
		public static List<short> IdItemBlocks = new List<short>(PickMob.IdItemBlockBase);

		// Token: 0x040011E7 RID: 4583
		public static List<sbyte> TypeItemPicks = new List<sbyte>();

		// Token: 0x040011E8 RID: 4584
		public static List<sbyte> TypeItemBlock = new List<sbyte>();

		// Token: 0x040011E9 RID: 4585
		public static int HpBuff = 0;

		// Token: 0x040011EA RID: 4586
		public static int MpBuff = 0;
	}
}
