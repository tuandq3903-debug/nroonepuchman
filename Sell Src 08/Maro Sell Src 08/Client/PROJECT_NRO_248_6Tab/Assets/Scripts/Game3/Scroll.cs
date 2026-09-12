using System;

namespace Game3
{
	// Token: 0x02000322 RID: 802
	public class Scroll
	{
		// Token: 0x06002409 RID: 9225 RVA: 0x0023D020 File Offset: 0x0023B220
		public void clear()
		{
			this.cmtoX = 0;
			this.cmtoY = 0;
			this.cmx = 0;
			this.cmy = 0;
			this.cmvx = 0;
			this.cmvy = 0;
			this.cmdx = 0;
			this.cmdy = 0;
			this.cmxLim = 0;
			this.cmyLim = 0;
			this.width = 0;
			this.height = 0;
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x0023D081 File Offset: 0x0023B281
		public ScrollResult updateKey()
		{
			if (this.styleUPDOWN)
			{
				return this.updateKeyScrollUpDown(false);
			}
			return this.updateKeyScrollLeftRight();
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0023D09C File Offset: 0x0023B29C
		private ScrollResult updateKeyScrollUpDown(bool isGetNow)
		{
			int num = this.xPos;
			int num2 = this.yPos;
			int w = this.width;
			int h = this.height;
			if (GameCanvas.isPointerDown)
			{
				if (!this.pointerIsDowning && GameCanvas.isPointer(num, num2, w, h))
				{
					for (int i = 0; i < this.pointerDownLastX.Length; i++)
					{
						this.pointerDownLastX[0] = GameCanvas.py;
					}
					this.pointerDownFirstX = GameCanvas.py;
					this.pointerIsDowning = true;
					if (!isGetNow)
					{
						this.selectedItem = -1;
					}
					this.isDownWhenRunning = (this.cmRun != 0);
					this.cmRun = 0;
				}
				else if (this.pointerIsDowning)
				{
					this.pointerDownTime++;
					if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning)
					{
						this.pointerDownFirstX = -1000;
						if (this.ITEM_PER_LINE > 1)
						{
							int num3 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
							int num4 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
							this.selectedItem = num3 * this.ITEM_PER_LINE + num4;
						}
						else
						{
							this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
						}
					}
					int num5 = GameCanvas.py - this.pointerDownLastX[0];
					if (!isGetNow)
					{
						if (num5 != 0 && this.selectedItem != -1)
						{
							this.selectedItem = -1;
						}
					}
					else
					{
						this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					}
					for (int num6 = this.pointerDownLastX.Length - 1; num6 > 0; num6--)
					{
						this.pointerDownLastX[num6] = this.pointerDownLastX[num6 - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.py;
					this.cmtoY -= num5;
					if (this.cmtoY < 0)
					{
						this.cmtoY = 0;
					}
					if (this.cmtoY > this.cmyLim)
					{
						this.cmtoY = this.cmyLim;
					}
					if (this.cmy < 0 || this.cmy > this.cmyLim)
					{
						num5 /= 2;
					}
					this.cmy -= num5;
				}
			}
			bool isFinish = false;
			if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
			{
				int i2 = GameCanvas.py - this.pointerDownLastX[0];
				GameCanvas.isPointerJustRelease = false;
				if (Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
				{
					this.cmRun = 0;
					this.cmtoY = this.cmy;
					this.pointerDownFirstX = -1000;
					if (this.ITEM_PER_LINE > 1)
					{
						int num7 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
						int num8 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
						this.selectedItem = num7 * this.ITEM_PER_LINE + num8;
					}
					else
					{
						this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					}
					this.pointerDownTime = 0;
					isFinish = true;
				}
				else if (this.selectedItem != -1 && this.pointerDownTime > 5)
				{
					this.pointerDownTime = 0;
					isFinish = true;
				}
				else if ((this.selectedItem == -1 && !this.isDownWhenRunning) || (isGetNow && this.selectedItem != -1 && !this.isDownWhenRunning))
				{
					if (this.cmy < 0)
					{
						this.cmtoY = 0;
					}
					else if (this.cmy > this.cmyLim)
					{
						this.cmtoY = this.cmyLim;
					}
					else
					{
						int num9 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
						num9 = ((num9 > 10) ? 10 : ((num9 < -10) ? -10 : 0));
						this.cmRun = -num9 * 100;
					}
				}
				this.pointerIsDowning = false;
				this.pointerDownTime = 0;
				GameCanvas.isPointerJustRelease = false;
			}
			return new ScrollResult
			{
				selected = this.selectedItem,
				isFinish = isFinish,
				isDowning = this.pointerIsDowning
			};
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0023D4C8 File Offset: 0x0023B6C8
		private ScrollResult updateKeyScrollLeftRight()
		{
			int num = this.xPos;
			int y = this.yPos;
			int w = this.width;
			int h = this.height;
			if (GameCanvas.isPointerDown)
			{
				if (!this.pointerIsDowning && GameCanvas.isPointer(num, y, w, h))
				{
					for (int i = 0; i < this.pointerDownLastX.Length; i++)
					{
						this.pointerDownLastX[0] = GameCanvas.px;
					}
					this.pointerDownFirstX = GameCanvas.px;
					this.pointerIsDowning = true;
					this.selectedItem = -1;
					this.isDownWhenRunning = (this.cmRun != 0);
					this.cmRun = 0;
				}
				else if (this.pointerIsDowning)
				{
					this.pointerDownTime++;
					if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning)
					{
						this.pointerDownFirstX = -1000;
						this.selectedItem = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
					}
					int num2 = GameCanvas.px - this.pointerDownLastX[0];
					if (num2 != 0 && this.selectedItem != -1)
					{
						this.selectedItem = -1;
					}
					for (int num3 = this.pointerDownLastX.Length - 1; num3 > 0; num3--)
					{
						this.pointerDownLastX[num3] = this.pointerDownLastX[num3 - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.px;
					this.cmtoX -= num2;
					if (this.cmtoX < 0)
					{
						this.cmtoX = 0;
					}
					if (this.cmtoX > this.cmxLim)
					{
						this.cmtoX = this.cmxLim;
					}
					if (this.cmx < 0 || this.cmx > this.cmxLim)
					{
						num2 /= 2;
					}
					this.cmx -= num2;
				}
			}
			bool isFinish = false;
			if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
			{
				int i2 = GameCanvas.px - this.pointerDownLastX[0];
				GameCanvas.isPointerJustRelease = false;
				if (Res.abs(i2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
				{
					this.cmRun = 0;
					this.cmtoX = this.cmx;
					this.pointerDownFirstX = -1000;
					this.selectedItem = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
					this.pointerDownTime = 0;
					isFinish = true;
				}
				else if (this.selectedItem != -1 && this.pointerDownTime > 5)
				{
					this.pointerDownTime = 0;
					isFinish = true;
				}
				else if (this.selectedItem == -1 && !this.isDownWhenRunning)
				{
					if (this.cmx < 0)
					{
						this.cmtoX = 0;
					}
					else if (this.cmx > this.cmxLim)
					{
						this.cmtoX = this.cmxLim;
					}
					else
					{
						int num4 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
						num4 = ((num4 > 10) ? 10 : ((num4 < -10) ? -10 : 0));
						this.cmRun = -num4 * 100;
					}
				}
				this.pointerIsDowning = false;
				this.pointerDownTime = 0;
				GameCanvas.isPointerJustRelease = false;
			}
			return new ScrollResult
			{
				selected = this.selectedItem,
				isFinish = isFinish,
				isDowning = this.pointerIsDowning
			};
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0023D818 File Offset: 0x0023BA18
		public void updatecm()
		{
			if (this.cmRun != 0 && !this.pointerIsDowning)
			{
				if (this.styleUPDOWN)
				{
					this.cmtoY += this.cmRun / 100;
					if (this.cmtoY < 0)
					{
						this.cmtoY = 0;
					}
					else if (this.cmtoY > this.cmyLim)
					{
						this.cmtoY = this.cmyLim;
					}
					else
					{
						this.cmy = this.cmtoY;
					}
				}
				else
				{
					this.cmtoX += this.cmRun / 100;
					if (this.cmtoX < 0)
					{
						this.cmtoX = 0;
					}
					else if (this.cmtoX > this.cmxLim)
					{
						this.cmtoX = this.cmxLim;
					}
					else
					{
						this.cmx = this.cmtoX;
					}
				}
				this.cmRun = this.cmRun * 9 / 10;
				if (this.cmRun < 100 && this.cmRun > -100)
				{
					this.cmRun = 0;
				}
			}
			if (this.cmx != this.cmtoX && !this.pointerIsDowning)
			{
				this.cmvx = this.cmtoX - this.cmx << 2;
				this.cmdx += this.cmvx;
				this.cmx += this.cmdx >> 4;
				this.cmdx &= 15;
			}
			if (this.cmy != this.cmtoY && !this.pointerIsDowning)
			{
				this.cmvy = this.cmtoY - this.cmy << 2;
				this.cmdy += this.cmvy;
				this.cmy += this.cmdy >> 4;
				this.cmdy &= 15;
			}
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x0023D9D8 File Offset: 0x0023BBD8
		public void setStyle(int nItem, int ITEM_SIZE, int xPos, int yPos, int width, int height, bool styleUPDOWN, int ITEM_PER_LINE)
		{
			this.xPos = xPos;
			this.yPos = yPos;
			this.ITEM_SIZE = ITEM_SIZE;
			this.nITEM = nItem;
			this.width = width;
			this.height = height;
			this.styleUPDOWN = styleUPDOWN;
			this.ITEM_PER_LINE = ITEM_PER_LINE;
			Res.outz(string.Concat(new string[]
			{
				"nItem= ",
				nItem.ToString(),
				" ITEMSIZE= ",
				ITEM_SIZE.ToString(),
				" heghit= ",
				height.ToString()
			}));
			if (styleUPDOWN)
			{
				int num = nItem / ITEM_PER_LINE;
				if (nItem % ITEM_PER_LINE != 0)
				{
					num++;
				}
				this.cmyLim = num * ITEM_SIZE - height;
			}
			else
			{
				this.cmxLim = ITEM_PER_LINE * ITEM_SIZE - width;
			}
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			if (this.cmxLim < 0)
			{
				this.cmxLim = 0;
			}
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x0023DAB8 File Offset: 0x0023BCB8
		public void moveTo(int to)
		{
			if (this.styleUPDOWN)
			{
				to -= (this.height - this.ITEM_SIZE) / 2;
				this.cmtoY = to;
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
				if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
					return;
				}
			}
			else
			{
				to -= (this.width - this.ITEM_SIZE) / 2;
				this.cmtoX = to;
				if (this.cmtoX < 0)
				{
					this.cmtoX = 0;
				}
				if (this.cmtoX > this.cmxLim)
				{
					this.cmtoX = this.cmxLim;
				}
			}
		}

		// Token: 0x040046DD RID: 18141
		public int cmtoX;

		// Token: 0x040046DE RID: 18142
		public int cmtoY;

		// Token: 0x040046DF RID: 18143
		public int cmx;

		// Token: 0x040046E0 RID: 18144
		public int cmy;

		// Token: 0x040046E1 RID: 18145
		public int cmvx;

		// Token: 0x040046E2 RID: 18146
		public int cmvy;

		// Token: 0x040046E3 RID: 18147
		public int cmdx;

		// Token: 0x040046E4 RID: 18148
		public int cmdy;

		// Token: 0x040046E5 RID: 18149
		public int xPos;

		// Token: 0x040046E6 RID: 18150
		public int yPos;

		// Token: 0x040046E7 RID: 18151
		public int width;

		// Token: 0x040046E8 RID: 18152
		public int height;

		// Token: 0x040046E9 RID: 18153
		public int cmxLim;

		// Token: 0x040046EA RID: 18154
		public int cmyLim;

		// Token: 0x040046EB RID: 18155
		public static Scroll gI;

		// Token: 0x040046EC RID: 18156
		private int pointerDownTime;

		// Token: 0x040046ED RID: 18157
		private int pointerDownFirstX;

		// Token: 0x040046EE RID: 18158
		private int[] pointerDownLastX = new int[3];

		// Token: 0x040046EF RID: 18159
		public bool pointerIsDowning;

		// Token: 0x040046F0 RID: 18160
		public bool isDownWhenRunning;

		// Token: 0x040046F1 RID: 18161
		private int cmRun;

		// Token: 0x040046F2 RID: 18162
		public int selectedItem;

		// Token: 0x040046F3 RID: 18163
		public int ITEM_SIZE;

		// Token: 0x040046F4 RID: 18164
		public int nITEM;

		// Token: 0x040046F5 RID: 18165
		public int ITEM_PER_LINE;

		// Token: 0x040046F6 RID: 18166
		public bool styleUPDOWN = true;
	}
}
