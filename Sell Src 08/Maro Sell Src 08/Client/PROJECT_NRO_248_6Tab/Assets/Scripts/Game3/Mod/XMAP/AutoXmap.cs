using System;

namespace Game3.Mod.XMAP
{
	// Token: 0x0200034F RID: 847
	public class AutoXmap
	{
		// Token: 0x060025D3 RID: 9683 RVA: 0x0024E4E9 File Offset: 0x0024C6E9
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

		// Token: 0x060025D4 RID: 9684 RVA: 0x0024E510 File Offset: 0x0024C710
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

		// Token: 0x060025D5 RID: 9685 RVA: 0x0024E55F File Offset: 0x0024C75F
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

		// Token: 0x060025D6 RID: 9686 RVA: 0x0024E594 File Offset: 0x0024C794
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

		// Token: 0x060025D7 RID: 9687 RVA: 0x0024E5BE File Offset: 0x0024C7BE
		public static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			Char.isLoadingMap = false;
		}

		// Token: 0x0400493D RID: 18749
		public static bool IsXmapRunning = false;

		// Token: 0x0400493E RID: 18750
		public static bool IsMapTransAsXmap = false;

		// Token: 0x0400493F RID: 18751
		public static bool IsShowPanelMapTrans = true;

		// Token: 0x04004940 RID: 18752
		public static bool IsUseCapsuleNormal = true;

		// Token: 0x04004941 RID: 18753
		public static bool IsUseCapsuleVip = true;

		// Token: 0x04004942 RID: 18754
		public static int IdMapCapsuleReturn = -1;
	}
}
