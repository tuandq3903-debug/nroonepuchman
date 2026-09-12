using System;

namespace Game4
{
	// Token: 0x02000226 RID: 550
	public class MotherCanvas
	{
		// Token: 0x06001866 RID: 6246 RVA: 0x00182A48 File Offset: 0x00180C48
		public MotherCanvas()
		{
			this.checkZoomLevel(this.getWidth(), this.getHeight());
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00182A74 File Offset: 0x00180C74
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

		// Token: 0x06001868 RID: 6248 RVA: 0x00182B15 File Offset: 0x00180D15
		public int getWidth()
		{
			return (int)ScaleGUI.WIDTH;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00182B1D File Offset: 0x00180D1D
		public int getHeight()
		{
			return (int)ScaleGUI.HEIGHT;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00182B25 File Offset: 0x00180D25
		public void setChildCanvas(GameCanvas tCanvas)
		{
			this.tCanvas = tCanvas;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00182B30 File Offset: 0x00180D30
		public int getWidthz()
		{
			int width = this.getWidth();
			return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00182B54 File Offset: 0x00180D54
		public int getHeightz()
		{
			int height = this.getHeight();
			return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
		}

		// Token: 0x04002FFB RID: 12283
		public static MotherCanvas instance;

		// Token: 0x04002FFC RID: 12284
		public GameCanvas tCanvas;

		// Token: 0x04002FFD RID: 12285
		public int zoomLevel = 1;

		// Token: 0x04002FFE RID: 12286
		private int OUTPUTSIZE = 20;
	}
}
