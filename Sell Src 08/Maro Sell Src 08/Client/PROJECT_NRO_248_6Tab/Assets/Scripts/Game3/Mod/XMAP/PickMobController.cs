using System;
using System.Collections.Generic;

namespace Game3.Mod.XMAP
{
	// Token: 0x02000355 RID: 853
	public class PickMobController
	{
		// Token: 0x060025F6 RID: 9718 RVA: 0x0024EF34 File Offset: 0x0024D134
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

		// Token: 0x060025F7 RID: 9719 RVA: 0x0024F550 File Offset: 0x0024D750
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

		// Token: 0x060025F8 RID: 9720 RVA: 0x0024F5C4 File Offset: 0x0024D7C4
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

		// Token: 0x060025F9 RID: 9721 RVA: 0x0024F664 File Offset: 0x0024D864
		private static bool FilterItemPick(ItemMap itemMap)
		{
			return (PickMob.IdItemPicks.Count == 0 || PickMob.IdItemPicks.Contains(itemMap.template.id)) && (PickMob.IdItemBlocks.Count == 0 || !PickMob.IdItemBlocks.Contains(itemMap.template.id)) && (PickMob.TypeItemPicks.Count == 0 || PickMob.TypeItemPicks.Contains(itemMap.template.type)) && (PickMob.TypeItemBlock.Count == 0 || !PickMob.TypeItemBlock.Contains(itemMap.template.type));
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x0024F704 File Offset: 0x0024D904
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

		// Token: 0x060025FB RID: 9723 RVA: 0x0024F798 File Offset: 0x0024D998
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

		// Token: 0x060025FC RID: 9724 RVA: 0x0024F7D8 File Offset: 0x0024D9D8
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

		// Token: 0x060025FD RID: 9725 RVA: 0x0024F830 File Offset: 0x0024DA30
		private static bool IsMobTanSat(Mob mob)
		{
			if (mob.status == 0 || mob.status == 1 || mob.hp <= 0L || mob.isMobMe)
			{
				return false;
			}
			bool flag = PickMob.neSieuQuai && !ItemTime.isExistItem(4387);
			return (mob.levelBoss == 0 || !flag) && PickMobController.FilterMobTanSat(mob);
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x0024F88D File Offset: 0x0024DA8D
		private static bool IsCharTanSat(Char c)
		{
			return Char.myCharz().isMeCanAttackOtherPlayer(c);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0024F89C File Offset: 0x0024DA9C
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

		// Token: 0x06002600 RID: 9728 RVA: 0x0024F970 File Offset: 0x0024DB70
		private static bool FilterMobTanSat(Mob mob)
		{
			return (PickMob.IdMobsTanSat.Count == 0 || PickMob.IdMobsTanSat.Contains(mob.mobId)) && (PickMob.TypeMobsTanSat.Count == 0 || PickMob.TypeMobsTanSat.Contains(mob.templateId)) && !mob.isMobMe;
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x0024F9C4 File Offset: 0x0024DBC4
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

		// Token: 0x06002602 RID: 9730 RVA: 0x0024FA60 File Offset: 0x0024DC60
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

		// Token: 0x06002603 RID: 9731 RVA: 0x0024FADC File Offset: 0x0024DCDC
		private static bool CanUseSkill(Skill skill)
		{
			if (mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > (long)skill.coolDown + 25L)
			{
				skill.paintCanNotUseSkill = false;
			}
			return !skill.paintCanNotUseSkill && (double)Char.myCharz().cMP >= PickMobController.GetManaUseSkill(skill);
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x0024FB2C File Offset: 0x0024DD2C
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

		// Token: 0x06002605 RID: 9733 RVA: 0x0024FB80 File Offset: 0x0024DD80
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

		// Token: 0x06002606 RID: 9734 RVA: 0x0024FBD4 File Offset: 0x0024DDD4
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

		// Token: 0x06002607 RID: 9735 RVA: 0x0024FC3E File Offset: 0x0024DE3E
		public static void Wait(int time)
		{
			PickMobController.IsWait = true;
			PickMobController.TimeStartWait = mSystem.currentTimeMillis();
			PickMobController.TimeWait = (long)time;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0024FC57 File Offset: 0x0024DE57
		public static bool IsWaiting()
		{
			if (PickMobController.IsWait && mSystem.currentTimeMillis() - PickMobController.TimeStartWait >= PickMobController.TimeWait)
			{
				PickMobController.IsWait = false;
			}
			return PickMobController.IsWait;
		}

		// Token: 0x04004968 RID: 18792
		private static readonly sbyte[] IdSkillsMelee = new sbyte[]
		{
			0,
			9,
			2,
			17,
			4
		};

		// Token: 0x04004969 RID: 18793
		private static readonly sbyte[] IdSkillsCanNotAttack = new sbyte[]
		{
			10,
			11,
			14,
			23,
			7
		};

		// Token: 0x0400496A RID: 18794
		private static readonly PickMobController _Instance = new PickMobController();

		// Token: 0x0400496B RID: 18795
		public static bool IsPickingItems;

		// Token: 0x0400496C RID: 18796
		private static bool IsWait;

		// Token: 0x0400496D RID: 18797
		private static long TimeStartWait;

		// Token: 0x0400496E RID: 18798
		private static long TimeWait;

		// Token: 0x0400496F RID: 18799
		public static List<ItemMap> ItemPicks = new List<ItemMap>();

		// Token: 0x04004970 RID: 18800
		private static int IndexItemPick = 0;

		// Token: 0x02000356 RID: 854
		private enum TpyePickItem
		{
			// Token: 0x04004972 RID: 18802
			CanNotPickItem,
			// Token: 0x04004973 RID: 18803
			PickItemNormal,
			// Token: 0x04004974 RID: 18804
			PickItemTDLT,
			// Token: 0x04004975 RID: 18805
			PickItemTanSat
		}
	}
}
