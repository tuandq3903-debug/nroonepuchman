using System;

namespace Game4
{
	// Token: 0x02000236 RID: 566
	public class NewBoss : Mob, IMapObject
	{
		// Token: 0x060018E1 RID: 6369 RVA: 0x00185DD8 File Offset: 0x00183FD8
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

		// Token: 0x060018E2 RID: 6370 RVA: 0x0012D408 File Offset: 0x0012B608
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0012D418 File Offset: 0x0012B618
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00186037 File Offset: 0x00184237
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0018606C File Offset: 0x0018426C
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

		// Token: 0x060018E6 RID: 6374 RVA: 0x00186104 File Offset: 0x00184304
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

		// Token: 0x060018E7 RID: 6375 RVA: 0x00186284 File Offset: 0x00184484
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

		// Token: 0x060018E8 RID: 6376 RVA: 0x00186380 File Offset: 0x00184580
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

		// Token: 0x060018E9 RID: 6377 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateMobFly()
		{
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00186430 File Offset: 0x00184630
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.frameArr[0]);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0018649F File Offset: 0x0018469F
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x001864B0 File Offset: 0x001846B0
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

		// Token: 0x060018EE RID: 6382 RVA: 0x00186538 File Offset: 0x00184738
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

		// Token: 0x060018EF RID: 6383 RVA: 0x001865F8 File Offset: 0x001847F8
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

		// Token: 0x060018F0 RID: 6384 RVA: 0x0012DA11 File Offset: 0x0012BC11
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x001866D0 File Offset: 0x001848D0
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

		// Token: 0x060018F2 RID: 6386 RVA: 0x0012DCC0 File Offset: 0x0012BEC0
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0017ABD4 File Offset: 0x00178DD4
		public new int getY()
		{
			return this.y;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0017ABDC File Offset: 0x00178DDC
		public new int getH()
		{
			return this.h;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0017ABE4 File Offset: 0x00178DE4
		public new int getW()
		{
			return this.w;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00186A90 File Offset: 0x00184C90
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0012DD12 File Offset: 0x0012BF12
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00186AD0 File Offset: 0x00184CD0
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

		// Token: 0x060018F9 RID: 6393 RVA: 0x00186B3C File Offset: 0x00184D3C
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

		// Token: 0x060018FA RID: 6394 RVA: 0x00186BC0 File Offset: 0x00184DC0
		public void setDie()
		{
			this.status = 0;
		}

		// Token: 0x040031EE RID: 12782
		public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

		// Token: 0x040031EF RID: 12783
		public int xTo;

		// Token: 0x040031F0 RID: 12784
		public int yTo;

		// Token: 0x040031F1 RID: 12785
		public new int xSd;

		// Token: 0x040031F2 RID: 12786
		public new int ySd;

		// Token: 0x040031F3 RID: 12787
		public new bool isShadown = true;

		// Token: 0x040031F4 RID: 12788
		private int tick;

		// Token: 0x040031F5 RID: 12789
		private int frame;

		// Token: 0x040031F6 RID: 12790
		public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x040031F7 RID: 12791
		private int fy;

		// Token: 0x040031F8 RID: 12792
		private bool flyUp;

		// Token: 0x040031F9 RID: 12793
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x040031FA RID: 12794
		private Char[] charAttack;

		// Token: 0x040031FB RID: 12795
		private int[] dameHP;

		// Token: 0x040031FC RID: 12796
		private sbyte type;

		// Token: 0x040031FD RID: 12797
		private int offset;

		// Token: 0x040031FE RID: 12798
		private int xTempRight = -1;

		// Token: 0x040031FF RID: 12799
		private int xTempLeft = -1;

		// Token: 0x04003200 RID: 12800
		private int yTemp = -1;

		// Token: 0x04003201 RID: 12801
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04003202 RID: 12802
		public new int forceWait;

		// Token: 0x04003203 RID: 12803
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
