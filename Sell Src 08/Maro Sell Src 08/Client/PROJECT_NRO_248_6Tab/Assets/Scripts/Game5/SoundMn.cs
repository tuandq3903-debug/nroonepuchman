using System;
using System.Collections.Generic;

namespace Game5
{
	// Token: 0x0200018A RID: 394
	public class SoundMn
	{
		// Token: 0x060011CC RID: 4556 RVA: 0x0011BE56 File Offset: 0x0011A056
		public static SoundMn gI()
		{
			if (SoundMn.gIz == null)
			{
				SoundMn.gIz = new SoundMn();
			}
			return SoundMn.gIz;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0011BE70 File Offset: 0x0011A070
		public void loadSound(int mapID)
		{
			Sound.init(new int[]
			{
				SoundMn.AIR_SHIP,
				SoundMn.RAIN,
				SoundMn.TAITAONANGLUONG
			}, new int[]
			{
				SoundMn.GET_ITEM,
				SoundMn.MOVE,
				SoundMn.LOW_PUNCH,
				SoundMn.LOW_KICK,
				SoundMn.FLY,
				SoundMn.JUMP,
				SoundMn.PANEL_OPEN,
				SoundMn.BUTTON_CLOSE,
				SoundMn.BUTTON_CLICK,
				SoundMn.MEDIUM_PUNCH,
				SoundMn.MEDIUM_KICK,
				SoundMn.PANEL_OPEN,
				SoundMn.EAT_PEAN,
				SoundMn.OPEN_DIALOG,
				SoundMn.NORMAL_KAME,
				SoundMn.NAMEK_KAME,
				SoundMn.XAYDA_KAME,
				SoundMn.EXPLODE_1,
				SoundMn.EXPLODE_2,
				SoundMn.TRAIDAT_KAME,
				SoundMn.HP_UP,
				SoundMn.THAIDUONGHASAN,
				SoundMn.HOISINH,
				SoundMn.GONG,
				SoundMn.KHICHAY,
				SoundMn.BIG_EXPLODE,
				SoundMn.NAMEK_LAZER,
				SoundMn.NAMEK_CHARGE,
				SoundMn.RADAR_CLICK,
				SoundMn.RADAR_ITEM,
				SoundMn.FIREWORK,
				SoundMn.KAMEX10_0,
				SoundMn.KAMEX10_1,
				SoundMn.DESTROY_0,
				SoundMn.DESTROY_1,
				SoundMn.MAFUBA_0,
				SoundMn.MAFUBA_1,
				SoundMn.MAFUBA_2,
				SoundMn.DESTROY_2
			});
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0011C000 File Offset: 0x0011A200
		public void getSoundOption()
		{
			if (GameCanvas.loginScr.isLogin2 && Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId >= 2)
			{
				if (Char.myCharz().havePet && Char.myCharz().havePet2)
				{
					Panel.strTool = new string[]
					{
						mResources.gameInfo,
						mResources.quayso,
						ModFunc.strPlayerInfo,
						mResources.radaCard,
						mResources.pet,
						ModFunc.strPet2,
						mResources.change_flag,
						mResources.change_zone,
						mResources.chat_world,
						mResources.account,
						mResources.option,
						mResources.change_account,
						mResources.REGISTOPROTECT
					};
				}
				else if (Char.myCharz().havePet || Char.myCharz().havePet2)
				{
					Panel.strTool = new string[]
					{
						mResources.gameInfo,
						mResources.quayso,
						ModFunc.strPlayerInfo,
						mResources.radaCard,
						Char.myCharz().havePet ? mResources.pet : ModFunc.strPet2,
						mResources.change_flag,
						mResources.change_zone,
						mResources.chat_world,
						mResources.account,
						mResources.option,
						mResources.change_account,
						mResources.REGISTOPROTECT
					};
				}
				else
				{
					Panel.strTool = new string[]
					{
						mResources.gameInfo,
						mResources.quayso,
						ModFunc.strPlayerInfo,
						mResources.radaCard,
						mResources.change_flag,
						mResources.change_zone,
						mResources.chat_world,
						mResources.account,
						mResources.option,
						mResources.change_account,
						mResources.REGISTOPROTECT
					};
				}
			}
			else if (Char.myCharz().havePet && Char.myCharz().havePet2)
			{
				Panel.strTool = new string[]
				{
					mResources.gameInfo,
					mResources.quayso,
					ModFunc.strPlayerInfo,
					mResources.radaCard,
					mResources.pet,
					ModFunc.strPet2,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account
				};
			}
			else if (Char.myCharz().havePet || Char.myCharz().havePet2)
			{
				Panel.strTool = new string[]
				{
					mResources.gameInfo,
					mResources.quayso,
					ModFunc.strPlayerInfo,
					mResources.radaCard,
					Char.myCharz().havePet ? mResources.pet : ModFunc.strPet2,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account
				};
			}
			else
			{
				Panel.strTool = new string[]
				{
					mResources.gameInfo,
					mResources.quayso,
					ModFunc.strPlayerInfo,
					mResources.radaCard,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account
				};
			}
			if (SoundMn.IsDelAcc)
			{
				string[] array = new string[Panel.strTool.Length + 1];
				for (int i = 0; i < Panel.strTool.Length; i++)
				{
					array[i] = Panel.strTool[i];
				}
				array[Panel.strTool.Length] = mResources.delacc;
				Panel.strTool = array;
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0011C3AC File Offset: 0x0011A5AC
		public void getStrOption()
		{
			if (GameCanvas.isLoadRes)
			{
				string text = "[x]   ";
				string text2 = "[  ]   ";
				string text3 = (GameScr.isAnalog != 0) ? (text + mResources.turnOffAnalog) : (text2 + mResources.turnOnAnalog);
				if (!GameCanvas.isTouch)
				{
					text3 = (GameScr.isPaintChatVip ? (text + mResources.serverchat_off) : (text2 + mResources.serverchat_off));
				}
				Panel.strCauhinh = new string[]
				{
					(!Char.isPaintAura) ? (text + mResources.aura_off.Trim()) : (text2 + mResources.aura_off.Trim()),
					(!Char.isPaintAura2) ? (text + mResources.aura_off_2.Trim()) : (text2 + mResources.aura_off_2.Trim()),
					(!GameCanvas.isPlaySound) ? (text2 + mResources.turnOffSound.Trim()) : (text + mResources.turnOffSound.Trim()),
					(!GameCanvas.lowGraphic) ? (text2 + mResources.cauhinhthap.Trim()) : (text + mResources.cauhinhthap.Trim()),
					text3
				};
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0011C4D8 File Offset: 0x0011A6D8
		public void GetStrModFunc()
		{
			if (!GameCanvas.isLoadRes)
			{
				return;
			}
			ValueTuple<bool, string>[][] modFuncByTab = new ValueTuple<bool, string>[][]
			{
				new ValueTuple<bool, string>[]
				{
					new ValueTuple<bool, string>(ModFunc.GI().isHighFps, ModFunc.strHighFps),
					new ValueTuple<bool, string>(ModFunc.GI().isUpdateZones, ModFunc.strUpdateZones),
					new ValueTuple<bool, string>(ModFunc.GI().showCharsInMap, ModFunc.strCharsInMap),
					new ValueTuple<bool, string>(ModFunc.GI().showInfoMe, ModFunc.strInfoMe),
					new ValueTuple<bool, string>(ModFunc.GI().isShowButton, ModFunc.strShowButton)
				},
				new ValueTuple<bool, string>[]
				{
					new ValueTuple<bool, string>(ModFunc.GI().isAutoPhaLe, ModFunc.strAutoPhaLe),
					new ValueTuple<bool, string>(ModFunc.GI().isAutoVQMM, ModFunc.strAutoVQMM),
					new ValueTuple<bool, string>(ModFunc.GI().autoWakeUp, ModFunc.strAutoWakeUp),
					new ValueTuple<bool, string>(ModFunc.isAutoLogin, ModFunc.strAutoLogin)
				},
				new ValueTuple<bool, string>[]
				{
					new ValueTuple<bool, string>(!ModFunc.ModNotLogo && ModFunc.isLogo, ModFunc.strLogo),
					new ValueTuple<bool, string>(!ModFunc.ModNotLogo && ModFunc.isLogoGif, ModFunc.strLogoGif),
					new ValueTuple<bool, string>(ModFunc.AnPlayer, ModFunc.strAnPlayer),
					new ValueTuple<bool, string>(ModFunc.isShowID, ModFunc.strShowID),
					new ValueTuple<bool, string>(ModFunc.isInventory, ModFunc.strInventoryOFF),
					new ValueTuple<bool, string>(ModFunc.isEffectInven, ModFunc.strEffectOff)
				},
				new ValueTuple<bool, string>[]
				{
					new ValueTuple<bool, string>(ModFunc.GI().isIntroOff, ModFunc.strIntroOff),
					new ValueTuple<bool, string>(ModFunc.GiamDungLuong, ModFunc.strGiamDungLuong),
					new ValueTuple<bool, string>(ModFunc.isEditButton, ModFunc.strEditButton),
					new ValueTuple<bool, string>(ModFunc.isFilterItem, "Lọc đồ")
				}
			};
			int currentTab = GameCanvas.panel.currentTabIndex;
			if (currentTab >= 0 && currentTab < modFuncByTab.Length)
			{
				ValueTuple<bool, string>[] array2 = modFuncByTab[currentTab];
				List<string> tabStrings = new List<string>();
				foreach (ValueTuple<bool, string> valueTuple in array2)
				{
					bool isEnabled = valueTuple.Item1;
					string label = valueTuple.Item2;
					if (!ModFunc.ModNotLogo || (label != ModFunc.strLogo && label != ModFunc.strLogoGif))
					{
						tabStrings.Add((isEnabled ? "[x]   " : "[  ]   ") + label);
					}
				}
				Panel.strModFunc = tabStrings.ToArray();
				return;
			}
			Panel.strModFunc = new string[0];
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0011C79D File Offset: 0x0011A99D
		public void HP_MPup()
		{
			Sound.playSound(SoundMn.HP_UP, 0.5f);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0011C7B0 File Offset: 0x0011A9B0
		public void charPunch(bool isKick, float volumn)
		{
			if (!Char.myCharz().me)
			{
				SoundMn.volume /= 2f;
			}
			if (volumn <= 0f)
			{
				volumn = 0.01f;
			}
			int num = Res.random(0, 3);
			if (isKick)
			{
				Sound.playSound((num != 0) ? SoundMn.MEDIUM_KICK : SoundMn.LOW_KICK, 0.1f);
			}
			else
			{
				Sound.playSound((num != 0) ? SoundMn.MEDIUM_PUNCH : SoundMn.LOW_PUNCH, 0.1f);
			}
			this.poolCount++;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0011C835 File Offset: 0x0011AA35
		public void thaiduonghasan()
		{
			Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0011C854 File Offset: 0x0011AA54
		public void rain()
		{
			Sound.playMus(SoundMn.RAIN, 0.3f, true);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0011C866 File Offset: 0x0011AA66
		public void gongName()
		{
			Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
			this.poolCount++;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0011C885 File Offset: 0x0011AA85
		public void gong()
		{
			Sound.playSound(SoundMn.GONG, 0.2f);
			this.poolCount++;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0011C8A4 File Offset: 0x0011AAA4
		public void getItem()
		{
			Sound.playSound(SoundMn.GET_ITEM, 0.3f);
			this.poolCount++;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0011C8C4 File Offset: 0x0011AAC4
		public void soundToolOption()
		{
			GameCanvas.isPlaySound = !GameCanvas.isPlaySound;
			if (GameCanvas.isPlaySound)
			{
				SoundMn.gI().loadSound(TileMap.mapID);
				Rms.saveRMSInt("isPlaySound", 1);
			}
			else
			{
				SoundMn.gI().closeSound();
				Rms.saveRMSInt("isPlaySound", 0);
			}
			this.getStrOption();
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0011C91C File Offset: 0x0011AB1C
		public void chatVipToolOption()
		{
			GameScr.isPaintChatVip = !GameScr.isPaintChatVip;
			if (GameScr.isPaintChatVip)
			{
				Rms.saveRMSInt("serverchat", 0);
			}
			else
			{
				Rms.saveRMSInt("serverchat", 1);
			}
			this.getStrOption();
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0011C950 File Offset: 0x0011AB50
		public void analogToolOption()
		{
			if (GameScr.isAnalog == 0)
			{
				GameScr.isAnalog = 1;
				Rms.saveRMSInt("analog", GameScr.isAnalog);
				GameScr.setSkillBarPosition();
			}
			else
			{
				GameScr.isAnalog = 0;
				Rms.saveRMSInt("analog", GameScr.isAnalog);
				GameScr.setSkillBarPosition();
			}
			this.getStrOption();
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0011C9A0 File Offset: 0x0011ABA0
		public void CaseAnalog()
		{
			if (!Main.isPC || Main.isIPhone)
			{
				if (!GameCanvas.isTouch)
				{
					this.chatVipToolOption();
					return;
				}
				this.analogToolOption();
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0011C9C4 File Offset: 0x0011ABC4
		public void CaseSizeScr()
		{
			if (GameCanvas.lowGraphic)
			{
				Rms.saveRMSInt("lowGraphic", 0);
				GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
			}
			else
			{
				Rms.saveRMSInt("lowGraphic", 1);
				GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
			}
			this.getStrOption();
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0011CA16 File Offset: 0x0011AC16
		public void AuraToolOption()
		{
			if (Char.isPaintAura)
			{
				Rms.saveRMSInt("isPaintAura", 0);
				Char.isPaintAura = false;
			}
			else
			{
				Rms.saveRMSInt("isPaintAura", 1);
				Char.isPaintAura = true;
			}
			this.getStrOption();
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0011CA49 File Offset: 0x0011AC49
		public void AuraToolOption2()
		{
			if (Char.isPaintAura2)
			{
				Rms.saveRMSInt("isPaintAura2", 0);
				Char.isPaintAura2 = false;
			}
			else
			{
				Rms.saveRMSInt("isPaintAura2", 1);
				Char.isPaintAura2 = true;
			}
			this.getStrOption();
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0011CA7C File Offset: 0x0011AC7C
		public void closeSound()
		{
			Sound.stopAll = true;
			this.stopAll();
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0011CA8A File Offset: 0x0011AC8A
		public void bigeExlode()
		{
			Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0011CAA9 File Offset: 0x0011ACA9
		public void explode_1()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0011CAA9 File Offset: 0x0011ACA9
		public void explode_2()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0011CAC8 File Offset: 0x0011ACC8
		public void traidatKame()
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
			this.poolCount++;
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0011CAE7 File Offset: 0x0011ACE7
		public void namekKame()
		{
			Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0011CB06 File Offset: 0x0011AD06
		public void nameLazer()
		{
			Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
			this.poolCount++;
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0011CB25 File Offset: 0x0011AD25
		public void xaydaKame()
		{
			Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0011CB44 File Offset: 0x0011AD44
		public void mobKame(int type)
		{
			int id = SoundMn.XAYDA_KAME;
			if (type == 13)
			{
				id = SoundMn.NORMAL_KAME;
			}
			Sound.playSound(id, 0.1f);
			this.poolCount++;
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0011CB7C File Offset: 0x0011AD7C
		public void charRun(float volumn)
		{
			if (!Char.myCharz().me)
			{
				SoundMn.volume /= 2f;
				if (volumn <= 0f)
				{
					volumn = 0.01f;
				}
			}
			if (GameCanvas.gameTick % 8 == 0)
			{
				Sound.playSound(SoundMn.MOVE, volumn);
				this.poolCount++;
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0011CBD6 File Offset: 0x0011ADD6
		public void monkeyRun(float volumn)
		{
			if (GameCanvas.gameTick % 8 == 0)
			{
				Sound.playSound(SoundMn.KHICHAY, 0.2f);
				this.poolCount++;
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0011CBFE File Offset: 0x0011ADFE
		public void charFall()
		{
			Sound.playSound(SoundMn.MOVE, 0.1f);
			this.poolCount++;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0011CC1D File Offset: 0x0011AE1D
		public void charJump()
		{
			Sound.playSound(SoundMn.MOVE, 0.2f);
			this.poolCount++;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0011CC3C File Offset: 0x0011AE3C
		public void panelOpen()
		{
			Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000034B9 File Offset: 0x000016B9
		public void buttonClose()
		{
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0011CC5B File Offset: 0x0011AE5B
		public void buttonClick()
		{
			Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0011CC7A File Offset: 0x0011AE7A
		public void charFly()
		{
			Sound.playSound(SoundMn.FLY, 0.2f);
			this.poolCount++;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0011CC99 File Offset: 0x0011AE99
		public void openMenu()
		{
			Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0011CCB8 File Offset: 0x0011AEB8
		public void panelClick()
		{
			Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0011CCD7 File Offset: 0x0011AED7
		public void eatPeans()
		{
			Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0011CCF6 File Offset: 0x0011AEF6
		public void openDialog()
		{
			Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0011CD07 File Offset: 0x0011AF07
		public void hoisinh()
		{
			Sound.playSound(SoundMn.HOISINH, 0.5f);
			this.poolCount++;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000034B9 File Offset: 0x000016B9
		public void taitaoPause()
		{
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0011CD28 File Offset: 0x0011AF28
		public bool isPlayRain()
		{
			bool result;
			try
			{
				result = Sound.isPlayingSound();
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isPlayAirShip()
		{
			return false;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0011CD54 File Offset: 0x0011AF54
		public void airShip()
		{
			SoundMn.cout++;
			if (SoundMn.cout % 2 == 0)
			{
				Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000034B9 File Offset: 0x000016B9
		public void pauseAirShip()
		{
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000034B9 File Offset: 0x000016B9
		public void resumeAirShip()
		{
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0011CD7B File Offset: 0x0011AF7B
		public void stopAll()
		{
			Sound.stopAllz();
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0011CD82 File Offset: 0x0011AF82
		public void backToRegister()
		{
			Session_ME.gI().close();
			GameCanvas.panel.hide();
			GameCanvas.loginScr.actRegister();
			GameCanvas.loginScr.switchToMe();
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0011CDAC File Offset: 0x0011AFAC
		public void newKame()
		{
			this.poolCount++;
			if (this.poolCount % 15 == 0)
			{
				Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
			}
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0011CDD6 File Offset: 0x0011AFD6
		public void radarClick()
		{
			Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0011CDE7 File Offset: 0x0011AFE7
		public void radarItem()
		{
			Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0011CDF8 File Offset: 0x0011AFF8
		public static void playSound(int x, int y, int id, float volume)
		{
			Sound.playSound(id, volume);
		}

		// Token: 0x040022F2 RID: 8946
		public static bool IsDelAcc;

		// Token: 0x040022F3 RID: 8947
		public static SoundMn gIz;

		// Token: 0x040022F4 RID: 8948
		public static bool isSound = true;

		// Token: 0x040022F5 RID: 8949
		public static float volume = 0.5f;

		// Token: 0x040022F6 RID: 8950
		private static int MAX_VOLUME = 10;

		// Token: 0x040022F7 RID: 8951
		public static int AIR_SHIP;

		// Token: 0x040022F8 RID: 8952
		public static int RAIN = 1;

		// Token: 0x040022F9 RID: 8953
		public static int TAITAONANGLUONG = 2;

		// Token: 0x040022FA RID: 8954
		public static int GET_ITEM;

		// Token: 0x040022FB RID: 8955
		public static int MOVE = 1;

		// Token: 0x040022FC RID: 8956
		public static int LOW_PUNCH = 2;

		// Token: 0x040022FD RID: 8957
		public static int LOW_KICK = 3;

		// Token: 0x040022FE RID: 8958
		public static int FLY = 4;

		// Token: 0x040022FF RID: 8959
		public static int JUMP = 5;

		// Token: 0x04002300 RID: 8960
		public static int PANEL_OPEN = 6;

		// Token: 0x04002301 RID: 8961
		public static int BUTTON_CLOSE = 7;

		// Token: 0x04002302 RID: 8962
		public static int BUTTON_CLICK = 8;

		// Token: 0x04002303 RID: 8963
		public static int MEDIUM_PUNCH = 9;

		// Token: 0x04002304 RID: 8964
		public static int MEDIUM_KICK = 10;

		// Token: 0x04002305 RID: 8965
		public static int PANEL_CLICK = 11;

		// Token: 0x04002306 RID: 8966
		public static int EAT_PEAN = 12;

		// Token: 0x04002307 RID: 8967
		public static int OPEN_DIALOG = 13;

		// Token: 0x04002308 RID: 8968
		public static int NORMAL_KAME = 14;

		// Token: 0x04002309 RID: 8969
		public static int NAMEK_KAME = 15;

		// Token: 0x0400230A RID: 8970
		public static int XAYDA_KAME = 16;

		// Token: 0x0400230B RID: 8971
		public static int EXPLODE_1 = 17;

		// Token: 0x0400230C RID: 8972
		public static int EXPLODE_2 = 18;

		// Token: 0x0400230D RID: 8973
		public static int TRAIDAT_KAME = 19;

		// Token: 0x0400230E RID: 8974
		public static int HP_UP = 20;

		// Token: 0x0400230F RID: 8975
		public static int THAIDUONGHASAN = 21;

		// Token: 0x04002310 RID: 8976
		public static int HOISINH = 22;

		// Token: 0x04002311 RID: 8977
		public static int GONG = 23;

		// Token: 0x04002312 RID: 8978
		public static int KHICHAY = 24;

		// Token: 0x04002313 RID: 8979
		public static int BIG_EXPLODE = 25;

		// Token: 0x04002314 RID: 8980
		public static int NAMEK_LAZER = 26;

		// Token: 0x04002315 RID: 8981
		public static int NAMEK_CHARGE = 27;

		// Token: 0x04002316 RID: 8982
		public static int RADAR_CLICK = 28;

		// Token: 0x04002317 RID: 8983
		public static int RADAR_ITEM = 29;

		// Token: 0x04002318 RID: 8984
		public static int FIREWORK = 30;

		// Token: 0x04002319 RID: 8985
		public static int KAMEX10_0 = 31;

		// Token: 0x0400231A RID: 8986
		public static int KAMEX10_1 = 32;

		// Token: 0x0400231B RID: 8987
		public static int DESTROY_0 = 33;

		// Token: 0x0400231C RID: 8988
		public static int DESTROY_1 = 34;

		// Token: 0x0400231D RID: 8989
		public static int MAFUBA_0 = 35;

		// Token: 0x0400231E RID: 8990
		public static int MAFUBA_1 = 36;

		// Token: 0x0400231F RID: 8991
		public static int MAFUBA_2 = 37;

		// Token: 0x04002320 RID: 8992
		public static int DESTROY_2 = 38;

		// Token: 0x04002321 RID: 8993
		public int poolCount;

		// Token: 0x04002322 RID: 8994
		public static int cout = 1;
	}
}
