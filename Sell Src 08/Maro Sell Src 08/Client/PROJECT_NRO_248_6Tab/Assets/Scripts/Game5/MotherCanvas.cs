using System;

namespace Game5
{
	// Token: 0x0200014E RID: 334
	public class MotherCanvas
	{
		// Token: 0x06000EC2 RID: 3778 RVA: 0x000ED9A4 File Offset: 0x000EBBA4
		public MotherCanvas()
		{
			this.checkZoomLevel(this.getWidth(), this.getHeight());
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000ED9D0 File Offset: 0x000EBBD0
		public void checkZoomLevel(int w, int h)
		{
			if (Main.isWindowsPhone)
			{
				mGraphics.zoomLevel = 2;
				if (w * h >= 2073600)
				{
					mGraphics.zoomLevel = 4;
					return;
				}
				if (w * h > 384000)
				{
					mGraphics.zoomLevel = 3;
					return;
				}
			}
			else if (!Main.isPC || Main.isIPhone)
			{
				if (Main.isIpod)
				{
					mGraphics.zoomLevel = 2;
					return;
				}
				if (w * h >= 2073600)
				{
					mGraphics.zoomLevel = 4;
					return;
				}
				if (w * h >= 691200)
				{
					mGraphics.zoomLevel = 3;
					return;
				}
				if (w * h > 153600)
				{
					mGraphics.zoomLevel = 2;
					return;
				}
			}
			else
			{
				mGraphics.zoomLevel = 2;
				if (w * h < 480000)
				{
					mGraphics.zoomLevel = 1;
				}
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000EDA71 File Offset: 0x000EBC71
		public int getWidth()
		{
			return (int)ScaleGUI.WIDTH;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000EDA79 File Offset: 0x000EBC79
		public int getHeight()
		{
			return (int)ScaleGUI.HEIGHT;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000EDA81 File Offset: 0x000EBC81
		public void setChildCanvas(GameCanvas tCanvas)
		{
			this.tCanvas = tCanvas;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000EDA8C File Offset: 0x000EBC8C
		public int getWidthz()
		{
			int width = this.getWidth();
			return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x000EDAB0 File Offset: 0x000EBCB0
		public int getHeightz()
		{
			int height = this.getHeight();
			return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
		}

		// Token: 0x04001D7C RID: 7548
		public static MotherCanvas instance;

		// Token: 0x04001D7D RID: 7549
		public GameCanvas tCanvas;

		// Token: 0x04001D7E RID: 7550
		public int zoomLevel = 1;

		// Token: 0x04001D7F RID: 7551
		private int OUTPUTSIZE = 20;
	}
}
