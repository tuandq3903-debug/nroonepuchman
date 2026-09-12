using System;

namespace Game5.Assets.src.g
{
	// Token: 0x020001AC RID: 428
	public class BigBoss : Mob, IMapObject
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x00127434 File Offset: 0x00125634
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

		// Token: 0x06001305 RID: 4869 RVA: 0x00127600 File Offset: 0x00125800
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

		// Token: 0x06001306 RID: 4870 RVA: 0x001276DC File Offset: 0x001258DC
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

		// Token: 0x06001307 RID: 4871 RVA: 0x00098364 File Offset: 0x00096564
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00098374 File Offset: 0x00096574
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x001277B0 File Offset: 0x001259B0
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x001277E4 File Offset: 0x001259E4
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

		// Token: 0x0600130B RID: 4875 RVA: 0x00127900 File Offset: 0x00125B00
		private void paintShadow(mGraphics g)
		{
			g.drawImage(BigBoss.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00127950 File Offset: 0x00125B50
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

		// Token: 0x0600130D RID: 4877 RVA: 0x00127A24 File Offset: 0x00125C24
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

		// Token: 0x0600130E RID: 4878 RVA: 0x00127AFC File Offset: 0x00125CFC
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

		// Token: 0x0600130F RID: 4879 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00127BE0 File Offset: 0x00125DE0
		private void updateMobStandWait()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00127C5D File Offset: 0x00125E5D
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00127C70 File Offset: 0x00125E70
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

		// Token: 0x06001313 RID: 4883 RVA: 0x00127CF4 File Offset: 0x00125EF4
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

		// Token: 0x06001314 RID: 4884 RVA: 0x000034B9 File Offset: 0x000016B9
		public new void updateMobWalk()
		{
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0009896D File Offset: 0x00096B6D
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00127FCC File Offset: 0x001261CC
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

		// Token: 0x06001317 RID: 4887 RVA: 0x00098C1C File Offset: 0x00096E1C
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0012826C File Offset: 0x0012646C
		public new int getY()
		{
			if (this.haftBody)
			{
				return this.y - 20;
			}
			return this.y - 60;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00003A3F File Offset: 0x00001C3F
		public new int getH()
		{
			return 40;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000931E1 File Offset: 0x000913E1
		public new int getW()
		{
			return 60;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0012828C File Offset: 0x0012648C
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00098C6E File Offset: 0x00096E6E
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x04002495 RID: 9365
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x04002496 RID: 9366
		public static EffectData data;

		// Token: 0x04002497 RID: 9367
		public int xTo;

		// Token: 0x04002498 RID: 9368
		public int yTo;

		// Token: 0x04002499 RID: 9369
		public bool haftBody;

		// Token: 0x0400249A RID: 9370
		public new int xSd;

		// Token: 0x0400249B RID: 9371
		public new int ySd;

		// Token: 0x0400249C RID: 9372
		private bool isOutMap;

		// Token: 0x0400249D RID: 9373
		private int wCount;

		// Token: 0x0400249E RID: 9374
		public new bool isShadown = true;

		// Token: 0x0400249F RID: 9375
		private int tick;

		// Token: 0x040024A0 RID: 9376
		private int frame;

		// Token: 0x040024A1 RID: 9377
		private int fy;

		// Token: 0x040024A2 RID: 9378
		private bool flyUp;

		// Token: 0x040024A3 RID: 9379
		private bool flyDown;

		// Token: 0x040024A4 RID: 9380
		private int dy;

		// Token: 0x040024A5 RID: 9381
		private int tShock;

		// Token: 0x040024A6 RID: 9382
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x040024A7 RID: 9383
		private Char[] charAttack;

		// Token: 0x040024A8 RID: 9384
		private int[] dameHP;

		// Token: 0x040024A9 RID: 9385
		private sbyte type;

		// Token: 0x040024AA RID: 9386
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

		// Token: 0x040024AB RID: 9387
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

		// Token: 0x040024AC RID: 9388
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

		// Token: 0x040024AD RID: 9389
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

		// Token: 0x040024AE RID: 9390
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

		// Token: 0x040024AF RID: 9391
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

		// Token: 0x040024B0 RID: 9392
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

		// Token: 0x040024B1 RID: 9393
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

		// Token: 0x040024B2 RID: 9394
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

		// Token: 0x040024B3 RID: 9395
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

		// Token: 0x040024B4 RID: 9396
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

		// Token: 0x040024B5 RID: 9397
		private bool shock;

		// Token: 0x040024B6 RID: 9398
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x040024B7 RID: 9399
		public new int forceWait;
	}
}
