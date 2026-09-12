using System;

namespace Game2
{
	// Token: 0x0200039C RID: 924
	public class FrameImage
	{
		// Token: 0x060028E6 RID: 10470 RVA: 0x00283FF8 File Offset: 0x002821F8
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

		// Token: 0x060028E7 RID: 10471 RVA: 0x00284064 File Offset: 0x00282264
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

		// Token: 0x060028E8 RID: 10472 RVA: 0x002840B4 File Offset: 0x002822B4
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

		// Token: 0x04004E71 RID: 20081
		public int frameWidth;

		// Token: 0x04004E72 RID: 20082
		public int frameHeight;

		// Token: 0x04004E73 RID: 20083
		public int nFrame;

		// Token: 0x04004E74 RID: 20084
		public Image imgFrame;

		// Token: 0x04004E75 RID: 20085
		public int Id = -1;

		// Token: 0x04004E76 RID: 20086
		public int numWidth;

		// Token: 0x04004E77 RID: 20087
		public int numHeight;
	}
}
