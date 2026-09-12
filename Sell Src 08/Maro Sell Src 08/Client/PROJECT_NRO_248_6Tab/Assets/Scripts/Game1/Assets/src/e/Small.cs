using System;

namespace Game1.Assets.src.e
{
	// Token: 0x02000514 RID: 1300
	public class Small
	{
		// Token: 0x060039DD RID: 14813 RVA: 0x00380658 File Offset: 0x0037E858
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x0038067C File Offset: 0x0037E87C
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x003806DC File Offset: 0x0037E8DC
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x00380700 File Offset: 0x0037E900
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

		// Token: 0x060039E1 RID: 14817 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x04006F07 RID: 28423
		public Image img;

		// Token: 0x04006F08 RID: 28424
		public int id;

		// Token: 0x04006F09 RID: 28425
		public int timePaint;

		// Token: 0x04006F0A RID: 28426
		public int timeUpdate;
	}
}
