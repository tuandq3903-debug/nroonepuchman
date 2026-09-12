using System;

namespace Game4
{
	// Token: 0x02000214 RID: 532
	public class MainImage
	{
		// Token: 0x06001752 RID: 5970 RVA: 0x00174412 File Offset: 0x00172612
		public MainImage()
		{
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00174434 File Offset: 0x00172634
		public MainImage(Image im, sbyte nFrame)
		{
			this.img = im;
			this.count = 0L;
			this.nFrame = nFrame;
		}

		// Token: 0x04002DCE RID: 11726
		public Image img;

		// Token: 0x04002DCF RID: 11727
		public long count = -1L;

		// Token: 0x04002DD0 RID: 11728
		public int timeImageNull;

		// Token: 0x04002DD1 RID: 11729
		public int idImage;

		// Token: 0x04002DD2 RID: 11730
		public long timerequest;

		// Token: 0x04002DD3 RID: 11731
		public sbyte nFrame = 1;

		// Token: 0x04002DD4 RID: 11732
		public long timeUse = mSystem.currentTimeMillis();
	}
}
