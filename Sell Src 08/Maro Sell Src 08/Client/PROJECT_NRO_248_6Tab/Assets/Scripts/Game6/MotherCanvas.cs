using System;

namespace Game6
{
	// Token: 0x02000076 RID: 118
	public class MotherCanvas
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x00058828 File Offset: 0x00056A28
		public MotherCanvas()
		{
			this.checkZoomLevel(this.getWidth(), this.getHeight());
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00058854 File Offset: 0x00056A54
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

		// Token: 0x06000520 RID: 1312 RVA: 0x000588F5 File Offset: 0x00056AF5
		public int getWidth()
		{
			return (int)ScaleGUI.WIDTH;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000588FD File Offset: 0x00056AFD
		public int getHeight()
		{
			return (int)ScaleGUI.HEIGHT;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00058905 File Offset: 0x00056B05
		public void setChildCanvas(GameCanvas tCanvas)
		{
			this.tCanvas = tCanvas;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00058910 File Offset: 0x00056B10
		public int getWidthz()
		{
			int width = this.getWidth();
			return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00058934 File Offset: 0x00056B34
		public int getHeightz()
		{
			int height = this.getHeight();
			return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
		}

		// Token: 0x04000AFD RID: 2813
		public static MotherCanvas instance;

		// Token: 0x04000AFE RID: 2814
		public GameCanvas tCanvas;

		// Token: 0x04000AFF RID: 2815
		public int zoomLevel = 1;

		// Token: 0x04000B00 RID: 2816
		private int OUTPUTSIZE = 20;
	}
}
