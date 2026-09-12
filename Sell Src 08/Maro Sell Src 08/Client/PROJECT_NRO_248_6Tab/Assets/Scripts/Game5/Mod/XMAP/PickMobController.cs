using System;
using System.Collections.Generic;

namespace Game5.Mod.XMAP
{
	// Token: 0x020001A5 RID: 421
	public class PickMobController
	{
		// Token: 0x060012AE RID: 4782 RVA: 0x00124DEC File Offset: 0x00122FEC
		public static void Update()
		{
			if (PickMobController.IsWaiting())
			{
				return;
			}
			Char @char = Char.myCharz();
			if (@char.statusMe == 14 || @char.cHP <= 0L)
			{
				return;
			}
			if (GameScr.hpPotion >= 1 && (@char.cHP <= @char.cHPFull * (long)PickMob.HpBuff / 100L || @char.cMP <= @char.cMPFull * (long)PickMob.MpBuff / 100L))
			{
				GameScr.gI().doUseHP();
			}
			if (PickMob.IsAutoPickItems)
			{
				if (PickMobController.IsPickingItems)
				{
					if (PickMobController.IndexItemPick >= PickMobController.ItemPicks.Count)
					{
						PickMobController.IsPickingItems = false;
						PickMobController.Wait(100);
						return;
					}
					ItemMap itemMap = PickMobController.ItemPicks[PickMobController.IndexItemPick];
					if (GameScr.vItemMap.contains(itemMap))
					{
						Service.gI().pickItem(itemMap.itemMapID);
						itemMap.countAutoPick++;
					}
					PickMobController.Wait(500);
					PickMobController.IndexItemPick++;
				}
				PickMobController.ItemPicks.Clear();
				PickMobController.IndexItemPick = 0;
				for (int i = 0; i < GameScr.vItemMap.size(); i++)
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(i);
					if (PickMobController.GetTypePickItem(itemMap2) != PickMobController.TpyePickItem.CanNotPickItem)
					{
						PickMobController.ItemPicks.Add(itemMap2);
					}
				}
				if (PickMobController.ItemPicks.Count > 0)
				{
					PickMobController.IsPickingItems = true;
					return;
				}
			}
			bool isTDLT = ItemTime.isExistItem(4387);
			if (PickMob.tanSat)
			{
				if (@char.isCharge)
				{
					PickMobController.Wait(100);
					return;
				}
				if (@char.mobFocus != null && !PickMobController.IsMobTanSat(@char.mobFocus))
				{
					@char.mobFocus = null;
				}
				if (@char.mobFocus == null)
				{
					@char.mobFocus = PickMobController.GetMobTanSat();
					if (isTDLT && @char.mobFocus != null)
					{
						if (PickMob.telePem)
						{
							if (Math.abs(@char.mobFocus.xFirst - @char.cx) >= 20 || Math.abs(@char.mobFocus.yFirst - @char.cy) >= 20)
							{
								ModFunc.GI().MoveTo(@char.mobFocus.xFirst, @char.mobFocus.yFirst);
							}
							return;
						}
						@char.cx = @char.mobFocus.xFirst;
						@char.cy = @char.mobFocus.yFirst;
						Service.gI().charMove();
					}
				}
				if (@char.mobFocus != null)
				{
					if (@char.skillInfoPaint() == null)
					{
						Skill skillAttack = PickMobController.GetSkillAttack2();
						if (skillAttack != null && PickMobController.CanUseSkill(skillAttack))
						{
							Mob mobFocus = @char.mobFocus;
							mobFocus.x = mobFocus.xFirst;
							mobFocus.y = mobFocus.yFirst;
							if (Char.myCharz().myskill != skillAttack)
							{
								GameScr.gI().doSelectSkill(skillAttack, true);
							}
							if (Res.distance(mobFocus.xFirst, mobFocus.yFirst, @char.cx, @char.cy) <= 48)
							{
								if (GameCanvas.gameTick % 50 == 0 && Mob.arrMobTemplate[Char.myCharz().mobFocus.templateId].type == 4)
								{
									ModFunc.GI().MoveTo(mobFocus.xFirst, mobFocus.yFirst + 1);
								}
								if (skillAttack.template.isAttackSkill())
								{
									ModFunc.GI().AttackMob(mobFocus);
									ModFunc.GI().SetUsedSkill(@char.myskill);
								}
								else if (skillAttack.template.isUseAlone())
								{
									ModFunc.GI().DoDoubleClickToObj(mobFocus);
								}
							}
							else
							{
								if (PickMob.telePem)
								{
									if (Math.abs(mobFocus.xFirst - @char.cx) >= 20 || Math.abs(mobFocus.yFirst - @char.cy) >= 20)
									{
										ModFunc.GI().MoveTo(mobFocus.xFirst, mobFocus.yFirst);
									}
									return;
								}
								PickMobController.Move(mobFocus.xFirst, mobFocus.yFirst);
							}
						}
					}
				}
				else if (!isTDLT)
				{
					Mob mobNext = PickMobController.GetMobNext();
					if (mobNext != null)
					{
						Char.myCharz().FocusManualTo(mobNext);
						if (PickMob.telePem)
						{
							if (Math.abs(mobNext.xFirst - @char.cx) >= 20 || Math.abs(mobNext.yFirst - @char.cy) >= 20)
							{
								ModFunc.GI().MoveTo(mobNext.xFirst, mobNext.yFirst);
							}
							return;
						}
						PickMobController.Move(mobNext.xFirst, mobNext.yFirst);
					}
				}
				PickMobController.Wait(100);
				return;
			}
			else
			{
				if (!PickMob.tsPlayer)
				{
					return;
				}
				if (@char.isCharge)
				{
					PickMobController.Wait(100);
					return;
				}
				if (@char.charFocus != null && !PickMobController.IsCharTanSat(@char.charFocus))
				{
					@char.charFocus = null;
				}
				if (@char.charFocus == null)
				{
					@char.charFocus = PickMobController.GetCharTanSat();
					if (@char.charFocus != null)
					{
						if (PickMob.telePem)
						{
							if (Math.abs(@char.charFocus.cx - @char.cx) >= 20 || Math.abs(@char.charFocus.cy - @char.cy) >= 20)
							{
								ModFunc.GI().MoveTo(@char.charFocus.cx, @char.charFocus.cy);
							}
							return;
						}
						@char.cx = @char.charFocus.cx;
						@char.cy = @char.charFocus.cy;
						Service.gI().charMove();
					}
				}
				if (@char.charFocus != null && @char.skillInfoPaint() == null)
				{
					Skill skillAttack2 = PickMobController.GetSkillAttack2();
					if (skillAttack2 != null && !skillAttack2.paintCanNotUseSkill)
					{
						Char charFocus = @char.charFocus;
						if (@char.myskill != skillAttack2)
						{
							GameScr.gI().doSelectSkill(skillAttack2, true);
						}
						if (Res.distance(charFocus.cx, charFocus.cy, @char.cx, @char.cy) <= 48)
						{
							ModFunc.GI().DoDoubleClickToObj(charFocus);
						}
						else
						{
							if (PickMob.telePem)
							{
								if (Math.abs(charFocus.cx - @char.cx) >= 20 || Math.abs(charFocus.cy - @char.cy) >= 20)
								{
									ModFunc.GI().MoveTo(charFocus.cx, charFocus.cy);
								}
								return;
							}
							PickMobController.Move(charFocus.cx, charFocus.cy);
						}
					}
				}
				PickMobController.Wait(100);
				return;
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00125408 File Offset: 0x00123608
		public static void Move(int x, int y)
		{
			Char @char = Char.myCharz();
			if (!PickMob.vuotDiaHinh)
			{
				@char.currentMovePoint = new MovePoint(x, y);
				return;
			}
			int[] pointYsdMax = PickMobController.GetPointYsdMax(@char.cx, x);
			if (pointYsdMax[1] >= y || (pointYsdMax[1] >= @char.cy && (@char.statusMe == 2 || @char.statusMe == 1)))
			{
				pointYsdMax[0] = x;
				pointYsdMax[1] = y;
			}
			@char.currentMovePoint = new MovePoint(pointYsdMax[0], pointYsdMax[1]);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0012547C File Offset: 0x0012367C
		private static PickMobController.TpyePickItem GetTypePickItem(ItemMap itemMap)
		{
			Char @char = Char.myCharz();
			if (itemMap.playerId != @char.charID && itemMap.playerId != -1 && !PickMob.IsPickItemsAll)
			{
				return PickMobController.TpyePickItem.CanNotPickItem;
			}
			if (PickMob.IsLimitTimesPickItem && itemMap.countAutoPick > PickMob.TimesAutoPickItemMax)
			{
				return PickMobController.TpyePickItem.CanNotPickItem;
			}
			if (!PickMobController.FilterItemPick(itemMap))
			{
				return PickMobController.TpyePickItem.CanNotPickItem;
			}
			if (PickMob.IsPickItemsDis || (Res.abs(@char.cx - itemMap.xEnd) < 100 && Res.abs(@char.cy - itemMap.yEnd) < 100))
			{
				return PickMobController.TpyePickItem.PickItemNormal;
			}
			if (ItemTime.isExistItem(4387))
			{
				return PickMobController.TpyePickItem.PickItemTDLT;
			}
			if (PickMob.tanSat)
			{
				return PickMobController.TpyePickItem.PickItemTanSat;
			}
			return PickMobController.TpyePickItem.CanNotPickItem;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0012551C File Offset: 0x0012371C
		private static bool FilterItemPick(ItemMap itemMap)
		{
			return (PickMob.IdItemPicks.Count == 0 || PickMob.IdItemPicks.Contains(itemMap.template.id)) && (PickMob.IdItemBlocks.Count == 0 || !PickMob.IdItemBlocks.Contains(itemMap.template.id)) && (PickMob.TypeItemPicks.Count == 0 || PickMob.TypeItemPicks.Contains(itemMap.template.type)) && (PickMob.TypeItemBlock.Count == 0 || !PickMob.TypeItemBlock.Contains(itemMap.template.type));
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x001255BC File Offset: 0x001237BC
		private static Mob GetMobTanSat()
		{
			Mob result = null;
			int num = int.MaxValue;
			Char @char = Char.myCharz();
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				int num2 = (mob.xFirst - @char.cx) * (mob.xFirst - @char.cx) + (mob.yFirst - @char.cy) * (mob.yFirst - @char.cy);
				if (PickMobController.IsMobTanSat(mob) && num2 < num)
				{
					result = mob;
					num = num2;
				}
			}
			return result;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00125650 File Offset: 0x00123850
		private static Char GetCharTanSat()
		{
			Char result = null;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char c = (Char)GameScr.vCharInMap.elementAt(i);
				if (PickMobController.IsCharTanSat(c))
				{
					result = c;
				}
			}
			return result;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00125690 File Offset: 0x00123890
		private static Mob GetMobNext()
		{
			Mob result = null;
			long num = mSystem.currentTimeMillis();
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (PickMobController.IsMobNext(mob) && mob.lastDie < num)
				{
					result = mob;
					num = mob.lastDie;
				}
			}
			return result;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x001256E8 File Offset: 0x001238E8
		private static bool IsMobTanSat(Mob mob)
		{
			if (mob.status == 0 || mob.status == 1 || mob.hp <= 0L || mob.isMobMe)
			{
				return false;
			}
			bool flag = PickMob.neSieuQuai && !ItemTime.isExistItem(4387);
			return (mob.levelBoss == 0 || !flag) && PickMobController.FilterMobTanSat(mob);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00125745 File Offset: 0x00123945
		private static bool IsCharTanSat(Char c)
		{
			return Char.myCharz().isMeCanAttackOtherPlayer(c);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00125754 File Offset: 0x00123954
		private static bool IsMobNext(Mob mob)
		{
			if (mob.isMobMe)
			{
				return false;
			}
			if (!PickMobController.FilterMobTanSat(mob))
			{
				return false;
			}
			if (PickMob.neSieuQuai && !ItemTime.isExistItem(4387) && mob.getTemplate().hp >= 3000L)
			{
				if (mob.levelBoss != 0)
				{
					Mob mob2 = null;
					bool flag4 = false;
					for (int i = 0; i < GameScr.vMob.size(); i++)
					{
						mob2 = (Mob)GameScr.vMob.elementAt(i);
						if (mob2.countDie == 10 && (mob2.status == 0 || mob2.status == 1))
						{
							flag4 = true;
							break;
						}
					}
					if (!flag4)
					{
						return false;
					}
					mob.lastDie = mob2.lastDie;
				}
				else if (mob.countDie == 10 && (mob.status == 0 || mob.status == 1))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00125828 File Offset: 0x00123A28
		private static bool FilterMobTanSat(Mob mob)
		{
			return (PickMob.IdMobsTanSat.Count == 0 || PickMob.IdMobsTanSat.Contains(mob.mobId)) && (PickMob.TypeMobsTanSat.Count == 0 || PickMob.TypeMobsTanSat.Contains(mob.templateId)) && !mob.isMobMe;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0012587C File Offset: 0x00123A7C
		private static Skill GetSkillAttack2()
		{
			Skill skill = null;
			foreach (Skill s in GameCanvas.isTouch ? GameScr.onScreenSkill : GameScr.keySkill)
			{
				if (s != null && s.template.id != 10 && s.template.id != 11 && s.template.id != 14 && s.template.id != 23 && !s.template.isBuffToPlayer() && !s.template.isSkillSpec() && PickMobController.IsSkillBetter(s, skill))
				{
					skill = s;
				}
			}
			return skill;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00125918 File Offset: 0x00123B18
		private static bool IsSkillBetter(Skill SkillBetter, Skill skill)
		{
			if (SkillBetter == null)
			{
				return false;
			}
			if (skill == null)
			{
				return true;
			}
			if (!PickMobController.CanUseSkill(SkillBetter))
			{
				return false;
			}
			if (!PickMobController.CanUseSkill(skill))
			{
				return true;
			}
			bool flag3 = (SkillBetter.template.id == 17 && skill.template.id == 2) || (SkillBetter.template.id == 9 && skill.template.id == 0);
			return skill.coolDown < SkillBetter.coolDown || flag3;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00125994 File Offset: 0x00123B94
		private static bool CanUseSkill(Skill skill)
		{
			if (mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > (long)skill.coolDown + 25L)
			{
				skill.paintCanNotUseSkill = false;
			}
			return !skill.paintCanNotUseSkill && (double)Char.myCharz().cMP >= PickMobController.GetManaUseSkill(skill);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x001259E4 File Offset: 0x00123BE4
		private static double GetManaUseSkill(Skill skill)
		{
			if (skill.template.manaUseType == 2)
			{
				return 1.0;
			}
			if (skill.template.manaUseType == 1)
			{
				return (double)((long)skill.manaUse * Char.myCharz().cMPFull / 100L);
			}
			return (double)skill.manaUse;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00125A38 File Offset: 0x00123C38
		public static int GetYsd(int xsd)
		{
			Char @char = Char.myCharz();
			int num = TileMap.pxh;
			int result = -1;
			for (int i = 24; i < TileMap.pxh; i += 24)
			{
				if (TileMap.tileTypeAt(xsd, i, 2))
				{
					int num2 = Res.abs(i - @char.cy);
					if (num2 < num)
					{
						num = num2;
						result = i;
					}
				}
			}
			return result;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00125A8C File Offset: 0x00123C8C
		private static int[] GetPointYsdMax(int xStart, int xEnd)
		{
			int num = TileMap.pxh;
			int num2 = -1;
			if (xStart > xEnd)
			{
				for (int i = xEnd; i < xStart; i += 24)
				{
					int ysd = PickMobController.GetYsd(i);
					if (ysd < num)
					{
						num = ysd;
						num2 = i;
					}
				}
			}
			else
			{
				for (int j = xEnd; j > xStart; j -= 24)
				{
					int ysd2 = PickMobController.GetYsd(j);
					if (ysd2 < num)
					{
						num = ysd2;
						num2 = j;
					}
				}
			}
			return new int[]
			{
				num2,
				num
			};
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00125AF6 File Offset: 0x00123CF6
		public static void Wait(int time)
		{
			PickMobController.IsWait = true;
			PickMobController.TimeStartWait = mSystem.currentTimeMillis();
			PickMobController.TimeWait = (long)time;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00125B0F File Offset: 0x00123D0F
		public static bool IsWaiting()
		{
			if (PickMobController.IsWait && mSystem.currentTimeMillis() - PickMobController.TimeStartWait >= PickMobController.TimeWait)
			{
				PickMobController.IsWait = false;
			}
			return PickMobController.IsWait;
		}

		// Token: 0x0400246A RID: 9322
		private static readonly sbyte[] IdSkillsMelee = new sbyte[]
		{
			0,
			9,
			2,
			17,
			4
		};

		// Token: 0x0400246B RID: 9323
		private static readonly sbyte[] IdSkillsCanNotAttack = new sbyte[]
		{
			10,
			11,
			14,
			23,
			7
		};

		// Token: 0x0400246C RID: 9324
		private static readonly PickMobController _Instance = new PickMobController();

		// Token: 0x0400246D RID: 9325
		public static bool IsPickingItems;

		// Token: 0x0400246E RID: 9326
		private static bool IsWait;

		// Token: 0x0400246F RID: 9327
		private static long TimeStartWait;

		// Token: 0x04002470 RID: 9328
		private static long TimeWait;

		// Token: 0x04002471 RID: 9329
		public static List<ItemMap> ItemPicks = new List<ItemMap>();

		// Token: 0x04002472 RID: 9330
		private static int IndexItemPick = 0;

		// Token: 0x020001A6 RID: 422
		private enum TpyePickItem
		{
			// Token: 0x04002474 RID: 9332
			CanNotPickItem,
			// Token: 0x04002475 RID: 9333
			PickItemNormal,
			// Token: 0x04002476 RID: 9334
			PickItemTDLT,
			// Token: 0x04002477 RID: 9335
			PickItemTanSat
		}
	}
}
