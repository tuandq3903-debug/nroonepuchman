using System;

namespace Game5
{
	// Token: 0x02000107 RID: 263
	public class EffectChar
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x000C03F6 File Offset: 0x000BE5F6
		public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
		{
			this.template = EffectChar.effTemplates[(int)templateId];
			this.timeStart = timeStart;
			this.timeLenght = timeLenght / 1000;
			this.param = param;
		}

		// Token: 0x04001664 RID: 5732
		public static EffectTemplate[] effTemplates;

		// Token: 0x04001665 RID: 5733
		public static sbyte EFF_FRIEND = 1;

		// Token: 0x04001666 RID: 5734
		public int timeStart;

		// Token: 0x04001667 RID: 5735
		public int timeLenght;

		// Token: 0x04001668 RID: 5736
		public short param;

		// Token: 0x04001669 RID: 5737
		public EffectTemplate template;
	}
}
