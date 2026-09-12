using System;
using System.Collections.Generic;

namespace Game1
{
	// Token: 0x020004EA RID: 1258
	public class SoundMn
	{
		// Token: 0x0600385C RID: 14428 RVA: 0x003700E6 File Offset: 0x0036E2E6
		public static SoundMn gI()
		{
			if (SoundMn.gIz == null)
			{
				SoundMn.gIz = new SoundMn();
			}
			return SoundMn.gIz;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x00370100 File Offset: 0x0036E300
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

		// Token: 0x0600385E RID: 14430 RVA: 0x00370290 File Offset: 0x0036E490
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

		// Token: 0x0600385F RID: 14431 RVA: 0x0037063C File Offset: 0x0036E83C
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

		// Token: 0x06003860 RID: 14432 RVA: 0x00370768 File Offset: 0x0036E968
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

		// Token: 0x06003861 RID: 14433 RVA: 0x00370A2D File Offset: 0x0036EC2D
		public void HP_MPup()
		{
			Sound.playSound(SoundMn.HP_UP, 0.5f);
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x00370A40 File Offset: 0x0036EC40
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

		// Token: 0x06003863 RID: 14435 RVA: 0x00370AC5 File Offset: 0x0036ECC5
		public void thaiduonghasan()
		{
			Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x00370AE4 File Offset: 0x0036ECE4
		public void rain()
		{
			Sound.playMus(SoundMn.RAIN, 0.3f, true);
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x00370AF6 File Offset: 0x0036ECF6
		public void gongName()
		{
			Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x00370B15 File Offset: 0x0036ED15
		public void gong()
		{
			Sound.playSound(SoundMn.GONG, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x00370B34 File Offset: 0x0036ED34
		public void getItem()
		{
			Sound.playSound(SoundMn.GET_ITEM, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x00370B54 File Offset: 0x0036ED54
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

		// Token: 0x06003869 RID: 14441 RVA: 0x00370BAC File Offset: 0x0036EDAC
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

		// Token: 0x0600386A RID: 14442 RVA: 0x00370BE0 File Offset: 0x0036EDE0
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

		// Token: 0x0600386B RID: 14443 RVA: 0x00370C30 File Offset: 0x0036EE30
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

		// Token: 0x0600386C RID: 14444 RVA: 0x00370C54 File Offset: 0x0036EE54
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

		// Token: 0x0600386D RID: 14445 RVA: 0x00370CA6 File Offset: 0x0036EEA6
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

		// Token: 0x0600386E RID: 14446 RVA: 0x00370CD9 File Offset: 0x0036EED9
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

		// Token: 0x0600386F RID: 14447 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x00370D0C File Offset: 0x0036EF0C
		public void closeSound()
		{
			Sound.stopAll = true;
			this.stopAll();
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x00370D1A File Offset: 0x0036EF1A
		public void bigeExlode()
		{
			Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x00370D39 File Offset: 0x0036EF39
		public void explode_1()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x00370D39 File Offset: 0x0036EF39
		public void explode_2()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x00370D58 File Offset: 0x0036EF58
		public void traidatKame()
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
			this.poolCount++;
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x00370D77 File Offset: 0x0036EF77
		public void namekKame()
		{
			Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x00370D96 File Offset: 0x0036EF96
		public void nameLazer()
		{
			Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x00370DB5 File Offset: 0x0036EFB5
		public void xaydaKame()
		{
			Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x00370DD4 File Offset: 0x0036EFD4
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

		// Token: 0x06003879 RID: 14457 RVA: 0x00370E0C File Offset: 0x0036F00C
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

		// Token: 0x0600387A RID: 14458 RVA: 0x00370E66 File Offset: 0x0036F066
		public void monkeyRun(float volumn)
		{
			if (GameCanvas.gameTick % 8 == 0)
			{
				Sound.playSound(SoundMn.KHICHAY, 0.2f);
				this.poolCount++;
			}
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x00370E8E File Offset: 0x0036F08E
		public void charFall()
		{
			Sound.playSound(SoundMn.MOVE, 0.1f);
			this.poolCount++;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x00370EAD File Offset: 0x0036F0AD
		public void charJump()
		{
			Sound.playSound(SoundMn.MOVE, 0.2f);
			this.poolCount++;
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x00370ECC File Offset: 0x0036F0CC
		public void panelOpen()
		{
			Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000034B9 File Offset: 0x000016B9
		public void buttonClose()
		{
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x00370EEB File Offset: 0x0036F0EB
		public void buttonClick()
		{
			Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x00370F0A File Offset: 0x0036F10A
		public void charFly()
		{
			Sound.playSound(SoundMn.FLY, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x00370F29 File Offset: 0x0036F129
		public void openMenu()
		{
			Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x00370F48 File Offset: 0x0036F148
		public void panelClick()
		{
			Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x00370F67 File Offset: 0x0036F167
		public void eatPeans()
		{
			Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x00370F86 File Offset: 0x0036F186
		public void openDialog()
		{
			Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x00370F97 File Offset: 0x0036F197
		public void hoisinh()
		{
			Sound.playSound(SoundMn.HOISINH, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000034B9 File Offset: 0x000016B9
		public void taitaoPause()
		{
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x00370FB8 File Offset: 0x0036F1B8
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

		// Token: 0x06003888 RID: 14472 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isPlayAirShip()
		{
			return false;
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x00370FE4 File Offset: 0x0036F1E4
		public void airShip()
		{
			SoundMn.cout++;
			if (SoundMn.cout % 2 == 0)
			{
				Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
			}
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000034B9 File Offset: 0x000016B9
		public void pauseAirShip()
		{
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000034B9 File Offset: 0x000016B9
		public void resumeAirShip()
		{
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x0037100B File Offset: 0x0036F20B
		public void stopAll()
		{
			Sound.stopAllz();
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x00371012 File Offset: 0x0036F212
		public void backToRegister()
		{
			Session_ME.gI().close();
			GameCanvas.panel.hide();
			GameCanvas.loginScr.actRegister();
			GameCanvas.loginScr.switchToMe();
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x0037103C File Offset: 0x0036F23C
		public void newKame()
		{
			this.poolCount++;
			if (this.poolCount % 15 == 0)
			{
				Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
			}
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x00371066 File Offset: 0x0036F266
		public void radarClick()
		{
			Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x00371077 File Offset: 0x0036F277
		public void radarItem()
		{
			Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x00371088 File Offset: 0x0036F288
		public static void playSound(int x, int y, int id, float volume)
		{
			Sound.playSound(id, volume);
		}

		// Token: 0x04006CEE RID: 27886
		public static bool IsDelAcc;

		// Token: 0x04006CEF RID: 27887
		public static SoundMn gIz;

		// Token: 0x04006CF0 RID: 27888
		public static bool isSound = true;

		// Token: 0x04006CF1 RID: 27889
		public static float volume = 0.5f;

		// Token: 0x04006CF2 RID: 27890
		private static int MAX_VOLUME = 10;

		// Token: 0x04006CF3 RID: 27891
		public static int AIR_SHIP;

		// Token: 0x04006CF4 RID: 27892
		public static int RAIN = 1;

		// Token: 0x04006CF5 RID: 27893
		public static int TAITAONANGLUONG = 2;

		// Token: 0x04006CF6 RID: 27894
		public static int GET_ITEM;

		// Token: 0x04006CF7 RID: 27895
		public static int MOVE = 1;

		// Token: 0x04006CF8 RID: 27896
		public static int LOW_PUNCH = 2;

		// Token: 0x04006CF9 RID: 27897
		public static int LOW_KICK = 3;

		// Token: 0x04006CFA RID: 27898
		public static int FLY = 4;

		// Token: 0x04006CFB RID: 27899
		public static int JUMP = 5;

		// Token: 0x04006CFC RID: 27900
		public static int PANEL_OPEN = 6;

		// Token: 0x04006CFD RID: 27901
		public static int BUTTON_CLOSE = 7;

		// Token: 0x04006CFE RID: 27902
		public static int BUTTON_CLICK = 8;

		// Token: 0x04006CFF RID: 27903
		public static int MEDIUM_PUNCH = 9;

		// Token: 0x04006D00 RID: 27904
		public static int MEDIUM_KICK = 10;

		// Token: 0x04006D01 RID: 27905
		public static int PANEL_CLICK = 11;

		// Token: 0x04006D02 RID: 27906
		public static int EAT_PEAN = 12;

		// Token: 0x04006D03 RID: 27907
		public static int OPEN_DIALOG = 13;

		// Token: 0x04006D04 RID: 27908
		public static int NORMAL_KAME = 14;

		// Token: 0x04006D05 RID: 27909
		public static int NAMEK_KAME = 15;

		// Token: 0x04006D06 RID: 27910
		public static int XAYDA_KAME = 16;

		// Token: 0x04006D07 RID: 27911
		public static int EXPLODE_1 = 17;

		// Token: 0x04006D08 RID: 27912
		public static int EXPLODE_2 = 18;

		// Token: 0x04006D09 RID: 27913
		public static int TRAIDAT_KAME = 19;

		// Token: 0x04006D0A RID: 27914
		public static int HP_UP = 20;

		// Token: 0x04006D0B RID: 27915
		public static int THAIDUONGHASAN = 21;

		// Token: 0x04006D0C RID: 27916
		public static int HOISINH = 22;

		// Token: 0x04006D0D RID: 27917
		public static int GONG = 23;

		// Token: 0x04006D0E RID: 27918
		public static int KHICHAY = 24;

		// Token: 0x04006D0F RID: 27919
		public static int BIG_EXPLODE = 25;

		// Token: 0x04006D10 RID: 27920
		public static int NAMEK_LAZER = 26;

		// Token: 0x04006D11 RID: 27921
		public static int NAMEK_CHARGE = 27;

		// Token: 0x04006D12 RID: 27922
		public static int RADAR_CLICK = 28;

		// Token: 0x04006D13 RID: 27923
		public static int RADAR_ITEM = 29;

		// Token: 0x04006D14 RID: 27924
		public static int FIREWORK = 30;

		// Token: 0x04006D15 RID: 27925
		public static int KAMEX10_0 = 31;

		// Token: 0x04006D16 RID: 27926
		public static int KAMEX10_1 = 32;

		// Token: 0x04006D17 RID: 27927
		public static int DESTROY_0 = 33;

		// Token: 0x04006D18 RID: 27928
		public static int DESTROY_1 = 34;

		// Token: 0x04006D19 RID: 27929
		public static int MAFUBA_0 = 35;

		// Token: 0x04006D1A RID: 27930
		public static int MAFUBA_1 = 36;

		// Token: 0x04006D1B RID: 27931
		public static int MAFUBA_2 = 37;

		// Token: 0x04006D1C RID: 27932
		public static int DESTROY_2 = 38;

		// Token: 0x04006D1D RID: 27933
		public int poolCount;

		// Token: 0x04006D1E RID: 27934
		public static int cout = 1;
	}
}
