using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Game3
{
	// Token: 0x02000340 RID: 832
	public class TabController
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x00249705 File Offset: 0x00247905
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

		// Token: 0x06002565 RID: 9573 RVA: 0x0024971B File Offset: 0x0024791B
		public TabController()
		{
			TabController.initCommand();
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00249728 File Offset: 0x00247928
		public static bool isShow
		{
			get
			{
				return TabController._isShow;
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00249730 File Offset: 0x00247930
		private static void initCommand()
		{
			TabController.firstCommand.x = GameCanvas.w - 80;
			TabController.firstCommand.y = 3;
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = TabManagement.tabNames[i];
			}
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0024977C File Offset: 0x0024797C
		public static void updateCaption()
		{
			TabController.firstCommand.caption = (TabManagement.tabIndex + 1).ToString();
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x002497AC File Offset: 0x002479AC
		private static void OnAsyncTab()
		{
			if (TabManagement.tab != TabType.Tab3)
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

		// Token: 0x0600256A RID: 9578 RVA: 0x002497E0 File Offset: 0x002479E0
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

		// Token: 0x0600256B RID: 9579 RVA: 0x00249850 File Offset: 0x00247A50
		public void paint(mGraphics g)
		{
			if (!TabController.isShow)
			{
				return;
			}
			TabController.firstCommand.paint(g);
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x00249865 File Offset: 0x00247A65
		private static void TransferTabIndex(sbyte index)
		{
			TabManagement.tabIndex = (int)index;
			SceneManager.LoadScene(string.Format("Nro{0}", TabManagement.tabIndex + 1));
			TabManagement.tab = TabManagement.tabs[TabManagement.tabIndex];
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x002498A4 File Offset: 0x00247AA4
		public static void updateCaptionTab(string[] names)
		{
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = names[i];
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x002498CC File Offset: 0x00247ACC
		private static void showTabSelect()
		{
			if (TabManagement.tab != TabType.Tab3)
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

		// Token: 0x0600256F RID: 9583 RVA: 0x0024998B File Offset: 0x00247B8B
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

		// Token: 0x04004851 RID: 18513
		private static TabController _Instance;

		// Token: 0x04004852 RID: 18514
		private static bool _isShow = true;

		// Token: 0x04004853 RID: 18515
		private static TabCommand firstCommand = new TabCommand((TabManagement.tabIndex + 1).ToString(), delegate()
		{
			TabController.showTabSelect();
		});

		// Token: 0x04004854 RID: 18516
		private static TabCommand[] TransferTab = (from i in Enumerable.Range(0, 5)
		select new TabCommand(string.Empty, delegate()
		{
			TabController.TransferTabIndex((sbyte)i);
		})).ToArray<TabCommand>();
	}
}
