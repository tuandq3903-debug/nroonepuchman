using System;

namespace Game3
{
	// Token: 0x02000344 RID: 836
	public class TaskOrder
	{
		// Token: 0x06002579 RID: 9593 RVA: 0x00249AE0 File Offset: 0x00247CE0
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

		// Token: 0x04004861 RID: 18529
		public int taskId;

		// Token: 0x04004862 RID: 18530
		public int count;

		// Token: 0x04004863 RID: 18531
		public short maxCount;

		// Token: 0x04004864 RID: 18532
		public string name;

		// Token: 0x04004865 RID: 18533
		public string description;

		// Token: 0x04004866 RID: 18534
		public int killId;

		// Token: 0x04004867 RID: 18535
		public int mapId;
	}
}
