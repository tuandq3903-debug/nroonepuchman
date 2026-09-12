using System;

namespace Game5.Mod.XMAP
{
	// Token: 0x0200019F RID: 415
	public class AutoXmap
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x001243A1 File Offset: 0x001225A1
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

		// Token: 0x0600128C RID: 4748 RVA: 0x001243C8 File Offset: 0x001225C8
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

		// Token: 0x0600128D RID: 4749 RVA: 0x00124417 File Offset: 0x00122617
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

		// Token: 0x0600128E RID: 4750 RVA: 0x0012444C File Offset: 0x0012264C
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

		// Token: 0x0600128F RID: 4751 RVA: 0x00124476 File Offset: 0x00122676
		public static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			Char.isLoadingMap = false;
		}

		// Token: 0x0400243F RID: 9279
		public static bool IsXmapRunning = false;

		// Token: 0x04002440 RID: 9280
		public static bool IsMapTransAsXmap = false;

		// Token: 0x04002441 RID: 9281
		public static bool IsShowPanelMapTrans = true;

		// Token: 0x04002442 RID: 9282
		public static bool IsUseCapsuleNormal = true;

		// Token: 0x04002443 RID: 9283
		public static bool IsUseCapsuleVip = true;

		// Token: 0x04002444 RID: 9284
		public static int IdMapCapsuleReturn = -1;
	}
}
