using System;
using System.Collections.Generic;

namespace Game4.Mod.XMAP
{
	// Token: 0x02000280 RID: 640
	public class XmapAlgorithm
	{
		// Token: 0x06001C67 RID: 7271 RVA: 0x001BAC30 File Offset: 0x001B8E30
		public static List<int> FindWay(int idMapStart, int idMapEnd)
		{
			List<int> wayPassedStart = XmapAlgorithm.GetWayPassedStart(idMapStart);
			return XmapAlgorithm.FindWay(idMapEnd, wayPassedStart);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x001BAC4C File Offset: 0x001B8E4C
		private static List<int> FindWay(int idMapEnd, List<int> wayPassed)
		{
			int num = wayPassed[wayPassed.Count - 1];
			if (num == idMapEnd)
			{
				return wayPassed;
			}
			if (!XmapData.GI().CanGetMapNexts(num))
			{
				return null;
			}
			List<List<int>> list = new List<List<int>>();
			foreach (MapNext mapNext in XmapData.GI().GetMapNexts(num))
			{
				List<int> list2 = null;
				if (!wayPassed.Contains(mapNext.MapID))
				{
					List<int> wayPassedNext = XmapAlgorithm.GetWayPassedNext(wayPassed, mapNext.MapID);
					list2 = XmapAlgorithm.FindWay(idMapEnd, wayPassedNext);
				}
				if (list2 != null)
				{
					list.Add(list2);
				}
			}
			return XmapAlgorithm.GetBestWay(list);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x001BAD04 File Offset: 0x001B8F04
		private static List<int> GetBestWay(List<List<int>> ways)
		{
			if (ways.Count == 0)
			{
				return null;
			}
			List<int> list = ways[0];
			for (int i = 1; i < ways.Count; i++)
			{
				if (XmapAlgorithm.IsWayBetter(ways[i], list))
				{
					list = ways[i];
				}
			}
			return list;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00090BB4 File Offset: 0x0008EDB4
		private static List<int> GetWayPassedStart(int idMapStart)
		{
			return new List<int>
			{
				idMapStart
			};
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00090BC2 File Offset: 0x0008EDC2
		private static List<int> GetWayPassedNext(List<int> wayPassed, int idMapNext)
		{
			return new List<int>(wayPassed)
			{
				idMapNext
			};
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x001BAD4C File Offset: 0x001B8F4C
		private static bool IsWayBetter(List<int> way1, List<int> way2)
		{
			bool flag = XmapAlgorithm.IsBadWay(way1);
			bool flag2 = XmapAlgorithm.IsBadWay(way2);
			return (!flag || flag2) && ((!flag && flag2) || way1.Count < way2.Count);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x001BAD89 File Offset: 0x001B8F89
		private static bool IsBadWay(List<int> way)
		{
			return XmapAlgorithm.IsWayGoFutureAndBack(way);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x001BAD94 File Offset: 0x001B8F94
		private static bool IsWayGoFutureAndBack(List<int> way)
		{
			List<int> list = new List<int>
			{
				27,
				28,
				29
			};
			for (int i = 1; i < way.Count - 1; i++)
			{
				if (way[i] == 102 && way[i + 1] == 24 && list.Contains(way[i - 1]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
