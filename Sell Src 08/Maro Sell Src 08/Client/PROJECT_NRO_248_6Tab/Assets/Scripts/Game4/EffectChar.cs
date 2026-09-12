using System;

namespace Game4
{
	// Token: 0x020001DF RID: 479
	public class EffectChar
	{
		// Token: 0x0600153F RID: 5439 RVA: 0x0015549A File Offset: 0x0015369A
		public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
		{
			this.template = EffectChar.effTemplates[(int)templateId];
			this.timeStart = timeStart;
			this.timeLenght = timeLenght / 1000;
			this.param = param;
		}

		// Token: 0x040028E3 RID: 10467
		public static EffectTemplate[] effTemplates;

		// Token: 0x040028E4 RID: 10468
		public static sbyte EFF_FRIEND = 1;

		// Token: 0x040028E5 RID: 10469
		public int timeStart;

		// Token: 0x040028E6 RID: 10470
		public int timeLenght;

		// Token: 0x040028E7 RID: 10471
		public short param;

		// Token: 0x040028E8 RID: 10472
		public EffectTemplate template;
	}
}
