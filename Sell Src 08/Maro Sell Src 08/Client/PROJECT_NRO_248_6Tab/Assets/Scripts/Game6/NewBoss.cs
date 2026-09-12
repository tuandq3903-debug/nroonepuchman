using System;

namespace Game6
{
	// Token: 0x02000086 RID: 134
	public class NewBoss : Mob, IMapObject
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x0005BBCC File Offset: 0x00059DCC
		public NewBoss(int id, short px, short py, int templateID, long hp, long maxHp, int s)
		{
			this.mobId = id;
			this.x = (this.xFirst = (int)(px + 20));
			this.yFirst = (int)py;
			this.y = (int)py;
			this.xTo = this.x;
			this.yTo = this.y;
			this.maxHp = maxHp;
			this.hp = hp;
			this.templateId = templateID;
			this.h_hp_bar = 6;
			this.w_hp_bar = 100;
			this.len = this.w_hp_bar;
			base.updateHp_bar();
			if (Mob.arrMobTemplate[this.templateId].data == null)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.status = 2;
			this.frameArr = null;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00003174 File Offset: 0x00001374
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00003184 File Offset: 0x00001384
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0005BE2B File Offset: 0x0005A02B
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0005BE60 File Offset: 0x0005A060
		public void updateShadown()
		{
			int num = 0;
			this.xSd = this.x;
			if (TileMap.tileTypeAt(this.x, this.y, 2))
			{
				this.ySd = this.y;
				return;
			}
			this.ySd = this.y;
			while (num < 30)
			{
				num++;
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

		// Token: 0x0600059E RID: 1438 RVA: 0x0005BEF8 File Offset: 0x0005A0F8
		private void paintShadow(mGraphics g)
		{
			int num = (int)TileMap.size;
			if ((TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128)
			{
				if (TileMap.tileTypeAt(this.xSd + num / 2, this.ySd + 1, 4))
				{
					g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
				}
				else if (TileMap.tileTypeAt((this.xSd - num / 2) / num, (this.ySd + 1) / num) == 0)
				{
					g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, 100, 100);
				}
				else if (TileMap.tileTypeAt((this.xSd + num / 2) / num, (this.ySd + 1) / num) == 0)
				{
					g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
				}
				else if (TileMap.tileTypeAt(this.xSd - num / 2, this.ySd + 1, 8))
				{
					g.setClip(this.xSd / 24 * num, (this.ySd - 30) / num * num, num, 100);
				}
			}
			g.drawImage(NewBoss.shadowBig, this.xSd, this.ySd - 5, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0005C078 File Offset: 0x0005A278
		public override void update()
		{
			if (this.frameArr == null && Mob.arrMobTemplate[this.templateId].data != null)
			{
				this.GetFrame();
			}
			if (this.frameArr == null || !this.isUpdate())
			{
				return;
			}
			this.updateShadown();
			switch (this.status)
			{
			case 0:
			case 1:
				this.updateDead();
				return;
			case 2:
				this.updateMobStandWait();
				return;
			case 3:
				this.updateMobAttack();
				return;
			case 4:
				this.updateMobFly();
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
				base.update();
				return;
			default:
				return;
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0005C174 File Offset: 0x0005A374
		private void updateDead()
		{
			this.tick++;
			if (this.tick > this.frameArr[13].Length - 1)
			{
				this.tick = this.frameArr[13].Length - 1;
			}
			this.frame = this.frameArr[13][this.tick];
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateMobFly()
		{
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0005C224 File Offset: 0x0005A424
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.frameArr[0]);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0005C293 File Offset: 0x0005A493
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0005C2A4 File Offset: 0x0005A4A4
		public void setAttack(Char[] cAttack, int[] dame, sbyte type, sbyte dir)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.dir = (int)dir;
			this.status = 3;
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0005C32C File Offset: 0x0005A52C
		public new void updateMobAttack()
		{
			if (this.tick == this.frameArr[(int)(this.type + 1)].Length - 1)
			{
				this.status = 2;
			}
			this.checkFrameTick(this.frameArr[(int)(this.type + 1)]);
			if (this.tick == this.frameArr[15][(int)(this.type - 1)])
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure((long)this.dameHP[i], 0L, false, false);
					ServerEffect.addServerEffect(this.frameArr[16][(int)(this.type - 1)], this.charAttack[i].cx, this.charAttack[i].cy, 1);
				}
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0005C3EC File Offset: 0x0005A5EC
		public new void updateMobWalk()
		{
			this.checkFrameTick(this.frameArr[1]);
			sbyte speed = Mob.arrMobTemplate[this.templateId].speed;
			int num = (int)speed;
			if (Res.abs(this.x - this.xTo) < (int)speed)
			{
				num = Res.abs(this.x - this.xTo);
			}
			this.x += ((this.x >= this.xTo) ? (-num) : num);
			this.y = this.yTo;
			if (this.x < this.xTo)
			{
				this.dir = 1;
			}
			else if (this.x > this.xTo)
			{
				this.dir = -1;
			}
			if (Res.abs(this.x - this.xTo) <= 1)
			{
				this.x = this.xTo;
				this.status = 2;
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000377D File Offset: 0x0000197D
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0005C4C4 File Offset: 0x0005A6C4
		public override void paint(mGraphics g)
		{
			if (Mob.arrMobTemplate[this.templateId].data == null || this.isHide)
			{
				return;
			}
			if (this.isMafuba)
			{
				if (!this.changBody)
				{
					Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.xMFB, this.yMFB, (this.dir != 1) ? 1 : 0, 2);
					return;
				}
				SmallImage.drawSmallImage(g, (int)this.smallBody, this.xMFB, this.yMFB, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
			else
			{
				if (this.isShadown)
				{
					this.paintShadow(g);
				}
				g.translate(0, GameCanvas.transY);
				if (!this.changBody)
				{
					int num = 33;
					if (this.yTemp == -1)
					{
						this.yTemp = this.y;
					}
					if (TileMap.tileTypeAt(this.x + num, this.y + this.fy, 4))
					{
						this.xTempLeft = TileMap.tileXofPixel(this.x + num) - num;
						this.xTempRight = TileMap.tileXofPixel(this.x + num);
						if (this.x > this.xTempLeft && this.x < this.xTempRight && this.xTempRight != -1)
						{
							this.x = this.xTempLeft;
						}
					}
					if (this.y < this.yTemp && this.yTemp != -1)
					{
						this.yTemp = this.y;
						this.x += num;
					}
					if (this.y > this.yTemp)
					{
						this.yTemp = this.y;
						this.x -= num;
					}
					Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				g.translate(0, -GameCanvas.transY);
				if (this.hp <= 0L)
				{
					return;
				}
				int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
				int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
				int num2 = imageWidth;
				int num3 = this.x - imageWidth;
				int num4 = this.y - this.h - 5;
				int num5 = imageWidth * 2 * this.per / 100;
				int num6 = num5;
				if (this.per_tem >= this.per)
				{
					int num8 = imageWidth;
					int per_tem = this.per_tem;
					int num10;
					if (GameCanvas.gameTick % 6 > 3)
					{
						int num9 = this.offset;
						this.offset = num9 + 1;
						num10 = num9;
					}
					else
					{
						num10 = this.offset;
					}
					num6 = num8 * (this.per_tem = per_tem - num10) / 100;
					if (this.per_tem <= 0)
					{
						this.per_tem = 0;
					}
					if (this.per_tem < this.per)
					{
						this.per_tem = this.per;
					}
					if (this.offset >= 3)
					{
						this.offset = 3;
					}
				}
				int num7;
				if (num5 > num2)
				{
					num7 = num5 - num2;
					if (num7 <= 0)
					{
						num7 = 0;
					}
				}
				else
				{
					num2 = num5;
					num7 = 0;
				}
				g.drawImage(GameScr.imgHP_tm_xam, num3, num4, mGraphics.TOP | mGraphics.LEFT);
				g.drawImage(GameScr.imgHP_tm_xam, num3 + imageWidth, num4, mGraphics.TOP | mGraphics.LEFT);
				g.setColor(16777215);
				g.fillRect(num3, num4, num6, 2);
				g.drawRegion(this.imgHPtem, 0, 0, num2, imageHeight, 0, num3, num4, mGraphics.TOP | mGraphics.LEFT);
				g.drawRegion(this.imgHPtem, 0, 0, num7, imageHeight, 0, num3 + imageWidth, num4, mGraphics.TOP | mGraphics.LEFT);
				return;
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00003A2C File Offset: 0x00001C2C
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000509B4 File Offset: 0x0004EBB4
		public new int getY()
		{
			return this.y;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000509BC File Offset: 0x0004EBBC
		public new int getH()
		{
			return this.h;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000509C4 File Offset: 0x0004EBC4
		public new int getW()
		{
			return this.w;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0005C884 File Offset: 0x0005AA84
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00003A82 File Offset: 0x00001C82
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0005C8C4 File Offset: 0x0005AAC4
		public new void move(short xMoveTo, short yMoveTo)
		{
			if (yMoveTo == -1)
			{
				this.xTo = (int)xMoveTo;
				this.status = 5;
				return;
			}
			if (Res.distance(this.x, this.y, this.xTo, this.yTo) > 100)
			{
				this.x = (int)xMoveTo;
				this.y = (int)yMoveTo;
				this.status = 2;
				return;
			}
			this.xTo = (int)xMoveTo;
			this.yTo = (int)yMoveTo;
			this.status = 5;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0005C930 File Offset: 0x0005AB30
		public new void GetFrame()
		{
			try
			{
				this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId.ToString() + string.Empty);
				this.w = Mob.arrMobTemplate[this.templateId].data.width;
				this.h = Mob.arrMobTemplate[this.templateId].data.height;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0005C9B4 File Offset: 0x0005ABB4
		public void setDie()
		{
			this.status = 0;
		}

		// Token: 0x04000CF0 RID: 3312
		public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

		// Token: 0x04000CF1 RID: 3313
		public int xTo;

		// Token: 0x04000CF2 RID: 3314
		public int yTo;

		// Token: 0x04000CF3 RID: 3315
		public new int xSd;

		// Token: 0x04000CF4 RID: 3316
		public new int ySd;

		// Token: 0x04000CF5 RID: 3317
		public new bool isShadown = true;

		// Token: 0x04000CF6 RID: 3318
		private int tick;

		// Token: 0x04000CF7 RID: 3319
		private int frame;

		// Token: 0x04000CF8 RID: 3320
		public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04000CF9 RID: 3321
		private int fy;

		// Token: 0x04000CFA RID: 3322
		private bool flyUp;

		// Token: 0x04000CFB RID: 3323
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04000CFC RID: 3324
		private Char[] charAttack;

		// Token: 0x04000CFD RID: 3325
		private int[] dameHP;

		// Token: 0x04000CFE RID: 3326
		private sbyte type;

		// Token: 0x04000CFF RID: 3327
		private int offset;

		// Token: 0x04000D00 RID: 3328
		private int xTempRight = -1;

		// Token: 0x04000D01 RID: 3329
		private int xTempLeft = -1;

		// Token: 0x04000D02 RID: 3330
		private int yTemp = -1;

		// Token: 0x04000D03 RID: 3331
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04000D04 RID: 3332
		public new int forceWait;

		// Token: 0x04000D05 RID: 3333
		private int[][] frameArr = new int[][]
		{
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1
			}
		};
	}
}
