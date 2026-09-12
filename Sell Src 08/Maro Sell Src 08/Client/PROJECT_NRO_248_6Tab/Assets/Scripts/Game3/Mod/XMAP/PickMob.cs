using System;
using System.Collections.Generic;
using System.Threading;

namespace Game3.Mod.XMAP
{
	// Token: 0x02000354 RID: 852
	public class PickMob
	{
		// Token: 0x060025F3 RID: 9715 RVA: 0x0024ED0D File Offset: 0x0024CF0D
		public static void Update()
		{
			PickMobController.Update();
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0024ED14 File Offset: 0x0024CF14
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

		// Token: 0x0400494E RID: 18766
		private static readonly sbyte[] IdSkillsBase = new sbyte[]
		{
			0,
			2,
			17,
			4,
			13
		};

		// Token: 0x0400494F RID: 18767
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

		// Token: 0x04004950 RID: 18768
		public static bool tanSat = false;

		// Token: 0x04004951 RID: 18769
		public static bool tsPlayer = false;

		// Token: 0x04004952 RID: 18770
		public static bool neSieuQuai = false;

		// Token: 0x04004953 RID: 18771
		public static bool vuotDiaHinh = true;

		// Token: 0x04004954 RID: 18772
		public static bool telePem = true;

		// Token: 0x04004955 RID: 18773
		public static bool isGoBack;

		// Token: 0x04004956 RID: 18774
		public static int mapGoback;

		// Token: 0x04004957 RID: 18775
		public static int zoneGoback;

		// Token: 0x04004958 RID: 18776
		public static int xGoback;

		// Token: 0x04004959 RID: 18777
		public static int yGoback;

		// Token: 0x0400495A RID: 18778
		public static List<int> IdMobsTanSat = new List<int>();

		// Token: 0x0400495B RID: 18779
		public static List<int> TypeMobsTanSat = new List<int>();

		// Token: 0x0400495C RID: 18780
		public static List<sbyte> IdSkillsTanSat = new List<sbyte>(PickMob.IdSkillsBase);

		// Token: 0x0400495D RID: 18781
		public static bool IsAutoPickItems = true;

		// Token: 0x0400495E RID: 18782
		public static bool IsPickItemsAll = true;

		// Token: 0x0400495F RID: 18783
		public static bool IsPickItemsDis = false;

		// Token: 0x04004960 RID: 18784
		public static bool IsLimitTimesPickItem = true;

		// Token: 0x04004961 RID: 18785
		public static int TimesAutoPickItemMax = 20;

		// Token: 0x04004962 RID: 18786
		public static List<short> IdItemPicks = new List<short>();

		// Token: 0x04004963 RID: 18787
		public static List<short> IdItemBlocks = new List<short>(PickMob.IdItemBlockBase);

		// Token: 0x04004964 RID: 18788
		public static List<sbyte> TypeItemPicks = new List<sbyte>();

		// Token: 0x04004965 RID: 18789
		public static List<sbyte> TypeItemBlock = new List<sbyte>();

		// Token: 0x04004966 RID: 18790
		public static int HpBuff = 0;

		// Token: 0x04004967 RID: 18791
		public static int MpBuff = 0;
	}
}
