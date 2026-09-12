using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Game5
{
	// Token: 0x02000190 RID: 400
	public class TabController
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0011F5BD File Offset: 0x0011D7BD
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

		// Token: 0x0600121D RID: 4637 RVA: 0x0011F5D3 File Offset: 0x0011D7D3
		public TabController()
		{
			TabController.initCommand();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0011F5E0 File Offset: 0x0011D7E0
		public static bool isShow
		{
			get
			{
				return TabController._isShow;
			}
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0011F5E8 File Offset: 0x0011D7E8
		private static void initCommand()
		{
			TabController.firstCommand.x = GameCanvas.w - 80;
			TabController.firstCommand.y = 3;
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = TabManagement.tabNames[i];
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0011F634 File Offset: 0x0011D834
		public static void updateCaption()
		{
			TabController.firstCommand.caption = (TabManagement.tabIndex + 1).ToString();
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0011F664 File Offset: 0x0011D864
		private static void OnAsyncTab()
		{
			if (TabManagement.tab != TabType.Tab5)
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

		// Token: 0x06001222 RID: 4642 RVA: 0x0011F698 File Offset: 0x0011D898
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

		// Token: 0x06001223 RID: 4643 RVA: 0x0011F708 File Offset: 0x0011D908
		public void paint(mGraphics g)
		{
			if (!TabController.isShow)
			{
				return;
			}
			TabController.firstCommand.paint(g);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0011F71D File Offset: 0x0011D91D
		private static void TransferTabIndex(sbyte index)
		{
			TabManagement.tabIndex = (int)index;
			SceneManager.LoadScene(string.Format("Nro{0}", TabManagement.tabIndex + 1));
			TabManagement.tab = TabManagement.tabs[TabManagement.tabIndex];
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0011F75C File Offset: 0x0011D95C
		public static void updateCaptionTab(string[] names)
		{
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = names[i];
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0011F784 File Offset: 0x0011D984
		private static void showTabSelect()
		{
			if (TabManagement.tab != TabType.Tab5)
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

		// Token: 0x06001227 RID: 4647 RVA: 0x0011F843 File Offset: 0x0011DA43
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

		// Token: 0x04002353 RID: 9043
		private static TabController _Instance;

		// Token: 0x04002354 RID: 9044
		private static bool _isShow = true;

		// Token: 0x04002355 RID: 9045
		private static TabCommand firstCommand = new TabCommand((TabManagement.tabIndex + 1).ToString(), delegate()
		{
			TabController.showTabSelect();
		});

		// Token: 0x04002356 RID: 9046
		private static TabCommand[] TransferTab = (from i in Enumerable.Range(0, 5)
		select new TabCommand(string.Empty, delegate()
		{
			TabController.TransferTabIndex((sbyte)i);
		})).ToArray<TabCommand>();
	}
}
