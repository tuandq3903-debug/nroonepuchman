using System;

namespace Game2
{
	// Token: 0x02000378 RID: 888
	public class BigBoss2 : Mob, IMapObject
	{
		// Token: 0x06002714 RID: 10004 RVA: 0x0025A4A0 File Offset: 0x002586A0
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

		// Token: 0x06002715 RID: 10005 RVA: 0x0025A624 File Offset: 0x00258824
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

		// Token: 0x06002716 RID: 10006 RVA: 0x00257550 File Offset: 0x00255750
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x00257560 File Offset: 0x00255760
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x0025A6F8 File Offset: 0x002588F8
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x0025A72C File Offset: 0x0025892C
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

		// Token: 0x0600271A RID: 10010 RVA: 0x0025A848 File Offset: 0x00258A48
		private void paintShadow(mGraphics g)
		{
			sbyte size = TileMap.size;
			g.drawImage(BigBoss2.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0025A89C File Offset: 0x00258A9C
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

		// Token: 0x0600271C RID: 10012 RVA: 0x0025A970 File Offset: 0x00258B70
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

		// Token: 0x0600271D RID: 10013 RVA: 0x0025AA38 File Offset: 0x00258C38
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

		// Token: 0x0600271E RID: 10014 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0025AB1C File Offset: 0x00258D1C
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.stand);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x0025AB89 File Offset: 0x00258D89
		public void setAttack(Char[] cAttack, int[] dame, sbyte type)
		{
			this.status = 3;
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0025ABB0 File Offset: 0x00258DB0
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

		// Token: 0x06002722 RID: 10018 RVA: 0x000034B9 File Offset: 0x000016B9
		public new void updateMobWalk()
		{
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00257B59 File Offset: 0x00255D59
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x0025AE40 File Offset: 0x00259040
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

		// Token: 0x06002725 RID: 10021 RVA: 0x00257E08 File Offset: 0x00256008
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x0025B0E0 File Offset: 0x002592E0
		public new int getY()
		{
			return this.y - 50;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x00006D13 File Offset: 0x00004F13
		public new int getW()
		{
			return 50;
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x0025B0EC File Offset: 0x002592EC
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x00257E5A File Offset: 0x0025605A
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x04004AC2 RID: 19138
		public static Image shadowBig;

		// Token: 0x04004AC3 RID: 19139
		public static EffectData data;

		// Token: 0x04004AC4 RID: 19140
		public int xTo;

		// Token: 0x04004AC5 RID: 19141
		public int yTo;

		// Token: 0x04004AC6 RID: 19142
		public new int xSd;

		// Token: 0x04004AC7 RID: 19143
		public new int ySd;

		// Token: 0x04004AC8 RID: 19144
		private bool isOutMap;

		// Token: 0x04004AC9 RID: 19145
		private int wCount;

		// Token: 0x04004ACA RID: 19146
		public new bool isShadown = true;

		// Token: 0x04004ACB RID: 19147
		private int tick;

		// Token: 0x04004ACC RID: 19148
		private int frame;

		// Token: 0x04004ACD RID: 19149
		public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04004ACE RID: 19150
		private int fy;

		// Token: 0x04004ACF RID: 19151
		private bool flyUp;

		// Token: 0x04004AD0 RID: 19152
		private bool flyDown;

		// Token: 0x04004AD1 RID: 19153
		private int dy;

		// Token: 0x04004AD2 RID: 19154
		private int tShock;

		// Token: 0x04004AD3 RID: 19155
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04004AD4 RID: 19156
		private Char[] charAttack;

		// Token: 0x04004AD5 RID: 19157
		private int[] dameHP;

		// Token: 0x04004AD6 RID: 19158
		private sbyte type;

		// Token: 0x04004AD7 RID: 19159
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

		// Token: 0x04004AD8 RID: 19160
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

		// Token: 0x04004AD9 RID: 19161
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

		// Token: 0x04004ADA RID: 19162
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

		// Token: 0x04004ADB RID: 19163
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

		// Token: 0x04004ADC RID: 19164
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

		// Token: 0x04004ADD RID: 19165
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

		// Token: 0x04004ADE RID: 19166
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

		// Token: 0x04004ADF RID: 19167
		private bool shock;

		// Token: 0x04004AE0 RID: 19168
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04004AE1 RID: 19169
		public new int forceWait;
	}
}
