using System;

namespace Game2
{
	// Token: 0x020003C4 RID: 964
	public class MainImage
	{
		// Token: 0x06002A9A RID: 10906 RVA: 0x0029E55A File Offset: 0x0029C75A
		public MainImage()
		{
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x0029E57C File Offset: 0x0029C77C
		public MainImage(Image im, sbyte nFrame)
		{
			this.img = im;
			this.count = 0L;
			this.nFrame = nFrame;
		}

		// Token: 0x040052CC RID: 21196
		public Image img;

		// Token: 0x040052CD RID: 21197
		public long count = -1L;

		// Token: 0x040052CE RID: 21198
		public int timeImageNull;

		// Token: 0x040052CF RID: 21199
		public int idImage;

		// Token: 0x040052D0 RID: 21200
		public long timerequest;

		// Token: 0x040052D1 RID: 21201
		public sbyte nFrame = 1;

		// Token: 0x040052D2 RID: 21202
		public long timeUse = mSystem.currentTimeMillis();
	}
}
