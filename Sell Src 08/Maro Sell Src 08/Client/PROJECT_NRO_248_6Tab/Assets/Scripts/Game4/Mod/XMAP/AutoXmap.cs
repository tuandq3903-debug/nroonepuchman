using System;

namespace Game4.Mod.XMAP
{
	// Token: 0x02000277 RID: 631
	public class AutoXmap
	{
		// Token: 0x06001C2F RID: 7215 RVA: 0x001B9445 File Offset: 0x001B7645
		public static void Update()
		{
			if (XmapData.GI().IsLoading)
			{
				XmapData.GI().Update();
			}
			if (AutoXmap.IsXmapRunning)
			{
				XmapController.Update();
			}
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x001B946C File Offset: 0x001B766C
		public static void Info(string text)
		{
			if (text.Equals("Bạn chưa thể đến khu vực này"))
			{
				XmapController.FinishXmap();
			}
			if ((text.ToLower().Contains("chức năng bảo vệ") || text.ToLower().Contains("đã hủy xmap")) && AutoXmap.IsXmapRunning)
			{
				XmapController.FinishXmap();
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x001B94BB File Offset: 0x001B76BB
		public static void SelectMapTrans(int selected)
		{
			if (AutoXmap.IsMapTransAsXmap)
			{
				XmapController.HideInfoDlg();
				XmapController.StartRunToMapId(XmapData.GetIdMapFromPanelXmap(GameCanvas.panel.mapNames[selected]));
				return;
			}
			XmapController.SaveIdMapCapsuleReturn();
			Service.gI().requestMapSelect(selected);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x001B94F0 File Offset: 0x001B76F0
		public static void ShowPanelMapTrans()
		{
			AutoXmap.IsMapTransAsXmap = false;
			if (AutoXmap.IsShowPanelMapTrans)
			{
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
				return;
			}
			AutoXmap.IsShowPanelMapTrans = true;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x001B951A File Offset: 0x001B771A
		public static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			Char.isLoadingMap = false;
		}

		// Token: 0x040036BE RID: 14014
		public static bool IsXmapRunning = false;

		// Token: 0x040036BF RID: 14015
		public static bool IsMapTransAsXmap = false;

		// Token: 0x040036C0 RID: 14016
		public static bool IsShowPanelMapTrans = true;

		// Token: 0x040036C1 RID: 14017
		public static bool IsUseCapsuleNormal = true;

		// Token: 0x040036C2 RID: 14018
		public static bool IsUseCapsuleVip = true;

		// Token: 0x040036C3 RID: 14019
		public static int IdMapCapsuleReturn = -1;
	}
}
