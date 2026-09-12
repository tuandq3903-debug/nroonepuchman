using System;

namespace Game5
{
	// Token: 0x02000193 RID: 403
	public class Task
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x0011F924 File Offset: 0x0011DB24
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

		// Token: 0x0400235A RID: 9050
		public int index;

		// Token: 0x0400235B RID: 9051
		public int max;

		// Token: 0x0400235C RID: 9052
		public short[] counts;

		// Token: 0x0400235D RID: 9053
		public short taskId;

		// Token: 0x0400235E RID: 9054
		public string[] names;

		// Token: 0x0400235F RID: 9055
		public string[] details;

		// Token: 0x04002360 RID: 9056
		public string[] subNames;

		// Token: 0x04002361 RID: 9057
		public string[] contentInfo;

		// Token: 0x04002362 RID: 9058
		public short count;
	}
}
