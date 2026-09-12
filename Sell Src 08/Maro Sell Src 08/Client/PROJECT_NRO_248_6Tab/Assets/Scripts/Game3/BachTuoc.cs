using System;

namespace Game3
{
	// Token: 0x0200029B RID: 667
	public class BachTuoc : Mob, IMapObject
	{
		// Token: 0x06001D33 RID: 7475 RVA: 0x001C22A4 File Offset: 0x001C04A4
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

		// Token: 0x06001D34 RID: 7476 RVA: 0x001C23D8 File Offset: 0x001C05D8
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

		// Token: 0x06001D35 RID: 7477 RVA: 0x001C24AC File Offset: 0x001C06AC
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x001C24BC File Offset: 0x001C06BC
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x001C24C5 File Offset: 0x001C06C5
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x001C24F8 File Offset: 0x001C06F8
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

		// Token: 0x06001D39 RID: 7481 RVA: 0x001C2614 File Offset: 0x001C0814
		private void paintShadow(mGraphics g)
		{
			sbyte size = TileMap.size;
			g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x001C2668 File Offset: 0x001C0868
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

		// Token: 0x06001D3B RID: 7483 RVA: 0x001C272C File Offset: 0x001C092C
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

		// Token: 0x06001D3C RID: 7484 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x001C27F4 File Offset: 0x001C09F4
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.stand);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x001C2861 File Offset: 0x001C0A61
		public void setAttack(Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.status = 3;
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x001C2880 File Offset: 0x001C0A80
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

		// Token: 0x06001D40 RID: 7488 RVA: 0x001C2A30 File Offset: 0x001C0C30
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

		// Token: 0x06001D41 RID: 7489 RVA: 0x001C2AB5 File Offset: 0x001C0CB5
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x001C2AC4 File Offset: 0x001C0CC4
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

		// Token: 0x06001D43 RID: 7491 RVA: 0x001C2D64 File Offset: 0x001C0F64
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x001C2D6C File Offset: 0x001C0F6C
		public new int getY()
		{
			return this.y - 40;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getW()
		{
			return 40;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x001C2D78 File Offset: 0x001C0F78
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x001C2DB6 File Offset: 0x001C0FB6
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x001C2DCB File Offset: 0x001C0FCB
		public new void move(short xMoveTo)
		{
			this.xTo = (int)xMoveTo;
			this.status = 5;
		}

		// Token: 0x040037D9 RID: 14297
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x040037DA RID: 14298
		public static EffectData data;

		// Token: 0x040037DB RID: 14299
		public int xTo;

		// Token: 0x040037DC RID: 14300
		public int yTo;

		// Token: 0x040037DD RID: 14301
		public new int xSd;

		// Token: 0x040037DE RID: 14302
		public new int ySd;

		// Token: 0x040037DF RID: 14303
		private bool isOutMap;

		// Token: 0x040037E0 RID: 14304
		private int wCount;

		// Token: 0x040037E1 RID: 14305
		public new bool isShadown = true;

		// Token: 0x040037E2 RID: 14306
		private int tick;

		// Token: 0x040037E3 RID: 14307
		private int frame;

		// Token: 0x040037E4 RID: 14308
		public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x040037E5 RID: 14309
		private int fy;

		// Token: 0x040037E6 RID: 14310
		private int tShock;

		// Token: 0x040037E7 RID: 14311
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x040037E8 RID: 14312
		private Char[] charAttack;

		// Token: 0x040037E9 RID: 14313
		private int[] dameHP;

		// Token: 0x040037EA RID: 14314
		private sbyte type;

		// Token: 0x040037EB RID: 14315
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

		// Token: 0x040037EC RID: 14316
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

		// Token: 0x040037ED RID: 14317
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

		// Token: 0x040037EE RID: 14318
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

		// Token: 0x040037EF RID: 14319
		public new int[] hurt = new int[]
		{
			1,
			1,
			7,
			7
		};

		// Token: 0x040037F0 RID: 14320
		private bool shock;

		// Token: 0x040037F1 RID: 14321
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x040037F2 RID: 14322
		public new int forceWait;
	}
}
