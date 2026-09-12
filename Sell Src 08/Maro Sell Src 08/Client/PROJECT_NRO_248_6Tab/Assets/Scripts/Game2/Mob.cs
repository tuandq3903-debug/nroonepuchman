using System;
using Game2.Assets.src.g;

namespace Game2
{
	// Token: 0x020003CC RID: 972
	public class Mob : IMapObject
	{
		// Token: 0x06002AFE RID: 11006 RVA: 0x002A2534 File Offset: 0x002A0734
		public Mob()
		{
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x002A26C0 File Offset: 0x002A08C0
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

		// Token: 0x06002B00 RID: 11008 RVA: 0x002A2B4F File Offset: 0x002A0D4F
		public bool isBigBoss()
		{
			return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x002A2B74 File Offset: 0x002A0D74
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

		// Token: 0x06002B02 RID: 11010 RVA: 0x00257550 File Offset: 0x00255750
		public virtual void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x00257560 File Offset: 0x00255760
		public virtual void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x002A2CB8 File Offset: 0x002A0EB8
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

		// Token: 0x06002B05 RID: 11013 RVA: 0x002A2CF8 File Offset: 0x002A0EF8
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

		// Token: 0x06002B06 RID: 11014 RVA: 0x002A2D62 File Offset: 0x002A0F62
		public void checkFrameTick(int[] array)
		{
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
			this.tick++;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x002A2D94 File Offset: 0x002A0F94
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

		// Token: 0x06002B08 RID: 11016 RVA: 0x002A2EB0 File Offset: 0x002A10B0
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

		// Token: 0x06002B09 RID: 11017 RVA: 0x002A2FFC File Offset: 0x002A11FC
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

		// Token: 0x06002B0A RID: 11018 RVA: 0x002A3060 File Offset: 0x002A1260
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

		// Token: 0x06002B0B RID: 11019 RVA: 0x002A3834 File Offset: 0x002A1A34
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

		// Token: 0x06002B0C RID: 11020 RVA: 0x002A38A4 File Offset: 0x002A1AA4
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

		// Token: 0x06002B0D RID: 11021 RVA: 0x002A38E8 File Offset: 0x002A1AE8
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

		// Token: 0x06002B0E RID: 11022 RVA: 0x002A392C File Offset: 0x002A1B2C
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

		// Token: 0x06002B0F RID: 11023 RVA: 0x002A3970 File Offset: 0x002A1B70
		public static NewBoss getNewBoss(sbyte idBoss)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
			if (mob is NewBoss)
			{
				return (NewBoss)mob;
			}
			return null;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x002A39A0 File Offset: 0x002A1BA0
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

		// Token: 0x06002B11 RID: 11025 RVA: 0x002A3A3F File Offset: 0x002A1C3F
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x002A3A6C File Offset: 0x002A1C6C
		private bool isNewModStand()
		{
			return this.templateId == 76;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x002A3A78 File Offset: 0x002A1C78
		private bool isNewMod()
		{
			return this.templateId >= 73 && !this.isNewModStand();
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x002A3A90 File Offset: 0x002A1C90
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

		// Token: 0x06002B15 RID: 11029 RVA: 0x002A3CF8 File Offset: 0x002A1EF8
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

		// Token: 0x06002B16 RID: 11030 RVA: 0x002A3EB4 File Offset: 0x002A20B4
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

		// Token: 0x06002B17 RID: 11031 RVA: 0x002A420C File Offset: 0x002A240C
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

		// Token: 0x06002B18 RID: 11032 RVA: 0x002A4754 File Offset: 0x002A2954
		public MobTemplate getTemplate()
		{
			return Mob.arrMobTemplate[this.templateId];
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x002A4764 File Offset: 0x002A2964
		public bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x002A4806 File Offset: 0x002A2A06
		public bool isUpdate()
		{
			return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x002A4838 File Offset: 0x002A2A38
		public bool checkIsBoss()
		{
			return this.isBoss || this.levelBoss > 0;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x002A4850 File Offset: 0x002A2A50
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

		// Token: 0x06002B1D RID: 11037 RVA: 0x002A4924 File Offset: 0x002A2B24
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

		// Token: 0x06002B1E RID: 11038 RVA: 0x002A4C00 File Offset: 0x002A2E00
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

		// Token: 0x06002B1F RID: 11039 RVA: 0x002A4C54 File Offset: 0x002A2E54
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

		// Token: 0x06002B20 RID: 11040 RVA: 0x00257E08 File Offset: 0x00256008
		public int getX()
		{
			return this.x;
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x002A4D1C File Offset: 0x002A2F1C
		public int getY()
		{
			return this.y;
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x002A4D24 File Offset: 0x002A2F24
		public int getH()
		{
			return this.h;
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x002A4D2C File Offset: 0x002A2F2C
		public int getW()
		{
			return this.w;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x002A4D34 File Offset: 0x002A2F34
		public void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x00257E5A File Offset: 0x0025605A
		public bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x002A4D72 File Offset: 0x002A2F72
		public void removeHoldEff()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x002A4D83 File Offset: 0x002A2F83
		public void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x002A4D8C File Offset: 0x002A2F8C
		public void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x002A4D98 File Offset: 0x002A2F98
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

		// Token: 0x06002B2A RID: 11050 RVA: 0x002A4E56 File Offset: 0x002A3056
		private bool isTypeNewMod()
		{
			return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
		}

		// Token: 0x04005390 RID: 21392
		public const sbyte TYPE_DUNG = 0;

		// Token: 0x04005391 RID: 21393
		public const sbyte TYPE_DI = 1;

		// Token: 0x04005392 RID: 21394
		public const sbyte TYPE_NHAY = 2;

		// Token: 0x04005393 RID: 21395
		public const sbyte TYPE_LET = 3;

		// Token: 0x04005394 RID: 21396
		public const sbyte TYPE_BAY = 4;

		// Token: 0x04005395 RID: 21397
		public const sbyte TYPE_BAY_DAU = 5;

		// Token: 0x04005396 RID: 21398
		public static MobTemplate[] arrMobTemplate;

		// Token: 0x04005397 RID: 21399
		public const sbyte MA_INHELL = 0;

		// Token: 0x04005398 RID: 21400
		public const sbyte MA_DEADFLY = 1;

		// Token: 0x04005399 RID: 21401
		public const sbyte MA_STANDWAIT = 2;

		// Token: 0x0400539A RID: 21402
		public const sbyte MA_ATTACK = 3;

		// Token: 0x0400539B RID: 21403
		public const sbyte MA_STANDFLY = 4;

		// Token: 0x0400539C RID: 21404
		public const sbyte MA_WALK = 5;

		// Token: 0x0400539D RID: 21405
		public const sbyte MA_FALL = 6;

		// Token: 0x0400539E RID: 21406
		public const sbyte MA_INJURE = 7;

		// Token: 0x0400539F RID: 21407
		public bool changBody;

		// Token: 0x040053A0 RID: 21408
		public short smallBody;

		// Token: 0x040053A1 RID: 21409
		public bool isHintFocus;

		// Token: 0x040053A2 RID: 21410
		public string flystring;

		// Token: 0x040053A3 RID: 21411
		public int flyx;

		// Token: 0x040053A4 RID: 21412
		public int flyy;

		// Token: 0x040053A5 RID: 21413
		public int flyIndex;

		// Token: 0x040053A6 RID: 21414
		public bool isFreez;

		// Token: 0x040053A7 RID: 21415
		public int seconds;

		// Token: 0x040053A8 RID: 21416
		public long last;

		// Token: 0x040053A9 RID: 21417
		public long cur;

		// Token: 0x040053AA RID: 21418
		public int holdEffID;

		// Token: 0x040053AB RID: 21419
		public long hp;

		// Token: 0x040053AC RID: 21420
		public long maxHp;

		// Token: 0x040053AD RID: 21421
		public int x;

		// Token: 0x040053AE RID: 21422
		public int y;

		// Token: 0x040053AF RID: 21423
		public int dir = 1;

		// Token: 0x040053B0 RID: 21424
		public int dirV = 1;

		// Token: 0x040053B1 RID: 21425
		public int status;

		// Token: 0x040053B2 RID: 21426
		public int p1;

		// Token: 0x040053B3 RID: 21427
		public int p2;

		// Token: 0x040053B4 RID: 21428
		public int p3;

		// Token: 0x040053B5 RID: 21429
		public int xFirst;

		// Token: 0x040053B6 RID: 21430
		public int yFirst;

		// Token: 0x040053B7 RID: 21431
		public int vy;

		// Token: 0x040053B8 RID: 21432
		public int exp;

		// Token: 0x040053B9 RID: 21433
		public int w;

		// Token: 0x040053BA RID: 21434
		public int h;

		// Token: 0x040053BB RID: 21435
		public long hpInjure;

		// Token: 0x040053BC RID: 21436
		public int charIndex;

		// Token: 0x040053BD RID: 21437
		public int timeStatus;

		// Token: 0x040053BE RID: 21438
		public int mobId;

		// Token: 0x040053BF RID: 21439
		public bool isx;

		// Token: 0x040053C0 RID: 21440
		public bool isy;

		// Token: 0x040053C1 RID: 21441
		public bool isDisable;

		// Token: 0x040053C2 RID: 21442
		public bool isDontMove;

		// Token: 0x040053C3 RID: 21443
		public bool isFire;

		// Token: 0x040053C4 RID: 21444
		public bool isIce;

		// Token: 0x040053C5 RID: 21445
		public bool isWind;

		// Token: 0x040053C6 RID: 21446
		public bool isDie;

		// Token: 0x040053C7 RID: 21447
		public long lastDie;

		// Token: 0x040053C8 RID: 21448
		public int countDie;

		// Token: 0x040053C9 RID: 21449
		public MyVector vMobMove = new MyVector();

		// Token: 0x040053CA RID: 21450
		public bool isGo;

		// Token: 0x040053CB RID: 21451
		public string mobName;

		// Token: 0x040053CC RID: 21452
		public int templateId;

		// Token: 0x040053CD RID: 21453
		public short pointx;

		// Token: 0x040053CE RID: 21454
		public short pointy;

		// Token: 0x040053CF RID: 21455
		public Char cFocus;

		// Token: 0x040053D0 RID: 21456
		public long dame;

		// Token: 0x040053D1 RID: 21457
		public long dameMp;

		// Token: 0x040053D2 RID: 21458
		public int sys;

		// Token: 0x040053D3 RID: 21459
		public sbyte levelBoss;

		// Token: 0x040053D4 RID: 21460
		public sbyte level;

		// Token: 0x040053D5 RID: 21461
		public bool isBoss;

		// Token: 0x040053D6 RID: 21462
		public bool isMobMe;

		// Token: 0x040053D7 RID: 21463
		public static MyVector lastMob = new MyVector();

		// Token: 0x040053D8 RID: 21464
		public static MyVector newMob = new MyVector();

		// Token: 0x040053D9 RID: 21465
		public bool isMafuba;

		// Token: 0x040053DA RID: 21466
		public int xMFB;

		// Token: 0x040053DB RID: 21467
		public int yMFB;

		// Token: 0x040053DC RID: 21468
		public int xSd;

		// Token: 0x040053DD RID: 21469
		public int ySd;

		// Token: 0x040053DE RID: 21470
		private bool isOutMap;

		// Token: 0x040053DF RID: 21471
		private int wCount;

		// Token: 0x040053E0 RID: 21472
		public bool isShadown = true;

		// Token: 0x040053E1 RID: 21473
		private int tick;

		// Token: 0x040053E2 RID: 21474
		private int frame;

		// Token: 0x040053E3 RID: 21475
		public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x040053E4 RID: 21476
		private bool wy;

		// Token: 0x040053E5 RID: 21477
		private int wt;

		// Token: 0x040053E6 RID: 21478
		private int fy;

		// Token: 0x040053E7 RID: 21479
		private int ty;

		// Token: 0x040053E8 RID: 21480
		public int typeSuperEff;

		// Token: 0x040053E9 RID: 21481
		public bool isBusyAttackSomeOne = true;

		// Token: 0x040053EA RID: 21482
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

		// Token: 0x040053EB RID: 21483
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

		// Token: 0x040053EC RID: 21484
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

		// Token: 0x040053ED RID: 21485
		public int[] attack1 = new int[]
		{
			4,
			5,
			6
		};

		// Token: 0x040053EE RID: 21486
		public int[] attack2 = new int[]
		{
			7,
			8,
			9
		};

		// Token: 0x040053EF RID: 21487
		public int[] hurt = new int[1];

		// Token: 0x040053F0 RID: 21488
		private int color = 8421504;

		// Token: 0x040053F1 RID: 21489
		public int len = 24;

		// Token: 0x040053F2 RID: 21490
		public int w_hp_bar = 24;

		// Token: 0x040053F3 RID: 21491
		public int per = 100;

		// Token: 0x040053F4 RID: 21492
		public int per_tem = 100;

		// Token: 0x040053F5 RID: 21493
		public byte h_hp_bar = 4;

		// Token: 0x040053F6 RID: 21494
		public Image imgHPtem;

		// Token: 0x040053F7 RID: 21495
		private int offset;

		// Token: 0x040053F8 RID: 21496
		public bool isHide;

		// Token: 0x040053F9 RID: 21497
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x040053FA RID: 21498
		public Char injureBy;

		// Token: 0x040053FB RID: 21499
		public bool injureThenDie;

		// Token: 0x040053FC RID: 21500
		public Mob mobToAttack;

		// Token: 0x040053FD RID: 21501
		public int forceWait;

		// Token: 0x040053FE RID: 21502
		public bool blindEff;

		// Token: 0x040053FF RID: 21503
		public bool sleepEff;

		// Token: 0x04005400 RID: 21504
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

		// Token: 0x04005401 RID: 21505
		private bool isGetFr = true;
	}
}
