using System;

namespace Game3
{
	// Token: 0x020002C7 RID: 711
	public class GamePad
	{
		// Token: 0x06001F94 RID: 8084 RVA: 0x001F4730 File Offset: 0x001F2930
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

		// Token: 0x06001F95 RID: 8085 RVA: 0x001F4824 File Offset: 0x001F2A24
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

		// Token: 0x06001F96 RID: 8086 RVA: 0x001F4DAC File Offset: 0x001F2FAC
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

		// Token: 0x06001F97 RID: 8087 RVA: 0x001F4E44 File Offset: 0x001F3044
		private void resetHold()
		{
			GameCanvas.clearKeyHold();
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x001F4E4C File Offset: 0x001F304C
		public void paint(mGraphics g)
		{
			g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
			g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x001F4E9D File Offset: 0x001F309D
		public bool disableCheckDrag()
		{
			return GameScr.isAnalog != 0 && this.isGamePad;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x001F4EB0 File Offset: 0x001F30B0
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

		// Token: 0x04003CC1 RID: 15553
		private int xC;

		// Token: 0x04003CC2 RID: 15554
		private int yC;

		// Token: 0x04003CC3 RID: 15555
		private int xM;

		// Token: 0x04003CC4 RID: 15556
		private int yM;

		// Token: 0x04003CC5 RID: 15557
		private int xMLast;

		// Token: 0x04003CC6 RID: 15558
		private int yMLast;

		// Token: 0x04003CC7 RID: 15559
		private int R;

		// Token: 0x04003CC8 RID: 15560
		private int d;

		// Token: 0x04003CC9 RID: 15561
		private int xTemp;

		// Token: 0x04003CCA RID: 15562
		private int yTemp;

		// Token: 0x04003CCB RID: 15563
		private int deltaX;

		// Token: 0x04003CCC RID: 15564
		private int deltaY;

		// Token: 0x04003CCD RID: 15565
		private int delta;

		// Token: 0x04003CCE RID: 15566
		private int angle;

		// Token: 0x04003CCF RID: 15567
		public int xZone;

		// Token: 0x04003CD0 RID: 15568
		public int yZone;

		// Token: 0x04003CD1 RID: 15569
		public int wZone;

		// Token: 0x04003CD2 RID: 15570
		public int hZone;

		// Token: 0x04003CD3 RID: 15571
		private bool isGamePad;

		// Token: 0x04003CD4 RID: 15572
		public bool isSmallGamePad;

		// Token: 0x04003CD5 RID: 15573
		public bool isMediumGamePad;

		// Token: 0x04003CD6 RID: 15574
		public bool isLargeGamePad;
	}
}
