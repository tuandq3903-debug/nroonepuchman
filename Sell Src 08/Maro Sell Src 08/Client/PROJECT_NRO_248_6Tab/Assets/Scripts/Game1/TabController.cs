using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Game1
{
	// Token: 0x020004F0 RID: 1264
	public class TabController
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x0037384D File Offset: 0x00371A4D
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

		// Token: 0x060038AD RID: 14509 RVA: 0x00373863 File Offset: 0x00371A63
		public TabController()
		{
			TabController.initCommand();
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060038AE RID: 14510 RVA: 0x00373870 File Offset: 0x00371A70
		public static bool isShow
		{
			get
			{
				return TabController._isShow;
			}
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x00373878 File Offset: 0x00371A78
		private static void initCommand()
		{
			TabController.firstCommand.x = GameCanvas.w - 80;
			TabController.firstCommand.y = 3;
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = TabManagement.tabNames[i];
			}
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x003738C4 File Offset: 0x00371AC4
		public static void updateCaption()
		{
			TabController.firstCommand.caption = (TabManagement.tabIndex + 1).ToString();
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x003738F4 File Offset: 0x00371AF4
		private static void OnAsyncTab()
		{
			if (TabManagement.tab != TabType.Tab1)
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

		// Token: 0x060038B2 RID: 14514 RVA: 0x00373928 File Offset: 0x00371B28
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

		// Token: 0x060038B3 RID: 14515 RVA: 0x00373998 File Offset: 0x00371B98
		public void paint(mGraphics g)
		{
			if (!TabController.isShow)
			{
				return;
			}
			TabController.firstCommand.paint(g);
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x003739AD File Offset: 0x00371BAD
		private static void TransferTabIndex(sbyte index)
		{
			TabManagement.tabIndex = (int)index;
			SceneManager.LoadScene(string.Format("Nro{0}", TabManagement.tabIndex + 1));
			TabManagement.tab = TabManagement.tabs[TabManagement.tabIndex];
			TabController.updateCaptionTab(TabManagement.tabNames);
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x003739EC File Offset: 0x00371BEC
		public static void updateCaptionTab(string[] names)
		{
			for (int i = 0; i < 5; i++)
			{
				TabController.TransferTab[i].caption = names[i];
			}
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x00373A14 File Offset: 0x00371C14
		private static void showTabSelect()
		{
			if (TabManagement.tab != TabType.Tab1)
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

		// Token: 0x060038B7 RID: 14519 RVA: 0x00373AD2 File Offset: 0x00371CD2
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

		// Token: 0x04006D4F RID: 27983
		private static TabController _Instance;

		// Token: 0x04006D50 RID: 27984
		private static bool _isShow = true;

		// Token: 0x04006D51 RID: 27985
		private static TabCommand firstCommand = new TabCommand((TabManagement.tabIndex + 1).ToString(), delegate()
		{
			TabController.showTabSelect();
		});

		// Token: 0x04006D52 RID: 27986
		private static TabCommand[] TransferTab = (from i in Enumerable.Range(0, 5)
		select new TabCommand(string.Empty, delegate()
		{
			TabController.TransferTabIndex((sbyte)i);
		})).ToArray<TabCommand>();
	}
}
