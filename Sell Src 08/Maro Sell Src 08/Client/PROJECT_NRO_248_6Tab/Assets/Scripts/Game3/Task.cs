using System;

namespace Game3
{
	// Token: 0x02000343 RID: 835
	public class Task
	{
		// Token: 0x06002578 RID: 9592 RVA: 0x00249A6C File Offset: 0x00247C6C
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

		// Token: 0x04004858 RID: 18520
		public int index;

		// Token: 0x04004859 RID: 18521
		public int max;

		// Token: 0x0400485A RID: 18522
		public short[] counts;

		// Token: 0x0400485B RID: 18523
		public short taskId;

		// Token: 0x0400485C RID: 18524
		public string[] names;

		// Token: 0x0400485D RID: 18525
		public string[] details;

		// Token: 0x0400485E RID: 18526
		public string[] subNames;

		// Token: 0x0400485F RID: 18527
		public string[] contentInfo;

		// Token: 0x04004860 RID: 18528
		public short count;
	}
}
