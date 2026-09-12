using System;

// Token: 0x02000002 RID: 2
public static class TabManagement
{
	// Token: 0x04000001 RID: 1
	public static TabType tab;

	// Token: 0x04000002 RID: 2
	public static TabType[] tabs = new TabType[]
	{
		TabType.Tab1,
		TabType.Tab2,
		TabType.Tab3,
		TabType.Tab4,
		TabType.Tab5,
		TabType.Tab6
	};

	// Token: 0x04000003 RID: 3
	public static bool SyncTab = true;

	// Token: 0x04000004 RID: 4
	public static bool Hien_Menu_Dong_Bo = false;

	// Token: 0x04000005 RID: 5
	public static int tabIndex = 0;

	// Token: 0x04000006 RID: 6
	public static string[] tabNames = new string[]
	{
		"Tab 1",
		"Tab 2",
		"Tab 3",
		"Tab 4",
		"Tab 5",
		"Tab 6"
	};
}
