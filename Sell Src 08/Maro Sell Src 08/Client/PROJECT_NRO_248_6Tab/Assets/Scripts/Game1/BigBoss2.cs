using System;

namespace Game1
{
	// Token: 0x02000450 RID: 1104
	public class BigBoss2 : Mob, IMapObject
	{
		// Token: 0x060030B8 RID: 12472 RVA: 0x002EF544 File Offset: 0x002ED744
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

		// Token: 0x060030B9 RID: 12473 RVA: 0x002EF6C8 File Offset: 0x002ED8C8
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

		// Token: 0x060030BA RID: 12474 RVA: 0x002EC5F4 File Offset: 0x002EA7F4
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x002EC604 File Offset: 0x002EA804
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x002EF79C File Offset: 0x002ED99C
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x002EF7D0 File Offset: 0x002ED9D0
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

		// Token: 0x060030BE RID: 12478 RVA: 0x002EF8EC File Offset: 0x002EDAEC
		private void paintShadow(mGraphics g)
		{
			sbyte size = TileMap.size;
			g.drawImage(BigBoss2.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x002EF940 File Offset: 0x002EDB40
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

		// Token: 0x060030C0 RID: 12480 RVA: 0x002EFA14 File Offset: 0x002EDC14
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

		// Token: 0x060030C1 RID: 12481 RVA: 0x002EFADC File Offset: 0x002EDCDC
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

		// Token: 0x060030C2 RID: 12482 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x002EFBC0 File Offset: 0x002EDDC0
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.stand);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x002EFC2D File Offset: 0x002EDE2D
		public void setAttack(Char[] cAttack, int[] dame, sbyte type)
		{
			this.status = 3;
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x002EFC54 File Offset: 0x002EDE54
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

		// Token: 0x060030C6 RID: 12486 RVA: 0x000034B9 File Offset: 0x000016B9
		public new void updateMobWalk()
		{
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x002ECBFD File Offset: 0x002EADFD
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x002EFEE4 File Offset: 0x002EE0E4
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

		// Token: 0x060030C9 RID: 12489 RVA: 0x002ECEAC File Offset: 0x002EB0AC
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x002F0184 File Offset: 0x002EE384
		public new int getY()
		{
			return this.y - 50;
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x00006D13 File Offset: 0x00004F13
		public new int getW()
		{
			return 50;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x002F0190 File Offset: 0x002EE390
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x002ECEFE File Offset: 0x002EB0FE
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x04005D41 RID: 23873
		public static Image shadowBig;

		// Token: 0x04005D42 RID: 23874
		public static EffectData data;

		// Token: 0x04005D43 RID: 23875
		public int xTo;

		// Token: 0x04005D44 RID: 23876
		public int yTo;

		// Token: 0x04005D45 RID: 23877
		public new int xSd;

		// Token: 0x04005D46 RID: 23878
		public new int ySd;

		// Token: 0x04005D47 RID: 23879
		private bool isOutMap;

		// Token: 0x04005D48 RID: 23880
		private int wCount;

		// Token: 0x04005D49 RID: 23881
		public new bool isShadown = true;

		// Token: 0x04005D4A RID: 23882
		private int tick;

		// Token: 0x04005D4B RID: 23883
		private int frame;

		// Token: 0x04005D4C RID: 23884
		public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04005D4D RID: 23885
		private int fy;

		// Token: 0x04005D4E RID: 23886
		private bool flyUp;

		// Token: 0x04005D4F RID: 23887
		private bool flyDown;

		// Token: 0x04005D50 RID: 23888
		private int dy;

		// Token: 0x04005D51 RID: 23889
		private int tShock;

		// Token: 0x04005D52 RID: 23890
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04005D53 RID: 23891
		private Char[] charAttack;

		// Token: 0x04005D54 RID: 23892
		private int[] dameHP;

		// Token: 0x04005D55 RID: 23893
		private sbyte type;

		// Token: 0x04005D56 RID: 23894
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

		// Token: 0x04005D57 RID: 23895
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

		// Token: 0x04005D58 RID: 23896
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

		// Token: 0x04005D59 RID: 23897
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

		// Token: 0x04005D5A RID: 23898
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

		// Token: 0x04005D5B RID: 23899
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

		// Token: 0x04005D5C RID: 23900
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

		// Token: 0x04005D5D RID: 23901
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

		// Token: 0x04005D5E RID: 23902
		private bool shock;

		// Token: 0x04005D5F RID: 23903
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04005D60 RID: 23904
		public new int forceWait;
	}
}
