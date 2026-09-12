using System;

namespace Game1
{
	// Token: 0x0200046C RID: 1132
	public class EffectPaint
	{
		// Token: 0x06003246 RID: 12870 RVA: 0x003155E2 File Offset: 0x003137E2
		public int getImgId()
		{
			return this.effCharPaint.arrEfInfo[this.index].idImg;
		}

		// Token: 0x04006078 RID: 24696
		public int index;

		// Token: 0x04006079 RID: 24697
		public Mob eMob;

		// Token: 0x0400607A RID: 24698
		public Char eChar;

		// Token: 0x0400607B RID: 24699
		public EffectCharPaint effCharPaint;

		// Token: 0x0400607C RID: 24700
		public bool isFly;
	}
}
