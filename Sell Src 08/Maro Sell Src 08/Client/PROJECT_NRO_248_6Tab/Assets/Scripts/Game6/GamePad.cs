using System;

namespace Game6
{
	// Token: 0x0200003F RID: 63
	public class GamePad
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000353EC File Offset: 0x000335EC
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

		// Token: 0x060002A9 RID: 681 RVA: 0x000354E0 File Offset: 0x000336E0
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

		// Token: 0x060002AA RID: 682 RVA: 0x00035A68 File Offset: 0x00033C68
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

		// Token: 0x060002AB RID: 683 RVA: 0x00035B00 File Offset: 0x00033D00
		private void resetHold()
		{
			GameCanvas.clearKeyHold();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00035B08 File Offset: 0x00033D08
		public void paint(mGraphics g)
		{
			g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
			g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00035B59 File Offset: 0x00033D59
		public bool disableCheckDrag()
		{
			return GameScr.isAnalog != 0 && this.isGamePad;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00035B6C File Offset: 0x00033D6C
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

		// Token: 0x04000544 RID: 1348
		private int xC;

		// Token: 0x04000545 RID: 1349
		private int yC;

		// Token: 0x04000546 RID: 1350
		private int xM;

		// Token: 0x04000547 RID: 1351
		private int yM;

		// Token: 0x04000548 RID: 1352
		private int xMLast;

		// Token: 0x04000549 RID: 1353
		private int yMLast;

		// Token: 0x0400054A RID: 1354
		private int R;

		// Token: 0x0400054B RID: 1355
		private int d;

		// Token: 0x0400054C RID: 1356
		private int xTemp;

		// Token: 0x0400054D RID: 1357
		private int yTemp;

		// Token: 0x0400054E RID: 1358
		private int deltaX;

		// Token: 0x0400054F RID: 1359
		private int deltaY;

		// Token: 0x04000550 RID: 1360
		private int delta;

		// Token: 0x04000551 RID: 1361
		private int angle;

		// Token: 0x04000552 RID: 1362
		public int xZone;

		// Token: 0x04000553 RID: 1363
		public int yZone;

		// Token: 0x04000554 RID: 1364
		public int wZone;

		// Token: 0x04000555 RID: 1365
		public int hZone;

		// Token: 0x04000556 RID: 1366
		private bool isGamePad;

		// Token: 0x04000557 RID: 1367
		public bool isSmallGamePad;

		// Token: 0x04000558 RID: 1368
		public bool isMediumGamePad;

		// Token: 0x04000559 RID: 1369
		public bool isLargeGamePad;
	}
}
