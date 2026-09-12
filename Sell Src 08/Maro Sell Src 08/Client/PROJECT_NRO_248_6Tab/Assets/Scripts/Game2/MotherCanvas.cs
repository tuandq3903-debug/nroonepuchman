using System;

namespace Game2
{
	// Token: 0x020003D6 RID: 982
	public class MotherCanvas
	{
		// Token: 0x06002BAE RID: 11182 RVA: 0x002ACB90 File Offset: 0x002AAD90
		public MotherCanvas()
		{
			this.checkZoomLevel(this.getWidth(), this.getHeight());
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x002ACBBC File Offset: 0x002AADBC
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

		// Token: 0x06002BB0 RID: 11184 RVA: 0x002ACC5D File Offset: 0x002AAE5D
		public int getWidth()
		{
			return (int)ScaleGUI.WIDTH;
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x002ACC65 File Offset: 0x002AAE65
		public int getHeight()
		{
			return (int)ScaleGUI.HEIGHT;
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x002ACC6D File Offset: 0x002AAE6D
		public void setChildCanvas(GameCanvas tCanvas)
		{
			this.tCanvas = tCanvas;
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x002ACC78 File Offset: 0x002AAE78
		public int getWidthz()
		{
			int width = this.getWidth();
			return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x002ACC9C File Offset: 0x002AAE9C
		public int getHeightz()
		{
			int height = this.getHeight();
			return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
		}

		// Token: 0x040054F9 RID: 21753
		public static MotherCanvas instance;

		// Token: 0x040054FA RID: 21754
		public GameCanvas tCanvas;

		// Token: 0x040054FB RID: 21755
		public int zoomLevel = 1;

		// Token: 0x040054FC RID: 21756
		private int OUTPUTSIZE = 20;
	}
}
