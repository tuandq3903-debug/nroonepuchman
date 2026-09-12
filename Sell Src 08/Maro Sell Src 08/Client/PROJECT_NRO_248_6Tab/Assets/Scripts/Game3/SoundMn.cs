using System;
using System.Collections.Generic;

namespace Game3
{
	// Token: 0x0200033A RID: 826
	public class SoundMn
	{
		// Token: 0x06002514 RID: 9492 RVA: 0x00245F9E File Offset: 0x0024419E
		public static SoundMn gI()
		{
			if (SoundMn.gIz == null)
			{
				SoundMn.gIz = new SoundMn();
			}
			return SoundMn.gIz;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00245FB8 File Offset: 0x002441B8
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

		// Token: 0x06002516 RID: 9494 RVA: 0x00246148 File Offset: 0x00244348
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

		// Token: 0x06002517 RID: 9495 RVA: 0x002464F4 File Offset: 0x002446F4
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

		// Token: 0x06002518 RID: 9496 RVA: 0x00246620 File Offset: 0x00244820
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

		// Token: 0x06002519 RID: 9497 RVA: 0x002468E5 File Offset: 0x00244AE5
		public void HP_MPup()
		{
			Sound.playSound(SoundMn.HP_UP, 0.5f);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x002468F8 File Offset: 0x00244AF8
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

		// Token: 0x0600251B RID: 9499 RVA: 0x0024697D File Offset: 0x00244B7D
		public void thaiduonghasan()
		{
			Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0024699C File Offset: 0x00244B9C
		public void rain()
		{
			Sound.playMus(SoundMn.RAIN, 0.3f, true);
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x002469AE File Offset: 0x00244BAE
		public void gongName()
		{
			Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
			this.poolCount++;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x002469CD File Offset: 0x00244BCD
		public void gong()
		{
			Sound.playSound(SoundMn.GONG, 0.2f);
			this.poolCount++;
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x002469EC File Offset: 0x00244BEC
		public void getItem()
		{
			Sound.playSound(SoundMn.GET_ITEM, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00246A0C File Offset: 0x00244C0C
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

		// Token: 0x06002521 RID: 9505 RVA: 0x00246A64 File Offset: 0x00244C64
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

		// Token: 0x06002522 RID: 9506 RVA: 0x00246A98 File Offset: 0x00244C98
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

		// Token: 0x06002523 RID: 9507 RVA: 0x00246AE8 File Offset: 0x00244CE8
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

		// Token: 0x06002524 RID: 9508 RVA: 0x00246B0C File Offset: 0x00244D0C
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

		// Token: 0x06002525 RID: 9509 RVA: 0x00246B5E File Offset: 0x00244D5E
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

		// Token: 0x06002526 RID: 9510 RVA: 0x00246B91 File Offset: 0x00244D91
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

		// Token: 0x06002527 RID: 9511 RVA: 0x000034B9 File Offset: 0x000016B9
		public void update()
		{
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x00246BC4 File Offset: 0x00244DC4
		public void closeSound()
		{
			Sound.stopAll = true;
			this.stopAll();
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x00246BD2 File Offset: 0x00244DD2
		public void bigeExlode()
		{
			Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00246BF1 File Offset: 0x00244DF1
		public void explode_1()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00246BF1 File Offset: 0x00244DF1
		public void explode_2()
		{
			Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00246C10 File Offset: 0x00244E10
		public void traidatKame()
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
			this.poolCount++;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00246C2F File Offset: 0x00244E2F
		public void namekKame()
		{
			Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00246C4E File Offset: 0x00244E4E
		public void nameLazer()
		{
			Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
			this.poolCount++;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00246C6D File Offset: 0x00244E6D
		public void xaydaKame()
		{
			Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
			this.poolCount++;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x00246C8C File Offset: 0x00244E8C
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

		// Token: 0x06002531 RID: 9521 RVA: 0x00246CC4 File Offset: 0x00244EC4
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

		// Token: 0x06002532 RID: 9522 RVA: 0x00246D1E File Offset: 0x00244F1E
		public void monkeyRun(float volumn)
		{
			if (GameCanvas.gameTick % 8 == 0)
			{
				Sound.playSound(SoundMn.KHICHAY, 0.2f);
				this.poolCount++;
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00246D46 File Offset: 0x00244F46
		public void charFall()
		{
			Sound.playSound(SoundMn.MOVE, 0.1f);
			this.poolCount++;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00246D65 File Offset: 0x00244F65
		public void charJump()
		{
			Sound.playSound(SoundMn.MOVE, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00246D84 File Offset: 0x00244F84
		public void panelOpen()
		{
			Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000034B9 File Offset: 0x000016B9
		public void buttonClose()
		{
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00246DA3 File Offset: 0x00244FA3
		public void buttonClick()
		{
			Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00246DC2 File Offset: 0x00244FC2
		public void charFly()
		{
			Sound.playSound(SoundMn.FLY, 0.2f);
			this.poolCount++;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00246DE1 File Offset: 0x00244FE1
		public void openMenu()
		{
			Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00246E00 File Offset: 0x00245000
		public void panelClick()
		{
			Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00246E1F File Offset: 0x0024501F
		public void eatPeans()
		{
			Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00246E3E File Offset: 0x0024503E
		public void openDialog()
		{
			Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x00246E4F File Offset: 0x0024504F
		public void hoisinh()
		{
			Sound.playSound(SoundMn.HOISINH, 0.5f);
			this.poolCount++;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000034B9 File Offset: 0x000016B9
		public void taitaoPause()
		{
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00246E70 File Offset: 0x00245070
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

		// Token: 0x06002540 RID: 9536 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isPlayAirShip()
		{
			return false;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x00246E9C File Offset: 0x0024509C
		public void airShip()
		{
			SoundMn.cout++;
			if (SoundMn.cout % 2 == 0)
			{
				Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
			}
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000034B9 File Offset: 0x000016B9
		public void pauseAirShip()
		{
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000034B9 File Offset: 0x000016B9
		public void resumeAirShip()
		{
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00246EC3 File Offset: 0x002450C3
		public void stopAll()
		{
			Sound.stopAllz();
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00246ECA File Offset: 0x002450CA
		public void backToRegister()
		{
			Session_ME.gI().close();
			GameCanvas.panel.hide();
			GameCanvas.loginScr.actRegister();
			GameCanvas.loginScr.switchToMe();
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00246EF4 File Offset: 0x002450F4
		public void newKame()
		{
			this.poolCount++;
			if (this.poolCount % 15 == 0)
			{
				Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
			}
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00246F1E File Offset: 0x0024511E
		public void radarClick()
		{
			Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x00246F2F File Offset: 0x0024512F
		public void radarItem()
		{
			Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x00246F40 File Offset: 0x00245140
		public static void playSound(int x, int y, int id, float volume)
		{
			Sound.playSound(id, volume);
		}

		// Token: 0x040047F0 RID: 18416
		public static bool IsDelAcc;

		// Token: 0x040047F1 RID: 18417
		public static SoundMn gIz;

		// Token: 0x040047F2 RID: 18418
		public static bool isSound = true;

		// Token: 0x040047F3 RID: 18419
		public static float volume = 0.5f;

		// Token: 0x040047F4 RID: 18420
		private static int MAX_VOLUME = 10;

		// Token: 0x040047F5 RID: 18421
		public static int AIR_SHIP;

		// Token: 0x040047F6 RID: 18422
		public static int RAIN = 1;

		// Token: 0x040047F7 RID: 18423
		public static int TAITAONANGLUONG = 2;

		// Token: 0x040047F8 RID: 18424
		public static int GET_ITEM;

		// Token: 0x040047F9 RID: 18425
		public static int MOVE = 1;

		// Token: 0x040047FA RID: 18426
		public static int LOW_PUNCH = 2;

		// Token: 0x040047FB RID: 18427
		public static int LOW_KICK = 3;

		// Token: 0x040047FC RID: 18428
		public static int FLY = 4;

		// Token: 0x040047FD RID: 18429
		public static int JUMP = 5;

		// Token: 0x040047FE RID: 18430
		public static int PANEL_OPEN = 6;

		// Token: 0x040047FF RID: 18431
		public static int BUTTON_CLOSE = 7;

		// Token: 0x04004800 RID: 18432
		public static int BUTTON_CLICK = 8;

		// Token: 0x04004801 RID: 18433
		public static int MEDIUM_PUNCH = 9;

		// Token: 0x04004802 RID: 18434
		public static int MEDIUM_KICK = 10;

		// Token: 0x04004803 RID: 18435
		public static int PANEL_CLICK = 11;

		// Token: 0x04004804 RID: 18436
		public static int EAT_PEAN = 12;

		// Token: 0x04004805 RID: 18437
		public static int OPEN_DIALOG = 13;

		// Token: 0x04004806 RID: 18438
		public static int NORMAL_KAME = 14;

		// Token: 0x04004807 RID: 18439
		public static int NAMEK_KAME = 15;

		// Token: 0x04004808 RID: 18440
		public static int XAYDA_KAME = 16;

		// Token: 0x04004809 RID: 18441
		public static int EXPLODE_1 = 17;

		// Token: 0x0400480A RID: 18442
		public static int EXPLODE_2 = 18;

		// Token: 0x0400480B RID: 18443
		public static int TRAIDAT_KAME = 19;

		// Token: 0x0400480C RID: 18444
		public static int HP_UP = 20;

		// Token: 0x0400480D RID: 18445
		public static int THAIDUONGHASAN = 21;

		// Token: 0x0400480E RID: 18446
		public static int HOISINH = 22;

		// Token: 0x0400480F RID: 18447
		public static int GONG = 23;

		// Token: 0x04004810 RID: 18448
		public static int KHICHAY = 24;

		// Token: 0x04004811 RID: 18449
		public static int BIG_EXPLODE = 25;

		// Token: 0x04004812 RID: 18450
		public static int NAMEK_LAZER = 26;

		// Token: 0x04004813 RID: 18451
		public static int NAMEK_CHARGE = 27;

		// Token: 0x04004814 RID: 18452
		public static int RADAR_CLICK = 28;

		// Token: 0x04004815 RID: 18453
		public static int RADAR_ITEM = 29;

		// Token: 0x04004816 RID: 18454
		public static int FIREWORK = 30;

		// Token: 0x04004817 RID: 18455
		public static int KAMEX10_0 = 31;

		// Token: 0x04004818 RID: 18456
		public static int KAMEX10_1 = 32;

		// Token: 0x04004819 RID: 18457
		public static int DESTROY_0 = 33;

		// Token: 0x0400481A RID: 18458
		public static int DESTROY_1 = 34;

		// Token: 0x0400481B RID: 18459
		public static int MAFUBA_0 = 35;

		// Token: 0x0400481C RID: 18460
		public static int MAFUBA_1 = 36;

		// Token: 0x0400481D RID: 18461
		public static int MAFUBA_2 = 37;

		// Token: 0x0400481E RID: 18462
		public static int DESTROY_2 = 38;

		// Token: 0x0400481F RID: 18463
		public int poolCount;

		// Token: 0x04004820 RID: 18464
		public static int cout = 1;
	}
}
