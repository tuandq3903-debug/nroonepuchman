using System;

namespace Game1
{
	// Token: 0x0200049C RID: 1180
	public class MainImage
	{
		// Token: 0x0600343E RID: 13374 RVA: 0x003335FE File Offset: 0x003317FE
		public MainImage()
		{
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x00333620 File Offset: 0x00331820
		public MainImage(Image im, sbyte nFrame)
		{
			this.img = im;
			this.count = 0L;
			this.nFrame = nFrame;
		}

		// Token: 0x0400654B RID: 25931
		public Image img;

		// Token: 0x0400654C RID: 25932
		public long count = -1L;

		// Token: 0x0400654D RID: 25933
		public int timeImageNull;

		// Token: 0x0400654E RID: 25934
		public int idImage;

		// Token: 0x0400654F RID: 25935
		public long timerequest;

		// Token: 0x04006550 RID: 25936
		public sbyte nFrame = 1;

		// Token: 0x04006551 RID: 25937
		public long timeUse = mSystem.currentTimeMillis();
	}
}
