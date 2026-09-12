using System;
using System.Collections.Generic;
using System.Threading;

namespace Game1.Mod.XMAP
{
	// Token: 0x02000504 RID: 1284
	public class PickMob
	{
		// Token: 0x0600393B RID: 14651 RVA: 0x00378E55 File Offset: 0x00377055
		public static void Update()
		{
			PickMobController.Update();
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x00378E5C File Offset: 0x0037705C
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

		// Token: 0x04006E4C RID: 28236
		private static readonly sbyte[] IdSkillsBase = new sbyte[]
		{
			0,
			2,
			17,
			4,
			13
		};

		// Token: 0x04006E4D RID: 28237
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

		// Token: 0x04006E4E RID: 28238
		public static bool tanSat = false;

		// Token: 0x04006E4F RID: 28239
		public static bool tsPlayer = false;

		// Token: 0x04006E50 RID: 28240
		public static bool neSieuQuai = false;

		// Token: 0x04006E51 RID: 28241
		public static bool vuotDiaHinh = true;

		// Token: 0x04006E52 RID: 28242
		public static bool telePem = true;

		// Token: 0x04006E53 RID: 28243
		public static bool isGoBack;

		// Token: 0x04006E54 RID: 28244
		public static int mapGoback;

		// Token: 0x04006E55 RID: 28245
		public static int zoneGoback;

		// Token: 0x04006E56 RID: 28246
		public static int xGoback;

		// Token: 0x04006E57 RID: 28247
		public static int yGoback;

		// Token: 0x04006E58 RID: 28248
		public static List<int> IdMobsTanSat = new List<int>();

		// Token: 0x04006E59 RID: 28249
		public static List<int> TypeMobsTanSat = new List<int>();

		// Token: 0x04006E5A RID: 28250
		public static List<sbyte> IdSkillsTanSat = new List<sbyte>(PickMob.IdSkillsBase);

		// Token: 0x04006E5B RID: 28251
		public static bool IsAutoPickItems = true;

		// Token: 0x04006E5C RID: 28252
		public static bool IsPickItemsAll = true;

		// Token: 0x04006E5D RID: 28253
		public static bool IsPickItemsDis = false;

		// Token: 0x04006E5E RID: 28254
		public static bool IsLimitTimesPickItem = true;

		// Token: 0x04006E5F RID: 28255
		public static int TimesAutoPickItemMax = 20;

		// Token: 0x04006E60 RID: 28256
		public static List<short> IdItemPicks = new List<short>();

		// Token: 0x04006E61 RID: 28257
		public static List<short> IdItemBlocks = new List<short>(PickMob.IdItemBlockBase);

		// Token: 0x04006E62 RID: 28258
		public static List<sbyte> TypeItemPicks = new List<sbyte>();

		// Token: 0x04006E63 RID: 28259
		public static List<sbyte> TypeItemBlock = new List<sbyte>();

		// Token: 0x04006E64 RID: 28260
		public static int HpBuff = 0;

		// Token: 0x04006E65 RID: 28261
		public static int MpBuff = 0;
	}
}
