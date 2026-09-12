using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x020001D7 RID: 471
	public class CreateCharScr : mScreen, IActionListener
	{
		// Token: 0x06001500 RID: 5376 RVA: 0x00153018 File Offset: 0x00151218
		public CreateCharScr()
		{
			try
			{
				if (!GameCanvas.lowGraphic)
				{
					CreateCharScr.loadMapFromResource(new sbyte[]
					{
						39,
						40,
						41
					});
				}
				this.loadMapTableFromResource(new sbyte[]
				{
					39,
					40,
					41
				});
			}
			catch (Exception)
			{
			}
			this.xPopup = (GameCanvas.w - 160) / 2;
			this.yPopup = (GameCanvas.h - 160) / 2;
			this.cx = GameCanvas.w / 2;
			this.cy = this.yPopup + 70;
			if (GameCanvas.w <= 200)
			{
				GameScr.setPopupSize(128, 100);
				GameScr.popupX = (GameCanvas.w - 128) / 2;
				GameScr.popupY = 10;
				this.cy += 15;
				this.dy -= 15;
			}
			CreateCharScr.tAddName = new TField
			{
				width = ((GameCanvas.w < 200) ? 60 : 160),
				height = mScreen.ITEM_HEIGHT + 2,
				strInfo = mResources.char_name,
				showSubTextField = true,
				name = mResources.char_name,
				x = this.xPopup,
				y = this.yPopup - 30,
				isFocus = !GameCanvas.isTouch
			};
			CreateCharScr.tAddName.setIputType(TField.INPUT_TYPE_ANY);
			if (CreateCharScr.tAddName.getText().Equals("@"))
			{
				CreateCharScr.tAddName.setText(GameCanvas.loginScr.tfUser.getText().Substring(0, GameCanvas.loginScr.tfUser.getText().IndexOf("@")));
			}
			CreateCharScr.indexGender = 1;
			CreateCharScr.indexHair = 0;
			this.center = new Command(mResources.NEWCHAR, this, 8000, null);
			this.left = new Command(mResources.BACK, this, 8001, null);
			if (!GameCanvas.isTouch)
			{
				this.right = CreateCharScr.tAddName.cmdClear;
			}
			this.yBegin = CreateCharScr.tAddName.y;
			this.mFontGender = mFont.tahoma_7b_dark;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00153250 File Offset: 0x00151450
		public static CreateCharScr gI()
		{
			if (CreateCharScr.instance == null)
			{
				CreateCharScr.instance = new CreateCharScr();
			}
			return CreateCharScr.instance;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00153268 File Offset: 0x00151468
		public static void loadMapFromResource(sbyte[] mapID)
		{
			for (int i = 0; i < mapID.Length; i++)
			{
				DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID[i].ToString());
				MapTemplate.tmw[i] = (int)((ushort)dataInputStream.read());
				MapTemplate.tmh[i] = (int)((ushort)dataInputStream.read());
				MapTemplate.maps[i] = new int[dataInputStream.available()];
				for (int j = 0; j < MapTemplate.tmw[i] * MapTemplate.tmh[i]; j++)
				{
					MapTemplate.maps[i][j] = dataInputStream.read();
				}
				MapTemplate.types[i] = new int[MapTemplate.maps[i].Length];
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00153314 File Offset: 0x00151514
		public void loadMapTableFromResource(sbyte[] mapID)
		{
			if (GameCanvas.lowGraphic)
			{
				return;
			}
			try
			{
				for (int i = 0; i < mapID.Length; i++)
				{
					DataInputStream dataInputStream = MyStream.readFile("/mymap/mapTable" + mapID[i].ToString());
					short num = dataInputStream.readShort();
					MapTemplate.vCurrItem[i] = new MyVector();
					for (int j = 0; j < (int)num; j++)
					{
						short id = dataInputStream.readShort();
						short num2 = dataInputStream.readShort();
						short num3 = dataInputStream.readShort();
						if (TileMap.getBIById((int)id) != null)
						{
							BgItem bIById = TileMap.getBIById((int)id);
							BgItem bgItem = new BgItem();
							bgItem.id = (int)id;
							bgItem.idImage = bIById.idImage;
							bgItem.dx = bIById.dx;
							bgItem.dy = bIById.dy;
							bgItem.x = (int)(num2 * (short)TileMap.size);
							bgItem.y = (int)(num3 * (short)TileMap.size);
							bgItem.layer = bIById.layer;
							MapTemplate.vCurrItem[i].addElement(bgItem);
							if (!BgItem.imgNew.containsKey(bgItem.idImage.ToString() + string.Empty))
							{
								try
								{
									Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
									if (image == null)
									{
										BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									else
									{
										BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
									}
								}
								catch (Exception)
								{
									Image image2 = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
									if (image2 == null)
									{
										image2 = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image2);
								}
								BgItem.vKeysLast.addElement(bgItem.idImage.ToString() + string.Empty);
							}
							if (!BgItem.isExistKeyNews(bgItem.idImage.ToString() + string.Empty))
							{
								BgItem.vKeysNew.addElement(bgItem.idImage.ToString() + string.Empty);
							}
							bgItem.changeColor();
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x001535DC File Offset: 0x001517DC
		public override void switchToMe()
		{
			LoginScr.isContinueToLogin = false;
			GameCanvas.menu.showMenu = false;
			GameCanvas.endDlg();
			GameCanvas.loadBG(1);
			base.switchToMe();
			CreateCharScr.indexGender = Res.random(0, 3);
			CreateCharScr.indexHair = Res.random(0, 3);
			Char.isLoadingMap = false;
			ServerListScreen.countDieConnect = 0;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0015362F File Offset: 0x0015182F
		public override void keyPress(int keyCode)
		{
			CreateCharScr.tAddName.keyPressed(keyCode);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00153640 File Offset: 0x00151840
		public override void update()
		{
			this.cp1++;
			if (this.cp1 > 30)
			{
				this.cp1 = 0;
			}
			if (this.cp1 % 15 < 5)
			{
				this.cf = 0;
			}
			else
			{
				this.cf = 1;
			}
			CreateCharScr.tAddName.update();
			CreateCharScr.tAddName.isFocus = (CreateCharScr.selected == 0);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x001536A4 File Offset: 0x001518A4
		public override void updateKey()
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				CreateCharScr.selected--;
				if (CreateCharScr.selected < 0)
				{
					CreateCharScr.selected = 2;
				}
			}
			else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
			{
				CreateCharScr.selected++;
				if (CreateCharScr.selected > 2)
				{
					CreateCharScr.selected = 0;
				}
			}
			if (CreateCharScr.selected == 0)
			{
				if (!GameCanvas.isTouch)
				{
					this.right = CreateCharScr.tAddName.cmdClear;
				}
				CreateCharScr.tAddName.update();
				this.mFontGender = mFont.tahoma_7b_dark;
			}
			else if (CreateCharScr.selected == 1)
			{
				if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
				{
					CreateCharScr.indexGender--;
					if (CreateCharScr.indexGender < 0)
					{
						CreateCharScr.indexGender = mResources.MENUGENDER.Length - 1;
					}
				}
				if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
				{
					CreateCharScr.indexGender++;
					if (CreateCharScr.indexGender > mResources.MENUGENDER.Length - 1)
					{
						CreateCharScr.indexGender = 0;
					}
				}
				this.right = null;
				this.mFontGender = mFont.tahoma_7b_blue;
			}
			else if (CreateCharScr.selected == 2)
			{
				if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
				{
					CreateCharScr.indexHair--;
					if (CreateCharScr.indexHair < 0)
					{
						CreateCharScr.indexHair = mResources.hairStyleName[0].Length - 1;
					}
				}
				if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
				{
					CreateCharScr.indexHair++;
					if (CreateCharScr.indexHair > mResources.hairStyleName[0].Length - 1)
					{
						CreateCharScr.indexHair = 0;
					}
				}
				this.right = null;
				this.mFontGender = mFont.tahoma_7b_dark;
			}
			if (GameCanvas.isPointerJustRelease)
			{
				int textWidth = mFont.tahoma_7b_dark.getWidth(mResources.MENUGENDER[CreateCharScr.indexGender]);
				int textHeight = mFont.tahoma_7b_dark.getHeight();
				if (GameCanvas.isPointerHoldIn(CreateCharScr.tAddName.x, CreateCharScr.tAddName.y, CreateCharScr.tAddName.width, CreateCharScr.tAddName.height))
				{
					CreateCharScr.selected = 0;
					this.mFontGender = mFont.tahoma_7b_dark;
				}
				else if (GameCanvas.isPointerHoldIn((GameCanvas.w - textWidth) / 2 - 10, this.yPopup + 20, textWidth + 10, textHeight + 10))
				{
					this.mFontGender = mFont.tahoma_7b_blue;
					CreateCharScr.selected = 1;
				}
				else if (GameCanvas.isPointerHoldIn(this.xPopup + 40, this.yPopup + 80, 80, 80))
				{
					CreateCharScr.selected = 2;
					this.mFontGender = mFont.tahoma_7b_dark;
				}
				else if (GameCanvas.isPointerHoldIn(this.xPopup + 10, this.yPopup + 25, 23, 26))
				{
					CreateCharScr.selected = 1;
					CreateCharScr.indexGender--;
					if (CreateCharScr.indexGender < 0)
					{
						CreateCharScr.indexGender = 2;
					}
				}
				else if (GameCanvas.isPointerHoldIn(this.xPopup + 130, this.yPopup + 25, 23, 26))
				{
					CreateCharScr.selected = 1;
					CreateCharScr.indexGender++;
					if (CreateCharScr.indexGender > 2)
					{
						CreateCharScr.indexGender = 0;
					}
				}
				else if (GameCanvas.isPointerHoldIn(this.xPopup + 10, this.cy + 20, 23, 26))
				{
					CreateCharScr.selected = 2;
					CreateCharScr.indexHair--;
					if (CreateCharScr.indexHair < 0)
					{
						CreateCharScr.indexHair = 2;
					}
				}
				else if (GameCanvas.isPointerHoldIn(this.xPopup + 130, this.cy + 20, 23, 26))
				{
					CreateCharScr.selected = 2;
					CreateCharScr.indexHair++;
					if (CreateCharScr.indexHair > 2)
					{
						CreateCharScr.indexHair = 0;
					}
				}
			}
			if (!TouchScreenKeyboard.visible)
			{
				base.updateKey();
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00153A6C File Offset: 0x00151C6C
		public override void paint(mGraphics g)
		{
			if (Char.isLoadingMap)
			{
				return;
			}
			GameCanvas.paintBGGameScr(g);
			int num = 30;
			if (GameCanvas.w == 128)
			{
				num = 20;
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			if (GameCanvas.currentDialog == null)
			{
				PopUp.paintPopUp(g, this.xPopup - 20, this.yPopup - 60, 200, 220, -1, true);
				this.mFontGender.drawString(g, mResources.MENUGENDER[CreateCharScr.indexGender], GameCanvas.w / 2, this.yPopup + 30, mFont.CENTER);
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 3, this.xPopup + 20, this.yPopup + 35, StaticObj.VCENTER_HCENTER);
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, this.xPopup + 140, this.yPopup + 35, StaticObj.VCENTER_HCENTER);
				int num2 = CreateCharScr.hairID[CreateCharScr.indexGender][CreateCharScr.indexHair];
				int num3 = CreateCharScr.defaultLeg[CreateCharScr.indexGender];
				int num4 = CreateCharScr.defaultBody[CreateCharScr.indexGender];
				g.drawImage(TileMap.bong, this.cx, this.cy + this.dy, 3);
				Part part3 = null;
				try
				{
					Part part4 = GameScr.parts[num2];
					Part part5 = GameScr.parts[num3];
					part3 = GameScr.parts[num4];
					SmallImage.drawSmallImage(g, (int)part4.pi[Char.CharInfo[this.cf][0][0]].id, this.cx + Char.CharInfo[this.cf][0][1] + (int)part4.pi[Char.CharInfo[this.cf][0][0]].dx, this.cy - Char.CharInfo[this.cf][0][2] + (int)part4.pi[Char.CharInfo[this.cf][0][0]].dy + this.dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part5.pi[Char.CharInfo[this.cf][1][0]].id, this.cx + Char.CharInfo[this.cf][1][1] + (int)part5.pi[Char.CharInfo[this.cf][1][0]].dx, this.cy - Char.CharInfo[this.cf][1][2] + (int)part5.pi[Char.CharInfo[this.cf][1][0]].dy + this.dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part3.pi[Char.CharInfo[this.cf][2][0]].id, this.cx + Char.CharInfo[this.cf][2][1] + (int)part3.pi[Char.CharInfo[this.cf][2][0]].dx, this.cy - Char.CharInfo[this.cf][2][2] + (int)part3.pi[Char.CharInfo[this.cf][2][0]].dy + this.dy, 0, 0);
				}
				catch (Exception ex)
				{
					ModFunc.WriteLog("Error at paint CreateChar: " + ex.Message + " --- " + ex.StackTrace);
				}
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 3, this.xPopup + 20, this.cy + 30, StaticObj.VCENTER_HCENTER);
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, this.xPopup + 140, this.cy + 30, StaticObj.VCENTER_HCENTER);
				if (!GameCanvas.lowGraphic)
				{
					for (int i = 0; i < MapTemplate.vCurrItem[CreateCharScr.indexGender].size(); i++)
					{
						BgItem bgItem2 = (BgItem)MapTemplate.vCurrItem[CreateCharScr.indexGender].elementAt(i);
						if (bgItem2.idImage != -1 && bgItem2.layer == 3)
						{
							bgItem2.paint(g);
						}
					}
				}
				if (!Main.isPC)
				{
					if (mGraphics.addYWhenOpenKeyBoard != 0)
					{
						this.yButton = 110;
						this.disY = 60;
						if (GameCanvas.w > GameCanvas.h)
						{
							this.yButton = GameScr.popupY + 30 + 3 * num + (int)part3.pi[Char.CharInfo[0][2][0]].dy + this.dy - 15;
							this.disY = 35;
						}
					}
					else
					{
						this.yButton = 110;
						this.disY = 60;
						if (GameCanvas.w > GameCanvas.h)
						{
							this.yButton = 100;
							this.disY = 45;
						}
					}
					CreateCharScr.tAddName.y = this.yButton - CreateCharScr.tAddName.height - this.disY + 5;
				}
				else
				{
					this.yButton = 110;
					this.disY = 60;
					if (GameCanvas.w > GameCanvas.h)
					{
						this.yButton = 100;
						this.disY = 45;
					}
					CreateCharScr.tAddName.y = this.yBegin;
				}
				CreateCharScr.tAddName.paint(g);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
			if (!TouchScreenKeyboard.visible)
			{
				base.paint(g);
			}
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00153FA0 File Offset: 0x001521A0
		public void perform(int idAction, object p)
		{
			if (idAction <= 8001)
			{
				if (idAction != 8000)
				{
					if (idAction != 8001)
					{
						return;
					}
					if (GameCanvas.loginScr.isLogin2)
					{
						GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, this, 10019, null), new Command(mResources.NO, this, 10020, null));
						return;
					}
					if (Main.isWindowsPhone)
					{
						GameMidlet.isBackWindowsPhone = true;
					}
					Session_ME.gI().close();
					GameCanvas.serverScreen.switchToMe();
					return;
				}
				else
				{
					if (CreateCharScr.tAddName.getText().Equals(string.Empty))
					{
						GameCanvas.startOKDlg(mResources.char_name_blank);
						return;
					}
					if (CreateCharScr.tAddName.getText().Length < 5)
					{
						GameCanvas.startOKDlg(mResources.char_name_short);
						return;
					}
					if (CreateCharScr.tAddName.getText().Length > 15)
					{
						GameCanvas.startOKDlg(mResources.char_name_long);
						return;
					}
					InfoDlg.showWait();
					Service.gI().createChar(CreateCharScr.tAddName.getText(), CreateCharScr.indexGender, CreateCharScr.hairIDOld[CreateCharScr.indexGender][CreateCharScr.indexHair]);
					return;
				}
			}
			else
			{
				if (idAction == 10019)
				{
					Session_ME.gI().close();
					GameCanvas.endDlg();
					GameCanvas.serverScreen.switchToMe();
					return;
				}
				if (idAction != 10020)
				{
					return;
				}
				GameCanvas.endDlg();
				return;
			}
		}

		// Token: 0x04002872 RID: 10354
		public static CreateCharScr instance;

		// Token: 0x04002873 RID: 10355
		public static bool isCreateChar = false;

		// Token: 0x04002874 RID: 10356
		public static TField tAddName;

		// Token: 0x04002875 RID: 10357
		public static int indexGender;

		// Token: 0x04002876 RID: 10358
		public static int indexHair;

		// Token: 0x04002877 RID: 10359
		public static int selected;

		// Token: 0x04002878 RID: 10360
		public static int[][] hairID = new int[][]
		{
			new int[]
			{
				106,
				108,
				127
			},
			new int[]
			{
				111,
				112,
				128
			},
			new int[]
			{
				105,
				107,
				126
			}
		};

		// Token: 0x04002879 RID: 10361
		public static int[][] hairIDOld = new int[][]
		{
			new int[]
			{
				64,
				30,
				31
			},
			new int[]
			{
				9,
				29,
				32
			},
			new int[]
			{
				6,
				27,
				28
			}
		};

		// Token: 0x0400287A RID: 10362
		public static int[] defaultLeg = new int[]
		{
			158,
			150,
			152
		};

		// Token: 0x0400287B RID: 10363
		public static int[] defaultBody = new int[]
		{
			157,
			149,
			151
		};

		// Token: 0x0400287C RID: 10364
		private int yButton;

		// Token: 0x0400287D RID: 10365
		private int disY;

		// Token: 0x0400287E RID: 10366
		public int yBegin;

		// Token: 0x0400287F RID: 10367
		private readonly int cx;

		// Token: 0x04002880 RID: 10368
		private readonly int cy;

		// Token: 0x04002881 RID: 10369
		private readonly int dy = 45;

		// Token: 0x04002882 RID: 10370
		private int cp1;

		// Token: 0x04002883 RID: 10371
		private int cf;

		// Token: 0x04002884 RID: 10372
		private readonly int xPopup;

		// Token: 0x04002885 RID: 10373
		private readonly int yPopup;

		// Token: 0x04002886 RID: 10374
		private mFont mFontGender;
	}
}
