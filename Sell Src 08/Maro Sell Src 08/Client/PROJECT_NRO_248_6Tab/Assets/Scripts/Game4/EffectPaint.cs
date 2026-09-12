using System;

namespace Game4
{
	// Token: 0x020001E4 RID: 484
	public class EffectPaint
	{
		// Token: 0x0600155A RID: 5466 RVA: 0x001563F6 File Offset: 0x001545F6
		public int getImgId()
		{
			return this.effCharPaint.arrEfInfo[this.index].idImg;
		}

		// Token: 0x040028FB RID: 10491
		public int index;

		// Token: 0x040028FC RID: 10492
		public Mob eMob;

		// Token: 0x040028FD RID: 10493
		public Char eChar;

		// Token: 0x040028FE RID: 10494
		public EffectCharPaint effCharPaint;

		// Token: 0x040028FF RID: 10495
		public bool isFly;
	}
}
