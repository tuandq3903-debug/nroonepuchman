using System;

namespace Game4
{
	// Token: 0x020001C3 RID: 451
	public class BachTuoc : Mob, IMapObject
	{
		// Token: 0x0600138F RID: 5007 RVA: 0x0012D200 File Offset: 0x0012B400
		public BachTuoc(int id, short px, short py, int templateID, long hp, long maxHp, int s)
		{
			this.mobId = id;
			this.xFirst = (this.x = (int)(px + 20));
			this.y = (int)py;
			this.yFirst = (int)py;
			this.xTo = this.x;
			this.yTo = this.y;
			this.maxHp = maxHp;
			this.hp = hp;
			this.templateId = templateID;
			this.w_hp_bar = 100;
			this.h_hp_bar = 6;
			this.len = this.w_hp_bar;
			base.updateHp_bar();
			this.getDataB();
			this.status = 2;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0012D334 File Offset: 0x0012B534
		public void getDataB()
		{
			BachTuoc.data = null;
			BachTuoc.data = new EffectData();
			string patch = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				108.ToString(),
				"/data"
			});
			try
			{
				BachTuoc.data.readData2(patch);
				BachTuoc.data.img = GameCanvas.loadImage("/effectdata/" + 108.ToString() + "/img.png");
			}
			catch (Exception)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.w = BachTuoc.data.width;
			this.h = BachTuoc.data.height;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0012D408 File Offset: 0x0012B608
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0012D418 File Offset: 0x0012B618
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0012D421 File Offset: 0x0012B621
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0012D454 File Offset: 0x0012B654
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

		// Token: 0x06001395 RID: 5013 RVA: 0x0012D570 File Offset: 0x0012B770
		private void paintShadow(mGraphics g)
		{
			sbyte size = TileMap.size;
			g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0012D5C4 File Offset: 0x0012B7C4
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
				break;
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

		// Token: 0x06001397 RID: 5015 RVA: 0x0012D688 File Offset: 0x0012B888
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

		// Token: 0x06001398 RID: 5016 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0012D750 File Offset: 0x0012B950
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.stand);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0012D7BD File Offset: 0x0012B9BD
		public void setAttack(Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.status = 3;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0012D7DC File Offset: 0x0012B9DC
		public new void updateMobAttack()
		{
			if (this.type == 3)
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
			if (this.type != 4)
			{
				return;
			}
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
					this.charAttack[j].doInjure((long)this.dameHP[j], 0L, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
				}
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0012D98C File Offset: 0x0012BB8C
		public new void updateMobWalk()
		{
			this.checkFrameTick(this.movee);
			this.x += ((this.x >= this.xTo) ? -2 : 2);
			this.y = this.yTo;
			this.dir = ((this.x < this.xTo) ? 1 : -1);
			if (Res.abs(this.x - this.xTo) <= 1)
			{
				this.x = this.xTo;
				this.status = 2;
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0012DA11 File Offset: 0x0012BC11
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0012DA20 File Offset: 0x0012BC20
		public override void paint(mGraphics g)
		{
			if (BachTuoc.data == null || this.isHide)
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
					BachTuoc.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
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
				BachTuoc.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
				return;
			}
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0012DCC0 File Offset: 0x0012BEC0
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0012DCC8 File Offset: 0x0012BEC8
		public new int getY()
		{
			return this.y - 40;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getW()
		{
			return 40;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0012DCD4 File Offset: 0x0012BED4
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0012DD12 File Offset: 0x0012BF12
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0012DD27 File Offset: 0x0012BF27
		public new void move(short xMoveTo)
		{
			this.xTo = (int)xMoveTo;
			this.status = 5;
		}

		// Token: 0x0400255A RID: 9562
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x0400255B RID: 9563
		public static EffectData data;

		// Token: 0x0400255C RID: 9564
		public int xTo;

		// Token: 0x0400255D RID: 9565
		public int yTo;

		// Token: 0x0400255E RID: 9566
		public new int xSd;

		// Token: 0x0400255F RID: 9567
		public new int ySd;

		// Token: 0x04002560 RID: 9568
		private bool isOutMap;

		// Token: 0x04002561 RID: 9569
		private int wCount;

		// Token: 0x04002562 RID: 9570
		public new bool isShadown = true;

		// Token: 0x04002563 RID: 9571
		private int tick;

		// Token: 0x04002564 RID: 9572
		private int frame;

		// Token: 0x04002565 RID: 9573
		public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04002566 RID: 9574
		private int fy;

		// Token: 0x04002567 RID: 9575
		private int tShock;

		// Token: 0x04002568 RID: 9576
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04002569 RID: 9577
		private Char[] charAttack;

		// Token: 0x0400256A RID: 9578
		private int[] dameHP;

		// Token: 0x0400256B RID: 9579
		private sbyte type;

		// Token: 0x0400256C RID: 9580
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

		// Token: 0x0400256D RID: 9581
		public int[] movee = new int[]
		{
			0,
			0,
			0,
			2,
			2,
			2,
			3,
			3,
			3,
			4,
			4,
			4
		};

		// Token: 0x0400256E RID: 9582
		public new int[] attack1 = new int[]
		{
			0,
			0,
			0,
			4,
			4,
			4,
			5,
			5,
			5,
			6,
			6,
			6
		};

		// Token: 0x0400256F RID: 9583
		public new int[] attack2 = new int[]
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
			9,
			10,
			10,
			10,
			11,
			11
		};

		// Token: 0x04002570 RID: 9584
		public new int[] hurt = new int[]
		{
			1,
			1,
			7,
			7
		};

		// Token: 0x04002571 RID: 9585
		private bool shock;

		// Token: 0x04002572 RID: 9586
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04002573 RID: 9587
		public new int forceWait;
	}
}
