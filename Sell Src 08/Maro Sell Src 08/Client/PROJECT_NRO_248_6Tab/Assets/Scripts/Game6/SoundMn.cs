using System;
using System.Collections.Generic;

namespace Game6
{
	// Token: 0x020000B2 RID: 178
	public class SoundMn
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x00086D4A File Offset: 0x00084F4A
		public static SoundMn gI()
		{
			if (SoundMn.gIz == null)
			{
				SoundMn.gIz = new SoundMn();
			}
			return SoundMn.gIz;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00086D64 File Offset: 0x00084F64
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

		// Token: 0x0600082A RID: 2090 RVA: 0x00086EF4 File Offset: 0x000850F4
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

		// Token: 0x0600082B RID: 2091 RVA: 0x000872A0 File Offset: 0x000854A0
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

		// Token: 0x0600082C RID: 2092 RVA: 0x000873CC File Offset: 0x000855CC
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

		// Token: 0x0600082D RID: 2093 RVA: 0x00087691 File Offset: 0x00085891
		public void HP_MPup()
		{
			Sound.playSound(SoundMn.HP_UP, 0.5f);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000876A4 File Offset: 0x000858A4
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

		// Token: 0x0600082F RID: 2095 RVA: 0x00087729 File Offset: 0x00085929
		public void thaiduonghasan()
		{
			Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00087748 File Offset: 0x00085948
		public void rain()
		{
			Sound.playMus(SoundMn.RAIN, 0.3f, true);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0008775A File Offset: 0x0008595A
		public void gongName()
		{
			Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00087779 File Offset: 0x00085979
		public void gong()
		{
			Sound.playSound(SoundMn.GONG, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00087798 File Offset: 0x00085998
		public void getItem()
		{
			Sound.playSound(SoundMn.GET_ITEM, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000877B8 File Offset: 0x000859B8
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

		// Token: 0x06000835 RID: 2101 RVA: 0x00087810 File Offset: 0x00085A10
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

		// Token: 0x06000836 RID: 2102 RVA: 0x00087844 File Offset: 0x00085A44
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

		// Token: 0x06000837 RID: 2103 RVA: 0x00087894 File Offset: 0x00085A94
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

		// Token: 0x06000838 RID: 2104 RVA: 0x000878B8 File Offset: 0x00085AB8
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

		// Token: 0x06000839 RID: 2105 RVA: 0x0008790A File Offset: 0x00085B0A
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

		// Token: 0x0600083A RID: 2106 RVA: 0x0008793D File Offset: 0x00085B3D
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

		// Token: 0x0600083B RID: 2107 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00087970 File Offset: 0x00085B70
		public void closeSound()
		{
			Sound.stopAll = true;
			this.stopAll();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0008797E File Offset: 0x00085B7E
		public void bigeExlode()
		{
			Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0008799D File Offset: 0x00085B9D
		public void explode_1()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0008799D File Offset: 0x00085B9D
		public void explode_2()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000879BC File Offset: 0x00085BBC
		public void traidatKame()
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
			this.poolCount++;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000879DB File Offset: 0x00085BDB
		public void namekKame()
		{
			Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000879FA File Offset: 0x00085BFA
		public void nameLazer()
		{
			Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00087A19 File Offset: 0x00085C19
		public void xaydaKame()
		{
			Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00087A38 File Offset: 0x00085C38
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

		// Token: 0x06000845 RID: 2117 RVA: 0x00087A70 File Offset: 0x00085C70
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

		// Token: 0x06000846 RID: 2118 RVA: 0x00087ACA File Offset: 0x00085CCA
		public void monkeyRun(float volumn)
		{
			if (GameCanvas.gameTick % 8 == 0)
			{
				Sound.playSound(SoundMn.KHICHAY, 0.2f);
				this.poolCount++;
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00087AF2 File Offset: 0x00085CF2
		public void charFall()
		{
			Sound.playSound(SoundMn.MOVE, 0.1f);
			this.poolCount++;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00087B11 File Offset: 0x00085D11
		public void charJump()
		{
			Sound.playSound(SoundMn.MOVE, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00087B30 File Offset: 0x00085D30
		public void panelOpen()
		{
			Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000034B9 File Offset: 0x000016B9
		public void buttonClose()
		{
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00087B4F File Offset: 0x00085D4F
		public void buttonClick()
		{
			Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00087B6E File Offset: 0x00085D6E
		public void charFly()
		{
			Sound.playSound(SoundMn.FLY, 0.2f);
			this.poolCount++;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00087B8D File Offset: 0x00085D8D
		public void openMenu()
		{
			Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00087BAC File Offset: 0x00085DAC
		public void panelClick()
		{
			Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00087BCB File Offset: 0x00085DCB
		public void eatPeans()
		{
			Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00087BEA File Offset: 0x00085DEA
		public void openDialog()
		{
			Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00087BFB File Offset: 0x00085DFB
		public void hoisinh()
		{
			Sound.playSound(SoundMn.HOISINH, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000034B9 File Offset: 0x000016B9
		public void taitaoPause()
		{
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00087C1C File Offset: 0x00085E1C
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

		// Token: 0x06000854 RID: 2132 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isPlayAirShip()
		{
			return false;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00087C48 File Offset: 0x00085E48
		public void airShip()
		{
			SoundMn.cout++;
			if (SoundMn.cout % 2 == 0)
			{
				Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000034B9 File Offset: 0x000016B9
		public void pauseAirShip()
		{
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000034B9 File Offset: 0x000016B9
		public void resumeAirShip()
		{
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00087C6F File Offset: 0x00085E6F
		public void stopAll()
		{
			Sound.stopAllz();
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00087C76 File Offset: 0x00085E76
		public void backToRegister()
		{
			Session_ME.gI().close();
			GameCanvas.panel.hide();
			GameCanvas.loginScr.actRegister();
			GameCanvas.loginScr.switchToMe();
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00087CA0 File Offset: 0x00085EA0
		public void newKame()
		{
			this.poolCount++;
			if (this.poolCount % 15 == 0)
			{
				Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00087CCA File Offset: 0x00085ECA
		public void radarClick()
		{
			Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00087CDB File Offset: 0x00085EDB
		public void radarItem()
		{
			Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00087CEC File Offset: 0x00085EEC
		public static void playSound(int x, int y, int id, float volume)
		{
			Sound.playSound(id, volume);
		}

		// Token: 0x04001073 RID: 4211
		public static bool IsDelAcc;

		// Token: 0x04001074 RID: 4212
		public static SoundMn gIz;

		// Token: 0x04001075 RID: 4213
		public static bool isSound = true;

		// Token: 0x04001076 RID: 4214
		public static float volume = 0.5f;

		// Token: 0x04001077 RID: 4215
		private static int MAX_VOLUME = 10;

		// Token: 0x04001078 RID: 4216
		public static int AIR_SHIP;

		// Token: 0x04001079 RID: 4217
		public static int RAIN = 1;

		// Token: 0x0400107A RID: 4218
		public static int TAITAONANGLUONG = 2;

		// Token: 0x0400107B RID: 4219
		public static int GET_ITEM;

		// Token: 0x0400107C RID: 4220
		public static int MOVE = 1;

		// Token: 0x0400107D RID: 4221
		public static int LOW_PUNCH = 2;

		// Token: 0x0400107E RID: 4222
		public static int LOW_KICK = 3;

		// Token: 0x0400107F RID: 4223
		public static int FLY = 4;

		// Token: 0x04001080 RID: 4224
		public static int JUMP = 5;

		// Token: 0x04001081 RID: 4225
		public static int PANEL_OPEN = 6;

		// Token: 0x04001082 RID: 4226
		public static int BUTTON_CLOSE = 7;

		// Token: 0x04001083 RID: 4227
		public static int BUTTON_CLICK = 8;

		// Token: 0x04001084 RID: 4228
		public static int MEDIUM_PUNCH = 9;

		// Token: 0x04001085 RID: 4229
		public static int MEDIUM_KICK = 10;

		// Token: 0x04001086 RID: 4230
		public static int PANEL_CLICK = 11;

		// Token: 0x04001087 RID: 4231
		public static int EAT_PEAN = 12;

		// Token: 0x04001088 RID: 4232
		public static int OPEN_DIALOG = 13;

		// Token: 0x04001089 RID: 4233
		public static int NORMAL_KAME = 14;

		// Token: 0x0400108A RID: 4234
		public static int NAMEK_KAME = 15;

		// Token: 0x0400108B RID: 4235
		public static int XAYDA_KAME = 16;

		// Token: 0x0400108C RID: 4236
		public static int EXPLODE_1 = 17;

		// Token: 0x0400108D RID: 4237
		public static int EXPLODE_2 = 18;

		// Token: 0x0400108E RID: 4238
		public static int TRAIDAT_KAME = 19;

		// Token: 0x0400108F RID: 4239
		public static int HP_UP = 20;

		// Token: 0x04001090 RID: 4240
		public static int THAIDUONGHASAN = 21;

		// Token: 0x04001091 RID: 4241
		public static int HOISINH = 22;

		// Token: 0x04001092 RID: 4242
		public static int GONG = 23;

		// Token: 0x04001093 RID: 4243
		public static int KHICHAY = 24;

		// Token: 0x04001094 RID: 4244
		public static int BIG_EXPLODE = 25;

		// Token: 0x04001095 RID: 4245
		public static int NAMEK_LAZER = 26;

		// Token: 0x04001096 RID: 4246
		public static int NAMEK_CHARGE = 27;

		// Token: 0x04001097 RID: 4247
		public static int RADAR_CLICK = 28;

		// Token: 0x04001098 RID: 4248
		public static int RADAR_ITEM = 29;

		// Token: 0x04001099 RID: 4249
		public static int FIREWORK = 30;

		// Token: 0x0400109A RID: 4250
		public static int KAMEX10_0 = 31;

		// Token: 0x0400109B RID: 4251
		public static int KAMEX10_1 = 32;

		// Token: 0x0400109C RID: 4252
		public static int DESTROY_0 = 33;

		// Token: 0x0400109D RID: 4253
		public static int DESTROY_1 = 34;

		// Token: 0x0400109E RID: 4254
		public static int MAFUBA_0 = 35;

		// Token: 0x0400109F RID: 4255
		public static int MAFUBA_1 = 36;

		// Token: 0x040010A0 RID: 4256
		public static int MAFUBA_2 = 37;

		// Token: 0x040010A1 RID: 4257
		public static int DESTROY_2 = 38;

		// Token: 0x040010A2 RID: 4258
		public int poolCount;

		// Token: 0x040010A3 RID: 4259
		public static int cout = 1;
	}
}
