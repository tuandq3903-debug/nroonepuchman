using System;

namespace Game5.Assets.src.e
{
	// Token: 0x020001B4 RID: 436
	public class Small
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x0012C3C8 File Offset: 0x0012A5C8
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0012C3EC File Offset: 0x0012A5EC
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0012C44C File Offset: 0x0012A64C
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0012C470 File Offset: 0x0012A670
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor, bool isClip)
		{
			if (mGraphics.getImageWidth(this.img) != 1)
			{
				g.drawRegion(this.img, 0, f * w, w, h, transform, x, y, anchor, isClip);
				if (GameCanvas.gameTick % 1000 == 0)
				{
					this.timePaint++;
					this.timeUpdate = this.timePaint;
				}
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x0400250B RID: 9483
		public Image img;

		// Token: 0x0400250C RID: 9484
		public int id;

		// Token: 0x0400250D RID: 9485
		public int timePaint;

		// Token: 0x0400250E RID: 9486
		public int timeUpdate;
	}
}
