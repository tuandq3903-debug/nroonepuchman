using System;

namespace Game1
{
	// Token: 0x020004F4 RID: 1268
	public class TaskOrder
	{
		// Token: 0x060038C1 RID: 14529 RVA: 0x00373C28 File Offset: 0x00371E28
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

		// Token: 0x04006D5F RID: 27999
		public int taskId;

		// Token: 0x04006D60 RID: 28000
		public int count;

		// Token: 0x04006D61 RID: 28001
		public short maxCount;

		// Token: 0x04006D62 RID: 28002
		public string name;

		// Token: 0x04006D63 RID: 28003
		public string description;

		// Token: 0x04006D64 RID: 28004
		public int killId;

		// Token: 0x04006D65 RID: 28005
		public int mapId;
	}
}
