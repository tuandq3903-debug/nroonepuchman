using System;
using Game2.Assets.src.e;
using Game2.Assets.src.g;
using UnityEngine;

namespace Game2
{
	// Token: 0x0200037A RID: 890
	public class Char : IMapObject
	{
		// Token: 0x0600272E RID: 10030 RVA: 0x0025B13C File Offset: 0x0025933C
		public Char()
		{
			this.statusMe = 6;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x0025B398 File Offset: 0x00259598
		public void applyCharLevelPercent()
		{
			try
			{
				long num = 1L;
				long num2 = 0L;
				int num3 = 0;
				for (int num4 = GameScr.exps.Length - 1; num4 >= 0; num4--)
				{
					if (this.cPower >= GameScr.exps[num4])
					{
						num = ((num4 != GameScr.exps.Length - 1) ? (GameScr.exps[num4 + 1] - GameScr.exps[num4]) : 1L);
						num2 = this.cPower - GameScr.exps[num4];
						num3 = num4;
						break;
					}
				}
				this.clevel = num3;
				this.cLevelPercent = num2 * 10000L / num;
				if (this.cLevelPercent > 10000L)
				{
					this.cLevelPercent = 10000L;
				}
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi char level percent: " + ex.ToString());
			}
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x0025B464 File Offset: 0x00259664
		public int getdxSkill()
		{
			if (this.myskill != null)
			{
				return this.myskill.dx;
			}
			return 0;
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x0025B47B File Offset: 0x0025967B
		public int getdySkill()
		{
			if (this.myskill != null)
			{
				return this.myskill.dy;
			}
			return 0;
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x0025B494 File Offset: 0x00259694
		public static void taskAction(bool isNextStep)
		{
			Task task = Char.myCharz().taskMaint;
			if (task.index > task.contentInfo.Length - 1)
			{
				task.index = task.contentInfo.Length - 1;
			}
			string text = task.contentInfo[task.index];
			if (text != null && !text.Equals(string.Empty))
			{
				if (text.StartsWith("#"))
				{
					text = NinjaUtil.Replace(text, "#", string.Empty);
					Npc npc = new Npc(5, 0, -100, -100, 5, GameScr.info1.charId[Char.myCharz().cgender][2]);
					npc.cx = (npc.cy = -100);
					npc.avatar = GameScr.info1.charId[Char.myCharz().cgender][2];
					npc.charID = 5;
					if (GameCanvas.currentScreen == GameScr.instance)
					{
						ChatPopup.addNextPopUpMultiLine(text, npc);
					}
				}
				else if (isNextStep)
				{
					GameScr.info1.addInfo(text, 0);
				}
			}
			GameScr.isHaveSelectSkill = true;
			Cout.println("TASKx " + Char.myCharz().taskMaint.taskId.ToString());
			if (Char.myCharz().taskMaint.taskId <= 2)
			{
				Char.myCharz().canFly = false;
			}
			else
			{
				Char.myCharz().canFly = true;
			}
			GameScr.gI().left = null;
			if (task.taskId == 0)
			{
				Hint.isViewMap = false;
				Hint.isViewPotential = false;
				GameScr.gI().right = null;
				GameScr.isHaveSelectSkill = false;
				GameScr.gI().left = null;
				if (task.index < 4)
				{
					MagicTree.isPaint = false;
					GameScr.isPaintRada = -1;
				}
				if (task.index == 4)
				{
					GameScr.isPaintRada = 1;
					MagicTree.isPaint = true;
				}
				if (task.index >= 5)
				{
					GameScr.gI().right = GameScr.gI().cmdFocus;
				}
			}
			if (task.taskId == 1)
			{
				GameScr.isHaveSelectSkill = true;
			}
			if (task.taskId >= 1)
			{
				GameScr.gI().right = GameScr.gI().cmdFocus;
				GameScr.gI().left = GameScr.gI().cmdMenu;
			}
			if (task.taskId >= 0)
			{
				Panel.isPaintMap = true;
			}
			else
			{
				Panel.isPaintMap = false;
			}
			if (task.taskId < 12)
			{
				GameCanvas.panel.mainTabName = mResources.mainTab1;
			}
			else
			{
				GameCanvas.panel.mainTabName = mResources.mainTab2;
			}
			GameCanvas.panel.tabName[0] = GameCanvas.panel.mainTabName;
			if (Char.myChar.taskMaint.taskId > 10)
			{
				Rms.saveRMSString("fake", "aa");
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0025B71C File Offset: 0x0025991C
		public string getStrLevel()
		{
			if (this.clevel >= this.strLevel.Length)
			{
				this.clevel = this.strLevel.Length - 1;
			}
			string text = string.Concat(new string[]
			{
				this.strLevel[this.clevel],
				"+",
				(this.cLevelPercent / 100L).ToString(),
				".",
				(this.cLevelPercent % 100L).ToString(),
				"%"
			});
			if (text.Length > 23 && text.IndexOf("cấp ") >= 0)
			{
				text = Res.replace(text, "cấp ", "c");
			}
			return text;
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0025B7D1 File Offset: 0x002599D1
		public int avatarz()
		{
			return this.getAvatar(this.head);
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x0025B7E0 File Offset: 0x002599E0
		public int getAvatar(int headId)
		{
			int result;
			try
			{
				for (int i = 0; i < Char.idHead.Length; i++)
				{
					if (headId == (int)Char.idHead[i])
					{
						return (int)Char.idAvatar[i];
					}
				}
				result = -1;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result = -1;
			}
			return result;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x0025B834 File Offset: 0x00259A34
		public void setPowerInfo(string info, short p, short maxP, short sc)
		{
			this.powerPoint = p;
			this.strInfo = info;
			this.maxPowerPoint = maxP;
			this.secondPower = sc;
			this.lastS = (this.currS = mSystem.currentTimeMillis());
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x0025B874 File Offset: 0x00259A74
		public void addInfo(string info)
		{
			if (this.chatInfo == null)
			{
				this.chatInfo = new Info();
			}
			Char cInfo = null;
			this.chatInfo.addInfo(info, 0, cInfo, false);
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x0025B8A5 File Offset: 0x00259AA5
		public static Char myCharz()
		{
			if (Char.myChar == null)
			{
				Char.myChar = new Char();
				Char.myChar.me = true;
				Char.myChar.cmtoChar = true;
			}
			return Char.myChar;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x0025B8D3 File Offset: 0x00259AD3
		public static Char myPetz()
		{
			if (Char.myPet == null)
			{
				Char.myPet = new Char();
				Char.myPet.me = false;
			}
			return Char.myPet;
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0025B8F6 File Offset: 0x00259AF6
		public static Char MyPet2z()
		{
			if (Char.myPet2 == null)
			{
				Char.myPet2 = new Char
				{
					me = false
				};
			}
			return Char.myPet2;
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x0025B915 File Offset: 0x00259B15
		public static void clearMyChar()
		{
			Char.myChar = null;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x0025B920 File Offset: 0x00259B20
		public void boxSort()
		{
			try
			{
				MyVector myVector = new MyVector();
				for (int i = 0; i < this.arrItemBox.Length; i++)
				{
					Item item = this.arrItemBox[i];
					if (item != null && item.template.isUpToUp && !item.isExpires)
					{
						myVector.addElement(item);
					}
				}
				for (int j = 0; j < myVector.size(); j++)
				{
					Item item2 = (Item)myVector.elementAt(j);
					if (item2 != null)
					{
						for (int k = j + 1; k < myVector.size(); k++)
						{
							Item item3 = (Item)myVector.elementAt(k);
							if (item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock)
							{
								item2.quantity += item3.quantity;
								this.arrItemBox[item3.indexUI] = null;
								myVector.setElementAt(null, k);
							}
						}
					}
				}
				for (int l = 0; l < this.arrItemBox.Length; l++)
				{
					if (this.arrItemBox[l] != null)
					{
						for (int m = 0; m <= l; m++)
						{
							if (this.arrItemBox[m] == null)
							{
								this.arrItemBox[m] = this.arrItemBox[l];
								this.arrItemBox[m].indexUI = m;
								this.arrItemBox[l] = null;
								break;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				Cout.println("Char.boxSort()");
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0025BAAC File Offset: 0x00259CAC
		public Skill getSkill(SkillTemplate skillTemplate)
		{
			for (int i = 0; i < this.vSkill.size(); i++)
			{
				if (((Skill)this.vSkill.elementAt(i)).template.id == skillTemplate.id)
				{
					return (Skill)this.vSkill.elementAt(i);
				}
			}
			return null;
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x0025BB08 File Offset: 0x00259D08
		public Waypoint isInEnterOfflinePoint()
		{
			Task task = Char.myChar.taskMaint;
			if (task != null && task.taskId == 0 && task.index < 6)
			{
				return null;
			}
			int num = TileMap.vGo.size();
			sbyte b = 0;
			while ((int)b < num)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
				if (PopUp.vPopups.size() >= num && !((PopUp)PopUp.vPopups.elementAt((int)b)).isPaint)
				{
					return null;
				}
				if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && waypoint.isOffline)
				{
					return waypoint;
				}
				b += 1;
			}
			return null;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x0025BBD8 File Offset: 0x00259DD8
		public Waypoint isInEnterOnlinePoint()
		{
			Task task = Char.myChar.taskMaint;
			if (task != null && task.taskId == 0 && task.index < 6)
			{
				return null;
			}
			int num = TileMap.vGo.size();
			sbyte b = 0;
			while ((int)b < num)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
				if (PopUp.vPopups.size() >= num && !((PopUp)PopUp.vPopups.elementAt((int)b)).isPaint)
				{
					return null;
				}
				if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && !waypoint.isOffline)
				{
					return waypoint;
				}
				b += 1;
			}
			return null;
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0025BCA8 File Offset: 0x00259EA8
		public bool isInWaypoint()
		{
			if (TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48)
			{
				return true;
			}
			if (this.isTeleport || this.isUsePlane)
			{
				return false;
			}
			int num = TileMap.vGo.size();
			sbyte b = 0;
			while ((int)b < num)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
				if ((TileMap.mapID == 47 || TileMap.isInAirMap()) && this.cy <= (int)(waypoint.minY + waypoint.maxY) && this.cx > (int)waypoint.minX && this.cx < (int)waypoint.maxX)
				{
					return !TileMap.isInAirMap() || this.cTypePk == 0;
				}
				if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && !waypoint.isEnter)
				{
					return true;
				}
				b += 1;
			}
			return false;
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x0025BDA8 File Offset: 0x00259FA8
		public bool isPunchKickSkill()
		{
			return this.skillPaint != null && ((this.skillPaint.id >= 0 && this.skillPaint.id <= 6) || this.skillPaint.id == 183 || ((this.skillPaint.id >= 14 && this.skillPaint.id <= 20) || this.skillPaint.id == 192) || ((this.skillPaint.id >= 28 && this.skillPaint.id <= 34) || this.skillPaint.id == 164) || ((this.skillPaint.id >= 63 && this.skillPaint.id <= 69) || this.skillPaint.id == 186));
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0025BE88 File Offset: 0x0025A088
		public void soundUpdate()
		{
			if (this.me && this.statusMe == 10 && this.cf == 8 && this.ty > 20 && GameCanvas.gameTick % 20 == 0)
			{
				SoundMn.gI().charFly();
			}
			if (this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length && this.isPunchKickSkill() && (this.me || (!this.me && this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w)) && GameCanvas.gameTick % 5 == 0)
			{
				if (this.cf == 9 || this.cf == 10 || this.cf == 11)
				{
					SoundMn.gI().charPunch(true, (!this.me) ? 0.05f : 0.1f);
					return;
				}
				SoundMn.gI().charPunch(false, (!this.me) ? 0.05f : 0.1f);
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0025BF9C File Offset: 0x0025A19C
		public virtual void update()
		{
			if (this.isMafuba)
			{
				this.cf = 23;
				this.countMafuba += 1;
				if (this.countMafuba > 150)
				{
					this.isMafuba = false;
				}
				return;
			}
			this.countMafuba = 0;
			if (this.isHide || this.isMabuHold)
			{
				return;
			}
			if ((this.isCopy || this.clevel >= 14) && this.statusMe != 1)
			{
				int num6 = this.statusMe;
			}
			if (this.petFollow != null)
			{
				if (GameCanvas.gameTick % 3 == 0)
				{
					if (Char.myCharz().cdir == 1)
					{
						this.petFollow.cmtoX = this.cx - 20;
					}
					if (Char.myCharz().cdir == -1)
					{
						this.petFollow.cmtoX = this.cx + 20;
					}
					this.petFollow.cmtoY = this.cy - 40;
					if (this.petFollow.cmx > this.cx)
					{
						this.petFollow.dir = -1;
					}
					else
					{
						this.petFollow.dir = 1;
					}
					if (this.petFollow.cmtoX < 100)
					{
						this.petFollow.cmtoX = 100;
					}
					if (this.petFollow.cmtoX > TileMap.pxw - 100)
					{
						this.petFollow.cmtoX = TileMap.pxw - 100;
					}
				}
				this.petFollow.update();
			}
			if (!this.me && this.cHP <= 0L && this.clanID != -100 && this.statusMe != 14 && this.statusMe != 5)
			{
				this.startDie((short)this.cx, (short)this.cy);
			}
			if (this.isInjureHp)
			{
				this.twHp++;
				if (this.twHp == 20)
				{
					this.twHp = 0;
					this.isInjureHp = false;
				}
			}
			else if (this.dHP > this.cHP)
			{
				long num = (this.dHP - this.cHP) / 2L;
				if (num < 1L)
				{
					num = 1L;
				}
				this.dHP -= num;
			}
			else
			{
				this.dHP = this.cHP;
			}
			if (this.secondPower != 0)
			{
				this.currS = mSystem.currentTimeMillis();
				if (this.currS - this.lastS >= 1000L)
				{
					this.lastS = mSystem.currentTimeMillis();
					this.secondPower -= 1;
				}
			}
			if (this.isPaintNewSkill)
			{
				if (GameCanvas.timeNow > this.timeReset_newSkill || this.statusMe == 14 || this.statusMe == 5)
				{
					this.timeReset_newSkill = 0L;
					this.isPaintNewSkill = false;
				}
				this.UpdSkillPaint_NEW();
				if (this.isShadown)
				{
					this.updateShadown();
					return;
				}
			}
			else
			{
				if (!this.me && GameScr.notPaint)
				{
					return;
				}
				if (this.sleepEff && GameCanvas.gameTick % 10 == 0)
				{
					EffecMn.addEff(new Effect(41, this.cx, this.cy, 3, 1, 1));
				}
				if (this.huytSao)
				{
					this.huytSao = false;
					EffecMn.addEff(new Effect(39, this.cx, this.cy, 3, 3, 1));
				}
				if (this.blindEff && GameCanvas.gameTick % 5 == 0)
				{
					ServerEffect.addServerEffect(113, this, 1);
				}
				if (this.protectEff)
				{
					int y = this.cH_new + 73;
					if (GameCanvas.gameTick % 5 == 0)
					{
						this.eProtect = new Effect(33, this.cx, y, 3, 3, 1);
					}
					if (this.eProtect != null)
					{
						this.eProtect.update();
						this.eProtect.x = this.cx;
						this.eProtect.y = y;
					}
				}
				if (this.danhHieuEff)
				{
					if (this.eDanhHieu == null)
					{
						string text = (string)GameCanvas.danhHieu.get(this.charID.ToString() + string.Empty);
						if (text != null)
						{
							string[] array = Res.split(text.Trim(), ",", 0);
							short id = short.Parse(array[0]);
							short num2 = short.Parse(array[1]);
							this.eDanhHieu = new Effect((int)id, this.cx, this.cH_new + 73, 1, -1, -1);
							this.eDanhHieu.timeExist = (long)(num2 * 1000) + mSystem.currentTimeMillis();
						}
					}
					if (this.eDanhHieu != null)
					{
						this.eDanhHieu.update();
						this.eDanhHieu.x = this.cx;
						this.eDanhHieu.y = this.cH_new;
						if (this.eDanhHieu.timeExist <= mSystem.currentTimeMillis())
						{
							this.eDanhHieu = null;
							GameCanvas.danhHieu.remove(this.charID.ToString() + string.Empty);
						}
					}
				}
				if (this.charFocus != null && this.charFocus.cy < 0)
				{
					this.charFocus = null;
				}
				if (this.isFusion)
				{
					this.tFusion++;
				}
				if (this.isNhapThe && GameCanvas.gameTick % 25 == 0)
				{
					ServerEffect.addServerEffect(114, this, 1);
				}
				if (this.isSetPos)
				{
					this.tpos++;
					if (this.tpos != 1)
					{
						return;
					}
					this.tpos = 0;
					this.isSetPos = false;
					this.cx = (int)this.xPos;
					this.cy = (int)this.yPos;
					this.cp1 = (this.cp2 = (this.cp3 = 0));
					if (this.typePos == 1)
					{
						if (this.me)
						{
							this.cxSend = this.cx;
							this.cySend = this.cy;
						}
						this.currentMovePoint = null;
						this.telePortSkill = false;
						ServerEffect.addServerEffect(173, this.cx, this.cy, 1);
					}
					else
					{
						ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
					}
					if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
					{
						this.statusMe = 1;
						return;
					}
					this.statusMe = 4;
					return;
				}
				else
				{
					this.soundUpdate();
					if (this.stone)
					{
						return;
					}
					if (this.isFreez)
					{
						if (GameCanvas.gameTick % 5 == 0)
						{
							ServerEffect.addServerEffect(113, this.cx, this.cy, 1);
						}
						this.cf = 23;
						long num3 = mSystem.currentTimeMillis();
						if (num3 - this.lastFreez >= 1000L)
						{
							this.freezSeconds--;
							this.lastFreez = num3;
							if (this.freezSeconds < 0)
							{
								this.isFreez = false;
								this.seconds = 0;
								if (this.me)
								{
									Char.myCharz().isLockMove = false;
									GameScr.gI().dem = 0;
									GameScr.gI().isFreez = false;
								}
							}
						}
						if (TileMap.tileTypeAt(this.cx / (int)TileMap.size, this.cy / (int)TileMap.size) == 0)
						{
							this.ty++;
							this.wt++;
							this.fy += ((!this.wy) ? 1 : -1);
							if (this.wt == 10)
							{
								this.wt = 0;
								this.wy = !this.wy;
							}
						}
						return;
					}
					if (this.isWaitMonkey)
					{
						this.isLockMove = true;
						this.cf = 17;
						if (GameCanvas.gameTick % 5 == 0)
						{
							ServerEffect.addServerEffect(154, this.cx, this.cy - 10, 2);
						}
						if (GameCanvas.gameTick % 5 == 0)
						{
							ServerEffect.addServerEffect(1, this.cx, this.cy + 10, 1);
						}
						this.chargeCount++;
						if (this.chargeCount == 500)
						{
							this.isWaitMonkey = false;
							this.isLockMove = false;
						}
						return;
					}
					if (this.isStandAndCharge)
					{
						this.chargeCount++;
						bool flag = !TileMap.tileTypeAt(Char.myCharz().cx, Char.myCharz().cy, 2);
						this.updateEffect();
						this.updateSkillPaint();
						this.moveFast = null;
						this.currentMovePoint = null;
						this.cf = 17;
						if (flag && this.cgender != 2)
						{
							this.cf = 12;
						}
						if (this.cgender == 2)
						{
							if (GameCanvas.gameTick % 3 == 0)
							{
								ServerEffect.addServerEffect(154, this.cx, this.cy - this.ch / 2 + 10, 1);
							}
							if (GameCanvas.gameTick % 5 == 0)
							{
								ServerEffect.addServerEffect(114, this.cx + Res.random(-20, 20), this.cy + Res.random(-20, 20), 1);
							}
						}
						if (this.cgender == 1)
						{
							int num7 = GameCanvas.gameTick % 4;
							if (GameCanvas.gameTick % 2 == 0)
							{
								if (this.cdir == 1)
								{
									ServerEffect.addServerEffect(70, this.cx - 18, this.cy - this.ch / 2 + 8, 1);
									ServerEffect.addServerEffect(70, this.cx + 23, this.cy - this.ch / 2 + 15, 1);
								}
								else
								{
									ServerEffect.addServerEffect(70, this.cx + 18, this.cy - this.ch / 2 + 8, 1);
									ServerEffect.addServerEffect(70, this.cx - 23, this.cy - this.ch / 2 + 15, 1);
								}
							}
						}
						this.cur = mSystem.currentTimeMillis();
						if (this.cur - this.last > (long)this.seconds || this.cur - this.last > 10000L)
						{
							this.stopUseChargeSkill();
							if (this.me)
							{
								GameScr.gI().auto = 0;
								if (this.cgender == 2)
								{
									Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)Char.myCharz().myskill.skillId], flag ? 1 : 0);
									Service.gI().skill_not_focus(8);
								}
								if (this.cgender == 1)
								{
									this.isCreateDark = true;
									Char.myCharz().setSkillPaint(GameScr.sks[(int)Char.myCharz().myskill.skillId], flag ? 1 : 0);
								}
							}
							else if (this.cgender == 2)
							{
								this.setAutoSkillPaint(GameScr.sks[this.skillTemplateId], flag ? 1 : 0);
							}
							if (this.cgender == 2 && this.statusMe != 14 && this.statusMe != 5)
							{
								GameScr.gI().activeSuperPower(this.cx, this.cy);
							}
						}
						this.chargeCount++;
						if (this.chargeCount == 500)
						{
							this.stopUseChargeSkill();
						}
						return;
					}
					if (this.isFlyAndCharge)
					{
						this.updateEffect();
						this.updateSkillPaint();
						this.moveFast = null;
						this.currentMovePoint = null;
						this.posDisY++;
						if (TileMap.tileTypeAt(this.cx, this.cy - this.ch, 8192))
						{
							this.stopUseChargeSkill();
							return;
						}
						if (this.posDisY == 20)
						{
							this.last = mSystem.currentTimeMillis();
						}
						if (this.posDisY > 20)
						{
							this.cur = mSystem.currentTimeMillis();
							if (this.cur - this.last > (long)this.seconds || this.cur - this.last > 10000L)
							{
								this.isFlyAndCharge = false;
								if (this.me)
								{
									this.isCreateDark = true;
									bool flag2 = TileMap.tileTypeAt(Char.myCharz().cx, Char.myCharz().cy, 2);
									this.isUseSkillAfterCharge = true;
									Char.myCharz().setSkillPaint(GameScr.sks[(int)Char.myCharz().myskill.skillId], (!flag2) ? 1 : 0);
								}
								return;
							}
							this.cf = 32;
							if (this.cgender == 0 && GameCanvas.gameTick % 3 == 0)
							{
								ServerEffect.addServerEffect(153, this.cx, this.cy - this.ch, 2);
							}
							this.chargeCount++;
							if (this.chargeCount == 500)
							{
								this.stopUseChargeSkill();
								return;
							}
						}
						else
						{
							if (this.statusMe != 14)
							{
								this.statusMe = 3;
							}
							this.cvy = -3;
							this.cy += this.cvy;
							this.cf = 7;
						}
						return;
					}
					else
					{
						if (this.me && GameCanvas.isTouch)
						{
							if (this.charFocus != null && this.charFocus.charID >= 0 && this.charFocus.cx > 100 && this.charFocus.cx < TileMap.pxw - 100 && this.isInEnterOnlinePoint() == null && this.isInEnterOfflinePoint() == null && !this.isAttacPlayerStatus() && TileMap.mapID != 51 && TileMap.mapID != 52 && GameCanvas.panel.vPlayerMenu.size() > 0 && GameScr.gI().popUpYesNo == null)
							{
								int num8 = Math.abs(this.cx - this.charFocus.cx);
								int num4 = Math.abs(this.cy - this.charFocus.cy);
								if (num8 < 60 && num4 < 40)
								{
									if (this.cmdMenu == null)
									{
										this.cmdMenu = new Command(mResources.MENU, 11111);
										this.cmdMenu.isPlaySoundButton = false;
									}
									this.cmdMenu.x = this.charFocus.cx - GameScr.cmx;
									this.cmdMenu.y = this.charFocus.cy - this.charFocus.ch - 30 - GameScr.cmy;
								}
								else
								{
									this.cmdMenu = null;
								}
							}
							else
							{
								this.cmdMenu = null;
							}
						}
						if (this.isShadown)
						{
							this.updateShadown();
						}
						if (this.isTeleport)
						{
							return;
						}
						if (this.chatInfo != null)
						{
							this.chatInfo.update();
						}
						if (this.shadowLife > 0)
						{
							this.shadowLife--;
						}
						if (this.resultTest > 0 && GameCanvas.gameTick % 2 == 0)
						{
							this.resultTest -= 1;
							if (this.resultTest == 30 || this.resultTest == 60)
							{
								this.resultTest = 0;
							}
						}
						this.updateSkillPaint();
						if (this.mobMe != null)
						{
							this.updateMobMe();
						}
						if (this.arr != null)
						{
							this.arr.update();
						}
						if (this.dart != null)
						{
							this.dart.update();
						}
						this.updateEffect();
						if (this.holdEffID != 0)
						{
							if (GameCanvas.gameTick % 5 == 0)
							{
								EffecMn.addEff(new Effect(32, this.cx, this.cy + 24, 3, 5, 1));
								return;
							}
						}
						else
						{
							if (this.blindEff || this.sleepEff)
							{
								return;
							}
							if (this.holder)
							{
								if (this.charHold != null && (this.charHold.statusMe == 14 || this.charHold.statusMe == 5))
								{
									this.removeHoleEff();
								}
								if (this.mobHold != null && this.mobHold.status == 1)
								{
									this.removeHoleEff();
								}
								if (this.me && this.statusMe == 2 && this.currentMovePoint != null)
								{
									this.holder = false;
									this.charHold = null;
									this.mobHold = null;
								}
								if (TileMap.tileTypeAt(this.cx, this.cy, 2))
								{
									this.cf = 16;
									return;
								}
								this.cf = 31;
								return;
							}
							else
							{
								if (this.cHP > 0L)
								{
									for (int i = 0; i < this.vEff.size(); i++)
									{
										EffectChar effectChar = (EffectChar)this.vEff.elementAt(i);
										if (effectChar.template.type == 0 || effectChar.template.type == 12)
										{
											if (GameCanvas.isEff1)
											{
												this.cHP += (long)effectChar.param;
												this.cMP += (long)effectChar.param;
											}
										}
										else if (effectChar.template.type == 4 || effectChar.template.type == 17)
										{
											if (GameCanvas.isEff1)
											{
												this.cHP += (long)effectChar.param;
											}
										}
										else if (effectChar.template.type == 13 && GameCanvas.isEff1)
										{
											this.cHP -= this.cHPFull * 3L / 100L;
											if (this.cHP < 1L)
											{
												this.cHP = 1L;
											}
										}
									}
									if (this.eff5BuffHp > 0 && GameCanvas.isEff2)
									{
										this.cHP += (long)this.eff5BuffHp;
									}
									if (this.eff5BuffMp > 0 && GameCanvas.isEff2)
									{
										this.cMP += (long)this.eff5BuffMp;
									}
									if (this.cHP > this.cHPFull)
									{
										this.cHP = this.cHPFull;
									}
									if (this.cMP > this.cMPFull)
									{
										this.cMP = this.cMPFull;
									}
								}
								if (this.cmtoChar)
								{
									GameScr.cmtoX = this.cx - GameScr.gW2;
									GameScr.cmtoY = this.cy - GameScr.gH23;
									if (!GameCanvas.isTouchControl)
									{
										GameScr.cmtoX += GameScr.gW6 * this.cdir;
									}
								}
								this.tick = (this.tick + 1) % 100;
								if (this.me)
								{
									if (this.charFocus != null && !GameScr.vCharInMap.contains(this.charFocus))
									{
										this.charFocus = null;
									}
									if (this.cx < 10)
									{
										this.cvx = 0;
										this.cx = 10;
									}
									else if (this.cx > TileMap.pxw - 10)
									{
										this.cx = TileMap.pxw - 10;
										this.cvx = 0;
									}
									if (this.me && !Char.ischangingMap && this.isInWaypoint())
									{
										Service.gI().charMove();
										if (TileMap.isTrainingMap())
										{
											Service.gI().getMapOffline();
											Char.ischangingMap = true;
										}
										else
										{
											Service.gI().requestChangeMap();
										}
										Char.isLockKey = true;
										Char.ischangingMap = true;
										GameCanvas.clearKeyHold();
										GameCanvas.clearKeyPressed();
										InfoDlg.showWait();
										return;
									}
									if (this.statusMe != 4 && Res.abs(this.cx - this.cxSend) + Res.abs(this.cy - this.cySend) >= 70 && this.cy - this.cySend <= 0 && this.me)
									{
										Service.gI().charMove();
									}
									if (this.isLockMove)
									{
										this.currentMovePoint = null;
									}
									if (this.currentMovePoint != null)
									{
										if (Char.abs(this.cx - this.currentMovePoint.xEnd) <= 16 && Char.abs(this.cy - this.currentMovePoint.yEnd) <= 16)
										{
											this.cx = (this.currentMovePoint.xEnd + this.cx) / 2;
											this.cy = this.currentMovePoint.yEnd;
											this.currentMovePoint = null;
											GameScr.instance.clickMoving = false;
											this.checkPerformEndMovePointAction();
											this.cvx = (this.cvy = 0);
											if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
											{
												this.statusMe = 1;
											}
											else
											{
												this.setCharFallFromJump();
											}
											Service.gI().charMove();
										}
										else
										{
											this.cdir = ((this.currentMovePoint.xEnd > this.cx) ? 1 : -1);
											if (TileMap.tileTypeAt(this.cx, this.cy, 2))
											{
												this.statusMe = 2;
												if (this.currentMovePoint != null)
												{
													this.cvx = this.cspeed * this.cdir;
													this.cvy = 0;
												}
												if (Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10)
												{
													if (this.currentMovePoint.yEnd > this.cy)
													{
														bool flag3 = false;
														sbyte b = 1;
														b = (sbyte)((cdir == 1) ? 1 : (-1));
														for (int j = 0; j < 2; j++)
														{
															if (TileMap.tileTypeAt(this.currentMovePoint.xEnd + this.chw * (int)b, this.cy + this.chh * j, 2))
															{
																flag3 = true;
																break;
															}
														}
														if (flag3)
														{
															this.currentMovePoint = null;
															GameScr.instance.clickMoving = false;
															this.statusMe = 1;
															this.cvx = (this.cvy = 0);
															this.checkPerformEndMovePointAction();
														}
														else
														{
															SoundMn.gI().charJump();
															this.cx = this.currentMovePoint.xEnd;
															this.statusMe = 10;
															this.cvy = -5;
															this.cvx = 0;
														}
													}
													else
													{
														SoundMn.gI().charJump();
														this.cx = this.currentMovePoint.xEnd;
														this.statusMe = 10;
														this.cvy = -5;
														this.cvx = 0;
													}
												}
												if (this.cdir == 1)
												{
													if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
													{
														this.cvx = this.cspeed * this.cdir;
														this.statusMe = 10;
														this.cvy = -5;
													}
												}
												else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
												{
													this.cvx = this.cspeed * this.cdir;
													this.statusMe = 10;
													this.cvy = -5;
												}
											}
											else
											{
												if (this.currentMovePoint.yEnd < this.cy + 10)
												{
													this.statusMe = 10;
													this.cvy = -5;
													if (Char.abs(this.cy - this.currentMovePoint.yEnd) <= 10)
													{
														this.cy = this.currentMovePoint.yEnd;
														this.cvy = 0;
													}
													if (Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10)
													{
														this.cvx = 0;
													}
													else
													{
														this.cvx = this.cspeed * this.cdir;
													}
												}
												else if (TileMap.tileTypeAt(this.cx, this.cy, 2))
												{
													this.currentMovePoint = null;
													GameScr.instance.clickMoving = false;
													this.statusMe = 1;
													this.cvx = (this.cvy = 0);
													this.checkPerformEndMovePointAction();
												}
												else
												{
													if (this.statusMe == 10 || this.statusMe == 2)
													{
														this.cvy = 0;
													}
													this.statusMe = 4;
												}
												if (this.currentMovePoint.yEnd > this.cy)
												{
													if (this.cdir == 1)
													{
														if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
														{
															this.cvx = (this.cvy = 0);
															this.statusMe = 4;
															this.currentMovePoint = null;
															GameScr.instance.clickMoving = false;
															this.checkPerformEndMovePointAction();
														}
													}
													else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
													{
														this.cvx = (this.cvy = 0);
														this.statusMe = 4;
														this.currentMovePoint = null;
														GameScr.instance.clickMoving = false;
														this.checkPerformEndMovePointAction();
													}
												}
											}
										}
									}
									this.searchFocus();
								}
								else
								{
									this.checkHideCharName();
									if (this.statusMe == 1 || this.statusMe == 6)
									{
										bool flag4 = false;
										if (this.currentMovePoint != null)
										{
											if (Char.abs(this.currentMovePoint.xEnd - this.cx) < 17 && Char.abs(this.currentMovePoint.yEnd - this.cy) < 25)
											{
												this.cx = this.currentMovePoint.xEnd;
												this.cy = this.currentMovePoint.yEnd;
												this.currentMovePoint = null;
												if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
												{
													this.statusMe = 1;
													this.cp3 = 0;
													GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
													GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
												}
												else
												{
													this.statusMe = 4;
													this.cvy = 0;
													this.cp1 = 0;
												}
												flag4 = true;
											}
											else if ((this.statusBeforeNothing == 10 || this.cf == 8) && this.vMovePoints.size() > 0)
											{
												flag4 = true;
											}
											else if (this.cy == this.currentMovePoint.yEnd)
											{
												if (this.cx != this.currentMovePoint.xEnd)
												{
													this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
													this.cf = GameCanvas.gameTick % 5 + 2;
												}
											}
											else if (this.cy < this.currentMovePoint.yEnd)
											{
												this.cf = 12;
												this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
												if (this.cvy < 0)
												{
													this.cvy = 0;
												}
												this.cy += this.cvy;
												if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
												{
													GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
													GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
												}
												this.cvy++;
												if (this.cvy > 16)
												{
													this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
												}
											}
											else
											{
												this.cf = 7;
												this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
												this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
											}
										}
										else
										{
											flag4 = true;
										}
										if (flag4 && this.vMovePoints.size() > 0)
										{
											this.currentMovePoint = (MovePoint)this.vMovePoints.firstElement();
											this.vMovePoints.removeElementAt(0);
											if (this.currentMovePoint.status == 2)
											{
												if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 12) & 2) != 2)
												{
													this.statusMe = 10;
													this.cp1 = 0;
													this.cp2 = 0;
													this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
													this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
													if (this.cx - this.currentMovePoint.xEnd > 0)
													{
														this.cdir = -1;
													}
													else if (this.cx - this.currentMovePoint.xEnd < 0)
													{
														this.cdir = 1;
													}
												}
												else
												{
													this.statusMe = 2;
													if (this.cx - this.currentMovePoint.xEnd > 0)
													{
														this.cdir = -1;
													}
													else if (this.cx - this.currentMovePoint.xEnd < 0)
													{
														this.cdir = 1;
													}
													this.cvx = this.cspeed * this.cdir;
													this.cvy = 0;
												}
											}
											else if (this.currentMovePoint.status == 3)
											{
												if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 23) & 2) != 2)
												{
													this.statusMe = 10;
													this.cp1 = 0;
													this.cp2 = 0;
													this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
													this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
													if (this.cx - this.currentMovePoint.xEnd > 0)
													{
														this.cdir = -1;
													}
													else if (this.cx - this.currentMovePoint.xEnd < 0)
													{
														this.cdir = 1;
													}
												}
												else
												{
													this.statusMe = 3;
													GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
													GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
													if (this.cx - this.currentMovePoint.xEnd > 0)
													{
														this.cdir = -1;
													}
													else if (this.cx - this.currentMovePoint.xEnd < 0)
													{
														this.cdir = 1;
													}
													this.cvx = Char.abs(this.cx - this.currentMovePoint.xEnd) / 10 * this.cdir;
													this.cvy = -10;
												}
											}
											else if (this.currentMovePoint.status == 4)
											{
												this.statusMe = 4;
												if (this.cx - this.currentMovePoint.xEnd > 0)
												{
													this.cdir = -1;
												}
												else if (this.cx - this.currentMovePoint.xEnd < 0)
												{
													this.cdir = 1;
												}
												this.cvx = Char.abs(this.cx - this.currentMovePoint.xEnd) / 9 * this.cdir;
												this.cvy = 0;
											}
											else
											{
												this.cx = this.currentMovePoint.xEnd;
												this.cy = this.currentMovePoint.yEnd;
												this.currentMovePoint = null;
											}
										}
									}
								}
								switch (this.statusMe)
								{
									case 1:
										this.updateCharStand();
										break;
									case 2:
										this.updateCharRun();
										break;
									case 3:
										this.updateCharJump();
										break;
									case 4:
										this.updateCharFall();
										break;
									case 5:
										this.updateCharDeadFly();
										break;
									case 6:
										if (this.isInjure <= 0)
										{
											this.cf = 0;
										}
										else if (this.statusBeforeNothing == 10)
										{
											this.cx += this.cvx;
										}
										else if (this.cf <= 1)
										{
											this.cp1++;
											if (this.cp1 > 6)
											{
												this.cf = 0;
											}
											else
											{
												this.cf = 1;
											}
											if (this.cp1 > 10)
											{
												this.cp1 = 0;
											}
										}
										if (this.cf != 7 && this.cf != 12 && (TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2)
										{
											this.cvx = 0;
											this.cvy = 0;
											this.statusMe = 4;
											this.cf = 7;
										}
										if (!this.me)
										{
											this.cp3++;
											if (this.cp3 > 10)
											{
												if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2)
												{
													this.cy += 5;
												}
												else
												{
													this.cf = 0;
												}
											}
											if (this.cp3 > 50)
											{
												this.cp3 = 0;
												this.currentMovePoint = null;
											}
										}
										break;
									case 9:
										this.updateCharAutoJump();
										break;
									case 10:
										this.updateCharFly();
										break;
									case 12:
										this.updateSkillStand();
										break;
									case 13:
										this.updateSkillFall();
										break;
									case 14:
										this.cp1++;
										if (this.cp1 > 30)
										{
											this.cp1 = 0;
										}
										if (this.cp1 % 15 < 5)
										{
											this.cf = 0;
										}
										else
										{
											this.cf = 1;
										}
										break;
									case 16:
										this.updateResetPoint();
										break;
								}
								if (this.isInjure > 0)
								{
									this.cf = 23;
									this.isInjure -= 1;
								}
								if (this.wdx != 0 || this.wdy != 0)
								{
									this.startDie(this.wdx, this.wdy);
									this.wdx = 0;
									this.wdy = 0;
								}
								if (this.moveFast != null)
								{
									if (this.moveFast[0] == 0)
									{
										short[] array2 = this.moveFast;
										int num9 = 0;
										array2[num9] += 1;
										ServerEffect.addServerEffect(60, this, 1);
									}
									else if (this.moveFast[0] < 10)
									{
										short[] array3 = this.moveFast;
										int num10 = 0;
										array3[num10] += 1;
									}
									else
									{
										this.cx = (int)this.moveFast[1];
										this.cy = (int)this.moveFast[2];
										this.moveFast = null;
										ServerEffect.addServerEffect(60, this, 1);
										if (this.me)
										{
											if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
											{
												this.statusMe = 4;
											}
											else
											{
												Service.gI().charMove();
											}
										}
									}
								}
								if (this.statusMe != 10)
								{
									this.fy = 0;
								}
								if (this.isCharge)
								{
									this.cf = 17;
									if (GameCanvas.gameTick % 4 == 0)
									{
										ServerEffect.addServerEffect(1, this.cx, this.cy + GameCanvas.transY, 1);
									}
									if (this.me)
									{
										long num5 = mSystem.currentTimeMillis();
										if (num5 - this.last >= 1000L)
										{
											this.last = num5;
											this.cHP += this.cHPFull * (long)this.myskill.damage / 100L;
											this.cMP += this.cMPFull * (long)this.myskill.damage / 100L;
											if (this.cHP < this.cHPFull)
											{
												GameScr.startFlyText("+" + (this.cHPFull * (long)this.myskill.damage / 100L).ToString() + " " + mResources.HP, this.cx, this.cy - this.ch - 20, 0, -1, mFont.HP);
											}
											if (this.cMP < this.cMPFull)
											{
												GameScr.startFlyText("+" + (this.cMPFull * (long)this.myskill.damage / 100L).ToString() + " " + mResources.KI, this.cx, this.cy - this.ch - 20, 0, -2, mFont.MP);
											}
											Service.gI().skill_not_focus(2);
										}
									}
								}
								if (this.isFlyUp)
								{
									if (this.me)
									{
										Char.isLockKey = true;
										this.statusMe = 3;
										this.cvy = -8;
										if (this.cy <= TileMap.pxh - 240)
										{
											this.isFlyUp = false;
											Char.isLockKey = false;
											this.statusMe = 4;
										}
									}
									else
									{
										this.statusMe = 3;
										this.cvy = -8;
										if (this.cy <= TileMap.pxh - 240)
										{
											this.cvy = 0;
											this.isFlyUp = false;
											this.cvy = 0;
											this.statusMe = 1;
										}
									}
								}
								this.updateMount();
								this.updEffChar();
								this.updateEye();
								this.updateFHead();
							}
						}
					}
				}
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0025E1E8 File Offset: 0x0025C3E8
		private void updateEffect()
		{
			if (this.effPaints != null)
			{
				for (int i = 0; i < this.effPaints.Length; i++)
				{
					if (this.effPaints[i] != null)
					{
						if (this.effPaints[i].eMob != null)
						{
							if (!this.effPaints[i].isFly)
							{
								this.effPaints[i].eMob.setInjure();
								this.effPaints[i].eMob.injureBy = this;
								if (this.me)
								{
									this.effPaints[i].eMob.hpInjure = Char.myCharz().cDamFull / 2L - Char.myCharz().cDamFull * (long)NinjaUtil.randomNumber(11) / 100L;
								}
								int num = this.effPaints[i].eMob.h >> 1;
								if (this.effPaints[i].eMob.isBigBoss())
								{
									num = this.effPaints[i].eMob.getY() + 20;
								}
								GameScr.startSplash(this.effPaints[i].eMob.x, this.effPaints[i].eMob.y - num, this.cdir);
								this.effPaints[i].isFly = true;
							}
						}
						else if (this.effPaints[i].eChar != null && !this.effPaints[i].isFly)
						{
							if (this.effPaints[i].eChar.charID >= 0)
							{
								this.effPaints[i].eChar.doInjure();
							}
							GameScr.startSplash(this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy - (this.effPaints[i].eChar.ch >> 1), this.cdir);
							this.effPaints[i].isFly = true;
						}
						this.effPaints[i].index++;
						if (this.effPaints[i].index >= this.effPaints[i].effCharPaint.arrEfInfo.Length)
						{
							this.effPaints[i] = null;
						}
					}
				}
			}
			if (this.indexEff >= 0 && this.eff != null && GameCanvas.gameTick % 2 == 0)
			{
				this.indexEff++;
				if (this.indexEff >= this.eff.arrEfInfo.Length)
				{
					this.indexEff = -1;
					this.eff = null;
				}
			}
			if (this.indexEffTask >= 0 && this.effTask != null && GameCanvas.gameTick % 2 == 0)
			{
				this.indexEffTask++;
				if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
				{
					this.indexEffTask = -1;
					this.effTask = null;
				}
			}
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0025E4A4 File Offset: 0x0025C6A4
		private void checkPerformEndMovePointAction()
		{
			if (this.endMovePointCommand != null)
			{
				Command command = this.endMovePointCommand;
				this.endMovePointCommand = null;
				command.performAction();
			}
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0025E4C0 File Offset: 0x0025C6C0
		private void checkHideCharName()
		{
			if (GameCanvas.gameTick % 20 != 0 || this.charID < 0)
			{
				return;
			}
			this.paintName = true;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = null;
				try
				{
					@char = (Char)GameScr.vCharInMap.elementAt(i);
				}
				catch (Exception)
				{
				}
				if (@char != null && !@char.Equals(this) && ((@char.cy == this.cy && Res.abs(@char.cx - this.cx) < 35) || (this.cy - @char.cy < 32 && this.cy - @char.cy > 0 && Res.abs(@char.cx - this.cx) < 24)))
				{
					this.paintName = false;
				}
			}
			for (int j = 0; j < GameScr.vNpc.size(); j++)
			{
				Npc npc = null;
				try
				{
					npc = (Npc)GameScr.vNpc.elementAt(j);
				}
				catch (Exception)
				{
				}
				if (npc != null && npc.cy == this.cy && Res.abs(npc.cx - this.cx) < 24)
				{
					this.paintName = false;
				}
			}
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0025E600 File Offset: 0x0025C800
		private void updateMobMe()
		{
			if (this.tMobMeBorn != 0)
			{
				this.tMobMeBorn--;
			}
			if (this.tMobMeBorn == 0)
			{
				this.mobMe.xFirst = ((this.cdir != 1) ? (this.cx + 30) : (this.cx - 30));
				this.mobMe.yFirst = this.cy - 60;
				int num = this.mobMe.xFirst - this.mobMe.x;
				int num2 = this.mobMe.yFirst - this.mobMe.y;
				this.mobMe.x += num / 4;
				this.mobMe.y += num2 / 4;
				this.mobMe.dir = this.cdir;
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0025E6D8 File Offset: 0x0025C8D8
		private void updateSkillPaint()
		{
			if (this.statusMe == 14 || this.statusMe == 5)
			{
				return;
			}
			if (this.skillPaint != null && ((this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus) && this.charFocus.statusMe == 14) || (this.mobFocus != null && this.mobFocus.status == 0)))
			{
				if (!this.me)
				{
					if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
					{
						this.statusMe = 1;
					}
					else
					{
						this.statusMe = 6;
					}
					this.cp3 = 0;
				}
				this.indexSkill = 0;
				this.skillPaint = null;
				this.skillPaintRandomPaint = null;
				this.eff0 = (this.eff1 = (this.eff2 = null));
				this.i0 = (this.i1 = (this.i2 = 0));
				this.mobFocus = null;
				this.charFocus = null;
				this.effPaints = null;
				this.currentMovePoint = null;
				this.arr = null;
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
				{
					this.delayFall = 5;
				}
			}
			if (this.skillPaint != null && this.arr == null && this.skillInfoPaint() != null && this.indexSkill >= this.skillInfoPaint().Length)
			{
				if (!this.me)
				{
					if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
					{
						this.statusMe = 1;
					}
					else
					{
						this.statusMe = 6;
					}
					this.cp3 = 0;
				}
				this.indexSkill = 0;
				this.skillPaint = null;
				this.skillPaintRandomPaint = null;
				this.eff0 = (this.eff1 = (this.eff2 = null));
				this.i0 = (this.i1 = (this.i2 = 0));
				this.arr = null;
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
				{
					this.delayFall = 5;
				}
			}
			SkillInfoPaint[] array = this.skillInfoPaint();
			if (array == null || this.indexSkill < 0 || this.indexSkill > array.Length - 1)
			{
				return;
			}
			if (array[this.indexSkill].effS0Id != 0)
			{
				this.eff0 = GameScr.efs[array[this.indexSkill].effS0Id - 1];
				this.i0 = (this.dx0 = (this.dy0 = 0));
			}
			if (array[this.indexSkill].effS1Id != 0)
			{
				this.eff1 = GameScr.efs[array[this.indexSkill].effS1Id - 1];
				this.i1 = (this.dx1 = (this.dy1 = 0));
			}
			if (array[this.indexSkill].effS2Id != 0)
			{
				this.eff2 = GameScr.efs[array[this.indexSkill].effS2Id - 1];
				this.i2 = (this.dx2 = (this.dy2 = 0));
			}
			SkillInfoPaint[] array2 = array;
			int num = this.indexSkill;
			if (array2 != null && array2[num] != null && num >= 0 && num <= array2.Length - 1 && array2[num].arrowId != 0)
			{
				int arrowId = array2[num].arrowId;
				if (arrowId >= 100)
				{
					IMapObject mapObject5;
					if (this.mobFocus == null)
					{
						IMapObject mapObject4 = this.charFocus;
						mapObject5 = mapObject4;
					}
					else
					{
						IMapObject mapObject4 = this.mobFocus;
						mapObject5 = mapObject4;
					}
					IMapObject mapObject2 = (IMapObject)mapObject5;
					if (mapObject2 != null)
					{
						int num4 = Res.abs(mapObject2.getX() - this.cx);
						int num2 = Res.abs(mapObject2.getY() - this.cy);
						int num3;
						if (num4 > 4 * num2)
						{
							num3 = 0;
						}
						else
						{
							num3 = ((mapObject2.getY() >= this.cy) ? 3 : -3);
							if (mapObject2 is BigBoss && ((BigBoss)mapObject2).haftBody)
							{
								num3 = -20;
							}
						}
						this.dart = new PlayerDart(this, arrowId - 100, this.skillPaintRandomPaint, this.cx + (array2[num].adx - 10) * this.cdir, this.cy + array2[num].ady + num3);
						if (this.myskill != null)
						{
							if (this.myskill.template.id == 1)
							{
								SoundMn.gI().traidatKame();
							}
							else if (this.myskill.template.id == 3)
							{
								SoundMn.gI().namekKame();
							}
							else if (this.myskill.template.id == 5)
							{
								SoundMn.gI().xaydaKame();
							}
							else if (this.myskill.template.id == 11)
							{
								SoundMn.gI().nameLazer();
							}
						}
					}
					else if (this.isFlyAndCharge || this.isUseSkillAfterCharge)
					{
						this.stopUseChargeSkill();
					}
				}
				else
				{
					this.arr = new Arrow(this, GameScr.arrs[arrowId - 1])
					{
						life = 10,
						ax = this.cx + array2[num].adx,
						ay = this.cy + array2[num].ady
					};
				}
			}
			if ((this.mobFocus != null || (!this.me && this.charFocus != null) || (this.me && this.charFocus != null && (this.isMeCanAttackOtherPlayer(this.charFocus) || this.isSelectingSkillBuffToPlayer()) && this.arr == null && this.dart == null)) && this.indexSkill == array.Length - 1)
			{
				this.setAttack();
				if (this.me && this.myskill.template.isAttackSkill())
				{
					this.saveLoadPreviousSkill();
				}
			}
			if (this.me)
			{
				return;
			}
			IMapObject mapObject3 = null;
			if (this.mobFocus != null)
			{
				mapObject3 = this.mobFocus;
			}
			else if (this.charFocus != null)
			{
				mapObject3 = this.charFocus;
			}
			if (mapObject3 == null)
			{
				return;
			}
			if (Res.abs(mapObject3.getX() - this.cx) < 10)
			{
				if (mapObject3.getX() > this.cx)
				{
					this.cx -= 10;
				}
				else
				{
					this.cx += 10;
				}
			}
			if (mapObject3.getX() > this.cx)
			{
				this.cdir = 1;
				return;
			}
			this.cdir = -1;
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000034B9 File Offset: 0x000016B9
		public void saveLoadPreviousSkill()
		{
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x0025ECFC File Offset: 0x0025CEFC
		public void setResetPoint(int x, int y)
		{
			InfoDlg.hide();
			this.currentMovePoint = null;
			int num = this.cx;
			if (this.cy - y == 0)
			{
				this.cx = x;
				Char.ischangingMap = false;
				Char.isLockKey = false;
				return;
			}
			this.statusMe = 16;
			this.cp2 = x;
			this.cp3 = y;
			this.cp1 = 0;
			Char.myCharz().cxSend = x;
			Char.myCharz().cySend = y;
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x0025ED70 File Offset: 0x0025CF70
		private void updateCharDeadFly()
		{
			this.isFreez = false;
			if (this.isCharge)
			{
				this.isCharge = false;
				SoundMn.gI().taitaoPause();
				Service.gI().skill_not_focus(3);
			}
			this.cp1++;
			this.cx += (this.cp2 - this.cx) / 4;
			if (this.cp1 > 7)
			{
				this.cy += (this.cp3 - this.cy) / 4;
			}
			else
			{
				this.cy += this.cp1 - 10;
			}
			if (Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10)
			{
				this.cx = this.cp2;
				this.cy = this.cp3;
				this.statusMe = 14;
				if (this.me)
				{
					GameScr.gI().resetButton();
					Service.gI().charMove();
				}
			}
			this.cf = 23;
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x0025EE80 File Offset: 0x0025D080
		private void updateResetPoint()
		{
			InfoDlg.hide();
			GameCanvas.clearAllPointerEvent();
			this.currentMovePoint = null;
			this.cp1++;
			this.cx += (this.cp2 - this.cx) / 4;
			if (this.cp1 > 7)
			{
				this.cy += (this.cp3 - this.cy) / 4;
			}
			else
			{
				this.cy += this.cp1 - 10;
			}
			if (Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10)
			{
				this.cx = this.cp2;
				this.cy = this.cp3;
				this.statusMe = 1;
				this.cp3 = 0;
				Char.ischangingMap = false;
				Service.gI().charMove();
			}
			this.cf = 23;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000034B9 File Offset: 0x000016B9
		public void updateSkillFall()
		{
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x0025EF70 File Offset: 0x0025D170
		public void updateSkillStand()
		{
			this.ty = 0;
			this.cp1++;
			if (this.cdir == 1)
			{
				if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - this.chh) & 4) == 4)
				{
					this.cvx = 0;
				}
			}
			else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - this.chh) & 8) == 8)
			{
				this.cvx = 0;
			}
			if (this.cy > this.ch && TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192))
			{
				if (!TileMap.tileTypeAt(this.cx, this.cy, 2))
				{
					this.statusMe = 4;
					this.cp1 = 0;
					this.cp2 = 0;
					this.cvy = 1;
				}
				else
				{
					this.cy = TileMap.tileYofPixel(this.cy);
				}
			}
			this.cx += this.cvx;
			this.cy += this.cvy;
			if (this.cy < 0)
			{
				this.cy = (this.cvy = 0);
			}
			if (this.cvy == 0)
			{
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
				{
					this.statusMe = 4;
					this.cvx = (this.cspeed >> 1) * this.cdir;
					this.cp1 = (this.cp2 = 0);
				}
			}
			else if (this.cvy < 0)
			{
				this.cvy++;
				if (this.cvy == 0)
				{
					this.cvy = 1;
				}
			}
			else
			{
				if (this.cvy < 20 && this.cp1 % 5 == 0)
				{
					this.cvy++;
				}
				if (this.cvy > 3)
				{
					this.cvy = 3;
				}
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2 && this.cy <= TileMap.tileXofPixel(this.cy + 3))
				{
					this.cvx = (this.cvy = 0);
					this.cy = TileMap.tileXofPixel(this.cy + 3);
				}
			}
			if (this.cvx > 0)
			{
				this.cvx--;
				return;
			}
			if (this.cvx < 0)
			{
				this.cvx++;
			}
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0025F1D8 File Offset: 0x0025D3D8
		public void updateCharAutoJump()
		{
			this.isFreez = false;
			if (this.isCharge)
			{
				this.isCharge = false;
				SoundMn.gI().taitaoPause();
				Service.gI().skill_not_focus(3);
			}
			this.cx += this.cvx * this.cdir;
			this.cy += this.cvyJump;
			this.cvyJump++;
			if (this.cp1 == 0)
			{
				this.cf = 7;
			}
			else
			{
				this.cf = 23;
			}
			if (this.cvyJump == -3)
			{
				this.cf = 8;
			}
			else if (this.cvyJump == -2)
			{
				this.cf = 9;
			}
			else if (this.cvyJump == -1)
			{
				this.cf = 10;
			}
			else if (this.cvyJump == 0)
			{
				this.cf = 11;
			}
			if (this.cvyJump == 0)
			{
				this.statusMe = 6;
				this.cp3 = 0;
				((MovePoint)this.vMovePoints.firstElement()).status = 4;
				this.isJump = true;
				this.cp1 = 0;
				this.cvy = 1;
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0025F2EF File Offset: 0x0025D4EF
		public void hide()
		{
			this.isHide = true;
			EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 15, 1));
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0025F317 File Offset: 0x0025D517
		public void show()
		{
			this.isHide = false;
			EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 10, 1));
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0025F340 File Offset: 0x0025D540
		public void updateCharStand()
		{
			this.isSoundJump = false;
			this.isAttack = false;
			this.isAttFly = false;
			this.cvx = 0;
			this.cvy = 0;
			this.cp1++;
			if (this.cp1 > 30)
			{
				this.cp1 = 0;
			}
			if (this.cp1 % 15 < 5)
			{
				this.cf = 0;
			}
			else
			{
				this.cf = 1;
			}
			this.updateCharInBridge();
			if (!this.me)
			{
				this.cp3++;
				if (this.cp3 > 50)
				{
					this.cp3 = 0;
					this.currentMovePoint = null;
				}
			}
			this.updateSuperEff();
			if (!this.me || GameScr.vCharInMap.size() == 0 || TileMap.mapID != 50)
			{
				return;
			}
			Char @char = (Char)GameScr.vCharInMap.elementAt(0);
			if (!@char.changePos)
			{
				if (@char.statusMe != 2)
				{
					@char.moveTo(this.cx - 45, this.cy, 0);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				if (Res.abs(this.cx - 45 - @char.cx) <= 10)
				{
					@char.changePos = true;
				}
			}
			else
			{
				if (@char.statusMe != 2)
				{
					@char.moveTo(this.cx + 45, this.cy, 0);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				if (Res.abs(this.cx + 45 - @char.cx) <= 10)
				{
					@char.changePos = false;
				}
			}
			if (GameCanvas.gameTick % 100 == 0)
			{
				@char.addInfo("Cắc cùm cum");
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0025F4C8 File Offset: 0x0025D6C8
		public void updateSuperEff()
		{
			if (ModFunc.GiamDungLuong || GameCanvas.panel.isShow || this.isCopy || this.isFusion || this.isSetPos || this.isPet || this.isMiniPet || this.isMonkey == 1)
			{
				return;
			}
			if (this.me)
			{
				if (!Char.isPaintAura && this.idAuraEff > -1)
				{
					return;
				}
			}
			else if (this.idAuraEff > -1)
			{
				return;
			}
			this.ty++;
			if (this.clevel >= 14)
			{
				return;
			}
			if (this.clevel >= 9 && !GameCanvas.lowGraphic && (this.ty == 40 || this.ty == 50))
			{
				GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
				GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
				this.addDustEff(1);
			}
			if (this.ty <= 50 || this.clevel < 9)
			{
				return;
			}
			if (this.cgender == 0)
			{
				if (GameCanvas.gameTick % 25 == 0)
				{
					ServerEffect.addServerEffect(114, this, 1);
				}
				if (this.clevel >= 13 && GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(132, this, 1);
				}
			}
			if (this.cgender == 1)
			{
				if (GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(132, this, 1);
				}
				if (this.clevel >= 13 && GameCanvas.gameTick % 7 == 0)
				{
					ServerEffect.addServerEffect(131, this, 1);
				}
			}
			if (this.cgender == 2)
			{
				if (GameCanvas.gameTick % 7 == 0)
				{
					ServerEffect.addServerEffect(131, this, 1);
				}
				if (this.clevel >= 13 && GameCanvas.gameTick % 25 == 0)
				{
					ServerEffect.addServerEffect(114, this, 1);
				}
			}
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0025F680 File Offset: 0x0025D880
		public float getSoundVolumn()
		{
			if (this.me)
			{
				return 0.1f;
			}
			int num = Res.abs(Char.myChar.cx - this.cx);
			if (num >= 0 && num <= 50)
			{
				return 0.1f;
			}
			return 0.05f;
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0025F6C8 File Offset: 0x0025D8C8
		public void updateCharRun()
		{
			int num = (this.isMonkey != 1 || this.me) ? 1 : 2;
			if (this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w)
			{
				if (this.isMonkey == 0)
				{
					SoundMn.gI().charRun(this.getSoundVolumn());
				}
				else
				{
					SoundMn.gI().monkeyRun(this.getSoundVolumn());
				}
			}
			this.ty = 0;
			this.isFreez = false;
			if (this.isCharge)
			{
				this.isCharge = false;
				SoundMn.gI().taitaoPause();
				Service.gI().skill_not_focus(3);
			}
			int num2 = 0;
			if (!this.me && this.currentMovePoint != null)
			{
				num2 = Char.abs(this.cx - this.currentMovePoint.xEnd);
			}
			this.cp1++;
			if (this.cp1 >= 10)
			{
				this.cp1 = 0;
				this.cBonusSpeed = 0;
			}
			this.cf = (this.cp1 >> 1) + 2;
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy - 1) & 64) == 64)
			{
				this.cx += this.cvx * num >> 1;
			}
			else
			{
				this.cx += this.cvx * num;
			}
			if (this.cdir == 1)
			{
				if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
				{
					if (this.me)
					{
						this.cvx = 0;
						this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
					}
					else
					{
						this.stop();
					}
				}
			}
			else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
			{
				if (this.me)
				{
					this.cvx = 0;
					this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
				}
				else
				{
					this.stop();
				}
			}
			if (this.me)
			{
				if (this.cvx > 0)
				{
					this.cvx--;
				}
				else if (this.cvx < 0)
				{
					this.cvx++;
				}
				else
				{
					if (this.cx - this.cxSend != 0 && this.me)
					{
						Service.gI().charMove();
					}
					this.statusMe = 1;
					this.cBonusSpeed = 0;
				}
			}
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				if (this.me)
				{
					if (this.cx - this.cxSend != 0 || this.cy - this.cySend != 0)
					{
						Service.gI().charMove();
					}
					this.cf = 7;
					this.statusMe = 4;
					this.delayFall = 0;
					this.cvx = 3 * this.cdir;
					this.cp2 = 0;
				}
				else
				{
					this.stop();
				}
			}
			if (!this.me && this.currentMovePoint != null && Char.abs(this.cx - this.currentMovePoint.xEnd) > num2)
			{
				this.stop();
			}
			GameCanvas.gI().startDust(this.cdir, this.cx - (this.cdir << 3), this.cy);
			this.updateCharInBridge();
			this.addDustEff(2);
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x0025FA1C File Offset: 0x0025DC1C
		private void stop()
		{
			this.statusMe = 6;
			this.cp3 = 0;
			this.cvx = 0;
			this.cvy = 0;
			this.cp1 = (this.cp2 = 0);
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x0000B68D File Offset: 0x0000988D
		public static int abs(int i)
		{
			if (i > 0)
			{
				return i;
			}
			return -i;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0025FA58 File Offset: 0x0025DC58
		public void updateCharJump()
		{
			this.setMountIsStart();
			this.ty = 0;
			this.isFreez = false;
			if (this.isCharge)
			{
				this.isCharge = false;
				SoundMn.gI().taitaoPause();
				Service.gI().skill_not_focus(3);
			}
			this.addDustEff(3);
			this.cx += this.cvx;
			this.cy += this.cvy;
			if (this.cy < 0)
			{
				this.cy = 0;
				this.cvy = -1;
			}
			this.cvy++;
			if (this.cvy > 0)
			{
				this.cvy = 0;
			}
			if (!this.me && this.currentMovePoint != null)
			{
				int num = this.currentMovePoint.xEnd - this.cx;
				if (num > 0)
				{
					if (this.cvx > num)
					{
						this.cvx = num;
					}
					if (this.cvx < 0)
					{
						this.cvx = num;
					}
				}
				else if (num < 0)
				{
					if (this.cvx < num)
					{
						this.cvx = num;
					}
					if (this.cvx > 0)
					{
						this.cvx = num;
					}
				}
				else
				{
					this.cvx = num;
				}
			}
			if (this.cdir == 1)
			{
				if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12)
				{
					this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
					this.cvx = 0;
				}
			}
			else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12)
			{
				this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
				this.cvx = 0;
			}
			if (this.cvy == 0)
			{
				if (!this.isAttFly)
				{
					if (this.me)
					{
						this.setCharFallFromJump();
					}
					else
					{
						this.stop();
					}
				}
				else
				{
					this.setCharFallFromJump();
				}
			}
			if (this.me && !Char.ischangingMap && this.isInWaypoint())
			{
				Service.gI().charMove();
				if (TileMap.isTrainingMap())
				{
					Char.ischangingMap = true;
					Service.gI().getMapOffline();
				}
				else
				{
					Service.gI().requestChangeMap();
				}
				Char.isLockKey = true;
				Char.ischangingMap = true;
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				InfoDlg.showWait();
				return;
			}
			if (this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0))
			{
				this.statusMe = 4;
				this.cp1 = 0;
				this.cp2 = 0;
				this.cvy = 1;
				this.delayFall = 0;
				if (this.cy < 0)
				{
					this.cy = 0;
				}
				this.cy = TileMap.tileYofPixel(this.cy + 25);
				GameCanvas.clearKeyHold();
			}
			if (this.cp3 < 0)
			{
				this.cp3++;
			}
			this.cf = 7;
			if (!this.me && this.currentMovePoint != null && this.cy < this.currentMovePoint.yEnd)
			{
				this.stop();
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x0025FDA4 File Offset: 0x0025DFA4
		public void setCharFallFromJump()
		{
			this.cyStartFall = this.cy;
			this.cp1 = 0;
			this.cp2 = 0;
			this.statusMe = 10;
			this.cvx = this.cdir << 2;
			this.cvy = 0;
			this.cy = TileMap.tileYofPixel(this.cy) + 12;
			if (this.me && (this.cx - this.cxSend != 0 || this.cy - this.cySend != 0) && (Res.abs(Char.myCharz().cx - Char.myCharz().cxSend) > 96 || Res.abs(Char.myCharz().cy - Char.myCharz().cySend) > 24))
			{
				Service.gI().charMove();
			}
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0025FE68 File Offset: 0x0025E068
		public void updateCharFall()
		{
			if (this.holder)
			{
				return;
			}
			this.ty = 0;
			if (this.cy + 4 >= TileMap.pxh)
			{
				this.statusMe = 1;
				if (this.me)
				{
					SoundMn.gI().charFall();
				}
				this.cvx = (this.cvy = 0);
				this.cp3 = 0;
				return;
			}
			if (this.cy % 24 == 0 && (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
			{
				this.delayFall = 0;
				if (this.me)
				{
					if (this.cy - this.cySend > 0)
					{
						Service.gI().charMove();
					}
					else if (this.cx - this.cxSend != 0 || this.cy - this.cySend < 0)
					{
						Service.gI().charMove();
					}
					this.cvx = (this.cvy = 0);
					this.cp1 = (this.cp2 = 0);
					this.statusMe = 1;
					this.cp3 = 0;
					return;
				}
				this.stop();
				this.cf = 0;
				GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
				GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
				this.addDustEff(1);
			}
			if (this.delayFall > 0)
			{
				this.delayFall--;
				if (this.delayFall % 10 > 5)
				{
					this.cy++;
					return;
				}
				this.cy--;
				return;
			}
			else
			{
				if (this.cvy < -4)
				{
					this.cf = 7;
				}
				else
				{
					this.cf = 12;
				}
				this.cx += this.cvx;
				if (!this.me && this.currentMovePoint != null)
				{
					int num = this.currentMovePoint.xEnd - this.cx;
					if (num > 0)
					{
						if (this.cvx > num)
						{
							this.cvx = num;
						}
						if (this.cvx < 0)
						{
							this.cvx = num;
						}
					}
					else if (num < 0)
					{
						if (this.cvx < num)
						{
							this.cvx = num;
						}
						if (this.cvx > 0)
						{
							this.cvx = num;
						}
					}
					else
					{
						this.cvx = num;
					}
				}
				this.cvy++;
				if (this.cvy > 8)
				{
					this.cvy = 8;
				}
				if (this.skillPaintRandomPaint == null)
				{
					this.cy += this.cvy;
				}
				if (this.cdir == 1)
				{
					if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12)
					{
						this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
						this.cvx = 0;
					}
				}
				else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12)
				{
					this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
					this.cvx = 0;
				}
				if (this.cvy > 3 && (this.cyStartFall == 0 || this.cyStartFall <= TileMap.tileYofPixel(this.cy + 3)) && (TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2)
				{
					if (this.me)
					{
						this.cyStartFall = 0;
						this.cvx = (this.cvy = 0);
						this.cp1 = (this.cp2 = 0);
						this.cy = TileMap.tileXofPixel(this.cy + 3);
						this.statusMe = 1;
						if (this.me)
						{
							SoundMn.gI().charFall();
						}
						this.cp3 = 0;
						GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
						GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
						this.addDustEff(1);
						if (this.cy - this.cySend > 0)
						{
							if (this.me)
							{
								Service.gI().charMove();
								return;
							}
						}
						else if ((this.cx - this.cxSend != 0 || this.cy - this.cySend < 0) && this.me)
						{
							Service.gI().charMove();
							return;
						}
					}
					else
					{
						this.stop();
						this.cy = TileMap.tileXofPixel(this.cy + 3);
						this.cf = 0;
						GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
						GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
						this.addDustEff(1);
					}
					return;
				}
				this.cf = 12;
				if (this.me)
				{
					bool flag = this.isAttack;
					return;
				}
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) == 2)
				{
					this.cf = 0;
				}
				if (this.currentMovePoint != null && this.cy > this.currentMovePoint.yEnd)
				{
					this.stop();
				}
				return;
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x002603A4 File Offset: 0x0025E5A4
		public void updateCharFly()
		{
			int num = (this.isMonkey != 1 || this.me) ? 1 : 2;
			this.setMountIsStart();
			if (this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0))
			{
				if (this.cy - this.ch < 0)
				{
					this.cy = this.ch;
				}
				this.cf = 7;
				this.statusMe = 4;
				this.cvx = 0;
				this.cp2 = 0;
				this.currentMovePoint = null;
				return;
			}
			int num2 = this.cy;
			this.cp1++;
			if (this.cp1 >= 9)
			{
				this.cp1 = 0;
				if (!this.me)
				{
					this.cvx = (this.cvy = 0);
				}
				this.cBonusSpeed = 0;
			}
			this.cf = 8;
			if (Res.abs(this.cvx) <= 4 && this.me)
			{
				if (this.currentMovePoint != null)
				{
					int num3 = Char.abs(this.cx - this.currentMovePoint.xEnd);
					int num4 = Char.abs(this.cy - this.currentMovePoint.yEnd);
					if (num3 > num4 * 10)
					{
						this.cf = 8;
					}
					else if (num3 > num4 && num3 > 48 && num4 > 32)
					{
						this.cf = 8;
					}
					else
					{
						this.cf = 7;
					}
				}
				else
				{
					if (this.cvy < 0)
					{
						this.cvy = 0;
					}
					if (this.cvy > 16)
					{
						this.cvy = 16;
					}
					this.cf = 7;
				}
			}
			if (!this.me)
			{
				if (Char.abs(this.cvx) < 2)
				{
					this.cvx = (this.cdir << 1) * num;
				}
				if (this.cvy != 0)
				{
					this.cf = 7;
				}
				if (Char.abs(this.cvx) <= 2)
				{
					this.cp2++;
					if (this.cp2 > 32)
					{
						this.statusMe = 4;
						this.cvx = 0;
						this.cvy = 0;
					}
				}
			}
			if (this.cdir == 1)
			{
				if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - 1, 4))
				{
					this.cvx = 0;
					this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
					if (this.cvy == 0)
					{
						this.currentMovePoint = null;
					}
				}
			}
			else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - 1, 8))
			{
				this.cvx = 0;
				this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
				if (this.cvy == 0)
				{
					this.currentMovePoint = null;
				}
			}
			this.cx += this.cvx * num;
			this.cy += this.cvy * num;
			if (!this.isMount && num2 - this.cy == 0)
			{
				this.ty++;
				this.wt++;
				this.fy += ((!this.wy) ? 1 : -1);
				if (this.wt == 10)
				{
					this.wt = 0;
					this.wy = !this.wy;
				}
				if (this.ty > 20)
				{
					this.delayFall = 10;
					if (GameCanvas.gameTick % 3 == 0)
					{
						ServerEffect.addServerEffect(111, this.cx + ((this.cdir != 1) ? 27 : -17), this.cy + this.fy + 13, 1, (this.cdir != 1) ? 2 : 0);
					}
				}
			}
			if (!this.me)
			{
				return;
			}
			if (this.cvx > 0)
			{
				this.cvx--;
			}
			else if (this.cvx < 0)
			{
				this.cvx++;
			}
			else if (this.cvy == 0)
			{
				this.statusMe = 4;
				this.checkDelayFallIfTooHigh();
				Service.gI().charMove();
			}
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 20) & 2) == 2 || (TileMap.tileTypeAtPixel(this.cx, this.cy + 40) & 2) == 2)
			{
				if (this.cvy == 0)
				{
					this.delayFall = 0;
				}
				this.cyStartFall = 0;
				this.cvx = (this.cvy = 0);
				this.cp1 = (this.cp2 = 0);
				this.statusMe = 4;
				this.addDustEff(3);
			}
			if (Char.abs(this.cx - this.cxSend) > 96 || Char.abs(this.cy - this.cySend) > 24)
			{
				Service.gI().charMove();
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00260860 File Offset: 0x0025EA60
		public void setMount(int cid, int ctrans, int cgender)
		{
			this.idcharMount = cid;
			this.transMount = ctrans;
			this.genderMount = cgender;
			this.speedMount = 30;
			if (this.transMount < 0)
			{
				this.transMount = 0;
				this.xMount = GameScr.cmx + GameCanvas.w + 50;
				this.dxMount = -19;
			}
			else if (this.transMount == 1)
			{
				this.transMount = 2;
				this.xMount = GameScr.cmx - 100;
				this.dxMount = -33;
			}
			this.dyMount = -17;
			this.yMount = this.cy;
			this.frameMount = 0;
			this.frameNewMount = 0;
			this.isMount = false;
			this.isEndMount = false;
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x00260910 File Offset: 0x0025EB10
		public void updateMount()
		{
			this.frameMount++;
			if (this.frameMount > this.FrameMount.Length - 1)
			{
				this.frameMount = 0;
			}
			this.frameNewMount++;
			if (this.frameNewMount > 1000)
			{
				this.frameNewMount = 0;
			}
			if (this.isStartMount && !this.isMount)
			{
				this.yMount = this.cy;
				if (this.transMount == 0)
				{
					if (this.xMount - this.cx >= this.speedMount)
					{
						this.xMount -= this.speedMount;
						return;
					}
					this.xMount = this.cx;
					this.isMount = true;
					this.isEndMount = false;
					return;
				}
				else if (this.transMount == 2)
				{
					if (this.cx - this.xMount >= this.speedMount)
					{
						this.xMount += this.speedMount;
						return;
					}
					this.xMount = this.cx;
					this.isMount = true;
					this.isEndMount = false;
					return;
				}
			}
			else
			{
				if (this.isMount)
				{
					if (this.statusMe == 14 || this.ySd - this.cy < 24)
					{
						this.setMountIsEnd();
					}
					if (this.cp1 % 15 < 5)
					{
						this.cf = 0;
					}
					else
					{
						this.cf = 1;
					}
					this.transMount = this.cdir;
					this.updateSuperEff();
					if (this.transMount < 0)
					{
						this.transMount = 0;
						this.dxMount = -19;
					}
					else if (this.transMount == 1)
					{
						this.transMount = 2;
						this.dxMount = -31;
						if (this.isEventMount)
						{
							this.dxMount = -38;
						}
					}
					if (this.skillInfoPaint() != null)
					{
						this.dyMount = -15;
					}
					else
					{
						this.dyMount = -17;
					}
					this.yMount = this.cy;
					this.xMount = this.cx;
					return;
				}
				if (this.isEndMount)
				{
					if (this.transMount == 0)
					{
						if (this.xMount > GameScr.cmx - 100)
						{
							this.xMount -= 20;
							return;
						}
						this.isStartMount = false;
						this.isMount = false;
						this.isEndMount = false;
						return;
					}
					else if (this.transMount == 2)
					{
						if (this.xMount < GameScr.cmx + GameCanvas.w + 50)
						{
							this.xMount += 20;
							return;
						}
						this.isStartMount = false;
						this.isMount = false;
						this.isEndMount = false;
						return;
					}
				}
				else if (!this.isStartMount || !this.isMount || !this.isEndMount)
				{
					this.xMount = GameScr.cmx - 100;
					this.yMount = GameScr.cmy - 100;
				}
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x00260BB8 File Offset: 0x0025EDB8
		public void getMountData()
		{
			if (Mob.arrMobTemplate[50].data == null)
			{
				Mob.arrMobTemplate[50].data = new EffectData();
				string text = "/Mob/" + 50.ToString();
				if (MyStream.readFile(text) != null)
				{
					Mob.arrMobTemplate[50].data.readData(text + "/data");
					Mob.arrMobTemplate[50].data.img = GameCanvas.loadImage(text + "/img.png");
				}
				else
				{
					Service.gI().requestModTemplate(50);
				}
				Mob.lastMob.addElement(50.ToString() + string.Empty);
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00260C71 File Offset: 0x0025EE71
		public void checkFrameTick(int[] array)
		{
			this.t++;
			if (this.t > array.Length - 1)
			{
				this.t = 0;
			}
			this.fM = array[this.t];
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00260CA4 File Offset: 0x0025EEA4
		public void paintMount1(mGraphics g)
		{
			if (this.xMount <= GameScr.cmx || this.xMount >= GameScr.cmx + GameCanvas.w)
			{
				return;
			}
			if (this.me)
			{
				if (!this.isEndMount && !this.isStartMount && !this.isMount)
				{
					return;
				}
				if (this.idMount >= Char.ID_NEW_MOUNT)
				{
					FrameImage fraImage = mSystem.getFraImage(this.strMount + ((int)(this.idMount - Char.ID_NEW_MOUNT)).ToString() + "_0");
					if (fraImage != null)
					{
						fraImage.drawFrame(this.frameNewMount / 2 % fraImage.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
						return;
					}
				}
				else
				{
					if (this.isSpeacialMount)
					{
						return;
					}
					if (this.isEventMount)
					{
						g.drawRegion(Char.imgEventMountWing, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					if (this.genderMount == 2)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(Char.imgMount_XD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(Char.imgMount_XD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(Char.imgMount_NM, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(Char.imgMount_NM_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
				}
			}
			else
			{
				if (this.me)
				{
					return;
				}
				if (this.idMount >= Char.ID_NEW_MOUNT)
				{
					FrameImage fraImage2 = mSystem.getFraImage(this.strMount + ((int)(this.idMount - Char.ID_NEW_MOUNT)).ToString() + "_0");
					if (fraImage2 != null)
					{
						fraImage2.drawFrame(this.frameNewMount / 2 % fraImage2.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
						return;
					}
				}
				else
				{
					if (this.isSpeacialMount)
					{
						return;
					}
					if (this.isEventMount)
					{
						g.drawRegion(Char.imgEventMountWing, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					if (!this.isMount)
					{
						return;
					}
					if (this.genderMount == 2)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(Char.imgMount_XD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(Char.imgMount_XD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(Char.imgMount_NM, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(Char.imgMount_NM_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					}
				}
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00261130 File Offset: 0x0025F330
		public void paintMount2(mGraphics g)
		{
			if (this.xMount <= GameScr.cmx || this.xMount >= GameScr.cmx + GameCanvas.w)
			{
				return;
			}
			if (this.me)
			{
				if (!this.isEndMount && !this.isStartMount && !this.isMount)
				{
					return;
				}
				if (this.idMount >= Char.ID_NEW_MOUNT)
				{
					FrameImage fraImage = mSystem.getFraImage(this.strMount + ((int)(this.idMount - Char.ID_NEW_MOUNT)).ToString() + "_1");
					if (fraImage != null)
					{
						fraImage.drawFrame(this.frameNewMount / 2 % fraImage.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
						return;
					}
				}
				else if (this.isSpeacialMount)
				{
					this.checkFrameTick(this.move);
					if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
					{
						Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : -8), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
						return;
					}
					this.getMountData();
					return;
				}
				else
				{
					if (this.isEventMount)
					{
						g.drawRegion(Char.imgEventMount, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					if (this.genderMount == 0)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(Char.imgMount_TD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(Char.imgMount_TD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(Char.imgMount_NM_1, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(Char.imgMount_NM_1_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
				}
			}
			else
			{
				if (this.me)
				{
					return;
				}
				if (this.idMount >= Char.ID_NEW_MOUNT)
				{
					FrameImage fraImage2 = mSystem.getFraImage(this.strMount + ((int)(this.idMount - Char.ID_NEW_MOUNT)).ToString() + "_1");
					if (fraImage2 != null)
					{
						fraImage2.drawFrame(this.frameNewMount / 2 % fraImage2.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
					}
					return;
				}
				if (this.isSpeacialMount)
				{
					this.checkFrameTick(this.move);
					if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
					{
						Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : -8), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
						return;
					}
					this.getMountData();
					return;
				}
				else
				{
					if (this.isEventMount)
					{
						g.drawRegion(Char.imgEventMount, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					}
					if (!this.isMount)
					{
						return;
					}
					if (this.genderMount == 0)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(Char.imgMount_TD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(Char.imgMount_TD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(Char.imgMount_NM_1, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(Char.imgMount_NM_1_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					}
				}
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00261698 File Offset: 0x0025F898
		public void setMountIsStart()
		{
			if (this.me)
			{
				this.isHaveMount = this.checkHaveMount();
				if (TileMap.isVoDaiMap())
				{
					this.isHaveMount = false;
				}
			}
			if (this.isHaveMount)
			{
				if (this.ySd - this.cy <= 20)
				{
					this.xChar = this.cx;
				}
				if (this.xdis < 100)
				{
					this.xdis = Res.abs(this.xChar - this.cx);
				}
				if (this.xdis >= 70 && this.ySd - this.cy > 30 && !this.isStartMount && !this.isEndMount)
				{
					this.setMount(this.charID, this.cdir, this.cgender);
					this.isStartMount = true;
				}
			}
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x0026175B File Offset: 0x0025F95B
		public void setMountIsEnd()
		{
			if (this.ySd - this.cy < 24 && !this.isEndMount)
			{
				this.isStartMount = false;
				this.isMount = false;
				this.isEndMount = true;
				this.xdis = 0;
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00261794 File Offset: 0x0025F994
		public bool checkHaveMount()
		{
			bool result = false;
			short num = -1;
			Item[] array = this.arrItemBody;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && (array[i].template.type == 24 || array[i].template.type == 23))
				{
					num = ((array[i].template.part < 0) ? array[i].template.id : ((short)(ID_NEW_MOUNT + array[i].template.part)));
					result = true;
					break;
				}
			}
			this.isMountVip = false;
			this.isSpeacialMount = false;
			this.isEventMount = false;
			this.idMount = -1;
			if (num - 349 > 2)
			{
				if (num != 396)
				{
					if (num != 532)
					{
						if (num >= Char.ID_NEW_MOUNT)
						{
							this.idMount = num;
						}
					}
					else
					{
						this.isSpeacialMount = true;
					}
				}
				else
				{
					this.isEventMount = true;
				}
			}
			else
			{
				this.isMountVip = true;
			}
			return result;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0026187C File Offset: 0x0025FA7C
		private void checkDelayFallIfTooHigh()
		{
			bool flag = true;
			for (int i = 0; i < 150; i += 24)
			{
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy + i) & 2) == 2 || this.cy + i > TileMap.tmh * (int)TileMap.size - 24)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this.delayFall = 40;
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x002618DB File Offset: 0x0025FADB
		public void setDefaultPart()
		{
			this.setDefaultWeapon();
			this.setDefaultBody();
			this.setDefaultLeg();
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x002618EF File Offset: 0x0025FAEF
		public void setDefaultWeapon()
		{
			if (this.cgender == 0)
			{
				this.wp = 0;
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00261900 File Offset: 0x0025FB00
		public void setDefaultBody()
		{
			if (this.cgender == 0)
			{
				this.body = 57;
				return;
			}
			if (this.cgender == 1)
			{
				this.body = 59;
				return;
			}
			if (this.cgender == 2)
			{
				this.body = 57;
			}
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00261936 File Offset: 0x0025FB36
		public void setDefaultLeg()
		{
			if (this.cgender == 0)
			{
				this.leg = 58;
				return;
			}
			if (this.cgender == 1)
			{
				this.leg = 60;
				return;
			}
			if (this.cgender == 2)
			{
				this.leg = 58;
			}
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x0026196C File Offset: 0x0025FB6C
		public bool isSelectingSkillUseAlone()
		{
			return this.myskill != null && this.myskill.template.isUseAlone();
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00261988 File Offset: 0x0025FB88
		public bool isUseSkillSpec()
		{
			return this.myskill != null && this.myskill.template.isSkillSpec();
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x002619A4 File Offset: 0x0025FBA4
		public bool isSelectingSkillBuffToPlayer()
		{
			return this.myskill != null && this.myskill.template.isBuffToPlayer();
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x002619C0 File Offset: 0x0025FBC0
		public bool isUseChargeSkill()
		{
			return !this.isUseSkillAfterCharge && this.myskill != null && (this.myskill.template.id == 10 || this.myskill.template.id == 11);
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x00261A00 File Offset: 0x0025FC00
		public void setSkillPaint(SkillPaint skillPaint, int sType)
		{
			this.hasSendAttack = false;
			if (this.stone || (this.me && this.myskill.template.id == 9 && this.cHP <= this.cHPFull / 10L))
			{
				return;
			}
			if (this.me)
			{
				if (this.mobFocus == null && this.charFocus == null)
				{
					this.stopUseChargeSkill();
				}
				if (this.mobFocus != null && (this.mobFocus.status == 1 || this.mobFocus.status == 0))
				{
					this.stopUseChargeSkill();
				}
				if (this.charFocus != null && (this.charFocus.statusMe == 14 || this.charFocus.statusMe == 5))
				{
					this.stopUseChargeSkill();
				}
				if ((this.myskill.template.id == 23 && ((this.charFocus != null && this.charFocus.holdEffID != 0) || (this.mobFocus != null && this.mobFocus.holdEffID != 0) || this.holdEffID != 0)) || this.sleepEff || this.blindEff)
				{
					return;
				}
			}
			if ((this.me && this.dart != null) || TileMap.isOfflineMap())
			{
				return;
			}
			long num = mSystem.currentTimeMillis();
			if (this.me)
			{
				if (this.isSelectingSkillBuffToPlayer() && this.charFocus == null)
				{
					return;
				}
				if (num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown)
				{
					this.myskill.paintCanNotUseSkill = true;
					return;
				}
				this.myskill.lastTimeUseThisSkill = num;
				if (this.myskill.template.manaUseType == 2)
				{
					this.cMP = 1L;
				}
				else if (this.myskill.template.manaUseType != 1)
				{
					this.cMP -= (long)this.myskill.manaUse;
				}
				else
				{
					this.cMP -= (long)this.myskill.manaUse * this.cMPFull / 100L;
				}
				Char.myCharz().cStamina--;
				GameScr.gI().isInjureMp = true;
				GameScr.gI().twMp = 0;
				if (this.cMP < 0L)
				{
					this.cMP = 0L;
				}
			}
			if (this.me)
			{
				if (this.myskill.template.id == 7)
				{
					SoundMn.gI().hoisinh();
				}
				if (this.myskill.template.id == 6)
				{
					Service.gI().skill_not_focus(0);
					GameScr.gI().isUseFreez = true;
					SoundMn.gI().thaiduonghasan();
				}
				if (this.myskill.template.id == 8)
				{
					if (!this.isCharge)
					{
						SoundMn.gI().taitaoPause();
						Service.gI().skill_not_focus(1);
						this.isCharge = true;
						this.last = (this.cur = mSystem.currentTimeMillis());
					}
					else
					{
						Service.gI().skill_not_focus(3);
						this.isCharge = false;
						SoundMn.gI().taitaoPause();
					}
				}
				if (this.myskill.template.id == 13)
				{
					if (this.isMonkey != 0)
					{
						GameScr.gI().auto = 0;
						return;
					}
					if (!this.isCreateDark)
					{
						SoundMn.gI().gong();
						Service.gI().skill_not_focus(6);
						this.chargeCount = 0;
						this.isWaitMonkey = true;
					}
					return;
				}
				else
				{
					if (this.myskill.template.id == 14)
					{
						SoundMn.gI().gong();
						Service.gI().skill_not_focus(7);
						this.useChargeSkill(true);
					}
					if (this.myskill.template.id == 21)
					{
						Service.gI().skill_not_focus(10);
						return;
					}
					if (this.myskill.template.id == 12)
					{
						Service.gI().skill_not_focus(8);
					}
					if (this.myskill.template.id == 19)
					{
						Service.gI().skill_not_focus(9);
						return;
					}
				}
			}
			if ((this.isMonkey == 1 && skillPaint.id >= 35 && skillPaint.id <= 41) || skillPaint.id == 165)
			{
				skillPaint = GameScr.sks[106];
			}
			if (skillPaint.id >= 128 && skillPaint.id <= 134)
			{
				skillPaint = GameScr.sks[skillPaint.id - 65];
				if (this.charFocus != null)
				{
					this.cx = this.charFocus.cx;
					this.cy = this.charFocus.cy;
					this.currentMovePoint = null;
				}
				if (this.mobFocus != null)
				{
					this.cx = this.mobFocus.x;
					this.cy = this.mobFocus.y;
					this.currentMovePoint = null;
				}
				ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
				this.telePortSkill = true;
			}
			if (skillPaint.id >= 107 && skillPaint.id <= 113)
			{
				skillPaint = GameScr.sks[skillPaint.id - 44];
				EffecMn.addEff(new Effect(23, this.cx, this.cy + this.ch / 2, 3, 2, 1));
			}
			this.setAutoSkillPaint(skillPaint, sType);
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x00261F0C File Offset: 0x0026010C
		public void useSkillNotFocus()
		{
			GameScr.gI().auto = 0;
			Char.myCharz().setSkillPaint(GameScr.sks[(int)Char.myCharz().myskill.skillId], (!TileMap.tileTypeAt(Char.myCharz().cx, Char.myCharz().cy, 2)) ? 1 : 0);
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x00261F64 File Offset: 0x00260164
		public void sendUseChargeSkill()
		{
			if (this.me && (this.isFreez || this.isUsePlane))
			{
				GameScr.gI().auto = 0;
				return;
			}
			long num = mSystem.currentTimeMillis();
			if (this.me && num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown)
			{
				this.myskill.paintCanNotUseSkill = true;
				return;
			}
			if (this.myskill.template.id == 10)
			{
				this.useChargeSkill(false);
			}
			if (this.myskill.template.id == 11)
			{
				this.useChargeSkill(true);
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x00262004 File Offset: 0x00260204
		public void stopUseChargeSkill()
		{
			this.isFlyAndCharge = false;
			this.isStandAndCharge = false;
			this.isUseSkillAfterCharge = false;
			this.isCreateDark = false;
			if (this.me && this.statusMe != 14 && this.statusMe != 5)
			{
				this.isLockMove = false;
			}
			GameScr.gI().auto = 0;
			if (this.isCharge)
			{
				this.isCharge = false;
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0026206C File Offset: 0x0026026C
		public void useChargeSkill(bool isGround)
		{
			if (this.isCreateDark)
			{
				return;
			}
			GameScr.gI().auto = 0;
			if (!isGround)
			{
				if (!this.isFlyAndCharge)
				{
					if (this.me)
					{
						GameScr.gI().auto = 0;
						this.isLockMove = true;
						Service.gI().skill_not_focus(4);
					}
					this.isUseSkillAfterCharge = false;
					this.chargeCount = 0;
					this.isFlyAndCharge = true;
					this.posDisY = 0;
					this.seconds = 50000;
					this.isFlying = TileMap.tileTypeAt(this.cx, this.cy, 2);
				}
				return;
			}
			if (this.isStandAndCharge)
			{
				return;
			}
			this.chargeCount = 0;
			this.seconds = 50000;
			this.posDisY = 0;
			this.last = mSystem.currentTimeMillis();
			if (this.me)
			{
				this.isLockMove = true;
				if (this.cgender == 1)
				{
					Service.gI().skill_not_focus(4);
				}
			}
			if (this.cgender == 1)
			{
				SoundMn.gI().gongName();
			}
			this.isStandAndCharge = true;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00262168 File Offset: 0x00260368
		public void setAutoSkillPaint(SkillPaint skillPaint, int sType)
		{
			this.skillPaint = skillPaint;
			if (skillPaint.id >= 0 && skillPaint.id <= 6)
			{
				int num = Res.random(0, skillPaint.id + 4) - 1;
				if (num < 0)
				{
					num = 0;
				}
				if (num > 6)
				{
					num = 6;
				}
				this.skillPaintRandomPaint = GameScr.sks[num];
			}
			else if (skillPaint.id >= 14 && skillPaint.id <= 20)
			{
				int num2 = Res.random(0, skillPaint.id - 14 + 4) - 1;
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num2 > 6)
				{
					num2 = 6;
				}
				this.skillPaintRandomPaint = GameScr.sks[num2 + 14];
			}
			else if ((skillPaint.id >= 28 && skillPaint.id <= 34) || skillPaint.id == 164)
			{
				int num3 = Res.random(0, ((this.isMonkey != 1) ? skillPaint.id : 105) - ((this.isMonkey != 1) ? 28 : 105) + 4) - 1;
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num3 > 6)
				{
					num3 = 6;
				}
				if (this.isMonkey == 1)
				{
					num3 = 0;
				}
				this.skillPaintRandomPaint = GameScr.sks[num3 + ((this.isMonkey != 1) ? 28 : 105)];
			}
			else if (skillPaint.id >= 63 && skillPaint.id <= 69)
			{
				int num4 = Res.random(0, skillPaint.id - 63 + 4) - 1;
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num4 > 6)
				{
					num4 = 6;
				}
				this.skillPaintRandomPaint = GameScr.sks[num4 + 63];
			}
			else if (skillPaint.id >= 107 && skillPaint.id <= 109)
			{
				int num5 = Res.random(0, skillPaint.id - 107 + 4) - 1;
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num5 > 6)
				{
					num5 = 6;
				}
				this.skillPaintRandomPaint = GameScr.sks[num5 + 107];
			}
			else
			{
				this.skillPaintRandomPaint = skillPaint;
			}
			this.sType = sType;
			this.indexSkill = 0;
			this.i0 = (this.i1 = (this.i2 = (this.dx0 = (this.dx1 = (this.dx2 = (this.dy0 = (this.dy1 = (this.dy2 = 0))))))));
			this.eff0 = null;
			this.eff1 = null;
			this.eff2 = null;
			this.cvy = 0;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x002623B0 File Offset: 0x002605B0
		public SkillInfoPaint[] skillInfoPaint()
		{
			if (this.skillPaint == null)
			{
				return null;
			}
			if (this.skillPaintRandomPaint == null)
			{
				return null;
			}
			if (this.sType == 0)
			{
				return this.skillPaintRandomPaint.skillStand;
			}
			return this.skillPaintRandomPaint.skillfly;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x002623E8 File Offset: 0x002605E8
		public void setAttack()
		{
			if (this.me)
			{
				SkillPaint skillPaint = this.skillPaintRandomPaint;
				if (this.dart != null)
				{
					skillPaint = this.dart.skillPaint;
				}
				if (skillPaint == null)
				{
					return;
				}
				MyVector myVector = new MyVector();
				MyVector myVector2 = new MyVector();
				if (this.charFocus != null)
				{
					myVector2.addElement(this.charFocus);
				}
				else if (this.mobFocus != null)
				{
					myVector.addElement(this.mobFocus);
				}
				this.effPaints = new EffectPaint[myVector.size() + myVector2.size()];
				for (int i = 0; i < myVector.size(); i++)
				{
					this.effPaints[i] = new EffectPaint();
					this.effPaints[i].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					if (!this.isSelectingSkillUseAlone())
					{
						this.effPaints[i].eMob = (Mob)myVector.elementAt(i);
					}
				}
				for (int j = 0; j < myVector2.size(); j++)
				{
					this.effPaints[j + myVector.size()] = new EffectPaint();
					this.effPaints[j + myVector.size()].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					this.effPaints[j + myVector.size()].eChar = (Char)myVector2.elementAt(j);
				}
				int type = 0;
				if (this.mobFocus != null)
				{
					type = 1;
				}
				else if (this.charFocus != null)
				{
					type = 2;
				}
				if (myVector.size() == 0 && myVector2.size() == 0)
				{
					this.stopUseChargeSkill();
				}
				if (this.me && !this.isSelectingSkillUseAlone() && !this.hasSendAttack)
				{
					Service.gI().sendPlayerAttack(myVector, myVector2, type);
					this.hasSendAttack = true;
				}
				return;
			}
			else
			{
				SkillPaint skillPaint2 = this.skillPaintRandomPaint;
				if (this.dart != null)
				{
					skillPaint2 = this.dart.skillPaint;
				}
				if (skillPaint2 == null)
				{
					return;
				}
				if (this.attMobs != null)
				{
					this.effPaints = new EffectPaint[this.attMobs.Length];
					for (int k = 0; k < this.attMobs.Length; k++)
					{
						this.effPaints[k] = new EffectPaint();
						this.effPaints[k].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
						this.effPaints[k].eMob = this.attMobs[k];
					}
					this.attMobs = null;
					return;
				}
				if (this.attChars != null)
				{
					this.effPaints = new EffectPaint[this.attChars.Length];
					for (int l = 0; l < this.attChars.Length; l++)
					{
						this.effPaints[l] = new EffectPaint();
						this.effPaints[l].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
						this.effPaints[l].eChar = this.attChars[l];
					}
					this.attChars = null;
				}
				return;
			}
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x002626B1 File Offset: 0x002608B1
		public bool isOutX()
		{
			return this.cx < GameScr.cmx || this.cx > GameScr.cmx + GameScr.gW;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x002626D8 File Offset: 0x002608D8
		public bool isPaint()
		{
			return this.cy >= GameScr.cmy && this.cy <= GameScr.cmy + GameScr.gH + 30 && !this.isOutX() && !this.isSetPos && !this.isFusion;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0026272B File Offset: 0x0026092B
		public void createShadow(int x, int y, int life)
		{
			this.shadowX = x;
			this.shadowY = y;
			this.shadowLife = life;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00262742 File Offset: 0x00260942
		public void setMabuHold(bool m)
		{
			this.isMabuHold = m;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0026274C File Offset: 0x0026094C
		public virtual void paint(mGraphics g)
		{
			if (ModFunc.AnPlayer || this.isHide)
			{
				return;
			}
			if (this.isMafuba)
			{
				this.paintCharWithoutSkill(g);
				return;
			}
			if (this.isMabuHold)
			{
				if (this.cmtoChar)
				{
					GameScr.cmtoX = this.cx - GameScr.gW2;
					GameScr.cmtoY = this.cy - GameScr.gH23;
					if (!GameCanvas.isTouchControl)
					{
						GameScr.cmtoX += GameScr.gW6 * this.cdir;
						return;
					}
				}
			}
			else
			{
				if (!this.isPaint() || (!this.me && GameScr.notPaint))
				{
					return;
				}
				if (this.petFollow != null)
				{
					this.petFollow.paint(g);
				}
				this.paintMount1(g);
				if ((TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48) || this.isTeleport)
				{
					return;
				}
				if (this.holder && GameCanvas.gameTick % 2 == 0)
				{
					g.setColor(16185600);
					if (this.charHold != null)
					{
						g.drawLine(this.cx, this.cy - this.ch / 2, this.charHold.cx, this.charHold.cy - this.charHold.ch / 2);
					}
					if (this.mobHold != null)
					{
						g.drawLine(this.cx, this.cy - this.ch / 2, this.mobHold.x, this.mobHold.y - this.mobHold.h / 2);
					}
				}
				this.paintSuperEffBehind(g);
				this.paintAuraBehind(g);
				this.paintEffBehind(g);
				this.paintEff_Lvup_behind(g);
				this.paintEff_Pet(g);
				if (this.shadowLife > 0)
				{
					if (GameCanvas.gameTick % 2 == 0)
					{
						this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 25, true);
					}
					else if (this.shadowLife > 5)
					{
						this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 7, true);
					}
				}
				if (!this.isPaint() && this.skillPaint != null && (this.skillPaint.id < 70 || this.skillPaint.id > 76) && (this.skillPaint.id < 77 || this.skillPaint.id > 83))
				{
					if (this.skillPaint != null)
					{
						this.indexSkill = this.skillInfoPaint().Length;
						this.skillPaint = null;
					}
					this.effPaints = null;
					this.eff = null;
					this.effTask = null;
					this.indexEff = -1;
					this.indexEffTask = -1;
					return;
				}
				if (this.statusMe != 15 && (this.moveFast == null || this.moveFast[0] <= 0))
				{
					this.PaintCharName_HP_MP_Overhead(g);
					if (this.skillPaint == null || this.skillInfoPaint() == null || this.indexSkill >= this.skillInfoPaint().Length)
					{
						this.paintCharWithoutSkill(g);
					}
					if (this.arr != null)
					{
						this.arr.paint(g);
					}
					if (this.dart != null)
					{
						this.dart.paint(g);
					}
					this.paintEffect(g);
					Mob mob = this.mobMe;
					this.paintMount2(g);
					this.paintEff_Lvup_front(g);
					this.paintSuperEffFront(g);
					this.paintAuraFront(g);
					this.paintEffFront(g);
					this.paint_map_line(g);
				}
			}
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x00262A8C File Offset: 0x00260C8C
		private void paint_map_line(mGraphics g)
		{
			if (this.isPaintNewSkill || this.x_hint == 0 || this.y_hint == 0 || this.statusMe == 14)
			{
				return;
			}
			int arg = 0;
			int x = this.cx - 30;
			int y = this.cy - 15;
			int num = -30;
			int num2 = 5;
			if (Res.abs(this.cy - (int)this.y_hint) > 150)
			{
				if (this.cy > (int)this.y_hint)
				{
					arg = 7;
					x = this.cx;
					y = this.cy - 15 - 60;
				}
				else
				{
					arg = 5;
					x = this.cx;
					y = this.cy - 15 + 60;
				}
			}
			else if (this.cx > (int)this.x_hint)
			{
				arg = 2;
			}
			else if (this.cx <= (int)this.x_hint)
			{
				x = this.cx + 30;
			}
			if (GameCanvas.gameTick % 10 >= 5)
			{
				if (Res.abs(this.cx - (int)this.x_hint) > 100)
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, x, y, StaticObj.VCENTER_HCENTER);
					return;
				}
				g.drawImage(Panel.imgBantay, (int)this.x_hint + num, (int)(this.y_hint - 60) + num2, 0);
			}
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x00262BB4 File Offset: 0x00260DB4
		private void paintEff_Pet(mGraphics g)
		{
			for (int i = 0; i < this.vEffChar.size(); i++)
			{
				Effect effect = (Effect)this.vEffChar.elementAt(i);
				if (effect.effId >= 201)
				{
					effect.paint(g);
				}
			}
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00262C00 File Offset: 0x00260E00
		private void paintSuperEffBehind(mGraphics g)
		{
			if (this.me)
			{
				if (!Char.isPaintAura && this.idAuraEff > -1)
				{
					return;
				}
			}
			else if (this.idAuraEff > -1)
			{
				return;
			}
			if (ModFunc.GiamDungLuong || !Char.isPaintAura2 || (this.statusMe != 1 && this.statusMe != 6) || GameCanvas.panel.isShow || mSystem.currentTimeMillis() - this.timeBlue <= 0L || this.isCopy || this.clevel < 16)
			{
				return;
			}
			int num = 7598;
			int num2 = 4;
			if (this.clevel >= 19)
			{
				num = 7676;
			}
			if (this.clevel >= 22)
			{
				num = 7677;
			}
			if (this.clevel >= 25)
			{
				num = 7678;
			}
			if (num != -1)
			{
				Small small = SmallImage.imgNew[num];
				if (small == null)
				{
					SmallImage.createImage(num);
					return;
				}
				int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
				g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, this.cx, this.cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00262D28 File Offset: 0x00260F28
		private void paintSuperEffFront(mGraphics g)
		{
			if (this.me)
			{
				if (!Char.isPaintAura && this.idAuraEff > -1)
				{
					return;
				}
			}
			else if (this.idAuraEff > -1)
			{
				return;
			}
			if (!Char.isPaintAura2)
			{
				return;
			}
			if (this.statusMe == 1 || this.statusMe == 6)
			{
				if (ModFunc.GiamDungLuong || GameCanvas.panel.isShow || mSystem.currentTimeMillis() - this.timeBlue <= 0L)
				{
					return;
				}
				if (this.isCopy)
				{
					if (GameCanvas.gameTick % 2 == 0)
					{
						this.tBlue++;
					}
					if (this.tBlue > 6)
					{
						this.tBlue = 0;
					}
					return;
				}
				if (this.clevel >= 14 && !GameCanvas.lowGraphic)
				{
					bool flag = false;
					if (mSystem.currentTimeMillis() - this.timeBlue > -1000L && this.IsAddDust1)
					{
						flag = true;
						this.IsAddDust1 = false;
					}
					if (mSystem.currentTimeMillis() - this.timeBlue > -500L && this.IsAddDust2)
					{
						flag = true;
						this.IsAddDust2 = false;
					}
					if (flag)
					{
						GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
						GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
						this.addDustEff(1);
					}
				}
				if (this.clevel == 14)
				{
					if (GameCanvas.gameTick % 2 == 0)
					{
						this.tBlue++;
					}
					if (this.tBlue > 6)
					{
						this.tBlue = 0;
						return;
					}
				}
				else if (this.clevel == 15)
				{
					if (GameCanvas.gameTick % 2 == 0)
					{
						this.tBlue++;
					}
					if (this.tBlue > 6)
					{
						this.tBlue = 0;
						return;
					}
				}
				else
				{
					if (this.clevel < 16)
					{
						return;
					}
					int num = -1;
					int num2 = 4;
					if (this.clevel >= 16 && this.clevel < 22)
					{
						num = 7599;
						num2 = 4;
					}
					if (num != -1)
					{
						Small small = SmallImage.imgNew[num];
						if (small == null)
						{
							SmallImage.createImage(num);
							return;
						}
						int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
						g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, this.cx, this.cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
						return;
					}
				}
			}
			else
			{
				this.timeBlue = mSystem.currentTimeMillis() + 1500L;
				this.IsAddDust1 = true;
				this.IsAddDust2 = true;
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00262F94 File Offset: 0x00261194
		private void paintEffect(mGraphics g)
		{
			if (this.effPaints != null)
			{
				for (int i = 0; i < this.effPaints.Length; i++)
				{
					if (this.effPaints[i] != null)
					{
						if (this.effPaints[i].eMob != null)
						{
							int y = this.effPaints[i].eMob.y;
							if (this.effPaints[i].eMob is BigBoss)
							{
								y = this.effPaints[i].eMob.y - 60;
							}
							if (this.effPaints[i].eMob is BigBoss2)
							{
								y = this.effPaints[i].eMob.y - 50;
							}
							if (this.effPaints[i].eMob is BachTuoc)
							{
								y = this.effPaints[i].eMob.y - 40;
							}
							SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eMob.x, y, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
						else if (this.effPaints[i].eChar != null)
						{
							SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
					}
				}
			}
			if (this.indexEff >= 0 && this.eff != null)
			{
				SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.indexEff].idImg, this.cx + this.eff.arrEfInfo[this.indexEff].dx, this.cy + this.eff.arrEfInfo[this.indexEff].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			if (this.indexEffTask >= 0 && this.effTask != null)
			{
				SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x002631E8 File Offset: 0x002613E8
		public void paintHp(mGraphics g, int x, int y)
		{
			long num = this.cHP * 100L / this.cHPFull / 10L - 1L;
			if (num < 0L)
			{
				num = 0L;
			}
			if (num > 9L)
			{
				num = 9L;
			}
			if (!this.me)
			{
				g.drawRegion(Mob.imgHP, 0, 6 * (9 - (int)num), 9, 6, 0, x, y, 3);
			}
			if (this.cTypePk == 0 && (Char.myCharz().cFlag == 0 || this.cFlag == 0 || (this.cFlag != 8 && Char.myCharz().cFlag != 8 && this.cFlag == Char.myCharz().cFlag)))
			{
				return;
			}
			this.len = (int)(this.cHP * 100L / this.cHPFull * (long)this.w_hp_bar) / 100;
			num = (long)((int)(this.cHP * 100L / this.cHPFull));
			if (num < 30L)
			{
				this.imgHPtem = GameScr.imgHP_tm_do;
			}
			else if (num < 60L)
			{
				this.imgHPtem = GameScr.imgHP_tm_vang;
			}
			else
			{
				this.imgHPtem = GameScr.imgHP_tm_xanh;
			}
			int imageWidth = mGraphics.getImageWidth(GameScr.imgHP_tm_do);
			int imageHeight = mGraphics.getImageHeight(GameScr.imgHP_tm_do);
			long w = (long)imageWidth * num / 100L;
			g.drawImage(GameScr.imgHP_tm_xam, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
			if (this.len < 5)
			{
				if (GameCanvas.gameTick % 6 < 3)
				{
					g.drawRegion(this.imgHPtem, 0, 0, (int)w, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
					return;
				}
			}
			else
			{
				g.drawRegion(this.imgHPtem, 0, 0, (int)w, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0026338C File Offset: 0x0026158C
		private void PaintCharName_HP_MP_Overhead(mGraphics g)
		{
			Part part = GameScr.parts[this.getFHead(this.head)];
			int num = Char.CharInfo[this.cf][0][2] - (int)part.pi[Char.CharInfo[this.cf][0][0]].dy + 5;
			if ((this.isInvisiblez && !this.me) || (!this.me && TileMap.mapID == 113 && this.cy >= 360))
			{
				return;
			}
			if (this.me)
			{
				num += 5;
				this.paintHp(g, this.cx, this.cy - num + 3);
				if (this.fraDanhHieu != null)
				{
					int x = this.cx - this.fraDanhHieu.frameWidth / 2;
					int y = this.cy - num + 3 - mFont.tahoma_7.getHeight() - (this.fraDanhHieu.frameHeight + 5);
					if (GameCanvas.gameTick % 5 == 0)
					{
						this.danhHieuFramme++;
					}
					if (this.danhHieuFramme >= this.fraDanhHieu.nFrame)
					{
						this.danhHieuFramme = 0;
					}
					this.fraDanhHieu.drawFrame(this.danhHieuFramme, x, y, 0, mGraphics.TOP | mGraphics.LEFT, g);
				}
				return;
			}
			bool isSameClan = Char.myChar.clan != null && this.clanID == Char.myChar.clan.ID;
			bool isPK = this.cTypePk == 3 || this.cTypePk == 5;
			bool isTrainning = this.cTypePk == 4;
			if (this.cName.StartsWith("$"))
			{
				string text = this.cName;
				this.cName = text.Substring(1, text.Length - 1);
				this.isPet = true;
			}
			if (this.cName.StartsWith("#"))
			{
				string text2 = this.cName;
				this.cName = text2.Substring(1, text2.Length - 1);
				this.isMiniPet = true;
			}
			if (Char.myCharz().charFocus != null && Char.myCharz().charFocus.Equals(this))
			{
				num += 5;
				this.paintHp(g, this.cx, this.cy - num + 3);
				if (this.fraDanhHieu != null)
				{
					int x2 = this.cx - this.fraDanhHieu.frameWidth / 2;
					int y2 = this.cy - num + 3 - mFont.tahoma_7.getHeight() - (this.fraDanhHieu.frameHeight + 5);
					if (GameCanvas.gameTick % 5 == 0)
					{
						this.danhHieuFramme++;
					}
					if (this.danhHieuFramme >= this.fraDanhHieu.nFrame)
					{
						this.danhHieuFramme = 0;
					}
					this.fraDanhHieu.drawFrame(this.danhHieuFramme, x2, y2, 0, mGraphics.TOP | mGraphics.LEFT, g);
				}
			}
			num += mFont.tahoma_7b_white.getHeight();
			mFont mFont2 = mFont.tahoma_7b_white;
			if (this.isPet)
			{
				mFont2 = mFont.tahoma_7_blue1Small;
			}
			else if (this.isMiniPet)
			{
				mFont2 = mFont.number_orange;
			}
			else if (isPK)
			{
				mFont2 = mFont.tahoma_7b_red;
			}
			else if (isTrainning)
			{
				mFont2 = mFont.tahoma_7b_yellow;
			}
			else if (isSameClan)
			{
				mFont2 = mFont.tahoma_7b_green;
			}
			int strLiength = mFont2.getWidth(this.cName);
			if ((this.paintName || isPK || isTrainning) && !isSameClan)
			{
				if (mSystem.clientType == 1)
				{
					mFont2.drawStringBorder(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				}
				else if (this.charID == -83)
				{
					mFont2.drawStringBorder(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				}
				else
				{
					mFont2.drawStringBorder(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				}
				if (this.isTichXanh)
				{
					ModFunc.PaintTicks(g, this.cx + strLiength / 2, this.cy - num + 1);
				}
				num += mFont.tahoma_7.getHeight();
			}
			if (!isSameClan)
			{
				return;
			}
			if (Char.myCharz().charFocus != null && Char.myCharz().charFocus.Equals(this))
			{
				mFont2.drawStringBorder(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				if (this.isTichXanh)
				{
					ModFunc.PaintTicks(g, this.cx + strLiength / 2, this.cy - num + 1);
					return;
				}
			}
			else if (this.charFocus == null)
			{
				mFont2.drawStringBorder(g, this.cName, this.cx - 10, this.cy - num + 3, mFont.LEFT, mFont.tahoma_7_grey);
				if (this.isTichXanh)
				{
					ModFunc.PaintTicks(g, this.cx + strLiength / 2 + 4, this.cy - num + 4);
				}
			}
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00263858 File Offset: 0x00261A58
		public void paintShadow(mGraphics g)
		{
			if (this.isMabuHold || this.head == 377 || this.leg == 471 || this.isTeleport || this.isFlyUp)
			{
				return;
			}
			int num = (int)TileMap.size;
			if ((TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128 && !TileMap.tileTypeAt(this.xSd + num / 2, this.ySd + 1, 4))
			{
				if (TileMap.tileTypeAt((this.xSd - num / 2) / num, (this.ySd + 1) / num) == 0)
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
			g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x002639E8 File Offset: 0x00261BE8
		public void updateShadown()
		{
			int num = 0;
			this.xSd = this.cx;
			if (TileMap.tileTypeAt(this.cx, this.cy, 2))
			{
				this.ySd = this.cy;
				return;
			}
			this.ySd = this.cy;
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

		// Token: 0x06002784 RID: 10116 RVA: 0x00263A80 File Offset: 0x00261C80
		private void paintCharWithoutSkill(mGraphics g)
		{
			try
			{
				if (this.isMafuba)
				{
					this.paintCharBody(g, this.xMFB, this.yMFB, this.cdir, this.cf, false);
				}
				else
				{
					if (this.isInvisiblez)
					{
						if (this.me)
						{
							if (GameCanvas.gameTick % 50 == 48 || GameCanvas.gameTick % 50 == 90)
							{
								SmallImage.drawSmallImage(g, 1196, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
							}
							else
							{
								SmallImage.drawSmallImage(g, 1195, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
							}
						}
					}
					else
					{
						this.paintCharBody(g, this.cx, this.cy + this.fy, this.cdir, this.cf, true);
					}
					if (this.isLockAttack)
					{
						SmallImage.drawSmallImage(g, 290, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi paint char without skill: " + ex.ToString());
			}
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00263BB4 File Offset: 0x00261DB4
		public void paintBag(mGraphics g, int[] id, int x, int y, int dir, bool isPaintChar)
		{
			int num = 0;
			int num2 = 0;
			if (this.statusMe == 6)
			{
				num = 8;
				num2 = 17;
			}
			if (this.statusMe == 1)
			{
				if (this.cp1 % 15 < 5)
				{
					num = 8;
					num2 = 17;
				}
				else
				{
					num = 8;
					num2 = 18;
				}
			}
			if (this.statusMe == 2)
			{
				if (this.cf <= 3)
				{
					num = 7;
					num2 = 17;
				}
				else
				{
					num = 7;
					num2 = 18;
				}
			}
			if (this.statusMe == 3 || this.statusMe == 9)
			{
				num = 5;
				num2 = 20;
			}
			if (this.statusMe == 4)
			{
				if (this.cf == 8)
				{
					num = 5;
					num2 = 16;
				}
				else
				{
					num = 5;
					num2 = 20;
				}
			}
			if (this.statusMe == 10)
			{
				if (this.cf == 8)
				{
					num = 0;
					num2 = 23;
				}
				else
				{
					num = 5;
					num2 = 22;
				}
			}
			if (this.isInjure > 0)
			{
				num = 5;
				num2 = 18;
			}
			if (this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length)
			{
				num = -1;
				num2 = 17;
			}
			this.fBag++;
			if (this.fBag > 10000)
			{
				this.fBag = 0;
			}
			sbyte b = (sbyte)(this.fBag / 4 % id.Length);
			if (!isPaintChar)
			{
				if (id.Length == 2)
				{
					b = 1;
				}
				if (id.Length == 3)
				{
					if (id[2] >= 0)
					{
						b = 2;
						if (GameCanvas.gameTick % 10 > 5)
						{
							b = 1;
						}
					}
					else
					{
						b = 1;
					}
				}
			}
			else if (id.Length > 1 && (b == 0 || b == 1) && this.statusMe != 1 && this.statusMe != 6)
			{
				this.fBag = 0;
				b = 0;
				if (GameCanvas.gameTick % 10 > 5)
				{
					b = 1;
				}
			}
			SmallImage.drawSmallImage(g, id[(int)b], x + ((dir != 1) ? num : (-num)), y - num2, (dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x00263D54 File Offset: 0x00261F54
		public void paintHeadWithXY(mGraphics g, int x, int y, int look)
		{
			Part part = GameScr.parts[this.head];
			SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[0][0][0]].id, x + Char.CharInfo[0][0][1] + (int)part.pi[Char.CharInfo[0][0][0]].dx - 3, y + 3, look, mGraphics.LEFT | mGraphics.BOTTOM);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00263DC4 File Offset: 0x00261FC4
		public void paintCharBody(mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
		{
			this.ph = GameScr.parts[this.head];
			this.pl = GameScr.parts[this.leg];
			this.pb = GameScr.parts[this.body];
			if (this.bag >= 0 && this.statusMe != 14)
			{
				if (!ClanImage.idImages.containsKey(this.bag.ToString() + string.Empty))
				{
					ClanImage.idImages.put(this.bag.ToString() + string.Empty, new ClanImage());
					Service.gI().requestBagImage((sbyte)this.bag);
				}
				else
				{
					ClanImage clanImage = (ClanImage)ClanImage.idImages.get(this.bag.ToString() + string.Empty);
					if (clanImage.idImage != null && isPaintBag)
					{
						this.paintBag(g, clanImage.idImage, cx, cy, cdir, true);
					}
				}
			}
			int num = 2;
			int anchor = 24;
			int anchor2 = StaticObj.TOP_RIGHT;
			int num2 = -1;
			if (cdir == 1)
			{
				num = 0;
				anchor = 0;
				anchor2 = 0;
				num2 = 1;
			}
			if (this.statusMe == 14)
			{
				if (GameCanvas.gameTick % 4 > 0)
				{
					g.drawImage(ItemMap.imageFlare, cx, cy - this.ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				int num3 = 0;
				if (this.head == 89 || this.head == 457 || this.head == 460 || this.head == 461 || this.head == 462 || this.head == 463 || this.head == 464 || this.head == 465 || this.head == 466)
				{
					num3 = 15;
				}
				SmallImage.drawSmallImage(g, 834, cx, cy - Char.CharInfo[cf][2][2] + (int)this.pb.pi[Char.CharInfo[cf][2][0]].dy - 2 + num3, num, StaticObj.TOP_CENTER);
				SmallImage.drawSmallImage(g, 79, cx, cy - this.ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
				SmallImage.drawSmallImage(g, (int)this.ph.pi[Char.CharInfo[cf][0][0]].id, cx + (Char.CharInfo[cf][0][1] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dx) * num2, cy - Char.CharInfo[cf][0][2] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dy, num, anchor);
				this.paintHat_behind(g, cf, cy - Char.CharInfo[cf][2][2] + (int)this.pb.pi[Char.CharInfo[cf][2][0]].dy);
				if (this.isHead_2Fr(this.head))
				{
					Part part = GameScr.parts[this.getFHead(this.head)];
					SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[cf][0][0]].id, cx + (Char.CharInfo[cf][0][1] + (int)part.pi[Char.CharInfo[cf][0][0]].dx) * num2, cy - Char.CharInfo[cf][0][2] + (int)part.pi[Char.CharInfo[cf][0][0]].dy, num, anchor);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.ph.pi[Char.CharInfo[cf][0][0]].id, cx + (Char.CharInfo[cf][0][1] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dx) * num2, cy - Char.CharInfo[cf][0][2] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dy, num, anchor);
				}
				this.paintHat_front(g, cf, cy - Char.CharInfo[cf][2][2] + (int)this.pb.pi[Char.CharInfo[cf][2][0]].dy);
				this.paintRedEye(g, cx + (Char.CharInfo[cf][0][1] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dx) * num2, cy - Char.CharInfo[cf][0][2] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			else
			{
				this.paintHat_behind(g, cf, cy - Char.CharInfo[cf][2][2] + (int)this.pb.pi[Char.CharInfo[cf][2][0]].dy);
				if (this.isHead_2Fr(this.head))
				{
					Part part2 = GameScr.parts[this.getFHead(this.head)];
					SmallImage.drawSmallImage(g, (int)part2.pi[Char.CharInfo[cf][0][0]].id, cx + (Char.CharInfo[cf][0][1] + (int)part2.pi[Char.CharInfo[cf][0][0]].dx) * num2, cy - Char.CharInfo[cf][0][2] + (int)part2.pi[Char.CharInfo[cf][0][0]].dy, num, anchor);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.ph.pi[Char.CharInfo[cf][0][0]].id, cx + (Char.CharInfo[cf][0][1] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dx) * num2, cy - Char.CharInfo[cf][0][2] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dy, num, anchor);
				}
				SmallImage.drawSmallImage(g, (int)this.pl.pi[Char.CharInfo[cf][1][0]].id, cx + (Char.CharInfo[cf][1][1] + (int)this.pl.pi[Char.CharInfo[cf][1][0]].dx) * num2, cy - Char.CharInfo[cf][1][2] + (int)this.pl.pi[Char.CharInfo[cf][1][0]].dy, num, anchor);
				SmallImage.drawSmallImage(g, (int)this.pb.pi[Char.CharInfo[cf][2][0]].id, cx + (Char.CharInfo[cf][2][1] + (int)this.pb.pi[Char.CharInfo[cf][2][0]].dx) * num2, cy - Char.CharInfo[cf][2][2] + (int)this.pb.pi[Char.CharInfo[cf][2][0]].dy, num, anchor);
				this.paintRedEye(g, cx + (Char.CharInfo[cf][0][1] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dx) * num2, cy - Char.CharInfo[cf][0][2] + (int)this.ph.pi[Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			this.ch = ((this.isMonkey != 1 && !this.isFusion) ? (Char.CharInfo[0][0][2] + (int)this.ph.pi[Char.CharInfo[0][0][0]].dy + 10) : 60);
			int num4 = (int)((Res.abs((int)this.ph.pi[Char.CharInfo[cf][0][0]].dy) < 22) ? this.ph.pi[Char.CharInfo[cf][0][0]].dy : ((this.ph.pi[Char.CharInfo[cf][0][0]].dy >= 0) ? (this.ph.pi[Char.CharInfo[cf][0][0]].dy - 5) : (this.ph.pi[Char.CharInfo[cf][0][0]].dy + 5)));
			this.cH_new = cy - Char.CharInfo[cf][0][2] + num4;
			if (this.statusMe == 1 && this.charID > 0 && !this.isMask && !this.isUseChargeSkill() && !this.isWaitMonkey && this.skillPaint == null && cf != 23 && this.bag < 0 && ((GameCanvas.gameTick + this.charID) % 30 == 0 || this.isFreez))
			{
				g.drawImage((this.cgender != 1) ? Char.eyeTraiDat : Char.eyeNamek, cx + -((this.cgender != 1) ? 2 : 2) * num2, cy - 32 + ((this.cgender != 1) ? 11 : 10) - cf, anchor2);
			}
			if (this.eProtect != null)
			{
				this.eProtect.paint(g);
			}
			if (this.eDanhHieu != null)
			{
				this.eDanhHieu.paint(g);
			}
			this.paintPKFlag(g);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x002646E4 File Offset: 0x002628E4
		public void paintCharWithSkill(mGraphics g)
		{
			this.ty = 0;
			SkillInfoPaint[] array = this.skillInfoPaint();
			this.cf = array[this.indexSkill].status;
			this.paintCharWithoutSkill(g);
			if (this.cdir == 1)
			{
				if (this.eff0 != null)
				{
					if (this.dx0 == 0)
					{
						this.dx0 = array[this.indexSkill].e0dx;
					}
					if (this.dy0 == 0)
					{
						this.dy0 = array[this.indexSkill].e0dy;
					}
					SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx + this.dx0 + this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					this.i0++;
					if (this.i0 >= this.eff0.arrEfInfo.Length)
					{
						this.eff0 = null;
						this.i0 = (this.dx0 = (this.dy0 = 0));
					}
				}
				if (this.eff1 != null)
				{
					if (this.dx1 == 0)
					{
						this.dx1 = array[this.indexSkill].e1dx;
					}
					if (this.dy1 == 0)
					{
						this.dy1 = array[this.indexSkill].e1dy;
					}
					SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx + this.dx1 + this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					this.i1++;
					if (this.i1 >= this.eff1.arrEfInfo.Length)
					{
						this.eff1 = null;
						this.i1 = (this.dx1 = (this.dy1 = 0));
					}
				}
				if (this.eff2 != null)
				{
					if (this.dx2 == 0)
					{
						this.dx2 = array[this.indexSkill].e2dx;
					}
					if (this.dy2 == 0)
					{
						this.dy2 = array[this.indexSkill].e2dy;
					}
					SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx + this.dx2 + this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					this.i2++;
					if (this.i2 >= this.eff2.arrEfInfo.Length)
					{
						this.eff2 = null;
						this.i2 = (this.dx2 = (this.dy2 = 0));
					}
				}
			}
			else
			{
				if (this.eff0 != null)
				{
					if (this.dx0 == 0)
					{
						this.dx0 = array[this.indexSkill].e0dx;
					}
					if (this.dy0 == 0)
					{
						this.dy0 = array[this.indexSkill].e0dy;
					}
					SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx - this.dx0 - this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
					this.i0++;
					if (this.i0 >= this.eff0.arrEfInfo.Length)
					{
						this.eff0 = null;
						this.i0 = 0;
						this.dx0 = 0;
						this.dy0 = 0;
					}
				}
				if (this.eff1 != null)
				{
					if (this.dx1 == 0)
					{
						this.dx1 = array[this.indexSkill].e1dx;
					}
					if (this.dy1 == 0)
					{
						this.dy1 = array[this.indexSkill].e1dy;
					}
					SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx - this.dx1 - this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
					this.i1++;
					if (this.i1 >= this.eff1.arrEfInfo.Length)
					{
						this.eff1 = null;
						this.i1 = 0;
						this.dx1 = 0;
						this.dy1 = 0;
					}
				}
				if (this.eff2 != null)
				{
					if (this.dx2 == 0)
					{
						this.dx2 = array[this.indexSkill].e2dx;
					}
					if (this.dy2 == 0)
					{
						this.dy2 = array[this.indexSkill].e2dy;
					}
					SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx - this.dx2 - this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
					this.i2++;
					if (this.i2 >= this.eff2.arrEfInfo.Length)
					{
						this.eff2 = null;
						this.i2 = 0;
						this.dx2 = 0;
						this.dy2 = 0;
					}
				}
			}
			this.indexSkill++;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00264CFC File Offset: 0x00262EFC
		public void moveTo(int toX, int toY, int type)
		{
			if (type == 1 || Res.abs(toX - this.cx) > 100 || Res.abs(toY - this.cy) > 300)
			{
				this.createShadow(this.cx, this.cy, 10);
				this.cx = toX;
				this.cy = toY;
				this.vMovePoints.removeAllElements();
				this.statusMe = 6;
				this.cp3 = 0;
				this.currentMovePoint = null;
				this.cf = 25;
				return;
			}
			int dir = 0;
			int act = 0;
			int num = toX - this.cx;
			int num2 = toY - this.cy;
			if (num == 0 && num2 == 0)
			{
				act = 1;
				this.cp3 = 0;
			}
			else if (num2 == 0)
			{
				act = 2;
				if (num > 0)
				{
					dir = 1;
				}
				if (num < 0)
				{
					dir = -1;
				}
			}
			else if (num2 != 0)
			{
				if (num2 < 0)
				{
					act = 3;
				}
				if (num2 > 0)
				{
					act = 4;
				}
				if (num < 0)
				{
					dir = -1;
				}
				if (num > 0)
				{
					dir = 1;
				}
			}
			this.vMovePoints.addElement(new MovePoint(toX, toY, act, dir));
			if (this.statusMe != 6)
			{
				this.statusBeforeNothing = this.statusMe;
			}
			this.statusMe = 6;
			this.cp3 = 0;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x00264E0C File Offset: 0x0026300C
		public void searchItem()
		{
			int[] array = new int[]
			{
				-1,
				-1,
				-1,
				-1
			};
			if (this.itemFocus != null)
			{
				return;
			}
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				int num = Math.abs(Char.myCharz().cx - itemMap.x);
				int num2 = Math.abs(Char.myCharz().cy - itemMap.y);
				int num3 = (num <= num2) ? num2 : num;
				if (num <= 48 && num2 <= 48 && (this.itemFocus == null || num3 < array[3]))
				{
					if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
					{
						if (itemMap.template.type == 9)
						{
							this.itemFocus = itemMap;
							array[3] = num3;
						}
					}
					else
					{
						this.itemFocus = itemMap;
						array[3] = num3;
					}
				}
			}
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00264EF8 File Offset: 0x002630F8
		public void searchFocus()
		{
			if (ModFunc.isLockFocus && Char.myCharz().charFocus != null)
			{
				return;
			}
			if (Char.myCharz().skillPaint != null || Char.myCharz().arr != null || Char.myCharz().dart != null)
			{
				this.timeFocusToMob = 200;
				return;
			}
			if (this.timeFocusToMob > 0)
			{
				this.timeFocusToMob--;
				return;
			}
			if (Char.isManualFocus && this.charFocus != null && (this.charFocus.statusMe == 15 || this.charFocus.isInvisiblez))
			{
				this.charFocus = null;
			}
			if (GameCanvas.gameTick % 2 == 0 || this.isMeCanAttackOtherPlayer(this.charFocus))
			{
				return;
			}
			int num = 0;
			if (this.nClass != null && (this.nClass.classId == 0 || this.nClass.classId == 1 || this.nClass.classId == 3 || this.nClass.classId == 5))
			{
				num = 40;
			}
			int[] array = new int[]
			{
				-1,
				-1,
				-1,
				-1
			};
			int num2 = GameScr.cmx - 10;
			int num3 = GameScr.cmx + GameCanvas.w + 10;
			int cmy = GameScr.cmy;
			int num4 = GameScr.cmy + GameCanvas.h - GameScr.cmdBarH + 10;
			if (Char.isManualFocus)
			{
				if ((this.mobFocus != null && this.mobFocus.status != 1 && this.mobFocus.status != 0 && num2 <= this.mobFocus.x && this.mobFocus.x <= num3 && cmy <= this.mobFocus.y && this.mobFocus.y <= num4) || (this.npcFocus != null && num2 <= this.npcFocus.cx && this.npcFocus.cx <= num3 && cmy <= this.npcFocus.cy && this.npcFocus.cy <= num4) || (this.charFocus != null && num2 <= this.charFocus.cx && this.charFocus.cx <= num3 && cmy <= this.charFocus.cy && this.charFocus.cy <= num4) || (this.itemFocus != null && num2 <= this.itemFocus.x && this.itemFocus.x <= num3 && cmy <= this.itemFocus.y && this.itemFocus.y <= num4))
				{
					return;
				}
				Char.isManualFocus = false;
			}
			num2 = Char.myCharz().cx - 80;
			num3 = Char.myCharz().cx + 80;
			cmy = Char.myCharz().cy - 30;
			num4 = Char.myCharz().cy + 30;
			if (this.npcFocus != null && this.npcFocus.template.npcTemplateId == 6)
			{
				num2 = Char.myCharz().cx - 20;
				num3 = Char.myCharz().cx + 20;
				cmy = Char.myCharz().cy - 10;
				num4 = Char.myCharz().cy + 10;
			}
			if (this.npcFocus == null)
			{
				for (int i = 0; i < GameScr.vNpc.size(); i++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(i);
					if (npc.statusMe != 15)
					{
						int num5 = Math.abs(Char.myCharz().cx - npc.cx);
						int num6 = Math.abs(Char.myCharz().cy - npc.cy);
						int num7 = (num5 <= num6) ? num6 : num5;
						num2 = Char.myCharz().cx - 80;
						num3 = Char.myCharz().cx + 80;
						cmy = Char.myCharz().cy - 30;
						num4 = Char.myCharz().cy + 30;
						if (npc.template.npcTemplateId == 6)
						{
							num2 = Char.myCharz().cx - 20;
							num3 = Char.myCharz().cx + 20;
							cmy = Char.myCharz().cy - 10;
							num4 = Char.myCharz().cy + 10;
						}
						if (num2 <= npc.cx && npc.cx <= num3 && cmy <= npc.cy && npc.cy <= num4 && (this.npcFocus == null || num7 < array[1]))
						{
							this.npcFocus = npc;
							array[1] = num7;
						}
					}
				}
			}
			else
			{
				if (num2 <= this.npcFocus.cx && this.npcFocus.cx <= num3 && cmy <= this.npcFocus.cy && this.npcFocus.cy <= num4)
				{
					this.ClearFocus(1);
					return;
				}
				this.deFocusNPC();
				for (int j = 0; j < GameScr.vNpc.size(); j++)
				{
					Npc npc2 = (Npc)GameScr.vNpc.elementAt(j);
					if (npc2.statusMe != 15)
					{
						int num8 = Math.abs(Char.myCharz().cx - npc2.cx);
						int num9 = Math.abs(Char.myCharz().cy - npc2.cy);
						int num10 = (num8 <= num9) ? num9 : num8;
						num2 = Char.myCharz().cx - 80;
						num3 = Char.myCharz().cx + 80;
						cmy = Char.myCharz().cy - 30;
						num4 = Char.myCharz().cy + 30;
						if (npc2.template.npcTemplateId == 6)
						{
							num2 = Char.myCharz().cx - 20;
							num3 = Char.myCharz().cx + 20;
							cmy = Char.myCharz().cy - 10;
							num4 = Char.myCharz().cy + 10;
						}
						if (num2 <= npc2.cx && npc2.cx <= num3 && cmy <= npc2.cy && npc2.cy <= num4 && (this.npcFocus == null || num10 < array[1]))
						{
							this.npcFocus = npc2;
							array[1] = num10;
						}
					}
				}
			}
			if (this.itemFocus == null)
			{
				for (int k = 0; k < GameScr.vItemMap.size(); k++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
					int num11 = Math.abs(Char.myCharz().cx - itemMap.x);
					int num12 = Math.abs(Char.myCharz().cy - itemMap.y);
					int num13 = (num11 <= num12) ? num12 : num11;
					if (num11 <= 48 && num12 <= 48 && (this.itemFocus == null || num13 < array[3]))
					{
						if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
						{
							if (itemMap.template.type == 9)
							{
								this.itemFocus = itemMap;
								array[3] = num13;
							}
						}
						else
						{
							this.itemFocus = itemMap;
							array[3] = num13;
						}
					}
				}
			}
			else
			{
				if (num2 <= this.itemFocus.x && this.itemFocus.x <= num3 && cmy <= this.itemFocus.y && this.itemFocus.y <= num4)
				{
					this.ClearFocus(3);
					return;
				}
				this.itemFocus = null;
				for (int l = 0; l < GameScr.vItemMap.size(); l++)
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(l);
					int num14 = Math.abs(Char.myCharz().cx - itemMap2.x);
					int num15 = Math.abs(Char.myCharz().cy - itemMap2.y);
					int num16 = (num14 <= num15) ? num15 : num14;
					if (num2 <= itemMap2.x && itemMap2.x <= num3 && cmy <= itemMap2.y && itemMap2.y <= num4 && (this.itemFocus == null || num16 < array[3]))
					{
						if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
						{
							if (itemMap2.template.type == 9)
							{
								this.itemFocus = itemMap2;
								array[3] = num16;
							}
						}
						else
						{
							this.itemFocus = itemMap2;
							array[3] = num16;
						}
					}
				}
			}
			num2 = Char.myCharz().cx - Char.myCharz().getdxSkill() - 10;
			num3 = Char.myCharz().cx + Char.myCharz().getdxSkill() + 10;
			cmy = Char.myCharz().cy - Char.myCharz().getdySkill() - num - 20;
			num4 = Char.myCharz().cy + Char.myCharz().getdySkill() + 20;
			if (num4 > Char.myCharz().cy + 30)
			{
				num4 = Char.myCharz().cy + 30;
			}
			if (this.mobFocus == null)
			{
				for (int m = 0; m < GameScr.vMob.size(); m++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(m);
					int num17 = Math.abs(Char.myCharz().cx - mob.x);
					int num18 = Math.abs(Char.myCharz().cy - mob.y);
					int num19 = (num17 <= num18) ? num18 : num17;
					if (num2 <= mob.x && mob.x <= num3 && cmy <= mob.y && mob.y <= num4 && (this.mobFocus == null || num19 < array[0]))
					{
						this.mobFocus = mob;
						array[0] = num19;
					}
				}
			}
			else
			{
				if (this.mobFocus.status != 1 && this.mobFocus.status != 0 && num2 <= this.mobFocus.x && this.mobFocus.x <= num3 && cmy <= this.mobFocus.y && this.mobFocus.y <= num4)
				{
					this.ClearFocus(0);
					return;
				}
				this.mobFocus = null;
				for (int n = 0; n < GameScr.vMob.size(); n++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(n);
					int num20 = Math.abs(Char.myCharz().cx - mob2.x);
					int num21 = Math.abs(Char.myCharz().cy - mob2.y);
					int num22 = (num20 <= num21) ? num21 : num20;
					if (num2 <= mob2.x && mob2.x <= num3 && cmy <= mob2.y && mob2.y <= num4 && (this.mobFocus == null || num22 < array[0]))
					{
						this.mobFocus = mob2;
						array[0] = num22;
					}
				}
			}
			if (this.charFocus == null)
			{
				for (int num23 = 0; num23 < GameScr.vCharInMap.size(); num23++)
				{
					Char @char = (Char)GameScr.vCharInMap.elementAt(num23);
					if (@char.statusMe != 15 && !@char.isInvisiblez && this.wdx == 0 && this.wdy == 0)
					{
						int num24 = Math.abs(Char.myCharz().cx - @char.cx);
						int num25 = Math.abs(Char.myCharz().cy - @char.cy);
						int num26 = (num24 <= num25) ? num25 : num24;
						if (num2 <= @char.cx && @char.cx <= num3 && cmy <= @char.cy && @char.cy <= num4 && (this.charFocus == null || num26 < array[2]))
						{
							this.charFocus = @char;
							array[2] = num26;
						}
					}
				}
			}
			else
			{
				if (num2 <= this.charFocus.cx && this.charFocus.cx <= num3 && cmy <= this.charFocus.cy && this.charFocus.cy <= num4 && this.charFocus.statusMe != 15 && !this.charFocus.isInvisiblez)
				{
					this.ClearFocus(2);
					return;
				}
				this.charFocus = null;
				for (int num27 = 0; num27 < GameScr.vCharInMap.size(); num27++)
				{
					Char char2 = (Char)GameScr.vCharInMap.elementAt(num27);
					if (char2.statusMe != 15 && !char2.isInvisiblez && this.wdx == 0 && this.wdy == 0)
					{
						int num28 = Math.abs(Char.myCharz().cx - char2.cx);
						int num29 = Math.abs(Char.myCharz().cy - char2.cy);
						int num30 = (num28 <= num29) ? num29 : num28;
						if (num2 <= char2.cx && char2.cx <= num3 && cmy <= char2.cy && char2.cy <= num4 && (this.charFocus == null || num30 < array[2]))
						{
							this.charFocus = char2;
							array[2] = num30;
						}
					}
				}
			}
			int num31 = -1;
			for (int num32 = 0; num32 < array.Length; num32++)
			{
				if (num31 == -1)
				{
					if (array[num32] != -1)
					{
						num31 = num32;
					}
				}
				else if (array[num32] < array[num31] && array[num32] != -1)
				{
					num31 = num32;
				}
			}
			this.ClearFocus(num31);
			if (this.me && this.isAttacPlayerStatus())
			{
				if (this.mobFocus != null && !this.mobFocus.isMobMe)
				{
					this.mobFocus = null;
				}
				this.npcFocus = null;
				this.itemFocus = null;
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00265C34 File Offset: 0x00263E34
		public void ClearFocus(int index)
		{
			ModFunc.isLockFocus = false;
			switch (index)
			{
				case 0:
					this.deFocusNPC();
					this.charFocus = null;
					this.itemFocus = null;
					return;
				case 1:
					this.mobFocus = null;
					this.charFocus = null;
					this.itemFocus = null;
					return;
				case 2:
					this.mobFocus = null;
					this.deFocusNPC();
					this.itemFocus = null;
					return;
				case 3:
					this.mobFocus = null;
					this.deFocusNPC();
					this.charFocus = null;
					return;
				default:
					return;
			}
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00265CB4 File Offset: 0x00263EB4
		public static bool isCharInScreen(Char c)
		{
			int cmx = GameScr.cmx;
			int num = GameScr.cmx + GameCanvas.w;
			int num2 = GameScr.cmy + 10;
			int num3 = GameScr.cmy + GameScr.gH;
			return c.statusMe != 15 && !c.isInvisiblez && cmx <= c.cx && c.cx <= num && num2 <= c.cy && c.cy <= num3;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00265D21 File Offset: 0x00263F21
		public bool isAttacPlayerStatus()
		{
			return this.cTypePk == 4 || this.cTypePk == 3;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x00265D37 File Offset: 0x00263F37
		public void setHoldChar(Char r)
		{
			if (this.cx < r.cx)
			{
				this.cdir = 1;
			}
			else
			{
				this.cdir = -1;
			}
			this.charHold = r;
			this.holder = true;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00265D65 File Offset: 0x00263F65
		public void setHoldMob(Mob r)
		{
			if (this.cx < r.x)
			{
				this.cdir = 1;
			}
			else
			{
				this.cdir = -1;
			}
			this.mobHold = r;
			this.holder = true;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00265D94 File Offset: 0x00263F94
		public void findNextFocusByKey()
		{
			if ((Char.myCharz().skillPaint != null || Char.myCharz().arr != null || Char.myCharz().dart != null || Char.myCharz().skillInfoPaint() != null) && this.focus.size() == 0)
			{
				return;
			}
			this.focus.removeAllElements();
			int num = 0;
			int num2 = GameScr.cmx + 10;
			int num3 = GameScr.cmx + GameCanvas.w - 10;
			int num4 = GameScr.cmy + 10;
			int num5 = GameScr.cmy + GameScr.gH;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				if (@char.statusMe != 15 && !@char.isInvisiblez && num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5 && @char.charID != -114 && (TileMap.mapID != 129 || (TileMap.mapID == 129 && Char.myCharz().cy > 264)))
				{
					this.focus.addElement(@char);
					if (this.charFocus != null && @char.Equals(this.charFocus))
					{
						num = this.focus.size();
					}
				}
			}
			if (this.me && this.isAttacPlayerStatus())
			{
				for (int j = 0; j < GameScr.vMob.size(); j++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(j);
					if (!GameScr.gI().isMeCanAttackMob(mob))
					{
						this.mobFocus = null;
					}
					else
					{
						this.focus.addElement(mob);
						if (this.mobFocus != null)
						{
							num = this.focus.size();
						}
					}
				}
				this.npcFocus = null;
				this.itemFocus = null;
				if (this.focus.size() > 0)
				{
					if (num >= this.focus.size())
					{
						num = 0;
					}
					this.FocusManualTo(this.focus.elementAt(num));
					return;
				}
				this.mobFocus = null;
				this.deFocusNPC();
				this.charFocus = null;
				this.itemFocus = null;
				Char.isManualFocus = false;
				return;
			}
			else
			{
				for (int k = 0; k < GameScr.vItemMap.size(); k++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
					if (num2 <= itemMap.x && itemMap.x <= num3 && num4 <= itemMap.y && itemMap.y <= num5)
					{
						this.focus.addElement(itemMap);
						if (this.itemFocus != null && itemMap.Equals(this.itemFocus))
						{
							num = this.focus.size();
						}
					}
				}
				for (int l = 0; l < GameScr.vMob.size(); l++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(l);
					if (mob2.status != 1 && mob2.status != 0 && num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5)
					{
						this.focus.addElement(mob2);
						if (this.mobFocus != null && mob2.Equals(this.mobFocus))
						{
							num = this.focus.size();
						}
					}
				}
				for (int m = 0; m < GameScr.vNpc.size(); m++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(m);
					if (npc.statusMe != 15 && num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5)
					{
						this.focus.addElement(npc);
						if (this.npcFocus != null && npc.Equals(this.npcFocus))
						{
							num = this.focus.size();
						}
					}
				}
				if (this.focus.size() > 0)
				{
					if (num >= this.focus.size())
					{
						num = 0;
					}
					this.FocusManualTo(this.focus.elementAt(num));
					return;
				}
				this.mobFocus = null;
				this.deFocusNPC();
				this.charFocus = null;
				this.itemFocus = null;
				Char.isManualFocus = false;
				return;
			}
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x002661D9 File Offset: 0x002643D9
		public void deFocusNPC()
		{
			if (this.me && this.npcFocus != null)
			{
				if (!GameCanvas.menu.showMenu)
				{
					Char.chatPopup = null;
				}
				this.npcFocus = null;
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00266204 File Offset: 0x00264404
		public void updateCharInBridge()
		{
			if (!GameCanvas.lowGraphic)
			{
				if (TileMap.tileTypeAt(this.cx, this.cy + 1, 1024))
				{
					TileMap.setTileTypeAtPixel(this.cx, this.cy + 1, 512);
					TileMap.setTileTypeAtPixel(this.cx, this.cy - 2, 512);
				}
				if (TileMap.tileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512))
				{
					TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512);
					TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy - 2, 512);
				}
				if (TileMap.tileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512))
				{
					TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512);
					TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy - 2, 512);
				}
			}
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00266320 File Offset: 0x00264520
		public void doInjure(long HPShow, long MPShow, bool isCrit, bool isMob)
		{
			this.isCrit = isCrit;
			this.isMob = isMob;
			this.cHP -= HPShow;
			this.cMP -= MPShow;
			GameScr.gI().isInjureHp = true;
			GameScr.gI().twHp = 0;
			GameScr.gI().isInjureMp = true;
			GameScr.gI().twMp = 0;
			if (this.cHP < 0L)
			{
				this.cHP = 0L;
			}
			if (this.cMP < 0L)
			{
				this.cMP = 0L;
			}
			if (isMob || (!isMob && this.cTypePk != 4 && this.damMP != -100))
			{
				if (HPShow <= 0L)
				{
					if (this.me)
					{
						GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS_ME);
					}
					else
					{
						GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS);
					}
				}
				else
				{
					GameScr.startFlyText("-" + HPShow.ToString(), this.cx, this.cy - this.ch, 0, -2, isCrit ? mFont.FATAL : mFont.RED);
				}
			}
			if (HPShow > 0L)
			{
				this.isInjure = 6;
			}
			ServerEffect.addServerEffect(80, this, 1);
			if (this.isDie)
			{
				this.isDie = false;
				Char.isLockKey = false;
				this.startDie((short)this.xSd, (short)this.ySd);
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x002664A0 File Offset: 0x002646A0
		public void doInjure()
		{
			GameScr.gI().isInjureHp = true;
			GameScr.gI().twHp = 0;
			GameScr.gI().isInjureMp = true;
			GameScr.gI().twMp = 0;
			this.isInjure = 6;
			ServerEffect.addServerEffect(8, this, 1);
			this.isInjureHp = true;
			this.twHp = 0;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x002664F8 File Offset: 0x002646F8
		public void startDie(short toX, short toY)
		{
			this.isMonkey = 0;
			this.isWaitMonkey = false;
			if (this.me && this.isDie)
			{
				return;
			}
			if (this.me)
			{
				this.isLockMove = true;
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					((Char)GameScr.vCharInMap.elementAt(i)).killCharId = -9999;
				}
				if (GameCanvas.panel != null && GameCanvas.panel.cp != null)
				{
					GameCanvas.panel.cp = null;
				}
				if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
				{
					GameCanvas.panel2.cp = null;
				}
			}
			this.statusMe = 5;
			this.cp2 = (int)toX;
			this.cp3 = (int)toY;
			this.cp1 = 0;
			this.cHP = 0L;
			this.testCharId = -9999;
			this.killCharId = -9999;
			if (this.me && this.myskill != null && this.myskill.template.id != 14)
			{
				this.stopUseChargeSkill();
			}
			this.cTypePk = 0;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00266609 File Offset: 0x00264809
		public void waitToDie(short toX, short toY)
		{
			this.wdx = toX;
			this.wdy = toY;
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x0026661C File Offset: 0x0026481C
		public void liveFromDead()
		{
			this.cHP = this.cHPFull;
			this.cMP = this.cMPFull;
			this.statusMe = 1;
			this.cp1 = (this.cp2 = (this.cp3 = 0));
			ServerEffect.addServerEffect(109, this, 2);
			GameScr.gI().center = null;
			GameScr.isHaveSelectSkill = true;
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x0026667C File Offset: 0x0026487C
		public bool doUsePotion()
		{
			if (this.arrItemBag == null)
			{
				return false;
			}
			for (int i = 0; i < this.arrItemBag.Length; i++)
			{
				if (this.arrItemBag[i] != null && this.arrItemBag[i].template.type == 6)
				{
					Service.gI().useItem(0, 1, -1, this.arrItemBag[i].template.id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x002666E8 File Offset: 0x002648E8
		public bool isLang()
		{
			return TileMap.mapID == 1 || TileMap.mapID == 27 || TileMap.mapID == 72 || TileMap.mapID == 10 || TileMap.mapID == 17 || TileMap.mapID == 22 || TileMap.mapID == 32 || TileMap.mapID == 38 || TileMap.mapID == 43 || TileMap.mapID == 48;
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x00266754 File Offset: 0x00264954
		public bool isMeCanAttackOtherPlayer(Char cAtt)
		{
			return cAtt != null && Char.myCharz().myskill != null && Char.myCharz().myskill.template.type != 2 && (Char.myCharz().myskill.template.type != 4 || cAtt.statusMe == 14 || cAtt.statusMe == 5) && (((cAtt.cTypePk == 3 && Char.myCharz().cTypePk == 3) || (Char.myCharz().cTypePk == 5 || cAtt.cTypePk == 5 || (Char.myCharz().cTypePk == 1 && cAtt.cTypePk == 1)) || (Char.myCharz().cTypePk == 4 && cAtt.cTypePk == 4) || (Char.myCharz().testCharId >= 0 && Char.myCharz().testCharId == cAtt.charID) || (Char.myCharz().killCharId >= 0 && Char.myCharz().killCharId == cAtt.charID && !this.isLang()) || (cAtt.killCharId >= 0 && cAtt.killCharId == Char.myCharz().charID && !this.isLang()) || (Char.myCharz().cFlag == 8 && cAtt.cFlag != 0) || (Char.myCharz().cFlag != 0 && cAtt.cFlag == 8) || (Char.myCharz().cFlag != cAtt.cFlag && Char.myCharz().cFlag != 0 && cAtt.cFlag != 0)) && cAtt.statusMe != 14) && cAtt.statusMe != 5;
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x002668F0 File Offset: 0x00264AF0
		public void clearTask()
		{
			Char.myCharz().taskMaint = null;
			for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
			{
				if (Char.myCharz().arrItemBag[i] != null && Char.myCharz().arrItemBag[i].template.type == 8)
				{
					Char.myCharz().arrItemBag[i] = null;
				}
			}
			Npc.clearEffTask();
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x00266958 File Offset: 0x00264B58
		public int getX()
		{
			return this.cx;
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x00266960 File Offset: 0x00264B60
		public int getY()
		{
			return this.cy;
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000125A8 File Offset: 0x000107A8
		public int getH()
		{
			return 32;
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000125AC File Offset: 0x000107AC
		public int getW()
		{
			return 24;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00266968 File Offset: 0x00264B68
		public void FocusManualTo(object objectz)
		{
			Mob mob = objectz as Mob;
			if (mob != null)
			{
				this.mobFocus = mob;
				this.deFocusNPC();
				this.charFocus = null;
				this.itemFocus = null;
			}
			else
			{
				Npc npc = objectz as Npc;
				if (npc != null)
				{
					Char.myCharz().mobFocus = null;
					Char.myCharz().deFocusNPC();
					Char.myCharz().npcFocus = npc;
					Char.myCharz().charFocus = null;
					Char.myCharz().itemFocus = null;
				}
				else
				{
					Char @char = objectz as Char;
					if (@char != null)
					{
						Char.myCharz().mobFocus = null;
						Char.myCharz().deFocusNPC();
						Char.myCharz().charFocus = @char;
						Char.myCharz().itemFocus = null;
					}
					else
					{
						ItemMap map = objectz as ItemMap;
						if (map != null)
						{
							Char.myCharz().mobFocus = null;
							Char.myCharz().deFocusNPC();
							Char.myCharz().charFocus = null;
							Char.myCharz().itemFocus = map;
						}
					}
				}
			}
			Char.isManualFocus = true;
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000034B9 File Offset: 0x000016B9
		public void stopMoving()
		{
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000034B9 File Offset: 0x000016B9
		public void cancelAttack()
		{
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isInvisible()
		{
			return false;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x00266A53 File Offset: 0x00264C53
		public bool focusToAttack()
		{
			return this.mobFocus != null || (this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus));
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x00266A78 File Offset: 0x00264C78
		public void addDustEff(int type)
		{
			if (GameCanvas.lowGraphic)
			{
				return;
			}
			switch (type)
			{
				case 1:
					if (this.clevel >= 9)
					{
						EffecMn.addEff(new Effect(19, this.cx - 5, this.cy + 20, 2, 1, -1));
						return;
					}
					break;
				case 2:
					if ((!this.me || this.isMonkey != 1) && this.isNhapThe && GameCanvas.gameTick % 5 == 0)
					{
						EffecMn.addEff(new Effect(22, this.cx - 5, this.cy + 35, 2, 1, -1));
						return;
					}
					break;
				case 3:
					if (this.clevel >= 9 && this.ySd - this.cy <= 5)
					{
						EffecMn.addEff(new Effect(19, this.cx - 5, this.ySd + 20, 2, 1, -1));
					}
					break;
				default:
					return;
			}
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x00266B50 File Offset: 0x00264D50
		public bool isGetFlagImage(sbyte getFlag)
		{
			bool result = true;
			for (int i = 0; i < GameScr.vFlag.size(); i++)
			{
				PKFlag pKFlag = (PKFlag)GameScr.vFlag.elementAt(i);
				if (pKFlag != null)
				{
					if (pKFlag.cflag == getFlag)
					{
						return true;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x00266B98 File Offset: 0x00264D98
		private void paintPKFlag(mGraphics g)
		{
			if (this.cdir == 1)
			{
				if (this.cFlag != 0 && this.cFlag != -1)
				{
					SmallImage.drawSmallImage(g, this.flagImage, this.cx - 10, this.cy - this.ch - ((!this.me) ? 30 : 30) + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), 2, 0);
					return;
				}
			}
			else if (this.cFlag != 0 && this.cFlag != -1)
			{
				SmallImage.drawSmallImage(g, this.flagImage, this.cx, this.cy - this.ch - ((!this.me) ? 30 : 30) + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), 0, 0);
			}
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x00266C6C File Offset: 0x00264E6C
		public void removeHoleEff()
		{
			if (this.holder)
			{
				this.holder = false;
				this.charHold = null;
				this.mobHold = null;
				return;
			}
			this.holdEffID = 0;
			this.charHold = null;
			this.mobHold = null;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x00266CA1 File Offset: 0x00264EA1
		public void removeProtectEff()
		{
			this.protectEff = false;
			this.eProtect = null;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x00266CB1 File Offset: 0x00264EB1
		public void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00266CBC File Offset: 0x00264EBC
		public void removeEffect()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
			if (this.holder)
			{
				this.holder = false;
			}
			if (this.protectEff)
			{
				this.protectEff = false;
			}
			this.eProtect = null;
			this.charHold = null;
			this.mobHold = null;
			this.blindEff = false;
			this.sleepEff = false;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x00266D1C File Offset: 0x00264F1C
		public void setPos(short xPos, short yPos, sbyte typePos)
		{
			this.isSetPos = true;
			this.xPos = xPos;
			this.yPos = yPos;
			this.typePos = typePos;
			this.tpos = 0;
			if (this.me)
			{
				if (GameCanvas.panel != null)
				{
					GameCanvas.panel.hide();
				}
				if (GameCanvas.panel2 != null)
				{
					GameCanvas.panel2.hide();
				}
			}
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x00266D76 File Offset: 0x00264F76
		public void removeHuytSao()
		{
			this.huytSao = false;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x00266D7F File Offset: 0x00264F7F
		public void fusionComplete()
		{
			this.isFusion = false;
			Char.isLockKey = false;
			this.tFusion = 0;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x00266D98 File Offset: 0x00264F98
		public void setFusion(sbyte fusion)
		{
			this.tFusion = 0;
			if (fusion == 4 || fusion == 5)
			{
				if (this.me)
				{
					Service.gI().funsion(fusion);
				}
				EffecMn.addEff(new Effect(34, this.cx, this.cy + 12, 2, 1, -1));
			}
			if (fusion == 6)
			{
				EffecMn.addEff(new Effect(38, this.cx, this.cy + 12, 2, 1, -1));
			}
			if (this.me)
			{
				GameCanvas.panel.hideNow();
				Char.isLockKey = true;
			}
			this.isFusion = true;
			if (fusion == 1)
			{
				this.isNhapThe = false;
				return;
			}
			this.isNhapThe = true;
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x00266E39 File Offset: 0x00265039
		public void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x00266E42 File Offset: 0x00265042
		public void setPartOld()
		{
			this.headTemp = this.head;
			this.bodyTemp = this.body;
			this.legTemp = this.leg;
			this.bagTemp = this.bag;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x00266E74 File Offset: 0x00265074
		public void setPartTemp(int head, int body, int leg, int bag)
		{
			if (head != -1)
			{
				this.head = head;
			}
			if (body != -1)
			{
				this.body = body;
			}
			if (leg != -1)
			{
				this.leg = leg;
			}
			if (bag != -1)
			{
				this.bag = bag;
			}
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x00266EA4 File Offset: 0x002650A4
		public void resetPartTemp()
		{
			if (this.headTemp != -1)
			{
				this.head = this.headTemp;
				this.headTemp = -1;
			}
			if (this.bodyTemp != -1)
			{
				this.body = this.bodyTemp;
				this.bodyTemp = -1;
			}
			if (this.legTemp != -1)
			{
				this.leg = this.legTemp;
				this.legTemp = -1;
			}
			if (this.bagTemp != -1)
			{
				this.bag = this.bagTemp;
				this.bagTemp = -1;
			}
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x00266F24 File Offset: 0x00265124
		public Effect getEffById(int id)
		{
			for (int i = 0; i < this.vEffChar.size(); i++)
			{
				Effect effect = (Effect)this.vEffChar.elementAt(i);
				if (effect.effId == id)
				{
					return effect;
				}
			}
			return null;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x00266F65 File Offset: 0x00265165
		public void addEffChar(Effect e)
		{
			this.removeEffChar(0, e.effId);
			this.vEffChar.addElement(e);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00266F80 File Offset: 0x00265180
		public void removeEffChar(int type, int id)
		{
			if (type == -1)
			{
				this.vEffChar.removeAllElements();
				return;
			}
			if (this.getEffById(id) != null)
			{
				this.vEffChar.removeElement(this.getEffById(id));
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x00266FB0 File Offset: 0x002651B0
		public void paintEffBehind(mGraphics g)
		{
			for (int i = 0; i < this.vEffChar.size(); i++)
			{
				Effect effect = (Effect)this.vEffChar.elementAt(i);
				if (effect.layer == 0)
				{
					bool flag = true;
					if (effect.isStand == 0)
					{
						flag = (this.statusMe == 1 || this.statusMe == 6);
					}
					if (flag)
					{
						effect.paint(g);
					}
				}
			}
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00267018 File Offset: 0x00265218
		public void paintEffFront(mGraphics g)
		{
			for (int i = 0; i < this.vEffChar.size(); i++)
			{
				Effect effect = (Effect)this.vEffChar.elementAt(i);
				if (effect.layer == 1)
				{
					bool flag = true;
					if (effect.isStand == 0)
					{
						flag = (this.statusMe == 1 || this.statusMe == 6);
					}
					if (flag)
					{
						effect.paint(g);
					}
				}
			}
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00267084 File Offset: 0x00265284
		public void updEffChar()
		{
			for (int i = 0; i < this.vEffChar.size(); i++)
			{
				((Effect)this.vEffChar.elementAt(i)).update();
			}
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x002670BD File Offset: 0x002652BD
		public int checkLuong()
		{
			return this.luong + this.luongKhoa;
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x002670CC File Offset: 0x002652CC
		public void updateEye()
		{
			if (this.head != 934)
			{
				return;
			}
			if (GameCanvas.timeNow - this.timeAddChopmat > 0L)
			{
				this.fChopmat++;
				if (this.fChopmat > this.frEye.Length - 1)
				{
					this.fChopmat = 0;
					this.timeAddChopmat = GameCanvas.timeNow + (long)Res.random(2000, 3500);
					this.frEye = this.frChopCham;
					if (Res.random(2) == 0)
					{
						this.frEye = this.frChopNhanh;
						return;
					}
				}
			}
			else
			{
				this.fChopmat = 0;
			}
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00267164 File Offset: 0x00265364
		private void paintRedEye(mGraphics g, int xx, int yy, int trans, int anchor)
		{
			if (this.head != 934 || (this.statusMe != 1 && this.statusMe != 6))
			{
				return;
			}
			if (Char.fraRedEye == null || Char.fraRedEye.imgFrame == null)
			{
				Char.fraRedEye = new FrameImage(mSystem.loadImage("/redeye.png"), 14, 10);
				return;
			}
			if (this.frEye[this.fChopmat] != -1)
			{
				int num = 8;
				int num2 = 15;
				if (trans == 2)
				{
					num = -8;
				}
				Char.fraRedEye.drawFrame(this.frEye[this.fChopmat], xx + num, yy + num2, trans, anchor, g);
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x002671FC File Offset: 0x002653FC
		public bool isHead_2Fr(int idHead)
		{
			for (int i = 0; i < Char.Arr_Head_2Fr.Length; i++)
			{
				if (Char.Arr_Head_2Fr[i][0] == idHead)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x0026722A File Offset: 0x0026542A
		private void updateFHead()
		{
			if (this.isHead_2Fr(this.head))
			{
				this.fHead++;
				if (this.fHead > 10000)
				{
					this.fHead = 0;
					return;
				}
			}
			else
			{
				this.fHead = 0;
			}
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x00267264 File Offset: 0x00265464
		private int getFHead(int idHead)
		{
			for (int i = 0; i < Char.Arr_Head_2Fr.Length; i++)
			{
				if (Char.Arr_Head_2Fr[i][0] == idHead)
				{
					return Char.Arr_Head_2Fr[i][this.fHead / 4 % Char.Arr_Head_2Fr[i].Length];
				}
			}
			return idHead;
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x002672AC File Offset: 0x002654AC
		public void paintAuraBehind(mGraphics g)
		{
			if (!ModFunc.GiamDungLuong && (!this.me || !Char.isPaintAura) && this.idAuraEff > -1 && (this.statusMe == 1 || this.statusMe == 6) && !GameCanvas.panel.isShow && mSystem.currentTimeMillis() - this.timeBlue > 0L)
			{
				FrameImage fraImage = mSystem.getFraImage(this.strEffAura + this.idAuraEff.ToString() + "_0");
				if (fraImage != null)
				{
					fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
			}
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0026736C File Offset: 0x0026556C
		public void paintAuraFront(mGraphics g)
		{
			if (ModFunc.GiamDungLuong || (this.me && !Char.isPaintAura) || this.idAuraEff <= -1)
			{
				return;
			}
			if (this.statusMe == 1 || this.statusMe == 6)
			{
				if (!GameCanvas.panel.isShow && !GameCanvas.lowGraphic)
				{
					bool flag = false;
					if (mSystem.currentTimeMillis() - this.timeBlue > -1000L && this.IsAddDust1)
					{
						flag = true;
						this.IsAddDust1 = false;
					}
					if (mSystem.currentTimeMillis() - this.timeBlue > -500L && this.IsAddDust2)
					{
						flag = true;
						this.IsAddDust2 = false;
					}
					if (flag)
					{
						GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
						GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
						this.addDustEff(1);
					}
					if (mSystem.currentTimeMillis() - this.timeBlue > 0L)
					{
						FrameImage fraImage = mSystem.getFraImage(this.strEffAura + this.idAuraEff.ToString() + "_1");
						if (fraImage != null)
						{
							fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy + 2, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
							return;
						}
					}
				}
			}
			else
			{
				this.timeBlue = mSystem.currentTimeMillis() + 1500L;
				this.IsAddDust1 = true;
				this.IsAddDust2 = true;
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x002674E8 File Offset: 0x002656E8
		public void paintEff_Lvup_behind(mGraphics g)
		{
			if (this.idEff_Set_Item != -1)
			{
				if (this.fraEff != null)
				{
					this.fraEff.drawFrame(GameCanvas.gameTick / 4 % this.fraEff.nFrame, this.cx, this.cy + 3, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					return;
				}
				this.fraEff = mSystem.getFraImage(this.strEff_Set_Item + this.idEff_Set_Item.ToString() + "_0");
			}
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x00267574 File Offset: 0x00265774
		public void paintEff_Lvup_front(mGraphics g)
		{
			if (this.idEff_Set_Item != -1)
			{
				if (this.fraEffSub != null)
				{
					this.fraEffSub.drawFrame(GameCanvas.gameTick / 4 % this.fraEffSub.nFrame, this.cx, this.cy + 8, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					return;
				}
				this.fraEffSub = mSystem.getFraImage(this.strEff_Set_Item + this.idEff_Set_Item.ToString() + "_1");
			}
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x00267600 File Offset: 0x00265800
		public void paintHat_behind(mGraphics g, int cf, int yh)
		{
			try
			{
				if (this.idHat != -1)
				{
					if (this.isFrNgang(cf))
					{
						if (this.fraHat_behind_2 != null)
						{
							this.fraHat_behind_2.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_behind_2.nFrame, this.cx + Char.hatInfo[cf][0] * ((this.cdir == 1) ? 1 : -1), yh + Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
						}
						else
						{
							this.fraHat_behind_2 = mSystem.getFraImage(this.strHat_behind + this.strNgang + this.idHat.ToString());
						}
					}
					else if (this.fraHat_behind != null)
					{
						this.fraHat_behind.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_behind.nFrame, this.cx + Char.hatInfo[cf][0] * ((this.cdir == 1) ? 1 : -1), yh + Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_behind = mSystem.getFraImage(this.strHat_behind + this.idHat.ToString());
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x0026776C File Offset: 0x0026596C
		public void paintHat_front(mGraphics g, int cf, int yh)
		{
			try
			{
				if (this.idHat != -1)
				{
					if (this.isFrNgang(cf))
					{
						if (this.fraHat_font_2 != null)
						{
							this.fraHat_font_2.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_font_2.nFrame, this.cx + Char.hatInfo[cf][0] * ((this.cdir == 1) ? 1 : -1), yh + Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
						}
						else
						{
							this.fraHat_font_2 = mSystem.getFraImage(this.strHat_font + this.strNgang + this.idHat.ToString());
						}
					}
					else if (this.fraHat_font != null)
					{
						this.fraHat_font.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_font.nFrame, this.cx + Char.hatInfo[cf][0] * ((this.cdir == 1) ? 1 : -1), yh + Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_font = mSystem.getFraImage(this.strHat_font + this.idHat.ToString());
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x002678D8 File Offset: 0x00265AD8
		public bool isFrNgang(int fr)
		{
			return fr == 2 || fr == 3 || fr == 4 || fr == 5 || fr == 6 || fr == 9 || fr == 10 || fr == 13 || fr == 14 || fr == 15 || fr == 16 || fr == 26 || fr == 27 || fr == 28 || fr == 29;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x00267930 File Offset: 0x00265B30
		public void sendNewAttack(short idTemplateSkill)
		{
			short x = -1;
			short y = -1;
			if (this.mobFocus != null)
			{
				x = (short)this.mobFocus.x;
				y = (short)this.mobFocus.y;
			}
			if (this.charFocus != null && !this.charFocus.isPet && !this.charFocus.isMiniPet)
			{
				x = (short)this.charFocus.cx;
				y = (short)this.charFocus.cy;
			}
			Service.gI().new_skill_not_focus((sbyte)idTemplateSkill, (sbyte)this.cdir, x, y);
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x002679B4 File Offset: 0x00265BB4
		public void SetSkillPaint_NEW(short idskillPaint, bool isFly, sbyte typeFrame, sbyte typePaint, sbyte dir, short timeGong, sbyte typeItem, sbyte level)
		{
			this.isPaintNewSkill = true;
			this.timeReset_newSkill = GameCanvas.timeNow + 10000L;
			this.idskillPaint = idskillPaint;
			this.isFly = isFly;
			this.typeFrame = typeFrame;
			this.typePaint = typePaint;
			this.typeItem = typeItem;
			this.cdir = (int)dir;
			this.count_NEW = 0;
			this.stt = 0;
			long lastTimeUseThisSkill = mSystem.currentTimeMillis();
			if (this.me)
			{
				this.saveLoadPreviousSkill();
				this.myskill.lastTimeUseThisSkill = lastTimeUseThisSkill;
				if (this.myskill.template.manaUseType == 2)
				{
					this.cMP = 1L;
				}
				else if (this.myskill.template.manaUseType != 1)
				{
					this.cMP -= (long)this.myskill.manaUse;
				}
				else
				{
					this.cMP -= (long)this.myskill.manaUse * this.cMPFull / 100L;
				}
				Char.myCharz().cStamina--;
				GameScr.gI().isInjureMp = true;
				GameScr.gI().twMp = 0;
				if (this.cMP < 0L)
				{
					this.cMP = 0L;
				}
			}
			switch (idskillPaint)
			{
				case 24:
					GameScr.addEffectEnd_Target(18, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0, level);
					GameScr.addEffectEnd_Target(21, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0, level);
					break;
				case 25:
					GameScr.addEffectEnd_Target(19, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0, level);
					GameScr.addEffectEnd_Target(22, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0, level);
					break;
				case 26:
					GameScr.addEffectEnd_Target(20, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0, level);
					GameScr.addEffectEnd_Target(23, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0, level);
					break;
			}
			if (this.typeFrame == 1)
			{
				if (!this.isFly)
				{
					this.fr_start = new byte[]
					{
						20,
						20,
						20,
						20,
						20,
						20,
						19
					};
					this.fr_atk = new byte[]
					{
						20
					};
					this.fr_end = new byte[1];
				}
				else
				{
					this.fr_start = new byte[]
					{
						31,
						31,
						31,
						31,
						31,
						31,
						30
					};
					this.fr_atk = new byte[]
					{
						31
					};
					this.fr_end = new byte[]
					{
						12
					};
				}
			}
			if (this.typeFrame == 2)
			{
				if (!this.isFly)
				{
					this.fr_start = new byte[]
					{
						20
					};
					this.fr_atk = new byte[]
					{
						13,
						13,
						13,
						14,
						14,
						14
					};
					this.fr_end = new byte[1];
				}
				else
				{
					this.fr_start = new byte[]
					{
						31
					};
					this.fr_atk = new byte[]
					{
						26,
						26,
						26,
						27,
						27,
						27
					};
					this.fr_end = new byte[]
					{
						12
					};
				}
			}
			if (this.typeFrame == 4)
			{
				if (!this.isFly)
				{
					this.fr_start = new byte[]
					{
						17,
						17,
						17,
						18,
						18,
						18
					};
					this.fr_atk = new byte[]
					{
						18
					};
					this.fr_end = new byte[1];
				}
				else
				{
					this.fr_start = new byte[]
					{
						7,
						7,
						7,
						12,
						12,
						12,
						12
					};
					this.fr_atk = new byte[]
					{
						12
					};
					this.fr_end = new byte[]
					{
						12
					};
				}
			}
			if (this.typeFrame == 3)
			{
				if (!this.isFly)
				{
					this.fr_start = new byte[]
					{
						24,
						24,
						24,
						17,
						17,
						17,
						18,
						18,
						18
					};
					this.fr_atk = new byte[]
					{
						20
					};
					this.fr_end = new byte[1];
					return;
				}
				this.fr_start = new byte[]
				{
					23,
					23,
					23,
					7,
					7,
					7,
					12,
					12,
					12,
					12
				};
				this.fr_atk = new byte[]
				{
					31
				};
				this.fr_end = new byte[]
				{
					12
				};
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x00267D8C File Offset: 0x00265F8C
		public void SetSkillPaint_STT(int stt, short idskillPaint, Point targetDame, short timeDame, short rangeDame, sbyte typePaint, Point[] listObj, sbyte typeItem, sbyte level)
		{
			this.stt = stt;
			this.idskillPaint = idskillPaint;
			this.count_NEW = 0;
			this.targetDame = targetDame;
			this.typePaint = typePaint;
			this.timeDame = mSystem.currentTimeMillis() + (long)timeDame;
			this.rangeDame = rangeDame;
			this.typeItem = typeItem;
			if (this.stt == 1)
			{
				if (this.idskillPaint == 24)
				{
					GameScr.addEffectEnd_Target(18, 1, (int)typePaint, this, null, 3, timeDame, 0, level);
					GameScr.addEffectEnd_Target(24, 0, (int)typePaint, this, this.targetDame, 1, timeDame, rangeDame, level);
				}
				if (this.idskillPaint == 25)
				{
					GameScr.addEffectEnd_Target(19, 0, (int)typePaint, this, null, 3, timeDame, 0, level);
					GameScr.addEffectEnd_Target(25, 0, (int)typePaint, this, this.targetDame, 1, timeDame, rangeDame, level);
				}
				if (this.idskillPaint == 26)
				{
					GameScr.addEffectEnd_Target(20, 0, (int)typePaint, this, null, 3, timeDame, 0, level);
					GameScr.addEffectEnd(26, (int)typeItem, (int)typePaint, targetDame.x, targetDame.y, 1, 0, timeDame, listObj, level);
				}
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x00267E8C File Offset: 0x0026608C
		public void UpdSkillPaint_NEW()
		{
			if (this.stt == 0)
			{
				if (this.isFly && this.count_NEW < 20)
				{
					this.cvy = -3;
					this.cy += this.cvy;
				}
				if (this.fr_start.Length == 1)
				{
					this.cf = (int)this.fr_start[0];
				}
				else if (this.count_NEW > this.fr_start.Length - 1)
				{
					this.cf = (int)this.fr_start[this.fr_start.Length - 1];
				}
				else
				{
					this.cf = (int)this.fr_start[this.count_NEW];
				}
			}
			else if (this.stt == 1)
			{
				this.cf = (int)this.fr_atk[this.count_NEW % this.fr_atk.Length];
				if (mSystem.currentTimeMillis() - this.timeDame > 0L)
				{
					this.SetSkillPaint_STT(2, 0, null, 0, 0, 0, null, 0, -1);
				}
				if (this.count_NEW % 5 == 0)
				{
					GameScr.shock_scr = 5;
				}
				if (this.typeFrame == 1 && this.count_NEW < 10 && !TileMap.tileTypeAt(this.cx - (this.chw + 1) * this.cdir, this.cy, (this.cdir != 1) ? 4 : 8))
				{
					this.cx -= this.cdir;
				}
				if (this.typeFrame == 2)
				{
				}
			}
			else if (this.stt == 2)
			{
				if (this.fr_end.Length == 1)
				{
					this.cf = (int)this.fr_end[0];
				}
				else if (this.count_NEW > this.fr_end.Length - 1)
				{
					this.cf = (int)this.fr_end[this.fr_end.Length - 1];
				}
				else
				{
					this.cf = (int)this.fr_end[this.count_NEW];
				}
				if (this.isFly)
				{
					this.cvx = (this.cvy = 0);
					this.statusMe = 4;
				}
				this.isPaintNewSkill = false;
			}
			this.count_NEW++;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x0026808C File Offset: 0x0026628C
		public Char clone()
		{
			Char @char = new Char();
			@char.charID = this.charID;
			@char.cx = this.cx;
			@char.cy = this.cy;
			@char.cdir = this.cdir;
			if (this.arrItemBody != null)
			{
				@char.arrItemBody = new Item[this.arrItemBody.Length];
				for (int i = 0; i < this.arrItemBody.Length; i++)
				{
					if (this.arrItemBody[i] == null)
					{
						@char.arrItemBody[i] = null;
					}
					else
					{
						@char.arrItemBody[i] = this.arrItemBody[i].clone();
					}
				}
			}
			return @char;
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x00268128 File Offset: 0x00266328
		public bool containsCaiTrang(int v)
		{
			if (this.arrItemBody != null)
			{
				for (int i = 0; i < this.arrItemBody.Length; i++)
				{
					if (this.arrItemBody[i] != null && this.arrItemBody[i].template != null && (int)this.arrItemBody[i].template.id == v)
					{
						return true;
					}
				}
			}
			Res.err("tim kiem id cai trang " + v.ToString() + " ko tim thay");
			return false;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0026819C File Offset: 0x0026639C
		// Note: this type is marked as 'beforefieldinit'.
		static Char()
		{
			int[][] array = new int[32][];
			array[0] = new int[]
			{
				5,
				-7
			};
			array[1] = new int[]
			{
				5,
				-7
			};
			array[2] = new int[]
			{
				5,
				-8
			};
			array[3] = new int[]
			{
				5,
				-7
			};
			array[4] = new int[]
			{
				5,
				-6
			};
			array[5] = new int[]
			{
				5,
				-8
			};
			array[6] = new int[]
			{
				5,
				-7
			};
			int num = 7;
			int[] array2 = new int[2];
			array2[0] = 9;
			array[num] = array2;
			array[8] = new int[]
			{
				11,
				1
			};
			int num2 = 9;
			int[] array3 = new int[2];
			array3[0] = 4;
			array[num2] = array3;
			array[10] = new int[]
			{
				4,
				-1
			};
			array[11] = new int[]
			{
				4,
				8
			};
			array[12] = new int[]
			{
				6,
				5
			};
			array[13] = new int[]
			{
				6,
				-6
			};
			array[14] = new int[]
			{
				2,
				-5
			};
			array[15] = new int[]
			{
				7,
				-8
			};
			array[16] = new int[]
			{
				7,
				-6
			};
			int num3 = 17;
			int[] array4 = new int[2];
			array4[0] = 8;
			array[num3] = array4;
			array[18] = new int[]
			{
				7,
				5
			};
			array[19] = new int[]
			{
				9,
				-7
			};
			array[20] = new int[]
			{
				7,
				-3
			};
			array[21] = new int[]
			{
				2,
				8
			};
			array[22] = new int[]
			{
				4,
				5
			};
			array[23] = new int[]
			{
				10,
				-5
			};
			array[24] = new int[]
			{
				9,
				-5
			};
			array[25] = new int[]
			{
				9,
				-5
			};
			array[26] = new int[]
			{
				6,
				-6
			};
			array[27] = new int[]
			{
				2,
				-5
			};
			array[28] = new int[]
			{
				7,
				-8
			};
			array[29] = new int[]
			{
				7,
				-6
			};
			array[30] = new int[]
			{
				9,
				-7
			};
			array[31] = new int[]
			{
				7,
				-3
			};
			Char.hatInfo = array;
		}

		// Token: 0x04004AE2 RID: 19170
		public string xuStr;

		// Token: 0x04004AE3 RID: 19171
		public string luongStr;

		// Token: 0x04004AE4 RID: 19172
		public string luongKhoaStr;

		// Token: 0x04004AE5 RID: 19173
		public long lastUpdateTime;

		// Token: 0x04004AE6 RID: 19174
		public bool meLive;

		// Token: 0x04004AE7 RID: 19175
		public bool isMask;

		// Token: 0x04004AE8 RID: 19176
		public bool isTeleport;

		// Token: 0x04004AE9 RID: 19177
		public bool isUsePlane;

		// Token: 0x04004AEA RID: 19178
		public int shadowX;

		// Token: 0x04004AEB RID: 19179
		public int shadowY;

		// Token: 0x04004AEC RID: 19180
		public int shadowLife;

		// Token: 0x04004AED RID: 19181
		public bool isNhapThe;

		// Token: 0x04004AEE RID: 19182
		public PetFollow petFollow;

		// Token: 0x04004AEF RID: 19183
		public int rank;

		// Token: 0x04004AF0 RID: 19184
		public const sbyte A_STAND = 1;

		// Token: 0x04004AF1 RID: 19185
		public const sbyte A_RUN = 2;

		// Token: 0x04004AF2 RID: 19186
		public const sbyte A_JUMP = 3;

		// Token: 0x04004AF3 RID: 19187
		public const sbyte A_FALL = 4;

		// Token: 0x04004AF4 RID: 19188
		public const sbyte A_DEADFLY = 5;

		// Token: 0x04004AF5 RID: 19189
		public const sbyte A_NOTHING = 6;

		// Token: 0x04004AF6 RID: 19190
		public const sbyte A_ATTK = 7;

		// Token: 0x04004AF7 RID: 19191
		public const sbyte A_INJURE = 8;

		// Token: 0x04004AF8 RID: 19192
		public const sbyte A_AUTOJUMP = 9;

		// Token: 0x04004AF9 RID: 19193
		public const sbyte A_FLY = 10;

		// Token: 0x04004AFA RID: 19194
		public const sbyte SKILL_STAND = 12;

		// Token: 0x04004AFB RID: 19195
		public const sbyte SKILL_FALL = 13;

		// Token: 0x04004AFC RID: 19196
		public const sbyte A_DEAD = 14;

		// Token: 0x04004AFD RID: 19197
		public const sbyte A_HIDE = 15;

		// Token: 0x04004AFE RID: 19198
		public const sbyte A_RESETPOINT = 16;

		// Token: 0x04004AFF RID: 19199
		public static ChatPopup chatPopup;

		// Token: 0x04004B00 RID: 19200
		public long cPower;

		// Token: 0x04004B01 RID: 19201
		public Info chatInfo;

		// Token: 0x04004B02 RID: 19202
		public sbyte petStatus;

		// Token: 0x04004B03 RID: 19203
		public int cx = 24;

		// Token: 0x04004B04 RID: 19204
		public int cy = 24;

		// Token: 0x04004B05 RID: 19205
		public int cvx;

		// Token: 0x04004B06 RID: 19206
		public int cvy;

		// Token: 0x04004B07 RID: 19207
		public int cp1;

		// Token: 0x04004B08 RID: 19208
		public int cp2;

		// Token: 0x04004B09 RID: 19209
		public int cp3;

		// Token: 0x04004B0A RID: 19210
		public int statusMe = 5;

		// Token: 0x04004B0B RID: 19211
		public int cdir = 1;

		// Token: 0x04004B0C RID: 19212
		public int charID;

		// Token: 0x04004B0D RID: 19213
		public int cgender;

		// Token: 0x04004B0E RID: 19214
		public int ctaskId;

		// Token: 0x04004B0F RID: 19215
		public int menuSelect;

		// Token: 0x04004B10 RID: 19216
		public int cBonusSpeed;

		// Token: 0x04004B11 RID: 19217
		public int cspeed = 4;

		// Token: 0x04004B12 RID: 19218
		public int ccurrentAttack;

		// Token: 0x04004B13 RID: 19219
		public long cDamFull;

		// Token: 0x04004B14 RID: 19220
		public long cDefull;

		// Token: 0x04004B15 RID: 19221
		public int cCriticalFull;

		// Token: 0x04004B16 RID: 19222
		public int clevel;

		// Token: 0x04004B17 RID: 19223
		public long cMP;

		// Token: 0x04004B18 RID: 19224
		public long cHP;

		// Token: 0x04004B19 RID: 19225
		public long cHPNew;

		// Token: 0x04004B1A RID: 19226
		public int cMaxEXP;

		// Token: 0x04004B1B RID: 19227
		public long cHPShow;

		// Token: 0x04004B1C RID: 19228
		public int xReload;

		// Token: 0x04004B1D RID: 19229
		public int yReload;

		// Token: 0x04004B1E RID: 19230
		public int cyStartFall;

		// Token: 0x04004B1F RID: 19231
		public int saveStatus;

		// Token: 0x04004B20 RID: 19232
		public int eff5BuffHp;

		// Token: 0x04004B21 RID: 19233
		public int eff5BuffMp;

		// Token: 0x04004B22 RID: 19234
		public long cHPFull;

		// Token: 0x04004B23 RID: 19235
		public long cMPFull;

		// Token: 0x04004B24 RID: 19236
		public int cdameDown;

		// Token: 0x04004B25 RID: 19237
		public int cStr;

		// Token: 0x04004B26 RID: 19238
		public long cLevelPercent;

		// Token: 0x04004B27 RID: 19239
		public long cTiemNang;

		// Token: 0x04004B28 RID: 19240
		public long cNangdong;

		// Token: 0x04004B29 RID: 19241
		public long damHP;

		// Token: 0x04004B2A RID: 19242
		public int damMP;

		// Token: 0x04004B2B RID: 19243
		public bool isMob;

		// Token: 0x04004B2C RID: 19244
		public bool isCrit;

		// Token: 0x04004B2D RID: 19245
		public bool isDie;

		// Token: 0x04004B2E RID: 19246
		public int pointUydanh;

		// Token: 0x04004B2F RID: 19247
		public int pointNon;

		// Token: 0x04004B30 RID: 19248
		public int pointVukhi;

		// Token: 0x04004B31 RID: 19249
		public int pointAo;

		// Token: 0x04004B32 RID: 19250
		public int pointLien;

		// Token: 0x04004B33 RID: 19251
		public int pointGangtay;

		// Token: 0x04004B34 RID: 19252
		public int pointNhan;

		// Token: 0x04004B35 RID: 19253
		public int pointQuan;

		// Token: 0x04004B36 RID: 19254
		public int pointNgocboi;

		// Token: 0x04004B37 RID: 19255
		public int pointGiay;

		// Token: 0x04004B38 RID: 19256
		public int pointPhu;

		// Token: 0x04004B39 RID: 19257
		public int countFinishDay;

		// Token: 0x04004B3A RID: 19258
		public int countLoopBoos;

		// Token: 0x04004B3B RID: 19259
		public int limitTiemnangso;

		// Token: 0x04004B3C RID: 19260
		public int limitKynangso;

		// Token: 0x04004B3D RID: 19261
		public short[] potential = new short[4];

		// Token: 0x04004B3E RID: 19262
		public string cName = string.Empty;

		// Token: 0x04004B3F RID: 19263
		public int clanID;

		// Token: 0x04004B40 RID: 19264
		public sbyte ctypeClan;

		// Token: 0x04004B41 RID: 19265
		public Clan clan;

		// Token: 0x04004B42 RID: 19266
		public sbyte role;

		// Token: 0x04004B43 RID: 19267
		public int cw = 22;

		// Token: 0x04004B44 RID: 19268
		public int ch = 32;

		// Token: 0x04004B45 RID: 19269
		public int chw = 11;

		// Token: 0x04004B46 RID: 19270
		public int chh = 16;

		// Token: 0x04004B47 RID: 19271
		public Command cmdMenu;

		// Token: 0x04004B48 RID: 19272
		public bool canFly = true;

		// Token: 0x04004B49 RID: 19273
		public bool cmtoChar;

		// Token: 0x04004B4A RID: 19274
		public bool me;

		// Token: 0x04004B4B RID: 19275
		public bool cFinishedAttack;

		// Token: 0x04004B4C RID: 19276
		public bool cchistlast;

		// Token: 0x04004B4D RID: 19277
		public bool isAttack;

		// Token: 0x04004B4E RID: 19278
		public bool isAttFly;

		// Token: 0x04004B4F RID: 19279
		public int cwpt;

		// Token: 0x04004B50 RID: 19280
		public int cwplv;

		// Token: 0x04004B51 RID: 19281
		public int cf;

		// Token: 0x04004B52 RID: 19282
		public int tick;

		// Token: 0x04004B53 RID: 19283
		public static bool fallAttack;

		// Token: 0x04004B54 RID: 19284
		public bool isJump;

		// Token: 0x04004B55 RID: 19285
		public bool autoFall;

		// Token: 0x04004B56 RID: 19286
		public bool attack = true;

		// Token: 0x04004B57 RID: 19287
		public long xu;

		// Token: 0x04004B58 RID: 19288
		public int xuInBox;

		// Token: 0x04004B59 RID: 19289
		public int yen;

		// Token: 0x04004B5A RID: 19290
		public int gold_lock;

		// Token: 0x04004B5B RID: 19291
		public int luong;

		// Token: 0x04004B5C RID: 19292
		public int luongKhoa;

		// Token: 0x04004B5D RID: 19293
		public NClass nClass;

		// Token: 0x04004B5E RID: 19294
		public Command endMovePointCommand;

		// Token: 0x04004B5F RID: 19295
		public MyVector vSkill = new MyVector();

		// Token: 0x04004B60 RID: 19296
		public MyVector vSkillFight = new MyVector();

		// Token: 0x04004B61 RID: 19297
		public MyVector vEff = new MyVector();

		// Token: 0x04004B62 RID: 19298
		public Skill myskill;

		// Token: 0x04004B63 RID: 19299
		public Task taskMaint;

		// Token: 0x04004B64 RID: 19300
		public bool paintName = true;

		// Token: 0x04004B65 RID: 19301
		public Archivement[] arrArchive;

		// Token: 0x04004B66 RID: 19302
		public Item[] arrItemBag;

		// Token: 0x04004B67 RID: 19303
		public Item[] arrItemBox;

		// Token: 0x04004B68 RID: 19304
		public Item[] arrItemBody;

		// Token: 0x04004B69 RID: 19305
		public Skill[] arrPetSkill;

		// Token: 0x04004B6A RID: 19306
		public Item[][] arrItemShop;

		// Token: 0x04004B6B RID: 19307
		public string[][] infoSpeacialSkill;

		// Token: 0x04004B6C RID: 19308
		public short[][] imgSpeacialSkill;

		// Token: 0x04004B6D RID: 19309
		public short cResFire;

		// Token: 0x04004B6E RID: 19310
		public short cResIce;

		// Token: 0x04004B6F RID: 19311
		public short cResWind;

		// Token: 0x04004B70 RID: 19312
		public short cMiss;

		// Token: 0x04004B71 RID: 19313
		public short cExactly;

		// Token: 0x04004B72 RID: 19314
		public short cFatal;

		// Token: 0x04004B73 RID: 19315
		public sbyte cPk;

		// Token: 0x04004B74 RID: 19316
		public sbyte cTypePk;

		// Token: 0x04004B75 RID: 19317
		public short cReactDame;

		// Token: 0x04004B76 RID: 19318
		public short sysUp;

		// Token: 0x04004B77 RID: 19319
		public short sysDown;

		// Token: 0x04004B78 RID: 19320
		public int avatar;

		// Token: 0x04004B79 RID: 19321
		public int skillTemplateId;

		// Token: 0x04004B7A RID: 19322
		public Mob mobFocus;

		// Token: 0x04004B7B RID: 19323
		public Mob mobMe;

		// Token: 0x04004B7C RID: 19324
		public int tMobMeBorn;

		// Token: 0x04004B7D RID: 19325
		public Npc npcFocus;

		// Token: 0x04004B7E RID: 19326
		public Char charFocus;

		// Token: 0x04004B7F RID: 19327
		public ItemMap itemFocus;

		// Token: 0x04004B80 RID: 19328
		public MyVector focus = new MyVector();

		// Token: 0x04004B81 RID: 19329
		public Mob[] attMobs;

		// Token: 0x04004B82 RID: 19330
		public Char[] attChars;

		// Token: 0x04004B83 RID: 19331
		public short[] moveFast;

		// Token: 0x04004B84 RID: 19332
		public int testCharId = -9999;

		// Token: 0x04004B85 RID: 19333
		public int killCharId = -9999;

		// Token: 0x04004B86 RID: 19334
		public sbyte resultTest;

		// Token: 0x04004B87 RID: 19335
		public int countKill;

		// Token: 0x04004B88 RID: 19336
		public int countKillMax;

		// Token: 0x04004B89 RID: 19337
		public bool isInvisiblez;

		// Token: 0x04004B8A RID: 19338
		public bool isShadown = true;

		// Token: 0x04004B8B RID: 19339
		public const sbyte PK_NORMAL = 0;

		// Token: 0x04004B8C RID: 19340
		public const sbyte PK_PHE = 1;

		// Token: 0x04004B8D RID: 19341
		public const sbyte PK_BANG = 2;

		// Token: 0x04004B8E RID: 19342
		public const sbyte PK_THIDAU = 3;

		// Token: 0x04004B8F RID: 19343
		public const sbyte PK_LUYENTAP = 4;

		// Token: 0x04004B90 RID: 19344
		public const sbyte PK_TUDO = 5;

		// Token: 0x04004B91 RID: 19345
		public MyVector taskOrders = new MyVector();

		// Token: 0x04004B92 RID: 19346
		public int cStamina;

		// Token: 0x04004B93 RID: 19347
		public static short[] idHead;

		// Token: 0x04004B94 RID: 19348
		public static short[] idAvatar;

		// Token: 0x04004B95 RID: 19349
		public int exp;

		// Token: 0x04004B96 RID: 19350
		public string[] strLevel;

		// Token: 0x04004B97 RID: 19351
		public string currStrLevel;

		// Token: 0x04004B98 RID: 19352
		public static Image eyeTraiDat = GameCanvas.loadImage("/mainImage/myTexture2dmat-trai-dat.png");

		// Token: 0x04004B99 RID: 19353
		public static Image eyeNamek = GameCanvas.loadImage("/mainImage/myTexture2dmat-namek.png");

		// Token: 0x04004B9A RID: 19354
		public bool isFreez;

		// Token: 0x04004B9B RID: 19355
		public bool isCharge;

		// Token: 0x04004B9C RID: 19356
		public int seconds;

		// Token: 0x04004B9D RID: 19357
		public int freezSeconds;

		// Token: 0x04004B9E RID: 19358
		public long last;

		// Token: 0x04004B9F RID: 19359
		public long cur;

		// Token: 0x04004BA0 RID: 19360
		public long lastFreez;

		// Token: 0x04004BA1 RID: 19361
		public long currFreez;

		// Token: 0x04004BA2 RID: 19362
		public bool isFlyUp;

		// Token: 0x04004BA3 RID: 19363
		public static MyVector vItemTime = new MyVector();

		// Token: 0x04004BA4 RID: 19364
		public static short ID_NEW_MOUNT = 30000;

		// Token: 0x04004BA5 RID: 19365
		public short idMount;

		// Token: 0x04004BA6 RID: 19366
		public bool isHaveMount;

		// Token: 0x04004BA7 RID: 19367
		public bool isMountVip;

		// Token: 0x04004BA8 RID: 19368
		public bool isEventMount;

		// Token: 0x04004BA9 RID: 19369
		public bool isSpeacialMount;

		// Token: 0x04004BAA RID: 19370
		public static Image imgMount_TD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi10.png");

		// Token: 0x04004BAB RID: 19371
		public static Image imgMount_NM = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi20.png");

		// Token: 0x04004BAC RID: 19372
		public static Image imgMount_NM_1 = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi21.png");

		// Token: 0x04004BAD RID: 19373
		public static Image imgMount_XD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi30.png");

		// Token: 0x04004BAE RID: 19374
		public static Image imgMount_TD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi11.png");

		// Token: 0x04004BAF RID: 19375
		public static Image imgMount_NM_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi22.png");

		// Token: 0x04004BB0 RID: 19376
		public static Image imgMount_NM_1_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi23.png");

		// Token: 0x04004BB1 RID: 19377
		public static Image imgMount_XD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi31.png");

		// Token: 0x04004BB2 RID: 19378
		public static Image imgEventMount = GameCanvas.loadImage("/mainImage/myTexture2drong.png");

		// Token: 0x04004BB3 RID: 19379
		public static Image imgEventMountWing = GameCanvas.loadImage("/mainImage/myTexture2dcanhrong.png");

		// Token: 0x04004BB4 RID: 19380
		public sbyte[] FrameMount = new sbyte[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			1,
			1
		};

		// Token: 0x04004BB5 RID: 19381
		public int frameMount;

		// Token: 0x04004BB6 RID: 19382
		public int frameNewMount;

		// Token: 0x04004BB7 RID: 19383
		public int transMount;

		// Token: 0x04004BB8 RID: 19384
		public int genderMount;

		// Token: 0x04004BB9 RID: 19385
		public int idcharMount;

		// Token: 0x04004BBA RID: 19386
		public int xMount;

		// Token: 0x04004BBB RID: 19387
		public int yMount;

		// Token: 0x04004BBC RID: 19388
		public int dxMount;

		// Token: 0x04004BBD RID: 19389
		public int dyMount;

		// Token: 0x04004BBE RID: 19390
		public int xChar;

		// Token: 0x04004BBF RID: 19391
		public int xdis;

		// Token: 0x04004BC0 RID: 19392
		public int speedMount;

		// Token: 0x04004BC1 RID: 19393
		public bool isStartMount;

		// Token: 0x04004BC2 RID: 19394
		public bool isMount;

		// Token: 0x04004BC3 RID: 19395
		public bool isEndMount;

		// Token: 0x04004BC4 RID: 19396
		public sbyte cFlag;

		// Token: 0x04004BC5 RID: 19397
		public int flagImage;

		// Token: 0x04004BC6 RID: 19398
		public short x_hint;

		// Token: 0x04004BC7 RID: 19399
		public short y_hint;

		// Token: 0x04004BC8 RID: 19400
		public short s_danhHieu1;

		// Token: 0x04004BC9 RID: 19401
		public static int[][][] CharInfo = new int[][][]
		{
			new int[][]
			{
				new int[]
				{
					0,
					-13,
					34
				},
				new int[]
				{
					1,
					-8,
					10
				},
				new int[]
				{
					1,
					-9,
					16
				},
				new int[]
				{
					1,
					-9,
					45
				}
			},
			new int[][]
			{
				new int[]
				{
					0,
					-13,
					35
				},
				new int[]
				{
					1,
					-8,
					10
				},
				new int[]
				{
					1,
					-9,
					17
				},
				new int[]
				{
					1,
					-9,
					46
				}
			},
			new int[][]
			{
				new int[]
				{
					1,
					-10,
					33
				},
				new int[]
				{
					2,
					-10,
					11
				},
				new int[]
				{
					2,
					-8,
					16
				},
				new int[]
				{
					1,
					-12,
					49
				}
			},
			new int[][]
			{
				new int[]
				{
					1,
					-10,
					32
				},
				new int[]
				{
					3,
					-12,
					10
				},
				new int[]
				{
					3,
					-11,
					15
				},
				new int[]
				{
					1,
					-13,
					47
				}
			},
			new int[][]
			{
				new int[]
				{
					1,
					-10,
					34
				},
				new int[]
				{
					4,
					-8,
					11
				},
				new int[]
				{
					4,
					-7,
					17
				},
				new int[]
				{
					1,
					-12,
					47
				}
			},
			new int[][]
			{
				new int[]
				{
					1,
					-10,
					34
				},
				new int[]
				{
					5,
					-12,
					11
				},
				new int[]
				{
					5,
					-9,
					17
				},
				new int[]
				{
					1,
					-13,
					49
				}
			},
			new int[][]
			{
				new int[]
				{
					1,
					-10,
					33
				},
				new int[]
				{
					6,
					-10,
					10
				},
				new int[]
				{
					6,
					-8,
					16
				},
				new int[]
				{
					1,
					-12,
					47
				}
			},
			new int[][]
			{
				new int[]
				{
					0,
					-9,
					36
				},
				new int[]
				{
					7,
					-5,
					17
				},
				new int[]
				{
					7,
					-11,
					25
				},
				new int[]
				{
					1,
					-8,
					49
				}
			},
			new int[][]
			{
				new int[]
				{
					0,
					-7,
					35
				},
				new int[]
				{
					0,
					-18,
					22
				},
				new int[]
				{
					7,
					-10,
					25
				},
				new int[]
				{
					1,
					-7,
					48
				}
			},
			new int[][]
			{
				new int[]
				{
					1,
					-11,
					35
				},
				new int[]
				{
					10,
					-3,
					25
				},
				new int[]
				{
					12,
					-10,
					26
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					1,
					-11,
					37
				},
				new int[]
				{
					11,
					-3,
					25
				},
				new int[]
				{
					12,
					-11,
					27
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-14,
					34
				},
				new int[]
				{
					12,
					-8,
					21
				},
				new int[]
				{
					9,
					-7,
					31
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-12,
					35
				},
				new int[]
				{
					8,
					-5,
					14
				},
				new int[]
				{
					8,
					-15,
					29
				},
				new int[]
				{
					1,
					-9,
					49
				}
			},
			new int[][]
			{
				new int[]
				{
					1,
					-9,
					34
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					10,
					-7,
					19
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					1,
					-13,
					34
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					11,
					-10,
					19
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					1,
					-8,
					32
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					2,
					-6,
					15
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					1,
					-8,
					32
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					13,
					-12,
					16
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-10,
					31
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					7,
					-13,
					20
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-11,
					32
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					8,
					-15,
					26
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-9,
					33
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					14,
					-8,
					18
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-11,
					33
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					15,
					-6,
					19
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-16,
					31
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					9,
					-8,
					28
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-14,
					34
				},
				new int[]
				{
					1,
					-8,
					10
				},
				new int[]
				{
					8,
					-16,
					28
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-8,
					36
				},
				new int[]
				{
					7,
					-5,
					17
				},
				new int[]
				{
					0,
					-5,
					25
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-9,
					31
				},
				new int[]
				{
					9,
					-12,
					9
				},
				new int[]
				{
					0,
					-6,
					20
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					2,
					-9,
					36
				},
				new int[]
				{
					13,
					-5,
					17
				},
				new int[]
				{
					16,
					-11,
					25
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					1,
					-9,
					34
				},
				new int[]
				{
					8,
					-5,
					13
				},
				new int[]
				{
					10,
					-7,
					19
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					1,
					-13,
					34
				},
				new int[]
				{
					8,
					-5,
					13
				},
				new int[]
				{
					11,
					-10,
					19
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					1,
					-8,
					32
				},
				new int[]
				{
					8,
					-5,
					13
				},
				new int[]
				{
					2,
					-6,
					15
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					1,
					-8,
					32
				},
				new int[]
				{
					8,
					-5,
					13
				},
				new int[]
				{
					13,
					-12,
					16
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-9,
					33
				},
				new int[]
				{
					8,
					-5,
					13
				},
				new int[]
				{
					14,
					-8,
					18
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-11,
					33
				},
				new int[]
				{
					8,
					-5,
					13
				},
				new int[]
				{
					15,
					-6,
					19
				},
				new int[3]
			},
			new int[][]
			{
				new int[]
				{
					0,
					-16,
					32
				},
				new int[]
				{
					8,
					-5,
					13
				},
				new int[]
				{
					9,
					-8,
					29
				},
				new int[3]
			}
		};

		// Token: 0x04004BCA RID: 19402
		public static int[] CHAR_WEAPONX = new int[]
		{
			-2,
			-6,
			22,
			21,
			19,
			22,
			10,
			-2,
			-2,
			5,
			19
		};

		// Token: 0x04004BCB RID: 19403
		public static int[] CHAR_WEAPONY = new int[]
		{
			9,
			22,
			25,
			17,
			26,
			37,
			36,
			49,
			50,
			52,
			36
		};

		// Token: 0x04004BCC RID: 19404
		private static Char myChar;

		// Token: 0x04004BCD RID: 19405
		private static Char myPet;

		// Token: 0x04004BCE RID: 19406
		private static Char myPet2;

		// Token: 0x04004BCF RID: 19407
		public static int[] listAttack;

		// Token: 0x04004BD0 RID: 19408
		public static int[][] listIonC;

		// Token: 0x04004BD1 RID: 19409
		public int cvyJump;

		// Token: 0x04004BD2 RID: 19410
		private int indexUseSkill = -1;

		// Token: 0x04004BD3 RID: 19411
		public int cxSend;

		// Token: 0x04004BD4 RID: 19412
		public int cySend;

		// Token: 0x04004BD5 RID: 19413
		public int cdirSend = 1;

		// Token: 0x04004BD6 RID: 19414
		public int cxFocus;

		// Token: 0x04004BD7 RID: 19415
		public int cyFocus;

		// Token: 0x04004BD8 RID: 19416
		public int cactFirst = 5;

		// Token: 0x04004BD9 RID: 19417
		public MyVector vMovePoints = new MyVector();

		// Token: 0x04004BDA RID: 19418
		public static string[][] inforClass = new string[][]
		{
			new string[]
			{
				"1",
				"1",
				"chiêu 1",
				"0"
			},
			new string[]
			{
				"2",
				"2",
				"chiêu 2",
				"5"
			}
		};

		// Token: 0x04004BDB RID: 19419
		public static int[][] inforSkill = new int[][]
		{
			new int[]
			{
				1,
				0,
				1,
				1000,
				40,
				1,
				0,
				20,
				0,
				0,
				0,
				0
			},
			new int[]
			{
				2,
				1,
				10,
				1000,
				100,
				1,
				0,
				40,
				0,
				0,
				0,
				0
			},
			new int[]
			{
				2,
				2,
				11,
				800,
				100,
				1,
				0,
				45,
				0,
				0,
				0,
				0
			},
			new int[]
			{
				2,
				3,
				12,
				600,
				100,
				1,
				0,
				50,
				0,
				0,
				0,
				0
			},
			new int[]
			{
				2,
				4,
				13,
				500,
				100,
				1,
				0,
				55,
				0,
				0,
				0,
				0
			},
			new int[]
			{
				3,
				1,
				14,
				500,
				100,
				1,
				0,
				60,
				0,
				0,
				0,
				0
			},
			new int[]
			{
				3,
				2,
				14,
				500,
				100,
				1,
				0,
				60,
				0,
				0,
				0,
				0
			},
			new int[]
			{
				3,
				3,
				14,
				500,
				100,
				1,
				0,
				60,
				0,
				0,
				0,
				0
			},
			new int[]
			{
				3,
				4,
				14,
				500,
				100,
				1,
				0,
				60,
				0,
				0,
				0,
				0
			},
			new int[]
			{
				3,
				5,
				14,
				500,
				100,
				1,
				0,
				60,
				0,
				0,
				0,
				0
			}
		};

		// Token: 0x04004BDC RID: 19420
		public static bool flag;

		// Token: 0x04004BDD RID: 19421
		public static bool ischangingMap;

		// Token: 0x04004BDE RID: 19422
		public static bool isLockKey;

		// Token: 0x04004BDF RID: 19423
		public static bool isLoadingMap;

		// Token: 0x04004BE0 RID: 19424
		public bool isLockMove;

		// Token: 0x04004BE1 RID: 19425
		public bool isLockAttack;

		// Token: 0x04004BE2 RID: 19426
		public string strInfo;

		// Token: 0x04004BE3 RID: 19427
		public short powerPoint;

		// Token: 0x04004BE4 RID: 19428
		public short maxPowerPoint;

		// Token: 0x04004BE5 RID: 19429
		public short secondPower;

		// Token: 0x04004BE6 RID: 19430
		public long lastS;

		// Token: 0x04004BE7 RID: 19431
		public long currS;

		// Token: 0x04004BE8 RID: 19432
		public bool havePet;

		// Token: 0x04004BE9 RID: 19433
		public bool havePet2;

		// Token: 0x04004BEA RID: 19434
		public MovePoint currentMovePoint;

		// Token: 0x04004BEB RID: 19435
		public int bom;

		// Token: 0x04004BEC RID: 19436
		public int delayFall;

		// Token: 0x04004BED RID: 19437
		private bool isSoundJump;

		// Token: 0x04004BEE RID: 19438
		public int lastFrame;

		// Token: 0x04004BEF RID: 19439
		private Effect eProtect;

		// Token: 0x04004BF0 RID: 19440
		private Effect eDanhHieu;

		// Token: 0x04004BF1 RID: 19441
		private int twHp;

		// Token: 0x04004BF2 RID: 19442
		public bool isInjureHp;

		// Token: 0x04004BF3 RID: 19443
		public bool changePos;

		// Token: 0x04004BF4 RID: 19444
		public bool isHide;

		// Token: 0x04004BF5 RID: 19445
		private bool wy;

		// Token: 0x04004BF6 RID: 19446
		public int wt;

		// Token: 0x04004BF7 RID: 19447
		public int fy;

		// Token: 0x04004BF8 RID: 19448
		public int ty;

		// Token: 0x04004BF9 RID: 19449
		private int t;

		// Token: 0x04004BFA RID: 19450
		private int fM;

		// Token: 0x04004BFB RID: 19451
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

		// Token: 0x04004BFC RID: 19452
		private string strMount = "mount_";

		// Token: 0x04004BFD RID: 19453
		public int headICON = -1;

		// Token: 0x04004BFE RID: 19454
		public int head;

		// Token: 0x04004BFF RID: 19455
		public int leg;

		// Token: 0x04004C00 RID: 19456
		public int body;

		// Token: 0x04004C01 RID: 19457
		public int bag;

		// Token: 0x04004C02 RID: 19458
		public int wp;

		// Token: 0x04004C03 RID: 19459
		public int indexEff = -1;

		// Token: 0x04004C04 RID: 19460
		public int indexEffTask = -1;

		// Token: 0x04004C05 RID: 19461
		public EffectCharPaint eff;

		// Token: 0x04004C06 RID: 19462
		public EffectCharPaint effTask;

		// Token: 0x04004C07 RID: 19463
		public int indexSkill;

		// Token: 0x04004C08 RID: 19464
		public int i0;

		// Token: 0x04004C09 RID: 19465
		public int i1;

		// Token: 0x04004C0A RID: 19466
		public int i2;

		// Token: 0x04004C0B RID: 19467
		public int dx0;

		// Token: 0x04004C0C RID: 19468
		public int dx1;

		// Token: 0x04004C0D RID: 19469
		public int dx2;

		// Token: 0x04004C0E RID: 19470
		public int dy0;

		// Token: 0x04004C0F RID: 19471
		public int dy1;

		// Token: 0x04004C10 RID: 19472
		public int dy2;

		// Token: 0x04004C11 RID: 19473
		public EffectCharPaint eff0;

		// Token: 0x04004C12 RID: 19474
		public EffectCharPaint eff1;

		// Token: 0x04004C13 RID: 19475
		public EffectCharPaint eff2;

		// Token: 0x04004C14 RID: 19476
		public Arrow arr;

		// Token: 0x04004C15 RID: 19477
		public PlayerDart dart;

		// Token: 0x04004C16 RID: 19478
		public bool isCreateDark;

		// Token: 0x04004C17 RID: 19479
		public SkillPaint skillPaint;

		// Token: 0x04004C18 RID: 19480
		public SkillPaint skillPaintRandomPaint;

		// Token: 0x04004C19 RID: 19481
		public EffectPaint[] effPaints;

		// Token: 0x04004C1A RID: 19482
		public int sType;

		// Token: 0x04004C1B RID: 19483
		public sbyte isInjure;

		// Token: 0x04004C1C RID: 19484
		public bool isUseSkillAfterCharge;

		// Token: 0x04004C1D RID: 19485
		public bool isFlyAndCharge;

		// Token: 0x04004C1E RID: 19486
		public bool isStandAndCharge;

		// Token: 0x04004C1F RID: 19487
		private bool isFlying;

		// Token: 0x04004C20 RID: 19488
		public int posDisY;

		// Token: 0x04004C21 RID: 19489
		private int chargeCount;

		// Token: 0x04004C22 RID: 19490
		private bool hasSendAttack;

		// Token: 0x04004C23 RID: 19491
		public bool isMabuHold;

		// Token: 0x04004C24 RID: 19492
		private long timeBlue;

		// Token: 0x04004C25 RID: 19493
		private int tBlue;

		// Token: 0x04004C26 RID: 19494
		private bool IsAddDust1;

		// Token: 0x04004C27 RID: 19495
		private bool IsAddDust2;

		// Token: 0x04004C28 RID: 19496
		public int len = 24;

		// Token: 0x04004C29 RID: 19497
		public int w_hp_bar = 24;

		// Token: 0x04004C2A RID: 19498
		private int per = 100;

		// Token: 0x04004C2B RID: 19499
		private int per_tem = 100;

		// Token: 0x04004C2C RID: 19500
		private Image imgHPtem;

		// Token: 0x04004C2D RID: 19501
		public bool isPet;

		// Token: 0x04004C2E RID: 19502
		public bool isMiniPet;

		// Token: 0x04004C2F RID: 19503
		private int iiii;

		// Token: 0x04004C30 RID: 19504
		private int danhHieuFramme;

		// Token: 0x04004C31 RID: 19505
		public int xSd;

		// Token: 0x04004C32 RID: 19506
		public int ySd;

		// Token: 0x04004C33 RID: 19507
		private bool isOutMap;

		// Token: 0x04004C34 RID: 19508
		private int fBag;

		// Token: 0x04004C35 RID: 19509
		private Part ph;

		// Token: 0x04004C36 RID: 19510
		private Part pl;

		// Token: 0x04004C37 RID: 19511
		private Part pb;

		// Token: 0x04004C38 RID: 19512
		public int cH_new = 32;

		// Token: 0x04004C39 RID: 19513
		private int statusBeforeNothing;

		// Token: 0x04004C3A RID: 19514
		private int timeFocusToMob;

		// Token: 0x04004C3B RID: 19515
		public static bool isManualFocus = false;

		// Token: 0x04004C3C RID: 19516
		private Char charHold;

		// Token: 0x04004C3D RID: 19517
		private Mob mobHold;

		// Token: 0x04004C3E RID: 19518
		private int nInjure;

		// Token: 0x04004C3F RID: 19519
		public short wdx;

		// Token: 0x04004C40 RID: 19520
		public short wdy;

		// Token: 0x04004C41 RID: 19521
		public bool isDirtyPostion;

		// Token: 0x04004C42 RID: 19522
		public Skill lastNormalSkill;

		// Token: 0x04004C43 RID: 19523
		public bool currentFireByShortcut;

		// Token: 0x04004C44 RID: 19524
		public long cDamGoc;

		// Token: 0x04004C45 RID: 19525
		public long cHPGoc;

		// Token: 0x04004C46 RID: 19526
		public long cMPGoc;

		// Token: 0x04004C47 RID: 19527
		public int cDefGoc;

		// Token: 0x04004C48 RID: 19528
		public int cCriticalGoc;

		// Token: 0x04004C49 RID: 19529
		public sbyte hpFrom1000TiemNang;

		// Token: 0x04004C4A RID: 19530
		public sbyte mpFrom1000TiemNang;

		// Token: 0x04004C4B RID: 19531
		public sbyte damFrom1000TiemNang;

		// Token: 0x04004C4C RID: 19532
		public sbyte defFrom1000TiemNang = 1;

		// Token: 0x04004C4D RID: 19533
		public sbyte criticalFrom1000Tiemnang = 1;

		// Token: 0x04004C4E RID: 19534
		public short cMaxStamina;

		// Token: 0x04004C4F RID: 19535
		public short expForOneAdd;

		// Token: 0x04004C50 RID: 19536
		public sbyte isMonkey;

		// Token: 0x04004C51 RID: 19537
		public bool isCopy;

		// Token: 0x04004C52 RID: 19538
		public bool isWaitMonkey;

		// Token: 0x04004C53 RID: 19539
		private bool isFeetEff;

		// Token: 0x04004C54 RID: 19540
		public bool meDead;

		// Token: 0x04004C55 RID: 19541
		public int holdEffID;

		// Token: 0x04004C56 RID: 19542
		public bool holder;

		// Token: 0x04004C57 RID: 19543
		public bool protectEff;

		// Token: 0x04004C58 RID: 19544
		public bool danhHieuEff = true;

		// Token: 0x04004C59 RID: 19545
		private bool isSetPos;

		// Token: 0x04004C5A RID: 19546
		private int tpos;

		// Token: 0x04004C5B RID: 19547
		private short xPos;

		// Token: 0x04004C5C RID: 19548
		private short yPos;

		// Token: 0x04004C5D RID: 19549
		private sbyte typePos;

		// Token: 0x04004C5E RID: 19550
		private bool isMyFusion;

		// Token: 0x04004C5F RID: 19551
		public bool isFusion;

		// Token: 0x04004C60 RID: 19552
		public int tFusion;

		// Token: 0x04004C61 RID: 19553
		public bool huytSao;

		// Token: 0x04004C62 RID: 19554
		public bool blindEff;

		// Token: 0x04004C63 RID: 19555
		public bool telePortSkill;

		// Token: 0x04004C64 RID: 19556
		public bool sleepEff;

		// Token: 0x04004C65 RID: 19557
		public bool stone;

		// Token: 0x04004C66 RID: 19558
		public int perCentMp = 100;

		// Token: 0x04004C67 RID: 19559
		public long dHP;

		// Token: 0x04004C68 RID: 19560
		public int headTemp = -1;

		// Token: 0x04004C69 RID: 19561
		public int bodyTemp = -1;

		// Token: 0x04004C6A RID: 19562
		public int legTemp = -1;

		// Token: 0x04004C6B RID: 19563
		public int bagTemp = -1;

		// Token: 0x04004C6C RID: 19564
		public int wpTemp = -1;

		// Token: 0x04004C6D RID: 19565
		public MyVector vEffChar = new MyVector("vEff");

		// Token: 0x04004C6E RID: 19566
		public static FrameImage fraRedEye;

		// Token: 0x04004C6F RID: 19567
		private int fChopmat;

		// Token: 0x04004C70 RID: 19568
		private bool isAddChopMat;

		// Token: 0x04004C71 RID: 19569
		private long timeAddChopmat;

		// Token: 0x04004C72 RID: 19570
		private int[] frChopNhanh = new int[]
		{
			-1,
			-1,
			-1,
			-1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			-1,
			-1,
			-1,
			-1
		};

		// Token: 0x04004C73 RID: 19571
		private int[] frChopCham = new int[]
		{
			-1,
			-1,
			-1,
			-1,
			0,
			0,
			1,
			1,
			1,
			0,
			0,
			1,
			1,
			1,
			0,
			0,
			1,
			1,
			1,
			-1,
			-1,
			-1,
			-1
		};

		// Token: 0x04004C74 RID: 19572
		private int[] frEye = new int[]
		{
			-1,
			-1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			1,
			1,
			0,
			0,
			-1,
			-1
		};

		// Token: 0x04004C75 RID: 19573
		public static int[][] Arr_Head_2Fr = new int[][]
		{
			new int[]
			{
				542,
				543
			}
		};

		// Token: 0x04004C76 RID: 19574
		private int fHead;

		// Token: 0x04004C77 RID: 19575
		private string strEffAura = "aura_";

		// Token: 0x04004C78 RID: 19576
		public short idAuraEff = -1;

		// Token: 0x04004C79 RID: 19577
		public static bool isPaintAura = true;

		// Token: 0x04004C7A RID: 19578
		public static bool isPaintAura2 = true;

		// Token: 0x04004C7B RID: 19579
		private FrameImage fraEff;

		// Token: 0x04004C7C RID: 19580
		private FrameImage fraEffSub;

		// Token: 0x04004C7D RID: 19581
		private string strEff_Set_Item = "set_eff_";

		// Token: 0x04004C7E RID: 19582
		public short idEff_Set_Item = -1;

		// Token: 0x04004C7F RID: 19583
		private FrameImage fraHat_behind;

		// Token: 0x04004C80 RID: 19584
		private FrameImage fraHat_font;

		// Token: 0x04004C81 RID: 19585
		private FrameImage fraHat_behind_2;

		// Token: 0x04004C82 RID: 19586
		private FrameImage fraHat_font_2;

		// Token: 0x04004C83 RID: 19587
		private string strHat_behind = "hat_sau_";

		// Token: 0x04004C84 RID: 19588
		private string strHat_font = "hat_truoc_";

		// Token: 0x04004C85 RID: 19589
		private string strNgang = "ngang_";

		// Token: 0x04004C86 RID: 19590
		public short idHat = -1;

		// Token: 0x04004C87 RID: 19591
		public static int[][] hatInfo;

		// Token: 0x04004C88 RID: 19592
		public const byte TYPE_SKILL_KAMEX10 = 1;

		// Token: 0x04004C89 RID: 19593
		public const byte TYPE_SKILL_FINAL = 2;

		// Token: 0x04004C8A RID: 19594
		public const byte TYPE_SKILL_MAFUBA = 3;

		// Token: 0x04004C8B RID: 19595
		public const byte TYPE_SKILL_GENKI = 4;

		// Token: 0x04004C8C RID: 19596
		public bool isPaintNewSkill;

		// Token: 0x04004C8D RID: 19597
		private bool isFly;

		// Token: 0x04004C8E RID: 19598
		private long timeReset_newSkill;

		// Token: 0x04004C8F RID: 19599
		private sbyte typeFrame;

		// Token: 0x04004C90 RID: 19600
		private short idskillPaint;

		// Token: 0x04004C91 RID: 19601
		private byte[] fr_start;

		// Token: 0x04004C92 RID: 19602
		private byte[] fr_atk;

		// Token: 0x04004C93 RID: 19603
		private byte[] fr_end;

		// Token: 0x04004C94 RID: 19604
		private int count_NEW;

		// Token: 0x04004C95 RID: 19605
		private int stt;

		// Token: 0x04004C96 RID: 19606
		private short rangeDame;

		// Token: 0x04004C97 RID: 19607
		private sbyte typePaint;

		// Token: 0x04004C98 RID: 19608
		private sbyte typeItem;

		// Token: 0x04004C99 RID: 19609
		private Point targetDame;

		// Token: 0x04004C9A RID: 19610
		private long timeDame;

		// Token: 0x04004C9B RID: 19611
		public bool isMafuba;

		// Token: 0x04004C9C RID: 19612
		private short countMafuba;

		// Token: 0x04004C9D RID: 19613
		public int xMFB;

		// Token: 0x04004C9E RID: 19614
		public int yMFB;

		// Token: 0x04004C9F RID: 19615
		public int timeGongSkill;

		// Token: 0x04004CA0 RID: 19616
		private FrameImage fraDanhHieu;

		// Token: 0x04004CA1 RID: 19617
		private MainImage mainImg;

		// Token: 0x04004CA2 RID: 19618
		public bool isTichXanh;

		// Token: 0x04004CA3 RID: 19619
		public int tlDef;

		// Token: 0x04004CA4 RID: 19620
		public int tlPst;

		// Token: 0x04004CA5 RID: 19621
		public int tlNeDon;

		// Token: 0x04004CA6 RID: 19622
		public int tlHutHp;

		// Token: 0x04004CA7 RID: 19623
		public int tlHutMp;

		// Token: 0x04004CA8 RID: 19624
		public int tileGiamTDHS;

		// Token: 0x04004CA9 RID: 19625
		public int timeGiamTDHS;

		// Token: 0x04004CAA RID: 19626
		public bool khangTDHS;

		// Token: 0x04004CAB RID: 19627
		public bool isKhongLanh;

		// Token: 0x04004CAC RID: 19628
		public bool wearingVoHinh;

		// Token: 0x04004CAD RID: 19629
		public bool teleport;
	}
}
