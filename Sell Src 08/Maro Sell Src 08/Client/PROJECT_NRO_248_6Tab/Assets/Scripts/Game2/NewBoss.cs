using System;

namespace Game2
{
	// Token: 0x020003E6 RID: 998
	public class NewBoss : Mob, IMapObject
	{
		// Token: 0x06002C29 RID: 11305 RVA: 0x002AFF20 File Offset: 0x002AE120
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

		// Token: 0x06002C2A RID: 11306 RVA: 0x00257550 File Offset: 0x00255750
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x00257560 File Offset: 0x00255760
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x002B017F File Offset: 0x002AE37F
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x002B01B4 File Offset: 0x002AE3B4
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

		// Token: 0x06002C2E RID: 11310 RVA: 0x002B024C File Offset: 0x002AE44C
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

		// Token: 0x06002C2F RID: 11311 RVA: 0x002B03CC File Offset: 0x002AE5CC
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

		// Token: 0x06002C30 RID: 11312 RVA: 0x002B04C8 File Offset: 0x002AE6C8
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

		// Token: 0x06002C31 RID: 11313 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateMobFly()
		{
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x002B0578 File Offset: 0x002AE778
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.frameArr[0]);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x002B05E7 File Offset: 0x002AE7E7
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x002B05F8 File Offset: 0x002AE7F8
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

		// Token: 0x06002C36 RID: 11318 RVA: 0x002B0680 File Offset: 0x002AE880
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

		// Token: 0x06002C37 RID: 11319 RVA: 0x002B0740 File Offset: 0x002AE940
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

		// Token: 0x06002C38 RID: 11320 RVA: 0x00257B59 File Offset: 0x00255D59
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x002B0818 File Offset: 0x002AEA18
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

		// Token: 0x06002C3A RID: 11322 RVA: 0x00257E08 File Offset: 0x00256008
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x002A4D1C File Offset: 0x002A2F1C
		public new int getY()
		{
			return this.y;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x002A4D24 File Offset: 0x002A2F24
		public new int getH()
		{
			return this.h;
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x002A4D2C File Offset: 0x002A2F2C
		public new int getW()
		{
			return this.w;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x002B0BD8 File Offset: 0x002AEDD8
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x00257E5A File Offset: 0x0025605A
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x002B0C18 File Offset: 0x002AEE18
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

		// Token: 0x06002C41 RID: 11329 RVA: 0x002B0C84 File Offset: 0x002AEE84
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

		// Token: 0x06002C42 RID: 11330 RVA: 0x002B0D08 File Offset: 0x002AEF08
		public void setDie()
		{
			this.status = 0;
		}

		// Token: 0x040056EC RID: 22252
		public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

		// Token: 0x040056ED RID: 22253
		public int xTo;

		// Token: 0x040056EE RID: 22254
		public int yTo;

		// Token: 0x040056EF RID: 22255
		public new int xSd;

		// Token: 0x040056F0 RID: 22256
		public new int ySd;

		// Token: 0x040056F1 RID: 22257
		public new bool isShadown = true;

		// Token: 0x040056F2 RID: 22258
		private int tick;

		// Token: 0x040056F3 RID: 22259
		private int frame;

		// Token: 0x040056F4 RID: 22260
		public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x040056F5 RID: 22261
		private int fy;

		// Token: 0x040056F6 RID: 22262
		private bool flyUp;

		// Token: 0x040056F7 RID: 22263
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x040056F8 RID: 22264
		private Char[] charAttack;

		// Token: 0x040056F9 RID: 22265
		private int[] dameHP;

		// Token: 0x040056FA RID: 22266
		private sbyte type;

		// Token: 0x040056FB RID: 22267
		private int offset;

		// Token: 0x040056FC RID: 22268
		private int xTempRight = -1;

		// Token: 0x040056FD RID: 22269
		private int xTempLeft = -1;

		// Token: 0x040056FE RID: 22270
		private int yTemp = -1;

		// Token: 0x040056FF RID: 22271
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04005700 RID: 22272
		public new int forceWait;

		// Token: 0x04005701 RID: 22273
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
