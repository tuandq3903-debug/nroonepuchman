using System;

namespace Game5
{
	// Token: 0x0200013C RID: 316
	public class MainImage
	{
		// Token: 0x06000DAE RID: 3502 RVA: 0x000DF36E File Offset: 0x000DD56E
		public MainImage()
		{
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000DF390 File Offset: 0x000DD590
		public MainImage(Image im, sbyte nFrame)
		{
			this.img = im;
			this.count = 0L;
			this.nFrame = nFrame;
		}

		// Token: 0x04001B4F RID: 6991
		public Image img;

		// Token: 0x04001B50 RID: 6992
		public long count = -1L;

		// Token: 0x04001B51 RID: 6993
		public int timeImageNull;

		// Token: 0x04001B52 RID: 6994
		public int idImage;

		// Token: 0x04001B53 RID: 6995
		public long timerequest;

		// Token: 0x04001B54 RID: 6996
		public sbyte nFrame = 1;

		// Token: 0x04001B55 RID: 6997
		public long timeUse = mSystem.currentTimeMillis();
	}
}
