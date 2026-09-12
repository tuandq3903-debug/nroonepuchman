using System;
using Game3.Assets.src.g;

namespace Game3
{
	// Token: 0x020002F4 RID: 756
	public class Mob : IMapObject
	{
		// Token: 0x0600215A RID: 8538 RVA: 0x0020D490 File Offset: 0x0020B690
		public Mob()
		{
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0020D61C File Offset: 0x0020B81C
		public Mob(int mobId, bool isDisable, bool isDontMove, bool isFire, bool isIce, bool isWind, int templateId, int sys, long hp, sbyte level, long maxp, short pointx, short pointy, sbyte status, sbyte levelBoss)
		{
			this.isDisable = isDisable;
			this.isDontMove = isDontMove;
			this.isFire = isFire;
			this.isIce = isIce;
			this.isWind = isWind;
			this.sys = sys;
			this.mobId = mobId;
			this.templateId = templateId;
			this.hp = hp;
			this.level = level;
			this.pointx = pointx;
			this.x = (int)pointx;
			this.xFirst = (int)pointx;
			this.pointy = pointy;
			this.y = (int)pointy;
			this.yFirst = (int)pointy;
			this.status = (int)status;
			if (templateId != 70)
			{
				this.checkData();
				this.getData();
			}
			if (!Mob.isExistNewMob(templateId.ToString() + string.Empty))
			{
				Mob.newMob.addElement(templateId.ToString() + string.Empty);
			}
			this.maxHp = maxp;
			this.levelBoss = levelBoss;
			this.updateHp_bar();
			this.per_tem = (int)(hp * 100L / this.maxHp);
			this.isDie = false;
			this.xSd = (int)pointx;
			this.ySd = (int)pointy;
			if (this.isNewModStand())
			{
				this.stand = new int[]
				{
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
					2,
					2,
					2,
					2,
					2,
					2,
					2
				};
				this.move = new int[]
				{
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
					2,
					2,
					2,
					2,
					2,
					2,
					2
				};
				this.moveFast = new int[]
				{
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
					2,
					2,
					2,
					2,
					2,
					2,
					2
				};
				this.attack1 = new int[]
				{
					3,
					3,
					3,
					3,
					4,
					4,
					4,
					4,
					5,
					5,
					5,
					5
				};
				this.attack2 = new int[]
				{
					3,
					3,
					3,
					3,
					4,
					4,
					4,
					4,
					5,
					5,
					5,
					5
				};
				return;
			}
			if (this.isNewMod())
			{
				this.stand = new int[]
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
				this.move = new int[]
				{
					1,
					1,
					1,
					1,
					2,
					2,
					2,
					2,
					1,
					1,
					1,
					1,
					3,
					3,
					3,
					3
				};
				this.moveFast = new int[]
				{
					1,
					1,
					2,
					2,
					1,
					1,
					3,
					3
				};
				this.attack1 = new int[]
				{
					4,
					4,
					4,
					5,
					5,
					5,
					6,
					6,
					6,
					6,
					6
				};
				this.attack2 = new int[]
				{
					7,
					7,
					7,
					8,
					8,
					8,
					9,
					9,
					9,
					9,
					9
				};
				return;
			}
			if (this.isSpecial())
			{
				this.stand = new int[]
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
				this.move = new int[]
				{
					2,
					2,
					3,
					3,
					2,
					2,
					4,
					4,
					2,
					2,
					3,
					3,
					2,
					2,
					4,
					4
				};
				this.moveFast = new int[]
				{
					2,
					2,
					3,
					3,
					2,
					2,
					4,
					4
				};
				this.attack1 = new int[]
				{
					5,
					6,
					7,
					8,
					9,
					10,
					11,
					12
				};
				this.attack2 = new int[]
				{
					5,
					12,
					13,
					14
				};
				return;
			}
			this.stand = new int[]
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
			this.move = new int[]
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
			this.moveFast = new int[]
			{
				1,
				1,
				2,
				2,
				3,
				3,
				2
			};
			this.attack1 = new int[]
			{
				4,
				5,
				6
			};
			this.attack2 = new int[]
			{
				7,
				8,
				9
			};
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0020DAAB File Offset: 0x0020BCAB
		public bool isBigBoss()
		{
			return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0020DAD0 File Offset: 0x0020BCD0
		public void getData()
		{
			if (Mob.arrMobTemplate[this.templateId].data == null)
			{
				Mob.arrMobTemplate[this.templateId].data = new EffectData();
				string text = "/Mob/" + this.templateId.ToString();
				if (MyStream.readFile(text) != null)
				{
					Mob.arrMobTemplate[this.templateId].data.readData(text + "/data");
					Mob.arrMobTemplate[this.templateId].data.img = GameCanvas.loadImage(text + "/img.png");
				}
				else
				{
					Service.gI().requestModTemplate(this.templateId);
				}
				if (Mob.lastMob.size() > 15)
				{
					Mob.arrMobTemplate[int.Parse((string)Mob.lastMob.elementAt(0))].data = null;
					Mob.lastMob.removeElementAt(0);
				}
				Mob.lastMob.addElement(this.templateId.ToString() + string.Empty);
				return;
			}
			this.w = Mob.arrMobTemplate[this.templateId].data.width;
			this.h = Mob.arrMobTemplate[this.templateId].data.height;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x001C24AC File Offset: 0x001C06AC
		public virtual void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x001C24BC File Offset: 0x001C06BC
		public virtual void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0020DC14 File Offset: 0x0020BE14
		public static bool isExistNewMob(string id)
		{
			for (int i = 0; i < Mob.newMob.size(); i++)
			{
				if (((string)Mob.newMob.elementAt(i)).Equals(id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0020DC54 File Offset: 0x0020BE54
		public void checkData()
		{
			int num = 0;
			for (int i = 0; i < Mob.arrMobTemplate.Length; i++)
			{
				if (Mob.arrMobTemplate[i].data != null)
				{
					num++;
				}
			}
			if (num < 10)
			{
				return;
			}
			for (int j = 0; j < Mob.arrMobTemplate.Length; j++)
			{
				if (Mob.arrMobTemplate[j].data != null && num > 5)
				{
					Mob.arrMobTemplate[j].data = null;
				}
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0020DCBE File Offset: 0x0020BEBE
		public void checkFrameTick(int[] array)
		{
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
			this.tick++;
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0020DCF0 File Offset: 0x0020BEF0
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

		// Token: 0x06002164 RID: 8548 RVA: 0x0020DE0C File Offset: 0x0020C00C
		private void paintShadow(mGraphics g)
		{
			int num = (int)TileMap.size;
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
			g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0020DF58 File Offset: 0x0020C158
		public void updateSuperEff()
		{
			if (this.typeSuperEff == 0 && GameCanvas.gameTick % 25 == 0)
			{
				ServerEffect.addServerEffect(114, this, 1);
			}
			if (this.typeSuperEff == 1 && GameCanvas.gameTick % 4 == 0)
			{
				ServerEffect.addServerEffect(132, this, 1);
			}
			if (this.typeSuperEff == 2 && GameCanvas.gameTick % 7 == 0)
			{
				ServerEffect.addServerEffect(131, this, 1);
			}
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0020DFBC File Offset: 0x0020C1BC
		public virtual void update()
		{
			if (this.isMafuba)
			{
				return;
			}
			this.GetFrame();
			if (this.blindEff && GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(113, this.x, this.y, 1);
			}
			if (this.sleepEff && GameCanvas.gameTick % 10 == 0)
			{
				EffecMn.addEff(new Effect(41, this.x, this.y, 3, 1, 1));
			}
			if (!GameCanvas.lowGraphic && this.status != 1 && this.status != 0 && !GameCanvas.lowGraphic && GameCanvas.gameTick % (15 + this.mobId * 2) == 0)
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					Char @char = (Char)GameScr.vCharInMap.elementAt(i);
					if (@char != null && @char.isFlyAndCharge && @char.cf == 32)
					{
						Char char2 = new Char();
						char2.cx = @char.cx;
						char2.cy = @char.cy - @char.ch;
						if (@char.cgender == 0)
						{
							MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100L, -100L, char2, 25);
						}
					}
				}
				if (Char.myCharz().isFlyAndCharge && Char.myCharz().cf == 32)
				{
					Char char3 = new Char();
					char3.cx = Char.myCharz().cx;
					char3.cy = Char.myCharz().cy - Char.myCharz().ch;
					if (Char.myCharz().cgender == 0)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100L, -100L, char3, 25);
					}
				}
			}
			if (this.holdEffID != 0 && GameCanvas.gameTick % 5 == 0)
			{
				EffecMn.addEff(new Effect(this.holdEffID, this.x, this.y + 24, 3, 5, 1));
			}
			if (this.isFreez)
			{
				if (GameCanvas.gameTick % 5 == 0)
				{
					ServerEffect.addServerEffect(113, this.x, this.y, 1);
				}
				long num = mSystem.currentTimeMillis();
				if (num - this.last >= 1000L)
				{
					this.seconds--;
					this.last = num;
					if (this.seconds < 0)
					{
						this.isFreez = false;
						this.seconds = 0;
					}
				}
				if (this.isTypeNewMod())
				{
					this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
				}
				else if (this.isNewModStand())
				{
					this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
				}
				else if (this.isNewMod())
				{
					if (GameCanvas.gameTick % 20 > 5)
					{
						this.frame = 11;
					}
					else
					{
						this.frame = 10;
					}
				}
				else if (this.isSpecial())
				{
					if (GameCanvas.gameTick % 20 > 5)
					{
						this.frame = 1;
					}
					else
					{
						this.frame = 15;
					}
				}
				else if (GameCanvas.gameTick % 20 > 5)
				{
					this.frame = 11;
				}
				else
				{
					this.frame = 10;
				}
			}
			if (!this.isUpdate())
			{
				return;
			}
			if (this.isShadown)
			{
				this.updateShadown();
			}
			if (this.vMobMove == null && Mob.arrMobTemplate[this.templateId].rangeMove != 0)
			{
				return;
			}
			if (this.status != 3 && this.isBusyAttackSomeOne)
			{
				if (this.cFocus != null)
				{
					this.cFocus.doInjure(this.dame, this.dameMp, false, true);
				}
				else if (this.mobToAttack != null)
				{
					this.mobToAttack.setInjure();
				}
				this.isBusyAttackSomeOne = false;
			}
			if (this.levelBoss > 0)
			{
				this.updateSuperEff();
			}
			switch (this.status)
			{
			case 1:
				this.isDisable = false;
				this.isDontMove = false;
				this.isFire = false;
				this.isIce = false;
				this.isWind = false;
				this.y += this.p1;
				if (GameCanvas.gameTick % 2 == 0)
				{
					if (this.p2 > 1)
					{
						this.p2--;
					}
					else if (this.p2 < -1)
					{
						this.p2++;
					}
				}
				this.x += this.p2;
				if (this.isTypeNewMod())
				{
					this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
				}
				else if (this.isNewModStand())
				{
					this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
				}
				else if (this.isNewMod())
				{
					this.frame = 11;
				}
				else if (this.isSpecial())
				{
					this.frame = 15;
				}
				else
				{
					this.frame = 11;
				}
				if (this.isDie)
				{
					this.isDie = false;
					if (this.isMobMe)
					{
						for (int j = 0; j < GameScr.vMob.size(); j++)
						{
							if (((Mob)GameScr.vMob.elementAt(j)).mobId == this.mobId)
							{
								GameScr.vMob.removeElementAt(j);
							}
						}
					}
					this.p1 = 0;
					this.p2 = 0;
					this.x = (this.y = 0);
					this.hp = this.getTemplate().hp;
					this.status = 0;
					this.timeStatus = 0;
					return;
				}
				if ((TileMap.tileTypeAtPixel(this.x, this.y) & 2) == 2)
				{
					this.p1 = ((this.p1 <= 4) ? (-this.p1) : -4);
					if (this.p3 == 0)
					{
						this.p3 = 16;
					}
				}
				else
				{
					this.p1++;
				}
				if (this.p3 > 0)
				{
					this.p3--;
					if (this.p3 == 0)
					{
						this.isDie = true;
						return;
					}
				}
				break;
			case 2:
				if (this.holdEffID == 0 && !this.isFreez && !this.blindEff && !this.sleepEff)
				{
					this.timeStatus = 0;
					this.updateMobStandWait();
					return;
				}
				break;
			case 3:
				if (this.holdEffID == 0 && !this.blindEff && !this.sleepEff && !this.isFreez)
				{
					this.updateMobAttack();
					return;
				}
				break;
			case 4:
				if (this.holdEffID == 0 && !this.blindEff && !this.sleepEff && !this.isFreez)
				{
					this.timeStatus = 0;
					this.p1++;
					if (this.p1 > 40 + this.mobId % 5)
					{
						this.y -= 2;
						this.status = 5;
						this.p1 = 0;
						return;
					}
				}
				break;
			case 5:
				if (this.holdEffID == 0 && !this.blindEff && !this.sleepEff)
				{
					if (!this.isFreez)
					{
						this.timeStatus = 0;
						this.updateMobWalk();
						return;
					}
					if (Mob.arrMobTemplate[this.templateId].type == 4)
					{
						this.ty++;
						this.wt++;
						this.fy += ((!this.wy) ? 1 : -1);
						if (this.wt == 10)
						{
							this.wt = 0;
							this.wy = !this.wy;
							return;
						}
					}
				}
				break;
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
				break;
			default:
				return;
			}
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0020E790 File Offset: 0x0020C990
		public void setInjure()
		{
			if (this.hp > 0L && this.status != 3 && this.status != 7)
			{
				this.timeStatus = 4;
				this.status = 7;
				if (this.getTemplate().type != 0 && Res.abs(this.x - this.xFirst) < 30)
				{
					this.x -= 10 * this.dir;
				}
			}
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0020E800 File Offset: 0x0020CA00
		public static BigBoss getBigBoss()
		{
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (mob is BigBoss)
				{
					return (BigBoss)mob;
				}
			}
			return null;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0020E844 File Offset: 0x0020CA44
		public static BigBoss2 getBigBoss2()
		{
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (mob is BigBoss2)
				{
					return (BigBoss2)mob;
				}
			}
			return null;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0020E888 File Offset: 0x0020CA88
		public static BachTuoc getBachTuoc()
		{
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (mob is BachTuoc)
				{
					return (BachTuoc)mob;
				}
			}
			return null;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0020E8CC File Offset: 0x0020CACC
		public static NewBoss getNewBoss(sbyte idBoss)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
			if (mob is NewBoss)
			{
				return (NewBoss)mob;
			}
			return null;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0020E8FC File Offset: 0x0020CAFC
		public void setAttack(Char cFocus)
		{
			this.isBusyAttackSomeOne = true;
			this.mobToAttack = null;
			this.cFocus = cFocus;
			this.p1 = 0;
			this.p2 = 0;
			this.status = 3;
			this.tick = 0;
			this.dir = ((cFocus.cx > this.x) ? 1 : -1);
			int cx = cFocus.cx;
			int cy = cFocus.cy;
			if (Res.abs(cx - this.x) < this.w * 2 && Res.abs(cy - this.y) < this.h * 2)
			{
				this.p3 = 0;
				return;
			}
			this.p3 = 1;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0020E99B File Offset: 0x0020CB9B
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0020E9C8 File Offset: 0x0020CBC8
		private bool isNewModStand()
		{
			return this.templateId == 76;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0020E9D4 File Offset: 0x0020CBD4
		private bool isNewMod()
		{
			return this.templateId >= 73 && !this.isNewModStand();
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0020E9EC File Offset: 0x0020CBEC
		private void updateInjure()
		{
			if (!this.isBusyAttackSomeOne && GameCanvas.gameTick % 4 == 0)
			{
				if (this.isTypeNewMod())
				{
					this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
				}
				else if (this.isNewModStand())
				{
					this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
				}
				else if (this.isNewMod())
				{
					if (this.frame != 10)
					{
						this.frame = 10;
					}
					else
					{
						this.frame = 11;
					}
				}
				else if (this.isSpecial())
				{
					if (this.frame != 1)
					{
						this.frame = 1;
					}
					else
					{
						this.frame = 15;
					}
				}
				else if (this.frame != 10)
				{
					this.frame = 10;
				}
				else
				{
					this.frame = 11;
				}
			}
			this.timeStatus--;
			if (this.timeStatus <= 0 && (this.isTypeNewMod() || this.isNewModStand() || (this.isNewMod() && this.frame == 11) || (this.isSpecial() && this.frame == 15) || (this.templateId < 58 && this.frame == 11)))
			{
				if ((this.injureBy != null && this.injureThenDie) || this.hp == 0L)
				{
					this.status = 1;
					this.p2 = this.injureBy.cdir << 1;
					this.p1 = -3;
					this.p3 = 0;
				}
				else
				{
					this.status = 5;
					if (this.injureBy != null)
					{
						this.dir = -this.injureBy.cdir;
						if (Res.abs(this.x - this.injureBy.cx) < 24)
						{
							this.status = 2;
						}
					}
					this.p1 = (this.p2 = (this.p3 = 0));
					this.timeStatus = 0;
				}
				this.injureBy = null;
				return;
			}
			if (Mob.arrMobTemplate[this.templateId].type != 0 && this.injureBy != null)
			{
				int num = -this.injureBy.cdir << 1;
				if (this.x > this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove && this.x < this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
				{
					this.x -= num;
				}
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0020EC54 File Offset: 0x0020CE54
		private void updateMobStandWait()
		{
			this.checkFrameTick(this.stand);
			sbyte type = Mob.arrMobTemplate[this.templateId].type;
			if (type > 3)
			{
				if (type - 4 <= 1)
				{
					this.p1++;
					if (this.p1 > this.mobId % 3 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
					{
						this.status = 5;
					}
				}
			}
			else
			{
				this.p1++;
				if (this.p1 > 10 + this.mobId % 10 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
				{
					this.status = 5;
				}
			}
			if (this.cFocus != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
			{
				if (this.cFocus.cx > this.x)
				{
					this.dir = 1;
				}
				else
				{
					this.dir = -1;
				}
			}
			else if (this.mobToAttack != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
			{
				if (this.mobToAttack.x > this.x)
				{
					this.dir = 1;
				}
				else
				{
					this.dir = -1;
				}
			}
			if (this.forceWait > 0)
			{
				this.forceWait--;
				this.status = 2;
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0020EE10 File Offset: 0x0020D010
		public void updateMobAttack()
		{
			int[] array = (this.p3 != 0) ? this.attack2 : this.attack1;
			if (this.tick < array.Length)
			{
				this.checkFrameTick(array);
				if (this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w && this.p3 == 0 && GameCanvas.gameTick % 2 == 0)
				{
					SoundMn.gI().charPunch(false, 0.05f);
				}
			}
			if (this.p1 == 0)
			{
				int num = (this.cFocus == null) ? this.mobToAttack.x : this.cFocus.cx;
				int num2 = (this.cFocus == null) ? this.mobToAttack.y : this.cFocus.cy;
				if (!this.isNewMod())
				{
					if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
					{
						this.p1 = 1;
					}
					if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
					{
						this.p1 = 1;
					}
				}
				if ((Mob.arrMobTemplate[this.templateId].type == 4 || Mob.arrMobTemplate[this.templateId].type == 5) && !this.isDontMove)
				{
					this.y += (num2 - this.y) / 20;
				}
				this.p2++;
				if (this.p2 > array.Length - 1 || this.p1 == 1)
				{
					this.p1 = 1;
					if (this.p3 == 0)
					{
						if (this.cFocus != null)
						{
							this.cFocus.doInjure(this.dame, this.dameMp, false, true);
						}
						else
						{
							this.mobToAttack.setInjure();
						}
						this.isBusyAttackSomeOne = false;
					}
					else
					{
						if (this.cFocus != null)
						{
							MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, this.cFocus, (int)this.getTemplate().dartType);
						}
						else
						{
							Char @char = new Char();
							@char.cx = this.mobToAttack.x;
							@char.cy = this.mobToAttack.y;
							@char.charID = -100;
							MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, @char, (int)this.getTemplate().dartType);
						}
						this.isBusyAttackSomeOne = false;
					}
				}
				this.dir = ((this.x < num) ? 1 : -1);
			}
			else if (this.p1 == 1)
			{
				if (Mob.arrMobTemplate[this.templateId].type != 0 && !this.isDontMove && !this.isIce)
				{
					bool flag = this.isWind;
				}
				if (this.tick == array.Length)
				{
					this.status = 2;
					this.p1 = 0;
					this.p2 = 0;
					this.tick = 0;
				}
			}
			if (this.tick == 5 && this.cFocus != null && this.cFocus.charID == Char.myCharz().charID)
			{
				if (this.templateId == 88 && this.p3 != 0)
				{
					GameScr.shock_scr = 2;
				}
				if (this.templateId == 89)
				{
					GameScr.shock_scr = 2;
				}
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0020F168 File Offset: 0x0020D368
		public void updateMobWalk()
		{
			int num = 0;
			try
			{
				if (this.injureThenDie)
				{
					this.status = 1;
					this.p2 = this.injureBy.cdir << 3;
					this.p1 = -5;
					this.p3 = 0;
				}
				num = 1;
				if (!this.isIce)
				{
					if (this.isDontMove || this.isWind)
					{
						this.checkFrameTick(this.stand);
					}
					else
					{
						switch (Mob.arrMobTemplate[this.templateId].type)
						{
						case 0:
							if (this.isNewModStand())
							{
								this.frame = this.stand[GameCanvas.gameTick % this.stand.Length];
							}
							else
							{
								this.frame = 0;
							}
							num = 2;
							break;
						case 1:
						case 2:
						case 3:
						{
							num = 3;
							sbyte b = Mob.arrMobTemplate[this.templateId].speed;
							if (b == 1)
							{
								if (GameCanvas.gameTick % 2 == 1)
								{
									break;
								}
							}
							else if (b > 2)
							{
								b += (sbyte)(this.mobId % 2);
							}
							else if (GameCanvas.gameTick % 2 == 1)
							{
								b -= 1;
							}
							this.x += (int)b * this.dir;
							if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
							{
								this.dir = -1;
							}
							else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
							{
								this.dir = 1;
							}
							if (Res.abs(this.x - Char.myCharz().cx) < 40 && Res.abs(this.x - this.xFirst) < (int)Mob.arrMobTemplate[this.templateId].rangeMove)
							{
								this.dir = ((this.x <= Char.myCharz().cx) ? 1 : -1);
								if (Res.abs(this.x - Char.myCharz().cx) < 20)
								{
									this.x -= this.dir * 10;
								}
								this.status = 2;
								this.forceWait = 20;
							}
							this.checkFrameTick((this.w <= 30) ? this.moveFast : this.move);
							break;
						}
						case 4:
						{
							num = 4;
							sbyte speed2 = Mob.arrMobTemplate[this.templateId].speed;
							speed2 += (sbyte)(this.mobId % 2);
							this.x += (int)speed2 * this.dir;
							if (GameCanvas.gameTick % 10 > 2)
							{
								this.y += (int)speed2 * this.dirV;
							}
							speed2 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
							if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
							{
								this.dir = -1;
								this.status = 2;
								this.forceWait = GameCanvas.gameTick % 20 + 20;
								this.p1 = 0;
							}
							else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
							{
								this.dir = 1;
								this.status = 2;
								this.forceWait = GameCanvas.gameTick % 20 + 20;
								this.p1 = 0;
							}
							if (this.y > this.yFirst + 24)
							{
								this.dirV = -1;
							}
							else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
							{
								this.dirV = 1;
							}
							this.checkFrameTick(this.move);
							break;
						}
						case 5:
						{
							num = 5;
							sbyte speed3 = Mob.arrMobTemplate[this.templateId].speed;
							speed3 += (sbyte)(this.mobId % 2);
							this.x += (int)speed3 * this.dir;
							speed3 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
							if (GameCanvas.gameTick % 10 > 2)
							{
								this.y += (int)speed3 * this.dirV;
							}
							if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
							{
								this.dir = -1;
								this.status = 2;
								this.forceWait = GameCanvas.gameTick % 20 + 20;
								this.p1 = 0;
							}
							else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
							{
								this.dir = 1;
								this.status = 2;
								this.forceWait = GameCanvas.gameTick % 20 + 20;
								this.p1 = 0;
							}
							if (this.y > this.yFirst + 24)
							{
								this.dirV = -1;
							}
							else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
							{
								this.dirV = 1;
							}
							if (TileMap.tileTypeAt(this.x, this.y, 2))
							{
								if (GameCanvas.gameTick % 10 > 5)
								{
									this.y = TileMap.tileYofPixel(this.y);
									this.status = 4;
									this.p1 = 0;
									this.dirV = -1;
								}
								else
								{
									this.dirV = -1;
								}
							}
							break;
						}
						}
					}
				}
			}
			catch (Exception)
			{
				Cout.println("lineee: " + num.ToString());
			}
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0020F6B0 File Offset: 0x0020D8B0
		public MobTemplate getTemplate()
		{
			return Mob.arrMobTemplate[this.templateId];
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0020F6C0 File Offset: 0x0020D8C0
		public bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0020F762 File Offset: 0x0020D962
		public bool isUpdate()
		{
			return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0020F794 File Offset: 0x0020D994
		public bool checkIsBoss()
		{
			return this.isBoss || this.levelBoss > 0;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0020F7AC File Offset: 0x0020D9AC
		public void updateHp_bar()
		{
			this.len = (int)(this.hp * 100L / this.maxHp * (long)this.w_hp_bar) / 100;
			this.per = (int)(this.hp * 100L / this.maxHp);
			if (this.per == 100)
			{
				this.per_tem = this.per;
			}
			if (this.per >= 100)
			{
				this.per_tem = this.per;
			}
			this.offset = 0;
			if (this.per < 30)
			{
				this.color = 15473700;
				this.imgHPtem = GameScr.imgHP_tm_do;
				return;
			}
			if (this.per < 60)
			{
				this.color = 16744448;
				this.imgHPtem = GameScr.imgHP_tm_vang;
				return;
			}
			this.color = 11992374;
			this.imgHPtem = GameScr.imgHP_tm_xanh;
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0020F880 File Offset: 0x0020DA80
		public virtual void paint(mGraphics g)
		{
			if (this.isHide)
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
				if (this.isShadown && this.status != 0)
				{
					this.paintShadow(g);
				}
				if (!this.isPaint() || (this.status == 1 && this.p3 > 0 && GameCanvas.gameTick % 3 == 0))
				{
					return;
				}
				g.translate(0, GameCanvas.transY);
				if (!this.changBody)
				{
					Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 9, (this.dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				g.translate(0, -GameCanvas.transY);
				if (Char.myCharz().mobFocus == null || !Char.myCharz().mobFocus.Equals(this) || this.status == 1 || this.hp <= 0L || this.imgHPtem == null)
				{
					return;
				}
				int imageWidth = mGraphics.getImageWidth(this.imgHPtem);
				int imageHeight = mGraphics.getImageHeight(this.imgHPtem);
				int num = imageWidth * this.per / 100;
				int num2 = num;
				if (this.per_tem >= this.per)
				{
					int num3 = imageWidth;
					int num4 = this.per_tem;
					int num6;
					if (GameCanvas.gameTick % 6 > 3)
					{
						int num5 = this.offset;
						this.offset = num5 + 1;
						num6 = num5;
					}
					else
					{
						num6 = this.offset;
					}
					num2 = num3 * (this.per_tem = num4 - num6) / 100;
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
				g.drawImage(GameScr.imgHP_tm_xam, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
				g.setColor(16777215);
				g.fillRect(this.x - (imageWidth >> 1), this.y - this.h - 5, num2, 2);
				g.drawRegion(this.imgHPtem, 0, 0, num, imageHeight, 0, this.x - (imageWidth >> 1), this.y - this.h - 5, mGraphics.TOP | mGraphics.LEFT);
				return;
			}
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0020FB5C File Offset: 0x0020DD5C
		public void startDie()
		{
			this.hp = 0L;
			this.injureThenDie = true;
			this.hp = 0L;
			this.status = 1;
			Res.outz("MOB DIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEe");
			this.p1 = -3;
			this.p2 = -this.dir;
			this.p3 = 0;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0020FBB0 File Offset: 0x0020DDB0
		public void attackOtherMob(Mob mobToAttack)
		{
			this.mobToAttack = mobToAttack;
			this.isBusyAttackSomeOne = true;
			this.cFocus = null;
			this.p1 = 0;
			this.p2 = 0;
			this.status = 3;
			this.tick = 0;
			this.dir = ((mobToAttack.x > this.x) ? 1 : -1);
			int num = mobToAttack.x;
			int num2 = mobToAttack.y;
			if (Res.abs(num - this.x) < this.w * 2 && Res.abs(num2 - this.y) < this.h * 2)
			{
				if (this.x < num)
				{
					this.x = num - this.w;
				}
				else
				{
					this.x = num + this.w;
				}
				this.p3 = 0;
				return;
			}
			this.p3 = 1;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x001C2D64 File Offset: 0x001C0F64
		public int getX()
		{
			return this.x;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0020FC78 File Offset: 0x0020DE78
		public int getY()
		{
			return this.y;
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0020FC80 File Offset: 0x0020DE80
		public int getH()
		{
			return this.h;
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0020FC88 File Offset: 0x0020DE88
		public int getW()
		{
			return this.w;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0020FC90 File Offset: 0x0020DE90
		public void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x001C2DB6 File Offset: 0x001C0FB6
		public bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0020FCCE File Offset: 0x0020DECE
		public void removeHoldEff()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0020FCDF File Offset: 0x0020DEDF
		public void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0020FCE8 File Offset: 0x0020DEE8
		public void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0020FCF4 File Offset: 0x0020DEF4
		public void GetFrame()
		{
			if (this.isGetFr && this.isTypeNewMod() && Mob.arrMobTemplate[this.templateId].data != null)
			{
				this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId.ToString() + string.Empty);
				this.stand = this.frameArr[0];
				this.move = this.frameArr[1];
				this.moveFast = this.frameArr[2];
				this.attack1 = this.frameArr[3];
				this.attack2 = this.frameArr[4];
				this.hurt = this.frameArr[5];
				this.isGetFr = false;
			}
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0020FDB2 File Offset: 0x0020DFB2
		private bool isTypeNewMod()
		{
			return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
		}

		// Token: 0x04004111 RID: 16657
		public const sbyte TYPE_DUNG = 0;

		// Token: 0x04004112 RID: 16658
		public const sbyte TYPE_DI = 1;

		// Token: 0x04004113 RID: 16659
		public const sbyte TYPE_NHAY = 2;

		// Token: 0x04004114 RID: 16660
		public const sbyte TYPE_LET = 3;

		// Token: 0x04004115 RID: 16661
		public const sbyte TYPE_BAY = 4;

		// Token: 0x04004116 RID: 16662
		public const sbyte TYPE_BAY_DAU = 5;

		// Token: 0x04004117 RID: 16663
		public static MobTemplate[] arrMobTemplate;

		// Token: 0x04004118 RID: 16664
		public const sbyte MA_INHELL = 0;

		// Token: 0x04004119 RID: 16665
		public const sbyte MA_DEADFLY = 1;

		// Token: 0x0400411A RID: 16666
		public const sbyte MA_STANDWAIT = 2;

		// Token: 0x0400411B RID: 16667
		public const sbyte MA_ATTACK = 3;

		// Token: 0x0400411C RID: 16668
		public const sbyte MA_STANDFLY = 4;

		// Token: 0x0400411D RID: 16669
		public const sbyte MA_WALK = 5;

		// Token: 0x0400411E RID: 16670
		public const sbyte MA_FALL = 6;

		// Token: 0x0400411F RID: 16671
		public const sbyte MA_INJURE = 7;

		// Token: 0x04004120 RID: 16672
		public bool changBody;

		// Token: 0x04004121 RID: 16673
		public short smallBody;

		// Token: 0x04004122 RID: 16674
		public bool isHintFocus;

		// Token: 0x04004123 RID: 16675
		public string flystring;

		// Token: 0x04004124 RID: 16676
		public int flyx;

		// Token: 0x04004125 RID: 16677
		public int flyy;

		// Token: 0x04004126 RID: 16678
		public int flyIndex;

		// Token: 0x04004127 RID: 16679
		public bool isFreez;

		// Token: 0x04004128 RID: 16680
		public int seconds;

		// Token: 0x04004129 RID: 16681
		public long last;

		// Token: 0x0400412A RID: 16682
		public long cur;

		// Token: 0x0400412B RID: 16683
		public int holdEffID;

		// Token: 0x0400412C RID: 16684
		public long hp;

		// Token: 0x0400412D RID: 16685
		public long maxHp;

		// Token: 0x0400412E RID: 16686
		public int x;

		// Token: 0x0400412F RID: 16687
		public int y;

		// Token: 0x04004130 RID: 16688
		public int dir = 1;

		// Token: 0x04004131 RID: 16689
		public int dirV = 1;

		// Token: 0x04004132 RID: 16690
		public int status;

		// Token: 0x04004133 RID: 16691
		public int p1;

		// Token: 0x04004134 RID: 16692
		public int p2;

		// Token: 0x04004135 RID: 16693
		public int p3;

		// Token: 0x04004136 RID: 16694
		public int xFirst;

		// Token: 0x04004137 RID: 16695
		public int yFirst;

		// Token: 0x04004138 RID: 16696
		public int vy;

		// Token: 0x04004139 RID: 16697
		public int exp;

		// Token: 0x0400413A RID: 16698
		public int w;

		// Token: 0x0400413B RID: 16699
		public int h;

		// Token: 0x0400413C RID: 16700
		public long hpInjure;

		// Token: 0x0400413D RID: 16701
		public int charIndex;

		// Token: 0x0400413E RID: 16702
		public int timeStatus;

		// Token: 0x0400413F RID: 16703
		public int mobId;

		// Token: 0x04004140 RID: 16704
		public bool isx;

		// Token: 0x04004141 RID: 16705
		public bool isy;

		// Token: 0x04004142 RID: 16706
		public bool isDisable;

		// Token: 0x04004143 RID: 16707
		public bool isDontMove;

		// Token: 0x04004144 RID: 16708
		public bool isFire;

		// Token: 0x04004145 RID: 16709
		public bool isIce;

		// Token: 0x04004146 RID: 16710
		public bool isWind;

		// Token: 0x04004147 RID: 16711
		public bool isDie;

		// Token: 0x04004148 RID: 16712
		public long lastDie;

		// Token: 0x04004149 RID: 16713
		public int countDie;

		// Token: 0x0400414A RID: 16714
		public MyVector vMobMove = new MyVector();

		// Token: 0x0400414B RID: 16715
		public bool isGo;

		// Token: 0x0400414C RID: 16716
		public string mobName;

		// Token: 0x0400414D RID: 16717
		public int templateId;

		// Token: 0x0400414E RID: 16718
		public short pointx;

		// Token: 0x0400414F RID: 16719
		public short pointy;

		// Token: 0x04004150 RID: 16720
		public Char cFocus;

		// Token: 0x04004151 RID: 16721
		public long dame;

		// Token: 0x04004152 RID: 16722
		public long dameMp;

		// Token: 0x04004153 RID: 16723
		public int sys;

		// Token: 0x04004154 RID: 16724
		public sbyte levelBoss;

		// Token: 0x04004155 RID: 16725
		public sbyte level;

		// Token: 0x04004156 RID: 16726
		public bool isBoss;

		// Token: 0x04004157 RID: 16727
		public bool isMobMe;

		// Token: 0x04004158 RID: 16728
		public static MyVector lastMob = new MyVector();

		// Token: 0x04004159 RID: 16729
		public static MyVector newMob = new MyVector();

		// Token: 0x0400415A RID: 16730
		public bool isMafuba;

		// Token: 0x0400415B RID: 16731
		public int xMFB;

		// Token: 0x0400415C RID: 16732
		public int yMFB;

		// Token: 0x0400415D RID: 16733
		public int xSd;

		// Token: 0x0400415E RID: 16734
		public int ySd;

		// Token: 0x0400415F RID: 16735
		private bool isOutMap;

		// Token: 0x04004160 RID: 16736
		private int wCount;

		// Token: 0x04004161 RID: 16737
		public bool isShadown = true;

		// Token: 0x04004162 RID: 16738
		private int tick;

		// Token: 0x04004163 RID: 16739
		private int frame;

		// Token: 0x04004164 RID: 16740
		public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04004165 RID: 16741
		private bool wy;

		// Token: 0x04004166 RID: 16742
		private int wt;

		// Token: 0x04004167 RID: 16743
		private int fy;

		// Token: 0x04004168 RID: 16744
		private int ty;

		// Token: 0x04004169 RID: 16745
		public int typeSuperEff;

		// Token: 0x0400416A RID: 16746
		public bool isBusyAttackSomeOne = true;

		// Token: 0x0400416B RID: 16747
		public int[] stand = new int[]
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

		// Token: 0x0400416C RID: 16748
		public int[] move = new int[]
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

		// Token: 0x0400416D RID: 16749
		public int[] moveFast = new int[]
		{
			1,
			1,
			2,
			2,
			3,
			3,
			2
		};

		// Token: 0x0400416E RID: 16750
		public int[] attack1 = new int[]
		{
			4,
			5,
			6
		};

		// Token: 0x0400416F RID: 16751
		public int[] attack2 = new int[]
		{
			7,
			8,
			9
		};

		// Token: 0x04004170 RID: 16752
		public int[] hurt = new int[1];

		// Token: 0x04004171 RID: 16753
		private int color = 8421504;

		// Token: 0x04004172 RID: 16754
		public int len = 24;

		// Token: 0x04004173 RID: 16755
		public int w_hp_bar = 24;

		// Token: 0x04004174 RID: 16756
		public int per = 100;

		// Token: 0x04004175 RID: 16757
		public int per_tem = 100;

		// Token: 0x04004176 RID: 16758
		public byte h_hp_bar = 4;

		// Token: 0x04004177 RID: 16759
		public Image imgHPtem;

		// Token: 0x04004178 RID: 16760
		private int offset;

		// Token: 0x04004179 RID: 16761
		public bool isHide;

		// Token: 0x0400417A RID: 16762
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x0400417B RID: 16763
		public Char injureBy;

		// Token: 0x0400417C RID: 16764
		public bool injureThenDie;

		// Token: 0x0400417D RID: 16765
		public Mob mobToAttack;

		// Token: 0x0400417E RID: 16766
		public int forceWait;

		// Token: 0x0400417F RID: 16767
		public bool blindEff;

		// Token: 0x04004180 RID: 16768
		public bool sleepEff;

		// Token: 0x04004181 RID: 16769
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
			}
		};

		// Token: 0x04004182 RID: 16770
		private bool isGetFr = true;
	}
}
