using System;
using System.Collections.Generic;

namespace Game2
{
	// Token: 0x02000412 RID: 1042
	public class SoundMn
	{
		// Token: 0x06002EB8 RID: 11960 RVA: 0x002DB042 File Offset: 0x002D9242
		public static SoundMn gI()
		{
			if (SoundMn.gIz == null)
			{
				SoundMn.gIz = new SoundMn();
			}
			return SoundMn.gIz;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x002DB05C File Offset: 0x002D925C
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

		// Token: 0x06002EBA RID: 11962 RVA: 0x002DB1EC File Offset: 0x002D93EC
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

		// Token: 0x06002EBB RID: 11963 RVA: 0x002DB598 File Offset: 0x002D9798
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

		// Token: 0x06002EBC RID: 11964 RVA: 0x002DB6C4 File Offset: 0x002D98C4
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

		// Token: 0x06002EBD RID: 11965 RVA: 0x002DB989 File Offset: 0x002D9B89
		public void HP_MPup()
		{
			Sound.playSound(SoundMn.HP_UP, 0.5f);
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x002DB99C File Offset: 0x002D9B9C
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

		// Token: 0x06002EBF RID: 11967 RVA: 0x002DBA21 File Offset: 0x002D9C21
		public void thaiduonghasan()
		{
			Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x002DBA40 File Offset: 0x002D9C40
		public void rain()
		{
			Sound.playMus(SoundMn.RAIN, 0.3f, true);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x002DBA52 File Offset: 0x002D9C52
		public void gongName()
		{
			Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x002DBA71 File Offset: 0x002D9C71
		public void gong()
		{
			Sound.playSound(SoundMn.GONG, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x002DBA90 File Offset: 0x002D9C90
		public void getItem()
		{
			Sound.playSound(SoundMn.GET_ITEM, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x002DBAB0 File Offset: 0x002D9CB0
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

		// Token: 0x06002EC5 RID: 11973 RVA: 0x002DBB08 File Offset: 0x002D9D08
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

		// Token: 0x06002EC6 RID: 11974 RVA: 0x002DBB3C File Offset: 0x002D9D3C
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

		// Token: 0x06002EC7 RID: 11975 RVA: 0x002DBB8C File Offset: 0x002D9D8C
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

		// Token: 0x06002EC8 RID: 11976 RVA: 0x002DBBB0 File Offset: 0x002D9DB0
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

		// Token: 0x06002EC9 RID: 11977 RVA: 0x002DBC02 File Offset: 0x002D9E02
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

		// Token: 0x06002ECA RID: 11978 RVA: 0x002DBC35 File Offset: 0x002D9E35
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

		// Token: 0x06002ECB RID: 11979 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x002DBC68 File Offset: 0x002D9E68
		public void closeSound()
		{
			Sound.stopAll = true;
			this.stopAll();
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x002DBC76 File Offset: 0x002D9E76
		public void bigeExlode()
		{
			Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x002DBC95 File Offset: 0x002D9E95
		public void explode_1()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x002DBC95 File Offset: 0x002D9E95
		public void explode_2()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x002DBCB4 File Offset: 0x002D9EB4
		public void traidatKame()
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
			this.poolCount++;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x002DBCD3 File Offset: 0x002D9ED3
		public void namekKame()
		{
			Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x002DBCF2 File Offset: 0x002D9EF2
		public void nameLazer()
		{
			Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x002DBD11 File Offset: 0x002D9F11
		public void xaydaKame()
		{
			Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x002DBD30 File Offset: 0x002D9F30
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

		// Token: 0x06002ED5 RID: 11989 RVA: 0x002DBD68 File Offset: 0x002D9F68
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

		// Token: 0x06002ED6 RID: 11990 RVA: 0x002DBDC2 File Offset: 0x002D9FC2
		public void monkeyRun(float volumn)
		{
			if (GameCanvas.gameTick % 8 == 0)
			{
				Sound.playSound(SoundMn.KHICHAY, 0.2f);
				this.poolCount++;
			}
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x002DBDEA File Offset: 0x002D9FEA
		public void charFall()
		{
			Sound.playSound(SoundMn.MOVE, 0.1f);
			this.poolCount++;
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x002DBE09 File Offset: 0x002DA009
		public void charJump()
		{
			Sound.playSound(SoundMn.MOVE, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x002DBE28 File Offset: 0x002DA028
		public void panelOpen()
		{
			Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000034B9 File Offset: 0x000016B9
		public void buttonClose()
		{
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x002DBE47 File Offset: 0x002DA047
		public void buttonClick()
		{
			Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x002DBE66 File Offset: 0x002DA066
		public void charFly()
		{
			Sound.playSound(SoundMn.FLY, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x002DBE85 File Offset: 0x002DA085
		public void openMenu()
		{
			Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x002DBEA4 File Offset: 0x002DA0A4
		public void panelClick()
		{
			Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x002DBEC3 File Offset: 0x002DA0C3
		public void eatPeans()
		{
			Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x002DBEE2 File Offset: 0x002DA0E2
		public void openDialog()
		{
			Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x002DBEF3 File Offset: 0x002DA0F3
		public void hoisinh()
		{
			Sound.playSound(SoundMn.HOISINH, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000034B9 File Offset: 0x000016B9
		public void taitaoPause()
		{
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x002DBF14 File Offset: 0x002DA114
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

		// Token: 0x06002EE4 RID: 12004 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isPlayAirShip()
		{
			return false;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x002DBF40 File Offset: 0x002DA140
		public void airShip()
		{
			SoundMn.cout++;
			if (SoundMn.cout % 2 == 0)
			{
				Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
			}
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000034B9 File Offset: 0x000016B9
		public void pauseAirShip()
		{
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000034B9 File Offset: 0x000016B9
		public void resumeAirShip()
		{
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x002DBF67 File Offset: 0x002DA167
		public void stopAll()
		{
			Sound.stopAllz();
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x002DBF6E File Offset: 0x002DA16E
		public void backToRegister()
		{
			Session_ME.gI().close();
			GameCanvas.panel.hide();
			GameCanvas.loginScr.actRegister();
			GameCanvas.loginScr.switchToMe();
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x002DBF98 File Offset: 0x002DA198
		public void newKame()
		{
			this.poolCount++;
			if (this.poolCount % 15 == 0)
			{
				Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
			}
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x002DBFC2 File Offset: 0x002DA1C2
		public void radarClick()
		{
			Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x002DBFD3 File Offset: 0x002DA1D3
		public void radarItem()
		{
			Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x002DBFE4 File Offset: 0x002DA1E4
		public static void playSound(int x, int y, int id, float volume)
		{
			Sound.playSound(id, volume);
		}

		// Token: 0x04005A6F RID: 23151
		public static bool IsDelAcc;

		// Token: 0x04005A70 RID: 23152
		public static SoundMn gIz;

		// Token: 0x04005A71 RID: 23153
		public static bool isSound = true;

		// Token: 0x04005A72 RID: 23154
		public static float volume = 0.5f;

		// Token: 0x04005A73 RID: 23155
		private static int MAX_VOLUME = 10;

		// Token: 0x04005A74 RID: 23156
		public static int AIR_SHIP;

		// Token: 0x04005A75 RID: 23157
		public static int RAIN = 1;

		// Token: 0x04005A76 RID: 23158
		public static int TAITAONANGLUONG = 2;

		// Token: 0x04005A77 RID: 23159
		public static int GET_ITEM;

		// Token: 0x04005A78 RID: 23160
		public static int MOVE = 1;

		// Token: 0x04005A79 RID: 23161
		public static int LOW_PUNCH = 2;

		// Token: 0x04005A7A RID: 23162
		public static int LOW_KICK = 3;

		// Token: 0x04005A7B RID: 23163
		public static int FLY = 4;

		// Token: 0x04005A7C RID: 23164
		public static int JUMP = 5;

		// Token: 0x04005A7D RID: 23165
		public static int PANEL_OPEN = 6;

		// Token: 0x04005A7E RID: 23166
		public static int BUTTON_CLOSE = 7;

		// Token: 0x04005A7F RID: 23167
		public static int BUTTON_CLICK = 8;

		// Token: 0x04005A80 RID: 23168
		public static int MEDIUM_PUNCH = 9;

		// Token: 0x04005A81 RID: 23169
		public static int MEDIUM_KICK = 10;

		// Token: 0x04005A82 RID: 23170
		public static int PANEL_CLICK = 11;

		// Token: 0x04005A83 RID: 23171
		public static int EAT_PEAN = 12;

		// Token: 0x04005A84 RID: 23172
		public static int OPEN_DIALOG = 13;

		// Token: 0x04005A85 RID: 23173
		public static int NORMAL_KAME = 14;

		// Token: 0x04005A86 RID: 23174
		public static int NAMEK_KAME = 15;

		// Token: 0x04005A87 RID: 23175
		public static int XAYDA_KAME = 16;

		// Token: 0x04005A88 RID: 23176
		public static int EXPLODE_1 = 17;

		// Token: 0x04005A89 RID: 23177
		public static int EXPLODE_2 = 18;

		// Token: 0x04005A8A RID: 23178
		public static int TRAIDAT_KAME = 19;

		// Token: 0x04005A8B RID: 23179
		public static int HP_UP = 20;

		// Token: 0x04005A8C RID: 23180
		public static int THAIDUONGHASAN = 21;

		// Token: 0x04005A8D RID: 23181
		public static int HOISINH = 22;

		// Token: 0x04005A8E RID: 23182
		public static int GONG = 23;

		// Token: 0x04005A8F RID: 23183
		public static int KHICHAY = 24;

		// Token: 0x04005A90 RID: 23184
		public static int BIG_EXPLODE = 25;

		// Token: 0x04005A91 RID: 23185
		public static int NAMEK_LAZER = 26;

		// Token: 0x04005A92 RID: 23186
		public static int NAMEK_CHARGE = 27;

		// Token: 0x04005A93 RID: 23187
		public static int RADAR_CLICK = 28;

		// Token: 0x04005A94 RID: 23188
		public static int RADAR_ITEM = 29;

		// Token: 0x04005A95 RID: 23189
		public static int FIREWORK = 30;

		// Token: 0x04005A96 RID: 23190
		public static int KAMEX10_0 = 31;

		// Token: 0x04005A97 RID: 23191
		public static int KAMEX10_1 = 32;

		// Token: 0x04005A98 RID: 23192
		public static int DESTROY_0 = 33;

		// Token: 0x04005A99 RID: 23193
		public static int DESTROY_1 = 34;

		// Token: 0x04005A9A RID: 23194
		public static int MAFUBA_0 = 35;

		// Token: 0x04005A9B RID: 23195
		public static int MAFUBA_1 = 36;

		// Token: 0x04005A9C RID: 23196
		public static int MAFUBA_2 = 37;

		// Token: 0x04005A9D RID: 23197
		public static int DESTROY_2 = 38;

		// Token: 0x04005A9E RID: 23198
		public int poolCount;

		// Token: 0x04005A9F RID: 23199
		public static int cout = 1;
	}
}
