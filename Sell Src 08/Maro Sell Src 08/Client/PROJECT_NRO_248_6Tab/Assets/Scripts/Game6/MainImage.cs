using System;

namespace Game6
{
	// Token: 0x02000064 RID: 100
	public class MainImage
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0004A1EA File Offset: 0x000483EA
		public MainImage()
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0004A20C File Offset: 0x0004840C
		public MainImage(Image im, sbyte nFrame)
		{
			this.img = im;
			this.count = 0L;
			this.nFrame = nFrame;
		}

		// Token: 0x040008D0 RID: 2256
		public Image img;

		// Token: 0x040008D1 RID: 2257
		public long count = -1L;

		// Token: 0x040008D2 RID: 2258
		public int timeImageNull;

		// Token: 0x040008D3 RID: 2259
		public int idImage;

		// Token: 0x040008D4 RID: 2260
		public long timerequest;

		// Token: 0x040008D5 RID: 2261
		public sbyte nFrame = 1;

		// Token: 0x040008D6 RID: 2262
		public long timeUse = mSystem.currentTimeMillis();
	}
}
