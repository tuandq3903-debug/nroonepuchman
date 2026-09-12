using System;
using System.Collections.Generic;
using System.Threading;

namespace Game5.Mod.XMAP
{
	// Token: 0x020001A4 RID: 420
	public class PickMob
	{
		// Token: 0x060012AB RID: 4779 RVA: 0x00124BC5 File Offset: 0x00122DC5
		public static void Update()
		{
			PickMobController.Update();
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00124BCC File Offset: 0x00122DCC
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

		// Token: 0x04002450 RID: 9296
		private static readonly sbyte[] IdSkillsBase = new sbyte[]
		{
			0,
			2,
			17,
			4,
			13
		};

		// Token: 0x04002451 RID: 9297
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

		// Token: 0x04002452 RID: 9298
		public static bool tanSat = false;

		// Token: 0x04002453 RID: 9299
		public static bool tsPlayer = false;

		// Token: 0x04002454 RID: 9300
		public static bool neSieuQuai = false;

		// Token: 0x04002455 RID: 9301
		public static bool vuotDiaHinh = true;

		// Token: 0x04002456 RID: 9302
		public static bool telePem = true;

		// Token: 0x04002457 RID: 9303
		public static bool isGoBack;

		// Token: 0x04002458 RID: 9304
		public static int mapGoback;

		// Token: 0x04002459 RID: 9305
		public static int zoneGoback;

		// Token: 0x0400245A RID: 9306
		public static int xGoback;

		// Token: 0x0400245B RID: 9307
		public static int yGoback;

		// Token: 0x0400245C RID: 9308
		public static List<int> IdMobsTanSat = new List<int>();

		// Token: 0x0400245D RID: 9309
		public static List<int> TypeMobsTanSat = new List<int>();

		// Token: 0x0400245E RID: 9310
		public static List<sbyte> IdSkillsTanSat = new List<sbyte>(PickMob.IdSkillsBase);

		// Token: 0x0400245F RID: 9311
		public static bool IsAutoPickItems = true;

		// Token: 0x04002460 RID: 9312
		public static bool IsPickItemsAll = true;

		// Token: 0x04002461 RID: 9313
		public static bool IsPickItemsDis = false;

		// Token: 0x04002462 RID: 9314
		public static bool IsLimitTimesPickItem = true;

		// Token: 0x04002463 RID: 9315
		public static int TimesAutoPickItemMax = 20;

		// Token: 0x04002464 RID: 9316
		public static List<short> IdItemPicks = new List<short>();

		// Token: 0x04002465 RID: 9317
		public static List<short> IdItemBlocks = new List<short>(PickMob.IdItemBlockBase);

		// Token: 0x04002466 RID: 9318
		public static List<sbyte> TypeItemPicks = new List<sbyte>();

		// Token: 0x04002467 RID: 9319
		public static List<sbyte> TypeItemBlock = new List<sbyte>();

		// Token: 0x04002468 RID: 9320
		public static int HpBuff = 0;

		// Token: 0x04002469 RID: 9321
		public static int MpBuff = 0;
	}
}
