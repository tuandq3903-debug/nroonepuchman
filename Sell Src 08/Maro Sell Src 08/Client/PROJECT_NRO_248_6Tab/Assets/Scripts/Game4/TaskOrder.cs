using System;

namespace Game4
{
	// Token: 0x0200026C RID: 620
	public class TaskOrder
	{
		// Token: 0x06001BD5 RID: 7125 RVA: 0x001B4A3C File Offset: 0x001B2C3C
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

		// Token: 0x040035E2 RID: 13794
		public int taskId;

		// Token: 0x040035E3 RID: 13795
		public int count;

		// Token: 0x040035E4 RID: 13796
		public short maxCount;

		// Token: 0x040035E5 RID: 13797
		public string name;

		// Token: 0x040035E6 RID: 13798
		public string description;

		// Token: 0x040035E7 RID: 13799
		public int killId;

		// Token: 0x040035E8 RID: 13800
		public int mapId;
	}
}
