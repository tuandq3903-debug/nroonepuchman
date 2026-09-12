using System;

namespace Game6
{
	// Token: 0x02000034 RID: 52
	public class EffectPaint
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0002C146 File Offset: 0x0002A346
		public int getImgId()
		{
			return this.effCharPaint.arrEfInfo[this.index].idImg;
		}

		// Token: 0x040003FD RID: 1021
		public int index;

		// Token: 0x040003FE RID: 1022
		public Mob eMob;

		// Token: 0x040003FF RID: 1023
		public Char eChar;

		// Token: 0x04000400 RID: 1024
		public EffectCharPaint effCharPaint;

		// Token: 0x04000401 RID: 1025
		public bool isFly;
	}
}
