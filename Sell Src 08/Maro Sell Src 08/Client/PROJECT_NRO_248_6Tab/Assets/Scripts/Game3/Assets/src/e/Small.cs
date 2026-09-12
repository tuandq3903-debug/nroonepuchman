using System;

namespace Game3.Assets.src.e
{
	// Token: 0x02000364 RID: 868
	public class Small
	{
		// Token: 0x06002695 RID: 9877 RVA: 0x00256510 File Offset: 0x00254710
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x00256534 File Offset: 0x00254734
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x00256594 File Offset: 0x00254794
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x002565B8 File Offset: 0x002547B8
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

		// Token: 0x06002699 RID: 9881 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x04004A09 RID: 18953
		public Image img;

		// Token: 0x04004A0A RID: 18954
		public int id;

		// Token: 0x04004A0B RID: 18955
		public int timePaint;

		// Token: 0x04004A0C RID: 18956
		public int timeUpdate;
	}
}
