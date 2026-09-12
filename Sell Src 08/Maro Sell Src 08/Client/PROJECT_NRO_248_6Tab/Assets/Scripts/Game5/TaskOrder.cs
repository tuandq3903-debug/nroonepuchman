using System;

namespace Game5
{
	// Token: 0x02000194 RID: 404
	public class TaskOrder
	{
		// Token: 0x06001231 RID: 4657 RVA: 0x0011F998 File Offset: 0x0011DB98
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

		// Token: 0x04002363 RID: 9059
		public int taskId;

		// Token: 0x04002364 RID: 9060
		public int count;

		// Token: 0x04002365 RID: 9061
		public short maxCount;

		// Token: 0x04002366 RID: 9062
		public string name;

		// Token: 0x04002367 RID: 9063
		public string description;

		// Token: 0x04002368 RID: 9064
		public int killId;

		// Token: 0x04002369 RID: 9065
		public int mapId;
	}
}
