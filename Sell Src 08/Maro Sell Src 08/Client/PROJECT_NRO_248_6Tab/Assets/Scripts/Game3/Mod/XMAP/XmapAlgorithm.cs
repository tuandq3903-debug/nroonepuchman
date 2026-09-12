using System;
using System.Collections.Generic;

namespace Game3.Mod.XMAP
{
	// Token: 0x02000358 RID: 856
	public class XmapAlgorithm
	{
		// Token: 0x0600260B RID: 9739 RVA: 0x0024FCD4 File Offset: 0x0024DED4
		public static List<int> FindWay(int idMapStart, int idMapEnd)
		{
			List<int> wayPassedStart = XmapAlgorithm.GetWayPassedStart(idMapStart);
			return XmapAlgorithm.FindWay(idMapEnd, wayPassedStart);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x0024FCF0 File Offset: 0x0024DEF0
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

		// Token: 0x0600260D RID: 9741 RVA: 0x0024FDA8 File Offset: 0x0024DFA8
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

		// Token: 0x0600260E RID: 9742 RVA: 0x00090BB4 File Offset: 0x0008EDB4
		private static List<int> GetWayPassedStart(int idMapStart)
		{
			return new List<int>
			{
				idMapStart
			};
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x00090BC2 File Offset: 0x0008EDC2
		private static List<int> GetWayPassedNext(List<int> wayPassed, int idMapNext)
		{
			return new List<int>(wayPassed)
			{
				idMapNext
			};
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0024FDF0 File Offset: 0x0024DFF0
		private static bool IsWayBetter(List<int> way1, List<int> way2)
		{
			bool flag = XmapAlgorithm.IsBadWay(way1);
			bool flag2 = XmapAlgorithm.IsBadWay(way2);
			return (!flag || flag2) && ((!flag && flag2) || way1.Count < way2.Count);
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0024FE2D File Offset: 0x0024E02D
		private static bool IsBadWay(List<int> way)
		{
			return XmapAlgorithm.IsWayGoFutureAndBack(way);
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x0024FE38 File Offset: 0x0024E038
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
