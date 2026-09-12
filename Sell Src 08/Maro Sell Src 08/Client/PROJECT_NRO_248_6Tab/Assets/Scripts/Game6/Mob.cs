using System;
using Game6.Assets.src.g;

namespace Game6
{
	// Token: 0x0200006C RID: 108
	public class Mob : IMapObject
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x0004E1CC File Offset: 0x0004C3CC
		public Mob()
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0004E358 File Offset: 0x0004C558
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

		// Token: 0x06000470 RID: 1136 RVA: 0x0004E7E7 File Offset: 0x0004C9E7
		public bool isBigBoss()
		{
			return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0004E80C File Offset: 0x0004CA0C
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

		// Token: 0x06000472 RID: 1138 RVA: 0x00003174 File Offset: 0x00001374
		public virtual void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00003184 File Offset: 0x00001384
		public virtual void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0004E950 File Offset: 0x0004CB50
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

		// Token: 0x06000475 RID: 1141 RVA: 0x0004E990 File Offset: 0x0004CB90
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

		// Token: 0x06000476 RID: 1142 RVA: 0x0004E9FA File Offset: 0x0004CBFA
		public void checkFrameTick(int[] array)
		{
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
			this.tick++;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0004EA2C File Offset: 0x0004CC2C
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

		// Token: 0x06000478 RID: 1144 RVA: 0x0004EB48 File Offset: 0x0004CD48
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

		// Token: 0x06000479 RID: 1145 RVA: 0x0004EC94 File Offset: 0x0004CE94
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

		// Token: 0x0600047A RID: 1146 RVA: 0x0004ECF8 File Offset: 0x0004CEF8
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

		// Token: 0x0600047B RID: 1147 RVA: 0x0004F4CC File Offset: 0x0004D6CC
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

		// Token: 0x0600047C RID: 1148 RVA: 0x0004F53C File Offset: 0x0004D73C
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

		// Token: 0x0600047D RID: 1149 RVA: 0x0004F580 File Offset: 0x0004D780
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

		// Token: 0x0600047E RID: 1150 RVA: 0x0004F5C4 File Offset: 0x0004D7C4
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

		// Token: 0x0600047F RID: 1151 RVA: 0x0004F608 File Offset: 0x0004D808
		public static NewBoss getNewBoss(sbyte idBoss)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
			if (mob is NewBoss)
			{
				return (NewBoss)mob;
			}
			return null;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0004F638 File Offset: 0x0004D838
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

		// Token: 0x06000481 RID: 1153 RVA: 0x0004F6D7 File Offset: 0x0004D8D7
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0004F704 File Offset: 0x0004D904
		private bool isNewModStand()
		{
			return this.templateId == 76;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0004F710 File Offset: 0x0004D910
		private bool isNewMod()
		{
			return this.templateId >= 73 && !this.isNewModStand();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0004F728 File Offset: 0x0004D928
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

		// Token: 0x06000485 RID: 1157 RVA: 0x0004F990 File Offset: 0x0004DB90
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

		// Token: 0x06000486 RID: 1158 RVA: 0x0004FB4C File Offset: 0x0004DD4C
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

		// Token: 0x06000487 RID: 1159 RVA: 0x0004FEA4 File Offset: 0x0004E0A4
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

		// Token: 0x06000488 RID: 1160 RVA: 0x000503EC File Offset: 0x0004E5EC
		public MobTemplate getTemplate()
		{
			return Mob.arrMobTemplate[this.templateId];
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000503FC File Offset: 0x0004E5FC
		public bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0005049E File Offset: 0x0004E69E
		public bool isUpdate()
		{
			return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000504D0 File Offset: 0x0004E6D0
		public bool checkIsBoss()
		{
			return this.isBoss || this.levelBoss > 0;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000504E8 File Offset: 0x0004E6E8
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

		// Token: 0x0600048D RID: 1165 RVA: 0x000505BC File Offset: 0x0004E7BC
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

		// Token: 0x0600048E RID: 1166 RVA: 0x00050898 File Offset: 0x0004EA98
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

		// Token: 0x0600048F RID: 1167 RVA: 0x000508EC File Offset: 0x0004EAEC
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

		// Token: 0x06000490 RID: 1168 RVA: 0x00003A2C File Offset: 0x00001C2C
		public int getX()
		{
			return this.x;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000509B4 File Offset: 0x0004EBB4
		public int getY()
		{
			return this.y;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000509BC File Offset: 0x0004EBBC
		public int getH()
		{
			return this.h;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000509C4 File Offset: 0x0004EBC4
		public int getW()
		{
			return this.w;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000509CC File Offset: 0x0004EBCC
		public void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00003A82 File Offset: 0x00001C82
		public bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00050A0A File Offset: 0x0004EC0A
		public void removeHoldEff()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00050A1B File Offset: 0x0004EC1B
		public void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00050A24 File Offset: 0x0004EC24
		public void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00050A30 File Offset: 0x0004EC30
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

		// Token: 0x0600049A RID: 1178 RVA: 0x00050AEE File Offset: 0x0004ECEE
		private bool isTypeNewMod()
		{
			return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
		}

		// Token: 0x04000994 RID: 2452
		public const sbyte TYPE_DUNG = 0;

		// Token: 0x04000995 RID: 2453
		public const sbyte TYPE_DI = 1;

		// Token: 0x04000996 RID: 2454
		public const sbyte TYPE_NHAY = 2;

		// Token: 0x04000997 RID: 2455
		public const sbyte TYPE_LET = 3;

		// Token: 0x04000998 RID: 2456
		public const sbyte TYPE_BAY = 4;

		// Token: 0x04000999 RID: 2457
		public const sbyte TYPE_BAY_DAU = 5;

		// Token: 0x0400099A RID: 2458
		public static MobTemplate[] arrMobTemplate;

		// Token: 0x0400099B RID: 2459
		public const sbyte MA_INHELL = 0;

		// Token: 0x0400099C RID: 2460
		public const sbyte MA_DEADFLY = 1;

		// Token: 0x0400099D RID: 2461
		public const sbyte MA_STANDWAIT = 2;

		// Token: 0x0400099E RID: 2462
		public const sbyte MA_ATTACK = 3;

		// Token: 0x0400099F RID: 2463
		public const sbyte MA_STANDFLY = 4;

		// Token: 0x040009A0 RID: 2464
		public const sbyte MA_WALK = 5;

		// Token: 0x040009A1 RID: 2465
		public const sbyte MA_FALL = 6;

		// Token: 0x040009A2 RID: 2466
		public const sbyte MA_INJURE = 7;

		// Token: 0x040009A3 RID: 2467
		public bool changBody;

		// Token: 0x040009A4 RID: 2468
		public short smallBody;

		// Token: 0x040009A5 RID: 2469
		public bool isHintFocus;

		// Token: 0x040009A6 RID: 2470
		public string flystring;

		// Token: 0x040009A7 RID: 2471
		public int flyx;

		// Token: 0x040009A8 RID: 2472
		public int flyy;

		// Token: 0x040009A9 RID: 2473
		public int flyIndex;

		// Token: 0x040009AA RID: 2474
		public bool isFreez;

		// Token: 0x040009AB RID: 2475
		public int seconds;

		// Token: 0x040009AC RID: 2476
		public long last;

		// Token: 0x040009AD RID: 2477
		public long cur;

		// Token: 0x040009AE RID: 2478
		public int holdEffID;

		// Token: 0x040009AF RID: 2479
		public long hp;

		// Token: 0x040009B0 RID: 2480
		public long maxHp;

		// Token: 0x040009B1 RID: 2481
		public int x;

		// Token: 0x040009B2 RID: 2482
		public int y;

		// Token: 0x040009B3 RID: 2483
		public int dir = 1;

		// Token: 0x040009B4 RID: 2484
		public int dirV = 1;

		// Token: 0x040009B5 RID: 2485
		public int status;

		// Token: 0x040009B6 RID: 2486
		public int p1;

		// Token: 0x040009B7 RID: 2487
		public int p2;

		// Token: 0x040009B8 RID: 2488
		public int p3;

		// Token: 0x040009B9 RID: 2489
		public int xFirst;

		// Token: 0x040009BA RID: 2490
		public int yFirst;

		// Token: 0x040009BB RID: 2491
		public int vy;

		// Token: 0x040009BC RID: 2492
		public int exp;

		// Token: 0x040009BD RID: 2493
		public int w;

		// Token: 0x040009BE RID: 2494
		public int h;

		// Token: 0x040009BF RID: 2495
		public long hpInjure;

		// Token: 0x040009C0 RID: 2496
		public int charIndex;

		// Token: 0x040009C1 RID: 2497
		public int timeStatus;

		// Token: 0x040009C2 RID: 2498
		public int mobId;

		// Token: 0x040009C3 RID: 2499
		public bool isx;

		// Token: 0x040009C4 RID: 2500
		public bool isy;

		// Token: 0x040009C5 RID: 2501
		public bool isDisable;

		// Token: 0x040009C6 RID: 2502
		public bool isDontMove;

		// Token: 0x040009C7 RID: 2503
		public bool isFire;

		// Token: 0x040009C8 RID: 2504
		public bool isIce;

		// Token: 0x040009C9 RID: 2505
		public bool isWind;

		// Token: 0x040009CA RID: 2506
		public bool isDie;

		// Token: 0x040009CB RID: 2507
		public long lastDie;

		// Token: 0x040009CC RID: 2508
		public int countDie;

		// Token: 0x040009CD RID: 2509
		public MyVector vMobMove = new MyVector();

		// Token: 0x040009CE RID: 2510
		public bool isGo;

		// Token: 0x040009CF RID: 2511
		public string mobName;

		// Token: 0x040009D0 RID: 2512
		public int templateId;

		// Token: 0x040009D1 RID: 2513
		public short pointx;

		// Token: 0x040009D2 RID: 2514
		public short pointy;

		// Token: 0x040009D3 RID: 2515
		public Char cFocus;

		// Token: 0x040009D4 RID: 2516
		public long dame;

		// Token: 0x040009D5 RID: 2517
		public long dameMp;

		// Token: 0x040009D6 RID: 2518
		public int sys;

		// Token: 0x040009D7 RID: 2519
		public sbyte levelBoss;

		// Token: 0x040009D8 RID: 2520
		public sbyte level;

		// Token: 0x040009D9 RID: 2521
		public bool isBoss;

		// Token: 0x040009DA RID: 2522
		public bool isMobMe;

		// Token: 0x040009DB RID: 2523
		public static MyVector lastMob = new MyVector();

		// Token: 0x040009DC RID: 2524
		public static MyVector newMob = new MyVector();

		// Token: 0x040009DD RID: 2525
		public bool isMafuba;

		// Token: 0x040009DE RID: 2526
		public int xMFB;

		// Token: 0x040009DF RID: 2527
		public int yMFB;

		// Token: 0x040009E0 RID: 2528
		public int xSd;

		// Token: 0x040009E1 RID: 2529
		public int ySd;

		// Token: 0x040009E2 RID: 2530
		private bool isOutMap;

		// Token: 0x040009E3 RID: 2531
		private int wCount;

		// Token: 0x040009E4 RID: 2532
		public bool isShadown = true;

		// Token: 0x040009E5 RID: 2533
		private int tick;

		// Token: 0x040009E6 RID: 2534
		private int frame;

		// Token: 0x040009E7 RID: 2535
		public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x040009E8 RID: 2536
		private bool wy;

		// Token: 0x040009E9 RID: 2537
		private int wt;

		// Token: 0x040009EA RID: 2538
		private int fy;

		// Token: 0x040009EB RID: 2539
		private int ty;

		// Token: 0x040009EC RID: 2540
		public int typeSuperEff;

		// Token: 0x040009ED RID: 2541
		public bool isBusyAttackSomeOne = true;

		// Token: 0x040009EE RID: 2542
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

		// Token: 0x040009EF RID: 2543
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

		// Token: 0x040009F0 RID: 2544
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

		// Token: 0x040009F1 RID: 2545
		public int[] attack1 = new int[]
		{
			4,
			5,
			6
		};

		// Token: 0x040009F2 RID: 2546
		public int[] attack2 = new int[]
		{
			7,
			8,
			9
		};

		// Token: 0x040009F3 RID: 2547
		public int[] hurt = new int[1];

		// Token: 0x040009F4 RID: 2548
		private int color = 8421504;

		// Token: 0x040009F5 RID: 2549
		public int len = 24;

		// Token: 0x040009F6 RID: 2550
		public int w_hp_bar = 24;

		// Token: 0x040009F7 RID: 2551
		public int per = 100;

		// Token: 0x040009F8 RID: 2552
		public int per_tem = 100;

		// Token: 0x040009F9 RID: 2553
		public byte h_hp_bar = 4;

		// Token: 0x040009FA RID: 2554
		public Image imgHPtem;

		// Token: 0x040009FB RID: 2555
		private int offset;

		// Token: 0x040009FC RID: 2556
		public bool isHide;

		// Token: 0x040009FD RID: 2557
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x040009FE RID: 2558
		public Char injureBy;

		// Token: 0x040009FF RID: 2559
		public bool injureThenDie;

		// Token: 0x04000A00 RID: 2560
		public Mob mobToAttack;

		// Token: 0x04000A01 RID: 2561
		public int forceWait;

		// Token: 0x04000A02 RID: 2562
		public bool blindEff;

		// Token: 0x04000A03 RID: 2563
		public bool sleepEff;

		// Token: 0x04000A04 RID: 2564
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

		// Token: 0x04000A05 RID: 2565
		private bool isGetFr = true;
	}
}
