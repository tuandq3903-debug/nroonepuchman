using System;

namespace Game3
{
	// Token: 0x020002B7 RID: 695
	public class EffectChar
	{
		// Token: 0x06001EE3 RID: 7907 RVA: 0x001EA53E File Offset: 0x001E873E
		public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
		{
			this.template = EffectChar.effTemplates[(int)templateId];
			this.timeStart = timeStart;
			this.timeLenght = timeLenght / 1000;
			this.param = param;
		}

		// Token: 0x04003B62 RID: 15202
		public static EffectTemplate[] effTemplates;

		// Token: 0x04003B63 RID: 15203
		public static sbyte EFF_FRIEND = 1;

		// Token: 0x04003B64 RID: 15204
		public int timeStart;

		// Token: 0x04003B65 RID: 15205
		public int timeLenght;

		// Token: 0x04003B66 RID: 15206
		public short param;

		// Token: 0x04003B67 RID: 15207
		public EffectTemplate template;
	}
}
