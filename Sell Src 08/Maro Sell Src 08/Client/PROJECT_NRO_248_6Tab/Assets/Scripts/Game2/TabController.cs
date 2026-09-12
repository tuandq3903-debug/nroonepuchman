using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Game2
{
	// Token: 0x02000418 RID: 1048
	public class TabController
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x002DE7A9 File Offset: 0x002DC9A9
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

		// Token: 0x06002F09 RID: 12041 RVA: 0x002DE7BF File Offset: 0x002DC9BF
		public TabController()
		{
			TabController.initCommand();
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x002DE7CC File Offset: 0x002DC9CC
		public static bool isShow
		{
			get
			{
				return TabController._isShow;
			}
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x002DE7D4 File Offset: 0x002DC9D4
		private static void initCommand()
		{
			TabController.firstCommand.x = GameCanvas.w - 80;
			TabController.firstCommand.y = 3;
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = TabManagement.tabNames[i];
			}
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x002DE820 File Offset: 0x002DCA20
		public static void updateCaption()
		{
			TabController.firstCommand.caption = (TabManagement.tabIndex + 1).ToString();
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x002DE850 File Offset: 0x002DCA50
		private static void OnAsyncTab()
		{
			if (TabManagement.tab != TabType.Tab2)
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

		// Token: 0x06002F0E RID: 12046 RVA: 0x002DE884 File Offset: 0x002DCA84
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

		// Token: 0x06002F0F RID: 12047 RVA: 0x002DE8F4 File Offset: 0x002DCAF4
		public void paint(mGraphics g)
		{
			if (!TabController.isShow)
			{
				return;
			}
			TabController.firstCommand.paint(g);
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x002DE909 File Offset: 0x002DCB09
		private static void TransferTabIndex(sbyte index)
		{
			TabManagement.tabIndex = (int)index;
			SceneManager.LoadScene(string.Format("Nro{0}", TabManagement.tabIndex + 1));
			TabManagement.tab = TabManagement.tabs[TabManagement.tabIndex];
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x002DE948 File Offset: 0x002DCB48
		public static void updateCaptionTab(string[] names)
		{
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = names[i];
			}
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x002DE970 File Offset: 0x002DCB70
		private static void showTabSelect()
		{
			if (TabManagement.tab != TabType.Tab2)
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

		// Token: 0x06002F13 RID: 12051 RVA: 0x002DEA2F File Offset: 0x002DCC2F
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

		// Token: 0x04005AD0 RID: 23248
		private static TabController _Instance;

		// Token: 0x04005AD1 RID: 23249
		private static bool _isShow = true;

		// Token: 0x04005AD2 RID: 23250
		private static TabCommand firstCommand = new TabCommand((TabManagement.tabIndex + 1).ToString(), delegate()
		{
			TabController.showTabSelect();
		});

		// Token: 0x04005AD3 RID: 23251
		private static TabCommand[] TransferTab = (from i in Enumerable.Range(0, 5)
		select new TabCommand(string.Empty, delegate()
		{
			TabController.TransferTabIndex((sbyte)i);
		})).ToArray<TabCommand>();
	}
}
