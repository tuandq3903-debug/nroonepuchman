using System;

namespace Game2
{
	// Token: 0x0200041C RID: 1052
	public class TaskOrder
	{
		// Token: 0x06002F1D RID: 12061 RVA: 0x002DEB84 File Offset: 0x002DCD84
		public TaskOrder(sbyte taskId, short count, short maxCount, string name, string description, sbyte killId, sbyte mapId)
		{
			this.count = (int)count;
			this.maxCount = maxCount;
			this.taskId = (int)taskId;
			this.name = name;
			this.description = description;
			this.killId = (int)killId;
			this.mapId = (int)mapId;
		}

		// Token: 0x04005AE0 RID: 23264
		public int taskId;

		// Token: 0x04005AE1 RID: 23265
		public int count;

		// Token: 0x04005AE2 RID: 23266
		public short maxCount;

		// Token: 0x04005AE3 RID: 23267
		public string name;

		// Token: 0x04005AE4 RID: 23268
		public string description;

		// Token: 0x04005AE5 RID: 23269
		public int killId;

		// Token: 0x04005AE6 RID: 23270
		public int mapId;
	}
}
