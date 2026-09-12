using System;

namespace Game3
{
	// Token: 0x0200030E RID: 782
	public class NewBoss : Mob, IMapObject
	{
		// Token: 0x06002285 RID: 8837 RVA: 0x0021AE7C File Offset: 0x0021907C
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

		// Token: 0x06002286 RID: 8838 RVA: 0x001C24AC File Offset: 0x001C06AC
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x001C24BC File Offset: 0x001C06BC
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x0021B0DB File Offset: 0x002192DB
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0021B110 File Offset: 0x00219310
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

		// Token: 0x0600228A RID: 8842 RVA: 0x0021B1A8 File Offset: 0x002193A8
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

		// Token: 0x0600228B RID: 8843 RVA: 0x0021B328 File Offset: 0x00219528
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

		// Token: 0x0600228C RID: 8844 RVA: 0x0021B424 File Offset: 0x00219624
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

		// Token: 0x0600228D RID: 8845 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateMobFly()
		{
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0021B4D4 File Offset: 0x002196D4
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.frameArr[0]);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0021B543 File Offset: 0x00219743
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0021B554 File Offset: 0x00219754
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

		// Token: 0x06002292 RID: 8850 RVA: 0x0021B5DC File Offset: 0x002197DC
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

		// Token: 0x06002293 RID: 8851 RVA: 0x0021B69C File Offset: 0x0021989C
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

		// Token: 0x06002294 RID: 8852 RVA: 0x001C2AB5 File Offset: 0x001C0CB5
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0021B774 File Offset: 0x00219974
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

		// Token: 0x06002296 RID: 8854 RVA: 0x001C2D64 File Offset: 0x001C0F64
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0020FC78 File Offset: 0x0020DE78
		public new int getY()
		{
			return this.y;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0020FC80 File Offset: 0x0020DE80
		public new int getH()
		{
			return this.h;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0020FC88 File Offset: 0x0020DE88
		public new int getW()
		{
			return this.w;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0021BB34 File Offset: 0x00219D34
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x001C2DB6 File Offset: 0x001C0FB6
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0021BB74 File Offset: 0x00219D74
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

		// Token: 0x0600229D RID: 8861 RVA: 0x0021BBE0 File Offset: 0x00219DE0
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

		// Token: 0x0600229E RID: 8862 RVA: 0x0021BC64 File Offset: 0x00219E64
		public void setDie()
		{
			this.status = 0;
		}

		// Token: 0x0400446D RID: 17517
		public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

		// Token: 0x0400446E RID: 17518
		public int xTo;

		// Token: 0x0400446F RID: 17519
		public int yTo;

		// Token: 0x04004470 RID: 17520
		public new int xSd;

		// Token: 0x04004471 RID: 17521
		public new int ySd;

		// Token: 0x04004472 RID: 17522
		public new bool isShadown = true;

		// Token: 0x04004473 RID: 17523
		private int tick;

		// Token: 0x04004474 RID: 17524
		private int frame;

		// Token: 0x04004475 RID: 17525
		public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04004476 RID: 17526
		private int fy;

		// Token: 0x04004477 RID: 17527
		private bool flyUp;

		// Token: 0x04004478 RID: 17528
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04004479 RID: 17529
		private Char[] charAttack;

		// Token: 0x0400447A RID: 17530
		private int[] dameHP;

		// Token: 0x0400447B RID: 17531
		private sbyte type;

		// Token: 0x0400447C RID: 17532
		private int offset;

		// Token: 0x0400447D RID: 17533
		private int xTempRight = -1;

		// Token: 0x0400447E RID: 17534
		private int xTempLeft = -1;

		// Token: 0x0400447F RID: 17535
		private int yTemp = -1;

		// Token: 0x04004480 RID: 17536
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04004481 RID: 17537
		public new int forceWait;

		// Token: 0x04004482 RID: 17538
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
