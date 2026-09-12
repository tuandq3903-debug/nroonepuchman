using System;

namespace Game5
{
	// Token: 0x02000114 RID: 276
	public class FrameImage
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x000C4E0C File Offset: 0x000C300C
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

		// Token: 0x06000BFB RID: 3067 RVA: 0x000C4E78 File Offset: 0x000C3078
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

		// Token: 0x06000BFC RID: 3068 RVA: 0x000C4EC8 File Offset: 0x000C30C8
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

		// Token: 0x040016F4 RID: 5876
		public int frameWidth;

		// Token: 0x040016F5 RID: 5877
		public int frameHeight;

		// Token: 0x040016F6 RID: 5878
		public int nFrame;

		// Token: 0x040016F7 RID: 5879
		public Image imgFrame;

		// Token: 0x040016F8 RID: 5880
		public int Id = -1;

		// Token: 0x040016F9 RID: 5881
		public int numWidth;

		// Token: 0x040016FA RID: 5882
		public int numHeight;
	}
}
