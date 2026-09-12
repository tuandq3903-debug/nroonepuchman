using System;

namespace Game1
{
	// Token: 0x02000467 RID: 1127
	public class EffectChar
	{
		// Token: 0x0600322B RID: 12843 RVA: 0x00314686 File Offset: 0x00312886
		public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
		{
			this.template = EffectChar.effTemplates[(int)templateId];
			this.timeStart = timeStart;
			this.timeLenght = timeLenght / 1000;
			this.param = param;
		}

		// Token: 0x04006060 RID: 24672
		public static EffectTemplate[] effTemplates;

		// Token: 0x04006061 RID: 24673
		public static sbyte EFF_FRIEND = 1;

		// Token: 0x04006062 RID: 24674
		public int timeStart;

		// Token: 0x04006063 RID: 24675
		public int timeLenght;

		// Token: 0x04006064 RID: 24676
		public short param;

		// Token: 0x04006065 RID: 24677
		public EffectTemplate template;
	}
}
