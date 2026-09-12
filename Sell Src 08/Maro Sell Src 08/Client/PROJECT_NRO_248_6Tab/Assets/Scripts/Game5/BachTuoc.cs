using System;

namespace Game5
{
	// Token: 0x020000EB RID: 235
	public class BachTuoc : Mob, IMapObject
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x0009815C File Offset: 0x0009635C
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

		// Token: 0x060009EC RID: 2540 RVA: 0x00098290 File Offset: 0x00096490
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

		// Token: 0x060009ED RID: 2541 RVA: 0x00098364 File Offset: 0x00096564
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00098374 File Offset: 0x00096574
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0009837D File Offset: 0x0009657D
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000983B0 File Offset: 0x000965B0
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

		// Token: 0x060009F1 RID: 2545 RVA: 0x000984CC File Offset: 0x000966CC
		private void paintShadow(mGraphics g)
		{
			sbyte size = TileMap.size;
			g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00098520 File Offset: 0x00096720
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

		// Token: 0x060009F3 RID: 2547 RVA: 0x000985E4 File Offset: 0x000967E4
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

		// Token: 0x060009F4 RID: 2548 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000986AC File Offset: 0x000968AC
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.stand);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00098719 File Offset: 0x00096919
		public void setAttack(Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.status = 3;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00098738 File Offset: 0x00096938
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

		// Token: 0x060009F8 RID: 2552 RVA: 0x000988E8 File Offset: 0x00096AE8
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

		// Token: 0x060009F9 RID: 2553 RVA: 0x0009896D File Offset: 0x00096B6D
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0009897C File Offset: 0x00096B7C
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

		// Token: 0x060009FB RID: 2555 RVA: 0x00098C1C File Offset: 0x00096E1C
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00098C24 File Offset: 0x00096E24
		public new int getY()
		{
			return this.y - 40;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getW()
		{
			return 40;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00098C30 File Offset: 0x00096E30
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00098C6E File Offset: 0x00096E6E
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00098C83 File Offset: 0x00096E83
		public new void move(short xMoveTo)
		{
			this.xTo = (int)xMoveTo;
			this.status = 5;
		}

		// Token: 0x040012DB RID: 4827
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x040012DC RID: 4828
		public static EffectData data;

		// Token: 0x040012DD RID: 4829
		public int xTo;

		// Token: 0x040012DE RID: 4830
		public int yTo;

		// Token: 0x040012DF RID: 4831
		public new int xSd;

		// Token: 0x040012E0 RID: 4832
		public new int ySd;

		// Token: 0x040012E1 RID: 4833
		private bool isOutMap;

		// Token: 0x040012E2 RID: 4834
		private int wCount;

		// Token: 0x040012E3 RID: 4835
		public new bool isShadown = true;

		// Token: 0x040012E4 RID: 4836
		private int tick;

		// Token: 0x040012E5 RID: 4837
		private int frame;

		// Token: 0x040012E6 RID: 4838
		public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x040012E7 RID: 4839
		private int fy;

		// Token: 0x040012E8 RID: 4840
		private int tShock;

		// Token: 0x040012E9 RID: 4841
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x040012EA RID: 4842
		private Char[] charAttack;

		// Token: 0x040012EB RID: 4843
		private int[] dameHP;

		// Token: 0x040012EC RID: 4844
		private sbyte type;

		// Token: 0x040012ED RID: 4845
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

		// Token: 0x040012EE RID: 4846
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

		// Token: 0x040012EF RID: 4847
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

		// Token: 0x040012F0 RID: 4848
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

		// Token: 0x040012F1 RID: 4849
		public new int[] hurt = new int[]
		{
			1,
			1,
			7,
			7
		};

		// Token: 0x040012F2 RID: 4850
		private bool shock;

		// Token: 0x040012F3 RID: 4851
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x040012F4 RID: 4852
		public new int forceWait;
	}
}
