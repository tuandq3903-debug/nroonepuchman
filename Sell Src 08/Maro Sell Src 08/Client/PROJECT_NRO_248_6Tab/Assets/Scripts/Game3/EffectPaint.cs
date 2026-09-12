using System;

namespace Game3
{
	// Token: 0x020002BC RID: 700
	public class EffectPaint
	{
		// Token: 0x06001EFE RID: 7934 RVA: 0x001EB49A File Offset: 0x001E969A
		public int getImgId()
		{
			return this.effCharPaint.arrEfInfo[this.index].idImg;
		}

		// Token: 0x04003B7A RID: 15226
		public int index;

		// Token: 0x04003B7B RID: 15227
		public Mob eMob;

		// Token: 0x04003B7C RID: 15228
		public Char eChar;

		// Token: 0x04003B7D RID: 15229
		public EffectCharPaint effCharPaint;

		// Token: 0x04003B7E RID: 15230
		public bool isFly;
	}
}
