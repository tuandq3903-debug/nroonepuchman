using System;
using Game6.Assets.src.g;

namespace Game6.Assets.src.f
{
	// Token: 0x020000DB RID: 219
	internal class Controller2
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x00094CFC File Offset: 0x00092EFC
		public static void readMessage(Message msg)
		{
			try
			{
				sbyte command = msg.command;
				if (command <= 42)
				{
					switch (command)
					{
					case -128:
						Controller2.readInfoEffChar(msg);
						break;
					case -127:
						Controller2.readLuckyRound(msg);
						break;
					case -126:
					{
						sbyte type = msg.reader().readByte();
						Res.outz("type quay= " + type.ToString());
						if (type == 1)
						{
							msg.reader().readByte();
							string num38 = msg.reader().readUTF();
							string finish = msg.reader().readUTF();
							GameScr.gI().showWinNumber(num38, finish);
						}
						if (type == 0)
						{
							GameScr.gI().showYourNumber(msg.reader().readUTF());
						}
						break;
					}
					case -125:
					{
						ChatTextField.gI().isShow = false;
						string text4 = msg.reader().readUTF();
						Res.outz("titile= " + text4);
						sbyte b32 = msg.reader().readByte();
						ClientInput.gI().setInput((int)b32, text4);
						for (int num39 = 0; num39 < (int)b32; num39++)
						{
							ClientInput.gI().tf[num39].name = msg.reader().readUTF();
							sbyte b55 = msg.reader().readByte();
							if (b55 == 0)
							{
								ClientInput.gI().tf[num39].setIputType(TField.INPUT_TYPE_NUMERIC);
							}
							if (b55 == 1)
							{
								ClientInput.gI().tf[num39].setIputType(TField.INPUT_TYPE_ANY);
							}
							if (b55 == 2)
							{
								ClientInput.gI().tf[num39].setIputType(TField.INPUT_TYPE_PASSWORD);
							}
						}
						break;
					}
					case -124:
					{
						sbyte b33 = msg.reader().readByte();
						sbyte b56 = msg.reader().readByte();
						if (b56 == 0)
						{
							if (b33 == 2)
							{
								int num40 = msg.reader().readInt();
								if (num40 == Char.myCharz().charID)
								{
									Char.myCharz().removeEffect();
								}
								else if (GameScr.findCharInMap(num40) != null)
								{
									GameScr.findCharInMap(num40).removeEffect();
								}
							}
							int num41 = (int)msg.reader().readUnsignedByte();
							int num42 = msg.reader().readInt();
							if (num41 == 32)
							{
								if (b33 == 1)
								{
									int num43 = msg.reader().readInt();
									if (num42 == Char.myCharz().charID)
									{
										Char.myCharz().holdEffID = num41;
										GameScr.findCharInMap(num43).setHoldChar(Char.myCharz());
									}
									else if (GameScr.findCharInMap(num42) != null && num43 != Char.myCharz().charID)
									{
										GameScr.findCharInMap(num42).holdEffID = num41;
										GameScr.findCharInMap(num43).setHoldChar(GameScr.findCharInMap(num42));
									}
									else if (GameScr.findCharInMap(num42) != null && num43 == Char.myCharz().charID)
									{
										GameScr.findCharInMap(num42).holdEffID = num41;
										Char.myCharz().setHoldChar(GameScr.findCharInMap(num42));
									}
								}
								else if (num42 == Char.myCharz().charID)
								{
									Char.myCharz().removeHoleEff();
								}
								else if (GameScr.findCharInMap(num42) != null)
								{
									GameScr.findCharInMap(num42).removeHoleEff();
								}
							}
							if (num41 == 33)
							{
								if (b33 == 1)
								{
									if (num42 == Char.myCharz().charID)
									{
										Char.myCharz().protectEff = true;
									}
									else if (GameScr.findCharInMap(num42) != null)
									{
										GameScr.findCharInMap(num42).protectEff = true;
									}
								}
								else if (num42 == Char.myCharz().charID)
								{
									Char.myCharz().removeProtectEff();
								}
								else if (GameScr.findCharInMap(num42) != null)
								{
									GameScr.findCharInMap(num42).removeProtectEff();
								}
							}
							if (num41 == 39)
							{
								if (b33 == 1)
								{
									if (num42 == Char.myCharz().charID)
									{
										Char.myCharz().huytSao = true;
									}
									else if (GameScr.findCharInMap(num42) != null)
									{
										GameScr.findCharInMap(num42).huytSao = true;
									}
								}
								else if (num42 == Char.myCharz().charID)
								{
									Char.myCharz().removeHuytSao();
								}
								else if (GameScr.findCharInMap(num42) != null)
								{
									GameScr.findCharInMap(num42).removeHuytSao();
								}
							}
							if (num41 == 40)
							{
								if (b33 == 1)
								{
									if (num42 == Char.myCharz().charID)
									{
										Char.myCharz().blindEff = true;
									}
									else if (GameScr.findCharInMap(num42) != null)
									{
										GameScr.findCharInMap(num42).blindEff = true;
									}
								}
								else if (num42 == Char.myCharz().charID)
								{
									Char.myCharz().removeBlindEff();
								}
								else if (GameScr.findCharInMap(num42) != null)
								{
									GameScr.findCharInMap(num42).removeBlindEff();
								}
							}
							if (num41 == 41)
							{
								if (b33 == 1)
								{
									if (num42 == Char.myCharz().charID)
									{
										Char.myCharz().sleepEff = true;
									}
									else if (GameScr.findCharInMap(num42) != null)
									{
										GameScr.findCharInMap(num42).sleepEff = true;
									}
								}
								else if (num42 == Char.myCharz().charID)
								{
									Char.myCharz().removeSleepEff();
								}
								else if (GameScr.findCharInMap(num42) != null)
								{
									GameScr.findCharInMap(num42).removeSleepEff();
								}
							}
							if (num41 == 42)
							{
								if (b33 == 1)
								{
									if (num42 == Char.myCharz().charID)
									{
										Char.myCharz().stone = true;
									}
								}
								else if (num42 == Char.myCharz().charID)
								{
									Char.myCharz().stone = false;
								}
							}
						}
						if (b56 == 1)
						{
							int num44 = (int)msg.reader().readUnsignedByte();
							sbyte b34 = msg.reader().readByte();
							Res.outz(string.Concat(new string[]
							{
								"modbHoldID= ",
								b34.ToString(),
								" skillID= ",
								num44.ToString(),
								"eff ID= ",
								b33.ToString()
							}));
							if (num44 == 32)
							{
								if (b33 == 1)
								{
									int num45 = msg.reader().readInt();
									if (num45 == Char.myCharz().charID)
									{
										GameScr.findMobInMap(b34).holdEffID = num44;
										Char.myCharz().setHoldMob(GameScr.findMobInMap(b34));
									}
									else if (GameScr.findCharInMap(num45) != null)
									{
										GameScr.findMobInMap(b34).holdEffID = num44;
										GameScr.findCharInMap(num45).setHoldMob(GameScr.findMobInMap(b34));
									}
								}
								else
								{
									GameScr.findMobInMap(b34).removeHoldEff();
								}
							}
							if (num44 == 40)
							{
								if (b33 == 1)
								{
									GameScr.findMobInMap(b34).blindEff = true;
								}
								else
								{
									GameScr.findMobInMap(b34).removeBlindEff();
								}
							}
							if (num44 == 41)
							{
								if (b33 == 1)
								{
									GameScr.findMobInMap(b34).sleepEff = true;
								}
								else
								{
									GameScr.findMobInMap(b34).removeSleepEff();
								}
							}
						}
						break;
					}
					case -123:
					{
						int charId3 = msg.reader().readInt();
						if (GameScr.findCharInMap(charId3) != null)
						{
							GameScr.findCharInMap(charId3).perCentMp = (int)msg.reader().readByte();
						}
						break;
					}
					case -122:
					{
						Npc npc = GameScr.findNPCInMap(msg.reader().readShort());
						sbyte b35 = msg.reader().readByte();
						npc.duahau = new int[(int)b35];
						for (int num46 = 0; num46 < (int)b35; num46++)
						{
							npc.duahau[num46] = (int)msg.reader().readShort();
						}
						npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
						break;
					}
					case -121:
						Service.logMap = mSystem.currentTimeMillis() - Service.curCheckMap;
						Service.gI().sendCheckMap();
						break;
					case -120:
						Service.logController = mSystem.currentTimeMillis() - Service.curCheckController;
						Service.gI().sendCheckController();
						break;
					case -119:
						Char.myCharz().rank = msg.reader().readInt();
						break;
					case -118:
					case -114:
					case -112:
					case -109:
					case -108:
					case -107:
					case -104:
					case -99:
					case -98:
					case -97:
					case -96:
					case -95:
					case -94:
					case -93:
					case -92:
					case -91:
					case -90:
						break;
					case -117:
						GameScr.gI().tMabuEff = 0;
						GameScr.gI().percentMabu = msg.reader().readByte();
						if (GameScr.gI().percentMabu == 100)
						{
							GameScr.gI().mabuEff = true;
						}
						if (GameScr.gI().percentMabu == 101)
						{
							Npc.mabuEff = true;
						}
						break;
					case -116:
						GameScr.canAutoPlay = (msg.reader().readByte() == 1);
						break;
					case -115:
						Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
						break;
					case -113:
					{
						sbyte[] array10 = new sbyte[10];
						for (int num47 = 0; num47 < 10; num47++)
						{
							array10[num47] = msg.reader().readByte();
							Res.outz("vlue i= " + array10[num47].ToString());
						}
						GameScr.gI().onKSkill(array10);
						GameScr.gI().onOSkill(array10);
						GameScr.gI().onCSkill(array10);
						break;
					}
					case -111:
					{
						short num48 = msg.reader().readShort();
						ImageSource.vSource = new MyVector();
						for (int i = 0; i < (int)num48; i++)
						{
							string iD = msg.reader().readUTF();
							sbyte version = msg.reader().readByte();
							ImageSource.vSource.addElement(new ImageSource(iD, version));
						}
						ImageSource.checkRMS();
						ImageSource.saveRMS();
						break;
					}
					case -110:
					{
						sbyte b57 = msg.reader().readByte();
						if (b57 == 1)
						{
							int num49 = msg.reader().readInt();
							sbyte[] array11 = Rms.loadRMS(num49.ToString() + string.Empty);
							if (array11 == null)
							{
								Service.gI().sendServerData(1, -1, null);
							}
							else
							{
								Service.gI().sendServerData(1, num49, array11);
							}
						}
						if (b57 == 0)
						{
							int num50 = msg.reader().readInt();
							short num51 = msg.reader().readShort();
							sbyte[] data = new sbyte[(int)num51];
							msg.reader().read(ref data, 0, (int)num51);
							Rms.saveRMS(num50.ToString() + string.Empty, data);
						}
						break;
					}
					case -106:
					{
						short num52 = msg.reader().readShort();
						int num53 = (int)msg.reader().readShort();
						if (ItemTime.isExistItem((int)num52))
						{
							ItemTime.getItemById((int)num52).initTime(num53);
						}
						else
						{
							ItemTime o = new ItemTime(num52, num53);
							Char.vItemTime.addElement(o);
						}
						break;
					}
					case -105:
						TransportScr.gI().time = 0;
						TransportScr.gI().maxTime = msg.reader().readShort();
						TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
						TransportScr.gI().type = msg.reader().readByte();
						TransportScr.gI().switchToMe();
						break;
					case -103:
					{
						sbyte b36 = msg.reader().readByte();
						if (b36 == 0)
						{
							GameCanvas.panel.vFlag.removeAllElements();
							sbyte b37 = msg.reader().readByte();
							for (int j = 0; j < (int)b37; j++)
							{
								Item item = new Item();
								short num54 = msg.reader().readShort();
								if (num54 != -1)
								{
									item.template = ItemTemplates.get(num54);
									sbyte b38 = msg.reader().readByte();
									if (b38 != -1)
									{
										item.itemOption = new ItemOption[(int)b38];
										for (int num55 = 0; num55 < item.itemOption.Length; num55++)
										{
											int num56 = (int)msg.reader().readUnsignedByte();
											int param2 = (int)msg.reader().readUnsignedShort();
											if (num56 != -1)
											{
												item.itemOption[num55] = new ItemOption(num56, param2);
											}
										}
									}
								}
								GameCanvas.panel.vFlag.addElement(item);
							}
							GameCanvas.panel.setTypeFlag();
							GameCanvas.panel.show();
						}
						else if (b36 == 1)
						{
							int num57 = msg.reader().readInt();
							sbyte b39 = msg.reader().readByte();
							Res.outz("---------------actionFlag1:  " + num57.ToString() + " : " + b39.ToString());
							if (num57 == Char.myCharz().charID)
							{
								Char.myCharz().cFlag = b39;
							}
							else if (GameScr.findCharInMap(num57) != null)
							{
								GameScr.findCharInMap(num57).cFlag = b39;
							}
							GameScr.gI().getFlagImage(num57, b39);
						}
						else if (b36 == 2)
						{
							sbyte b40 = msg.reader().readByte();
							int num58 = (int)msg.reader().readShort();
							PKFlag pKFlag = new PKFlag();
							pKFlag.cflag = b40;
							pKFlag.IDimageFlag = num58;
							GameScr.vFlag.addElement(pKFlag);
							for (int num59 = 0; num59 < GameScr.vFlag.size(); num59++)
							{
								PKFlag pKFlag2 = (PKFlag)GameScr.vFlag.elementAt(num59);
								Res.outz(string.Concat(new string[]
								{
									"i: ",
									num59.ToString(),
									"  cflag: ",
									pKFlag2.cflag.ToString(),
									"   IDimageFlag: ",
									pKFlag2.IDimageFlag.ToString()
								}));
							}
							for (int num60 = 0; num60 < GameScr.vCharInMap.size(); num60++)
							{
								Char char5 = (Char)GameScr.vCharInMap.elementAt(num60);
								if (char5 != null && char5.cFlag == b40)
								{
									char5.flagImage = num58;
								}
							}
							if (Char.myCharz().cFlag == b40)
							{
								Char.myCharz().flagImage = num58;
							}
						}
						break;
					}
					case -102:
					{
						sbyte b41 = msg.reader().readByte();
						if (b41 != 0 && b41 == 1)
						{
							GameCanvas.loginScr.isLogin2 = false;
							Service.gI().login(Rms.loadRMSString("acc6"), Rms.loadRMSString("pass6"), GameMidlet.VERSION, 0);
							LoginScr.isLoggingIn = true;
						}
						break;
					}
					case -101:
					{
						GameCanvas.loginScr.isLogin2 = true;
						GameCanvas.connect();
						string text5 = msg.reader().readUTF();
						Rms.saveRMSString("userAo6" + ServerListScreen.ipSelect.ToString(), text5);
						Service.gI().setClientType();
						Service.gI().login(text5, string.Empty, GameMidlet.VERSION, 1);
						break;
					}
					case -100:
					{
						InfoDlg.hide();
						bool flag = false;
						if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
						{
							flag = true;
						}
						sbyte b42 = msg.reader().readByte();
						Res.outz("t Indxe= " + b42.ToString());
						GameCanvas.panel.maxPageShop[(int)b42] = (int)msg.reader().readByte();
						GameCanvas.panel.currPageShop[(int)b42] = (int)msg.reader().readByte();
						Res.outz("max page= " + GameCanvas.panel.maxPageShop[(int)b42].ToString() + " curr page= " + GameCanvas.panel.currPageShop[(int)b42].ToString());
						int num61 = (int)msg.reader().readUnsignedByte();
						Char.myCharz().arrItemShop[(int)b42] = new Item[num61];
						for (int k = 0; k < num61; k++)
						{
							short num62 = msg.reader().readShort();
							if (num62 != -1)
							{
								Res.outz("template id= " + num62.ToString());
								Char.myCharz().arrItemShop[(int)b42][k] = new Item();
								Char.myCharz().arrItemShop[(int)b42][k].template = ItemTemplates.get(num62);
								Char.myCharz().arrItemShop[(int)b42][k].itemId = (int)msg.reader().readShort();
								Char.myCharz().arrItemShop[(int)b42][k].buyCoin = msg.reader().readInt();
								Char.myCharz().arrItemShop[(int)b42][k].buyGold = msg.reader().readInt();
								Char.myCharz().arrItemShop[(int)b42][k].buyType = msg.reader().readByte();
								Char.myCharz().arrItemShop[(int)b42][k].quantity = msg.reader().readInt();
								Char.myCharz().arrItemShop[(int)b42][k].isMe = msg.reader().readByte();
								Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
								sbyte b43 = msg.reader().readByte();
								if (b43 != -1)
								{
									Char.myCharz().arrItemShop[(int)b42][k].itemOption = new ItemOption[(int)b43];
									for (int l = 0; l < Char.myCharz().arrItemShop[(int)b42][k].itemOption.Length; l++)
									{
										int num63 = (int)msg.reader().readUnsignedByte();
										int param3 = (int)msg.reader().readUnsignedShort();
										if (num63 != -1)
										{
											Char.myCharz().arrItemShop[(int)b42][k].itemOption[l] = new ItemOption(num63, param3);
											Char.myCharz().arrItemShop[(int)b42][k].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[(int)b42][k]);
										}
									}
								}
								if (msg.reader().readByte() == 1)
								{
									int headTemp = (int)msg.reader().readShort();
									int bodyTemp = (int)msg.reader().readShort();
									int legTemp = (int)msg.reader().readShort();
									int bagTemp = (int)msg.reader().readShort();
									Char.myCharz().arrItemShop[(int)b42][k].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
								}
							}
						}
						if (flag)
						{
							GameCanvas.panel2.setTabKiGui();
						}
						GameCanvas.panel.setTabShop();
						GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
						break;
					}
					case -89:
						GameCanvas.open3Hour = (msg.reader().readByte() == 1);
						break;
					default:
						if (command != 31)
						{
							if (command == 42)
							{
								GameCanvas.endDlg();
								LoginScr.isContinueToLogin = false;
								Char.isLoadingMap = false;
								msg.reader().readByte();
								if (GameCanvas.registerScr == null)
								{
									GameCanvas.registerScr = new RegisterScreen();
								}
								GameCanvas.registerScr.switchToMe();
							}
						}
						else
						{
							int num64 = msg.reader().readInt();
							if (msg.reader().readByte() == 1)
							{
								short smallID = msg.reader().readShort();
								sbyte b44 = -1;
								int[] array12 = null;
								short wimg = 0;
								short himg = 0;
								try
								{
									b44 = msg.reader().readByte();
									if (b44 > 0)
									{
										sbyte b45 = msg.reader().readByte();
										array12 = new int[(int)b45];
										for (int m = 0; m < (int)b45; m++)
										{
											array12[m] = (int)msg.reader().readByte();
										}
										wimg = msg.reader().readShort();
										himg = msg.reader().readShort();
									}
								}
								catch (Exception)
								{
								}
								if (num64 == Char.myCharz().charID)
								{
									Char.myCharz().petFollow = new PetFollow();
									Char.myCharz().petFollow.smallID = smallID;
									if (b44 > 0)
									{
										Char.myCharz().petFollow.SetImg((int)b44, array12, (int)wimg, (int)himg);
									}
								}
								else
								{
									Char char6 = GameScr.findCharInMap(num64);
									char6.petFollow = new PetFollow();
									char6.petFollow.smallID = smallID;
									if (b44 > 0)
									{
										char6.petFollow.SetImg((int)b44, array12, (int)wimg, (int)himg);
									}
								}
							}
							else if (num64 == Char.myCharz().charID)
							{
								Char.myCharz().petFollow.remove();
								Char.myCharz().petFollow = null;
							}
							else
							{
								Char char9 = GameScr.findCharInMap(num64);
								char9.petFollow.remove();
								char9.petFollow = null;
							}
						}
						break;
					}
				}
				else if (command <= 93)
				{
					switch (command)
					{
					case 48:
						ServerListScreen.ipSelect = (int)msg.reader().readByte();
						GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
						Session_ME.gI().close();
						GameCanvas.endDlg();
						ServerListScreen.waitToLogin = true;
						break;
					case 49:
					case 50:
						break;
					case 51:
					{
						Mabu mabu2 = (Mabu)GameScr.findCharInMap(msg.reader().readInt());
						sbyte id2 = msg.reader().readByte();
						short x = msg.reader().readShort();
						short y = msg.reader().readShort();
						sbyte b46 = msg.reader().readByte();
						Char[] array13 = new Char[(int)b46];
						int[] array14 = new int[(int)b46];
						for (int n = 0; n < (int)b46; n++)
						{
							int num65 = msg.reader().readInt();
							Res.outz("char ID=" + num65.ToString());
							array13[n] = null;
							if (num65 != Char.myCharz().charID)
							{
								array13[n] = GameScr.findCharInMap(num65);
							}
							else
							{
								array13[n] = Char.myCharz();
							}
							array14[n] = msg.reader().readInt();
						}
						mabu2.setSkill(id2, x, y, array13, array14);
						break;
					}
					case 52:
					{
						sbyte b58 = msg.reader().readByte();
						if (b58 == 1)
						{
							int num66 = msg.reader().readInt();
							if (num66 == Char.myCharz().charID)
							{
								Char.myCharz().setMabuHold(true);
								Char.myCharz().cx = (int)msg.reader().readShort();
								Char.myCharz().cy = (int)msg.reader().readShort();
							}
							else
							{
								Char char7 = GameScr.findCharInMap(num66);
								if (char7 != null)
								{
									char7.setMabuHold(true);
									char7.cx = (int)msg.reader().readShort();
									char7.cy = (int)msg.reader().readShort();
								}
							}
						}
						if (b58 == 0)
						{
							int num67 = msg.reader().readInt();
							if (num67 == Char.myCharz().charID)
							{
								Char.myCharz().setMabuHold(false);
							}
							else
							{
								Char char10 = GameScr.findCharInMap(num67);
								if (char10 != null)
								{
									char10.setMabuHold(false);
								}
							}
						}
						if (b58 == 2)
						{
							int charId4 = msg.reader().readInt();
							int id3 = msg.reader().readInt();
							((Mabu)GameScr.findCharInMap(charId4)).eat(id3);
						}
						if (b58 == 3)
						{
							GameScr.mabuPercent = msg.reader().readByte();
						}
						break;
					}
					default:
						if (command == 93)
						{
							string str = msg.reader().readUTF();
							str = Res.changeString(str);
							GameScr.gI().chatVip(str);
						}
						break;
					}
				}
				else
				{
					switch (command)
					{
					case 98:
					{
						string str2 = msg.reader().readUTF();
						ModFunc.GI().AddNotifTichXanh(str2);
						break;
					}
					case 99:
						break;
					case 100:
					{
						sbyte b59 = msg.reader().readByte();
						sbyte b47 = msg.reader().readByte();
						Item item2 = null;
						if (b59 == 0)
						{
							item2 = Char.myCharz().arrItemBody[(int)b47];
						}
						if (b59 == 1)
						{
							item2 = Char.myCharz().arrItemBag[(int)b47];
						}
						short num68 = msg.reader().readShort();
						if (num68 != -1)
						{
							item2.template = ItemTemplates.get(num68);
							item2.quantity = msg.reader().readInt();
							item2.info = msg.reader().readUTF();
							item2.content = msg.reader().readUTF();
							sbyte b48 = msg.reader().readByte();
							if (b48 != 0)
							{
								item2.itemOption = new ItemOption[(int)b48];
								for (int num69 = 0; num69 < item2.itemOption.Length; num69++)
								{
									int num70 = (int)msg.reader().readUnsignedByte();
									Res.outz("id o= " + num70.ToString());
									int param4 = (int)msg.reader().readUnsignedShort();
									if (num70 != -1)
									{
										item2.itemOption[num69] = new ItemOption(num70, param4);
									}
								}
							}
						}
						break;
					}
					case 101:
					{
						Res.outz("big boss--------------------------------------------------");
						BigBoss bigBoss2 = Mob.getBigBoss();
						if (bigBoss2 != null)
						{
							sbyte b49 = msg.reader().readByte();
							if (b49 == 0 || b49 == 1 || b49 == 2 || b49 == 4 || b49 == 3)
							{
								if (b49 == 3)
								{
									bigBoss2.xTo = (bigBoss2.xFirst = (int)msg.reader().readShort());
									bigBoss2.yTo = (bigBoss2.yFirst = (int)msg.reader().readShort());
									bigBoss2.setFly();
								}
								else
								{
									sbyte b50 = msg.reader().readByte();
									Res.outz("CHUONG nChar= " + b50.ToString());
									Char[] array15 = new Char[(int)b50];
									int[] array16 = new int[(int)b50];
									for (int num71 = 0; num71 < (int)b50; num71++)
									{
										int num72 = msg.reader().readInt();
										Res.outz("char ID=" + num72.ToString());
										array15[num71] = null;
										if (num72 != Char.myCharz().charID)
										{
											array15[num71] = GameScr.findCharInMap(num72);
										}
										else
										{
											array15[num71] = Char.myCharz();
										}
										array16[num71] = msg.reader().readInt();
									}
									bigBoss2.setAttack(array15, array16, b49);
								}
							}
							if (b49 == 5)
							{
								bigBoss2.haftBody = true;
								bigBoss2.status = 2;
							}
							if (b49 == 6)
							{
								bigBoss2.getDataB2();
								bigBoss2.x = (int)msg.reader().readShort();
								bigBoss2.y = (int)msg.reader().readShort();
							}
							if (b49 == 7)
							{
								bigBoss2.setAttack(null, null, b49);
							}
							if (b49 == 8)
							{
								bigBoss2.xTo = (bigBoss2.xFirst = (int)msg.reader().readShort());
								bigBoss2.yTo = (bigBoss2.yFirst = (int)msg.reader().readShort());
								bigBoss2.status = 2;
							}
							if (b49 == 9)
							{
								bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
							}
						}
						break;
					}
					case 102:
					{
						sbyte b51 = msg.reader().readByte();
						if (b51 == 0 || b51 == 1 || b51 == 2 || b51 == 6)
						{
							BigBoss2 bigBoss3 = Mob.getBigBoss2();
							if (bigBoss3 == null)
							{
								break;
							}
							if (b51 == 6)
							{
								bigBoss3.x = (bigBoss3.y = (bigBoss3.xTo = (bigBoss3.yTo = (bigBoss3.xFirst = (bigBoss3.yFirst = -1000)))));
								break;
							}
							sbyte b52 = msg.reader().readByte();
							Char[] array17 = new Char[(int)b52];
							int[] array18 = new int[(int)b52];
							for (int num73 = 0; num73 < (int)b52; num73++)
							{
								int num74 = msg.reader().readInt();
								array17[num73] = null;
								if (num74 != Char.myCharz().charID)
								{
									array17[num73] = GameScr.findCharInMap(num74);
								}
								else
								{
									array17[num73] = Char.myCharz();
								}
								array18[num73] = msg.reader().readInt();
							}
							bigBoss3.setAttack(array17, array18, b51);
						}
						if (b51 == 3 || b51 == 4 || b51 == 5 || b51 == 7)
						{
							BachTuoc bachTuoc = Mob.getBachTuoc();
							if (bachTuoc == null)
							{
								break;
							}
							if (b51 == 7)
							{
								bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
								break;
							}
							if (b51 == 3 || b51 == 4)
							{
								sbyte b53 = msg.reader().readByte();
								Char[] array19 = new Char[(int)b53];
								int[] array20 = new int[(int)b53];
								for (int num75 = 0; num75 < (int)b53; num75++)
								{
									int num76 = msg.reader().readInt();
									array19[num75] = null;
									if (num76 != Char.myCharz().charID)
									{
										array19[num75] = GameScr.findCharInMap(num76);
									}
									else
									{
										array19[num75] = Char.myCharz();
									}
									array20[num75] = msg.reader().readInt();
								}
								bachTuoc.setAttack(array19, array20, b51);
							}
							if (b51 == 5)
							{
								short xMoveTo = msg.reader().readShort();
								bachTuoc.move(xMoveTo);
							}
						}
						if (b51 > 9 && b51 < 30)
						{
							Controller2.readActionBoss(msg, (int)b51);
						}
						break;
					}
					default:
					{
						switch (command)
						{
						case 113:
							break;
						case 114:
							try
							{
								msg.reader().readUTF();
								mSystem.curINAPP = msg.reader().readByte();
								mSystem.maxINAPP = msg.reader().readByte();
								goto IL_1E81;
							}
							catch (Exception)
							{
								goto IL_1E81;
							}
							break;
						case 115:
						case 116:
						case 117:
						case 118:
						case 119:
						case 120:
						case 126:
							goto IL_1E81;
						case 121:
							mSystem.publicID = msg.reader().readUTF();
							mSystem.strAdmob = msg.reader().readUTF();
							Res.outz("SHOW AD public ID= " + mSystem.publicID);
							mSystem.createAdmob();
							goto IL_1E81;
						case 122:
						{
							short num77 = msg.reader().readShort();
							Res.outz("second login = " + num77.ToString());
							LoginScr.timeLogin = num77;
							LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
							GameCanvas.endDlg();
							goto IL_1E81;
						}
						case 123:
						{
							Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
							int num78 = msg.reader().readInt();
							short xPos = msg.reader().readShort();
							short yPos = msg.reader().readShort();
							sbyte b54 = msg.reader().readByte();
							Char char8 = null;
							if (num78 == Char.myCharz().charID)
							{
								char8 = Char.myCharz();
							}
							else if (GameScr.findCharInMap(num78) != null)
							{
								char8 = GameScr.findCharInMap(num78);
							}
							if (char8 != null)
							{
								ServerEffect.addServerEffect((b54 != 0) ? 173 : 60, char8, 1);
								char8.setPos(xPos, yPos, b54);
								goto IL_1E81;
							}
							goto IL_1E81;
						}
						case 124:
						{
							short num79 = msg.reader().readShort();
							string text6 = msg.reader().readUTF();
							Res.outz("noi chuyen = " + text6 + "npc ID= " + num79.ToString());
							Npc npc2 = GameScr.findNPCInMap(num79);
							if (npc2 == null)
							{
								goto IL_1E81;
							}
							npc2.addInfo(text6);
							goto IL_1E81;
						}
						case 125:
						{
							sbyte fusion = msg.reader().readByte();
							int num80 = msg.reader().readInt();
							if (num80 == Char.myCharz().charID)
							{
								Char.myCharz().setFusion(fusion);
								goto IL_1E81;
							}
							if (GameScr.findCharInMap(num80) != null)
							{
								GameScr.findCharInMap(num80).setFusion(fusion);
								goto IL_1E81;
							}
							goto IL_1E81;
						}
						case 127:
							Controller2.readInfoRada(msg);
							goto IL_1E81;
						default:
							goto IL_1E81;
						}
						int loop = 0;
						int layer = 0;
						int id4 = 0;
						short x2 = 0;
						short y2 = 0;
						short loopCount = -1;
						try
						{
							loop = (int)msg.reader().readByte();
							layer = (int)msg.reader().readByte();
							id4 = (int)msg.reader().readUnsignedByte();
							x2 = msg.reader().readShort();
							y2 = msg.reader().readShort();
							loopCount = msg.reader().readShort();
						}
						catch (Exception)
						{
						}
						EffecMn.addEff(new Effect(id4, (int)x2, (int)y2, layer, loop, (int)loopCount));
						break;
					}
					}
				}
				IL_1E81:;
			}
			catch (Exception ex4)
			{
				Res.outz("=====> Controller2 " + ex4.StackTrace);
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00096C0C File Offset: 0x00094E0C
		private static void readLuckyRound(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					sbyte b2 = msg.reader().readByte();
					short[] array = new short[(int)b2];
					for (int i = 0; i < (int)b2; i++)
					{
						array[i] = msg.reader().readShort();
					}
					sbyte b3 = msg.reader().readByte();
					int price = msg.reader().readInt();
					short idTicket = msg.reader().readShort();
					CrackBallScr.gI().SetCrackBallScr(array, (byte)b3, price, idTicket);
				}
				else if (b == 1)
				{
					sbyte b4 = msg.reader().readByte();
					short[] array2 = new short[(int)b4];
					for (int j = 0; j < (int)b4; j++)
					{
						array2[j] = msg.reader().readShort();
					}
					CrackBallScr.gI().DoneCrackBallScr(array2);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00096CF0 File Offset: 0x00094EF0
		private static void readInfoRada(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					RadarScr.gI();
					MyVector myVector = new MyVector(string.Empty);
					short num = msg.reader().readShort();
					int num2 = 0;
					for (int i = 0; i < (int)num; i++)
					{
						Info_RadaScr info_RadaScr = new Info_RadaScr();
						int id = (int)msg.reader().readShort();
						int no = i + 1;
						int idIcon = (int)msg.reader().readShort();
						sbyte rank = msg.reader().readByte();
						sbyte amount = msg.reader().readByte();
						sbyte max_amount = msg.reader().readByte();
						short templateId = -1;
						Char charInfo = null;
						sbyte b2 = msg.reader().readByte();
						if (b2 == 0)
						{
							templateId = msg.reader().readShort();
						}
						else
						{
							int head = (int)msg.reader().readShort();
							int body = (int)msg.reader().readShort();
							int leg = (int)msg.reader().readShort();
							int bag = (int)msg.reader().readShort();
							charInfo = Info_RadaScr.SetCharInfo(head, body, leg, bag);
						}
						string name = msg.reader().readUTF();
						string info = msg.reader().readUTF();
						sbyte b3 = msg.reader().readByte();
						sbyte use = msg.reader().readByte();
						sbyte b4 = msg.reader().readByte();
						ItemOption[] array = null;
						if (b4 != 0)
						{
							array = new ItemOption[(int)b4];
							for (int j = 0; j < array.Length; j++)
							{
								int num3 = (int)msg.reader().readUnsignedByte();
								int param = (int)msg.reader().readUnsignedShort();
								sbyte activeCard = msg.reader().readByte();
								if (num3 != -1)
								{
									array[j] = new ItemOption(num3, param);
									array[j].activeCard = activeCard;
								}
							}
						}
						info_RadaScr.SetInfo(id, no, idIcon, rank, b2, templateId, name, info, charInfo, array);
						info_RadaScr.SetLevel(b3);
						info_RadaScr.SetUse(use);
						info_RadaScr.SetAmount(amount, max_amount);
						myVector.addElement(info_RadaScr);
						if (b3 > 0)
						{
							num2++;
						}
					}
					RadarScr.gI().SetRadarScr(myVector, num2, (int)num);
					RadarScr.gI().switchToMe();
				}
				else if (b == 1)
				{
					int id2 = (int)msg.reader().readShort();
					sbyte use2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id2) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id2).SetUse(use2);
					}
					RadarScr.SetListUse();
				}
				else if (b == 2)
				{
					int num4 = (int)msg.reader().readShort();
					sbyte level = msg.reader().readByte();
					int num5 = 0;
					for (int k = 0; k < RadarScr.list.size(); k++)
					{
						Info_RadaScr info_RadaScr2 = (Info_RadaScr)RadarScr.list.elementAt(k);
						if (info_RadaScr2 != null)
						{
							if (info_RadaScr2.id == num4)
							{
								info_RadaScr2.SetLevel(level);
							}
							if (info_RadaScr2.level > 0)
							{
								num5++;
							}
						}
					}
					RadarScr.SetNum(num5, RadarScr.list.size());
					if (Info_RadaScr.GetInfo(RadarScr.listUse, num4) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, num4).SetLevel(level);
					}
				}
				else if (b == 3)
				{
					int id3 = (int)msg.reader().readShort();
					sbyte amount2 = msg.reader().readByte();
					sbyte max_amount2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id3) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id3).SetAmount(amount2, max_amount2);
					}
					if (Info_RadaScr.GetInfo(RadarScr.listUse, id3) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, id3).SetAmount(amount2, max_amount2);
					}
				}
				else if (b == 4)
				{
					int num6 = msg.reader().readInt();
					short idAuraEff = msg.reader().readShort();
					Char @char = (num6 != Char.myCharz().charID) ? GameScr.findCharInMap(num6) : Char.myCharz();
					if (@char != null)
					{
						@char.idAuraEff = idAuraEff;
						@char.idEff_Set_Item = (short)msg.reader().readByte();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000970F4 File Offset: 0x000952F4
		private static void readInfoEffChar(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				int num = msg.reader().readInt();
				Char @char = (num != Char.myCharz().charID) ? GameScr.findCharInMap(num) : Char.myCharz();
				if (b == 0)
				{
					int id = (int)msg.reader().readShort();
					int layer = (int)msg.reader().readByte();
					int loop = (int)msg.reader().readByte();
					short loopCount = msg.reader().readShort();
					sbyte isStand = msg.reader().readByte();
					if (@char != null)
					{
						@char.addEffChar(new Effect(id, @char, layer, loop, (int)loopCount, isStand));
					}
				}
				else if (b == 1)
				{
					int id2 = (int)msg.reader().readShort();
					if (@char != null)
					{
						@char.removeEffChar(0, id2);
					}
				}
				else if (b == 2 && @char != null)
				{
					@char.removeEffChar(-1, 0);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000971D8 File Offset: 0x000953D8
		private static void readActionBoss(Message msg, int actionBoss)
		{
			try
			{
				NewBoss newBoss = Mob.getNewBoss(msg.reader().readByte());
				if (newBoss != null)
				{
					if (actionBoss == 10)
					{
						short xMoveTo = msg.reader().readShort();
						short yMoveTo = msg.reader().readShort();
						newBoss.move(xMoveTo, yMoveTo);
					}
					if (actionBoss >= 11 && actionBoss <= 20)
					{
						sbyte b = msg.reader().readByte();
						Char[] array = new Char[(int)b];
						int[] array2 = new int[(int)b];
						for (int i = 0; i < (int)b; i++)
						{
							int num = msg.reader().readInt();
							array[i] = null;
							if (num != Char.myCharz().charID)
							{
								array[i] = GameScr.findCharInMap(num);
							}
							else
							{
								array[i] = Char.myCharz();
							}
							array2[i] = msg.reader().readInt();
						}
						sbyte dir = msg.reader().readByte();
						newBoss.setAttack(array, array2, (sbyte)(actionBoss - 10), dir);
					}
					if (actionBoss == 21)
					{
						newBoss.xTo = (int)msg.reader().readShort();
						newBoss.yTo = (int)msg.reader().readShort();
						newBoss.setFly();
					}
					if (actionBoss == 23)
					{
						newBoss.setDie();
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
