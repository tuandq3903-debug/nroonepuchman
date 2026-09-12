using System;

namespace Game6
{
	// Token: 0x02000013 RID: 19
	public class BachTuoc : Mob, IMapObject
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002F6C File Offset: 0x0000116C
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

		// Token: 0x06000048 RID: 72 RVA: 0x000030A0 File Offset: 0x000012A0
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

		// Token: 0x06000049 RID: 73 RVA: 0x00003174 File Offset: 0x00001374
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003184 File Offset: 0x00001384
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000318D File Offset: 0x0000138D
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000031C0 File Offset: 0x000013C0
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

		// Token: 0x0600004D RID: 77 RVA: 0x000032DC File Offset: 0x000014DC
		private void paintShadow(mGraphics g)
		{
			sbyte size = TileMap.size;
			g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003330 File Offset: 0x00001530
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

		// Token: 0x0600004F RID: 79 RVA: 0x000033F4 File Offset: 0x000015F4
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

		// Token: 0x06000050 RID: 80 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000034BC File Offset: 0x000016BC
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.stand);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003529 File Offset: 0x00001729
		public void setAttack(Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.status = 3;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003548 File Offset: 0x00001748
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

		// Token: 0x06000054 RID: 84 RVA: 0x000036F8 File Offset: 0x000018F8
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

		// Token: 0x06000055 RID: 85 RVA: 0x0000377D File Offset: 0x0000197D
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000378C File Offset: 0x0000198C
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

		// Token: 0x06000057 RID: 87 RVA: 0x00003A2C File Offset: 0x00001C2C
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003A34 File Offset: 0x00001C34
		public new int getY()
		{
			return this.y - 40;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getW()
		{
			return 40;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003A44 File Offset: 0x00001C44
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003A82 File Offset: 0x00001C82
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003A97 File Offset: 0x00001C97
		public new void move(short xMoveTo)
		{
			this.xTo = (int)xMoveTo;
			this.status = 5;
		}

		// Token: 0x0400005D RID: 93
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x0400005E RID: 94
		public static EffectData data;

		// Token: 0x0400005F RID: 95
		public int xTo;

		// Token: 0x04000060 RID: 96
		public int yTo;

		// Token: 0x04000061 RID: 97
		public new int xSd;

		// Token: 0x04000062 RID: 98
		public new int ySd;

		// Token: 0x04000063 RID: 99
		private bool isOutMap;

		// Token: 0x04000064 RID: 100
		private int wCount;

		// Token: 0x04000065 RID: 101
		public new bool isShadown = true;

		// Token: 0x04000066 RID: 102
		private int tick;

		// Token: 0x04000067 RID: 103
		private int frame;

		// Token: 0x04000068 RID: 104
		public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04000069 RID: 105
		private int fy;

		// Token: 0x0400006A RID: 106
		private int tShock;

		// Token: 0x0400006B RID: 107
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x0400006C RID: 108
		private Char[] charAttack;

		// Token: 0x0400006D RID: 109
		private int[] dameHP;

		// Token: 0x0400006E RID: 110
		private sbyte type;

		// Token: 0x0400006F RID: 111
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

		// Token: 0x04000070 RID: 112
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

		// Token: 0x04000071 RID: 113
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

		// Token: 0x04000072 RID: 114
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

		// Token: 0x04000073 RID: 115
		public new int[] hurt = new int[]
		{
			1,
			1,
			7,
			7
		};

		// Token: 0x04000074 RID: 116
		private bool shock;

		// Token: 0x04000075 RID: 117
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04000076 RID: 118
		public new int forceWait;
	}
}
