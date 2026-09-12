using System;
using Game2.Assets.src.e;
using Game2.Assets.src.f;
using Game2.Assets.src.g;
using Game2.Mod.XMAP;
using UnityEngine;

namespace Game2
{
	// Token: 0x02000383 RID: 899
	public class Controller : IMessageHandler
	{
		// Token: 0x0600280B RID: 10251 RVA: 0x0026C16A File Offset: 0x0026A36A
		public static Controller gI()
		{
			if (Controller.me == null)
			{
				Controller.me = new Controller();
			}
			return Controller.me;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x0026C182 File Offset: 0x0026A382
		public void onConnectOK(bool isMain1)
		{
			Controller.isMain = isMain1;
			mSystem.onConnectOK();
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0026C18F File Offset: 0x0026A38F
		public void onConnectionFail(bool isMain1)
		{
			Controller.isMain = isMain1;
			mSystem.onConnectionFail();
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0026C19C File Offset: 0x0026A39C
		public void onDisconnected(bool isMain1)
		{
			Controller.isMain = isMain1;
			mSystem.onDisconnected();
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0026C1AC File Offset: 0x0026A3AC
		public void requestItemPlayer(Message msg)
		{
			try
			{
				int num = (int)msg.reader().readUnsignedByte();
				Item item = GameScr.currentCharViewInfo.arrItemBody[num];
				item.saleCoinLock = msg.reader().readInt();
				item.sys = (int)msg.reader().readByte();
				item.options = new MyVector();
				try
				{
					for (;;)
					{
						item.options.addElement(new ItemOption((int)msg.reader().readUnsignedByte(), (int)msg.reader().readUnsignedShort()));
					}
				}
				catch (Exception)
				{
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x0026C24C File Offset: 0x0026A44C
		public void onMessage(Message msg)
		{
			GameCanvas.debugSession.removeAllElements();
			GameCanvas.debug("SA1", 2);
			try
			{
				Debug.Log("<<<Read cmd= " + msg.command.ToString());
				Char @char = null;
				MyVector myVector = new MyVector();
				int num = 0;
				GameCanvas.timeLoading = 15;
				Controller2.readMessage(msg);
				switch (msg.command)
				{
				case -112:
				{
					sbyte b102 = msg.reader().readByte();
					if (b102 == 0)
					{
						GameScr.findMobInMap(msg.reader().readByte()).clearBody();
					}
					if (b102 == 1)
					{
						GameScr.findMobInMap(msg.reader().readByte()).setBody(msg.reader().readShort());
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -111:
				case -110:
				case -108:
				case -106:
				case -105:
				case -104:
				case -103:
				case -102:
				case -101:
				case -100:
				case -89:
				case -78:
				case -75:
				case -73:
				case -72:
				case -71:
				case -58:
				case -56:
				case -55:
				case -54:
				case -49:
				case -48:
				case -40:
				case -39:
				case -38:
				case -33:
				case -27:
				case -17:
				case -16:
				case -15:
				case -13:
				case -12:
				case -11:
				case -10:
				case -9:
				case -8:
				case -7:
				case -6:
				case -5:
				case -3:
				case -2:
				case -1:
				case 4:
				case 5:
				case 8:
				case 9:
				case 10:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 21:
				case 22:
				case 23:
				case 25:
				case 26:
				case 28:
				case 30:
				case 31:
				case 34:
				case 35:
				case 36:
				case 37:
				case 42:
				case 44:
				case 45:
				case 48:
				case 49:
				case 51:
				case 52:
				case 53:
				case 55:
				case 59:
				case 60:
				case 61:
				case 67:
				case 70:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 89:
				case 91:
				case 93:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
					goto IL_8CF1;
				case -109:
					Char.myPetz().cHPGoc = msg.readLong();
					Char.myPetz().cMPGoc = msg.readLong();
					Char.myPetz().cDamGoc = msg.readLong();
					Char.myPetz().cDefGoc = msg.reader().readInt();
					Char.myPetz().cCriticalGoc = msg.reader().readInt();
					goto IL_8CF1;
				case -107:
				{
					sbyte b103 = msg.reader().readByte();
					if (b103 == 0)
					{
						Char.myCharz().havePet = false;
					}
					if (b103 == 1)
					{
						Char.myCharz().havePet = true;
					}
					if (b103 != 2)
					{
						goto IL_8CF1;
					}
					InfoDlg.hide();
					Char.myPetz().head = (int)msg.reader().readShort();
					Char.myPetz().setDefaultPart();
					int num2 = (int)msg.reader().readUnsignedByte();
					Char.myPetz().arrItemBody = new Item[num2];
					for (int num3 = 0; num3 < num2; num3++)
					{
						short num4 = msg.reader().readShort();
						if (num4 != -1)
						{
							Char.myPetz().arrItemBody[num3] = new Item
							{
								template = ItemTemplates.get(num4)
							};
							int num5 = (int)Char.myPetz().arrItemBody[num3].template.type;
							Char.myPetz().arrItemBody[num3].quantity = msg.reader().readInt();
							Char.myPetz().arrItemBody[num3].info = msg.reader().readUTF();
							Char.myPetz().arrItemBody[num3].content = msg.reader().readUTF();
							int num6 = (int)msg.reader().readUnsignedByte();
							if (num6 != 0)
							{
								Char.myPetz().arrItemBody[num3].itemOption = new ItemOption[num6];
								for (int num7 = 0; num7 < Char.myPetz().arrItemBody[num3].itemOption.Length; num7++)
								{
									int num8 = (int)msg.reader().readUnsignedByte();
									int param3 = (int)msg.reader().readUnsignedShort();
									if (num8 != -1)
									{
										Char.myPetz().arrItemBody[num3].itemOption[num7] = new ItemOption(num8, param3);
									}
								}
							}
							if (num5 != 0)
							{
								if (num5 == 1)
								{
									Char.myPetz().leg = (int)Char.myPetz().arrItemBody[num3].template.part;
								}
							}
							else
							{
								Char.myPetz().body = (int)Char.myPetz().arrItemBody[num3].template.part;
							}
						}
					}
					Char.myPetz().cHP = msg.readLong();
					Char.myPetz().cHPFull = msg.readLong();
					Char.myPetz().cMP = msg.readLong();
					Char.myPetz().cMPFull = msg.readLong();
					Char.myPetz().cDamFull = msg.readLong();
					Char.myPetz().cName = msg.reader().readUTF();
					Char.myPetz().currStrLevel = msg.reader().readUTF();
					Char.myPetz().cPower = msg.reader().readLong();
					Char.myPetz().cTiemNang = msg.reader().readLong();
					Char.myPetz().petStatus = msg.reader().readByte();
					Char.myPetz().cStamina = (int)msg.reader().readShort();
					Char.myPetz().cMaxStamina = msg.reader().readShort();
					Char.myPetz().cCriticalFull = (int)msg.reader().readByte();
					Char.myPetz().cDefull = (long)msg.reader().readShort();
					Char.myPetz().arrPetSkill = new Skill[(int)msg.reader().readByte()];
					for (int num9 = 0; num9 < Char.myPetz().arrPetSkill.Length; num9++)
					{
						short num10 = msg.reader().readShort();
						if (num10 != -1)
						{
							Char.myPetz().arrPetSkill[num9] = Skills.get(num10);
						}
						else
						{
							Char.myPetz().arrPetSkill[num9] = new Skill();
							Char.myPetz().arrPetSkill[num9].template = null;
							Char.myPetz().arrPetSkill[num9].moreInfo = msg.reader().readUTF();
						}
					}
					if (!ModFunc.userOpenPet)
					{
						return;
					}
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[][]
						{
							new string[]
							{
								string.Empty
							}
						};
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
						GameCanvas.panel.setTypePetMain();
						GameCanvas.panel.show();
						ModFunc.userOpenPet = false;
						goto IL_8CF1;
					}
					GameCanvas.panel.tabName[21] = mResources.petMainTab;
					GameCanvas.panel.setTypePetMain();
					GameCanvas.panel.show();
					ModFunc.userOpenPet = false;
					goto IL_8CF1;
				}
				case -99:
					InfoDlg.hide();
					if (msg.reader().readByte() == 0)
					{
						GameCanvas.panel.vEnemy.removeAllElements();
						int num11 = (int)msg.reader().readUnsignedByte();
						for (int num12 = 0; num12 < num11; num12++)
						{
							Char char2 = new Char();
							char2.charID = msg.reader().readInt();
							char2.head = (int)msg.reader().readShort();
							char2.headICON = (int)msg.reader().readShort();
							char2.body = (int)msg.reader().readShort();
							char2.leg = (int)msg.reader().readShort();
							char2.bag = (int)msg.reader().readShort();
							char2.cName = msg.reader().readUTF();
							InfoItem infoItem = new InfoItem(msg.reader().readUTF());
							bool flag8 = msg.reader().readBoolean();
							infoItem.charInfo = char2;
							infoItem.isOnline = flag8;
							GameCanvas.panel.vEnemy.addElement(infoItem);
						}
						GameCanvas.panel.setTypeEnemy();
						GameCanvas.panel.show();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				case -98:
				{
					bool flag17 = msg.reader().readByte() != 0;
					GameCanvas.menu.showMenu = false;
					if (!flag17)
					{
						GameCanvas.startYesNoDlg(msg.reader().readUTF(), new Command(mResources.YES, GameCanvas.instance, 888397, msg.reader().readUTF()), new Command(mResources.NO, GameCanvas.instance, 888396, null));
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -97:
					Char.myCharz().cNangdong = (long)msg.reader().readInt();
					goto IL_8CF1;
				case -96:
				{
					sbyte typeTop = msg.reader().readByte();
					GameCanvas.panel.vTop.removeAllElements();
					string topName = msg.reader().readUTF();
					sbyte size = msg.reader().readByte();
					for (int i = 0; i < (int)size; i++)
					{
						int rank = msg.reader().readInt();
						int pId = msg.reader().readInt();
						short headID = msg.reader().readShort();
						short headICON = msg.reader().readShort();
						short body = msg.reader().readShort();
						short leg = msg.reader().readShort();
						string name = msg.reader().readUTF();
						string info2 = msg.reader().readUTF();
						TopInfo topInfo = new TopInfo
						{
							rank = rank,
							headID = (int)headID,
							headICON = (int)headICON,
							body = body,
							leg = leg,
							name = name,
							info = info2,
							info2 = msg.reader().readUTF(),
							pId = pId
						};
						GameCanvas.panel.vTop.addElement(topInfo);
					}
					GameCanvas.panel.topName = topName;
					GameCanvas.panel.setTypeTop(typeTop);
					GameCanvas.panel.show();
					goto IL_8CF1;
				}
				case -95:
				{
					sbyte type = msg.reader().readByte();
					if (type == 0)
					{
						int num13 = msg.reader().readInt();
						short templateId = msg.reader().readShort();
						long hp = msg.readLong();
						SoundMn.gI().explode_1();
						if (num13 == Char.myCharz().charID)
						{
							Char.myCharz().mobMe = new Mob(num13, false, false, false, false, false, (int)templateId, 1, hp, 0, hp, (short)(Char.myCharz().cx + ((Char.myCharz().cdir != 1) ? -40 : 40)), (short)Char.myCharz().cy, 4, 0)
							{
								isMobMe = true
							};
							EffecMn.addEff(new Effect(18, Char.myCharz().mobMe.x, Char.myCharz().mobMe.y, 2, 10, -1));
							Char.myCharz().tMobMeBorn = 30;
							GameScr.vMob.addElement(Char.myCharz().mobMe);
						}
						else
						{
							@char = GameScr.findCharInMap(num13);
							if (@char != null)
							{
								@char.mobMe = new Mob(num13, false, false, false, false, false, (int)templateId, 1, hp, 0, hp, (short)@char.cx, (short)@char.cy, 4, 0)
								{
									isMobMe = true
								};
								GameScr.vMob.addElement(@char.mobMe);
							}
							else if (GameScr.findMobInMap(num13) == null)
							{
								Mob mob7 = new Mob(num13, false, false, false, false, false, (int)templateId, 1, hp, 0, hp, -100, -100, 4, 0)
								{
									isMobMe = true
								};
								GameScr.vMob.addElement(mob7);
							}
						}
					}
					if (type == 1)
					{
						int num14 = msg.reader().readInt();
						int mobId = (int)msg.reader().readByte();
						if (num14 == Char.myCharz().charID)
						{
							if (GameScr.findMobInMap(mobId) != null)
							{
								Char.myCharz().mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
							}
						}
						else
						{
							@char = GameScr.findCharInMap(num14);
							if (@char != null && GameScr.findMobInMap(mobId) != null)
							{
								@char.mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
							}
						}
					}
					if (type == 2)
					{
						int num15 = msg.reader().readInt();
						int num16 = msg.reader().readInt();
						long dameHit = msg.readLong();
						long cHPNew = msg.readLong();
						if (num15 == Char.myCharz().charID)
						{
							@char = GameScr.findCharInMap(num16);
							if (@char != null)
							{
								@char.cHPNew = cHPNew;
								if (Char.myCharz().mobMe.isBusyAttackSomeOne)
								{
									@char.doInjure(dameHit, 0L, false, true);
								}
								else
								{
									Char.myCharz().mobMe.dame = dameHit;
									Char.myCharz().mobMe.setAttack(@char);
								}
							}
						}
						else
						{
							Mob mob8 = GameScr.findMobInMap(num15);
							if (mob8 != null)
							{
								if (num16 == Char.myCharz().charID)
								{
									Char.myCharz().cHPNew = cHPNew;
									if (mob8.isBusyAttackSomeOne)
									{
										Char.myCharz().doInjure(dameHit, 0L, false, true);
									}
									else
									{
										mob8.dame = dameHit;
										mob8.setAttack(Char.myCharz());
									}
								}
								else
								{
									@char = GameScr.findCharInMap(num16);
									if (@char != null)
									{
										@char.cHPNew = cHPNew;
										if (mob8.isBusyAttackSomeOne)
										{
											@char.doInjure(dameHit, 0L, false, true);
										}
										else
										{
											mob8.dame = dameHit;
											mob8.setAttack(@char);
										}
									}
								}
							}
						}
					}
					if (type == 3)
					{
						int num17 = msg.reader().readInt();
						int mobId2 = msg.reader().readInt();
						long hp2 = msg.readLong();
						long dame = msg.readLong();
						@char = null;
						@char = ((Char.myCharz().charID != num17) ? GameScr.findCharInMap(num17) : Char.myCharz());
						if (@char != null)
						{
							Mob mob8 = GameScr.findMobInMap(mobId2);
							if (@char.mobMe != null)
							{
								@char.mobMe.attackOtherMob(mob8);
							}
							if (mob8 != null)
							{
								mob8.hp = hp2;
								mob8.updateHp_bar();
								if (dame == 0L)
								{
									mob8.x = mob8.xFirst;
									mob8.y = mob8.yFirst;
									GameScr.startFlyText(mResources.miss, mob8.x, mob8.y - mob8.h, 0, -2, mFont.MISS);
								}
								else
								{
									GameScr.startFlyText("-" + dame.ToString(), mob8.x, mob8.y - mob8.h, 0, -2, mFont.ORANGE);
								}
							}
						}
					}
					if (type == 5)
					{
						int num18 = msg.reader().readInt();
						sbyte b61 = msg.reader().readByte();
						int mobId3 = msg.reader().readInt();
						int num19 = msg.readInt3Byte();
						int hp3 = msg.readInt3Byte();
						@char = null;
						@char = ((num18 != Char.myCharz().charID) ? GameScr.findCharInMap(num18) : Char.myCharz());
						if (@char == null)
						{
							return;
						}
						if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
						{
							@char.setSkillPaint(GameScr.sks[(int)b61], 0);
						}
						else
						{
							@char.setSkillPaint(GameScr.sks[(int)b61], 1);
						}
						Mob mob9 = GameScr.findMobInMap(mobId3);
						if (@char.cx <= mob9.x)
						{
							@char.cdir = 1;
						}
						else
						{
							@char.cdir = -1;
						}
						@char.mobFocus = mob9;
						mob9.hp = (long)hp3;
						mob9.updateHp_bar();
						if (num19 == 0)
						{
							mob9.x = mob9.xFirst;
							mob9.y = mob9.yFirst;
							GameScr.startFlyText(mResources.miss, mob9.x, mob9.y - mob9.h, 0, -2, mFont.MISS);
						}
						else
						{
							GameScr.startFlyText("-" + num19.ToString(), mob9.x, mob9.y - mob9.h, 0, -2, mFont.ORANGE);
						}
					}
					if (type == 6)
					{
						int num20 = msg.reader().readInt();
						if (num20 == Char.myCharz().charID)
						{
							Char.myCharz().mobMe.startDie();
						}
						else
						{
							Char char14 = GameScr.findCharInMap(num20);
							if (char14 != null)
							{
								char14.mobMe.startDie();
							}
						}
					}
					if (type != 7)
					{
						goto IL_8CF1;
					}
					int num21 = msg.reader().readInt();
					if (num21 == Char.myCharz().charID)
					{
						Char.myCharz().mobMe = null;
						for (int num22 = 0; num22 < GameScr.vMob.size(); num22++)
						{
							if (((Mob)GameScr.vMob.elementAt(num22)).mobId == num21)
							{
								GameScr.vMob.removeElementAt(num22);
							}
						}
						goto IL_8CF1;
					}
					@char = GameScr.findCharInMap(num21);
					for (int num23 = 0; num23 < GameScr.vMob.size(); num23++)
					{
						if (((Mob)GameScr.vMob.elementAt(num23)).mobId == num21)
						{
							GameScr.vMob.removeElementAt(num23);
						}
					}
					if (@char != null)
					{
						@char.mobMe = null;
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -94:
					while (msg.reader().available() > 0)
					{
						short num24 = msg.reader().readShort();
						int num25 = msg.reader().readInt();
						for (int num26 = 0; num26 < Char.myCharz().vSkill.size(); num26++)
						{
							Skill skill = (Skill)Char.myCharz().vSkill.elementAt(num26);
							if (skill != null && skill.skillId == num24 && num25 < skill.coolDown)
							{
								skill.lastTimeUseThisSkill = mSystem.currentTimeMillis() - (long)(skill.coolDown - num25);
							}
						}
					}
					goto IL_8CF1;
				case -93:
				{
					short num27 = msg.reader().readShort();
					BgItem.newSmallVersion = new sbyte[(int)num27];
					for (int j = 0; j < (int)num27; j++)
					{
						BgItem.newSmallVersion[j] = msg.reader().readByte();
					}
					goto IL_8CF1;
				}
				case -92:
					Main.typeClient = (int)msg.reader().readByte();
					if (Rms.loadRMSString("ResVersion") == null)
					{
						Rms.clearAll();
					}
					Rms.saveRMSInt("clienttype", Main.typeClient);
					Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
					if (Rms.loadRMSString("ResVersion") == null)
					{
						GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
						goto IL_8CF1;
					}
					goto IL_8CF1;
				case -91:
				{
					sbyte b62 = msg.reader().readByte();
					GameCanvas.panel.mapNames = new string[(int)b62];
					GameCanvas.panel.planetNames = new string[(int)b62];
					for (int num28 = 0; num28 < (int)b62; num28++)
					{
						GameCanvas.panel.mapNames[num28] = msg.reader().readUTF();
						GameCanvas.panel.planetNames[num28] = msg.reader().readUTF();
					}
					AutoXmap.ShowPanelMapTrans();
					goto IL_8CF1;
				}
				case -90:
				{
					sbyte b104 = msg.reader().readByte();
					int num29 = msg.reader().readInt();
					@char = ((Char.myCharz().charID != num29) ? GameScr.findCharInMap(num29) : Char.myCharz());
					if (b104 != -1)
					{
						short num30 = msg.reader().readShort();
						short num31 = msg.reader().readShort();
						short num32 = msg.reader().readShort();
						sbyte isMonkey = msg.reader().readByte();
						if (@char != null)
						{
							if (@char.charID == num29)
							{
								@char.isMask = true;
								@char.isMonkey = isMonkey;
								if (@char.isMonkey != 0)
								{
									@char.isWaitMonkey = false;
									@char.isLockMove = false;
								}
							}
							else if (@char != null)
							{
								@char.isMask = true;
								@char.isMonkey = isMonkey;
							}
							if (num30 != -1)
							{
								@char.head = (int)num30;
							}
							if (num31 != -1)
							{
								@char.body = (int)num31;
							}
							if (num32 != -1)
							{
								@char.leg = (int)num32;
							}
						}
					}
					if (b104 == -1 && @char != null)
					{
						@char.isMask = false;
						@char.isMonkey = 0;
					}
					if (@char == null)
					{
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -88:
					GameCanvas.endDlg();
					GameCanvas.serverScreen.switchToMe();
					goto IL_8CF1;
				case -87:
				{
					msg.reader().mark(100000);
					this.createData(msg.reader(), true);
					msg.reader().reset();
					sbyte[] data3 = new sbyte[msg.reader().available()];
					msg.reader().readFully(ref data3);
					sbyte[] data4 = new sbyte[]
					{
						GameScr.vcData
					};
					Rms.saveRMS("NRdataVersion", data4);
					LoginScr.isUpdateData = false;
					if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
						Service.gI().clientOk();
						return;
					}
					goto IL_8CF1;
				}
				case -86:
				{
					sbyte b63 = msg.reader().readByte();
					if (b63 == 0)
					{
						int playerID = msg.reader().readInt();
						GameScr.gI().giaodich(playerID);
					}
					if (b63 == 1)
					{
						int num33 = msg.reader().readInt();
						Char char3 = GameScr.findCharInMap(num33);
						if (char3 == null)
						{
							return;
						}
						GameCanvas.panel.setTypeGiaoDich(char3);
						GameCanvas.panel.show();
						Service.gI().getPlayerMenu(num33);
					}
					if (b63 == 2)
					{
						sbyte b64 = msg.reader().readByte();
						for (int num34 = 0; num34 < GameCanvas.panel.vMyGD.size(); num34++)
						{
							Item item2 = (Item)GameCanvas.panel.vMyGD.elementAt(num34);
							if (item2.indexUI == (int)b64)
							{
								GameCanvas.panel.vMyGD.removeElement(item2);
								break;
							}
						}
					}
					if (b63 == 6)
					{
						GameCanvas.panel.isFriendLock = true;
						if (GameCanvas.panel2 != null)
						{
							GameCanvas.panel2.isFriendLock = true;
						}
						GameCanvas.panel.vFriendGD.removeAllElements();
						if (GameCanvas.panel2 != null)
						{
							GameCanvas.panel2.vFriendGD.removeAllElements();
						}
						int friendMoneyGD = msg.reader().readInt();
						sbyte b65 = msg.reader().readByte();
						for (int num35 = 0; num35 < (int)b65; num35++)
						{
							Item item3 = new Item();
							item3.template = ItemTemplates.get(msg.reader().readShort());
							item3.quantity = msg.reader().readInt();
							int num36 = (int)msg.reader().readUnsignedByte();
							if (num36 != 0)
							{
								item3.itemOption = new ItemOption[num36];
								for (int num37 = 0; num37 < item3.itemOption.Length; num37++)
								{
									int num38 = (int)msg.reader().readUnsignedByte();
									int param4 = (int)msg.reader().readUnsignedShort();
									if (num38 != -1)
									{
										item3.itemOption[num37] = new ItemOption(num38, param4);
										item3.compare = GameCanvas.panel.getCompare(item3);
									}
								}
							}
							if (GameCanvas.panel2 != null)
							{
								GameCanvas.panel2.vFriendGD.addElement(item3);
							}
							else
							{
								GameCanvas.panel.vFriendGD.addElement(item3);
							}
						}
						if (GameCanvas.panel2 != null)
						{
							GameCanvas.panel2.setTabGiaoDich(false);
							GameCanvas.panel2.friendMoneyGD = friendMoneyGD;
						}
						else
						{
							GameCanvas.panel.friendMoneyGD = friendMoneyGD;
							if (GameCanvas.panel.currentTabIndex == 2)
							{
								GameCanvas.panel.setTabGiaoDich(false);
							}
						}
					}
					if (b63 != 7)
					{
						goto IL_8CF1;
					}
					InfoDlg.hide();
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.hide();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -85:
				{
					sbyte b105 = msg.reader().readByte();
					if (b105 == 0)
					{
						int num39 = (int)msg.reader().readUnsignedShort();
						sbyte[] data5 = new sbyte[num39];
						msg.reader().read(ref data5, 0, num39);
						GameScr.imgCapcha = Image.createImage(data5, 0, num39);
						GameScr.gI().keyInput = "-----";
						GameScr.gI().strCapcha = msg.reader().readUTF();
						GameScr.gI().keyCapcha = new int[GameScr.gI().strCapcha.Length];
						GameScr.gI().mobCapcha = new Mob();
						GameScr.gI().right = null;
					}
					if (b105 == 1)
					{
						MobCapcha.isAttack = true;
					}
					if (b105 == 2)
					{
						MobCapcha.explode = true;
						GameScr.gI().right = GameScr.gI().cmdFocus;
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -84:
				{
					int index2 = (int)msg.reader().readUnsignedByte();
					Mob mob10 = null;
					try
					{
						mob10 = (Mob)GameScr.vMob.elementAt(index2);
					}
					catch (Exception)
					{
					}
					if (mob10 != null)
					{
						mob10.maxHp = (long)msg.reader().readInt();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -83:
				{
					sbyte b106 = msg.reader().readByte();
					if (b106 == 0)
					{
						int num40 = (int)msg.reader().readShort();
						int bgRID = (int)msg.reader().readShort();
						int num41 = (int)msg.reader().readUnsignedByte();
						int num42 = msg.reader().readInt();
						msg.reader().readUTF();
						int num43 = (int)msg.reader().readShort();
						int num44 = (int)msg.reader().readShort();
						if (msg.reader().readByte() == 1)
						{
							GameScr.gI().isRongNamek = true;
						}
						else
						{
							GameScr.gI().isRongNamek = false;
						}
						GameScr.gI().xR = num43;
						GameScr.gI().yR = num44;
						if (Char.myCharz().charID == num42)
						{
							GameCanvas.panel.hideNow();
							GameScr.gI().activeRongThanEff(true);
						}
						else if (TileMap.mapID == num40 && TileMap.zoneID == num41)
						{
							GameScr.gI().activeRongThanEff(false);
						}
						else if (mGraphics.zoomLevel > 1)
						{
							GameScr.gI().doiMauTroi();
						}
						GameScr.gI().mapRID = num40;
						GameScr.gI().bgRID = bgRID;
						GameScr.gI().zoneRID = num41;
					}
					if (b106 == 1)
					{
						if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
						{
							GameScr.gI().hideRongThanEff();
						}
						else
						{
							GameScr.gI().isRongThanXuatHien = false;
							if (GameScr.gI().isRongNamek)
							{
								GameScr.gI().isRongNamek = false;
							}
						}
					}
					if (b106 != 2)
					{
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -82:
				{
					sbyte size2 = msg.reader().readByte();
					TileMap.tileIndex = new int[(int)size2][][];
					TileMap.tileType = new int[(int)size2][];
					for (int k = 0; k < (int)size2; k++)
					{
						sbyte b66 = msg.reader().readByte();
						TileMap.tileType[k] = new int[(int)b66];
						TileMap.tileIndex[k] = new int[(int)b66][];
						for (int l = 0; l < (int)b66; l++)
						{
							TileMap.tileType[k][l] = msg.reader().readInt();
							sbyte b67 = msg.reader().readByte();
							TileMap.tileIndex[k][l] = new int[(int)b67];
							for (int m = 0; m < (int)b67; m++)
							{
								TileMap.tileIndex[k][l][m] = (int)msg.reader().readByte();
							}
						}
					}
					goto IL_8CF1;
				}
				case -81:
				{
					sbyte b68 = msg.reader().readByte();
					if (b68 == 0)
					{
						string src = msg.reader().readUTF();
						string src2 = msg.reader().readUTF();
						GameCanvas.panel.setTypeCombine();
						GameCanvas.panel.combineInfo = mFont.tahoma_7b_blue.splitFontArray(src, Panel.WIDTH_PANEL);
						GameCanvas.panel.combineTopInfo = mFont.tahoma_7.splitFontArray(src2, Panel.WIDTH_PANEL);
						GameCanvas.panel.show();
					}
					if (b68 == 1)
					{
						GameCanvas.panel.vItemCombine.removeAllElements();
						sbyte b69 = msg.reader().readByte();
						for (int num45 = 0; num45 < (int)b69; num45++)
						{
							sbyte b70 = msg.reader().readByte();
							for (int num46 = 0; num46 < Char.myCharz().arrItemBag.Length; num46++)
							{
								Item item4 = Char.myCharz().arrItemBag[num46];
								if (item4 != null && item4.indexUI == (int)b70)
								{
									item4.isSelect = true;
									GameCanvas.panel.vItemCombine.addElement(item4);
								}
							}
						}
						if (GameCanvas.panel.isShow)
						{
							GameCanvas.panel.setTabCombine();
						}
					}
					if (b68 == 2)
					{
						GameCanvas.panel.combineSuccess = 0;
						GameCanvas.panel.setCombineEff(0);
					}
					if (b68 == 3)
					{
						GameCanvas.panel.combineSuccess = 1;
						GameCanvas.panel.setCombineEff(0);
					}
					if (b68 == 4)
					{
						short iconID = msg.reader().readShort();
						GameCanvas.panel.iconID3 = iconID;
						GameCanvas.panel.combineSuccess = 0;
						GameCanvas.panel.setCombineEff(1);
					}
					if (b68 == 5)
					{
						short iconID2 = msg.reader().readShort();
						GameCanvas.panel.iconID3 = iconID2;
						GameCanvas.panel.combineSuccess = 0;
						GameCanvas.panel.setCombineEff(2);
					}
					if (b68 == 6)
					{
						short iconID3 = msg.reader().readShort();
						short iconID4 = msg.reader().readShort();
						GameCanvas.panel.combineSuccess = 0;
						GameCanvas.panel.setCombineEff(3);
						GameCanvas.panel.iconID1 = iconID3;
						GameCanvas.panel.iconID3 = iconID4;
					}
					if (b68 == 7)
					{
						short iconID5 = msg.reader().readShort();
						GameCanvas.panel.iconID3 = iconID5;
						GameCanvas.panel.combineSuccess = 0;
						GameCanvas.panel.setCombineEff(4);
					}
					if (b68 == 8)
					{
						GameCanvas.panel.iconID3 = -1;
						GameCanvas.panel.combineSuccess = 1;
						GameCanvas.panel.setCombineEff(4);
					}
					short num47 = 21;
					try
					{
						num47 = msg.reader().readShort();
						int num48 = (int)msg.reader().readShort();
						int num49 = (int)msg.reader().readShort();
						GameCanvas.panel.xS = num48 - GameScr.cmx;
						GameCanvas.panel.yS = num49 - GameScr.cmy;
					}
					catch (Exception)
					{
					}
					for (int num50 = 0; num50 < GameScr.vNpc.size(); num50++)
					{
						Npc npc = (Npc)GameScr.vNpc.elementAt(num50);
						if (npc.template.npcTemplateId == (int)num47)
						{
							GameCanvas.panel.xS = npc.cx - GameScr.cmx;
							GameCanvas.panel.yS = npc.cy - GameScr.cmy;
							GameCanvas.panel.idNPC = (int)num47;
							break;
						}
					}
					goto IL_8CF1;
				}
				case -80:
				{
					sbyte b71 = msg.reader().readByte();
					InfoDlg.hide();
					if (b71 == 0)
					{
						GameCanvas.panel.vFriend.removeAllElements();
						int num51 = (int)msg.reader().readUnsignedByte();
						for (int num52 = 0; num52 < num51; num52++)
						{
							Char char4 = new Char();
							char4.charID = msg.reader().readInt();
							char4.head = (int)msg.reader().readShort();
							char4.headICON = (int)msg.reader().readShort();
							char4.body = (int)msg.reader().readShort();
							char4.leg = (int)msg.reader().readShort();
							char4.bag = (int)msg.reader().readUnsignedByte();
							char4.cName = msg.reader().readUTF();
							bool isOnline = msg.reader().readBoolean();
							InfoItem infoItem2 = new InfoItem(mResources.power + ": " + msg.reader().readUTF());
							infoItem2.charInfo = char4;
							infoItem2.isOnline = isOnline;
							GameCanvas.panel.vFriend.addElement(infoItem2);
						}
						GameCanvas.panel.setTypeFriend();
						GameCanvas.panel.show();
					}
					if (b71 == 3)
					{
						MyVector vFriend = GameCanvas.panel.vFriend;
						int num53 = msg.reader().readInt();
						for (int num54 = 0; num54 < vFriend.size(); num54++)
						{
							InfoItem infoItem3 = (InfoItem)vFriend.elementAt(num54);
							if (infoItem3.charInfo != null && infoItem3.charInfo.charID == num53)
							{
								infoItem3.isOnline = msg.reader().readBoolean();
								break;
							}
						}
					}
					if (b71 != 2)
					{
						goto IL_8CF1;
					}
					MyVector vFriend2 = GameCanvas.panel.vFriend;
					int num55 = msg.reader().readInt();
					for (int num56 = 0; num56 < vFriend2.size(); num56++)
					{
						InfoItem infoItem4 = (InfoItem)vFriend2.elementAt(num56);
						if (infoItem4.charInfo != null && infoItem4.charInfo.charID == num55)
						{
							vFriend2.removeElement(infoItem4);
							break;
						}
					}
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.setTabFriend();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -79:
				{
					InfoDlg.hide();
					msg.reader().readInt();
					Char charMenu = GameCanvas.panel.charMenu;
					if (charMenu == null)
					{
						return;
					}
					charMenu.cPower = msg.reader().readLong();
					charMenu.currStrLevel = msg.reader().readUTF();
					goto IL_8CF1;
				}
				case -77:
				{
					short num57 = msg.reader().readShort();
					SmallImage.newSmallVersion = new sbyte[(int)num57];
					SmallImage.maxSmall = num57;
					SmallImage.imgNew = new Small[(int)num57];
					for (int num58 = 0; num58 < (int)num57; num58++)
					{
						SmallImage.newSmallVersion[num58] = msg.reader().readByte();
					}
					goto IL_8CF1;
				}
				case -76:
				{
					sbyte type2 = msg.reader().readByte();
					if (type2 == 0)
					{
						sbyte sz = msg.reader().readByte();
						if (sz <= 0)
						{
							return;
						}
						Char.myCharz().arrArchive = new Archivement[(int)sz];
						for (int n = 0; n < (int)sz; n++)
						{
							Char.myCharz().arrArchive[n] = new Archivement
							{
								info1 = (n + 1).ToString() + ". " + msg.reader().readUTF(),
								info2 = msg.reader().readUTF(),
								money = (int)msg.reader().readShort(),
								isFinish = msg.reader().readBoolean(),
								isRecieve = msg.reader().readBoolean()
							};
						}
						GameCanvas.panel.setTypeArchivement();
						GameCanvas.panel.show();
						goto IL_8CF1;
					}
					else
					{
						if (type2 != 1)
						{
							goto IL_8CF1;
						}
						int idArchive = (int)msg.reader().readUnsignedByte();
						if (Char.myCharz().arrArchive[idArchive] != null)
						{
							Char.myCharz().arrArchive[idArchive].isRecieve = true;
							goto IL_8CF1;
						}
						goto IL_8CF1;
					}
					break;
				}
				case -74:
				{
					if (ServerListScreen.stopDownload)
					{
						return;
					}
					if (!GameCanvas.isGetResourceFromServer())
					{
						Service.gI().getResource(3, null);
						SmallImage.loadBigRMS();
						if (Rms.loadRMSString("acc2") != null || Rms.loadRMSString("userAo2" + ServerListScreen.ipSelect.ToString()) != null)
						{
							LoginScr.isContinueToLogin = true;
						}
						GameCanvas.loginScr = new LoginScr();
						GameCanvas.loginScr.switchToMe();
						return;
					}
					sbyte b72 = msg.reader().readByte();
					if (b72 == 0)
					{
						int num59 = msg.reader().readInt();
						string text3 = Rms.loadRMSString("ResVersion");
						int num60 = (text3 == null || !(text3 != string.Empty)) ? -1 : int.Parse(text3);
						if (Session_ME.gI().isCompareIPConnect())
						{
							if (num60 == -1 || num60 != num59)
							{
								GameCanvas.serverScreen.show2();
							}
							else
							{
								SmallImage.loadBigRMS();
								ServerListScreen.loadScreen = true;
								if (GameCanvas.currentScreen != GameCanvas.loginScr)
								{
									GameCanvas.serverScreen.switchToMe();
								}
							}
						}
						else
						{
							Session_ME.gI().close();
							ServerListScreen.loadScreen = true;
							ServerListScreen.isAutoConect = false;
							ServerListScreen.countDieConnect = 1000;
							GameCanvas.serverScreen.switchToMe();
						}
					}
					if (b72 == 1)
					{
						ServerListScreen.strWait = mResources.downloading_data;
						ServerListScreen.nBig = (int)msg.reader().readShort();
						Service.gI().getResource(2, null);
					}
					if (b72 == 2)
					{
						try
						{
							Controller.isLoadingData = true;
							GameCanvas.endDlg();
							ServerListScreen.demPercent++;
							ServerListScreen.percent = ServerListScreen.demPercent * 100 / ServerListScreen.nBig;
							string[] array8 = Res.split(msg.reader().readUTF(), "/", 0);
							string filename = "x" + mGraphics.zoomLevel.ToString() + array8[array8.Length - 1];
							int num61 = msg.reader().readInt();
							sbyte[] data6 = new sbyte[num61];
							msg.reader().read(ref data6, 0, num61);
							Rms.saveRMS(filename, data6);
						}
						catch (Exception)
						{
							GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
						}
					}
					if (b72 == 3)
					{
						Rms.saveRMSInt("musicSize", ModFunc.musicCount);
						ModFunc.InitMusic();
						Controller.isLoadingData = false;
						Rms.saveRMSString("ResVersion", msg.reader().readInt().ToString() + string.Empty);
						Service.gI().getResource(3, null);
						GameCanvas.endDlg();
						SmallImage.loadBigRMS();
						mSystem.gcc();
						ServerListScreen.bigOk = true;
						ServerListScreen.loadScreen = true;
						GameScr.gI().loadGameScr();
						if (GameCanvas.currentScreen != GameCanvas.loginScr)
						{
							GameCanvas.serverScreen.switchToMe();
						}
					}
					if (b72 != 4)
					{
						goto IL_8CF1;
					}
					string name2 = msg.reader().readUTF();
					sbyte[] data7 = null;
					try
					{
						data7 = NinjaUtil.readByteArray(msg);
					}
					catch (Exception)
					{
						data7 = null;
					}
					if (data7 != null)
					{
						ModFunc.musicCount++;
						Rms.saveRMS("music_" + name2, data7);
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -70:
				{
					GameCanvas.endDlg();
					if (PickMob.tanSat)
					{
						ModFunc.GI().perform(44, true);
						return;
					}
					int avatar2 = (int)msg.reader().readShort();
					string chat3 = msg.reader().readUTF();
					Npc npc2 = new Npc(-1, 0, 0, 0, 0, 0)
					{
						avatar = avatar2
					};
					ChatPopup.addBigMessage(chat3, 100000, npc2);
					sbyte b107 = msg.reader().readByte();
					if (b107 == 0)
					{
						ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null)
						{
							x = GameCanvas.w / 2 - 35,
							y = GameCanvas.h - 35
						};
					}
					if (b107 == 1)
					{
						string p2 = msg.reader().readUTF();
						string caption2 = msg.reader().readUTF();
						ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null)
						{
							x = GameCanvas.w / 2 + 11,
							y = GameCanvas.h - 35
						};
						ChatPopup.serverChatPopUp.cmdMsg2 = new Command(caption2, ChatPopup.serverChatPopUp, 1000, p2)
						{
							x = GameCanvas.w / 2 - 75,
							y = GameCanvas.h - 35
						};
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -69:
					Char.myCharz().cMaxStamina = msg.reader().readShort();
					goto IL_8CF1;
				case -68:
					Char.myCharz().cStamina = (int)msg.reader().readShort();
					goto IL_8CF1;
				case -67:
				{
					int iconId = msg.reader().readInt();
					Debug.Log("GET ICON: " + iconId.ToString());
					try
					{
						sbyte[] data8 = NinjaUtil.readByteArray(msg);
						Image img = this.createImage(data8);
						SmallImage.imgNew[iconId].img = img;
						if (mGraphics.zoomLevel > 1)
						{
							SmallImage.imageRaw.Add(iconId, img);
						}
						goto IL_8CF1;
					}
					catch (Exception)
					{
						SmallImage.imgNew[iconId].img = Image.createRGBImage(new int[1], 1, 1, true);
						goto IL_8CF1;
					}
					break;
				}
				case -66:
					break;
				case -65:
				{
					InfoDlg.hide();
					int num62 = msg.reader().readInt();
					sbyte b73 = msg.reader().readByte();
					if (b73 == 0)
					{
						goto IL_8CF1;
					}
					if (Char.myCharz().charID == num62)
					{
						Controller.isStopReadMessage = true;
						GameScr.lockTick = 500;
						GameScr.gI().center = null;
						if (b73 == 0 || b73 == 1 || b73 == 3)
						{
							Teleport.addTeleport(new Teleport(Char.myCharz().cx, Char.myCharz().cy, Char.myCharz().head, Char.myCharz().cdir, 0, true, (b73 != 1) ? ((int)b73) : Char.myCharz().cgender));
						}
						if (b73 == 2)
						{
							GameScr.lockTick = 50;
							Char.myCharz().hide();
							goto IL_8CF1;
						}
						goto IL_8CF1;
					}
					else
					{
						Char char5 = GameScr.findCharInMap(num62);
						if ((b73 == 0 || b73 == 1 || b73 == 3) && char5 != null)
						{
							char5.isUsePlane = true;
							Teleport.addTeleport(new Teleport(char5.cx, char5.cy, char5.head, char5.cdir, 0, false, (b73 != 1) ? ((int)b73) : char5.cgender)
							{
								id = num62
							});
						}
						if (b73 == 2)
						{
							char5.hide();
							goto IL_8CF1;
						}
						goto IL_8CF1;
					}
					break;
				}
				case -64:
				{
					int num63 = msg.reader().readInt();
					int num64 = (int)msg.reader().readUnsignedByte();
					@char = null;
					@char = ((num63 != Char.myCharz().charID) ? GameScr.findCharInMap(num63) : Char.myCharz());
					if (@char == null)
					{
						return;
					}
					@char.bag = num64;
					for (int num65 = 0; num65 < 54; num65++)
					{
						@char.removeEffChar(0, 201 + num65);
					}
					if (@char.bag >= 201 && @char.bag < 255)
					{
						@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
						{
							typeEff = 5
						});
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -63:
				{
					byte id = msg.reader().readUnsignedByte();
					sbyte size3 = msg.reader().readByte();
					int[] idImages = new int[(int)size3];
					if (size3 > 0)
					{
						for (int i2 = 0; i2 < (int)size3; i2++)
						{
							idImages[i2] = (int)msg.reader().readShort();
							Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaa");
						}
						Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaa");
					}
					short idNew;
					try
					{
						idNew = msg.reader().readShort();
					}
					catch
					{
						idNew = (short)id;
					}
					if (size3 > 0)
					{
						for (int i3 = 0; i3 < (int)size3; i3++)
						{
							try
							{
								idImages[i3] = msg.reader().readInt();
							}
							catch
							{
							}
						}
					}
					ClanImage clanImage3 = new ClanImage
					{
						ID = (int)idNew,
						idImage = idImages
					};
					if (size3 > 0)
					{
						ClanImage.idImages.put(id.ToString() + string.Empty, clanImage3);
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -62:
				{
					byte id2 = msg.reader().readUnsignedByte();
					sbyte size4 = msg.reader().readByte();
					int[] idImage = new int[(int)size4];
					if (size4 > 0)
					{
						for (int i4 = 0; i4 < (int)size4; i4++)
						{
							idImage[i4] = (int)msg.reader().readShort();
							if (idImage[i4] > 0)
							{
								SmallImage.vKeys.addElement(idImage[i4].ToString() + string.Empty);
							}
						}
					}
					short idNew2;
					try
					{
						idNew2 = msg.reader().readShort();
					}
					catch
					{
						idNew2 = (short)id2;
					}
					if (size4 > 0)
					{
						for (int i5 = 0; i5 < (int)size4; i5++)
						{
							try
							{
								idImage[i5] = msg.reader().readInt();
							}
							catch
							{
							}
							if (idImage[i5] > 0)
							{
								SmallImage.vKeys.addElement(idImage[i5].ToString() + string.Empty);
							}
						}
					}
					ClanImage clanImage4 = ClanImage.getClanImage(idNew2);
					if (clanImage4 != null)
					{
						clanImage4.idImage = idImage;
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -61:
				{
					int num66 = msg.reader().readInt();
					if (num66 != Char.myCharz().charID)
					{
						if (GameScr.findCharInMap(num66) == null)
						{
							goto IL_8CF1;
						}
						GameScr.findCharInMap(num66).clanID = msg.reader().readInt();
						if (GameScr.findCharInMap(num66).clanID == -2)
						{
							GameScr.findCharInMap(num66).isCopy = true;
							goto IL_8CF1;
						}
						goto IL_8CF1;
					}
					else
					{
						if (Char.myCharz().clan != null)
						{
							Char.myCharz().clan.ID = msg.reader().readInt();
							goto IL_8CF1;
						}
						goto IL_8CF1;
					}
					break;
				}
				case -60:
				{
					GameCanvas.debug("SA7666", 2);
					int num67 = msg.reader().readInt();
					int num68 = -1;
					if (num67 != Char.myCharz().charID)
					{
						Char char6 = GameScr.findCharInMap(num67);
						if (char6 == null)
						{
							return;
						}
						if (char6.currentMovePoint != null)
						{
							char6.createShadow(char6.cx, char6.cy, 10);
							char6.cx = char6.currentMovePoint.xEnd;
							char6.cy = char6.currentMovePoint.yEnd;
						}
						int num69 = (int)msg.reader().readUnsignedByte();
						if ((TileMap.tileTypeAtPixel(char6.cx, char6.cy) & 2) == 2)
						{
							char6.setSkillPaint(GameScr.sks[num69], 0);
						}
						else
						{
							char6.setSkillPaint(GameScr.sks[num69], 1);
						}
						Char[] array9 = new Char[(int)msg.reader().readByte()];
						for (num = 0; num < array9.Length; num++)
						{
							num68 = msg.reader().readInt();
							Char char7;
							if (num68 == Char.myCharz().charID)
							{
								char7 = Char.myCharz();
								if (!GameScr.isChangeZone && GameScr.isAutoPlay && GameScr.canAutoPlay)
								{
									Service.gI().requestChangeZone(-1, -1);
									GameScr.isChangeZone = true;
								}
							}
							else
							{
								char7 = GameScr.findCharInMap(num68);
							}
							array9[num] = char7;
							if (num == 0)
							{
								if (char6.cx <= char7.cx)
								{
									char6.cdir = 1;
								}
								else
								{
									char6.cdir = -1;
								}
							}
						}
						if (num > 0)
						{
							char6.attChars = new Char[num];
							for (num = 0; num < char6.attChars.Length; num++)
							{
								char6.attChars[num] = array9[num];
							}
							char6.mobFocus = null;
							char6.charFocus = char6.attChars[0];
						}
					}
					else
					{
						msg.reader().readByte();
						msg.reader().readByte();
						num68 = msg.reader().readInt();
					}
					try
					{
						if (msg.reader().readByte() == 1)
						{
							sbyte b74 = msg.reader().readByte();
							if (num68 == Char.myCharz().charID)
							{
								@char = Char.myCharz();
								long num70 = msg.readLong();
								@char.isDie = msg.reader().readBoolean();
								if (@char.isDie)
								{
									Char.isLockKey = true;
								}
								long num71 = 0L;
								bool flag9 = @char.isCrit = msg.reader().readBoolean();
								@char.isMob = false;
								num70 = (@char.damHP = num70 + num71);
								if (b74 == 0)
								{
									@char.doInjure(num70, 0L, flag9, false);
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num68);
								if (@char == null)
								{
									return;
								}
								long num72 = msg.readLong();
								@char.isDie = msg.reader().readBoolean();
								long num73 = 0L;
								bool flag10 = @char.isCrit = msg.reader().readBoolean();
								@char.isMob = false;
								num72 = (@char.damHP = num72 + num73);
								if (b74 == 0)
								{
									@char.doInjure(num72, 0L, flag10, false);
								}
							}
						}
					}
					catch (Exception)
					{
					}
					goto IL_8CF1;
				}
				case -59:
				{
					sbyte typePK = msg.reader().readByte();
					GameScr.gI().player_vs_player(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readUTF(), typePK);
					goto IL_8CF1;
				}
				case -57:
				{
					string strInvite = msg.reader().readUTF();
					int clanID = msg.reader().readInt();
					int code = msg.reader().readInt();
					GameScr.gI().clanInvite(strInvite, clanID, code);
					goto IL_8CF1;
				}
				case -53:
				{
					InfoDlg.hide();
					bool flag11 = false;
					int num74 = msg.reader().readInt();
					if (num74 == -1)
					{
						Char.myCharz().clan = null;
						ClanMessage.vMessage.removeAllElements();
						if (GameCanvas.panel.member != null)
						{
							GameCanvas.panel.member.removeAllElements();
						}
						if (GameCanvas.panel.myMember != null)
						{
							GameCanvas.panel.myMember.removeAllElements();
						}
						if (GameCanvas.currentScreen == GameScr.gI())
						{
							GameCanvas.panel.setTabClans();
						}
						return;
					}
					GameCanvas.panel.tabIcon = null;
					if (Char.myCharz().clan == null)
					{
						Char.myCharz().clan = new Clan();
					}
					Char.myCharz().clan.ID = num74;
					Char.myCharz().clan.name = msg.reader().readUTF();
					Char.myCharz().clan.slogan = msg.reader().readUTF();
					Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
					Char.myCharz().clan.powerPoint = msg.reader().readUTF();
					Char.myCharz().clan.leaderName = msg.reader().readUTF();
					Char.myCharz().clan.currMember = (int)msg.reader().readUnsignedByte();
					Char.myCharz().clan.maxMember = (int)msg.reader().readUnsignedByte();
					Char.myCharz().role = msg.reader().readByte();
					Char.myCharz().clan.clanPoint = msg.reader().readInt();
					Char.myCharz().clan.level = (int)msg.reader().readByte();
					GameCanvas.panel.myMember = new MyVector();
					for (int num75 = 0; num75 < Char.myCharz().clan.currMember; num75++)
					{
						Member member2 = new Member();
						member2.ID = msg.reader().readInt();
						member2.head = msg.reader().readShort();
						member2.headICON = msg.reader().readShort();
						member2.leg = msg.reader().readShort();
						member2.body = msg.reader().readShort();
						member2.name = msg.reader().readUTF();
						member2.role = msg.reader().readByte();
						member2.powerPoint = msg.reader().readUTF();
						member2.donate = msg.reader().readInt();
						member2.receive_donate = msg.reader().readInt();
						member2.clanPoint = msg.reader().readInt();
						member2.curClanPoint = msg.reader().readInt();
						member2.joinTime = NinjaUtil.getDate(msg.reader().readInt());
						GameCanvas.panel.myMember.addElement(member2);
					}
					int num76 = (int)msg.reader().readUnsignedByte();
					for (int num77 = 0; num77 < num76; num77++)
					{
						this.readClanMsg(msg, -1);
					}
					if (GameCanvas.panel.isSearchClan || GameCanvas.panel.isViewMember || GameCanvas.panel.isMessage)
					{
						GameCanvas.panel.setTabClans();
					}
					if (flag11)
					{
						GameCanvas.panel.setTabClans();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -52:
				{
					sbyte b108 = msg.reader().readByte();
					if (b108 == 0)
					{
						Member member3 = new Member();
						member3.ID = msg.reader().readInt();
						member3.head = msg.reader().readShort();
						member3.headICON = msg.reader().readShort();
						member3.leg = msg.reader().readShort();
						member3.body = msg.reader().readShort();
						member3.name = msg.reader().readUTF();
						member3.role = msg.reader().readByte();
						member3.powerPoint = msg.reader().readUTF();
						member3.donate = msg.reader().readInt();
						member3.receive_donate = msg.reader().readInt();
						member3.clanPoint = msg.reader().readInt();
						member3.joinTime = NinjaUtil.getDate(msg.reader().readInt());
						if (GameCanvas.panel.myMember == null)
						{
							GameCanvas.panel.myMember = new MyVector();
						}
						GameCanvas.panel.myMember.addElement(member3);
						GameCanvas.panel.initTabClans();
					}
					if (b108 == 1)
					{
						GameCanvas.panel.myMember.removeElementAt((int)msg.reader().readByte());
						GameCanvas.panel.currentListLength--;
						GameCanvas.panel.initTabClans();
					}
					if (b108 == 2)
					{
						Member member4 = new Member();
						member4.ID = msg.reader().readInt();
						member4.head = msg.reader().readShort();
						member4.headICON = msg.reader().readShort();
						member4.leg = msg.reader().readShort();
						member4.body = msg.reader().readShort();
						member4.name = msg.reader().readUTF();
						member4.role = msg.reader().readByte();
						member4.powerPoint = msg.reader().readUTF();
						member4.donate = msg.reader().readInt();
						member4.receive_donate = msg.reader().readInt();
						member4.clanPoint = msg.reader().readInt();
						member4.joinTime = NinjaUtil.getDate(msg.reader().readInt());
						for (int num78 = 0; num78 < GameCanvas.panel.myMember.size(); num78++)
						{
							Member member5 = (Member)GameCanvas.panel.myMember.elementAt(num78);
							if (member5.ID == member4.ID)
							{
								if (Char.myCharz().charID == member4.ID)
								{
									Char.myCharz().role = member4.role;
								}
								Member o = member4;
								GameCanvas.panel.myMember.removeElement(member5);
								GameCanvas.panel.myMember.insertElementAt(o, num78);
								return;
							}
						}
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -51:
					InfoDlg.hide();
					this.readClanMsg(msg, 0);
					if (GameCanvas.panel.isMessage && GameCanvas.panel.type == 5)
					{
						GameCanvas.panel.initTabClans();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				case -50:
				{
					InfoDlg.hide();
					GameCanvas.panel.member = new MyVector();
					sbyte b75 = msg.reader().readByte();
					for (int num79 = 0; num79 < (int)b75; num79++)
					{
						Member member6 = new Member();
						member6.ID = msg.reader().readInt();
						member6.head = msg.reader().readShort();
						member6.headICON = msg.reader().readShort();
						member6.leg = msg.reader().readShort();
						member6.body = msg.reader().readShort();
						member6.name = msg.reader().readUTF();
						member6.role = msg.reader().readByte();
						member6.powerPoint = msg.reader().readUTF();
						member6.donate = msg.reader().readInt();
						member6.receive_donate = msg.reader().readInt();
						member6.clanPoint = msg.reader().readInt();
						member6.joinTime = NinjaUtil.getDate(msg.reader().readInt());
						GameCanvas.panel.member.addElement(member6);
					}
					GameCanvas.panel.isViewMember = true;
					GameCanvas.panel.isSearchClan = false;
					GameCanvas.panel.isMessage = false;
					GameCanvas.panel.currentListLength = GameCanvas.panel.member.size() + 2;
					GameCanvas.panel.initTabClans();
					goto IL_8CF1;
				}
				case -47:
				{
					InfoDlg.hide();
					sbyte b76 = msg.reader().readByte();
					if (b76 == 0)
					{
						GameCanvas.panel.clanReport = mResources.cannot_find_clan;
						GameCanvas.panel.clans = null;
					}
					else
					{
						GameCanvas.panel.clans = new Clan[(int)b76];
						for (int num80 = 0; num80 < GameCanvas.panel.clans.Length; num80++)
						{
							GameCanvas.panel.clans[num80] = new Clan();
							GameCanvas.panel.clans[num80].ID = msg.reader().readInt();
							GameCanvas.panel.clans[num80].name = msg.reader().readUTF();
							GameCanvas.panel.clans[num80].slogan = msg.reader().readUTF();
							GameCanvas.panel.clans[num80].imgID = (int)msg.reader().readUnsignedByte();
							GameCanvas.panel.clans[num80].powerPoint = msg.reader().readUTF();
							GameCanvas.panel.clans[num80].leaderName = msg.reader().readUTF();
							GameCanvas.panel.clans[num80].currMember = (int)msg.reader().readUnsignedByte();
							GameCanvas.panel.clans[num80].maxMember = (int)msg.reader().readUnsignedByte();
							GameCanvas.panel.clans[num80].date = msg.reader().readInt();
						}
					}
					GameCanvas.panel.isSearchClan = true;
					GameCanvas.panel.isViewMember = false;
					GameCanvas.panel.isMessage = false;
					if (GameCanvas.panel.isSearchClan)
					{
						GameCanvas.panel.initTabClans();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -46:
				{
					InfoDlg.hide();
					sbyte type3 = msg.reader().readByte();
					if (type3 == 1 || type3 == 3)
					{
						GameCanvas.endDlg();
						ClanImage.vClanImage.removeAllElements();
						int size5 = (int)msg.reader().readUnsignedByte();
						for (int i6 = 0; i6 < size5; i6++)
						{
							byte id3 = msg.reader().readUnsignedByte();
							string name3 = msg.reader().readUTF();
							int xu = msg.reader().readInt();
							int luong = msg.reader().readInt();
							ClanImage clanImage5 = new ClanImage
							{
								ID = (int)id3,
								name = name3,
								xu = xu,
								luong = luong
							};
							if (!ClanImage.isExistClanImage(clanImage5.ID))
							{
								ClanImage.addClanImage(clanImage5);
							}
							else
							{
								ClanImage.getClanImage((short)clanImage5.ID).name = clanImage5.name;
								ClanImage.getClanImage((short)clanImage5.ID).xu = clanImage5.xu;
								ClanImage.getClanImage((short)clanImage5.ID).luong = clanImage5.luong;
							}
						}
						if (Char.myCharz().clan != null)
						{
							GameCanvas.panel.changeIcon();
						}
					}
					if (type3 == 4)
					{
						Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
						Char.myCharz().clan.slogan = msg.reader().readUTF();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -45:
				{
					sbyte type4 = msg.reader().readByte();
					int playerId = msg.reader().readInt();
					short skillId = msg.reader().readShort();
					if (type4 == 20)
					{
						sbyte b77 = msg.reader().readByte();
						sbyte dir = msg.reader().readByte();
						short timeGong = msg.reader().readShort();
						bool isFly = msg.reader().readByte() != 0;
						sbyte typePaint = msg.reader().readByte();
						sbyte typeItem = -1;
						try
						{
							typeItem = msg.reader().readByte();
						}
						catch (Exception)
						{
						}
						sbyte level = -1;
						try
						{
							level = msg.reader().readByte();
						}
						catch (Exception)
						{
						}
						@char = ((Char.myCharz().charID != playerId) ? GameScr.findCharInMap(playerId) : Char.myCharz());
						@char.SetSkillPaint_NEW(skillId, isFly, b77, typePaint, dir, timeGong, typeItem, level);
					}
					if (type4 == 21)
					{
						Point point = new Point
						{
							x = (int)msg.reader().readShort(),
							y = (int)msg.reader().readShort()
						};
						short timeDame = msg.reader().readShort();
						short rangeDame = msg.reader().readShort();
						sbyte typePaint2 = 0;
						sbyte typeItem2 = -1;
						Point[] targets = null;
						@char = ((Char.myCharz().charID != playerId) ? GameScr.findCharInMap(playerId) : Char.myCharz());
						try
						{
							typePaint2 = msg.reader().readByte();
							targets = new Point[(int)msg.reader().readByte()];
							for (int i7 = 0; i7 < targets.Length; i7++)
							{
								targets[i7] = new Point
								{
									type = msg.reader().readByte()
								};
								if (targets[i7].type == 0)
								{
									targets[i7].id = (int)msg.reader().readByte();
								}
								else
								{
									targets[i7].id = msg.reader().readInt();
								}
							}
						}
						catch (Exception)
						{
						}
						try
						{
							typeItem2 = msg.reader().readByte();
						}
						catch (Exception)
						{
						}
						sbyte level2 = -1;
						try
						{
							level2 = msg.reader().readByte();
						}
						catch (Exception)
						{
						}
						@char.SetSkillPaint_STT(1, skillId, point, timeDame, rangeDame, typePaint2, targets, typeItem2, level2);
					}
					if (type4 == 0)
					{
						Res.outz("id use= " + playerId.ToString());
						if (Char.myCharz().charID != playerId)
						{
							@char = GameScr.findCharInMap(playerId);
							if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
							{
								@char.setSkillPaint(GameScr.sks[(int)skillId], 0);
							}
							else
							{
								@char.setSkillPaint(GameScr.sks[(int)skillId], 1);
								@char.delayFall = 20;
							}
						}
						else
						{
							Char.myCharz().saveLoadPreviousSkill();
							Res.outz("LOAD LAST SKILL");
						}
						sbyte b78 = msg.reader().readByte();
						Res.outz("npc size= " + b78.ToString());
						for (int num81 = 0; num81 < (int)b78; num81++)
						{
							sbyte b79 = msg.reader().readByte();
							sbyte b80 = msg.reader().readByte();
							Res.outz("index= " + b79.ToString());
							if (skillId >= 42 && skillId <= 48)
							{
								((Mob)GameScr.vMob.elementAt((int)b79)).isFreez = true;
								((Mob)GameScr.vMob.elementAt((int)b79)).seconds = (int)b80;
								((Mob)GameScr.vMob.elementAt((int)b79)).last = (((Mob)GameScr.vMob.elementAt((int)b79)).cur = mSystem.currentTimeMillis());
							}
						}
						sbyte b81 = msg.reader().readByte();
						for (int num82 = 0; num82 < (int)b81; num82++)
						{
							int num83 = msg.reader().readInt();
							sbyte b82 = msg.reader().readByte();
							Res.outz("player ID= " + num83.ToString() + " my ID= " + Char.myCharz().charID.ToString());
							if (skillId >= 42 && skillId <= 48)
							{
								if (num83 == Char.myCharz().charID)
								{
									if (!Char.myCharz().isFlyAndCharge && !Char.myCharz().isStandAndCharge)
									{
										GameScr.gI().isFreez = true;
										Char.myCharz().isFreez = true;
										Char.myCharz().freezSeconds = (int)b82;
										Char.myCharz().lastFreez = (Char.myCharz().currFreez = mSystem.currentTimeMillis());
										Char.myCharz().isLockMove = true;
									}
								}
								else
								{
									@char = GameScr.findCharInMap(num83);
									if (@char != null && !@char.isFlyAndCharge && !@char.isStandAndCharge)
									{
										@char.isFreez = true;
										@char.seconds = (int)b82;
										@char.freezSeconds = (int)b82;
										@char.lastFreez = (GameScr.findCharInMap(num83).currFreez = mSystem.currentTimeMillis());
									}
								}
							}
						}
					}
					if (type4 == 1 && playerId != Char.myCharz().charID)
					{
						GameScr.findCharInMap(playerId).isCharge = true;
					}
					if (type4 == 3)
					{
						if (playerId == Char.myCharz().charID)
						{
							Char.myCharz().isCharge = false;
							SoundMn.gI().taitaoPause();
							Char.myCharz().saveLoadPreviousSkill();
						}
						else
						{
							GameScr.findCharInMap(playerId).isCharge = false;
						}
					}
					if (type4 == 4)
					{
						if (playerId == Char.myCharz().charID)
						{
							Char.myCharz().seconds = (int)(msg.reader().readShort() - 1000);
							Char.myCharz().last = mSystem.currentTimeMillis();
							Res.outz("second= " + Char.myCharz().seconds.ToString() + " last= " + Char.myCharz().last.ToString());
						}
						else if (GameScr.findCharInMap(playerId) != null)
						{
							int cgender = GameScr.findCharInMap(playerId).cgender;
							if (cgender != 0)
							{
								if (cgender == 1)
								{
									GameScr.findCharInMap(playerId).useChargeSkill(true);
								}
							}
							else
							{
								GameScr.findCharInMap(playerId).useChargeSkill(false);
							}
							GameScr.findCharInMap(playerId).skillTemplateId = (int)skillId;
							GameScr.findCharInMap(playerId).isUseSkillAfterCharge = true;
							GameScr.findCharInMap(playerId).seconds = (int)msg.reader().readShort();
							GameScr.findCharInMap(playerId).last = mSystem.currentTimeMillis();
						}
					}
					if (type4 == 5)
					{
						if (playerId == Char.myCharz().charID)
						{
							Char.myCharz().stopUseChargeSkill();
						}
						else if (GameScr.findCharInMap(playerId) != null)
						{
							GameScr.findCharInMap(playerId).stopUseChargeSkill();
						}
					}
					if (type4 == 6)
					{
						if (playerId == Char.myCharz().charID)
						{
							Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)skillId], 0);
						}
						else if (GameScr.findCharInMap(playerId) != null)
						{
							GameScr.findCharInMap(playerId).setAutoSkillPaint(GameScr.sks[(int)skillId], 0);
							SoundMn.gI().gong();
						}
					}
					if (type4 == 7)
					{
						if (playerId == Char.myCharz().charID)
						{
							Char.myCharz().seconds = (int)msg.reader().readShort();
							Res.outz("second = " + Char.myCharz().seconds.ToString());
							Char.myCharz().last = mSystem.currentTimeMillis();
						}
						else if (GameScr.findCharInMap(playerId) != null)
						{
							GameScr.findCharInMap(playerId).useChargeSkill(true);
							GameScr.findCharInMap(playerId).seconds = (int)msg.reader().readShort();
							GameScr.findCharInMap(playerId).last = mSystem.currentTimeMillis();
							SoundMn.gI().gong();
						}
					}
					if (type4 == 8 && playerId != Char.myCharz().charID && GameScr.findCharInMap(playerId) != null)
					{
						GameScr.findCharInMap(playerId).setAutoSkillPaint(GameScr.sks[(int)skillId], 0);
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -44:
				{
					bool flag12 = false;
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						flag12 = true;
					}
					sbyte type_shop = msg.reader().readByte();
					int tabSz = (int)msg.reader().readUnsignedByte();
					Char.myCharz().arrItemShop = new Item[tabSz][];
					GameCanvas.panel.shopTabName = new string[tabSz + ((!flag12) ? 1 : 0)][];
					for (int num84 = 0; num84 < GameCanvas.panel.shopTabName.Length; num84++)
					{
						GameCanvas.panel.shopTabName[num84] = new string[2];
					}
					if (type_shop == 2)
					{
						GameCanvas.panel.maxPageShop = new int[tabSz];
						GameCanvas.panel.currPageShop = new int[tabSz];
					}
					if (!flag12)
					{
						GameCanvas.panel.shopTabName[tabSz] = mResources.inventory;
					}
					for (int i8 = 0; i8 < tabSz; i8++)
					{
						string[] name4 = Res.split(msg.reader().readUTF(), "\n", 0);
						if (type_shop == 2)
						{
							GameCanvas.panel.maxPageShop[i8] = (int)msg.reader().readUnsignedByte();
						}
						if (name4.Length == 2)
						{
							GameCanvas.panel.shopTabName[i8] = name4;
						}
						if (name4.Length == 1)
						{
							GameCanvas.panel.shopTabName[i8][0] = name4[0];
							GameCanvas.panel.shopTabName[i8][1] = string.Empty;
						}
						int itemSz = (int)msg.reader().readUnsignedByte();
						Char.myCharz().arrItemShop[i8] = new Item[itemSz];
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
						if (type_shop == 1)
						{
							Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy2;
						}
						for (int num85 = 0; num85 < itemSz; num85++)
						{
							short itemId = msg.reader().readShort();
							if (itemId != -1)
							{
								Char.myCharz().arrItemShop[i8][num85] = new Item();
								Char.myCharz().arrItemShop[i8][num85].template = ItemTemplates.get(itemId);
								Res.outz(string.Concat(new string[]
								{
									"name ",
									i8.ToString(),
									" = ",
									Char.myCharz().arrItemShop[i8][num85].template.name,
									" id templat= ",
									Char.myCharz().arrItemShop[i8][num85].template.id.ToString()
								}));
								if (type_shop == 8)
								{
									Char.myCharz().arrItemShop[i8][num85].buyCoin = msg.reader().readInt();
									Char.myCharz().arrItemShop[i8][num85].buyGold = msg.reader().readInt();
									Char.myCharz().arrItemShop[i8][num85].quantity = msg.reader().readInt();
								}
								else if (type_shop == 4)
								{
									Char.myCharz().arrItemShop[i8][num85].reason = msg.reader().readUTF();
								}
								else if (type_shop == 0)
								{
									Char.myCharz().arrItemShop[i8][num85].buyCoin = msg.reader().readInt();
									Char.myCharz().arrItemShop[i8][num85].buyGold = msg.reader().readInt();
								}
								else if (type_shop == 1)
								{
									Char.myCharz().arrItemShop[i8][num85].powerRequire = msg.reader().readLong();
								}
								else if (type_shop == 2)
								{
									Char.myCharz().arrItemShop[i8][num85].itemId = (int)msg.reader().readShort();
									Char.myCharz().arrItemShop[i8][num85].buyCoin = msg.reader().readInt();
									Char.myCharz().arrItemShop[i8][num85].buyGold = msg.reader().readInt();
									Char.myCharz().arrItemShop[i8][num85].buyType = msg.reader().readByte();
									Char.myCharz().arrItemShop[i8][num85].quantity = msg.reader().readInt();
									Char.myCharz().arrItemShop[i8][num85].isMe = msg.reader().readByte();
								}
								else if (type_shop == 3)
								{
									Char.myCharz().arrItemShop[i8][num85].isBuySpec = true;
									Char.myCharz().arrItemShop[i8][num85].iconSpec = msg.reader().readShort();
									Char.myCharz().arrItemShop[i8][num85].buySpec = msg.reader().readInt();
								}
								int optSz = (int)msg.reader().readUnsignedByte();
								if (optSz != 0)
								{
									Char.myCharz().arrItemShop[i8][num85].itemOption = new ItemOption[optSz];
									for (int j2 = 0; j2 < Char.myCharz().arrItemShop[i8][num85].itemOption.Length; j2++)
									{
										int optId = (int)msg.reader().readUnsignedByte();
										int param5 = (int)msg.reader().readUnsignedShort();
										if (optId != -1)
										{
											Char.myCharz().arrItemShop[i8][num85].itemOption[j2] = new ItemOption(optId, param5);
											Char.myCharz().arrItemShop[i8][num85].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[i8][num85]);
										}
									}
								}
								sbyte isNew = msg.reader().readByte();
								Char.myCharz().arrItemShop[i8][num85].newItem = (isNew != 0);
								if (msg.reader().readByte() == 1)
								{
									int headTemp = (int)msg.reader().readShort();
									int bodyTemp = (int)msg.reader().readShort();
									int legTemp = (int)msg.reader().readShort();
									int bagTemp = (int)msg.reader().readShort();
									Char.myCharz().arrItemShop[i8][num85].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
								}
							}
						}
					}
					if (flag12)
					{
						if (type_shop != 2)
						{
							GameCanvas.panel2 = new Panel();
							GameCanvas.panel2.tabName[7] = new string[][]
							{
								new string[]
								{
									string.Empty
								}
							};
							GameCanvas.panel2.setTypeBodyOnly();
							GameCanvas.panel2.show();
						}
						else
						{
							GameCanvas.panel2 = new Panel();
							GameCanvas.panel2.setTypeKiGuiOnly();
							GameCanvas.panel2.show();
						}
					}
					GameCanvas.panel.tabName[1] = GameCanvas.panel.shopTabName;
					if (type_shop == 2)
					{
						string[][] array10 = GameCanvas.panel.tabName[1];
						if (flag12)
						{
							GameCanvas.panel.tabName[1] = new string[][]
							{
								array10[0],
								array10[1],
								array10[2],
								array10[3]
							};
						}
						else
						{
							GameCanvas.panel.tabName[1] = new string[][]
							{
								array10[0],
								array10[1],
								array10[2],
								array10[3],
								array10[4]
							};
						}
					}
					GameCanvas.panel.setTypeShop((int)type_shop);
					GameCanvas.panel.show();
					goto IL_8CF1;
				}
				case -43:
				{
					sbyte itemAction = msg.reader().readByte();
					sbyte where = msg.reader().readByte();
					sbyte index3 = msg.reader().readByte();
					string info3 = msg.reader().readUTF();
					GameCanvas.panel.itemRequest(itemAction, info3, where, index3);
					goto IL_8CF1;
				}
				case -42:
					Char.myCharz().cHPGoc = msg.readLong();
					Char.myCharz().cMPGoc = msg.readLong();
					Char.myCharz().cDamGoc = msg.readLong();
					Char.myCharz().cHPFull = msg.readLong();
					Char.myCharz().cMPFull = msg.readLong();
					Char.myCharz().cHP = msg.readLong();
					Char.myCharz().cMP = msg.readLong();
					Char.myCharz().cspeed = (int)msg.reader().readByte();
					Char.myCharz().hpFrom1000TiemNang = msg.reader().readByte();
					Char.myCharz().mpFrom1000TiemNang = msg.reader().readByte();
					Char.myCharz().damFrom1000TiemNang = msg.reader().readByte();
					Char.myCharz().cDamFull = msg.readLong();
					Char.myCharz().cDefull = (long)msg.reader().readInt();
					Char.myCharz().cCriticalFull = (int)msg.reader().readByte();
					Char.myCharz().cTiemNang = msg.reader().readLong();
					Char.myCharz().expForOneAdd = msg.reader().readShort();
					Char.myCharz().cDefGoc = msg.reader().readInt();
					Char.myCharz().cCriticalGoc = (int)msg.reader().readByte();
					try
					{
						Char.myCharz().tlDef = msg.readInt3Byte();
						Char.myCharz().tlPst = msg.readInt3Byte();
						Char.myCharz().tlNeDon = msg.readInt3Byte();
						Char.myCharz().tlHutHp = msg.readInt3Byte();
						Char.myCharz().tlHutMp = msg.readInt3Byte();
						Char.myCharz().tileGiamTDHS = msg.readInt3Byte();
						Char.myCharz().timeGiamTDHS = msg.readInt3Byte();
						Char.myCharz().khangTDHS = msg.reader().readBool();
						Char.myCharz().isKhongLanh = msg.reader().readBool();
						Char.myCharz().wearingVoHinh = msg.reader().readBool();
						Char.myCharz().teleport = msg.reader().readBool();
					}
					catch
					{
						Char.myCharz().tlDef = 0;
						Char.myCharz().tlPst = 0;
						Char.myCharz().tlNeDon = 0;
						Char.myCharz().tlHutHp = 0;
						Char.myCharz().tlHutMp = 0;
						Char.myCharz().tileGiamTDHS = 0;
						Char.myCharz().timeGiamTDHS = 0;
						Char.myCharz().khangTDHS = false;
						Char.myCharz().isKhongLanh = false;
						Char.myCharz().wearingVoHinh = false;
						Char.myCharz().teleport = false;
					}
					InfoDlg.hide();
					goto IL_8CF1;
				case -41:
				{
					sbyte b83 = msg.reader().readByte();
					Char.myCharz().strLevel = new string[(int)b83];
					for (int i9 = 0; i9 < (int)b83; i9++)
					{
						string text4 = msg.reader().readUTF();
						Char.myCharz().strLevel[i9] = text4;
					}
					Res.outz("---   xong  level caption cmd : " + msg.command.ToString());
					goto IL_8CF1;
				}
				case -37:
					if (msg.reader().readByte() == 0)
					{
						Char.myCharz().head = (int)msg.reader().readShort();
						Char.myCharz().setDefaultPart();
						int num86 = (int)msg.reader().readUnsignedByte();
						Res.outz("num body = " + num86.ToString());
						Char.myCharz().arrItemBody = new Item[num86];
						for (int num87 = 0; num87 < num86; num87++)
						{
							short num88 = msg.reader().readShort();
							if (num88 != -1)
							{
								Char.myCharz().arrItemBody[num87] = new Item();
								Char.myCharz().arrItemBody[num87].template = ItemTemplates.get(num88);
								int num89 = (int)Char.myCharz().arrItemBody[num87].template.type;
								Char.myCharz().arrItemBody[num87].quantity = msg.reader().readInt();
								Char.myCharz().arrItemBody[num87].info = msg.reader().readUTF();
								Char.myCharz().arrItemBody[num87].content = msg.reader().readUTF();
								int num90 = (int)msg.reader().readUnsignedByte();
								if (num90 != 0)
								{
									Char.myCharz().arrItemBody[num87].itemOption = new ItemOption[num90];
									for (int num91 = 0; num91 < Char.myCharz().arrItemBody[num87].itemOption.Length; num91++)
									{
										int num92 = (int)msg.reader().readUnsignedByte();
										int param6 = (int)msg.reader().readUnsignedShort();
										if (num92 != -1)
										{
											Char.myCharz().arrItemBody[num87].itemOption[num91] = new ItemOption(num92, param6);
										}
									}
								}
								if (num89 != 0)
								{
									if (num89 == 1)
									{
										Char.myCharz().leg = (int)Char.myCharz().arrItemBody[num87].template.part;
									}
								}
								else
								{
									Char.myCharz().body = (int)Char.myCharz().arrItemBody[num87].template.part;
								}
							}
						}
						goto IL_8CF1;
					}
					goto IL_8CF1;
				case -36:
				{
					sbyte b84 = msg.reader().readByte();
					Res.outz("cAction= " + b84.ToString());
					if (b84 == 0)
					{
						int num93 = (int)msg.reader().readUnsignedByte();
						Char.myCharz().arrItemBag = new Item[num93];
						GameScr.hpPotion = 0;
						Res.outz("numC=" + num93.ToString());
						for (int j3 = 0; j3 < num93; j3++)
						{
							short num94 = msg.reader().readShort();
							if (num94 != -1)
							{
								Char.myCharz().arrItemBag[j3] = new Item();
								Char.myCharz().arrItemBag[j3].template = ItemTemplates.get(num94);
								Char.myCharz().arrItemBag[j3].quantity = msg.reader().readInt();
								Char.myCharz().arrItemBag[j3].info = msg.reader().readUTF();
								Char.myCharz().arrItemBag[j3].content = msg.reader().readUTF();
								Char.myCharz().arrItemBag[j3].indexUI = j3;
								int num95 = (int)msg.reader().readUnsignedByte();
								if (num95 != 0)
								{
									Char.myCharz().arrItemBag[j3].itemOption = new ItemOption[num95];
									for (int k2 = 0; k2 < Char.myCharz().arrItemBag[j3].itemOption.Length; k2++)
									{
										int num96 = (int)msg.reader().readUnsignedByte();
										int param7 = (int)msg.reader().readUnsignedShort();
										if (num96 != -1)
										{
											Char.myCharz().arrItemBag[j3].itemOption[k2] = new ItemOption(num96, param7);
										}
									}
									Char.myCharz().arrItemBag[j3].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemBag[j3]);
								}
								sbyte type7 = Char.myCharz().arrItemBag[j3].template.type;
								if (Char.myCharz().arrItemBag[j3].template.type == 6)
								{
									GameScr.hpPotion += Char.myCharz().arrItemBag[j3].quantity;
								}
							}
						}
					}
					if (b84 != 2)
					{
						goto IL_8CF1;
					}
					sbyte b85 = msg.reader().readByte();
					int quantity = msg.reader().readInt();
					int quantity2 = Char.myCharz().arrItemBag[(int)b85].quantity;
					Char.myCharz().arrItemBag[(int)b85].quantity = quantity;
					if (Char.myCharz().arrItemBag[(int)b85].quantity < quantity2 && Char.myCharz().arrItemBag[(int)b85].template.type == 6)
					{
						GameScr.hpPotion -= quantity2 - Char.myCharz().arrItemBag[(int)b85].quantity;
					}
					if (Char.myCharz().arrItemBag[(int)b85].quantity == 0)
					{
						Char.myCharz().arrItemBag[(int)b85] = null;
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -35:
				{
					sbyte b86 = msg.reader().readByte();
					Res.outz("cAction= " + b86.ToString());
					if (b86 == 0)
					{
						int num97 = (int)msg.reader().readUnsignedByte();
						Char.myCharz().arrItemBox = new Item[num97];
						GameCanvas.panel.hasUse = 0;
						for (int num98 = 0; num98 < num97; num98++)
						{
							short num99 = msg.reader().readShort();
							if (num99 != -1)
							{
								Char.myCharz().arrItemBox[num98] = new Item();
								Char.myCharz().arrItemBox[num98].template = ItemTemplates.get(num99);
								Char.myCharz().arrItemBox[num98].quantity = msg.reader().readInt();
								Char.myCharz().arrItemBox[num98].info = msg.reader().readUTF();
								Char.myCharz().arrItemBox[num98].content = msg.reader().readUTF();
								int num100 = (int)msg.reader().readUnsignedByte();
								if (num100 != 0)
								{
									Char.myCharz().arrItemBox[num98].itemOption = new ItemOption[num100];
									for (int num101 = 0; num101 < Char.myCharz().arrItemBox[num98].itemOption.Length; num101++)
									{
										int num102 = (int)msg.reader().readUnsignedByte();
										int param8 = (int)msg.reader().readUnsignedShort();
										if (num102 != -1)
										{
											Char.myCharz().arrItemBox[num98].itemOption[num101] = new ItemOption(num102, param8);
										}
									}
								}
								GameCanvas.panel.hasUse++;
							}
						}
					}
					if (b86 == 1)
					{
						bool isBoxClan = false;
						try
						{
							if (msg.reader().readByte() == 1)
							{
								isBoxClan = true;
							}
						}
						catch (Exception)
						{
						}
						GameCanvas.panel.setTypeBox();
						GameCanvas.panel.isBoxClan = isBoxClan;
						GameCanvas.panel.show();
					}
					if (b86 != 2)
					{
						goto IL_8CF1;
					}
					sbyte b87 = msg.reader().readByte();
					int quantity3 = msg.reader().readInt();
					Char.myCharz().arrItemBox[(int)b87].quantity = quantity3;
					if (Char.myCharz().arrItemBox[(int)b87].quantity == 0)
					{
						Char.myCharz().arrItemBox[(int)b87] = null;
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -34:
				{
					sbyte b88 = msg.reader().readByte();
					Res.outz("act= " + b88.ToString());
					if (b88 == 0 && GameScr.gI().magicTree != null)
					{
						Res.outz("toi duoc day");
						MagicTree magicTree = GameScr.gI().magicTree;
						magicTree.id = (int)msg.reader().readShort();
						magicTree.name = msg.reader().readUTF();
						magicTree.name = Res.changeString(magicTree.name);
						magicTree.x = (int)msg.reader().readShort();
						magicTree.y = (int)msg.reader().readShort();
						magicTree.level = (int)msg.reader().readByte();
						magicTree.currPeas = (int)msg.reader().readShort();
						magicTree.maxPeas = (int)msg.reader().readShort();
						Res.outz("curr Peas= " + magicTree.currPeas.ToString());
						magicTree.strInfo = msg.reader().readUTF();
						magicTree.seconds = msg.reader().readInt();
						magicTree.timeToRecieve = magicTree.seconds;
						sbyte b89 = msg.reader().readByte();
						magicTree.peaPostionX = new int[(int)b89];
						magicTree.peaPostionY = new int[(int)b89];
						for (int num103 = 0; num103 < (int)b89; num103++)
						{
							magicTree.peaPostionX[num103] = (int)msg.reader().readByte();
							magicTree.peaPostionY[num103] = (int)msg.reader().readByte();
						}
						magicTree.isUpdate = msg.reader().readBool();
						magicTree.last = (magicTree.cur = mSystem.currentTimeMillis());
						GameScr.gI().magicTree.isUpdateTree = true;
					}
					if (b88 == 1)
					{
						myVector = new MyVector();
						try
						{
							while (msg.reader().available() > 0)
							{
								string caption3 = msg.reader().readUTF();
								myVector.addElement(new Command(caption3, GameCanvas.instance, 888392, null));
							}
						}
						catch (Exception ex4)
						{
							Cout.println("Loi MAGIC_TREE " + ex4.ToString());
						}
						GameCanvas.menu.startAt(myVector, 3);
					}
					if (b88 == 2)
					{
						GameScr.gI().magicTree.remainPeas = (int)msg.reader().readShort();
						GameScr.gI().magicTree.seconds = msg.reader().readInt();
						GameScr.gI().magicTree.last = (GameScr.gI().magicTree.cur = mSystem.currentTimeMillis());
						GameScr.gI().magicTree.isUpdateTree = true;
						GameScr.gI().magicTree.isPeasEffect = true;
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -32:
				{
					short num104 = msg.reader().readShort();
					int num105 = msg.reader().readInt();
					sbyte[] array11 = null;
					Image image = null;
					try
					{
						array11 = new sbyte[num105];
						for (int num106 = 0; num106 < num105; num106++)
						{
							array11[num106] = msg.reader().readByte();
						}
						image = Image.createImage(array11, 0, num105);
						BgItem.imgNew.put(num104.ToString() + string.Empty, image);
					}
					catch (Exception)
					{
						array11 = null;
						BgItem.imgNew.put(num104.ToString() + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
					}
					if (array11 != null)
					{
						if (mGraphics.zoomLevel > 1)
						{
							Rms.saveRMS(mGraphics.zoomLevel.ToString() + "bgItem" + num104.ToString(), array11);
						}
						BgItemMn.blendcurrBg(num104, image);
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -31:
				{
					TileMap.vItemBg.removeAllElements();
					short num107 = msg.reader().readShort();
					for (int num108 = 0; num108 < (int)num107; num108++)
					{
						BgItem bgItem = new BgItem();
						bgItem.id = num108;
						bgItem.idImage = msg.reader().readShort();
						bgItem.layer = msg.reader().readByte();
						bgItem.dx = (int)msg.reader().readShort();
						bgItem.dy = (int)msg.reader().readShort();
						sbyte b90 = msg.reader().readByte();
						bgItem.tileX = new int[(int)b90];
						bgItem.tileY = new int[(int)b90];
						for (int num109 = 0; num109 < (int)b90; num109++)
						{
							bgItem.tileX[num108] = (int)msg.reader().readByte();
							bgItem.tileY[num108] = (int)msg.reader().readByte();
						}
						TileMap.vItemBg.addElement(bgItem);
					}
					goto IL_8CF1;
				}
				case -30:
					this.messageSubCommand(msg);
					goto IL_8CF1;
				case -29:
					this.messageNotLogin(msg);
					goto IL_8CF1;
				case -28:
					this.messageNotMap(msg);
					goto IL_8CF1;
				case -26:
				{
					ServerListScreen.testConnect = 2;
					string msgDlg = msg.reader().readUTF();
					if (msgDlg == "Vui lòng mở giới hạn sức mạnh" || msgDlg == "")
					{
						ModFunc.indexAutoPoint = -1;
						ModFunc.pointIncrease = 0;
						ModFunc.autoPointForPet = false;
						GameScr.info1.addInfo("Chỉ số đã đạt tối đa", 0);
					}
					GameCanvas.startOKDlg(msgDlg);
					InfoDlg.hide();
					LoginScr.isContinueToLogin = false;
					Char.isLoadingMap = false;
					if (GameCanvas.currentScreen == GameCanvas.loginScr)
					{
						GameCanvas.serverScreen.switchToMe();
					}
					if (ModFunc.autoLogin != null)
					{
						ModFunc.autoLogin.waitToNextLogin = false;
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case -25:
					GameScr.info1.addInfo(msg.reader().readUTF(), 0);
					goto IL_8CF1;
				case -24:
					if (GameCanvas.currentScreen is GameScr)
					{
						GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 3000L;
					}
					else
					{
						GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
					}
					Char.isLoadingMap = true;
					GameScr.gI().magicTree = null;
					GameCanvas.isLoading = true;
					GameScr.resetAllvector();
					GameCanvas.endDlg();
					TileMap.vGo.removeAllElements();
					PopUp.vPopups.removeAllElements();
					mSystem.gcc();
					TileMap.mapID = (int)msg.reader().readUnsignedByte();
					TileMap.planetID = msg.reader().readByte();
					TileMap.tileID = (int)msg.reader().readByte();
					TileMap.bgID = (int)msg.reader().readByte();
					TileMap.typeMap = (int)msg.reader().readByte();
					TileMap.mapName = msg.reader().readUTF();
					TileMap.zoneID = (int)msg.reader().readByte();
					try
					{
						TileMap.loadMapFromResource(TileMap.mapID);
					}
					catch (Exception)
					{
						Service.gI().requestMaptemplate(TileMap.mapID);
						this.messWait = msg;
						return;
					}
					this.loadInfoMap(msg);
					try
					{
						TileMap.isMapDouble = (msg.reader().readByte() != 0);
					}
					catch (Exception)
					{
					}
					GameScr.cmx = GameScr.cmtoX;
					GameScr.cmy = GameScr.cmtoY;
					goto IL_8CF1;
				case -23:
					this.LoadAuraNpcs(msg);
					goto IL_8CF1;
				case -22:
					Char.isLockKey = true;
					Char.ischangingMap = true;
					GameScr.gI().timeStartMap = 0;
					GameScr.gI().timeLengthMap = 0;
					Char.myCharz().mobFocus = null;
					Char.myCharz().npcFocus = null;
					Char.myCharz().charFocus = null;
					Char.myCharz().itemFocus = null;
					Char.myCharz().focus.removeAllElements();
					Char.myCharz().testCharId = -9999;
					Char.myCharz().killCharId = -9999;
					GameCanvas.resetBg();
					GameScr.gI().resetButton();
					GameScr.gI().center = null;
					goto IL_8CF1;
				case -21:
				{
					GameCanvas.debug("SA60", 2);
					short itemMapID = msg.reader().readShort();
					for (int num110 = 0; num110 < GameScr.vItemMap.size(); num110++)
					{
						if (((ItemMap)GameScr.vItemMap.elementAt(num110)).itemMapID == (int)itemMapID)
						{
							GameScr.vItemMap.removeElementAt(num110);
							break;
						}
					}
					goto IL_8CF1;
				}
				case -20:
				{
					GameCanvas.debug("SA61", 2);
					Char.myCharz().itemFocus = null;
					short itemMapID2 = msg.reader().readShort();
					int num111 = 0;
					while (num111 < GameScr.vItemMap.size())
					{
						ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(num111);
						if (itemMap2.itemMapID == (int)itemMapID2)
						{
							itemMap2.setPoint(Char.myCharz().cx, Char.myCharz().cy - 10);
							string text5 = msg.reader().readUTF();
							num = 0;
							try
							{
								num = (int)msg.reader().readShort();
								if (itemMap2.template.type == 9)
								{
									num = (int)msg.reader().readShort();
									Char.myCharz().xu += (long)num;
									Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
								}
								else if (itemMap2.template.type == 10)
								{
									num = (int)msg.reader().readShort();
									Char.myCharz().luong += num;
									Char.myCharz().luongStr = mSystem.numberTostring((long)Char.myCharz().luong);
								}
								else if (itemMap2.template.type == 34)
								{
									num = (int)msg.reader().readShort();
									Char.myCharz().luongKhoa += num;
									Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)Char.myCharz().luongKhoa);
								}
							}
							catch (Exception)
							{
							}
							if (text5.Equals(string.Empty))
							{
								if (itemMap2.template.type == 9)
								{
									GameScr.startFlyText(((num >= 0) ? "+" : string.Empty) + num.ToString(), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -2, mFont.YELLOW);
									SoundMn.gI().getItem();
								}
								else if (itemMap2.template.type == 10)
								{
									GameScr.startFlyText(((num >= 0) ? "+" : string.Empty) + num.ToString(), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -2, mFont.GREEN);
									SoundMn.gI().getItem();
								}
								else if (itemMap2.template.type == 34)
								{
									GameScr.startFlyText(((num >= 0) ? "+" : string.Empty) + num.ToString(), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -2, mFont.RED);
									SoundMn.gI().getItem();
								}
								else
								{
									GameScr.info1.addInfo(mResources.you_receive + " " + ((num <= 0) ? string.Empty : (num.ToString() + " ")) + itemMap2.template.name, 0);
									SoundMn.gI().getItem();
								}
								if (num > 0 && Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 4683)
								{
									ServerEffect.addServerEffect(55, Char.myCharz().petFollow.cmx, Char.myCharz().petFollow.cmy, 1);
									ServerEffect.addServerEffect(55, Char.myCharz().cx, Char.myCharz().cy, 1);
									break;
								}
								break;
							}
							else
							{
								if (text5.Length == 1)
								{
									Cout.LogError3("strInf.Length =1:  " + text5);
									break;
								}
								GameScr.info1.addInfo(text5, 0);
								break;
							}
						}
						else
						{
							num111++;
						}
					}
					goto IL_8CF1;
				}
				case -19:
				{
					short itemMapID3 = msg.reader().readShort();
					@char = GameScr.findCharInMap(msg.reader().readInt());
					int num112 = 0;
					while (num112 < GameScr.vItemMap.size())
					{
						ItemMap itemMap3 = (ItemMap)GameScr.vItemMap.elementAt(num112);
						if (itemMap3.itemMapID == (int)itemMapID3)
						{
							if (@char == null)
							{
								return;
							}
							itemMap3.setPoint(@char.cx, @char.cy - 10);
							if (itemMap3.x < @char.cx)
							{
								@char.cdir = -1;
								break;
							}
							if (itemMap3.x > @char.cx)
							{
								@char.cdir = 1;
								break;
							}
							break;
						}
						else
						{
							num112++;
						}
					}
					goto IL_8CF1;
				}
				case -18:
				{
					GameCanvas.debug("SA63", 2);
					int num113 = (int)msg.reader().readByte();
					GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), Char.myCharz().arrItemBag[num113].template.id, Char.myCharz().cx, Char.myCharz().cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
					Char.myCharz().arrItemBag[num113] = null;
					goto IL_8CF1;
				}
				case -14:
					@char = GameScr.findCharInMap(msg.reader().readInt());
					if (@char == null)
					{
						return;
					}
					GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), msg.reader().readShort(), @char.cx, @char.cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
					goto IL_8CF1;
				case -4:
				{
					GameCanvas.debug("SA76", 2);
					@char = GameScr.findCharInMap(msg.reader().readInt());
					if (@char == null)
					{
						return;
					}
					GameCanvas.debug("SA76v1", 2);
					if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
					{
						@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 0);
					}
					else
					{
						@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 1);
					}
					GameCanvas.debug("SA76v2", 2);
					@char.attMobs = new Mob[(int)msg.reader().readByte()];
					for (int num114 = 0; num114 < @char.attMobs.Length; num114++)
					{
						Mob mob11 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
						@char.attMobs[num114] = mob11;
						if (num114 == 0)
						{
							if (@char.cx <= mob11.x)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
					}
					GameCanvas.debug("SA76v3", 2);
					@char.charFocus = null;
					@char.mobFocus = @char.attMobs[0];
					Char[] array12 = new Char[10];
					num = 0;
					try
					{
						for (num = 0; num < array12.Length; num++)
						{
							int num115 = msg.reader().readInt();
							Char char8 = array12[num] = ((num115 != Char.myCharz().charID) ? GameScr.findCharInMap(num115) : Char.myCharz());
							if (num == 0)
							{
								if (@char.cx <= char8.cx)
								{
									@char.cdir = 1;
								}
								else
								{
									@char.cdir = -1;
								}
							}
						}
					}
					catch (Exception ex5)
					{
						Cout.println("Loi PLAYER_ATTACK_N_P " + ex5.ToString());
					}
					GameCanvas.debug("SA76v4", 2);
					if (num > 0)
					{
						@char.attChars = new Char[num];
						for (num = 0; num < @char.attChars.Length; num++)
						{
							@char.attChars[num] = array12[num];
						}
						@char.charFocus = @char.attChars[0];
						@char.mobFocus = null;
					}
					GameCanvas.debug("SA76v5", 2);
					goto IL_8CF1;
				}
				case 0:
					this.readLogin(msg);
					goto IL_8CF1;
				case 1:
				{
					bool flag13 = msg.reader().readBool();
					Res.outz("isRes= " + flag13.ToString());
					if (!flag13)
					{
						GameCanvas.startOKDlg(msg.reader().readUTF());
						goto IL_8CF1;
					}
					GameCanvas.loginScr.isLogin2 = false;
					Rms.saveRMSString("userAo2" + ServerListScreen.ipSelect.ToString(), string.Empty);
					GameCanvas.endDlg();
					GameCanvas.loginScr.doLogin();
					goto IL_8CF1;
				}
				case 2:
					Char.isLoadingMap = false;
					LoginScr.isLoggingIn = false;
					if (!GameScr.isLoadAllData)
					{
						GameScr.gI().initSelectChar();
					}
					BgItem.clearHashTable();
					GameCanvas.endDlg();
					CreateCharScr.isCreateChar = true;
					CreateCharScr.gI().switchToMe();
					goto IL_8CF1;
				case 3:
				{
					sbyte b109 = msg.reader().readByte();
					if (b109 == 0)
					{
						Char.myCharz().havePet2 = false;
					}
					if (b109 == 1)
					{
						Char.myCharz().havePet2 = true;
					}
					if (b109 != 2)
					{
						goto IL_8CF1;
					}
					InfoDlg.hide();
					Char.MyPet2z().head = (int)msg.reader().readShort();
					Char.MyPet2z().setDefaultPart();
					int arrBodySz = (int)msg.reader().readUnsignedByte();
					Char.MyPet2z().arrItemBody = new Item[arrBodySz];
					for (int i10 = 0; i10 < arrBodySz; i10++)
					{
						short tempId = msg.reader().readShort();
						if (tempId != -1)
						{
							Char.MyPet2z().arrItemBody[i10] = new Item
							{
								template = ItemTemplates.get(tempId)
							};
							int num116 = (int)Char.MyPet2z().arrItemBody[i10].template.type;
							Char.MyPet2z().arrItemBody[i10].quantity = msg.reader().readInt();
							Char.MyPet2z().arrItemBody[i10].info = msg.reader().readUTF();
							Char.MyPet2z().arrItemBody[i10].content = msg.reader().readUTF();
							int num117 = (int)msg.reader().readUnsignedByte();
							if (num117 != 0)
							{
								Char.MyPet2z().arrItemBody[i10].itemOption = new ItemOption[num117];
								for (int num118 = 0; num118 < Char.MyPet2z().arrItemBody[i10].itemOption.Length; num118++)
								{
									int num119 = (int)msg.reader().readUnsignedByte();
									int param9 = (int)msg.reader().readUnsignedShort();
									if (num119 != -1)
									{
										Char.MyPet2z().arrItemBody[i10].itemOption[num118] = new ItemOption(num119, param9);
									}
								}
							}
							if (num116 != 0)
							{
								if (num116 == 1)
								{
									Char.MyPet2z().leg = (int)Char.MyPet2z().arrItemBody[i10].template.part;
								}
							}
							else
							{
								Char.MyPet2z().body = (int)Char.MyPet2z().arrItemBody[i10].template.part;
							}
						}
					}
					Char.MyPet2z().cHP = msg.readLong();
					Char.MyPet2z().cHPFull = msg.readLong();
					Char.MyPet2z().cMP = msg.readLong();
					Char.MyPet2z().cMPFull = msg.readLong();
					Char.MyPet2z().cDamFull = msg.readLong();
					Char.MyPet2z().cName = msg.reader().readUTF();
					Char.MyPet2z().currStrLevel = msg.reader().readUTF();
					Char.MyPet2z().cPower = msg.reader().readLong();
					Char.MyPet2z().cTiemNang = msg.reader().readLong();
					Char.MyPet2z().petStatus = msg.reader().readByte();
					Char.MyPet2z().cStamina = (int)msg.reader().readShort();
					Char.MyPet2z().cMaxStamina = msg.reader().readShort();
					Char.MyPet2z().cCriticalFull = (int)msg.reader().readByte();
					Char.MyPet2z().cDefull = (long)msg.reader().readInt();
					Char.MyPet2z().arrPetSkill = new Skill[(int)msg.reader().readByte()];
					for (int num120 = 0; num120 < Char.MyPet2z().arrPetSkill.Length; num120++)
					{
						short num121 = msg.reader().readShort();
						if (num121 != -1)
						{
							Char.MyPet2z().arrPetSkill[num120] = Skills.get(num121);
						}
						else
						{
							Char.MyPet2z().arrPetSkill[num120] = new Skill();
							Char.MyPet2z().arrPetSkill[num120].template = null;
							Char.MyPet2z().arrPetSkill[num120].moreInfo = msg.reader().readUTF();
						}
					}
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[][]
						{
							new string[]
							{
								string.Empty
							}
						};
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
						GameCanvas.panel.setTypePet2Main();
						GameCanvas.panel.show();
						goto IL_8CF1;
					}
					GameCanvas.panel.tabName[21] = mResources.petMainTab;
					GameCanvas.panel.setTypePet2Main();
					GameCanvas.panel.show();
					goto IL_8CF1;
				}
				case 6:
					Char.myCharz().xu = msg.reader().readLong();
					Char.myCharz().luong = msg.reader().readInt();
					Char.myCharz().luongKhoa = msg.reader().readInt();
					Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
					Char.myCharz().luongStr = mSystem.numberTostring((long)Char.myCharz().luong);
					Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)Char.myCharz().luongKhoa);
					GameCanvas.endDlg();
					goto IL_8CF1;
				case 7:
				{
					sbyte type5 = msg.reader().readByte();
					short id4 = msg.reader().readShort();
					string info4 = msg.reader().readUTF();
					GameCanvas.panel.saleRequest(type5, info4, id4);
					goto IL_8CF1;
				}
				case 11:
				{
					GameCanvas.debug("SA9", 2);
					int num122 = (int)msg.reader().readByte();
					sbyte b91 = msg.reader().readByte();
					if (b91 != 0)
					{
						Mob.arrMobTemplate[num122].data.readDataNewBoss(NinjaUtil.readByteArray(msg), b91);
					}
					else
					{
						Mob.arrMobTemplate[num122].data.readData(NinjaUtil.readByteArray(msg));
					}
					for (int i11 = 0; i11 < GameScr.vMob.size(); i11++)
					{
						Mob mob8 = (Mob)GameScr.vMob.elementAt(i11);
						if (mob8.templateId == num122)
						{
							mob8.w = Mob.arrMobTemplate[num122].data.width;
							mob8.h = Mob.arrMobTemplate[num122].data.height;
						}
					}
					sbyte[] array13 = NinjaUtil.readByteArray(msg);
					Image img2 = Image.createImage(array13, 0, array13.Length);
					Mob.arrMobTemplate[num122].data.img = img2;
					int num123 = (int)msg.reader().readByte();
					Mob.arrMobTemplate[num122].data.typeData = num123;
					if (num123 == 1 || num123 == 2)
					{
						this.readFrameBoss(msg, num122);
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case 20:
					this.phuban_Info(msg);
					goto IL_8CF1;
				case 24:
					this.read_opt(msg);
					goto IL_8CF1;
				case 27:
				{
					myVector = new MyVector();
					msg.reader().readUTF();
					int num124 = (int)msg.reader().readByte();
					for (int num125 = 0; num125 < num124; num125++)
					{
						string caption4 = msg.reader().readUTF();
						short num126 = msg.reader().readShort();
						myVector.addElement(new Command(caption4, GameCanvas.instance, 88819, num126));
					}
					GameCanvas.menu.startWithoutCloseButton(myVector, 3);
					goto IL_8CF1;
				}
				case 29:
					GameCanvas.debug("SA58", 2);
					GameScr.gI().openUIZone(msg);
					goto IL_8CF1;
				case 32:
				{
					int npcId = (int)msg.reader().readShort();
					for (int i12 = 0; i12 < GameScr.vNpc.size(); i12++)
					{
						Npc npc3 = (Npc)GameScr.vNpc.elementAt(i12);
						if (npc3.template.npcTemplateId == npcId && npc3.Equals(Char.myCharz().npcFocus))
						{
							string chat = msg.reader().readUTF();
							string[] menu = new string[(int)msg.reader().readByte()];
							for (int num127 = 0; num127 < menu.Length; num127++)
							{
								menu[num127] = msg.reader().readUTF();
							}
							GameScr.gI().createMenu(menu, npc3);
							ChatPopup.addChatPopup(chat, 100000, npc3);
							if (npcId == 21 && chat.Contains("tối đa"))
							{
								ModFunc.GI().maxPhale = ModFunc.GI().currPhale;
							}
							return;
						}
					}
					Npc npc4 = new Npc(npcId, 0, -100, 100, npcId, GameScr.info1.charId[Char.myCharz().cgender][2]);
					string chat2 = msg.reader().readUTF();
					string[] menu2 = new string[(int)msg.reader().readByte()];
					for (int j4 = 0; j4 < menu2.Length; j4++)
					{
						menu2[j4] = msg.reader().readUTF();
					}
					try
					{
						short avatar3 = msg.reader().readShort();
						npc4.avatar = (int)avatar3;
					}
					catch (Exception)
					{
					}
					GameScr.gI().createMenu(menu2, npc4);
					ChatPopup.addChatPopup(chat2, 100000, npc4);
					goto IL_8CF1;
				}
				case 33:
					InfoDlg.hide();
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					myVector = new MyVector();
					try
					{
						for (;;)
						{
							string caption5 = msg.reader().readUTF();
							myVector.addElement(new Command(caption5, GameCanvas.instance, 88822, null));
						}
					}
					catch (Exception ex6)
					{
						Cout.println("Loi OPEN_UI_MENU " + ex6.ToString());
					}
					if (Char.myCharz().npcFocus == null)
					{
						return;
					}
					for (int num128 = 0; num128 < Char.myCharz().npcFocus.template.menu.Length; num128++)
					{
						string[] array14 = Char.myCharz().npcFocus.template.menu[num128];
						myVector.addElement(new Command(array14[0], GameCanvas.instance, 88820, array14));
					}
					GameCanvas.menu.startAt(myVector, 3);
					goto IL_8CF1;
				case 38:
				{
					InfoDlg.hide();
					int num129 = (int)msg.reader().readShort();
					string str = msg.reader().readUTF();
					str = Res.changeString(str);
					for (int num130 = 0; num130 < GameScr.vNpc.size(); num130++)
					{
						Npc npc5 = (Npc)GameScr.vNpc.elementAt(num130);
						if (npc5.template.npcTemplateId == num129)
						{
							ChatPopup.addChatPopupMultiLine(str, 100000, npc5);
							GameCanvas.panel.hideNow();
							return;
						}
					}
					Npc npc6 = new Npc(num129, 0, 0, 0, num129, GameScr.info1.charId[Char.myCharz().cgender][2]);
					if (npc6.template.npcTemplateId == 5)
					{
						npc6.charID = 5;
					}
					try
					{
						npc6.avatar = (int)msg.reader().readShort();
					}
					catch (Exception)
					{
					}
					ChatPopup.addChatPopupMultiLine(str, 100000, npc6);
					GameCanvas.panel.hideNow();
					goto IL_8CF1;
				}
				case 39:
					GameCanvas.debug("SA49", 2);
					GameScr.gI().typeTradeOrder = 2;
					if (GameScr.gI().typeTrade >= 2 && GameScr.gI().typeTradeOrder >= 2)
					{
						InfoDlg.showWait();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				case 40:
				{
					GameCanvas.debug("SA52", 2);
					GameCanvas.taskTick = 150;
					short taskId = msg.reader().readShort();
					sbyte index4 = msg.reader().readByte();
					string str2 = msg.reader().readUTF();
					str2 = Res.changeString(str2);
					string str3 = msg.reader().readUTF();
					str3 = Res.changeString(str3);
					string[] array15 = new string[(int)msg.reader().readByte()];
					string[] array16 = new string[array15.Length];
					GameScr.tasks = new int[array15.Length];
					GameScr.mapTasks = new int[array15.Length];
					short[] array17 = new short[array15.Length];
					short count = -1;
					for (int num131 = 0; num131 < array15.Length; num131++)
					{
						string str4 = msg.reader().readUTF();
						str4 = Res.changeString(str4);
						GameScr.tasks[num131] = (int)msg.reader().readByte();
						GameScr.mapTasks[num131] = (int)msg.reader().readShort();
						string str5 = msg.reader().readUTF();
						str5 = Res.changeString(str5);
						array17[num131] = -1;
						if (!str4.Equals(string.Empty))
						{
							array15[num131] = str4;
							array16[num131] = str5;
						}
					}
					try
					{
						count = msg.reader().readShort();
						for (int num132 = 0; num132 < array15.Length; num132++)
						{
							array17[num132] = msg.reader().readShort();
						}
					}
					catch (Exception ex7)
					{
						Cout.println("Loi TASK_GET " + ex7.ToString());
					}
					Char.myCharz().taskMaint = new Task(taskId, index4, str2, str3, array15, array17, count, array16);
					if (Char.myCharz().npcFocus != null)
					{
						Npc.clearEffTask();
					}
					Char.taskAction(false);
					goto IL_8CF1;
				}
				case 41:
					GameCanvas.debug("SA53", 2);
					GameCanvas.taskTick = 100;
					Res.outz("TASK NEXT");
					Char.myCharz().taskMaint.index++;
					Char.myCharz().taskMaint.count = 0;
					Npc.clearEffTask();
					Char.taskAction(true);
					goto IL_8CF1;
				case 43:
					GameCanvas.taskTick = 50;
					GameCanvas.debug("SA55", 2);
					Char.myCharz().taskMaint.count = msg.reader().readShort();
					if (Char.myCharz().npcFocus != null)
					{
						Npc.clearEffTask();
					}
					try
					{
						short num133 = msg.reader().readShort();
						short num134 = msg.reader().readShort();
						Char.myCharz().x_hint = num133;
						Char.myCharz().y_hint = num134;
						Res.outz("CMD   TASK_UPDATE:43_mapID =    x|y " + num133.ToString() + "|" + num134.ToString());
						for (int num135 = 0; num135 < TileMap.vGo.size(); num135++)
						{
							string str7 = "===> ";
							object obj = TileMap.vGo.elementAt(num135);
							Res.outz(str7 + ((obj != null) ? obj.ToString() : null));
						}
						goto IL_8CF1;
					}
					catch (Exception)
					{
						goto IL_8CF1;
					}
					goto IL_748D;
				case 46:
					GameCanvas.debug("SA5", 2);
					Cout.LogWarning("Controler RESET_POINT  " + Char.ischangingMap.ToString());
					Char.isLockKey = false;
					Char.myCharz().setResetPoint((int)msg.reader().readShort(), (int)msg.reader().readShort());
					goto IL_8CF1;
				case 47:
					GameScr.gI().resetButton();
					goto IL_8CF1;
				case 50:
				{
					sbyte b92 = msg.reader().readByte();
					Panel.vGameInfo.removeAllElements();
					for (int num136 = 0; num136 < (int)b92; num136++)
					{
						GameInfo gameInfo = new GameInfo();
						gameInfo.id = msg.reader().readShort();
						gameInfo.main = msg.reader().readUTF();
						gameInfo.content = msg.reader().readUTF();
						Panel.vGameInfo.addElement(gameInfo);
						bool hasRead = Rms.loadRMSInt(gameInfo.id.ToString() + string.Empty) != -1;
						gameInfo.hasRead = hasRead;
					}
					goto IL_8CF1;
				}
				case 54:
				{
					@char = GameScr.findCharInMap(msg.reader().readInt());
					if (@char == null)
					{
						return;
					}
					int num137 = (int)msg.reader().readUnsignedByte();
					if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
					{
						@char.setSkillPaint(GameScr.sks[num137], 0);
					}
					else
					{
						@char.setSkillPaint(GameScr.sks[num137], 1);
					}
					Mob[] array18 = new Mob[10];
					num = 0;
					try
					{
						for (num = 0; num < array18.Length; num++)
						{
							Mob mob12 = array18[num] = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
							if (num == 0)
							{
								if (@char.cx <= mob12.x)
								{
									@char.cdir = 1;
								}
								else
								{
									@char.cdir = -1;
								}
							}
						}
					}
					catch (Exception)
					{
					}
					if (num > 0)
					{
						@char.attMobs = new Mob[num];
						for (num = 0; num < @char.attMobs.Length; num++)
						{
							@char.attMobs[num] = array18[num];
						}
						@char.charFocus = null;
						@char.mobFocus = @char.attMobs[0];
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case 56:
				{
					@char = null;
					int charID = msg.reader().readInt();
					if (charID == Char.myCharz().charID)
					{
						bool flag14 = false;
						@char = Char.myCharz();
						@char.cHP = msg.readLong();
						long dameHit2 = msg.readLong();
						if (dameHit2 != 0L)
						{
							@char.doInjure();
						}
						try
						{
							flag14 = msg.reader().readBoolean();
							sbyte effId = msg.reader().readByte();
							if (effId != -1)
							{
								EffecMn.addEff(new Effect((int)effId, @char.cx, @char.cy, 3, 1, -1));
							}
						}
						catch (Exception)
						{
						}
						if (Char.myCharz().cTypePk == 4)
						{
							goto IL_8CF1;
						}
						if (dameHit2 == 0L)
						{
							GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS_ME);
							goto IL_8CF1;
						}
						GameScr.startFlyText("-" + dameHit2.ToString(), @char.cx, @char.cy - @char.ch, 0, -3, flag14 ? mFont.FATAL : mFont.RED);
						goto IL_8CF1;
					}
					else
					{
						@char = GameScr.findCharInMap(charID);
						if (@char == null)
						{
							return;
						}
						@char.cHP = msg.readLong();
						bool flag15 = false;
						long dameHit3 = msg.readLong();
						if (dameHit3 != 0L)
						{
							@char.doInjure();
						}
						int num138 = 0;
						try
						{
							flag15 = msg.reader().readBoolean();
							sbyte effId2 = msg.reader().readByte();
							if (effId2 != -1)
							{
								EffecMn.addEff(new Effect((int)effId2, @char.cx, @char.cy, 3, 1, -1));
							}
						}
						catch (Exception)
						{
						}
						dameHit3 += (long)num138;
						if (@char.cTypePk == 4)
						{
							goto IL_8CF1;
						}
						if (dameHit3 == 0L)
						{
							GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS);
							goto IL_8CF1;
						}
						GameScr.startFlyText("-" + dameHit3.ToString(), @char.cx, @char.cy - @char.ch, 0, -3, flag15 ? mFont.FATAL : mFont.ORANGE);
						goto IL_8CF1;
					}
					break;
				}
				case 57:
				{
					GameCanvas.debug("SZ6", 2);
					MyVector myVector2 = new MyVector();
					myVector2.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88817, null));
					GameCanvas.menu.startAt(myVector2, 3);
					goto IL_8CF1;
				}
				case 58:
				{
					int charId = msg.reader().readInt();
					Char char15 = (charId != Char.myCharz().charID) ? GameScr.findCharInMap(charId) : Char.myCharz();
					char15.moveFast = new short[3];
					char15.moveFast[0] = 0;
					short x = msg.reader().readShort();
					short y = msg.reader().readShort();
					char15.moveFast[1] = x;
					char15.moveFast[2] = y;
					try
					{
						charId = msg.reader().readInt();
						Char char16 = (charId != Char.myCharz().charID) ? GameScr.findCharInMap(charId) : Char.myCharz();
						char16.cx = (int)x;
						char16.cy = (int)y;
						goto IL_8CF1;
					}
					catch (Exception ex8)
					{
						Cout.println("Loi MOVE_FAST " + ex8.ToString());
						goto IL_8CF1;
					}
					goto IL_6DB9;
				}
				case 62:
					@char = GameScr.findCharInMap(msg.reader().readInt());
					if (@char != null)
					{
						@char.killCharId = Char.myCharz().charID;
						Char.myCharz().npcFocus = null;
						Char.myCharz().mobFocus = null;
						Char.myCharz().itemFocus = null;
						Char.myCharz().charFocus = @char;
						Char.isManualFocus = true;
						GameScr.info1.addInfo(@char.cName + mResources.CUU_SAT, 0);
						goto IL_8CF1;
					}
					goto IL_8CF1;
				case 63:
					Char.myCharz().killCharId = msg.reader().readInt();
					Char.myCharz().npcFocus = null;
					Char.myCharz().mobFocus = null;
					Char.myCharz().itemFocus = null;
					Char.myCharz().charFocus = GameScr.findCharInMap(Char.myCharz().killCharId);
					Char.isManualFocus = true;
					goto IL_8CF1;
				case 64:
					GameCanvas.debug("SZ5", 2);
					@char = Char.myCharz();
					try
					{
						@char = GameScr.findCharInMap(msg.reader().readInt());
					}
					catch (Exception ex9)
					{
						Cout.println("Loi CLEAR_CUU_SAT " + ex9.ToString());
					}
					@char.killCharId = -9999;
					goto IL_8CF1;
				case 65:
				{
					sbyte b93 = msg.reader().readSByte();
					string text6 = msg.reader().readUTF();
					short num139 = msg.reader().readShort();
					if (!ItemTime.isExistMessage((int)b93))
					{
						ItemTime itemTime = new ItemTime();
						itemTime.initTimeText(b93, text6, (int)num139);
						GameScr.textTime.addElement(itemTime);
						goto IL_8CF1;
					}
					if (num139 != 0)
					{
						ItemTime.getMessageById((int)b93).initTimeText(b93, text6, (int)num139);
						goto IL_8CF1;
					}
					GameScr.textTime.removeElement(ItemTime.getMessageById((int)b93));
					goto IL_8CF1;
				}
				case 66:
					this.readGetImgByName(msg);
					goto IL_8CF1;
				case 68:
				{
					short itemMapID4 = msg.reader().readShort();
					short itemTemplateID = msg.reader().readShort();
					int x2 = (int)msg.reader().readShort();
					int y2 = (int)msg.reader().readShort();
					int num160 = msg.reader().readInt();
					short r = 0;
					if (num160 == -2)
					{
						r = msg.reader().readShort();
					}
					ItemMap o2 = new ItemMap(num160, itemMapID4, itemTemplateID, x2, y2, r);
					GameScr.vItemMap.addElement(o2);
					goto IL_8CF1;
				}
				case 69:
					SoundMn.IsDelAcc = (msg.reader().readByte() != 0);
					goto IL_8CF1;
				case 81:
					((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isDisable = msg.reader().readBool();
					goto IL_8CF1;
				case 82:
					((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isDontMove = msg.reader().readBool();
					goto IL_8CF1;
				case 83:
				{
					int num140 = msg.reader().readInt();
					@char = ((num140 != Char.myCharz().charID) ? GameScr.findCharInMap(num140) : Char.myCharz());
					if (@char == null)
					{
						return;
					}
					Mob mobToAttack = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
					if (@char.mobMe != null)
					{
						@char.mobMe.attackOtherMob(mobToAttack);
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case 84:
				{
					int num141 = msg.reader().readInt();
					if (num141 == Char.myCharz().charID)
					{
						@char = Char.myCharz();
					}
					else
					{
						@char = GameScr.findCharInMap(num141);
						if (@char == null)
						{
							return;
						}
					}
					@char.cHP = @char.cHPFull;
					@char.cMP = @char.cMPFull;
					@char.cx = (int)msg.reader().readShort();
					@char.cy = (int)msg.reader().readShort();
					@char.liveFromDead();
					goto IL_8CF1;
				}
				case 85:
					((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isFire = msg.reader().readBool();
					goto IL_8CF1;
				case 86:
				{
					Mob mob13 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
					mob13.isIce = msg.reader().readBool();
					if (!mob13.isIce)
					{
						ServerEffect.addServerEffect(77, mob13.x, mob13.y - 9, 1);
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case 87:
					((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isWind = msg.reader().readBool();
					goto IL_8CF1;
				case 88:
					goto IL_6DB9;
				case 90:
					goto IL_748D;
				case 92:
				{
					if (GameCanvas.currentScreen == GameScr.instance)
					{
						GameCanvas.endDlg();
					}
					string text7 = msg.reader().readUTF();
					string str6 = msg.reader().readUTF();
					str6 = Res.changeString(str6);
					string empty = string.Empty;
					Char char9 = null;
					sbyte b94 = 0;
					if (!text7.Equals(string.Empty))
					{
						char9 = new Char();
						char9.charID = msg.reader().readInt();
						char9.head = (int)msg.reader().readShort();
						char9.headICON = (int)msg.reader().readShort();
						char9.body = (int)msg.reader().readShort();
						char9.bag = (int)msg.reader().readShort();
						char9.leg = (int)msg.reader().readShort();
						b94 = msg.reader().readByte();
						char9.cName = text7;
						try
						{
							char9.isTichXanh = (msg.reader().readByte() == 1);
						}
						catch (Exception)
						{
							char9.isTichXanh = false;
						}
					}
					empty += str6;
					InfoDlg.hide();
					if (text7.Equals(string.Empty))
					{
						GameScr.info1.addInfo(empty, 0);
						goto IL_8CF1;
					}
					GameScr.info2.addInfoWithChar(empty, char9, b94 == 0);
					if (GameCanvas.panel.isShow && GameCanvas.panel.type == 8)
					{
						GameCanvas.panel.initLogMessage();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				case 94:
					GameScr.info1.addInfo(msg.reader().readUTF(), 0);
					goto IL_8CF1;
				case 112:
				{
					sbyte type6 = msg.reader().readByte();
					if (type6 == 0)
					{
						Panel.spearcialImage = msg.reader().readShort();
						Panel.specialInfo = msg.reader().readUTF();
						ModFunc.GI().CheckAutoIntrinsic(Panel.specialInfo);
						goto IL_8CF1;
					}
					if (type6 == 1)
					{
						sbyte tabSize = msg.reader().readByte();
						Char.myCharz().infoSpeacialSkill = new string[(int)tabSize][];
						Char.myCharz().imgSpeacialSkill = new short[(int)tabSize][];
						GameCanvas.panel.speacialTabName = new string[(int)tabSize][];
						for (int j5 = 0; j5 < (int)tabSize; j5++)
						{
							GameCanvas.panel.speacialTabName[j5] = new string[2];
							string[] array19 = Res.split(msg.reader().readUTF(), "\n", 0);
							if (array19.Length == 2)
							{
								GameCanvas.panel.speacialTabName[j5] = array19;
							}
							if (array19.Length == 1)
							{
								GameCanvas.panel.speacialTabName[j5][0] = array19[0];
								GameCanvas.panel.speacialTabName[j5][1] = string.Empty;
							}
							int size6 = (int)msg.reader().readByte();
							Char.myCharz().infoSpeacialSkill[j5] = new string[size6];
							Char.myCharz().imgSpeacialSkill[j5] = new short[size6];
							for (int i13 = 0; i13 < size6; i13++)
							{
								Char.myCharz().imgSpeacialSkill[j5][i13] = msg.reader().readShort();
								Char.myCharz().infoSpeacialSkill[j5][i13] = msg.reader().readUTF();
							}
						}
						GameCanvas.panel.tabName[25] = GameCanvas.panel.speacialTabName;
						GameCanvas.panel.setTypeSpeacialSkill();
						GameCanvas.panel.show();
						goto IL_8CF1;
					}
					goto IL_8CF1;
				}
				default:
					goto IL_8CF1;
				}
				int id5 = (int)msg.reader().readShort();
				sbyte[] data9 = NinjaUtil.readByteArray(msg);
				EffectData effDataById = Effect.getEffDataById(id5);
				sbyte b95 = msg.reader().readSByte();
				if (b95 == 0)
				{
					effDataById.readData(data9);
				}
				else
				{
					effDataById.readDataNewBoss(data9, b95);
				}
				sbyte[] array20 = NinjaUtil.readByteArray(msg);
				effDataById.img = Image.createImage(array20, 0, array20.Length);
				goto IL_8CF1;
				IL_6DB9:
				string info5 = msg.reader().readUTF();
				short num142 = msg.reader().readShort();
				GameCanvas.inputDlg.show(info5, new Command(mResources.ACCEPT, GameCanvas.instance, 88818, num142), TField.INPUT_TYPE_ANY);
				goto IL_8CF1;
				IL_748D:
				GameCanvas.debug("SA577", 2);
				this.requestItemPlayer(msg);
				IL_8CF1:
				sbyte command = msg.command;
				if (command <= 19)
				{
					if (command <= -73)
					{
						if (command != -75)
						{
							if (command != -73)
							{
								goto IL_A54A;
							}
							sbyte npcId2 = msg.reader().readByte();
							int i14 = 0;
							while (i14 < GameScr.vNpc.size())
							{
								Npc npc7 = (Npc)GameScr.vNpc.elementAt(i14);
								if (npc7.template.npcTemplateId == (int)npcId2)
								{
									if (msg.reader().readByte() == 0)
									{
										npc7.isHide = true;
										break;
									}
									npc7.isHide = false;
									break;
								}
								else
								{
									i14++;
								}
							}
							goto IL_A54A;
						}
						else
						{
							Mob mob14 = null;
							try
							{
								mob14 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
							}
							catch (Exception)
							{
							}
							if (mob14 == null)
							{
								goto IL_A54A;
							}
							mob14.levelBoss = msg.reader().readByte();
							if (mob14.levelBoss > 0)
							{
								mob14.typeSuperEff = Res.random(0, 3);
								goto IL_A54A;
							}
							goto IL_A54A;
						}
					}
					else
					{
						switch (command)
						{
						case -17:
							Char.myCharz().meDead = true;
							Char.myCharz().cPk = msg.reader().readByte();
							Char.myCharz().startDie(msg.reader().readShort(), msg.reader().readShort());
							try
							{
								Char.myCharz().cPower = msg.reader().readLong();
								Char.myCharz().applyCharLevelPercent();
							}
							catch (Exception)
							{
								Cout.println("Loi tai ME_DIE " + msg.command.ToString());
							}
							Char.myCharz().countKill = 0;
							goto IL_A54A;
						case -16:
							if (Char.myCharz().wdx != 0 || Char.myCharz().wdy != 0)
							{
								Char.myCharz().cx = (int)Char.myCharz().wdx;
								Char.myCharz().cy = (int)Char.myCharz().wdy;
								Char.myCharz().wdx = (Char.myCharz().wdy = 0);
							}
							Char.myCharz().liveFromDead();
							Char.myCharz().isLockMove = false;
							Char.myCharz().meDead = false;
							goto IL_A54A;
						case -15:
						case -14:
						case -4:
							goto IL_A54A;
						case -13:
						{
							int num143 = (int)msg.reader().readUnsignedByte();
							if (num143 > GameScr.vMob.size() - 1 || num143 < 0)
							{
								return;
							}
							Mob mob15 = (Mob)GameScr.vMob.elementAt(num143);
							if (mob15.status != 0 && mob15.status != 1)
							{
								return;
							}
							mob15.sys = (int)msg.reader().readByte();
							mob15.levelBoss = msg.reader().readByte();
							if (mob15.levelBoss != 0)
							{
								mob15.typeSuperEff = Res.random(0, 3);
							}
							mob15.x = mob15.xFirst;
							mob15.y = mob15.yFirst;
							mob15.status = 5;
							mob15.injureThenDie = false;
							mob15.hp = msg.readLong();
							mob15.maxHp = mob15.hp;
							mob15.updateHp_bar();
							ServerEffect.addServerEffect(60, mob15.x, mob15.y, 1);
							goto IL_A54A;
						}
						case -12:
						{
							Mob mob16 = null;
							try
							{
								mob16 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
							}
							catch (Exception)
							{
							}
							if (mob16 == null || mob16.status == 0 || mob16.status == 0)
							{
								goto IL_A54A;
							}
							mob16.startDie();
							try
							{
								long dameHit4 = msg.readLong();
								if (msg.reader().readBool())
								{
									GameScr.startFlyText("-" + dameHit4.ToString(), mob16.x, mob16.y - mob16.h, 0, -2, mFont.FATAL);
								}
								else
								{
									GameScr.startFlyText("-" + dameHit4.ToString(), mob16.x, mob16.y - mob16.h, 0, -2, mFont.ORANGE);
								}
								sbyte b96 = msg.reader().readByte();
								for (int num144 = 0; num144 < (int)b96; num144++)
								{
									ItemMap itemMap4 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob16.x, mob16.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
									itemMap4.playerId = msg.reader().readInt();
									GameScr.vItemMap.addElement(itemMap4);
									if (Res.abs(itemMap4.y - Char.myCharz().cy) < 24 && Res.abs(itemMap4.x - Char.myCharz().cx) < 24)
									{
										Char.myCharz().charFocus = null;
									}
								}
								goto IL_A54A;
							}
							catch (Exception)
							{
								goto IL_A54A;
							}
							break;
						}
						case -11:
						{
							Mob mob17 = null;
							try
							{
								int index5 = (int)msg.reader().readUnsignedByte();
								mob17 = (Mob)GameScr.vMob.elementAt(index5);
							}
							catch (Exception)
							{
							}
							if (mob17 == null)
							{
								goto IL_A54A;
							}
							Char.myCharz().isDie = false;
							Char.isLockKey = false;
							long dame2 = msg.readLong();
							int num145;
							try
							{
								num145 = msg.readInt3Byte();
							}
							catch (Exception)
							{
								num145 = 0;
							}
							if (mob17.isBusyAttackSomeOne)
							{
								Char.myCharz().doInjure(dame2, (long)num145, false, true);
								goto IL_A54A;
							}
							mob17.dame = dame2;
							mob17.dameMp = (long)num145;
							mob17.setAttack(Char.myCharz());
							goto IL_A54A;
						}
						case -10:
						{
							Mob mob18 = null;
							try
							{
								mob18 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
							}
							catch (Exception)
							{
							}
							if (mob18 == null)
							{
								goto IL_A54A;
							}
							@char = GameScr.findCharInMap(msg.reader().readInt());
							if (@char == null)
							{
								return;
							}
							long cHP = msg.readLong();
							mob18.dame = @char.cHP - cHP;
							@char.cHPNew = cHP;
							try
							{
								@char.cMP = (long)msg.readInt3Byte();
							}
							catch (Exception)
							{
							}
							if (mob18.isBusyAttackSomeOne)
							{
								@char.doInjure(mob18.dame, 0L, false, true);
								goto IL_A54A;
							}
							mob18.setAttack(@char);
							goto IL_A54A;
						}
						case -9:
						{
							Mob mob19 = null;
							try
							{
								mob19 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
							}
							catch (Exception)
							{
							}
							if (mob19 == null)
							{
								goto IL_A54A;
							}
							mob19.hp = msg.readLong();
							mob19.updateHp_bar();
							long dame3 = msg.readLong();
							if (dame3 == 1L)
							{
								return;
							}
							if (dame3 > 1L)
							{
								mob19.setInjure();
							}
							bool flag16 = false;
							try
							{
								flag16 = msg.reader().readBoolean();
							}
							catch (Exception)
							{
							}
							sbyte b97 = msg.reader().readByte();
							if (b97 != -1)
							{
								EffecMn.addEff(new Effect((int)b97, mob19.x, mob19.getY(), 3, 1, -1));
							}
							if (flag16)
							{
								GameScr.startFlyText("-" + dame3.ToString(), mob19.x, mob19.getY() - mob19.getH(), 0, -2, mFont.FATAL);
								goto IL_A54A;
							}
							if (dame3 == 0L)
							{
								mob19.x = mob19.xFirst;
								mob19.y = mob19.yFirst;
								GameScr.startFlyText(mResources.miss, mob19.x, mob19.getY() - mob19.getH(), 0, -2, mFont.MISS);
								goto IL_A54A;
							}
							if (dame3 > 1L)
							{
								GameScr.startFlyText("-" + dame3.ToString(), mob19.x, mob19.getY() - mob19.getH(), 0, -2, mFont.ORANGE);
								goto IL_A54A;
							}
							goto IL_A54A;
						}
						case -8:
							@char = GameScr.findCharInMap(msg.reader().readInt());
							if (@char == null)
							{
								return;
							}
							@char.cPk = msg.reader().readByte();
							@char.waitToDie(msg.reader().readShort(), msg.reader().readShort());
							goto IL_A54A;
						case -7:
						{
							int num146 = msg.reader().readInt();
							for (int num147 = 0; num147 < GameScr.vCharInMap.size(); num147++)
							{
								Char char10 = null;
								try
								{
									char10 = (Char)GameScr.vCharInMap.elementAt(num147);
								}
								catch (Exception)
								{
								}
								if (char10 == null)
								{
									break;
								}
								if (char10.charID == num146)
								{
									GameCanvas.debug("SA8x2y" + num147.ToString(), 2);
									char10.moveTo((int)msg.reader().readShort(), (int)msg.reader().readShort(), 0);
									char10.lastUpdateTime = mSystem.currentTimeMillis();
									break;
								}
							}
							GameCanvas.debug("SA80x3", 2);
							goto IL_A54A;
						}
						case -6:
						{
							GameCanvas.debug("SA81", 2);
							int num148 = msg.reader().readInt();
							for (int num149 = 0; num149 < GameScr.vCharInMap.size(); num149++)
							{
								Char char11 = (Char)GameScr.vCharInMap.elementAt(num149);
								if (char11 != null && char11.charID == num148)
								{
									if (!char11.isInvisiblez && !char11.isUsePlane)
									{
										ServerEffect.addServerEffect(60, char11.cx, char11.cy, 1);
									}
									if (!char11.isUsePlane)
									{
										GameScr.vCharInMap.removeElementAt(num149);
									}
									break;
								}
							}
							goto IL_A54A;
						}
						case -5:
						{
							int charID2 = msg.reader().readInt();
							int num150 = msg.reader().readInt();
							Char char12;
							if (num150 != -100)
							{
								char12 = new Char
								{
									charID = charID2,
									clanID = num150
								};
							}
							else
							{
								char12 = new Mabu
								{
									charID = charID2,
									clanID = num150
								};
							}
							if (char12.clanID == -2)
							{
								char12.isCopy = true;
							}
							if (this.readCharInfo(char12, msg))
							{
								sbyte b98 = msg.reader().readByte();
								if (char12.cy <= 10 && b98 != 0 && b98 != 2)
								{
									Teleport teleport = new Teleport(char12.cx, char12.cy, char12.head, char12.cdir, 1, false, (b98 != 1) ? ((int)b98) : char12.cgender);
									teleport.id = char12.charID;
									char12.isTeleport = true;
									Teleport.addTeleport(teleport);
								}
								if (b98 == 2)
								{
									char12.show();
								}
								for (int num151 = 0; num151 < GameScr.vMob.size(); num151++)
								{
									Mob mob20 = (Mob)GameScr.vMob.elementAt(num151);
									if (mob20 != null && mob20.isMobMe && mob20.mobId == char12.charID)
									{
										char12.mobMe = mob20;
										char12.mobMe.x = char12.cx;
										char12.mobMe.y = char12.cy - 40;
										break;
									}
								}
								if (GameScr.findCharInMap(char12.charID) == null)
								{
									GameScr.vCharInMap.addElement(char12);
								}
								char12.isMonkey = msg.reader().readByte();
								short num152 = msg.reader().readShort();
								if (num152 != -1)
								{
									char12.isHaveMount = true;
									if (num152 <= 351)
									{
										if (num152 - 346 <= 2)
										{
											char12.isMountVip = false;
											goto IL_960B;
										}
										if (num152 - 349 <= 2)
										{
											char12.isMountVip = true;
											goto IL_960B;
										}
									}
									else
									{
										if (num152 == 396)
										{
											char12.isEventMount = true;
											goto IL_960B;
										}
										if (num152 == 532)
										{
											char12.isSpeacialMount = true;
											goto IL_960B;
										}
									}
									if (num152 >= Char.ID_NEW_MOUNT)
									{
										char12.idMount = num152;
									}
								}
								else
								{
									char12.isHaveMount = false;
								}
							}
							IL_960B:
							sbyte b99 = msg.reader().readByte();
							char12.cFlag = b99;
							char12.isNhapThe = (msg.reader().readByte() == 1);
							try
							{
								char12.idAuraEff = msg.reader().readShort();
								char12.idEff_Set_Item = (short)msg.reader().readSByte();
								char12.idHat = msg.reader().readShort();
								if (char12.bag >= 201 && char12.bag < 255)
								{
									char12.addEffChar(new Effect(char12.bag, char12, 2, -1, 10, 1)
									{
										typeEff = 5
									});
								}
								else
								{
									for (int num153 = 0; num153 < 54; num153++)
									{
										char12.removeEffChar(0, 201 + num153);
									}
								}
							}
							catch (Exception ex10)
							{
								Res.outz("cmd: -5 err: " + ex10.StackTrace);
							}
							char12.isTichXanh = (msg.reader().readByte() == 1);
							GameScr.gI().getFlagImage(char12.charID, char12.cFlag);
							goto IL_A54A;
						}
						case -3:
						{
							sbyte b110 = msg.reader().readByte();
							long param10 = msg.readLong();
							if (b110 == 0)
							{
								Char.myCharz().cPower += param10;
							}
							if (b110 == 1)
							{
								Char.myCharz().cTiemNang += param10;
							}
							if (b110 == 2)
							{
								Char.myCharz().cPower += param10;
								Char.myCharz().cTiemNang += param10;
							}
							Char.myCharz().applyCharLevelPercent();
							if (Char.myCharz().cTypePk == 3)
							{
								goto IL_A54A;
							}
							GameScr.startFlyText(((param10 <= 0L) ? string.Empty : "+") + param10.ToString(), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -4, mFont.GREEN);
							if (param10 > 0L && Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 5002)
							{
								ServerEffect.addServerEffect(55, Char.myCharz().petFollow.cmx, Char.myCharz().petFollow.cmy, 1);
								ServerEffect.addServerEffect(55, Char.myCharz().cx, Char.myCharz().cy, 1);
								goto IL_A54A;
							}
							goto IL_A54A;
						}
						case -2:
						{
							GameCanvas.debug("SA77", 22);
							int num154 = msg.reader().readInt();
							Char.myCharz().yen += num154;
							GameScr.startFlyText((num154 <= 0) ? (string.Empty + num154.ToString()) : ("+" + num154.ToString()), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
							goto IL_A54A;
						}
						case -1:
						{
							GameCanvas.debug("SA77", 222);
							int num155 = msg.reader().readInt();
							Char.myCharz().xu += (long)num155;
							Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
							Char.myCharz().yen -= num155;
							GameScr.startFlyText("+" + num155.ToString(), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
							goto IL_A54A;
						}
						default:
							if (command == 18)
							{
								sbyte b100 = msg.reader().readByte();
								for (int num156 = 0; num156 < (int)b100; num156++)
								{
									int charId2 = msg.reader().readInt();
									int cx = (int)msg.reader().readShort();
									int cy = (int)msg.reader().readShort();
									int cHPShow = msg.readInt3Byte();
									Char char13 = GameScr.findCharInMap(charId2);
									if (char13 != null)
									{
										char13.cx = cx;
										char13.cy = cy;
										char13.cHP = (char13.cHPShow = (long)cHPShow);
										char13.lastUpdateTime = mSystem.currentTimeMillis();
									}
								}
								goto IL_A54A;
							}
							if (command != 19)
							{
								goto IL_A54A;
							}
							Char.myCharz().countKill = (int)msg.reader().readUnsignedShort();
							Char.myCharz().countKillMax = (int)msg.reader().readUnsignedShort();
							goto IL_A54A;
						}
					}
				}
				else if (command <= 45)
				{
					if (command != 44)
					{
						if (command != 45)
						{
							goto IL_A54A;
						}
						Mob mob21 = null;
						try
						{
							mob21 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception)
						{
						}
						if (mob21 != null)
						{
							mob21.hp = (long)msg.reader().readInt();
							mob21.updateHp_bar();
							GameScr.startFlyText(mResources.miss, mob21.x, mob21.y - mob21.h, 0, -2, mFont.MISS);
							goto IL_A54A;
						}
						goto IL_A54A;
					}
					else
					{
						int num157 = msg.reader().readInt();
						string text8 = msg.reader().readUTF();
						@char = ((Char.myCharz().charID != num157) ? GameScr.findCharInMap(num157) : Char.myCharz());
						if (@char == null)
						{
							return;
						}
						@char.addInfo(text8);
						goto IL_A54A;
					}
				}
				else
				{
					if (command == 66)
					{
						goto IL_A54A;
					}
					if (command != 74)
					{
						switch (command)
						{
						case 95:
						{
							GameCanvas.debug("SA77", 22);
							int num158 = msg.reader().readInt();
							Char.myCharz().xu += (long)num158;
							Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
							GameScr.startFlyText((num158 <= 0) ? (string.Empty + num158.ToString()) : ("+" + num158.ToString()), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
							goto IL_A54A;
						}
						case 96:
							GameCanvas.debug("SA77a", 22);
							Char.myCharz().taskOrders.addElement(new TaskOrder(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte()));
							goto IL_A54A;
						case 97:
						{
							sbyte b101 = msg.reader().readByte();
							for (int num159 = 0; num159 < Char.myCharz().taskOrders.size(); num159++)
							{
								TaskOrder taskOrder = (TaskOrder)Char.myCharz().taskOrders.elementAt(num159);
								if (taskOrder.taskId == (int)b101)
								{
									taskOrder.count = (int)msg.reader().readShort();
									break;
								}
							}
							goto IL_A54A;
						}
						default:
							goto IL_A54A;
						}
					}
				}
				Mob mob22 = null;
				try
				{
					mob22 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
				}
				if (mob22 != null && mob22.status != 0 && mob22.status != 0)
				{
					mob22.status = 0;
					ServerEffect.addServerEffect(60, mob22.x, mob22.y, 1);
					ItemMap itemMap5 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob22.x, mob22.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
					GameScr.vItemMap.addElement(itemMap5);
					if (Res.abs(itemMap5.y - Char.myCharz().cy) < 24 && Res.abs(itemMap5.x - Char.myCharz().cx) < 24)
					{
						Char.myCharz().charFocus = null;
					}
				}
				IL_A54A:;
			}
			catch (Exception)
			{
			}
			finally
			{
				if (msg != null)
				{
					msg.cleanup();
				}
			}
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x00276C68 File Offset: 0x00274E68
		private void readLogin(Message msg)
		{
			sbyte b = msg.reader().readByte();
			ChooseCharScr.playerData = new PlayerData[(int)b];
			Res.outz("[LEN] sl nguoi choi " + b.ToString());
			for (int i = 0; i < (int)b; i++)
			{
				int playerID = msg.reader().readInt();
				string name = msg.reader().readUTF();
				short head = msg.reader().readShort();
				short body = msg.reader().readShort();
				short leg = msg.reader().readShort();
				long ppoint = msg.reader().readLong();
				ChooseCharScr.playerData[i] = new PlayerData(playerID, name, head, body, leg, ppoint);
			}
			GameCanvas.chooseCharScr.switchToMe();
			GameCanvas.chooseCharScr.updateChooseCharacter((byte)b);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x00276D28 File Offset: 0x00274F28
		private void createSkill(myReader d)
		{
			GameScr.vcSkill = d.readByte();
			GameScr.gI().sOptionTemplates = new SkillOptionTemplate[(int)d.readByte()];
			for (int i = 0; i < GameScr.gI().sOptionTemplates.Length; i++)
			{
				GameScr.gI().sOptionTemplates[i] = new SkillOptionTemplate();
				GameScr.gI().sOptionTemplates[i].id = i;
				GameScr.gI().sOptionTemplates[i].name = d.readUTF();
			}
			GameScr.nClasss = new NClass[(int)d.readByte()];
			for (int j = 0; j < GameScr.nClasss.Length; j++)
			{
				GameScr.nClasss[j] = new NClass();
				GameScr.nClasss[j].classId = j;
				GameScr.nClasss[j].name = d.readUTF();
				GameScr.nClasss[j].skillTemplates = new SkillTemplate[(int)d.readByte()];
				for (int k = 0; k < GameScr.nClasss[j].skillTemplates.Length; k++)
				{
					GameScr.nClasss[j].skillTemplates[k] = new SkillTemplate();
					GameScr.nClasss[j].skillTemplates[k].id = d.readByte();
					GameScr.nClasss[j].skillTemplates[k].name = d.readUTF();
					GameScr.nClasss[j].skillTemplates[k].maxPoint = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].manaUseType = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].type = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].iconId = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].damInfo = d.readUTF();
					int lineWidth = 130;
					if (GameCanvas.w == 128 || GameCanvas.h <= 208)
					{
						lineWidth = 100;
					}
					GameScr.nClasss[j].skillTemplates[k].description = mFont.tahoma_7_green2.splitFontArray(d.readUTF(), lineWidth);
					GameScr.nClasss[j].skillTemplates[k].skills = new Skill[(int)d.readByte()];
					for (int l = 0; l < GameScr.nClasss[j].skillTemplates[k].skills.Length; l++)
					{
						GameScr.nClasss[j].skillTemplates[k].skills[l] = new Skill();
						GameScr.nClasss[j].skillTemplates[k].skills[l].skillId = d.readShort();
						GameScr.nClasss[j].skillTemplates[k].skills[l].template = GameScr.nClasss[j].skillTemplates[k];
						GameScr.nClasss[j].skillTemplates[k].skills[l].point = (int)d.readByte();
						GameScr.nClasss[j].skillTemplates[k].skills[l].powRequire = d.readLong();
						GameScr.nClasss[j].skillTemplates[k].skills[l].manaUse = (int)d.readShort();
						GameScr.nClasss[j].skillTemplates[k].skills[l].coolDown = d.readInt();
						GameScr.nClasss[j].skillTemplates[k].skills[l].dx = (int)d.readShort();
						GameScr.nClasss[j].skillTemplates[k].skills[l].dy = (int)d.readShort();
						GameScr.nClasss[j].skillTemplates[k].skills[l].maxFight = (int)d.readByte();
						GameScr.nClasss[j].skillTemplates[k].skills[l].damage = d.readShort();
						GameScr.nClasss[j].skillTemplates[k].skills[l].price = d.readShort();
						GameScr.nClasss[j].skillTemplates[k].skills[l].moreInfo = d.readUTF();
						Skills.add(GameScr.nClasss[j].skillTemplates[k].skills[l]);
					}
				}
			}
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x00277148 File Offset: 0x00275348
		private void createMap(myReader d)
		{
			GameScr.vcMap = d.readByte();
			TileMap.mapNames = new string[(int)d.readUnsignedByte()];
			for (int i = 0; i < TileMap.mapNames.Length; i++)
			{
				TileMap.mapNames[i] = d.readUTF();
			}
			Npc.arrNpcTemplate = new NpcTemplate[(int)d.readByte()];
			sbyte b = 0;
			while ((int)b < Npc.arrNpcTemplate.Length)
			{
				Npc.arrNpcTemplate[(int)b] = new NpcTemplate();
				Npc.arrNpcTemplate[(int)b].npcTemplateId = (int)b;
				Npc.arrNpcTemplate[(int)b].name = d.readUTF();
				Npc.arrNpcTemplate[(int)b].headId = (int)d.readShort();
				Npc.arrNpcTemplate[(int)b].bodyId = (int)d.readShort();
				Npc.arrNpcTemplate[(int)b].legId = (int)d.readShort();
				Npc.arrNpcTemplate[(int)b].menu = new string[(int)d.readByte()][];
				for (int j = 0; j < Npc.arrNpcTemplate[(int)b].menu.Length; j++)
				{
					Npc.arrNpcTemplate[(int)b].menu[j] = new string[(int)d.readByte()];
					for (int k = 0; k < Npc.arrNpcTemplate[(int)b].menu[j].Length; k++)
					{
						Npc.arrNpcTemplate[(int)b].menu[j][k] = d.readUTF();
					}
				}
				b += 1;
			}
			Mob.arrMobTemplate = new MobTemplate[(int)d.readByte()];
			sbyte b2 = 0;
			while ((int)b2 < Mob.arrMobTemplate.Length)
			{
				Mob.arrMobTemplate[(int)b2] = new MobTemplate();
				Mob.arrMobTemplate[(int)b2].mobTemplateId = b2;
				Mob.arrMobTemplate[(int)b2].type = d.readByte();
				Mob.arrMobTemplate[(int)b2].name = d.readUTF();
				Mob.arrMobTemplate[(int)b2].hp = (long)d.readInt();
				Mob.arrMobTemplate[(int)b2].rangeMove = d.readByte();
				Mob.arrMobTemplate[(int)b2].speed = d.readByte();
				Mob.arrMobTemplate[(int)b2].dartType = d.readByte();
				b2 += 1;
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00277350 File Offset: 0x00275550
		private void createData(myReader d, bool isSaveRMS)
		{
			GameScr.vcData = d.readByte();
			if (isSaveRMS)
			{
				Rms.saveRMS("NR_dart", NinjaUtil.readByteArray(d));
				Rms.saveRMS("NR_arrow", NinjaUtil.readByteArray(d));
				Rms.saveRMS("NR_effect", NinjaUtil.readByteArray(d));
				Rms.saveRMS("NR_image", NinjaUtil.readByteArray(d));
				Rms.saveRMS("NR_part", NinjaUtil.readByteArray(d));
				Rms.saveRMS("NR_skill", NinjaUtil.readByteArray(d));
				Rms.DeleteStorage("NRdata");
			}
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x002773D8 File Offset: 0x002755D8
		private Image createImage(sbyte[] arr)
		{
			try
			{
				return Image.createImage(arr, 0, arr.Length);
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x00277408 File Offset: 0x00275608
		public void readClanMsg(Message msg, int index)
		{
			try
			{
				ClanMessage clanMessage = new ClanMessage();
				sbyte b = msg.reader().readByte();
				clanMessage.type = (int)b;
				clanMessage.id = msg.reader().readInt();
				clanMessage.playerId = msg.reader().readInt();
				clanMessage.playerName = msg.reader().readUTF();
				clanMessage.role = msg.reader().readByte();
				clanMessage.time = (long)(msg.reader().readInt() + 1000000000);
				bool flag = false;
				GameScr.isNewClanMessage = false;
				if (b == 0)
				{
					string text = msg.reader().readUTF();
					GameScr.isNewClanMessage = true;
					if (mFont.tahoma_7.getWidth(text) > Panel.WIDTH_PANEL - 60)
					{
						clanMessage.chat = mFont.tahoma_7.splitFontArray(text, Panel.WIDTH_PANEL - 10);
					}
					else
					{
						clanMessage.chat = new string[1];
						clanMessage.chat[0] = text;
					}
					clanMessage.color = msg.reader().readByte();
				}
				else if (b == 1)
				{
					clanMessage.recieve = (int)msg.reader().readByte();
					clanMessage.maxCap = (int)msg.reader().readByte();
					flag = (msg.reader().readByte() == 1);
					if (flag)
					{
						GameScr.isNewClanMessage = true;
					}
					if (clanMessage.playerId != Char.myCharz().charID)
					{
						if (clanMessage.recieve < clanMessage.maxCap)
						{
							clanMessage.option = new string[]
							{
								mResources.donate
							};
						}
						else
						{
							clanMessage.option = null;
						}
					}
					if (GameCanvas.panel.cp != null)
					{
						GameCanvas.panel.updateRequest(clanMessage.recieve, clanMessage.maxCap);
					}
				}
				else if (b == 2 && Char.myCharz().role == 0)
				{
					GameScr.isNewClanMessage = true;
					clanMessage.option = new string[]
					{
						mResources.CANCEL,
						mResources.receive
					};
				}
				if (GameCanvas.currentScreen != GameScr.instance)
				{
					GameScr.isNewClanMessage = false;
				}
				else if (GameCanvas.panel.isShow && GameCanvas.panel.type == 0 && GameCanvas.panel.currentTabIndex == 3)
				{
					GameScr.isNewClanMessage = false;
				}
				ClanMessage.addMessage(clanMessage, index, flag);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00277640 File Offset: 0x00275840
		public void loadCurrMap(sbyte teleport3)
		{
			GameScr.gI().auto = 0;
			GameScr.isChangeZone = false;
			CreateCharScr.instance = null;
			GameScr.info1.isUpdate = false;
			GameScr.info2.isUpdate = false;
			GameScr.lockTick = 0;
			GameCanvas.panel.isShow = false;
			SoundMn.gI().stopAll();
			if (!GameScr.isLoadAllData && !CreateCharScr.isCreateChar)
			{
				GameScr.gI().initSelectChar();
			}
			GameScr.loadCamera(false, (teleport3 != 1) ? -1 : Char.myCharz().cx, (teleport3 == 0) ? -1 : 0);
			TileMap.loadMainTile();
			TileMap.loadMap(TileMap.tileID);
			Char.myCharz().cvx = 0;
			Char.myCharz().statusMe = 4;
			Char.myCharz().currentMovePoint = null;
			Char.myCharz().mobFocus = null;
			Char.myCharz().charFocus = null;
			Char.myCharz().npcFocus = null;
			Char.myCharz().itemFocus = null;
			Char.myCharz().skillPaint = null;
			Char.myCharz().setMabuHold(false);
			Char.myCharz().skillPaintRandomPaint = null;
			GameCanvas.clearAllPointerEvent();
			if (Char.myCharz().cy >= TileMap.pxh - 100)
			{
				Char.myCharz().isFlyUp = true;
				Char.myCharz().cx += Res.abs(Res.random(0, 80));
				Service.gI().charMove();
			}
			GameScr.gI().loadGameScr();
			GameCanvas.loadBG(TileMap.bgID);
			Char.isLockKey = false;
			for (int i = 0; i < Char.myCharz().vEff.size(); i++)
			{
				if (((EffectChar)Char.myCharz().vEff.elementAt(i)).template.type == 10)
				{
					Char.isLockKey = true;
					break;
				}
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			GameScr.gI().dHP = Char.myCharz().cHP;
			GameScr.gI().dMP = Char.myCharz().cMP;
			Char.ischangingMap = false;
			GameScr.gI().switchToMe();
			if (Char.myCharz().cy <= 10 && teleport3 != 0 && teleport3 != 2)
			{
				Teleport.addTeleport(new Teleport(Char.myCharz().cx, Char.myCharz().cy, Char.myCharz().head, Char.myCharz().cdir, 1, true, (teleport3 != 1) ? ((int)teleport3) : Char.myCharz().cgender));
				Char.myCharz().isTeleport = true;
			}
			if (teleport3 == 2)
			{
				Char.myCharz().show();
			}
			if (GameScr.gI().isRongThanXuatHien)
			{
				if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
				{
					GameScr.gI().callRongThan(GameScr.gI().xR, GameScr.gI().yR);
				}
				if (mGraphics.zoomLevel > 1)
				{
					GameScr.gI().doiMauTroi();
				}
			}
			InfoDlg.hide();
			InfoDlg.show(TileMap.mapName, mResources.zone + " " + TileMap.zoneID.ToString(), 30);
			GameCanvas.endDlg();
			GameCanvas.isLoading = false;
			Hint.clickMob();
			Hint.clickNpc();
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x00277948 File Offset: 0x00275B48
		public void loadInfoMap(Message msg)
		{
			try
			{
				if (mGraphics.zoomLevel == 1)
				{
					SmallImage.clearHastable();
				}
				Char.myCharz().cx = (Char.myCharz().cxSend = (Char.myCharz().cxFocus = (int)msg.reader().readShort()));
				Char.myCharz().cy = (Char.myCharz().cySend = (Char.myCharz().cyFocus = (int)msg.reader().readShort()));
				Char.myCharz().xSd = Char.myCharz().cx;
				Char.myCharz().ySd = Char.myCharz().cy;
				if (Char.myCharz().cx >= 0 && Char.myCharz().cx <= 100)
				{
					Char.myCharz().cdir = 1;
				}
				else if (Char.myCharz().cx >= TileMap.tmw - 100 && Char.myCharz().cx <= TileMap.tmw)
				{
					Char.myCharz().cdir = -1;
				}
				int num = (int)msg.reader().readByte();
				if (!GameScr.info1.isDone)
				{
					GameScr.info1.cmx = Char.myCharz().cx - GameScr.cmx;
					GameScr.info1.cmy = Char.myCharz().cy - GameScr.cmy;
				}
				for (int i = 0; i < num; i++)
				{
					Waypoint waypoint = new Waypoint(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readUTF());
					if ((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && waypoint.minX >= 0)
					{
						short minX = waypoint.minX;
					}
				}
				Resources.UnloadUnusedAssets();
				GC.Collect();
				num = (int)msg.reader().readByte();
				Mob.newMob.removeAllElements();
				sbyte b = 0;
				while ((int)b < num)
				{
					Mob mob = new Mob((int)b, msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), (int)msg.reader().readByte(), (int)msg.reader().readByte(), msg.readLong(), msg.reader().readByte(), msg.readLong(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte());
					mob.xSd = mob.x;
					mob.ySd = mob.y;
					mob.isBoss = msg.reader().readBoolean();
					if (Mob.arrMobTemplate[mob.templateId].type != 0)
					{
						if (b % 3 == 0)
						{
							mob.dir = -1;
						}
						else
						{
							mob.dir = 1;
						}
						mob.x += (int)(10 - b % 20);
					}
					mob.isMobMe = false;
					BigBoss bigBoss = null;
					BachTuoc bachTuoc = null;
					BigBoss2 bigBoss2 = null;
					NewBoss newBoss = null;
					if (mob.templateId == 70)
					{
						bigBoss = new BigBoss((int)b, (short)mob.x, (short)mob.y, 70, mob.hp, mob.maxHp, mob.sys);
					}
					if (mob.templateId == 71)
					{
						bachTuoc = new BachTuoc((int)b, (short)mob.x, (short)mob.y, 71, mob.hp, mob.maxHp, mob.sys);
					}
					if (mob.templateId == 72)
					{
						bigBoss2 = new BigBoss2((int)b, (short)mob.x, (short)mob.y, 72, mob.hp, mob.maxHp, 3);
					}
					if (mob.isBoss)
					{
						newBoss = new NewBoss((int)b, (short)mob.x, (short)mob.y, mob.templateId, mob.hp, mob.maxHp, mob.sys);
					}
					if (newBoss != null)
					{
						GameScr.vMob.addElement(newBoss);
					}
					else if (bigBoss != null)
					{
						GameScr.vMob.addElement(bigBoss);
					}
					else if (bachTuoc != null)
					{
						GameScr.vMob.addElement(bachTuoc);
					}
					else if (bigBoss2 != null)
					{
						GameScr.vMob.addElement(bigBoss2);
					}
					else
					{
						GameScr.vMob.addElement(mob);
					}
					b += 1;
				}
				if (Char.myCharz().mobMe != null && GameScr.findMobInMap(Char.myCharz().mobMe.mobId) == null)
				{
					Char.myCharz().mobMe.getData();
					Char.myCharz().mobMe.x = Char.myCharz().cx;
					Char.myCharz().mobMe.y = Char.myCharz().cy - 40;
					GameScr.vMob.addElement(Char.myCharz().mobMe);
				}
				num = (int)msg.reader().readByte();
				byte b2 = 0;
				while ((int)b2 < num)
				{
					b2 += 1;
				}
				num = (int)msg.reader().readByte();
				for (int j = 0; j < num; j++)
				{
					sbyte b3 = msg.reader().readByte();
					short cx = msg.reader().readShort();
					short num2 = msg.reader().readShort();
					sbyte b4 = msg.reader().readByte();
					short num3 = msg.reader().readShort();
					if (b4 != 6 && ((Char.myCharz().taskMaint.taskId >= 7 && (Char.myCharz().taskMaint.taskId != 7 || Char.myCharz().taskMaint.index > 1)) || (b4 != 7 && b4 != 8 && b4 != 9)) && (Char.myCharz().taskMaint.taskId >= 6 || b4 != 16))
					{
						if (b4 == 4)
						{
							GameScr.gI().magicTree = new MagicTree(j, (int)b3, (int)cx, (int)num2, (int)b4, (int)num3);
							Service.gI().magicTree(2);
							GameScr.vNpc.addElement(GameScr.gI().magicTree);
						}
						else
						{
							Npc o = new Npc(j, (int)b3, (int)cx, (int)(num2 + 3), (int)b4, (int)num3);
							GameScr.vNpc.addElement(o);
						}
					}
				}
				num = (int)msg.reader().readByte();
				string empty = string.Empty;
				empty = empty + "item: " + num.ToString();
				for (int k = 0; k < num; k++)
				{
					short itemMapID = msg.reader().readShort();
					short num4 = msg.reader().readShort();
					int x = (int)msg.reader().readShort();
					int y = (int)msg.reader().readShort();
					int num15 = msg.reader().readInt();
					short r = 0;
					if (num15 == -2)
					{
						r = msg.reader().readShort();
					}
					ItemMap itemMap = new ItemMap(num15, itemMapID, num4, x, y, r);
					bool flag = false;
					for (int l = 0; l < GameScr.vItemMap.size(); l++)
					{
						if (((ItemMap)GameScr.vItemMap.elementAt(l)).itemMapID == itemMap.itemMapID)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						GameScr.vItemMap.addElement(itemMap);
					}
					empty = empty + num4.ToString() + ",";
				}
				TileMap.vCurrItem.removeAllElements();
				if (mGraphics.zoomLevel == 1)
				{
					BgItem.clearHashTable();
				}
				BgItem.vKeysNew.removeAllElements();
				if (!GameCanvas.lowGraphic || (GameCanvas.lowGraphic && TileMap.isVoDaiMap()) || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 47 || TileMap.mapID == 48)
				{
					short num5 = msg.reader().readShort();
					empty = "item high graphic: ";
					for (int m = 0; m < (int)num5; m++)
					{
						short num6 = msg.reader().readShort();
						short num7 = msg.reader().readShort();
						short num8 = msg.reader().readShort();
						if (TileMap.getBIById((int)num6) != null)
						{
							BgItem bIById = TileMap.getBIById((int)num6);
							BgItem bgItem = new BgItem();
							bgItem.id = (int)num6;
							bgItem.idImage = bIById.idImage;
							bgItem.dx = bIById.dx;
							bgItem.dy = bIById.dy;
							bgItem.x = (int)(num7 * (short)TileMap.size);
							bgItem.y = (int)(num8 * (short)TileMap.size);
							bgItem.layer = bIById.layer;
							if (TileMap.isExistMoreOne(bgItem.id))
							{
								bgItem.trans = ((m % 2 != 0) ? 2 : 0);
								if (TileMap.mapID == 45)
								{
									bgItem.trans = 0;
								}
							}
							if (!BgItem.imgNew.containsKey(bgItem.idImage.ToString() + string.Empty))
							{
								if (mGraphics.zoomLevel == 1)
								{
									Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
									if (image == null)
									{
										image = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
								}
								else
								{
									bool flag2 = false;
									sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel.ToString() + "bgItem" + bgItem.idImage.ToString());
									if (array != null)
									{
										if (BgItem.newSmallVersion != null && array.Length % 127 != (int)BgItem.newSmallVersion[(int)bgItem.idImage])
										{
											flag2 = true;
										}
										if (!flag2)
										{
											Image image = Image.createImage(array, 0, array.Length);
											if (image != null)
											{
												BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
											}
											else
											{
												flag2 = true;
											}
										}
									}
									else
									{
										flag2 = true;
									}
									if (flag2)
									{
										Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
										if (image == null)
										{
											image = Image.createRGBImage(new int[1], 1, 1, true);
											Service.gI().getBgTemplate(bgItem.idImage);
										}
										BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
									}
								}
								BgItem.vKeysLast.addElement(bgItem.idImage.ToString() + string.Empty);
							}
							if (!BgItem.isExistKeyNews(bgItem.idImage.ToString() + string.Empty))
							{
								BgItem.vKeysNew.addElement(bgItem.idImage.ToString() + string.Empty);
							}
							bgItem.changeColor();
							TileMap.vCurrItem.addElement(bgItem);
						}
						empty = empty + num6.ToString() + ",";
					}
					for (int n = 0; n < BgItem.vKeysLast.size(); n++)
					{
						string text = (string)BgItem.vKeysLast.elementAt(n);
						if (!BgItem.isExistKeyNews(text))
						{
							BgItem.imgNew.remove(text);
							if (BgItem.imgNew.containsKey(text + "blend" + 1.ToString()))
							{
								BgItem.imgNew.remove(text + "blend" + 1.ToString());
							}
							if (BgItem.imgNew.containsKey(text + "blend" + 3.ToString()))
							{
								BgItem.imgNew.remove(text + "blend" + 3.ToString());
							}
							BgItem.vKeysLast.removeElementAt(n);
							n--;
						}
					}
					BackgroudEffect.isFog = false;
					BackgroudEffect.nCloud = 0;
					EffecMn.vEff.removeAllElements();
					BackgroudEffect.vBgEffect.removeAllElements();
					Effect.newEff.removeAllElements();
					short num9 = msg.reader().readShort();
					for (int num10 = 0; num10 < (int)num9; num10++)
					{
						string key = msg.reader().readUTF();
						string value = msg.reader().readUTF();
						this.keyValueAction(key, value);
					}
				}
				else
				{
					short num11 = msg.reader().readShort();
					for (int num12 = 0; num12 < (int)num11; num12++)
					{
						msg.reader().readShort();
						msg.reader().readShort();
						msg.reader().readShort();
					}
					short num13 = msg.reader().readShort();
					for (int num14 = 0; num14 < (int)num13; num14++)
					{
						msg.reader().readUTF();
						msg.reader().readUTF();
					}
				}
				TileMap.bgType = (int)msg.reader().readByte();
				sbyte teleport = msg.reader().readByte();
				this.loadCurrMap(teleport);
				Char.isLoadingMap = false;
				Resources.UnloadUnusedAssets();
				GC.Collect();
				ModFunc.GI().canUpdate = true;
			}
			catch (Exception)
			{
				AutoXmap.FixBlackScreen();
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00278624 File Offset: 0x00276824
		public void LoadAuraNpcs(Message msg)
		{
			sbyte sz = msg.reader().readByte();
			for (sbyte i = 0; i < sz; i += 1)
			{
				int tempId = (int)msg.reader().readByte();
				short auraId = msg.reader().readShort();
				Npc npc = ModFunc.GetNpcByTempId(tempId);
				if (npc != null)
				{
					npc.idAura = auraId;
				}
			}
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x00278674 File Offset: 0x00276874
		public void keyValueAction(string key, string value)
		{
			if (!key.Equals("eff"))
			{
				if (key.Equals("beff") && Panel.graphics <= 1)
				{
					BackgroudEffect.addEffect(int.Parse(value));
				}
				return;
			}
			if (Panel.graphics > 0)
			{
				return;
			}
			string[] array = Res.split(value, ".", 0);
			int id = int.Parse(array[0]);
			int layer = int.Parse(array[1]);
			int x = int.Parse(array[2]);
			int y = int.Parse(array[3]);
			int loop;
			int loopCount;
			if (array.Length <= 4)
			{
				loop = -1;
				loopCount = 1;
			}
			else
			{
				loop = int.Parse(array[4]);
				loopCount = int.Parse(array[5]);
			}
			Effect effect = new Effect(id, x, y, layer, loop, loopCount);
			if (array.Length > 6)
			{
				effect.typeEff = int.Parse(array[6]);
				if (array.Length > 7)
				{
					effect.indexFrom = int.Parse(array[7]);
					effect.indexTo = int.Parse(array[8]);
				}
			}
			EffecMn.addEff(effect);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0027875C File Offset: 0x0027695C
		public void messageNotMap(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				switch (b)
				{
				case 4:
					GameCanvas.loginScr.savePass();
					GameScr.isAutoPlay = false;
					GameScr.canAutoPlay = false;
					LoginScr.isUpdateAll = true;
					LoginScr.isUpdateData = true;
					LoginScr.isUpdateMap = true;
					LoginScr.isUpdateSkill = true;
					LoginScr.isUpdateItem = true;
					GameScr.vsData = msg.reader().readByte();
					GameScr.vsMap = msg.reader().readByte();
					GameScr.vsSkill = msg.reader().readByte();
					GameScr.vsItem = msg.reader().readByte();
					msg.reader().readByte();
					if (GameCanvas.loginScr.isLogin2)
					{
						Rms.saveRMSString("acc2", string.Empty);
						Rms.saveRMSString("pass2", string.Empty);
					}
					else
					{
						Rms.saveRMSString("userAo2" + ServerListScreen.ipSelect.ToString(), string.Empty);
					}
					if (GameScr.vsData != GameScr.vcData)
					{
						GameScr.isLoadAllData = false;
						Service.gI().updateData();
					}
					else
					{
						try
						{
							LoginScr.isUpdateData = false;
						}
						catch (Exception)
						{
							GameScr.vcData = -1;
							Service.gI().updateData();
						}
					}
					if (GameScr.vsMap != GameScr.vcMap)
					{
						GameScr.isLoadAllData = false;
						Service.gI().updateMap();
					}
					else
					{
						try
						{
							if (!GameScr.isLoadAllData)
							{
								DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NRmap"));
								this.createMap(dataInputStream.r);
							}
							LoginScr.isUpdateMap = false;
						}
						catch (Exception)
						{
							GameScr.vcMap = -1;
							Service.gI().updateMap();
						}
					}
					if (GameScr.vsSkill != GameScr.vcSkill)
					{
						GameScr.isLoadAllData = false;
						Service.gI().updateSkill();
					}
					else
					{
						try
						{
							if (!GameScr.isLoadAllData)
							{
								DataInputStream dataInputStream2 = new DataInputStream(Rms.loadRMS("NRskill"));
								this.createSkill(dataInputStream2.r);
							}
							LoginScr.isUpdateSkill = false;
						}
						catch (Exception)
						{
							GameScr.vcSkill = -1;
							Service.gI().updateSkill();
						}
					}
					if (GameScr.vsItem != GameScr.vcItem)
					{
						GameScr.isLoadAllData = false;
						Service.gI().updateItem();
					}
					else
					{
						try
						{
							DataInputStream dataInputStream3 = new DataInputStream(Rms.loadRMS("NRitem0"));
							this.loadItemNew(dataInputStream3.r, 0, false);
							DataInputStream dataInputStream4 = new DataInputStream(Rms.loadRMS("NRitem1"));
							this.loadItemNew(dataInputStream4.r, 1, false);
							DataInputStream dataInputStream5 = new DataInputStream(Rms.loadRMS("NRitem2"));
							this.loadItemNew(dataInputStream5.r, 2, false);
							DataInputStream dataInputStream6 = new DataInputStream(Rms.loadRMS("NRitem100"));
							this.loadItemNew(dataInputStream6.r, 100, false);
							LoginScr.isUpdateItem = false;
						}
						catch (Exception)
						{
							GameScr.vcItem = -1;
							Service.gI().updateItem();
						}
					}
					if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
					{
						if (!GameScr.isLoadAllData)
						{
							GameScr.gI().readDart();
							GameScr.gI().readEfect();
							GameScr.gI().readArrow();
							GameScr.gI().readSkill();
						}
						Service.gI().clientOk();
					}
					GameScr.exps = new long[(int)msg.reader().readByte()];
					for (int i = 0; i < GameScr.exps.Length; i++)
					{
						GameScr.exps[i] = msg.reader().readLong();
					}
					break;
				case 5:
				case 11:
				case 13:
				case 14:
				case 15:
				case 19:
					break;
				case 6:
				{
					msg.reader().mark(100000);
					this.createMap(msg.reader());
					msg.reader().reset();
					sbyte[] data3 = new sbyte[msg.reader().available()];
					msg.reader().readFully(ref data3);
					Rms.saveRMS("NRmap", data3);
					sbyte[] data4 = new sbyte[]
					{
						GameScr.vcMap
					};
					Rms.saveRMS("NRmapVersion", data4);
					LoginScr.isUpdateMap = false;
					if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
						Service.gI().clientOk();
					}
					break;
				}
				case 7:
				{
					msg.reader().mark(100000);
					this.createSkill(msg.reader());
					msg.reader().reset();
					sbyte[] data5 = new sbyte[msg.reader().available()];
					msg.reader().readFully(ref data5);
					Rms.saveRMS("NRskill", data5);
					sbyte[] data6 = new sbyte[]
					{
						GameScr.vcSkill
					};
					Rms.saveRMS("NRskillVersion", data6);
					LoginScr.isUpdateSkill = false;
					if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
						Service.gI().clientOk();
					}
					break;
				}
				case 8:
					Res.outz("GET UPDATE_ITEM " + msg.reader().available().ToString() + " bytes");
					this.createItemNew(msg.reader());
					break;
				case 9:
					GameCanvas.debug("SA11", 2);
					break;
				case 10:
					try
					{
						Char.isLoadingMap = true;
						Res.outz("REQUEST MAP TEMPLATE");
						GameCanvas.isLoading = true;
						TileMap.maps = null;
						TileMap.types = null;
						mSystem.gcc();
						GameCanvas.debug("SA99", 2);
						TileMap.tmw = (int)msg.reader().readByte();
						TileMap.tmh = (int)msg.reader().readByte();
						TileMap.maps = new int[TileMap.tmw * TileMap.tmh];
						Res.err("   M apsize= " + (TileMap.tmw * TileMap.tmh).ToString());
						for (int j = 0; j < TileMap.maps.Length; j++)
						{
							int num = (int)msg.reader().readByte();
							if (num < 0)
							{
								num += 256;
							}
							TileMap.maps[j] = (int)((ushort)num);
						}
						TileMap.types = new int[TileMap.maps.Length];
						msg = this.messWait;
						this.loadInfoMap(msg);
						try
						{
							TileMap.isMapDouble = (msg.reader().readByte() != 0);
						}
						catch (Exception ex)
						{
							Res.err(" 1 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex.ToString());
						}
					}
					catch (Exception ex2)
					{
						Res.err("2 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex2.ToString());
					}
					msg.cleanup();
					this.messWait.cleanup();
					msg = (this.messWait = null);
					GameScr.gI().switchToMe();
					break;
				case 12:
					GameCanvas.debug("SA10", 2);
					break;
				case 16:
					MoneyCharge.gI().switchToMe();
					break;
				case 17:
					Char.myCharz().clearTask();
					break;
				case 18:
				{
					GameCanvas.isLoading = false;
					GameCanvas.endDlg();
					int num2 = msg.reader().readInt();
					GameCanvas.inputDlg.show(mResources.changeNameChar, new Command(mResources.OK, GameCanvas.instance, 88829, num2), TField.INPUT_TYPE_ANY);
					break;
				}
				case 20:
					Char.myCharz().cPk = msg.reader().readByte();
					GameScr.info1.addInfo(mResources.PK_NOW + " " + Char.myCharz().cPk.ToString(), 0);
					break;
				default:
					if (b != 35)
					{
						if (b == 36)
						{
							GameScr.typeActive = msg.reader().readByte();
						}
					}
					else
					{
						GameCanvas.endDlg();
						GameScr.gI().resetButton();
						GameScr.info1.addInfo(msg.reader().readUTF(), 0);
					}
					break;
				}
			}
			catch (Exception)
			{
				Cout.LogError("LOI TAI messageNotMap + " + msg.command.ToString());
			}
			finally
			{
				if (msg != null)
				{
					msg.cleanup();
				}
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00279058 File Offset: 0x00277258
		public void messageNotLogin(Message msg)
		{
			try
			{
				if (msg.reader().readByte() == 2)
				{
					string linkDefault = msg.reader().readUTF();
					if (Rms.loadRMSInt("AdminLink") != 1)
					{
						if (mSystem.clientType == 1)
						{
							ServerListScreen.linkDefault = linkDefault;
						}
						else
						{
							ServerListScreen.linkDefault = linkDefault;
						}
						mSystem.AddIpTest();
						ServerListScreen.GetServerList(ServerListScreen.linkDefault);
						try
						{
							Panel.CanNapTien = (msg.reader().readByte() == 1);
							sbyte b3 = msg.reader().readByte();
							Rms.saveRMSInt("AdminLink", (int)b3);
						}
						catch (Exception)
						{
						}
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				if (msg != null)
				{
					msg.cleanup();
				}
			}
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x0027911C File Offset: 0x0027731C
		public void messageSubCommand(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				Debug.Log("byte: " + b.ToString());
				switch (b)
				{
				case 0:
				{
					RadarScr.list = new MyVector();
					Teleport.vTeleport.removeAllElements();
					GameScr.vCharInMap.removeAllElements();
					GameScr.vItemMap.removeAllElements();
					Char.vItemTime.removeAllElements();
					GameScr.loadImg();
					GameScr.currentCharViewInfo = Char.myCharz();
					Char.myCharz().charID = msg.reader().readInt();
					Char.myCharz().ctaskId = (int)msg.reader().readByte();
					Char.myCharz().cgender = (int)msg.reader().readByte();
					Char.myCharz().head = (int)msg.reader().readShort();
					Char.myCharz().cName = msg.reader().readUTF();
					TabController.updateCharName(Char.myCharz().cName, 0);
					Char.myCharz().cPk = msg.reader().readByte();
					Char.myCharz().cTypePk = msg.reader().readByte();
					Char.myCharz().cPower = msg.reader().readLong();
					Char.myCharz().applyCharLevelPercent();
					Char.myCharz().eff5BuffHp = (int)msg.reader().readShort();
					Char.myCharz().eff5BuffMp = (int)msg.reader().readShort();
					Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
					Char.myCharz().vSkill.removeAllElements();
					Char.myCharz().vSkillFight.removeAllElements();
					GameScr.gI().dHP = Char.myCharz().cHP;
					GameScr.gI().dMP = Char.myCharz().cMP;
					sbyte b2 = msg.reader().readByte();
					for (sbyte b3 = 0; b3 < b2; b3 += 1)
					{
						Skill skill3 = Skills.get(msg.reader().readShort());
						this.useSkill(skill3);
					}
					GameScr.gI().sortSkill();
					GameScr.gI().loadSkillShortcut();
					Char.myCharz().xu = msg.reader().readLong();
					Char.myCharz().luongKhoa = msg.reader().readInt();
					Char.myCharz().luong = msg.reader().readInt();
					Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
					Char.myCharz().luongStr = mSystem.numberTostring((long)Char.myCharz().luong);
					Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)Char.myCharz().luongKhoa);
					Char.myCharz().arrItemBody = new Item[(int)msg.reader().readByte()];
					try
					{
						Char.myCharz().setDefaultPart();
						for (int i = 0; i < Char.myCharz().arrItemBody.Length; i++)
						{
							short num6 = msg.reader().readShort();
							if (num6 != -1)
							{
								ItemTemplate itemTemplate = ItemTemplates.get(num6);
								int num7 = (int)itemTemplate.type;
								Char.myCharz().arrItemBody[i] = new Item();
								Char.myCharz().arrItemBody[i].template = itemTemplate;
								Char.myCharz().arrItemBody[i].quantity = msg.reader().readInt();
								Char.myCharz().arrItemBody[i].info = msg.reader().readUTF();
								Char.myCharz().arrItemBody[i].content = msg.reader().readUTF();
								int num8 = (int)msg.reader().readUnsignedByte();
								if (num8 != 0)
								{
									Char.myCharz().arrItemBody[i].itemOption = new ItemOption[num8];
									for (int j = 0; j < Char.myCharz().arrItemBody[i].itemOption.Length; j++)
									{
										int num9 = (int)msg.reader().readUnsignedByte();
										int param = (int)msg.reader().readUnsignedShort();
										if (num9 != -1)
										{
											Char.myCharz().arrItemBody[i].itemOption[j] = new ItemOption(num9, param);
										}
									}
								}
								if (num7 != 0)
								{
									if (num7 == 1)
									{
										Char.myCharz().leg = (int)Char.myCharz().arrItemBody[i].template.part;
									}
								}
								else
								{
									Char.myCharz().body = (int)Char.myCharz().arrItemBody[i].template.part;
								}
							}
						}
					}
					catch (Exception)
					{
					}
					Char.myCharz().arrItemBag = new Item[(int)msg.reader().readByte()];
					GameScr.hpPotion = 0;
					for (int k = 0; k < Char.myCharz().arrItemBag.Length; k++)
					{
						short num10 = msg.reader().readShort();
						if (num10 != -1)
						{
							Char.myCharz().arrItemBag[k] = new Item();
							Char.myCharz().arrItemBag[k].template = ItemTemplates.get(num10);
							Char.myCharz().arrItemBag[k].quantity = msg.reader().readInt();
							Char.myCharz().arrItemBag[k].info = msg.reader().readUTF();
							Char.myCharz().arrItemBag[k].content = msg.reader().readUTF();
							Char.myCharz().arrItemBag[k].indexUI = k;
							sbyte b4 = msg.reader().readByte();
							if (b4 != 0)
							{
								Char.myCharz().arrItemBag[k].itemOption = new ItemOption[(int)b4];
								for (int l = 0; l < Char.myCharz().arrItemBag[k].itemOption.Length; l++)
								{
									int num11 = (int)msg.reader().readUnsignedByte();
									int param2 = (int)msg.reader().readUnsignedShort();
									if (num11 != -1)
									{
										Char.myCharz().arrItemBag[k].itemOption[l] = new ItemOption(num11, param2);
										Char.myCharz().arrItemBag[k].getCompare();
									}
								}
							}
							if (Char.myCharz().arrItemBag[k].template.type == 6)
							{
								GameScr.hpPotion += Char.myCharz().arrItemBag[k].quantity;
							}
						}
					}
					Char.myCharz().arrItemBox = new Item[(int)msg.reader().readByte()];
					GameCanvas.panel.hasUse = 0;
					for (int num12 = 0; num12 < Char.myCharz().arrItemBox.Length; num12++)
					{
						short num13 = msg.reader().readShort();
						if (num13 != -1)
						{
							Char.myCharz().arrItemBox[num12] = new Item();
							Char.myCharz().arrItemBox[num12].template = ItemTemplates.get(num13);
							Char.myCharz().arrItemBox[num12].quantity = msg.reader().readInt();
							Char.myCharz().arrItemBox[num12].info = msg.reader().readUTF();
							Char.myCharz().arrItemBox[num12].content = msg.reader().readUTF();
							Char.myCharz().arrItemBox[num12].itemOption = new ItemOption[(int)msg.reader().readByte()];
							for (int num14 = 0; num14 < Char.myCharz().arrItemBox[num12].itemOption.Length; num14++)
							{
								int num15 = (int)msg.reader().readUnsignedByte();
								int param3 = (int)msg.reader().readUnsignedShort();
								if (num15 != -1)
								{
									Char.myCharz().arrItemBox[num12].itemOption[num14] = new ItemOption(num15, param3);
									Char.myCharz().arrItemBox[num12].getCompare();
								}
							}
							GameCanvas.panel.hasUse++;
						}
					}
					Char.myCharz().statusMe = 4;
					if (Rms.loadRMSInt(Char.myCharz().cName + "vci") < 1)
					{
						GameScr.isViewClanInvite = false;
					}
					else
					{
						GameScr.isViewClanInvite = true;
					}
					short num16 = msg.reader().readShort();
					Char.idHead = new short[(int)num16];
					Char.idAvatar = new short[(int)num16];
					for (int num17 = 0; num17 < (int)num16; num17++)
					{
						Char.idHead[num17] = msg.reader().readShort();
						Char.idAvatar[num17] = msg.reader().readShort();
					}
					for (int num18 = 0; num18 < GameScr.info1.charId.Length; num18++)
					{
						GameScr.info1.charId[num18] = new int[3];
					}
					GameScr.info1.charId[Char.myCharz().cgender][0] = (int)msg.reader().readShort();
					GameScr.info1.charId[Char.myCharz().cgender][1] = (int)msg.reader().readShort();
					GameScr.info1.charId[Char.myCharz().cgender][2] = (int)msg.reader().readShort();
					Char.myCharz().isNhapThe = (msg.reader().readByte() == 1);
					GameScr.deltaTime = mSystem.currentTimeMillis() - (long)msg.reader().readInt() * 1000L;
					GameScr.isNewMember = msg.reader().readByte();
					Char.myCharz().isTichXanh = (GameScr.isNewMember == 1);
					Service.gI().updateCaption((sbyte)Char.myCharz().cgender);
					Service.gI().androidPack();
					try
					{
						Char.myCharz().idAuraEff = msg.reader().readShort();
						Char.myCharz().idEff_Set_Item = (short)msg.reader().readSByte();
						Char.myCharz().idHat = msg.reader().readShort();
						goto IL_17B5;
					}
					catch (Exception)
					{
						goto IL_17B5;
					}
					break;
				}
				case 1:
					GameCanvas.debug("SA13", 2);
					Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
					Char.myCharz().cTiemNang = msg.reader().readLong();
					Char.myCharz().vSkill.removeAllElements();
					Char.myCharz().vSkillFight.removeAllElements();
					Char.myCharz().myskill = null;
					goto IL_17B5;
				case 2:
				{
					GameCanvas.debug("SA14", 2);
					if (Char.myCharz().statusMe != 14 && Char.myCharz().statusMe != 5)
					{
						Char.myCharz().cHP = Char.myCharz().cHPFull;
						Char.myCharz().cMP = Char.myCharz().cMPFull;
						Cout.LogError2(" ME_LOAD_SKILL");
					}
					Char.myCharz().vSkill.removeAllElements();
					Char.myCharz().vSkillFight.removeAllElements();
					sbyte b5 = msg.reader().readByte();
					for (sbyte b6 = 0; b6 < b5; b6 += 1)
					{
						Skill skill4 = Skills.get(msg.reader().readShort());
						this.useSkill(skill4);
					}
					GameScr.gI().sortSkill();
					if (GameScr.isPaintInfoMe)
					{
						GameScr.indexRow = -1;
						GameScr.gI().left = (GameScr.gI().center = null);
						goto IL_17B5;
					}
					goto IL_17B5;
				}
				case 3:
				case 16:
				case 17:
				case 18:
				case 20:
				case 22:
				case 24:
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
					goto IL_17B5;
				case 4:
					break;
				case 5:
				{
					long cHP = Char.myCharz().cHP;
					Char.myCharz().cHP = msg.readLong();
					if (Char.myCharz().cHP > cHP && Char.myCharz().cTypePk != 4)
					{
						GameScr.startFlyText("+" + (Char.myCharz().cHP - cHP).ToString() + " " + mResources.HP, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 20, 0, -1, mFont.HP);
						SoundMn.gI().HP_MPup();
						if (Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 5003)
						{
							MonsterDart.addMonsterDart(Char.myCharz().petFollow.cmx + ((Char.myCharz().petFollow.dir != 1) ? -10 : 10), Char.myCharz().petFollow.cmy + 10, true, -1L, -1L, Char.myCharz(), 29);
						}
					}
					if (Char.myCharz().cHP < cHP)
					{
						GameScr.startFlyText("-" + (cHP - Char.myCharz().cHP).ToString() + " " + mResources.HP, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 20, 0, -1, mFont.HP);
					}
					GameScr.gI().dHP = Char.myCharz().cHP;
					if (GameScr.isPaintInfoMe)
					{
						goto IL_17B5;
					}
					goto IL_17B5;
				}
				case 6:
				{
					if (Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5)
					{
						goto IL_17B5;
					}
					long cMP = Char.myCharz().cMP;
					Char.myCharz().cMP = msg.readLong();
					if (Char.myCharz().cMP > cMP)
					{
						GameScr.startFlyText("+" + (Char.myCharz().cMP - cMP).ToString() + " " + mResources.KI, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 23, 0, -2, mFont.MP);
						SoundMn.gI().HP_MPup();
						if (Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 5001)
						{
							MonsterDart.addMonsterDart(Char.myCharz().petFollow.cmx + ((Char.myCharz().petFollow.dir != 1) ? -10 : 10), Char.myCharz().petFollow.cmy + 10, true, -1L, -1L, Char.myCharz(), 29);
						}
					}
					if (Char.myCharz().cMP < cMP)
					{
						GameScr.startFlyText("-" + (cMP - Char.myCharz().cMP).ToString() + " " + mResources.KI, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 23, 0, -2, mFont.MP);
					}
					GameScr.gI().dMP = Char.myCharz().cMP;
					if (GameScr.isPaintInfoMe)
					{
						goto IL_17B5;
					}
					goto IL_17B5;
				}
				case 7:
				{
					Char @char = GameScr.findCharInMap(msg.reader().readInt());
					if (@char != null)
					{
						@char.clanID = msg.reader().readInt();
						if (@char.clanID == -2)
						{
							@char.isCopy = true;
						}
						this.readCharInfo(@char, msg);
						try
						{
							@char.idAuraEff = msg.reader().readShort();
							@char.idEff_Set_Item = (short)msg.reader().readSByte();
							@char.idHat = msg.reader().readShort();
							if (@char.bag >= 201)
							{
								@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
								{
									typeEff = 5
								});
							}
							else
							{
								@char.removeEffChar(0, 201);
							}
							goto IL_17B5;
						}
						catch (Exception)
						{
							goto IL_17B5;
						}
						goto IL_1177;
					}
					goto IL_17B5;
				}
				case 8:
					goto IL_1177;
				case 9:
				{
					GameCanvas.debug("SA27", 2);
					Char char2 = GameScr.findCharInMap(msg.reader().readInt());
					if (char2 != null)
					{
						char2.cHP = (long)msg.readInt3Byte();
						char2.cHPFull = (long)msg.readInt3Byte();
						goto IL_17B5;
					}
					goto IL_17B5;
				}
				case 10:
				{
					GameCanvas.debug("SA28", 2);
					Char char3 = GameScr.findCharInMap(msg.reader().readInt());
					if (char3 == null)
					{
						goto IL_17B5;
					}
					char3.cHP = (long)msg.readInt3Byte();
					char3.cHPFull = (long)msg.readInt3Byte();
					char3.eff5BuffHp = (int)msg.reader().readShort();
					char3.eff5BuffMp = (int)msg.reader().readShort();
					char3.wp = (int)msg.reader().readShort();
					if (char3.wp == -1)
					{
						char3.setDefaultWeapon();
						goto IL_17B5;
					}
					goto IL_17B5;
				}
				case 11:
				{
					GameCanvas.debug("SA29", 2);
					Char char4 = GameScr.findCharInMap(msg.reader().readInt());
					if (char4 == null)
					{
						goto IL_17B5;
					}
					char4.cHP = (long)msg.readInt3Byte();
					char4.cHPFull = (long)msg.readInt3Byte();
					char4.eff5BuffHp = (int)msg.reader().readShort();
					char4.eff5BuffMp = (int)msg.reader().readShort();
					char4.body = (int)msg.reader().readShort();
					if (char4.body == -1)
					{
						char4.setDefaultBody();
						goto IL_17B5;
					}
					goto IL_17B5;
				}
				case 12:
				{
					GameCanvas.debug("SA30", 2);
					Char char5 = GameScr.findCharInMap(msg.reader().readInt());
					if (char5 == null)
					{
						goto IL_17B5;
					}
					char5.cHP = (long)msg.readInt3Byte();
					char5.cHPFull = (long)msg.readInt3Byte();
					char5.eff5BuffHp = (int)msg.reader().readShort();
					char5.eff5BuffMp = (int)msg.reader().readShort();
					char5.leg = (int)msg.reader().readShort();
					if (char5.leg == -1)
					{
						char5.setDefaultLeg();
						goto IL_17B5;
					}
					goto IL_17B5;
				}
				case 13:
				{
					GameCanvas.debug("SA31", 2);
					int num19 = msg.reader().readInt();
					Char char6 = (num19 != Char.myCharz().charID) ? GameScr.findCharInMap(num19) : Char.myCharz();
					if (char6 != null)
					{
						char6.cHP = (long)msg.readInt3Byte();
						char6.cHPFull = (long)msg.readInt3Byte();
						char6.eff5BuffHp = (int)msg.reader().readShort();
						char6.eff5BuffMp = (int)msg.reader().readShort();
						goto IL_17B5;
					}
					goto IL_17B5;
				}
				case 14:
				{
					Char char7 = GameScr.findCharInMap(msg.reader().readInt());
					if (char7 != null)
					{
						char7.cHP = msg.readLong();
						sbyte b8 = msg.reader().readByte();
						if (b8 == 1)
						{
							ServerEffect.addServerEffect(11, char7, 5);
							ServerEffect.addServerEffect(104, char7, 4);
						}
						if (b8 == 2)
						{
							char7.doInjure();
						}
						try
						{
							char7.cHPFull = msg.readLong();
							goto IL_17B5;
						}
						catch (Exception)
						{
							goto IL_17B5;
						}
						goto IL_1492;
					}
					goto IL_17B5;
				}
				case 15:
					goto IL_1492;
				case 19:
					GameCanvas.debug("SA17", 2);
					Char.myCharz().boxSort();
					goto IL_17B5;
				case 21:
				{
					int num20 = msg.reader().readInt();
					Char.myCharz().xuInBox -= num20;
					Char.myCharz().xu += (long)num20;
					Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
					goto IL_17B5;
				}
				case 23:
				{
					short num21 = msg.reader().readShort();
					Skill skill5 = Skills.get(num21);
					this.useSkill(skill5);
					if (num21 != 0 && num21 != 14 && num21 != 28)
					{
						GameScr.info1.addInfo(mResources.LEARN_SKILL + " " + skill5.template.name, 0);
						goto IL_17B5;
					}
					goto IL_17B5;
				}
				case 35:
				{
					GameCanvas.debug("SY3", 2);
					int num22 = msg.reader().readInt();
					Res.outz("CID = " + num22.ToString());
					if (TileMap.mapID == 130)
					{
						GameScr.gI().starVS();
					}
					if (num22 == Char.myCharz().charID)
					{
						Char.myCharz().cTypePk = msg.reader().readByte();
						if (GameScr.gI().isVS() && Char.myCharz().cTypePk != 0)
						{
							GameScr.gI().starVS();
						}
						Res.outz("type pk= " + Char.myCharz().cTypePk.ToString());
						Char.myCharz().npcFocus = null;
						if (!GameScr.gI().isMeCanAttackMob(Char.myCharz().mobFocus))
						{
							Char.myCharz().mobFocus = null;
						}
						Char.myCharz().itemFocus = null;
					}
					else
					{
						Char char8 = GameScr.findCharInMap(num22);
						if (char8 != null)
						{
							Res.outz("type pk= " + char8.cTypePk.ToString());
							char8.cTypePk = msg.reader().readByte();
							if (char8.isAttacPlayerStatus())
							{
								Char.myCharz().charFocus = char8;
							}
						}
					}
					for (int m = 0; m < GameScr.vCharInMap.size(); m++)
					{
						Char char9 = GameScr.findCharInMap(m);
						if (char9 != null && char9.cTypePk != 0 && char9.cTypePk == Char.myCharz().cTypePk)
						{
							if (!Char.myCharz().mobFocus.isMobMe)
							{
								Char.myCharz().mobFocus = null;
							}
							Char.myCharz().npcFocus = null;
							Char.myCharz().itemFocus = null;
							break;
						}
					}
					Res.outz("update type pk= ");
					goto IL_17B5;
				}
				default:
					switch (b)
					{
					case 61:
					{
						string text = msg.reader().readUTF();
						sbyte[] data = new sbyte[msg.reader().readInt()];
						msg.reader().read(ref data);
						if (data.Length == 0)
						{
							data = null;
						}
						if (text.Equals("KSkill"))
						{
							GameScr.gI().onKSkill(data);
							goto IL_17B5;
						}
						if (text.Equals("OSkill"))
						{
							GameScr.gI().onOSkill(data);
							goto IL_17B5;
						}
						if (text.Equals("CSkill"))
						{
							GameScr.gI().onCSkill(data);
							goto IL_17B5;
						}
						goto IL_17B5;
					}
					case 62:
						Res.outz("ME UPDATE SKILL");
						this.read_UpdateSkill(msg);
						goto IL_17B5;
					case 63:
					{
						sbyte b7 = msg.reader().readByte();
						if (b7 > 0)
						{
							GameCanvas.panel.vPlayerMenu_id.removeAllElements();
							InfoDlg.showWait();
							MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
							for (int n = 0; n < (int)b7; n++)
							{
								string caption3 = msg.reader().readUTF();
								string caption2 = msg.reader().readUTF();
								short num23 = msg.reader().readShort();
								GameCanvas.panel.vPlayerMenu_id.addElement(num23.ToString() + string.Empty);
								Char.myCharz().charFocus.menuSelect = (int)num23;
								vPlayerMenu.addElement(new Command(caption3, 11115, Char.myCharz().charFocus)
								{
									caption2 = caption2
								});
							}
							InfoDlg.hide();
							GameCanvas.panel.setTabPlayerMenu();
							goto IL_17B5;
						}
						goto IL_17B5;
					}
					default:
						goto IL_17B5;
					}
					break;
				}
				Char.myCharz().xu = msg.reader().readLong();
				Char.myCharz().luong = msg.reader().readInt();
				Char.myCharz().cHP = msg.readLong();
				Char.myCharz().cMP = msg.readLong();
				Char.myCharz().luongKhoa = msg.reader().readInt();
				Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
				Char.myCharz().luongStr = mSystem.numberTostring((long)Char.myCharz().luong);
				Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)Char.myCharz().luongKhoa);
				goto IL_17B5;
				IL_1177:
				GameCanvas.debug("SA26", 2);
				Char char10 = GameScr.findCharInMap(msg.reader().readInt());
				if (char10 != null)
				{
					char10.cspeed = (int)msg.reader().readByte();
					goto IL_17B5;
				}
				goto IL_17B5;
				IL_1492:
				Char char11 = GameScr.findCharInMap(msg.reader().readInt());
				if (char11 != null)
				{
					char11.cHP = msg.readLong();
					char11.cHPFull = msg.readLong();
					char11.cx = (int)msg.reader().readShort();
					char11.cy = (int)msg.reader().readShort();
					char11.statusMe = 1;
					char11.cp3 = 3;
					ServerEffect.addServerEffect(109, char11, 2);
				}
				IL_17B5:;
			}
			catch (Exception ex5)
			{
				Cout.println("Loi tai Sub : " + ex5.ToString());
			}
			finally
			{
				if (msg != null)
				{
					msg.cleanup();
				}
			}
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x0027A998 File Offset: 0x00278B98
		private void useSkill(Skill skill)
		{
			if (Char.myCharz().myskill == null)
			{
				Char.myCharz().myskill = skill;
			}
			else if (skill.template.Equals(Char.myCharz().myskill.template))
			{
				Char.myCharz().myskill = skill;
			}
			Char.myCharz().vSkill.addElement(skill);
			if ((skill.template.type == 1 || skill.template.type == 4 || skill.template.type == 2 || skill.template.type == 3) && (skill.template.maxPoint == 0 || (skill.template.maxPoint > 0 && skill.point > 0)))
			{
				if ((int)skill.template.id == Char.myCharz().skillTemplateId)
				{
					Service.gI().selectSkill(Char.myCharz().skillTemplateId);
				}
				Char.myCharz().vSkillFight.addElement(skill);
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x0027AA8C File Offset: 0x00278C8C
		public bool readCharInfo(Char c, Message msg)
		{
			try
			{
				c.clevel = (int)msg.reader().readByte();
				c.isInvisiblez = msg.reader().readBoolean();
				c.cTypePk = msg.reader().readByte();
				c.nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				c.cgender = (int)msg.reader().readByte();
				c.head = (int)msg.reader().readShort();
				c.cName = msg.reader().readUTF();
				c.cHP = msg.readLong();
				c.dHP = c.cHP;
				if (c.cHP == 0L)
				{
					c.statusMe = 14;
				}
				c.cHPFull = msg.readLong();
				if (c.cy >= TileMap.pxh - 100)
				{
					c.isFlyUp = true;
				}
				c.body = (int)msg.reader().readShort();
				c.leg = (int)msg.reader().readShort();
				c.bag = (int)msg.reader().readUnsignedByte();
				c.isShadown = true;
				msg.reader().readByte();
				if (c.wp == -1)
				{
					c.setDefaultWeapon();
				}
				if (c.body == -1)
				{
					c.setDefaultBody();
				}
				if (c.leg == -1)
				{
					c.setDefaultLeg();
				}
				c.cx = (int)msg.reader().readShort();
				c.cy = (int)msg.reader().readShort();
				c.xSd = c.cx;
				c.ySd = c.cy;
				c.eff5BuffHp = (int)msg.reader().readShort();
				c.eff5BuffMp = (int)msg.reader().readShort();
				int num = (int)msg.reader().readByte();
				for (int i = 0; i < num; i++)
				{
					EffectChar effectChar = new EffectChar(msg.reader().readByte(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readShort());
					c.vEff.addElement(effectChar);
					if (effectChar.template.type == 12 || effectChar.template.type == 11)
					{
						c.isInvisiblez = true;
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			return false;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x0027ACDC File Offset: 0x00278EDC
		private void readGetImgByName(Message msg)
		{
			try
			{
				string text = msg.reader().readUTF();
				sbyte nFrame = msg.reader().readByte();
				sbyte[] array = NinjaUtil.readByteArray(msg);
				Image img = this.createImage(array);
				ImgByName.SetImage(text, img, nFrame);
				if (array != null)
				{
					ImgByName.saveRMS(text, nFrame, array);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x0027AD3C File Offset: 0x00278F3C
		private void createItemNew(myReader d)
		{
			try
			{
				this.loadItemNew(d, -1, true);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x0027AD68 File Offset: 0x00278F68
		private void loadItemNew(myReader d, sbyte type, bool isSave)
		{
			try
			{
				d.mark(100000);
				GameScr.vcItem = d.readByte();
				type = d.readByte();
				if (type == 0)
				{
					GameScr.gI().iOptionTemplates = new ItemOptionTemplate[(int)d.readUnsignedByte()];
					for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
					{
						GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
						GameScr.gI().iOptionTemplates[i].id = i;
						GameScr.gI().iOptionTemplates[i].name = d.readUTF();
						GameScr.gI().iOptionTemplates[i].type = (int)d.readByte();
					}
					if (isSave)
					{
						d.reset();
						sbyte[] data = new sbyte[d.available()];
						d.readFully(ref data);
						Rms.saveRMS("NRitem0", data);
					}
				}
				else if (type == 1)
				{
					ItemTemplates.itemTemplates.clear();
					int num = (int)d.readShort();
					for (int j = 0; j < num; j++)
					{
						ItemTemplates.add(new ItemTemplate((short)j, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBoolean()));
					}
					if (isSave)
					{
						d.reset();
						sbyte[] data2 = new sbyte[d.available()];
						d.readFully(ref data2);
						Rms.saveRMS("NRitem1", data2);
					}
				}
				else if (type == 2)
				{
					int num3 = (int)d.readShort();
					int num2 = (int)d.readShort();
					for (int k = num3; k < num2; k++)
					{
						ItemTemplates.add(new ItemTemplate((short)k, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBoolean()));
					}
					if (isSave)
					{
						d.reset();
						sbyte[] data3 = new sbyte[d.available()];
						d.readFully(ref data3);
						Rms.saveRMS("NRitem2", data3);
						sbyte[] data4 = new sbyte[]
						{
							GameScr.vcItem
						};
						Rms.saveRMS("NRitemVersion", data4);
						LoginScr.isUpdateItem = false;
						if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
						{
							GameScr.gI().readDart();
							GameScr.gI().readEfect();
							GameScr.gI().readArrow();
							GameScr.gI().readSkill();
							Service.gI().clientOk();
						}
					}
				}
				else if (type == 100)
				{
					Char.Arr_Head_2Fr = this.readArrHead(d);
					if (isSave)
					{
						d.reset();
						sbyte[] data5 = new sbyte[d.available()];
						d.readFully(ref data5);
						Rms.saveRMS("NRitem100", data5);
					}
				}
			}
			catch (Exception ex)
			{
				ex.ToString();
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x0027B060 File Offset: 0x00279260
		private void readFrameBoss(Message msg, int mobTemplateId)
		{
			try
			{
				int num = (int)msg.reader().readByte();
				int[][] array = new int[num][];
				for (int i = 0; i < num; i++)
				{
					int num2 = (int)msg.reader().readByte();
					array[i] = new int[num2];
					for (int j = 0; j < num2; j++)
					{
						array[i][j] = (int)msg.reader().readByte();
					}
				}
				Controller.frameHT_NEWBOSS.put(mobTemplateId.ToString() + string.Empty, array);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x0027B0F4 File Offset: 0x002792F4
		private int[][] readArrHead(myReader d)
		{
			int[][] array = new int[][]
			{
				new int[]
				{
					542,
					543
				}
			};
			try
			{
				array = new int[(int)d.readShort()][];
				for (int i = 0; i < array.Length; i++)
				{
					int num2 = (int)d.readByte();
					array[i] = new int[num2];
					for (int j = 0; j < num2; j++)
					{
						array[i][j] = (int)d.readShort();
					}
				}
			}
			catch (Exception)
			{
			}
			return array;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0027B178 File Offset: 0x00279378
		public void phuban_Info(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					this.readPhuBan_CHIENTRUONGNAMEK(msg, (int)b);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0027B1B4 File Offset: 0x002793B4
		private void readPhuBan_CHIENTRUONGNAMEK(Message msg, int type_PB)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					short idmapPaint = msg.reader().readShort();
					string nameTeam = msg.reader().readUTF();
					string nameTeam2 = msg.reader().readUTF();
					int maxPoint = msg.reader().readInt();
					short timeSecond = msg.reader().readShort();
					int maxLife = (int)msg.reader().readByte();
					GameScr.phuban_Info = new InfoPhuBan(type_PB, idmapPaint, nameTeam, nameTeam2, maxPoint, timeSecond);
					GameScr.phuban_Info.maxLife = maxLife;
					GameScr.phuban_Info.updateLife(type_PB, 0, 0);
				}
				else if (b == 1)
				{
					int pointTeam = msg.reader().readInt();
					int pointTeam2 = msg.reader().readInt();
					if (GameScr.phuban_Info != null)
					{
						GameScr.phuban_Info.updatePoint(type_PB, pointTeam, pointTeam2);
					}
				}
				else if (b == 2)
				{
					sbyte b2 = msg.reader().readByte();
					short type = 0;
					if (b2 == 1)
					{
						type = 1;
					}
					else if (b2 == 2)
					{
						type = 2;
					}
					short num = -1;
					GameScr.phuban_Info = null;
					GameScr.addEffectEnd((int)type, (int)num, 0, GameCanvas.hw, GameCanvas.hh, 0, 0, -1, null);
				}
				else if (b == 5)
				{
					short timeSecond2 = msg.reader().readShort();
					if (GameScr.phuban_Info != null)
					{
						GameScr.phuban_Info.updateTime(type_PB, timeSecond2);
					}
				}
				else if (b == 4)
				{
					int lifeTeam = (int)msg.reader().readByte();
					int lifeTeam2 = (int)msg.reader().readByte();
					if (GameScr.phuban_Info != null)
					{
						GameScr.phuban_Info.updateLife(type_PB, lifeTeam, lifeTeam2);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0027B354 File Offset: 0x00279554
		public void read_opt(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					short idHat = msg.reader().readShort();
					Char.myCharz().idHat = idHat;
					SoundMn.gI().getStrOption();
				}
				else if (b == 2)
				{
					int num = msg.reader().readInt();
					sbyte b2 = msg.reader().readByte();
					short num2 = msg.reader().readShort();
					string v = num2.ToString() + "," + b2.ToString();
					ImgByName.getImagePath("banner_" + num2.ToString(), ImgByName.hashImagePath);
					GameCanvas.danhHieu.put(num.ToString() + string.Empty, v);
				}
				else if (b == 3)
				{
					short num3 = msg.reader().readShort();
					SmallImage.createImage((int)num3);
					BackgroudEffect.id_water1 = num3;
				}
				else if (b == 4)
				{
					string o = msg.reader().readUTF();
					GameCanvas.messageServer.addElement(o);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x0027B464 File Offset: 0x00279664
		public void read_UpdateSkill(Message msg)
		{
			try
			{
				short num = msg.reader().readShort();
				sbyte b = -1;
				try
				{
					b = msg.reader().readSByte();
				}
				catch (Exception)
				{
				}
				if (b == 0)
				{
					short curExp = msg.reader().readShort();
					for (int i = 0; i < Char.myCharz().vSkill.size(); i++)
					{
						Skill skill = (Skill)Char.myCharz().vSkill.elementAt(i);
						if (skill.skillId == num)
						{
							skill.curExp = curExp;
							break;
						}
					}
				}
				else if (b == 1)
				{
					sbyte b2 = msg.reader().readByte();
					for (int j = 0; j < Char.myCharz().vSkill.size(); j++)
					{
						Skill skill2 = (Skill)Char.myCharz().vSkill.elementAt(j);
						if (skill2.skillId == num)
						{
							for (int k = 0; k < 20; k++)
							{
								ImgByName.getImagePath(string.Concat(new string[]
								{
									"Skills_",
									skill2.template.id.ToString(),
									"_",
									b2.ToString(),
									"_",
									k.ToString()
								}), ImgByName.hashImagePath);
							}
							break;
						}
					}
				}
				else if (b == -1)
				{
					Skill skill3 = Skills.get(num);
					for (int l = 0; l < Char.myCharz().vSkill.size(); l++)
					{
						if (((Skill)Char.myCharz().vSkill.elementAt(l)).template.id == skill3.template.id)
						{
							Char.myCharz().vSkill.setElementAt(skill3, l);
							break;
						}
					}
					for (int m = 0; m < Char.myCharz().vSkillFight.size(); m++)
					{
						if (((Skill)Char.myCharz().vSkillFight.elementAt(m)).template.id == skill3.template.id)
						{
							Char.myCharz().vSkillFight.setElementAt(skill3, m);
							break;
						}
					}
					for (int n = 0; n < GameScr.onScreenSkill.Length; n++)
					{
						if (GameScr.onScreenSkill[n] != null && GameScr.onScreenSkill[n].template.id == skill3.template.id)
						{
							GameScr.onScreenSkill[n] = skill3;
							break;
						}
					}
					for (int num2 = 0; num2 < GameScr.keySkill.Length; num2++)
					{
						if (GameScr.keySkill[num2] != null && GameScr.keySkill[num2].template.id == skill3.template.id)
						{
							GameScr.keySkill[num2] = skill3;
							break;
						}
					}
					if (Char.myCharz().myskill.template.id == skill3.template.id)
					{
						Char.myCharz().myskill = skill3;
					}
					GameScr.info1.addInfo(mResources.hasJustUpgrade1 + skill3.template.name + mResources.hasJustUpgrade2 + skill3.point.ToString(), 0);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04004D36 RID: 19766
		protected static Controller me;

		// Token: 0x04004D37 RID: 19767
		public Message messWait;

		// Token: 0x04004D38 RID: 19768
		public static bool isLoadingData = false;

		// Token: 0x04004D39 RID: 19769
		public static bool isConnectOK;

		// Token: 0x04004D3A RID: 19770
		public static bool isConnectionFail;

		// Token: 0x04004D3B RID: 19771
		public static bool isDisconnected;

		// Token: 0x04004D3C RID: 19772
		public static bool isMain;

		// Token: 0x04004D3D RID: 19773
		public static bool isStopReadMessage;

		// Token: 0x04004D3E RID: 19774
		public static MyHashTable frameHT_NEWBOSS = new MyHashTable();
	}
}
