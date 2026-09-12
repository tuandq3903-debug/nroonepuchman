using System;
using Game5.Assets.src.g;

namespace Game5
{
	// Token: 0x02000144 RID: 324
	public class Mob : IMapObject
	{
		// Token: 0x06000E12 RID: 3602 RVA: 0x000E3348 File Offset: 0x000E1548
		public Mob()
		{
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x000E34D4 File Offset: 0x000E16D4
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

		// Token: 0x06000E14 RID: 3604 RVA: 0x000E3963 File Offset: 0x000E1B63
		public bool isBigBoss()
		{
			return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x000E3988 File Offset: 0x000E1B88
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

		// Token: 0x06000E16 RID: 3606 RVA: 0x00098364 File Offset: 0x00096564
		public virtual void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00098374 File Offset: 0x00096574
		public virtual void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x000E3ACC File Offset: 0x000E1CCC
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

		// Token: 0x06000E19 RID: 3609 RVA: 0x000E3B0C File Offset: 0x000E1D0C
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

		// Token: 0x06000E1A RID: 3610 RVA: 0x000E3B76 File Offset: 0x000E1D76
		public void checkFrameTick(int[] array)
		{
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
			this.tick++;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x000E3BA8 File Offset: 0x000E1DA8
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

		// Token: 0x06000E1C RID: 3612 RVA: 0x000E3CC4 File Offset: 0x000E1EC4
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

		// Token: 0x06000E1D RID: 3613 RVA: 0x000E3E10 File Offset: 0x000E2010
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

		// Token: 0x06000E1E RID: 3614 RVA: 0x000E3E74 File Offset: 0x000E2074
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

		// Token: 0x06000E1F RID: 3615 RVA: 0x000E4648 File Offset: 0x000E2848
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

		// Token: 0x06000E20 RID: 3616 RVA: 0x000E46B8 File Offset: 0x000E28B8
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

		// Token: 0x06000E21 RID: 3617 RVA: 0x000E46FC File Offset: 0x000E28FC
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

		// Token: 0x06000E22 RID: 3618 RVA: 0x000E4740 File Offset: 0x000E2940
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

		// Token: 0x06000E23 RID: 3619 RVA: 0x000E4784 File Offset: 0x000E2984
		public static NewBoss getNewBoss(sbyte idBoss)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
			if (mob is NewBoss)
			{
				return (NewBoss)mob;
			}
			return null;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000E47B4 File Offset: 0x000E29B4
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

		// Token: 0x06000E25 RID: 3621 RVA: 0x000E4853 File Offset: 0x000E2A53
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000E4880 File Offset: 0x000E2A80
		private bool isNewModStand()
		{
			return this.templateId == 76;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000E488C File Offset: 0x000E2A8C
		private bool isNewMod()
		{
			return this.templateId >= 73 && !this.isNewModStand();
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000E48A4 File Offset: 0x000E2AA4
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

		// Token: 0x06000E29 RID: 3625 RVA: 0x000E4B0C File Offset: 0x000E2D0C
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

		// Token: 0x06000E2A RID: 3626 RVA: 0x000E4CC8 File Offset: 0x000E2EC8
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

		// Token: 0x06000E2B RID: 3627 RVA: 0x000E5020 File Offset: 0x000E3220
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

		// Token: 0x06000E2C RID: 3628 RVA: 0x000E5568 File Offset: 0x000E3768
		public MobTemplate getTemplate()
		{
			return Mob.arrMobTemplate[this.templateId];
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000E5578 File Offset: 0x000E3778
		public bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x000E561A File Offset: 0x000E381A
		public bool isUpdate()
		{
			return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000E564C File Offset: 0x000E384C
		public bool checkIsBoss()
		{
			return this.isBoss || this.levelBoss > 0;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000E5664 File Offset: 0x000E3864
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

		// Token: 0x06000E31 RID: 3633 RVA: 0x000E5738 File Offset: 0x000E3938
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

		// Token: 0x06000E32 RID: 3634 RVA: 0x000E5A14 File Offset: 0x000E3C14
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

		// Token: 0x06000E33 RID: 3635 RVA: 0x000E5A68 File Offset: 0x000E3C68
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

		// Token: 0x06000E34 RID: 3636 RVA: 0x00098C1C File Offset: 0x00096E1C
		public int getX()
		{
			return this.x;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000E5B30 File Offset: 0x000E3D30
		public int getY()
		{
			return this.y;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000E5B38 File Offset: 0x000E3D38
		public int getH()
		{
			return this.h;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000E5B40 File Offset: 0x000E3D40
		public int getW()
		{
			return this.w;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000E5B48 File Offset: 0x000E3D48
		public void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00098C6E File Offset: 0x00096E6E
		public bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000E5B86 File Offset: 0x000E3D86
		public void removeHoldEff()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000E5B97 File Offset: 0x000E3D97
		public void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000E5BA0 File Offset: 0x000E3DA0
		public void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000E5BAC File Offset: 0x000E3DAC
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

		// Token: 0x06000E3E RID: 3646 RVA: 0x000E5C6A File Offset: 0x000E3E6A
		private bool isTypeNewMod()
		{
			return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
		}

		// Token: 0x04001C13 RID: 7187
		public const sbyte TYPE_DUNG = 0;

		// Token: 0x04001C14 RID: 7188
		public const sbyte TYPE_DI = 1;

		// Token: 0x04001C15 RID: 7189
		public const sbyte TYPE_NHAY = 2;

		// Token: 0x04001C16 RID: 7190
		public const sbyte TYPE_LET = 3;

		// Token: 0x04001C17 RID: 7191
		public const sbyte TYPE_BAY = 4;

		// Token: 0x04001C18 RID: 7192
		public const sbyte TYPE_BAY_DAU = 5;

		// Token: 0x04001C19 RID: 7193
		public static MobTemplate[] arrMobTemplate;

		// Token: 0x04001C1A RID: 7194
		public const sbyte MA_INHELL = 0;

		// Token: 0x04001C1B RID: 7195
		public const sbyte MA_DEADFLY = 1;

		// Token: 0x04001C1C RID: 7196
		public const sbyte MA_STANDWAIT = 2;

		// Token: 0x04001C1D RID: 7197
		public const sbyte MA_ATTACK = 3;

		// Token: 0x04001C1E RID: 7198
		public const sbyte MA_STANDFLY = 4;

		// Token: 0x04001C1F RID: 7199
		public const sbyte MA_WALK = 5;

		// Token: 0x04001C20 RID: 7200
		public const sbyte MA_FALL = 6;

		// Token: 0x04001C21 RID: 7201
		public const sbyte MA_INJURE = 7;

		// Token: 0x04001C22 RID: 7202
		public bool changBody;

		// Token: 0x04001C23 RID: 7203
		public short smallBody;

		// Token: 0x04001C24 RID: 7204
		public bool isHintFocus;

		// Token: 0x04001C25 RID: 7205
		public string flystring;

		// Token: 0x04001C26 RID: 7206
		public int flyx;

		// Token: 0x04001C27 RID: 7207
		public int flyy;

		// Token: 0x04001C28 RID: 7208
		public int flyIndex;

		// Token: 0x04001C29 RID: 7209
		public bool isFreez;

		// Token: 0x04001C2A RID: 7210
		public int seconds;

		// Token: 0x04001C2B RID: 7211
		public long last;

		// Token: 0x04001C2C RID: 7212
		public long cur;

		// Token: 0x04001C2D RID: 7213
		public int holdEffID;

		// Token: 0x04001C2E RID: 7214
		public long hp;

		// Token: 0x04001C2F RID: 7215
		public long maxHp;

		// Token: 0x04001C30 RID: 7216
		public int x;

		// Token: 0x04001C31 RID: 7217
		public int y;

		// Token: 0x04001C32 RID: 7218
		public int dir = 1;

		// Token: 0x04001C33 RID: 7219
		public int dirV = 1;

		// Token: 0x04001C34 RID: 7220
		public int status;

		// Token: 0x04001C35 RID: 7221
		public int p1;

		// Token: 0x04001C36 RID: 7222
		public int p2;

		// Token: 0x04001C37 RID: 7223
		public int p3;

		// Token: 0x04001C38 RID: 7224
		public int xFirst;

		// Token: 0x04001C39 RID: 7225
		public int yFirst;

		// Token: 0x04001C3A RID: 7226
		public int vy;

		// Token: 0x04001C3B RID: 7227
		public int exp;

		// Token: 0x04001C3C RID: 7228
		public int w;

		// Token: 0x04001C3D RID: 7229
		public int h;

		// Token: 0x04001C3E RID: 7230
		public long hpInjure;

		// Token: 0x04001C3F RID: 7231
		public int charIndex;

		// Token: 0x04001C40 RID: 7232
		public int timeStatus;

		// Token: 0x04001C41 RID: 7233
		public int mobId;

		// Token: 0x04001C42 RID: 7234
		public bool isx;

		// Token: 0x04001C43 RID: 7235
		public bool isy;

		// Token: 0x04001C44 RID: 7236
		public bool isDisable;

		// Token: 0x04001C45 RID: 7237
		public bool isDontMove;

		// Token: 0x04001C46 RID: 7238
		public bool isFire;

		// Token: 0x04001C47 RID: 7239
		public bool isIce;

		// Token: 0x04001C48 RID: 7240
		public bool isWind;

		// Token: 0x04001C49 RID: 7241
		public bool isDie;

		// Token: 0x04001C4A RID: 7242
		public long lastDie;

		// Token: 0x04001C4B RID: 7243
		public int countDie;

		// Token: 0x04001C4C RID: 7244
		public MyVector vMobMove = new MyVector();

		// Token: 0x04001C4D RID: 7245
		public bool isGo;

		// Token: 0x04001C4E RID: 7246
		public string mobName;

		// Token: 0x04001C4F RID: 7247
		public int templateId;

		// Token: 0x04001C50 RID: 7248
		public short pointx;

		// Token: 0x04001C51 RID: 7249
		public short pointy;

		// Token: 0x04001C52 RID: 7250
		public Char cFocus;

		// Token: 0x04001C53 RID: 7251
		public long dame;

		// Token: 0x04001C54 RID: 7252
		public long dameMp;

		// Token: 0x04001C55 RID: 7253
		public int sys;

		// Token: 0x04001C56 RID: 7254
		public sbyte levelBoss;

		// Token: 0x04001C57 RID: 7255
		public sbyte level;

		// Token: 0x04001C58 RID: 7256
		public bool isBoss;

		// Token: 0x04001C59 RID: 7257
		public bool isMobMe;

		// Token: 0x04001C5A RID: 7258
		public static MyVector lastMob = new MyVector();

		// Token: 0x04001C5B RID: 7259
		public static MyVector newMob = new MyVector();

		// Token: 0x04001C5C RID: 7260
		public bool isMafuba;

		// Token: 0x04001C5D RID: 7261
		public int xMFB;

		// Token: 0x04001C5E RID: 7262
		public int yMFB;

		// Token: 0x04001C5F RID: 7263
		public int xSd;

		// Token: 0x04001C60 RID: 7264
		public int ySd;

		// Token: 0x04001C61 RID: 7265
		private bool isOutMap;

		// Token: 0x04001C62 RID: 7266
		private int wCount;

		// Token: 0x04001C63 RID: 7267
		public bool isShadown = true;

		// Token: 0x04001C64 RID: 7268
		private int tick;

		// Token: 0x04001C65 RID: 7269
		private int frame;

		// Token: 0x04001C66 RID: 7270
		public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04001C67 RID: 7271
		private bool wy;

		// Token: 0x04001C68 RID: 7272
		private int wt;

		// Token: 0x04001C69 RID: 7273
		private int fy;

		// Token: 0x04001C6A RID: 7274
		private int ty;

		// Token: 0x04001C6B RID: 7275
		public int typeSuperEff;

		// Token: 0x04001C6C RID: 7276
		public bool isBusyAttackSomeOne = true;

		// Token: 0x04001C6D RID: 7277
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

		// Token: 0x04001C6E RID: 7278
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

		// Token: 0x04001C6F RID: 7279
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

		// Token: 0x04001C70 RID: 7280
		public int[] attack1 = new int[]
		{
			4,
			5,
			6
		};

		// Token: 0x04001C71 RID: 7281
		public int[] attack2 = new int[]
		{
			7,
			8,
			9
		};

		// Token: 0x04001C72 RID: 7282
		public int[] hurt = new int[1];

		// Token: 0x04001C73 RID: 7283
		private int color = 8421504;

		// Token: 0x04001C74 RID: 7284
		public int len = 24;

		// Token: 0x04001C75 RID: 7285
		public int w_hp_bar = 24;

		// Token: 0x04001C76 RID: 7286
		public int per = 100;

		// Token: 0x04001C77 RID: 7287
		public int per_tem = 100;

		// Token: 0x04001C78 RID: 7288
		public byte h_hp_bar = 4;

		// Token: 0x04001C79 RID: 7289
		public Image imgHPtem;

		// Token: 0x04001C7A RID: 7290
		private int offset;

		// Token: 0x04001C7B RID: 7291
		public bool isHide;

		// Token: 0x04001C7C RID: 7292
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04001C7D RID: 7293
		public Char injureBy;

		// Token: 0x04001C7E RID: 7294
		public bool injureThenDie;

		// Token: 0x04001C7F RID: 7295
		public Mob mobToAttack;

		// Token: 0x04001C80 RID: 7296
		public int forceWait;

		// Token: 0x04001C81 RID: 7297
		public bool blindEff;

		// Token: 0x04001C82 RID: 7298
		public bool sleepEff;

		// Token: 0x04001C83 RID: 7299
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

		// Token: 0x04001C84 RID: 7300
		private bool isGetFr = true;
	}
}
