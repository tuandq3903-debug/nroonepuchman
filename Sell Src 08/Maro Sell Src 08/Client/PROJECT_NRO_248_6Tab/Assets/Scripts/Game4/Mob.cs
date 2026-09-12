using System;
using Game4.Assets.src.g;

namespace Game4
{
	// Token: 0x0200021C RID: 540
	public class Mob : IMapObject
	{
		// Token: 0x060017B6 RID: 6070 RVA: 0x001783EC File Offset: 0x001765EC
		public Mob()
		{
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00178578 File Offset: 0x00176778
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

		// Token: 0x060017B8 RID: 6072 RVA: 0x00178A07 File Offset: 0x00176C07
		public bool isBigBoss()
		{
			return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00178A2C File Offset: 0x00176C2C
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

		// Token: 0x060017BA RID: 6074 RVA: 0x0012D408 File Offset: 0x0012B608
		public virtual void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0012D418 File Offset: 0x0012B618
		public virtual void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00178B70 File Offset: 0x00176D70
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

		// Token: 0x060017BD RID: 6077 RVA: 0x00178BB0 File Offset: 0x00176DB0
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

		// Token: 0x060017BE RID: 6078 RVA: 0x00178C1A File Offset: 0x00176E1A
		public void checkFrameTick(int[] array)
		{
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
			this.tick++;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00178C4C File Offset: 0x00176E4C
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

		// Token: 0x060017C0 RID: 6080 RVA: 0x00178D68 File Offset: 0x00176F68
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

		// Token: 0x060017C1 RID: 6081 RVA: 0x00178EB4 File Offset: 0x001770B4
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

		// Token: 0x060017C2 RID: 6082 RVA: 0x00178F18 File Offset: 0x00177118
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

		// Token: 0x060017C3 RID: 6083 RVA: 0x001796EC File Offset: 0x001778EC
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

		// Token: 0x060017C4 RID: 6084 RVA: 0x0017975C File Offset: 0x0017795C
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

		// Token: 0x060017C5 RID: 6085 RVA: 0x001797A0 File Offset: 0x001779A0
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

		// Token: 0x060017C6 RID: 6086 RVA: 0x001797E4 File Offset: 0x001779E4
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

		// Token: 0x060017C7 RID: 6087 RVA: 0x00179828 File Offset: 0x00177A28
		public static NewBoss getNewBoss(sbyte idBoss)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
			if (mob is NewBoss)
			{
				return (NewBoss)mob;
			}
			return null;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00179858 File Offset: 0x00177A58
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

		// Token: 0x060017C9 RID: 6089 RVA: 0x001798F7 File Offset: 0x00177AF7
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00179924 File Offset: 0x00177B24
		private bool isNewModStand()
		{
			return this.templateId == 76;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00179930 File Offset: 0x00177B30
		private bool isNewMod()
		{
			return this.templateId >= 73 && !this.isNewModStand();
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00179948 File Offset: 0x00177B48
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

		// Token: 0x060017CD RID: 6093 RVA: 0x00179BB0 File Offset: 0x00177DB0
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

		// Token: 0x060017CE RID: 6094 RVA: 0x00179D6C File Offset: 0x00177F6C
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

		// Token: 0x060017CF RID: 6095 RVA: 0x0017A0C4 File Offset: 0x001782C4
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

		// Token: 0x060017D0 RID: 6096 RVA: 0x0017A60C File Offset: 0x0017880C
		public MobTemplate getTemplate()
		{
			return Mob.arrMobTemplate[this.templateId];
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0017A61C File Offset: 0x0017881C
		public bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0017A6BE File Offset: 0x001788BE
		public bool isUpdate()
		{
			return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0017A6F0 File Offset: 0x001788F0
		public bool checkIsBoss()
		{
			return this.isBoss || this.levelBoss > 0;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0017A708 File Offset: 0x00178908
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

		// Token: 0x060017D5 RID: 6101 RVA: 0x0017A7DC File Offset: 0x001789DC
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

		// Token: 0x060017D6 RID: 6102 RVA: 0x0017AAB8 File Offset: 0x00178CB8
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

		// Token: 0x060017D7 RID: 6103 RVA: 0x0017AB0C File Offset: 0x00178D0C
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

		// Token: 0x060017D8 RID: 6104 RVA: 0x0012DCC0 File Offset: 0x0012BEC0
		public int getX()
		{
			return this.x;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0017ABD4 File Offset: 0x00178DD4
		public int getY()
		{
			return this.y;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0017ABDC File Offset: 0x00178DDC
		public int getH()
		{
			return this.h;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0017ABE4 File Offset: 0x00178DE4
		public int getW()
		{
			return this.w;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0017ABEC File Offset: 0x00178DEC
		public void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0012DD12 File Offset: 0x0012BF12
		public bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0017AC2A File Offset: 0x00178E2A
		public void removeHoldEff()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0017AC3B File Offset: 0x00178E3B
		public void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0017AC44 File Offset: 0x00178E44
		public void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0017AC50 File Offset: 0x00178E50
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

		// Token: 0x060017E2 RID: 6114 RVA: 0x0017AD0E File Offset: 0x00178F0E
		private bool isTypeNewMod()
		{
			return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
		}

		// Token: 0x04002E92 RID: 11922
		public const sbyte TYPE_DUNG = 0;

		// Token: 0x04002E93 RID: 11923
		public const sbyte TYPE_DI = 1;

		// Token: 0x04002E94 RID: 11924
		public const sbyte TYPE_NHAY = 2;

		// Token: 0x04002E95 RID: 11925
		public const sbyte TYPE_LET = 3;

		// Token: 0x04002E96 RID: 11926
		public const sbyte TYPE_BAY = 4;

		// Token: 0x04002E97 RID: 11927
		public const sbyte TYPE_BAY_DAU = 5;

		// Token: 0x04002E98 RID: 11928
		public static MobTemplate[] arrMobTemplate;

		// Token: 0x04002E99 RID: 11929
		public const sbyte MA_INHELL = 0;

		// Token: 0x04002E9A RID: 11930
		public const sbyte MA_DEADFLY = 1;

		// Token: 0x04002E9B RID: 11931
		public const sbyte MA_STANDWAIT = 2;

		// Token: 0x04002E9C RID: 11932
		public const sbyte MA_ATTACK = 3;

		// Token: 0x04002E9D RID: 11933
		public const sbyte MA_STANDFLY = 4;

		// Token: 0x04002E9E RID: 11934
		public const sbyte MA_WALK = 5;

		// Token: 0x04002E9F RID: 11935
		public const sbyte MA_FALL = 6;

		// Token: 0x04002EA0 RID: 11936
		public const sbyte MA_INJURE = 7;

		// Token: 0x04002EA1 RID: 11937
		public bool changBody;

		// Token: 0x04002EA2 RID: 11938
		public short smallBody;

		// Token: 0x04002EA3 RID: 11939
		public bool isHintFocus;

		// Token: 0x04002EA4 RID: 11940
		public string flystring;

		// Token: 0x04002EA5 RID: 11941
		public int flyx;

		// Token: 0x04002EA6 RID: 11942
		public int flyy;

		// Token: 0x04002EA7 RID: 11943
		public int flyIndex;

		// Token: 0x04002EA8 RID: 11944
		public bool isFreez;

		// Token: 0x04002EA9 RID: 11945
		public int seconds;

		// Token: 0x04002EAA RID: 11946
		public long last;

		// Token: 0x04002EAB RID: 11947
		public long cur;

		// Token: 0x04002EAC RID: 11948
		public int holdEffID;

		// Token: 0x04002EAD RID: 11949
		public long hp;

		// Token: 0x04002EAE RID: 11950
		public long maxHp;

		// Token: 0x04002EAF RID: 11951
		public int x;

		// Token: 0x04002EB0 RID: 11952
		public int y;

		// Token: 0x04002EB1 RID: 11953
		public int dir = 1;

		// Token: 0x04002EB2 RID: 11954
		public int dirV = 1;

		// Token: 0x04002EB3 RID: 11955
		public int status;

		// Token: 0x04002EB4 RID: 11956
		public int p1;

		// Token: 0x04002EB5 RID: 11957
		public int p2;

		// Token: 0x04002EB6 RID: 11958
		public int p3;

		// Token: 0x04002EB7 RID: 11959
		public int xFirst;

		// Token: 0x04002EB8 RID: 11960
		public int yFirst;

		// Token: 0x04002EB9 RID: 11961
		public int vy;

		// Token: 0x04002EBA RID: 11962
		public int exp;

		// Token: 0x04002EBB RID: 11963
		public int w;

		// Token: 0x04002EBC RID: 11964
		public int h;

		// Token: 0x04002EBD RID: 11965
		public long hpInjure;

		// Token: 0x04002EBE RID: 11966
		public int charIndex;

		// Token: 0x04002EBF RID: 11967
		public int timeStatus;

		// Token: 0x04002EC0 RID: 11968
		public int mobId;

		// Token: 0x04002EC1 RID: 11969
		public bool isx;

		// Token: 0x04002EC2 RID: 11970
		public bool isy;

		// Token: 0x04002EC3 RID: 11971
		public bool isDisable;

		// Token: 0x04002EC4 RID: 11972
		public bool isDontMove;

		// Token: 0x04002EC5 RID: 11973
		public bool isFire;

		// Token: 0x04002EC6 RID: 11974
		public bool isIce;

		// Token: 0x04002EC7 RID: 11975
		public bool isWind;

		// Token: 0x04002EC8 RID: 11976
		public bool isDie;

		// Token: 0x04002EC9 RID: 11977
		public long lastDie;

		// Token: 0x04002ECA RID: 11978
		public int countDie;

		// Token: 0x04002ECB RID: 11979
		public MyVector vMobMove = new MyVector();

		// Token: 0x04002ECC RID: 11980
		public bool isGo;

		// Token: 0x04002ECD RID: 11981
		public string mobName;

		// Token: 0x04002ECE RID: 11982
		public int templateId;

		// Token: 0x04002ECF RID: 11983
		public short pointx;

		// Token: 0x04002ED0 RID: 11984
		public short pointy;

		// Token: 0x04002ED1 RID: 11985
		public Char cFocus;

		// Token: 0x04002ED2 RID: 11986
		public long dame;

		// Token: 0x04002ED3 RID: 11987
		public long dameMp;

		// Token: 0x04002ED4 RID: 11988
		public int sys;

		// Token: 0x04002ED5 RID: 11989
		public sbyte levelBoss;

		// Token: 0x04002ED6 RID: 11990
		public sbyte level;

		// Token: 0x04002ED7 RID: 11991
		public bool isBoss;

		// Token: 0x04002ED8 RID: 11992
		public bool isMobMe;

		// Token: 0x04002ED9 RID: 11993
		public static MyVector lastMob = new MyVector();

		// Token: 0x04002EDA RID: 11994
		public static MyVector newMob = new MyVector();

		// Token: 0x04002EDB RID: 11995
		public bool isMafuba;

		// Token: 0x04002EDC RID: 11996
		public int xMFB;

		// Token: 0x04002EDD RID: 11997
		public int yMFB;

		// Token: 0x04002EDE RID: 11998
		public int xSd;

		// Token: 0x04002EDF RID: 11999
		public int ySd;

		// Token: 0x04002EE0 RID: 12000
		private bool isOutMap;

		// Token: 0x04002EE1 RID: 12001
		private int wCount;

		// Token: 0x04002EE2 RID: 12002
		public bool isShadown = true;

		// Token: 0x04002EE3 RID: 12003
		private int tick;

		// Token: 0x04002EE4 RID: 12004
		private int frame;

		// Token: 0x04002EE5 RID: 12005
		public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04002EE6 RID: 12006
		private bool wy;

		// Token: 0x04002EE7 RID: 12007
		private int wt;

		// Token: 0x04002EE8 RID: 12008
		private int fy;

		// Token: 0x04002EE9 RID: 12009
		private int ty;

		// Token: 0x04002EEA RID: 12010
		public int typeSuperEff;

		// Token: 0x04002EEB RID: 12011
		public bool isBusyAttackSomeOne = true;

		// Token: 0x04002EEC RID: 12012
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

		// Token: 0x04002EED RID: 12013
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

		// Token: 0x04002EEE RID: 12014
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

		// Token: 0x04002EEF RID: 12015
		public int[] attack1 = new int[]
		{
			4,
			5,
			6
		};

		// Token: 0x04002EF0 RID: 12016
		public int[] attack2 = new int[]
		{
			7,
			8,
			9
		};

		// Token: 0x04002EF1 RID: 12017
		public int[] hurt = new int[1];

		// Token: 0x04002EF2 RID: 12018
		private int color = 8421504;

		// Token: 0x04002EF3 RID: 12019
		public int len = 24;

		// Token: 0x04002EF4 RID: 12020
		public int w_hp_bar = 24;

		// Token: 0x04002EF5 RID: 12021
		public int per = 100;

		// Token: 0x04002EF6 RID: 12022
		public int per_tem = 100;

		// Token: 0x04002EF7 RID: 12023
		public byte h_hp_bar = 4;

		// Token: 0x04002EF8 RID: 12024
		public Image imgHPtem;

		// Token: 0x04002EF9 RID: 12025
		private int offset;

		// Token: 0x04002EFA RID: 12026
		public bool isHide;

		// Token: 0x04002EFB RID: 12027
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04002EFC RID: 12028
		public Char injureBy;

		// Token: 0x04002EFD RID: 12029
		public bool injureThenDie;

		// Token: 0x04002EFE RID: 12030
		public Mob mobToAttack;

		// Token: 0x04002EFF RID: 12031
		public int forceWait;

		// Token: 0x04002F00 RID: 12032
		public bool blindEff;

		// Token: 0x04002F01 RID: 12033
		public bool sleepEff;

		// Token: 0x04002F02 RID: 12034
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

		// Token: 0x04002F03 RID: 12035
		private bool isGetFr = true;
	}
}
