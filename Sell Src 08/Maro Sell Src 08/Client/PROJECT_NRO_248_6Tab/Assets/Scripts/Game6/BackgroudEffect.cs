using System;

namespace Game6
{
	// Token: 0x02000014 RID: 20
	public class BackgroudEffect
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public BackgroudEffect(int typeS)
		{
			BackgroudEffect.isFog = true;
			BackgroudEffect.initCloud();
			this.typeEff = typeS;
			switch (this.typeEff)
			{
			case 0:
			case 12:
				if (BackgroudEffect.imgHatMua == null)
				{
					BackgroudEffect.imgHatMua = GameCanvas.loadImageRMS("/bg/mua.png");
				}
				if (BackgroudEffect.imgMua1 == null)
				{
					BackgroudEffect.imgMua1 = GameCanvas.loadImageRMS("/bg/mua1.png");
				}
				if (BackgroudEffect.imgMua2 == null)
				{
					BackgroudEffect.imgMua2 = GameCanvas.loadImageRMS("/bg/mua2.png");
				}
				this.sum = Res.random(GameCanvas.w / 3, GameCanvas.w / 2);
				this.x = new int[this.sum];
				this.y = new int[this.sum];
				this.vx = new int[this.sum];
				this.vy = new int[this.sum];
				this.type = new int[this.sum];
				this.t = new int[this.sum];
				this.frame = new int[this.sum];
				this.isRainEffect = new bool[this.sum];
				this.activeEff = new bool[this.sum];
				for (int i = 0; i < this.sum; i++)
				{
					this.y[i] = Res.random(-10, GameCanvas.h + 100) + GameScr.cmy;
					this.x[i] = Res.random(-10, GameCanvas.w + 300) + GameScr.cmx;
					this.t[i] = Res.random(0, 1);
					this.vx[i] = -12;
					this.vy[i] = 12;
					this.type[i] = Res.random(1, 3);
					this.isRainEffect[i] = false;
					if (this.type[i] == 2 && i % 2 == 0)
					{
						this.isRainEffect[i] = true;
					}
					this.activeEff[i] = false;
					this.frame[i] = Res.random(1, 2);
				}
				return;
			case 1:
			case 2:
			case 5:
			case 6:
			case 7:
			case 11:
			case 15:
				if (this.typeEff == 1)
				{
					BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay.png");
					BackgroudEffect.PIXEL = 10;
				}
				if (this.typeEff == 2)
				{
					BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay2.png");
					BackgroudEffect.PIXEL = 18;
				}
				if (this.typeEff == 5)
				{
					BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay3.png");
					BackgroudEffect.PIXEL = 14;
				}
				if (this.typeEff == 6)
				{
					BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay4.png");
					BackgroudEffect.PIXEL = 14;
				}
				if (this.typeEff == 7)
				{
					BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/lacay5.png");
					BackgroudEffect.PIXEL = 12;
				}
				if (this.typeEff == 11)
				{
					BackgroudEffect.imgLacay = GameCanvas.loadImageRMS("/bg/tuyet.png");
				}
				if (this.typeEff == 15)
				{
					if (SmallImage.imgNew[11120] == null)
					{
						SmallImage.createImage(11120);
					}
					BackgroudEffect.PIXEL = 16;
				}
				this.sum = Res.random(15, 25);
				if (this.typeEff == 11)
				{
					this.sum = 100;
				}
				this.x = new int[this.sum];
				this.y = new int[this.sum];
				this.vx = new int[this.sum];
				this.vy = new int[this.sum];
				this.t = new int[this.sum];
				this.frame = new int[this.sum];
				this.activeEff = new bool[this.sum];
				for (int j = 0; j < this.sum; j++)
				{
					this.x[j] = Res.random(-10, TileMap.pxw + 10);
					this.y[j] = Res.random(0, TileMap.pxh);
					this.frame[j] = Res.random(0, 1);
					this.t[j] = Res.random(0, 1);
					this.vx[j] = Res.random(-3, 3);
					this.vy[j] = Res.random(1, 4);
					if (this.typeEff == 11)
					{
						this.frame[j] = Res.random(0, 2);
						this.vx[j] = Res.abs(Res.random(1, 3));
						this.vy[j] = Res.abs(Res.random(1, 3));
					}
					if (this.typeEff == 15)
					{
						this.frame[j] = Res.random(0, 2);
						this.vx[j] = Res.abs(Res.random(1, 3));
						this.vy[j] = Res.abs(Res.random(1, 3));
					}
				}
				return;
			case 3:
				GameCanvas.isBoltEff = true;
				return;
			case 4:
				this.sum = Res.random(5, 10);
				if (BackgroudEffect.imgSao == null)
				{
					BackgroudEffect.imgSao = GameCanvas.loadImageRMS("/bg/sao.png");
				}
				this.x = new int[this.sum];
				this.y = new int[this.sum];
				this.frame = new int[this.sum];
				this.t = new int[this.sum];
				this.tick = new int[this.sum];
				for (int k = 0; k < this.sum; k++)
				{
					this.x[k] = Res.random(0, GameCanvas.w);
					this.y[k] = Res.random(0, 50);
					if (k % 2 == 0)
					{
						this.tick[k] = 0;
					}
					else if (k % 3 == 0)
					{
						this.tick[k] = 1;
					}
					else if (k % 4 == 0)
					{
						this.tick[k] = 2;
					}
					else
					{
						this.tick[k] = 3;
					}
					this.t[k] = Res.random(0, 10);
				}
				return;
			case 8:
				this.tStart = Res.random(100, 300);
				if (BackgroudEffect.imgShip == null)
				{
					BackgroudEffect.imgShip = GameCanvas.loadImageRMS("/bg/ship.png");
				}
				if (BackgroudEffect.imgFire1 == null)
				{
					BackgroudEffect.imgFire1 = GameCanvas.loadImageRMS("/bg/fire1.png");
				}
				if (BackgroudEffect.imgFire2 == null)
				{
					BackgroudEffect.imgFire2 = GameCanvas.loadImageRMS("/bg/fire2.png");
				}
				this.isFly = false;
				this.reloadShip();
				return;
			case 9:
				if (BackgroudEffect.imgChamTron1 == null)
				{
					BackgroudEffect.imgChamTron1 = GameCanvas.loadImageRMS("/bg/cham-tron1.png");
				}
				if (BackgroudEffect.imgChamTron2 == null)
				{
					BackgroudEffect.imgChamTron2 = GameCanvas.loadImageRMS("/bg/cham-tron2.png");
				}
				this.num = 20;
				this.x = new int[this.num];
				this.y = new int[this.num];
				BackgroudEffect.wP = new int[this.num];
				this.vx = new int[this.num];
				for (int l = 0; l < this.num; l++)
				{
					this.x[l] = Res.abs(Res.random(0, GameCanvas.w));
					this.y[l] = Res.abs(Res.random(10, 80));
					BackgroudEffect.wP[l] = Res.abs(Res.random(1, 3));
					this.vx[l] = BackgroudEffect.wP[l];
				}
				return;
			case 10:
			{
				this.num = 30;
				this.x = new int[this.num];
				this.y = new int[this.num];
				BackgroudEffect.wP = new int[this.num];
				this.vx = new int[this.num];
				int num = 0;
				for (int m = 0; m < this.num; m++)
				{
					this.x[m] = Res.abs(Res.random(0, GameCanvas.w)) + GameScr.cmx;
					num++;
					if (num > this.num / 2)
					{
						this.y[m] = Res.abs(Res.random(20, 60));
						BackgroudEffect.wP[m] = 10;
					}
					else
					{
						this.y[m] = Res.abs(Res.random(0, 20));
						BackgroudEffect.wP[m] = 7;
					}
					this.vx[m] = BackgroudEffect.wP[m] / 2 - 2;
				}
				return;
			}
			case 13:
				if (Res.abs(Res.random(0, 2)) == 0)
				{
					if (Res.abs(Res.random(0, 2)) == 0)
					{
						BackgroudEffect.isPaintFar = true;
					}
					else
					{
						BackgroudEffect.isPaintFar = false;
					}
					BackgroudEffect.nCloud = Res.abs(Res.random(2, 5));
					BackgroudEffect.initCloud();
					return;
				}
				break;
			case 14:
				if (Res.abs(Res.random(0, 2)) == 0)
				{
					BackgroudEffect.isFog = true;
					BackgroudEffect.initCloud();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004323 File Offset: 0x00002523
		public static void clearImage()
		{
			TileMap.yWater = 0;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000432C File Offset: 0x0000252C
		public static bool isHaveRain()
		{
			for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
			{
				BackgroudEffect backgroudEffect = (BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i);
				if (backgroudEffect.typeEff == 0 || backgroudEffect.typeEff == 12)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004374 File Offset: 0x00002574
		public static void initCloud()
		{
			if (mSystem.clientType == 1)
			{
				BackgroudEffect.imgFog = null;
				return;
			}
			if (GameCanvas.lowGraphic)
			{
				BackgroudEffect.imgFog = null;
				return;
			}
			if (!BackgroudEffect.isFog)
			{
				BackgroudEffect.imgFog = null;
				return;
			}
			if (BackgroudEffect.imgFog == null)
			{
				BackgroudEffect.imgFog = GameCanvas.loadImage("/bg/fog0.png");
			}
			BackgroudEffect.fogw = 287;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000043CC File Offset: 0x000025CC
		public static void updateFog()
		{
			if (mSystem.clientType != 1 && !GameCanvas.lowGraphic && BackgroudEffect.isFog)
			{
				BackgroudEffect.xfog--;
				if (BackgroudEffect.xfog < -BackgroudEffect.fogw)
				{
					BackgroudEffect.xfog = 0;
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004404 File Offset: 0x00002604
		public static void paintFog(mGraphics g)
		{
			if (mSystem.clientType == 1 || GameCanvas.lowGraphic || !BackgroudEffect.isFog || BackgroudEffect.imgFog == null)
			{
				return;
			}
			for (int i = BackgroudEffect.xfog; i < TileMap.pxw; i += BackgroudEffect.fogw)
			{
				if (i >= GameScr.cmx - BackgroudEffect.fogw)
				{
					g.drawImageFog(BackgroudEffect.imgFog, i, BackgroudEffect.yfog, 0);
				}
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004468 File Offset: 0x00002668
		private void reloadShip()
		{
			int cmx = GameScr.cmx;
			int cmy = GameScr.cmy;
			this.way = Res.random(1, 3);
			this.isFly = false;
			this.speed = Res.random(3, 5);
			if (this.way == 1)
			{
				this.xShip = -50;
				this.yShip = Res.random(cmy, GameCanvas.h - 100 + cmy);
				this.trans = 0;
				return;
			}
			if (this.way == 2)
			{
				this.xShip = TileMap.pxw + 50;
				this.yShip = Res.random(cmy, GameCanvas.h - 100 + cmy);
				this.trans = 2;
				return;
			}
			if (this.way == 3)
			{
				this.xShip = Res.random(50 + cmx, GameCanvas.w - 50 + cmx);
				this.yShip = -50;
				this.trans = ((Res.random(0, 2) != 0) ? 2 : 0);
				return;
			}
			if (this.way == 4)
			{
				this.xShip = Res.random(50 + cmx, GameCanvas.w - 50 + cmx);
				this.yShip = TileMap.pxh + 50;
				this.trans = ((Res.random(0, 2) != 0) ? 2 : 0);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000458C File Offset: 0x0000278C
		public void paintWater(mGraphics g)
		{
			if (this.typeEff != 10)
			{
				return;
			}
			g.setColor(this.colorWater);
			for (int i = 0; i < this.num; i++)
			{
				g.drawImage((i >= this.num / 2) ? BackgroudEffect.water1 : BackgroudEffect.water2, this.x[i], this.y[i] + this.yWater, 0);
			}
			if (BackgroudEffect.id_water1 != 0 && BackgroudEffect.water3 == null)
			{
				BackgroudEffect.water3 = SmallImage.imgNew[(int)BackgroudEffect.id_water1].img;
			}
			if (BackgroudEffect.water3 != null)
			{
				for (int j = 0; j < this.num / 2; j++)
				{
					g.drawImage(BackgroudEffect.water3, this.x[j], this.y[j] + this.yWater, 0);
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004658 File Offset: 0x00002858
		public void paintFar(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			if (this.typeEff == 4)
			{
				for (int i = 0; i < this.sum; i++)
				{
					g.drawRegion(BackgroudEffect.imgSao, 0, 16 * this.frame[i], 16, 16, 0, this.x[i], this.y[i], 0);
				}
			}
			if (this.typeEff == 9)
			{
				g.setColor(16777215);
				for (int j = 0; j < this.num; j++)
				{
					g.drawImage((BackgroudEffect.wP[j] != 1) ? BackgroudEffect.imgChamTron2 : BackgroudEffect.imgChamTron1, this.x[j], this.y[j], 3);
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004714 File Offset: 0x00002914
		public void update()
		{
			try
			{
				switch (this.typeEff)
				{
				case 0:
				case 12:
					for (int i = 0; i < this.sum; i++)
					{
						if (i % 3 != 0 && this.typeEff != 12 && TileMap.tileTypeAt(this.x[i], this.y[i] - GameCanvas.transY, 2))
						{
							this.activeEff[i] = true;
						}
						if (i % 3 == 0 && this.y[i] > GameCanvas.h + GameScr.cmy)
						{
							this.x[i] = Res.random(-10, GameCanvas.w + 300) + GameScr.cmx;
							this.y[i] = Res.random(-100, 0) + GameScr.cmy;
						}
						if (!this.activeEff[i])
						{
							this.y[i] += this.vy[i];
							this.x[i] += this.vx[i];
						}
						if (this.activeEff[i])
						{
							this.t[i]++;
							if (this.t[i] > 2)
							{
								this.frame[i]++;
								this.t[i] = 0;
								if (this.frame[i] > 1)
								{
									this.frame[i] = 0;
									this.activeEff[i] = false;
									this.x[i] = Res.random(-10, GameCanvas.w + 300) + GameScr.cmx;
									this.y[i] = Res.random(-100, 0) + GameScr.cmy;
								}
							}
						}
					}
					break;
				case 1:
				case 2:
				case 5:
				case 6:
				case 7:
				case 11:
				case 15:
					for (int j = 0; j < this.sum; j++)
					{
						if (j % 3 != 0 && TileMap.tileTypeAt(this.x[j], this.y[j] + ((TileMap.tileID == 15) ? 10 : 0), 2))
						{
							this.activeEff[j] = true;
						}
						if (j % 3 == 0 && this.y[j] > TileMap.pxh)
						{
							this.x[j] = Res.random(-10, TileMap.pxw + 50);
							this.y[j] = Res.random(-50, 0);
						}
						if (!this.activeEff[j])
						{
							for (int k = 0; k < Teleport.vTeleport.size(); k++)
							{
								Teleport teleport = (Teleport)Teleport.vTeleport.elementAt(k);
								if (teleport != null && teleport.paintFire && this.x[j] < teleport.x + 80 && this.x[j] > teleport.x - 80 && this.y[j] < teleport.y + 80 && this.y[j] > teleport.y - 80)
								{
									this.x[j] += ((this.x[j] >= teleport.x) ? 10 : -10);
								}
							}
							this.y[j] += this.vy[j];
							this.x[j] += this.vx[j];
							this.t[j]++;
							int num2 = (this.typeEff != 11) ? 4 : 3;
							int num = (this.typeEff != 15) ? 4 : 4;
							if (this.t[j] > ((this.typeEff == 2) ? 4 : 2))
							{
								if (this.typeEff != 11 && this.typeEff != 15)
								{
									this.frame[j]++;
								}
								this.t[j] = 0;
								if (this.frame[j] > num - 1)
								{
									this.frame[j] = 0;
								}
							}
						}
						else
						{
							this.t[j]++;
							if (this.t[j] == 100)
							{
								this.t[j] = 0;
								this.x[j] = Res.random(-10, TileMap.pxw + 50);
								this.y[j] = Res.random(-50, 0);
								this.activeEff[j] = false;
							}
						}
					}
					break;
				case 4:
					for (int l = 0; l < this.sum; l++)
					{
						this.t[l]++;
						if (this.t[l] > 10)
						{
							this.tick[l]++;
							this.t[l] = 0;
							if (this.tick[l] > 5)
							{
								this.tick[l] = 0;
							}
							this.frame[l] = this.dem[this.tick[l]];
						}
					}
					break;
				case 8:
					this.tFire++;
					if (this.tFire == 3)
					{
						this.tFire = 0;
						this.frameFire++;
						if (this.frameFire > 1)
						{
							this.frameFire = 0;
						}
					}
					if (GameCanvas.gameTick % this.tStart == 0)
					{
						this.isFly = true;
					}
					if (this.isFly)
					{
						if (this.way == 1)
						{
							this.xShip += this.speed;
							if (this.xShip > TileMap.pxw + 50)
							{
								this.reloadShip();
							}
						}
						else if (this.way == 2)
						{
							this.xShip -= this.speed;
							if (this.xShip < -50)
							{
								this.reloadShip();
							}
						}
						else if (this.way == 3)
						{
							this.yShip += this.speed;
							if (this.yShip > TileMap.pxh + 50)
							{
								this.reloadShip();
							}
						}
						else if (this.way == 4)
						{
							this.yShip -= this.speed;
							if (this.yShip < -50)
							{
								this.reloadShip();
							}
						}
					}
					break;
				case 9:
					for (int m = 0; m < this.num; m++)
					{
						this.x[m] -= this.vx[m];
						if (this.x[m] < -this.vx[m])
						{
							BackgroudEffect.wP[m] = Res.abs(Res.random(1, 3));
							this.vx[m] = BackgroudEffect.wP[m];
							this.x[m] = GameCanvas.w + this.vx[m];
						}
					}
					break;
				case 10:
					for (int n = 0; n < this.num; n++)
					{
						this.x[n] -= this.vx[n];
						if (this.x[n] < -this.vx[n] + GameScr.cmx)
						{
							this.x[n] = GameCanvas.w + this.vx[n] + GameScr.cmx;
						}
					}
					break;
				case 14:
					BackgroudEffect.updateFog();
					break;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004E20 File Offset: 0x00003020
		public void paintFront(mGraphics g)
		{
			try
			{
				switch (this.typeEff)
				{
				case 0:
				case 12:
				{
					int cmx = GameScr.cmx;
					int cmy = GameScr.cmy;
					for (int i = 0; i < this.sum; i++)
					{
						if (this.type[i] == 2 && this.x[i] >= GameScr.cmx && this.x[i] <= GameCanvas.w + GameScr.cmx && this.y[i] >= GameScr.cmy && this.y[i] <= GameCanvas.h + GameScr.cmy)
						{
							if (this.activeEff[i])
							{
								g.drawRegion(BackgroudEffect.imgHatMua, 0, 10 * this.frame[i], 13, 10, 0, this.x[i], this.y[i] - 10, 0);
							}
							else
							{
								g.drawImage(BackgroudEffect.imgMua1, this.x[i], this.y[i], 0);
							}
						}
					}
					break;
				}
				case 1:
				case 2:
				case 5:
				case 6:
				case 7:
				case 11:
				case 15:
					if (this.typeEff == 15)
					{
						if (SmallImage.imgNew[11120] != null && SmallImage.imgNew[11120].img != null)
						{
							BackgroudEffect.imgLacay = SmallImage.imgNew[11120].img;
						}
						if (BackgroudEffect.imgLacay == null)
						{
							break;
						}
					}
					this.paintLacay1(g, BackgroudEffect.imgLacay);
					break;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004FC8 File Offset: 0x000031C8
		public void paintLacay1(mGraphics g, Image img)
		{
			int num = this.typeEff;
			int num2 = this.typeEff;
			for (int i = 0; i < this.sum; i++)
			{
				if (i % 3 == 0 && this.x[i] >= GameScr.cmx && this.x[i] <= GameCanvas.w + GameScr.cmx && this.y[i] >= GameScr.cmy && this.y[i] <= GameCanvas.h + GameScr.cmy && img != null)
				{
					g.drawRegion(img, 0, BackgroudEffect.PIXEL * this.frame[i], img.getWidth(), BackgroudEffect.PIXEL, 0, this.x[i], this.y[i], 0);
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005080 File Offset: 0x00003280
		public void paintLacay2(mGraphics g, Image img)
		{
			int num = this.typeEff;
			int num2 = this.typeEff;
			for (int i = 0; i < this.sum; i++)
			{
				if (i % 3 != 0 && this.x[i] >= GameScr.cmx && this.x[i] <= GameCanvas.w + GameScr.cmx && this.y[i] >= GameScr.cmy && this.y[i] <= GameCanvas.h + GameScr.cmy && img != null)
				{
					g.drawRegion(img, 0, BackgroudEffect.PIXEL * this.frame[i], img.getWidth(), BackgroudEffect.PIXEL, 0, this.x[i], this.y[i], 0);
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005138 File Offset: 0x00003338
		public void paintBehindTile(mGraphics g)
		{
			if (this.typeEff == 8)
			{
				g.drawRegion(BackgroudEffect.imgShip, 0, 0, BackgroudEffect.imgShip.getWidth(), BackgroudEffect.imgShip.getHeight(), this.trans, this.xShip, this.yShip, 3);
				if (this.way == 1 || this.way == 2)
				{
					int num = (this.trans != 0) ? 25 : -25;
					g.drawRegion(BackgroudEffect.imgFire1, 0, this.frameFire * 8, 20, 8, this.trans, this.xShip + num, this.yShip + 5, 3);
					return;
				}
				int num2 = (this.trans != 0) ? -11 : 11;
				g.drawRegion(BackgroudEffect.imgFire2, 0, this.frameFire * 18, 8, 18, this.trans, this.xShip + num2, this.yShip + 22, 3);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005214 File Offset: 0x00003414
		public void paintBack(mGraphics g)
		{
			switch (this.typeEff)
			{
			case 0:
			{
				int cmx = GameScr.cmx;
				int cmy = GameScr.cmy;
				g.setColor(10742731);
				for (int i = 0; i < this.sum; i++)
				{
					if (this.type[i] != 2 && this.x[i] >= GameScr.cmx && this.x[i] <= GameCanvas.w + GameScr.cmx && this.y[i] >= GameScr.cmy && this.y[i] <= GameCanvas.h + GameScr.cmy)
					{
						g.drawImage(BackgroudEffect.imgMua2, this.x[i], this.y[i], 0);
					}
				}
				return;
			}
			case 1:
			case 2:
			case 5:
			case 6:
			case 7:
			case 11:
			case 15:
				if (this.typeEff == 15)
				{
					if (SmallImage.imgNew[11120] != null && SmallImage.imgNew[11120].img != null)
					{
						BackgroudEffect.imgLacay = SmallImage.imgNew[11120].img;
					}
					if (BackgroudEffect.imgLacay == null)
					{
						break;
					}
				}
				this.paintLacay2(g, BackgroudEffect.imgLacay);
				break;
			case 3:
			case 4:
			case 8:
			case 9:
			case 10:
			case 12:
			case 13:
			case 14:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005358 File Offset: 0x00003558
		public static void addEffect(int id)
		{
			if (!GameCanvas.lowGraphic)
			{
				BackgroudEffect o = new BackgroudEffect(id);
				BackgroudEffect.vBgEffect.addElement(o);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005380 File Offset: 0x00003580
		public static void addWater(int color, int yWater)
		{
			BackgroudEffect backgroudEffect = new BackgroudEffect(10);
			backgroudEffect.colorWater = color;
			backgroudEffect.yWater = yWater;
			BackgroudEffect.vBgEffect.addElement(backgroudEffect);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000053B0 File Offset: 0x000035B0
		public static void paintWaterAll(mGraphics g)
		{
			for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
			{
				((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintWater(g);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000053E8 File Offset: 0x000035E8
		public static void paintBehindTileAll(mGraphics g)
		{
			for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
			{
				((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintBehindTile(g);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005420 File Offset: 0x00003620
		public static void paintFrontAll(mGraphics g)
		{
			for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
			{
				((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintFront(g);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005458 File Offset: 0x00003658
		public static void paintFarAll(mGraphics g)
		{
			for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
			{
				((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintFar(g);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005490 File Offset: 0x00003690
		public static void paintBackAll(mGraphics g)
		{
			for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
			{
				((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).paintBack(g);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000054C8 File Offset: 0x000036C8
		public static void updateEff()
		{
			for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
			{
				((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i)).update();
			}
		}

		// Token: 0x04000077 RID: 119
		public static MyVector vBgEffect = new MyVector();

		// Token: 0x04000078 RID: 120
		private int[] x;

		// Token: 0x04000079 RID: 121
		private int[] y;

		// Token: 0x0400007A RID: 122
		private int[] vx;

		// Token: 0x0400007B RID: 123
		private int[] vy;

		// Token: 0x0400007C RID: 124
		public static int[] wP;

		// Token: 0x0400007D RID: 125
		private int num;

		// Token: 0x0400007E RID: 126
		private int xShip;

		// Token: 0x0400007F RID: 127
		private int yShip;

		// Token: 0x04000080 RID: 128
		private int way;

		// Token: 0x04000081 RID: 129
		private int trans;

		// Token: 0x04000082 RID: 130
		private int frameFire;

		// Token: 0x04000083 RID: 131
		private int tFire;

		// Token: 0x04000084 RID: 132
		private int tStart;

		// Token: 0x04000085 RID: 133
		private int speed;

		// Token: 0x04000086 RID: 134
		private bool isFly;

		// Token: 0x04000087 RID: 135
		public static Image imgHatMua;

		// Token: 0x04000088 RID: 136
		public static Image imgMua1;

		// Token: 0x04000089 RID: 137
		public static Image imgMua2;

		// Token: 0x0400008A RID: 138
		public static Image imgSao;

		// Token: 0x0400008B RID: 139
		private static Image imgLacay;

		// Token: 0x0400008C RID: 140
		private static Image imgShip;

		// Token: 0x0400008D RID: 141
		private static Image imgFire1;

		// Token: 0x0400008E RID: 142
		private static Image imgFire2;

		// Token: 0x0400008F RID: 143
		private int[] type;

		// Token: 0x04000090 RID: 144
		private int sum;

		// Token: 0x04000091 RID: 145
		public int typeEff;

		// Token: 0x04000092 RID: 146
		private bool[] isRainEffect;

		// Token: 0x04000093 RID: 147
		private int[] frame;

		// Token: 0x04000094 RID: 148
		private int[] t;

		// Token: 0x04000095 RID: 149
		private bool[] activeEff;

		// Token: 0x04000096 RID: 150
		private int yWater;

		// Token: 0x04000097 RID: 151
		private int colorWater;

		// Token: 0x04000098 RID: 152
		public static int PIXEL = 16;

		// Token: 0x04000099 RID: 153
		public static Image water1 = GameCanvas.loadImage("/mainImage/myTexture2dwater1.png");

		// Token: 0x0400009A RID: 154
		public static Image water2 = GameCanvas.loadImage("/mainImage/myTexture2dwater2.png");

		// Token: 0x0400009B RID: 155
		public static Image imgChamTron1;

		// Token: 0x0400009C RID: 156
		public static Image imgChamTron2;

		// Token: 0x0400009D RID: 157
		public static short id_water1;

		// Token: 0x0400009E RID: 158
		public static Image water3 = null;

		// Token: 0x0400009F RID: 159
		public static bool isFog;

		// Token: 0x040000A0 RID: 160
		public static bool isPaintFar;

		// Token: 0x040000A1 RID: 161
		public static int nCloud;

		// Token: 0x040000A2 RID: 162
		public static Image imgFog;

		// Token: 0x040000A3 RID: 163
		public static int xfog;

		// Token: 0x040000A4 RID: 164
		public static int yfog;

		// Token: 0x040000A5 RID: 165
		public static int fogw;

		// Token: 0x040000A6 RID: 166
		private int[] dem = new int[]
		{
			0,
			1,
			2,
			1,
			0,
			0
		};

		// Token: 0x040000A7 RID: 167
		private int[] tick;
	}
}
