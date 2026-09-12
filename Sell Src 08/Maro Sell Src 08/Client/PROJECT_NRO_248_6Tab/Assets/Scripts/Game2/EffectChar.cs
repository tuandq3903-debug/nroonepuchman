using System;

namespace Game2
{
	// Token: 0x0200038F RID: 911
	public class EffectChar
	{
		// Token: 0x06002887 RID: 10375 RVA: 0x0027F5E2 File Offset: 0x0027D7E2
		public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
		{
			this.template = EffectChar.effTemplates[(int)templateId];
			this.timeStart = timeStart;
			this.timeLenght = timeLenght / 1000;
			this.param = param;
		}

		// Token: 0x04004DE1 RID: 19937
		public static EffectTemplate[] effTemplates;

		// Token: 0x04004DE2 RID: 19938
		public static sbyte EFF_FRIEND = 1;

		// Token: 0x04004DE3 RID: 19939
		public int timeStart;

		// Token: 0x04004DE4 RID: 19940
		public int timeLenght;

		// Token: 0x04004DE5 RID: 19941
		public short param;

		// Token: 0x04004DE6 RID: 19942
		public EffectTemplate template;
	}
}
