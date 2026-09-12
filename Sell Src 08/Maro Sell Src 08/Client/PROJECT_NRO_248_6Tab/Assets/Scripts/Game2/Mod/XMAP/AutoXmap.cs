using System;

namespace Game2.Mod.XMAP
{
	// Token: 0x02000427 RID: 1063
	public class AutoXmap
	{
		// Token: 0x06002F77 RID: 12151 RVA: 0x002E358D File Offset: 0x002E178D
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

		// Token: 0x06002F78 RID: 12152 RVA: 0x002E35B4 File Offset: 0x002E17B4
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

		// Token: 0x06002F79 RID: 12153 RVA: 0x002E3603 File Offset: 0x002E1803
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

		// Token: 0x06002F7A RID: 12154 RVA: 0x002E3638 File Offset: 0x002E1838
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

		// Token: 0x06002F7B RID: 12155 RVA: 0x002E3662 File Offset: 0x002E1862
		public static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			Char.isLoadingMap = false;
		}

		// Token: 0x04005BBC RID: 23484
		public static bool IsXmapRunning = false;

		// Token: 0x04005BBD RID: 23485
		public static bool IsMapTransAsXmap = false;

		// Token: 0x04005BBE RID: 23486
		public static bool IsShowPanelMapTrans = true;

		// Token: 0x04005BBF RID: 23487
		public static bool IsUseCapsuleNormal = true;

		// Token: 0x04005BC0 RID: 23488
		public static bool IsUseCapsuleVip = true;

		// Token: 0x04005BC1 RID: 23489
		public static int IdMapCapsuleReturn = -1;
	}
}
