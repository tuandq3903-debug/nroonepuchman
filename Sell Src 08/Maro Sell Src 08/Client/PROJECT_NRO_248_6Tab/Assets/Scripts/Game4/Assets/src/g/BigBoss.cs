using System;

namespace Game4.Assets.src.g
{
	// Token: 0x02000284 RID: 644
	public class BigBoss : Mob, IMapObject
	{
		// Token: 0x06001CA8 RID: 7336 RVA: 0x001BC4D8 File Offset: 0x001BA6D8
		public BigBoss(int id, short px, short py, int templateID, long hp, long maxhp, int s)
		{
			this.xFirst = (this.x = (int)(px + 20));
			this.y = (int)py;
			this.yFirst = (int)py;
			this.mobId = id;
			this.hp = hp;
			this.maxHp = maxhp;
			this.templateId = templateID;
			this.w_hp_bar = 100;
			this.h_hp_bar = 6;
			this.len = this.w_hp_bar;
			base.updateHp_bar();
			if (s == 0)
			{
				this.getDataB();
			}
			if (s == 1)
			{
				this.getDataB2();
			}
			if (s == 2)
			{
				this.getDataB2();
				this.haftBody = true;
			}
			this.status = 2;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x001BC6A4 File Offset: 0x001BA8A4
		public void getDataB2()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				100.ToString(),
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 100.ToString() + "/img.png");
			}
			catch (Exception)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.status = 2;
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x001BC780 File Offset: 0x001BA980
		public void getDataB()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				101.ToString(),
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 101.ToString() + "/img.png");
			}
			catch (Exception)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0012D408 File Offset: 0x0012B608
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0012D418 File Offset: 0x0012B618
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x001BC854 File Offset: 0x001BAA54
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x001BC888 File Offset: 0x001BAA88
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

		// Token: 0x06001CAF RID: 7343 RVA: 0x001BC9A4 File Offset: 0x001BABA4
		private void paintShadow(mGraphics g)
		{
			g.drawImage(BigBoss.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x001BC9F4 File Offset: 0x001BABF4
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

		// Token: 0x06001CB1 RID: 7345 RVA: 0x001BCAC8 File Offset: 0x001BACC8
		private void updateDead()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
			}
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x001BCBA0 File Offset: 0x001BADA0
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

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x001BCC84 File Offset: 0x001BAE84
		private void updateMobStandWait()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x001BCD01 File Offset: 0x001BAF01
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x001BCD14 File Offset: 0x001BAF14
		public void setAttack(Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
			if (type < 3)
			{
				this.status = 3;
			}
			if (type == 3)
			{
				this.flyUp = true;
				this.status = 4;
			}
			if (type == 4)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure((long)this.dameHP[i], 0L, false, false);
				}
			}
			if (type == 7)
			{
				this.status = 3;
			}
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x001BCD98 File Offset: 0x001BAF98
		public new void updateMobAttack()
		{
			if (this.type == 7)
			{
				if (this.tick > 8)
				{
					this.tick = 8;
				}
				this.checkFrameTick(this.attack1);
				if (GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(70, this.x + ((this.dir != 1) ? -15 : 15), this.y - 40, 1);
				}
			}
			if (this.type == 0)
			{
				if (this.tick == this.attack1.Length - 1)
				{
					this.status = 2;
				}
				this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
				this.checkFrameTick(this.attack1);
				if (this.tick == 8)
				{
					for (int i = 0; i < this.charAttack.Length; i++)
					{
						MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 30, true, (long)this.dameHP[i], 0L, this.charAttack[i], 24);
					}
				}
			}
			if (this.type == 1)
			{
				if (this.tick == ((!this.haftBody) ? (this.attack2.Length - 1) : (this.attack2_1.Length - 1)))
				{
					this.status = 2;
				}
				this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
				this.checkFrameTick((!this.haftBody) ? this.attack2 : this.attack2_1);
				this.x += (this.charAttack[0].cx - this.x) / 4;
				this.y += (this.charAttack[0].cy - this.y) / 4;
				if (this.tick == 18)
				{
					for (int j = 0; j < this.charAttack.Length; j++)
					{
						this.charAttack[j].doInjure((long)this.dameHP[j], 0L, false, false);
						ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
					}
				}
			}
			sbyte b = this.type;
			if (this.type != 2)
			{
				return;
			}
			if (this.tick == ((!this.haftBody) ? (this.attack3.Length - 1) : (this.attack3_1.Length - 1)))
			{
				this.status = 2;
			}
			this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
			this.checkFrameTick((!this.haftBody) ? this.attack3 : this.attack3_1);
			if (this.tick == 13)
			{
				GameScr.shock_scr = 10;
				this.shock = true;
				for (int k = 0; k < this.charAttack.Length; k++)
				{
					this.charAttack[k].doInjure((long)this.dameHP[k], 0L, false, false);
				}
			}
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000034B9 File Offset: 0x000016B9
		public new void updateMobWalk()
		{
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0012DA11 File Offset: 0x0012BC11
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x001BD070 File Offset: 0x001BB270
		public override void paint(mGraphics g)
		{
			if (BigBoss.data == null || this.isHide)
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
					BigBoss.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
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
				BigBoss.data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
				return;
			}
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0012DCC0 File Offset: 0x0012BEC0
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x001BD310 File Offset: 0x001BB510
		public new int getY()
		{
			if (this.haftBody)
			{
				return this.y - 20;
			}
			return this.y - 60;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x000931E1 File Offset: 0x000913E1
		public new int getW()
		{
			return 60;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x001BD330 File Offset: 0x001BB530
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0012DD12 File Offset: 0x0012BF12
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x04003714 RID: 14100
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x04003715 RID: 14101
		public static EffectData data;

		// Token: 0x04003716 RID: 14102
		public int xTo;

		// Token: 0x04003717 RID: 14103
		public int yTo;

		// Token: 0x04003718 RID: 14104
		public bool haftBody;

		// Token: 0x04003719 RID: 14105
		public new int xSd;

		// Token: 0x0400371A RID: 14106
		public new int ySd;

		// Token: 0x0400371B RID: 14107
		private bool isOutMap;

		// Token: 0x0400371C RID: 14108
		private int wCount;

		// Token: 0x0400371D RID: 14109
		public new bool isShadown = true;

		// Token: 0x0400371E RID: 14110
		private int tick;

		// Token: 0x0400371F RID: 14111
		private int frame;

		// Token: 0x04003720 RID: 14112
		private int fy;

		// Token: 0x04003721 RID: 14113
		private bool flyUp;

		// Token: 0x04003722 RID: 14114
		private bool flyDown;

		// Token: 0x04003723 RID: 14115
		private int dy;

		// Token: 0x04003724 RID: 14116
		private int tShock;

		// Token: 0x04003725 RID: 14117
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04003726 RID: 14118
		private Char[] charAttack;

		// Token: 0x04003727 RID: 14119
		private int[] dameHP;

		// Token: 0x04003728 RID: 14120
		private sbyte type;

		// Token: 0x04003729 RID: 14121
		public new int[] stand = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		};

		// Token: 0x0400372A RID: 14122
		public int[] stand_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			38,
			39,
			39,
			40,
			40,
			40,
			39,
			39,
			39,
			38,
			38,
			38
		};

		// Token: 0x0400372B RID: 14123
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

		// Token: 0x0400372C RID: 14124
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

		// Token: 0x0400372D RID: 14125
		public new int[] attack1 = new int[]
		{
			0,
			0,
			34,
			34,
			35,
			35,
			36,
			36,
			2,
			2,
			1,
			1
		};

		// Token: 0x0400372E RID: 14126
		public new int[] attack2 = new int[]
		{
			0,
			0,
			0,
			4,
			4,
			6,
			6,
			9,
			9,
			10,
			10,
			13,
			13,
			15,
			15,
			17,
			17,
			19,
			19,
			21,
			21,
			23,
			23
		};

		// Token: 0x0400372F RID: 14127
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

		// Token: 0x04003730 RID: 14128
		public int[] attack2_1 = new int[]
		{
			37,
			37,
			5,
			5,
			7,
			7,
			11,
			11,
			14,
			14,
			16,
			16,
			18,
			18,
			20,
			20,
			22,
			22,
			24,
			24
		};

		// Token: 0x04003731 RID: 14129
		public int[] attack3_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			5,
			5,
			7,
			7,
			11,
			11,
			27,
			27,
			29,
			29,
			31,
			31,
			33,
			33,
			38,
			38
		};

		// Token: 0x04003732 RID: 14130
		public int[] fly = new int[]
		{
			8,
			8,
			9,
			9,
			10,
			10,
			12,
			12
		};

		// Token: 0x04003733 RID: 14131
		public int[] hitground = new int[]
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

		// Token: 0x04003734 RID: 14132
		private bool shock;

		// Token: 0x04003735 RID: 14133
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04003736 RID: 14134
		public new int forceWait;
	}
}
