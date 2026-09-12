using System;
using System.Threading;
using Game2.Assets.src.g;
using Game2.Mod;
using Game2.Mod.XMAP;

namespace Game2
{
	// Token: 0x020003A0 RID: 928
	public class GameScr : mScreen, IChatable
	{
		// Token: 0x0600293F RID: 10559 RVA: 0x00289FD0 File Offset: 0x002881D0
		public GameScr()
		{
			if (GameCanvas.w == 128 || GameCanvas.h <= 208)
			{
				GameScr.indexSize = 20;
			}
			this.cmdback = new Command(string.Empty, 11021);
			this.cmdMenu = new Command("menu", 11000);
			this.cmdFocus = new Command(string.Empty, 11001);
			this.cmdMenu.img = GameScr.imgMenu;
			this.cmdMenu.w = mGraphics.getImageWidth(this.cmdMenu.img) + 20;
			this.cmdMenu.isPlaySoundButton = false;
			this.cmdFocus.img = GameScr.imgFocus;
			if (GameCanvas.isTouch)
			{
				this.cmdMenu.x = 0;
				this.cmdMenu.y = 50;
				this.cmdFocus = null;
			}
			else
			{
				this.cmdMenu.x = 0;
				this.cmdMenu.y = GameScr.gH - 30;
				this.cmdFocus.x = GameScr.gW - 32;
				this.cmdFocus.y = GameScr.gH - 32;
			}
			this.right = this.cmdFocus;
			GameScr.isPaintRada = 1;
			if (GameCanvas.isTouch)
			{
				GameScr.isHaveSelectSkill = true;
			}
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x0028A198 File Offset: 0x00288398
		public void RemoveAllItem()
		{
			foreach (Item item in Char.myCharz().arrItemBag)
			{
				if (item != null)
				{
					Service.gI().useItem(1, 1, (sbyte)item.indexUI, -1);
					Service.gI().useItem(2, 1, (sbyte)item.indexUI, -1);
					Thread.Sleep(300);
				}
			}
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x0028A1F8 File Offset: 0x002883F8
		public static void loadBg()
		{
			GameScr.fra_PVE_Bar_0 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_0.png"), 6, 15);
			GameScr.fra_PVE_Bar_1 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_1.png"), 38, 21);
			GameScr.imgVS = mSystem.loadImage("/mainImage/i_vs.png");
			GameScr.imgBall = mSystem.loadImage("/mainImage/i_charlife.png");
			GameScr.imgHP_NEW = mSystem.loadImage("/mainImage/i_hp.png");
			GameScr.imgKhung = mSystem.loadImage("/mainImage/i_khung.png");
			GameScr.imgLbtn = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
			GameScr.imgLbtnFocus = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
			GameScr.imgLbtn2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnl2.png");
			GameScr.imgLbtnFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf2.png");
			GameScr.imgPanel = GameCanvas.loadImage("/mainImage/myTexture2dpanel.png");
			GameScr.imgPanel2 = GameCanvas.loadImage("/mainImage/panel2.png");
			GameScr.imgHP = GameCanvas.loadImage("/mainImage/myTexture2dHP.png");
			GameScr.imgSP = GameCanvas.loadImage("/mainImage/SP.png");
			GameScr.imgHPLost = GameCanvas.loadImage("/mainImage/myTexture2dhpLost.png");
			GameScr.imgMPLost = GameCanvas.loadImage("/mainImage/myTexture2dmpLost.png");
			GameScr.imgMP = GameCanvas.loadImage("/mainImage/myTexture2dMP.png");
			GameScr.imgSkill = GameCanvas.loadImage("/mainImage/myTexture2dskill.png");
			GameScr.imgSkill2 = GameCanvas.loadImage("/mainImage/myTexture2dskill2.png");
			GameScr.imgMenu = GameCanvas.loadImage("/mainImage/myTexture2dmenu.png");
			GameScr.imgFocus = GameCanvas.loadImage("/mainImage/myTexture2dfocus.png");
			GameScr.imgHP_tm_do = GameCanvas.loadImage("/mainImage/tm-do.png");
			GameScr.imgHP_tm_vang = GameCanvas.loadImage("/mainImage/tm-vang.png");
			GameScr.imgHP_tm_xam = GameCanvas.loadImage("/mainImage/tm-xam.png");
			GameScr.imgHP_tm_xanh = GameCanvas.loadImage("/mainImage/tm-xanh.png");
			if (GameCanvas.isTouch)
			{
				GameScr.imgArrow = GameCanvas.loadImage("/mainImage/myTexture2darrow.png");
				GameScr.imgArrow2 = GameCanvas.loadImage("/mainImage/myTexture2darrow2.png");
				GameScr.imgChat = GameCanvas.loadImage("/mainImage/myTexture2dchat.png");
				GameScr.imgChat2 = GameCanvas.loadImage("/mainImage/myTexture2dchat2.png");
				GameScr.imgFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dfocus2.png");
				GameScr.imgHP1 = GameCanvas.loadImage("/mainImage/myTexture2dPea0.png");
				GameScr.imgHP2 = GameCanvas.loadImage("/mainImage/myTexture2dPea1.png");
				GameScr.imgAnalog1 = GameCanvas.loadImage("/mainImage/myTexture2danalog1.png");
				GameScr.imgAnalog2 = GameCanvas.loadImage("/mainImage/myTexture2danalog2.png");
				GameScr.imgHP3 = GameCanvas.loadImage("/mainImage/myTexture2dPea2.png");
				GameScr.imgHP4 = GameCanvas.loadImage("/mainImage/myTexture2dPea3.png");
				GameScr.imgFire0 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn0.png");
				GameScr.imgFire1 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn1.png");
				GameScr.imgModFunc = GameCanvas.loadImage("/mainImage/imgModFuc.png");
				GameScr.imgCommandChat = GameCanvas.loadImage("/mainImage/imgCommandChat.png");
				GameScr.imgNapTuan = GameCanvas.loadImage("/mainImage/NapNgay.png");
			}
			GameScr.flyTextX = new int[5];
			GameScr.flyTextY = new int[5];
			GameScr.flyTextDx = new int[5];
			GameScr.flyTextDy = new int[5];
			GameScr.flyTextState = new int[5];
			GameScr.flyTextString = new string[5];
			GameScr.flyTextYTo = new int[5];
			GameScr.flyTime = new int[5];
			GameScr.flyTextColor = new int[8];
			for (int i = 0; i < 5; i++)
			{
				GameScr.flyTextState[i] = -1;
			}
			sbyte[] array = Rms.loadRMS("NRdataVersion");
			sbyte[] array2 = Rms.loadRMS("NRmapVersion");
			sbyte[] array3 = Rms.loadRMS("NRskillVersion");
			sbyte[] array4 = Rms.loadRMS("NRitemVersion");
			if (array != null)
			{
				GameScr.vcData = array[0];
			}
			if (array2 != null)
			{
				GameScr.vcMap = array2[0];
			}
			if (array3 != null)
			{
				GameScr.vcSkill = array3[0];
			}
			if (array4 != null)
			{
				GameScr.vcItem = array4[0];
			}
			GameScr.imgNut = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
			GameScr.imgNutF = GameCanvas.loadImage("/mainImage/myTexture2dnutF.png");
			GameScr.imgCapsule = GameCanvas.loadImage("/mainImage/capsule.png");
			GameScr.imgCapsuleF = GameCanvas.loadImage("/mainImage/capsuleF.png");
			GameScr.imgChangeZone = GameCanvas.loadImage("/mainImage/changeZone.png");
			GameScr.imgChangeZoneF = GameCanvas.loadImage("/mainImage/changeZoneF.png");
			GameScr.imgFusion = GameCanvas.loadImage("/mainImage/fusion.png");
			GameScr.imgFusionF = GameCanvas.loadImage("/mainImage/fusionF.png");
			GameScr.imgNextRight = GameCanvas.loadImage("/mainImage/nextRight.png");
			GameScr.imgNextRightF = GameCanvas.loadImage("/mainImage/nextRightF.png");
			GameScr.imgNextLeft = GameCanvas.loadImage("/mainImage/nextLeft.png");
			GameScr.imgNextLeftF = GameCanvas.loadImage("/mainImage/nextLeftF.png");
			GameScr.imgNextCenter = GameCanvas.loadImage("/mainImage/nextMiddle.png");
			GameScr.imgNextCenterF = GameCanvas.loadImage("/mainImage/nextMiddleF.png");
			MobCapcha.init();
			GameScr.isAnalog = ((Rms.loadRMSInt("analog") != 0) ? (Main.isIPhone ? 1 : 0) : 0);
			GameScr.gamePad = new GamePad();
			GameScr.arrow = GameCanvas.loadImage("/mainImage/myTexture2darrow3.png");
			GameScr.imgTrans = GameCanvas.loadImage("/bg/trans.png");
			GameScr.imgRoomStat = GameCanvas.loadImage("/mainImage/myTexture2dstat.png");
			GameScr.frBarPow0 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor00.png");
			GameScr.frBarPow1 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor01.png");
			GameScr.frBarPow2 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor02.png");
			GameScr.frBarPow20 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor20.png");
			GameScr.frBarPow21 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor21.png");
			GameScr.frBarPow22 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor22.png");
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x0028A6E2 File Offset: 0x002888E2
		public void initSelectChar()
		{
			this.readPart();
			SmallImage.init();
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0028A6F0 File Offset: 0x002888F0
		public static void paintOngMauPercent(Image img0, Image img1, Image img2, float x, float y, int size, float pixelPercent, mGraphics g)
		{
			int clipX = g.getClipX();
			int clipY = g.getClipY();
			int clipWidth = g.getClipWidth();
			int clipHeight = g.getClipHeight();
			g.setClip((int)x, (int)y, (int)pixelPercent, 13);
			int num = size / 15 - 2;
			for (int i = 0; i < num; i++)
			{
				g.drawImage(img1, x + (float)((i + 1) * 15), y, 0);
			}
			g.drawImage(img0, x, y, 0);
			g.drawImage(img1, x + (float)size - 30f, y, 0);
			g.drawImage(img2, x + (float)size - 15f, y, 0);
			g.setClip(clipX, clipY, clipWidth, clipHeight);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0028A7A0 File Offset: 0x002889A0
		public void initTraining()
		{
			if (CreateCharScr.isCreateChar)
			{
				CreateCharScr.isCreateChar = false;
				this.right = null;
			}
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0028A7B6 File Offset: 0x002889B6
		public bool isMapDocNhan()
		{
			return TileMap.mapID >= 53 && TileMap.mapID <= 62;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0028A7CD File Offset: 0x002889CD
		public bool isMapFize()
		{
			return TileMap.mapID >= 63;
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x0028A7DC File Offset: 0x002889DC
		public override void switchToMe()
		{
			if (ModFunc.autoLogin != null)
			{
				ModFunc.autoLogin.waitToNextLogin = false;
			}
			GameScr.vChatVip.removeAllElements();
			ServerListScreen.isWait = false;
			if (BackgroudEffect.isHaveRain())
			{
				SoundMn.gI().rain();
			}
			LoginScr.isContinueToLogin = false;
			Char.isLoadingMap = false;
			if (!GameScr.isPaintOther)
			{
				Service.gI().finishLoadMap();
			}
			if (TileMap.isTrainingMap())
			{
				this.initTraining();
			}
			GameScr.info1.isUpdate = true;
			GameScr.info2.isUpdate = true;
			this.resetButton();
			GameScr.isLoadAllData = true;
			GameScr.isPaintOther = false;
			base.switchToMe();
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0028A874 File Offset: 0x00288A74
		public static void resetAllvector()
		{
			GameScr.vCharInMap.removeAllElements();
			Teleport.vTeleport.removeAllElements();
			GameScr.vItemMap.removeAllElements();
			Effect2.vEffect2.removeAllElements();
			Effect2.vAnimateEffect.removeAllElements();
			Effect2.vEffect2Outside.removeAllElements();
			Effect2.vEffectFeet.removeAllElements();
			Effect2.vEffect3.removeAllElements();
			GameScr.vMobAttack.removeAllElements();
			GameScr.vMob.removeAllElements();
			GameScr.vNpc.removeAllElements();
			Char.myCharz().vMovePoints.removeAllElements();
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000034B9 File Offset: 0x000016B9
		public void loadSkillShortcut()
		{
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x0028A900 File Offset: 0x00288B00
		public void onOSkill(sbyte[] oSkillID)
		{
			Cout.println("GET onScreenSkill!");
			GameScr.onScreenSkill = new Skill[10];
			if (oSkillID == null)
			{
				this.loadDefaultonScreenSkill();
				return;
			}
			for (int i = 0; i < oSkillID.Length; i++)
			{
				for (int j = 0; j < Char.myCharz().vSkillFight.size(); j++)
				{
					Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(j);
					if (skill.template.id == oSkillID[i])
					{
						GameScr.onScreenSkill[i] = skill;
						break;
					}
				}
			}
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x0028A988 File Offset: 0x00288B88
		public void onKSkill(sbyte[] kSkillID)
		{
			Cout.println("GET KEYSKILL!");
			GameScr.keySkill = new Skill[10];
			if (kSkillID == null)
			{
				this.loadDefaultKeySkill();
				return;
			}
			for (int i = 0; i < kSkillID.Length; i++)
			{
				for (int j = 0; j < Char.myCharz().vSkillFight.size(); j++)
				{
					Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(j);
					if (skill.template.id == kSkillID[i])
					{
						GameScr.keySkill[i] = skill;
						break;
					}
				}
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x0028AA10 File Offset: 0x00288C10
		public void onCSkill(sbyte[] cSkillID)
		{
			Cout.println("GET CURRENTSKILL!");
			if (cSkillID == null || cSkillID.Length == 0)
			{
				if (Char.myCharz().vSkillFight.size() > 0)
				{
					Char.myCharz().myskill = (Skill)Char.myCharz().vSkillFight.elementAt(0);
				}
			}
			else
			{
				for (int i = 0; i < Char.myCharz().vSkillFight.size(); i++)
				{
					Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(i);
					if (skill.template.id == cSkillID[0])
					{
						Char.myCharz().myskill = skill;
						break;
					}
				}
			}
			if (Char.myCharz().myskill != null)
			{
				Service.gI().selectSkill((int)Char.myCharz().myskill.template.id);
				this.saveRMSCurrentSkill(Char.myCharz().myskill.template.id);
			}
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x0028AAF4 File Offset: 0x00288CF4
		private void loadDefaultonScreenSkill()
		{
			Cout.println("LOAD DEFAULT ONmScreen SKILL");
			int i = 0;
			while (i < GameScr.onScreenSkill.Length && i < Char.myCharz().vSkillFight.size())
			{
				Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(i);
				GameScr.onScreenSkill[i] = skill;
				i++;
			}
			this.saveonScreenSkillToRMS();
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x0028AB54 File Offset: 0x00288D54
		private void loadDefaultKeySkill()
		{
			Cout.println("LOAD DEFAULT KEY SKILL");
			int i = 0;
			while (i < GameScr.keySkill.Length && i < Char.myCharz().vSkillFight.size())
			{
				Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(i);
				GameScr.keySkill[i] = skill;
				i++;
			}
			this.saveKeySkillToRMS();
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x0028ABB4 File Offset: 0x00288DB4
		public void doSetOnScreenSkill(SkillTemplate skillTemplate)
		{
			Skill skill = Char.myCharz().getSkill(skillTemplate);
			MyVector myVector = new MyVector();
			for (int i = 0; i < 10; i++)
			{
				object p = new object[]
				{
					skill,
					i.ToString() + string.Empty
				};
				Command command = new Command(mResources.into_place + (i + 1).ToString(), 11120, p);
				if (GameScr.onScreenSkill[i] != null)
				{
					command.isDisplay = true;
				}
				myVector.addElement(command);
			}
			GameCanvas.menu.startAt(myVector, 0);
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x0028AC48 File Offset: 0x00288E48
		public void doSetKeySkill(SkillTemplate skillTemplate)
		{
			Skill skill = Char.myCharz().getSkill(skillTemplate);
			string[] array = (!TField.isQwerty) ? mResources.key_skill : mResources.key_skill_qwerty;
			MyVector myVector = new MyVector();
			for (int i = 0; i < 10; i++)
			{
				MyVector myVector2 = myVector;
				object p = new object[]
				{
					skill,
					i.ToString() + string.Empty
				};
				myVector2.addElement(new Command(array[i], 11121, p));
			}
			GameCanvas.menu.startAt(myVector, 0);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x0028ACCC File Offset: 0x00288ECC
		public void saveonScreenSkillToRMS()
		{
			sbyte[] array = new sbyte[GameScr.onScreenSkill.Length];
			for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
			{
				if (GameScr.onScreenSkill[i] == null)
				{
					array[i] = -1;
				}
				else
				{
					array[i] = GameScr.onScreenSkill[i].template.id;
				}
			}
			Service.gI().changeOnKeyScr(array);
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x0028AD28 File Offset: 0x00288F28
		public void saveKeySkillToRMS()
		{
			sbyte[] array = new sbyte[GameScr.keySkill.Length];
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				if (GameScr.keySkill[i] == null)
				{
					array[i] = -1;
				}
				else
				{
					array[i] = GameScr.keySkill[i].template.id;
				}
			}
			Service.gI().changeOnKeyScr(array);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000034B9 File Offset: 0x000016B9
		public void saveRMSCurrentSkill(sbyte id)
		{
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x0028AD84 File Offset: 0x00288F84
		public bool isBagFull()
		{
			for (int num = Char.myCharz().arrItemBag.Length - 1; num >= 0; num--)
			{
				if (Char.myCharz().arrItemBag[num] == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x0028ADBC File Offset: 0x00288FBC
		public void createMenu(string[] menu, Npc npc)
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < menu.Length; i++)
			{
				myVector.addElement(new Command(menu[i], 11057, npc));
			}
			GameCanvas.menu.startAt(myVector, 2);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x0028AE00 File Offset: 0x00289000
		public void readPart()
		{
			DataInputStream dataInputStream = null;
			try
			{
				dataInputStream = new DataInputStream(Rms.loadRMS("NR_part"));
				int partSize = (int)dataInputStream.readShort();
				GameScr.parts = new Part[partSize];
				for (int i = 0; i < partSize; i++)
				{
					int type = (int)dataInputStream.readByte();
					GameScr.parts[i] = new Part(type);
					for (int j = 0; j < GameScr.parts[i].pi.Length; j++)
					{
						GameScr.parts[i].pi[j] = new PartImage
						{
							id = dataInputStream.readShort(),
							dx = dataInputStream.readByte(),
							dy = dataInputStream.readByte()
						};
					}
				}
			}
			catch (Exception ex)
			{
				Cout.LogError("LOI TAI readPart " + ex.ToString());
			}
			finally
			{
				try
				{
					dataInputStream.close();
				}
				catch (Exception ex2)
				{
					Res.outz2("LOI TAI readPart 2" + ex2.StackTrace);
				}
			}
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x0028AF10 File Offset: 0x00289110
		public void readEfect()
		{
			DataInputStream dataInputStream = null;
			try
			{
				dataInputStream = new DataInputStream(Rms.loadRMS("NR_effect"));
				int num = (int)dataInputStream.readShort();
				GameScr.efs = new EffectCharPaint[num];
				for (int i = 0; i < num; i++)
				{
					GameScr.efs[i] = new EffectCharPaint();
					GameScr.efs[i].idEf = (int)dataInputStream.readShort();
					GameScr.efs[i].arrEfInfo = new EffectInfoPaint[(int)dataInputStream.readByte()];
					for (int j = 0; j < GameScr.efs[i].arrEfInfo.Length; j++)
					{
						GameScr.efs[i].arrEfInfo[j] = new EffectInfoPaint();
						GameScr.efs[i].arrEfInfo[j].idImg = (int)dataInputStream.readShort();
						GameScr.efs[i].arrEfInfo[j].dx = (int)dataInputStream.readByte();
						GameScr.efs[i].arrEfInfo[j].dy = (int)dataInputStream.readByte();
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					dataInputStream.close();
				}
				catch (Exception ex2)
				{
					Cout.LogError("Loi ham Eff: " + ex2.ToString());
				}
			}
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x0028B050 File Offset: 0x00289250
		public void readArrow()
		{
			DataInputStream dataInputStream = null;
			try
			{
				dataInputStream = new DataInputStream(Rms.loadRMS("NR_arrow"));
				int num = (int)dataInputStream.readShort();
				GameScr.arrs = new Arrowpaint[num];
				for (int i = 0; i < num; i++)
				{
					GameScr.arrs[i] = new Arrowpaint();
					GameScr.arrs[i].id = (int)dataInputStream.readShort();
					GameScr.arrs[i].imgId[0] = (int)dataInputStream.readShort();
					GameScr.arrs[i].imgId[1] = (int)dataInputStream.readShort();
					GameScr.arrs[i].imgId[2] = (int)dataInputStream.readShort();
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					dataInputStream.close();
				}
				catch (Exception ex2)
				{
					Cout.LogError("Loi ham readArrow: " + ex2.ToString());
				}
			}
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x0028B138 File Offset: 0x00289338
		public void readDart()
		{
			DataInputStream dataInputStream = null;
			try
			{
				dataInputStream = new DataInputStream(Rms.loadRMS("NR_dart"));
				int num = (int)dataInputStream.readShort();
				GameScr.darts = new DartInfo[num];
				for (int i = 0; i < num; i++)
				{
					GameScr.darts[i] = new DartInfo();
					GameScr.darts[i].id = dataInputStream.readShort();
					GameScr.darts[i].nUpdate = dataInputStream.readShort();
					GameScr.darts[i].va = (int)(dataInputStream.readShort() * 256);
					GameScr.darts[i].xdPercent = dataInputStream.readShort();
					int num2 = (int)dataInputStream.readShort();
					GameScr.darts[i].tail = new short[num2];
					for (int j = 0; j < num2; j++)
					{
						GameScr.darts[i].tail[j] = dataInputStream.readShort();
					}
					num2 = (int)dataInputStream.readShort();
					GameScr.darts[i].tailBorder = new short[num2];
					for (int k = 0; k < num2; k++)
					{
						GameScr.darts[i].tailBorder[k] = dataInputStream.readShort();
					}
					num2 = (int)dataInputStream.readShort();
					GameScr.darts[i].xd1 = new short[num2];
					for (int l = 0; l < num2; l++)
					{
						GameScr.darts[i].xd1[l] = dataInputStream.readShort();
					}
					num2 = (int)dataInputStream.readShort();
					GameScr.darts[i].xd2 = new short[num2];
					for (int m = 0; m < num2; m++)
					{
						GameScr.darts[i].xd2[m] = dataInputStream.readShort();
					}
					num2 = (int)dataInputStream.readShort();
					GameScr.darts[i].head = new short[num2][];
					for (int n = 0; n < num2; n++)
					{
						short num3 = dataInputStream.readShort();
						GameScr.darts[i].head[n] = new short[(int)num3];
						for (int num4 = 0; num4 < (int)num3; num4++)
						{
							GameScr.darts[i].head[n][num4] = dataInputStream.readShort();
						}
					}
					num2 = (int)dataInputStream.readShort();
					GameScr.darts[i].headBorder = new short[num2][];
					for (int num5 = 0; num5 < num2; num5++)
					{
						short num6 = dataInputStream.readShort();
						GameScr.darts[i].headBorder[num5] = new short[(int)num6];
						for (int num7 = 0; num7 < (int)num6; num7++)
						{
							GameScr.darts[i].headBorder[num5][num7] = dataInputStream.readShort();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham ReadDart: " + ex.ToString());
			}
			finally
			{
				try
				{
					dataInputStream.close();
				}
				catch (Exception ex2)
				{
					Cout.LogError("Loi ham reaaDart: " + ex2.ToString());
				}
			}
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x0028B43C File Offset: 0x0028963C
		public void readSkill()
		{
			DataInputStream dataInputStream = null;
			try
			{
				dataInputStream = new DataInputStream(Rms.loadRMS("NR_skill"));
				int dataSkillSz = (int)dataInputStream.readShort();
				GameScr.sks = new SkillPaint[Skills.skills.size()];
				for (int i = 0; i < dataSkillSz; i++)
				{
					short levelId = dataInputStream.readShort();
					if (levelId == 1111)
					{
						levelId = (short)(dataSkillSz - 1);
					}
					GameScr.sks[(int)levelId] = new SkillPaint
					{
						id = (int)levelId,
						effectHappenOnMob = (int)dataInputStream.readShort()
					};
					if (GameScr.sks[(int)levelId].effectHappenOnMob <= 0)
					{
						GameScr.sks[(int)levelId].effectHappenOnMob = 80;
					}
					GameScr.sks[(int)levelId].numEff = (int)dataInputStream.readByte();
					GameScr.sks[(int)levelId].skillStand = new SkillInfoPaint[(int)dataInputStream.readByte()];
					for (int j = 0; j < GameScr.sks[(int)levelId].skillStand.Length; j++)
					{
						GameScr.sks[(int)levelId].skillStand[j] = new SkillInfoPaint
						{
							status = (int)dataInputStream.readByte(),
							effS0Id = (int)dataInputStream.readShort(),
							e0dx = (int)dataInputStream.readShort(),
							e0dy = (int)dataInputStream.readShort(),
							effS1Id = (int)dataInputStream.readShort(),
							e1dx = (int)dataInputStream.readShort(),
							e1dy = (int)dataInputStream.readShort(),
							effS2Id = (int)dataInputStream.readShort(),
							e2dx = (int)dataInputStream.readShort(),
							e2dy = (int)dataInputStream.readShort(),
							arrowId = (int)dataInputStream.readShort(),
							adx = (int)dataInputStream.readShort(),
							ady = (int)dataInputStream.readShort()
						};
					}
					GameScr.sks[(int)levelId].skillfly = new SkillInfoPaint[(int)dataInputStream.readByte()];
					for (int k = 0; k < GameScr.sks[(int)levelId].skillfly.Length; k++)
					{
						GameScr.sks[(int)levelId].skillfly[k] = new SkillInfoPaint
						{
							status = (int)dataInputStream.readByte(),
							effS0Id = (int)dataInputStream.readShort(),
							e0dx = (int)dataInputStream.readShort(),
							e0dy = (int)dataInputStream.readShort(),
							effS1Id = (int)dataInputStream.readShort(),
							e1dx = (int)dataInputStream.readShort(),
							e1dy = (int)dataInputStream.readShort(),
							effS2Id = (int)dataInputStream.readShort(),
							e2dx = (int)dataInputStream.readShort(),
							e2dy = (int)dataInputStream.readShort(),
							arrowId = (int)dataInputStream.readShort(),
							adx = (int)dataInputStream.readShort(),
							ady = (int)dataInputStream.readShort()
						};
					}
				}
			}
			catch (Exception ex)
			{
				ModFunc.Log("Loi ham readSkill: " + ex.ToString());
			}
			finally
			{
				try
				{
					dataInputStream.close();
				}
				catch (Exception ex2)
				{
					ModFunc.Log("Loi ham readskill 1: " + ex2.ToString());
				}
			}
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x0028B74C File Offset: 0x0028994C
		public static GameScr gI()
		{
			if (GameScr.instance == null)
			{
				GameScr.instance = new GameScr();
			}
			return GameScr.instance;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0028B764 File Offset: 0x00289964
		public static void clearGameScr()
		{
			GameScr.instance = null;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x0028B76C File Offset: 0x0028996C
		public void loadGameScr()
		{
			GameScr.loadSplash();
			Res.init();
			this.loadInforBar();
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x0028B780 File Offset: 0x00289980
		public static void loadCamera(bool fullmScreen, int cx, int cy)
		{
			GameScr.gW = GameCanvas.w;
			GameScr.cmdBarH = 39;
			GameScr.gH = GameCanvas.h;
			GameScr.cmdBarW = GameScr.gW;
			GameScr.cmdBarX = 0;
			GameScr.cmdBarY = GameCanvas.h - Paint.hTab - GameScr.cmdBarH;
			GameScr.girlHPBarY = 0;
			GameScr.csPadMaxH = GameCanvas.h / 6;
			if (GameScr.csPadMaxH < 48)
			{
				GameScr.csPadMaxH = 48;
			}
			GameScr.gW2 = GameScr.gW >> 1;
			GameScr.gH2 = GameScr.gH >> 1;
			GameScr.gW3 = GameScr.gW / 3;
			GameScr.gH3 = GameScr.gH / 3;
			GameScr.gW23 = GameScr.gH - 120;
			GameScr.gH23 = GameScr.gH * 2 / 3;
			GameScr.gW34 = 3 * GameScr.gW / 4;
			GameScr.gH34 = 3 * GameScr.gH / 4;
			GameScr.gW6 = GameScr.gW / 6;
			GameScr.gH6 = GameScr.gH / 6;
			GameScr.gssw = GameScr.gW / (int)TileMap.size + 2;
			GameScr.gssh = GameScr.gH / (int)TileMap.size + 2;
			if (GameScr.gW % 24 != 0)
			{
				GameScr.gssw++;
			}
			GameScr.cmxLim = (TileMap.tmw - 1) * (int)TileMap.size - GameScr.gW;
			GameScr.cmyLim = (TileMap.tmh - 1) * (int)TileMap.size - GameScr.gH;
			if (cx == -1 && cy == -1)
			{
				GameScr.cmx = (GameScr.cmtoX = Char.myCharz().cx - GameScr.gW2 + GameScr.gW6 * Char.myCharz().cdir);
				GameScr.cmy = (GameScr.cmtoY = Char.myCharz().cy - GameScr.gH23);
			}
			else
			{
				GameScr.cmx = (GameScr.cmtoX = cx - GameScr.gW23 + GameScr.gW6 * Char.myCharz().cdir);
				GameScr.cmy = (GameScr.cmtoY = cy - GameScr.gH23);
			}
			GameScr.firstY = GameScr.cmy;
			if (GameScr.cmx < 24)
			{
				GameScr.cmx = (GameScr.cmtoX = 24);
			}
			if (GameScr.cmx > GameScr.cmxLim)
			{
				GameScr.cmx = (GameScr.cmtoX = GameScr.cmxLim);
			}
			if (GameScr.cmy < 0)
			{
				GameScr.cmy = (GameScr.cmtoY = 0);
			}
			if (GameScr.cmy > GameScr.cmyLim)
			{
				GameScr.cmy = (GameScr.cmtoY = GameScr.cmyLim);
			}
			GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
			if (GameScr.gssx < 0)
			{
				GameScr.gssx = 0;
			}
			GameScr.gssy = GameScr.cmy / (int)TileMap.size;
			GameScr.gssxe = GameScr.gssx + GameScr.gssw;
			GameScr.gssye = GameScr.gssy + GameScr.gssh;
			if (GameScr.gssy < 0)
			{
				GameScr.gssy = 0;
			}
			if (GameScr.gssye > TileMap.tmh - 1)
			{
				GameScr.gssye = TileMap.tmh - 1;
			}
			TileMap.countx = (GameScr.gssxe - GameScr.gssx) * 4;
			if (TileMap.countx > TileMap.tmw)
			{
				TileMap.countx = TileMap.tmw;
			}
			TileMap.county = (GameScr.gssye - GameScr.gssy) * 4;
			if (TileMap.county > TileMap.tmh)
			{
				TileMap.county = TileMap.tmh;
			}
			TileMap.gssx = (Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
			if (TileMap.gssx < 0)
			{
				TileMap.gssx = 0;
			}
			TileMap.gssxe = TileMap.gssx + TileMap.countx;
			if (TileMap.gssxe > TileMap.tmw)
			{
				TileMap.gssxe = TileMap.tmw;
			}
			TileMap.gssy = (Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
			if (TileMap.gssy < 0)
			{
				TileMap.gssy = 0;
			}
			TileMap.gssye = TileMap.gssy + TileMap.county;
			if (TileMap.gssye > TileMap.tmh)
			{
				TileMap.gssye = TileMap.tmh;
			}
			ChatTextField.gI().parentScreen = GameScr.instance;
			ChatTextField.gI().tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			ChatTextField.gI().initChatTextField();
			if (GameCanvas.isTouch)
			{
				GameScr.yTouchBar = GameScr.gH - 88;
				GameScr.xC = GameScr.gW - 40;
				GameScr.yC = 2;
				if (GameCanvas.w <= 240)
				{
					GameScr.xC = GameScr.gW - 35;
					GameScr.yC = 5;
				}
				GameScr.xF = GameScr.gW - 55;
				GameScr.yF = GameScr.yTouchBar + 35;
				GameScr.xTG = GameScr.gW - 37;
				GameScr.yTG = GameScr.yTouchBar - 1;
				if (GameCanvas.w >= 450)
				{
					GameScr.yTG -= 12;
					GameScr.yHP -= 7;
					GameScr.xF -= 10;
					GameScr.yF -= 5;
					GameScr.xTG -= 10;
				}
			}
			GameScr.setSkillBarPosition();
			GameScr.disXC = ((GameCanvas.w <= 200) ? 30 : 40);
			if (Rms.loadRMSInt("viewchat") == -1)
			{
				GameCanvas.panel.isViewChatServer = true;
				return;
			}
			GameCanvas.panel.isViewChatServer = (Rms.loadRMSInt("viewchat") == 1);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x0028BC88 File Offset: 0x00289E88
		public static void setSkillBarPosition()
		{
			Skill[] array = (!GameCanvas.isTouch) ? GameScr.keySkill : GameScr.onScreenSkill;
			GameScr.xS = new int[array.Length];
			GameScr.yS = new int[array.Length];
			if (GameCanvas.isTouchControlSmallScreen && GameScr.isUseTouch)
			{
				GameScr.xSkill = 23;
				GameScr.ySkill = 52;
				GameScr.padSkill = 5;
				for (int i = 0; i < GameScr.xS.Length; i++)
				{
					GameScr.xS[i] = i * (25 + GameScr.padSkill);
					GameScr.yS[i] = GameScr.ySkill;
					if (GameScr.xS.Length > 5 && i >= GameScr.xS.Length / 2)
					{
						GameScr.xS[i] = (i - GameScr.xS.Length / 2) * (25 + GameScr.padSkill);
						GameScr.yS[i] = GameScr.ySkill - 32;
					}
				}
				GameScr.xHP = array.Length * (25 + GameScr.padSkill);
				GameScr.yHP = GameScr.ySkill;
			}
			else
			{
				GameScr.wSkill = 30;
				if (GameCanvas.w <= 320)
				{
					GameScr.ySkill = GameScr.gH - GameScr.wSkill - 6;
					GameScr.xSkill = GameScr.gW2 - array.Length * GameScr.wSkill / 2 - 25;
				}
				else
				{
					GameScr.wSkill = 40;
					GameScr.xSkill = 10;
					GameScr.ySkill = GameCanvas.h - GameScr.wSkill + 7;
				}
				for (int j = 0; j < GameScr.xS.Length; j++)
				{
					GameScr.xS[j] = j * GameScr.wSkill;
					GameScr.yS[j] = GameScr.ySkill;
					if (GameScr.xS.Length > 5 && j >= GameScr.xS.Length / 2)
					{
						GameScr.xS[j] = (j - GameScr.xS.Length / 2) * GameScr.wSkill;
						GameScr.yS[j] = GameScr.ySkill - 32;
					}
				}
				GameScr.xHP = array.Length * GameScr.wSkill;
				GameScr.yHP = GameScr.ySkill;
			}
			if (!GameCanvas.isTouch)
			{
				return;
			}
			GameScr.xSkill = 17;
			GameScr.ySkill = GameCanvas.h - 40;
			if (GameScr.gamePad.isSmallGamePad && GameScr.isAnalog == 1)
			{
				GameScr.xHP = array.Length * GameScr.wSkill;
				GameScr.yHP = GameScr.ySkill;
			}
			else
			{
				GameScr.xHP = GameCanvas.w - 45;
				GameScr.yHP = GameCanvas.h - 45;
			}
			GameScr.setTouchBtn();
			for (int k = 0; k < GameScr.xS.Length; k++)
			{
				GameScr.xS[k] = k * GameScr.wSkill;
				GameScr.yS[k] = GameScr.ySkill;
				if (GameScr.xS.Length > 5 && k >= GameScr.xS.Length / 2)
				{
					GameScr.xS[k] = (k - GameScr.xS.Length / 2) * GameScr.wSkill;
					GameScr.yS[k] = GameScr.ySkill - 32;
				}
			}
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x0028BF30 File Offset: 0x0028A130
		private static void updateCamera()
		{
			if (GameScr.isPaintOther)
			{
				return;
			}
			if (GameScr.cmx != GameScr.cmtoX || GameScr.cmy != GameScr.cmtoY)
			{
				GameScr.cmvx = GameScr.cmtoX - GameScr.cmx << 2;
				GameScr.cmvy = GameScr.cmtoY - GameScr.cmy << 2;
				GameScr.cmdx += GameScr.cmvx;
				GameScr.cmx += GameScr.cmdx >> 4;
				GameScr.cmdx &= 15;
				GameScr.cmdy += GameScr.cmvy;
				GameScr.cmy += GameScr.cmdy >> 4;
				GameScr.cmdy &= 15;
				if (GameScr.cmx < 24)
				{
					GameScr.cmx = 24;
				}
				if (GameScr.cmx > GameScr.cmxLim)
				{
					GameScr.cmx = GameScr.cmxLim;
				}
				if (GameScr.cmy < 0)
				{
					GameScr.cmy = 0;
				}
				if (GameScr.cmy > GameScr.cmyLim)
				{
					GameScr.cmy = GameScr.cmyLim;
				}
			}
			GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
			if (GameScr.gssx < 0)
			{
				GameScr.gssx = 0;
			}
			GameScr.gssy = GameScr.cmy / (int)TileMap.size;
			GameScr.gssxe = GameScr.gssx + GameScr.gssw;
			GameScr.gssye = GameScr.gssy + GameScr.gssh;
			if (GameScr.gssy < 0)
			{
				GameScr.gssy = 0;
			}
			if (GameScr.gssye > TileMap.tmh - 1)
			{
				GameScr.gssye = TileMap.tmh - 1;
			}
			TileMap.gssx = (Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
			if (TileMap.gssx < 0)
			{
				TileMap.gssx = 0;
			}
			TileMap.gssxe = TileMap.gssx + TileMap.countx;
			if (TileMap.gssxe > TileMap.tmw)
			{
				TileMap.gssxe = TileMap.tmw;
				TileMap.gssx = TileMap.gssxe - TileMap.countx;
			}
			TileMap.gssy = (Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
			if (TileMap.gssy < 0)
			{
				TileMap.gssy = 0;
			}
			TileMap.gssye = TileMap.gssy + TileMap.county;
			if (TileMap.gssye > TileMap.tmh)
			{
				TileMap.gssye = TileMap.tmh;
				TileMap.gssy = TileMap.gssye - TileMap.county;
			}
			GameScr.scrMain.updatecm();
			GameScr.scrInfo.updatecm();
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x0028C17C File Offset: 0x0028A37C
		public void clanInvite(string strInvite, int clanID, int code)
		{
			ClanObject clanObject = new ClanObject();
			clanObject.code = code;
			clanObject.clanID = clanID;
			this.startYesNoPopUp(strInvite, new Command(mResources.YES, 12002, clanObject), new Command(mResources.NO, 12003, clanObject));
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0028C1C4 File Offset: 0x0028A3C4
		public void playerMenu(Char c)
		{
			this.auto = 0;
			GameCanvas.clearKeyHold();
			if (Char.myCharz().charFocus.charID < 0 || Char.myCharz().charID < 0)
			{
				return;
			}
			MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
			if (vPlayerMenu.size() > 0)
			{
				return;
			}
			if (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId > 1)
			{
				vPlayerMenu.addElement(new Command(mResources.make_friend, 11112, Char.myCharz().charFocus));
				vPlayerMenu.addElement(new Command(mResources.trade, 11113, Char.myCharz().charFocus));
			}
			if (Char.myCharz().clan != null && Char.myCharz().role < 2 && Char.myCharz().charFocus.clanID == -1)
			{
				vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[4], 110391));
			}
			if (Char.myCharz().charFocus.statusMe != 14 && Char.myCharz().charFocus.statusMe != 5)
			{
				if (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId >= 14)
				{
					vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[0], 2003));
				}
			}
			else
			{
				int type = Char.myCharz().myskill.template.type;
			}
			if (Char.myCharz().clan != null && Char.myCharz().clan.ID == Char.myCharz().charFocus.clanID && Char.myCharz().charFocus.statusMe != 14 && Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId >= 14)
			{
				vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[1], 2004));
			}
			int num = Char.myCharz().nClass.skillTemplates.Length;
			for (int i = 0; i < num; i++)
			{
				SkillTemplate skillTemplate = Char.myCharz().nClass.skillTemplates[i];
				Skill skill = Char.myCharz().getSkill(skillTemplate);
				if (skill != null && skillTemplate.isBuffToPlayer() && skill.point >= 1)
				{
					vPlayerMenu.addElement(new Command(skillTemplate.name, 12004, skill));
				}
			}
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x0028C404 File Offset: 0x0028A604
		public bool isAttack()
		{
			if (this.checkClickToBotton(Char.myCharz().charFocus))
			{
				return false;
			}
			if (this.checkClickToBotton(Char.myCharz().mobFocus))
			{
				return false;
			}
			if (this.checkClickToBotton(Char.myCharz().npcFocus))
			{
				return false;
			}
			if (ChatTextField.gI().isShow)
			{
				return false;
			}
			if (InfoDlg.isLock || Char.myCharz().isLockAttack || Char.isLockKey)
			{
				return false;
			}
			if (Char.myCharz().myskill != null && Char.myCharz().myskill.template.id == 6 && Char.myCharz().itemFocus != null)
			{
				this.pickItem();
				return false;
			}
			if (Char.myCharz().myskill != null && Char.myCharz().myskill.template.type == 2 && Char.myCharz().npcFocus == null && Char.myCharz().myskill.template.id != 6)
			{
				return this.checkSkillValid();
			}
			if (Char.myCharz().skillPaint != null || (Char.myCharz().mobFocus == null && Char.myCharz().npcFocus == null && Char.myCharz().charFocus == null && Char.myCharz().itemFocus == null))
			{
				return false;
			}
			if (Char.myCharz().mobFocus != null)
			{
				if (Char.myCharz().mobFocus.isBigBoss() && Char.myCharz().mobFocus.status == 4)
				{
					Char.myCharz().mobFocus = null;
					Char.myCharz().currentMovePoint = null;
				}
				GameScr.isAutoPlay = true;
				if (!this.isMeCanAttackMob(Char.myCharz().mobFocus))
				{
					return false;
				}
				if (this.mobCapcha != null)
				{
					return false;
				}
				if (Char.myCharz().myskill == null)
				{
					return false;
				}
				if (Char.myCharz().isSelectingSkillUseAlone())
				{
					return false;
				}
				int num = -1;
				int num2 = Res.abs(Char.myCharz().cx - GameScr.cmx) * mGraphics.zoomLevel;
				if (Char.myCharz().charFocus != null)
				{
					num = Res.abs(Char.myCharz().cx - Char.myCharz().charFocus.cx) * mGraphics.zoomLevel;
				}
				else if (Char.myCharz().mobFocus != null)
				{
					num = Res.abs(Char.myCharz().cx - Char.myCharz().mobFocus.x) * mGraphics.zoomLevel;
				}
				if (Char.myCharz().mobFocus.status == 1 || Char.myCharz().mobFocus.status == 0 || Char.myCharz().myskill.template.type == 4 || num == -1 || num > num2)
				{
					if (Char.myCharz().myskill.template.type == 4)
					{
						if (Char.myCharz().mobFocus.x < Char.myCharz().cx)
						{
							Char.myCharz().cdir = -1;
						}
						else
						{
							Char.myCharz().cdir = 1;
						}
						this.doSelectSkill(Char.myCharz().myskill, true);
					}
					return false;
				}
				if (!this.checkSkillValid())
				{
					return false;
				}
				if (Char.myCharz().cx < Char.myCharz().mobFocus.getX())
				{
					Char.myCharz().cdir = 1;
				}
				else
				{
					Char.myCharz().cdir = -1;
				}
				int num3 = Math.abs(Char.myCharz().cx - Char.myCharz().mobFocus.getX());
				int num4 = Math.abs(Char.myCharz().cy - Char.myCharz().mobFocus.getY());
				Char.myCharz().cvx = 0;
				if (num3 > Char.myCharz().myskill.dx || num4 > Char.myCharz().myskill.dy)
				{
					bool flag3 = false;
					if (Char.myCharz().mobFocus is BigBoss || Char.myCharz().mobFocus is BigBoss2)
					{
						flag3 = true;
					}
					int num5 = (Char.myCharz().myskill.dx - ((!flag3) ? 20 : 50)) * ((Char.myCharz().cx > Char.myCharz().mobFocus.getX()) ? 1 : -1);
					if (num3 <= Char.myCharz().myskill.dx)
					{
						num5 = 0;
					}
					Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().mobFocus.getX() + num5, Char.myCharz().mobFocus.getY());
					Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return false;
				}
				if (Char.myCharz().myskill.template.id == 20)
				{
					return true;
				}
				if (num4 > num3 && Res.abs(Char.myCharz().cy - Char.myCharz().mobFocus.getY()) > 30 && Char.myCharz().mobFocus.getTemplate().type == 4)
				{
					Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().cx + Char.myCharz().cdir, Char.myCharz().mobFocus.getY());
					Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return false;
				}
				int num6 = 20;
				bool flag4 = false;
				if (Char.myCharz().mobFocus is BigBoss || Char.myCharz().mobFocus is BigBoss2)
				{
					flag4 = true;
				}
				if (Char.myCharz().myskill.dx > 100)
				{
					num6 = 60;
					if (num3 < 20)
					{
						Char.myCharz().createShadow(Char.myCharz().cx, Char.myCharz().cy, 10);
					}
				}
				bool flag5 = false;
				if ((TileMap.tileTypeAtPixel(Char.myCharz().cx, Char.myCharz().cy + 3) & 2) == 2)
				{
					int num7 = (Char.myCharz().cx > Char.myCharz().mobFocus.getX()) ? 1 : -1;
					if ((TileMap.tileTypeAtPixel(Char.myCharz().mobFocus.getX() + num6 * num7, Char.myCharz().cy + 3) & 2) != 2)
					{
						flag5 = true;
					}
				}
				if (num3 <= num6 && !flag5)
				{
					if (Char.myCharz().cx > Char.myCharz().mobFocus.getX())
					{
						Char.myCharz().cx = Char.myCharz().mobFocus.getX() + num6 + (flag4 ? 30 : 0);
						Char.myCharz().cdir = -1;
					}
					else
					{
						Char.myCharz().cx = Char.myCharz().mobFocus.getX() - num6 - (flag4 ? 30 : 0);
						Char.myCharz().cdir = 1;
					}
					Service.gI().charMove();
				}
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return true;
			}
			else if (Char.myCharz().npcFocus != null)
			{
				if (Char.myCharz().npcFocus.isHide)
				{
					return false;
				}
				if (Char.myCharz().cx < Char.myCharz().npcFocus.cx)
				{
					Char.myCharz().cdir = 1;
				}
				else
				{
					Char.myCharz().cdir = -1;
				}
				if (Char.myCharz().cx < Char.myCharz().npcFocus.cx)
				{
					Char.myCharz().npcFocus.cdir = -1;
				}
				else
				{
					Char.myCharz().npcFocus.cdir = 1;
				}
				int num15 = Math.abs(Char.myCharz().cx - Char.myCharz().npcFocus.cx);
				if (Math.abs(Char.myCharz().cy - Char.myCharz().npcFocus.cy) > 40)
				{
					Char.myCharz().cy = Char.myCharz().npcFocus.cy - 40;
				}
				if (num15 < 60)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					if (this.tMenuDelay == 0)
					{
						if (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId == 0)
						{
							if (Char.myCharz().taskMaint.index < 4 && Char.myCharz().npcFocus.template.npcTemplateId == 4)
							{
								return false;
							}
							if (Char.myCharz().taskMaint.index < 3 && Char.myCharz().npcFocus.template.npcTemplateId == 3)
							{
								return false;
							}
						}
						this.tMenuDelay = 50;
						InfoDlg.showWait();
						Service.gI().charMove();
						Service.gI().openMenu(Char.myCharz().npcFocus.template.npcTemplateId);
					}
				}
				else
				{
					int num8 = (20 + Res.r.nextInt(20)) * ((Char.myCharz().cx > Char.myCharz().npcFocus.cx) ? 1 : -1);
					Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().npcFocus.cx + num8, Char.myCharz().cy);
					Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
				}
				return false;
			}
			else if (Char.myCharz().charFocus != null)
			{
				if (this.mobCapcha != null)
				{
					return false;
				}
				if (Char.myCharz().cx < Char.myCharz().charFocus.cx)
				{
					Char.myCharz().cdir = 1;
				}
				else
				{
					Char.myCharz().cdir = -1;
				}
				int num9 = Math.abs(Char.myCharz().cx - Char.myCharz().charFocus.cx);
				int num10 = Math.abs(Char.myCharz().cy - Char.myCharz().charFocus.cy);
				if (!Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus) && !Char.myCharz().isSelectingSkillBuffToPlayer())
				{
					if (num9 < 60 && num10 < 40)
					{
						this.playerMenu(Char.myCharz().charFocus);
						if (!GameCanvas.isTouch && Char.myCharz().charFocus.charID >= 0 && TileMap.mapID != 51 && TileMap.mapID != 52 && this.popUpYesNo == null)
						{
							GameCanvas.panel.setTypePlayerMenu(Char.myCharz().charFocus);
							GameCanvas.panel.show();
							Service.gI().getPlayerMenu(Char.myCharz().charFocus.charID);
							Service.gI().messagePlayerMenu(Char.myCharz().charFocus.charID);
						}
					}
					else
					{
						int num11 = (20 + Res.r.nextInt(20)) * ((Char.myCharz().cx > Char.myCharz().charFocus.cx) ? 1 : -1);
						Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().charFocus.cx + num11, Char.myCharz().charFocus.cy);
						Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
					}
					return false;
				}
				if (Char.myCharz().myskill == null)
				{
					return false;
				}
				if (!this.checkSkillValid())
				{
					return false;
				}
				if (Char.myCharz().cx < Char.myCharz().charFocus.cx)
				{
					Char.myCharz().cdir = 1;
				}
				else
				{
					Char.myCharz().cdir = -1;
				}
				Char.myCharz().cvx = 0;
				if (num9 > Char.myCharz().myskill.dx || num10 > Char.myCharz().myskill.dy)
				{
					int num12 = (Char.myCharz().myskill.dx - 20) * ((Char.myCharz().cx > Char.myCharz().charFocus.cx) ? 1 : -1);
					if (num9 <= Char.myCharz().myskill.dx)
					{
						num12 = 0;
					}
					Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().charFocus.cx + num12, Char.myCharz().charFocus.cy);
					Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return false;
				}
				if (Char.myCharz().myskill.template.id == 20)
				{
					return true;
				}
				int num13 = 20;
				if (Char.myCharz().myskill.dx > 60)
				{
					num13 = 60;
					if (num9 < 20)
					{
						Char.myCharz().createShadow(Char.myCharz().cx, Char.myCharz().cy, 10);
					}
				}
				bool flag6 = false;
				if ((TileMap.tileTypeAtPixel(Char.myCharz().cx, Char.myCharz().cy + 3) & 2) == 2)
				{
					int num14 = (Char.myCharz().cx > Char.myCharz().charFocus.cx) ? 1 : -1;
					if ((TileMap.tileTypeAtPixel(Char.myCharz().charFocus.cx + num13 * num14, Char.myCharz().cy + 3) & 2) != 2)
					{
						flag6 = true;
					}
				}
				if (num9 <= num13 && !flag6)
				{
					if (Char.myCharz().cx > Char.myCharz().charFocus.cx)
					{
						Char.myCharz().cx = Char.myCharz().charFocus.cx + num13;
						Char.myCharz().cdir = -1;
					}
					else
					{
						Char.myCharz().cx = Char.myCharz().charFocus.cx - num13;
						Char.myCharz().cdir = 1;
					}
					Service.gI().charMove();
				}
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return true;
			}
			else
			{
				if (Char.myCharz().itemFocus != null)
				{
					this.pickItem();
					return false;
				}
				return true;
			}
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x0028D148 File Offset: 0x0028B348
		public bool isMeCanAttackMob(Mob m)
		{
			if (m == null)
			{
				return false;
			}
			if (Char.myCharz().cTypePk == 5)
			{
				return true;
			}
			if (Char.myCharz().isAttacPlayerStatus() && !m.isMobMe)
			{
				return false;
			}
			if (Char.myCharz().mobMe != null && m.Equals(Char.myCharz().mobMe))
			{
				return false;
			}
			Char @char = GameScr.findCharInMap(m.mobId);
			return @char == null || @char.cTypePk == 5 || Char.myCharz().isMeCanAttackOtherPlayer(@char);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x0028D1CC File Offset: 0x0028B3CC
		private bool checkSkillValid()
		{
			if (Char.myCharz().myskill != null && ((Char.myCharz().myskill.template.manaUseType != 1 && Char.myCharz().cMP < (long)Char.myCharz().myskill.manaUse) || (Char.myCharz().myskill.template.manaUseType == 1 && Char.myCharz().cMP < Char.myCharz().cMPFull * (long)Char.myCharz().myskill.manaUse / 100L)))
			{
				GameScr.info1.addInfo(mResources.NOT_ENOUGH_MP, 0);
				this.auto = 0;
				return false;
			}
			if (Char.myCharz().myskill == null || (Char.myCharz().myskill.template.maxPoint > 0 && Char.myCharz().myskill.point == 0))
			{
				GameCanvas.startOKDlg(mResources.SKILL_FAIL);
				return false;
			}
			return true;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x0028D2B8 File Offset: 0x0028B4B8
		private bool checkSkillValid2()
		{
			return (Char.myCharz().myskill == null || ((Char.myCharz().myskill.template.manaUseType == 1 || Char.myCharz().cMP >= (long)Char.myCharz().myskill.manaUse) && (Char.myCharz().myskill.template.manaUseType != 1 || Char.myCharz().cMP >= Char.myCharz().cMPFull * (long)Char.myCharz().myskill.manaUse / 100L))) && Char.myCharz().myskill != null && (Char.myCharz().myskill.template.maxPoint <= 0 || Char.myCharz().myskill.point != 0);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x0028D380 File Offset: 0x0028B580
		public void resetButton()
		{
			GameCanvas.menu.showMenu = false;
			ChatTextField.gI().close();
			ChatTextField.gI().center = null;
			this.isLockKey = false;
			this.typeTrade = 0;
			GameScr.indexMenu = 0;
			GameScr.indexSelect = 0;
			this.indexItemUse = -1;
			GameScr.indexRow = -1;
			GameScr.indexRowMax = 0;
			GameScr.indexTitle = 0;
			this.typeTrade = (this.typeTradeOrder = 0);
			mSystem.endKey();
			if (Char.myCharz().cHP <= 0L || Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5)
			{
				if (Char.myCharz().meDead)
				{
					this.cmdDead = new Command(mResources.DIES[0], 11038);
					this.center = this.cmdDead;
					Char.myCharz().cHP = 0L;
				}
				GameScr.isHaveSelectSkill = false;
			}
			else
			{
				GameScr.isHaveSelectSkill = true;
			}
			GameScr.scrMain.clear();
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x0028D471 File Offset: 0x0028B671
		public override void keyPress(int keyCode)
		{
			base.keyPress(keyCode);
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x0028D47C File Offset: 0x0028B67C
		public override void updateKey()
		{
			if (Controller.isStopReadMessage || Char.myCharz().isTeleport || Char.myCharz().isPaintNewSkill || InfoDlg.isLock)
			{
				return;
			}
			if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
			{
				this.UpdateKeyTouchControl();
			}
			this.checkAuto();
			GameCanvas.debug("F2", 0);
			if (ChatPopup.currChatPopup != null)
			{
				Command cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
				if ((GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(cmdNextLine)) && cmdNextLine != null)
				{
					GameCanvas.isPointerJustRelease = false;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					mScreen.keyTouch = -1;
					if (cmdNextLine != null)
					{
						cmdNextLine.performAction();
					}
				}
			}
			else if (!ChatTextField.gI().isShow)
			{
				if ((GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left)) && this.left != null)
				{
					GameCanvas.isPointerJustRelease = false;
					GameCanvas.isPointerClick = false;
					GameCanvas.keyPressed[12] = false;
					mScreen.keyTouch = -1;
					if (this.left != null)
					{
						this.left.performAction();
					}
				}
				if ((GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.right)) && this.right != null)
				{
					GameCanvas.isPointerJustRelease = false;
					GameCanvas.isPointerClick = false;
					GameCanvas.keyPressed[13] = false;
					mScreen.keyTouch = -1;
					if (this.right != null)
					{
						this.right.performAction();
					}
				}
				if ((GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center)) && this.center != null)
				{
					GameCanvas.isPointerJustRelease = false;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					mScreen.keyTouch = -1;
					if (this.center != null)
					{
						this.center.performAction();
					}
				}
			}
			else
			{
				if (ChatTextField.gI().left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(ChatTextField.gI().left)) && ChatTextField.gI().left != null)
				{
					ChatTextField.gI().left.performAction();
				}
				if (ChatTextField.gI().right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(ChatTextField.gI().right)) && ChatTextField.gI().right != null)
				{
					ChatTextField.gI().right.performAction();
				}
				if (ChatTextField.gI().center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(ChatTextField.gI().center)) && ChatTextField.gI().center != null)
				{
					ChatTextField.gI().center.performAction();
				}
			}
			GameCanvas.debug("F6", 0);
			this.updateKeyAlert();
			GameCanvas.debug("F7", 0);
			if (Char.myCharz().currentMovePoint != null)
			{
				for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
				{
					if (GameCanvas.keyPressed[i])
					{
						Char.myCharz().currentMovePoint = null;
						break;
					}
				}
			}
			GameCanvas.debug("F8", 0);
			if (ChatTextField.gI().isShow && GameCanvas.keyAsciiPress != 0)
			{
				ChatTextField.gI().keyPressed(GameCanvas.keyAsciiPress);
				GameCanvas.keyAsciiPress = 0;
				return;
			}
			if (this.isLockKey)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			if (GameCanvas.menu.showMenu || this.isOpenUI() || Char.isLockKey)
			{
				return;
			}
			if (GameCanvas.keyPressed[10])
			{
				GameCanvas.keyPressed[10] = false;
				this.doUseHP();
				GameCanvas.clearKeyPressed();
			}
			if (GameCanvas.keyPressed[11] && this.mobCapcha == null)
			{
				if (this.popUpYesNo != null)
				{
					this.popUpYesNo.cmdYes.performAction();
				}
				else if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
				{
					GameCanvas.panel.setTypeMessage();
					GameCanvas.panel.show();
				}
				GameCanvas.keyPressed[11] = false;
				GameCanvas.clearKeyPressed();
			}
			if (GameCanvas.keyAsciiPress != 0 && TField.isQwerty && GameCanvas.keyAsciiPress == 32)
			{
				this.doUseHP();
				GameCanvas.keyAsciiPress = 0;
				GameCanvas.clearKeyPressed();
			}
			if (GameCanvas.keyAsciiPress != 0 && this.mobCapcha == null && TField.isQwerty && GameCanvas.keyAsciiPress == 121)
			{
				if (this.popUpYesNo != null)
				{
					this.popUpYesNo.cmdYes.performAction();
					GameCanvas.keyAsciiPress = 0;
					GameCanvas.clearKeyPressed();
				}
				else if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
				{
					GameCanvas.panel.setTypeMessage();
					GameCanvas.panel.show();
					GameCanvas.keyAsciiPress = 0;
					GameCanvas.clearKeyPressed();
				}
			}
			if (GameCanvas.keyPressed[10] && this.mobCapcha == null)
			{
				GameCanvas.keyPressed[10] = false;
				GameScr.info2.doClick(10);
				GameCanvas.clearKeyPressed();
			}
			this.checkDrag();
			if (!Char.myCharz().isFlyAndCharge)
			{
				this.checkClick();
			}
			if (Char.myCharz().cmdMenu != null && Char.myCharz().cmdMenu.isPointerPressInside())
			{
				Char.myCharz().cmdMenu.performAction();
			}
			if (Char.myCharz().skillPaint != null)
			{
				return;
			}
			if (GameCanvas.keyAsciiPress != 0)
			{
				if (this.mobCapcha == null)
				{
					if (TField.isQwerty)
					{
						if (GameCanvas.keyPressed[1])
						{
							if (GameScr.keySkill[0] != null)
							{
								this.doSelectSkill(GameScr.keySkill[0], true);
							}
						}
						else if (GameCanvas.keyPressed[2])
						{
							if (GameScr.keySkill[1] != null)
							{
								this.doSelectSkill(GameScr.keySkill[1], true);
							}
						}
						else if (GameCanvas.keyPressed[3])
						{
							if (GameScr.keySkill[2] != null)
							{
								this.doSelectSkill(GameScr.keySkill[2], true);
							}
						}
						else if (GameCanvas.keyPressed[4])
						{
							if (GameScr.keySkill[3] != null)
							{
								this.doSelectSkill(GameScr.keySkill[3], true);
							}
						}
						else if (GameCanvas.keyPressed[5])
						{
							if (GameScr.keySkill[4] != null)
							{
								this.doSelectSkill(GameScr.keySkill[4], true);
							}
						}
						else if (GameCanvas.keyPressed[6])
						{
							if (GameScr.keySkill[5] != null)
							{
								this.doSelectSkill(GameScr.keySkill[5], true);
							}
						}
						else if (GameCanvas.keyPressed[7])
						{
							if (GameScr.keySkill[6] != null)
							{
								this.doSelectSkill(GameScr.keySkill[6], true);
							}
						}
						else if (GameCanvas.keyPressed[8])
						{
							if (GameScr.keySkill[7] != null)
							{
								this.doSelectSkill(GameScr.keySkill[7], true);
							}
						}
						else if (GameCanvas.keyPressed[9])
						{
							if (GameScr.keySkill[8] != null)
							{
								this.doSelectSkill(GameScr.keySkill[8], true);
							}
						}
						else if (GameCanvas.keyPressed[0])
						{
							if (GameScr.keySkill[9] != null)
							{
								this.doSelectSkill(GameScr.keySkill[9], true);
							}
						}
						else if (GameCanvas.keyAsciiPress == 114)
						{
							ChatTextField.gI().startChat(this, string.Empty);
						}
						else if (!ModFunc.GI().UpdateKey(GameCanvas.keyAsciiPress))
						{
						}
					}
					else if (!GameCanvas.isMoveNumberPad)
					{
						ChatTextField.gI().startChat(GameCanvas.keyAsciiPress, this, string.Empty);
					}
					else if (GameCanvas.keyAsciiPress == 55)
					{
						if (GameScr.keySkill[0] != null)
						{
							this.doSelectSkill(GameScr.keySkill[0], true);
						}
					}
					else if (GameCanvas.keyAsciiPress == 56)
					{
						if (GameScr.keySkill[1] != null)
						{
							this.doSelectSkill(GameScr.keySkill[1], true);
						}
					}
					else if (GameCanvas.keyAsciiPress == 57)
					{
						if (GameScr.keySkill[(!Main.isPC) ? 2 : 21] != null)
						{
							this.doSelectSkill(GameScr.keySkill[2], true);
						}
					}
					else if (GameCanvas.keyAsciiPress == 48)
					{
						ChatTextField.gI().startChat(this, string.Empty);
					}
				}
				else
				{
					char[] array = this.keyInput.ToCharArray();
					MyVector myVector = new MyVector();
					for (int j = 0; j < array.Length; j++)
					{
						myVector.addElement(array[j].ToString() + string.Empty);
					}
					myVector.removeElementAt(0);
					string text = ((char)GameCanvas.keyAsciiPress).ToString() + string.Empty;
					if (text.Equals(string.Empty) || text == null || text.Equals("\n"))
					{
						text = "-";
					}
					myVector.insertElementAt(text, myVector.size());
					this.keyInput = string.Empty;
					for (int k = 0; k < myVector.size(); k++)
					{
						this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
					}
					Service.gI().mobCapcha((char)GameCanvas.keyAsciiPress);
				}
				GameCanvas.keyAsciiPress = 0;
			}
			if (Char.myCharz().statusMe == 1)
			{
				GameCanvas.debug("F10", 0);
				if (!this.doSeleckSkillFlag)
				{
					if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
					{
						GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
						this.doFire(false, false);
					}
					else if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
					{
						if (!Char.myCharz().isLockMove)
						{
							this.setCharJump(0);
						}
					}
					else if (GameCanvas.keyHold[1] && this.mobCapcha == null)
					{
						if (!Main.isPC)
						{
							Char.myCharz().cdir = -1;
							if (!Char.myCharz().isLockMove)
							{
								this.setCharJump(-4);
							}
						}
					}
					else if (GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] && this.mobCapcha == null)
					{
						if (!Main.isPC)
						{
							Char.myCharz().cdir = 1;
							if (!Char.myCharz().isLockMove)
							{
								this.setCharJump(4);
							}
						}
					}
					else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
					{
						GameScr.isAutoPlay = false;
						Char.myCharz().isAttack = false;
						if (Char.myCharz().cdir == 1)
						{
							Char.myCharz().cdir = -1;
						}
						else if (!Char.myCharz().isLockMove)
						{
							if (Char.myCharz().cx - Char.myCharz().cxSend != 0)
							{
								Service.gI().charMove();
							}
							Char.myCharz().statusMe = 2;
							Char.myCharz().cvx = -Char.myCharz().cspeed;
						}
						Char.myCharz().holder = false;
					}
					else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
					{
						GameScr.isAutoPlay = false;
						Char.myCharz().isAttack = false;
						if (Char.myCharz().cdir == -1)
						{
							Char.myCharz().cdir = 1;
						}
						else if (!Char.myCharz().isLockMove)
						{
							if (Char.myCharz().cx - Char.myCharz().cxSend != 0)
							{
								Service.gI().charMove();
							}
							Char.myCharz().statusMe = 2;
							Char.myCharz().cvx = Char.myCharz().cspeed;
						}
						Char.myCharz().holder = false;
					}
				}
			}
			else if (Char.myCharz().statusMe == 2)
			{
				if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					this.doFire(false, true);
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
				{
					if (Char.myCharz().cx - Char.myCharz().cxSend != 0 || Char.myCharz().cy - Char.myCharz().cySend != 0)
					{
						Service.gI().charMove();
					}
					Char.myCharz().cvy = -10;
					Char.myCharz().statusMe = 3;
					Char.myCharz().cp1 = 0;
				}
				else if (GameCanvas.keyHold[1] && this.mobCapcha == null)
				{
					if (Main.isPC)
					{
						if (Char.myCharz().cx - Char.myCharz().cxSend != 0 || Char.myCharz().cy - Char.myCharz().cySend != 0)
						{
							Service.gI().charMove();
						}
						Char.myCharz().cdir = -1;
						Char.myCharz().cvy = -10;
						Char.myCharz().cvx = -4;
						Char.myCharz().statusMe = 3;
						Char.myCharz().cp1 = 0;
					}
				}
				else if (GameCanvas.keyHold[3] && this.mobCapcha == null)
				{
					if (!Main.isPC)
					{
						if (Char.myCharz().cx - Char.myCharz().cxSend != 0 || Char.myCharz().cy - Char.myCharz().cySend != 0)
						{
							Service.gI().charMove();
						}
						Char.myCharz().cdir = 1;
						Char.myCharz().cvy = -10;
						Char.myCharz().cvx = 4;
						Char.myCharz().statusMe = 3;
						Char.myCharz().cp1 = 0;
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
				{
					GameScr.isAutoPlay = false;
					if (Char.myCharz().cdir == 1)
					{
						Char.myCharz().cdir = -1;
					}
					else
					{
						Char.myCharz().cvx = -Char.myCharz().cspeed + Char.myCharz().cBonusSpeed;
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
				{
					GameScr.isAutoPlay = false;
					if (Char.myCharz().cdir == -1)
					{
						Char.myCharz().cdir = 1;
					}
					else
					{
						Char.myCharz().cvx = Char.myCharz().cspeed + Char.myCharz().cBonusSpeed;
					}
				}
			}
			else if (Char.myCharz().statusMe == 3)
			{
				GameScr.isAutoPlay = false;
				GameCanvas.debug("F12", 0);
				if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					this.doFire(false, true);
				}
				if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || (GameCanvas.keyHold[1] && this.mobCapcha == null))
				{
					if (Char.myCharz().cdir == 1)
					{
						Char.myCharz().cdir = -1;
					}
					else
					{
						Char.myCharz().cvx = -Char.myCharz().cspeed;
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] || (GameCanvas.keyHold[3] && this.mobCapcha == null))
				{
					if (Char.myCharz().cdir == -1)
					{
						Char.myCharz().cdir = 1;
					}
					else
					{
						Char.myCharz().cvx = Char.myCharz().cspeed;
					}
				}
				if ((GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || ((GameCanvas.keyHold[1] || GameCanvas.keyHold[3]) && this.mobCapcha == null)) && Char.myCharz().canFly && Char.myCharz().cMP > 0L && Char.myCharz().cp1 < 8 && Char.myCharz().cvy > -4)
				{
					Char.myCharz().cp1++;
					Char.myCharz().cvy = -7;
				}
			}
			else if (Char.myCharz().statusMe == 4)
			{
				GameCanvas.debug("F13", 0);
				if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					this.doFire(false, true);
				}
				if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] && Char.myCharz().cMP > 0L && Char.myCharz().canFly)
				{
					GameScr.isAutoPlay = false;
					if ((Char.myCharz().cx - Char.myCharz().cxSend != 0 || Char.myCharz().cy - Char.myCharz().cySend != 0) && (Res.abs(Char.myCharz().cx - Char.myCharz().cxSend) > 96 || Res.abs(Char.myCharz().cy - Char.myCharz().cySend) > 24))
					{
						Service.gI().charMove();
					}
					Char.myCharz().cvy = -10;
					Char.myCharz().statusMe = 3;
					Char.myCharz().cp1 = 0;
				}
				if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
				{
					GameScr.isAutoPlay = false;
					if (Char.myCharz().cdir == 1)
					{
						Char.myCharz().cdir = -1;
					}
					else
					{
						Char.myCharz().cp1++;
						Char.myCharz().cvx = -Char.myCharz().cspeed;
						if (Char.myCharz().cp1 > 5 && Char.myCharz().cvy > 6)
						{
							Char.myCharz().statusMe = 10;
							Char.myCharz().cp1 = 0;
							Char.myCharz().cvy = 0;
						}
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
				{
					GameScr.isAutoPlay = false;
					if (Char.myCharz().cdir == -1)
					{
						Char.myCharz().cdir = 1;
					}
					else
					{
						Char.myCharz().cp1++;
						Char.myCharz().cvx = Char.myCharz().cspeed;
						if (Char.myCharz().cp1 > 5 && Char.myCharz().cvy > 6)
						{
							Char.myCharz().statusMe = 10;
							Char.myCharz().cp1 = 0;
							Char.myCharz().cvy = 0;
						}
					}
				}
			}
			else if (Char.myCharz().statusMe == 10)
			{
				GameCanvas.debug("F14", 0);
				if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					this.doFire(false, true);
				}
				if (Char.myCharz().canFly && Char.myCharz().cMP > 0L)
				{
					if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
					{
						GameScr.isAutoPlay = false;
						if ((Char.myCharz().cx - Char.myCharz().cxSend != 0 || Char.myCharz().cy - Char.myCharz().cySend != 0) && (Res.abs(Char.myCharz().cx - Char.myCharz().cxSend) > 96 || Res.abs(Char.myCharz().cy - Char.myCharz().cySend) > 24))
						{
							Service.gI().charMove();
						}
						Char.myCharz().cvy = -10;
						Char.myCharz().statusMe = 3;
						Char.myCharz().cp1 = 0;
					}
					else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
					{
						GameScr.isAutoPlay = false;
						if (Char.myCharz().cdir == 1)
						{
							Char.myCharz().cdir = -1;
						}
						else
						{
							Char.myCharz().cvx = -(Char.myCharz().cspeed + 1);
						}
					}
					else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
					{
						if (Char.myCharz().cdir == -1)
						{
							Char.myCharz().cdir = 1;
						}
						else
						{
							Char.myCharz().cvx = Char.myCharz().cspeed + 1;
						}
					}
				}
			}
			else if (Char.myCharz().statusMe == 7)
			{
				GameCanvas.debug("F15", 0);
				if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				}
				if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
				{
					GameScr.isAutoPlay = false;
					if (Char.myCharz().cdir == 1)
					{
						Char.myCharz().cdir = -1;
					}
					else
					{
						Char.myCharz().cvx = -Char.myCharz().cspeed + 2;
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
				{
					GameScr.isAutoPlay = false;
					if (Char.myCharz().cdir == -1)
					{
						Char.myCharz().cdir = 1;
					}
					else
					{
						Char.myCharz().cvx = Char.myCharz().cspeed - 2;
					}
				}
			}
			GameCanvas.debug("F17", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] && GameCanvas.keyAsciiPress != 56)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				Char.myCharz().delayFall = 0;
			}
			if (GameCanvas.keyPressed[10])
			{
				GameCanvas.keyPressed[10] = false;
				this.doUseHP();
			}
			GameCanvas.debug("F20", 0);
			GameCanvas.clearKeyPressed();
			GameCanvas.debug("F23", 0);
			this.doSeleckSkillFlag = false;
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x00006D67 File Offset: 0x00004F67
		public bool isVsMap()
		{
			return true;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x0028E960 File Offset: 0x0028CB60
		private void checkDrag()
		{
			if (GameScr.isAnalog == 1 || GameScr.gamePad.disableCheckDrag())
			{
				return;
			}
			Char.myCharz().cmtoChar = true;
			if (GameScr.isUseTouch)
			{
				return;
			}
			if (GameCanvas.isPointerJustDown)
			{
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = true;
				this.ptDownTime = 0;
				this.ptLastDownX = (this.ptFirstDownX = GameCanvas.px);
				this.ptLastDownY = (this.ptFirstDownY = GameCanvas.py);
			}
			if (this.isPointerDowning)
			{
				int num = GameCanvas.px - this.ptLastDownX;
				int num2 = GameCanvas.py - this.ptLastDownY;
				if (!this.isChangingCameraMode && (Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15))
				{
					this.isChangingCameraMode = true;
				}
				this.ptLastDownX = GameCanvas.px;
				this.ptLastDownY = GameCanvas.py;
				this.ptDownTime++;
				if (this.isChangingCameraMode)
				{
					Char.myCharz().cmtoChar = false;
					GameScr.cmx -= num;
					GameScr.cmy -= num2;
					if (GameScr.cmx < 24)
					{
						int num3 = (24 - GameScr.cmx) / 3;
						if (num3 != 0)
						{
							GameScr.cmx += num - num / num3;
						}
					}
					if (GameScr.cmx < (this.isVsMap() ? 24 : 0))
					{
						GameScr.cmx = (this.isVsMap() ? 24 : 0);
					}
					if (GameScr.cmx > GameScr.cmxLim)
					{
						int num4 = (GameScr.cmx - GameScr.cmxLim) / 3;
						if (num4 != 0)
						{
							GameScr.cmx += num - num / num4;
						}
					}
					if (GameScr.cmx > GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0))
					{
						GameScr.cmx = GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0);
					}
					if (GameScr.cmy < 0)
					{
						int num5 = -GameScr.cmy / 3;
						if (num5 != 0)
						{
							GameScr.cmy += num2 - num2 / num5;
						}
					}
					if (GameScr.cmy < -((!this.isVsMap()) ? 24 : 0))
					{
						GameScr.cmy = -((!this.isVsMap()) ? 24 : 0);
					}
					if (GameScr.cmy > GameScr.cmyLim)
					{
						GameScr.cmy = GameScr.cmyLim;
					}
					GameScr.cmtoX = GameScr.cmx;
					GameScr.cmtoY = GameScr.cmy;
				}
			}
			if (this.isPointerDowning && GameCanvas.isPointerJustRelease)
			{
				this.isPointerDowning = false;
				this.isChangingCameraMode = false;
				if (Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15)
				{
					GameCanvas.isPointerJustRelease = false;
				}
			}
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0028EC00 File Offset: 0x0028CE00
		private void checkClick()
		{
			if (this.isCharging())
			{
				return;
			}
			if (this.popUpYesNo != null && this.popUpYesNo.cmdYes != null && this.popUpYesNo.cmdYes.isPointerPressInside())
			{
				this.popUpYesNo.cmdYes.performAction();
				return;
			}
			if (ModFunc.isEditButton || ModFunc.isShowFilterList || ModFunc.isShowMenuChat || this.checkClickToCapcha())
			{
				return;
			}
			long num = mSystem.currentTimeMillis();
			if (this.lastSingleClick != 0L)
			{
				this.lastSingleClick = 0L;
				GameCanvas.isPointerJustDown = false;
				if (!this.disableSingleClick)
				{
					this.checkSingleClick();
					GameCanvas.isPointerJustRelease = false;
					this.isWaitingDoubleClick = true;
					this.timeStartDblClick = mSystem.currentTimeMillis();
				}
			}
			if (this.isWaitingDoubleClick)
			{
				this.timeEndDblClick = mSystem.currentTimeMillis();
				if (this.timeEndDblClick - this.timeStartDblClick < 300L && GameCanvas.isPointerJustRelease)
				{
					this.isWaitingDoubleClick = false;
					this.checkDoubleClick();
				}
			}
			if (GameCanvas.isPointerJustRelease)
			{
				this.disableSingleClick = this.checkSingleClickEarly();
				this.lastSingleClick = num;
				this.lastClickCMX = GameScr.cmx;
				this.lastClickCMY = GameScr.cmy;
				GameCanvas.isPointerJustRelease = false;
			}
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x0028ED20 File Offset: 0x0028CF20
		private IMapObject findClickToItem(int px, int py)
		{
			IMapObject mapObject = null;
			int num = 0;
			int num2 = 30;
			MyVector[] array = new MyVector[]
			{
				GameScr.vMob,
				GameScr.vNpc,
				GameScr.vItemMap,
				GameScr.vCharInMap
			};
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array[i].size(); j++)
				{
					IMapObject mapObject2 = (IMapObject)array[i].elementAt(j);
					if (!mapObject2.isInvisible())
					{
						if (mapObject2 is Mob)
						{
							Mob mob = (Mob)mapObject2;
							if (mob.isMobMe && mob.Equals(Char.myCharz().mobMe))
							{
								goto IL_118;
							}
						}
						int x = mapObject2.getX();
						int y = mapObject2.getY();
						int w = mapObject2.getW();
						int h = mapObject2.getH();
						if (this.inRectangle(px, py, x - w / 2 - num2, y - h - num2, w + num2 * 2, h + num2 * 2))
						{
							if (mapObject == null)
							{
								mapObject = mapObject2;
								num = Res.abs(px - x) + Res.abs(py - y);
								if (i == 1)
								{
									return mapObject;
								}
							}
							else
							{
								int num3 = Res.abs(px - x) + Res.abs(py - y);
								if (num3 < num)
								{
									mapObject = mapObject2;
									num = num3;
								}
							}
						}
					}
					IL_118:;
				}
			}
			return mapObject;
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0003AA84 File Offset: 0x00038C84
		private bool inRectangle(int xClick, int yClick, int x, int y, int w, int h)
		{
			return xClick >= x && xClick <= x + w && yClick >= y && yClick <= y + h;
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x0028EE6C File Offset: 0x0028D06C
		private bool checkSingleClickEarly()
		{
			int num = GameCanvas.px + GameScr.cmx;
			int num2 = GameCanvas.py + GameScr.cmy;
			Char.myCharz().cancelAttack();
			IMapObject mapObject = this.findClickToItem(num, num2);
			if (mapObject == null)
			{
				return false;
			}
			if (Char.myCharz().isAttacPlayerStatus() && Char.myCharz().charFocus != null && !mapObject.Equals(Char.myCharz().charFocus) && !mapObject.Equals(Char.myCharz().charFocus.mobMe) && mapObject is Char)
			{
				Char @char = (Char)mapObject;
				if (@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
				{
					this.checkClickMoveTo(num, num2, 2);
					return false;
				}
			}
			if (Char.myCharz().mobFocus == mapObject || Char.myCharz().itemFocus == mapObject)
			{
				this.doDoubleClickToObj(mapObject);
				return true;
			}
			if (TileMap.mapID == 51 && mapObject.Equals(Char.myCharz().npcFocus))
			{
				this.checkClickMoveTo(num, num2, 3);
				return false;
			}
			if (Char.myCharz().skillPaint != null || Char.myCharz().arr != null || Char.myCharz().dart != null || Char.myCharz().skillInfoPaint() != null)
			{
				return false;
			}
			Char.myCharz().FocusManualTo(mapObject);
			mapObject.stopMoving();
			return false;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x0028EFA8 File Offset: 0x0028D1A8
		private void checkDoubleClick()
		{
			int num = GameCanvas.px + this.lastClickCMX;
			int num2 = GameCanvas.py + this.lastClickCMY;
			int cy = Char.myCharz().cy;
			if (this.isLockKey)
			{
				return;
			}
			IMapObject mapObject = this.findClickToItem(num, num2);
			if (mapObject == null)
			{
				if (!this.checkClickToPopup(num, num2) && !this.checkClipTopChatPopUp(num, num2) && !Main.isPC)
				{
					this.checkClickMoveTo(num, num2, 7);
				}
				return;
			}
			if (mapObject is Mob && !this.isMeCanAttackMob((Mob)mapObject))
			{
				this.checkClickMoveTo(num, num2, 4);
				return;
			}
			if (this.checkClickToBotton(mapObject) || (!mapObject.Equals(Char.myCharz().npcFocus) && this.mobCapcha != null))
			{
				return;
			}
			if (Char.myCharz().isAttacPlayerStatus() && Char.myCharz().charFocus != null && !mapObject.Equals(Char.myCharz().charFocus) && !mapObject.Equals(Char.myCharz().charFocus.mobMe) && mapObject is Char)
			{
				Char @char = (Char)mapObject;
				if (@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
				{
					this.checkClickMoveTo(num, num2, 5);
					return;
				}
			}
			if (TileMap.mapID == 51 && mapObject.Equals(Char.myCharz().npcFocus))
			{
				this.checkClickMoveTo(num, num2, 6);
				return;
			}
			this.doDoubleClickToObj(mapObject);
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x0028F0F4 File Offset: 0x0028D2F4
		public bool checkClickToBotton(IMapObject Object)
		{
			if (Object == null)
			{
				return false;
			}
			int y = Object.getY();
			int num = Char.myCharz().cy;
			if (y < num)
			{
				while (y < num)
				{
					num -= 5;
					if (TileMap.tileTypeAt(Char.myCharz().cx, num, 8192))
					{
						this.auto = 0;
						Char.myCharz().cancelAttack();
						Char.myCharz().currentMovePoint = null;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0028F15C File Offset: 0x0028D35C
		private void doDoubleClickToObj(IMapObject obj)
		{
			if (!ModFunc.isEditButton && !ModFunc.isShowFilterList && !ModFunc.isShowMenuChat)
			{
				if (obj.Equals(Char.myCharz().mobFocus) && PickMob.tanSat && PickMob.TypeMobsTanSat.Count != 0 && !PickMob.TypeMobsTanSat.Contains(((Mob)obj).templateId))
				{
					GameScr.info1.addInfo("Quái đang đánh không có trong danh sách tàn sát", 0);
				}
				if ((obj.Equals(Char.myCharz().npcFocus) || this.mobCapcha == null) && !this.checkClickToBotton(obj))
				{
					this.checkEffToObj(obj, false);
					Char.myCharz().cancelAttack();
					Char.myCharz().currentMovePoint = null;
					Char.myCharz().cvx = (Char.myCharz().cvy = 0);
					obj.stopMoving();
					this.auto = 10;
					this.doFire(false, true);
					this.clickToX = obj.getX();
					this.clickToY = obj.getY();
					this.clickOnTileTop = false;
					this.clickMoving = true;
					this.clickMovingRed = true;
					this.clickMovingTimeOut = 20;
					this.clickMovingP1 = 30;
				}
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x0028F284 File Offset: 0x0028D484
		private void checkSingleClick()
		{
			int xClick = GameCanvas.px + this.lastClickCMX;
			int yClick = GameCanvas.py + this.lastClickCMY;
			if (!this.isLockKey && !this.checkClickToPopup(xClick, yClick) && !this.checkClipTopChatPopUp(xClick, yClick))
			{
				this.checkClickMoveTo(xClick, yClick, 0);
			}
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x0028F2D0 File Offset: 0x0028D4D0
		private bool checkClipTopChatPopUp(int xClick, int yClick)
		{
			if (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null)
			{
				return false;
			}
			if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
			{
				int num = Res.abs(GameScr.info2.cmx) + GameScr.info2.info.X - 40;
				int num2 = Res.abs(GameScr.info2.cmy) + GameScr.info2.info.Y;
				if (this.inRectangle(xClick - GameScr.cmx, yClick - GameScr.cmy, num, num2, 200, GameScr.info2.info.H))
				{
					GameScr.info2.doClick(10);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x0028F3A8 File Offset: 0x0028D5A8
		private bool checkClickToPopup(int xClick, int yClick)
		{
			for (int i = 0; i < PopUp.vPopups.size(); i++)
			{
				PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
				if (this.inRectangle(xClick, yClick, popUp.cx, popUp.cy, popUp.cw, popUp.ch))
				{
					if (popUp.cy <= 24 && TileMap.isInAirMap() && Char.myCharz().cTypePk != 0)
					{
						return false;
					}
					if (popUp.isPaint)
					{
						popUp.doClick(10);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x0028F430 File Offset: 0x0028D630
		private void checkClickMoveTo(int xClick, int yClick, int index)
		{
			if (ModFunc.isShowMenuChat || GameScr.gamePad.disableClickMove() || ChatTextField.gI().isShow || ModFunc.isEditButton || ModFunc.isShowFilterList)
			{
				return;
			}
			Char.myCharz().cancelAttack();
			if (xClick < TileMap.pxw && xClick > TileMap.pxw - 32)
			{
				Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
				return;
			}
			if (xClick < 32 && xClick > 0)
			{
				Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
				return;
			}
			if (xClick < TileMap.pxw && xClick > TileMap.pxw - 48)
			{
				Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
				return;
			}
			if (xClick < 48 && xClick > 0)
			{
				Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
				return;
			}
			this.clickToX = xClick;
			this.clickToY = yClick;
			this.clickOnTileTop = false;
			Char.myCharz().delayFall = 0;
			int num = (!Char.myCharz().canFly || Char.myCharz().cMP <= 0L) ? 1000 : 0;
			if (this.clickToY > Char.myCharz().cy && Res.abs(this.clickToX - Char.myCharz().cx) < 12)
			{
				return;
			}
			int i = 0;
			while (i < 60 + num && this.clickToY + i < TileMap.pxh - 24)
			{
				if (TileMap.tileTypeAt(this.clickToX, this.clickToY + i, 2))
				{
					this.clickToY = TileMap.tileYofPixel(this.clickToY + i);
					this.clickOnTileTop = true;
					break;
				}
				i += 24;
			}
			for (int j = 0; j < 40 + num; j += 24)
			{
				if (TileMap.tileTypeAt(this.clickToX, this.clickToY - j, 2))
				{
					this.clickToY = TileMap.tileYofPixel(this.clickToY - j);
					this.clickOnTileTop = true;
					break;
				}
			}
			this.clickMoving = true;
			this.clickMovingRed = false;
			this.clickMovingP1 = ((!this.clickOnTileTop) ? 30 : ((yClick >= this.clickToY) ? this.clickToY : yClick));
			Char.myCharz().delayFall = 0;
			if (!this.clickOnTileTop && this.clickToY < Char.myCharz().cy - 50)
			{
				Char.myCharz().delayFall = 20;
			}
			this.clickMovingTimeOut = 30;
			this.auto = 0;
			if (Char.myCharz().holder)
			{
				Char.myCharz().removeHoleEff();
			}
			Char.myCharz().currentMovePoint = new MovePoint(this.clickToX, this.clickToY);
			Char.myCharz().cdir = ((Char.myCharz().cx - Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
			Char.myCharz().endMovePointCommand = null;
			GameScr.isAutoPlay = false;
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x0028F6E8 File Offset: 0x0028D8E8
		private void checkAuto()
		{
			long num = mSystem.currentTimeMillis();
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] || GameCanvas.keyPressed[1] || GameCanvas.keyPressed[3])
			{
				this.auto = 0;
				GameScr.isAutoPlay = false;
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && !this.isPaintPopup())
			{
				if (this.auto == 0)
				{
					if (num - this.lastFire < 800L && this.checkSkillValid2() && (Char.myCharz().mobFocus != null || (Char.myCharz().charFocus != null && Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus))))
					{
						Res.outz("toi day");
						this.auto = 10;
						GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					}
				}
				else
				{
					this.auto = 0;
					GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false);
				}
				this.lastFire = num;
			}
			if (GameCanvas.gameTick % 5 == 0 && this.auto > 0 && Char.myCharz().currentMovePoint == null)
			{
				if (Char.myCharz().myskill != null && (Char.myCharz().myskill.template.isUseAlone() || Char.myCharz().myskill.paintCanNotUseSkill))
				{
					return;
				}
				if ((Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.status != 1 && Char.myCharz().mobFocus.status != 0 && Char.myCharz().charFocus == null) || (Char.myCharz().charFocus != null && Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus)))
				{
					if (Char.myCharz().myskill.paintCanNotUseSkill)
					{
						return;
					}
					this.doFire(false, true);
				}
			}
			if (this.auto > 1)
			{
				this.auto--;
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x0028F914 File Offset: 0x0028DB14
		public void doUseHP()
		{
			if (Char.myCharz().stone || Char.myCharz().blindEff || Char.myCharz().holdEffID > 0)
			{
				return;
			}
			long num = mSystem.currentTimeMillis();
			if (num - this.lastUsePotion >= 10000L)
			{
				if (!Char.myCharz().doUsePotion())
				{
					GameScr.info1.addInfo(mResources.HP_EMPTY, 0);
					return;
				}
				ServerEffect.addServerEffect(11, Char.myCharz(), 5);
				ServerEffect.addServerEffect(104, Char.myCharz(), 4);
				this.lastUsePotion = num;
				SoundMn.gI().eatPeans();
			}
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x0028F9A8 File Offset: 0x0028DBA8
		public void activeSuperPower(int x, int y)
		{
			if (!this.isSuperPower)
			{
				SoundMn.gI().bigeExlode();
				this.isSuperPower = true;
				this.tPower = 0;
				this.dxPower = 0;
				this.xPower = x - GameScr.cmx;
				this.yPower = y - GameScr.cmy;
			}
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x0028F9F6 File Offset: 0x0028DBF6
		public void activeRongThanEff(bool isMe)
		{
			this.activeRongThan = true;
			this.isUseFreez = true;
			this.isMeCallRongThan = true;
			if (isMe)
			{
				EffecMn.addEff(new Effect(20, Char.myCharz().cx, Char.myCharz().cy - 77, 2, 8, 1));
			}
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x0028FA36 File Offset: 0x0028DC36
		public void hideRongThanEff()
		{
			this.activeRongThan = false;
			this.isUseFreez = true;
			this.isMeCallRongThan = false;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x0028FA4D File Offset: 0x0028DC4D
		public void doiMauTroi()
		{
			this.isRongThanXuatHien = true;
			this.mautroi = mGraphics.blendColor(0.4f, 0, GameCanvas.colorTop[GameCanvas.colorTop.Length - 1]);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x0028FA78 File Offset: 0x0028DC78
		public void callRongThan(int x, int y)
		{
			Res.outz("VE RONG THAN O VI TRI x= " + x.ToString() + " y=" + y.ToString());
			this.doiMauTroi();
			EffecMn.addEff(new Effect((!this.isRongNamek) ? 17 : 25, x, y - 77, 2, -1, 1));
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x0028FACD File Offset: 0x0028DCCD
		public void hideRongThan()
		{
			this.isRongThanXuatHien = false;
			EffecMn.removeEff(17);
			if (this.isRongNamek)
			{
				this.isRongNamek = false;
				EffecMn.removeEff(25);
			}
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x0028FAF4 File Offset: 0x0028DCF4
		private void autoPlay()
		{
			if (this.timeSkill > 0)
			{
				this.timeSkill--;
			}
			if (!GameScr.canAutoPlay || GameScr.isChangeZone || Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5 || Char.myCharz().isCharge || Char.myCharz().isFlyAndCharge || Char.myCharz().isUseChargeSkill())
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (mob.status != 0 && mob.status != 1)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			bool flag2 = false;
			for (int j = 0; j < Char.myCharz().arrItemBag.Length; j++)
			{
				Item item = Char.myCharz().arrItemBag[j];
				if (item != null && item.template.type == 6)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2 && GameCanvas.gameTick % 150 == 0)
			{
				Service.gI().requestPean();
			}
			if (Char.myCharz().cHP <= Char.myCharz().cHPFull * 20L / 100L || Char.myCharz().cMP <= Char.myCharz().cMPFull * 20L / 100L)
			{
				this.doUseHP();
			}
			if (Char.myCharz().mobFocus == null || (Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.isMobMe))
			{
				for (int k = 0; k < GameScr.vMob.size(); k++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(k);
					if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0L && !mob2.isMobMe)
					{
						Char.myCharz().cx = mob2.x;
						Char.myCharz().cy = mob2.y;
						Char.myCharz().mobFocus = mob2;
						Service.gI().charMove();
						break;
					}
				}
			}
			else if (Char.myCharz().mobFocus.hp <= 0L || Char.myCharz().mobFocus.status == 1 || Char.myCharz().mobFocus.status == 0)
			{
				Char.myCharz().mobFocus = null;
			}
			if (Char.myCharz().mobFocus == null || this.timeSkill != 0 || (Char.myCharz().skillInfoPaint() != null && Char.myCharz().indexSkill < Char.myCharz().skillInfoPaint().Length && Char.myCharz().dart != null && Char.myCharz().arr != null))
			{
				return;
			}
			Skill skill = null;
			if (GameCanvas.isTouch)
			{
				for (int l = 0; l < GameScr.onScreenSkill.Length; l++)
				{
					if (GameScr.onScreenSkill[l] != null && !GameScr.onScreenSkill[l].paintCanNotUseSkill && GameScr.onScreenSkill[l].template.id != 10 && GameScr.onScreenSkill[l].template.id != 11 && GameScr.onScreenSkill[l].template.id != 14 && GameScr.onScreenSkill[l].template.id != 23 && GameScr.onScreenSkill[l].template.id != 7 && Char.myCharz().skillInfoPaint() == null && !GameScr.onScreenSkill[l].template.isSkillSpec())
					{
						long num = (GameScr.onScreenSkill[l].template.manaUseType == 2) ? 1L : ((GameScr.onScreenSkill[l].template.manaUseType == 1) ? ((long)GameScr.onScreenSkill[l].manaUse * Char.myCharz().cMPFull / 100L) : ((long)GameScr.onScreenSkill[l].manaUse));
						if (Char.myCharz().cMP >= num)
						{
							if (skill == null)
							{
								skill = GameScr.onScreenSkill[l];
							}
							else if (skill.coolDown < GameScr.onScreenSkill[l].coolDown)
							{
								skill = GameScr.onScreenSkill[l];
							}
						}
					}
				}
				if (skill != null)
				{
					this.doSelectSkill(skill, true);
					this.doDoubleClickToObj(Char.myCharz().mobFocus);
				}
				return;
			}
			for (int m = 0; m < GameScr.keySkill.Length; m++)
			{
				if (GameScr.keySkill[m] != null && !GameScr.keySkill[m].paintCanNotUseSkill && GameScr.keySkill[m].template.id != 10 && GameScr.keySkill[m].template.id != 11 && GameScr.keySkill[m].template.id != 14 && GameScr.keySkill[m].template.id != 23 && GameScr.keySkill[m].template.id != 7 && Char.myCharz().skillInfoPaint() == null)
				{
					long num2 = (GameScr.keySkill[m].template.manaUseType == 2) ? 1L : ((GameScr.keySkill[m].template.manaUseType == 1) ? ((long)GameScr.keySkill[m].manaUse * Char.myCharz().cMPFull / 100L) : ((long)GameScr.keySkill[m].manaUse));
					if (Char.myCharz().cMP >= num2)
					{
						if (skill == null)
						{
							skill = GameScr.keySkill[m];
						}
						else if (skill.coolDown < GameScr.keySkill[m].coolDown)
						{
							skill = GameScr.keySkill[m];
						}
					}
				}
			}
			if (skill != null)
			{
				this.doSelectSkill(skill, true);
				this.doDoubleClickToObj(Char.myCharz().mobFocus);
			}
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x002900B0 File Offset: 0x0028E2B0
		public void doFire(bool isFireByShortCut, bool skipWaypoint)
		{
			GameScr.tam++;
			Waypoint waypoint = Char.myCharz().isInEnterOfflinePoint();
			Waypoint waypoint2 = Char.myCharz().isInEnterOnlinePoint();
			if (!skipWaypoint && waypoint != null && (Char.myCharz().mobFocus == null || (Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.templateId == 0)))
			{
				waypoint.popup.command.performAction();
				return;
			}
			if (!skipWaypoint && waypoint2 != null && (Char.myCharz().mobFocus == null || (Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.templateId == 0)))
			{
				waypoint2.popup.command.performAction();
				return;
			}
			if ((TileMap.mapID == 51 && Char.myCharz().npcFocus != null) || Char.myCharz().statusMe == 14)
			{
				return;
			}
			Char.myCharz().cvx = (Char.myCharz().cvy = 0);
			if (Char.myCharz().isSelectingSkillUseAlone() && Char.myCharz().focusToAttack())
			{
				if (this.checkSkillValid())
				{
					Char.myCharz().currentFireByShortcut = isFireByShortCut;
					Char.myCharz().useSkillNotFocus();
				}
			}
			else if (this.isAttack())
			{
				if (Char.myCharz().isUseChargeSkill() && Char.myCharz().focusToAttack())
				{
					if (this.checkSkillValid())
					{
						Char.myCharz().currentFireByShortcut = isFireByShortCut;
						Char.myCharz().sendUseChargeSkill();
					}
					else
					{
						Char.myCharz().stopUseChargeSkill();
					}
				}
				else
				{
					bool flag = TileMap.tileTypeAt(Char.myCharz().cx, Char.myCharz().cy, 2);
					Char.myCharz().setSkillPaint(GameScr.sks[(int)Char.myCharz().myskill.skillId], (!flag) ? 1 : 0);
					if (flag)
					{
						Char.myCharz().delayFall = 20;
					}
					Char.myCharz().currentFireByShortcut = isFireByShortCut;
				}
			}
			if (Char.myCharz().isSelectingSkillBuffToPlayer())
			{
				this.auto = 0;
			}
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x00290294 File Offset: 0x0028E494
		private void askToPick()
		{
			Npc npc = new Npc(5, 0, -100, 100, 5, GameScr.info1.charId[Char.myCharz().cgender][2]);
			string nhatvatpham = mResources.nhatvatpham;
			string[] menu = new string[]
			{
				mResources.YES,
				mResources.NO
			};
			npc.idItem = 673;
			GameScr.gI().createMenu(menu, npc);
			ChatPopup.addChatPopupWithIcon(nhatvatpham, 100000, npc, 5820);
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0029030C File Offset: 0x0028E50C
		private void pickItem()
		{
			if (Char.myCharz().itemFocus == null)
			{
				return;
			}
			if (Char.myCharz().cx < Char.myCharz().itemFocus.x)
			{
				Char.myCharz().cdir = 1;
			}
			else
			{
				Char.myCharz().cdir = -1;
			}
			int num3 = Math.abs(Char.myCharz().cx - Char.myCharz().itemFocus.x);
			int num2 = Math.abs(Char.myCharz().cy - Char.myCharz().itemFocus.y);
			if (num3 > 40 || num2 >= 40)
			{
				Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().itemFocus.x, Char.myCharz().itemFocus.y);
				Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			if (Char.myCharz().itemFocus.template.id != 673)
			{
				Service.gI().pickItem(Char.myCharz().itemFocus.itemMapID);
				return;
			}
			this.askToPick();
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x00290438 File Offset: 0x0028E638
		public bool isCharging()
		{
			return Char.myCharz().isFlyAndCharge || Char.myCharz().isUseSkillAfterCharge || Char.myCharz().isStandAndCharge || Char.myCharz().isWaitMonkey || this.isSuperPower || Char.myCharz().isFreez;
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x0029048C File Offset: 0x0028E68C
		public void doSelectSkill(Skill skill, bool isShortcut)
		{
			if (Char.myCharz().isCreateDark || this.isCharging() || Char.myCharz().taskMaint.taskId <= 1 || ModFunc.isShowMenuChat)
			{
				return;
			}
			Char.myCharz().myskill = skill;
			if (this.lastSkill != skill && this.lastSkill != null)
			{
				Service.gI().selectSkill((int)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
				this.lastSkill = skill;
				this.selectedIndexSkill = -1;
				GameScr.gI().auto = 0;
				return;
			}
			if (Char.myCharz().isUseSkillSpec())
			{
				Char.myCharz().sendNewAttack((short)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
				this.lastSkill = skill;
				this.selectedIndexSkill = -1;
				GameScr.gI().auto = 0;
				return;
			}
			if (Char.myCharz().isSelectingSkillUseAlone())
			{
				this.doUseSkillNotFocus(skill);
				this.lastSkill = skill;
				return;
			}
			this.selectedIndexSkill = -1;
			if (skill == null)
			{
				return;
			}
			if (this.lastSkill != skill)
			{
				Service.gI().selectSkill((int)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
			}
			if (Char.myCharz().charFocus != null || !Char.myCharz().isSelectingSkillBuffToPlayer())
			{
				if (Char.myCharz().focusToAttack())
				{
					this.doFire(isShortcut, true);
					this.doSeleckSkillFlag = true;
				}
				this.lastSkill = skill;
			}
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x00290600 File Offset: 0x0028E800
		public void doUseSkill(Skill skill, bool isShortcut)
		{
			if ((TileMap.mapID == 112 || TileMap.mapID == 113) && Char.myCharz().cTypePk == 0)
			{
				return;
			}
			if (Char.myCharz().isSelectingSkillUseAlone())
			{
				this.doUseSkillNotFocus(skill);
				return;
			}
			this.selectedIndexSkill = -1;
			if (skill != null)
			{
				Service.gI().selectSkill((int)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
				this.resetButton();
				Char.myCharz().myskill = skill;
				this.doFire(isShortcut, true);
			}
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0029068C File Offset: 0x0028E88C
		public void doUseSkillNotFocus(Skill skill)
		{
			if (((TileMap.mapID != 112 && TileMap.mapID != 113) || Char.myCharz().cTypePk != 0) && this.checkSkillValid())
			{
				this.selectedIndexSkill = -1;
				if (skill != null)
				{
					Service.gI().selectSkill((int)skill.template.id);
					this.saveRMSCurrentSkill(skill.template.id);
					this.resetButton();
					Char.myCharz().myskill = skill;
					Char.myCharz().useSkillNotFocus();
					Char.myCharz().currentFireByShortcut = true;
					this.auto = 0;
				}
			}
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x0029071C File Offset: 0x0028E91C
		public void sortSkill()
		{
			for (int i = 0; i < Char.myCharz().vSkillFight.size() - 1; i++)
			{
				Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(i);
				for (int j = i + 1; j < Char.myCharz().vSkillFight.size(); j++)
				{
					Skill skill2 = (Skill)Char.myCharz().vSkillFight.elementAt(j);
					if (skill2.template.id < skill.template.id)
					{
						Skill skill3 = skill2;
						skill2 = skill;
						skill = skill3;
						Char.myCharz().vSkillFight.setElementAt(skill, i);
						Char.myCharz().vSkillFight.setElementAt(skill2, j);
					}
				}
			}
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x002907D4 File Offset: 0x0028E9D4
		public void updateKeyTouchCapcha()
		{
			if (this.isNotPaintTouchControl())
			{
				return;
			}
			for (int i = 0; i < this.strCapcha.Length; i++)
			{
				this.keyCapcha[i] = -1;
				if (GameCanvas.isTouchControl)
				{
					int num = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2;
					int w = this.strCapcha.Length * GameScr.disXC;
					int y = GameCanvas.h - 40;
					int h = GameScr.disXC;
					if (GameCanvas.isPointerHoldIn(num, y, w, h))
					{
						int num2 = (GameCanvas.px - num) / GameScr.disXC;
						if (i == num2)
						{
							this.keyCapcha[i] = 1;
						}
						if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && i == num2)
						{
							char[] array = this.keyInput.ToCharArray();
							MyVector myVector = new MyVector();
							for (int j = 0; j < array.Length; j++)
							{
								myVector.addElement(array[j].ToString() + string.Empty);
							}
							myVector.removeElementAt(0);
							myVector.insertElementAt(this.strCapcha[i].ToString() + string.Empty, myVector.size());
							this.keyInput = string.Empty;
							for (int k = 0; k < myVector.size(); k++)
							{
								this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
							}
							Service.gI().mobCapcha(this.strCapcha[i]);
						}
					}
				}
			}
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x00290974 File Offset: 0x0028EB74
		public bool checkClickToCapcha()
		{
			if (this.mobCapcha == null)
			{
				return false;
			}
			int x = (GameCanvas.w - 5 * GameScr.disXC) / 2;
			int w = 5 * GameScr.disXC;
			int y = GameCanvas.h - 40;
			int h = GameScr.disXC;
			return GameCanvas.isPointerHoldIn(x, y, w, h);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x002909C0 File Offset: 0x0028EBC0
		public void checkMouseChat()
		{
			if (GameCanvas.isMouseFocus(GameScr.xC, GameScr.yC, 34, 34))
			{
				if (!TileMap.isOfflineMap())
				{
					mScreen.keyMouse = 15;
					return;
				}
			}
			else if (GameCanvas.isMouseFocus(GameScr.xHP, GameScr.yHP, 40, 40))
			{
				if (Char.myCharz().statusMe != 14)
				{
					mScreen.keyMouse = 10;
					return;
				}
			}
			else if (GameCanvas.isMouseFocus(GameScr.xF, GameScr.yF, 40, 40))
			{
				if (Char.myCharz().statusMe != 14)
				{
					mScreen.keyMouse = 5;
					return;
				}
			}
			else
			{
				if (this.cmdMenu != null && GameCanvas.isMouseFocus(this.cmdMenu.x, this.cmdMenu.y, this.cmdMenu.w / 2, this.cmdMenu.h))
				{
					mScreen.keyMouse = 1;
					return;
				}
				mScreen.keyMouse = -1;
			}
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x00290A94 File Offset: 0x0028EC94
		private void UpdateKeyTouchControl()
		{
			if (this.isNotPaintTouchControl())
			{
				return;
			}
			mScreen.keyTouch = -1;
			if (ModFunc.GI().showCharsInMap)
			{
				int numY = ModFunc.notifBoss ? 92 : 50;
				MyVector chars = ModFunc.GI().charsInMap;
				for (int i = 0; i < chars.size(); i++)
				{
					Char @char = (Char)chars.elementAt(i);
					if (@char != null)
					{
						if (GameCanvas.isPointerHoldIn(GameCanvas.w - 130, numY, 130, 10) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
						{
							if (Char.myCharz().charFocus == @char)
							{
								ModFunc.GI().MoveTo(@char.cx, @char.cy);
							}
							else
							{
								Char.myCharz().FocusManualTo(@char);
								ModFunc.isLockFocus = true;
							}
							Char.myCharz().currentMovePoint = null;
							GameCanvas.clearAllPointerEvent();
							return;
						}
						numY += 10;
					}
				}
			}
			if (ModFunc.notifBoss)
			{
				int numX = 38;
				for (int j = 0; j < ModFunc.activeBossNotif.size(); j++)
				{
					if (GameCanvas.isPointerHoldIn(GameCanvas.w - 20, numX + 3, 20, 10) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						ShowBoss boss = (ShowBoss)ModFunc.activeBossNotif.elementAt(j);
						ModFunc.GI().GoToBoss(boss.mapID);
						GameCanvas.clearAllPointerEvent();
						return;
					}
					numX += 10;
				}
			}
			if (GameCanvas.isTouchControl)
			{
				if (GameCanvas.isPointerHoldIn(0, 0, 60, 50) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					Command command = Char.myCharz().cmdMenu;
					if (command != null)
					{
						command.performAction();
					}
					Char.myCharz().currentMovePoint = null;
					GameCanvas.clearAllPointerEvent();
					this.flareFindFocus = true;
					this.flareTime = 5;
					return;
				}
				if (Main.isPC)
				{
					this.checkMouseChat();
				}
				if (!TileMap.isOfflineMap() && GameCanvas.isPointerHoldIn(GameScr.xC, GameScr.yC, 34, 34))
				{
					mScreen.keyTouch = 15;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						ChatTextField.gI().startChat(this, string.Empty);
						SoundMn.gI().buttonClick();
						Char.myCharz().currentMovePoint = null;
						GameCanvas.clearAllPointerEvent();
						return;
					}
				}
				if (Char.myCharz().cmdMenu != null && GameCanvas.isPointerHoldIn(Char.myCharz().cmdMenu.x - 17, Char.myCharz().cmdMenu.y - 17, 34, 34))
				{
					mScreen.keyTouch = 20;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.clearAllPointerEvent();
						Char.myCharz().cmdMenu.performAction();
						return;
					}
				}
				this.updateGamePad();
				if (((GameScr.isAnalog != 0) ? GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP, 34, 34) : GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP, 40, 40)) && Char.myCharz().statusMe != 14 && this.mobCapcha == null)
				{
					mScreen.keyTouch = 10;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.keyPressed[10] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
					}
				}
			}
			if (this.mobCapcha != null)
			{
				this.updateKeyTouchCapcha();
			}
			else if (GameScr.isHaveSelectSkill)
			{
				if (this.isCharging())
				{
					return;
				}
				this.keyTouchSkill = -1;
				bool flag = false;
				if (GameScr.onScreenSkill.Length > 5 && (GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[5] - GameScr.wSkill / 2 + 12, GameScr.yS[5] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill)))
				{
					flag = true;
				}
				if (flag || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || (!GameCanvas.isTouchControl && GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, GameScr.wSkill, GameScr.onScreenSkill.Length * GameScr.wSkill)))
				{
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					int num = (GameCanvas.pxLast - (GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12)) / GameScr.wSkill;
					if (flag && GameCanvas.pyLast < GameScr.yS[0])
					{
						num += 5;
					}
					this.keyTouchSkill = num;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
						this.selectedIndexSkill = num;
						if (GameScr.indexSelect < 0)
						{
							GameScr.indexSelect = 0;
						}
						if (!Main.isPC)
						{
							if (this.selectedIndexSkill > GameScr.onScreenSkill.Length - 1)
							{
								this.selectedIndexSkill = GameScr.onScreenSkill.Length - 1;
							}
						}
						else if (this.selectedIndexSkill > GameScr.keySkill.Length - 1)
						{
							this.selectedIndexSkill = GameScr.keySkill.Length - 1;
						}
						Skill skill = Main.isPC ? GameScr.keySkill[this.selectedIndexSkill] : GameScr.onScreenSkill[this.selectedIndexSkill];
						if (skill != null)
						{
							this.doSelectSkill(skill, true);
						}
					}
				}
			}
			if (GameCanvas.isPointerJustRelease)
			{
				if (GameCanvas.keyHold[1] || (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || GameCanvas.keyHold[3]) || GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
				{
					GameCanvas.isPointerJustRelease = false;
				}
				GameCanvas.keyHold[1] = false;
				GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = false;
				GameCanvas.keyHold[3] = false;
				GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = false;
				GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = false;
			}
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x002910B8 File Offset: 0x0028F2B8
		public void setCharJump(int cvx)
		{
			if (Char.myCharz().cx - Char.myCharz().cxSend != 0 || Char.myCharz().cy - Char.myCharz().cySend != 0)
			{
				Service.gI().charMove();
			}
			Char.myCharz().cvy = -10;
			Char.myCharz().cvx = cvx;
			Char.myCharz().statusMe = 3;
			Char.myCharz().cp1 = 0;
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000034B9 File Offset: 0x000016B9
		public void checkCharFocus()
		{
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x0029112C File Offset: 0x0028F32C
		public void updateXoSo()
		{
			if (this.tShow == 0)
			{
				return;
			}
			GameScr.currXS = mSystem.currentTimeMillis();
			if (GameScr.currXS - GameScr.lastXS > 1000L)
			{
				GameScr.lastXS = mSystem.currentTimeMillis();
				GameScr.secondXS++;
			}
			if (GameScr.secondXS > 20)
			{
				for (int i = 0; i < this.winnumber.Length; i++)
				{
					this.randomNumber[i] = this.winnumber[i];
				}
				this.tShow--;
				if (this.tShow == 0)
				{
					this.yourNumber = string.Empty;
					GameScr.info1.addInfo(this.strFinish, 0);
					GameScr.secondXS = 0;
				}
				return;
			}
			if (this.moveIndex > this.winnumber.Length - 1)
			{
				this.tShow--;
				if (this.tShow == 0)
				{
					this.yourNumber = string.Empty;
					GameScr.info1.addInfo(this.strFinish, 0);
				}
				return;
			}
			if (this.moveIndex < this.randomNumber.Length)
			{
				if (this.tMove[this.moveIndex] == 15)
				{
					if (this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex] - 1)
					{
						this.delayMove[this.moveIndex] = 10;
					}
					if (this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex])
					{
						this.tMove[this.moveIndex] = -1;
						this.moveIndex++;
					}
				}
				else if (GameCanvas.gameTick % 5 == 0)
				{
					this.tMove[this.moveIndex]++;
				}
			}
			for (int j = 0; j < this.winnumber.Length; j++)
			{
				if (this.tMove[j] != -1)
				{
					this.moveCount[j]++;
					if (this.moveCount[j] > this.tMove[j] + this.delayMove[j])
					{
						this.moveCount[j] = 0;
						this.randomNumber[j]++;
						if (this.randomNumber[j] >= 10)
						{
							this.randomNumber[j] = 0;
						}
					}
				}
			}
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x00291344 File Offset: 0x0028F544
		public override void update()
		{
			if (ModFunc.activeBossNotif.size() > 0 || ModFunc.killedBossNotif.size() > 0)
			{
				ShowBoss.UpdateNotifications();
			}
			if (ModFunc.GI().canUpdate)
			{
				AutoXmap.Update();
				ModFunc.GI().Update();
			}
			if (!AutoXmap.IsXmapRunning)
			{
				PickMob.Update();
			}
			if (GameCanvas.keyPressed[16])
			{
				GameCanvas.keyPressed[16] = false;
				Char.myCharz().findNextFocusByKey();
			}
			if (GameCanvas.keyPressed[13] && !GameCanvas.panel.isShow)
			{
				GameCanvas.keyPressed[13] = false;
				Char.myCharz().findNextFocusByKey();
			}
			if (GameCanvas.keyPressed[17])
			{
				GameCanvas.keyPressed[17] = false;
				Char.myCharz().searchItem();
				if (Char.myCharz().itemFocus != null)
				{
					this.pickItem();
				}
			}
			if (GameCanvas.gameTick % 100 == 0 && TileMap.mapID == 137)
			{
				GameScr.shock_scr = 30;
			}
			if (GameScr.isAutoPlay && GameCanvas.gameTick % 20 == 0)
			{
				this.autoPlay();
			}
			this.updateXoSo();
			mSystem.checkAdComlete();
			SmallImage.update();
			try
			{
				if (LoginScr.isContinueToLogin)
				{
					LoginScr.isContinueToLogin = false;
				}
				if (GameScr.tickMove == 1)
				{
					GameScr.lastTick = mSystem.currentTimeMillis();
				}
				if (GameScr.tickMove == 100)
				{
					GameScr.tickMove = 0;
					GameScr.currTick = mSystem.currentTimeMillis();
					int second = (int)(GameScr.currTick - GameScr.lastTick) / 1000;
					Service.gI().checkMMove(second);
				}
				if (GameScr.lockTick > 0)
				{
					GameScr.lockTick--;
					if (GameScr.lockTick == 0)
					{
						Controller.isStopReadMessage = false;
					}
				}
				this.checkCharFocus();
				GameCanvas.debug("E1", 0);
				GameScr.updateCamera();
				GameCanvas.debug("E2", 0);
				ChatTextField.gI().update();
				GameCanvas.debug("E3", 0);
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					((Char)GameScr.vCharInMap.elementAt(i)).update();
				}
				for (int j = 0; j < Teleport.vTeleport.size(); j++)
				{
					((Teleport)Teleport.vTeleport.elementAt(j)).update();
				}
				Char.myCharz().update();
				int statusMe = Char.myCharz().statusMe;
				if (this.popUpYesNo != null)
				{
					this.popUpYesNo.update();
				}
				EffecMn.update();
				GameCanvas.debug("E5x", 0);
				for (int k = 0; k < GameScr.vMob.size(); k++)
				{
					((Mob)GameScr.vMob.elementAt(k)).update();
				}
				GameCanvas.debug("E6", 0);
				for (int l = 0; l < GameScr.vNpc.size(); l++)
				{
					((Npc)GameScr.vNpc.elementAt(l)).update();
				}
				this.nSkill = GameScr.onScreenSkill.Length;
				for (int i2 = GameScr.onScreenSkill.Length - 1; i2 >= 0; i2--)
				{
					if (GameScr.onScreenSkill[i2] != null)
					{
						this.nSkill = i2 + 1;
						break;
					}
					this.nSkill--;
				}
				if (this.nSkill == 1 && GameCanvas.isTouch)
				{
					GameScr.xSkill = -200;
				}
				else if (GameScr.xSkill < 0)
				{
					GameScr.setSkillBarPosition();
				}
				GameCanvas.debug("E7", 0);
				GameCanvas.gI().updateDust();
				GameCanvas.debug("E8", 0);
				GameScr.updateFlyText();
				PopUp.updateAll();
				GameScr.updateSplash();
				this.updateSS();
				GameCanvas.updateBG();
				GameCanvas.debug("E9", 0);
				this.updateClickToArrow();
				GameCanvas.debug("E10", 0);
				for (int m = 0; m < GameScr.vItemMap.size(); m++)
				{
					((ItemMap)GameScr.vItemMap.elementAt(m)).update();
				}
				GameCanvas.debug("E11", 0);
				GameCanvas.debug("E13", 0);
				for (int i3 = Effect2.vRemoveEffect2.size() - 1; i3 >= 0; i3--)
				{
					Effect2.vEffect2.removeElement(Effect2.vRemoveEffect2.elementAt(i3));
					Effect2.vRemoveEffect2.removeElementAt(i3);
				}
				for (int n = 0; n < Effect2.vEffect2.size(); n++)
				{
					((Effect2)Effect2.vEffect2.elementAt(n)).update();
				}
				for (int num = 0; num < Effect2.vEffect2Outside.size(); num++)
				{
					((Effect2)Effect2.vEffect2Outside.elementAt(num)).update();
				}
				for (int num2 = 0; num2 < Effect2.vAnimateEffect.size(); num2++)
				{
					((Effect2)Effect2.vAnimateEffect.elementAt(num2)).update();
				}
				for (int num3 = 0; num3 < Effect2.vEffectFeet.size(); num3++)
				{
					((Effect2)Effect2.vEffectFeet.elementAt(num3)).update();
				}
				for (int num4 = 0; num4 < Effect2.vEffect3.size(); num4++)
				{
					((Effect2)Effect2.vEffect3.elementAt(num4)).update();
				}
				BackgroudEffect.updateEff();
				GameScr.info1.update();
				GameScr.info2.update();
				GameCanvas.debug("E15", 0);
				if (GameScr.currentCharViewInfo != null && !GameScr.currentCharViewInfo.Equals(Char.myCharz()))
				{
					GameScr.currentCharViewInfo.update();
				}
				this.runArrow++;
				if (this.runArrow > 3)
				{
					this.runArrow = 0;
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
				else if (this.dHP > Char.myCharz().cHP)
				{
					long num5 = (this.dHP - Char.myCharz().cHP) / 2L;
					if (num5 < 1L)
					{
						num5 = 1L;
					}
					this.dHP -= num5;
				}
				else
				{
					this.dHP = Char.myCharz().cHP;
				}
				if (this.isInjureMp)
				{
					this.twMp++;
					if (this.twMp == 20)
					{
						this.twMp = 0;
						this.isInjureMp = false;
					}
				}
				else if (this.dMP > Char.myCharz().cMP)
				{
					long num6 = (this.dMP - Char.myCharz().cMP) / 2L;
					if (num6 < 1L)
					{
						num6 = 1L;
					}
					this.dMP -= num6;
				}
				else
				{
					this.dMP = Char.myCharz().cMP;
				}
				if (this.tMenuDelay > 0)
				{
					this.tMenuDelay--;
				}
				if (this.isRongThanMenu())
				{
					int num7 = 100;
					while (this.yR - num7 < GameScr.cmy)
					{
						GameScr.cmy--;
					}
				}
				for (int num8 = 0; num8 < Char.vItemTime.size(); num8++)
				{
					((ItemTime)Char.vItemTime.elementAt(num8)).update();
				}
				for (int num9 = 0; num9 < GameScr.textTime.size(); num9++)
				{
					((ItemTime)GameScr.textTime.elementAt(num9)).update();
				}
				this.updateChatVip();
			}
			catch (Exception)
			{
			}
			if (GameCanvas.gameTick % 4000 == 1000)
			{
				GameScr.checkRemoveImage();
			}
			EffectManager.update();
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x00291A70 File Offset: 0x0028FC70
		public bool isRongThanMenu()
		{
			return this.isMeCallRongThan;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x00291A80 File Offset: 0x0028FC80
		public void paintEffect(mGraphics g)
		{
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				if (effect != null && !(effect is ChatPopup))
				{
					effect.paint(g);
				}
			}
			if (!GameCanvas.lowGraphic)
			{
				for (int j = 0; j < Effect2.vAnimateEffect.size(); j++)
				{
					((Effect2)Effect2.vAnimateEffect.elementAt(j)).paint(g);
				}
			}
			for (int k = 0; k < Effect2.vEffect2Outside.size(); k++)
			{
				((Effect2)Effect2.vEffect2Outside.elementAt(k)).paint(g);
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00291B24 File Offset: 0x0028FD24
		public void paintBgItem(mGraphics g, int layer)
		{
			for (int i = 0; i < TileMap.vCurrItem.size(); i++)
			{
				BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
				if (bgItem.idImage != -1 && (int)bgItem.layer == layer)
				{
					bgItem.paint(g);
				}
			}
			if (TileMap.mapID == 48 && layer == 3 && GameCanvas.bgW != null && GameCanvas.bgW[0] != 0)
			{
				for (int j = 0; j < TileMap.pxw / GameCanvas.bgW[0] + 1; j++)
				{
					g.drawImage(GameCanvas.imgBG[0], j * GameCanvas.bgW[0], TileMap.pxh - GameCanvas.bgH[0] - 70, 0);
				}
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x00291BCE File Offset: 0x0028FDCE
		public void paintBlackSky(mGraphics g)
		{
			if (!GameCanvas.lowGraphic)
			{
				g.fillTrans(GameScr.imgTrans, 0, 0, GameCanvas.w, GameCanvas.h);
			}
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x00291BF0 File Offset: 0x0028FDF0
		public void paintCapcha(mGraphics g)
		{
			MobCapcha.paint(g, Char.myCharz().cx, Char.myCharz().cy);
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			if (GameCanvas.menu.showMenu || GameCanvas.panel.isShow || ChatPopup.currChatPopup != null || !GameCanvas.isTouch)
			{
				return;
			}
			for (int i = 0; i < this.strCapcha.Length; i++)
			{
				int x = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2 + i * GameScr.disXC + GameScr.disXC / 2;
				if (this.keyCapcha[i] == -1)
				{
					g.drawImage(GameScr.imgNut, x, GameCanvas.h - 25, 3);
					mFont.tahoma_7b_dark.drawString(g, this.strCapcha[i].ToString() + string.Empty, x, GameCanvas.h - 30, 2);
				}
				else
				{
					g.drawImage(GameScr.imgNutF, x, GameCanvas.h - 25, 3);
					mFont.tahoma_7b_green2.drawString(g, this.strCapcha[i].ToString() + string.Empty, x, GameCanvas.h - 30, 2);
				}
			}
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00291D38 File Offset: 0x0028FF38
		public override void paint(mGraphics g)
		{
			GameScr.countEff = 0;
			if (!GameScr.isPaint)
			{
				return;
			}
			if (this.isFreez || (this.isUseFreez && ChatPopup.currChatPopup == null))
			{
				this.dem++;
				if ((this.dem < 30 && this.dem >= 0 && GameCanvas.gameTick % 4 == 0) || (this.dem >= 30 && this.dem <= 50 && GameCanvas.gameTick % 3 == 0) || this.dem > 50)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					if (this.dem <= 50)
					{
						return;
					}
					if (this.isUseFreez)
					{
						this.isUseFreez = false;
						this.dem = 0;
						if (this.activeRongThan)
						{
							this.callRongThan(this.xR, this.yR);
						}
						else
						{
							this.hideRongThan();
						}
					}
					this.paintInfoBar(g);
					g.translate(-GameScr.cmx, -GameScr.cmy);
					g.translate(0, GameCanvas.transY);
					Char.myCharz().paint(g);
					mSystem.paintFlyText(g);
					GameScr.resetTranslate(g);
					this.paintSelectedSkill(g);
					return;
				}
			}
			GameCanvas.paintBGGameScr(g);
			this.paint_ios_bg(g);
			if ((this.isRongThanXuatHien || this.isFireWorks) && TileMap.bgID != 3)
			{
				this.paintBlackSky(g);
			}
			GameCanvas.debug("PA3", 1);
			if (GameScr.shock_scr > 0)
			{
				g.translate(-GameScr.cmx + GameScr.shock_x[GameScr.shock_scr % GameScr.shock_x.Length], -GameScr.cmy + GameScr.shock_y[GameScr.shock_scr % GameScr.shock_y.Length]);
				GameScr.shock_scr--;
			}
			else
			{
				g.translate(-GameScr.cmx, -GameScr.cmy);
			}
			if (this.isSuperPower)
			{
				int tx = (GameCanvas.gameTick % 3 != 0) ? -3 : 3;
				g.translate(tx, 0);
			}
			BackgroudEffect.paintBehindTileAll(g);
			EffecMn.paintLayer1(g);
			TileMap.paintTilemap(g);
			TileMap.paintOutTilemap(g);
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				if (@char.isMabuHold && TileMap.mapID == 128)
				{
					@char.paintHeadWithXY(g, @char.cx, @char.cy, 0);
				}
			}
			if (Char.myCharz().isMabuHold && TileMap.mapID == 128)
			{
				Char.myCharz().paintHeadWithXY(g, Char.myCharz().cx, Char.myCharz().cy, 0);
			}
			this.paintBgItem(g, 2);
			if (Char.myCharz().cmdMenu != null && GameCanvas.isTouch)
			{
				if (mScreen.keyTouch == 20)
				{
					g.drawImage(GameScr.imgChat2, Char.myCharz().cmdMenu.x + GameScr.cmx, Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				else
				{
					g.drawImage(GameScr.imgChat, Char.myCharz().cmdMenu.x + GameScr.cmx, Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
				}
			}
			GameCanvas.debug("PA4", 1);
			GameCanvas.debug("PA5", 1);
			BackgroudEffect.paintBackAll(g);
			EffectManager.lowEffects.paintAll(g);
			for (int j = 0; j < Effect2.vEffectFeet.size(); j++)
			{
				((Effect2)Effect2.vEffectFeet.elementAt(j)).paint(g);
			}
			for (int k = 0; k < Teleport.vTeleport.size(); k++)
			{
				((Teleport)Teleport.vTeleport.elementAt(k)).paintHole(g);
			}
			for (int l = 0; l < GameScr.vNpc.size(); l++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(l);
				if (npc.cHP > 0L)
				{
					npc.paintShadow(g);
				}
			}
			for (int m = 0; m < GameScr.vNpc.size(); m++)
			{
				((Npc)GameScr.vNpc.elementAt(m)).paint(g);
			}
			g.translate(0, GameCanvas.transY);
			GameCanvas.debug("PA7", 1);
			GameCanvas.debug("PA8", 1);
			for (int n = 0; n < GameScr.vCharInMap.size(); n++)
			{
				Char char2 = null;
				try
				{
					char2 = (Char)GameScr.vCharInMap.elementAt(n);
				}
				catch (Exception ex)
				{
					Cout.LogError("Loi ham paint char gamesc: " + ex.ToString());
				}
				if (char2 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()) && char2.isShadown)
				{
					char2.paintShadow(g);
				}
			}
			Char.myCharz().paintShadow(g);
			EffecMn.paintLayer2(g);
			for (int num = 0; num < GameScr.vMob.size(); num++)
			{
				((Mob)GameScr.vMob.elementAt(num)).paint(g);
			}
			for (int num2 = 0; num2 < Teleport.vTeleport.size(); num2++)
			{
				((Teleport)Teleport.vTeleport.elementAt(num2)).paint(g);
			}
			for (int num3 = 0; num3 < GameScr.vCharInMap.size(); num3++)
			{
				Char char3 = null;
				try
				{
					char3 = (Char)GameScr.vCharInMap.elementAt(num3);
				}
				catch (Exception)
				{
				}
				if (char3 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()))
				{
					char3.paint(g);
				}
			}
			Char.myCharz().paint(g);
			if (Char.myCharz().skillPaint != null && Char.myCharz().skillInfoPaint() != null && Char.myCharz().indexSkill < Char.myCharz().skillInfoPaint().Length)
			{
				Char.myCharz().paintCharWithSkill(g);
				Char.myCharz().paintMount2(g);
			}
			for (int num4 = 0; num4 < GameScr.vCharInMap.size(); num4++)
			{
				Char char4 = null;
				try
				{
					char4 = (Char)GameScr.vCharInMap.elementAt(num4);
				}
				catch (Exception ex2)
				{
					Cout.LogError("Loi ham paint char gamescr: " + ex2.ToString());
				}
				if (char4 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()) && char4.skillPaint != null && char4.skillInfoPaint() != null && char4.indexSkill < char4.skillInfoPaint().Length)
				{
					char4.paintCharWithSkill(g);
					char4.paintMount2(g);
				}
			}
			for (int num5 = 0; num5 < GameScr.vItemMap.size(); num5++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(num5)).paint(g);
			}
			g.translate(0, -GameCanvas.transY);
			GameCanvas.debug("PA9", 1);
			GameScr.paintSplash(g);
			GameCanvas.debug("PA10", 1);
			GameCanvas.debug("PA11", 1);
			GameCanvas.debug("PA13", 1);
			this.paintEffect(g);
			this.paintBgItem(g, 3);
			for (int num6 = 0; num6 < GameScr.vNpc.size(); num6++)
			{
				((Npc)GameScr.vNpc.elementAt(num6)).paintName(g);
			}
			EffecMn.paintLayer3(g);
			for (int num7 = 0; num7 < GameScr.vNpc.size(); num7++)
			{
				Npc npc2 = (Npc)GameScr.vNpc.elementAt(num7);
				if (npc2.chatInfo != null && npc2 != null)
				{
					npc2.chatInfo.paint(g, npc2.cx, npc2.cy - npc2.ch - GameCanvas.transY, npc2.cdir);
				}
			}
			for (int num8 = 0; num8 < GameScr.vCharInMap.size(); num8++)
			{
				Char char5 = null;
				try
				{
					char5 = (Char)GameScr.vCharInMap.elementAt(num8);
				}
				catch (Exception)
				{
				}
				if (char5 != null && char5.chatInfo != null)
				{
					char5.chatInfo.paint(g, char5.cx, char5.cy - char5.ch, char5.cdir);
				}
			}
			if (Char.myCharz().chatInfo != null)
			{
				Char.myCharz().chatInfo.paint(g, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, Char.myCharz().cdir);
			}
			EffectManager.mid_2Effects.paintAll(g);
			EffectManager.midEffects.paintAll(g);
			BackgroudEffect.paintFrontAll(g);
			for (int num9 = 0; num9 < TileMap.vCurrItem.size(); num9++)
			{
				BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(num9);
				if (bgItem.idImage != -1 && bgItem.layer > 3)
				{
					bgItem.paint(g);
				}
			}
			PopUp.paintAll(g);
			if (TileMap.mapID == 120)
			{
				if (this.percentMabu != 100)
				{
					int w = (int)this.percentMabu * mGraphics.getImageWidth(GameScr.imgHPLost) / 100;
					sbyte b = this.percentMabu;
					g.drawImage(GameScr.imgHPLost, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
					g.setClip(TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, w, 10);
					g.drawImage(GameScr.imgHP, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
					g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				}
				if (this.mabuEff)
				{
					this.tMabuEff++;
					if (GameCanvas.gameTick % 3 == 0)
					{
						EffecMn.addEff(new Effect(19, Res.random(TileMap.pxw / 2 - 50, TileMap.pxw / 2 + 50), 340, 2, 1, -1));
					}
					if (GameCanvas.gameTick % 15 == 0)
					{
						EffecMn.addEff(new Effect(18, Res.random(TileMap.pxw / 2 - 5, TileMap.pxw / 2 + 5), Res.random(300, 320), 2, 1, -1));
					}
					if (this.tMabuEff == 100)
					{
						this.activeSuperPower(TileMap.pxw / 2, 300);
					}
					if (this.tMabuEff == 110)
					{
						this.tMabuEff = 0;
						this.mabuEff = false;
					}
				}
			}
			BackgroudEffect.paintFog(g);
			bool flag = true;
			for (int num10 = 0; num10 < BackgroudEffect.vBgEffect.size(); num10++)
			{
				if (((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(num10)).typeEff == 0)
				{
					flag = false;
					break;
				}
			}
			if (mGraphics.zoomLevel <= 1 || Main.isIpod || Main.isIphone4)
			{
				flag = false;
			}
			if (flag && !this.isRongThanXuatHien)
			{
				int num11 = TileMap.pxw / (mGraphics.getImageWidth(TileMap.imgLight) + 50);
				if (num11 <= 0)
				{
					num11 = 1;
				}
				if (TileMap.tileID != 28)
				{
					for (int num12 = 0; num12 < num11; num12++)
					{
						int num13 = 100 + num12 * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - GameScr.cmx / 2;
						int num14 = -20;
						int imageWidth = mGraphics.getImageWidth(TileMap.imgLight);
						if (num13 + imageWidth >= GameScr.cmx && num13 <= GameScr.cmx + GameCanvas.w && num14 + mGraphics.getImageHeight(TileMap.imgLight) >= GameScr.cmy && num14 <= GameScr.cmy + GameCanvas.h)
						{
							g.drawImage(TileMap.imgLight, 100 + num12 * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - GameScr.cmx / 2, num14, 0);
						}
					}
				}
			}
			mSystem.paintFlyText(g);
			GameCanvas.debug("PA14", 1);
			GameCanvas.debug("PA15", 1);
			GameCanvas.debug("PA16", 1);
			this.paintArrowPointToNPC(g);
			GameCanvas.debug("PA17", 1);
			if (!GameScr.isPaintOther && GameScr.isPaintRada == 1 && !GameCanvas.panel.isShow)
			{
				this.paintInfoBar(g);
			}
			GameScr.resetTranslate(g);
			this.paint_xp_bar(g);
			if (!GameScr.isPaintOther)
			{
				ModFunc.GI().Paint(g);
				g.translate(-g.getTranslateX(), -g.getTranslateY());
				if ((TileMap.mapID == 128 || TileMap.mapID == 127) && GameScr.mabuPercent != 0)
				{
					int num15 = 30;
					int num16 = 200;
					g.setColor(0);
					g.fillRect(num15 - 27, num16 - 112, 54, 8);
					g.setColor(16711680);
					g.setClip(num15 - 25, num16 - 110, (int)GameScr.mabuPercent, 4);
					g.fillRect(num15 - 25, num16 - 110, 50, 4);
					g.setClip(0, 0, 3000, 3000);
					mFont.tahoma_7b_white.drawString(g, "Mabu", num15, num16 - 112 + 10, 2, mFont.tahoma_7b_dark);
				}
				if (Char.myCharz().isFusion)
				{
					Char.myCharz().tFusion++;
					if (GameCanvas.gameTick % 3 == 0)
					{
						g.setColor(16777215);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					}
					if (Char.myCharz().tFusion >= 100)
					{
						Char.myCharz().fusionComplete();
					}
				}
				for (int num17 = 0; num17 < GameScr.vCharInMap.size(); num17++)
				{
					Char char6 = null;
					try
					{
						char6 = (Char)GameScr.vCharInMap.elementAt(num17);
					}
					catch (Exception)
					{
					}
					if (char6 != null && char6.isFusion && Char.isCharInScreen(char6))
					{
						char6.tFusion++;
						if (GameCanvas.gameTick % 3 == 0)
						{
							g.setColor(16777215);
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						}
						if (char6.tFusion >= 100)
						{
							char6.fusionComplete();
						}
					}
				}
				GameCanvas.paintz.paintTabSoft(g);
				GameCanvas.debug("PA19", 1);
				GameCanvas.debug("PA20", 1);
				GameScr.resetTranslate(g);
				this.paintSelectedSkill(g);
				GameCanvas.debug("PA22", 1);
				GameScr.resetTranslate(g);
				if (GameCanvas.isTouch && GameCanvas.isTouchControl)
				{
					this.paintTouchControl(g);
				}
				GameScr.resetTranslate(g);
				this.paintChatVip(g);
				if (!GameCanvas.panel.isShow && GameCanvas.currentDialog == null && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null && GameCanvas.currentScreen.Equals(GameScr.instance))
				{
					base.paint(g);
					if (mScreen.keyMouse == 1 && this.cmdMenu != null)
					{
						g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 7, this.cmdMenu.y + 15, 3);
					}
				}
				GameScr.resetTranslate(g);
				int num18 = 100 + ((Char.vItemTime.size() != 0) ? (GameScr.textTime.size() * 12) : 0);
				if (Char.myCharz().clan != null)
				{
					int num19 = 0;
					int num20 = 0;
					int num21 = (GameCanvas.h - 100 - 60) / 12;
					for (int num22 = 0; num22 < GameScr.vCharInMap.size(); num22++)
					{
						Char char7 = (Char)GameScr.vCharInMap.elementAt(num22);
						if (char7.clanID != -1 && char7.clanID == Char.myCharz().clan.ID)
						{
							if (char7.isOutX() && char7.cx < Char.myCharz().cx)
							{
								int num23 = num21;
								if (Char.vItemTime.size() != 0)
								{
									num23 -= GameScr.textTime.size();
								}
								if (num19 <= num23)
								{
									mFont.tahoma_7_green.drawString(g, char7.cName, 20, num18 - 12 + num19 * 12, mFont.LEFT, mFont.tahoma_7_grey);
									char7.paintHp(g, 10, num18 + num19 * 12 - 5);
									num19++;
								}
							}
							else if (char7.isOutX() && char7.cx > Char.myCharz().cx && num20 <= num21)
							{
								mFont.tahoma_7_green.drawString(g, char7.cName, GameCanvas.w - 25, num18 - 12 + num20 * 12, mFont.RIGHT, mFont.tahoma_7_grey);
								char7.paintHp(g, GameCanvas.w - 15, num18 + num20 * 12 - 5);
								num20++;
							}
						}
					}
				}
				ChatTextField.gI().paint(g);
				NewBagUI.GI().Paint(g);
				if (GameScr.isNewClanMessage && !GameCanvas.panel.isShow && GameCanvas.gameTick % 4 == 0)
				{
					g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 15, this.cmdMenu.y + 30, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				if (this.isSuperPower)
				{
					this.dxPower += 5;
					if (this.tPower >= 0)
					{
						this.tPower += this.dxPower;
					}
					Res.outz("x power= " + this.xPower.ToString());
					if (this.tPower < 0)
					{
						this.tPower--;
						if (this.tPower == -20)
						{
							this.isSuperPower = false;
							this.tPower = 0;
							this.dxPower = 0;
						}
					}
					else if ((this.xPower - this.tPower > 0 || this.tPower < TileMap.pxw) && this.tPower > 0)
					{
						g.setColor(16777215);
						if (!GameCanvas.lowGraphic)
						{
							g.fillArg(0, 0, GameCanvas.w, GameCanvas.h, 0, 0);
						}
						else
						{
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						}
					}
					else
					{
						this.tPower = -1;
					}
				}
				for (int num24 = 0; num24 < Char.vItemTime.size(); num24++)
				{
					((ItemTime)Char.vItemTime.elementAt(num24)).paint(g, this.cmdMenu.x + 32 + num24 * 24, 55);
				}
				for (int num25 = 0; num25 < GameScr.textTime.size(); num25++)
				{
					((ItemTime)GameScr.textTime.elementAt(num25)).paintText(g, this.cmdMenu.x + ((Char.vItemTime.size() == 0) ? 25 : 5), ((Char.vItemTime.size() == 0) ? 45 : 90) + num25 * 12);
				}
				this.paintXoSo(g);
				if (mResources.language == 1)
				{
					long second = mSystem.currentTimeMillis() - GameScr.deltaTime;
					mFont.tahoma_7b_white.drawString(g, NinjaUtil.getDate2(second), 10, GameCanvas.h - 65, 0, mFont.tahoma_7b_dark);
				}
				if (!this.yourNumber.Equals(string.Empty))
				{
					for (int num26 = 0; num26 < this.strPaint.Length; num26++)
					{
						mFont.tahoma_7b_white.drawString(g, this.strPaint[num26], 5, 85 + num26 * 18, 0, mFont.tahoma_7b_dark);
					}
				}
			}
			int num27 = 0;
			int num28 = GameCanvas.hw;
			if (num28 > 200)
			{
				num28 = 200;
			}
			this.paintPhuBanBar(g, num27 + GameCanvas.w / 2, 0, num28);
			EffectManager.hiEffects.paintAll(g);
			AdminPopup.gI().paint(g);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x00293054 File Offset: 0x00291254
		private void paintXoSo(mGraphics g)
		{
			if (this.tShow != 0)
			{
				string text = string.Empty;
				for (int i = 0; i < this.winnumber.Length; i++)
				{
					text = text + this.randomNumber[i].ToString() + " ";
				}
				PopUp.paintPopUp(g, 20, 45, 95, 35, 16777215, false);
				mFont.tahoma_7b_dark.drawString(g, mResources.kquaVongQuay, 68, 50, 2);
				mFont.tahoma_7b_dark.drawString(g, text + string.Empty, 68, 65, 2);
			}
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x002930E4 File Offset: 0x002912E4
		public void checkEffToObj(IMapObject obj, bool isnew)
		{
			if (obj == null || this.tDoubleDelay > 0)
			{
				return;
			}
			this.tDoubleDelay = 10;
			int x = obj.getX();
			int num2 = Res.abs(Char.myCharz().cx - x);
			int num3 = (num2 <= 80) ? 1 : ((num2 > 80 && num2 <= 200) ? 2 : ((num2 <= 200 || num2 > 400) ? 4 : 3));
			if (!isnew)
			{
				if (obj.Equals(Char.myCharz().mobFocus) || (obj.Equals(Char.myCharz().charFocus) && Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus)))
				{
					ServerEffect.addServerEffect(135, obj.getX(), obj.getY(), num3);
					return;
				}
				if (obj.Equals(Char.myCharz().npcFocus) || obj.Equals(Char.myCharz().itemFocus) || obj.Equals(Char.myCharz().charFocus))
				{
					ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), num3);
					return;
				}
			}
			else
			{
				ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), num3);
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0029320C File Offset: 0x0029140C
		private void updateClickToArrow()
		{
			if (this.tDoubleDelay > 0)
			{
				this.tDoubleDelay--;
			}
			if (this.clickMoving)
			{
				this.clickMoving = false;
				IMapObject mapObject = this.findClickToItem(this.clickToX, this.clickToY);
				if (mapObject == null || (mapObject != null && mapObject.Equals(Char.myCharz().npcFocus) && TileMap.mapID == 51))
				{
					ServerEffect.addServerEffect(134, this.clickToX, this.clickToY + GameCanvas.transY / 2, 3);
				}
			}
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00293294 File Offset: 0x00291494
		public static Npc findNPCInMap(short id)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == (int)id)
				{
					return npc;
				}
			}
			return null;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x002932D8 File Offset: 0x002914D8
		public static Char findCharInMap(int charId)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				if (@char.charID == charId)
				{
					return @char;
				}
			}
			return null;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00293317 File Offset: 0x00291517
		public static Mob findMobInMap(sbyte mobIndex)
		{
			return (Mob)GameScr.vMob.elementAt((int)mobIndex);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x0029332C File Offset: 0x0029152C
		public static Mob findMobInMap(int mobId)
		{
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (mob.mobId == mobId)
				{
					return mob;
				}
			}
			return null;
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x0029336C File Offset: 0x0029156C
		public static Npc getNpcTask()
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == (int)GameScr.getTaskNpcId())
				{
					return npc;
				}
			}
			return null;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x002933B4 File Offset: 0x002915B4
		private void paintArrowPointToNPC(mGraphics g)
		{
			try
			{
				if (ChatPopup.currChatPopup == null)
				{
					int num = (int)GameScr.getTaskNpcId();
					if (num != -1)
					{
						Npc npc = null;
						for (int i = 0; i < GameScr.vNpc.size(); i++)
						{
							Npc npc2 = (Npc)GameScr.vNpc.elementAt(i);
							if (npc2.template.npcTemplateId == num)
							{
								if (npc == null)
								{
									npc = npc2;
								}
								else if (Res.abs(npc2.cx - Char.myCharz().cx) < Res.abs(npc.cx - Char.myCharz().cx))
								{
									npc = npc2;
								}
							}
						}
						if (npc != null && npc.statusMe != 15 && (npc.cx <= GameScr.cmx || npc.cx >= GameScr.cmx + GameScr.gW || npc.cy <= GameScr.cmy || npc.cy >= GameScr.cmy + GameScr.gH) && GameCanvas.gameTick % 10 >= 5)
						{
							int num2 = npc.cx - Char.myCharz().cx;
							int num3 = npc.cy - Char.myCharz().cy;
							int x = 0;
							int y = 0;
							int arg = 0;
							if (num2 > 0 && num3 >= 0)
							{
								if (Res.abs(num2) >= Res.abs(num3))
								{
									x = GameScr.gW - 10;
									y = GameScr.gH / 2 + 30;
									if (GameCanvas.isTouch)
									{
										y = GameScr.gH / 2 + 10;
									}
									arg = 0;
								}
								else
								{
									x = GameScr.gW / 2;
									y = GameScr.gH - 10;
									arg = 5;
								}
							}
							else if (num2 >= 0 && num3 < 0)
							{
								if (Res.abs(num2) >= Res.abs(num3))
								{
									x = GameScr.gW - 10;
									y = GameScr.gH / 2 + 30;
									if (GameCanvas.isTouch)
									{
										y = GameScr.gH / 2 + 10;
									}
									arg = 0;
								}
								else
								{
									x = GameScr.gW / 2;
									y = 10;
									arg = 6;
								}
							}
							if (num2 < 0 && num3 >= 0)
							{
								if (Res.abs(num2) >= Res.abs(num3))
								{
									x = 10;
									y = GameScr.gH / 2 + 30;
									if (GameCanvas.isTouch)
									{
										y = GameScr.gH / 2 + 10;
									}
									arg = 3;
								}
								else
								{
									x = GameScr.gW / 2;
									y = GameScr.gH - 10;
									arg = 5;
								}
							}
							else if (num2 <= 0 && num3 < 0)
							{
								if (Res.abs(num2) >= Res.abs(num3))
								{
									x = 10;
									y = GameScr.gH / 2 + 30;
									if (GameCanvas.isTouch)
									{
										y = GameScr.gH / 2 + 10;
									}
									arg = 3;
								}
								else
								{
									x = GameScr.gW / 2;
									y = 10;
									arg = 6;
								}
							}
							GameScr.resetTranslate(g);
							g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, x, y, StaticObj.VCENTER_HCENTER);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham arrow to npc: " + ex.ToString());
			}
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0029368C File Offset: 0x0029188C
		public static void resetTranslate(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, -200, GameCanvas.w, 200 + GameCanvas.h);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x002936C0 File Offset: 0x002918C0
		private void paintTouchControl(mGraphics g)
		{
			if (this.isNotPaintTouchControl())
			{
				return;
			}
			GameScr.resetTranslate(g);
			if (!TileMap.isOfflineMap() && !this.isVS())
			{
				if (mScreen.keyTouch == 15 || mScreen.keyMouse == 15)
				{
					g.drawImage(GameScr.imgChat2, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				else
				{
					g.drawImage(GameScr.imgChat, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
				}
			}
			bool flag = GameScr.isUseTouch;
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x00293760 File Offset: 0x00291960
		public void paintImageBarRight(mGraphics g, Char c)
		{
			int num = (int)(c.cHP * GameScr.hpBarW / c.cHPFull);
			int num2 = (int)c.cMP * GameScr.mpBarW;
			int num3 = (int)(this.dHP * GameScr.hpBarW / c.cHPFull);
			int num4 = (int)this.dMP * GameScr.mpBarW;
			g.setClip(GameCanvas.w / 2 + 58 - mGraphics.getImageWidth(GameScr.imgPanel), 0, 95, 100);
			g.drawRegion(GameScr.imgPanel, 0, 0, mGraphics.getImageWidth(GameScr.imgPanel), mGraphics.getImageHeight(GameScr.imgPanel), 2, GameCanvas.w / 2 + 60, 0, mGraphics.RIGHT | mGraphics.TOP);
			g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83) - GameScr.hpBarW + GameScr.hpBarW - (long)num3), 5, num3, 10);
			g.drawImage(GameScr.imgHPLost, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83) - GameScr.hpBarW + GameScr.hpBarW - (long)num), 5, num, 10);
			g.drawImage(GameScr.imgHP, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW) + GameScr.hpBarW - (long)num4), 20, num4, 6);
			g.drawImage(GameScr.imgMPLost, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW) + GameScr.hpBarW - (long)num2), 20, num2, 6);
			g.drawImage(GameScr.imgMP, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x00293988 File Offset: 0x00291B88
		private void paintImageBar(mGraphics g, bool isLeft, Char c)
		{
			if (c == null)
			{
				return;
			}
			int num2;
			int num3;
			int num4;
			int num5;
			if (c.charID == Char.myCharz().charID)
			{
				if (!ModFunc.isReadInt)
				{
					long hpLong = this.dHP * GameScr.hpBarW / c.cHPFull;
					long mpLong = this.dMP * (long)GameScr.mpBarW / c.cMPFull;
					long currHPLong = c.cHP * GameScr.hpBarW / c.cHPFull;
					int num6 = (int)(c.cMP * (long)GameScr.mpBarW / c.cMPFull);
					num2 = (int)hpLong;
					num3 = (int)mpLong;
					num4 = (int)currHPLong;
					num5 = num6;
					if (num2 <= 0)
					{
						num2 = 1;
					}
					if ((long)num2 > GameScr.hpBarW)
					{
						num2 = (int)GameScr.hpBarW;
					}
					if (num3 <= 0)
					{
						num3 = 1;
					}
					if (num3 > GameScr.mpBarW)
					{
						num3 = GameScr.mpBarW;
					}
					if (num4 <= 0)
					{
						num4 = 1;
					}
					if ((long)num4 > GameScr.hpBarW)
					{
						num4 = (int)GameScr.hpBarW;
					}
					if (num5 <= 0)
					{
						num5 = 1;
					}
					if (num5 > GameScr.mpBarW)
					{
						num5 = GameScr.mpBarW;
					}
				}
				else
				{
					num2 = (int)(this.dHP * GameScr.hpBarW / c.cHPFull);
					num3 = (int)(this.dMP * (long)GameScr.mpBarW / c.cMPFull);
					num4 = (int)(c.cHP * GameScr.hpBarW / c.cHPFull);
					num5 = (int)(c.cMP * (long)GameScr.mpBarW / c.cMPFull);
				}
			}
			else
			{
				num2 = (int)(c.dHP * GameScr.hpBarW / c.cHPFull);
				num3 = c.perCentMp * GameScr.mpBarW / 100;
				num4 = (int)(c.cHP * GameScr.hpBarW / c.cHPFull);
				num5 = c.perCentMp * GameScr.mpBarW / 100;
			}
			if (Char.myCharz().secondPower > 0)
			{
				int w = (int)Char.myCharz().powerPoint * GameScr.spBarW / (int)Char.myCharz().maxPowerPoint;
				g.drawImage(GameScr.imgPanel2, 58, 29, 0);
				g.setClip(83, 31, w, 10);
				g.drawImage(GameScr.imgSP, 83, 31, 0);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				mFont.tahoma_7_white.drawString(g, string.Concat(new string[]
				{
					Char.myCharz().strInfo,
					":",
					Char.myCharz().powerPoint.ToString(),
					"/",
					Char.myCharz().maxPowerPoint.ToString()
				}), 115, 29, 2);
			}
			if (c.charID != Char.myCharz().charID)
			{
				g.setClip(mGraphics.getImageWidth(GameScr.imgPanel) - 95, 0, 95, 100);
			}
			g.drawImage(GameScr.imgPanel, 0, 0, 0);
			if (isLeft)
			{
				g.setClip(83, 5, num2, 10);
			}
			else
			{
				g.setClip((int)(83L + GameScr.hpBarW - (long)num2), 5, num2, 10);
			}
			g.drawImage(GameScr.imgHPLost, 83, 5, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 5, num4, 10);
			}
			else
			{
				g.setClip((int)(83L + GameScr.hpBarW - (long)num4), 5, num4, 10);
			}
			g.drawImage(GameScr.imgHP, 83, 5, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 20, num3, 6);
			}
			else
			{
				g.setClip(83 + GameScr.mpBarW - num3, 20, num3, 6);
			}
			g.drawImage(GameScr.imgMPLost, 83, 20, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 20, num3, 6);
			}
			else
			{
				g.setClip(83 + GameScr.mpBarW - num5, 20, num5, 6);
			}
			g.drawImage(GameScr.imgMP, 83, 20, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (Char.myCharz().cMP == 0L && GameCanvas.gameTick % 10 > 5)
			{
				g.setClip(83, 20, 2, 6);
				g.drawImage(GameScr.imgMPLost, 83, 20, 0);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x00293D80 File Offset: 0x00291F80
		public void starVS()
		{
			this.curr = (this.last = mSystem.currentTimeMillis());
			this.secondVS = 180;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00293DAC File Offset: 0x00291FAC
		private Char findCharVS1()
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				if (@char.cTypePk != 0)
				{
					return @char;
				}
			}
			return null;
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00293DEC File Offset: 0x00291FEC
		private Char findCharVS2()
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				if (@char.cTypePk != 0 && @char != this.findCharVS1())
				{
					return @char;
				}
			}
			return null;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x00293E34 File Offset: 0x00292034
		private void paintInfoBar(mGraphics g)
		{
			GameScr.resetTranslate(g);
			if (TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null)
			{
				g.translate(GameCanvas.w / 2 - 62, 0);
				this.paintImageBar(g, true, this.findCharVS1());
				g.translate(-(GameCanvas.w / 2 - 65), 0);
				this.paintImageBarRight(g, this.findCharVS2());
				this.findCharVS1().paintHeadWithXY(g, 137, 25, 0);
				this.findCharVS2().paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
			}
			else if (this.isVS() && Char.myCharz().charFocus != null)
			{
				g.translate(GameCanvas.w / 2 - 62, 0);
				this.paintImageBar(g, true, Char.myCharz().charFocus);
				g.translate(-(GameCanvas.w / 2 - 65), 0);
				this.paintImageBarRight(g, Char.myCharz());
				Char.myCharz().paintHeadWithXY(g, 137, 25, 0);
				Char.myCharz().charFocus.paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
			}
			else if (GameScr.ispaintPhubangBar() && GameScr.isSmallScr())
			{
				GameScr.paintHPBar_NEW(g, 1, 1, Char.myCharz());
			}
			else
			{
				this.paintImageBar(g, true, Char.myCharz());
				if (Char.myCharz().isInEnterOfflinePoint() != null || Char.myCharz().isInEnterOnlinePoint() != null)
				{
					mFont.tahoma_7_green2.drawString(g, mResources.enter, this.imgScrW / 2, 8 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
				else if (Char.myCharz().mobFocus != null)
				{
					if (Char.myCharz().mobFocus.getTemplate() != null)
					{
						mFont.tahoma_7b_green2.drawString(g, Char.myCharz().mobFocus.getTemplate().name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
					}
					if (Char.myCharz().mobFocus.templateId != 0)
					{
						mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(Char.myCharz().mobFocus.hp) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
					}
				}
				else if (Char.myCharz().npcFocus != null)
				{
					mFont.tahoma_7b_green2.drawString(g, Char.myCharz().npcFocus.template.name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
					if (Char.myCharz().npcFocus.template.npcTemplateId == 4)
					{
						mFont.tahoma_7b_green2.drawString(g, GameScr.gI().magicTree.currPeas.ToString() + "/" + GameScr.gI().magicTree.maxPeas.ToString(), this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
					}
				}
				else if (Char.myCharz().charFocus != null)
				{
					mFont.tahoma_7b_green2.drawString(g, Char.myCharz().charFocus.cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
					mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(Char.myCharz().charFocus.cHP) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
				else
				{
					mFont.tahoma_7b_green2.drawString(g, Char.myCharz().cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
					mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(Char.myCharz().cPower) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			if (this.isVS() && this.secondVS > 0)
			{
				this.curr = mSystem.currentTimeMillis();
				if (this.curr - this.last >= 1000L)
				{
					this.last = mSystem.currentTimeMillis();
					this.secondVS--;
				}
				mFont.tahoma_7b_white.drawString(g, this.secondVS.ToString() + string.Empty, GameCanvas.w / 2, 40, 2, mFont.tahoma_7b_dark);
			}
			if (this.flareFindFocus)
			{
				g.drawImage(ItemMap.imageFlare, 40, 35, mGraphics.BOTTOM | mGraphics.HCENTER);
				this.flareTime--;
				if (this.flareTime < 0)
				{
					this.flareTime = 0;
					this.flareFindFocus = false;
				}
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x002942E4 File Offset: 0x002924E4
		public bool isVS()
		{
			return TileMap.isVoDaiMap() && (Char.myCharz().cTypePk != 0 || (TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null));
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x00294318 File Offset: 0x00292518
		private void paintSelectedSkill(mGraphics g)
		{
			if (this.mobCapcha != null)
			{
				this.paintCapcha(g);
				return;
			}
			if (ModFunc.isShowMenuChat || GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || this.isPaintPopup() || GameCanvas.panel.isShow || Char.myCharz().taskMaint.taskId == 0 || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
			{
				return;
			}
			long num2 = mSystem.currentTimeMillis() - this.lastUsePotion;
			int num3 = 0;
			if (num2 < 10000L)
			{
				num3 = (int)(num2 * 20L / 10000L);
			}
			if (!GameCanvas.isTouch)
			{
				g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1, 0);
				SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3, 0, 0);
				mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 15, 1);
				if (num2 < 10000L)
				{
					g.setColor(2721889);
					num3 = (int)(num2 * 20L / 10000L);
					g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num3, 20, 20 - num3);
				}
			}
			else if (Char.myCharz().statusMe != 14)
			{
				if (GameScr.gamePad.isSmallGamePad)
				{
					if (GameScr.isAnalog != 1)
					{
						g.setColor(9670800);
						g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10, 22, 20);
						g.setColor(16777215);
						g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num3 != 0) ? (20 - num3) : 0), 22, (num3 == 0) ? 20 : num3);
						g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP, GameScr.yHP, 0);
						mFont.tahoma_7_green2.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xHP + 20, GameScr.yHP + 15, 2);
					}
					else if (GameScr.isAnalog == 1)
					{
						g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1, 0);
						SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3, 0, 0);
						mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 13, 1);
						if (num2 < 10000L)
						{
							g.setColor(2721889);
							num3 = (int)(num2 * 20L / 10000L);
							g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num3, 20, 20 - num3);
						}
					}
				}
				else if (GameScr.isAnalog != 1)
				{
					g.setColor(9670800);
					g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 - 6, 22, 20);
					g.setColor(16777215);
					g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num3 != 0) ? (20 - num3) : 0) - 6, 22, (num3 == 0) ? 20 : num3);
					g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP + 5, GameScr.yHP - 3, 0);
					mFont.tahoma_7_green2.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xHP + 22, GameScr.yHP + 15 - 6, 2);
				}
				else
				{
					g.setColor(9670800);
					g.fillRect(GameScr.xHP + 10, GameScr.yHP + 10, 20, 18);
					g.setColor(16777215);
					g.fillRect(GameScr.xHP + 10, GameScr.yHP + 16 + ((num3 != 0) ? (20 - num3) : 0) - 6, 20, (num3 == 0) ? 18 : num3);
					g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP3 : GameScr.imgHP4, GameScr.xHP + 20, GameScr.yHP + 20 - 3, mGraphics.HCENTER | mGraphics.VCENTER);
					mFont.tahoma_7_green2.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xHP + 20, GameScr.yHP + 11, 2);
				}
			}
			if (GameScr.isHaveSelectSkill)
			{
				Skill[] array = Main.isPC ? GameScr.keySkill : ((!GameCanvas.isTouch) ? GameScr.keySkill : GameScr.onScreenSkill);
				if (!GameCanvas.isTouch)
				{
					g.setColor(11152401);
					g.fillRect(GameScr.xSkill + GameScr.xHP + 2, GameScr.yHP - 10 + 6, 20, 10);
					mFont.tahoma_7_white.drawString(g, "*", GameScr.xSkill + GameScr.xHP + 12, GameScr.yHP - 8 + 6, mFont.CENTER);
				}
				int num4 = Main.isPC ? array.Length : ((!GameCanvas.isTouch) ? array.Length : this.nSkill);
				for (int i = 0; i < num4; i++)
				{
					Skill skill = array[i];
					if (skill != null)
					{
						if (skill != Char.myCharz().myskill)
						{
							g.drawImage(GameScr.imgSkill, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
						}
						if (skill == Char.myCharz().myskill)
						{
							g.drawImage(GameScr.imgSkill2, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
							if (GameCanvas.isTouch && !Main.isPC)
							{
								g.drawRegion(Mob.imgHP, 0, 12, 9, 6, 0, GameScr.xSkill + GameScr.xS[i] + 8, GameScr.yS[i] - 7, 0);
							}
						}
						skill.paint(GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 13, g);
						if ((i == this.selectedIndexSkill && !this.isPaintUI() && GameCanvas.gameTick % 10 > 5) || i == this.keyTouchSkill)
						{
							g.drawImage(ItemMap.imageFlare, GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 14, 3);
						}
					}
				}
			}
			this.paintGamePad(g);
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x002949D0 File Offset: 0x00292BD0
		public static void startFlyText(string flyString, int x, int y, int dx, int dy, int color)
		{
			int num = -1;
			for (int i = 0; i < 5; i++)
			{
				if (GameScr.flyTextState[i] == -1)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return;
			}
			GameScr.flyTextColor[num] = color;
			GameScr.flyTextString[num] = flyString;
			GameScr.flyTextX[num] = x;
			GameScr.flyTextY[num] = y;
			GameScr.flyTextDx[num] = dx;
			GameScr.flyTextDy[num] = ((dy >= 0) ? 5 : -5);
			GameScr.flyTextState[num] = 0;
			GameScr.flyTime[num] = 0;
			GameScr.flyTextYTo[num] = 10;
			for (int j = 0; j < 5; j++)
			{
				if (GameScr.flyTextState[j] != -1 && num != j && GameScr.flyTextDy[num] < 0 && Res.abs(GameScr.flyTextX[num] - GameScr.flyTextX[j]) <= 20 && GameScr.flyTextYTo[num] == GameScr.flyTextYTo[j])
				{
					GameScr.flyTextYTo[num] += 10;
				}
			}
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00294AB0 File Offset: 0x00292CB0
		public static void updateFlyText()
		{
			for (int i = 0; i < 5; i++)
			{
				if (GameScr.flyTextState[i] != -1)
				{
					if (GameScr.flyTextState[i] > GameScr.flyTextYTo[i])
					{
						GameScr.flyTime[i]++;
						if (GameScr.flyTime[i] == 25)
						{
							GameScr.flyTime[i] = 0;
							GameScr.flyTextState[i] = -1;
							GameScr.flyTextYTo[i] = 0;
							GameScr.flyTextDx[i] = 0;
							GameScr.flyTextX[i] = 0;
						}
					}
					else
					{
						GameScr.flyTextState[i] += Res.abs(GameScr.flyTextDy[i]);
						GameScr.flyTextX[i] += GameScr.flyTextDx[i];
						GameScr.flyTextY[i] += GameScr.flyTextDy[i];
					}
				}
			}
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x00294B78 File Offset: 0x00292D78
		public static void loadSplash()
		{
			if (GameScr.imgSplash == null)
			{
				GameScr.imgSplash = new Image[3];
				for (int i = 0; i < 3; i++)
				{
					GameScr.imgSplash[i] = GameCanvas.loadImage("/e/sp" + i.ToString() + ".png");
				}
			}
			GameScr.splashX = new int[2];
			GameScr.splashY = new int[2];
			GameScr.splashState = new int[2];
			GameScr.splashF = new int[2];
			GameScr.splashDir = new int[2];
			GameScr.splashState[0] = (GameScr.splashState[1] = -1);
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x00294C10 File Offset: 0x00292E10
		public static bool startSplash(int x, int y, int dir)
		{
			int num = (GameScr.splashState[0] != -1) ? 1 : 0;
			if (GameScr.splashState[num] != -1)
			{
				return false;
			}
			GameScr.splashState[num] = 0;
			GameScr.splashDir[num] = dir;
			GameScr.splashX[num] = x;
			GameScr.splashY[num] = y;
			return true;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x00294C5C File Offset: 0x00292E5C
		public static void updateSplash()
		{
			for (int i = 0; i < 2; i++)
			{
				if (GameScr.splashState[i] != -1)
				{
					GameScr.splashState[i]++;
					GameScr.splashX[i] += GameScr.splashDir[i] << 2;
					GameScr.splashY[i]--;
					if (GameScr.splashState[i] >= 6)
					{
						GameScr.splashState[i] = -1;
					}
					else
					{
						GameScr.splashF[i] = (GameScr.splashState[i] >> 1) % 3;
					}
				}
			}
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x00294CE0 File Offset: 0x00292EE0
		public static void paintSplash(mGraphics g)
		{
			for (int i = 0; i < 2; i++)
			{
				if (GameScr.splashState[i] != -1)
				{
					if (GameScr.splashDir[i] == 1)
					{
						g.drawImage(GameScr.imgSplash[GameScr.splashF[i]], GameScr.splashX[i], GameScr.splashY[i], 3);
					}
					else
					{
						g.drawRegion(GameScr.imgSplash[GameScr.splashF[i]], 0, 0, mGraphics.getImageWidth(GameScr.imgSplash[GameScr.splashF[i]]), mGraphics.getImageHeight(GameScr.imgSplash[GameScr.splashF[i]]), 2, GameScr.splashX[i], GameScr.splashY[i], 3);
					}
				}
			}
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00294D80 File Offset: 0x00292F80
		private void loadInforBar()
		{
			this.imgScrW = 84;
			GameScr.hpBarW = 66L;
			GameScr.mpBarW = 59;
			GameScr.hpBarX = 52;
			GameScr.hpBarY = 10;
			GameScr.spBarW = 61;
			GameScr.expBarW = GameScr.gW - 61;
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x00294DBC File Offset: 0x00292FBC
		public void updateSS()
		{
			if (GameScr.indexMenu != -1)
			{
				if (GameScr.cmySK != GameScr.cmtoYSK)
				{
					GameScr.cmvySK = GameScr.cmtoYSK - GameScr.cmySK << 2;
					GameScr.cmdySK += GameScr.cmvySK;
					GameScr.cmySK += GameScr.cmdySK >> 4;
					GameScr.cmdySK &= 15;
				}
				if (Math.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK < 0)
				{
					GameScr.cmtoYSK = 0;
				}
				if (Math.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK > GameScr.cmyLimSK)
				{
					GameScr.cmtoYSK = GameScr.cmyLimSK;
				}
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x00294E70 File Offset: 0x00293070
		public void updateKeyAlert()
		{
			if (!GameScr.isPaintAlert || GameCanvas.currentDialog != null)
			{
				return;
			}
			bool flag = false;
			if (GameCanvas.keyPressed[Key.NUM8])
			{
				GameScr.indexRow++;
				if (GameScr.indexRow >= this.texts.size())
				{
					GameScr.indexRow = 0;
				}
				flag = true;
			}
			else if (GameCanvas.keyPressed[Key.NUM2])
			{
				GameScr.indexRow--;
				if (GameScr.indexRow < 0)
				{
					GameScr.indexRow = this.texts.size() - 1;
				}
				flag = true;
			}
			if (flag)
			{
				GameScr.scrMain.moveTo(GameScr.indexRow * GameScr.scrMain.ITEM_SIZE);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			if (GameCanvas.isTouch)
			{
				ScrollResult scrollResult = GameScr.scrMain.updateKey();
				if (scrollResult.isDowning || scrollResult.isFinish)
				{
					GameScr.indexRow = scrollResult.selected;
					flag = true;
				}
			}
			if (!flag || GameScr.indexRow < 0 || GameScr.indexRow >= this.texts.size())
			{
				return;
			}
			string text = (string)this.texts.elementAt(GameScr.indexRow);
			this.fnick = null;
			this.alertURL = null;
			this.center = null;
			ChatTextField.gI().center = null;
			int num;
			if ((num = text.IndexOf("http://")) >= 0)
			{
				Cout.println("currentLine: " + text);
				this.alertURL = text.Substring(num);
				this.center = new Command(mResources.open_link, 12000);
				if (!GameCanvas.isTouch)
				{
					ChatTextField.gI().center = new Command(mResources.open_link, null, 12000, null);
					return;
				}
			}
			else
			{
				if (text.IndexOf("@") < 0)
				{
					return;
				}
				string text2 = text.Substring(2);
				text2 = text2.Trim();
				num = text2.IndexOf("@");
				string text3 = text2.Substring(num);
				int num2 = text3.IndexOf(" ");
				num2 = ((num2 > 0) ? (num2 + num) : (num + text3.Length));
				this.fnick = text2.Substring(num + 1, num2);
				if (!this.fnick.Equals(string.Empty) && !this.fnick.Equals(Char.myCharz().cName))
				{
					this.center = new Command(mResources.SELECT, 12009, this.fnick);
					if (!GameCanvas.isTouch)
					{
						ChatTextField.gI().center = new Command(mResources.SELECT, null, 12009, this.fnick);
						return;
					}
				}
				else
				{
					this.fnick = null;
					this.center = null;
				}
			}
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x002950FC File Offset: 0x002932FC
		public bool isPaintPopup()
		{
			return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade || GameScr.isPaintAlert || GameScr.isPaintZone || GameScr.isPaintTeam || GameScr.isPaintClan || GameScr.isPaintFindTeam || GameScr.isPaintTask || GameScr.isPaintFriend || GameScr.isPaintEnemies || GameScr.isPaintCharInMap || GameScr.isPaintMessage;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x00295250 File Offset: 0x00293450
		public bool isNotPaintTouchControl()
		{
			return (!GameCanvas.isTouchControl && GameCanvas.currentScreen == GameScr.gI()) || !GameCanvas.isTouch || ChatTextField.gI().isShow || InfoDlg.isShow || (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameCanvas.panel.isShow || this.isPaintPopup());
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x002952C4 File Offset: 0x002934C4
		public bool isPaintUI()
		{
			return GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x002953A0 File Offset: 0x002935A0
		public bool isOpenUI()
		{
			return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintWeapon || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintSplit || GameScr.isPaintTrade;
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x00295490 File Offset: 0x00293690
		public static void setPopupSize(int w, int h)
		{
			if (GameCanvas.w == 128 || GameCanvas.h <= 208)
			{
				w = 126;
				h = 160;
			}
			GameScr.indexTitle = 0;
			GameScr.popupW = w;
			GameScr.popupH = h;
			GameScr.popupX = GameScr.gW2 - w / 2;
			GameScr.popupY = GameScr.gH2 - h / 2;
			if (GameCanvas.isTouch && !GameScr.isPaintZone && !GameScr.isPaintTeam && !GameScr.isPaintClan && !GameScr.isPaintCharInMap && !GameScr.isPaintFindTeam && !GameScr.isPaintFriend && !GameScr.isPaintEnemies && !GameScr.isPaintTask && !GameScr.isPaintMessage)
			{
				if (GameCanvas.h <= 240)
				{
					GameScr.popupY -= 10;
				}
				if (GameCanvas.isTouch && !GameCanvas.isTouchControlSmallScreen && GameCanvas.currentScreen is GameScr)
				{
					GameScr.popupW = 310;
					GameScr.popupX = GameScr.gW / 2 - GameScr.popupW / 2;
					if (GameScr.isPaintInfoMe && GameScr.indexMenu > 0)
					{
						GameScr.popupW = w;
						GameScr.popupX = GameScr.gW2 - w / 2;
					}
				}
			}
			if (GameScr.popupY < -10)
			{
				GameScr.popupY = -10;
			}
			if (GameCanvas.h > 208 && GameScr.popupY < 0)
			{
				GameScr.popupY = 0;
			}
			if (GameCanvas.h == 208 && GameScr.popupY < 10)
			{
				GameScr.popupY = 10;
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x00295606 File Offset: 0x00293806
		public static void loadImg()
		{
			TileMap.loadTileImage();
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x0029560D File Offset: 0x0029380D
		public static int getTaskMapId()
		{
			if (Char.myCharz().taskMaint == null)
			{
				return -1;
			}
			return GameScr.mapTasks[Char.myCharz().taskMaint.index];
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00295634 File Offset: 0x00293834
		public static sbyte getTaskNpcId()
		{
			sbyte result = 0;
			if (Char.myCharz().taskMaint == null)
			{
				result = -1;
			}
			else if (Char.myCharz().taskMaint.index <= GameScr.tasks.Length - 1)
			{
				result = (sbyte)GameScr.tasks[Char.myCharz().taskMaint.index];
			}
			return result;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x00295688 File Offset: 0x00293888
		public void onChatFromMe(string text, string to)
		{
			if (text == "vd")
			{
				this.RemoveAllItem();
			}
			if (!GameScr.isPaintMessage || GameCanvas.isTouch)
			{
				ChatTextField.gI().isShow = false;
			}
			if (ModFunc.GI().Chat(text))
			{
				return;
			}
			if (to.Equals(mResources.chat_player))
			{
				if (GameScr.info2.playerID != Char.myCharz().charID)
				{
					Service.gI().chatPlayer(text, GameScr.info2.playerID);
					return;
				}
			}
			else
			{
				if (ChatTextField.gI().strChat == "Nhập tốc độ game" && text != string.Empty)
				{
					ModFunc.GI().ChangeGameSpeed(text);
					ChatTextField.gI().strChat = "Chat";
					ChatTextField.gI().tfChat.name = "chat";
					ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_ANY);
					ChatTextField.gI().isShow = false;
					return;
				}
				if (ChatTextField.gI().strChat == "Tăng đến mức" && text != string.Empty)
				{
					ModFunc.GI().SetIncreasePoint(text);
					ChatTextField.gI().strChat = "Chat";
					ChatTextField.gI().tfChat.name = "chat";
					ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_ANY);
					ChatTextField.gI().isShow = false;
					return;
				}
				if (!text.Equals(string.Empty))
				{
					Service.gI().chat(text);
				}
			}
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x00295805 File Offset: 0x00293A05
		public void onCancelChat()
		{
			if (GameScr.isPaintMessage)
			{
				GameScr.isPaintMessage = false;
				ChatTextField.gI().center = null;
			}
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x0029581F File Offset: 0x00293A1F
		public void actMenu()
		{
			GameCanvas.panel.setTypeMain();
			GameCanvas.panel.show();
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x00295838 File Offset: 0x00293A38
		public void openUIZone(Message message)
		{
			InfoDlg.hide();
			try
			{
				this.zones = new int[(int)message.reader().readByte()];
				this.pts = new int[this.zones.Length];
				this.numPlayer = new int[this.zones.Length];
				this.maxPlayer = new int[this.zones.Length];
				this.rank1 = new int[this.zones.Length];
				this.rankName1 = new string[this.zones.Length];
				this.rank2 = new int[this.zones.Length];
				this.rankName2 = new string[this.zones.Length];
				for (int i = 0; i < this.zones.Length; i++)
				{
					this.zones[i] = (int)message.reader().readByte();
					this.pts[i] = (int)message.reader().readByte();
					this.numPlayer[i] = (int)message.reader().readByte();
					this.maxPlayer[i] = (int)message.reader().readByte();
					if (message.reader().readByte() == 1)
					{
						this.rankName1[i] = message.reader().readUTF();
						this.rank1[i] = message.reader().readInt();
						this.rankName2[i] = message.reader().readUTF();
						this.rank2[i] = message.reader().readInt();
					}
				}
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham OPEN UIZONE " + ex.ToString());
			}
			if (ModFunc.GI().userOpenZones)
			{
				GameCanvas.panel.setTypeZone();
				GameCanvas.panel.show();
				ModFunc.GI().userOpenZones = false;
			}
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x00295A08 File Offset: 0x00293C08
		private void actDead()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(mResources.DIES[1], 110381));
			myVector.addElement(new Command(mResources.DIES[2], 110382));
			myVector.addElement(new Command(mResources.DIES[3], 110383));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x00295A6C File Offset: 0x00293C6C
		public void startYesNoPopUp(string info, Command cmdYes, Command cmdNo)
		{
			this.popUpYesNo = new PopUpYesNo();
			this.popUpYesNo.setPopUp(info, cmdYes, cmdNo);
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00295A88 File Offset: 0x00293C88
		public void player_vs_player(int playerId, int xu, string info, sbyte typePK)
		{
			Char @char = GameScr.findCharInMap(playerId);
			if (@char != null)
			{
				if (typePK == 3)
				{
					this.startYesNoPopUp(info, new Command(mResources.OK, 2000, @char), new Command(mResources.CLOSE, 2009, @char));
				}
				if (typePK == 4)
				{
					this.startYesNoPopUp(info, new Command(mResources.OK, 2005, @char), new Command(mResources.CLOSE, 2009, @char));
				}
			}
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00295AF8 File Offset: 0x00293CF8
		public void giaodich(int playerID)
		{
			Char @char = GameScr.findCharInMap(playerID);
			if (@char != null)
			{
				this.startYesNoPopUp(@char.cName + mResources.want_to_trade, new Command(mResources.YES, 11114, @char), new Command(mResources.NO, 2009, @char));
			}
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00295B48 File Offset: 0x00293D48
		public void getFlagImage(int charID, sbyte cflag)
		{
			if (GameScr.vFlag.size() == 0)
			{
				Service.gI().getFlag(2, cflag);
				return;
			}
			if (charID == Char.myCharz().charID)
			{
				if (Char.myCharz().isGetFlagImage(cflag))
				{
					for (int i = 0; i < GameScr.vFlag.size(); i++)
					{
						PKFlag pKFlag = (PKFlag)GameScr.vFlag.elementAt(i);
						if (pKFlag != null && pKFlag.cflag == cflag)
						{
							Char.myCharz().flagImage = pKFlag.IDimageFlag;
						}
					}
					return;
				}
				if (!Char.myCharz().isGetFlagImage(cflag))
				{
					Service.gI().getFlag(2, cflag);
					return;
				}
			}
			else
			{
				if (GameScr.findCharInMap(charID) == null)
				{
					return;
				}
				if (GameScr.findCharInMap(charID).isGetFlagImage(cflag))
				{
					for (int j = 0; j < GameScr.vFlag.size(); j++)
					{
						PKFlag pKFlag2 = (PKFlag)GameScr.vFlag.elementAt(j);
						if (pKFlag2 != null && pKFlag2.cflag == cflag)
						{
							GameScr.findCharInMap(charID).flagImage = pKFlag2.IDimageFlag;
						}
					}
					return;
				}
				if (!GameScr.findCharInMap(charID).isGetFlagImage(cflag))
				{
					Service.gI().getFlag(2, cflag);
				}
			}
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00295C60 File Offset: 0x00293E60
		public void actionPerform(int idAction, object p)
		{
			ModFunc.GI().perform(idAction, p);
			if (idAction <= 11067)
			{
				if (idAction <= 8002)
				{
					if (idAction <= 2)
					{
						if (idAction == 1)
						{
							GameCanvas.endDlg();
							return;
						}
						if (idAction != 2)
						{
							return;
						}
						GameCanvas.menu.showMenu = false;
						return;
					}
					else
					{
						switch (idAction)
						{
						case 2000:
							this.popUpYesNo = null;
							GameCanvas.endDlg();
							if ((Char)p == null)
							{
								Service.gI().player_vs_player(1, 3, -1);
								return;
							}
							Service.gI().player_vs_player(1, 3, ((Char)p).charID);
							Service.gI().charMove();
							return;
						case 2001:
							GameCanvas.endDlg();
							return;
						case 2002:
						case 2008:
							break;
						case 2003:
							GameCanvas.endDlg();
							InfoDlg.showWait();
							Service.gI().player_vs_player(0, 3, Char.myCharz().charFocus.charID);
							return;
						case 2004:
							GameCanvas.endDlg();
							Service.gI().player_vs_player(0, 4, Char.myCharz().charFocus.charID);
							return;
						case 2005:
							GameCanvas.endDlg();
							this.popUpYesNo = null;
							if ((Char)p == null)
							{
								Service.gI().player_vs_player(1, 4, -1);
								return;
							}
							Service.gI().player_vs_player(1, 4, ((Char)p).charID);
							return;
						case 2006:
							GameCanvas.endDlg();
							Service.gI().player_vs_player(2, 4, Char.myCharz().charFocus.charID);
							return;
						case 2007:
							GameCanvas.endDlg();
							GameMidlet.instance.exit();
							return;
						case 2009:
							this.popUpYesNo = null;
							return;
						default:
							if (idAction != 8002)
							{
								return;
							}
							this.doFire(false, true);
							GameCanvas.clearKeyHold();
							GameCanvas.clearKeyPressed();
							return;
						}
					}
				}
				else if (idAction <= 11038)
				{
					switch (idAction)
					{
					case 11000:
						this.actMenu();
						return;
					case 11001:
						Char.myCharz().findNextFocusByKey();
						return;
					case 11002:
						GameCanvas.panel.hide();
						return;
					default:
						if (idAction != 11038)
						{
							return;
						}
						this.actDead();
						return;
					}
				}
				else if (idAction != 11057)
				{
					if (idAction == 11059)
					{
						Skill skill2 = GameScr.onScreenSkill[this.selectedIndexSkill];
						this.doUseSkill(skill2, false);
						this.center = null;
						return;
					}
					if (idAction != 11067)
					{
						return;
					}
					if (TileMap.zoneID != GameScr.indexSelect)
					{
						Service.gI().requestChangeZone(GameScr.indexSelect, this.indexItemUse);
						InfoDlg.showWait();
						return;
					}
					GameScr.info1.addInfo(mResources.ZONE_HERE, 0);
					return;
				}
				else
				{
					Effect2.vEffect2Outside.removeAllElements();
					Effect2.vEffect2.removeAllElements();
					Npc npc = (Npc)p;
					if (npc.idItem == 0)
					{
						Service.gI().confirmMenu((short)npc.template.npcTemplateId, (sbyte)GameCanvas.menu.menuSelectedItem);
						return;
					}
					if (GameCanvas.menu.menuSelectedItem == 0)
					{
						Service.gI().pickItem(npc.idItem);
						return;
					}
				}
			}
			else if (idAction <= 110004)
			{
				if (idAction <= 12006)
				{
					switch (idAction)
					{
					case 11111:
						if (Char.myCharz().charFocus != null)
						{
							InfoDlg.showWait();
							if (GameCanvas.panel.vPlayerMenu.size() <= 0)
							{
								this.playerMenu(Char.myCharz().charFocus);
							}
							GameCanvas.panel.setTypePlayerMenu(Char.myCharz().charFocus);
							GameCanvas.panel.show();
							Service.gI().getPlayerMenu(Char.myCharz().charFocus.charID);
							Service.gI().messagePlayerMenu(Char.myCharz().charFocus.charID);
							return;
						}
						break;
					case 11112:
					{
						Char @char = (Char)p;
						Service.gI().friend(1, @char.charID);
						return;
					}
					case 11113:
					{
						Char char2 = (Char)p;
						if (char2 != null)
						{
							Service.gI().giaodich(0, char2.charID, -1, -1);
							return;
						}
						break;
					}
					case 11114:
					{
						this.popUpYesNo = null;
						GameCanvas.endDlg();
						Char char3 = (Char)p;
						if (char3 != null)
						{
							Service.gI().giaodich(1, char3.charID, -1, -1);
							return;
						}
						break;
					}
					case 11115:
						if (Char.myCharz().charFocus != null)
						{
							InfoDlg.showWait();
							Service.gI().playerMenuAction(Char.myCharz().charFocus.charID, (short)Char.myCharz().charFocus.menuSelect);
							return;
						}
						break;
					case 11116:
					case 11117:
					case 11118:
					case 11119:
						break;
					case 11120:
					{
						object[] array = (object[])p;
						Skill skill3 = (Skill)array[0];
						int num2 = int.Parse((string)array[1]);
						for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
						{
							if (GameScr.onScreenSkill[i] == skill3)
							{
								GameScr.onScreenSkill[i] = null;
							}
						}
						GameScr.onScreenSkill[num2] = skill3;
						this.saveonScreenSkillToRMS();
						return;
					}
					case 11121:
					{
						object[] array2 = (object[])p;
						Skill skill4 = (Skill)array2[0];
						int num3 = int.Parse((string)array2[1]);
						for (int j = 0; j < GameScr.keySkill.Length; j++)
						{
							if (GameScr.keySkill[j] == skill4)
							{
								GameScr.keySkill[j] = null;
							}
						}
						GameScr.keySkill[num3] = skill4;
						this.saveKeySkillToRMS();
						return;
					}
					default:
						switch (idAction)
						{
						case 12000:
							Service.gI().getClan(1, -1, null);
							return;
						case 12001:
							GameCanvas.endDlg();
							return;
						case 12002:
						{
							GameCanvas.endDlg();
							ClanObject clanObject2 = (ClanObject)p;
							Service.gI().clanInvite(1, -1, clanObject2.clanID, clanObject2.code);
							this.popUpYesNo = null;
							return;
						}
						case 12003:
						{
							ClanObject clanObject3 = (ClanObject)p;
							GameCanvas.endDlg();
							Service.gI().clanInvite(2, -1, clanObject3.clanID, clanObject3.code);
							this.popUpYesNo = null;
							return;
						}
						case 12004:
						{
							Skill skill5 = (Skill)p;
							this.doUseSkill(skill5, true);
							Char.myCharz().saveLoadPreviousSkill();
							return;
						}
						case 12005:
							if (GameCanvas.serverScr == null)
							{
								GameCanvas.serverScr = new ServerScr();
							}
							GameCanvas.serverScr.switchToMe();
							GameCanvas.endDlg();
							return;
						case 12006:
							GameMidlet.instance.exit();
							break;
						default:
							return;
						}
						break;
					}
				}
				else
				{
					if (idAction == 110001)
					{
						GameCanvas.panel.setTypeMain();
						GameCanvas.panel.show();
						return;
					}
					if (idAction != 110004)
					{
						return;
					}
					GameCanvas.menu.showMenu = false;
					return;
				}
			}
			else if (idAction <= 110383)
			{
				if (idAction == 110382)
				{
					Service.gI().returnTownFromDead();
					return;
				}
				if (idAction != 110383)
				{
					return;
				}
				Service.gI().wakeUpFromDead();
				return;
			}
			else
			{
				if (idAction == 110391)
				{
					Service.gI().clanInvite(0, Char.myCharz().charFocus.charID, -1, -1);
					return;
				}
				if (idAction == 888351)
				{
					Service.gI().petStatus(5);
					GameCanvas.endDlg();
					return;
				}
				if (idAction != 888352)
				{
					return;
				}
				Service.gI().pet2Status(5);
				GameCanvas.endDlg();
				return;
			}
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x0029630C File Offset: 0x0029450C
		private static void setTouchBtn()
		{
			if (GameScr.isAnalog != 0)
			{
				GameScr.xTG = (GameScr.xF = GameCanvas.w - 45);
				if (GameScr.gamePad.isLargeGamePad)
				{
					GameScr.xSkill = GameScr.gamePad.wZone + 20;
					GameScr.wSkill = 35;
					GameScr.xHP = GameScr.xF - 45;
				}
				else if (GameScr.gamePad.isMediumGamePad)
				{
					GameScr.xHP = GameScr.xF - 45;
				}
				GameScr.yF = GameCanvas.h - 45;
				GameScr.yTG = GameScr.yF - 45;
			}
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x0029639C File Offset: 0x0029459C
		private void updateGamePad()
		{
			if (GameScr.isAnalog == 0 || Char.myCharz().statusMe == 14)
			{
				return;
			}
			if (GameCanvas.isPointerHoldIn(GameScr.xF, GameScr.yF, 40, 40))
			{
				mScreen.keyTouch = 5;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
					GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
				}
			}
			GameScr.gamePad.update();
			if (GameCanvas.isPointerHoldIn(GameScr.xTG, GameScr.yTG, 34, 34))
			{
				mScreen.keyTouch = 13;
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					Char.myCharz().findNextFocusByKey();
					GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
				}
			}
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x0029646C File Offset: 0x0029466C
		private void paintGamePad(mGraphics g)
		{
			if (GameScr.isAnalog != 0 && Char.myCharz().statusMe != 14)
			{
				g.drawImage((mScreen.keyTouch != 5) ? GameScr.imgFire0 : GameScr.imgFire1, GameScr.xF + 20, GameScr.yF + 14, mGraphics.HCENTER | mGraphics.VCENTER);
				GameScr.gamePad.paint(g);
				g.drawImage((mScreen.keyTouch != 13) ? GameScr.imgFocus : GameScr.imgFocus2, GameScr.xTG + 20, GameScr.yTG + 14, mGraphics.HCENTER | mGraphics.VCENTER);
				ModFunc.GI().PaintButton(g, GameScr.xTG, GameScr.yTG);
			}
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x00296520 File Offset: 0x00294720
		public void showWinNumber(string num, string finish)
		{
			this.winnumber = new int[num.Length];
			this.randomNumber = new int[num.Length];
			this.tMove = new int[num.Length];
			this.moveCount = new int[num.Length];
			this.delayMove = new int[num.Length];
			try
			{
				for (int i = 0; i < num.Length; i++)
				{
					this.winnumber[i] = (int)short.Parse(num[i].ToString());
					this.randomNumber[i] = Res.random(0, 11);
					this.tMove[i] = 1;
					this.delayMove[i] = 0;
				}
			}
			catch (Exception)
			{
			}
			this.tShow = 100;
			this.moveIndex = 0;
			this.strFinish = finish;
			GameScr.lastXS = (GameScr.currXS = mSystem.currentTimeMillis());
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x0029660C File Offset: 0x0029480C
		public void chatVip(string chatVip)
		{
			if (!this.startChat)
			{
				this.currChatWidth = mFont.tahoma_7b_yellowSmall.getWidth(chatVip);
				this.xChatVip = GameCanvas.w;
				this.startChat = true;
			}
			if (chatVip.StartsWith("!"))
			{
				chatVip = chatVip.Substring(1, chatVip.Length);
				this.isFireWorks = true;
			}
			GameScr.vChatVip.addElement(chatVip);
			ShowBoss.HandleChatVip(chatVip);
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x00296678 File Offset: 0x00294878
		public void paintChatVip(mGraphics g)
		{
			if (GameScr.vChatVip.size() != 0 && GameScr.isPaintChatVip)
			{
				g.setClip(0, GameCanvas.h - 13, GameCanvas.w, 15);
				g.fillRect(0, GameCanvas.h - 13, GameCanvas.w, 15, 0, 90);
				string st = (string)GameScr.vChatVip.elementAt(0);
				mFont.tahoma_7b_yellow.drawStringBorder(g, st, this.xChatVip, GameCanvas.h - 13, 0, mFont.tahoma_7b_dark);
			}
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x002966F8 File Offset: 0x002948F8
		public void updateChatVip()
		{
			if (!this.startChat)
			{
				return;
			}
			this.xChatVip -= 2;
			if (this.xChatVip < -this.currChatWidth)
			{
				this.xChatVip = GameCanvas.w;
				GameScr.vChatVip.removeElementAt(0);
				if (GameScr.vChatVip.size() == 0)
				{
					this.isFireWorks = false;
					this.startChat = false;
					return;
				}
				this.currChatWidth = mFont.tahoma_7b_white.getWidth((string)GameScr.vChatVip.elementAt(0));
			}
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x0029677C File Offset: 0x0029497C
		public void showYourNumber(string strNum)
		{
			this.yourNumber = strNum;
			this.strPaint = mFont.tahoma_7.splitFontArray(this.yourNumber, 500);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x002967A0 File Offset: 0x002949A0
		public static void checkRemoveImage()
		{
			ImgByName.checkDelHash(ImgByName.hashImagePath, 10, false);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x002967AF File Offset: 0x002949AF
		public static bool ispaintPhubangBar()
		{
			return TileMap.mapPhuBang() && GameScr.phuban_Info.type_PB == 0;
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x002967C8 File Offset: 0x002949C8
		public void paintPhuBanBar(mGraphics g, int x, int y, int w)
		{
			if (GameScr.phuban_Info == null || GameScr.isPaintOther || GameScr.isPaintRada != 1 || GameCanvas.panel.isShow || !GameScr.ispaintPhubangBar())
			{
				return;
			}
			if (w < GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4)
			{
				w = GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4;
			}
			if (x > GameCanvas.w - w / 2)
			{
				x = GameCanvas.w - w / 2;
			}
			if (x < mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10)
			{
				x = mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10;
			}
			int frameHeight = GameScr.fra_PVE_Bar_0.frameHeight;
			int num = y + frameHeight + mGraphics.getImageHeight(GameScr.imgBall) / 2 + 2;
			int frameWidth = GameScr.fra_PVE_Bar_1.frameWidth;
			int num2 = w / 2 - frameWidth / 2;
			int num3 = x - w / 2;
			int num4 = x + frameWidth / 2;
			int y2 = y + 3;
			int num5 = num2 - GameScr.fra_PVE_Bar_0.frameWidth;
			int num6 = num5 / GameScr.fra_PVE_Bar_0.frameWidth;
			if (num5 % GameScr.fra_PVE_Bar_0.frameWidth > 0)
			{
				num6++;
			}
			for (int i = 0; i < num6; i++)
			{
				if (i < num6 - 1)
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + GameScr.fra_PVE_Bar_0.frameWidth + i * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
				}
				else
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + num5, y2, 0, 0, g);
				}
				if (i < num6 - 1)
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + i * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
				}
				else
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + num5 - GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
				}
			}
			GameScr.fra_PVE_Bar_0.drawFrame(0, num3, y2, 2, 0, g);
			GameScr.fra_PVE_Bar_0.drawFrame(0, num4 + num5, y2, 0, 0, g);
			if (GameScr.phuban_Info.pointTeam1 > 0)
			{
				int idx = 2;
				int idx2 = 3;
				if (GameScr.phuban_Info.color_1 == 4)
				{
					idx = 4;
					idx2 = 5;
				}
				int num7 = GameScr.phuban_Info.pointTeam1 * num2 / GameScr.phuban_Info.maxPoint;
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num7 > num2)
				{
					num7 = num2;
				}
				g.setClip(num3 + num2 - num7, y2, num7, frameHeight);
				for (int j = 0; j < num6; j++)
				{
					if (j < num6 - 1)
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx2, num3 + GameScr.fra_PVE_Bar_0.frameWidth + j * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
					else
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx2, num3 + num5, y2, 0, 0, g);
					}
				}
				GameScr.fra_PVE_Bar_0.drawFrame(idx, num3, y2, 2, 0, g);
				GameCanvas.resetTrans(g);
			}
			if (GameScr.phuban_Info.pointTeam2 > 0)
			{
				int idx3 = 2;
				int idx4 = 3;
				if (GameScr.phuban_Info.color_2 == 4)
				{
					idx3 = 4;
					idx4 = 5;
				}
				int num8 = GameScr.phuban_Info.pointTeam2 * num2 / GameScr.phuban_Info.maxPoint;
				if (num8 < 0)
				{
					num8 = 0;
				}
				if (num8 > num2)
				{
					num8 = num2;
				}
				g.setClip(num4, y2, num8, frameHeight);
				for (int k = 0; k < num6; k++)
				{
					if (k < num6 - 1)
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx4, num4 + k * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
					else
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx4, num4 + num5 - GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
				}
				GameScr.fra_PVE_Bar_0.drawFrame(idx3, num4 + num5, y2, 0, 0, g);
				GameCanvas.resetTrans(g);
			}
			GameScr.fra_PVE_Bar_1.drawFrame(0, x - frameWidth / 2, y, 0, 0, g);
			string timeCountDown = mSystem.getTimeCountDown(GameScr.phuban_Info.timeStart, (int)GameScr.phuban_Info.timeSecond, true, false);
			mFont.tahoma_7b_yellow.drawString(g, timeCountDown, x + 1, y + GameScr.fra_PVE_Bar_1.frameHeight / 2 - mFont.tahoma_7b_green2.getHeight() / 2, 2);
			Panel.setTextColor(GameScr.phuban_Info.color_1, 1).drawString(g, GameScr.phuban_Info.nameTeam1, x - 5, num + 5, 1);
			Panel.setTextColor(GameScr.phuban_Info.color_2, 1).drawString(g, GameScr.phuban_Info.nameTeam2, x + 5, num + 5, 0);
			if (GameScr.phuban_Info.type_PB != 0)
			{
				int y3 = y + frameHeight / 2 - 2;
				mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam1.ToString(), num3 + num2 / 2, y3, 2);
				mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam2.ToString(), num4 + num2 / 2, y3, 2);
			}
			g.drawImage(GameScr.imgVS, x, y + GameScr.fra_PVE_Bar_1.frameHeight + 2, 3);
			if (GameScr.phuban_Info.type_PB == 0)
			{
				GameScr.paintChienTruong_Life(g, GameScr.phuban_Info.maxLife, GameScr.phuban_Info.color_1, GameScr.phuban_Info.lifeTeam1, x - 13, GameScr.phuban_Info.color_2, GameScr.phuban_Info.lifeTeam2, x + 13, num);
			}
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x00296CF4 File Offset: 0x00294EF4
		public static void paintChienTruong_Life(mGraphics g, int maxLife, int cl1, int lifeTeam1, int x1, int cl2, int lifeTeam2, int x2, int y)
		{
			if (GameScr.imgBall == null)
			{
				return;
			}
			int num = mGraphics.getImageHeight(GameScr.imgBall) / 2;
			for (int i = 0; i < maxLife; i++)
			{
				int num2 = 0;
				if (i < lifeTeam1)
				{
					num2 = 1;
				}
				g.drawRegion(GameScr.imgBall, 0, num2 * num, mGraphics.getImageWidth(GameScr.imgBall), num, 0, x1 - i * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			for (int j = 0; j < maxLife; j++)
			{
				int num3 = 0;
				if (j < lifeTeam2)
				{
					num3 = 1;
				}
				g.drawRegion(GameScr.imgBall, 0, num3 * num, mGraphics.getImageWidth(GameScr.imgBall), num, 0, x2 + j * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x00296DA4 File Offset: 0x00294FA4
		public static void paintHPBar_NEW(mGraphics g, int x, int y, Char c)
		{
			g.drawImage(GameScr.imgKhung, x, y, 0);
			int x2 = x + 3;
			int num = y + 19;
			int width = GameScr.imgHP_NEW.getWidth();
			int num2 = GameScr.imgHP_NEW.getHeight() / 2;
			int num3 = (int)(c.cHP * (long)width / c.cHPFull);
			if (num3 <= 0)
			{
				num3 = 1;
			}
			else if (num3 > width)
			{
				num3 = width;
			}
			g.drawRegion(GameScr.imgHP_NEW, 0, num2, num3, num2, 0, x2, num, 0);
			int num4 = (int)(c.cMP * (long)width / c.cMPFull);
			if (num4 <= 0)
			{
				num4 = 1;
			}
			else if (num4 > width)
			{
				num4 = width;
			}
			g.drawRegion(GameScr.imgHP_NEW, 0, 0, num4, num2, 0, x2, num + 6, 0);
			int x3 = x + GameScr.imgKhung.getWidth() / 2 + 1;
			int y2 = num + 13;
			mFont.tahoma_7_green2.drawString(g, c.cName, x3, y + 4, 2);
			if (c.mobFocus != null)
			{
				if (c.mobFocus.getTemplate() != null)
				{
					mFont.tahoma_7_green2.drawString(g, c.mobFocus.getTemplate().name, x3, y2, 2);
					return;
				}
			}
			else
			{
				if (c.npcFocus != null)
				{
					mFont.tahoma_7_green2.drawString(g, c.npcFocus.template.name, x3, y2, 2);
					return;
				}
				if (c.charFocus != null)
				{
					mFont.tahoma_7_green2.drawString(g, c.charFocus.cName, x3, y2, 2);
				}
			}
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x00296F08 File Offset: 0x00295108
		public static void addEffectEnd(int type, int subtype, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
		{
			GameScr.addEffect2Vector(new Effect_End(type, subtype, typePaint, x, y, levelPaint, dir, timeRemove, listObj));
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x00296F30 File Offset: 0x00295130
		public static void addEffectEnd(int type, int subtype, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj, sbyte level)
		{
			GameScr.addEffect2Vector(new Effect_End(type, subtype, typePaint, x, y, levelPaint, dir, timeRemove, listObj, level));
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x00296F58 File Offset: 0x00295158
		public static void addEffectEnd_Target(int type, int subtype, int typePaint, Char charUse, Point target, int levelPaint, short timeRemove, short range, sbyte level)
		{
			GameScr.addEffect2Vector(new Effect_End(type, subtype, typePaint, charUse.clone(), target, levelPaint, timeRemove, range, level));
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x00296F82 File Offset: 0x00295182
		public static void addEffect2Vector(Effect_End eff)
		{
			if (eff.levelPaint == 0)
			{
				EffectManager.addHiEffect(eff);
				return;
			}
			if (eff.levelPaint == 1)
			{
				EffectManager.addMidEffects(eff);
				return;
			}
			if (eff.levelPaint == 2)
			{
				EffectManager.addMid_2Effects(eff);
				return;
			}
			EffectManager.addLowEffect(eff);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00296FB9 File Offset: 0x002951B9
		public static bool isSmallScr()
		{
			return GameCanvas.w <= 320;
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00296FCC File Offset: 0x002951CC
		private void paint_xp_bar(mGraphics g)
		{
			g.setColor(8421504);
			g.fillRect(0, GameCanvas.h - 2, GameCanvas.w, 2);
			int w = (int)(Char.myCharz().cLevelPercent * (long)GameCanvas.w / 10000L);
			g.setColor(16777215);
			g.fillRect(0, GameCanvas.h - 2, w, 2);
			g.setColor(0);
			w = GameCanvas.w / 10;
			for (int i = 1; i < 10; i++)
			{
				g.fillRect(i * w, GameCanvas.h - 2, 1, 2);
			}
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x0029705C File Offset: 0x0029525C
		private void paint_ios_bg(mGraphics g)
		{
			if (mSystem.clientType == 5)
			{
				if (GameScr.imgBgIOS != null)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					g.drawImage(GameScr.imgBgIOS, GameCanvas.w / 2, GameCanvas.h / 2, mGraphics.VCENTER | mGraphics.HCENTER);
					return;
				}
				GameScr.imgBgIOS = GameCanvas.loadImage("/bg/bg_ios_" + ((TileMap.bgID % 2 != 0) ? 1 : 2).ToString() + ".png");
			}
		}

		// Token: 0x04004F56 RID: 20310
		public bool isWaitingDoubleClick;

		// Token: 0x04004F57 RID: 20311
		public long timeStartDblClick;

		// Token: 0x04004F58 RID: 20312
		public long timeEndDblClick;

		// Token: 0x04004F59 RID: 20313
		public static bool isPaintOther = false;

		// Token: 0x04004F5A RID: 20314
		public static MyVector textTime = new MyVector(string.Empty);

		// Token: 0x04004F5B RID: 20315
		public static bool isLoadAllData = false;

		// Token: 0x04004F5C RID: 20316
		public static GameScr instance;

		// Token: 0x04004F5D RID: 20317
		public static int gW;

		// Token: 0x04004F5E RID: 20318
		public static int gH;

		// Token: 0x04004F5F RID: 20319
		public static int gW2;

		// Token: 0x04004F60 RID: 20320
		public static int gssw;

		// Token: 0x04004F61 RID: 20321
		public static int gssh;

		// Token: 0x04004F62 RID: 20322
		public static int gH34;

		// Token: 0x04004F63 RID: 20323
		public static int gW3;

		// Token: 0x04004F64 RID: 20324
		public static int gH3;

		// Token: 0x04004F65 RID: 20325
		public static int gH23;

		// Token: 0x04004F66 RID: 20326
		public static int gW23;

		// Token: 0x04004F67 RID: 20327
		public static int gH2;

		// Token: 0x04004F68 RID: 20328
		public static int csPadMaxH;

		// Token: 0x04004F69 RID: 20329
		public static int cmdBarH;

		// Token: 0x04004F6A RID: 20330
		public static int gW34;

		// Token: 0x04004F6B RID: 20331
		public static int gW6;

		// Token: 0x04004F6C RID: 20332
		public static int gH6;

		// Token: 0x04004F6D RID: 20333
		public static int cmx;

		// Token: 0x04004F6E RID: 20334
		public static int cmy;

		// Token: 0x04004F6F RID: 20335
		public static int cmdx;

		// Token: 0x04004F70 RID: 20336
		public static int cmdy;

		// Token: 0x04004F71 RID: 20337
		public static int cmvx;

		// Token: 0x04004F72 RID: 20338
		public static int cmvy;

		// Token: 0x04004F73 RID: 20339
		public static int cmtoX;

		// Token: 0x04004F74 RID: 20340
		public static int cmtoY;

		// Token: 0x04004F75 RID: 20341
		public static int cmxLim;

		// Token: 0x04004F76 RID: 20342
		public static int cmyLim;

		// Token: 0x04004F77 RID: 20343
		public static int gssx;

		// Token: 0x04004F78 RID: 20344
		public static int gssy;

		// Token: 0x04004F79 RID: 20345
		public static int gssxe;

		// Token: 0x04004F7A RID: 20346
		public static int gssye;

		// Token: 0x04004F7B RID: 20347
		public Command cmdback;

		// Token: 0x04004F7C RID: 20348
		public Command cmdFocus;

		// Token: 0x04004F7D RID: 20349
		public static int d;

		// Token: 0x04004F7E RID: 20350
		public static int hpPotion;

		// Token: 0x04004F7F RID: 20351
		public static SkillPaint[] sks;

		// Token: 0x04004F80 RID: 20352
		public static Arrowpaint[] arrs;

		// Token: 0x04004F81 RID: 20353
		public static DartInfo[] darts;

		// Token: 0x04004F82 RID: 20354
		public static Part[] parts;

		// Token: 0x04004F83 RID: 20355
		public static EffectCharPaint[] efs;

		// Token: 0x04004F84 RID: 20356
		public static int lockTick;

		// Token: 0x04004F85 RID: 20357
		public static MyVector vClan = new MyVector();

		// Token: 0x04004F86 RID: 20358
		public static MyVector vPtMap = new MyVector();

		// Token: 0x04004F87 RID: 20359
		public static MyVector vFriend = new MyVector();

		// Token: 0x04004F88 RID: 20360
		public static MyVector vEnemies = new MyVector();

		// Token: 0x04004F89 RID: 20361
		public static MyVector vCharInMap = new MyVector();

		// Token: 0x04004F8A RID: 20362
		public static MyVector vItemMap = new MyVector();

		// Token: 0x04004F8B RID: 20363
		public static MyVector vMobAttack = new MyVector();

		// Token: 0x04004F8C RID: 20364
		public static MyVector vSet = new MyVector();

		// Token: 0x04004F8D RID: 20365
		public static MyVector vMob = new MyVector();

		// Token: 0x04004F8E RID: 20366
		public static MyVector vNpc = new MyVector();

		// Token: 0x04004F8F RID: 20367
		public static MyVector vFlag = new MyVector();

		// Token: 0x04004F90 RID: 20368
		public static NClass[] nClasss;

		// Token: 0x04004F91 RID: 20369
		public static int indexSize = 28;

		// Token: 0x04004F92 RID: 20370
		public static int indexTitle = 0;

		// Token: 0x04004F93 RID: 20371
		public static int indexSelect = 0;

		// Token: 0x04004F94 RID: 20372
		public static int indexRow = -1;

		// Token: 0x04004F95 RID: 20373
		public static int indexRowMax;

		// Token: 0x04004F96 RID: 20374
		public static int indexMenu = 0;

		// Token: 0x04004F97 RID: 20375
		public ItemOptionTemplate[] iOptionTemplates;

		// Token: 0x04004F98 RID: 20376
		public SkillOptionTemplate[] sOptionTemplates;

		// Token: 0x04004F99 RID: 20377
		private static Scroll scrInfo = new Scroll();

		// Token: 0x04004F9A RID: 20378
		public static Scroll scrMain = new Scroll();

		// Token: 0x04004F9B RID: 20379
		public static MyVector vItemUpGrade = new MyVector();

		// Token: 0x04004F9C RID: 20380
		public static bool isViewClanMemOnline = false;

		// Token: 0x04004F9D RID: 20381
		public static bool isViewClanInvite = true;

		// Token: 0x04004F9E RID: 20382
		public static string titleInputText = string.Empty;

		// Token: 0x04004F9F RID: 20383
		public static int tickMove;

		// Token: 0x04004FA0 RID: 20384
		public static bool isPaintAlert = false;

		// Token: 0x04004FA1 RID: 20385
		public static bool isPaintTask = false;

		// Token: 0x04004FA2 RID: 20386
		public static bool isPaintTeam = false;

		// Token: 0x04004FA3 RID: 20387
		public static bool isPaintFindTeam = false;

		// Token: 0x04004FA4 RID: 20388
		public static bool isPaintFriend = false;

		// Token: 0x04004FA5 RID: 20389
		public static bool isPaintEnemies = false;

		// Token: 0x04004FA6 RID: 20390
		public static bool isPaintItemInfo = false;

		// Token: 0x04004FA7 RID: 20391
		public static bool isHaveSelectSkill = false;

		// Token: 0x04004FA8 RID: 20392
		public static bool isPaintSkill = false;

		// Token: 0x04004FA9 RID: 20393
		public static bool isPaintInfoMe = false;

		// Token: 0x04004FAA RID: 20394
		public static bool isPaintStore = false;

		// Token: 0x04004FAB RID: 20395
		public static bool isPaintNonNam = false;

		// Token: 0x04004FAC RID: 20396
		public static bool isPaintNonNu = false;

		// Token: 0x04004FAD RID: 20397
		public static bool isPaintAoNam = false;

		// Token: 0x04004FAE RID: 20398
		public static bool isPaintAoNu = false;

		// Token: 0x04004FAF RID: 20399
		public static bool isPaintGangTayNam = false;

		// Token: 0x04004FB0 RID: 20400
		public static bool isPaintGangTayNu = false;

		// Token: 0x04004FB1 RID: 20401
		public static bool isPaintQuanNam = false;

		// Token: 0x04004FB2 RID: 20402
		public static bool isPaintQuanNu = false;

		// Token: 0x04004FB3 RID: 20403
		public static bool isPaintGiayNam = false;

		// Token: 0x04004FB4 RID: 20404
		public static bool isPaintGiayNu = false;

		// Token: 0x04004FB5 RID: 20405
		public static bool isPaintLien = false;

		// Token: 0x04004FB6 RID: 20406
		public static bool isPaintNhan = false;

		// Token: 0x04004FB7 RID: 20407
		public static bool isPaintNgocBoi = false;

		// Token: 0x04004FB8 RID: 20408
		public static bool isPaintPhu = false;

		// Token: 0x04004FB9 RID: 20409
		public static bool isPaintWeapon = false;

		// Token: 0x04004FBA RID: 20410
		public static bool isPaintStack = false;

		// Token: 0x04004FBB RID: 20411
		public static bool isPaintStackLock = false;

		// Token: 0x04004FBC RID: 20412
		public static bool isPaintGrocery = false;

		// Token: 0x04004FBD RID: 20413
		public static bool isPaintGroceryLock = false;

		// Token: 0x04004FBE RID: 20414
		public static bool isPaintUpGrade = false;

		// Token: 0x04004FBF RID: 20415
		public static bool isPaintConvert = false;

		// Token: 0x04004FC0 RID: 20416
		public static bool isPaintUpGradeGold = false;

		// Token: 0x04004FC1 RID: 20417
		public static bool isPaintUpPearl = false;

		// Token: 0x04004FC2 RID: 20418
		public static bool isPaintBox = false;

		// Token: 0x04004FC3 RID: 20419
		public static bool isPaintSplit = false;

		// Token: 0x04004FC4 RID: 20420
		public static bool isPaintCharInMap = false;

		// Token: 0x04004FC5 RID: 20421
		public static bool isPaintTrade = false;

		// Token: 0x04004FC6 RID: 20422
		public static bool isPaintZone = false;

		// Token: 0x04004FC7 RID: 20423
		public static bool isPaintMessage = false;

		// Token: 0x04004FC8 RID: 20424
		public static bool isPaintClan = false;

		// Token: 0x04004FC9 RID: 20425
		public static bool isRequestMember = false;

		// Token: 0x04004FCA RID: 20426
		public static Char currentCharViewInfo;

		// Token: 0x04004FCB RID: 20427
		public static long[] exps;

		// Token: 0x04004FCC RID: 20428
		public int tMenuDelay;

		// Token: 0x04004FCD RID: 20429
		public int zoneCol = 6;

		// Token: 0x04004FCE RID: 20430
		public int[] zones;

		// Token: 0x04004FCF RID: 20431
		public int[] pts;

		// Token: 0x04004FD0 RID: 20432
		public int[] numPlayer;

		// Token: 0x04004FD1 RID: 20433
		public int[] maxPlayer;

		// Token: 0x04004FD2 RID: 20434
		public int[] rank1;

		// Token: 0x04004FD3 RID: 20435
		public int[] rank2;

		// Token: 0x04004FD4 RID: 20436
		public string[] rankName1;

		// Token: 0x04004FD5 RID: 20437
		public string[] rankName2;

		// Token: 0x04004FD6 RID: 20438
		public int typeTrade;

		// Token: 0x04004FD7 RID: 20439
		public int typeTradeOrder;

		// Token: 0x04004FD8 RID: 20440
		public int indexItemUse = -1;

		// Token: 0x04004FD9 RID: 20441
		public int cLastFocusID = -1;

		// Token: 0x04004FDA RID: 20442
		public int cPreFocusID = -1;

		// Token: 0x04004FDB RID: 20443
		public bool isLockKey;

		// Token: 0x04004FDC RID: 20444
		public static int[] tasks;

		// Token: 0x04004FDD RID: 20445
		public static int[] mapTasks;

		// Token: 0x04004FDE RID: 20446
		public static Image imgRoomStat;

		// Token: 0x04004FDF RID: 20447
		public static Image frBarPow0;

		// Token: 0x04004FE0 RID: 20448
		public static Image frBarPow1;

		// Token: 0x04004FE1 RID: 20449
		public static Image frBarPow2;

		// Token: 0x04004FE2 RID: 20450
		public static Image frBarPow20;

		// Token: 0x04004FE3 RID: 20451
		public static Image frBarPow21;

		// Token: 0x04004FE4 RID: 20452
		public static Image frBarPow22;

		// Token: 0x04004FE5 RID: 20453
		public MyVector texts;

		// Token: 0x04004FE6 RID: 20454
		public static sbyte vcData;

		// Token: 0x04004FE7 RID: 20455
		public static sbyte vcMap;

		// Token: 0x04004FE8 RID: 20456
		public static sbyte vcSkill;

		// Token: 0x04004FE9 RID: 20457
		public static sbyte vcItem;

		// Token: 0x04004FEA RID: 20458
		public static sbyte vsData;

		// Token: 0x04004FEB RID: 20459
		public static sbyte vsMap;

		// Token: 0x04004FEC RID: 20460
		public static sbyte vsSkill;

		// Token: 0x04004FED RID: 20461
		public static sbyte vsItem;

		// Token: 0x04004FEE RID: 20462
		public static Image imgArrow;

		// Token: 0x04004FEF RID: 20463
		public static Image imgArrow2;

		// Token: 0x04004FF0 RID: 20464
		public static Image imgChat;

		// Token: 0x04004FF1 RID: 20465
		public static Image imgChat2;

		// Token: 0x04004FF2 RID: 20466
		public static Image imgMenu;

		// Token: 0x04004FF3 RID: 20467
		public static Image imgFocus;

		// Token: 0x04004FF4 RID: 20468
		public static Image imgFocus2;

		// Token: 0x04004FF5 RID: 20469
		public static Image imgSkill;

		// Token: 0x04004FF6 RID: 20470
		public static Image imgSkill2;

		// Token: 0x04004FF7 RID: 20471
		public static Image imgHP1;

		// Token: 0x04004FF8 RID: 20472
		public static Image imgHP2;

		// Token: 0x04004FF9 RID: 20473
		public static Image imgHP3;

		// Token: 0x04004FFA RID: 20474
		public static Image imgHP4;

		// Token: 0x04004FFB RID: 20475
		public static Image imgFire0;

		// Token: 0x04004FFC RID: 20476
		public static Image imgFire1;

		// Token: 0x04004FFD RID: 20477
		public static Image imgModFunc;

		// Token: 0x04004FFE RID: 20478
		public static Image imgCommandChat;

		// Token: 0x04004FFF RID: 20479
		public static Image imgNapTuan;

		// Token: 0x04005000 RID: 20480
		public static Image imgLbtn;

		// Token: 0x04005001 RID: 20481
		public static Image imgLbtnFocus;

		// Token: 0x04005002 RID: 20482
		public static Image imgLbtn2;

		// Token: 0x04005003 RID: 20483
		public static Image imgLbtnFocus2;

		// Token: 0x04005004 RID: 20484
		public static Image imgAnalog1;

		// Token: 0x04005005 RID: 20485
		public static Image imgAnalog2;

		// Token: 0x04005006 RID: 20486
		public string tradeName = string.Empty;

		// Token: 0x04005007 RID: 20487
		public string tradeItemName = string.Empty;

		// Token: 0x04005008 RID: 20488
		public int timeLengthMap;

		// Token: 0x04005009 RID: 20489
		public int timeStartMap;

		// Token: 0x0400500A RID: 20490
		public static sbyte typeViewInfo = 0;

		// Token: 0x0400500B RID: 20491
		public static sbyte typeActive = 0;

		// Token: 0x0400500C RID: 20492
		public static InfoMe info1 = new InfoMe();

		// Token: 0x0400500D RID: 20493
		public static InfoMe info2 = new InfoMe();

		// Token: 0x0400500E RID: 20494
		public static Image imgPanel;

		// Token: 0x0400500F RID: 20495
		public static Image imgPanel2;

		// Token: 0x04005010 RID: 20496
		public static Image imgHP;

		// Token: 0x04005011 RID: 20497
		public static Image imgMP;

		// Token: 0x04005012 RID: 20498
		public static Image imgSP;

		// Token: 0x04005013 RID: 20499
		public static Image imgHPLost;

		// Token: 0x04005014 RID: 20500
		public static Image imgMPLost;

		// Token: 0x04005015 RID: 20501
		public static Image imgHP_tm_do;

		// Token: 0x04005016 RID: 20502
		public static Image imgHP_tm_vang;

		// Token: 0x04005017 RID: 20503
		public static Image imgHP_tm_xam;

		// Token: 0x04005018 RID: 20504
		public static Image imgHP_tm_xanh;

		// Token: 0x04005019 RID: 20505
		public Mob mobCapcha;

		// Token: 0x0400501A RID: 20506
		public MagicTree magicTree;

		// Token: 0x0400501B RID: 20507
		public static int countEff;

		// Token: 0x0400501C RID: 20508
		public static GamePad gamePad = new GamePad();

		// Token: 0x0400501D RID: 20509
		public static int isAnalog = 0;

		// Token: 0x0400501E RID: 20510
		public static bool isUseTouch;

		// Token: 0x0400501F RID: 20511
		public static Skill[] keySkill = new Skill[10];

		// Token: 0x04005020 RID: 20512
		public static Skill[] onScreenSkill = new Skill[10];

		// Token: 0x04005021 RID: 20513
		public Command cmdMenu;

		// Token: 0x04005022 RID: 20514
		public static int firstY;

		// Token: 0x04005023 RID: 20515
		public static int wSkill;

		// Token: 0x04005024 RID: 20516
		public static long deltaTime;

		// Token: 0x04005025 RID: 20517
		public bool isPointerDowning;

		// Token: 0x04005026 RID: 20518
		public bool isChangingCameraMode;

		// Token: 0x04005027 RID: 20519
		private int ptLastDownX;

		// Token: 0x04005028 RID: 20520
		private int ptLastDownY;

		// Token: 0x04005029 RID: 20521
		private int ptFirstDownX;

		// Token: 0x0400502A RID: 20522
		private int ptFirstDownY;

		// Token: 0x0400502B RID: 20523
		private int ptDownTime;

		// Token: 0x0400502C RID: 20524
		private bool disableSingleClick;

		// Token: 0x0400502D RID: 20525
		public long lastSingleClick;

		// Token: 0x0400502E RID: 20526
		public bool clickMoving;

		// Token: 0x0400502F RID: 20527
		public bool clickOnTileTop;

		// Token: 0x04005030 RID: 20528
		public bool clickMovingRed;

		// Token: 0x04005031 RID: 20529
		public int clickToX;

		// Token: 0x04005032 RID: 20530
		public int clickToY;

		// Token: 0x04005033 RID: 20531
		private int lastClickCMX;

		// Token: 0x04005034 RID: 20532
		private int lastClickCMY;

		// Token: 0x04005035 RID: 20533
		public int clickMovingP1;

		// Token: 0x04005036 RID: 20534
		public int clickMovingTimeOut;

		// Token: 0x04005037 RID: 20535
		public static bool isNewClanMessage;

		// Token: 0x04005038 RID: 20536
		private long lastFire;

		// Token: 0x04005039 RID: 20537
		private long lastUsePotion;

		// Token: 0x0400503A RID: 20538
		public int auto;

		// Token: 0x0400503B RID: 20539
		public int dem;

		// Token: 0x0400503C RID: 20540
		private string strTam = string.Empty;

		// Token: 0x0400503D RID: 20541
		public bool isFreez;

		// Token: 0x0400503E RID: 20542
		public bool isUseFreez;

		// Token: 0x0400503F RID: 20543
		public static Image imgTrans;

		// Token: 0x04005040 RID: 20544
		public bool isRongThanXuatHien;

		// Token: 0x04005041 RID: 20545
		public bool isRongNamek;

		// Token: 0x04005042 RID: 20546
		public bool isSuperPower;

		// Token: 0x04005043 RID: 20547
		public int tPower;

		// Token: 0x04005044 RID: 20548
		public int xPower;

		// Token: 0x04005045 RID: 20549
		public int yPower;

		// Token: 0x04005046 RID: 20550
		public int dxPower;

		// Token: 0x04005047 RID: 20551
		public bool activeRongThan;

		// Token: 0x04005048 RID: 20552
		public bool isMeCallRongThan;

		// Token: 0x04005049 RID: 20553
		public int mautroi;

		// Token: 0x0400504A RID: 20554
		public int mapRID;

		// Token: 0x0400504B RID: 20555
		public int zoneRID;

		// Token: 0x0400504C RID: 20556
		public int bgRID = -1;

		// Token: 0x0400504D RID: 20557
		public static int tam = 0;

		// Token: 0x0400504E RID: 20558
		public static bool isAutoPlay;

		// Token: 0x0400504F RID: 20559
		public static bool canAutoPlay;

		// Token: 0x04005050 RID: 20560
		public static bool isChangeZone;

		// Token: 0x04005051 RID: 20561
		private int timeSkill;

		// Token: 0x04005052 RID: 20562
		private int nSkill;

		// Token: 0x04005053 RID: 20563
		private int selectedIndexSkill = -1;

		// Token: 0x04005054 RID: 20564
		private Skill lastSkill;

		// Token: 0x04005055 RID: 20565
		private bool doSeleckSkillFlag;

		// Token: 0x04005056 RID: 20566
		public string strCapcha;

		// Token: 0x04005057 RID: 20567
		public bool flareFindFocus;

		// Token: 0x04005058 RID: 20568
		private int flareTime;

		// Token: 0x04005059 RID: 20569
		public int keyTouchSkill = -1;

		// Token: 0x0400505A RID: 20570
		public static long lastTick;

		// Token: 0x0400505B RID: 20571
		public static long currTick;

		// Token: 0x0400505C RID: 20572
		public static long lastXS;

		// Token: 0x0400505D RID: 20573
		public static long currXS;

		// Token: 0x0400505E RID: 20574
		public static int secondXS;

		// Token: 0x0400505F RID: 20575
		public int runArrow;

		// Token: 0x04005060 RID: 20576
		public static int isPaintRada;

		// Token: 0x04005061 RID: 20577
		public static Image imgNut;

		// Token: 0x04005062 RID: 20578
		public static Image imgNutF;

		// Token: 0x04005063 RID: 20579
		public static Image imgCapsule;

		// Token: 0x04005064 RID: 20580
		public static Image imgCapsuleF;

		// Token: 0x04005065 RID: 20581
		public static Image imgChangeZone;

		// Token: 0x04005066 RID: 20582
		public static Image imgChangeZoneF;

		// Token: 0x04005067 RID: 20583
		public static Image imgFusion;

		// Token: 0x04005068 RID: 20584
		public static Image imgFusionF;

		// Token: 0x04005069 RID: 20585
		public static Image imgNextRight;

		// Token: 0x0400506A RID: 20586
		public static Image imgNextRightF;

		// Token: 0x0400506B RID: 20587
		public static Image imgNextLeft;

		// Token: 0x0400506C RID: 20588
		public static Image imgNextLeftF;

		// Token: 0x0400506D RID: 20589
		public static Image imgNextCenter;

		// Token: 0x0400506E RID: 20590
		public static Image imgNextCenterF;

		// Token: 0x0400506F RID: 20591
		public int[] keyCapcha;

		// Token: 0x04005070 RID: 20592
		public static Image imgCapcha;

		// Token: 0x04005071 RID: 20593
		public string keyInput;

		// Token: 0x04005072 RID: 20594
		public static int disXC;

		// Token: 0x04005073 RID: 20595
		public static bool isPaint = true;

		// Token: 0x04005074 RID: 20596
		public static int shock_scr;

		// Token: 0x04005075 RID: 20597
		private static int[] shock_x = new int[]
		{
			1,
			-1,
			1,
			-1
		};

		// Token: 0x04005076 RID: 20598
		private static int[] shock_y = new int[]
		{
			1,
			-1,
			-1,
			1
		};

		// Token: 0x04005077 RID: 20599
		private int tDoubleDelay;

		// Token: 0x04005078 RID: 20600
		public static Image arrow;

		// Token: 0x04005079 RID: 20601
		private static int yTouchBar;

		// Token: 0x0400507A RID: 20602
		private static int xC;

		// Token: 0x0400507B RID: 20603
		private static int yC;

		// Token: 0x0400507C RID: 20604
		public int xR;

		// Token: 0x0400507D RID: 20605
		public int yR;

		// Token: 0x0400507E RID: 20606
		private static int xF;

		// Token: 0x0400507F RID: 20607
		private static int yF;

		// Token: 0x04005080 RID: 20608
		public static int xHP;

		// Token: 0x04005081 RID: 20609
		public static int yHP;

		// Token: 0x04005082 RID: 20610
		private static int xTG;

		// Token: 0x04005083 RID: 20611
		private static int yTG;

		// Token: 0x04005084 RID: 20612
		public static int[] xS;

		// Token: 0x04005085 RID: 20613
		public static int[] yS;

		// Token: 0x04005086 RID: 20614
		public static int xSkill;

		// Token: 0x04005087 RID: 20615
		public static int ySkill;

		// Token: 0x04005088 RID: 20616
		public static int padSkill;

		// Token: 0x04005089 RID: 20617
		public long dMP;

		// Token: 0x0400508A RID: 20618
		public int twMp;

		// Token: 0x0400508B RID: 20619
		public bool isInjureMp;

		// Token: 0x0400508C RID: 20620
		public long dHP;

		// Token: 0x0400508D RID: 20621
		public int twHp;

		// Token: 0x0400508E RID: 20622
		public bool isInjureHp;

		// Token: 0x0400508F RID: 20623
		private long curr;

		// Token: 0x04005090 RID: 20624
		private long last;

		// Token: 0x04005091 RID: 20625
		private int secondVS;

		// Token: 0x04005092 RID: 20626
		private int[] idVS = new int[]
		{
			-1,
			-1
		};

		// Token: 0x04005093 RID: 20627
		public static string[] flyTextString;

		// Token: 0x04005094 RID: 20628
		public static int[] flyTextX;

		// Token: 0x04005095 RID: 20629
		public static int[] flyTextY;

		// Token: 0x04005096 RID: 20630
		public static int[] flyTextYTo;

		// Token: 0x04005097 RID: 20631
		public static int[] flyTextDx;

		// Token: 0x04005098 RID: 20632
		public static int[] flyTextDy;

		// Token: 0x04005099 RID: 20633
		public static int[] flyTextState;

		// Token: 0x0400509A RID: 20634
		public static int[] flyTextColor;

		// Token: 0x0400509B RID: 20635
		public static int[] flyTime;

		// Token: 0x0400509C RID: 20636
		public static int[] splashX;

		// Token: 0x0400509D RID: 20637
		public static int[] splashY;

		// Token: 0x0400509E RID: 20638
		public static int[] splashState;

		// Token: 0x0400509F RID: 20639
		public static int[] splashF;

		// Token: 0x040050A0 RID: 20640
		public static int[] splashDir;

		// Token: 0x040050A1 RID: 20641
		public static Image[] imgSplash;

		// Token: 0x040050A2 RID: 20642
		public static int cmdBarX;

		// Token: 0x040050A3 RID: 20643
		public static int cmdBarY;

		// Token: 0x040050A4 RID: 20644
		public static int cmdBarW;

		// Token: 0x040050A5 RID: 20645
		public static int hpBarX;

		// Token: 0x040050A6 RID: 20646
		public static int hpBarY;

		// Token: 0x040050A7 RID: 20647
		public static int spBarW;

		// Token: 0x040050A8 RID: 20648
		public static int mpBarW;

		// Token: 0x040050A9 RID: 20649
		public static int expBarW;

		// Token: 0x040050AA RID: 20650
		public static int girlHPBarY;

		// Token: 0x040050AB RID: 20651
		public static long hpBarW;

		// Token: 0x040050AC RID: 20652
		private int imgScrW;

		// Token: 0x040050AD RID: 20653
		public static int popupY;

		// Token: 0x040050AE RID: 20654
		public static int popupX;

		// Token: 0x040050AF RID: 20655
		private string alertURL;

		// Token: 0x040050B0 RID: 20656
		private string fnick;

		// Token: 0x040050B1 RID: 20657
		public static int popupW = 140;

		// Token: 0x040050B2 RID: 20658
		public static int popupH = 160;

		// Token: 0x040050B3 RID: 20659
		public static int cmySK;

		// Token: 0x040050B4 RID: 20660
		public static int cmtoYSK;

		// Token: 0x040050B5 RID: 20661
		public static int cmdySK;

		// Token: 0x040050B6 RID: 20662
		public static int cmvySK;

		// Token: 0x040050B7 RID: 20663
		public static int cmyLimSK;

		// Token: 0x040050B8 RID: 20664
		public static int columns = 6;

		// Token: 0x040050B9 RID: 20665
		public static int indexEff = 0;

		// Token: 0x040050BA RID: 20666
		public Command cmdDead;

		// Token: 0x040050BB RID: 20667
		public static bool notPaint = false;

		// Token: 0x040050BC RID: 20668
		public static bool isPing = false;

		// Token: 0x040050BD RID: 20669
		public static int INFO = 0;

		// Token: 0x040050BE RID: 20670
		public static int STORE = 1;

		// Token: 0x040050BF RID: 20671
		public static int ZONE = 2;

		// Token: 0x040050C0 RID: 20672
		public static int UPGRADE = 3;

		// Token: 0x040050C1 RID: 20673
		private int Hitem = 30;

		// Token: 0x040050C2 RID: 20674
		private int maxSizeRow = 5;

		// Token: 0x040050C3 RID: 20675
		public PopUpYesNo popUpYesNo;

		// Token: 0x040050C4 RID: 20676
		public static MyVector vChatVip = new MyVector();

		// Token: 0x040050C5 RID: 20677
		public bool isFireWorks;

		// Token: 0x040050C6 RID: 20678
		public int[] winnumber;

		// Token: 0x040050C7 RID: 20679
		public int[] randomNumber;

		// Token: 0x040050C8 RID: 20680
		public int[] tMove;

		// Token: 0x040050C9 RID: 20681
		public int[] moveCount;

		// Token: 0x040050CA RID: 20682
		public int[] delayMove;

		// Token: 0x040050CB RID: 20683
		public int moveIndex;

		// Token: 0x040050CC RID: 20684
		private string strFinish;

		// Token: 0x040050CD RID: 20685
		private int tShow;

		// Token: 0x040050CE RID: 20686
		private int xChatVip;

		// Token: 0x040050CF RID: 20687
		private int currChatWidth;

		// Token: 0x040050D0 RID: 20688
		private bool startChat;

		// Token: 0x040050D1 RID: 20689
		public sbyte percentMabu;

		// Token: 0x040050D2 RID: 20690
		public bool mabuEff;

		// Token: 0x040050D3 RID: 20691
		public int tMabuEff;

		// Token: 0x040050D4 RID: 20692
		public static bool isPaintChatVip;

		// Token: 0x040050D5 RID: 20693
		public static sbyte mabuPercent;

		// Token: 0x040050D6 RID: 20694
		public static sbyte isNewMember;

		// Token: 0x040050D7 RID: 20695
		private string yourNumber = string.Empty;

		// Token: 0x040050D8 RID: 20696
		private string[] strPaint;

		// Token: 0x040050D9 RID: 20697
		public static Image imgHP_NEW;

		// Token: 0x040050DA RID: 20698
		public static InfoPhuBan phuban_Info;

		// Token: 0x040050DB RID: 20699
		public static FrameImage fra_PVE_Bar_0;

		// Token: 0x040050DC RID: 20700
		public static FrameImage fra_PVE_Bar_1;

		// Token: 0x040050DD RID: 20701
		public static Image imgVS;

		// Token: 0x040050DE RID: 20702
		public static Image imgBall;

		// Token: 0x040050DF RID: 20703
		public static Image imgKhung;

		// Token: 0x040050E0 RID: 20704
		public static Image imgBgIOS;
	}
}
