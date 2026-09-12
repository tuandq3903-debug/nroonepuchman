using System;

namespace Game6
{
	// Token: 0x020000BC RID: 188
	public class TaskOrder
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x0008A880 File Offset: 0x00088A80
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

		// Token: 0x040010E4 RID: 4324
		public int taskId;

		// Token: 0x040010E5 RID: 4325
		public int count;

		// Token: 0x040010E6 RID: 4326
		public short maxCount;

		// Token: 0x040010E7 RID: 4327
		public string name;

		// Token: 0x040010E8 RID: 4328
		public string description;

		// Token: 0x040010E9 RID: 4329
		public int killId;

		// Token: 0x040010EA RID: 4330
		public int mapId;
	}
}
