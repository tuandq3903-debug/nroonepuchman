using System;
using System.Collections.Generic;

namespace Game6.Mod.XMAP
{
	// Token: 0x020000D0 RID: 208
	public class XmapAlgorithm
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x00090A98 File Offset: 0x0008EC98
		public static List<int> FindWay(int idMapStart, int idMapEnd)
		{
			List<int> wayPassedStart = XmapAlgorithm.GetWayPassedStart(idMapStart);
			return XmapAlgorithm.FindWay(idMapEnd, wayPassedStart);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00090AB4 File Offset: 0x0008ECB4
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

		// Token: 0x06000921 RID: 2337 RVA: 0x00090B6C File Offset: 0x0008ED6C
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

		// Token: 0x06000922 RID: 2338 RVA: 0x00090BB4 File Offset: 0x0008EDB4
		private static List<int> GetWayPassedStart(int idMapStart)
		{
			return new List<int>
			{
				idMapStart
			};
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00090BC2 File Offset: 0x0008EDC2
		private static List<int> GetWayPassedNext(List<int> wayPassed, int idMapNext)
		{
			return new List<int>(wayPassed)
			{
				idMapNext
			};
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00090BD4 File Offset: 0x0008EDD4
		private static bool IsWayBetter(List<int> way1, List<int> way2)
		{
			bool flag = XmapAlgorithm.IsBadWay(way1);
			bool flag2 = XmapAlgorithm.IsBadWay(way2);
			return (!flag || flag2) && ((!flag && flag2) || way1.Count < way2.Count);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00090C11 File Offset: 0x0008EE11
		private static bool IsBadWay(List<int> way)
		{
			return XmapAlgorithm.IsWayGoFutureAndBack(way);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00090C1C File Offset: 0x0008EE1C
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
