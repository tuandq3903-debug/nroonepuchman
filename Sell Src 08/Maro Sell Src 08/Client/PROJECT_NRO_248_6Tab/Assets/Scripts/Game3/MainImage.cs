using System;

namespace Game3
{
	// Token: 0x020002EC RID: 748
	public class MainImage
	{
		// Token: 0x060020F6 RID: 8438 RVA: 0x002094B6 File Offset: 0x002076B6
		public MainImage()
		{
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x002094D8 File Offset: 0x002076D8
		public MainImage(Image im, sbyte nFrame)
		{
			this.img = im;
			this.count = 0L;
			this.nFrame = nFrame;
		}

		// Token: 0x0400404D RID: 16461
		public Image img;

		// Token: 0x0400404E RID: 16462
		public long count = -1L;

		// Token: 0x0400404F RID: 16463
		public int timeImageNull;

		// Token: 0x04004050 RID: 16464
		public int idImage;

		// Token: 0x04004051 RID: 16465
		public long timerequest;

		// Token: 0x04004052 RID: 16466
		public sbyte nFrame = 1;

		// Token: 0x04004053 RID: 16467
		public long timeUse = mSystem.currentTimeMillis();
	}
}
