using System;

namespace Game4
{
	// Token: 0x0200026B RID: 619
	public class Task
	{
		// Token: 0x06001BD4 RID: 7124 RVA: 0x001B49C8 File Offset: 0x001B2BC8
		public Task(short taskId, sbyte index, string name, string detail, string[] subNames, short[] counts, short count, string[] contentInfo)
		{
			this.taskId = taskId;
			this.index = (int)index;
			this.names = mFont.tahoma_7b_green2.splitFontArray(name, Panel.WIDTH_PANEL - 20);
			this.details = mFont.tahoma_7.splitFontArray(detail, Panel.WIDTH_PANEL - 20);
			this.subNames = subNames;
			this.counts = counts;
			this.count = count;
			this.contentInfo = contentInfo;
		}

		// Token: 0x040035D9 RID: 13785
		public int index;

		// Token: 0x040035DA RID: 13786
		public int max;

		// Token: 0x040035DB RID: 13787
		public short[] counts;

		// Token: 0x040035DC RID: 13788
		public short taskId;

		// Token: 0x040035DD RID: 13789
		public string[] names;

		// Token: 0x040035DE RID: 13790
		public string[] details;

		// Token: 0x040035DF RID: 13791
		public string[] subNames;

		// Token: 0x040035E0 RID: 13792
		public string[] contentInfo;

		// Token: 0x040035E1 RID: 13793
		public short count;
	}
}
