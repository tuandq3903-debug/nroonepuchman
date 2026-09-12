using System;

namespace Game6
{
	// Token: 0x0200002F RID: 47
	public class EffectChar
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0002B1EA File Offset: 0x000293EA
		public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
		{
			this.template = EffectChar.effTemplates[(int)templateId];
			this.timeStart = timeStart;
			this.timeLenght = timeLenght / 1000;
			this.param = param;
		}

		// Token: 0x040003E5 RID: 997
		public static EffectTemplate[] effTemplates;

		// Token: 0x040003E6 RID: 998
		public static sbyte EFF_FRIEND = 1;

		// Token: 0x040003E7 RID: 999
		public int timeStart;

		// Token: 0x040003E8 RID: 1000
		public int timeLenght;

		// Token: 0x040003E9 RID: 1001
		public short param;

		// Token: 0x040003EA RID: 1002
		public EffectTemplate template;
	}
}
