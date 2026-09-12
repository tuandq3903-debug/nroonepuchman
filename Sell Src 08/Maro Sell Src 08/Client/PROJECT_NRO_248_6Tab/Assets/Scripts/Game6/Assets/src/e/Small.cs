using System;

namespace Game6.Assets.src.e
{
	// Token: 0x020000DC RID: 220
	public class Small
	{
		// Token: 0x060009A9 RID: 2473 RVA: 0x00097324 File Offset: 0x00095524
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00097348 File Offset: 0x00095548
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000973A8 File Offset: 0x000955A8
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000973CC File Offset: 0x000955CC
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

		// Token: 0x060009AD RID: 2477 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x0400128C RID: 4748
		public Image img;

		// Token: 0x0400128D RID: 4749
		public int id;

		// Token: 0x0400128E RID: 4750
		public int timePaint;

		// Token: 0x0400128F RID: 4751
		public int timeUpdate;
	}
}
