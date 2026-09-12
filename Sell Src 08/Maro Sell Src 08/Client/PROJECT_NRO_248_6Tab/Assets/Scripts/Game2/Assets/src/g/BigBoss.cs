using System;

namespace Game2.Assets.src.g
{
	// Token: 0x02000434 RID: 1076
	public class BigBoss : Mob, IMapObject
	{
		// Token: 0x06002FF0 RID: 12272 RVA: 0x002E6620 File Offset: 0x002E4820
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

		// Token: 0x06002FF1 RID: 12273 RVA: 0x002E67EC File Offset: 0x002E49EC
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

		// Token: 0x06002FF2 RID: 12274 RVA: 0x002E68C8 File Offset: 0x002E4AC8
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

		// Token: 0x06002FF3 RID: 12275 RVA: 0x00257550 File Offset: 0x00255750
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x00257560 File Offset: 0x00255760
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x002E699C File Offset: 0x002E4B9C
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x002E69D0 File Offset: 0x002E4BD0
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

		// Token: 0x06002FF7 RID: 12279 RVA: 0x002E6AEC File Offset: 0x002E4CEC
		private void paintShadow(mGraphics g)
		{
			g.drawImage(BigBoss.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x002E6B3C File Offset: 0x002E4D3C
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

		// Token: 0x06002FF9 RID: 12281 RVA: 0x002E6C10 File Offset: 0x002E4E10
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

		// Token: 0x06002FFA RID: 12282 RVA: 0x002E6CE8 File Offset: 0x002E4EE8
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

		// Token: 0x06002FFB RID: 12283 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x002E6DCC File Offset: 0x002E4FCC
		private void updateMobStandWait()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x002E6E49 File Offset: 0x002E5049
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x002E6E5C File Offset: 0x002E505C
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

		// Token: 0x06002FFF RID: 12287 RVA: 0x002E6EE0 File Offset: 0x002E50E0
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

		// Token: 0x06003000 RID: 12288 RVA: 0x000034B9 File Offset: 0x000016B9
		public new void updateMobWalk()
		{
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x00257B59 File Offset: 0x00255D59
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x002E71B8 File Offset: 0x002E53B8
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

		// Token: 0x06003003 RID: 12291 RVA: 0x00257E08 File Offset: 0x00256008
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x002E7458 File Offset: 0x002E5658
		public new int getY()
		{
			if (this.haftBody)
			{
				return this.y - 20;
			}
			return this.y - 60;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000931E1 File Offset: 0x000913E1
		public new int getW()
		{
			return 60;
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x002E7478 File Offset: 0x002E5678
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x00257E5A File Offset: 0x0025605A
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x04005C12 RID: 23570
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x04005C13 RID: 23571
		public static EffectData data;

		// Token: 0x04005C14 RID: 23572
		public int xTo;

		// Token: 0x04005C15 RID: 23573
		public int yTo;

		// Token: 0x04005C16 RID: 23574
		public bool haftBody;

		// Token: 0x04005C17 RID: 23575
		public new int xSd;

		// Token: 0x04005C18 RID: 23576
		public new int ySd;

		// Token: 0x04005C19 RID: 23577
		private bool isOutMap;

		// Token: 0x04005C1A RID: 23578
		private int wCount;

		// Token: 0x04005C1B RID: 23579
		public new bool isShadown = true;

		// Token: 0x04005C1C RID: 23580
		private int tick;

		// Token: 0x04005C1D RID: 23581
		private int frame;

		// Token: 0x04005C1E RID: 23582
		private int fy;

		// Token: 0x04005C1F RID: 23583
		private bool flyUp;

		// Token: 0x04005C20 RID: 23584
		private bool flyDown;

		// Token: 0x04005C21 RID: 23585
		private int dy;

		// Token: 0x04005C22 RID: 23586
		private int tShock;

		// Token: 0x04005C23 RID: 23587
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04005C24 RID: 23588
		private Char[] charAttack;

		// Token: 0x04005C25 RID: 23589
		private int[] dameHP;

		// Token: 0x04005C26 RID: 23590
		private sbyte type;

		// Token: 0x04005C27 RID: 23591
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

		// Token: 0x04005C28 RID: 23592
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

		// Token: 0x04005C29 RID: 23593
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

		// Token: 0x04005C2A RID: 23594
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

		// Token: 0x04005C2B RID: 23595
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

		// Token: 0x04005C2C RID: 23596
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

		// Token: 0x04005C2D RID: 23597
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

		// Token: 0x04005C2E RID: 23598
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

		// Token: 0x04005C2F RID: 23599
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

		// Token: 0x04005C30 RID: 23600
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

		// Token: 0x04005C31 RID: 23601
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

		// Token: 0x04005C32 RID: 23602
		private bool shock;

		// Token: 0x04005C33 RID: 23603
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04005C34 RID: 23604
		public new int forceWait;
	}
}
