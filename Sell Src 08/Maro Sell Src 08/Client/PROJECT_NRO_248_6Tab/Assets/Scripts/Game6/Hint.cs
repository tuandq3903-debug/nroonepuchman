using System;

namespace Game6
{
	// Token: 0x02000041 RID: 65
	public class Hint
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00042FD3 File Offset: 0x000411D3
		public static bool isOnTask(int tastId, int index)
		{
			return Char.myCharz().taskMaint != null && (int)Char.myCharz().taskMaint.taskId == tastId && Char.myCharz().taskMaint.index == index;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00043008 File Offset: 0x00041208
		public static void clickNpc()
		{
			if (GameCanvas.panel.isShow)
			{
				Hint.isPaint = false;
			}
			if (GameScr.getNpcTask() != null)
			{
				Hint.x = GameScr.getNpcTask().cx;
				Hint.y = GameScr.getNpcTask().cy;
				Hint.trans = 0;
				Hint.isCamera = true;
				Hint.type = (GameCanvas.isTouch ? 1 : 0);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00043068 File Offset: 0x00041268
		public static void nextMap(int index)
		{
			if (!GameCanvas.panel.isShow && PopUp.vPopups.size() - 1 >= index)
			{
				PopUp popUp = (PopUp)PopUp.vPopups.elementAt(index);
				Hint.x = popUp.cx + popUp.sayWidth / 2;
				Hint.y = popUp.cy + 30;
				if (popUp.isHide || !popUp.isPaint)
				{
					Hint.isPaint = false;
				}
				else
				{
					Hint.isPaint = true;
				}
				Hint.type = 0;
				Hint.isCamera = true;
				Hint.trans = 0;
				if (!GameCanvas.isTouch)
				{
					Hint.isPaint = false;
				}
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00043100 File Offset: 0x00041300
		public static void clickMob()
		{
			Hint.type = 1;
			if (GameCanvas.panel.isShow)
			{
				Hint.isPaint = false;
			}
			bool flag = false;
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				if (((Mob)GameScr.vMob.elementAt(i)).isHintFocus)
				{
					flag = true;
					break;
				}
			}
			int j = 0;
			while (j < GameScr.vMob.size())
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(j);
				if (mob2.isHintFocus)
				{
					Hint.x = mob2.x;
					Hint.y = mob2.y + 5;
					Hint.isCamera = true;
					if (mob2.status == 0)
					{
						mob2.isHintFocus = false;
						return;
					}
					break;
				}
				else
				{
					if (!flag)
					{
						if (mob2.status != 0)
						{
							mob2.isHintFocus = true;
							return;
						}
						mob2.isHintFocus = false;
					}
					j++;
				}
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000431D0 File Offset: 0x000413D0
		public static bool isHaveItem()
		{
			if (GameCanvas.panel.isShow)
			{
				Hint.isPaint = false;
			}
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				if (itemMap.playerId == Char.myCharz().charID && itemMap.template.id == 73)
				{
					Hint.type = 1;
					Hint.x = itemMap.x;
					Hint.y = itemMap.y + 5;
					Hint.isCamera = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00043260 File Offset: 0x00041460
		public static void paintArrowPointToHint(mGraphics g)
		{
			try
			{
				if (Hint.isPaintArrow && (Hint.x <= GameScr.cmx || Hint.x >= GameScr.cmx + GameScr.gW || Hint.y <= GameScr.cmy || Hint.y >= GameScr.cmy + GameScr.gH) && GameCanvas.gameTick % 10 >= 5 && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null && !GameCanvas.panel.isShow && Hint.isCamera)
				{
					int num = Hint.x - Char.myCharz().cx;
					int num2 = Hint.y - Char.myCharz().cy;
					int num3 = 0;
					int num4 = 0;
					int arg = 0;
					if (num > 0 && num2 >= 0)
					{
						if (Res.abs(num) >= Res.abs(num2))
						{
							num3 = GameScr.gW - 10;
							num4 = GameScr.gH / 2 + 30;
							if (GameCanvas.isTouch)
							{
								num4 = GameScr.gH / 2 + 10;
							}
							arg = 0;
						}
						else
						{
							num3 = GameScr.gW / 2;
							num4 = GameScr.gH - 10;
							arg = 5;
						}
					}
					else if (num >= 0 && num2 < 0)
					{
						if (Res.abs(num) >= Res.abs(num2))
						{
							num3 = GameScr.gW - 10;
							num4 = GameScr.gH / 2 + 30;
							if (GameCanvas.isTouch)
							{
								num4 = GameScr.gH / 2 + 10;
							}
							arg = 0;
						}
						else
						{
							num3 = GameScr.gW / 2;
							num4 = 10;
							arg = 6;
						}
					}
					if (num < 0 && num2 >= 0)
					{
						if (Res.abs(num) >= Res.abs(num2))
						{
							num3 = 10;
							num4 = GameScr.gH / 2 + 30;
							if (GameCanvas.isTouch)
							{
								num4 = GameScr.gH / 2 + 10;
							}
							arg = 3;
						}
						else
						{
							num3 = GameScr.gW / 2;
							num4 = GameScr.gH - 10;
							arg = 5;
						}
					}
					else if (num <= 0 && num2 < 0)
					{
						if (Res.abs(num) >= Res.abs(num2))
						{
							num3 = 10;
							num4 = GameScr.gH / 2 + 30;
							if (GameCanvas.isTouch)
							{
								num4 = GameScr.gH / 2 + 10;
							}
							arg = 3;
						}
						else
						{
							num3 = GameScr.gW / 2;
							num4 = 10;
							arg = 6;
						}
					}
					GameScr.resetTranslate(g);
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, num3, num4, StaticObj.VCENTER_HCENTER);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0004348C File Offset: 0x0004168C
		public static void paint(mGraphics g)
		{
			if (ChatPopup.serverChatPopUp != null || Char.myCharz().isUsePlane || Char.myCharz().isTeleport)
			{
				return;
			}
			Hint.paintArrowPointToHint(g);
			if (GameCanvas.menu.tDelay == 0 && Hint.isPaint && ChatPopup.scr == null && !Char.ischangingMap && GameCanvas.currentScreen == GameScr.gI() && (!GameCanvas.panel.isShow || GameCanvas.panel.cmx == 0))
			{
				if (Hint.isCamera)
				{
					g.translate(-GameScr.cmx, -GameScr.cmy);
				}
				if (Hint.trans == 0)
				{
					g.drawImage(Panel.imgBantay, Hint.x - 15, Hint.y, 0);
				}
				if (Hint.trans == 1)
				{
					g.drawRegion(Panel.imgBantay, 0, 0, 14, 16, 2, Hint.x + 15, Hint.y, StaticObj.TOP_RIGHT);
				}
				if (Hint.paintFlare)
				{
					g.drawImage(ItemMap.imageFlare, Hint.x, Hint.y, 3);
				}
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00043598 File Offset: 0x00041798
		public static void hint()
		{
			if (Char.myCharz().taskMaint == null || GameCanvas.currentScreen != GameScr.instance)
			{
				Hint.isPaint = false;
				Hint.isPaintArrow = false;
				return;
			}
			int taskId = (int)Char.myCharz().taskMaint.taskId;
			int index = Char.myCharz().taskMaint.index;
			Hint.isCamera = false;
			Hint.trans = 0;
			Hint.type = 0;
			Hint.isPaint = true;
			Hint.isPaintArrow = true;
			if (GameCanvas.menu.showMenu && taskId > 0)
			{
				Hint.isPaint = false;
			}
			switch (taskId)
			{
			case 0:
				if (ChatPopup.currChatPopup != null || Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
					return;
				}
				if (index == 0 && TileMap.vGo.size() != 0)
				{
					Hint.x = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minX - 100);
					Hint.y = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minY + 40);
					Hint.isCamera = true;
				}
				if (index == 1)
				{
					Hint.nextMap(0);
				}
				if (index == 2)
				{
					Hint.clickNpc();
				}
				if (index == 3)
				{
					if (!GameCanvas.panel.isShow)
					{
						Hint.clickNpc();
					}
					else if (GameCanvas.panel.currentTabIndex == 0)
					{
						if (GameCanvas.panel.cp == null)
						{
							Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
							Hint.y = GameCanvas.panel.yScroll + 20;
						}
						else if (GameCanvas.menu.tDelay != 0)
						{
							Hint.x = GameCanvas.panel.xScroll + 25;
							Hint.y = GameCanvas.panel.yScroll + 60;
						}
					}
					else if (GameCanvas.panel.currentTabIndex == 1)
					{
						Hint.x = GameCanvas.panel.startTabPos + 10;
						Hint.y = 65;
					}
				}
				if (index == 4)
				{
					if (GameCanvas.panel.isShow)
					{
						Hint.x = GameCanvas.panel.cmdClose.x + 5;
						Hint.y = GameCanvas.panel.cmdClose.y + 5;
					}
					else if (GameCanvas.menu.showMenu)
					{
						Hint.x = GameCanvas.w / 2;
						Hint.y = GameCanvas.h - 20;
					}
					else
					{
						Hint.clickNpc();
					}
				}
				if (index == 5)
				{
					Hint.clickNpc();
				}
				return;
			case 1:
				if (ChatPopup.currChatPopup != null || Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
					return;
				}
				if (index == 0)
				{
					if (TileMap.isOfflineMap())
					{
						Hint.nextMap(0);
					}
					else
					{
						Hint.clickMob();
					}
				}
				if (index == 1)
				{
					if (!TileMap.isOfflineMap())
					{
						Hint.nextMap(1);
						return;
					}
					Hint.clickNpc();
				}
				return;
			case 2:
				if (ChatPopup.currChatPopup != null || Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
					return;
				}
				if (index == 0)
				{
					if (!TileMap.isOfflineMap())
					{
						Hint.isViewMap = true;
					}
					if (!GameCanvas.panel.isShow)
					{
						if (!Hint.isViewMap)
						{
							Hint.x = GameScr.gI().cmdMenu.x;
							Hint.y = GameScr.gI().cmdMenu.y + 13;
							Hint.trans = 1;
						}
						else
						{
							if (GameScr.getTaskMapId() == TileMap.mapID)
							{
								if (!Hint.isHaveItem())
								{
									Hint.clickMob();
								}
							}
							else
							{
								Hint.nextMap(0);
							}
							if (Hint.isViewMap)
							{
								Hint.isCloseMap = true;
							}
						}
					}
					else if (!Hint.isViewMap)
					{
						if (GameCanvas.panel.currentTabIndex == 0)
						{
							int num = (GameCanvas.h <= 300) ? 10 : 15;
							Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
							Hint.y = GameCanvas.panel.yScroll + GameCanvas.panel.hScroll - num;
						}
						else
						{
							Hint.x = GameCanvas.panel.startTabPos + 10;
							Hint.y = 65;
						}
					}
					else if (!Hint.isCloseMap)
					{
						Hint.x = GameCanvas.panel.cmdClose.x + 5;
						Hint.y = GameCanvas.panel.cmdClose.y + 5;
					}
					else
					{
						Hint.isPaint = false;
					}
					if (Char.myCharz().cMP <= 0L)
					{
						Hint.x = GameScr.xHP + 5;
						Hint.y = GameScr.yHP + 13;
						Hint.isCamera = false;
					}
				}
				if (index == 1)
				{
					Hint.isPaint = false;
					Hint.isPaintArrow = false;
				}
				return;
			case 3:
				if (ChatPopup.currChatPopup != null || Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
					return;
				}
				if (index == 0)
				{
					if (!GameCanvas.panel.isShow)
					{
						if (!Hint.isViewPotential)
						{
							Hint.x = GameScr.gI().cmdMenu.x;
							Hint.y = GameScr.gI().cmdMenu.y + 13;
							Hint.trans = 1;
						}
						else
						{
							if (GameScr.getTaskMapId() == TileMap.mapID)
							{
								if (!Hint.isHaveItem())
								{
									Hint.clickMob();
								}
							}
							else
							{
								Hint.nextMap(0);
							}
							if (Hint.isViewMap)
							{
								Hint.isCloseMap = true;
							}
						}
					}
					else if (!Hint.isViewPotential)
					{
						int h = GameCanvas.h;
						Hint.x = GameCanvas.panel.xScroll + 10 + 108 - 18;
						Hint.y = 65;
					}
					else if (!Hint.isCloseMap)
					{
						Hint.x = GameCanvas.panel.cmdClose.x + 5;
						Hint.y = GameCanvas.panel.cmdClose.y + 5;
					}
					else
					{
						Hint.isPaint = false;
					}
					if (Char.myCharz().cMP <= 0L)
					{
						Hint.x = GameScr.xHP + 5;
						Hint.y = GameScr.yHP + 13;
						Hint.isCamera = false;
						return;
					}
				}
				else
				{
					Hint.isPaint = false;
					Hint.isPaintArrow = false;
				}
				return;
			default:
				if (Char.myCharz().taskMaint.taskId == 9 && Char.myCharz().taskMaint.index == 2)
				{
					for (int i = 0; i < PopUp.vPopups.size(); i++)
					{
						PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
						if (popUp.cy <= 24)
						{
							Hint.x = popUp.cx + popUp.sayWidth / 2;
							Hint.y = popUp.cy + 30;
							Hint.isCamera = true;
							Hint.isPaint = false;
							Hint.isPaintArrow = true;
							return;
						}
					}
				}
				Hint.isPaint = false;
				Hint.isPaintArrow = false;
				return;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00043C00 File Offset: 0x00041E00
		public static void update()
		{
			Hint.hint();
			int num = (Hint.trans != 0) ? -2 : 2;
			if (!Hint.activeClick)
			{
				Hint.paintFlare = false;
				Hint.t++;
				if (Hint.t == 50)
				{
					Hint.t = 0;
					Hint.activeClick = true;
				}
				return;
			}
			Hint.t++;
			if (Hint.type == 0)
			{
				if (Hint.t == 2)
				{
					Hint.x += 2 * num;
					Hint.y -= 4;
					Hint.paintFlare = true;
				}
				if (Hint.t == 4)
				{
					Hint.x -= 2 * num;
					Hint.y += 4;
					Hint.activeClick = false;
					Hint.paintFlare = false;
					Hint.t = 0;
				}
				if (Hint.t > 4)
				{
					Hint.activeClick = false;
				}
			}
			if (Hint.type != 1)
			{
				return;
			}
			if (Hint.t == 2)
			{
				if (GameCanvas.isTouch)
				{
					GameScr.startFlyText(mResources.press_twice, Hint.x, Hint.y + 10, 0, 20, mFont.MISS_ME);
				}
				Hint.paintFlare = true;
				Hint.x += 2 * num;
				Hint.y -= 4;
			}
			if (Hint.t == 4)
			{
				Hint.paintFlare = false;
				Hint.x -= num;
				Hint.y += 2;
			}
			if (Hint.t == 6)
			{
				Hint.paintFlare = true;
				Hint.x += num;
				Hint.y -= 2;
			}
			if (Hint.t == 8)
			{
				Hint.paintFlare = false;
				Hint.x -= num;
				Hint.y += 2;
			}
			if (Hint.t == 10)
			{
				Hint.x -= num;
				Hint.y += 2;
				Hint.activeClick = false;
				Hint.t = 0;
			}
		}

		// Token: 0x040006E5 RID: 1765
		public static int x;

		// Token: 0x040006E6 RID: 1766
		public static int y;

		// Token: 0x040006E7 RID: 1767
		public static int type;

		// Token: 0x040006E8 RID: 1768
		public static int t;

		// Token: 0x040006E9 RID: 1769
		public static bool isShow;

		// Token: 0x040006EA RID: 1770
		public static bool activeClick;

		// Token: 0x040006EB RID: 1771
		public static bool isViewMap;

		// Token: 0x040006EC RID: 1772
		public static bool isCloseMap;

		// Token: 0x040006ED RID: 1773
		public static bool isViewPotential;

		// Token: 0x040006EE RID: 1774
		public static bool isPaint;

		// Token: 0x040006EF RID: 1775
		public static bool isCamera;

		// Token: 0x040006F0 RID: 1776
		public static int trans;

		// Token: 0x040006F1 RID: 1777
		public static bool paintFlare;

		// Token: 0x040006F2 RID: 1778
		public static bool isPaintArrow;
	}
}
