using System;

namespace Game2
{
	// Token: 0x02000394 RID: 916
	public class EffectPaint
	{
		// Token: 0x060028A2 RID: 10402 RVA: 0x0028053E File Offset: 0x0027E73E
		public int getImgId()
		{
			return this.effCharPaint.arrEfInfo[this.index].idImg;
		}

		// Token: 0x04004DF9 RID: 19961
		public int index;

		// Token: 0x04004DFA RID: 19962
		public Mob eMob;

		// Token: 0x04004DFB RID: 19963
		public Char eChar;

		// Token: 0x04004DFC RID: 19964
		public EffectCharPaint effCharPaint;

		// Token: 0x04004DFD RID: 19965
		public bool isFly;
	}
}
