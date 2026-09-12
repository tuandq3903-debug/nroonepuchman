using System;

namespace Game1
{
	// Token: 0x02000474 RID: 1140
	public class FrameImage
	{
		// Token: 0x0600328A RID: 12938 RVA: 0x0031909C File Offset: 0x0031729C
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

		// Token: 0x0600328B RID: 12939 RVA: 0x00319108 File Offset: 0x00317308
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

		// Token: 0x0600328C RID: 12940 RVA: 0x00319158 File Offset: 0x00317358
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

		// Token: 0x040060F0 RID: 24816
		public int frameWidth;

		// Token: 0x040060F1 RID: 24817
		public int frameHeight;

		// Token: 0x040060F2 RID: 24818
		public int nFrame;

		// Token: 0x040060F3 RID: 24819
		public Image imgFrame;

		// Token: 0x040060F4 RID: 24820
		public int Id = -1;

		// Token: 0x040060F5 RID: 24821
		public int numWidth;

		// Token: 0x040060F6 RID: 24822
		public int numHeight;
	}
}
