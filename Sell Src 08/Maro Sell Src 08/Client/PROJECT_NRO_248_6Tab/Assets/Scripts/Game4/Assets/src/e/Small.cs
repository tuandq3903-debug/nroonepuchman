using System;

namespace Game4.Assets.src.e
{
	// Token: 0x0200028C RID: 652
	public class Small
	{
		// Token: 0x06001CF1 RID: 7409 RVA: 0x001C146C File Offset: 0x001BF66C
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x001C1490 File Offset: 0x001BF690
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x001C14F0 File Offset: 0x001BF6F0
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x001C1514 File Offset: 0x001BF714
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

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x0400378A RID: 14218
		public Image img;

		// Token: 0x0400378B RID: 14219
		public int id;

		// Token: 0x0400378C RID: 14220
		public int timePaint;

		// Token: 0x0400378D RID: 14221
		public int timeUpdate;
	}
}
