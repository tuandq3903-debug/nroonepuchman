using System;

namespace Game6
{
	// Token: 0x0200003C RID: 60
	public class FrameImage
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0002FC00 File Offset: 0x0002DE00
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

		// Token: 0x06000257 RID: 599 RVA: 0x0002FC6C File Offset: 0x0002DE6C
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

		// Token: 0x06000258 RID: 600 RVA: 0x0002FCBC File Offset: 0x0002DEBC
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

		// Token: 0x04000475 RID: 1141
		public int frameWidth;

		// Token: 0x04000476 RID: 1142
		public int frameHeight;

		// Token: 0x04000477 RID: 1143
		public int nFrame;

		// Token: 0x04000478 RID: 1144
		public Image imgFrame;

		// Token: 0x04000479 RID: 1145
		public int Id = -1;

		// Token: 0x0400047A RID: 1146
		public int numWidth;

		// Token: 0x0400047B RID: 1147
		public int numHeight;
	}
}
