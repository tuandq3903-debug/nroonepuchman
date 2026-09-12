using System;

namespace Game6.Mod.XMAP
{
	// Token: 0x020000C7 RID: 199
	public class AutoXmap
	{
		// Token: 0x060008E7 RID: 2279 RVA: 0x0008F289 File Offset: 0x0008D489
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

		// Token: 0x060008E8 RID: 2280 RVA: 0x0008F2B0 File Offset: 0x0008D4B0
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

		// Token: 0x060008E9 RID: 2281 RVA: 0x0008F2FF File Offset: 0x0008D4FF
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

		// Token: 0x060008EA RID: 2282 RVA: 0x0008F334 File Offset: 0x0008D534
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

		// Token: 0x060008EB RID: 2283 RVA: 0x0008F35E File Offset: 0x0008D55E
		public static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			Char.isLoadingMap = false;
		}

		// Token: 0x040011C0 RID: 4544
		public static bool IsXmapRunning = false;

		// Token: 0x040011C1 RID: 4545
		public static bool IsMapTransAsXmap = false;

		// Token: 0x040011C2 RID: 4546
		public static bool IsShowPanelMapTrans = true;

		// Token: 0x040011C3 RID: 4547
		public static bool IsUseCapsuleNormal = true;

		// Token: 0x040011C4 RID: 4548
		public static bool IsUseCapsuleVip = true;

		// Token: 0x040011C5 RID: 4549
		public static int IdMapCapsuleReturn = -1;
	}
}
