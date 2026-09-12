using System;
using System.Collections.Generic;

namespace Game4
{
	// Token: 0x02000262 RID: 610
	public class SoundMn
	{
		// Token: 0x06001B70 RID: 7024 RVA: 0x001B0EFA File Offset: 0x001AF0FA
		public static SoundMn gI()
		{
			if (SoundMn.gIz == null)
			{
				SoundMn.gIz = new SoundMn();
			}
			return SoundMn.gIz;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x001B0F14 File Offset: 0x001AF114
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

		// Token: 0x06001B72 RID: 7026 RVA: 0x001B10A4 File Offset: 0x001AF2A4
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

		// Token: 0x06001B73 RID: 7027 RVA: 0x001B1450 File Offset: 0x001AF650
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

		// Token: 0x06001B74 RID: 7028 RVA: 0x001B157C File Offset: 0x001AF77C
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

		// Token: 0x06001B75 RID: 7029 RVA: 0x001B1841 File Offset: 0x001AFA41
		public void HP_MPup()
		{
			Sound.playSound(SoundMn.HP_UP, 0.5f);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x001B1854 File Offset: 0x001AFA54
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

		// Token: 0x06001B77 RID: 7031 RVA: 0x001B18D9 File Offset: 0x001AFAD9
		public void thaiduonghasan()
		{
			Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x001B18F8 File Offset: 0x001AFAF8
		public void rain()
		{
			Sound.playMus(SoundMn.RAIN, 0.3f, true);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x001B190A File Offset: 0x001AFB0A
		public void gongName()
		{
			Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x001B1929 File Offset: 0x001AFB29
		public void gong()
		{
			Sound.playSound(SoundMn.GONG, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x001B1948 File Offset: 0x001AFB48
		public void getItem()
		{
			Sound.playSound(SoundMn.GET_ITEM, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x001B1968 File Offset: 0x001AFB68
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

		// Token: 0x06001B7D RID: 7037 RVA: 0x001B19C0 File Offset: 0x001AFBC0
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

		// Token: 0x06001B7E RID: 7038 RVA: 0x001B19F4 File Offset: 0x001AFBF4
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

		// Token: 0x06001B7F RID: 7039 RVA: 0x001B1A44 File Offset: 0x001AFC44
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

		// Token: 0x06001B80 RID: 7040 RVA: 0x001B1A68 File Offset: 0x001AFC68
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

		// Token: 0x06001B81 RID: 7041 RVA: 0x001B1ABA File Offset: 0x001AFCBA
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

		// Token: 0x06001B82 RID: 7042 RVA: 0x001B1AED File Offset: 0x001AFCED
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

		// Token: 0x06001B83 RID: 7043 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x001B1B20 File Offset: 0x001AFD20
		public void closeSound()
		{
			Sound.stopAll = true;
			this.stopAll();
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x001B1B2E File Offset: 0x001AFD2E
		public void bigeExlode()
		{
			Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x001B1B4D File Offset: 0x001AFD4D
		public void explode_1()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x001B1B4D File Offset: 0x001AFD4D
		public void explode_2()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x001B1B6C File Offset: 0x001AFD6C
		public void traidatKame()
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
			this.poolCount++;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x001B1B8B File Offset: 0x001AFD8B
		public void namekKame()
		{
			Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x001B1BAA File Offset: 0x001AFDAA
		public void nameLazer()
		{
			Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x001B1BC9 File Offset: 0x001AFDC9
		public void xaydaKame()
		{
			Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x001B1BE8 File Offset: 0x001AFDE8
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

		// Token: 0x06001B8D RID: 7053 RVA: 0x001B1C20 File Offset: 0x001AFE20
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

		// Token: 0x06001B8E RID: 7054 RVA: 0x001B1C7A File Offset: 0x001AFE7A
		public void monkeyRun(float volumn)
		{
			if (GameCanvas.gameTick % 8 == 0)
			{
				Sound.playSound(SoundMn.KHICHAY, 0.2f);
				this.poolCount++;
			}
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x001B1CA2 File Offset: 0x001AFEA2
		public void charFall()
		{
			Sound.playSound(SoundMn.MOVE, 0.1f);
			this.poolCount++;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x001B1CC1 File Offset: 0x001AFEC1
		public void charJump()
		{
			Sound.playSound(SoundMn.MOVE, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x001B1CE0 File Offset: 0x001AFEE0
		public void panelOpen()
		{
			Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000034B9 File Offset: 0x000016B9
		public void buttonClose()
		{
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x001B1CFF File Offset: 0x001AFEFF
		public void buttonClick()
		{
			Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x001B1D1E File Offset: 0x001AFF1E
		public void charFly()
		{
			Sound.playSound(SoundMn.FLY, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x001B1D3D File Offset: 0x001AFF3D
		public void openMenu()
		{
			Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x001B1D5C File Offset: 0x001AFF5C
		public void panelClick()
		{
			Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x001B1D7B File Offset: 0x001AFF7B
		public void eatPeans()
		{
			Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x001B1D9A File Offset: 0x001AFF9A
		public void openDialog()
		{
			Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x001B1DAB File Offset: 0x001AFFAB
		public void hoisinh()
		{
			Sound.playSound(SoundMn.HOISINH, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000034B9 File Offset: 0x000016B9
		public void taitaoPause()
		{
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x001B1DCC File Offset: 0x001AFFCC
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

		// Token: 0x06001B9C RID: 7068 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isPlayAirShip()
		{
			return false;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x001B1DF8 File Offset: 0x001AFFF8
		public void airShip()
		{
			SoundMn.cout++;
			if (SoundMn.cout % 2 == 0)
			{
				Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
			}
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000034B9 File Offset: 0x000016B9
		public void pauseAirShip()
		{
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000034B9 File Offset: 0x000016B9
		public void resumeAirShip()
		{
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x001B1E1F File Offset: 0x001B001F
		public void stopAll()
		{
			Sound.stopAllz();
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x001B1E26 File Offset: 0x001B0026
		public void backToRegister()
		{
			Session_ME.gI().close();
			GameCanvas.panel.hide();
			GameCanvas.loginScr.actRegister();
			GameCanvas.loginScr.switchToMe();
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x001B1E50 File Offset: 0x001B0050
		public void newKame()
		{
			this.poolCount++;
			if (this.poolCount % 15 == 0)
			{
				Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
			}
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x001B1E7A File Offset: 0x001B007A
		public void radarClick()
		{
			Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x001B1E8B File Offset: 0x001B008B
		public void radarItem()
		{
			Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x001B1E9C File Offset: 0x001B009C
		public static void playSound(int x, int y, int id, float volume)
		{
			Sound.playSound(id, volume);
		}

		// Token: 0x04003571 RID: 13681
		public static bool IsDelAcc;

		// Token: 0x04003572 RID: 13682
		public static SoundMn gIz;

		// Token: 0x04003573 RID: 13683
		public static bool isSound = true;

		// Token: 0x04003574 RID: 13684
		public static float volume = 0.5f;

		// Token: 0x04003575 RID: 13685
		private static int MAX_VOLUME = 10;

		// Token: 0x04003576 RID: 13686
		public static int AIR_SHIP;

		// Token: 0x04003577 RID: 13687
		public static int RAIN = 1;

		// Token: 0x04003578 RID: 13688
		public static int TAITAONANGLUONG = 2;

		// Token: 0x04003579 RID: 13689
		public static int GET_ITEM;

		// Token: 0x0400357A RID: 13690
		public static int MOVE = 1;

		// Token: 0x0400357B RID: 13691
		public static int LOW_PUNCH = 2;

		// Token: 0x0400357C RID: 13692
		public static int LOW_KICK = 3;

		// Token: 0x0400357D RID: 13693
		public static int FLY = 4;

		// Token: 0x0400357E RID: 13694
		public static int JUMP = 5;

		// Token: 0x0400357F RID: 13695
		public static int PANEL_OPEN = 6;

		// Token: 0x04003580 RID: 13696
		public static int BUTTON_CLOSE = 7;

		// Token: 0x04003581 RID: 13697
		public static int BUTTON_CLICK = 8;

		// Token: 0x04003582 RID: 13698
		public static int MEDIUM_PUNCH = 9;

		// Token: 0x04003583 RID: 13699
		public static int MEDIUM_KICK = 10;

		// Token: 0x04003584 RID: 13700
		public static int PANEL_CLICK = 11;

		// Token: 0x04003585 RID: 13701
		public static int EAT_PEAN = 12;

		// Token: 0x04003586 RID: 13702
		public static int OPEN_DIALOG = 13;

		// Token: 0x04003587 RID: 13703
		public static int NORMAL_KAME = 14;

		// Token: 0x04003588 RID: 13704
		public static int NAMEK_KAME = 15;

		// Token: 0x04003589 RID: 13705
		public static int XAYDA_KAME = 16;

		// Token: 0x0400358A RID: 13706
		public static int EXPLODE_1 = 17;

		// Token: 0x0400358B RID: 13707
		public static int EXPLODE_2 = 18;

		// Token: 0x0400358C RID: 13708
		public static int TRAIDAT_KAME = 19;

		// Token: 0x0400358D RID: 13709
		public static int HP_UP = 20;

		// Token: 0x0400358E RID: 13710
		public static int THAIDUONGHASAN = 21;

		// Token: 0x0400358F RID: 13711
		public static int HOISINH = 22;

		// Token: 0x04003590 RID: 13712
		public static int GONG = 23;

		// Token: 0x04003591 RID: 13713
		public static int KHICHAY = 24;

		// Token: 0x04003592 RID: 13714
		public static int BIG_EXPLODE = 25;

		// Token: 0x04003593 RID: 13715
		public static int NAMEK_LAZER = 26;

		// Token: 0x04003594 RID: 13716
		public static int NAMEK_CHARGE = 27;

		// Token: 0x04003595 RID: 13717
		public static int RADAR_CLICK = 28;

		// Token: 0x04003596 RID: 13718
		public static int RADAR_ITEM = 29;

		// Token: 0x04003597 RID: 13719
		public static int FIREWORK = 30;

		// Token: 0x04003598 RID: 13720
		public static int KAMEX10_0 = 31;

		// Token: 0x04003599 RID: 13721
		public static int KAMEX10_1 = 32;

		// Token: 0x0400359A RID: 13722
		public static int DESTROY_0 = 33;

		// Token: 0x0400359B RID: 13723
		public static int DESTROY_1 = 34;

		// Token: 0x0400359C RID: 13724
		public static int MAFUBA_0 = 35;

		// Token: 0x0400359D RID: 13725
		public static int MAFUBA_1 = 36;

		// Token: 0x0400359E RID: 13726
		public static int MAFUBA_2 = 37;

		// Token: 0x0400359F RID: 13727
		public static int DESTROY_2 = 38;

		// Token: 0x040035A0 RID: 13728
		public int poolCount;

		// Token: 0x040035A1 RID: 13729
		public static int cout = 1;
	}
}
