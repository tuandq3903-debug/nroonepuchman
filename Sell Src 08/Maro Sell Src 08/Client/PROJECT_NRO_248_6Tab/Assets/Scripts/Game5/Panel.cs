using System;
using System.Collections.Generic;
using System.Threading;
using Game5.Assets.src.g;
using Game5.Mod.XMAP;
using UnityEngine;

namespace Game5
{
	// Token: 0x020003EB RID: 1003
	public class Panel : IActionListener, IChatable
	{
		// Token: 0x06002C5E RID: 11358 RVA: 0x002B2948 File Offset: 0x002B0B48
		public Panel()
		{
			this.init();
			this.cmdClose = new Command(string.Empty, this, 1003, null);
			this.cmdClose.img = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
			this.cmdClose.cmdClosePanel = true;
			this.currItem = null;
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x002B2D70 File Offset: 0x002B0F70
		public static void loadBg()
		{
			Panel.imgMap = GameCanvas.loadImage("/img/map" + TileMap.planetID.ToString() + ".png");
			Panel.imgBantay = GameCanvas.loadImage("/mainImage/myTexture2dbantay.png");
			Panel.imgX = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
			Panel.imgXu = GameCanvas.loadImage("/mainImage/myTexture2dimgMoney.png");
			Panel.imgThoivang = GameCanvas.loadImage("/mainImage/thoivang.png");
			Panel.imgLuong = GameCanvas.loadImage("/mainImage/myTexture2dimgDiamond.png");
			Panel.imgLuongKhoa = GameCanvas.loadImage("/mainImage/luongkhoa.png");
			Panel.imgUp = GameCanvas.loadImage("/mainImage/myTexture2dup.png");
			Panel.imgDown = GameCanvas.loadImage("/mainImage/myTexture2ddown.png");
			Panel.imgStar = GameCanvas.loadImage("/mainImage/star.png");
			Panel.imgMaxStar = GameCanvas.loadImage("/mainImage/starE.png");
			Panel.imgStar8 = GameCanvas.loadImage("/mainImage/star8.png");
			Panel.imgStar10 = GameCanvas.loadImage("/mainImage/star10.png");
			Panel.imgNew = GameCanvas.loadImage("/mainImage/new.png");
			Panel.imgTicket = GameCanvas.loadImage("/mainImage/ticket12.png");
			for (int i = 0; i < 8; i++)
			{
				if (Panel.bgcam[i] == null)
				{
					Panel.bgcam[i] = GameCanvas.loadEffect("/cam/" + i.ToString());
				}
				if (Panel.bgdo[i] == null)
				{
					Panel.bgdo[i] = GameCanvas.loadEffect("/do/" + i.ToString());
				}
				if (Panel.bgtim[i] == null)
				{
					Panel.bgtim[i] = GameCanvas.loadEffect("/tim/" + i.ToString());
				}
				if (Panel.bgxanhdam[i] == null)
				{
					Panel.bgxanhdam[i] = GameCanvas.loadEffect("/xanhdam/" + i.ToString());
				}
				if (Panel.bgxanhla[i] == null)
				{
					Panel.bgxanhla[i] = GameCanvas.loadEffect("/xanhla/" + i.ToString());
				}
				if (Panel.bgxanhnhat[i] == null)
				{
					Panel.bgxanhnhat[i] = GameCanvas.loadEffect("/xanhnhat/" + i.ToString());
				}
				if (Panel.bghong[i] == null)
				{
					Panel.bghong[i] = GameCanvas.loadEffect("/hong/" + i.ToString());
				}
				if (Panel.bgxanhduong[i] == null)
				{
					Panel.bgxanhduong[i] = GameCanvas.loadEffect("/xanhduong/" + i.ToString());
				}
			}
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x002B2FB4 File Offset: 0x002B11B4
		private void paintEffectItem(mGraphics g, Item item, int x, int y)
		{
			try
			{
				Image[] eff = null;
				if (item != null && item.itemOption != null)
				{
					foreach (ItemOption option in item.itemOption)
					{
						if (option != null && option.optionTemplate.id == 72)
						{
							Image[] bg;
							switch (option.param)
							{
								case 1:
									bg = Panel.bgcam;
									break;
								case 2:
									bg = Panel.bgdo;
									break;
								case 3:
									bg = Panel.bghong;
									break;
								case 4:
									bg = Panel.bgtim;
									break;
								case 5:
									bg = Panel.bgxanhdam;
									break;
								case 6:
									bg = Panel.bgxanhla;
									break;
								case 7:
									bg = Panel.bgxanhduong;
									break;
								case 8:
									bg = Panel.bgxanhnhat;
									break;
								default:
									bg = Panel.bgdo;
									break;
							}
							if (eff != null)
							{
								g.drawImage(eff[GameCanvas.gameTick / 6 % 9], x + 2, y + 2);
							}
							if (bg != null)
							{
								g.drawImage(bg[GameCanvas.gameTick / 5 % 8], x - 1, y - 2);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x002B30E8 File Offset: 0x002B12E8
		public void init()
		{
			this.pX = GameCanvas.pxLast + this.cmxMap;
			this.pY = GameCanvas.pyLast + this.cmyMap;
			this.lastTabIndex = new int[this.tabName.Length];
			for (int i = 0; i < this.lastTabIndex.Length; i++)
			{
				this.lastTabIndex[i] = -1;
			}
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x002B3148 File Offset: 0x002B1348
		public int getXMap()
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				if (TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i])
				{
					return Panel.mapX[(int)TileMap.planetID][i];
				}
			}
			return -1;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x002B3190 File Offset: 0x002B1390
		public int getYMap()
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				if (TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i])
				{
					return Panel.mapY[(int)TileMap.planetID][i];
				}
			}
			return -1;
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x002B31D8 File Offset: 0x002B13D8
		public int getXMapTask()
		{
			if (Char.myCharz().taskMaint == null)
			{
				return -1;
			}
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				if (GameScr.mapTasks[Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i])
				{
					return Panel.mapX[(int)TileMap.planetID][i];
				}
			}
			return -1;
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x002B3240 File Offset: 0x002B1440
		public int getYMapTask()
		{
			if (Char.myCharz().taskMaint == null)
			{
				return -1;
			}
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				if (GameScr.mapTasks[Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i])
				{
					return Panel.mapY[(int)TileMap.planetID][i];
				}
			}
			return -1;
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x002B32A8 File Offset: 0x002B14A8
		private void setType(int position)
		{
			this.typeShop = -1;
			this.W = Panel.WIDTH_PANEL + 6;
			this.H = GameCanvas.h;
			this.X = 0;
			this.Y = 0;
			this.ITEM_HEIGHT = 25;
			this.position = position;
			if (position != 0)
			{
				if (position == 1)
				{
					this.wScroll = this.W - 4;
					this.xScroll = GameCanvas.w - this.wScroll;
					this.yScroll = 80;
					this.hScroll = this.H - 96;
					this.X = this.xScroll - 2;
					this.cmx = -(GameCanvas.w + this.W);
					this.cmtoX = GameCanvas.w - this.W;
				}
			}
			else
			{
				this.xScroll = 2;
				this.yScroll = 80;
				this.wScroll = this.W - 4;
				this.hScroll = this.H - 96;
				this.cmx = this.wScroll;
				this.cmtoX = 0;
				this.X = 0;
			}
			this.TAB_W = this.W / 5 - 1;
			this.currentTabIndex = 0;
			this.currentTabName = this.tabName[this.type];
			if (this.currentTabName.Length < 5)
			{
				this.TAB_W += 5;
			}
			this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
			this.lastSelect = new int[this.currentTabName.Length];
			this.cmyLast = new int[this.currentTabName.Length];
			for (int i = 0; i < this.currentTabName.Length; i++)
			{
				this.lastSelect[i] = (GameCanvas.isTouch ? -1 : 0);
			}
			if (this.lastTabIndex[this.type] != -1)
			{
				this.currentTabIndex = this.lastTabIndex[this.type];
			}
			if (this.currentTabIndex < 0)
			{
				this.currentTabIndex = 0;
			}
			if (this.currentTabIndex > this.currentTabName.Length - 1)
			{
				this.currentTabIndex = this.currentTabName.Length - 1;
			}
			this.scroll = null;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x002B34C4 File Offset: 0x002B16C4
		public void setTypeMapTrans()
		{
			this.type = 14;
			this.setType(0);
			this.setTabMapTrans();
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x002B34F8 File Offset: 0x002B16F8
		public void setTypeMap()
		{
			if (!GameScr.gI().isMapFize() && Panel.isPaintMap)
			{
				if (Hint.isOnTask(2, 0))
				{
					Hint.isViewMap = true;
					GameScr.info1.addInfo(mResources.go_to_quest, 0);
				}
				if (Hint.isOnTask(3, 0))
				{
					Hint.isViewPotential = true;
				}
				this.type = 4;
				this.currentTabName = this.tabName[this.type];
				this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
				this.cmx = (this.cmtoX = 0);
				this.setTabMap();
			}
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x002B35A4 File Offset: 0x002B17A4
		public void setTypeArchivement()
		{
			this.currentListLength = Char.myCharz().arrArchive.Length;
			this.setType(0);
			this.type = 9;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = 0);
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x002B3660 File Offset: 0x002B1860
		public void setTypeKiGuiOnly()
		{
			this.type = 17;
			this.setType(1);
			this.setTabKiGui();
			this.typeShop = 2;
			this.currentTabIndex = 0;
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x002B3688 File Offset: 0x002B1888
		public void setTabKiGui()
		{
			this.ITEM_HEIGHT = 24;
			this.currentListLength = Char.myCharz().arrItemShop[4].Length;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x002B3744 File Offset: 0x002B1944
		public void setTypeBodyOnly()
		{
			this.type = 7;
			this.setType(1);
			this.setTabInventory(true);
			this.currentTabIndex = 0;
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x002B3762 File Offset: 0x002B1962
		public void addChatMessage(InfoItem info)
		{
			this.logChat.insertElementAt(info, 0);
			if (this.logChat.size() > 20)
			{
				this.logChat.removeElementAt(this.logChat.size() - 1);
			}
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x002B3798 File Offset: 0x002B1998
		public void setTabPlayerMenu()
		{
			this.ITEM_HEIGHT = 24;
			this.currentListLength = this.vPlayerMenu.size();
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x002B3851 File Offset: 0x002B1A51
		public void setTypeFlag()
		{
			this.type = 18;
			this.setType(0);
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.setTabFlag();
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x002B3884 File Offset: 0x002B1A84
		public void setTabFlag()
		{
			this.currentListLength = this.vFlag.size();
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			if (this.selected > this.currentListLength - 1)
			{
				this.selected = this.currentListLength - 1;
			}
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x002B3952 File Offset: 0x002B1B52
		public void setTypePlayerMenu(Char c)
		{
			this.type = 10;
			this.setType(0);
			this.setTabPlayerMenu();
			this.charMenu = c;
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x002B3970 File Offset: 0x002B1B70
		public void setTypeFriend()
		{
			this.type = 11;
			this.setType(0);
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.setTabFriend();
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x002B39A0 File Offset: 0x002B1BA0
		public void setTypeEnemy()
		{
			this.type = 16;
			this.setType(0);
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.setTabEnemy();
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x002B39D0 File Offset: 0x002B1BD0
		public void setTypeTop(sbyte t)
		{
			this.type = 15;
			this.setType(0);
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.setTabTop();
			this.isThachDau = (t != 0);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x002B3A0C File Offset: 0x002B1C0C
		public void setTabTop()
		{
			this.currentListLength = this.vTop.size();
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			if (this.selected > this.currentListLength - 1)
			{
				this.selected = this.currentListLength - 1;
			}
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x002B3ADC File Offset: 0x002B1CDC
		public void setTabFriend()
		{
			this.currentListLength = this.vFriend.size();
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			if (this.selected > this.currentListLength - 1)
			{
				this.selected = this.currentListLength - 1;
			}
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x002B3BAC File Offset: 0x002B1DAC
		public void setTabEnemy()
		{
			this.currentListLength = this.vEnemy.size();
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			if (this.selected > this.currentListLength - 1)
			{
				this.selected = this.currentListLength - 1;
			}
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x002B3C7A File Offset: 0x002B1E7A
		public void setTypeMessage()
		{
			this.type = 8;
			this.setType(0);
			this.setTabMessage();
			this.currentTabIndex = 0;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x002B3C97 File Offset: 0x002B1E97
		public void setTypeShop(int typeShop)
		{
			this.type = 1;
			this.setType(0);
			this.setTabShop();
			this.currentTabIndex = 0;
			this.typeShop = typeShop;
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x002B3CBC File Offset: 0x002B1EBC
		public void setTypeBox()
		{
			this.type = 2;
			if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
			{
				Panel.boxTabName = new string[][]
				{
					mResources.chestt
				};
			}
			else
			{
				Panel.boxTabName = new string[][]
				{
					mResources.chestt,
					mResources.inventory
				};
			}
			this.tabName[2] = Panel.boxTabName;
			this.setType(0);
			if (this.currentTabIndex == 0)
			{
				this.setTabBox();
			}
			if (this.currentTabIndex == 1)
			{
				this.setTabInventory(true);
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
			}
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x002B3D90 File Offset: 0x002B1F90
		public void setTypeCombine()
		{
			this.type = 12;
			if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
			{
				Panel.boxCombine = new string[][]
				{
					mResources.combine
				};
			}
			else
			{
				Panel.boxCombine = new string[][]
				{
					mResources.combine,
					mResources.inventory
				};
			}
			this.tabName[this.type] = Panel.boxCombine;
			this.setType(0);
			if (this.currentTabIndex == 0)
			{
				this.setTabCombine();
			}
			if (this.currentTabIndex == 1)
			{
				this.setTabInventory(true);
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
			}
			this.combineSuccess = -1;
			this.isDoneCombine = true;
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x002B3E78 File Offset: 0x002B2078
		public void setTabCombine()
		{
			this.currentListLength = this.vItemCombine.size() + 1;
			this.ITEM_HEIGHT = 24;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 9;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x002B3F34 File Offset: 0x002B2134
		public void setTypeAuto()
		{
			this.type = 22;
			this.setType(0);
			this.setTabAuto();
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x002B3F68 File Offset: 0x002B2168
		private void setTabAuto()
		{
			this.currentListLength = Panel.strAuto.Length;
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x002B4020 File Offset: 0x002B2220
		public void setTypePetMain()
		{
			this.type = 21;
			if (GameCanvas.panel2 != null)
			{
				Panel.boxPet = mResources.petMainTab2;
			}
			else
			{
				Panel.boxPet = mResources.petMainTab;
			}
			this.tabName[21] = Panel.boxPet;
			if (Char.myCharz().cgender == 1)
			{
				this.strStatus = new string[]
				{
					mResources.follow,
					mResources.defend,
					mResources.attack,
					mResources.gohome,
					mResources.fusion,
					mResources.fusionForever
				};
			}
			else
			{
				this.strStatus = new string[]
				{
					mResources.follow,
					mResources.defend,
					mResources.attack,
					mResources.gohome,
					mResources.fusion
				};
			}
			this.setType(2);
			if (this.currentTabIndex == 0)
			{
				this.setTabPetInventory(false);
				return;
			}
			if (this.currentTabIndex == 1)
			{
				this.setTabPetSkill(false);
				return;
			}
			if (this.currentTabIndex == 2)
			{
				this.setTabPetStatus();
				return;
			}
			if (this.currentTabIndex == 3)
			{
				this.setTabInventory(true);
			}
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x002B4128 File Offset: 0x002B2328
		public void setTypePet2Main()
		{
			this.type = 28;
			if (GameCanvas.panel2 != null)
			{
				Panel.boxPet = mResources.petMainTab2;
			}
			else
			{
				Panel.boxPet = mResources.petMainTab;
			}
			this.tabName[28] = Panel.boxPet;
			if (Char.myCharz().cgender == 1)
			{
				this.strStatus = new string[]
				{
					mResources.follow,
					mResources.defend,
					mResources.attack,
					mResources.gohome,
					mResources.fusion,
					mResources.fusionForever
				};
			}
			else
			{
				this.strStatus = new string[]
				{
					mResources.follow,
					mResources.defend,
					mResources.attack,
					mResources.gohome,
					mResources.fusion
				};
			}
			this.setType(2);
			if (this.currentTabIndex == 0)
			{
				this.setTabPetInventory(true);
				return;
			}
			if (this.currentTabIndex == 1)
			{
				this.setTabPetSkill(true);
				return;
			}
			if (this.currentTabIndex == 2)
			{
				this.setTabPetStatus();
				return;
			}
			if (this.currentTabIndex == 3)
			{
				this.setTabInventory(true);
			}
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x002B4230 File Offset: 0x002B2430
		public void setTypeMain()
		{
			this.type = 0;
			this.setType(0);
			if (this.currentTabIndex == 1)
			{
				this.setTabInventory(true);
			}
			if (this.currentTabIndex == 2)
			{
				this.setTabSkill();
			}
			if (this.currentTabIndex == 3)
			{
				if (this.mainTabName.Length == 4)
				{
					this.setTabTool();
				}
				else
				{
					this.setTabClans();
				}
			}
			if (this.currentTabIndex == 4)
			{
				this.setTabTool();
			}
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x002B429C File Offset: 0x002B249C
		public void setTypeZone()
		{
			this.type = 3;
			this.setType(0);
			this.setTabZone();
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x002B42D0 File Offset: 0x002B24D0
		public void addItemDetail(Item item)
		{
			try
			{
				this.cp = new ChatPopup();
				string empty = string.Empty;
				string text = string.Empty;
				if ((int)item.template.gender != Char.myCharz().cgender)
				{
					if (item.template.gender == 0)
					{
						text = text + "\n|7|1|" + mResources.from_earth;
					}
					else if (item.template.gender == 1)
					{
						text = text + "\n|7|1|" + mResources.from_namec;
					}
					else if (item.template.gender == 2)
					{
						text = text + "\n|7|1|" + mResources.from_sayda;
					}
				}
				mFont tahoma_7b_dark = mFont.tahoma_7b_dark;
				string text2 = string.Empty;
				if (item.itemOption != null)
				{
					for (int i = 0; i < item.itemOption.Length; i++)
					{
						if (item.itemOption[i].optionTemplate.id == 72)
						{
							text2 = " [+" + item.itemOption[i].param.ToString() + "]";
						}
					}
				}
				bool flag = false;
				if (item.itemOption != null)
				{
					for (int j = 0; j < item.itemOption.Length; j++)
					{
						if (item.itemOption[j].optionTemplate.id == 72)
						{
							flag = true;
							if (item.itemOption[j].param >= 1 && item.itemOption[j].param <= 5)
							{
								text = text + "|2|1|" + item.template.name + text2;
							}
							if (item.itemOption[j].param >= 6 && item.itemOption[j].param <= 7)
							{
								text = text + "|8|1|" + item.template.name + text2;
							}
							if (item.itemOption[j].param >= 8 && item.itemOption[j].param <= 10)
							{
								text = text + "|7|1|" + item.template.name + text2;
							}
						}
					}
				}
				if (!flag)
				{
					text = text + "|0|1|" + item.template.name + text2;
				}
				if (item.itemOption != null)
				{
					for (int k = 0; k < item.itemOption.Length; k++)
					{
						if (item.itemOption[k].optionTemplate.name.StartsWith("$"))
						{
							empty = item.itemOption[k].getOptiongColor();
							if (item.itemOption[k].param == 1)
							{
								text = text + "\n|1|1|" + empty;
							}
							if (item.itemOption[k].param == 0)
							{
								text = text + "\n|0|1|" + empty;
							}
						}
						else
						{
							empty = item.itemOption[k].getOptionString();
							if (!empty.Equals(string.Empty) && item.itemOption[k].optionTemplate.id != 72)
							{
								if (item.itemOption[k].optionTemplate.id == 102)
								{
									this.cp.starSlot = (sbyte)item.itemOption[k].param;
									Res.outz("STAR SLOT= " + this.cp.starSlot.ToString());
								}
								else if (item.itemOption[k].optionTemplate.id == 107)
								{
									this.cp.maxStarSlot = (sbyte)item.itemOption[k].param;
									Res.outz("STAR SLOT= " + this.cp.maxStarSlot.ToString());
								}
								else
								{
									text = text + "\n|1|1|" + empty;
								}
							}
						}
					}
				}
				if (this.currItem.template.strRequire > 1)
				{
					string text3 = mResources.pow_request + ": " + this.currItem.template.strRequire.ToString();
					if ((long)this.currItem.template.strRequire > Char.myCharz().cPower)
					{
						text = text + "\n|3|1|" + text3;
						string text4 = text;
						text = string.Concat(new string[]
						{
							text4,
							"\n|3|1|",
							mResources.your_pow,
							": ",
							Char.myCharz().cPower.ToString()
						});
					}
					else
					{
						text = text + "\n|6|1|" + text3;
					}
				}
				else
				{
					text += "\n|6|1|";
				}
				this.currItem.compare = this.getCompare(this.currItem);
				text += "\n--";
				text = text + "\n|6|" + item.template.description;
				if (!item.reason.Equals(string.Empty))
				{
					if (!item.template.description.Equals(string.Empty))
					{
						text += "\n--";
					}
					text = text + "\n|2|" + item.reason;
				}
				if (this.cp.maxStarSlot > 0)
				{
					text += "\n\n";
				}
				this.popUpDetailInit(this.cp, text);
				this.idIcon = (int)item.template.iconID;
				this.partID = null;
				this.charInfo = null;
			}
			catch (Exception ex)
			{
				Res.outz("ex " + ex.StackTrace);
			}
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x002B482C File Offset: 0x002B2A2C
		public void popUpDetailInit(ChatPopup cp, string chat)
		{
			cp.isClip = false;
			cp.sayWidth = 180;
			cp.cx = 3 + this.X - ((this.X != 0) ? (Res.abs(cp.sayWidth - this.W) + 8) : 0);
			cp.says = mFont.tahoma_7_red.splitFontArray(chat, cp.sayWidth - 10);
			cp.delay = 10000000;
			cp.c = null;
			cp.sayRun = 7;
			cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
			if (cp.ch > GameCanvas.h - 80)
			{
				cp.ch = GameCanvas.h - 80;
				cp.lim = cp.says.Length * 12 - cp.ch + 17;
				if (cp.lim < 0)
				{
					cp.lim = 0;
				}
				ChatPopup.cmyText = 0;
				cp.isClip = true;
			}
			cp.cy = GameCanvas.menu.menuY - cp.ch;
			while (cp.cy < 10)
			{
				cp.cy++;
				GameCanvas.menu.menuY++;
			}
			cp.mH = 0;
			cp.strY = 10;
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x002B4974 File Offset: 0x002B2B74
		public void addMessageDetail(ClanMessage cm)
		{
			this.cp = new ChatPopup();
			string text = "|0|" + cm.playerName;
			text = text + "\n|1|" + Member.getRole((int)cm.role);
			for (int i = 0; i < this.myMember.size(); i++)
			{
				Member member = (Member)this.myMember.elementAt(i);
				if (cm.playerId == member.ID)
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"\n|5|",
						mResources.clan_capsuledonate,
						": ",
						member.clanPoint.ToString()
					});
					text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"\n|5|",
						mResources.clan_capsuleself,
						": ",
						member.curClanPoint.ToString()
					});
					text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"\n|4|",
						mResources.give_pea,
						": ",
						member.donate.ToString(),
						mResources.time
					});
					text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"\n|4|",
						mResources.receive_pea,
						": ",
						member.receive_donate.ToString(),
						mResources.time
					});
					this.partID = new int[]
					{
						(int)member.head,
						(int)member.leg,
						(int)member.body
					};
					break;
				}
			}
			text += "\n--";
			for (int j = 0; j < cm.chat.Length; j++)
			{
				text = text + "\n" + cm.chat[j];
			}
			if (cm.type == 1)
			{
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					"\n|6|",
					mResources.received,
					" ",
					cm.recieve.ToString(),
					"/",
					cm.maxCap.ToString()
				});
			}
			this.popUpDetailInit(this.cp, text);
			this.charInfo = null;
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x002B4BB4 File Offset: 0x002B2DB4
		public void addThachDauDetail(TopInfo t)
		{
			string text = "|0|1|" + t.name;
			text = text + "\n|1|Top " + t.rank.ToString();
			text = text + "\n|1|" + t.info;
			text = text + "\n|2|" + t.info2;
			this.cp = new ChatPopup();
			this.popUpDetailInit(this.cp, text);
			this.partID = new int[]
			{
				t.headID,
				(int)t.leg,
				(int)t.body
			};
			this.currItem = null;
			this.charInfo = null;
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x002B4C5C File Offset: 0x002B2E5C
		public void addClanMemberDetail(Member m)
		{
			string text = "|0|1|" + m.name;
			string text2 = "\n|2|1|";
			if (m.role == 0)
			{
				text2 = "\n|7|1|";
			}
			if (m.role == 1)
			{
				text2 = "\n|1|1|";
			}
			if (m.role == 2)
			{
				text2 = "\n|0|1|";
			}
			text = text + text2 + Member.getRole((int)m.role);
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|2|1|",
				mResources.power,
				": ",
				m.powerPoint
			});
			text += "\n--";
			text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|5|",
				mResources.clan_capsuledonate,
				": ",
				m.clanPoint.ToString()
			});
			text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|5|",
				mResources.clan_capsuleself,
				": ",
				m.curClanPoint.ToString()
			});
			text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|4|",
				mResources.give_pea,
				": ",
				m.donate.ToString(),
				mResources.time
			});
			text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|4|",
				mResources.receive_pea,
				": ",
				m.receive_donate.ToString(),
				mResources.time
			});
			text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|6|",
				mResources.join_date,
				": ",
				m.joinTime
			});
			this.cp = new ChatPopup();
			this.popUpDetailInit(this.cp, text);
			this.partID = new int[]
			{
				(int)m.head,
				(int)m.leg,
				(int)m.body
			};
			this.currItem = null;
			this.charInfo = null;
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x002B4E70 File Offset: 0x002B3070
		public void addClanDetail(Clan cl)
		{
			try
			{
				string text = "|0|" + cl.name;
				string[] array = mFont.tahoma_7_green.splitFontArray(cl.slogan, this.wScroll - 60);
				for (int i = 0; i < array.Length; i++)
				{
					text = text + "\n|2|" + array[i];
				}
				text += "\n--";
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\n|7|",
					mResources.clan_leader,
					": ",
					cl.leaderName
				});
				text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\n|1|",
					mResources.power_point,
					": ",
					cl.powerPoint
				});
				text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\n|4|",
					mResources.member,
					": ",
					cl.currMember.ToString(),
					"/",
					cl.maxMember.ToString()
				});
				text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\n|4|",
					mResources.level,
					": ",
					cl.level.ToString()
				});
				text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\n|4|",
					mResources.clan_birthday,
					": ",
					NinjaUtil.getDate(cl.date)
				});
				this.cp = new ChatPopup();
				this.popUpDetailInit(this.cp, text);
				this.idIcon = ClanImage.getClanImage((short)cl.imgID).idImage[0];
				this.currItem = null;
			}
			catch (Exception ex)
			{
				Res.outz("Throw  exception " + ex.StackTrace);
			}
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x002B5068 File Offset: 0x002B3268
		public void addSkillDetail(SkillTemplate tp, Skill skill, Skill nextSkill)
		{
			string text = "|0|" + tp.name;
			for (int i = 0; i < tp.description.Length; i++)
			{
				text = text + "\n|4|" + tp.description[i];
			}
			text += "\n--";
			if (skill != null)
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\n|2|",
					mResources.cap_do,
					": ",
					skill.point.ToString()
				});
				text = text + "\n|5|" + NinjaUtil.Replace(tp.damInfo, "#", skill.damage.ToString() + string.Empty);
				text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\n|5|",
					mResources.KI_consume,
					skill.manaUse.ToString(),
					(tp.manaUseType != 1) ? string.Empty : "%"
				});
				text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\n|5|",
					mResources.cooldown,
					": ",
					skill.strTimeReplay(),
					"s"
				});
				text += "\n--";
				if (skill.point == tp.maxPoint)
				{
					text = text + "\n|0|" + mResources.max_level_reach;
				}
				else
				{
					if (!skill.template.isSkillSpec())
					{
						text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							"\n|1|",
							mResources.next_level_require,
							Res.formatNumber(nextSkill.powRequire),
							" ",
							mResources.potential
						});
					}
					text = text + "\n|4|" + NinjaUtil.Replace(tp.damInfo, "#", nextSkill.damage.ToString() + string.Empty);
				}
			}
			else
			{
				text = text + "\n|2|" + mResources.not_learn;
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					"\n|1|",
					mResources.learn_require,
					Res.formatNumber(nextSkill.powRequire),
					" ",
					mResources.potential
				});
				text = text + "\n|4|" + NinjaUtil.Replace(tp.damInfo, "#", nextSkill.damage.ToString() + string.Empty);
				text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					"\n|4|",
					mResources.KI_consume,
					nextSkill.manaUse.ToString(),
					(tp.manaUseType != 1) ? string.Empty : "%"
				});
				text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					"\n|4|",
					mResources.cooldown,
					": ",
					nextSkill.strTimeReplay(),
					"s"
				});
			}
			this.currItem = null;
			this.partID = null;
			this.charInfo = null;
			this.cp = new ChatPopup();
			this.popUpDetailInit(this.cp, text);
			this.idIcon = 0;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x002B5398 File Offset: 0x002B3598
		public void show()
		{
			if (GameCanvas.isTouch)
			{
				this.cmdClose.x = 156;
				this.cmdClose.y = 3;
			}
			else
			{
				this.cmdClose.x = GameCanvas.w - 19;
				this.cmdClose.y = GameCanvas.h - 19;
			}
			this.cmdClose.isPlaySoundButton = false;
			ChatPopup.currChatPopup = null;
			InfoDlg.hide();
			this.timeShow = 20;
			this.isShow = true;
			this.isClose = false;
			SoundMn.gI().panelOpen();
			if (this.isTypeShop())
			{
				Char.myCharz().setPartOld();
			}
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x002B543C File Offset: 0x002B363C
		public void chatTFUpdateKey()
		{
			if (this.chatTField != null && this.chatTField.isShow)
			{
				if (this.chatTField.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.chatTField.left)) && this.chatTField.left != null)
				{
					this.chatTField.left.performAction();
				}
				if (this.chatTField.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.chatTField.right)) && this.chatTField.right != null)
				{
					this.chatTField.right.performAction();
				}
				if (this.chatTField.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.chatTField.center)) && this.chatTField.center != null)
				{
					this.chatTField.center.performAction();
				}
				if (this.chatTField.isShow && GameCanvas.keyAsciiPress != 0)
				{
					this.chatTField.keyPressed(GameCanvas.keyAsciiPress);
					GameCanvas.keyAsciiPress = 0;
				}
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x002B5574 File Offset: 0x002B3774
		public void updateKey()
		{
			if ((this.chatTField != null && this.chatTField.isShow) || !GameCanvas.panel.isDoneCombine || InfoDlg.isShow)
			{
				return;
			}
			if (this.tabIcon != null && this.tabIcon.isShow)
			{
				this.tabIcon.updateKey();
				return;
			}
			if (this.isClose || !this.isShow)
			{
				return;
			}
			if (this.cmdClose.isPointerPressInside())
			{
				this.cmdClose.performAction();
				return;
			}
			if (GameCanvas.keyPressed[13])
			{
				if (this.type != 4)
				{
					this.hide();
					return;
				}
				this.setTypeMain();
				this.cmx = (this.cmtoX = 0);
			}
			if (GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				if (this.left.idAction > 0)
				{
					this.perform(this.left.idAction, this.left.p);
				}
				else
				{
					this.waitToPerform = 2;
				}
			}
			if (this.Equals(GameCanvas.panel) && GameCanvas.panel2 == null && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(this.X, this.Y, this.W, this.H) && !this.pointerIsDowning)
			{
				this.hide();
				return;
			}
			if (!this.isClanOption)
			{
				this.updateKeyInTabBar();
			}
			switch (this.type)
			{
				case 0:
					if (this.currentTabIndex == 0)
					{
						this.updateKeyQuest();
						GameCanvas.clearKeyPressed();
						return;
					}
					if (this.currentTabIndex == 1)
					{
						this.updateKeyInventory();
					}
					if (this.currentTabIndex == 2)
					{
						this.updateKeySkill();
					}
					if (this.currentTabIndex == 3)
					{
						if (this.mainTabName.Length == 4)
						{
							this.updateKeyTool();
						}
						else
						{
							this.updateKeyClans();
						}
					}
					if (this.currentTabIndex == 4)
					{
						this.updateKeyTool();
					}
					break;
				case 1:
				case 17:
				case 25:
					if (this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17)
					{
						this.updateKeyScrollView();
					}
					else if (this.typeShop == 0)
					{
						this.updateKeyInventory();
					}
					else
					{
						this.updateKeyScrollView();
					}
					break;
				case 2:
					this.updateKeyInventory();
					break;
				case 3:
				case 8:
				case 9:
				case 10:
				case 11:
				case 14:
				case 15:
				case 16:
				case 18:
				case 23:
				case 24:
				case 26:
				case 27:
					this.updateKeyScrollView();
					break;
				case 4:
					this.updateKeyMap();
					GameCanvas.clearKeyPressed();
					return;
				case 7:
					this.updateKeyInventory();
					break;
				case 12:
					this.updateKeyCombine();
					break;
				case 13:
					this.updateKeyGiaoDich();
					break;
				case 19:
					this.updateKeyOption();
					break;
				case 20:
					this.updateKeyOption();
					break;
				case 21:
				case 28:
					if (this.currentTabIndex == 0 || this.currentTabIndex == 2)
					{
						this.updateKeyScrollView();
					}
					else if (this.currentTabIndex == 1)
					{
						this.updateKeyPetStatus();
					}
					break;
				case 22:
					this.updateKeyAuto();
					break;
			}
			GameCanvas.clearKeyHold();
			for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
			{
				GameCanvas.keyPressed[i] = false;
			}
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000034B9 File Offset: 0x000016B9
		private void updateKeyAuto()
		{
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x002B58A6 File Offset: 0x002B3AA6
		private void updateKeyPetStatus()
		{
			this.updateKeyScrollView();
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x002B58A6 File Offset: 0x002B3AA6
		private void keyGiaodich()
		{
			this.updateKeyScrollView();
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x002B58B0 File Offset: 0x002B3AB0
		private void updateKeyGiaoDich()
		{
			if (this.currentTabIndex == 0)
			{
				if (this.Equals(GameCanvas.panel))
				{
					this.updateKeyInventory();
				}
				if (this.Equals(GameCanvas.panel2))
				{
					this.keyGiaodich();
				}
			}
			if (this.currentTabIndex == 1 || this.currentTabIndex == 2)
			{
				this.keyGiaodich();
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x002B58A6 File Offset: 0x002B3AA6
		private void updateKeyTool()
		{
			this.updateKeyScrollView();
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x002B58A6 File Offset: 0x002B3AA6
		private void updateKeySkill()
		{
			this.updateKeyScrollView();
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x002B5904 File Offset: 0x002B3B04
		public void setTabGiaoDich(bool isMe)
		{
			this.currentListLength = ((!isMe) ? (this.vFriendGD.size() + 3) : (this.vMyGD.size() + 3));
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x002B59D4 File Offset: 0x002B3BD4
		public void setTypeGiaoDich(Char cGD)
		{
			this.type = 13;
			this.tabName[this.type] = Panel.boxGD;
			this.isAccept = false;
			this.isLock = false;
			this.isFriendLock = false;
			this.vMyGD.removeAllElements();
			this.vFriendGD.removeAllElements();
			this.moneyGD = 0;
			this.friendMoneyGD = 0;
			if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
			{
				GameCanvas.panel2 = new Panel();
				GameCanvas.panel2.type = 13;
				GameCanvas.panel2.tabName[this.type] = new string[][]
				{
					mResources.item_receive
				};
				GameCanvas.panel2.setType(1);
				GameCanvas.panel2.setTabGiaoDich(false);
				GameCanvas.panel.tabName[this.type] = new string[][]
				{
					mResources.inventory,
					mResources.item_give
				};
				GameCanvas.panel2.show();
				GameCanvas.panel2.charMenu = cGD;
			}
			if (this.Equals(GameCanvas.panel))
			{
				this.setType(0);
			}
			if (this.currentTabIndex == 0)
			{
				this.setTabInventory(true);
			}
			if (this.currentTabIndex == 1)
			{
				this.setTabGiaoDich(true);
			}
			if (this.currentTabIndex == 2)
			{
				this.setTabGiaoDich(false);
			}
			this.charMenu = cGD;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x002B5B18 File Offset: 0x002B3D18
		private void paintGiaoDich(mGraphics g, bool isMe)
		{
			g.setColor(16711680);
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			MyVector myVector = (!isMe) ? this.vFriendGD : this.vMyGD;
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll + 36;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = this.wScroll - 36;
				int num4 = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + i * this.ITEM_HEIGHT;
				int num7 = 34;
				int num8 = this.ITEM_HEIGHT - 1;
				if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					if (i == this.currentListLength - 1)
					{
						if (isMe)
						{
							g.setColor(15196114);
							g.fillRect(num5, num2, this.wScroll, num4, 5);
							if (!this.isLock)
							{
								if (!this.isFriendLock)
								{
									mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
								else
								{
									mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
							}
							else if (this.isFriendLock)
							{
								g.setColor(15196114);
								g.fillRect(num5, num2, this.wScroll, num4, 5);
								g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
								((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.done, this.xScroll + this.wScroll - 22, num2 + 7, 2);
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
							}
							else
							{
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
						}
					}
					else if (i == this.currentListLength - 2)
					{
						if (isMe)
						{
							g.setColor(15196114);
							g.fillRect(num5, num2, this.wScroll, num4, 5);
							if (!this.isAccept)
							{
								if (!this.isLock)
								{
									g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
									((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.mlock, this.xScroll + this.wScroll - 22, num2 + 7, 2);
									mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.not_lock_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
								}
								else
								{
									g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
									((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.CANCEL, this.xScroll + this.wScroll - 22, num2 + 7, 2);
									mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
								}
							}
						}
						else if (!this.isFriendLock)
						{
							mFont.tahoma_7b_dark.drawString(g, mResources.not_lock_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
						}
						else
						{
							mFont.tahoma_7b_dark.drawString(g, mResources.locked_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
						}
					}
					else if (i == this.currentListLength - 3)
					{
						if (this.isLock)
						{
							g.setColor(13748667);
						}
						else
						{
							g.setColor((i != this.selected) ? 15196114 : 16383818);
						}
						g.fillRect(num, num2, num3, num4, 5);
						if (this.isLock)
						{
							g.setColor(13748667);
						}
						else
						{
							g.setColor((i != this.selected) ? 9993045 : 7300181);
						}
						g.fillRect(num5, num6, num7, num8, 5);
						g.drawImage(Panel.imgXu, num5 + num7 / 2, num6 + num8 / 2, 3);
						mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys((long)((!isMe) ? this.friendMoneyGD : this.moneyGD)) + " " + mResources.XU, num + 5, num2 + 11, 0);
						mFont.tahoma_7_green.drawString(g, mResources.money_trade, num + 5, num2, 0);
					}
					else
					{
						if (myVector.size() == 0)
						{
							return;
						}
						if (this.isLock)
						{
							g.setColor(13748667);
						}
						else
						{
							g.setColor((i != this.selected) ? 15196114 : 16383818);
						}
						g.fillRect(num, num2, num3, num4, 5);
						if (this.isLock)
						{
							g.setColor(13748667);
						}
						else
						{
							g.setColor((i != this.selected) ? 9993045 : 9541120);
						}
						Item item = (Item)myVector.elementAt(i);
						if (item != null)
						{
							for (int j = 0; j < item.itemOption.Length; j++)
							{
								if (item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0)
								{
									sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
									if (Panel.GetColor_ItemBg((int)color_Item_Upgrade) != -1)
									{
										if (this.isLock)
										{
											g.setColor(13748667);
										}
										else
										{
											g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
										}
									}
								}
							}
						}
						g.fillRect(num5, num6, num7, num8, 5);
						if (item != null)
						{
							string text = string.Empty;
							mFont mFont2 = mFont.tahoma_7_green2;
							if (item.itemOption != null)
							{
								for (int k = 0; k < item.itemOption.Length; k++)
								{
									if (item.itemOption[k].optionTemplate.id == 72)
									{
										text = " [+" + item.itemOption[k].param.ToString() + "]";
									}
									if (item.itemOption[k].optionTemplate.id == 72)
									{
										if (item.itemOption[k].param >= 1 && item.itemOption[k].param <= 5)
										{
											mFont2 = Panel.GetFont(2);
										}
										else if (item.itemOption[k].param >= 6 && item.itemOption[k].param <= 7)
										{
											mFont2 = Panel.GetFont(8);
										}
										else if (item.itemOption[k].param >= 8 && item.itemOption[k].param <= 10)
										{
											mFont2 = Panel.GetFont(7);
										}
									}
								}
							}
							if (ModFunc.isShowID)
							{
								mFont2.drawString(g, string.Concat(new string[]
								{
									"[",
									item.template.id.ToString(),
									"] ",
									item.template.name,
									text
								}), num + 5, num2 + 1, 0);
							}
							else
							{
								mFont2.drawString(g, item.template.name + text, num + 5, num2 + 1, 0);
							}
							string text2 = string.Empty;
							if (item.itemOption != null)
							{
								if (item.itemOption.Length != 0 && item.itemOption[0] != null)
								{
									text2 += item.itemOption[0].getOptionString();
								}
								mFont mFont3 = mFont.tahoma_7_blue;
								if (item.compare < 0 && item.template.type != 5)
								{
									mFont3 = mFont.tahoma_7_red;
								}
								if (item.itemOption.Length > 1)
								{
									for (int l = 1; l < Math.min(item.itemOption.Length, 3); l++)
									{
										if (item.itemOption[l] != null && item.itemOption[l].IsValidOption())
										{
											text2 = text2 + ", " + item.itemOption[l].getOptionString();
										}
									}
								}
								mFont3.drawString(g, text2, num + 5, num2 + 10, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
							if (item.itemOption != null)
							{
								for (int m = 0; m < item.itemOption.Length; m++)
								{
									this.paintOptItemInventory(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num5, num6, num7, num8, item);
								}
								for (int n = 0; n < item.itemOption.Length; n++)
								{
									this.paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
								}
							}
							if (item.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x002B6560 File Offset: 0x002B4760
		private void updateKeyMap()
		{
			if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
			{
				this.yMove -= 5;
				this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
				if (this.yMove < this.yScroll)
				{
					this.yMove = this.yScroll;
				}
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
			{
				this.yMove += 5;
				this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
				if (this.yMove > this.yScroll + 200)
				{
					this.yMove = this.yScroll + 200;
				}
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
			{
				this.xMove -= 5;
				this.cmxMap = this.xMove - this.wScroll / 2;
				if (this.xMove < 16)
				{
					this.xMove = 16;
				}
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
			{
				this.xMove += 5;
				this.cmxMap = this.xMove - this.wScroll / 2;
				if (this.xMove > 250)
				{
					this.xMove = 250;
				}
			}
			if (GameCanvas.isPointerDown)
			{
				this.pointerIsDowning = true;
				if (!this.trans)
				{
					this.pa1 = this.cmxMap;
					this.pa2 = this.cmyMap;
					this.trans = true;
				}
				this.cmxMap = this.pa1 + (GameCanvas.pxLast - GameCanvas.px);
				this.cmyMap = this.pa2 + (GameCanvas.pyLast - GameCanvas.py);
			}
			if (GameCanvas.isPointerJustRelease)
			{
				this.trans = false;
				GameCanvas.pxLast = GameCanvas.px;
				GameCanvas.pyLast = GameCanvas.py;
				this.pX = GameCanvas.pxLast + this.cmxMap;
				this.pY = GameCanvas.pyLast + this.cmyMap;
			}
			if (GameCanvas.isPointerClick)
			{
				this.pointerIsDowning = false;
			}
			if (this.cmxMap < 0)
			{
				this.cmxMap = 0;
			}
			if (this.cmxMap > this.cmxMapLim)
			{
				this.cmxMap = this.cmxMapLim;
			}
			if (this.cmyMap < 0)
			{
				this.cmyMap = 0;
			}
			if (this.cmyMap > this.cmyMapLim)
			{
				this.cmyMap = this.cmyMapLim;
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x002B67D0 File Offset: 0x002B49D0
		private void updateKeyCombine()
		{
			if (this.currentTabIndex == 0)
			{
				this.updateKeyScrollView();
				this.keyTouchCombine = -1;
				if (this.selected == this.vItemCombine.size() && GameCanvas.isPointerClick)
				{
					GameCanvas.isPointerClick = false;
					this.keyTouchCombine = 1;
				}
			}
			if (this.currentTabIndex == 1)
			{
				this.updateKeyScrollView();
			}
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x002B6828 File Offset: 0x002B4A28
		private void updateKeyQuest()
		{
			if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
			{
				this.cmyQuest -= 5;
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
			{
				this.cmyQuest += 5;
			}
			if (this.cmyQuest < 0)
			{
				this.cmyQuest = 0;
			}
			int num = this.indexRowMax * 12 - (this.hScroll - 60);
			if (num < 0)
			{
				num = 0;
			}
			if (this.cmyQuest > num)
			{
				this.cmyQuest = num;
			}
			if (this.scroll != null)
			{
				if (!GameCanvas.isTouch)
				{
					this.scroll.cmy = this.cmyQuest;
				}
				this.scroll.updateKey();
			}
			int num2 = this.xScroll + this.wScroll / 2 - 35;
			int num3 = (GameCanvas.h <= 300) ? 15 : 20;
			int num4 = this.yScroll + this.hScroll - num3 - 15;
			int px = GameCanvas.px;
			int py = GameCanvas.py;
			this.keyTouchMapButton = -1;
			if (Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && px >= num2 && px <= num2 + 70 && py >= num4 && py <= num4 + 30 && (this.scroll == null || !this.scroll.pointerIsDowning))
			{
				this.keyTouchMapButton = 1;
				if (GameCanvas.isPointerJustRelease)
				{
					SoundMn.gI().buttonClick();
					this.waitToPerform = 2;
					GameCanvas.clearAllPointerEvent();
				}
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x002B6994 File Offset: 0x002B4B94
		private void getCurrClanOtion()
		{
			this.isClanOption = false;
			if (this.type != 0 || this.mainTabName.Length != 5 || this.currentTabIndex != 3)
			{
				return;
			}
			this.isClanOption = false;
			if (this.selected == 0)
			{
				this.currClanOption = new int[this.clansOption.Length];
				for (int i = 0; i < this.currClanOption.Length; i++)
				{
					this.currClanOption[i] = i;
				}
				if (!this.isViewMember)
				{
					this.isClanOption = true;
					return;
				}
			}
			else if (this.selected != 1 && !this.isSearchClan && this.selected > 0)
			{
				this.currClanOption = new int[1];
				for (int j = 0; j < this.currClanOption.Length; j++)
				{
					this.currClanOption[j] = j;
				}
				this.isClanOption = true;
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x002B6A5C File Offset: 0x002B4C5C
		private void updateKeyClansOption()
		{
			if (this.currClanOption == null)
			{
				return;
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				this.currMess = this.getCurrMessage();
				this.cSelected--;
				if (this.selected == 0 && this.cSelected < 0)
				{
					this.cSelected = this.currClanOption.Length - 1;
				}
				if (this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected < 0)
				{
					this.cSelected = this.currMess.option.Length - 1;
					return;
				}
			}
			else if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				this.currMess = this.getCurrMessage();
				this.cSelected++;
				if (this.selected == 0 && this.cSelected > this.currClanOption.Length - 1)
				{
					this.cSelected = 0;
				}
				if (this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected > this.currMess.option.Length - 1)
				{
					this.cSelected = 0;
				}
			}
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x002B6B98 File Offset: 0x002B4D98
		private void updateKeyClans()
		{
			this.updateKeyScrollView();
			this.updateKeyClansOption();
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x002B6BA8 File Offset: 0x002B4DA8
		private void checkOptionSelect()
		{
			try
			{
				if (this.type == 0 && this.currentTabIndex == 3 && this.mainTabName.Length == 5 && this.selected != -1)
				{
					int num = 0;
					if (this.selected == 0)
					{
						num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
						this.cSelected = (GameCanvas.px - num) / this.TAB_W;
					}
					else
					{
						this.currMess = this.getCurrMessage();
						if (this.currMess != null && this.currMess.option != null)
						{
							num = this.xScroll + this.wScroll - 2 - this.currMess.option.Length * 40;
							this.cSelected = (GameCanvas.px - num) / 40;
						}
					}
					if (GameCanvas.px < num)
					{
						this.cSelected = -1;
					}
				}
			}
			catch (Exception ex)
			{
				Res.outz("Throw err " + ex.StackTrace);
			}
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x002B6CAC File Offset: 0x002B4EAC
		public void updateScroolMouse(int a)
		{
			bool flag = false;
			if (GameCanvas.pxMouse > this.wScroll)
			{
				return;
			}
			if (this.indexMouse == -1)
			{
				this.indexMouse = this.selected;
			}
			if (a > 0)
			{
				this.indexMouse -= a;
				flag = true;
			}
			else if (a < 0)
			{
				this.indexMouse += -a;
				flag = true;
			}
			if (this.indexMouse < 0)
			{
				this.indexMouse = 0;
			}
			if (flag)
			{
				this.cmtoY = this.indexMouse * 12;
				if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x002B6D58 File Offset: 0x002B4F58
		private void updateKeyScrollView222222()
		{
			if (this.currentListLength <= 0)
			{
				return;
			}
			bool flag = false;
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				flag = true;
				if (this.isTabInven() && this.isnewInventory)
				{
					if (this.selected > 0 && this.sellectInventory == 0)
					{
						this.selected--;
					}
				}
				else
				{
					this.selected--;
					if (this.type == 24 || this.type == 27)
					{
						this.selected -= 2;
						if (this.selected < 0)
						{
							this.selected = 0;
						}
					}
					else if (this.selected < 0)
					{
						if (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1)
						{
							InfoDlg.showWait();
							if (this.currPageShop[this.currentTabIndex] <= 0)
							{
								Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
								return;
							}
							Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
							return;
						}
						else
						{
							this.selected = this.currentListLength - 1;
							if (this.isClanOption)
							{
								this.selected = -1;
							}
							if (this.size_tab > 0)
							{
								this.selected = -1;
							}
						}
					}
					this.lastSelect[this.currentTabIndex] = this.selected;
					this.cSelected = 0;
					this.getCurrClanOtion();
				}
			}
			else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				flag = true;
				if (this.isTabInven() && this.isnewInventory)
				{
					if (this.selected < 1 && this.sellectInventory == 0)
					{
						this.selected++;
					}
				}
				else
				{
					this.selected++;
					if (this.type == 24 || this.type == 27)
					{
						this.selected += 2;
						if (this.selected > this.currentListLength - 1)
						{
							this.selected = this.currentListLength - 1;
						}
					}
					else if (this.selected > this.currentListLength - 1)
					{
						if (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1)
						{
							InfoDlg.showWait();
							if (this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1)
							{
								Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
								return;
							}
							Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
							return;
						}
						else
						{
							this.selected = 0;
						}
					}
					this.lastSelect[this.currentTabIndex] = this.selected;
					this.cSelected = 0;
					this.getCurrClanOtion();
				}
			}
			if (this.isnewInventory && GameCanvas.keyPressed[5] && this.itemInvenNew != null)
			{
				this.pointerDownTime = 0;
				this.waitToPerform = 2;
			}
			if (flag)
			{
				this.cmtoY = this.selected * this.ITEM_HEIGHT - this.hScroll / 2;
				if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
				this.cmy = this.cmtoY;
			}
			if (GameCanvas.isPointerDown)
			{
				this.justRelease = false;
				if (!this.pointerIsDowning && GameCanvas.isPointer(this.xScroll, this.yScroll, this.wScroll, this.hScroll))
				{
					for (int i = 0; i < this.pointerDownLastX.Length; i++)
					{
						this.pointerDownLastX[0] = GameCanvas.py;
					}
					this.pointerDownFirstX = GameCanvas.py;
					this.pointerIsDowning = true;
					this.isDownWhenRunning = (this.cmRun != 0);
					this.cmRun = 0;
				}
				else if (this.pointerIsDowning)
				{
					this.pointerDownTime++;
					if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning)
					{
						this.pointerDownFirstX = -1000;
						this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
						if (this.selected >= this.currentListLength)
						{
							this.selected = -1;
						}
						this.checkOptionSelect();
					}
					else
					{
						this.indexMouse = -1;
					}
					int num = GameCanvas.py - this.pointerDownLastX[0];
					if (num != 0 && this.selected != -1)
					{
						this.selected = -1;
						this.cSelected = -1;
					}
					for (int num2 = this.pointerDownLastX.Length - 1; num2 > 0; num2--)
					{
						this.pointerDownLastX[num2] = this.pointerDownLastX[num2 - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.py;
					this.cmtoY -= num;
					if (this.cmtoY < 0)
					{
						this.cmtoY = 0;
					}
					if (this.cmtoY > this.cmyLim)
					{
						this.cmtoY = this.cmyLim;
					}
					if (this.cmy < 0 || this.cmy > this.cmyLim)
					{
						num /= 2;
					}
					this.cmy -= num;
					if (this.cmy < -(GameCanvas.h / 3))
					{
						this.wantUpdateList = true;
					}
					else
					{
						this.wantUpdateList = false;
					}
					if (this.isnewInventory)
					{
						int num3 = GameCanvas.px - this.xScroll;
						int num4 = GameCanvas.py - this.yScroll;
						this.sellectInventory = num4 / 34 * 5 + num3 / 34;
					}
				}
			}
			if (!GameCanvas.isPointerJustRelease || !this.pointerIsDowning)
			{
				return;
			}
			this.justRelease = true;
			int i2 = GameCanvas.py - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			if (Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
			{
				this.cmRun = 0;
				this.cmtoY = this.cmy;
				this.pointerDownFirstX = -1000;
				this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
				if (this.selected >= this.currentListLength)
				{
					this.selected = -1;
				}
				this.checkOptionSelect();
				this.pointerDownTime = 0;
				this.waitToPerform = 10;
				if (this.isnewInventory)
				{
					this.waitToPerform = -1;
				}
				SoundMn.gI().panelClick();
			}
			else if (this.selected != -1 && this.pointerDownTime > 5)
			{
				this.pointerDownTime = 0;
				this.waitToPerform = 1;
			}
			else if (this.selected == -1 && !this.isDownWhenRunning)
			{
				if (this.cmy < 0)
				{
					this.cmtoY = 0;
				}
				else if (this.cmy > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				else
				{
					int num5 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					num5 = ((num5 > 10) ? 10 : ((num5 < -10) ? -10 : 0));
					this.cmRun = -num5 * 100;
				}
			}
			if ((this.isTabInven() || this.type == 13) && GameCanvas.py < this.yScroll + 21)
			{
				this.selected = 0;
				this.updateKeyInvenTab();
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x002B74E8 File Offset: 0x002B56E8
		private void updateKeyScrollView()
		{
			if (this.currentListLength <= 0)
			{
				return;
			}
			if (!ModFunc.isInventory)
			{
				this.updateKeyScrollView222222();
				return;
			}
			bool flag = false;
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				flag = true;
				this.selected--;
				if (this.type == 24)
				{
					this.selected -= 2;
					if (this.selected < 0)
					{
						this.selected = 0;
					}
				}
				else if (this.type == 28)
				{
					this.selected -= 2;
					if (this.selected < 0)
					{
						this.selected = 0;
					}
				}
				else if (this.selected < 0)
				{
					if (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1)
					{
						InfoDlg.showWait();
						if (this.currPageShop[this.currentTabIndex] <= 0)
						{
							Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
							return;
						}
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
						return;
					}
					else
					{
						this.selected = this.currentListLength - 1;
						if (this.isTabInven())
						{
							this.selected = Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length - 1;
						}
						else if (this.isTabBox())
						{
							this.selected = Char.myCharz().arrItemBox.Length - 1;
						}
						if (this.isClanOption)
						{
							this.selected = -1;
						}
						if (this.size_tab > 0)
						{
							this.selected = -1;
						}
					}
				}
				this.lastSelect[this.currentTabIndex] = this.selected;
				this.cSelected = 0;
				this.getCurrClanOtion();
			}
			else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				flag = true;
				this.selected++;
				if (this.type == 24 || this.type == 28)
				{
					this.selected += 2;
					if (this.selected > this.currentListLength - 1)
					{
						this.selected = this.currentListLength - 1;
					}
				}
				else if (this.isTabInven() && this.selected >= this.currentListLength - Char.myCharz().arrItemBag.Length / this.CountBoxInRow)
				{
					if (this.selected >= Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length)
					{
						this.selected = 0;
					}
				}
				else if (this.isTabBox() && this.selected >= this.currentListLength)
				{
					if (this.selected >= Char.myCharz().arrItemBox.Length)
					{
						this.selected = 0;
					}
				}
				else if (this.selected > this.currentListLength - 1)
				{
					if (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1)
					{
						InfoDlg.showWait();
						if (this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1)
						{
							Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
							return;
						}
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
						return;
					}
					else
					{
						this.selected = 0;
					}
				}
				this.lastSelect[this.currentTabIndex] = this.selected;
				this.cSelected = 0;
				this.getCurrClanOtion();
			}
			if (flag)
			{
				int s = this.selected;
				if (this.isTabInven() && this.selected >= this.currentListLength - Char.myCharz().arrItemBag.Length / this.CountBoxInRow)
				{
					s = (this.selected - Char.myCharz().arrItemBody.Length) / this.CountBoxInRow + Char.myCharz().arrItemBody.Length;
				}
				else if (this.isTabBox())
				{
					s = this.selected / this.CountBoxInRow;
				}
				this.cmtoY = s * this.ITEM_HEIGHT - this.hScroll / 2;
				if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
				this.cmy = this.cmtoY;
			}
			if (GameCanvas.isPointerDown)
			{
				this.justRelease = false;
				if (!this.pointerIsDowning && GameCanvas.isPointer(this.xScroll, this.yScroll, this.wScroll, this.hScroll))
				{
					for (int i = 0; i < this.pointerDownLastX.Length; i++)
					{
						this.pointerDownLastX[0] = GameCanvas.py;
					}
					this.pointerDownFirstX = GameCanvas.py;
					this.pointerIsDowning = true;
					this.isDownWhenRunning = (this.cmRun != 0);
					this.cmRun = 0;
				}
				else if (this.pointerIsDowning)
				{
					this.pointerDownTime++;
					if (this.pointerDownTime > this.CountBoxInRow && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning)
					{
						this.pointerDownFirstX = -1000;
						int row = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
						this.selected = row;
						if (this.selected >= this.currentListLength)
						{
							this.selected = -1;
						}
						else if (this.isTabInven() && this.selected >= this.currentListLength - Char.myCharz().arrItemBag.Length / this.CountBoxInRow - 1)
						{
							int row2 = (this.selected - Char.myCharz().arrItemBody.Length) * (this.CountBoxInRow - 1);
							int column = (GameCanvas.px - this.xScroll) / (this.WidthBoxNew + 1);
							this.selected = this.selected + row2 + column;
						}
						else if (this.isTabBox())
						{
							int row3 = this.selected * (this.CountBoxInRow - 1);
							int column2 = (GameCanvas.px - this.xScroll) / (this.WidthBoxNew + 1);
							this.selected = this.selected + row3 + column2;
						}
						this.checkOptionSelect();
					}
					else
					{
						this.indexMouse = -1;
					}
					int num = GameCanvas.py - this.pointerDownLastX[0];
					if (num != 0 && this.selected != -1)
					{
						this.selected = -1;
						this.cSelected = -1;
					}
					for (int num2 = this.pointerDownLastX.Length - 1; num2 > 0; num2--)
					{
						this.pointerDownLastX[num2] = this.pointerDownLastX[num2 - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.py;
					this.cmtoY -= num;
					if (this.cmtoY < 0)
					{
						this.cmtoY = 0;
					}
					if (this.cmtoY > this.cmyLim)
					{
						this.cmtoY = this.cmyLim;
					}
					if (this.cmy < 0 || this.cmy > this.cmyLim)
					{
						num /= 2;
					}
					this.cmy -= num;
					if (this.cmy < -(GameCanvas.h / 3))
					{
						this.wantUpdateList = true;
					}
					else
					{
						this.wantUpdateList = false;
					}
				}
			}
			if (!GameCanvas.isPointerJustRelease || !this.pointerIsDowning)
			{
				return;
			}
			this.justRelease = true;
			int i2 = GameCanvas.py - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			if (Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
			{
				this.cmRun = 0;
				this.cmtoY = this.cmy;
				this.pointerDownFirstX = -1000;
				int row4 = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
				this.selected = row4;
				if (this.selected >= this.currentListLength)
				{
					this.selected = -1;
				}
				else if (this.isTabInven() && this.selected >= this.currentListLength - Char.myCharz().arrItemBag.Length / this.CountBoxInRow)
				{
					int row5 = (this.selected - Char.myCharz().arrItemBody.Length) * (this.CountBoxInRow - 1);
					int column3 = (GameCanvas.px - this.xScroll) / (this.WidthBoxNew + 1);
					this.selected = this.selected + row5 + column3;
				}
				else if (this.isTabBox())
				{
					int row6 = this.selected * (this.CountBoxInRow - 1);
					int column4 = (GameCanvas.px - this.xScroll) / (this.WidthBoxNew + 1);
					this.selected = this.selected + row6 + column4;
				}
				this.checkOptionSelect();
				this.pointerDownTime = 0;
				this.waitToPerform = 10;
				SoundMn.gI().panelClick();
			}
			else if (this.selected != -1 && this.pointerDownTime > 6)
			{
				this.pointerDownTime = 0;
				this.waitToPerform = 1;
			}
			else if (this.selected == -1 && !this.isDownWhenRunning)
			{
				if (this.cmy < 0)
				{
					this.cmtoY = 0;
				}
				else if (this.cmy > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				else
				{
					int num3 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					num3 = ((num3 > 10) ? 10 : ((num3 < -10) ? -10 : 0));
					this.cmRun = -num3 * 100;
				}
			}
			if (this.isTabInven() && GameCanvas.py < this.yScroll + 21)
			{
				this.selected = 0;
				this.updateKeyInvenTab();
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x002B7E9C File Offset: 0x002B609C
		private void updateKeyInTabBar()
		{
			if ((this.scroll != null && this.scroll.pointerIsDowning) || this.pointerIsDowning)
			{
				return;
			}
			int num = this.currentTabIndex;
			if (this.isTabInven() && this.isnewInventory)
			{
				if (this.selected == -1)
				{
					if (GameCanvas.keyPressed[6])
					{
						this.currentTabIndex++;
						if (this.currentTabIndex >= this.currentTabName.Length)
						{
							if (GameCanvas.panel2 != null)
							{
								this.currentTabIndex = this.currentTabName.Length - 1;
								GameCanvas.isFocusPanel2 = true;
							}
							else
							{
								this.currentTabIndex = 0;
							}
						}
						this.selected = this.lastSelect[this.currentTabIndex];
						this.lastTabIndex[this.type] = this.currentTabIndex;
					}
					if (GameCanvas.keyPressed[4])
					{
						this.currentTabIndex--;
						if (this.currentTabIndex < 0)
						{
							this.currentTabIndex = this.currentTabName.Length - 1;
						}
						if (GameCanvas.isFocusPanel2)
						{
							GameCanvas.isFocusPanel2 = false;
						}
						this.selected = this.lastSelect[this.currentTabIndex];
						this.lastTabIndex[this.type] = this.currentTabIndex;
					}
				}
				else if (this.selected > 0)
				{
					if (GameCanvas.keyPressed[8])
					{
						if (this.newSelected == 0)
						{
							this.sellectInventory++;
						}
						else
						{
							this.sellectInventory += 5;
						}
					}
					else if (GameCanvas.keyPressed[2])
					{
						if (this.newSelected == 0)
						{
							this.sellectInventory--;
						}
						else
						{
							this.sellectInventory -= 5;
						}
					}
					else if (GameCanvas.keyPressed[4])
					{
						if (this.newSelected == 0)
						{
							this.sellectInventory -= 5;
						}
						else
						{
							this.sellectInventory--;
						}
					}
					else if (GameCanvas.keyPressed[6])
					{
						if (this.newSelected == 0)
						{
							this.sellectInventory += 5;
						}
						else
						{
							this.sellectInventory++;
						}
					}
				}
				int num2 = this.sellectInventory;
				if (this.sellectInventory == this.nTableItem)
				{
					this.sellectInventory = 0;
				}
			}
			else if (!this.IsTabOption())
			{
				if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
				{
					if (this.isTabInven())
					{
						if (this.selected >= 0 && !ModFunc.isInventory)
						{
							this.updateKeyInvenTab();
							return;
						}
						this.currentTabIndex++;
						if (this.currentTabIndex >= this.currentTabName.Length)
						{
							if (GameCanvas.panel2 != null)
							{
								this.currentTabIndex = this.currentTabName.Length - 1;
								GameCanvas.isFocusPanel2 = true;
							}
							else
							{
								this.currentTabIndex = 0;
							}
						}
						this.selected = this.lastSelect[this.currentTabIndex];
						this.lastTabIndex[this.type] = this.currentTabIndex;
					}
					else
					{
						this.currentTabIndex++;
						if (this.currentTabIndex >= this.currentTabName.Length)
						{
							if (GameCanvas.panel2 != null)
							{
								this.currentTabIndex = this.currentTabName.Length - 1;
								GameCanvas.isFocusPanel2 = true;
							}
							else
							{
								this.currentTabIndex = 0;
							}
						}
						this.selected = this.lastSelect[this.currentTabIndex];
						this.lastTabIndex[this.type] = this.currentTabIndex;
					}
				}
				if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
				{
					this.currentTabIndex--;
					if (this.currentTabIndex < 0)
					{
						this.currentTabIndex = this.currentTabName.Length - 1;
					}
					if (GameCanvas.isFocusPanel2)
					{
						GameCanvas.isFocusPanel2 = false;
					}
					this.selected = this.lastSelect[this.currentTabIndex];
					this.lastTabIndex[this.type] = this.currentTabIndex;
				}
			}
			this.keyTouchTab = -1;
			for (int i = 0; i < this.currentTabName.Length; i++)
			{
				if (GameCanvas.isPointer(this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25))
				{
					this.keyTouchTab = i;
					if (GameCanvas.isPointerJustRelease)
					{
						if (this.type == 8)
						{
							ModFunc.DoChatGlobal();
							break;
						}
						this.currentTabIndex = i;
						this.lastTabIndex[this.type] = i;
						GameCanvas.isPointerJustRelease = false;
						this.selected = this.lastSelect[this.currentTabIndex];
						if (num == this.currentTabIndex && this.cmRun == 0)
						{
							this.cmtoY = 0;
							this.selected = (GameCanvas.isTouch ? -1 : 0);
							break;
						}
						break;
					}
				}
			}
			if (num == this.currentTabIndex)
			{
				return;
			}
			this.size_tab = 0;
			SoundMn.gI().panelClick();
			int num3 = this.type;
			if (num3 <= 13)
			{
				switch (num3)
				{
					case 0:
						if (this.currentTabIndex == 0)
						{
							this.setTabTask();
						}
						if (this.currentTabIndex == 1)
						{
							this.setTabInventory(true);
						}
						if (this.currentTabIndex == 2)
						{
							this.setTabSkill();
						}
						if (this.currentTabIndex == 3)
						{
							if (this.mainTabName.Length > 4)
							{
								this.setTabClans();
							}
							else
							{
								this.setTabTool();
							}
						}
						if (this.currentTabIndex == 4)
						{
							this.setTabTool();
						}
						break;
					case 1:
						this.setTabShop();
						break;
					case 2:
						if (this.currentTabIndex == 0)
						{
							this.setTabBox();
						}
						if (this.currentTabIndex == 1)
						{
							this.setTabInventory(true);
						}
						break;
					case 3:
						this.setTabZone();
						break;
					default:
						if (num3 != 12)
						{
							if (num3 == 13)
							{
								if (this.currentTabIndex == 0)
								{
									if (this.Equals(GameCanvas.panel))
									{
										this.setTabInventory(true);
									}
									else if (this.Equals(GameCanvas.panel2))
									{
										this.setTabGiaoDich(false);
									}
								}
								if (this.currentTabIndex == 1)
								{
									this.setTabGiaoDich(true);
								}
								if (this.currentTabIndex == 2)
								{
									this.setTabGiaoDich(false);
								}
							}
						}
						else
						{
							if (this.currentTabIndex == 0)
							{
								this.setTabCombine();
							}
							if (this.currentTabIndex == 1)
							{
								this.setTabInventory(true);
							}
						}
						break;
				}
			}
			else
			{
				if (num3 != 21)
				{
					if (num3 == 25)
					{
						this.setTabSpeacialSkill();
						goto IL_645;
					}
					if (num3 != 28)
					{
						goto IL_645;
					}
				}
				if (this.currentTabIndex == 0)
				{
					this.setTabPetInventory(this.type == 28);
				}
				else if (this.currentTabIndex == 1)
				{
					this.setTabPetSkill(this.type == 28);
				}
				else if (this.currentTabIndex == 2)
				{
					this.setTabPetStatus();
				}
				else if (this.currentTabIndex == 3)
				{
					this.setTabInventory(true);
				}
			}
		IL_645:
			this.selected = this.lastSelect[this.currentTabIndex];
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x002B8504 File Offset: 0x002B6704
		private void setTabPetStatus()
		{
			this.currentListLength = this.strStatus.Length;
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x002B85BC File Offset: 0x002B67BC
		private void setTabPetSkill(bool isPet2)
		{
			this.ITEM_HEIGHT = 30;
			this.currentListLength = (isPet2 ? Char.MyPet2z() : Char.myPetz()).arrPetSkill.Length + 5;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = this.cmyLim;
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x002B867C File Offset: 0x002B687C
		private void setTabTool()
		{
			SoundMn.gI().getSoundOption();
			this.currentListLength = Panel.strTool.Length;
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x002B873C File Offset: 0x002B693C
		public void initTabClans()
		{
			if (this.isSearchClan)
			{
				this.currentListLength = ((this.clans != null) ? (this.clans.Length + 2) : 2);
				this.clanInfo = mResources.clan_list;
			}
			else if (this.isViewMember)
			{
				this.clanReport = string.Empty;
				this.currentListLength = ((this.member != null) ? this.member.size() : this.myMember.size()) + 2;
				this.clanInfo = mResources.member + " " + ((this.currClan == null) ? Char.myCharz().clan.name : this.currClan.name);
			}
			else if (this.isMessage)
			{
				this.currentListLength = ClanMessage.vMessage.size() + 2;
				this.clanInfo = mResources.msg;
				this.clanReport = string.Empty;
			}
			if (Char.myCharz().clan == null)
			{
				this.clansOption = new string[][]
				{
					mResources.findClan,
					mResources.createClan
				};
			}
			else if (!this.isViewMember)
			{
				if (this.myMember.size() > 1)
				{
					this.clansOption = new string[][]
					{
						mResources.chatClan,
						mResources.request_pea2,
						mResources.memberr
					};
				}
				else
				{
					this.clansOption = new string[][]
					{
						mResources.memberr
					};
				}
			}
			else if (Char.myCharz().role > 0)
			{
				this.clansOption = new string[][]
				{
					mResources.msgg,
					mResources.leaveClan
				};
			}
			else if (this.myMember.size() > 1)
			{
				this.clansOption = new string[][]
				{
					mResources.msgg,
					mResources.leaveClan,
					mResources.khau_hieuu,
					mResources.bieu_tuongg
				};
			}
			else
			{
				this.clansOption = new string[][]
				{
					mResources.msgg,
					mResources.khau_hieuu,
					mResources.bieu_tuongg
				};
			}
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x002B89B4 File Offset: 0x002B6BB4
		public void setTabClans()
		{
			GameScr.isNewClanMessage = false;
			this.ITEM_HEIGHT = 24;
			if (this.lastSelect != null && this.lastSelect[3] == 0)
			{
				this.lastSelect[3] = -1;
			}
			this.currentListLength = 2;
			if (Char.myCharz().clan != null)
			{
				this.isMessage = true;
				this.isViewMember = false;
				this.isSearchClan = false;
			}
			else
			{
				this.isMessage = false;
				this.isViewMember = false;
				this.isSearchClan = true;
			}
			if (Char.myCharz().clan != null)
			{
				this.currentListLength = ClanMessage.vMessage.size() + 2;
			}
			this.initTabClans();
			this.cSelected = -1;
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
			}
			if (Char.myCharz().clan == null)
			{
				this.clanReport = mResources.findingClan;
				Service.gI().searchClan(string.Empty);
			}
			this.selected = this.lastSelect[this.currentTabIndex];
			if (GameCanvas.isTouch)
			{
				this.selected = -1;
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x002B8AF4 File Offset: 0x002B6CF4
		public void initLogMessage()
		{
			this.currentListLength = this.logChat.size() + 1;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x002B8BA8 File Offset: 0x002B6DA8
		private void paintItemStar(mGraphics g, string opt, int x, int y)
		{
			if (this.imgStarItem == null)
			{
				this.imgStarItem = GameCanvas.loadImage("/mainImage/star.png");
				return;
			}
			g.drawImage(this.imgStarItem, x, y);
			mFont.tahoma_7b_yellow.drawString(g, opt, x, y, mFont.RIGHT, mFont.tahoma_7b_green2);
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x002B8BF6 File Offset: 0x002B6DF6
		private void setTabMessage()
		{
			this.ITEM_HEIGHT = 24;
			this.initLogMessage();
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x002B8C18 File Offset: 0x002B6E18
		public void setTabShop()
		{
			this.ITEM_HEIGHT = 24;
			if (this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2)
			{
				this.currentListLength = this.checkCurrentListLength(Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length);
			}
			else
			{
				this.currentListLength = Char.myCharz().arrItemShop[this.currentTabIndex].Length;
			}
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x002B8D24 File Offset: 0x002B6F24
		private void setTabSkill()
		{
			this.ITEM_HEIGHT = 30;
			this.currentListLength = Char.myCharz().nClass.skillTemplates.Length + 6;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = this.cmyLim;
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x002B8DDC File Offset: 0x002B6FDC
		private void setTabMapTrans()
		{
			this.ITEM_HEIGHT = 24;
			this.currentListLength = this.mapNames.Length;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = this.cmyLim;
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x002B8E8C File Offset: 0x002B708C
		private void setTabZone()
		{
			this.ITEM_HEIGHT = 24;
			this.currentListLength = GameScr.gI().zones.Length;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			this.cmy = (this.cmtoY = 0);
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x002B8EF0 File Offset: 0x002B70F0
		private void setTabBox()
		{
			if (!ModFunc.isInventory)
			{
				this.currentListLength = this.checkCurrentListLengthNew(Char.myCharz().arrItemBox.Length);
				this.ITEM_HEIGHT = 25;
				this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll + 8;
				if (this.cmyLim < 0)
				{
					this.cmyLim = 9;
				}
				this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
				if (this.cmy < 0)
				{
					this.cmy = (this.cmtoY = 0);
				}
				if (this.cmy > this.cmyLim)
				{
					this.cmy = (this.cmtoY = this.cmyLim);
				}
				this.selected = (GameCanvas.isTouch ? -1 : 0);
				return;
			}
			this.currentListLength = this.checkCurrentListLengthNew(Char.myCharz().arrItemBox.Length / 5);
			this.ITEM_HEIGHT = 25;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll + 8;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 9;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x002B9078 File Offset: 0x002B7278
		private void setTabPetInventory(bool isPet2)
		{
			this.ITEM_HEIGHT = 25;
			Item[] arrItemBody = (isPet2 ? Char.MyPet2z() : Char.myPetz()).arrItemBody;
			this.currentListLength = arrItemBody.Length;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = 0);
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x002B913C File Offset: 0x002B733C
		public void setTabInventory(bool resetSelect)
		{
			if (!ModFunc.isInventory)
			{
				this.currentListLength = this.checkCurrentListLength(Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length);
				this.ITEM_HEIGHT = 25;
				this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
				this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
				if (this.cmyLim < 0)
				{
					this.cmyLim = 0;
				}
				if (this.cmy < 0)
				{
					this.cmy = (this.cmtoY = 0);
				}
				if (this.cmy > this.cmyLim)
				{
					this.cmy = (this.cmtoY = 0);
				}
				if (resetSelect)
				{
					this.selected = (GameCanvas.isTouch ? -1 : 0);
					return;
				}
			}
			else
			{
				this.currentListLength = Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length;
				this.ITEM_HEIGHT = 25;
				this.cmyLim = (Char.myCharz().arrItemBag.Length / 5 + Char.myCharz().arrItemBody.Length) * this.ITEM_HEIGHT - this.hScroll;
				this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
				if (this.cmyLim < 0)
				{
					this.cmyLim = 0;
				}
				if (this.cmy < 0)
				{
					this.cmy = (this.cmtoY = 0);
				}
				if (this.cmy > this.cmyLim)
				{
					this.cmy = (this.cmtoY = 0);
				}
				if (resetSelect)
				{
					this.selected = (GameCanvas.isTouch ? -1 : 0);
				}
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x002B92E4 File Offset: 0x002B74E4
		private void setTabMap()
		{
			if (!Panel.isPaintMap)
			{
				return;
			}
			if (TileMap.lastPlanetId != TileMap.planetID)
			{
				Res.outz("LOAD TAM HINH");
				Panel.imgMap = GameCanvas.loadImageRMS("/img/map" + TileMap.planetID.ToString() + ".png");
				TileMap.lastPlanetId = TileMap.planetID;
			}
			this.cmxMap = this.getXMap() - this.wScroll / 2;
			this.cmyMap = this.getYMap() + this.yScroll - (this.yScroll + this.hScroll / 2);
			this.pa1 = this.cmxMap;
			this.pa2 = this.cmyMap;
			this.cmxMapLim = 250 - this.wScroll;
			this.cmyMapLim = 220 - this.hScroll;
			if (this.cmxMapLim < 0)
			{
				this.cmxMapLim = 0;
			}
			if (this.cmyMapLim < 0)
			{
				this.cmyMapLim = 0;
			}
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				if (TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i])
				{
					this.xMove = Panel.mapX[(int)TileMap.planetID][i] + this.xScroll;
					this.yMove = Panel.mapY[(int)TileMap.planetID][i] + this.yScroll + 5;
					break;
				}
			}
			this.xMap = this.getXMap() + this.xScroll;
			this.yMap = this.getYMap() + this.yScroll;
			this.xMapTask = this.getXMapTask() + this.xScroll;
			this.yMapTask = this.getYMapTask() + this.yScroll;
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x002B948B File Offset: 0x002B768B
		private void setTabTask()
		{
			this.cmyQuest = 0;
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x002B9494 File Offset: 0x002B7694
		public void moveCamera()
		{
			if (this.timeShow > 0)
			{
				this.timeShow--;
			}
			if (this.justRelease && this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1)
			{
				if (this.cmy < -50)
				{
					InfoDlg.showWait();
					this.justRelease = false;
					if (this.currPageShop[this.currentTabIndex] <= 0)
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
					}
					else
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
					}
				}
				else if (this.cmy > this.cmyLim + 50)
				{
					this.justRelease = false;
					InfoDlg.showWait();
					if (this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1)
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
					}
					else
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
					}
				}
			}
			if (this.cmx != this.cmtoX && !this.pointerIsDowning)
			{
				this.cmvx = this.cmtoX - this.cmx << 2;
				this.cmdx += this.cmvx;
				this.cmx += this.cmdx >> 3;
				this.cmdx &= 15;
			}
			if (Math.abs(this.cmtoX - this.cmx) < 10)
			{
				this.cmx = this.cmtoX;
			}
			if (this.isClose)
			{
				this.isClose = false;
				this.cmtoX = this.wScroll;
			}
			if (this.cmtoX >= this.wScroll - 10 && this.cmx >= this.wScroll - 10 && this.position == 0)
			{
				this.isShow = false;
				this.cleanCombine();
				if (this.isChangeZone)
				{
					this.isChangeZone = false;
					if (Char.myCharz().cHP > 0L && Char.myCharz().statusMe != 14)
					{
						InfoDlg.showWait();
						if (this.type == 3)
						{
							Service.gI().requestChangeZone(this.selected, -1);
						}
						else if (this.type == 14)
						{
							AutoXmap.SelectMapTrans(this.selected);
						}
					}
				}
				if (this.isSelectPlayerMenu)
				{
					this.isSelectPlayerMenu = false;
					int num = this.vPlayerMenu.size() - this.vPlayerMenu_id.size();
					if (Char.myCharz().charFocus != null)
					{
						if (this.selected - num < 0)
						{
							Char.myCharz().charFocus.menuSelect = this.selected;
						}
						else
						{
							Char.myCharz().charFocus.menuSelect = (int)short.Parse((string)this.vPlayerMenu_id.elementAt(this.selected - num));
						}
					}
					((Command)this.vPlayerMenu.elementAt(this.selected)).performAction();
				}
				this.vPlayerMenu.removeAllElements();
				this.charMenu = null;
			}
			if (this.cmRun != 0 && !this.pointerIsDowning)
			{
				this.cmtoY += this.cmRun / 100;
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
				else if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				else
				{
					this.cmy = this.cmtoY;
				}
				this.cmRun = this.cmRun * 9 / 10;
				if (this.cmRun < 100 && this.cmRun > -100)
				{
					this.cmRun = 0;
				}
			}
			if (this.cmy != this.cmtoY && !this.pointerIsDowning)
			{
				this.cmvy = this.cmtoY - this.cmy << 2;
				this.cmdy += this.cmvy;
				this.cmy += this.cmdy >> 4;
				this.cmdy &= 15;
			}
			this.cmyLast[this.currentTabIndex] = this.cmy;
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x002B98D0 File Offset: 0x002B7AD0
		public void paintDetail(mGraphics g)
		{
			if (this.cp == null || this.cp.says == null)
			{
				return;
			}
			this.cp.paint(g);
			int num = this.cp.cx + 13;
			int num2 = this.cp.cy + 11;
			if (this.type == 15)
			{
				num += 5;
				num2 += 26;
			}
			if (this.type == 0 && this.currentTabIndex == 3)
			{
				if (this.isSearchClan)
				{
					num -= 5;
				}
				else if (this.partID != null || this.charInfo != null)
				{
					num = this.cp.cx + 21;
					num2 = this.cp.cy + 40;
				}
			}
			if (this.partID != null)
			{
				Part part = GameScr.parts[this.partID[0]];
				Part part2 = GameScr.parts[this.partID[1]];
				Part part3 = GameScr.parts[this.partID[2]];
				SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[0][0][0]].id, num + Char.CharInfo[0][0][1] + (int)part.pi[Char.CharInfo[0][0][0]].dx, num2 - Char.CharInfo[0][0][2] + (int)part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part2.pi[Char.CharInfo[0][1][0]].id, num + Char.CharInfo[0][1][1] + (int)part2.pi[Char.CharInfo[0][1][0]].dx, num2 - Char.CharInfo[0][1][2] + (int)part2.pi[Char.CharInfo[0][1][0]].dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part3.pi[Char.CharInfo[0][2][0]].id, num + Char.CharInfo[0][2][1] + (int)part3.pi[Char.CharInfo[0][2][0]].dx, num2 - Char.CharInfo[0][2][2] + (int)part3.pi[Char.CharInfo[0][2][0]].dy, 0, 0);
			}
			else if (this.charInfo != null)
			{
				this.charInfo.paintCharBody(g, num + 5, num2 + 25, 1, 0, true);
			}
			else if (this.idIcon != -1)
			{
				SmallImage.drawSmallImage(g, this.idIcon, num, num2, 0, 3);
			}
			if (this.currItem != null && this.currItem.template.type != 5)
			{
				if (this.currItem.compare > 0)
				{
					g.drawImage(Panel.imgUp, num - 7, num2 + 13, 3);
					mFont.tahoma_7b_green.drawString(g, Res.abs(this.currItem.compare).ToString() + string.Empty, num + 1, num2 + 8, 0);
					return;
				}
				if (this.currItem.compare < 0 && this.currItem.compare != -1)
				{
					g.drawImage(Panel.imgDown, num - 7, num2 + 13, 3);
					mFont.tahoma_7b_red.drawString(g, Res.abs(this.currItem.compare).ToString() + string.Empty, num + 1, num2 + 8, 0);
				}
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x002B9C0C File Offset: 0x002B7E0C
		public void paintTop(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			g.setColor(0);
			if (this.currentListLength == 0)
			{
				return;
			}
			int num = (this.cmy + this.hScroll) / 24 + 1;
			if (num < this.hScroll / 24 + 1)
			{
				num = this.hScroll / 24 + 1;
			}
			if (num > this.currentListLength)
			{
				num = this.currentListLength;
			}
			int num2 = this.cmy / 24;
			if (num2 >= num)
			{
				num2 = num - 1;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			for (int i = num2; i < num; i++)
			{
				int num3 = this.xScroll;
				int num4 = this.yScroll + i * this.ITEM_HEIGHT;
				int num5 = 29;
				int h = this.ITEM_HEIGHT - 1;
				int num6 = this.xScroll + num5;
				int num7 = this.yScroll + i * this.ITEM_HEIGHT;
				int num8 = this.wScroll - num5;
				int num9 = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num6, num7, num8, num9, 5);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num3, num4, num5, h, 5);
				TopInfo topInfo = (TopInfo)this.vTop.elementAt(i);
				if (topInfo.headICON != -1)
				{
					SmallImage.drawSmallImage(g, topInfo.headICON, num3, num4, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[topInfo.headID];
					SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[0][0][0]].id, num3 + (int)part.pi[Char.CharInfo[0][0][0]].dx, num4 + num9 - 1, 0, mGraphics.BOTTOM | mGraphics.LEFT);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				if (topInfo.pId != Char.myCharz().charID)
				{
					mFont.tahoma_7b_green.drawString(g, topInfo.name, num6 + 5, num7, 0);
				}
				else
				{
					mFont.tahoma_7b_red.drawString(g, topInfo.name, num6 + 5, num7, 0);
				}
				mFont.tahoma_7_blue.drawString(g, topInfo.info, num6 + num8 - 5, num7 + 11, 1);
				mFont.tahoma_7_green2.drawString(g, mResources.rank + ": " + topInfo.rank.ToString() + string.Empty, num6 + 5, num7 + 11, 0);
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x002B9EB8 File Offset: 0x002B80B8
		public void paint(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY() + mGraphics.addYWhenOpenKeyBoard);
			g.translate(-this.cmx, 0);
			g.translate(this.X, this.Y);
			if (GameCanvas.panel.combineSuccess != -1)
			{
				if (this.Equals(GameCanvas.panel))
				{
					this.paintCombineEff(g);
				}
				return;
			}
			GameCanvas.paintz.paintFrameSimple(this.X, this.Y, this.W, this.H, g);
			this.paintTopInfo(g);
			this.paintBottomMoneyInfo(g);
			this.paintTab(g);
			switch (this.type)
			{
				case 0:
					if (this.currentTabIndex == 0)
					{
						this.paintTask(g);
					}
					if (this.currentTabIndex == 1)
					{
						this.paintInventory(g);
					}
					if (this.currentTabIndex == 2)
					{
						this.paintSkill(g);
					}
					if (this.currentTabIndex == 3)
					{
						if (this.mainTabName.Length == 4)
						{
							this.paintTools(g);
						}
						else
						{
							this.paintClans(g);
						}
					}
					if (this.currentTabIndex == 4)
					{
						this.paintTools(g);
					}
					break;
				case 1:
					this.paintShop(g);
					break;
				case 2:
					if (this.currentTabIndex == 0)
					{
						this.paintBox(g);
					}
					if (this.currentTabIndex == 1)
					{
						this.paintInventory(g);
					}
					break;
				case 3:
					this.paintZone(g);
					break;
				case 4:
					this.paintMap(g);
					break;
				case 7:
					this.paintInventory(g);
					break;
				case 8:
					this.paintLogChat(g);
					break;
				case 9:
					this.paintArchivement(g);
					break;
				case 10:
					this.paintPlayerMenu(g);
					break;
				case 11:
					this.paintFriend(g);
					break;
				case 12:
					if (this.currentTabIndex == 0)
					{
						this.paintCombine(g);
					}
					if (this.currentTabIndex == 1)
					{
						this.paintInventory(g);
					}
					break;
				case 13:
					if (this.currentTabIndex == 0)
					{
						if (this.Equals(GameCanvas.panel))
						{
							this.paintInventory(g);
						}
						else
						{
							this.paintGiaoDich(g, false);
						}
					}
					if (this.currentTabIndex == 1)
					{
						this.paintGiaoDich(g, true);
					}
					if (this.currentTabIndex == 2)
					{
						this.paintGiaoDich(g, false);
					}
					break;
				case 14:
					this.paintMapTrans(g);
					break;
				case 15:
					this.paintTop(g);
					break;
				case 16:
					this.paintEnemy(g);
					break;
				case 17:
					this.paintShop(g);
					break;
				case 18:
					this.paintFlagChange(g);
					break;
				case 19:
					this.paintOption(g);
					break;
				case 20:
					this.paintAccount(g);
					break;
				case 21:
				case 28:
					if (this.currentTabIndex == 0)
					{
						this.paintPetInventory(g, this.type == 28);
					}
					else if (this.currentTabIndex == 1)
					{
						this.paintPetSkill(g, this.type == 28);
					}
					else if (this.currentTabIndex == 2)
					{
						this.paintPetStatus(g);
					}
					else if (this.currentTabIndex == 3)
					{
						this.paintInventory(g);
					}
					break;
				case 22:
					this.paintAuto(g);
					break;
				case 23:
					this.paintGameInfo(g);
					break;
				case 24:
					this.paintGameSubInfo(g);
					break;
				case 25:
					this.paintSpeacialSkill(g);
					break;
				case 26:
					this.PaintModFunc(g);
					break;
				case 27:
					this.paintPlayerInfo(g);
					break;
			}
			GameScr.resetTranslate(g);
			this.paintDetail(g);
			if (this.cmx == this.cmtoX)
			{
				this.cmdClose.paint(g);
			}
			if (this.tabIcon != null && this.tabIcon.isShow)
			{
				this.tabIcon.paint(g);
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.translate(this.X, this.Y);
			g.translate(-this.cmx, 0);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x002BA2B0 File Offset: 0x002B84B0
		private void paintShop(mGraphics g)
		{
			try
			{
				if (this.type == 1 && this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2)
				{
					this.paintInventory(g);
				}
				else
				{
					g.setColor(16711680);
					g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
					if (this.typeShop == 2 && this.Equals(GameCanvas.panel))
					{
						if (this.currentTabIndex <= 3 && GameCanvas.isTouch)
						{
							if (this.cmy < -50)
							{
								GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + 30, g);
							}
							else if (this.cmy < 0)
							{
								mFont.tahoma_7_grey.drawString(g, mResources.getDown, this.xScroll + this.wScroll / 2, this.yScroll + 15, 2);
							}
							else if (this.cmyLim >= 0)
							{
								if (this.cmy > this.cmyLim + 50)
								{
									GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 30, g);
								}
								else if (this.cmy > this.cmyLim)
								{
									mFont.tahoma_7_grey.drawString(g, mResources.getUp, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 25, 2);
								}
							}
						}
						if (Char.myCharz().arrItemShop[this.currentTabIndex].Length == 0 && this.type != 17)
						{
							mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
							return;
						}
					}
					g.translate(0, -this.cmy);
					Item[] array = Char.myCharz().arrItemShop[this.currentTabIndex];
					if (this.typeShop == 2 && (this.currentTabIndex == 4 || this.type == 17))
					{
						array = Char.myCharz().arrItemShop[4];
						if (array.Length == 0)
						{
							mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
							return;
						}
					}
					int num = array.Length;
					for (int i = 0; i < num; i++)
					{
						int num2 = this.xScroll + 36;
						int num3 = this.yScroll + i * this.ITEM_HEIGHT;
						int num4 = this.wScroll - 36;
						int h = this.ITEM_HEIGHT - 1;
						int num5 = this.xScroll;
						int num6 = this.yScroll + i * this.ITEM_HEIGHT;
						int num7 = 34;
						int num8 = this.ITEM_HEIGHT - 1;
						if (num3 - this.cmy <= this.yScroll + this.hScroll && num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
						{
							g.setColor((i != this.selected) ? 15196114 : 16383818);
							g.fillRect(num2, num3, num4, h, 5);
							g.setColor(6047789, 0.3f);
							g.fillRect(num5, num6, num7, num8, 5);
							Item item = array[i];
							if (item != null)
							{
								string text = string.Empty;
								mFont mFont2 = mFont.tahoma_7_green2;
								if (item.isMe != 0 && this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2))
								{
									mFont2 = mFont.tahoma_7b_green;
								}
								if (item.itemOption != null)
								{
									for (int j = 0; j < item.itemOption.Length; j++)
									{
										if (item.itemOption[j].optionTemplate.id == 72)
										{
											text = " [+" + item.itemOption[j].param.ToString() + "]";
										}
										if (item.itemOption[j].optionTemplate.id == 72)
										{
											if (item.itemOption[j].param >= 1 && item.itemOption[j].param <= 5)
											{
												mFont2 = Panel.GetFont(2);
											}
											else if (item.itemOption[j].param >= 6 && item.itemOption[j].param <= 7)
											{
												mFont2 = Panel.GetFont(8);
											}
											else if (item.itemOption[j].param >= 8 && item.itemOption[j].param <= 10)
											{
												mFont2 = Panel.GetFont(7);
											}
										}
									}
								}
								if (ModFunc.isShowID)
								{
									mFont2.drawString(g, string.Concat(new string[]
									{
										"[",
										item.template.id.ToString(),
										"] ",
										item.template.name,
										text
									}), num2 + 5, num3 + 1, 0);
								}
								else
								{
									mFont2.drawString(g, item.template.name + text, num2 + 5, num3 + 1, 0);
								}
								string text2 = string.Empty;
								if (item.itemOption != null)
								{
									if (item.itemOption.Length != 0 && item.itemOption[0] != null)
									{
										text2 += item.itemOption[0].getOptionString();
									}
									mFont mFont3 = mFont.tahoma_7_blue;
									if (item.compare < 0 && item.template.type != 5)
									{
										mFont3 = mFont.tahoma_7_red;
									}
									if (item.itemOption.Length > 1)
									{
										for (int k = 1; k < Math.min(item.itemOption.Length, 3); k++)
										{
											if (item.itemOption[k] != null && item.itemOption[k].IsValidOption())
											{
												text2 = text2 + ", " + item.itemOption[k].getOptionString();
											}
										}
									}
									if (this.typeShop == 2 && item.itemOption.Length > 1 && item.buyType != -1)
									{
										text2 += string.Empty;
									}
									if (this.typeShop != 2 || (this.typeShop == 2 && item.buyType <= 1))
									{
										mFont3.drawString(g, text2, num2 + 5, num3 + 10, 0);
									}
								}
								if (item.buySpec > 0)
								{
									SmallImage.drawSmallImage(g, (int)item.iconSpec, num2 + num4 - 7, num3 + h - 7, 0, 3);
									mFont.tahoma_7b_dark.drawString(g, Res.formatNumber((long)item.buySpec), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
								}
								if (item.buyCoin != 0 || item.buyGold != 0)
								{
									if (this.typeShop != 2 && item.powerRequire == 0L)
									{
										if (item.buyCoin > 0 && item.buyGold > 0)
										{
											if (item.buyCoin > 0)
											{
												g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + h - 5, 3);
												mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
											}
											if (item.buyGold > 0)
											{
												g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + h - 5, 3);
												mFont.tahoma_7b_blue.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
											}
										}
										else
										{
											if (item.buyCoin > 0)
											{
												g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + h - 5, 3);
												mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
											}
											if (item.buyGold > 0)
											{
												g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + h - 5, 3);
												mFont.tahoma_7b_blue.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
											}
										}
									}
									if (this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2))
									{
										if (item.buyCoin > 0 && item.buyGold > 0)
										{
											if (item.buyCoin > 0)
											{
												g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + h - 5, 3);
												mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
											}
											if (item.buyGold > 0)
											{
												g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + h - 5, 3);
												mFont.tahoma_7b_green.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
											}
										}
										else
										{
											if (item.buyCoin > 0)
											{
												g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + h - 5, 3);
												mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
											}
											if (item.buyGold > 0)
											{
												g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + h - 5, 3);
												mFont.tahoma_7b_green.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
											}
										}
									}
								}
								SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
								if (item.quantity > 1)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
								if (item.newItem && GameCanvas.gameTick % 10 > 5)
								{
									g.drawImage(Panel.imgNew, num5 + num7 / 2, num3 + 19, 3);
								}
							}
							if (this.typeShop == 2 && (this.Equals(GameCanvas.panel2) || this.currentTabIndex == 4) && item.buyType != 0)
							{
								if (item.buyType == 1)
								{
									mFont.tahoma_7_green.drawString(g, mResources.dangban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
									if (item.buyCoin != -1)
									{
										g.drawImage(Panel.imgThoivang, num2 + num4 - 7, num3 + h - 5, 3);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
									}
									else if (item.buyGold != -1)
									{
										g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + h - 5, 3);
										mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
									}
								}
								else if (item.buyType == 2)
								{
									mFont.tahoma_7b_blue.drawString(g, mResources.daban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
									if (item.buyCoin != -1)
									{
										g.drawImage(Panel.imgThoivang, num2 + num4 - 7, num3 + h - 5, 3);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
									}
									else if (item.buyGold != -1)
									{
										g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + h - 5, 3);
										mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
									}
								}
							}
						}
					}
					this.paintScrollArrow(g);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000034B9 File Offset: 0x000016B9
		private void paintAuto(mGraphics g)
		{
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x002BAEDC File Offset: 0x002B90DC
		private void paintPetStatus(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < this.strStatus.Length; i++)
			{
				int x = this.xScroll;
				int num = this.yScroll + i * this.ITEM_HEIGHT;
				int num2 = this.wScroll - 1;
				int h = this.ITEM_HEIGHT - 1;
				if (num - this.cmy <= this.yScroll + this.hScroll && num - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, this.strStatus[i], this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x002BAFE0 File Offset: 0x002B91E0
		private void paintPetSkill(mGraphics g, bool isPet2)
		{
			g.setColor(16711680);
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			Char pet = isPet2 ? Char.MyPet2z() : Char.myPetz();
			int num = 5 + pet.arrPetSkill.Length;
			for (int i = 0; i < num; i++)
			{
				int num2 = this.xScroll + 30;
				int num3 = this.yScroll + i * this.ITEM_HEIGHT;
				int num4 = this.wScroll - 30;
				int h = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + i * this.ITEM_HEIGHT;
				int item_HEIGHT = this.ITEM_HEIGHT;
				if (num3 - this.cmy <= this.yScroll + this.hScroll && num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					if (i >= 5)
					{
						g.setColor((i != this.selected) ? 16765060 : 16776068);
					}
					g.fillRect(num2, num3, num4, h);
					g.drawImage(GameScr.imgSkill, num5, num6, 0);
					if (i == 0)
					{
						SmallImage.drawSmallImage(g, 567, num5 + 4, num6 + 4, 0, 0);
						string st = string.Concat(new string[]
						{
							mResources.HP,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys(pet.cHPGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							NinjaUtil.getMoneys(pet.cHPGoc + 1000L),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							pet.hpFrom1000TiemNang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 1)
					{
						SmallImage.drawSmallImage(g, 569, num5 + 4, num6 + 4, 0, 0);
						string st2 = string.Concat(new string[]
						{
							mResources.KI,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys(pet.cMPGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st2, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							NinjaUtil.getMoneys(pet.cMPGoc + 1000L),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							pet.mpFrom1000TiemNang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 2)
					{
						SmallImage.drawSmallImage(g, 568, num5 + 4, num6 + 4, 0, 0);
						string st3 = string.Concat(new string[]
						{
							mResources.hit_point,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys(pet.cDamGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st3, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							NinjaUtil.getMoneys(pet.cDamGoc * 100L),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							pet.damFrom1000TiemNang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 3)
					{
						SmallImage.drawSmallImage(g, 721, num5 + 4, num6 + 4, 0, 0);
						string st4 = string.Concat(new string[]
						{
							mResources.armor,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)pet.cDefGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st4, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							NinjaUtil.getMoneys((long)(500000 + pet.cDefGoc * 100000)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							pet.defFrom1000TiemNang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 4)
					{
						SmallImage.drawSmallImage(g, 719, num5 + 4, num6 + 4, 0, 0);
						string st5 = string.Concat(new string[]
						{
							mResources.critical,
							" ",
							mResources.root,
							": ",
							pet.cCriticalGoc.ToString(),
							"%"
						});
						int num7 = pet.cCriticalGoc;
						if (num7 > Panel.t_tiemnang.Length - 1)
						{
							num7 = Panel.t_tiemnang.Length - 1;
						}
						long num8 = Panel.t_tiemnang[num7];
						mFont.tahoma_7b_blue.drawString(g, st5, num2 + 5, num3 + 3, 0);
						long number = num8;
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							Res.formatNumber2(number),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							pet.criticalFrom1000Tiemnang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i >= 5)
					{
						Skill skill = pet.arrPetSkill[i - 5];
						g.drawImage(GameScr.imgSkill2, num5, num6, 0);
						if (skill.template != null)
						{
							mFont.tahoma_7_blue.drawString(g, skill.template.name, num2 + 5, num3 + 3, 0);
							mFont.tahoma_7_green2.drawString(g, mResources.level + ": " + skill.point.ToString(), num2 + 5, num3 + 15, 0);
							SmallImage.drawSmallImage(g, skill.template.iconId, num5 + 4, num6 + 4, 0, 0);
						}
						else
						{
							mFont.tahoma_7_green2.drawString(g, skill.moreInfo, num2 + 5, num3 + 3, 0);
							mFont.tahoma_7_green2.drawString(g, mResources.level + ": " + 0.ToString(), num2 + 5, num3 + 15, 0);
							SmallImage.drawSmallImage(g, GameScr.efs[98].arrEfInfo[0].idImg, num5 + 8, num6 + 7, 0, 0);
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x002BB690 File Offset: 0x002B9890
		private void paintPetInventory(mGraphics g, bool isPet2)
		{
			g.setColor(16711680);
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			try
			{
				Item[] arrItemBody = (isPet2 ? Char.MyPet2z() : Char.myPetz()).arrItemBody;
				for (int i = 0; i < arrItemBody.Length; i++)
				{
					int num = i;
					int num2 = this.xScroll + 36;
					int num3 = this.yScroll + i * this.ITEM_HEIGHT;
					int num4 = this.wScroll - 36;
					int h = this.ITEM_HEIGHT - 1;
					int num5 = this.xScroll;
					int num6 = this.yScroll + i * this.ITEM_HEIGHT;
					int num7 = 34;
					int num8 = this.ITEM_HEIGHT - 1;
					if (num3 - this.cmy <= this.yScroll + this.hScroll && num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
					{
						g.setColor((i != this.selected) ? 15196114 : 16383818);
						g.fillRect(num2, num3, num4, h, 5);
						g.setColor((i != this.selected) ? 9993045 : 9541120);
						Item item = arrItemBody[num];
						if (item != null)
						{
							for (int j = 0; j < item.itemOption.Length; j++)
							{
								if (item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0)
								{
									sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
									if (Panel.GetColor_ItemBg((int)color_Item_Upgrade) != -1)
									{
										g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
									}
								}
							}
						}
						g.setColor(6047789, 0.3f);
						g.fillRect(num5, num6, num7, num8, 5);
						this.paintEffectItem(g, item, num5, num6);
						if (item != null)
						{
							string text = string.Empty;
							mFont mFont2 = mFont.tahoma_7_green2;
							if (item.itemOption != null)
							{
								for (int k = 0; k < item.itemOption.Length; k++)
								{
									if (item.itemOption[k].optionTemplate.id == 72)
									{
										text = " [+" + item.itemOption[k].getOptionString() + "]";
									}
									else if (item.itemOption[k].optionTemplate.id == 107 && item.itemOption[k].param > 0)
									{
										this.paintItemStar(g, item.itemOption[k].param.ToString(), num2 + 125, num3);
									}
								}
							}
							mFont2.drawString(g, string.Concat(new string[]
							{
								"[",
								item.template.id.ToString(),
								"] ",
								item.template.name,
								text
							}), num2 + 5, num3 + 1, 0);
							string text2 = string.Empty;
							if (item.itemOption != null)
							{
								if (item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
								{
									text2 += item.itemOption[0].getOptionString();
								}
								mFont mFont3 = mFont.tahoma_7_blue;
								if (item.compare < 0 && item.template.type != 5)
								{
									mFont3 = mFont.tahoma_7_red;
								}
								if (item.itemOption.Length > 1)
								{
									for (int l = 1; l < 2; l++)
									{
										if (item.itemOption[l] != null && item.itemOption[l].optionTemplate.id != 102 && item.itemOption[l].optionTemplate.id != 107)
										{
											text2 = text2 + ", " + item.itemOption[l].getOptionString();
										}
									}
								}
								mFont3.drawString(g, text2, num2 + 5, num3 + 11, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
							if (item.itemOption != null)
							{
								for (int m = 0; m < item.itemOption.Length; m++)
								{
									this.paintOptItem(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num5, num6, num7, num8);
								}
								for (int num9 = 0; num9 < item.itemOption.Length; num9++)
								{
									this.paintOptSlotItem(g, item.itemOption[num9].optionTemplate.id, item.itemOption[num9].param, num5, num6, num7, num8);
								}
							}
							if (item.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x002BBBEC File Offset: 0x002B9DEC
		private void paintScrollArrow(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			if ((this.cmy > 24 && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1))
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
			}
			if ((this.cmy < this.cmyLim && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1))
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 8, 0);
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x002BBCE0 File Offset: 0x002B9EE0
		private void paintTools(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < Panel.strTool.Length; i++)
			{
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = this.wScroll - 1;
				int h = this.ITEM_HEIGHT - 1;
				if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);
					mFont.tahoma_7b_dark.drawString(g, Panel.strTool[i], this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
					if (Panel.strTool[i].Equals(mResources.gameInfo))
					{
						int j = 0;
						while (j < Panel.vGameInfo.size())
						{
							if (!((GameInfo)Panel.vGameInfo.elementAt(j)).hasRead)
							{
								if (GameCanvas.gameTick % 20 > 10)
								{
									g.drawImage(Panel.imgNew, num + 10, num2 + 10, 3);
									break;
								}
								break;
							}
							else
							{
								j++;
							}
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x002BBE4C File Offset: 0x002BA04C
		private void paintGameSubInfo(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < Panel.contenInfo.Length; i++)
			{
				int num3 = this.xScroll;
				int num2 = this.yScroll + i * 15;
				int num4 = this.wScroll;
				int item_HEIGHT = this.ITEM_HEIGHT;
				if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					mFont.tahoma_7b_dark.drawString(g, Panel.contenInfo[i], this.xScroll + 5, num2 + 6, mFont.LEFT);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x002BBF10 File Offset: 0x002BA110
		private void paintPlayerInfo(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < Panel.contenInfo.Length; i++)
			{
				int num3 = this.xScroll;
				int num2 = this.yScroll + i * 15;
				int num4 = this.wScroll;
				int item_HEIGHT = this.ITEM_HEIGHT;
				if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					mFont.tahoma_7b_dark.drawString(g, Panel.contenInfo[i], this.xScroll + 5, num2 + 6, mFont.LEFT);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x002BBFD4 File Offset: 0x002BA1D4
		private void paintGameInfo(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < Panel.vGameInfo.size(); i++)
			{
				GameInfo gameInfo = (GameInfo)Panel.vGameInfo.elementAt(i);
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = this.wScroll - 1;
				int h = this.ITEM_HEIGHT - 1;
				if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);
					mFont.tahoma_7b_dark.drawString(g, gameInfo.main, this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
					if (!gameInfo.hasRead && GameCanvas.gameTick % 20 > 10)
					{
						g.drawImage(Panel.imgNew, num + 10, num2 + 10, 3);
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x002BC114 File Offset: 0x002BA314
		private void paintSkill(mGraphics g)
		{
			g.setColor(16711680);
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			int num = Char.myCharz().nClass.skillTemplates.Length;
			for (int i = 0; i < num + 6; i++)
			{
				int num2 = this.xScroll + 30;
				int num3 = this.yScroll + i * this.ITEM_HEIGHT;
				int num4 = this.wScroll - 30;
				int h = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + i * this.ITEM_HEIGHT;
				int item_HEIGHT = this.ITEM_HEIGHT;
				if (num3 - this.cmy <= this.yScroll + this.hScroll && num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					if (i == 5)
					{
						g.setColor((i != this.selected) ? 16765060 : 16776068);
					}
					g.fillRect(num2, num3, num4, h);
					g.drawImage(GameScr.imgSkill, num5, num6, 0);
					if (i == 0)
					{
						SmallImage.drawSmallImage(g, 567, num5 + 4, num6 + 4, 0, 0);
						string st = string.Concat(new string[]
						{
							mResources.HP,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys(Char.myCharz().cHPGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							NinjaUtil.getMoneys(Char.myCharz().cHPGoc + 1000L),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							Char.myCharz().hpFrom1000TiemNang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 1)
					{
						SmallImage.drawSmallImage(g, 569, num5 + 4, num6 + 4, 0, 0);
						string st2 = string.Concat(new string[]
						{
							mResources.KI,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys(Char.myCharz().cMPGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st2, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							NinjaUtil.getMoneys(Char.myCharz().cMPGoc + 1000L),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							Char.myCharz().mpFrom1000TiemNang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 2)
					{
						SmallImage.drawSmallImage(g, 568, num5 + 4, num6 + 4, 0, 0);
						string st3 = string.Concat(new string[]
						{
							mResources.hit_point,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys(Char.myCharz().cDamGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st3, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							NinjaUtil.getMoneys(Char.myCharz().cDamGoc * 100L),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							Char.myCharz().damFrom1000TiemNang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 3)
					{
						SmallImage.drawSmallImage(g, 721, num5 + 4, num6 + 4, 0, 0);
						string st4 = string.Concat(new string[]
						{
							mResources.armor,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)Char.myCharz().cDefGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st4, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							NinjaUtil.getMoneys((long)(500000 + Char.myCharz().cDefGoc * 100000)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							Char.myCharz().defFrom1000TiemNang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 4)
					{
						SmallImage.drawSmallImage(g, 719, num5 + 4, num6 + 4, 0, 0);
						string st5 = string.Concat(new string[]
						{
							mResources.critical,
							" ",
							mResources.root,
							": ",
							Char.myCharz().cCriticalGoc.ToString(),
							"%"
						});
						int num7 = Char.myCharz().cCriticalGoc;
						if (num7 > Panel.t_tiemnang.Length - 1)
						{
							num7 = Panel.t_tiemnang.Length - 1;
						}
						long num11 = Panel.t_tiemnang[num7];
						mFont.tahoma_7b_blue.drawString(g, st5, num2 + 5, num3 + 3, 0);
						long number = num11;
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							Res.formatNumber2(number),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							Char.myCharz().criticalFrom1000Tiemnang.ToString()
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 5)
					{
						if (Panel.specialInfo != null)
						{
							SmallImage.drawSmallImage(g, (int)Panel.spearcialImage, num5 + 4, num6 + 4, 0, 0);
							string[] array = mFont.tahoma_7.splitFontArray(Panel.specialInfo, 120);
							for (int j = 0; j < array.Length; j++)
							{
								mFont.tahoma_7_green2.drawString(g, array[j], num2 + 5, num3 + 3 + j * 12, 0);
							}
						}
						else
						{
							mFont.tahoma_7_green2.drawString(g, string.Empty, num2 + 5, num3 + 9, 0);
						}
					}
					if (i >= 6)
					{
						int num8 = i - 6;
						SkillTemplate skillTemplate = Char.myCharz().nClass.skillTemplates[num8];
						SmallImage.drawSmallImage(g, skillTemplate.iconId, num5 + 4, num6 + 4, 0, 0);
						Skill skill = Char.myCharz().getSkill(skillTemplate);
						if (skill != null)
						{
							mFont.tahoma_7b_blue.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
							mFont.tahoma_7_blue.drawString(g, mResources.level + ": " + skill.point.ToString(), num2 + num4 - 5, num3 + 3, mFont.RIGHT);
							if (skill.point == skillTemplate.maxPoint)
							{
								mFont.tahoma_7_green2.drawString(g, mResources.max_level_reach, num2 + 5, num3 + 15, 0);
							}
							else if (skill.template.isSkillSpec())
							{
								string text = mResources.proficiency + ": ";
								int x = mFont.tahoma_7_green2.getWidthExactOf(text) + num2 + 5;
								int num9 = num3 + 15;
								mFont.tahoma_7_green2.drawString(g, text, num2 + 5, num9, 0);
								mFont.tahoma_7_green2.drawString(g, "(" + skill.strCurExp() + ")", num2 + num4 - 5, num9, mFont.RIGHT);
								num9 += 4;
								g.setColor(7169134);
								g.fillRect(x, num9, 50, 5);
								int num10 = (int)(skill.curExp * 50 / 1000);
								g.setColor(11992374);
								g.fillRect(x, num9, num10, 5);
								if (skill.curExp < 1000)
								{
								}
							}
							else
							{
								Skill skill2 = skillTemplate.skills[skill.point];
								mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
								{
									mResources.level,
									" ",
									(skill.point + 1).ToString(),
									" ",
									mResources.need,
									" ",
									Res.formatNumber2(skill2.powRequire),
									" ",
									mResources.potential
								}), num2 + 5, num3 + 15, 0);
							}
						}
						else
						{
							Skill skill3 = skillTemplate.skills[0];
							mFont.tahoma_7b_green.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
							mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
							{
								mResources.need_upper,
								" ",
								Res.formatNumber2(skill3.powRequire),
								" ",
								mResources.potential_to_learn
							}), num2 + 5, num3 + 15, 0);
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x002BCA04 File Offset: 0x002BAC04
		private void paintMapTrans(mGraphics g)
		{
			g.setColor(16711680);
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < this.mapNames.Length; i++)
			{
				int num3 = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num4 = this.wScroll;
				int h = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll;
				int item_HEIGHT = this.ITEM_HEIGHT;
				int item_HEIGHT2 = this.ITEM_HEIGHT;
				int item_HEIGHT3 = this.ITEM_HEIGHT;
				if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(this.xScroll, num2, this.wScroll, h);
					mFont.tahoma_7b_blue.drawString(g, this.mapNames[i], 5, num2 + 1, 0);
					mFont.tahoma_7_grey.drawString(g, this.planetNames[i], 5, num2 + 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x002BCB40 File Offset: 0x002BAD40
		private void paintZone(mGraphics g)
		{
			g.setColor(16711680);
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			int[] zones = GameScr.gI().zones;
			int[] pts = GameScr.gI().pts;
			for (int i = 0; i < pts.Length; i++)
			{
				int num = this.xScroll + 36;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = this.wScroll - 36;
				int h = this.ITEM_HEIGHT - 1;
				int num4 = this.xScroll;
				int y = this.yScroll + i * this.ITEM_HEIGHT;
				int num5 = 34;
				int h2 = this.ITEM_HEIGHT - 1;
				if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h, 5);
					g.setColor(this.zoneColor[pts[i]]);
					g.fillRect(num4, y, num5, h2, 5);
					if (zones[i] != -1)
					{
						if (pts[i] != 1)
						{
							mFont.tahoma_7_yellow.drawString(g, zones[i].ToString() + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
						}
						else
						{
							mFont.tahoma_7_grey.drawString(g, zones[i].ToString() + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
						}
						mFont.tahoma_7_green2.drawString(g, GameScr.gI().numPlayer[i].ToString() + "/" + GameScr.gI().maxPlayer[i].ToString(), num + 5, num2 + 6, 0);
					}
					if (GameScr.gI().rankName1[i] != null)
					{
						mFont.tahoma_7_grey.drawString(g, GameScr.gI().rankName1[i] + "(Top " + GameScr.gI().rank1[i].ToString() + ")", num + num3 - 2, num2 + 1, mFont.RIGHT);
						mFont.tahoma_7_grey.drawString(g, GameScr.gI().rankName2[i] + "(Top " + GameScr.gI().rank2[i].ToString() + ")", num + num3 - 2, num2 + 11, mFont.RIGHT);
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x002BCDEC File Offset: 0x002BAFEC
		private void paintSpeacialSkill(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			g.setColor(0);
			if (this.currentListLength == 0)
			{
				return;
			}
			int num = (this.cmy + this.hScroll) / 24 + 1;
			if (num < this.hScroll / 24 + 1)
			{
				num = this.hScroll / 24 + 1;
			}
			if (num > this.currentListLength)
			{
				num = this.currentListLength;
			}
			int num2 = this.cmy / 24;
			if (num2 >= num)
			{
				num2 = num - 1;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			for (int i = num2; i < num; i++)
			{
				int num3 = this.xScroll;
				int num4 = this.yScroll + i * this.ITEM_HEIGHT;
				int num5 = 24;
				int num6 = this.ITEM_HEIGHT - 1;
				int num7 = this.xScroll + num5;
				int num8 = this.yScroll + i * this.ITEM_HEIGHT;
				int num9 = this.wScroll - num5;
				int h = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num7, num8, num9, h);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num3, num4, num5, num6);
				SmallImage.drawSmallImage(g, (int)Char.myCharz().imgSpeacialSkill[this.currentTabIndex][i], num3 + num5 / 2, num4 + num6 / 2, 0, 3);
				string[] array = mFont.tahoma_7_grey.splitFontArray(Char.myCharz().infoSpeacialSkill[this.currentTabIndex][i], 140);
				for (int j = 0; j < array.Length; j++)
				{
					mFont.tahoma_7_grey.drawString(g, array[j], num7 + 5, num8 + 1 + j * 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x002BCFCC File Offset: 0x002BB1CC
		private void paintBox222222(mGraphics g)
		{
			g.setColor(16711680);
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			try
			{
				Item[] arrItemBox = Char.myCharz().arrItemBox;
				this.currentListLength = this.checkCurrentListLength(arrItemBox.Length);
				int num = arrItemBox.Length / 20 + ((arrItemBox.Length % 20 > 0) ? 1 : 0);
				this.TAB_W_NEW = this.wScroll / num;
				for (int i = 0; i < this.currentListLength; i++)
				{
					int num2 = this.xScroll + 36;
					int num3 = this.yScroll + i * this.ITEM_HEIGHT;
					int num4 = this.wScroll - 36;
					int h = this.ITEM_HEIGHT - 1;
					int num5 = this.xScroll;
					int num6 = this.yScroll + i * this.ITEM_HEIGHT;
					int num7 = 34;
					int num8 = this.ITEM_HEIGHT - 1;
					if (num3 - this.cmy <= this.yScroll + this.hScroll && num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
					{
						if (i == 0)
						{
							for (int j = 0; j < num; j++)
							{
								int num9 = (j == this.newSelected && this.selected == 0) ? ((GameCanvas.gameTick % 10 < 7) ? -1 : 0) : 0;
								g.setColor((j != this.newSelected) ? 15723751 : 16383818);
								g.fillRect(this.xScroll + j * this.TAB_W_NEW, num3 + num9, this.TAB_W_NEW - 1, 20);
								mFont.tahoma_7_grey.drawString(g, string.Empty + j.ToString(), this.xScroll + j * this.TAB_W_NEW + this.TAB_W_NEW / 2, this.yScroll + num9 + 2, mFont.CENTER);
							}
						}
						else
						{
							g.setColor((i != this.selected) ? 15196114 : 16383818);
							g.fillRect(num2, num3, num4, h, 5);
							g.setColor((i != this.selected) ? 9993045 : 9541120);
							int inventorySelect_body = this.GetInventorySelect_body(i, this.newSelected);
							Item item = arrItemBox[inventorySelect_body];
							if (item != null)
							{
								for (int k = 0; k < item.itemOption.Length; k++)
								{
									if (item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0)
									{
										sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
										if (Panel.GetColor_ItemBg((int)color_Item_Upgrade) != -1)
										{
											g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
										}
									}
									if (item.itemOption[k].optionTemplate.id == 107 && item.itemOption[k].param > 0 && item.itemOption[k].param > 0)
									{
										this.paintItemStar(g, item.itemOption[k].param.ToString(), num2 + 125, num3);
									}
								}
							}
							g.setColor(6047789, 0.3f);
							g.fillRect(num5, num6, num7, num8, 5);
							this.paintEffectItem(g, item, num5, num6);
							if (item != null)
							{
								string text = string.Empty;
								mFont mFont2 = mFont.tahoma_7_green2;
								if (item.itemOption != null)
								{
									for (int l = 0; l < item.itemOption.Length; l++)
									{
										if (item.itemOption[l].optionTemplate.id == 72)
										{
											text = " [+" + item.itemOption[l].getOptionString() + "]";
										}
									}
								}
								if (ModFunc.isShowID)
								{
									mFont2.drawString(g, string.Concat(new string[]
									{
										"[",
										item.template.id.ToString(),
										"] ",
										item.template.name,
										text
									}), num2 + 5, num3 + 1, 0);
								}
								else
								{
									mFont2.drawString(g, item.template.name + text, num2 + 5, num3 + 1, 0);
								}
								string text2 = string.Empty;
								if (item.itemOption != null)
								{
									if (item.itemOption.Length != 0 && item.itemOption[0] != null)
									{
										text2 += item.itemOption[0].getOptionString();
									}
									mFont mFont3 = mFont.tahoma_7_blue;
									if (item.compare < 0 && item.template.type != 5)
									{
										mFont3 = mFont.tahoma_7_red;
									}
									if (item.itemOption.Length > 1)
									{
										for (int m = 1; m < Math.min(item.itemOption.Length, 3); m++)
										{
											if (item.itemOption[m] != null && item.itemOption[m].IsValidOption())
											{
												text2 = text2 + ", " + item.itemOption[m].getOptionString();
											}
										}
									}
									mFont3.drawString(g, text2, num2 + 5, num3 + 10, mFont.LEFT);
								}
								SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
								if (item.itemOption != null)
								{
									for (int n = 0; n < item.itemOption.Length; n++)
									{
										this.paintOptItemInventory(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8, item);
									}
									for (int num10 = 0; num10 < item.itemOption.Length; num10++)
									{
										this.paintOptSlotItem(g, item.itemOption[num10].optionTemplate.id, item.itemOption[num10].param, num5, num6, num7, num8);
									}
								}
								if (item.quantity > 1)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x002BD62C File Offset: 0x002BB82C
		private void paintBox(mGraphics g)
		{
			try
			{
				if (!ModFunc.isInventory)
				{
					this.paintBox222222(g);
				}
				else
				{
					g.setColor(16711680);
					g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
					g.translate(0, -this.cmy);
					Item[] arrItemBox = Char.myCharz().arrItemBox;
					this.currentListLength = this.checkCurrentListLengthNew(arrItemBox.Length / 5);
					this.TAB_W_NEW = 1;
					int columns = 5;
					int itemWidth = 35;
					int itemHeight = this.ITEM_HEIGHT;
					for (int i = 0; i < arrItemBox.Length; i++)
					{
						int row = i / columns;
						int col = i % columns;
						int num2 = this.xScroll + col * (itemWidth + 1);
						int num3 = this.yScroll + row * itemHeight;
						int num4 = this.xScroll + col * (itemWidth + 1);
						int num5 = this.yScroll + row * itemHeight;
						int num6 = itemWidth;
						int num7 = itemHeight - 1;
						if (num3 - this.cmy <= this.yScroll + this.hScroll && num3 - this.cmy >= this.yScroll - itemHeight)
						{
							if (i == this.selected)
							{
								g.setColor(16383818);
								g.fillRect(num2 - 1, num3 - 1, itemWidth + 1, itemHeight, 5);
							}
							int inventorySelect_body = this.GetInventorySelect_body(i, this.newSelected);
							Item item = arrItemBox[inventorySelect_body];
							if (item != null)
							{
								for (int j = 0; j < item.itemOption.Length; j++)
								{
									if (item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0)
									{
										sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
										if (Panel.GetColor_ItemBg((int)color_Item_Upgrade) != -1)
										{
											g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
										}
									}
								}
							}
							g.setColor(6047789, 0.3f);
							g.fillRect(num4, num5, num6, num7, 5);
							this.paintEffectItem(g, item, num4, num5);
							if (item != null)
							{
								string empty = string.Empty;
								mFont tahoma_7_green = mFont.tahoma_7_green2;
								if (item.itemOption != null)
								{
									for (int k = 0; k < item.itemOption.Length; k++)
									{
										if (item.itemOption[k].optionTemplate.id == 72)
										{
											_ = " [+" + item.itemOption[k].getOptionString() + "]";
										}
									}
								}
								string empty2 = string.Empty;
								SmallImage.drawSmallImage(g, (int)item.template.iconID, num4 + num6 / 2, num5 + num7 / 2, 0, 3);
								if (item.itemOption != null)
								{
									for (int l = 0; l < item.itemOption.Length; l++)
									{
										this.paintOptItem(g, item.itemOption[l].optionTemplate.id, item.itemOption[l].param, num4, num5, num6, num7);
									}
									for (int num8 = 0; num8 < item.itemOption.Length; num8++)
									{
										this.paintOptSlotItem(g, item.itemOption[num8].optionTemplate.id, item.itemOption[num8].param, num4, num5, num6, num7);
									}
								}
								if (item.quantity > 1)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num4 + num6, num5 + num7 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
							}
						}
					}
					this.paintScrollArrow(g);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x002BD9DC File Offset: 0x002BBBDC
		public Member getCurrMember()
		{
			if (this.selected < 2)
			{
				return null;
			}
			if (this.selected > ((this.member == null) ? this.myMember.size() : this.member.size()) + 1)
			{
				return null;
			}
			if (this.member != null)
			{
				return (Member)this.member.elementAt(this.selected - 2);
			}
			return (Member)this.myMember.elementAt(this.selected - 2);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x002BDA59 File Offset: 0x002BBC59
		public ClanMessage getCurrMessage()
		{
			if (this.selected < 2)
			{
				return null;
			}
			if (this.selected > ClanMessage.vMessage.size() + 1)
			{
				return null;
			}
			return (ClanMessage)ClanMessage.vMessage.elementAt(this.selected - 2);
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x002BDA93 File Offset: 0x002BBC93
		public Clan getCurrClan()
		{
			if (this.selected < 2)
			{
				return null;
			}
			if (this.selected > this.clans.Length + 1)
			{
				return null;
			}
			return this.clans[this.selected - 2];
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x002BDAC4 File Offset: 0x002BBCC4
		private void paintLogChat(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			g.setColor(0);
			if (this.logChat.size() == 0)
			{
				mFont.tahoma_7_green2.drawString(g, mResources.no_msg, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2 + 24, 2);
			}
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = 24;
				int h = this.ITEM_HEIGHT - 1;
				int num4 = this.xScroll + num3;
				int num5 = this.yScroll + i * this.ITEM_HEIGHT;
				int num6 = this.wScroll - num3;
				int num7 = this.ITEM_HEIGHT - 1;
				if (i == 0)
				{
					g.setColor(15196114);
					g.fillRect(num, num5, this.wScroll, num7);
					g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num5 + 2, StaticObj.TOP_RIGHT);
					((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, (!this.isViewChatServer) ? mResources.on : mResources.off, this.xScroll + this.wScroll - 22, num5 + 7, 2);
					mFont.tahoma_7_grey.drawString(g, (!this.isViewChatServer) ? mResources.onPlease : mResources.offPlease, this.xScroll + 5, num5 + num7 / 2 - 4, mFont.LEFT);
				}
				else
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num4, num5, num6, num7);
					g.setColor((i != this.selected) ? 9993045 : 9541120);
					g.fillRect(num, num2, num3, h);
					InfoItem infoItem = (InfoItem)this.logChat.elementAt(i - 1);
					if (infoItem.charInfo.headICON != -1)
					{
						SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
					}
					else
					{
						Part part = GameScr.parts[infoItem.charInfo.head];
						SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[0][0][0]].id, num + (int)part.pi[Char.CharInfo[0][0][0]].dx, num2 + (int)part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
					}
					g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
					mFont tahoma_7b_dark = mFont.tahoma_7b_dark;
					mFont.tahoma_7b_green2.drawString(g, (infoItem.charInfo.isTichXanh ? "     " : string.Empty) + infoItem.charInfo.cName, num4 + 5, num5, 0);
					if (infoItem.charInfo.isTichXanh)
					{
						ModFunc.PaintTicks(g, num4 + 4, num5 + 1);
					}
					if (!infoItem.isChatServer)
					{
						mFont.tahoma_7_blue.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
					}
					else
					{
						mFont.tahoma_7_red.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x002BDE68 File Offset: 0x002BC068
		private void paintFlagChange(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			g.setColor(0);
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll + 26;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = this.wScroll - 26;
				int h = this.ITEM_HEIGHT - 1;
				int num4 = this.xScroll;
				int num5 = this.yScroll + i * this.ITEM_HEIGHT;
				int num6 = 24;
				int num7 = this.ITEM_HEIGHT - 1;
				if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);
					g.setColor((i != this.selected) ? 9993045 : 9541120);
					g.fillRect(num4, num5, num6, num7);
					Item item = (Item)this.vFlag.elementAt(i);
					if (item != null)
					{
						mFont.tahoma_7_green2.drawString(g, item.template.name, num + 5, num2 + 1, 0);
						string text = string.Empty;
						if (item.itemOption != null && item.itemOption.Length >= 1)
						{
							if (item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
							{
								text += item.itemOption[0].getOptionString();
							}
							mFont.tahoma_7_blue.drawString(g, text, num + 5, num2 + 11, 0);
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num4 + num6 / 2, num5 + num7 / 2, 0, 3);
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x002BE080 File Offset: 0x002BC280
		private void paintEnemy(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			g.setColor(0);
			if (this.currentListLength == 0)
			{
				mFont.tahoma_7_green2.drawString(g, mResources.no_enemy, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
				return;
			}
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = 24;
				int h = this.ITEM_HEIGHT - 1;
				int num4 = this.xScroll + num3;
				int num5 = this.yScroll + i * this.ITEM_HEIGHT;
				int num6 = this.wScroll - num3;
				int h2 = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num4, num5, num6, h2);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, h);
				InfoItem infoItem = (InfoItem)this.vEnemy.elementAt(i);
				if (infoItem.charInfo.headICON != -1)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[0][0][0]].id, num + (int)part.pi[Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				if (infoItem.isOnline)
				{
					mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x002BE310 File Offset: 0x002BC510
		private void paintFriend(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			g.setColor(0);
			if (this.currentListLength == 0)
			{
				mFont.tahoma_7_green2.drawString(g, mResources.no_friend, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
				return;
			}
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = 24;
				int h = this.ITEM_HEIGHT - 1;
				int num4 = this.xScroll + num3;
				int num5 = this.yScroll + i * this.ITEM_HEIGHT;
				int num6 = this.wScroll - num3;
				int h2 = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num4, num5, num6, h2);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, h);
				InfoItem infoItem = (InfoItem)this.vFriend.elementAt(i);
				if (infoItem.charInfo.headICON != -1)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[0][0][0]].id, num + (int)part.pi[Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				if (infoItem.isOnline)
				{
					mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x002BE5A0 File Offset: 0x002BC7A0
		public void paintPlayerMenu(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < this.vPlayerMenu.size(); i++)
			{
				int x = this.xScroll;
				int num = this.yScroll + i * this.ITEM_HEIGHT;
				int num2 = this.wScroll - 1;
				int h = this.ITEM_HEIGHT - 1;
				if (num - this.cmy <= this.yScroll + this.hScroll && num - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					Command command = (Command)this.vPlayerMenu.elementAt(i);
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					if (command.caption2.Equals(string.Empty))
					{
						mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
					}
					else
					{
						mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num + 1, mFont.CENTER);
						mFont.tahoma_7b_dark.drawString(g, command.caption2, this.xScroll + this.wScroll / 2, num + 11, mFont.CENTER);
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x002BE724 File Offset: 0x002BC924
		private void paintClans(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(-this.cmx, -this.cmy);
			g.setColor(0);
			int num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
			if (this.currentListLength == 2)
			{
				mFont.tahoma_7_green2.drawString(g, this.clanReport, this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
				if (this.isMessage && this.myMember.size() == 1)
				{
					for (int i = 0; i < mResources.clanEmpty.Length; i++)
					{
						mFont.tahoma_7b_dark.drawString(g, mResources.clanEmpty[i], this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mResources.clanEmpty.Length * 12 / 2 + i * 12, mFont.CENTER);
					}
				}
			}
			if (this.isMessage)
			{
				this.currentListLength = ClanMessage.vMessage.size() + 2;
			}
			for (int j = 0; j < this.currentListLength; j++)
			{
				int num2 = this.xScroll;
				int num3 = this.yScroll + j * this.ITEM_HEIGHT;
				int num4 = 24;
				int num5 = this.ITEM_HEIGHT - 1;
				int num6 = this.xScroll + num4;
				int num7 = this.yScroll + j * this.ITEM_HEIGHT;
				int num8 = this.wScroll - num4;
				int num9 = this.ITEM_HEIGHT - 1;
				if (num7 - this.cmy <= this.yScroll + this.hScroll && num7 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					if (j != 0)
					{
						if (j != 1)
						{
							if (this.isSearchClan)
							{
								if (this.clans != null && this.clans.Length != 0)
								{
									g.setColor((j != this.selected) ? 15196114 : 16383818);
									g.fillRect(num6, num7, num8, num9, 5);
									g.setColor((j != this.selected) ? 9993045 : 9541120);
									g.fillRect(num2, num3, num4, num5, 5);
									if (ClanImage.isExistClanImage(this.clans[j - 2].imgID))
									{
										if (ClanImage.getClanImage((short)this.clans[j - 2].imgID).idImage != null)
										{
											SmallImage.drawSmallImage(g, ClanImage.getClanImage((short)this.clans[j - 2].imgID).idImage[0], num2 + num4 / 2, num3 + num5 / 2, 0, StaticObj.VCENTER_HCENTER);
										}
									}
									else
									{
										ClanImage clanImage = new ClanImage();
										clanImage.ID = this.clans[j - 2].imgID;
										if (!ClanImage.isExistClanImage(clanImage.ID))
										{
											ClanImage.addClanImage(clanImage);
										}
									}
									string st = (this.clans[j - 2].name.Length <= 23) ? this.clans[j - 2].name : (this.clans[j - 2].name.Substring(0, 23) + "...");
									mFont.tahoma_7b_green2.drawString(g, st, num6 + 5, num7, 0);
									g.setClip(num6, num7, num8 - 10, num9);
									mFont.tahoma_7_blue.drawString(g, this.clans[j - 2].slogan, num6 + 5, num7 + 11, 0);
									g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
									mFont.tahoma_7_green2.drawString(g, this.clans[j - 2].currMember.ToString() + "/" + this.clans[j - 2].maxMember.ToString(), num6 + num8 - 5, num7, mFont.RIGHT);
								}
							}
							else if (this.isViewMember)
							{
								g.setColor((j != this.selected) ? 15196114 : 16383818);
								g.fillRect(num6, num7, num8, num9, 5);
								g.setColor((j != this.selected) ? 9993045 : 9541120);
								g.fillRect(num2, num3, num4, num5);
								Member member = (this.member == null) ? ((Member)this.myMember.elementAt(j - 2)) : ((Member)this.member.elementAt(j - 2));
								if (member.headICON != -1)
								{
									SmallImage.drawSmallImage(g, (int)member.headICON, num2, num3, 0, 0);
								}
								else
								{
									Part part = GameScr.parts[(int)member.head];
									SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[0][0][0]].id, num2 + (int)part.pi[Char.CharInfo[0][0][0]].dx, num3 + 3 + (int)part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
								}
								g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
								mFont mFont2 = mFont.tahoma_7b_dark;
								if (member.role == 0)
								{
									mFont2 = mFont.tahoma_7b_red;
								}
								else if (member.role == 1)
								{
									mFont2 = mFont.tahoma_7b_green;
								}
								else if (member.role == 2)
								{
									mFont2 = mFont.tahoma_7b_green2;
								}
								mFont2.drawString(g, member.name, num6 + 5, num7, 0);
								mFont.tahoma_7_blue.drawString(g, mResources.power + ": " + member.powerPoint, num6 + 5, num7 + 11, 0);
								SmallImage.drawSmallImage(g, 7223, num6 + num8 - 7, num7 + 12, 0, 3);
								mFont.tahoma_7_blue.drawString(g, string.Empty + member.clanPoint.ToString(), num6 + num8 - 15, num7 + 6, mFont.RIGHT);
							}
							else if (this.isMessage && ClanMessage.vMessage.size() != 0)
							{
								ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(j - 2);
								g.setColor((j != this.selected || clanMessage.option != null) ? 15196114 : 16383818);
								g.fillRect(num2, num3, num8 + num4, num9, 5);
								clanMessage.paint(g, num2, num3);
								if (clanMessage.option != null)
								{
									int num10 = this.xScroll + this.wScroll - 2 - clanMessage.option.Length * 40;
									for (int k = 0; k < clanMessage.option.Length; k++)
									{
										if (k == this.cSelected && j == this.selected)
										{
											g.drawImage(GameScr.imgLbtnFocus2, num10 + k * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
											mFont.tahoma_7b_green2.drawString(g, clanMessage.option[k], num10 + k * 40 + 20, num7 + 6, mFont.CENTER);
										}
										else
										{
											g.drawImage(GameScr.imgLbtn2, num10 + k * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
											mFont.tahoma_7b_dark.drawString(g, clanMessage.option[k], num10 + k * 40 + 20, num7 + 6, mFont.CENTER);
										}
									}
								}
							}
						}
						else
						{
							g.setColor((j != this.selected) ? 15196114 : 16383818);
							g.fillRect(this.xScroll, num7, this.wScroll, num9, 5);
							if (this.clanInfo != null)
							{
								mFont.tahoma_7b_dark.drawString(g, this.clanInfo, this.xScroll + this.wScroll / 2, num7 + 6, mFont.CENTER);
							}
						}
					}
					else
					{
						for (int l = 0; l < this.clansOption.Length; l++)
						{
							g.setColor((l != this.cSelected || j != this.selected) ? 15723751 : 16383818);
							g.fillRect(num + l * this.TAB_W, num7, this.TAB_W - 1, 23, 5);
							for (int m = 0; m < this.clansOption[l].Length; m++)
							{
								mFont.tahoma_7_grey.drawString(g, this.clansOption[l][m], num + l * this.TAB_W + this.TAB_W / 2, this.yScroll + m * 11, mFont.CENTER);
							}
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x002BEFC8 File Offset: 0x002BD1C8
		private void paintArchivement(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			g.setColor(0);
			if (this.currentListLength == 0)
			{
				mFont.tahoma_7_green2.drawString(g, mResources.no_mission, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
				return;
			}
			if (Char.myCharz().arrArchive == null || Char.myCharz().arrArchive.Length != this.currentListLength)
			{
				return;
			}
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = this.wScroll;
				int num4 = this.ITEM_HEIGHT - 1;
				Archivement archivement = Char.myCharz().arrArchive[i];
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, num4);
				if (archivement != null)
				{
					if (!archivement.isFinish)
					{
						mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
						mFont.tahoma_7_red.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
						mFont.tahoma_7_green.drawString(g, archivement.money.ToString() + " Thỏi Vàng", num + num3 - 5, num2, mFont.RIGHT);
					}
					else if (archivement.isFinish && !archivement.isRecieve)
					{
						mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
						mFont.tahoma_7_blue.drawString(g, mResources.reward_mission + archivement.money.ToString() + " Thỏi Vàng", num + 5, num2 + 11, 0);
						g.drawImage((i == this.selected) ? GameScr.imgLbtnFocus2 : GameScr.imgLbtn2, num + num3 - 20, num2 + num4 / 2, StaticObj.VCENTER_HCENTER);
						mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
					}
					else if (archivement.isFinish && archivement.isRecieve)
					{
						mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
						mFont.tahoma_7_red.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
						mFont.tahoma_7_green.drawString(g, mResources.received, num + num3 - 5, num2, mFont.RIGHT);
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x002BF264 File Offset: 0x002BD464
		private void paintCombine(mGraphics g)
		{
			g.setColor(6047789);
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			if (this.vItemCombine.size() == 0)
			{
				if (this.combineInfo != null)
				{
					for (int i = 0; i < this.combineInfo.Length; i++)
					{
						mFont.tahoma_7b_dark.drawString(g, this.combineInfo[i], this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - this.combineInfo.Length * 14 / 2 + i * 14 + 5, 2);
					}
				}
				return;
			}
			for (int j = 0; j < this.vItemCombine.size() + 1; j++)
			{
				int num = this.xScroll + 36;
				int num2 = this.yScroll + j * this.ITEM_HEIGHT;
				int num3 = this.wScroll - 36;
				int num4 = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + j * this.ITEM_HEIGHT;
				int num7 = 34;
				int num8 = this.ITEM_HEIGHT - 1;
				if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					if (j == this.vItemCombine.size())
					{
						if (this.vItemCombine.size() > 0)
						{
							if (!GameCanvas.isTouch && j == this.selected)
							{
								g.setColor(16383818);
								g.fillRect(num5, num2, this.wScroll, num4 + 2);
							}
							if ((j == this.selected && this.keyTouchCombine == 1) || (!GameCanvas.isTouch && j == this.selected))
							{
								g.drawImage(GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
								mFont.tahoma_7b_green2.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
							else
							{
								g.drawImage(GameScr.imgLbtn, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
								mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
						}
					}
					else
					{
						g.setColor((j != this.selected) ? 15196114 : 16383818);
						g.fillRect(num, num2, num3, num4, 5);
						g.setColor(6047789, 0.3f);
						Item item = (Item)this.vItemCombine.elementAt(j);
						g.fillRect(num5, num6, num7, num8, 5);
						this.paintEffectItem(g, item, num5, num6);
						if (item != null)
						{
							string text = string.Empty;
							mFont mFont2 = mFont.tahoma_7_green2;
							if (item.itemOption != null)
							{
								for (int k = 0; k < item.itemOption.Length; k++)
								{
									if (item.itemOption[k].optionTemplate.id == 72)
									{
										text = " [+" + item.itemOption[k].getOptionString() + "]";
									}
								}
							}
							if (ModFunc.isShowID)
							{
								mFont2.drawString(g, string.Concat(new string[]
								{
									"[",
									item.template.id.ToString(),
									"] ",
									item.template.name,
									text
								}), num + 5, num2 + 1, 0);
							}
							else
							{
								mFont2.drawString(g, item.template.name + text, num + 5, num2 + 1, 0);
							}
							string text2 = string.Empty;
							if (item.itemOption != null)
							{
								if (item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].IsValidOption())
								{
									text2 += item.itemOption[0].getOptionString();
								}
								mFont mFont3 = mFont.tahoma_7_blue;
								if (item.compare < 0 && item.template.type != 5)
								{
									mFont3 = mFont.tahoma_7_red;
								}
								if (item.itemOption.Length > 1)
								{
									for (int l = 1; l < item.itemOption.Length; l++)
									{
										if (item.itemOption[l] != null && item.itemOption[l].IsValidOption())
										{
											text2 = text2 + ", " + item.itemOption[l].getOptionString();
										}
									}
								}
								mFont3.drawString(g, text2, num + 5, num2 + 10, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
							if (item.itemOption != null)
							{
								for (int m = 0; m < item.itemOption.Length; m++)
								{
									this.paintOptItemInventory(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num5, num6, num7, num8, item);
								}
								for (int num9 = 0; num9 < item.itemOption.Length; num9++)
								{
									this.paintOptSlotItem(g, item.itemOption[num9].optionTemplate.id, item.itemOption[num9].param, num5, num6, num7, num8);
								}
							}
							if (item.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x002BF830 File Offset: 0x002BDA30
		private void paintInventoryNormal(mGraphics g)
		{
			g.setColor(16711680);
			Item[] arrItemBody2 = Char.myCharz().arrItemBody;
			Item[] arrItemBag2 = Char.myCharz().arrItemBag;
			this.currentListLength = this.checkCurrentListLength(arrItemBody2.Length + arrItemBag2.Length);
			int num18 = (arrItemBody2.Length + arrItemBag2.Length) / 20 + (((arrItemBody2.Length + arrItemBag2.Length) % 20 > 0) ? 1 : 0);
			this.TAB_W_NEW = this.wScroll / num18;
			for (int i = 0; i < num18; i++)
			{
				int num19 = (i == this.newSelected && this.selected == 0) ? ((GameCanvas.gameTick % 10 < 7) ? -1 : 0) : 0;
				g.setColor((i != this.newSelected) ? 15723751 : 16383818);
				g.fillRect(this.xScroll + i * this.TAB_W_NEW, 89 + num19 - 10, this.TAB_W_NEW - 1, 21);
				if (i == this.newSelected)
				{
					g.setColor(13524492);
					int x3 = this.xScroll + i * this.TAB_W_NEW;
					int num20 = 89 + num19 - 10 + 21;
					g.fillRect(x3, num20 - 3, this.TAB_W_NEW - 1, 3);
				}
				mFont.tahoma_7_grey.drawString(g, string.Empty + (i + 1).ToString(), this.xScroll + i * this.TAB_W_NEW + this.TAB_W_NEW / 2, 91 + num19 - 10, mFont.CENTER);
			}
			g.setClip(this.xScroll, this.yScroll + 21, this.wScroll, this.hScroll - 21);
			g.translate(0, -this.cmy);
			try
			{
				for (int j = 1; j < this.currentListLength; j++)
				{
					int num21 = this.xScroll + 36;
					int num22 = this.yScroll + j * this.ITEM_HEIGHT;
					int num23 = this.wScroll - 36;
					int h2 = this.ITEM_HEIGHT - 1;
					int num24 = this.xScroll;
					int num25 = this.yScroll + j * this.ITEM_HEIGHT;
					int num26 = 34;
					int num27 = this.ITEM_HEIGHT - 1;
					if (num22 - this.cmy <= this.yScroll + this.hScroll && num22 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
					{
						bool inventorySelect_isbody = this.GetInventorySelect_isbody(j, this.newSelected, Char.myCharz().arrItemBody);
						int inventorySelect_body = this.GetInventorySelect_body(j, this.newSelected);
						int inventorySelect_bag = this.GetInventorySelect_bag(j, this.newSelected, Char.myCharz().arrItemBody);
						g.setColor((j == this.selected) ? 16383818 : ((!inventorySelect_isbody) ? 15723751 : 15196114));
						g.fillRect(num21, num22, num23, h2, 5);
						g.setColor(6047789, 0.3f);
						Item item3 = (!inventorySelect_isbody) ? arrItemBag2[inventorySelect_bag] : arrItemBody2[inventorySelect_body];
						if (item3 != null)
						{
							for (int k = 0; k < item3.itemOption.Length; k++)
							{
								if (item3.itemOption[k].optionTemplate.id == 107 && item3.itemOption[k].param > 0 && item3.itemOption[k].param > 0)
								{
									this.paintItemStar(g, item3.itemOption[k].param.ToString(), num21 + 125, num22);
								}
							}
						}
						g.fillRect(num24, num25, num26, num27, 5);
						if (item3 != null)
						{
							this.paintEffectItem(g, item3, num24, num25);
							string text3 = string.Empty;
							mFont mFont4 = mFont.tahoma_7_green2;
							if (item3.itemOption != null)
							{
								for (int l = 0; l < item3.itemOption.Length; l++)
								{
									if (item3.itemOption[l].optionTemplate.id == 72)
									{
										text3 = " [+" + item3.itemOption[l].param.ToString() + "]";
									}
								}
							}
							mFont4.drawString(g, string.Concat(new string[]
							{
								"[",
								item3.template.id.ToString(),
								"] ",
								item3.template.name,
								text3
							}), num21 + 5, num22 + 1, 0);
							string text4 = string.Empty;
							if (item3.itemOption != null)
							{
								if (item3.itemOption.Length != 0 && item3.itemOption[0] != null && item3.itemOption[0].optionTemplate.id != 102 && item3.itemOption[0].optionTemplate.id != 107)
								{
									text4 += item3.itemOption[0].getOptionString();
								}
								mFont mFont5 = mFont.tahoma_7_blue;
								if (item3.compare < 0 && item3.template.type != 5)
								{
									mFont5 = mFont.tahoma_7_red;
								}
								if (item3.itemOption.Length > 1)
								{
									for (int m = 1; m < 2; m++)
									{
										if (item3.itemOption[m] != null && item3.itemOption[m].optionTemplate.id != 102 && item3.itemOption[m].optionTemplate.id != 107)
										{
											text4 = text4 + "," + item3.itemOption[m].getOptionString();
										}
									}
								}
								mFont5.drawString(g, text4, num21 + 5, num22 + 11, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item3.template.iconID, num24 + num26 / 2, num25 + num27 / 2, 0, 3);
							if (item3.itemOption != null)
							{
								for (int n = 0; n < item3.itemOption.Length; n++)
								{
									this.paintOptItem(g, item3.itemOption[n].optionTemplate.id, item3.itemOption[n].param, num24, num25, num26, num27);
								}
								for (int num28 = 0; num28 < item3.itemOption.Length; num28++)
								{
									this.paintOptSlotItem(g, item3.itemOption[num28].optionTemplate.id, item3.itemOption[num28].param, num24, num25, num26, num27);
								}
							}
							if (item3.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item3.quantity.ToString(), num24 + num26, num25 + num27 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x002BFEC8 File Offset: 0x002BE0C8
		private void paintInventory(mGraphics g)
		{
			if (!ModFunc.isInventory)
			{
				this.paintInventoryNormal(g);
				return;
			}
			g.setColor(16711680);
			Item[] arrItemBody = Char.myCharz().arrItemBody;
			Item[] arrItemBag = Char.myCharz().arrItemBag;
			this.currentListLength = this.checkCurrentListLengthNew(arrItemBody.Length + arrItemBag.Length / 5);
			this.TAB_W_NEW = 1;
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			try
			{
				int bodyStartY = this.yScroll;
				int bagStartY = bodyStartY + arrItemBody.Length * this.ITEM_HEIGHT;
				for (int i = 0; i < arrItemBody.Length; i++)
				{
					int x = this.xScroll + 36;
					int y = bodyStartY + i * this.ITEM_HEIGHT;
					int num18 = this.xScroll;
					int num7 = this.xScroll;
					int num8 = y;
					int num9 = 34;
					int num10 = this.ITEM_HEIGHT - 1;
					if (y - this.cmy <= this.yScroll + this.hScroll && y - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
					{
						this.GetInventorySelect_isbody(i, this.newSelected, Char.myCharz().arrItemBody);
						g.setColor((i == this.selected) ? 16383818 : 15196114);
						g.fillRect(x, y, this.wScroll, this.ITEM_HEIGHT - 1, 5);
						g.setColor(6047789, 0.3f);
						Item item = arrItemBody[i];
						if (item != null)
						{
							for (int j = 0; j < item.itemOption.Length; j++)
							{
								if (item.itemOption[j].optionTemplate.id == 107 && item.itemOption[j].param > 0 && item.itemOption[j].param > 0)
								{
									this.paintItemStar(g, item.itemOption[j].param.ToString(), x + 125, y);
								}
							}
						}
						g.fillRect(num7, num8, num9, num10, 5);
						this.paintEffectItem(g, item, num7, num8);
						if (item != null && item.isSelect && GameCanvas.panel.type == 12)
						{
							g.setColor(6047789, 0.3f);
							g.fillRect(num7, num8, num9, num10, 5);
						}
						if (item != null && item != null)
						{
							string text = string.Empty;
							mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
							{
								"[",
								item.template.id.ToString(),
								"] ",
								item.template.name,
								text
							}), x + 5, y + 1, 0);
							string text2 = string.Empty;
							if (item.itemOption != null)
							{
								if (item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].IsValidOption())
								{
									text2 += item.itemOption[0].getOptionString();
								}
								mFont mFont3 = mFont.tahoma_7_blue;
								if (item.compare < 0 && item.template.type != 5)
								{
									mFont3 = mFont.tahoma_7_red;
								}
								if (item.itemOption.Length > 1)
								{
									for (int num11 = 1; num11 < Math.min(item.itemOption.Length, 3); num11++)
									{
										if (item.itemOption[num11] != null && item.itemOption[num11].IsValidOption())
										{
											text2 = text2 + "| " + item.itemOption[num11].getOptionString();
										}
									}
								}
								mFont3.drawString(g, text2, x + 5, y + 10, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num7 + num9 / 2, num8 + num10 / 2, 0, 3);
							if (item.itemOption != null)
							{
								for (int k = 0; k < item.itemOption.Length; k++)
								{
									this.paintOptItemInventory(g, item.itemOption[k].optionTemplate.id, item.itemOption[k].param, num7, num8, num9, num10, item);
								}
								for (int num12 = 0; num12 < item.itemOption.Length; num12++)
								{
									this.paintOptSlotItem(g, item.itemOption[num12].optionTemplate.id, item.itemOption[num12].param, num7, num8, num9, num10);
								}
							}
						}
					}
				}
				for (int l = 0; l < arrItemBag.Length; l++)
				{
					int num13 = 34;
					int x2 = this.xScroll + l % 5 * (num13 + 2);
					int y2 = bagStartY + l / 5 * this.ITEM_HEIGHT;
					int num14 = x2;
					int num15 = y2;
					int num16 = this.ITEM_HEIGHT - 1;
					if (y2 - this.cmy <= this.yScroll + this.hScroll && y2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
					{
						int inventorySelect_bag = this.GetInventorySelect_bag(this.selected, this.newSelected, Char.myCharz().arrItemBody);
						if (l == inventorySelect_bag)
						{
							g.setColor(16383818);
							g.fillRect(x2, y2, num13, num16, 5);
						}
						Item item2 = arrItemBag[l];
						if (item2 != null)
						{
							for (int m = 0; m < item2.itemOption.Length; m++)
							{
								if (item2.itemOption[m].optionTemplate.id == 72 && item2.itemOption[m].param > 0)
								{
									byte id = (byte)Panel.GetColor_Item_Upgrade(item2.itemOption[m].param);
									if (Panel.GetColor_ItemBg((int)id) != -1)
									{
										g.setColor((l != inventorySelect_bag) ? Panel.GetColor_ItemBg((int)id) : Panel.GetColor_ItemBg((int)id));
									}
								}
							}
						}
						g.setColor(6047789, 0.3f);
						g.fillRect(x2, y2, num13, num16, 5);
						this.paintEffectItem(g, item2, x2, y2);
						if (item2 != null && item2.isSelect && GameCanvas.panel.type == 12)
						{
							g.setColor((l != inventorySelect_bag) ? 6047789 : 7040779);
							g.fillRect(x2, y2, num13, num16, 5);
						}
						if (item2 != null)
						{
							mFont tahoma_7_green = mFont.tahoma_7_green2;
							if (item2 != null)
							{
								if (item2.itemOption != null)
								{
									for (int n = 0; n < item2.itemOption.Length; n++)
									{
										if (item2.itemOption[n].optionTemplate.id == 72)
										{
											if (item2.itemOption[n].param >= 1 && item2.itemOption[n].param <= 5)
											{
												Panel.GetFont(0);
											}
											else if (item2.itemOption[n].param >= 6 && item2.itemOption[n].param <= 7)
											{
												Panel.GetFont(8);
											}
											else if (item2.itemOption[n].param >= 8 && item2.itemOption[n].param <= 10)
											{
												Panel.GetFont(7);
											}
										}
									}
								}
								SmallImage.drawSmallImage(g, (int)item2.template.iconID, x2 + num13 / 2, y2 + this.ITEM_HEIGHT / 2, 0, 3);
								if (item2.itemOption != null)
								{
									for (int n2 = 0; n2 < item2.itemOption.Length; n2++)
									{
										this.paintOptItem(g, item2.itemOption[n2].optionTemplate.id, item2.itemOption[n2].param, num14, num15, num13, num16);
									}
									for (int num17 = 0; num17 < item2.itemOption.Length; num17++)
									{
										this.paintOptSlotItem(g, item2.itemOption[num17].optionTemplate.id, item2.itemOption[num17].param, num14, num15, num13, num16);
									}
								}
								if (item2.quantity > 1)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item2.quantity.ToString(), x2 + num13, y2 + num16 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x002C0708 File Offset: 0x002BE908
		private void paintTab(mGraphics g)
		{
			if (this.type == 26)
			{
				for (int i = 0; i < Panel.boxMod.Length; i++)
				{
					g.setColor((i != this.currentTabIndex) ? 16773296 : 6805896);
					PopUp.paintPopUp(g, this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25, (i == this.currentTabIndex) ? 1 : 0, true);
					if (i == this.keyTouchTab)
					{
						g.drawImage(ItemMap.imageFlare, this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 62, 3);
					}
					mFont mFont2 = (i != this.currentTabIndex) ? mFont.tahoma_7_grey : mFont.tahoma_7_green2;
					if (!Panel.boxMod[i][1].Equals(string.Empty))
					{
						mFont2.drawString(g, Panel.boxMod[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 53, mFont.CENTER);
						mFont2.drawString(g, Panel.boxMod[i][1], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 64, mFont.CENTER);
					}
					else
					{
						mFont2.drawString(g, Panel.boxMod[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 59, mFont.CENTER);
					}
				}
				if (this.keyTouchTab != -1)
				{
					this.currentTabIndex = this.keyTouchTab;
					this.loadTabModFunc();
					this.keyTouchTab = -1;
				}
			}
			if (this.type == 2 && GameCanvas.panel2 != null)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.chest, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 3)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.select_zone, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 4)
			{
				mFont.tahoma_7b_dark.drawString(g, mResources.map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				return;
			}
			if (this.type == 7)
			{
				mFont.tahoma_7b_dark.drawString(g, mResources.trangbi, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				return;
			}
			if (this.type == 8)
			{
				mFont.tahoma_7b_dark.drawString(g, mResources.msg + ModFunc.strClickToChat, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				return;
			}
			if (this.type == 9)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.achievement_mission, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 10)
			{
				mFont.tahoma_7b_dark.drawString(g, mResources.wat_do_u_want, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				return;
			}
			if (this.type == 11)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.friend, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 12 && GameCanvas.panel2 != null)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 13 && this.Equals(GameCanvas.panel2))
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.item_receive2, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 14)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.select_map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 15)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, this.topName, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 16)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.enemy, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 17)
			{
				mFont.tahoma_7b_dark.drawString(g, mResources.kigui, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				return;
			}
			if (this.type == 18)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.change_flag, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 19)
			{
				string s = (this.type == 19) ? mResources.option : ((this.type == 26) ? ModFunc.strModFunc : ModFunc.strPlayerInfo);
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, s, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 20)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.account, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 22)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.autoFunction, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 23 || this.type == 24)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.gameInfo, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.type == 27)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, ModFunc.strPlayerInfo, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				return;
			}
			if (this.currentTabIndex == 3 && this.mainTabName.Length != 4)
			{
				g.translate(-this.cmx, 0);
			}
			for (int j = 0; j < this.currentTabName.Length; j++)
			{
				g.setColor((j != this.currentTabIndex) ? 16773296 : 6805896);
				PopUp.paintPopUp(g, this.startTabPos + j * this.TAB_W, 52, this.TAB_W - 1, 25, (j == this.currentTabIndex) ? 1 : 0, true);
				if (j == this.keyTouchTab)
				{
					g.drawImage(ItemMap.imageFlare, this.startTabPos + j * this.TAB_W + this.TAB_W / 2, 62, 3);
				}
				mFont mFont3 = (j != this.currentTabIndex) ? mFont.tahoma_7_grey : mFont.tahoma_7_green2;
				if (!this.currentTabName[j][1].Equals(string.Empty))
				{
					mFont3.drawString(g, this.currentTabName[j][0], this.startTabPos + j * this.TAB_W + this.TAB_W / 2, 53, mFont.CENTER);
					mFont3.drawString(g, this.currentTabName[j][1], this.startTabPos + j * this.TAB_W + this.TAB_W / 2, 64, mFont.CENTER);
				}
				else
				{
					mFont3.drawString(g, this.currentTabName[j][0], this.startTabPos + j * this.TAB_W + this.TAB_W / 2, 59, mFont.CENTER);
				}
				if (this.type == 0 && this.currentTabName.Length == 5 && GameScr.isNewClanMessage && GameCanvas.gameTick % 4 == 0)
				{
					g.drawImage(ItemMap.imageFlare, this.startTabPos + 3 * this.TAB_W + this.TAB_W / 2, 77, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			g.setColor(13524492);
			g.fillRect(1, 78, this.W - 2, 1);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x002C1168 File Offset: 0x002BF368
		private void paintBottomMoneyInfo(mGraphics g)
		{
			if (this.type != 13 || (this.currentTabIndex != 2 && !this.Equals(GameCanvas.panel2)))
			{
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				g.setColor(11837316);
				g.fillRect(this.X + 1, this.H - 15, this.W - 2, 14);
				g.setColor(13524492);
				g.fillRect(this.X + 1, this.H - 15, this.W - 2, 1);
				g.drawImage(Panel.imgXu, this.X + 11, this.H - 7, 3);
				g.drawImage(Panel.imgLuong, this.X + 75, this.H - 8, 3);
				mFont.tahoma_7_yellow.drawString(g, Char.myCharz().xuStr + string.Empty, this.X + 24, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_yellow.drawString(g, Char.myCharz().luongStr + string.Empty, this.X + 85, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
				g.drawImage(Panel.imgLuongKhoa, this.X + 130, this.H - 8, 3);
				mFont.tahoma_7_yellow.drawString(g, Char.myCharz().luongKhoaStr + string.Empty, this.X + 140, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
			}
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x002C1314 File Offset: 0x002BF514
		private void paintClanInfo(mGraphics g)
		{
			if (Char.myCharz().clan == null)
			{
				SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), 25, 50, 0, 33);
				mFont.tahoma_7b_white.drawString(g, mResources.not_join_clan, (this.wScroll - 50) / 2 + 50, 20, mFont.CENTER);
				return;
			}
			if (!this.isViewMember)
			{
				Clan clan = Char.myCharz().clan;
				if (clan != null)
				{
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), 25, 50, 0, 33);
					mFont.tahoma_7b_white.drawString(g, clan.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
					mFont.tahoma_7_yellow.drawString(g, mResources.achievement_point + ": " + clan.powerPoint, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
					mFont.tahoma_7_yellow.drawString(g, mResources.clan_point + ": " + clan.clanPoint.ToString(), 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
					mFont.tahoma_7_yellow.drawString(g, mResources.level + ": " + clan.level.ToString(), 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
					TextInfo.paint(g, clan.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
					return;
				}
			}
			else
			{
				Clan clan2 = (this.currClan == null) ? Char.myCharz().clan : this.currClan;
				SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), 25, 50, 0, 33);
				mFont.tahoma_7b_white.drawString(g, clan2.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
				mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
				{
					mResources.member,
					": ",
					clan2.currMember.ToString(),
					"/",
					clan2.maxMember.ToString()
				}), 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_yellow.drawString(g, mResources.clan_leader + ": " + clan2.leaderName, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
				TextInfo.paint(g, clan2.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
			}
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x002C1570 File Offset: 0x002BF770
		private void paintToolInfo(mGraphics g)
		{
			mFont.tahoma_7b_white.drawString(g, mResources.dragon_ball + " " + GameMidlet.VERSION, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7_yellow.drawString(g, (Char.myCharz().isTichXanh ? "     " : string.Empty) + Char.myCharz().cName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			if (Char.myCharz().isTichXanh)
			{
				ModFunc.PaintTicks(g, 58, 17);
			}
			string text = (!GameCanvas.loginScr.tfUser.getText().Equals(string.Empty)) ? GameCanvas.loginScr.tfUser.getText() : mResources.not_register_yet;
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
			{
				mResources.account_server,
				" ",
				ServerListScreen.nameServer[ServerListScreen.ipSelect],
				": ",
				text
			}), 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x002C1680 File Offset: 0x002BF880
		private void paintGiaoDichInfo(mGraphics g)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.select_item, 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, mResources.lock_trade, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, mResources.wait_opp_lock_trade, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, mResources.press_done, 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x002C1704 File Offset: 0x002BF904
		private void paintMyInfo(mGraphics g)
		{
			this.paintCharInfo(g, Char.myCharz());
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x002C1714 File Offset: 0x002BF914
		private void paintPetInfo(mGraphics g, bool isPet2)
		{
			Char pet = isPet2 ? Char.MyPet2z() : Char.myPetz();
			mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(pet.cPower), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
			if (pet.cPower > 0L)
			{
				mFont.tahoma_7_yellow.drawString(g, (!pet.me) ? pet.currStrLevel : pet.getStrLevel(), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			}
			if (pet.cDamFull > 0L)
			{
				mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + NinjaUtil.getMoneys(pet.cDamFull), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
			}
			if (pet.cMaxStamina > 0)
			{
				mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
				g.drawImage(GameScr.imgMPLost, this.X + 100, 41, 0);
				int num = pet.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)pet.cMaxStamina;
				g.setClip(100, this.X + 41, num, 20);
				g.drawImage(GameScr.imgMP, this.X + 100, 41, 0);
			}
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x002C188C File Offset: 0x002BFA8C
		private void paintPetSkillInfo(mGraphics g, bool isPet2)
		{
			Char pet = isPet2 ? Char.MyPet2z() : Char.myPetz();
			mFont.tahoma_7b_white.drawString(g, "HP: " + NinjaUtil.getMoneys(pet.cHP) + "/" + NinjaUtil.getMoneys(pet.cHPFull), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7b_white.drawString(g, "MP: " + NinjaUtil.getMoneys(pet.cMP) + "/" + NinjaUtil.getMoneys(pet.cMPFull), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
			{
				mResources.critical,
				": ",
				pet.cCriticalFull.ToString(),
				"   ",
				mResources.armor,
				": ",
				NinjaUtil.getMoneys(pet.cDefull)
			}), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, mResources.potential2 + ": " + NinjaUtil.getMoneys(pet.cTiemNang), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x002C19DC File Offset: 0x002BFBDC
		private void paintCharInfo(mGraphics g, Char c)
		{
			mFont.tahoma_7b_white.drawString(g, (c.isTichXanh ? "     " : string.Empty) + c.cName, this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
			if (c.isTichXanh)
			{
				ModFunc.PaintTicks(g, this.X + 60, 5);
			}
			if (c.cMaxStamina > 0)
			{
				mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
				g.drawImage(GameScr.imgMPLost, this.X + 95, 19, 0);
				int num = c.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)c.cMaxStamina;
				g.setClip(95, this.X + 19, num, 20);
				g.drawImage(GameScr.imgMP, this.X + 95, 19, 0);
			}
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (c.cPower > 0L)
			{
				mFont.tahoma_7_yellow.drawString(g, (!c.me) ? c.currStrLevel : c.getStrLevel(), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
			}
			mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(c.cPower), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x002C1B58 File Offset: 0x002BFD58
		private void paintZoneInfo(mGraphics g)
		{
			mFont.tahoma_7b_white.drawString(g, mResources.zone + " " + TileMap.zoneID.ToString(), 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7_yellow.drawString(g, TileMap.mapName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7b_white.drawString(g, TileMap.zoneID.ToString() + string.Empty, 25, 27, mFont.CENTER);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x002C1BDC File Offset: 0x002BFDDC
		public int getCompare(Item item)
		{
			if (item == null)
			{
				return -1;
			}
			if (!item.isTypeBody())
			{
				return 0;
			}
			if (item.itemOption == null)
			{
				return -1;
			}
			ItemOption itemOption = item.itemOption[0];
			if (itemOption.optionTemplate.id == 22)
			{
				itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
				itemOption.param *= 1000;
			}
			if (itemOption.optionTemplate.id == 23)
			{
				itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
				itemOption.param *= 1000;
			}
			Item item2 = null;
			for (int i = 0; i < Char.myCharz().arrItemBody.Length; i++)
			{
				Item item3 = Char.myCharz().arrItemBody[i];
				if (itemOption.optionTemplate.id == 22)
				{
					itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
					itemOption.param *= 1000;
				}
				if (itemOption.optionTemplate.id == 23)
				{
					itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
					itemOption.param *= 1000;
				}
				if (item3 != null && item3.itemOption != null && item3.template.type == item.template.type)
				{
					item2 = item3;
					break;
				}
			}
			if (item2 == null)
			{
				this.isUp = true;
				return itemOption.param;
			}
			int num = (item2 == null || item2.itemOption == null) ? itemOption.param : (itemOption.param - item2.itemOption[0].param);
			if (num < 0)
			{
				this.isUp = false;
				return num;
			}
			this.isUp = true;
			return num;
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x002C1D80 File Offset: 0x002BFF80
		private void paintMapInfo(mGraphics g)
		{
			mFont.tahoma_7b_white.drawString(g, mResources.MENUGENDER[(int)TileMap.planetID], 60, 4, mFont.LEFT);
			string text = string.Empty;
			if (TileMap.mapID >= 135 && TileMap.mapID <= 138)
			{
				text = " " + mResources.tang + TileMap.zoneID.ToString();
			}
			mFont.tahoma_7_yellow.drawString(g, TileMap.mapName + text, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7b_white.drawString(g, mResources.quest_place + ": ", 60, 27, mFont.LEFT);
			if (GameScr.getTaskMapId() >= 0 && GameScr.getTaskMapId() <= TileMap.mapNames.Length - 1)
			{
				mFont.tahoma_7_yellow.drawString(g, TileMap.mapNames[GameScr.getTaskMapId()], 60, 38, mFont.LEFT);
				return;
			}
			mFont.tahoma_7_yellow.drawString(g, mResources.random, 60, 38, mFont.LEFT);
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x002C1E7C File Offset: 0x002C007C
		private void paintShopInfo(mGraphics g)
		{
			if (this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null)
			{
				this.paintMyInfo(g);
				return;
			}
			if (this.selected < 0)
			{
				if (this.typeShop != 2)
				{
					mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 14, 0);
					mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 26, 0);
					return;
				}
				mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 5, 0);
				mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 17, 0);
				mFont.tahoma_7_white.drawString(g, string.Concat(new string[]
				{
					mResources.page,
					" ",
					(this.currPageShop[this.currentTabIndex] + 1).ToString(),
					"/",
					this.maxPageShop[this.currentTabIndex].ToString()
				}), this.X + 60, 29, 0);
				return;
			}
			else
			{
				if (this.currentTabIndex < 0 || this.currentTabIndex > Char.myCharz().arrItemShop.Length - 1 || this.selected < 0 || this.selected > Char.myCharz().arrItemShop[this.currentTabIndex].Length - 1)
				{
					return;
				}
				Item item = Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
				if (item != null)
				{
					if (this.Equals(GameCanvas.panel) && this.currentTabIndex <= 3 && this.typeShop == 2)
					{
						mFont.tahoma_7b_white.drawString(g, string.Concat(new string[]
						{
							mResources.page,
							" ",
							(this.currPageShop[this.currentTabIndex] + 1).ToString(),
							"/",
							this.maxPageShop[this.currentTabIndex].ToString()
						}), this.X + 55, 4, 0);
					}
					mFont.tahoma_7b_white.drawString(g, item.template.name, this.X + 55, 24, 0);
					string st = mResources.pow_request + " " + Res.formatNumber((long)item.template.strRequire);
					if ((long)item.template.strRequire > Char.myCharz().cPower)
					{
						mFont.tahoma_7_yellow.drawString(g, st, this.X + 55, 35, 0);
						return;
					}
					mFont.tahoma_7_green.drawString(g, st, this.X + 55, 35, 0);
				}
				return;
			}
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x002C211C File Offset: 0x002C031C
		private void paintItemBoxInfo(mGraphics g)
		{
			string st = string.Concat(new string[]
			{
				mResources.used,
				": ",
				this.hasUse.ToString(),
				"/",
				Char.myCharz().arrItemBox.Length.ToString(),
				" ",
				mResources.place
			});
			mFont.tahoma_7b_white.drawString(g, mResources.chest, 60, 4, 0);
			mFont.tahoma_7_yellow.drawString(g, st, 60, 16, 0);
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x002C21A8 File Offset: 0x002C03A8
		private void paintSkillInfo(mGraphics g)
		{
			mFont.tahoma_7_white.drawString(g, "Top " + Char.myCharz().rank.ToString(), this.X + 45 + (this.W - 50) / 2, 2, mFont.CENTER);
			mFont.tahoma_7_yellow.drawString(g, mResources.potential_point, this.X + 45 + (this.W - 50) / 2, 14, mFont.CENTER);
			mFont.tahoma_7_white.drawString(g, string.Empty + NinjaUtil.getMoneys(Char.myCharz().cTiemNang), this.X + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0) + 45 + (this.W - 50) / 2, 26, mFont.CENTER);
			mFont.tahoma_7_yellow.drawString(g, mResources.active_point + ": " + NinjaUtil.getMoneys(Char.myCharz().cNangdong), this.X + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0) + 45 + (this.W - 50) / 2, 38, mFont.CENTER);
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x002C22D8 File Offset: 0x002C04D8
		private void paintItemBodyBagInfo(mGraphics g)
		{
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
			{
				mResources.HP,
				": ",
				NinjaUtil.getMoneys(Char.myCharz().cHP),
				" / ",
				NinjaUtil.getMoneys(Char.myCharz().cHPFull)
			}), this.X + 60, 2, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
			{
				mResources.KI,
				": ",
				NinjaUtil.getMoneys(Char.myCharz().cMP),
				" / ",
				NinjaUtil.getMoneys(Char.myCharz().cMPFull)
			}), this.X + 60, 14, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + NinjaUtil.getMoneys(Char.myCharz().cDamFull), this.X + 60, 26, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
			{
				mResources.armor,
				": ",
				NinjaUtil.getMoneys(Char.myCharz().cDefull),
				", ",
				mResources.critical,
				": ",
				Char.myCharz().cCriticalFull.ToString(),
				"%"
			}), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x002C2470 File Offset: 0x002C0670
		private void paintTopInfo(mGraphics g)
		{
			g.setClip(this.X + 1, this.Y, this.W - 2, this.yScroll - 2);
			g.setColor(9993045);
			g.fillRect(this.X, this.Y, this.W - 2, 50);
			switch (this.type)
			{
				case 0:
					try
					{
						if (this.currentTabIndex == 0)
						{
							SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
							this.paintMyInfo(g);
						}
						if (this.currentTabIndex == 1)
						{
							SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
							if (this.isnewInventory)
							{
								this.paintCharInfo(g, Char.myCharz());
							}
							else
							{
								this.paintItemBodyBagInfo(g);
							}
						}
						if (this.currentTabIndex == 2)
						{
							SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
							this.paintSkillInfo(g);
						}
						if (this.currentTabIndex == 3)
						{
							if (this.mainTabName.Length == 5)
							{
								this.paintClanInfo(g);
							}
							else
							{
								SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
								this.paintToolInfo(g);
							}
						}
						if (this.currentTabIndex == 4)
						{
							SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
							this.paintToolInfo(g);
						}
						return;
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						return;
					}
					break;
				case 1:
					if (this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null)
					{
						SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					}
					else
					{
						SmallImage.drawSmallImage(g, Char.myCharz().npcFocus.avatar, this.X + 25, 50, 0, 33);
					}
					this.paintShopInfo(g);
					return;
				case 2:
					if (this.currentTabIndex == 0)
					{
						SmallImage.drawSmallImage(g, 526, this.X + 25, 50, 0, 33);
						this.paintItemBoxInfo(g);
					}
					if (this.currentTabIndex == 1)
					{
						SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
						this.paintItemBodyBagInfo(g);
						return;
					}
					return;
				case 3:
					SmallImage.drawSmallImage(g, 561, this.X + 25, 50, 0, 33);
					this.paintZoneInfo(g);
					return;
				case 4:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMapInfo(g);
					return;
				case 5:
				case 6:
					return;
				case 7:
				case 17:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMyInfo(g);
					return;
				case 8:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMyInfo(g);
					return;
				case 9:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMyInfo(g);
					return;
				case 10:
					if (this.charMenu != null)
					{
						SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
						this.paintCharInfo(g, this.charMenu);
						return;
					}
					return;
				case 11:
				case 16:
				case 23:
				case 24:
				case 27:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMyInfo(g);
					return;
				case 12:
					if (this.currentTabIndex == 0)
					{
						int id = 1410;
						for (int i = 0; i < GameScr.vNpc.size(); i++)
						{
							Npc npc = (Npc)GameScr.vNpc.elementAt(i);
							if (npc.template.npcTemplateId == this.idNPC)
							{
								id = npc.avatar;
							}
						}
						SmallImage.drawSmallImage(g, id, this.X + 25, 50, 0, 33);
						this.paintCombineInfo(g);
					}
					if (this.currentTabIndex == 1)
					{
						SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
						this.paintMyInfo(g);
						return;
					}
					return;
				case 13:
					if (this.currentTabIndex == 0 || this.currentTabIndex == 1)
					{
						if (this.Equals(GameCanvas.panel))
						{
							SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
							this.paintGiaoDichInfo(g);
						}
						if (this.Equals(GameCanvas.panel2) && this.charMenu != null)
						{
							SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
							this.paintCharInfo(g, this.charMenu);
						}
					}
					if (this.currentTabIndex == 2 && this.charMenu != null)
					{
						SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
						this.paintCharInfo(g, this.charMenu);
						return;
					}
					return;
				case 14:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMapInfo(g);
					return;
				case 15:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMyInfo(g);
					return;
				case 18:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMyInfo(g);
					return;
				case 19:
				case 26:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintToolInfo(g);
					return;
				case 20:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintToolInfo(g);
					return;
				case 21:
				case 28:
					{
						Char pet = (this.type == 28) ? Char.MyPet2z() : Char.myPetz();
						if (this.currentTabIndex == 0)
						{
							SmallImage.drawSmallImage(g, pet.avatarz(), this.X + 25, 50, 0, 33);
							this.paintPetInfo(g, this.type == 28);
							return;
						}
						if (this.currentTabIndex == 1)
						{
							SmallImage.drawSmallImage(g, pet.avatarz(), this.X + 25, 50, 0, 33);
							this.paintPetSkillInfo(g, this.type == 28);
							return;
						}
						if (this.currentTabIndex == 2)
						{
							SmallImage.drawSmallImage(g, pet.avatarz(), this.X + 25, 50, 0, 33);
							this.paintPetStatusInfo(g, this.type == 28);
							return;
						}
						if (this.currentTabIndex == 3)
						{
							SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
							this.paintItemBodyBagInfo(g);
							return;
						}
						return;
					}
				case 22:
					SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintToolInfo(g);
					return;
				case 25:
					break;
				default:
					return;
			}
			SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x002C2BB4 File Offset: 0x002C0DB4
		private void paintPetStatusInfo(mGraphics g, bool isPet2)
		{
			Char pet = isPet2 ? Char.MyPet2z() : Char.myPetz();
			mFont.tahoma_7b_white.drawString(g, "HP: " + NinjaUtil.getMoneys(pet.cHP) + "/" + NinjaUtil.getMoneys(pet.cHPFull), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7b_white.drawString(g, "MP: " + NinjaUtil.getMoneys(pet.cMP) + "/" + NinjaUtil.getMoneys(pet.cMPFull), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
			{
				mResources.critical,
				": ",
				NinjaUtil.getMoneys((long)pet.cCriticalFull),
				"   ",
				mResources.armor,
				": ",
				NinjaUtil.getMoneys(pet.cDefull)
			}), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, mResources.status + ": " + this.strStatus[(int)pet.petStatus], this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x002C2D08 File Offset: 0x002C0F08
		private void paintCombineInfo(mGraphics g)
		{
			if (this.combineTopInfo != null)
			{
				for (int i = 0; i < this.combineTopInfo.Length; i++)
				{
					mFont.tahoma_7_white.drawString(g, this.combineTopInfo[i], this.X + 45 + (this.W - 50) / 2, 5 + i * 14, mFont.CENTER);
				}
			}
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x002C2D64 File Offset: 0x002C0F64
		public void paintMap(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(-this.cmxMap, -this.cmyMap);
			g.drawImage(Panel.imgMap, this.xScroll, this.yScroll, 0);
			int head = Char.myCharz().head;
			Part part = GameScr.parts[head];
			SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[0][0][0]].id, this.xMap, this.yMap + 5, 0, 3);
			int align = mFont.CENTER;
			if (this.xMap <= 40)
			{
				align = mFont.LEFT;
			}
			if (this.xMap >= 220)
			{
				align = mFont.RIGHT;
			}
			mFont.tahoma_7b_yellow.drawString(g, TileMap.mapName, this.xMap, this.yMap - 12, align, mFont.tahoma_7_grey);
			int num = -1;
			if (GameScr.getTaskMapId() != -1)
			{
				for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
				{
					if (Panel.mapId[(int)TileMap.planetID][i] == GameScr.getTaskMapId())
					{
						num = i;
						break;
					}
					num = 4;
				}
				if (GameCanvas.gameTick % 4 > 0)
				{
					g.drawImage(ItemMap.imageFlare, this.xScroll + Panel.mapX[(int)TileMap.planetID][num], this.yScroll + Panel.mapY[(int)TileMap.planetID][num], 3);
				}
			}
			if (!GameCanvas.isTouch)
			{
				g.drawImage(Panel.imgBantay, this.xMove, this.yMove, StaticObj.TOP_RIGHT);
				for (int j = 0; j < Panel.mapX[(int)TileMap.planetID].Length; j++)
				{
					int num2 = Panel.mapX[(int)TileMap.planetID][j] + this.xScroll;
					int num3 = Panel.mapY[(int)TileMap.planetID][j] + this.yScroll;
					if (Res.inRect(num2 - 15, num3 - 15, 30, 30, this.xMove, this.yMove))
					{
						align = mFont.CENTER;
						if (num2 <= 20)
						{
							align = mFont.LEFT;
						}
						if (num2 >= 220)
						{
							align = mFont.RIGHT;
						}
						mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][j]], num2, num3 - 12, align, mFont.tahoma_7_grey);
						break;
					}
				}
			}
			else if (!this.trans)
			{
				for (int k = 0; k < Panel.mapX[(int)TileMap.planetID].Length; k++)
				{
					int num4 = Panel.mapX[(int)TileMap.planetID][k] + this.xScroll;
					int num5 = Panel.mapY[(int)TileMap.planetID][k] + this.yScroll;
					if (Res.inRect(num4 - 15, num5 - 15, 30, 30, this.pX, this.pY))
					{
						align = mFont.CENTER;
						if (num4 <= 30)
						{
							align = mFont.LEFT;
						}
						if (num4 >= 220)
						{
							align = mFont.RIGHT;
						}
						g.drawImage(Panel.imgBantay, num4, num5, StaticObj.TOP_RIGHT);
						mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][k]], num4, num5 - 12, align, mFont.tahoma_7_grey);
						break;
					}
				}
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			if (num != -1)
			{
				if (Panel.mapX[(int)TileMap.planetID][num] + this.xScroll < this.cmxMap)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 5, this.xScroll + 5, this.yScroll + this.hScroll / 2 - 4, 0);
				}
				if (this.cmxMap + this.wScroll < Panel.mapX[(int)TileMap.planetID][num] + this.xScroll)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 6, this.xScroll + this.wScroll - 5, this.yScroll + this.hScroll / 2 - 4, StaticObj.TOP_RIGHT);
				}
				if (Panel.mapY[(int)TileMap.planetID][num] < this.cmyMap)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll / 2, this.yScroll + 5, StaticObj.TOP_CENTER);
				}
				if (Panel.mapY[(int)TileMap.planetID][num] > this.cmyMap + this.hScroll)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 5, StaticObj.BOTTOM_HCENTER);
				}
			}
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x002C31E4 File Offset: 0x002C13E4
		public void paintTask(mGraphics g)
		{
			int num = (GameCanvas.h <= 300) ? 15 : 20;
			if (Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && !GameScr.gI().isMapFize())
			{
				g.drawImage((this.keyTouchMapButton != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - num, 3);
				mFont.tahoma_7b_dark.drawString(g, mResources.map, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - (num + 5), mFont.CENTER);
			}
			this.xstart = this.xScroll + 5;
			this.ystart = this.yScroll + 14;
			this.yPaint = this.ystart;
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll - 35);
			if (this.scroll != null)
			{
				if (this.scroll.cmy > 0)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
				}
				if (this.scroll.cmy < this.scroll.cmyLim)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 45, 0);
				}
				g.translate(0, -this.scroll.cmy);
			}
			this.indexRowMax = 0;
			if (this.indexMenu == 0)
			{
				bool flag = false;
				if (Char.myCharz().taskMaint != null)
				{
					for (int i = 0; i < Char.myCharz().taskMaint.names.Length; i++)
					{
						mFont.tahoma_7_grey.drawString(g, Char.myCharz().taskMaint.names[i], this.xScroll + this.wScroll / 2, this.yPaint - 5 + i * 12, mFont.CENTER);
						this.indexRowMax++;
					}
					this.yPaint += (Char.myCharz().taskMaint.names.Length - 1) * 12;
					int num2 = 0;
					string empty = string.Empty;
					for (int j = 0; j < Char.myCharz().taskMaint.subNames.Length; j++)
					{
						if (Char.myCharz().taskMaint.subNames[j] != null)
						{
							num2 = j;
							empty = "- " + Char.myCharz().taskMaint.subNames[j];
							if (Char.myCharz().taskMaint.counts[j] != -1)
							{
								if (Char.myCharz().taskMaint.index == j)
								{
									if (Char.myCharz().taskMaint.count != Char.myCharz().taskMaint.counts[j])
									{
										string text = empty;
										empty = string.Concat(new string[]
										{
											text,
											" (",
											Char.myCharz().taskMaint.count.ToString(),
											"/",
											Char.myCharz().taskMaint.counts[j].ToString(),
											")"
										});
									}
									if (Char.myCharz().taskMaint.count == Char.myCharz().taskMaint.counts[j])
									{
										mFont.tahoma_7.drawString(g, empty, this.xstart + 5, this.yPaint += 12, 0);
									}
									else
									{
										mFont tahoma_7_grey = mFont.tahoma_7_grey;
										if (!flag)
										{
											flag = true;
											tahoma_7_grey = mFont.tahoma_7_blue;
											tahoma_7_grey.drawString(g, empty, this.xstart + 5 + ((tahoma_7_grey == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
										}
										else
										{
											tahoma_7_grey.drawString(g, "- ...", this.xstart + 5 + ((tahoma_7_grey == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
										}
									}
								}
								else if (Char.myCharz().taskMaint.index > j)
								{
									if (Char.myCharz().taskMaint.counts[j] != 1)
									{
										string text2 = empty;
										empty = string.Concat(new string[]
										{
											text2,
											" (",
											Char.myCharz().taskMaint.counts[j].ToString(),
											"/",
											Char.myCharz().taskMaint.counts[j].ToString(),
											")"
										});
									}
									mFont.tahoma_7_white.drawString(g, empty, this.xstart + 5, this.yPaint += 12, 0);
								}
								else
								{
									if (Char.myCharz().taskMaint.counts[j] != 1)
									{
										empty = empty + " 0/" + Char.myCharz().taskMaint.counts[j].ToString();
									}
									mFont tahoma_7_grey2 = mFont.tahoma_7_grey;
									if (!flag)
									{
										flag = true;
										tahoma_7_grey2 = mFont.tahoma_7_blue;
										tahoma_7_grey2.drawString(g, empty, this.xstart + 5 + ((tahoma_7_grey2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
									}
									else
									{
										tahoma_7_grey2.drawString(g, "- ...", this.xstart + 5 + ((tahoma_7_grey2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
									}
								}
							}
							else if (Char.myCharz().taskMaint.index > j)
							{
								mFont.tahoma_7_white.drawString(g, empty, this.xstart + 5, this.yPaint += 12, 0);
							}
							else
							{
								mFont tahoma_7_grey3 = mFont.tahoma_7_grey;
								if (!flag)
								{
									flag = true;
									tahoma_7_grey3 = mFont.tahoma_7_blue;
									tahoma_7_grey3.drawString(g, empty, this.xstart + 5 + ((tahoma_7_grey3 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
								}
								else
								{
									tahoma_7_grey3.drawString(g, "- ...", this.xstart + 5 + ((tahoma_7_grey3 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
								}
							}
							this.indexRowMax++;
						}
						else if (Char.myCharz().taskMaint.index <= j)
						{
							empty = "- " + Char.myCharz().taskMaint.subNames[num2];
							mFont mFont2 = mFont.tahoma_7_grey;
							if (!flag)
							{
								flag = true;
								mFont2 = mFont.tahoma_7_blue;
							}
							mFont2.drawString(g, empty, this.xstart + 5 + ((mFont2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
						}
					}
					this.yPaint += 5;
					for (int k = 0; k < Char.myCharz().taskMaint.details.Length; k++)
					{
						mFont.tahoma_7_green2.drawString(g, Char.myCharz().taskMaint.details[k], this.xstart + 5, this.yPaint += 12, 0);
						this.indexRowMax++;
					}
				}
				else
				{
					int taskMapId = GameScr.getTaskMapId();
					sbyte taskNpcId = GameScr.getTaskNpcId();
					string empty2 = string.Empty;
					if (taskMapId == -3 || taskNpcId == -3)
					{
						empty2 = mResources.DES_TASK[3];
					}
					else if (Char.myCharz().taskMaint == null && Char.myCharz().ctaskId == 9 && Char.myCharz().nClass.classId == 0)
					{
						empty2 = mResources.TASK_INPUT_CLASS;
					}
					else
					{
						if (taskNpcId < 0 || taskMapId < 0)
						{
							return;
						}
						empty2 = string.Concat(new string[]
						{
							mResources.DES_TASK[0],
							Npc.arrNpcTemplate[(int)taskNpcId].name,
							mResources.DES_TASK[1],
							TileMap.mapNames[taskMapId],
							mResources.DES_TASK[2]
						});
					}
					string[] array = mFont.tahoma_7_white.splitFontArray(empty2, 150);
					for (int l = 0; l < array.Length; l++)
					{
						if (l == 0)
						{
							mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint = this.ystart, 0);
						}
						else
						{
							mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint += 12, 0);
						}
					}
				}
			}
			else if (this.indexMenu == 1)
			{
				this.yPaint = this.ystart - 12;
				for (int m = 0; m < Char.myCharz().taskOrders.size(); m++)
				{
					TaskOrder taskOrder = (TaskOrder)Char.myCharz().taskOrders.elementAt(m);
					mFont.tahoma_7_white.drawString(g, taskOrder.name, this.xstart + 5, this.yPaint += 12, 0);
					if (taskOrder.count == (int)taskOrder.maxCount)
					{
						mFont.tahoma_7_white.drawString(g, string.Concat(new string[]
						{
							(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
							" ",
							Mob.arrMobTemplate[taskOrder.killId].name,
							" (",
							taskOrder.count.ToString(),
							"/",
							taskOrder.maxCount.ToString(),
							")"
						}), this.xstart + 5, this.yPaint += 12, 0);
					}
					else
					{
						mFont.tahoma_7_blue.drawString(g, string.Concat(new string[]
						{
							(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
							" ",
							Mob.arrMobTemplate[taskOrder.killId].name,
							" (",
							taskOrder.count.ToString(),
							"/",
							taskOrder.maxCount.ToString(),
							")"
						}), this.xstart + 5, this.yPaint += 12, 0);
					}
					this.indexRowMax += 3;
					this.inforW = this.popupW - 25;
					this.paintMultiLine(g, mFont.tahoma_7_grey, taskOrder.description, this.xstart + 5, this.yPaint += 12, 0);
					this.yPaint += 12;
				}
			}
			if (this.scroll == null)
			{
				this.scroll = new Scroll();
				this.scroll.setStyle(this.indexRowMax, 12, this.xScroll, this.yScroll, this.wScroll, this.hScroll - num - 40, true, 1);
			}
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x002C3DB4 File Offset: 0x002C1FB4
		public void paintMultiLine(mGraphics g, mFont f, string str, int x, int y, int align)
		{
			int num = (!GameCanvas.isTouch || GameCanvas.w < 320) ? 10 : 20;
			string[] array = f.splitFontArray(str, this.inforW - num);
			for (int i = 0; i < array.Length; i++)
			{
				if (i == 0)
				{
					f.drawString(g, array[i], x, y, align);
				}
				else
				{
					if (i < this.indexRow + 15 && i > this.indexRow - 15)
					{
						f.drawString(g, array[i], x, y += 12, align);
					}
					else
					{
						y += 12;
					}
					this.yPaint += 12;
					this.indexRowMax++;
				}
			}
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x002C3E64 File Offset: 0x002C2064
		public void cleanCombine()
		{
			for (int i = 0; i < this.vItemCombine.size(); i++)
			{
				((Item)this.vItemCombine.elementAt(i)).isSelect = false;
			}
			this.vItemCombine.removeAllElements();
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x002C3EAC File Offset: 0x002C20AC
		public void hideNow()
		{
			if (this.timeShow > 0)
			{
				this.isClose = false;
				return;
			}
			if (this.isTypeShop())
			{
				Char.myCharz().resetPartTemp();
			}
			if (this.chatTField != null && this.type == 13 && this.chatTField.isShow)
			{
				this.chatTField = null;
			}
			if (this.type == 13 && !this.isAccept)
			{
				Service.gI().giaodich(3, -1, -1, -1);
			}
			SoundMn.gI().buttonClose();
			GameScr.isPaint = true;
			TileMap.lastPlanetId = -1;
			Panel.imgMap = null;
			mSystem.gcc();
			this.isClanOption = false;
			this.isClose = true;
			this.cleanCombine();
			Hint.clickNpc();
			GameCanvas.panel2 = null;
			GameCanvas.clearAllPointerEvent();
			GameCanvas.clearKeyPressed();
			this.pointerDownTime = (this.pointerDownFirstX = 0);
			this.pointerIsDowning = false;
			this.isShow = false;
			if ((Char.myCharz().cHP <= 0L || Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5) && Char.myCharz().meDead)
			{
				Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
				GameScr.gI().center = center;
				Char.myCharz().cHP = 0L;
			}
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x002C3FF0 File Offset: 0x002C21F0
		public void hide()
		{
			if (this.timeShow > 0)
			{
				this.isClose = false;
				return;
			}
			if (this.isTypeShop())
			{
				Char.myCharz().resetPartTemp();
			}
			if (this.chatTField != null && this.type == 13 && this.chatTField.isShow)
			{
				this.chatTField = null;
			}
			if (this.type == 13 && !this.isAccept)
			{
				Service.gI().giaodich(3, -1, -1, -1);
			}
			if (this.type == 15)
			{
				Service.gI().sendThachDau(-1);
			}
			SoundMn.gI().buttonClose();
			GameScr.isPaint = true;
			TileMap.lastPlanetId = -1;
			if (Panel.imgMap != null)
			{
				Panel.imgMap.texture = null;
				Panel.imgMap = null;
			}
			mSystem.gcc();
			this.isClanOption = false;
			if (this.type != 4)
			{
				if (this.type == 24)
				{
					this.setTypeGameInfo();
				}
				else if (this.type == 23)
				{
					this.setTypeMain();
				}
				else if (this.type == 3 || this.type == 14)
				{
					if (this.isChangeZone)
					{
						this.isClose = true;
					}
					else
					{
						this.setTypeMain();
						this.cmx = (this.cmtoX = 0);
					}
				}
				else if (this.type == 18 || this.type == 19 || this.type == 20 || this.type == 21 || this.type == 26 || this.type == 27 || this.type == 28)
				{
					this.setTypeMain();
					this.cmx = (this.cmtoX = 0);
				}
				else if (this.type == 8 || this.type == 11 || this.type == 16)
				{
					this.setTypeAccount();
					this.cmx = (this.cmtoX = 0);
				}
				else
				{
					this.isClose = true;
				}
			}
			else
			{
				this.setTypeMain();
				this.cmx = (this.cmtoX = 0);
			}
			Hint.clickNpc();
			GameCanvas.panel2 = null;
			GameCanvas.clearAllPointerEvent();
			GameCanvas.clearKeyPressed();
			GameCanvas.isFocusPanel2 = false;
			this.pointerDownTime = (this.pointerDownFirstX = 0);
			this.pointerIsDowning = false;
			if ((Char.myCharz().cHP <= 0L || Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5) && Char.myCharz().meDead)
			{
				Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
				GameScr.gI().center = center;
				Char.myCharz().cHP = 0L;
			}
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x002C4274 File Offset: 0x002C2474
		public void update()
		{
			if (this.chatTField != null && this.chatTField.isShow)
			{
				this.chatTField.update();
				return;
			}
			if (this.isKiguiXu)
			{
				this.delayKigui++;
				if (this.delayKigui == 10)
				{
					this.delayKigui = 0;
					this.isKiguiXu = false;
					this.chatTField.tfChat.setText(string.Empty);
					this.chatTField.strChat = mResources.kiguiXuchat + " ";
					this.chatTField.tfChat.name = mResources.input_money;
					this.chatTField.to = string.Empty;
					this.chatTField.isShow = true;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
					this.chatTField.tfChat.setMaxTextLenght(9);
					if (GameCanvas.isTouch)
					{
						this.chatTField.tfChat.doChangeToTextBox();
					}
					if (Main.isWindowsPhone)
					{
						this.chatTField.tfChat.strInfo = this.chatTField.strChat;
					}
					if (!Main.isPC)
					{
						this.chatTField.startChat(this, string.Empty);
					}
				}
				return;
			}
			if (this.isKiguiLuong)
			{
				this.delayKigui++;
				if (this.delayKigui == 10)
				{
					this.delayKigui = 0;
					this.isKiguiLuong = false;
					this.chatTField.tfChat.setText(string.Empty);
					this.chatTField.strChat = mResources.kiguiLuongchat + "  ";
					this.chatTField.tfChat.name = mResources.input_money;
					this.chatTField.to = string.Empty;
					this.chatTField.isShow = true;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
					this.chatTField.tfChat.setMaxTextLenght(9);
					if (GameCanvas.isTouch)
					{
						this.chatTField.tfChat.doChangeToTextBox();
					}
					if (Main.isWindowsPhone)
					{
						this.chatTField.tfChat.strInfo = this.chatTField.strChat;
					}
					if (!Main.isPC)
					{
						this.chatTField.startChat(this, string.Empty);
					}
				}
				return;
			}
			if (this.scroll != null)
			{
				this.scroll.updatecm();
			}
			if (this.tabIcon != null && this.tabIcon.isShow)
			{
				this.tabIcon.update();
				return;
			}
			this.moveCamera();
			if (this.isTabInven() && this.isnewInventory)
			{
				if (this.eBanner == null)
				{
					this.eBanner = new Effect(205, 0, 0, 3, 10, -1);
					this.eBanner.typeEff = 2;
				}
				if (this.eBanner != null)
				{
					this.eBanner.update();
				}
			}
			if (this.waitToPerform > 0)
			{
				this.waitToPerform--;
				if (this.waitToPerform == 0)
				{
					this.lastSelect[this.currentTabIndex] = this.selected;
					switch (this.type)
					{
						case 0:
							this.doFireMain();
							break;
						case 1:
						case 17:
							this.doFireShop();
							break;
						case 2:
							this.doFireBox();
							break;
						case 3:
							this.doFireZone();
							break;
						case 4:
							this.doFireMap();
							break;
						case 7:
							if (this.Equals(GameCanvas.panel2) && GameCanvas.panel.type == 2)
							{
								this.doFireBox();
								return;
							}
							this.doFireInventory();
							break;
						case 8:
							this.doFireLogMessage();
							break;
						case 9:
							this.doFireArchivement();
							break;
						case 10:
							this.doFirePlayerMenu();
							break;
						case 11:
							this.doFireFriend();
							break;
						case 12:
							this.doFireCombine();
							break;
						case 13:
							this.doFireGiaoDich();
							break;
						case 14:
							this.doFireMapTrans();
							break;
						case 15:
							this.doFireTop();
							break;
						case 16:
							this.doFireEnemy();
							break;
						case 18:
							this.doFireChangeFlag();
							break;
						case 19:
							this.doFireOption();
							break;
						case 20:
							this.doFireAccount();
							break;
						case 21:
							this.doFirePetMain();
							break;
						case 22:
							this.doFireAuto();
							break;
						case 23:
							this.doFireGameInfo();
							break;
						case 25:
							this.doSpeacialSkill();
							break;
						case 26:
							this.DoFireModFunc();
							break;
						case 28:
							this.DoFirePet2Main();
							break;
					}
				}
			}
			for (int i = 0; i < ClanMessage.vMessage.size(); i++)
			{
				((ClanMessage)ClanMessage.vMessage.elementAt(i)).update();
			}
			this.updateCombineEff();
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x002C4730 File Offset: 0x002C2930
		private void doSpeacialSkill()
		{
			string info = Char.myCharz().infoSpeacialSkill[0][this.selected];
			MyVector myVector8 = new MyVector();
			myVector8.addElement(new Command(ModFunc.strChooseIntrinsic, this, 8011, info));
			GameCanvas.menu.startAt(myVector8, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x002C479C File Offset: 0x002C299C
		private void doFireGameInfo()
		{
			if (this.selected != -1)
			{
				this.infoSelect = this.selected;
				((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).hasRead = true;
				Rms.saveRMSInt(((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).id.ToString() + string.Empty, 1);
				this.setTypeGameSubInfo();
			}
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000034B9 File Offset: 0x000016B9
		private void doFireAuto()
		{
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x002C4810 File Offset: 0x002C2A10
		private void DoFirePet2Main()
		{
			if (this.currentTabIndex == 0)
			{
				if (this.selected != -1 && this.selected <= Char.MyPet2z().arrItemBody.Length - 1)
				{
					MyVector myVector = new MyVector(string.Empty);
					Item item = Char.MyPet2z().arrItemBody[this.selected];
					this.currItem = item;
					if (this.currItem != null)
					{
						myVector.addElement(new Command(mResources.MOVEOUT, this, 2008, this.currItem));
						GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
						this.addItemDetail(this.currItem);
						return;
					}
					this.cp = null;
					return;
				}
			}
			else if (this.currentTabIndex != 1)
			{
				if (this.currentTabIndex == 2)
				{
					this.doFirePetStatus();
					return;
				}
				if (this.currentTabIndex == 3)
				{
					this.doFireInventory();
				}
			}
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x002C4900 File Offset: 0x002C2B00
		private void doFirePetMain()
		{
			if (this.currentTabIndex == 0)
			{
				if (this.selected != -1 && this.selected <= Char.myPetz().arrItemBody.Length - 1)
				{
					MyVector myVector = new MyVector(string.Empty);
					Item item = Char.myPetz().arrItemBody[this.selected];
					this.currItem = item;
					if (this.currItem != null)
					{
						myVector.addElement(new Command(mResources.MOVEOUT, this, 2006, this.currItem));
						GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
						this.addItemDetail(this.currItem);
						return;
					}
					this.cp = null;
					return;
				}
			}
			else
			{
				if (this.currentTabIndex == 1)
				{
					this.DoFirePetSkill();
					return;
				}
				if (this.currentTabIndex == 2)
				{
					this.doFirePetStatus();
					return;
				}
				if (this.currentTabIndex == 3)
				{
					this.doFireInventory();
				}
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x002C49F8 File Offset: 0x002C2BF8
		private void DoFirePetSkill()
		{
			if (this.selected < 0)
			{
				return;
			}
			if (this.selected != 0 && this.selected != 1 && this.selected != 2 && this.selected != 3 && this.selected != 4)
			{
				return;
			}
			long cTiemNang = Char.myPetz().cTiemNang;
			long cHPGoc = Char.myPetz().cHPGoc;
			long cMPGoc = Char.myPetz().cMPGoc;
			long cDamGoc = Char.myPetz().cDamGoc;
			int cDefGoc = Char.myPetz().cDefGoc;
			int cCriticalGoc = Char.myPetz().cCriticalGoc;
			int num2 = 1000;
			if (this.selected == 0)
			{
				if (cTiemNang < cHPGoc + (long)num2)
				{
					GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + cTiemNang.ToString() + mResources.not_enough_potential_point2 + (cHPGoc + (long)num2).ToString(), false);
					return;
				}
				if (cTiemNang > cHPGoc && cTiemNang < 10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L)
				{
					GameCanvas.startYesNoDlg(string.Concat(new string[]
					{
						mResources.use_potential_point_for1,
						(cHPGoc + (long)num2).ToString(),
						mResources.use_potential_point_for2,
						Char.myCharz().hpFrom1000TiemNang.ToString(),
						mResources.for_HP
					}), new Command(mResources.increase_upper, this, 9000, true), new Command(mResources.CANCEL, this, 4007, null));
					return;
				}
				MyVector myVector = new MyVector(string.Empty);
				if (cTiemNang >= 10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L && cTiemNang < 100L * (2L * (cHPGoc + (long)num2) + 1980L) / 2L)
				{
					myVector.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().hpFrom1000TiemNang.ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(cHPGoc + (long)num2)
					}), this, 9000, true));
					myVector.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().hpFrom1000TiemNang)).ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L)
					}), this, 9006, true));
				}
				else if (cTiemNang >= 100L * (2L * (cHPGoc + (long)num2) + 1980L) / 2L)
				{
					myVector.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().hpFrom1000TiemNang.ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(cHPGoc + (long)num2)
					}), this, 9000, true));
					myVector.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().hpFrom1000TiemNang)).ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L)
					}), this, 9006, true));
					myVector.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(100 * Char.myCharz().hpFrom1000TiemNang)).ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(100L * (2L * (cHPGoc + (long)num2) + 1980L) / 2L)
					}), this, 9007, true));
				}
				myVector.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, this.selected.ToString() + "-" + true.ToString()));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected, false);
			}
			if (this.selected == 1)
			{
				if (cTiemNang < cMPGoc + (long)num2)
				{
					GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + cTiemNang.ToString() + mResources.not_enough_potential_point2 + (cMPGoc + (long)num2).ToString(), false);
					return;
				}
				if (cTiemNang > cMPGoc && cTiemNang < 10L * (2L * (cMPGoc + (long)num2) + 180L) / 2L)
				{
					GameCanvas.startYesNoDlg(string.Concat(new string[]
					{
						mResources.use_potential_point_for1,
						(cMPGoc + (long)num2).ToString(),
						mResources.use_potential_point_for2,
						Char.myCharz().mpFrom1000TiemNang.ToString(),
						mResources.for_KI
					}), new Command(mResources.increase_upper, this, 9000, true), new Command(mResources.CANCEL, this, 4007, null));
					return;
				}
				MyVector myVector2 = new MyVector(string.Empty);
				if (cTiemNang >= 10L * (2L * (cMPGoc + (long)num2) + 180L) / 2L && cTiemNang < 100L * (2L * (cMPGoc + (long)num2) + 1980L) / 2L)
				{
					myVector2.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().mpFrom1000TiemNang.ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(cHPGoc + (long)num2)
					}), this, 9000, true));
					myVector2.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().mpFrom1000TiemNang)).ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L)
					}), this, 9006, true));
				}
				else if (cTiemNang >= 100L * (2L * (cMPGoc + (long)num2) + 1980L) / 2L)
				{
					myVector2.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().mpFrom1000TiemNang.ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(cMPGoc + (long)num2)
					}), this, 9000, true));
					myVector2.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().mpFrom1000TiemNang)).ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(10L * (2L * (cMPGoc + (long)num2) + 180L) / 2L)
					}), this, 9006, true));
					myVector2.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(100 * Char.myCharz().mpFrom1000TiemNang)).ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(100L * (2L * (cMPGoc + (long)num2) + 1980L) / 2L)
					}), this, 9007, true));
				}
				myVector2.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, this.selected.ToString() + "-" + true.ToString()));
				GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected, false);
			}
			if (this.selected == 2)
			{
				if (cTiemNang < cDamGoc * (long)Char.myCharz().expForOneAdd)
				{
					GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + cTiemNang.ToString() + mResources.not_enough_potential_point2 + (cDamGoc * 100L).ToString(), false);
					return;
				}
				if (cTiemNang > cDamGoc && cTiemNang < 10L * (2L * cDamGoc + 9L) / 2L * (long)Char.myCharz().expForOneAdd)
				{
					GameCanvas.startYesNoDlg(string.Concat(new string[]
					{
						mResources.use_potential_point_for1,
						(cDamGoc * 100L).ToString(),
						mResources.use_potential_point_for2,
						Char.myCharz().damFrom1000TiemNang.ToString(),
						mResources.for_hit_point
					}), new Command(mResources.increase_upper, this, 9000, true), new Command(mResources.CANCEL, this, 4007, null));
					return;
				}
				MyVector myVector3 = new MyVector(string.Empty);
				if (cTiemNang >= 10L * (2L * cDamGoc + 9L) / 2L * (long)Char.myCharz().expForOneAdd && cTiemNang < 100L * (2L * cDamGoc + 99L) / 2L * (long)Char.myCharz().expForOneAdd)
				{
					myVector3.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().damFrom1000TiemNang.ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(cDamGoc * 100L)
					}), this, 9000, true));
					myVector3.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().damFrom1000TiemNang)).ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(10L * (2L * cDamGoc + 9L) / 2L * (long)Char.myCharz().expForOneAdd)
					}), this, 9006, true));
				}
				else if (cTiemNang >= 100L * (2L * cDamGoc + 99L) / 2L * (long)Char.myCharz().expForOneAdd)
				{
					myVector3.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().damFrom1000TiemNang.ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(cDamGoc * 100L)
					}), this, 9000, true));
					myVector3.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().damFrom1000TiemNang)).ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(10L * (2L * cDamGoc + 9L) / 2L * (long)Char.myCharz().expForOneAdd)
					}), this, 9006, true));
					myVector3.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(100 * Char.myCharz().damFrom1000TiemNang)).ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(100L * (2L * cDamGoc + 99L) / 2L * (long)Char.myCharz().expForOneAdd)
					}), this, 9007, true));
				}
				myVector3.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, this.selected.ToString() + "-" + true.ToString()));
				GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected, false);
			}
			if (this.selected != 3)
			{
				if (this.selected == 4)
				{
					int crit = cCriticalGoc;
					if (crit > Panel.t_tiemnang.Length - 1)
					{
						crit = Panel.t_tiemnang.Length - 1;
					}
					long num3 = Panel.t_tiemnang[crit];
					if (cTiemNang < num3)
					{
						GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Res.formatNumber2(cTiemNang) + mResources.not_enough_potential_point2 + Res.formatNumber2(num3), false);
						return;
					}
					GameCanvas.startYesNoDlg(string.Concat(new string[]
					{
						mResources.use_potential_point_for1,
						Res.formatNumber(num3),
						mResources.use_potential_point_for2,
						Char.myCharz().criticalFrom1000Tiemnang.ToString(),
						mResources.for_crit
					}), new Command(mResources.increase_upper, this, 9000, true), new Command(mResources.CANCEL, this, 4007, null));
				}
				return;
			}
			if (cTiemNang < (long)(50000 + cDefGoc * 1000))
			{
				GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + NinjaUtil.getMoneys(cTiemNang) + mResources.not_enough_potential_point2 + NinjaUtil.getMoneys((long)(50000 + cDefGoc * 1000)), false);
				return;
			}
			long number = (long)(2 * (cDefGoc + 5)) / 2L * 100000L;
			long number2 = 10L * (long)(2 * (cDefGoc + 5) + 9) / 2L * 100000L;
			long number3 = 100L * (long)(2 * (cDefGoc + 5) + 99) / 2L * 100000L;
			MyVector myVector4 = new MyVector(string.Empty);
			myVector4.addElement(new Command(string.Concat(new string[]
			{
				mResources.increase_upper,
				"\n1 ",
				mResources.armor,
				"\n",
				Res.formatNumber2(number)
			}), this, 9000, true));
			myVector4.addElement(new Command(string.Concat(new string[]
			{
				mResources.increase_upper,
				"\n10 ",
				mResources.armor,
				"\n",
				Res.formatNumber2(number2)
			}), this, 9006, true));
			myVector4.addElement(new Command(string.Concat(new string[]
			{
				mResources.increase_upper,
				"\n100 ",
				mResources.armor,
				"\n",
				Res.formatNumber2(number3)
			}), this, 9007, true));
			myVector4.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, this.selected.ToString() + "-" + true.ToString()));
			GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addSkillDetail2(this.selected, false);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x002C5934 File Offset: 0x002C3B34
		private void doFirePetStatus()
		{
			if (this.selected == -1)
			{
				return;
			}
			if (this.selected == 5)
			{
				GameCanvas.startYesNoDlg(mResources.sure_fusion, new Command(mResources.YES, (this.type == 28) ? 888352 : 888351), new Command(mResources.NO, 2001));
				return;
			}
			if (this.type == 28)
			{
				Service.gI().pet2Status((sbyte)this.selected);
				if (this.selected < 4)
				{
					Char.MyPet2z().petStatus = (sbyte)this.selected;
					return;
				}
			}
			else
			{
				Service.gI().petStatus((sbyte)this.selected);
				if (this.selected < 4)
				{
					Char.myPetz().petStatus = (sbyte)this.selected;
				}
			}
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x002C59F0 File Offset: 0x002C3BF0
		private void doFireTop()
		{
			if (this.selected >= -1)
			{
				if (this.isThachDau)
				{
					Service.gI().sendTop(this.topName, (sbyte)this.selected);
					return;
				}
				MyVector myVector = new MyVector(string.Empty);
				myVector.addElement(new Command(mResources.CHAR_ORDER[0], this, 9999, (TopInfo)this.vTop.elementAt(this.selected)));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addThachDauDetail((TopInfo)this.vTop.elementAt(this.selected));
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x002C5AAC File Offset: 0x002C3CAC
		private void doFireMapTrans()
		{
			this.doFireZone();
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x002C5AB4 File Offset: 0x002C3CB4
		private void doFireGiaoDich()
		{
			if (this.currentTabIndex == 0 && this.Equals(GameCanvas.panel))
			{
				this.doFireInventory();
				return;
			}
			if ((this.currentTabIndex == 0 && this.Equals(GameCanvas.panel2)) || this.currentTabIndex == 2)
			{
				if (this.Equals(GameCanvas.panel2))
				{
					this.currItem = (Item)GameCanvas.panel2.vFriendGD.elementAt(this.selected);
				}
				else
				{
					this.currItem = (Item)GameCanvas.panel.vFriendGD.elementAt(this.selected);
				}
				Res.outz2("toi day select= " + this.selected.ToString());
				MyVector myVector = new MyVector();
				myVector.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
				if (this.currItem != null)
				{
					GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
					this.addItemDetail(this.currItem);
				}
				else
				{
					this.cp = null;
				}
			}
			if (this.currentTabIndex == 1)
			{
				if (this.selected == this.currentListLength - 3)
				{
					if (this.isLock)
					{
						return;
					}
					this.putMoney();
				}
				else if (this.selected == this.currentListLength - 2)
				{
					if (!this.isAccept)
					{
						this.isLock = !this.isLock;
						if (this.isLock)
						{
							Service.gI().giaodich(5, -1, -1, -1);
						}
						else
						{
							this.hide();
							InfoDlg.showWait();
							Service.gI().giaodich(3, -1, -1, -1);
						}
					}
					else
					{
						this.isAccept = false;
					}
				}
				else if (this.selected == this.currentListLength - 1)
				{
					if (this.isLock && !this.isAccept && this.isFriendLock)
					{
						GameCanvas.startYesNoDlg(mResources.do_u_sure_to_trade, new Command(mResources.YES, this, 7002, null), new Command(mResources.NO, this, 4005, null));
					}
				}
				else
				{
					if (this.isLock)
					{
						return;
					}
					this.currItem = (Item)GameCanvas.panel.vMyGD.elementAt(this.selected);
					MyVector myVector2 = new MyVector();
					myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
					if (this.currItem != null)
					{
						GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
						this.addItemDetail(this.currItem);
					}
					else
					{
						this.cp = null;
					}
				}
			}
			if (GameCanvas.isTouch)
			{
				this.selected = -1;
			}
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x002C5D78 File Offset: 0x002C3F78
		private void doFireCombine()
		{
			if (this.currentTabIndex == 0)
			{
				if (this.selected == -1 || this.vItemCombine.size() == 0)
				{
					return;
				}
				if (this.selected == this.vItemCombine.size())
				{
					this.keyTouchCombine = -1;
					this.selected = (GameCanvas.isTouch ? -1 : 0);
					InfoDlg.showWait();
					Service.gI().combine(1, this.vItemCombine);
					return;
				}
				if (this.selected > this.vItemCombine.size() - 1)
				{
					return;
				}
				this.currItem = (Item)GameCanvas.panel.vItemCombine.elementAt(this.selected);
				MyVector myVector = new MyVector();
				myVector.addElement(new Command(mResources.GETOUT, this, 6001, this.currItem));
				if (ModFunc.GI().isAutoPhaLe)
				{
					myVector.addElement(new Command("Nhập số sao", this, 8010, this.currItem));
				}
				if (this.currItem != null)
				{
					GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
					this.addItemDetail(this.currItem);
				}
				else
				{
					this.cp = null;
				}
			}
			if (this.currentTabIndex == 1)
			{
				this.doFireInventory();
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x002C5EC3 File Offset: 0x002C40C3
		private void doFirePlayerMenu()
		{
			if (this.selected != -1)
			{
				this.isSelectPlayerMenu = true;
				this.hide();
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x002C5EDC File Offset: 0x002C40DC
		private void doFireShop()
		{
			this.currItem = null;
			if (this.selected < 0)
			{
				return;
			}
			MyVector myVector = new MyVector();
			if (this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17)
			{
				this.currItem = Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
				if (this.currItem != null)
				{
					if (this.currItem.isBuySpec)
					{
						if (this.currItem.buySpec > 0)
						{
							myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2((long)this.currItem.buySpec), this, 3005, this.currItem));
						}
					}
					else if (this.typeShop == 4)
					{
						myVector.addElement(new Command(mResources.receive_upper, this, 30001, this.currItem));
						myVector.addElement(new Command(mResources.DELETE, this, 30002, this.currItem));
						myVector.addElement(new Command(mResources.receive_all, this, 30003, this.currItem));
					}
					else if (this.currItem.buyCoin == 0 && this.currItem.buyGold == 0)
					{
						if (this.currItem.powerRequire != 0L)
						{
							myVector.addElement(new Command(string.Concat(new string[]
							{
								mResources.learn_with,
								"\n",
								Res.formatNumber(this.currItem.powerRequire),
								" \n",
								mResources.potential
							}), this, 3004, this.currItem));
						}
						else
						{
							myVector.addElement(new Command(mResources.receive_upper + "\n" + mResources.free, this, 3000, this.currItem));
						}
					}
					else if (this.typeShop == 8)
					{
						if (this.currItem.buyCoin > 0)
						{
							myVector.addElement(new Command(string.Concat(new string[]
							{
								mResources.buy_with,
								"\n",
								Res.formatNumber2((long)this.currItem.buyCoin),
								"\n",
								mResources.XU
							}), this, 30001, this.currItem));
						}
						if (this.currItem.buyGold > 0)
						{
							myVector.addElement(new Command(string.Concat(new string[]
							{
								mResources.buy_with,
								"\n",
								Res.formatNumber2((long)this.currItem.buyGold),
								"\n",
								mResources.LUONG
							}), this, 30002, this.currItem));
						}
					}
					else if (this.typeShop != 2)
					{
						if (this.currItem.buyCoin > 0)
						{
							myVector.addElement(new Command(string.Concat(new string[]
							{
								mResources.buy_with,
								"\n",
								Res.formatNumber2((long)this.currItem.buyCoin),
								"\n",
								mResources.XU
							}), this, 3000, this.currItem));
						}
						if (this.currItem.buyGold > 0)
						{
							myVector.addElement(new Command(string.Concat(new string[]
							{
								mResources.buy_with,
								"\n",
								Res.formatNumber2((long)this.currItem.buyGold),
								"\n",
								mResources.LUONG
							}), this, 3001, this.currItem));
						}
					}
					else if (this.typeShop != 2)
					{
						if (this.currItem.buyCoin > 0)
						{
							myVector.addElement(new Command(string.Concat(new string[]
							{
								mResources.buy_with,
								"\n",
								Res.formatNumber2((long)this.currItem.buyCoin),
								"\n",
								mResources.XU
							}), this, 3000, this.currItem));
						}
						if (this.currItem.buyGold > 0)
						{
							myVector.addElement(new Command(string.Concat(new string[]
							{
								mResources.buy_with,
								"\n",
								Res.formatNumber2((long)this.currItem.buyGold),
								"\n",
								mResources.LUONG
							}), this, 3001, this.currItem));
						}
					}
					else
					{
						if (this.currItem.buyCoin != -1)
						{
							myVector.addElement(new Command(string.Concat(new string[]
							{
								mResources.buy_with,
								"\n",
								Res.formatNumber2((long)this.currItem.buyCoin),
								"\n",
								mResources.RUBY
							}), this, 10016, this.currItem));
						}
						if (this.currItem.buyGold != -1)
						{
							myVector.addElement(new Command(string.Concat(new string[]
							{
								mResources.buy_with,
								"\n",
								Res.formatNumber2((long)this.currItem.buyGold),
								"\n",
								mResources.LUONG
							}), this, 10017, this.currItem));
						}
					}
				}
			}
			else if (this.typeShop == 0)
			{
				this.currItem = null;
				if (!this.GetInventorySelect_isbody(this.selected, this.newSelected, Char.myCharz().arrItemBody))
				{
					Item item = Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, Char.myCharz().arrItemBody)];
					if (item != null)
					{
						this.currItem = item;
					}
				}
				else
				{
					Item item2 = Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
					if (item2 != null)
					{
						this.currItem = item2;
					}
				}
				if (this.currItem != null)
				{
					myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
				}
			}
			else
			{
				if (this.type == 17)
				{
					this.currItem = Char.myCharz().arrItemShop[4][this.selected];
				}
				else
				{
					this.currItem = Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
				}
				if (this.currItem.buyType == 0)
				{
					if (this.currItem.isHaveOption(87))
					{
						myVector.addElement(new Command(mResources.kiguiLuong, this, 10013, this.currItem));
					}
					else
					{
						myVector.addElement(new Command(mResources.kiguiXu, this, 10012, this.currItem));
					}
				}
				else if (this.currItem.buyType == 1)
				{
					myVector.addElement(new Command(mResources.huykigui, this, 10014, this.currItem));
					myVector.addElement(new Command(mResources.upTop, this, 10018, this.currItem));
				}
				else if (this.currItem.buyType == 2)
				{
					myVector.addElement(new Command(mResources.nhantien, this, 10015, this.currItem));
				}
			}
			if (this.currItem != null)
			{
				Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
				if (this.type != 4 && this.type != 17)
				{
					myVector.addElement(new Command("Auto Mua", delegate ()
					{
						AutoItem.gI().addItemBuys(new AutoItem((int)this.currItem.template.id, (sbyte)this.currItem.template.id, -1, this.currItem.buyGold == 1, this.currItem.buyGold != 1));
					}));
				}
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
				return;
			}
			this.cp = null;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x002C66A8 File Offset: 0x002C48A8
		private void doFireArchivement()
		{
			if (this.selected >= 0 && Char.myCharz().arrArchive[this.selected].isFinish && !Char.myCharz().arrArchive[this.selected].isRecieve)
			{
				if (!GameCanvas.isTouch)
				{
					Service.gI().getArchivemnt(this.selected);
					return;
				}
				if (GameCanvas.px > this.xScroll + this.wScroll - 40)
				{
					Service.gI().getArchivemnt(this.selected);
				}
			}
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x002C6730 File Offset: 0x002C4930
		private void doFireInventory()
		{
			if (Char.myCharz().statusMe == 14)
			{
				GameCanvas.startOKDlg(mResources.can_not_do_when_die);
				return;
			}
			if (this.selected == -1)
			{
				return;
			}
			if (this.selected == 0 && !ModFunc.isInventory)
			{
				this.setNewSelected(Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length, false);
				return;
			}
			this.currItem = null;
			MyVector myVector = new MyVector();
			if (this.isnewInventory && this.isnewInventory)
			{
				this.currItem = this.itemInvenNew;
				if (this.newSelected == 0)
				{
					myVector.addElement(new Command(mResources.GETOUT, this, 2002, this.currItem));
				}
				else if (GameCanvas.panel.type == 12)
				{
					myVector.addElement(new Command(mResources.use_for_combine, this, 6000, this.currItem));
				}
				else if (GameCanvas.panel.type == 13)
				{
					myVector.addElement(new Command(mResources.use_for_trade, this, 7000, this.currItem));
				}
				else if (this.currItem.isTypeBody())
				{
					myVector.addElement(new Command(mResources.USE, this, 2000, this.currItem));
					if (Char.myCharz().havePet)
					{
						myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
					}
					if (Char.myCharz().havePet2)
					{
						myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, this.currItem));
					}
				}
				else
				{
					myVector.addElement(new Command(mResources.USE, this, 2001, this.currItem));
					if (Char.myCharz().havePet)
					{
						myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
					}
					if (Char.myCharz().havePet2)
					{
						myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, this.currItem));
					}
				}
			}
			else if (!this.GetInventorySelect_isbody(this.selected, this.newSelected, Char.myCharz().arrItemBody))
			{
				Item item = Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, Char.myCharz().arrItemBody)];
				if (item != null)
				{
					this.currItem = item;
					if (GameCanvas.panel.type == 12)
					{
						myVector.addElement(new Command(mResources.use_for_combine, this, 6000, this.currItem));
					}
					else if (GameCanvas.panel.type == 13)
					{
						myVector.addElement(new Command(mResources.use_for_trade, this, 7000, this.currItem));
					}
					else if (item.isTypeBody())
					{
						myVector.addElement(new Command(mResources.USE, this, 2000, this.currItem));
						if (Char.myCharz().havePet)
						{
							myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
						}
						if (Char.myCharz().havePet2)
						{
							myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, this.currItem));
						}
					}
					else
					{
						myVector.addElement(new Command(mResources.USE, this, 2001, this.currItem));
						if (Char.myCharz().havePet)
						{
							myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
						}
						if (Char.myCharz().havePet2)
						{
							myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, this.currItem));
						}
					}
				}
			}
			else
			{
				Item item2 = Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
				if (item2 != null)
				{
					this.currItem = item2;
					myVector.addElement(new Command(mResources.GETOUT, this, 2002, this.currItem));
				}
			}
			if (this.currItem != null)
			{
				Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
				if (GameCanvas.panel.type != 12 && GameCanvas.panel.type != 13)
				{
					if (this.position == 0)
					{
						myVector.addElement(new Command(mResources.MOVEOUT, this, 2003, this.currItem));
						if (this.currItem.template.type == 29 || this.currItem.template.type == 33 || this.currItem.template.id == 380 || this.currItem.quantity >= 2)
						{
							if (ModFunc.GI().listItemAuto.Exists((ItemAuto i) => i.id == (int)this.currItem.template.id))
							{
								myVector.addElement(new Command(ModFunc.strRemoveAutoItem, ModFunc.GI(), 501, this.currItem));
							}
							else
							{
								myVector.addElement(new Command(ModFunc.strAddAutoItem, ModFunc.GI(), 500, this.currItem));
							}
						}
					}
					if (this.position == 1)
					{
						myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
					}
					if (ModFunc.isFilterItem)
					{
						if (ModFunc.listFilterItems.Exists((ItemAutoFilter i) => i.id == (int)this.currItem.template.id))
						{
							myVector.addElement(new Command(ModFunc.strRemoveFilterItem, ModFunc.GI(), 503, this.currItem));
						}
						else
						{
							myVector.addElement(new Command(ModFunc.strAddFilterItem, ModFunc.GI(), 502, this.currItem));
						}
					}
				}
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
				return;
			}
			this.cp = null;
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x002C6D1B File Offset: 0x002C4F1B
		private void doRada()
		{
			this.hide();
			if (RadarScr.list == null || RadarScr.list.size() == 0)
			{
				Service.gI().SendRada(0, -1);
				RadarScr.gI().switchToMe();
				return;
			}
			RadarScr.gI().switchToMe();
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x002C6D58 File Offset: 0x002C4F58
		private void doFireTool()
		{
			if (this.selected < 0)
			{
				return;
			}
			if (SoundMn.IsDelAcc && this.selected == Panel.strTool.Length - 1)
			{
				Service.gI().sendDelAcc();
				return;
			}
			if (!Char.myCharz().havePet && !Char.myCharz().havePet2)
			{
				switch (this.selected)
				{
					case 0:
						this.setTypeGameInfo();
						return;
					case 1:
						this.SetTypeModFunc();
						return;
					case 2:
						this.SetTypePlayerInfo();
						return;
					case 3:
						this.doRada();
						return;
					case 4:
						Service.gI().getFlag(0, -1);
						InfoDlg.showWait();
						return;
					case 5:
						if (Char.myCharz().statusMe == 14)
						{
							GameCanvas.startOKDlg(mResources.can_not_do_when_die);
							return;
						}
						ModFunc.GI().userOpenZones = true;
						Service.gI().openUIZone();
						return;
					case 6:
						ModFunc.DoChatGlobal();
						return;
					case 7:
						this.setTypeAccount();
						return;
					case 8:
						this.setTypeOption();
						return;
					case 9:
						GameCanvas.loginScr.backToRegister();
						return;
					case 10:
						if (GameCanvas.loginScr.isLogin2)
						{
							SoundMn.gI().backToRegister();
						}
						return;
					default:
						return;
				}
			}
			else if (Char.myCharz().havePet && Char.myCharz().havePet2)
			{
				switch (this.selected)
				{
					case 0:
						this.setTypeGameInfo();
						return;
					case 1:
						this.SetTypeModFunc();
						return;
					case 2:
						this.SetTypePlayerInfo();
						return;
					case 3:
						this.doRada();
						return;
					case 4:
						this.doFirePet();
						return;
					case 5:
						this.doFirePet2();
						return;
					case 6:
						Service.gI().getFlag(0, -1);
						InfoDlg.showWait();
						return;
					case 7:
						if (Char.myCharz().statusMe == 14)
						{
							GameCanvas.startOKDlg(mResources.can_not_do_when_die);
							return;
						}
						ModFunc.GI().userOpenZones = true;
						Service.gI().openUIZone();
						return;
					case 8:
						ModFunc.DoChatGlobal();
						return;
					case 9:
						this.setTypeAccount();
						return;
					case 10:
						this.setTypeOption();
						return;
					case 11:
						GameCanvas.loginScr.backToRegister();
						return;
					case 12:
						if (GameCanvas.loginScr.isLogin2)
						{
							SoundMn.gI().backToRegister();
						}
						return;
					default:
						return;
				}
			}
			else
			{
				switch (this.selected)
				{
					case 0:
						this.setTypeGameInfo();
						return;
					case 1:
						this.SetTypeModFunc();
						return;
					case 2:
						this.SetTypePlayerInfo();
						return;
					case 3:
						this.doRada();
						return;
					case 4:
						if (Char.myCharz().havePet)
						{
							this.doFirePet();
							return;
						}
						this.doFirePet2();
						return;
					case 5:
						Service.gI().getFlag(0, -1);
						InfoDlg.showWait();
						return;
					case 6:
						if (Char.myCharz().statusMe == 14)
						{
							GameCanvas.startOKDlg(mResources.can_not_do_when_die);
							return;
						}
						ModFunc.GI().userOpenZones = true;
						Service.gI().openUIZone();
						return;
					case 7:
						ModFunc.DoChatGlobal();
						return;
					case 8:
						this.setTypeAccount();
						return;
					case 9:
						this.setTypeOption();
						return;
					case 10:
						GameCanvas.loginScr.backToRegister();
						return;
					case 11:
						if (GameCanvas.loginScr.isLogin2)
						{
							SoundMn.gI().backToRegister();
						}
						return;
					default:
						return;
				}
			}
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x002C7068 File Offset: 0x002C5268
		private void setTypeGameSubInfo()
		{
			string content = ((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).content;
			Panel.contenInfo = mFont.tahoma_7_grey.splitFontArray(content, this.wScroll - 40);
			this.currentListLength = Panel.contenInfo.Length;
			this.ITEM_HEIGHT = 16;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.type = 24;
			this.setType(0);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x002C7144 File Offset: 0x002C5344
		private void SetTypePlayerInfo()
		{
			string content = string.Concat(new string[]
			{
				"Tộc: ",
				(Char.myCharz().cgender == 0) ? "Trái Đất" : ((Char.myCharz().cgender == 1) ? "Namek" : "Xayda"),
				"\nHP: ",
				NinjaUtil.getMoneys(Char.myCharz().cHP),
				" / ",
				NinjaUtil.getMoneys(Char.myCharz().cHPFull),
				"\nKI: ",
				NinjaUtil.getMoneys(Char.myCharz().cMP),
				" / ",
				NinjaUtil.getMoneys(Char.myCharz().cMPFull),
				"\nSĐ: ",
				NinjaUtil.getMoneys(Char.myCharz().cDamFull),
				"\nChí mạng: ",
				Char.myCharz().cCriticalFull.ToString(),
				"%\nGiảm sát thương: ",
				Char.myCharz().tlDef.ToString(),
				"%\nPhản sát thương: ",
				Char.myCharz().tlPst.ToString(),
				"%\nNé đòn: ",
				Char.myCharz().tlNeDon.ToString(),
				"%\nHút HP: ",
				Char.myCharz().tlHutHp.ToString(),
				"%\nHút KI: ",
				Char.myCharz().tlHutMp.ToString(),
				"%\nGiảm TDHS: ",
				Char.myCharz().tileGiamTDHS.ToString(),
				"%\nGiảm TDHS: ",
				Char.myCharz().timeGiamTDHS.ToString(),
				" giây\nKháng TDHS: ",
				Char.myCharz().khangTDHS ? "Có" : "Không",
				"\nKháng lạnh: ",
				Char.myCharz().isKhongLanh ? "Có" : "Không",
				"\nVô hình: ",
				Char.myCharz().wearingVoHinh ? "Có" : "Không",
				"\nDịch chuyển: ",
				Char.myCharz().teleport ? "Có" : "Không",
				"\n"
			});
			Panel.contenInfo = mFont.tahoma_7_grey.splitFontArray(content, this.wScroll - 40);
			this.currentListLength = Panel.contenInfo.Length;
			this.ITEM_HEIGHT = 16;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.type = 27;
			this.setType(0);
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x002C744C File Offset: 0x002C564C
		private void setTypeGameInfo()
		{
			this.currentListLength = Panel.vGameInfo.size();
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.type = 23;
			this.setType(0);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x002C74F7 File Offset: 0x002C56F7
		private void doFirePet()
		{
			InfoDlg.showWait();
			Service.gI().petInfo();
			this.timeShow = 20;
			ModFunc.userOpenPet = true;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x002C7516 File Offset: 0x002C5716
		private void doFirePet2()
		{
			InfoDlg.showWait();
			Service.gI().PetInfo2();
			this.timeShow = 20;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x002C7530 File Offset: 0x002C5730
		private void searchClan()
		{
			this.chatTField.strChat = mResources.input_clan_name;
			this.chatTField.tfChat.name = mResources.clan_name;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.isFocus = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat(this, string.Empty);
			}
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x002C75E0 File Offset: 0x002C57E0
		private void chatClan()
		{
			this.chatTField.strChat = mResources.chat_clan;
			this.chatTField.tfChat.name = mResources.CHAT;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.isFocus = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat(this, string.Empty);
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x002C7690 File Offset: 0x002C5890
		public void creatClan()
		{
			this.chatTField.strChat = mResources.input_clan_name_to_create;
			this.chatTField.tfChat.name = mResources.input_clan_name;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat(this, string.Empty);
			}
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x002C7730 File Offset: 0x002C5930
		public void putMoney()
		{
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
			}
			this.chatTField.strChat = mResources.input_money_to_trade;
			this.chatTField.tfChat.name = mResources.input_money;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.chatTField.tfChat.setMaxTextLenght(9);
			if (GameCanvas.isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat(this, string.Empty);
			}
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x002C784C File Offset: 0x002C5A4C
		public void putQuantily()
		{
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
			}
			this.chatTField.strChat = mResources.input_quantity_to_trade;
			this.chatTField.tfChat.name = mResources.input_quantity;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			if (GameCanvas.isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat(this, string.Empty);
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x002C7958 File Offset: 0x002C5B58
		public void chagenSlogan()
		{
			this.chatTField.strChat = mResources.input_clan_slogan;
			this.chatTField.tfChat.name = mResources.input_clan_slogan;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.isFocus = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat(this, string.Empty);
			}
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x002C7A08 File Offset: 0x002C5C08
		public void changeIcon()
		{
			if (this.tabIcon == null)
			{
				this.tabIcon = new TabClanIcon();
			}
			this.tabIcon.text = this.chatTField.tfChat.getText();
			this.tabIcon.show(false);
			this.chatTField.isShow = false;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x002C7A5C File Offset: 0x002C5C5C
		private void addFriend(InfoItem info)
		{
			string text = "|0|1|" + info.charInfo.cName;
			text += "\n";
			text = ((!info.isOnline) ? (text + "|3|1|" + mResources.is_offline) : (text + "|4|1|" + mResources.is_online));
			text += "\n--";
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|5|",
				mResources.power,
				": ",
				info.s
			});
			this.cp = new ChatPopup();
			this.popUpDetailInit(this.cp, text);
			this.charInfo = info.charInfo;
			this.currItem = null;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x002C7B20 File Offset: 0x002C5D20
		private void doFireEnemy()
		{
			if (this.selected >= 0 && this.vEnemy.size() != 0)
			{
				MyVector myVector = new MyVector();
				this.currInfoItem = this.selected;
				myVector.addElement(new Command(mResources.REVENGE, this, 10000, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
				myVector.addElement(new Command(mResources.DELETE, this, 10001, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addFriend((InfoItem)this.vEnemy.elementAt(this.selected));
			}
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x002C7C00 File Offset: 0x002C5E00
		private void doFireFriend()
		{
			if (this.selected >= 0 && this.vFriend.size() != 0)
			{
				MyVector myVector = new MyVector();
				this.currInfoItem = this.selected;
				InfoItem infoItem = (InfoItem)this.vFriend.elementAt(this.currInfoItem);
				myVector.addElement(new Command(mResources.CHAT, this, 8001, infoItem));
				myVector.addElement(new Command(mResources.DELETE, this, 8002, infoItem));
				myVector.addElement(new Command(mResources.den, this, 8004, infoItem.charInfo.charID));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addFriend((InfoItem)this.vFriend.elementAt(this.selected));
			}
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x002C7CF0 File Offset: 0x002C5EF0
		private void doFireChangeFlag()
		{
			if (this.selected >= 0)
			{
				MyVector myVector = new MyVector();
				this.currInfoItem = this.selected;
				myVector.addElement(new Command(mResources.change_flag, this, 10030, null));
				myVector.addElement(new Command(mResources.BACK, this, 10031, null));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x002C7D74 File Offset: 0x002C5F74
		private void doFireLogMessage()
		{
			if (this.selected == 0)
			{
				this.isViewChatServer = !this.isViewChatServer;
				Rms.saveRMSInt("viewchat", this.isViewChatServer ? 1 : 0);
				if (GameCanvas.isTouch)
				{
					this.selected = -1;
					return;
				}
			}
			else if (this.selected >= 0 && this.logChat.size() != 0)
			{
				MyVector myVector = new MyVector();
				this.currInfoItem = this.selected - 1;
				InfoItem infoItem = (InfoItem)this.logChat.elementAt(this.currInfoItem);
				myVector.addElement(new Command(mResources.CHAT, this, 8001, infoItem));
				myVector.addElement(new Command(mResources.make_friend, this, 8003, infoItem));
				myVector.addElement(new Command(ModFunc.strTeleportTo, this, 8004, infoItem.charInfo.charID));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addLogMessage((InfoItem)this.logChat.elementAt(this.selected - 1));
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x002C7EA8 File Offset: 0x002C60A8
		private void doFireClanOption()
		{
			try
			{
				this.partID = null;
				this.charInfo = null;
				if (this.selected < 0)
				{
					this.cSelected = -1;
				}
				else
				{
					if (Char.myCharz().clan == null)
					{
						if (this.selected == 0)
						{
							if (this.cSelected == 0)
							{
								this.searchClan();
							}
							else if (this.cSelected == 1)
							{
								InfoDlg.showWait();
								this.creatClan();
								Service.gI().getClan(1, -1, null);
							}
						}
						else if (this.selected != -1)
						{
							if (this.selected == 1)
							{
								if (this.isSearchClan)
								{
									Service.gI().searchClan(string.Empty);
								}
								else if (this.isViewMember && this.currClan != null)
								{
									GameCanvas.startYesNoDlg(mResources.do_u_want_join_clan + this.currClan.name, new Command(mResources.YES, this, 4000, this.currClan), new Command(mResources.NO, this, 4005, this.currClan));
								}
							}
							else if (this.isSearchClan)
							{
								this.currClan = this.getCurrClan();
								if (this.currClan != null)
								{
									MyVector myVector = new MyVector();
									myVector.addElement(new Command(mResources.request_join_clan, this, 4000, this.currClan));
									myVector.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
									GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
									this.addClanDetail(this.getCurrClan());
								}
							}
							else if (this.isViewMember)
							{
								this.currMem = this.getCurrMember();
								if (this.currMem != null)
								{
									MyVector myVector2 = new MyVector();
									myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currClan));
									GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
									GameCanvas.menu.startAt(myVector2, 0, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
									this.addClanMemberDetail(this.currMem);
								}
							}
						}
					}
					else if (this.selected == 0)
					{
						if (this.isMessage)
						{
							if (this.cSelected == 0)
							{
								if (this.myMember.size() > 1)
								{
									this.chatClan();
								}
								else
								{
									this.member = null;
									this.isSearchClan = false;
									this.isViewMember = true;
									this.isMessage = false;
									this.currentListLength = this.myMember.size() + 2;
									this.initTabClans();
								}
							}
							if (this.cSelected == 1)
							{
								Service.gI().clanMessage(1, null, -1);
							}
							if (this.cSelected == 2)
							{
								this.member = null;
								this.isSearchClan = false;
								this.isViewMember = true;
								this.isMessage = false;
								this.currentListLength = this.myMember.size() + 2;
								this.initTabClans();
								this.getCurrClanOtion();
							}
						}
						else if (this.isViewMember)
						{
							if (this.cSelected == 0)
							{
								this.isSearchClan = false;
								this.isViewMember = false;
								this.isMessage = true;
								this.currentListLength = ClanMessage.vMessage.size() + 2;
								this.initTabClans();
							}
							if (this.cSelected == 1)
							{
								if (this.myMember.size() > 1)
								{
									Service.gI().leaveClan();
								}
								else
								{
									this.chagenSlogan();
								}
							}
							if (this.cSelected == 2)
							{
								if (this.myMember.size() > 1)
								{
									this.chagenSlogan();
								}
								else
								{
									Service.gI().getClan(3, -1, null);
								}
							}
							if (this.cSelected == 3)
							{
								Service.gI().getClan(3, -1, null);
							}
						}
					}
					else if (this.selected == 1)
					{
						if (this.isSearchClan)
						{
							Service.gI().searchClan(string.Empty);
						}
					}
					else if (this.isSearchClan)
					{
						this.currClan = this.getCurrClan();
						if (this.currClan != null)
						{
							MyVector myVector3 = new MyVector();
							myVector3.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
							GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addClanDetail(this.getCurrClan());
						}
					}
					else if (this.isViewMember)
					{
						this.currMem = this.getCurrMember();
						if (this.currMem != null)
						{
							MyVector myVector4 = new MyVector();
							if (this.member != null)
							{
								myVector4.addElement(new Command(mResources.CLOSE, this, 8000, null));
							}
							else if (this.myMember != null)
							{
								if (Char.myCharz().charID == this.currMem.ID || Char.myCharz().role == 2)
								{
									myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
								}
								if (Char.myCharz().role < 2 && Char.myCharz().charID != this.currMem.ID)
								{
									if (this.currMem.role == 0 || this.currMem.role == 1)
									{
										myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
									}
									if (this.currMem.role == 2)
									{
										myVector4.addElement(new Command(mResources.create_clan_co_leader, this, 5002, this.currMem));
									}
									if (Char.myCharz().role == 0)
									{
										myVector4.addElement(new Command(mResources.create_clan_leader, this, 5001, this.currMem));
										if (this.currMem.role == 1)
										{
											myVector4.addElement(new Command(mResources.disable_clan_mastership, this, 5003, this.currMem));
										}
									}
								}
								if (Char.myCharz().role < this.currMem.role)
								{
									myVector4.addElement(new Command(mResources.kick_clan_mem, this, 5004, this.currMem));
								}
							}
							myVector4.addElement(new Command(ModFunc.strTeleportTo, this, 8004, this.currMem.ID));
							GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addClanMemberDetail(this.currMem);
						}
					}
					else if (this.isMessage)
					{
						this.currMess = this.getCurrMessage();
						if (this.currMess != null)
						{
							if (this.currMess.type == 0)
							{
								MyVector myVector5 = new MyVector();
								myVector5.addElement(new Command(mResources.CLOSE, this, 8000, this.currMess));
								GameCanvas.menu.startAt(myVector5, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								this.addMessageDetail(this.currMess);
							}
							else if (this.currMess.type == 1)
							{
								if (this.currMess.playerId != Char.myCharz().charID && this.cSelected != -1)
								{
									Service.gI().clanDonate(this.currMess.id);
								}
							}
							else if (this.currMess.type == 2 && this.currMess.option != null)
							{
								if (this.cSelected == 0)
								{
									Service.gI().joinClan(this.currMess.id, 1);
								}
								else if (this.cSelected == 1)
								{
									Service.gI().joinClan(this.currMess.id, 0);
								}
							}
						}
					}
					if (GameCanvas.isTouch)
					{
						this.cSelected = -1;
						this.selected = -1;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x002C86A4 File Offset: 0x002C68A4
		private void doFireMain()
		{
			try
			{
				if (this.currentTabIndex == 0)
				{
					this.setTypeMap();
				}
				if (this.currentTabIndex == 1)
				{
					this.doFireInventory();
				}
				if (this.currentTabIndex == 2)
				{
					this.doFireSkill();
				}
				if (this.currentTabIndex == 3)
				{
					if (this.mainTabName.Length == 4)
					{
						this.doFireTool();
					}
					else
					{
						this.doFireClanOption();
					}
				}
				if (this.currentTabIndex == 4)
				{
					this.doFireTool();
				}
			}
			catch (Exception ex)
			{
				Res.outz("Throw ex " + ex.StackTrace);
			}
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x002C8738 File Offset: 0x002C6938
		private void doFireSkill()
		{
			if (this.selected < 0)
			{
				return;
			}
			if (Char.myCharz().statusMe == 14)
			{
				GameCanvas.startOKDlg(mResources.can_not_do_when_die);
				return;
			}
			if (this.selected != 0 && this.selected != 1 && this.selected != 2 && this.selected != 3 && this.selected != 4 && this.selected != 5)
			{
				int index = this.selected - 6;
				SkillTemplate skillTemplate = Char.myCharz().nClass.skillTemplates[index];
				Skill skill = Char.myCharz().getSkill(skillTemplate);
				Skill skill2 = null;
				MyVector myVector8 = new MyVector(string.Empty);
				if (skill != null)
				{
					if (skill.point == skillTemplate.maxPoint)
					{
						myVector8.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
						myVector8.addElement(new Command(mResources.CLOSE, 2));
					}
					else
					{
						skill2 = skillTemplate.skills[skill.point];
						myVector8.addElement(new Command(mResources.UPGRADE, this, 9002, skill2));
						myVector8.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
					}
				}
				else
				{
					skill2 = skillTemplate.skills[0];
					myVector8.addElement(new Command(mResources.learn, this, 9004, skill2));
				}
				GameCanvas.menu.startAt(myVector8, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail(skillTemplate, skill, skill2);
				return;
			}
			long cTiemNang = Char.myCharz().cTiemNang;
			long cHPGoc = Char.myCharz().cHPGoc;
			long cMPGoc = Char.myCharz().cMPGoc;
			long cDamGoc = Char.myCharz().cDamGoc;
			long cDefGoc = (long)Char.myCharz().cDefGoc;
			int cCriticalGoc = Char.myCharz().cCriticalGoc;
			int num2 = 1000;
			if (this.selected == 0)
			{
				if (cTiemNang < Char.myCharz().cHPGoc + (long)num2)
				{
					GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Char.myCharz().cTiemNang.ToString() + mResources.not_enough_potential_point2 + (Char.myCharz().cHPGoc + (long)num2).ToString(), false);
					return;
				}
				if (cTiemNang > cHPGoc && cTiemNang < 10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L)
				{
					GameCanvas.startYesNoDlg(string.Concat(new string[]
					{
						mResources.use_potential_point_for1,
						(cHPGoc + (long)num2).ToString(),
						mResources.use_potential_point_for2,
						Char.myCharz().hpFrom1000TiemNang.ToString(),
						mResources.for_HP
					}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
					return;
				}
				MyVector myVector9 = new MyVector(string.Empty);
				if (cTiemNang >= 10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L && cTiemNang < 100L * (2L * (cHPGoc + (long)num2) + 1980L) / 2L)
				{
					myVector9.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().hpFrom1000TiemNang.ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(cHPGoc + (long)num2)
					}), this, 9000, null));
					myVector9.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().hpFrom1000TiemNang)).ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L)
					}), this, 9006, null));
				}
				else if (cTiemNang >= 100L * (2L * (cHPGoc + (long)num2) + 1980L) / 2L)
				{
					myVector9.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().hpFrom1000TiemNang.ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(cHPGoc + (long)num2)
					}), this, 9000, null));
					myVector9.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().hpFrom1000TiemNang)).ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L)
					}), this, 9006, null));
					myVector9.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(100 * Char.myCharz().hpFrom1000TiemNang)).ToString(),
						mResources.HP,
						"\n-",
						Res.formatNumber2(100L * (2L * (cHPGoc + (long)num2) + 1980L) / 2L)
					}), this, 9007, null));
				}
				myVector9.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, this.selected.ToString() + "-" + false.ToString()));
				GameCanvas.menu.startAt(myVector9, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected, false);
			}
			if (this.selected == 1)
			{
				if (Char.myCharz().cTiemNang < Char.myCharz().cMPGoc + (long)num2)
				{
					GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Char.myCharz().cTiemNang.ToString() + mResources.not_enough_potential_point2 + (Char.myCharz().cMPGoc + (long)num2).ToString());
					return;
				}
				if (cTiemNang > cMPGoc && cTiemNang < 10L * (2L * (cMPGoc + (long)num2) + 180L) / 2L)
				{
					GameCanvas.startYesNoDlg(string.Concat(new string[]
					{
						mResources.use_potential_point_for1,
						(cMPGoc + (long)num2).ToString(),
						mResources.use_potential_point_for2,
						Char.myCharz().mpFrom1000TiemNang.ToString(),
						mResources.for_KI
					}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
					return;
				}
				MyVector myVector10 = new MyVector(string.Empty);
				if (cTiemNang >= 10L * (2L * (cMPGoc + (long)num2) + 180L) / 2L && cTiemNang < 100L * (2L * (cMPGoc + (long)num2) + 1980L) / 2L)
				{
					myVector10.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().mpFrom1000TiemNang.ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(cHPGoc + (long)num2)
					}), this, 9000, null));
					myVector10.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().mpFrom1000TiemNang)).ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(10L * (2L * (cHPGoc + (long)num2) + 180L) / 2L)
					}), this, 9006, null));
				}
				else if (cTiemNang >= 100L * (2L * (cMPGoc + (long)num2) + 1980L) / 2L)
				{
					myVector10.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().mpFrom1000TiemNang.ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(cMPGoc + (long)num2)
					}), this, 9000, null));
					myVector10.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().mpFrom1000TiemNang)).ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(10L * (2L * (cMPGoc + (long)num2) + 180L) / 2L)
					}), this, 9006, null));
					myVector10.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(100 * Char.myCharz().mpFrom1000TiemNang)).ToString(),
						mResources.KI,
						"\n-",
						Res.formatNumber2(100L * (2L * (cMPGoc + (long)num2) + 1980L) / 2L)
					}), this, 9007, null));
				}
				myVector10.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, this.selected.ToString() + "-" + false.ToString()));
				GameCanvas.menu.startAt(myVector10, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected, false);
			}
			if (this.selected == 2)
			{
				if (Char.myCharz().cTiemNang < Char.myCharz().cDamGoc * (long)Char.myCharz().expForOneAdd)
				{
					GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Char.myCharz().cTiemNang.ToString() + mResources.not_enough_potential_point2 + (cDamGoc * 100L).ToString());
					return;
				}
				if (cTiemNang > cDamGoc && cTiemNang < 10L * (2L * cDamGoc + 9L) / 2L * (long)Char.myCharz().expForOneAdd)
				{
					GameCanvas.startYesNoDlg(string.Concat(new string[]
					{
						mResources.use_potential_point_for1,
						(cDamGoc * 100L).ToString(),
						mResources.use_potential_point_for2,
						Char.myCharz().damFrom1000TiemNang.ToString(),
						mResources.for_hit_point
					}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
					return;
				}
				MyVector myVector11 = new MyVector(string.Empty);
				if (cTiemNang >= 10L * (2L * cDamGoc + 9L) / 2L * (long)Char.myCharz().expForOneAdd && cTiemNang < 100L * (2L * cDamGoc + 99L) / 2L * (long)Char.myCharz().expForOneAdd)
				{
					myVector11.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().damFrom1000TiemNang.ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(cDamGoc * 100L)
					}), this, 9000, null));
					myVector11.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().damFrom1000TiemNang)).ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(10L * (2L * cDamGoc + 9L) / 2L * (long)Char.myCharz().expForOneAdd)
					}), this, 9006, null));
				}
				else if (cTiemNang >= 100L * (2L * cDamGoc + 99L) / 2L * (long)Char.myCharz().expForOneAdd)
				{
					myVector11.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						Char.myCharz().damFrom1000TiemNang.ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(cDamGoc * 100L)
					}), this, 9000, null));
					myVector11.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(10 * Char.myCharz().damFrom1000TiemNang)).ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(10L * (2L * cDamGoc + 9L) / 2L * (long)Char.myCharz().expForOneAdd)
					}), this, 9006, null));
					myVector11.addElement(new Command(string.Concat(new string[]
					{
						mResources.increase_upper,
						"\n",
						((int)(100 * Char.myCharz().damFrom1000TiemNang)).ToString(),
						"\n",
						mResources.hit_point,
						"\n-",
						Res.formatNumber2(100L * (2L * cDamGoc + 99L) / 2L * (long)Char.myCharz().expForOneAdd)
					}), this, 9007, null));
				}
				myVector11.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, this.selected.ToString() + "-" + false.ToString()));
				GameCanvas.menu.startAt(myVector11, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected, false);
			}
			if (this.selected == 3)
			{
				if (Char.myCharz().cTiemNang < (long)(50000 + Char.myCharz().cDefGoc * 1000))
				{
					GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + NinjaUtil.getMoneys(Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + NinjaUtil.getMoneys((long)(50000 + Char.myCharz().cDefGoc * 1000)));
					return;
				}
				long number = 2L * (cDefGoc + 5L) / 2L * 100000L;
				long number2 = 10L * (2L * (cDefGoc + 5L) + 9L) / 2L * 100000L;
				long number3 = 100L * (2L * (cDefGoc + 5L) + 99L) / 2L * 100000L;
				MyVector myVector12 = new MyVector(string.Empty);
				myVector12.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n1 ",
					mResources.armor,
					"\n",
					Res.formatNumber2(number)
				}), this, 9000, null));
				myVector12.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n10 ",
					mResources.armor,
					"\n",
					Res.formatNumber2(number2)
				}), this, 9006, null));
				myVector12.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n100 ",
					mResources.armor,
					"\n",
					Res.formatNumber2(number3)
				}), this, 9007, null));
				myVector12.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, this.selected.ToString() + "-" + false.ToString()));
				GameCanvas.menu.startAt(myVector12, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected, false);
				return;
			}
			else
			{
				if (this.selected != 4)
				{
					if (this.selected == 5)
					{
						Service.gI().speacialSkill(0);
					}
					return;
				}
				int num3 = Char.myCharz().cCriticalGoc;
				if (num3 > Panel.t_tiemnang.Length - 1)
				{
					num3 = Panel.t_tiemnang.Length - 1;
				}
				long num4 = Panel.t_tiemnang[num3];
				if (Char.myCharz().cTiemNang < num4)
				{
					GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Res.formatNumber2(Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + Res.formatNumber2(num4));
					return;
				}
				GameCanvas.startYesNoDlg(string.Concat(new string[]
				{
					mResources.use_potential_point_for1,
					Res.formatNumber(num4),
					mResources.use_potential_point_for2,
					Char.myCharz().criticalFrom1000Tiemnang.ToString(),
					mResources.for_crit
				}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
				return;
			}
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x002C981C File Offset: 0x002C7A1C
		private void addLogMessage(InfoItem info)
		{
			string text = "|0|1|" + info.charInfo.cName;
			text += "\n";
			text += "\n--";
			text = text + "\n|5|" + Res.split(info.s, "|", 0)[2];
			this.cp = new ChatPopup();
			this.popUpDetailInit(this.cp, text);
			this.charInfo = info.charInfo;
			this.currItem = null;
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x002C98A4 File Offset: 0x002C7AA4
		private void addSkillDetail2(int type, bool isPet)
		{
			string empty = string.Empty;
			long num = 0L;
			Char @char = isPet ? Char.myPetz() : Char.myCharz();
			if (this.selected == 0)
			{
				num = @char.cHPGoc + 1000L;
			}
			if (this.selected == 1)
			{
				num = @char.cMPGoc + 1000L;
			}
			if (this.selected == 2)
			{
				num = @char.cDamGoc * (long)@char.expForOneAdd;
			}
			if (this.selected == 3)
			{
				num = (long)(500000 + @char.cDefGoc * 100000);
			}
			string text = empty;
			empty = string.Concat(new string[]
			{
				text,
				"|5|2|",
				mResources.USE,
				" ",
				num.ToString(),
				" ",
				mResources.potential
			});
			if (type == 0)
			{
				empty = empty + "\n|5|2|" + mResources.to_gain_20hp;
			}
			if (type == 1)
			{
				empty = empty + "\n|5|2|" + mResources.to_gain_20mp;
			}
			if (type == 2)
			{
				empty = empty + "\n|5|2|" + mResources.to_gain_1pow;
			}
			if (type == 3)
			{
				empty = empty + "\n|5|2|" + mResources.to_gain_1pow;
			}
			this.currItem = null;
			this.partID = null;
			this.charInfo = null;
			this.idIcon = -1;
			this.cp = new ChatPopup();
			this.popUpDetailInit(this.cp, empty);
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x002C99F8 File Offset: 0x002C7BF8
		private void doFireMap()
		{
			if (Panel.imgMap != null)
			{
				Panel.imgMap.texture = null;
				Panel.imgMap = null;
			}
			TileMap.lastPlanetId = -1;
			mSystem.gcc();
			SmallImage.loadBigRMS();
			this.setTypeMain();
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x002C9A43 File Offset: 0x002C7C43
		private void doFireZone()
		{
			if (this.selected != -1)
			{
				Res.outz("FIRE ZONE");
				this.isChangeZone = true;
				GameCanvas.panel.hide();
			}
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x002C9A6C File Offset: 0x002C7C6C
		public void updateRequest(int recieve, int maxCap)
		{
			this.cp.says[this.cp.says.Length - 1] = string.Concat(new string[]
			{
				mResources.received,
				" ",
				recieve.ToString(),
				"/",
				maxCap.ToString()
			});
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x002C9ACC File Offset: 0x002C7CCC
		private void doFireBox()
		{
			if (this.selected < 0)
			{
				return;
			}
			this.currItem = null;
			MyVector myVector = new MyVector();
			if (this.currentTabIndex == 0 && !this.Equals(GameCanvas.panel2))
			{
				if (this.selected == 0 && !ModFunc.isInventory)
				{
					this.setNewSelected(Char.myCharz().arrItemBox.Length, false);
					return;
				}
				sbyte b = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
				Item item = Char.myCharz().arrItemBox[(int)b];
				if (item != null)
				{
					if (this.isBoxClan)
					{
						myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
						myVector.addElement(new Command(mResources.USE, this, 2010, item));
					}
					else if (item.isTypeBody())
					{
						myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
					}
					else
					{
						myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
					}
					this.currItem = item;
				}
			}
			if (this.currentTabIndex == 1 || this.Equals(GameCanvas.panel2))
			{
				if (this.selected == 0 && !ModFunc.isInventory)
				{
					this.setNewSelected(Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length, true);
					return;
				}
				Item[] arrItemBody = Char.myCharz().arrItemBody;
				if (!this.GetInventorySelect_isbody(this.selected, this.newSelected, arrItemBody))
				{
					sbyte b2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
					Item item2 = Char.myCharz().arrItemBag[(int)b2];
					if (item2 != null)
					{
						myVector.addElement(new Command(mResources.move_to_chest, this, 1001, item2));
						if (item2.isTypeBody())
						{
							myVector.addElement(new Command(mResources.USE, this, 2000, item2));
						}
						else
						{
							myVector.addElement(new Command(mResources.USE, this, 2001, item2));
						}
						this.currItem = item2;
					}
				}
				else
				{
					Item item3 = Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
					if (item3 != null)
					{
						myVector.addElement(new Command(mResources.move_to_chest2, this, 1002, item3));
						this.currItem = item3;
					}
				}
			}
			if (this.currItem != null)
			{
				Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
				if (this.isBoxClan)
				{
					myVector.addElement(new Command(mResources.MOVEOUT, this, 2011, this.currItem));
				}
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x002C9DC4 File Offset: 0x002C7FC4
		public void itemRequest(sbyte itemAction, string info, sbyte where, sbyte index)
		{
			GameCanvas.endDlg();
			ItemObject itemObject = new ItemObject();
			itemObject.type = (int)itemAction;
			itemObject.id = (int)index;
			itemObject.where = (int)where;
			if (ModFunc.isFilterItem)
			{
				this.perform(2004, itemObject);
				return;
			}
			GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 2004, itemObject), new Command(mResources.NO, this, 4005, null));
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x002C9E30 File Offset: 0x002C8030
		public void saleRequest(sbyte type, string info, short id)
		{
			ItemObject itemObject = new ItemObject();
			itemObject.type = (int)type;
			itemObject.id = (int)id;
			GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 3003, itemObject), new Command(mResources.NO, this, 4005, null));
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x002C9E7C File Offset: 0x002C807C
		public void perform(int idAction, object p)
		{
			if (idAction != 8010)
			{
				if (idAction == 8011)
				{
					if (this.chatTField == null)
					{
						this.chatTField = new ChatTextField();
						this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
						this.chatTField.initChatTextField();
						this.chatTField.parentScreen = this;
					}
					string infoIntrinsic = (string)p;
					ModFunc.GI().curSelectIntrinsic = infoIntrinsic;
					ModFunc.GI().MyChatTextField(this.chatTField, "Nhập chỉ số mong muốn", "Chỉ nhập số");
				}
			}
			else
			{
				if (this.chatTField == null)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = this;
				}
				ModFunc.GI().MyChatTextField(this.chatTField, "Nhập số sao cần đập", "Chỉ được nhập số");
			}
			if (idAction == 9999)
			{
				TopInfo topInfo = (TopInfo)p;
				Service.gI().sendThachDau(topInfo.pId);
			}
			if (idAction == 170391)
			{
				Rms.clearAll();
				if (mGraphics.zoomLevel > 1)
				{
					Rms.saveRMSInt("levelScreenKN", 1);
				}
				else
				{
					Rms.saveRMSInt("levelScreenKN", 0);
				}
				GameMidlet.instance.exit();
			}
			if (idAction == 6001)
			{
				Item item = (Item)p;
				item.isSelect = false;
				GameCanvas.panel.vItemCombine.removeElement(item);
				if (GameCanvas.panel.currentTabIndex == 0)
				{
					GameCanvas.panel.setTabCombine();
				}
				if (ModFunc.GI().isAutoPhaLe)
				{
					ModFunc.GI().itemPhale = null;
					ModFunc.GI().maxPhale = -1;
					ModFunc.GI().currPhale = -1;
				}
			}
			if (idAction == 6000)
			{
				Item item2 = (Item)p;
				for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
				{
					if (((Item)GameCanvas.panel.vItemCombine.elementAt(i)).template.id == item2.template.id)
					{
						GameCanvas.startOKDlg(mResources.already_has_item);
						return;
					}
				}
				item2.isSelect = true;
				GameCanvas.panel.vItemCombine.addElement(item2);
				if (GameCanvas.panel.currentTabIndex == 0)
				{
					GameCanvas.panel.setTabCombine();
				}
				if (ModFunc.GI().isAutoPhaLe)
				{
					ModFunc.GI().itemPhale = item2;
				}
			}
			if (idAction == 7000)
			{
				if (this.isLock)
				{
					GameCanvas.startOKDlg(mResources.unlock_item_to_trade);
					return;
				}
				Item item3 = (Item)p;
				for (int j = 0; j < GameCanvas.panel.vMyGD.size(); j++)
				{
					if (((Item)GameCanvas.panel.vMyGD.elementAt(j)).indexUI == item3.indexUI)
					{
						GameCanvas.startOKDlg(mResources.already_has_item);
						return;
					}
				}
				if (item3.quantity > 1)
				{
					this.putQuantily();
					return;
				}
				item3.isSelect = true;
				Item item4 = new Item();
				item4.template = item3.template;
				item4.itemOption = item3.itemOption;
				item4.indexUI = item3.indexUI;
				GameCanvas.panel.vMyGD.addElement(item4);
				Service.gI().giaodich(2, -1, (sbyte)item4.indexUI, item4.quantity);
			}
			if (idAction == 7001)
			{
				Item item5 = (Item)p;
				item5.isSelect = false;
				GameCanvas.panel.vMyGD.removeElement(item5);
				if (GameCanvas.panel.currentTabIndex == 1)
				{
					GameCanvas.panel.setTabGiaoDich(true);
				}
				Service.gI().giaodich(4, -1, (sbyte)item5.indexUI, -1);
			}
			if (idAction == 7002)
			{
				this.isAccept = true;
				GameCanvas.endDlg();
				Service.gI().giaodich(7, -1, -1, -1);
				this.hide();
			}
			if (idAction == 8003)
			{
				InfoItem infoItem = (InfoItem)p;
				Service.gI().friend(1, infoItem.charInfo.charID);
				int num = this.type;
			}
			if (idAction == 8002)
			{
				InfoItem infoItem2 = (InfoItem)p;
				Service.gI().friend(2, infoItem2.charInfo.charID);
			}
			if (idAction == 8004)
			{
				int charID = (int)p;
				ModFunc.GI().TeleportToPlayer(charID);
			}
			if (idAction == 8001)
			{
				InfoItem infoItem3 = (InfoItem)p;
				if (this.chatTField == null)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = GameCanvas.panel;
				}
				this.chatTField.strChat = mResources.chat_player;
				this.chatTField.tfChat.name = mResources.chat_with + " " + infoItem3.charInfo.cName;
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.isFocus = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				if (Main.isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				if (!Main.isPC)
				{
					this.chatTField.startChat(this, string.Empty);
				}
			}
			if (idAction == 1000)
			{
				Service.gI().getItem(Panel.BOX_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
			}
			if (idAction == 1001)
			{
				sbyte id = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, Char.myCharz().arrItemBody);
				Service.gI().getItem(Panel.BAG_BOX, id);
			}
			if (idAction == 1003)
			{
				this.hide();
			}
			if (idAction == 1002)
			{
				Service.gI().getItem(Panel.BODY_BOX, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
			}
			if (idAction == 2011)
			{
				Service.gI().useItem(1, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
			}
			if (idAction == 2010)
			{
				Service.gI().useItem(0, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
				Item item6 = (Item)p;
				if (item6 != null && (item6.template.id == 193 || item6.template.id == 194))
				{
					GameCanvas.panel.hide();
				}
			}
			if (idAction == 2000)
			{
				Item[] arrItemBody = Char.myCharz().arrItemBody;
				sbyte id2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
				if (this.isnewInventory)
				{
					id2 = (sbyte)this.currItem.indexUI;
				}
				Service.gI().getItem(Panel.BAG_BODY, id2);
			}
			if (idAction == 2001)
			{
				Res.outz("use item");
				Item item7 = (Item)p;
				bool inventorySelect_isbody = this.GetInventorySelect_isbody(this.selected, this.newSelected, Char.myCharz().arrItemBody);
				sbyte b = inventorySelect_isbody ? ((sbyte)this.GetInventorySelect_body(this.selected, this.newSelected)) : ((sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, Char.myCharz().arrItemBody));
				if (this.isnewInventory)
				{
					b = (sbyte)this.currItem.indexUI;
					sbyte where = 0;
					if (this.newSelected != 0)
					{
						where = 1;
					}
					Service.gI().useItem(0, where, b, -1);
				}
				else
				{
					Service.gI().useItem(0, (!inventorySelect_isbody) ? ((sbyte)1) : ((sbyte)0), b, -1);
				}
				if (item7.template.id == 193 || item7.template.id == 194)
				{
					GameCanvas.panel.hide();
				}
			}
			if (idAction == 2002)
			{
				if (this.isnewInventory)
				{
					Service.gI().getItem(Panel.BODY_BAG, (sbyte)this.sellectInventory);
				}
				else
				{
					Service.gI().getItem(Panel.BODY_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
				}
			}
			if (idAction == 2003)
			{
				Res.outz("remove item");
				bool inventorySelect_isbody2 = this.GetInventorySelect_isbody(this.selected, this.newSelected, Char.myCharz().arrItemBody);
				sbyte b2 = inventorySelect_isbody2 ? ((sbyte)this.GetInventorySelect_body(this.selected, this.newSelected)) : ((sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, Char.myCharz().arrItemBody));
				Service.gI().useItem(1, (!inventorySelect_isbody2) ? ((sbyte)1) : ((sbyte)0), b2, -1);
			}
			if (idAction == 2004)
			{
				GameCanvas.endDlg();
				ItemObject itemObject = (ItemObject)p;
				sbyte where2 = (sbyte)itemObject.where;
				sbyte index = (sbyte)itemObject.id;
				Service.gI().useItem((sbyte)((itemObject.type != 0) ? 2 : 3), where2, index, -1);
			}
			if (idAction == 2005)
			{
				sbyte id3 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, Char.myCharz().arrItemBody);
				Service.gI().getItem(Panel.BAG_PET, id3);
			}
			if (idAction == 2006)
			{
				sbyte id4 = (sbyte)this.selected;
				Service.gI().getItem(Panel.PET_BAG, id4);
			}
			if (idAction == 2007)
			{
				sbyte id5 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, Char.myCharz().arrItemBody);
				Service.gI().getItem(Panel.BAG_PET2, id5);
			}
			if (idAction == 2008)
			{
				sbyte id6 = (sbyte)this.selected;
				Service.gI().getItem(Panel.PET2_BAG, id6);
			}
			if (idAction == 30001)
			{
				Res.outz("nhan do");
				Service.gI().buyItem(0, this.selected, 0);
			}
			if (idAction == 30002)
			{
				Res.outz("xoa do");
				Service.gI().buyItem(1, this.selected, 0);
			}
			if (idAction == 30003)
			{
				Res.outz("nhan tat");
				Service.gI().buyItem(2, this.selected, 0);
			}
			if (idAction == 3000)
			{
				Res.outz("mua do");
				Item item8 = (Item)p;
				Service.gI().buyItem(0, (int)item8.template.id, 0);
			}
			if (idAction == 3001)
			{
				Item item9 = (Item)p;
				GameCanvas.msgdlg.pleasewait();
				Service.gI().buyItem(1, (int)item9.template.id, 0);
			}
			if (idAction == 3002)
			{
				GameCanvas.endDlg();
				bool inventorySelect_isbody3 = this.GetInventorySelect_isbody(this.selected, this.newSelected, Char.myCharz().arrItemBody);
				sbyte b3 = inventorySelect_isbody3 ? ((sbyte)this.GetInventorySelect_body(this.selected, this.newSelected)) : ((sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, Char.myCharz().arrItemBody));
				Service.gI().saleItem(0, (!inventorySelect_isbody3) ? ((sbyte)1) : ((sbyte)0), b3);
			}
			if (idAction == 3003)
			{
				GameCanvas.endDlg();
				ItemObject itemObject2 = (ItemObject)p;
				Service.gI().saleItem(1, (sbyte)itemObject2.type, (short)itemObject2.id);
			}
			if (idAction == 3004)
			{
				Item item10 = (Item)p;
				Service.gI().buyItem(3, (int)item10.template.id, 0);
			}
			if (idAction == 3005)
			{
				Item item11 = (Item)p;
				Service.gI().buyItem(3, (int)item11.template.id, 0);
			}
			if (idAction == 3006)
			{
				Item item12 = (Item)p;
				ModFunc.GI().AutoBuyItem(50, item12);
			}
			if (idAction == 3007)
			{
				Item item13 = (Item)p;
				ModFunc.GI().AutoBuyItem(1000, item13);
			}
			if (idAction == 4000)
			{
				Clan clan = (Clan)p;
				if (clan != null)
				{
					GameCanvas.endDlg();
					Service.gI().clanMessage(2, null, clan.ID);
				}
			}
			if (idAction == 4001)
			{
				Clan clan2 = (Clan)p;
				if (clan2 != null)
				{
					InfoDlg.showWait();
					this.clanReport = mResources.PLEASEWAIT;
					Service.gI().clanMember(clan2.ID);
				}
			}
			if (idAction == 4005)
			{
				GameCanvas.endDlg();
			}
			if (idAction == 4007)
			{
				GameCanvas.endDlg();
			}
			if (idAction == 4006)
			{
				ClanMessage clanMessage = (ClanMessage)p;
				Service.gI().clanDonate(clanMessage.id);
			}
			if (idAction == 5001)
			{
				Member member = (Member)p;
				Service.gI().clanRemote(member.ID, 0);
			}
			if (idAction == 5002)
			{
				Member member2 = (Member)p;
				Service.gI().clanRemote(member2.ID, 1);
			}
			if (idAction == 5003)
			{
				Member member3 = (Member)p;
				Service.gI().clanRemote(member3.ID, 2);
			}
			if (idAction == 5004)
			{
				Member member4 = (Member)p;
				Service.gI().clanRemote(member4.ID, -1);
			}
			if (idAction == 9000)
			{
				bool forPet = p != null && (bool)p;
				Service.gI().upPotential(forPet, this.selected, 1);
				GameCanvas.endDlg();
				InfoDlg.showWait();
			}
			if (idAction == 9006)
			{
				bool forPet2 = p != null && (bool)p;
				Service.gI().upPotential(forPet2, this.selected, 10);
				GameCanvas.endDlg();
				InfoDlg.showWait();
			}
			if (idAction == 9007)
			{
				bool forPet3 = p != null && (bool)p;
				Service.gI().upPotential(forPet3, this.selected, 100);
				GameCanvas.endDlg();
				InfoDlg.showWait();
			}
			if (idAction == 9002)
			{
				Skill skill = (Skill)p;
				if (skill.template.isSkillSpec())
				{
					GameCanvas.startOKDlg(mResources.updSkill);
				}
				else
				{
					GameCanvas.startOKDlg(string.Concat(new string[]
					{
						mResources.can_buy_from_Uron1,
						skill.powRequire.ToString(),
						mResources.can_buy_from_Uron2,
						skill.moreInfo,
						mResources.can_buy_from_Uron3
					}));
				}
			}
			if (idAction == 9003)
			{
				if (GameCanvas.isTouch && !Main.isPC)
				{
					GameScr.gI().doSetOnScreenSkill((SkillTemplate)p);
				}
				else
				{
					GameScr.gI().doSetKeySkill((SkillTemplate)p);
				}
			}
			if (idAction == 9004)
			{
				Skill skill2 = (Skill)p;
				if (skill2.template.isSkillSpec())
				{
					GameCanvas.startOKDlg(mResources.learnSkill);
				}
				else
				{
					GameCanvas.startOKDlg(string.Concat(new string[]
					{
						mResources.can_buy_from_Uron1,
						skill2.powRequire.ToString(),
						mResources.can_buy_from_Uron2,
						skill2.moreInfo,
						mResources.can_buy_from_Uron3
					}));
				}
			}
			if (idAction == 10000)
			{
				InfoItem infoItem4 = (InfoItem)p;
				Service.gI().enemy(1, infoItem4.charInfo.charID);
				GameCanvas.panel.hideNow();
			}
			if (idAction == 10001)
			{
				InfoItem infoItem5 = (InfoItem)p;
				Service.gI().enemy(2, infoItem5.charInfo.charID);
				InfoDlg.showWait();
			}
			if (idAction == 10012)
			{
				if (this.chatTField == null)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
				}
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				this.chatTField.tfChat.setText(string.Empty);
				if (this.currItem.quantity == 1)
				{
					this.chatTField.strChat = mResources.kiguiXuchat;
					this.chatTField.tfChat.name = mResources.input_money;
				}
				else
				{
					this.chatTField.strChat = mResources.input_quantity + " ";
					this.chatTField.tfChat.name = mResources.input_quantity;
				}
				this.chatTField.tfChat.setMaxTextLenght(9);
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				if (GameCanvas.isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
				}
				if (Main.isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				if (!Main.isPC)
				{
					this.chatTField.startChat(this, string.Empty);
				}
			}
			if (idAction == 10013)
			{
				if (this.chatTField == null)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
				}
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				this.chatTField.tfChat.setText(string.Empty);
				if (this.currItem.quantity == 1)
				{
					this.chatTField.strChat = mResources.kiguiLuongchat;
					this.chatTField.tfChat.name = mResources.input_money;
				}
				else
				{
					this.chatTField.strChat = mResources.input_quantity + "  ";
					this.chatTField.tfChat.name = mResources.input_quantity;
				}
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				if (GameCanvas.isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
				}
				if (Main.isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				if (!Main.isPC)
				{
					this.chatTField.startChat(this, string.Empty);
				}
			}
			if (idAction == 10014)
			{
				Item item14 = (Item)p;
				Service.gI().kigui(1, item14.itemId, -1, -1, -1);
				InfoDlg.showWait();
			}
			if (idAction == 10015)
			{
				Item item15 = (Item)p;
				Service.gI().kigui(2, item15.itemId, -1, -1, -1);
				InfoDlg.showWait();
			}
			if (idAction == 10016)
			{
				Item item16 = (Item)p;
				Service.gI().kigui(3, item16.itemId, 0, item16.buyCoin, -1);
				InfoDlg.showWait();
			}
			if (idAction == 10017)
			{
				Item item17 = (Item)p;
				Service.gI().kigui(3, item17.itemId, 1, item17.buyGold, -1);
				InfoDlg.showWait();
			}
			if (idAction == 10018)
			{
				Item item18 = (Item)p;
				Service.gI().kigui(5, item18.itemId, -1, -1, -1);
				InfoDlg.showWait();
			}
			if (idAction == 10019)
			{
				Session_ME.gI().close();
				Rms.saveRMSString("acc", string.Empty);
				Rms.saveRMSString("pass", string.Empty);
				GameCanvas.loginScr.tfPass.setText(string.Empty);
				GameCanvas.loginScr.tfUser.setText(string.Empty);
				GameCanvas.loginScr.isLogin2 = false;
				GameCanvas.loginScr.switchToMe();
				GameCanvas.endDlg();
				this.hide();
			}
			if (idAction == 10020)
			{
				GameCanvas.endDlg();
			}
			if (idAction == 10030)
			{
				Service.gI().getFlag(1, (sbyte)this.selected);
				GameCanvas.panel.hideNow();
			}
			if (idAction == 10031)
			{
				Session_ME.gI().close();
			}
			if (idAction == 11000)
			{
				Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, 1);
				GameCanvas.endDlg();
			}
			if (idAction == 11001)
			{
				Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, this.currItem.quantilyToBuy);
				GameCanvas.endDlg();
			}
			if (idAction == 11002)
			{
				this.chatTField.isShow = false;
				GameCanvas.endDlg();
			}
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x002CB27C File Offset: 0x002C947C
		public void onChatFromMe(string text, string to)
		{
			if (this.chatTField.strChat == "Nhập chỉ số mong muốn")
			{
				if (this.chatTField.tfChat.getText() != string.Empty)
				{
					int param;
					if (int.TryParse(text, out param))
					{
						ModFunc.GI().SetAutoIntrinsic(param);
						this.isShow = false;
					}
					else
					{
						GameScr.info1.addInfo("Chỉ số đã nhập không hợp lệ", 0);
					}
					this.chatTField.isShow = false;
				}
			}
			else if (this.chatTField.strChat == "Nhập số sao cần đập" && this.chatTField.tfChat.getText() != string.Empty)
			{
				int maxPhale;
				if (int.TryParse(text, out maxPhale) && maxPhale > 0)
				{
					GameScr.info1.addInfo(string.Concat(new string[]
					{
						"Đập ",
						ModFunc.GI().itemPhale.template.name,
						" đến ",
						maxPhale.ToString(),
						" sao"
					}), 0);
					ModFunc.GI().maxPhale = maxPhale;
				}
				else
				{
					GameScr.info1.addInfo("Số sao đã nhập không đúng", 0);
				}
				this.chatTField.isShow = false;
			}
			if (this.chatTField.tfChat.getText() == null || this.chatTField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
			{
				this.chatTField.isShow = false;
				return;
			}
			if (this.chatTField.strChat.Equals(mResources.input_clan_name))
			{
				InfoDlg.showWait();
				this.chatTField.isShow = false;
				Service.gI().searchClan(text);
				return;
			}
			if (this.chatTField.strChat.Equals(mResources.chat_clan))
			{
				InfoDlg.showWait();
				this.chatTField.isShow = false;
				Service.gI().clanMessage(0, text, -1);
				return;
			}
			if (this.chatTField.strChat.Equals(mResources.input_clan_name_to_create))
			{
				if (this.chatTField.tfChat.getText() == string.Empty)
				{
					GameScr.info1.addInfo(mResources.clan_name_blank, 0);
					return;
				}
				if (this.tabIcon == null)
				{
					this.tabIcon = new TabClanIcon();
				}
				this.tabIcon.text = this.chatTField.tfChat.getText();
				this.tabIcon.show(false);
				this.chatTField.isShow = false;
				return;
			}
			else
			{
				if (!this.chatTField.strChat.Equals(mResources.input_clan_slogan))
				{
					if (this.chatTField.strChat.Equals(mResources.input_Inventory_Pass))
					{
						try
						{
							int lockInventory = int.Parse(this.chatTField.tfChat.getText());
							this.chatTField.isShow = false;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							this.hide();
							if (this.chatTField.tfChat.getText().Length != 6 || this.chatTField.tfChat.getText().Equals(string.Empty))
							{
								GameCanvas.startOKDlg(mResources.input_Inventory_Pass_wrong);
							}
							else
							{
								Service.gI().setLockInventory(lockInventory);
								this.chatTField.isShow = false;
								this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
								this.hide();
							}
							return;
						}
						catch (Exception)
						{
							GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
							return;
						}
					}
					if (this.chatTField.strChat.Equals(mResources.world_channel_5_luong))
					{
						if (!this.chatTField.tfChat.getText().Equals(string.Empty))
						{
							Service.gI().chatGlobal(this.chatTField.tfChat.getText());
							this.chatTField.isShow = false;
							return;
						}
					}
					else if (this.chatTField.strChat.Equals(mResources.chat_player))
					{
						this.chatTField.isShow = false;
						InfoItem infoItem = null;
						if (this.type == 8)
						{
							infoItem = (InfoItem)this.logChat.elementAt(this.currInfoItem);
						}
						else if (this.type == 11)
						{
							infoItem = (InfoItem)this.vFriend.elementAt(this.currInfoItem);
						}
						if (infoItem.charInfo.charID != Char.myCharz().charID)
						{
							Service.gI().chatPlayer(text, infoItem.charInfo.charID);
							return;
						}
					}
					else if (this.chatTField.strChat.Equals(mResources.input_quantity_to_trade))
					{
						int num = 0;
						try
						{
							num = int.Parse(this.chatTField.tfChat.getText());
						}
						catch (Exception)
						{
							GameCanvas.startOKDlg(mResources.input_quantity_wrong);
							this.chatTField.isShow = false;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							return;
						}
						if (num <= 0 || num > this.currItem.quantity)
						{
							GameCanvas.startOKDlg(mResources.input_quantity_wrong);
							this.chatTField.isShow = false;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							return;
						}
						this.currItem.isSelect = true;
						Item item = new Item
						{
							template = this.currItem.template,
							quantity = num,
							indexUI = this.currItem.indexUI,
							itemOption = this.currItem.itemOption
						};
						GameCanvas.panel.vMyGD.addElement(item);
						Service.gI().giaodich(2, -1, (sbyte)item.indexUI, item.quantity);
						this.chatTField.isShow = false;
						this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
						return;
					}
					else if (this.chatTField.strChat == mResources.input_money_to_trade)
					{
						int num2;
						try
						{
							num2 = int.Parse(this.chatTField.tfChat.getText());
						}
						catch (Exception)
						{
							GameCanvas.startOKDlg(mResources.input_money_wrong);
							this.chatTField.isShow = false;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							return;
						}
						if ((long)num2 > Char.myCharz().xu)
						{
							GameCanvas.startOKDlg(mResources.not_enough_money);
							this.chatTField.isShow = false;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							return;
						}
						this.moneyGD = num2;
						Service.gI().giaodich(2, -1, -1, num2);
						this.chatTField.isShow = false;
						this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
						return;
					}
					else
					{
						if (this.chatTField.strChat.Equals(mResources.kiguiXuchat))
						{
							Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), 1);
							this.chatTField.isShow = false;
							return;
						}
						if (this.chatTField.strChat.Equals(mResources.kiguiXuchat + " "))
						{
							Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), this.currItem.quantilyToBuy);
							this.chatTField.isShow = false;
							return;
						}
						if (this.chatTField.strChat.Equals(mResources.kiguiLuongchat))
						{
							this.doNotiRuby(0);
							this.chatTField.isShow = false;
							return;
						}
						if (this.chatTField.strChat.Equals(mResources.kiguiLuongchat + "  "))
						{
							this.doNotiRuby(1);
							this.chatTField.isShow = false;
							return;
						}
						if (this.chatTField.strChat.Equals(mResources.input_quantity + " "))
						{
							this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
							if (this.currItem.quantilyToBuy > this.currItem.quantity)
							{
								GameCanvas.startOKDlg(mResources.input_quantity_wrong);
								return;
							}
							this.isKiguiXu = true;
							this.chatTField.isShow = false;
							return;
						}
						else if (this.chatTField.strChat.Equals(mResources.input_quantity + "  "))
						{
							this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
							if (this.currItem.quantilyToBuy > this.currItem.quantity)
							{
								GameCanvas.startOKDlg(mResources.input_quantity_wrong);
								return;
							}
							this.isKiguiLuong = true;
							this.chatTField.isShow = false;
						}
					}
					return;
				}
				if (this.chatTField.tfChat.getText() == string.Empty)
				{
					GameScr.info1.addInfo(mResources.clan_slogan_blank, 0);
					return;
				}
				Service.gI().getClan(4, (sbyte)Char.myCharz().clan.imgID, this.chatTField.tfChat.getText());
				this.chatTField.isShow = false;
				return;
			}
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x002CBB98 File Offset: 0x002C9D98
		public void onCancelChat()
		{
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x002CBBB0 File Offset: 0x002C9DB0
		public void setCombineEff(int type)
		{
			this.typeCombine = type;
			this.rS = 90;
			if (this.typeCombine == 0)
			{
				this.iDotS = 5;
				this.angleS = (this.angleO = 90);
				this.time = 2;
				for (int i = 0; i < this.vItemCombine.size(); i++)
				{
					Item item = (Item)this.vItemCombine.elementAt(i);
					if (item != null)
					{
						if (item.template.type == 14)
						{
							this.iconID2 = item.template.iconID;
						}
						else
						{
							this.iconID1 = item.template.iconID;
						}
					}
				}
			}
			else if (this.typeCombine == 1)
			{
				this.iDotS = 2;
				this.angleS = (this.angleO = 0);
				this.time = 1;
				for (int j = 0; j < this.vItemCombine.size(); j++)
				{
					Item item2 = (Item)this.vItemCombine.elementAt(j);
					if (item2 != null)
					{
						if (j == 0)
						{
							this.iconID1 = item2.template.iconID;
						}
						else
						{
							this.iconID2 = item2.template.iconID;
						}
					}
				}
			}
			else if (this.typeCombine == 2)
			{
				this.iDotS = 7;
				this.angleS = (this.angleO = 25);
				this.time = 1;
				for (int k = 0; k < this.vItemCombine.size(); k++)
				{
					Item item3 = (Item)this.vItemCombine.elementAt(k);
					if (item3 != null)
					{
						this.iconID1 = item3.template.iconID;
					}
				}
			}
			else if (this.typeCombine == 3)
			{
				this.xS = GameCanvas.hw;
				this.yS = GameCanvas.hh;
				this.iDotS = 1;
				this.angleS = (this.angleO = 1);
				this.time = 4;
				for (int l = 0; l < this.vItemCombine.size(); l++)
				{
					Item item4 = (Item)this.vItemCombine.elementAt(l);
					if (item4 != null)
					{
						this.iconID1 = item4.template.iconID;
					}
				}
			}
			else if (this.typeCombine == 4)
			{
				this.iDotS = this.vItemCombine.size();
				this.iconID = new short[this.iDotS];
				this.angleS = (this.angleO = 25);
				this.time = 1;
				for (int m = 0; m < this.vItemCombine.size(); m++)
				{
					Item item5 = (Item)this.vItemCombine.elementAt(m);
					if (item5 != null)
					{
						this.iconID[m] = item5.template.iconID;
					}
				}
			}
			this.speed = 1;
			this.isSpeedCombine = true;
			this.isDoneCombine = false;
			this.isCompleteEffCombine = false;
			this.iAngleS = 360 / this.iDotS;
			this.xArgS = new int[this.iDotS];
			this.yArgS = new int[this.iDotS];
			this.xDotS = new int[this.iDotS];
			this.yDotS = new int[this.iDotS];
			this.setDotStar();
			this.isPaintCombine = true;
			this.countUpdate = 10;
			this.countR = 30;
			this.countWait = 10;
			this.addTextCombineNPC(this.idNPC, mResources.combineSpell);
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x002CBF08 File Offset: 0x002CA108
		private void updateCombineEff()
		{
			this.countUpdate--;
			if (this.countUpdate < 0)
			{
				this.countUpdate = 0;
			}
			this.countR--;
			if (this.countR < 0)
			{
				this.countR = 0;
			}
			if (this.countUpdate != 0)
			{
				return;
			}
			if (!this.isCompleteEffCombine)
			{
				if (this.time > 0)
				{
					if (this.combineSuccess != -1)
					{
						if (this.typeCombine == 3)
						{
							if (GameCanvas.gameTick % 10 == 0)
							{
								EffecMn.addEff(new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1));
								this.time--;
							}
						}
						else
						{
							if (GameCanvas.gameTick % 2 == 0)
							{
								if (this.isSpeedCombine)
								{
									if (this.speed < 40)
									{
										this.speed += 2;
									}
								}
								else if (this.speed > 10)
								{
									this.speed -= 2;
								}
							}
							if (this.countR == 0)
							{
								if (this.isSpeedCombine)
								{
									if (this.rS > 0)
									{
										this.rS -= 5;
									}
									else if (GameCanvas.gameTick % 10 == 0)
									{
										this.isSpeedCombine = false;
										this.time--;
										this.countR = 5;
										this.countWait = 10;
									}
								}
								else if (this.rS < 90)
								{
									this.rS += 5;
								}
								else if (GameCanvas.gameTick % 10 == 0)
								{
									this.isSpeedCombine = true;
									this.countR = 10;
								}
							}
							this.angleS = this.angleO;
							this.angleS -= this.speed;
							if (this.angleS >= 360)
							{
								this.angleS -= 360;
							}
							if (this.angleS < 0)
							{
								this.angleS = 360 + this.angleS;
							}
							this.angleO = this.angleS;
							this.setDotStar();
						}
					}
				}
				else if (GameCanvas.gameTick % 20 == 0)
				{
					this.isCompleteEffCombine = true;
				}
				if (GameCanvas.gameTick % 20 == 0)
				{
					if (this.typeCombine != 3)
					{
						EffectPanel.addServerEffect(132, this.xS, this.yS, 2);
					}
					EffectPanel.addServerEffect(114, this.xS, this.yS + 20, 2);
					return;
				}
			}
			else
			{
				if (!this.isCompleteEffCombine)
				{
					return;
				}
				if (this.combineSuccess == 1)
				{
					if (this.countWait == 10)
					{
						EffecMn.addEff(new Effect(22, this.xS - 3, this.yS + 25, 4, 1, 1));
					}
					this.countWait--;
					if (this.countWait < 0)
					{
						this.countWait = 0;
					}
					if (this.rS < 300)
					{
						this.rS = Res.abs(this.rS + 10);
						if (this.rS == 20)
						{
							this.addTextCombineNPC(this.idNPC, mResources.combineFail);
						}
					}
					else if (GameCanvas.gameTick % 20 == 0)
					{
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
						}
						this.combineSuccess = -1;
						this.isDoneCombine = true;
						if (this.typeCombine == 4)
						{
							GameCanvas.panel.hideNow();
						}
					}
					this.setDotStar();
					return;
				}
				if (this.combineSuccess != 0)
				{
					return;
				}
				if (this.countWait == 10)
				{
					if (this.typeCombine == 2)
					{
						EffecMn.addEff(new Effect(20, this.xS - 3, this.yS + 15, 4, 2, 1));
					}
					else
					{
						EffecMn.addEff(new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1));
					}
					this.addTextCombineNPC(this.idNPC, mResources.combineSuccess);
					this.isPaintCombine = false;
				}
				if (this.isPaintCombine)
				{
					return;
				}
				this.countWait--;
				if (this.countWait < -50)
				{
					this.countWait = -50;
					if (this.typeCombine < 3 && GameCanvas.w > 2 * Panel.WIDTH_PANEL)
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
					this.combineSuccess = -1;
					this.isDoneCombine = true;
					if (this.typeCombine == 4)
					{
						GameCanvas.panel.hideNow();
					}
				}
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x002CC398 File Offset: 0x002CA598
		public void paintCombineEff(mGraphics g)
		{
			GameScr.gI().paintBlackSky(g);
			this.paintCombineNPC(g);
			if (GameCanvas.gameTick % 4 == 0)
			{
				g.drawImage(ItemMap.imageFlare, this.xS, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if (this.typeCombine == 0)
			{
				for (int i = 0; i < this.yArgS.Length; i++)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					if (this.isPaintCombine)
					{
						SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[i], this.yDotS[i], 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
				}
				return;
			}
			if (this.typeCombine == 1)
			{
				if (!this.isPaintCombine)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					return;
				}
				for (int j = 0; j < this.yArgS.Length; j++)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[0], this.yDotS[0], 0, mGraphics.VCENTER | mGraphics.HCENTER);
					SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[1], this.yDotS[1], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
				return;
			}
			else if (this.typeCombine == 2)
			{
				if (!this.isPaintCombine)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					return;
				}
				for (int k = 0; k < this.yArgS.Length; k++)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[k], this.yDotS[k], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
				return;
			}
			else if (this.typeCombine == 3)
			{
				if (!this.isPaintCombine)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					return;
				}
				SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				return;
			}
			else
			{
				if (this.typeCombine != 4)
				{
					return;
				}
				if (!this.isPaintCombine)
				{
					if (this.iconID3 != -1)
					{
						SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						return;
					}
				}
				else
				{
					for (int l = 0; l < this.iconID.Length; l++)
					{
						SmallImage.drawSmallImage(g, (int)this.iconID[l], this.xDotS[l], this.yDotS[l], 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
				}
				return;
			}
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x002CC63C File Offset: 0x002CA83C
		private void setDotStar()
		{
			for (int i = 0; i < this.yArgS.Length; i++)
			{
				if (this.angleS >= 360)
				{
					this.angleS -= 360;
				}
				if (this.angleS < 0)
				{
					this.angleS = 360 + this.angleS;
				}
				this.yArgS[i] = Res.abs(this.rS * Res.sin(this.angleS) / 1024);
				this.xArgS[i] = Res.abs(this.rS * Res.cos(this.angleS) / 1024);
				if (this.angleS < 90)
				{
					this.xDotS[i] = this.xS + this.xArgS[i];
					this.yDotS[i] = this.yS - this.yArgS[i];
				}
				else if (this.angleS >= 90 && this.angleS < 180)
				{
					this.xDotS[i] = this.xS - this.xArgS[i];
					this.yDotS[i] = this.yS - this.yArgS[i];
				}
				else if (this.angleS >= 180 && this.angleS < 270)
				{
					this.xDotS[i] = this.xS - this.xArgS[i];
					this.yDotS[i] = this.yS + this.yArgS[i];
				}
				else
				{
					this.xDotS[i] = this.xS + this.xArgS[i];
					this.yDotS[i] = this.yS + this.yArgS[i];
				}
				this.angleS -= this.iAngleS;
			}
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x002CC7F8 File Offset: 0x002CA9F8
		public void paintCombineNPC(mGraphics g)
		{
			g.translate(-GameScr.cmx, -GameScr.cmy);
			if (this.typeCombine < 3)
			{
				for (int i = 0; i < GameScr.vNpc.size(); i++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(i);
					if (npc.template.npcTemplateId == this.idNPC)
					{
						npc.paint(g);
						if (npc.chatInfo != null)
						{
							npc.chatInfo.paint(g, npc.cx, npc.cy - npc.ch - GameCanvas.transY, npc.cdir);
						}
					}
				}
			}
			GameCanvas.resetTrans(g);
			if (GameCanvas.gameTick % 4 == 0)
			{
				g.drawImage(ItemMap.imageFlare, this.xS - 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
				g.drawImage(ItemMap.imageFlare, this.xS + 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
				g.drawImage(ItemMap.imageFlare, this.xS, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			for (int j = 0; j < Effect2.vEffect3.size(); j++)
			{
				((Effect2)Effect2.vEffect3.elementAt(j)).paint(g);
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x002CC940 File Offset: 0x002CAB40
		public void addTextCombineNPC(int idNPC, string text)
		{
			if (this.typeCombine >= 3)
			{
				return;
			}
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == idNPC)
				{
					npc.addInfo(text);
				}
			}
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x002CC994 File Offset: 0x002CAB94
		public void setTypeOption()
		{
			this.type = 19;
			this.setType(0);
			this.setTabOption();
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x002CC9C8 File Offset: 0x002CABC8
		public void SetTypeModFunc()
		{
			this.type = 26;
			this.tabName[26] = Panel.boxMod;
			this.setType(0);
			this.SetTabModFunc();
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x002CCA08 File Offset: 0x002CAC08
		private void SetTabModFunc()
		{
			SoundMn.gI().GetStrModFunc();
			this.currentListLength = Panel.strModFunc.Length;
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x002CCAC8 File Offset: 0x002CACC8
		private void setTabOption()
		{
			SoundMn.gI().getStrOption();
			this.currentListLength = Panel.strCauhinh.Length;
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x002CCB88 File Offset: 0x002CAD88
		private void paintOption(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < Panel.strCauhinh.Length; i++)
			{
				int x = this.xScroll;
				int num = this.yScroll + i * this.ITEM_HEIGHT;
				int num2 = this.wScroll - 1;
				int h = this.ITEM_HEIGHT - 1;
				if (num - this.cmy <= this.yScroll + this.hScroll && num - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, Panel.strCauhinh[i], this.xScroll + 10, num + 6, mFont.LEFT);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x002CCC84 File Offset: 0x002CAE84
		private void PaintModFunc(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < Panel.strModFunc.Length; i++)
			{
				int x = this.xScroll;
				int num = this.yScroll + i * this.ITEM_HEIGHT;
				int num2 = this.wScroll - 1;
				int h = this.ITEM_HEIGHT - 1;
				if (num - this.cmy <= this.yScroll + this.hScroll && num - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, Panel.strModFunc[i], this.xScroll + 10, num + 6, mFont.LEFT);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x002CCD80 File Offset: 0x002CAF80
		private void doFireOption()
		{
			if (this.selected >= 0)
			{
				switch (this.selected)
				{
					case 0:
						SoundMn.gI().AuraToolOption();
						return;
					case 1:
						SoundMn.gI().AuraToolOption2();
						return;
					case 2:
						SoundMn.gI().soundToolOption();
						return;
					case 3:
						SoundMn.gI().CaseSizeScr();
						return;
					case 4:
						SoundMn.gI().CaseAnalog();
						return;
					case 5:
						SoundMn.gI().CaseAnalog();
						break;
					default:
						return;
				}
			}
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x002CCE00 File Offset: 0x002CB000
		private void DoFireModFunc()
		{
			if (this.selected < 0)
			{
				return;
			}
			int currentTab = this.currentTabIndex;
			int selectedInTab = this.selected;
			switch (currentTab)
			{
				case 0:
					switch (selectedInTab)
					{
						case 0:
							ModFunc.GI().isHighFps = !ModFunc.GI().isHighFps;
							ModFunc.GI().ChangeFPSTarget();
							GameScr.info1.addInfo("Đã " + (ModFunc.GI().isHighFps ? "bật" : "tắt") + " FPS cao", 0);
							break;
						case 1:
							ModFunc.GI().isUpdateZones = !ModFunc.GI().isUpdateZones;
							GameScr.info1.addInfo("Đã " + (ModFunc.GI().isUpdateZones ? "bật" : "tắt") + " cập nhật khu vực", 0);
							break;
						case 2:
							ModFunc.GI().showCharsInMap = !ModFunc.GI().showCharsInMap;
							GameScr.info1.addInfo("Đã " + (ModFunc.GI().showCharsInMap ? "bật" : "tắt") + " hiển thị người chơi trong bản đồ", 0);
							break;
						case 3:
							ModFunc.GI().showInfoMe = !ModFunc.GI().showInfoMe;
							GameScr.info1.addInfo("Đã " + (ModFunc.GI().showInfoMe ? "bật" : "tắt") + " hiển thị thông tin bản thân", 0);
							break;
						case 4:
							if (Main.isIPhone)
							{
								ModFunc.GI().isShowButton = !ModFunc.GI().isShowButton;
								GameScr.info1.addInfo("Đã " + (ModFunc.GI().isShowButton ? "bật" : "tắt") + " hiển thị nút", 0);
							}
							else
							{
								GameScr.info1.addInfo("Hiển thị nút không hỗ trợ trên PC", 0);
							}
							break;
					}
					break;
				case 1:
					switch (selectedInTab)
					{
						case 0:
							ModFunc.GI().isAutoPhaLe = !ModFunc.GI().isAutoPhaLe;
							if (ModFunc.GI().isAutoPhaLe)
							{
								new Thread(new ThreadStart(ModFunc.GI().AutoPhaLe)).Start();
							}
							GameScr.info1.addInfo("Đã " + (ModFunc.GI().isAutoPhaLe ? "bật" : "tắt") + " tự động pha lê", 0);
							break;
						case 1:
							if (!ModFunc.GI().isAutoVQMM)
							{
								this.hideNow();
							}
							ModFunc.GI().isAutoVQMM = !ModFunc.GI().isAutoVQMM;
							GameScr.info1.addInfo("Đã " + (ModFunc.GI().isAutoVQMM ? "bật" : "tắt") + " tự động vòng quay may mắn", 0);
							break;
						case 2:
							ModFunc.GI().autoWakeUp = !ModFunc.GI().autoWakeUp;
							GameScr.info1.addInfo("Đã " + (ModFunc.GI().autoWakeUp ? "bật" : "tắt") + " Auto Hồi Sinh", 0);
							break;
						case 3:
							if (ModFunc.isAutoLogin)
							{
								ModFunc.isAutoLogin = false;
								ModFunc.autoLogin = null;
							}
							else
							{
								ModFunc.isAutoLogin = true;
								ModFunc.autoLogin = new AutoLogin
								{
									accAutoLogin = GameCanvas.loginScr.tfUser.getText()
								};
							}
							GameScr.info1.addInfo("Đã " + (ModFunc.isAutoLogin ? "bật" : "tắt") + " tự động đăng nhập", 0);
							break;
					}
					break;
				case 2:
					switch (selectedInTab)
					{
						case 0:
							if (!ModFunc.ModNotLogo)
							{
								ModFunc.changeStatusLogo();
								GameScr.info1.addInfo("Đã " + (ModFunc.isLogo ? "bật" : "tắt") + " hiển thị logo", 0);
							}
							break;
						case 1:
							if (!ModFunc.ModNotLogo && ModFunc.isLogo)
							{
								if (!ModFunc.ModNotLogoGif)
								{
									ModFunc.changeStatusLogoGif();
									GameScr.info1.addInfo("Đã " + (ModFunc.isLogoGif ? "bật" : "tắt") + " logo động", 0);
								}
								else
								{
									GameScr.info1.addInfo("Server Không có Logo GIF Bạn ơi", 0);
								}
							}
							else
							{
								GameScr.info1.addInfo("Vui lòng bật logo trước khi bật logo động", 0);
							}
							break;
						case 2:
							ModFunc.changeStatusAnPlayer();
							GameScr.info1.addInfo("Đã " + (ModFunc.AnPlayer ? "bật" : "tắt") + " ẩn người chơi", 0);
							break;
						case 3:
							ModFunc.changeStatusShowID();
							GameScr.info1.addInfo("Đã " + (ModFunc.isShowID ? "bật" : "tắt") + " hiển thị ID", 0);
							break;
						case 4:
							ModFunc.chanegStatusInventory();
							GameScr.info1.addInfo("Đã " + (ModFunc.isInventory ? "bật" : "tắt") + " hiển thị túi đồ", 0);
							break;
						case 5:
							ModFunc.changeStatusEffectInven();
							GameScr.info1.addInfo("Đã " + (ModFunc.isEffectInven ? "bật" : "tắt") + " hiệu ứng túi đồ", 0);
							break;
					}
					break;
				case 3:
					switch (selectedInTab)
					{
						case 0:
							ModFunc.GI().isIntroOff = !ModFunc.GI().isIntroOff;
							Rms.saveRMSInt("IntroOff", ModFunc.GI().isIntroOff ? 1 : 0);
							GameScr.info1.addInfo("Đã " + (ModFunc.GI().isIntroOff ? "bật" : "tắt") + " intro", 0);
							break;
						case 1:
							ModFunc.changeStatusBackground();
							GameScr.info1.addInfo("Đã " + (ModFunc.GiamDungLuong ? "bật" : "tắt") + " giảm dung lượng", 0);
							break;
						case 2:
							if (Main.isIPhone)
							{
								ModFunc.changeStatusEditButton();
								GameScr.info1.addInfo("Đã " + (ModFunc.isEditButton ? "bật" : "tắt") + " chỉnh sửa nút", 0);
							}
							else
							{
								GameScr.info1.addInfo("Chỉnh sửa nút không hỗ trợ trên PC", 0);
							}
							break;
						case 3:
							ModFunc.isFilterItem = !ModFunc.isFilterItem;
							GameScr.info1.addInfo("Đã " + (ModFunc.isFilterItem ? "bật" : "tắt") + " chế độ lọc đồ", 0);
							break;
					}
					break;
			}
			SoundMn.gI().GetStrModFunc();
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x002CD4B8 File Offset: 0x002CB6B8
		public void setTypeAccount()
		{
			this.type = 20;
			this.setType(0);
			this.setTabAccount();
			this.cmx = (this.cmtoX = 0);
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x002CD4EC File Offset: 0x002CB6EC
		private void setTabAccount()
		{
			if (Main.IphoneVersionApp)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg
				};
				if (GameScr.canAutoPlay)
				{
					Panel.strAccount = new string[]
					{
						mResources.inventory_Pass,
						mResources.friend,
						mResources.enemy,
						mResources.msg,
						mResources.autoFunction
					};
				}
			}
			else
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.charger
				};
				if (GameScr.canAutoPlay)
				{
					Panel.strAccount = new string[]
					{
						mResources.inventory_Pass,
						mResources.friend,
						mResources.enemy,
						mResources.msg,
						mResources.charger,
						mResources.autoFunction
					};
				}
				if ((mSystem.clientType == 2 || mSystem.clientType == 7) && mResources.language != 2)
				{
					Panel.strAccount = new string[]
					{
						mResources.inventory_Pass,
						mResources.friend,
						mResources.enemy,
						mResources.msg,
						mResources.charger
					};
					if (GameScr.canAutoPlay)
					{
						Panel.strAccount = new string[]
						{
							mResources.inventory_Pass,
							mResources.friend,
							mResources.enemy,
							mResources.msg,
							mResources.charger,
							mResources.autoFunction
						};
					}
				}
			}
			this.currentListLength = Panel.strAccount.Length;
			this.ITEM_HEIGHT = 24;
			this.selected = (GameCanvas.isTouch ? -1 : 0);
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x002CD718 File Offset: 0x002CB918
		private void paintAccount(mGraphics g)
		{
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			g.translate(0, -this.cmy);
			for (int i = 0; i < Panel.strAccount.Length; i++)
			{
				int x = this.xScroll;
				int num = this.yScroll + i * this.ITEM_HEIGHT;
				int num2 = this.wScroll - 1;
				int h = this.ITEM_HEIGHT - 1;
				if (num - this.cmy <= this.yScroll + this.hScroll && num - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, Panel.strAccount[i], this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
				}
			}
			this.paintScrollArrow(g);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x002CD818 File Offset: 0x002CBA18
		private void doFireAccount()
		{
			if (this.selected < 0)
			{
				return;
			}
			switch (this.selected)
			{
				case 0:
					GameCanvas.endDlg();
					if (this.chatTField == null)
					{
						this.chatTField = new ChatTextField();
						this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
						this.chatTField.initChatTextField();
						this.chatTField.parentScreen = GameCanvas.panel;
					}
					this.chatTField.tfChat.setText(string.Empty);
					this.chatTField.strChat = mResources.input_Inventory_Pass;
					this.chatTField.tfChat.name = mResources.input_Inventory_Pass;
					this.chatTField.to = string.Empty;
					this.chatTField.isShow = true;
					this.chatTField.tfChat.isFocus = true;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
					if (GameCanvas.isTouch)
					{
						this.chatTField.tfChat.doChangeToTextBox();
					}
					if (!Main.isPC)
					{
						this.chatTField.startChat(this, string.Empty);
					}
					if (Main.isWindowsPhone)
					{
						this.chatTField.tfChat.strInfo = this.chatTField.strChat;
						return;
					}
					break;
				case 1:
					Service.gI().friend(0, -1);
					InfoDlg.showWait();
					return;
				case 2:
					Service.gI().enemy(0, -1);
					InfoDlg.showWait();
					return;
				case 3:
					this.setTypeMessage();
					if (this.chatTField == null)
					{
						this.chatTField = new ChatTextField();
						this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
						this.chatTField.initChatTextField();
						this.chatTField.parentScreen = GameCanvas.panel;
						return;
					}
					break;
				case 4:
					if (mResources.language == 2)
					{
						string url = "http://dragonball.indonaga.com/coda/?username=" + GameCanvas.loginScr.tfUser.getText();
						this.hideNow();
						try
						{
							GameMidlet.instance.platformRequest(url);
							break;
						}
						catch (Exception ex)
						{
							ex.StackTrace.ToString();
							break;
						}
					}
					this.hideNow();
					if (Char.myCharz().taskMaint.taskId <= 10)
					{
						GameCanvas.startOKDlg(mResources.finishBomong);
						return;
					}
					MoneyCharge.gI().switchToMe();
					return;
				case 5:
					this.setTypeAuto();
					break;
				default:
					return;
			}
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x002B58A6 File Offset: 0x002B3AA6
		private void updateKeyOption()
		{
			this.updateKeyScrollView();
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x002CDA8C File Offset: 0x002CBC8C
		public void setTypeSpeacialSkill()
		{
			this.type = 25;
			this.setType(0);
			this.setTabSpeacialSkill();
			this.currentTabIndex = 0;
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x002CDAAC File Offset: 0x002CBCAC
		private void setTabSpeacialSkill()
		{
			this.ITEM_HEIGHT = 24;
			this.currentListLength = Char.myCharz().infoSpeacialSkill[this.currentTabIndex].Length;
			this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
			if (this.cmyLim < 0)
			{
				this.cmyLim = 0;
			}
			this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
			if (this.cmy < 0)
			{
				this.cmy = (this.cmtoY = 0);
			}
			if (this.cmy > this.cmyLim)
			{
				this.cmy = (this.cmtoY = this.cmyLim);
			}
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x002CDB6D File Offset: 0x002CBD6D
		public bool isTypeShop()
		{
			return this.type == 1;
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x002CDB7C File Offset: 0x002CBD7C
		private void doNotiRuby(int type)
		{
			try
			{
				this.currItem.buyRuby = int.Parse(this.chatTField.tfChat.getText());
			}
			catch (Exception)
			{
				GameCanvas.startOKDlg(mResources.input_money_wrong);
				this.chatTField.isShow = false;
				return;
			}
			Command cmdYes = new Command(mResources.YES, this, (type != 0) ? 11001 : 11000, null);
			Command cmdNo = new Command(mResources.NO, this, 11002, null);
			GameCanvas.startYesNoDlg(mResources.notiRuby, cmdYes, cmdNo);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x002CDC10 File Offset: 0x002CBE10
		public static void paintUpgradeEffect(int x, int y, int wItem, int hItem, int nline, int cl, mGraphics g)
		{
			try
			{
				int num2 = ((wItem << 1) + (hItem << 1)) / nline;
				Panel.nsize = Panel.sizeUpgradeEff.Length;
				if (nline > 4)
				{
					Panel.nsize = 2;
				}
				for (int i = 0; i < nline; i++)
				{
					for (int j = 0; j < Panel.nsize; j++)
					{
						int wSize = (Panel.sizeUpgradeEff[j] <= 1) ? 1 : ((Panel.sizeUpgradeEff[j] >> 1) + 1);
						int x2 = x + Panel.upgradeEffectX(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
						int y2 = y + Panel.upgradeEffectY(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
						g.setColor(Panel.colorUpgradeEffect[cl][j]);
						g.fillRect(x2, y2, Panel.sizeUpgradeEff[j], Panel.sizeUpgradeEff[j]);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x002CDCEC File Offset: 0x002CBEEC
		private static int upgradeEffectX(int dk, int tick, int wItem, int hitem, int wSize)
		{
			int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
			if (0 <= num && num < wItem)
			{
				return num % wItem;
			}
			if (wItem <= num && num < wItem + hitem)
			{
				return wItem - wSize;
			}
			if (wItem + hitem <= num && num < (wItem << 1) + hitem)
			{
				return wItem - (num - hitem) % wItem - wSize;
			}
			return 0;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x002CDD3C File Offset: 0x002CBF3C
		private static int upgradeEffectY(int dk, int tick, int wItem, int hitem, int wSize)
		{
			int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
			if (0 <= num && num < wItem)
			{
				return 0;
			}
			if (wItem <= num && num < wItem + hitem)
			{
				return num % wItem;
			}
			if (wItem + hitem <= num && num < (wItem << 1) + hitem)
			{
				return hitem - wSize;
			}
			return hitem - (num - (wItem << 1)) % hitem - wSize;
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x002CDD8C File Offset: 0x002CBF8C
		public static int GetColor_ItemBg(int id)
		{
			int result;
			switch (id)
			{
				case 1:
					result = 2786816;
					break;
				case 2:
					result = 7078041;
					break;
				case 3:
					result = 12537346;
					break;
				case 4:
					result = 1269146;
					break;
				case 5:
					result = 13279744;
					break;
				case 6:
					result = 11599872;
					break;
				default:
					result = -1;
					break;
			}
			return result;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x002CDDF0 File Offset: 0x002CBFF0
		public static sbyte GetColor_Item_Upgrade(int lv)
		{
			if (lv < 0)
			{
				return 0;
			}
			switch (lv)
			{
				case 0:
				case 1:
					return 4;
				case 2:
				case 3:
					return 1;
				case 4:
				case 5:
					return 2;
				case 6:
				case 7:
					return 3;
				case 8:
					return 5;
				case 9:
					return 6;
				case 10:
					return 0;
				default:
					return 0;
			}
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x002CDE48 File Offset: 0x002CC048
		public static mFont GetFont(int color)
		{
			mFont result = mFont.tahoma_7;
			switch (color)
			{
				case -1:
					result = mFont.tahoma_7;
					break;
				case 0:
					result = mFont.tahoma_7b_dark;
					break;
				case 1:
					result = mFont.tahoma_7b_green;
					break;
				case 2:
					result = mFont.tahoma_7b_blue;
					break;
				case 3:
					result = mFont.tahoma_7b_blue;
					break;
				case 4:
					result = mFont.tahoma_7b_blue;
					break;
				case 5:
					result = mFont.tahoma_7b_blue;
					break;
				case 7:
					result = mFont.tahoma_7b_red;
					break;
				case 8:
					result = mFont.tahoma_7b_yellow;
					break;
			}
			return result;
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x002CDED4 File Offset: 0x002CC0D4
		public void paintOptItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
		{
			switch (idOpt)
			{
				case 34:
					if (this.imgo_1 != null)
					{
						g.drawImage(this.imgo_1, x, y + h - this.imgo_1.getHeight());
						return;
					}
					this.imgo_1 = mSystem.loadImage("/mainImage/o_1.png");
					return;
				case 35:
					if (this.imgo_2 != null)
					{
						g.drawImage(this.imgo_2, x, y + h - this.imgo_2.getHeight());
						return;
					}
					this.imgo_2 = mSystem.loadImage("/mainImage/o_2.png");
					return;
				case 36:
					if (this.imgo_3 != null)
					{
						g.drawImage(this.imgo_3, x, y + h - this.imgo_3.getHeight());
						return;
					}
					this.imgo_3 = mSystem.loadImage("/mainImage/o_3.png");
					return;
				default:
					return;
			}
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x002CDFA4 File Offset: 0x002CC1A4
		public void paintOptItemInventory(mGraphics g, int idOpt, int param, int x, int y, int w, int h, Item item)
		{
			try
			{
				switch (idOpt)
				{
					case 34:
						if (this.imgo_1 != null)
						{
							g.drawImage(this.imgo_1, x, y + h - this.imgo_1.getHeight());
						}
						else
						{
							this.imgo_1 = mSystem.loadImage("/mainImage/o_1.png");
						}
						break;
					case 35:
						if (this.imgo_2 != null)
						{
							g.drawImage(this.imgo_2, x, y + h - this.imgo_2.getHeight());
						}
						else
						{
							this.imgo_2 = mSystem.loadImage("/mainImage/o_2.png");
						}
						break;
					case 36:
						if (this.imgo_3 != null)
						{
							g.drawImage(this.imgo_3, x, y + h - this.imgo_3.getHeight());
						}
						else
						{
							this.imgo_3 = mSystem.loadImage("/mainImage/o_3.png");
						}
						break;
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x002CE098 File Offset: 0x002CC298
		public void paintOptSlotItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
		{
			if (idOpt == 102 && param > ChatPopup.numSlot)
			{
				sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(param);
				int nline = param - ChatPopup.numSlot;
				Panel.paintUpgradeEffect(x, y, w, h, nline, (int)color_Item_Upgrade, g);
			}
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x002CE0D1 File Offset: 0x002CC2D1
		private void loadTabModFunc()
		{
			if (this.type == 26)
			{
				SoundMn.gI().GetStrModFunc();
				this.selected = 0;
			}
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x002CE0F0 File Offset: 0x002CC2F0
		public static mFont setTextColor(int id, int type)
		{
			if (type == 0)
			{
				switch (id)
				{
					case 0:
						return mFont.bigNumber_While;
					case 1:
						return mFont.bigNumber_green;
					case 3:
						return mFont.bigNumber_orange;
					case 4:
						return mFont.bigNumber_blue;
					case 5:
						return mFont.bigNumber_yellow;
					case 6:
						return mFont.bigNumber_red;
				}
				return mFont.bigNumber_While;
			}
			switch (id)
			{
				case 0:
					return mFont.tahoma_7b_white;
				case 1:
					return mFont.tahoma_7b_green;
				case 3:
					return mFont.tahoma_7b_yellowSmall2;
				case 4:
					return mFont.tahoma_7b_blue;
				case 5:
					return mFont.tahoma_7b_yellow;
				case 6:
					return mFont.tahoma_7b_red;
				case 7:
					return mFont.tahoma_7b_dark;
			}
			return mFont.tahoma_7b_white;
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x002CE1C4 File Offset: 0x002CC3C4
		private bool GetInventorySelect_isbody(int select, int subSelect, Item[] arrItem)
		{
			int num = select - ((!ModFunc.isInventory) ? 1 : 0) + subSelect * 20;
			return subSelect == 0 && num < arrItem.Length;
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x002CE1EF File Offset: 0x002CC3EF
		private int GetInventorySelect_body(int select, int subSelect)
		{
			return select - ((!ModFunc.isInventory) ? 1 : 0) + subSelect * 20;
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x002CE203 File Offset: 0x002CC403
		private int GetInventorySelect_bag(int select, int subSelect, Item[] arrItem)
		{
			return select - ((!ModFunc.isInventory) ? 1 : 0) + subSelect * 20 - arrItem.Length;
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x002CE21B File Offset: 0x002CC41B
		private bool isTabBox()
		{
			return this.type == 2 && this.currentTabIndex == 0;
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x002CE231 File Offset: 0x002CC431
		private bool isTabInven()
		{
			return (this.type == 0 && this.currentTabIndex == 1) || (this.type == 7 && this.currentTabIndex == 0) || (this.type == 13 && this.currentTabIndex == 0);
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x002CE26C File Offset: 0x002CC46C
		private void updateKeyInvenTab()
		{
			if (this.selected < 0)
			{
				return;
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				this.newSelected--;
				if (this.isnewInventory)
				{
					this.currentListLength = 5;
				}
				if (this.newSelected < 0)
				{
					this.newSelected = 0;
					if (GameCanvas.isFocusPanel2)
					{
						GameCanvas.isFocusPanel2 = false;
						GameCanvas.panel.selected = 0;
					}
				}
				if (this.type == 26)
				{
					this.currentTabIndex = this.newSelected;
					this.loadTabModFunc();
					return;
				}
			}
			else
			{
				if (!GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
				{
					return;
				}
				this.newSelected++;
				if (this.isnewInventory)
				{
					this.currentListLength = 5;
				}
				if (this.newSelected > (int)(this.size_tab - 1))
				{
					this.newSelected = (int)(this.size_tab - 1);
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.isFocusPanel2 = true;
						GameCanvas.panel2.selected = 0;
					}
				}
				if (this.type == 26)
				{
					this.currentTabIndex = this.newSelected;
					this.loadTabModFunc();
				}
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x002CE380 File Offset: 0x002CC580
		private void updateKeyInventory()
		{
			this.updateKeyScrollView();
			if (this.selected == 0)
			{
				this.updateKeyInvenTab();
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x002CE396 File Offset: 0x002CC596
		private bool IsTabOption()
		{
			if (this.size_tab > 0)
			{
				if (this.currentTabName.Length > 1)
				{
					if (this.selected == 0)
					{
						return true;
					}
				}
				else if (this.selected >= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x002CE3C4 File Offset: 0x002CC5C4
		private int checkCurrentListLength(int arrLength)
		{
			if (!ModFunc.isInventory)
			{
				int num = 20;
				int num2 = arrLength / 20 + ((arrLength % 20 > 0) ? 1 : 0);
				this.size_tab = (sbyte)num2;
				if (this.newSelected > num2 - 1)
				{
					this.newSelected = num2 - 1;
				}
				if (arrLength % 20 > 0 && this.newSelected == num2 - 1)
				{
					num = arrLength % 20;
				}
				return num + 1;
			}
			return arrLength + 1;
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x0007A0DE File Offset: 0x000782DE
		private int checkCurrentListLengthNew(int arrLength)
		{
			return arrLength + 1;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x002CE428 File Offset: 0x002CC628
		private void setNewSelected(int arrLength, bool resetSelect)
		{
			int num = arrLength / 20 + ((arrLength % 20 > 0) ? 1 : 0);
			int num2 = this.xScroll;
			this.newSelected = (GameCanvas.px - num2) / this.TAB_W_NEW;
			if (this.newSelected > num - 1)
			{
				this.newSelected = num - 1;
			}
			if (GameCanvas.px < num2)
			{
				this.newSelected = 0;
			}
			this.setTabInventory(resetSelect);
		}

		// Token: 0x04005739 RID: 22329
		public bool isShow;

		// Token: 0x0400573A RID: 22330
		public int X;

		// Token: 0x0400573B RID: 22331
		public int Y;

		// Token: 0x0400573C RID: 22332
		public int W;

		// Token: 0x0400573D RID: 22333
		public int H;

		// Token: 0x0400573E RID: 22334
		public int ITEM_HEIGHT;

		// Token: 0x0400573F RID: 22335
		public int TAB_W;

		// Token: 0x04005740 RID: 22336
		public int TAB_W_NEW;

		// Token: 0x04005741 RID: 22337
		public int cmtoY;

		// Token: 0x04005742 RID: 22338
		public int cmy;

		// Token: 0x04005743 RID: 22339
		public int cmdy;

		// Token: 0x04005744 RID: 22340
		public int cmvy;

		// Token: 0x04005745 RID: 22341
		public int cmyLim;

		// Token: 0x04005746 RID: 22342
		public int xc;

		// Token: 0x04005747 RID: 22343
		public int[] cmyLast;

		// Token: 0x04005748 RID: 22344
		public int cmtoX;

		// Token: 0x04005749 RID: 22345
		public int cmx;

		// Token: 0x0400574A RID: 22346
		public int cmxLim;

		// Token: 0x0400574B RID: 22347
		public int cmxMap;

		// Token: 0x0400574C RID: 22348
		public int cmyMap;

		// Token: 0x0400574D RID: 22349
		public int cmxMapLim;

		// Token: 0x0400574E RID: 22350
		public int cmyMapLim;

		// Token: 0x0400574F RID: 22351
		public int cmyQuest;

		// Token: 0x04005750 RID: 22352
		public static Image imgBantay;

		// Token: 0x04005751 RID: 22353
		public static Image imgX;

		// Token: 0x04005752 RID: 22354
		public static Image imgMap;

		// Token: 0x04005753 RID: 22355
		public TabClanIcon tabIcon;

		// Token: 0x04005754 RID: 22356
		public MyVector vItemCombine = new MyVector();

		// Token: 0x04005755 RID: 22357
		public int moneyGD;

		// Token: 0x04005756 RID: 22358
		public int friendMoneyGD;

		// Token: 0x04005757 RID: 22359
		public bool isLock;

		// Token: 0x04005758 RID: 22360
		public bool isFriendLock;

		// Token: 0x04005759 RID: 22361
		public bool isAccept;

		// Token: 0x0400575A RID: 22362
		public bool isFriendAccep;

		// Token: 0x0400575B RID: 22363
		public string topName;

		// Token: 0x0400575C RID: 22364
		public ChatTextField chatTField;

		// Token: 0x0400575D RID: 22365
		public static string specialInfo;

		// Token: 0x0400575E RID: 22366
		public static short spearcialImage;

		// Token: 0x0400575F RID: 22367
		public static Image imgStar;

		// Token: 0x04005760 RID: 22368
		public static Image imgMaxStar;

		// Token: 0x04005761 RID: 22369
		public static Image imgStar8;

		// Token: 0x04005762 RID: 22370
		public static Image imgStar10;

		// Token: 0x04005763 RID: 22371
		public static Image imgNew;

		// Token: 0x04005764 RID: 22372
		public static Image imgXu;

		// Token: 0x04005765 RID: 22373
		public static Image imgThoivang;

		// Token: 0x04005766 RID: 22374
		public static Image imgTicket;

		// Token: 0x04005767 RID: 22375
		public static Image imgLuong;

		// Token: 0x04005768 RID: 22376
		public static Image imgLuongKhoa;

		// Token: 0x04005769 RID: 22377
		private static Image imgUp;

		// Token: 0x0400576A RID: 22378
		private static Image imgDown;

		// Token: 0x0400576B RID: 22379
		private int pa1;

		// Token: 0x0400576C RID: 22380
		private int pa2;

		// Token: 0x0400576D RID: 22381
		private bool trans;

		// Token: 0x0400576E RID: 22382
		private int pX;

		// Token: 0x0400576F RID: 22383
		private int pY;

		// Token: 0x04005770 RID: 22384
		private Command left = new Command(mResources.SELECT, 0);

		// Token: 0x04005771 RID: 22385
		public int type;

		// Token: 0x04005772 RID: 22386
		public int currentTabIndex;

		// Token: 0x04005773 RID: 22387
		public int startTabPos;

		// Token: 0x04005774 RID: 22388
		public int[] lastTabIndex;

		// Token: 0x04005775 RID: 22389
		public string[][] currentTabName;

		// Token: 0x04005776 RID: 22390
		private int[] currClanOption;

		// Token: 0x04005777 RID: 22391
		public int mainTabPos = 4;

		// Token: 0x04005778 RID: 22392
		public int shopTabPos = 50;

		// Token: 0x04005779 RID: 22393
		public int boxTabPos = 50;

		// Token: 0x0400577A RID: 22394
		public string[][] mainTabName;

		// Token: 0x0400577B RID: 22395
		public string[] mapNames;

		// Token: 0x0400577C RID: 22396
		public string[] planetNames;

		// Token: 0x0400577D RID: 22397
		public static string[] strTool = new string[]
		{
			mResources.gameInfo,
			mResources.change_flag,
			mResources.change_zone,
			mResources.chat_world,
			mResources.account,
			mResources.option,
			mResources.change_account
		};

		// Token: 0x0400577E RID: 22398
		public static string[] strCauhinh = new string[]
		{
			(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
			mResources.increase_vga,
			mResources.analog,
			(mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen
		};

		// Token: 0x0400577F RID: 22399
		public static string[] strModFunc = new string[]
		{
			""
		};

		// Token: 0x04005780 RID: 22400
		public static string[] strAccount = new string[]
		{
			mResources.inventory_Pass,
			mResources.friend,
			mResources.enemy,
			mResources.msg,
			mResources.charger
		};

		// Token: 0x04005781 RID: 22401
		public static string[] strAuto = new string[]
		{
			mResources.useGem
		};

		// Token: 0x04005782 RID: 22402
		public static int graphics = 0;

		// Token: 0x04005783 RID: 22403
		public string[][] shopTabName;

		// Token: 0x04005784 RID: 22404
		public int[] maxPageShop;

		// Token: 0x04005785 RID: 22405
		public int[] currPageShop;

		// Token: 0x04005786 RID: 22406
		private static string[][] boxTabName = new string[][]
		{
			mResources.chestt,
			mResources.inventory
		};

		// Token: 0x04005787 RID: 22407
		private static string[][] boxCombine = new string[][]
		{
			mResources.combine,
			mResources.inventory
		};

		// Token: 0x04005788 RID: 22408
		private static string[][] boxZone = new string[][]
		{
			mResources.zonee
		};

		// Token: 0x04005789 RID: 22409
		private static string[][] boxMap = new string[][]
		{
			mResources.mapp
		};

		// Token: 0x0400578A RID: 22410
		private static string[][] boxGD = new string[][]
		{
			mResources.inventory,
			mResources.item_give,
			mResources.item_receive
		};

		// Token: 0x0400578B RID: 22411
		private static string[][] boxPet = mResources.petMainTab;

		// Token: 0x0400578C RID: 22412
		public string[][][] tabName = new string[][][]
		{
			null,
			null,
			Panel.boxTabName,
			Panel.boxZone,
			Panel.boxMap,
			null,
			null,
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			Panel.boxCombine,
			Panel.boxGD,
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			Panel.boxPet,
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			Panel.boxMod,
			new string[][]
			{
				new string[]
				{
					string.Empty
				}
			},
			Panel.boxPet
		};

		// Token: 0x0400578D RID: 22413
		private static readonly string[][] boxMod = new string[][]
		{
			new string[]
			{
				"Chức",
				"năng"
			},
			new string[]
			{
				"Tự",
				"động"
			},
			new string[]
			{
				"Hiển",
				"thị"
			},
			new string[]
			{
				"Cài",
				"đặt"
			}
		};

		// Token: 0x0400578E RID: 22414
		private static readonly sbyte BOX_BAG = 0;

		// Token: 0x0400578F RID: 22415
		private static readonly sbyte BAG_BOX = 1;

		// Token: 0x04005790 RID: 22416
		private static readonly sbyte BODY_BOX = 3;

		// Token: 0x04005791 RID: 22417
		private static readonly sbyte BAG_BODY = 4;

		// Token: 0x04005792 RID: 22418
		private static readonly sbyte BODY_BAG = 5;

		// Token: 0x04005793 RID: 22419
		private static readonly sbyte BAG_PET = 6;

		// Token: 0x04005794 RID: 22420
		private static readonly sbyte PET_BAG = 7;

		// Token: 0x04005795 RID: 22421
		private static readonly sbyte BAG_PET2 = 8;

		// Token: 0x04005796 RID: 22422
		private static readonly sbyte PET2_BAG = 9;

		// Token: 0x04005797 RID: 22423
		public int hasUse;

		// Token: 0x04005798 RID: 22424
		public int hasUseBag;

		// Token: 0x04005799 RID: 22425
		public int currentListLength;

		// Token: 0x0400579A RID: 22426
		private int[] lastSelect;

		// Token: 0x0400579B RID: 22427
		public static int[] mapIdTraidat = new int[]
		{
			21,
			0,
			1,
			2,
			24,
			3,
			4,
			5,
			6,
			27,
			28,
			29,
			30,
			42,
			47,
			46
		};

		// Token: 0x0400579C RID: 22428
		public static int[] mapXTraidat = new int[]
		{
			39,
			42,
			105,
			93,
			61,
			93,
			142,
			165,
			210,
			100,
			165,
			220,
			233,
			10,
			125,
			125
		};

		// Token: 0x0400579D RID: 22429
		public static int[] mapYTraidat = new int[]
		{
			28,
			60,
			48,
			96,
			88,
			131,
			136,
			95,
			32,
			200,
			189,
			167,
			120,
			110,
			20,
			20
		};

		// Token: 0x0400579E RID: 22430
		public static int[] mapIdNamek = new int[]
		{
			22,
			7,
			8,
			9,
			25,
			11,
			12,
			13,
			10,
			31,
			32,
			33,
			34,
			43
		};

		// Token: 0x0400579F RID: 22431
		public static int[] mapXNamek = new int[]
		{
			55,
			30,
			93,
			80,
			24,
			149,
			219,
			220,
			233,
			170,
			148,
			195,
			148,
			10
		};

		// Token: 0x040057A0 RID: 22432
		public static int[] mapYNamek = new int[]
		{
			136,
			84,
			69,
			34,
			25,
			42,
			32,
			110,
			192,
			70,
			106,
			156,
			210,
			57
		};

		// Token: 0x040057A1 RID: 22433
		public static int[] mapIdSaya = new int[]
		{
			23,
			14,
			15,
			16,
			26,
			17,
			18,
			20,
			19,
			35,
			36,
			37,
			38,
			44
		};

		// Token: 0x040057A2 RID: 22434
		public static int[] mapXSaya = new int[]
		{
			90,
			95,
			144,
			234,
			231,
			122,
			176,
			158,
			205,
			54,
			105,
			159,
			231,
			27
		};

		// Token: 0x040057A3 RID: 22435
		public static int[] mapYSaya = new int[]
		{
			10,
			43,
			20,
			36,
			69,
			87,
			112,
			167,
			160,
			151,
			173,
			207,
			194,
			29
		};

		// Token: 0x040057A4 RID: 22436
		public static int[][] mapId = new int[][]
		{
			Panel.mapIdTraidat,
			Panel.mapIdNamek,
			Panel.mapIdSaya
		};

		// Token: 0x040057A5 RID: 22437
		public static int[][] mapX = new int[][]
		{
			Panel.mapXTraidat,
			Panel.mapXNamek,
			Panel.mapXSaya
		};

		// Token: 0x040057A6 RID: 22438
		public static int[][] mapY = new int[][]
		{
			Panel.mapYTraidat,
			Panel.mapYNamek,
			Panel.mapYSaya
		};

		// Token: 0x040057A7 RID: 22439
		public Item currItem;

		// Token: 0x040057A8 RID: 22440
		public Clan currClan;

		// Token: 0x040057A9 RID: 22441
		public ClanMessage currMess;

		// Token: 0x040057AA RID: 22442
		public Member currMem;

		// Token: 0x040057AB RID: 22443
		public Clan[] clans;

		// Token: 0x040057AC RID: 22444
		public MyVector member;

		// Token: 0x040057AD RID: 22445
		public MyVector myMember;

		// Token: 0x040057AE RID: 22446
		public MyVector logChat = new MyVector();

		// Token: 0x040057AF RID: 22447
		public MyVector vPlayerMenu = new MyVector();

		// Token: 0x040057B0 RID: 22448
		public MyVector vFriend = new MyVector();

		// Token: 0x040057B1 RID: 22449
		public MyVector vMyGD = new MyVector();

		// Token: 0x040057B2 RID: 22450
		public MyVector vFriendGD = new MyVector();

		// Token: 0x040057B3 RID: 22451
		public MyVector vTop = new MyVector();

		// Token: 0x040057B4 RID: 22452
		public MyVector vEnemy = new MyVector();

		// Token: 0x040057B5 RID: 22453
		public MyVector vFlag = new MyVector();

		// Token: 0x040057B6 RID: 22454
		public MyVector vPlayerMenu_id = new MyVector();

		// Token: 0x040057B7 RID: 22455
		public Command cmdClose;

		// Token: 0x040057B8 RID: 22456
		public static bool CanNapTien = false;

		// Token: 0x040057B9 RID: 22457
		public static int WIDTH_PANEL = 245;

		// Token: 0x040057BA RID: 22458
		private int position;

		// Token: 0x040057BB RID: 22459
		public string playerChat;

		// Token: 0x040057BC RID: 22460
		public Dictionary<string, Panel.PlayerChat> chats = new Dictionary<string, Panel.PlayerChat>();

		// Token: 0x040057BD RID: 22461
		public Char charMenu;

		// Token: 0x040057BE RID: 22462
		private bool isThachDau;

		// Token: 0x040057BF RID: 22463
		public int typeShop = -1;

		// Token: 0x040057C0 RID: 22464
		public int xScroll;

		// Token: 0x040057C1 RID: 22465
		public int yScroll;

		// Token: 0x040057C2 RID: 22466
		public int wScroll;

		// Token: 0x040057C3 RID: 22467
		public int hScroll;

		// Token: 0x040057C4 RID: 22468
		public ChatPopup cp;

		// Token: 0x040057C5 RID: 22469
		public int idIcon;

		// Token: 0x040057C6 RID: 22470
		public int[] partID;

		// Token: 0x040057C7 RID: 22471
		private int timeShow;

		// Token: 0x040057C8 RID: 22472
		public bool isBoxClan;

		// Token: 0x040057C9 RID: 22473
		public int w;

		// Token: 0x040057CA RID: 22474
		private int pa;

		// Token: 0x040057CB RID: 22475
		public int selected;

		// Token: 0x040057CC RID: 22476
		private int cSelected;

		// Token: 0x040057CD RID: 22477
		private int newSelected;

		// Token: 0x040057CE RID: 22478
		private bool isClanOption;

		// Token: 0x040057CF RID: 22479
		public bool isSearchClan;

		// Token: 0x040057D0 RID: 22480
		public bool isMessage;

		// Token: 0x040057D1 RID: 22481
		public bool isViewMember;

		// Token: 0x040057D2 RID: 22482
		public const int TYPE_MAIN = 0;

		// Token: 0x040057D3 RID: 22483
		public const int TYPE_SHOP = 1;

		// Token: 0x040057D4 RID: 22484
		public const int TYPE_BOX = 2;

		// Token: 0x040057D5 RID: 22485
		public const int TYPE_ZONE = 3;

		// Token: 0x040057D6 RID: 22486
		public const int TYPE_MAP = 4;

		// Token: 0x040057D7 RID: 22487
		public const int TYPE_CLANS = 5;

		// Token: 0x040057D8 RID: 22488
		public const int TYPE_INFOMATION = 6;

		// Token: 0x040057D9 RID: 22489
		public const int TYPE_BODY = 7;

		// Token: 0x040057DA RID: 22490
		public const int TYPE_MESS = 8;

		// Token: 0x040057DB RID: 22491
		public const int TYPE_ARCHIVEMENT = 9;

		// Token: 0x040057DC RID: 22492
		public const int PLAYER_MENU = 10;

		// Token: 0x040057DD RID: 22493
		public const int TYPE_FRIEND = 11;

		// Token: 0x040057DE RID: 22494
		public const int TYPE_COMBINE = 12;

		// Token: 0x040057DF RID: 22495
		public const int TYPE_GIAODICH = 13;

		// Token: 0x040057E0 RID: 22496
		public const int TYPE_MAPTRANS = 14;

		// Token: 0x040057E1 RID: 22497
		public const int TYPE_TOP = 15;

		// Token: 0x040057E2 RID: 22498
		public const int TYPE_ENEMY = 16;

		// Token: 0x040057E3 RID: 22499
		public const int TYPE_KIGUI = 17;

		// Token: 0x040057E4 RID: 22500
		public const int TYPE_FLAG = 18;

		// Token: 0x040057E5 RID: 22501
		public const int TYPE_OPTION = 19;

		// Token: 0x040057E6 RID: 22502
		public const int TYPE_ACCOUNT = 20;

		// Token: 0x040057E7 RID: 22503
		public const int TYPE_PET_MAIN = 21;

		// Token: 0x040057E8 RID: 22504
		public const int TYPE_AUTO = 22;

		// Token: 0x040057E9 RID: 22505
		public const int TYPE_GAMEINFO = 23;

		// Token: 0x040057EA RID: 22506
		public const int TYPE_GAMEINFOSUB = 24;

		// Token: 0x040057EB RID: 22507
		public const int TYPE_SPEACIALSKILL = 25;

		// Token: 0x040057EC RID: 22508
		private int pointerDownTime;

		// Token: 0x040057ED RID: 22509
		private int pointerDownFirstX;

		// Token: 0x040057EE RID: 22510
		private int[] pointerDownLastX = new int[3];

		// Token: 0x040057EF RID: 22511
		private bool pointerIsDowning;

		// Token: 0x040057F0 RID: 22512
		private bool isDownWhenRunning;

		// Token: 0x040057F1 RID: 22513
		private bool wantUpdateList;

		// Token: 0x040057F2 RID: 22514
		private int waitToPerform;

		// Token: 0x040057F3 RID: 22515
		private int cmRun;

		// Token: 0x040057F4 RID: 22516
		private int keyTouchLock = -1;

		// Token: 0x040057F5 RID: 22517
		private int keyToundGD = -1;

		// Token: 0x040057F6 RID: 22518
		private int keyTouchCombine = -1;

		// Token: 0x040057F7 RID: 22519
		private int keyTouchMapButton = -1;

		// Token: 0x040057F8 RID: 22520
		public int indexMouse = -1;

		// Token: 0x040057F9 RID: 22521
		private bool justRelease;

		// Token: 0x040057FA RID: 22522
		private int keyTouchTab = -1;

		// Token: 0x040057FB RID: 22523
		private int nTableItem;

		// Token: 0x040057FC RID: 22524
		public string[][] clansOption = new string[][]
		{
			mResources.findClan,
			mResources.createClan
		};

		// Token: 0x040057FD RID: 22525
		public string clanInfo = string.Empty;

		// Token: 0x040057FE RID: 22526
		public string clanReport = string.Empty;

		// Token: 0x040057FF RID: 22527
		private bool isHaveClan;

		// Token: 0x04005800 RID: 22528
		private Scroll scroll;

		// Token: 0x04005801 RID: 22529
		private int cmvx;

		// Token: 0x04005802 RID: 22530
		private int cmdx;

		// Token: 0x04005803 RID: 22531
		private bool isSelectPlayerMenu;

		// Token: 0x04005804 RID: 22532
		private string[] strStatus = new string[]
		{
			mResources.follow,
			mResources.defend,
			mResources.attack,
			mResources.gohome,
			mResources.fusion,
			mResources.fusionForever
		};

		// Token: 0x04005805 RID: 22533
		private static string log;

		// Token: 0x04005806 RID: 22534
		private int tt;

		// Token: 0x04005807 RID: 22535
		private int currentButtonPress;

		// Token: 0x04005808 RID: 22536
		public static long[] t_tiemnang = new long[]
		{
			50000000L,
			250000000L,
			1250000000L,
			5000000000L,
			15000000000L,
			30000000000L,
			45000000000L,
			60000000000L,
			75000000000L,
			90000000000L,
			110000000000L,
			130000000000L,
			150000000000L,
			170000000000L
		};

		// Token: 0x04005809 RID: 22537
		private int[] zoneColor = new int[]
		{
			43520,
			14743570,
			14155776
		};

		// Token: 0x0400580A RID: 22538
		public string[] combineInfo;

		// Token: 0x0400580B RID: 22539
		public string[] combineTopInfo;

		// Token: 0x0400580C RID: 22540
		public static int[] color1 = new int[]
		{
			2327248,
			8982199,
			16713222
		};

		// Token: 0x0400580D RID: 22541
		public static int[] color2 = new int[]
		{
			4583423,
			16719103,
			16714764
		};

		// Token: 0x0400580E RID: 22542
		private int sellectInventory;

		// Token: 0x0400580F RID: 22543
		private Item itemInvenNew;

		// Token: 0x04005810 RID: 22544
		private Effect eBanner;

		// Token: 0x04005811 RID: 22545
		private static FrameImage screenTab6;

		// Token: 0x04005812 RID: 22546
		private bool isUp;

		// Token: 0x04005813 RID: 22547
		private int compare;

		// Token: 0x04005814 RID: 22548
		public static string strWantToBuy = string.Empty;

		// Token: 0x04005815 RID: 22549
		public int xstart;

		// Token: 0x04005816 RID: 22550
		public int ystart;

		// Token: 0x04005817 RID: 22551
		public int popupW = 140;

		// Token: 0x04005818 RID: 22552
		public int popupH = 160;

		// Token: 0x04005819 RID: 22553
		public int cmySK;

		// Token: 0x0400581A RID: 22554
		public int cmtoYSK;

		// Token: 0x0400581B RID: 22555
		public int cmdySK;

		// Token: 0x0400581C RID: 22556
		public int cmvySK;

		// Token: 0x0400581D RID: 22557
		public int cmyLimSK;

		// Token: 0x0400581E RID: 22558
		public int popupY;

		// Token: 0x0400581F RID: 22559
		public int popupX;

		// Token: 0x04005820 RID: 22560
		public int isborderIndex;

		// Token: 0x04005821 RID: 22561
		public int isselectedRow;

		// Token: 0x04005822 RID: 22562
		public int indexSize = 28;

		// Token: 0x04005823 RID: 22563
		public int indexTitle;

		// Token: 0x04005824 RID: 22564
		public int indexSelect;

		// Token: 0x04005825 RID: 22565
		public int indexRow = -1;

		// Token: 0x04005826 RID: 22566
		public int indexRowMax;

		// Token: 0x04005827 RID: 22567
		public int indexMenu;

		// Token: 0x04005828 RID: 22568
		public int columns = 6;

		// Token: 0x04005829 RID: 22569
		public int rows;

		// Token: 0x0400582A RID: 22570
		public int inforX;

		// Token: 0x0400582B RID: 22571
		public int inforY;

		// Token: 0x0400582C RID: 22572
		public int inforW;

		// Token: 0x0400582D RID: 22573
		public int inforH;

		// Token: 0x0400582E RID: 22574
		private int yPaint;

		// Token: 0x0400582F RID: 22575
		private int xMap;

		// Token: 0x04005830 RID: 22576
		private int yMap;

		// Token: 0x04005831 RID: 22577
		private int xMapTask;

		// Token: 0x04005832 RID: 22578
		private int yMapTask;

		// Token: 0x04005833 RID: 22579
		private int xMove;

		// Token: 0x04005834 RID: 22580
		private int yMove;

		// Token: 0x04005835 RID: 22581
		public static bool isPaintMap = true;

		// Token: 0x04005836 RID: 22582
		public bool isClose;

		// Token: 0x04005837 RID: 22583
		private int infoSelect;

		// Token: 0x04005838 RID: 22584
		public static MyVector vGameInfo = new MyVector(string.Empty);

		// Token: 0x04005839 RID: 22585
		public static string[] contenInfo;

		// Token: 0x0400583A RID: 22586
		public bool isViewChatServer;

		// Token: 0x0400583B RID: 22587
		private int currInfoItem;

		// Token: 0x0400583C RID: 22588
		public Char charInfo;

		// Token: 0x0400583D RID: 22589
		private bool isChangeZone;

		// Token: 0x0400583E RID: 22590
		private bool isKiguiXu;

		// Token: 0x0400583F RID: 22591
		private bool isKiguiLuong;

		// Token: 0x04005840 RID: 22592
		private int delayKigui;

		// Token: 0x04005841 RID: 22593
		public sbyte combineSuccess = -1;

		// Token: 0x04005842 RID: 22594
		public int idNPC;

		// Token: 0x04005843 RID: 22595
		public int xS;

		// Token: 0x04005844 RID: 22596
		public int yS;

		// Token: 0x04005845 RID: 22597
		private int rS;

		// Token: 0x04005846 RID: 22598
		private int angleS;

		// Token: 0x04005847 RID: 22599
		private int angleO;

		// Token: 0x04005848 RID: 22600
		private int iAngleS;

		// Token: 0x04005849 RID: 22601
		private int iDotS;

		// Token: 0x0400584A RID: 22602
		private int speed;

		// Token: 0x0400584B RID: 22603
		private int[] xArgS;

		// Token: 0x0400584C RID: 22604
		private int[] yArgS;

		// Token: 0x0400584D RID: 22605
		private int[] xDotS;

		// Token: 0x0400584E RID: 22606
		private int[] yDotS;

		// Token: 0x0400584F RID: 22607
		private int time;

		// Token: 0x04005850 RID: 22608
		private int typeCombine;

		// Token: 0x04005851 RID: 22609
		private int countUpdate;

		// Token: 0x04005852 RID: 22610
		private int countR;

		// Token: 0x04005853 RID: 22611
		private int countWait;

		// Token: 0x04005854 RID: 22612
		private bool isSpeedCombine;

		// Token: 0x04005855 RID: 22613
		private bool isCompleteEffCombine = true;

		// Token: 0x04005856 RID: 22614
		private bool isPaintCombine;

		// Token: 0x04005857 RID: 22615
		public bool isDoneCombine = true;

		// Token: 0x04005858 RID: 22616
		public short iconID1;

		// Token: 0x04005859 RID: 22617
		public short iconID2;

		// Token: 0x0400585A RID: 22618
		public short iconID3;

		// Token: 0x0400585B RID: 22619
		public short[] iconID;

		// Token: 0x0400585C RID: 22620
		public string[][] speacialTabName;

		// Token: 0x0400585D RID: 22621
		public static int[] sizeUpgradeEff = new int[]
		{
			2,
			1,
			1
		};

		// Token: 0x0400585E RID: 22622
		public static int nsize = 1;

		// Token: 0x0400585F RID: 22623
		public const sbyte COLOR_WHITE = 0;

		// Token: 0x04005860 RID: 22624
		public const sbyte COLOR_GREEN = 1;

		// Token: 0x04005861 RID: 22625
		public const sbyte COLOR_PURPLE = 2;

		// Token: 0x04005862 RID: 22626
		public const sbyte COLOR_ORANGE = 3;

		// Token: 0x04005863 RID: 22627
		public const sbyte COLOR_BLUE = 4;

		// Token: 0x04005864 RID: 22628
		public const sbyte COLOR_YELLOW = 5;

		// Token: 0x04005865 RID: 22629
		public const sbyte COLOR_RED = 6;

		// Token: 0x04005866 RID: 22630
		public const sbyte COLOR_BLACK = 7;

		// Token: 0x04005867 RID: 22631
		public static int[][] colorUpgradeEffect = new int[][]
		{
			new int[]
			{
				16777215,
				15000805,
				13487823,
				11711155,
				9671828,
				7895160
			},
			new int[]
			{
				61952,
				58624,
				52224,
				45824,
				39168,
				32768
			},
			new int[]
			{
				13500671,
				12058853,
				10682572,
				9371827,
				7995545,
				6684800
			},
			new int[]
			{
				16744192,
				15037184,
				13395456,
				11753728,
				10046464,
				8404992
			},
			new int[]
			{
				37119,
				33509,
				28108,
				24499,
				21145,
				17536
			},
			new int[]
			{
				16776192,
				15063040,
				12635136,
				11776256,
				10063872,
				8290304
			},
			new int[]
			{
				16711680,
				15007744,
				13369344,
				11730944,
				10027008,
				8388608
			}
		};

		// Token: 0x04005868 RID: 22632
		public const int color_item_white = 15987701;

		// Token: 0x04005869 RID: 22633
		public const int color_item_green = 2786816;

		// Token: 0x0400586A RID: 22634
		public const int color_item_purple = 7078041;

		// Token: 0x0400586B RID: 22635
		public const int color_item_orange = 12537346;

		// Token: 0x0400586C RID: 22636
		public const int color_item_blue = 1269146;

		// Token: 0x0400586D RID: 22637
		public const int color_item_yellow = 13279744;

		// Token: 0x0400586E RID: 22638
		public const int color_item_red = 11599872;

		// Token: 0x0400586F RID: 22639
		public const int color_item_black = 2039326;

		// Token: 0x04005870 RID: 22640
		private Image imgo_0;

		// Token: 0x04005871 RID: 22641
		private Image imgo_00;

		// Token: 0x04005872 RID: 22642
		private Image imgo_1;

		// Token: 0x04005873 RID: 22643
		private Image imgo_2;

		// Token: 0x04005874 RID: 22644
		private Image imgo_3;

		// Token: 0x04005875 RID: 22645
		private Image imgo_4;

		// Token: 0x04005876 RID: 22646
		private Image imgo_5;

		// Token: 0x04005877 RID: 22647
		private Image imgo_6;

		// Token: 0x04005878 RID: 22648
		private Image imgo_7;

		// Token: 0x04005879 RID: 22649
		private Image imgo_8;

		// Token: 0x0400587A RID: 22650
		private Image imgo_10;

		// Token: 0x0400587B RID: 22651
		private Image imgo_11;

		// Token: 0x0400587C RID: 22652
		private Image imgo_12;

		// Token: 0x0400587D RID: 22653
		private Image imgo_13;

		// Token: 0x0400587E RID: 22654
		private Image imgo_14;

		// Token: 0x0400587F RID: 22655
		private Image imgo_15;

		// Token: 0x04005880 RID: 22656
		private Image imgo_16;

		// Token: 0x04005881 RID: 22657
		private Image imgo_17;

		// Token: 0x04005882 RID: 22658
		private Image imgo_18;

		// Token: 0x04005883 RID: 22659
		private Image imgo_19;

		// Token: 0x04005884 RID: 22660
		private Image imgo_20;

		// Token: 0x04005885 RID: 22661
		private Image imgo_21;

		// Token: 0x04005886 RID: 22662
		private Image imgo_22;

		// Token: 0x04005887 RID: 22663
		private Image imgo_23;

		// Token: 0x04005888 RID: 22664
		public const int numItem = 20;

		// Token: 0x04005889 RID: 22665
		public const sbyte INVENTORY_TAB = 1;

		// Token: 0x0400588A RID: 22666
		public sbyte size_tab;

		// Token: 0x0400588B RID: 22667
		private bool isnewInventory;

		// Token: 0x0400588C RID: 22668
		private static Image[] bgcam = new Image[8];

		// Token: 0x0400588D RID: 22669
		private static Image[] bgdo = new Image[8];

		// Token: 0x0400588E RID: 22670
		private static Image[] bgxanhla = new Image[8];

		// Token: 0x0400588F RID: 22671
		private static Image[] bgtim = new Image[8];

		// Token: 0x04005890 RID: 22672
		private static Image[] bgxanhnhat = new Image[8];

		// Token: 0x04005891 RID: 22673
		private static Image[] bgxanhdam = new Image[8];

		// Token: 0x04005892 RID: 22674
		private static Image[] bghong = new Image[8];

		// Token: 0x04005893 RID: 22675
		private static Image[] bgxanhduong = new Image[8];

		// Token: 0x04005894 RID: 22676
		private int WidthBoxNew = 33;

		// Token: 0x04005895 RID: 22677
		private int CountBoxInRow = 5;

		// Token: 0x04005896 RID: 22678
		public static string[][] MenuOption = new string[][]
		{
			new string[]
			{
				"D.sách",
				"Item"
			},
			new string[]
			{
				"Chức",
				"Năng"
			},
			new string[]
			{
				"H.Dẫn",
				"Sử Dụng"
			}
		};

		// Token: 0x04005897 RID: 22679
		private Image imgStarItem;

		// Token: 0x020003EC RID: 1004
		public class PlayerChat
		{
			// Token: 0x04005898 RID: 22680
			public string name;

			// Token: 0x04005899 RID: 22681
			public int charID;

			// Token: 0x0400589A RID: 22682
			public bool isNewMessage;

			// Token: 0x0400589B RID: 22683
			public List<InfoItem> chats;
		}
	}
}
