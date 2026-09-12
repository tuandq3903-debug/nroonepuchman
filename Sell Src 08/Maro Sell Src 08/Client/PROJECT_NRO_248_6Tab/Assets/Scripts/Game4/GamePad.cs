using System;

namespace Game4
{
	// Token: 0x020001EF RID: 495
	public class GamePad
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x0015F68C File Offset: 0x0015D88C
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

		// Token: 0x060015F1 RID: 5617 RVA: 0x0015F780 File Offset: 0x0015D980
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

		// Token: 0x060015F2 RID: 5618 RVA: 0x0015FD08 File Offset: 0x0015DF08
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

		// Token: 0x060015F3 RID: 5619 RVA: 0x0015FDA0 File Offset: 0x0015DFA0
		private void resetHold()
		{
			GameCanvas.clearKeyHold();
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0015FDA8 File Offset: 0x0015DFA8
		public void paint(mGraphics g)
		{
			g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
			g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0015FDF9 File Offset: 0x0015DFF9
		public bool disableCheckDrag()
		{
			return GameScr.isAnalog != 0 && this.isGamePad;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0015FE0C File Offset: 0x0015E00C
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

		// Token: 0x04002A42 RID: 10818
		private int xC;

		// Token: 0x04002A43 RID: 10819
		private int yC;

		// Token: 0x04002A44 RID: 10820
		private int xM;

		// Token: 0x04002A45 RID: 10821
		private int yM;

		// Token: 0x04002A46 RID: 10822
		private int xMLast;

		// Token: 0x04002A47 RID: 10823
		private int yMLast;

		// Token: 0x04002A48 RID: 10824
		private int R;

		// Token: 0x04002A49 RID: 10825
		private int d;

		// Token: 0x04002A4A RID: 10826
		private int xTemp;

		// Token: 0x04002A4B RID: 10827
		private int yTemp;

		// Token: 0x04002A4C RID: 10828
		private int deltaX;

		// Token: 0x04002A4D RID: 10829
		private int deltaY;

		// Token: 0x04002A4E RID: 10830
		private int delta;

		// Token: 0x04002A4F RID: 10831
		private int angle;

		// Token: 0x04002A50 RID: 10832
		public int xZone;

		// Token: 0x04002A51 RID: 10833
		public int yZone;

		// Token: 0x04002A52 RID: 10834
		public int wZone;

		// Token: 0x04002A53 RID: 10835
		public int hZone;

		// Token: 0x04002A54 RID: 10836
		private bool isGamePad;

		// Token: 0x04002A55 RID: 10837
		public bool isSmallGamePad;

		// Token: 0x04002A56 RID: 10838
		public bool isMediumGamePad;

		// Token: 0x04002A57 RID: 10839
		public bool isLargeGamePad;
	}
}
