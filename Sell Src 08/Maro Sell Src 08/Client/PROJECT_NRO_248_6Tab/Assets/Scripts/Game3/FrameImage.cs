using System;

namespace Game3
{
	// Token: 0x020002C4 RID: 708
	public class FrameImage
	{
		// Token: 0x06001F42 RID: 8002 RVA: 0x001EEF54 File Offset: 0x001ED154
		public FrameImage(int ID)
		{
			this.Id = ID;
			Image image = Effect_End.getImage(ID);
			if (image != null)
			{
				this.imgFrame = image;
				this.frameWidth = (int)Effect_End.arrInfoEff[ID][0];
				this.frameHeight = (int)(Effect_End.arrInfoEff[ID][1] / Effect_End.arrInfoEff[ID][2]);
				this.nFrame = (int)Effect_End.arrInfoEff[ID][2];
			}
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x001EEFC0 File Offset: 0x001ED1C0
		public FrameImage(Image img, int width, int height)
		{
			if (img != null)
			{
				this.imgFrame = img;
				this.frameWidth = width;
				this.frameHeight = height;
				this.nFrame = img.getHeight() / height;
				if (this.nFrame < 1)
				{
					this.nFrame = 1;
				}
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x001EF010 File Offset: 0x001ED210
		public void drawFrame(int idx, int x, int y, int trans, int anchor, mGraphics g)
		{
			try
			{
				if (this.imgFrame != null)
				{
					if (idx > this.nFrame)
					{
						idx = this.nFrame;
					}
					int num = idx * this.frameHeight;
					if (num > this.frameHeight * (this.nFrame - 1) || num < 0)
					{
						num = this.frameHeight * (this.nFrame - 1);
					}
					g.drawRegion(this.imgFrame, 0, num, this.frameWidth, this.frameHeight, trans, x, y, anchor);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04003BF2 RID: 15346
		public int frameWidth;

		// Token: 0x04003BF3 RID: 15347
		public int frameHeight;

		// Token: 0x04003BF4 RID: 15348
		public int nFrame;

		// Token: 0x04003BF5 RID: 15349
		public Image imgFrame;

		// Token: 0x04003BF6 RID: 15350
		public int Id = -1;

		// Token: 0x04003BF7 RID: 15351
		public int numWidth;

		// Token: 0x04003BF8 RID: 15352
		public int numHeight;
	}
}
