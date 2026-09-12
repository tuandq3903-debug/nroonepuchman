using System;

namespace Game1
{
	// Token: 0x020004AE RID: 1198
	public class MotherCanvas
	{
		// Token: 0x06003552 RID: 13650 RVA: 0x00341C34 File Offset: 0x0033FE34
		public MotherCanvas()
		{
			this.checkZoomLevel(this.getWidth(), this.getHeight());
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x00341C60 File Offset: 0x0033FE60
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

		// Token: 0x06003554 RID: 13652 RVA: 0x00341D01 File Offset: 0x0033FF01
		public int getWidth()
		{
			return (int)ScaleGUI.WIDTH;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x00341D09 File Offset: 0x0033FF09
		public int getHeight()
		{
			return (int)ScaleGUI.HEIGHT;
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x00341D11 File Offset: 0x0033FF11
		public void setChildCanvas(GameCanvas tCanvas)
		{
			this.tCanvas = tCanvas;
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x00341D1C File Offset: 0x0033FF1C
		public int getWidthz()
		{
			int width = this.getWidth();
			return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x00341D40 File Offset: 0x0033FF40
		public int getHeightz()
		{
			int height = this.getHeight();
			return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
		}

		// Token: 0x04006778 RID: 26488
		public static MotherCanvas instance;

		// Token: 0x04006779 RID: 26489
		public GameCanvas tCanvas;

		// Token: 0x0400677A RID: 26490
		public int zoomLevel = 1;

		// Token: 0x0400677B RID: 26491
		private int OUTPUTSIZE = 20;
	}
}
