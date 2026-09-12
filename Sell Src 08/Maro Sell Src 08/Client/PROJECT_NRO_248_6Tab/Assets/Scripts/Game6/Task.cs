using System;

namespace Game6
{
	// Token: 0x020000BB RID: 187
	public class Task
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x0008A80C File Offset: 0x00088A0C
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

		// Token: 0x040010DB RID: 4315
		public int index;

		// Token: 0x040010DC RID: 4316
		public int max;

		// Token: 0x040010DD RID: 4317
		public short[] counts;

		// Token: 0x040010DE RID: 4318
		public short taskId;

		// Token: 0x040010DF RID: 4319
		public string[] names;

		// Token: 0x040010E0 RID: 4320
		public string[] details;

		// Token: 0x040010E1 RID: 4321
		public string[] subNames;

		// Token: 0x040010E2 RID: 4322
		public string[] contentInfo;

		// Token: 0x040010E3 RID: 4323
		public short count;
	}
}
