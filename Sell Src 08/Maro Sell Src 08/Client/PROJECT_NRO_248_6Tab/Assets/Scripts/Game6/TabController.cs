using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Game6
{
	// Token: 0x020000B8 RID: 184
	public class TabController
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0008A4B1 File Offset: 0x000886B1
		public static TabController Instance
		{
			get
			{
				TabController result;
				if ((result = TabController._Instance) == null)
				{
					result = (TabController._Instance = new TabController());
				}
				return result;
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0008A4C7 File Offset: 0x000886C7
		public TabController()
		{
			TabController.initCommand();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0008A4D4 File Offset: 0x000886D4
		public static bool isShow
		{
			get
			{
				return TabController._isShow;
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0008A4DC File Offset: 0x000886DC
		private static void initCommand()
		{
			TabController.firstCommand.x = GameCanvas.w - 80;
			TabController.firstCommand.y = 3;
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = TabManagement.tabNames[i];
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0008A528 File Offset: 0x00088728
		public static void updateCaption()
		{
			TabController.firstCommand.caption = (TabManagement.tabIndex + 1).ToString();
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0008A558 File Offset: 0x00088758
		private static void OnAsyncTab()
		{
			if (TabManagement.tab != TabType.Tab6)
			{
				return;
			}
			if (TabManagement.SyncTab)
			{
				TabManagement.SyncTab = false;
				GameCanvas.startOKDlg("<color=red>Tắt đồng bộ tab!</color>");
				return;
			}
			TabManagement.SyncTab = true;
			GameCanvas.startOKDlg("<color=red>Đã đồng bộ tab!</color>");
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0008A58C File Offset: 0x0008878C
		public static void updateCharName(string name, sbyte type)
		{
			if (type == 0)
			{
				TabManagement.tabNames[TabManagement.tabIndex] = string.Format("Tab {0}\n[{1}]", TabManagement.tabIndex + 1, name);
			}
			else
			{
				TabManagement.tabNames[TabManagement.tabIndex] = string.Format("Tab {0}", TabManagement.tabIndex + 1);
			}
			TabController.TransferTab[TabManagement.tabIndex].caption = TabManagement.tabNames[TabManagement.tabIndex];
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0008A5FC File Offset: 0x000887FC
		public void paint(mGraphics g)
		{
			if (!TabController.isShow)
			{
				return;
			}
			TabController.firstCommand.paint(g);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0008A611 File Offset: 0x00088811
		private static void TransferTabIndex(sbyte index)
		{
			TabManagement.tabIndex = (int)index;
			SceneManager.LoadScene(string.Format("Nro{0}", TabManagement.tabIndex + 1));
			TabManagement.tab = TabManagement.tabs[TabManagement.tabIndex];
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0008A650 File Offset: 0x00088850
		public static void updateCaptionTab(string[] names)
		{
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = names[i];
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0008A678 File Offset: 0x00088878
		private static void showTabSelect()
		{
			if (TabManagement.tab != TabType.Tab6)
			{
				return;
			}
			MyVector i = new MyVector();
			foreach (TabCommand cmd in TabController.TransferTab)
			{
				i.addElement(new Command(cmd.caption, cmd.action));
			}
			if (TabManagement.Hien_Menu_Dong_Bo)
			{
				i.addElement(new Command("Đồng Bộ\n[" + TabManagement.SyncTab.ToString().Replace("True", "Bật").Replace("False", "Tắt") + "]", delegate()
				{
					TabController.OnAsyncTab();
				}));
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0008A72B File Offset: 0x0008892B
		public bool isPointerHoldInTab()
		{
			if (!TabController.isShow)
			{
				return false;
			}
			if (TabController.firstCommand.isPointerInside())
			{
				TabController.firstCommand.Invoke();
				return true;
			}
			return false;
		}

		// Token: 0x040010D4 RID: 4308
		private static TabController _Instance;

		// Token: 0x040010D5 RID: 4309
		private static bool _isShow = true;

		// Token: 0x040010D6 RID: 4310
		private static TabCommand firstCommand = new TabCommand((TabManagement.tabIndex + 1).ToString(), delegate()
		{
			TabController.showTabSelect();
		});

		// Token: 0x040010D7 RID: 4311
		private static TabCommand[] TransferTab = (from i in Enumerable.Range(0, 5)
		select new TabCommand(string.Empty, delegate()
		{
			TabController.TransferTabIndex((sbyte)i);
		})).ToArray<TabCommand>();
	}
}
