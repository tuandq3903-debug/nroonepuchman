using System;

namespace Game1
{
	// Token: 0x020004BE RID: 1214
	public class NewBoss : Mob, IMapObject
	{
		// Token: 0x060035CD RID: 13773 RVA: 0x00344FC4 File Offset: 0x003431C4
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

		// Token: 0x060035CE RID: 13774 RVA: 0x002EC5F4 File Offset: 0x002EA7F4
		public override void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x002EC604 File Offset: 0x002EA804
		public override void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x00345223 File Offset: 0x00343423
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x00345258 File Offset: 0x00343458
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

		// Token: 0x060035D2 RID: 13778 RVA: 0x003452F0 File Offset: 0x003434F0
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

		// Token: 0x060035D3 RID: 13779 RVA: 0x00345470 File Offset: 0x00343670
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

		// Token: 0x060035D4 RID: 13780 RVA: 0x0034556C File Offset: 0x0034376C
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

		// Token: 0x060035D5 RID: 13781 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateMobFly()
		{
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateInjure()
		{
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x0034561C File Offset: 0x0034381C
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.frameArr[0]);
			if (this.x != this.xTo || this.y != this.yTo)
			{
				this.x += (this.xTo - this.x) / 4;
				this.y += (this.yTo - this.y) / 4;
			}
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x0034568B File Offset: 0x0034388B
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x0034569C File Offset: 0x0034389C
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

		// Token: 0x060035DA RID: 13786 RVA: 0x00345724 File Offset: 0x00343924
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

		// Token: 0x060035DB RID: 13787 RVA: 0x003457E4 File Offset: 0x003439E4
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

		// Token: 0x060035DC RID: 13788 RVA: 0x002ECBFD File Offset: 0x002EADFD
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x003458BC File Offset: 0x00343ABC
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

		// Token: 0x060035DE RID: 13790 RVA: 0x002ECEAC File Offset: 0x002EB0AC
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x00339DC0 File Offset: 0x00337FC0
		public new int getY()
		{
			return this.y;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x00339DC8 File Offset: 0x00337FC8
		public new int getH()
		{
			return this.h;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x00339DD0 File Offset: 0x00337FD0
		public new int getW()
		{
			return this.w;
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x00345C7C File Offset: 0x00343E7C
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x002ECEFE File Offset: 0x002EB0FE
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x00345CBC File Offset: 0x00343EBC
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

		// Token: 0x060035E5 RID: 13797 RVA: 0x00345D28 File Offset: 0x00343F28
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

		// Token: 0x060035E6 RID: 13798 RVA: 0x00345DAC File Offset: 0x00343FAC
		public void setDie()
		{
			this.status = 0;
		}

		// Token: 0x0400696B RID: 26987
		public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

		// Token: 0x0400696C RID: 26988
		public int xTo;

		// Token: 0x0400696D RID: 26989
		public int yTo;

		// Token: 0x0400696E RID: 26990
		public new int xSd;

		// Token: 0x0400696F RID: 26991
		public new int ySd;

		// Token: 0x04006970 RID: 26992
		public new bool isShadown = true;

		// Token: 0x04006971 RID: 26993
		private int tick;

		// Token: 0x04006972 RID: 26994
		private int frame;

		// Token: 0x04006973 RID: 26995
		public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04006974 RID: 26996
		private int fy;

		// Token: 0x04006975 RID: 26997
		private bool flyUp;

		// Token: 0x04006976 RID: 26998
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04006977 RID: 26999
		private Char[] charAttack;

		// Token: 0x04006978 RID: 27000
		private int[] dameHP;

		// Token: 0x04006979 RID: 27001
		private sbyte type;

		// Token: 0x0400697A RID: 27002
		private int offset;

		// Token: 0x0400697B RID: 27003
		private int xTempRight = -1;

		// Token: 0x0400697C RID: 27004
		private int xTempLeft = -1;

		// Token: 0x0400697D RID: 27005
		private int yTemp = -1;

		// Token: 0x0400697E RID: 27006
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x0400697F RID: 27007
		public new int forceWait;

		// Token: 0x04006980 RID: 27008
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
