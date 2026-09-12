using System;

namespace Game1
{
	// Token: 0x02000477 RID: 1143
	public class GamePad
	{
		// Token: 0x060032DC RID: 13020 RVA: 0x0031E878 File Offset: 0x0031CA78
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

		// Token: 0x060032DD RID: 13021 RVA: 0x0031E96C File Offset: 0x0031CB6C
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

		// Token: 0x060032DE RID: 13022 RVA: 0x0031EEF4 File Offset: 0x0031D0F4
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

		// Token: 0x060032DF RID: 13023 RVA: 0x0031EF8C File Offset: 0x0031D18C
		private void resetHold()
		{
			GameCanvas.clearKeyHold();
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x0031EF94 File Offset: 0x0031D194
		public void paint(mGraphics g)
		{
			g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
			g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x0031EFE5 File Offset: 0x0031D1E5
		public bool disableCheckDrag()
		{
			return GameScr.isAnalog != 0 && this.isGamePad;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x0031EFF8 File Offset: 0x0031D1F8
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

		// Token: 0x040061BF RID: 25023
		private int xC;

		// Token: 0x040061C0 RID: 25024
		private int yC;

		// Token: 0x040061C1 RID: 25025
		private int xM;

		// Token: 0x040061C2 RID: 25026
		private int yM;

		// Token: 0x040061C3 RID: 25027
		private int xMLast;

		// Token: 0x040061C4 RID: 25028
		private int yMLast;

		// Token: 0x040061C5 RID: 25029
		private int R;

		// Token: 0x040061C6 RID: 25030
		private int d;

		// Token: 0x040061C7 RID: 25031
		private int xTemp;

		// Token: 0x040061C8 RID: 25032
		private int yTemp;

		// Token: 0x040061C9 RID: 25033
		private int deltaX;

		// Token: 0x040061CA RID: 25034
		private int deltaY;

		// Token: 0x040061CB RID: 25035
		private int delta;

		// Token: 0x040061CC RID: 25036
		private int angle;

		// Token: 0x040061CD RID: 25037
		public int xZone;

		// Token: 0x040061CE RID: 25038
		public int yZone;

		// Token: 0x040061CF RID: 25039
		public int wZone;

		// Token: 0x040061D0 RID: 25040
		public int hZone;

		// Token: 0x040061D1 RID: 25041
		private bool isGamePad;

		// Token: 0x040061D2 RID: 25042
		public bool isSmallGamePad;

		// Token: 0x040061D3 RID: 25043
		public bool isMediumGamePad;

		// Token: 0x040061D4 RID: 25044
		public bool isLargeGamePad;
	}
}
