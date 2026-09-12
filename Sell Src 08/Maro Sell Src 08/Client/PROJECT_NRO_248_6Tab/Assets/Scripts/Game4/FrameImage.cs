using System;

namespace Game4
{
	// Token: 0x020001EC RID: 492
	public class FrameImage
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x00159EB0 File Offset: 0x001580B0
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

		// Token: 0x0600159F RID: 5535 RVA: 0x00159F1C File Offset: 0x0015811C
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

		// Token: 0x060015A0 RID: 5536 RVA: 0x00159F6C File Offset: 0x0015816C
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

		// Token: 0x04002973 RID: 10611
		public int frameWidth;

		// Token: 0x04002974 RID: 10612
		public int frameHeight;

		// Token: 0x04002975 RID: 10613
		public int nFrame;

		// Token: 0x04002976 RID: 10614
		public Image imgFrame;

		// Token: 0x04002977 RID: 10615
		public int Id = -1;

		// Token: 0x04002978 RID: 10616
		public int numWidth;

		// Token: 0x04002979 RID: 10617
		public int numHeight;
	}
}
