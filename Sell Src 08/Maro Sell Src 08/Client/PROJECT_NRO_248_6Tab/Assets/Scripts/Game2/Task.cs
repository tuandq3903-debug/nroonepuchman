using System;

namespace Game2
{
	// Token: 0x0200041B RID: 1051
	public class Task
	{
		// Token: 0x06002F1C RID: 12060 RVA: 0x002DEB10 File Offset: 0x002DCD10
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

		// Token: 0x04005AD7 RID: 23255
		public int index;

		// Token: 0x04005AD8 RID: 23256
		public int max;

		// Token: 0x04005AD9 RID: 23257
		public short[] counts;

		// Token: 0x04005ADA RID: 23258
		public short taskId;

		// Token: 0x04005ADB RID: 23259
		public string[] names;

		// Token: 0x04005ADC RID: 23260
		public string[] details;

		// Token: 0x04005ADD RID: 23261
		public string[] subNames;

		// Token: 0x04005ADE RID: 23262
		public string[] contentInfo;

		// Token: 0x04005ADF RID: 23263
		public short count;
	}
}
