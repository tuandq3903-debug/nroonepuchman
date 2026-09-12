using System;

namespace Game3
{
	// Token: 0x020002FE RID: 766
	public class MotherCanvas
	{
		// Token: 0x0600220A RID: 8714 RVA: 0x00217AEC File Offset: 0x00215CEC
		public MotherCanvas()
		{
			this.checkZoomLevel(this.getWidth(), this.getHeight());
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x00217B18 File Offset: 0x00215D18
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

		// Token: 0x0600220C RID: 8716 RVA: 0x00217BB9 File Offset: 0x00215DB9
		public int getWidth()
		{
			return (int)ScaleGUI.WIDTH;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00217BC1 File Offset: 0x00215DC1
		public int getHeight()
		{
			return (int)ScaleGUI.HEIGHT;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00217BC9 File Offset: 0x00215DC9
		public void setChildCanvas(GameCanvas tCanvas)
		{
			this.tCanvas = tCanvas;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00217BD4 File Offset: 0x00215DD4
		public int getWidthz()
		{
			int width = this.getWidth();
			return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00217BF8 File Offset: 0x00215DF8
		public int getHeightz()
		{
			int height = this.getHeight();
			return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
		}

		// Token: 0x0400427A RID: 17018
		public static MotherCanvas instance;

		// Token: 0x0400427B RID: 17019
		public GameCanvas tCanvas;

		// Token: 0x0400427C RID: 17020
		public int zoomLevel = 1;

		// Token: 0x0400427D RID: 17021
		private int OUTPUTSIZE = 20;
	}
}
