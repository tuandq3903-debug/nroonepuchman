using System;

namespace Game5
{
	// Token: 0x0200010C RID: 268
	public class EffectPaint
	{
		// Token: 0x06000BB6 RID: 2998 RVA: 0x000C1352 File Offset: 0x000BF552
		public int getImgId()
		{
			return this.effCharPaint.arrEfInfo[this.index].idImg;
		}

		// Token: 0x0400167C RID: 5756
		public int index;

		// Token: 0x0400167D RID: 5757
		public Mob eMob;

		// Token: 0x0400167E RID: 5758
		public Char eChar;

		// Token: 0x0400167F RID: 5759
		public EffectCharPaint effCharPaint;

		// Token: 0x04001680 RID: 5760
		public bool isFly;
	}
}
