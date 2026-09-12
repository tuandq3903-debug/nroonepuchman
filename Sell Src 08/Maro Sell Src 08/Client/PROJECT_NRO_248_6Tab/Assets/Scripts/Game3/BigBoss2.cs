using System;

namespace Game3
{
	// Token: 0x020002A0 RID: 672
	public class BigBoss2 : Mob, IMapObject
	{
		// Token: 0x06001D70 RID: 7536 RVA: 0x001C53FC File Offset: 0x001C35FC
		public BigBoss2(int id, short px, short py, int templateID, long hp, long maxHp, int s)
		{
			if (BigBoss2.shadowBig == null)
			{
				BigBoss2.shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");
			}
			this.mobId = id;
			this.xTo = (this.x = (int)(px + 20));
			this.y = (int)py;
			this.yTo = (int)py;
			this.yFirst = (int)py;
			this.hp = hp;
			this.maxHp = maxHp;
			this.templateId = templateID;
			this.w_hp_bar = 100;
			this.h_hp_bar = 6;
			this.len = this.w_hp_bar;
			base.updateHp_bar();
			this.getDataB();
			this.status = 2;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x001C5580 File Offset: 0x001C3780
		public void getDataB()
		{
			BigBoss2.data = null;
			BigBoss2.data = new EffectData();
			string patch = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				109.ToString(),
				"/data"
			});
			try
			{
				BigBoss2.data.readData2(patch);
				BigBoss2.data.img = GameCanvas.loadImage("/effectdata/" + 109.ToString() + "/img.png");
			}
			catch (Exception)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.w = BigBoss2.data.width;
			this.h = BigBoss2.data.height;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x001C24AC File Offset: 0x001C06AC
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x001C24BC File Offset: 0x001C06BC
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x001C5654 File Offset: 0x001C3854
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x001C5688 File Offset: 0x001C3888
		private void updateShadown()
		{
			int num = (int)TileMap.size;
			this.xSd = this.x;
			this.wCount = 0;
			if (this.ySd <= 0 || TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				return;
			}
			if (TileMap.tileTypeAt(this.xSd / num, this.ySd / num) == 0)
			{
				this.isOutMap = true;
			}
			else if (TileMap.tileTypeAt(this.xSd / num, this.ySd / num) != 0 && !TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				this.xSd = this.x;
				this.ySd = this.y;
				this.isOutMap = false;
			}
			while (this.isOutMap && this.wCount < 10)
			{
				this.wCount++;
				this.ySd += 24;
				if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
				{
					if (this.ySd % 24 != 0)
					{
						this.ySd -= this.ySd % 24;
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x001C57A4 File Offset: 0x001C39A4
		private void paintShadow(mGraphics g)
		{
			sbyte size = TileMap.size;
			g.drawImage(BigBoss2.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x001C57F8 File Offset: 0x001C39F8
		public override void update()
		{
			if (!this.isUpdate())
			{
				return;
			}
			this.updateShadown();
			switch (this.status)
			{
			case 0:
			case 1:
				this.updateDead();
				break;
			case 2:
				this.updateMobStandWait();
				return;
			case 3:
				this.updateMobAttack();
				return;
			case 4:
				this.timeStatus = 0;
				this.updateMobFly();
				return;
			case 5:
				this.timeStatus = 0;
				this.updateMobWalk();
				return;
			case 6:
				this.timeStatus = 0;
				this.p1++;
				this.y += this.p1;
				if (this.y >= this.yFirst)
				{
					this.y = this.yFirst;
					this.p1 = 0;
					this.status = 5;
					return;
				}
				break;
			case 7:
				this.updateInjure();
				return;
			default:
				return;
			}
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x001C58CC File Offset: 0x001C3ACC
		private void updateDead()
		{
			this.checkFrameTick(this.stand);
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
			}
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x001C5994 File Offset: 0x001C3B94
		private void updateMobFly()
		{
			if (this.flyUp)
			{
				this.dy++;
				this.y -= this.dy;
				this.checkFrameTick(this.fly);
				if (this.y <= -500)
				{
					this.flyUp = false;
					this.flyDown = true;
					this.dy = 0;
				}
			}
			if (this.flyDown)
			{
				this.x = this.xTo;
				this.dy += 2;
				this.y += this.dy;
				this.checkFrameTick(this.hitground);
				if (this.y > this.yFirst)
				{
					this.y = this.yFirst;
					this.flyDown = false;
					this.dy = 0;
					this.status = 2;
					GameScr.shock_scr = 10;
					this.shock = true;
				}
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x001C5A78 File Offset: 0x001C3C78
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.stand);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x001C5AE5 File Offset: 0x001C3CE5
		public void setAttack(Char[] cAttack, int[] dame, sbyte type)
		{
			this.status = 3;
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x001C5B0C File Offset: 0x001C3D0C
		public new void updateMobAttack()
		{
			if (this.type == 0)
			{
				if (this.tick == this.attack1.Length - 1)
				{
					this.status = 2;
				}
				this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
				this.checkFrameTick(this.attack1);
				this.x += (this.charAttack[0].cx - this.x) / 4;
				this.y += (this.charAttack[0].cy - this.y) / 4;
				this.xTo = this.x;
				if (this.tick == 8)
				{
					for (int i = 0; i < this.charAttack.Length; i++)
					{
						this.charAttack[i].doInjure((long)this.dameHP[i], 0L, false, false);
						ServerEffect.addServerEffect(102, this.charAttack[i].cx, this.charAttack[i].cy, 1);
					}
				}
			}
			if (this.type == 1)
			{
				if (this.tick == this.attack2.Length - 1)
				{
					this.status = 2;
				}
				this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
				this.checkFrameTick(this.attack2);
				if (this.tick == 8)
				{
					for (int j = 0; j < this.charAttack.Length; j++)
					{
						MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 25, true, (long)this.dameHP[j], 0L, this.charAttack[j], 24);
					}
				}
			}
			if (this.type != 2)
			{
				return;
			}
			if (this.tick == this.fly.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
			this.checkFrameTick(this.fly);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.xTo = this.x;
			this.yTo = this.y;
			if (this.tick == 12)
			{
				for (int k = 0; k < this.charAttack.Length; k++)
				{
					this.charAttack[k].doInjure((long)this.dameHP[k], 0L, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[k].cx, this.charAttack[k].cy, 1);
				}
			}
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000034B9 File Offset: 0x000016B9
		public new void updateMobWalk()
		{
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x001C2AB5 File Offset: 0x001C0CB5
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x001C5D9C File Offset: 0x001C3F9C
		public override void paint(mGraphics g)
		{
			if (BigBoss2.data == null || this.isHide)
			{
				return;
			}
			if (!this.isMafuba)
			{
				if (this.isShadown && this.status != 0)
				{
					this.paintShadow(g);
				}
				g.translate(0, GameCanvas.transY);
				if (!this.changBody)
				{
					BigBoss2.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				g.translate(0, -GameCanvas.transY);
				int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
				int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
				int num = imageWidth;
				int num2 = this.x - imageWidth;
				int num3 = this.y - this.h - 5;
				int num4 = imageWidth * 2 * this.per / 100;
				int num5;
				if (num4 > num)
				{
					num5 = num4 - num;
					if (num5 <= 0)
					{
						num5 = 0;
					}
				}
				else
				{
					num = num4;
					num5 = 0;
				}
				g.drawImage(GameScr.imgHP_tm_xam, num2, num3, mGraphics.TOP | mGraphics.LEFT);
				g.drawImage(GameScr.imgHP_tm_xam, num2 + imageWidth, num3, mGraphics.TOP | mGraphics.LEFT);
				g.drawRegion(this.imgHPtem, 0, 0, num, imageHeight, 0, num2, num3, mGraphics.TOP | mGraphics.LEFT);
				g.drawRegion(this.imgHPtem, 0, 0, num5, imageHeight, 0, num2 + imageWidth, num3, mGraphics.TOP | mGraphics.LEFT);
				if (this.shock)
				{
					this.tShock++;
					EffecMn.addEff(new Effect((this.type != 2) ? 22 : 19, this.x + this.tShock * 50, this.y + 25, 2, 1, -1));
					EffecMn.addEff(new Effect((this.type != 2) ? 22 : 19, this.x - this.tShock * 50, this.y + 25, 2, 1, -1));
					if (this.tShock == 50)
					{
						this.tShock = 0;
						this.shock = false;
					}
				}
				return;
			}
			if (!this.changBody)
			{
				BigBoss2.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
				return;
			}
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x001C2D64 File Offset: 0x001C0F64
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x001C603C File Offset: 0x001C423C
		public new int getY()
		{
			return this.y - 50;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x00006D13 File Offset: 0x00004F13
		public new int getW()
		{
			return 50;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x001C6048 File Offset: 0x001C4248
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x001C2DB6 File Offset: 0x001C0FB6
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x04003843 RID: 14403
		public static Image shadowBig;

		// Token: 0x04003844 RID: 14404
		public static EffectData data;

		// Token: 0x04003845 RID: 14405
		public int xTo;

		// Token: 0x04003846 RID: 14406
		public int yTo;

		// Token: 0x04003847 RID: 14407
		public new int xSd;

		// Token: 0x04003848 RID: 14408
		public new int ySd;

		// Token: 0x04003849 RID: 14409
		private bool isOutMap;

		// Token: 0x0400384A RID: 14410
		private int wCount;

		// Token: 0x0400384B RID: 14411
		public new bool isShadown = true;

		// Token: 0x0400384C RID: 14412
		private int tick;

		// Token: 0x0400384D RID: 14413
		private int frame;

		// Token: 0x0400384E RID: 14414
		public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x0400384F RID: 14415
		private int fy;

		// Token: 0x04003850 RID: 14416
		private bool flyUp;

		// Token: 0x04003851 RID: 14417
		private bool flyDown;

		// Token: 0x04003852 RID: 14418
		private int dy;

		// Token: 0x04003853 RID: 14419
		private int tShock;

		// Token: 0x04003854 RID: 14420
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04003855 RID: 14421
		private Char[] charAttack;

		// Token: 0x04003856 RID: 14422
		private int[] dameHP;

		// Token: 0x04003857 RID: 14423
		private sbyte type;

		// Token: 0x04003858 RID: 14424
		public new int[] stand = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			1,
			1
		};

		// Token: 0x04003859 RID: 14425
		public new int[] move = new int[]
		{
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			2,
			2,
			2
		};

		// Token: 0x0400385A RID: 14426
		public new int[] moveFast = new int[]
		{
			1,
			1,
			2,
			2,
			3,
			3,
			2
		};

		// Token: 0x0400385B RID: 14427
		public new int[] attack1 = new int[]
		{
			0,
			0,
			0,
			7,
			7,
			7,
			8,
			8,
			8,
			9,
			9,
			9
		};

		// Token: 0x0400385C RID: 14428
		public new int[] attack2 = new int[]
		{
			0,
			0,
			0,
			10,
			10,
			10,
			11,
			11,
			11,
			12,
			12,
			12
		};

		// Token: 0x0400385D RID: 14429
		public int[] attack3 = new int[]
		{
			0,
			0,
			1,
			1,
			4,
			4,
			6,
			6,
			8,
			8,
			25,
			25,
			26,
			26,
			28,
			28,
			30,
			30,
			32,
			32,
			2,
			2,
			1,
			1
		};

		// Token: 0x0400385E RID: 14430
		public int[] fly = new int[]
		{
			4,
			4,
			4,
			5,
			5,
			5,
			6,
			6,
			6,
			6,
			6,
			6,
			3,
			3,
			3,
			2,
			2,
			2,
			1,
			1,
			1
		};

		// Token: 0x0400385F RID: 14431
		public int[] hitground = new int[]
		{
			6,
			6,
			6,
			3,
			3,
			3,
			2,
			2,
			2,
			1,
			1,
			1
		};

		// Token: 0x04003860 RID: 14432
		private bool shock;

		// Token: 0x04003861 RID: 14433
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04003862 RID: 14434
		public new int forceWait;
	}
}
