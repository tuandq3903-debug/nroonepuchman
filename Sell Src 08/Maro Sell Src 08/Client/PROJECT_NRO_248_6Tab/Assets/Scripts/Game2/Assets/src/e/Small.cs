using System;

namespace Game2.Assets.src.e
{
	// Token: 0x0200043C RID: 1084
	public class Small
	{
		// Token: 0x06003039 RID: 12345 RVA: 0x002EB5B4 File Offset: 0x002E97B4
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x002EB5D8 File Offset: 0x002E97D8
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x002EB638 File Offset: 0x002E9838
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x002EB65C File Offset: 0x002E985C
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

		// Token: 0x0600303D RID: 12349 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x04005C88 RID: 23688
		public Image img;

		// Token: 0x04005C89 RID: 23689
		public int id;

		// Token: 0x04005C8A RID: 23690
		public int timePaint;

		// Token: 0x04005C8B RID: 23691
		public int timeUpdate;
	}
}
