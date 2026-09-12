using System;

namespace Game1
{
	// Token: 0x020004F3 RID: 1267
	public class Task
	{
		// Token: 0x060038C0 RID: 14528 RVA: 0x00373BB4 File Offset: 0x00371DB4
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

		// Token: 0x04006D56 RID: 27990
		public int index;

		// Token: 0x04006D57 RID: 27991
		public int max;

		// Token: 0x04006D58 RID: 27992
		public short[] counts;

		// Token: 0x04006D59 RID: 27993
		public short taskId;

		// Token: 0x04006D5A RID: 27994
		public string[] names;

		// Token: 0x04006D5B RID: 27995
		public string[] details;

		// Token: 0x04006D5C RID: 27996
		public string[] subNames;

		// Token: 0x04006D5D RID: 27997
		public string[] contentInfo;

		// Token: 0x04006D5E RID: 27998
		public short count;
	}
}
