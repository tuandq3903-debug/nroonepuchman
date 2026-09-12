using System;

namespace Game1.Mod.XMAP
{
	// Token: 0x020004FF RID: 1279
	public class AutoXmap
	{
		// Token: 0x0600391B RID: 14619 RVA: 0x00378631 File Offset: 0x00376831
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

		// Token: 0x0600391C RID: 14620 RVA: 0x00378658 File Offset: 0x00376858
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

		// Token: 0x0600391D RID: 14621 RVA: 0x003786A7 File Offset: 0x003768A7
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

		// Token: 0x0600391E RID: 14622 RVA: 0x003786DC File Offset: 0x003768DC
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

		// Token: 0x0600391F RID: 14623 RVA: 0x00378706 File Offset: 0x00376906
		public static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			Char.isLoadingMap = false;
		}

		// Token: 0x04006E3B RID: 28219
		public static bool IsXmapRunning = false;

		// Token: 0x04006E3C RID: 28220
		public static bool IsMapTransAsXmap = false;

		// Token: 0x04006E3D RID: 28221
		public static bool IsShowPanelMapTrans = true;

		// Token: 0x04006E3E RID: 28222
		public static bool IsUseCapsuleNormal = true;

		// Token: 0x04006E3F RID: 28223
		public static bool IsUseCapsuleVip = true;

		// Token: 0x04006E40 RID: 28224
		public static int IdMapCapsuleReturn = -1;
	}
}
