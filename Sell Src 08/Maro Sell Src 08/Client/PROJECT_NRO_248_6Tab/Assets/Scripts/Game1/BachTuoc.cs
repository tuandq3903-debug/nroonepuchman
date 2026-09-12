using System;

namespace Game1
{
	// Token: 0x0200044B RID: 1099
	public class BachTuoc : Mob, IMapObject
	{
		// Token: 0x0600307B RID: 12411 RVA: 0x002EC3EC File Offset: 0x002EA5EC
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

		// Token: 0x0600307C RID: 12412 RVA: 0x002EC520 File Offset: 0x002EA720
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

		// Token: 0x0600307D RID: 12413 RVA: 0x002EC5F4 File Offset: 0x002EA7F4
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x002EC604 File Offset: 0x002EA804
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x002EC60D File Offset: 0x002EA80D
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x002EC640 File Offset: 0x002EA840
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

		// Token: 0x06003081 RID: 12417 RVA: 0x002EC75C File Offset: 0x002EA95C
		private void paintShadow(mGraphics g)
		{
			sbyte size = TileMap.size;
			g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x002EC7B0 File Offset: 0x002EA9B0
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

		// Token: 0x06003083 RID: 12419 RVA: 0x002EC874 File Offset: 0x002EAA74
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

		// Token: 0x06003084 RID: 12420 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x002EC93C File Offset: 0x002EAB3C
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.stand);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x002EC9A9 File Offset: 0x002EABA9
		public void setAttack(Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.status = 3;
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x002EC9C8 File Offset: 0x002EABC8
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

		// Token: 0x06003088 RID: 12424 RVA: 0x002ECB78 File Offset: 0x002EAD78
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

		// Token: 0x06003089 RID: 12425 RVA: 0x002ECBFD File Offset: 0x002EADFD
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x002ECC0C File Offset: 0x002EAE0C
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

		// Token: 0x0600308B RID: 12427 RVA: 0x002ECEAC File Offset: 0x002EB0AC
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x002ECEB4 File Offset: 0x002EB0B4
		public new int getY()
		{
			return this.y - 40;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getW()
		{
			return 40;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x002ECEC0 File Offset: 0x002EB0C0
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x002ECEFE File Offset: 0x002EB0FE
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x002ECF13 File Offset: 0x002EB113
		public new void move(short xMoveTo)
		{
			this.xTo = (int)xMoveTo;
			this.status = 5;
		}

		// Token: 0x04005CD7 RID: 23767
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x04005CD8 RID: 23768
		public static EffectData data;

		// Token: 0x04005CD9 RID: 23769
		public int xTo;

		// Token: 0x04005CDA RID: 23770
		public int yTo;

		// Token: 0x04005CDB RID: 23771
		public new int xSd;

		// Token: 0x04005CDC RID: 23772
		public new int ySd;

		// Token: 0x04005CDD RID: 23773
		private bool isOutMap;

		// Token: 0x04005CDE RID: 23774
		private int wCount;

		// Token: 0x04005CDF RID: 23775
		public new bool isShadown = true;

		// Token: 0x04005CE0 RID: 23776
		private int tick;

		// Token: 0x04005CE1 RID: 23777
		private int frame;

		// Token: 0x04005CE2 RID: 23778
		public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04005CE3 RID: 23779
		private int fy;

		// Token: 0x04005CE4 RID: 23780
		private int tShock;

		// Token: 0x04005CE5 RID: 23781
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04005CE6 RID: 23782
		private Char[] charAttack;

		// Token: 0x04005CE7 RID: 23783
		private int[] dameHP;

		// Token: 0x04005CE8 RID: 23784
		private sbyte type;

		// Token: 0x04005CE9 RID: 23785
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

		// Token: 0x04005CEA RID: 23786
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

		// Token: 0x04005CEB RID: 23787
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

		// Token: 0x04005CEC RID: 23788
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

		// Token: 0x04005CED RID: 23789
		public new int[] hurt = new int[]
		{
			1,
			1,
			7,
			7
		};

		// Token: 0x04005CEE RID: 23790
		private bool shock;

		// Token: 0x04005CEF RID: 23791
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04005CF0 RID: 23792
		public new int forceWait;
	}
}
