using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Game4
{
	// Token: 0x02000268 RID: 616
	public class TabController
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x001B4661 File Offset: 0x001B2861
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

		// Token: 0x06001BC1 RID: 7105 RVA: 0x001B4677 File Offset: 0x001B2877
		public TabController()
		{
			TabController.initCommand();
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x001B4684 File Offset: 0x001B2884
		public static bool isShow
		{
			get
			{
				return TabController._isShow;
			}
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x001B468C File Offset: 0x001B288C
		private static void initCommand()
		{
			TabController.firstCommand.x = GameCanvas.w - 80;
			TabController.firstCommand.y = 3;
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = TabManagement.tabNames[i];
			}
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x001B46D8 File Offset: 0x001B28D8
		public static void updateCaption()
		{
			TabController.firstCommand.caption = (TabManagement.tabIndex + 1).ToString();
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x001B4708 File Offset: 0x001B2908
		private static void OnAsyncTab()
		{
			if (TabManagement.tab != TabType.Tab4)
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

		// Token: 0x06001BC6 RID: 7110 RVA: 0x001B473C File Offset: 0x001B293C
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

		// Token: 0x06001BC7 RID: 7111 RVA: 0x001B47AC File Offset: 0x001B29AC
		public void paint(mGraphics g)
		{
			if (!TabController.isShow)
			{
				return;
			}
			TabController.firstCommand.paint(g);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x001B47C1 File Offset: 0x001B29C1
		private static void TransferTabIndex(sbyte index)
		{
			TabManagement.tabIndex = (int)index;
			SceneManager.LoadScene(string.Format("Nro{0}", TabManagement.tabIndex + 1));
			TabManagement.tab = TabManagement.tabs[TabManagement.tabIndex];
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x001B4800 File Offset: 0x001B2A00
		public static void updateCaptionTab(string[] names)
		{
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = names[i];
			}
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x001B4828 File Offset: 0x001B2A28
		private static void showTabSelect()
		{
			if (TabManagement.tab != TabType.Tab4)
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
			GameCanvas.menu.startAt(i, 0);
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x001B48E7 File Offset: 0x001B2AE7
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

		// Token: 0x040035D2 RID: 13778
		private static TabController _Instance;

		// Token: 0x040035D3 RID: 13779
		private static bool _isShow = true;

		// Token: 0x040035D4 RID: 13780
		private static TabCommand firstCommand = new TabCommand((TabManagement.tabIndex + 1).ToString(), delegate()
		{
			TabController.showTabSelect();
		});

		// Token: 0x040035D5 RID: 13781
		private static TabCommand[] TransferTab = (from i in Enumerable.Range(0, 5)
		select new TabCommand(string.Empty, delegate()
		{
			TabController.TransferTabIndex((sbyte)i);
		})).ToArray<TabCommand>();
	}
}
