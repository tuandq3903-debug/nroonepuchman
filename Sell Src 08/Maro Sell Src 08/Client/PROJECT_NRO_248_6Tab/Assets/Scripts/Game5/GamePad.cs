using System;

namespace Game5
{
	// Token: 0x02000117 RID: 279
	public class GamePad
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x000CA5E8 File Offset: 0x000C87E8
		public GamePad()
		{
			this.R = 28;
			if (GameCanvas.w < 300)
			{
				this.isSmallGamePad = true;
				this.isMediumGamePad = false;
				this.isLargeGamePad = false;
			}
			if (GameCanvas.w >= 300 && GameCanvas.w <= 380)
			{
				this.isSmallGamePad = false;
				this.isMediumGamePad = true;
				this.isLargeGamePad = false;
			}
			if (GameCanvas.w > 380)
			{
				this.isSmallGamePad = false;
				this.isMediumGamePad = false;
				this.isLargeGamePad = true;
			}
			if (!this.isLargeGamePad)
			{
				this.xZone = 0;
				this.wZone = GameCanvas.hw;
				this.yZone = GameCanvas.hh >> 1;
				this.hZone = GameCanvas.h - 80;
				return;
			}
			this.xZone = 0;
			this.wZone = GameCanvas.hw / 4 * 3 - 20;
			this.yZone = GameCanvas.hh >> 1;
			this.hZone = GameCanvas.h;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000CA6DC File Offset: 0x000C88DC
		public void update()
		{
			try
			{
				if (GameScr.isAnalog != 0)
				{
					if (GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease)
					{
						this.xTemp = GameCanvas.pxFirst;
						this.yTemp = GameCanvas.pyFirst;
						if (this.xTemp >= this.xZone && this.xTemp <= this.wZone && this.yTemp >= this.yZone && this.yTemp <= this.hZone)
						{
							if (!this.isGamePad)
							{
								this.xC = (this.xM = this.xTemp);
								this.yC = (this.yM = this.yTemp);
							}
							this.isGamePad = true;
							this.deltaX = GameCanvas.px - this.xC;
							this.deltaY = GameCanvas.py - this.yC;
							this.delta = Math.pow(this.deltaX, 2) + Math.pow(this.deltaY, 2);
							this.d = Res.sqrt(this.delta);
							if (Math.abs(this.deltaX) > 4 || Math.abs(this.deltaY) > 4)
							{
								this.angle = Res.angle(this.deltaX, this.deltaY);
								if (!GameCanvas.isPointerHoldIn(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R))
								{
									if (this.d != 0)
									{
										this.yM = this.deltaY * this.R / this.d;
										this.xM = this.deltaX * this.R / this.d;
										this.xM += this.xC;
										this.yM += this.yC;
										if (!Res.inRect(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R, this.xM, this.yM))
										{
											this.xM = this.xMLast;
											this.yM = this.yMLast;
										}
										else
										{
											this.xMLast = this.xM;
											this.yMLast = this.yM;
										}
									}
									else
									{
										this.xM = this.xMLast;
										this.yM = this.yMLast;
									}
								}
								else
								{
									this.xM = GameCanvas.px;
									this.yM = GameCanvas.py;
								}
								this.resetHold();
								if (this.checkPointerMove(2))
								{
									if ((this.angle <= 360 && this.angle >= 340) || (this.angle >= 0 && this.angle <= 20))
									{
										GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
									}
									else if (this.angle > 40 && this.angle < 70)
									{
										GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
									}
									else if (this.angle >= 70 && this.angle <= 110)
									{
										GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
									}
									else if (this.angle > 110 && this.angle < 120)
									{
										GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
									}
									else if (this.angle >= 120 && this.angle <= 200)
									{
										GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
									}
									else if (this.angle > 200 && this.angle < 250)
									{
										GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
										GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
									}
									else if (this.angle >= 250 && this.angle <= 290)
									{
										GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
									}
									else if (this.angle > 290 && this.angle < 340)
									{
										GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
										GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
									}
								}
								else
								{
									this.resetHold();
								}
							}
						}
					}
					else
					{
						this.xM = (this.xC = 45);
						if (!this.isLargeGamePad)
						{
							this.yM = (this.yC = GameCanvas.h - 90);
						}
						else
						{
							this.yM = (this.yC = GameCanvas.h - 45);
						}
						this.isGamePad = false;
						this.resetHold();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000CAC64 File Offset: 0x000C8E64
		private bool checkPointerMove(int distance)
		{
			if (GameScr.isAnalog == 0)
			{
				return false;
			}
			if (Char.myCharz().statusMe == 3)
			{
				return true;
			}
			try
			{
				for (int num = 2; num > 0; num--)
				{
					int i3 = GameCanvas.arrPos[num].x - GameCanvas.arrPos[num - 1].x;
					int i2 = GameCanvas.arrPos[num].y - GameCanvas.arrPos[num - 1].y;
					if (Res.abs(i3) > distance && Res.abs(i2) > distance)
					{
						return false;
					}
				}
			}
			catch (Exception)
			{
			}
			return true;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000CACFC File Offset: 0x000C8EFC
		private void resetHold()
		{
			GameCanvas.clearKeyHold();
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000CAD04 File Offset: 0x000C8F04
		public void paint(mGraphics g)
		{
			g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
			g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000CAD55 File Offset: 0x000C8F55
		public bool disableCheckDrag()
		{
			return GameScr.isAnalog != 0 && this.isGamePad;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000CAD68 File Offset: 0x000C8F68
		public bool disableClickMove()
		{
			bool result;
			try
			{
				if (GameScr.isAnalog == 0)
				{
					result = false;
				}
				else
				{
					result = ((GameCanvas.px >= this.xZone && GameCanvas.px <= this.wZone && GameCanvas.py >= this.yZone && GameCanvas.py <= this.hZone) || GameCanvas.px >= GameCanvas.w - 50);
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040017C3 RID: 6083
		private int xC;

		// Token: 0x040017C4 RID: 6084
		private int yC;

		// Token: 0x040017C5 RID: 6085
		private int xM;

		// Token: 0x040017C6 RID: 6086
		private int yM;

		// Token: 0x040017C7 RID: 6087
		private int xMLast;

		// Token: 0x040017C8 RID: 6088
		private int yMLast;

		// Token: 0x040017C9 RID: 6089
		private int R;

		// Token: 0x040017CA RID: 6090
		private int d;

		// Token: 0x040017CB RID: 6091
		private int xTemp;

		// Token: 0x040017CC RID: 6092
		private int yTemp;

		// Token: 0x040017CD RID: 6093
		private int deltaX;

		// Token: 0x040017CE RID: 6094
		private int deltaY;

		// Token: 0x040017CF RID: 6095
		private int delta;

		// Token: 0x040017D0 RID: 6096
		private int angle;

		// Token: 0x040017D1 RID: 6097
		public int xZone;

		// Token: 0x040017D2 RID: 6098
		public int yZone;

		// Token: 0x040017D3 RID: 6099
		public int wZone;

		// Token: 0x040017D4 RID: 6100
		public int hZone;

		// Token: 0x040017D5 RID: 6101
		private bool isGamePad;

		// Token: 0x040017D6 RID: 6102
		public bool isSmallGamePad;

		// Token: 0x040017D7 RID: 6103
		public bool isMediumGamePad;

		// Token: 0x040017D8 RID: 6104
		public bool isLargeGamePad;
	}
}
