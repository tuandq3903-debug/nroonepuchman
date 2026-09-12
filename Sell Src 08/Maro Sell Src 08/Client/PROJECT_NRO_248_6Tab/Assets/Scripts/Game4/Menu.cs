using System;

namespace Game4
{
	// Token: 0x02000218 RID: 536
	public class Menu
	{
		// Token: 0x0600175A RID: 5978 RVA: 0x00174543 File Offset: 0x00172743
		public static void loadBg()
		{
			Menu.imgMenu1 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu1.png");
			Menu.imgMenu2 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu2.png");
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00174563 File Offset: 0x00172763
		public void startWithoutCloseButton(MyVector menuItems, int pos)
		{
			this.startAt(menuItems, pos);
			this.disableClose = true;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00174574 File Offset: 0x00172774
		public void startAt(MyVector menuItems, int x, int y)
		{
			this.startAt(menuItems, 0);
			this.menuX = x;
			this.menuY = y;
			while (this.menuY + this.menuH > GameCanvas.h)
			{
				this.menuY -= 2;
			}
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x001745B0 File Offset: 0x001727B0
		public void startAt(MyVector menuItems, int pos)
		{
			if (this.showMenu)
			{
				return;
			}
			this.isClose = false;
			this.touch = false;
			this.close = false;
			this.tDelay = 0;
			if (menuItems.size() == 1)
			{
				this.menuSelectedItem = 0;
				Command command = (Command)menuItems.elementAt(0);
				if (command != null && command.caption.Equals(mResources.saying))
				{
					command.performAction();
					this.showMenu = false;
					InfoDlg.showWait();
					return;
				}
			}
			SoundMn.gI().openMenu();
			this.isNotClose = new bool[menuItems.size()];
			for (int i = 0; i < this.isNotClose.Length; i++)
			{
				this.isNotClose[i] = false;
			}
			this.disableClose = false;
			ChatPopup.currChatPopup = null;
			Effect2.vEffect2.removeAllElements();
			Effect2.vEffect2Outside.removeAllElements();
			InfoDlg.hide();
			if (menuItems.size() != 0)
			{
				this.menuItems = menuItems;
				this.menuW = 60;
				this.menuH = 60;
				for (int j = 0; j < menuItems.size(); j++)
				{
					Command command2 = (Command)menuItems.elementAt(j);
					command2.isPlaySoundButton = false;
					mFont.tahoma_7_yellow.getWidth(command2.caption);
					command2.subCaption = mFont.tahoma_7_yellow.splitFontArray(command2.caption, this.menuW - 10);
				}
				Menu.menuTemY = new int[menuItems.size()];
				this.menuX = (GameCanvas.w - menuItems.size() * this.menuW) / 2;
				if (this.menuX < 1)
				{
					this.menuX = 1;
				}
				this.menuY = GameCanvas.h - this.menuH - (Paint.hTab + 1) - 1;
				if (GameCanvas.isTouch)
				{
					this.menuY -= 3;
				}
				this.menuY += 27;
				for (int k = 0; k < Menu.menuTemY.Length; k++)
				{
					Menu.menuTemY[k] = GameCanvas.h;
				}
				this.showMenu = true;
				this.menuSelectedItem = 0;
				Menu.cmxLim = this.menuItems.size() * this.menuW - GameCanvas.w;
				if (Menu.cmxLim < 0)
				{
					Menu.cmxLim = 0;
				}
				Menu.cmtoX = 0;
				Menu.cmx = 0;
				Menu.xc = 50;
				this.w = menuItems.size() * this.menuW - 1;
				if (this.w > GameCanvas.w - 2)
				{
					this.w = GameCanvas.w - 2;
				}
				if (GameCanvas.isTouch && !Main.isPC)
				{
					this.menuSelectedItem = -1;
				}
			}
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0017482C File Offset: 0x00172A2C
		public bool isScrolling()
		{
			return (!this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] > this.menuY) || (this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] < GameCanvas.h);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0017487C File Offset: 0x00172A7C
		public void updateMenuKey()
		{
			if ((GameScr.gI().activeRongThan && GameScr.gI().isUseFreez) || !this.showMenu || this.isScrolling())
			{
				return;
			}
			bool flag = false;
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				flag = true;
				this.menuSelectedItem--;
				if (this.menuSelectedItem < 0)
				{
					this.menuSelectedItem = this.menuItems.size() - 1;
				}
			}
			else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				flag = true;
				this.menuSelectedItem++;
				if (this.menuSelectedItem > this.menuItems.size() - 1)
				{
					this.menuSelectedItem = 0;
				}
			}
			else if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				if (this.center != null)
				{
					if (this.center.idAction > 0)
					{
						if (this.center.actionListener == GameScr.gI())
						{
							GameScr.gI().actionPerform(this.center.idAction, this.center.p);
						}
						else
						{
							this.perform(this.center.idAction, this.center.p);
						}
					}
				}
				else
				{
					this.waitToPerform = 2;
				}
			}
			else if (GameCanvas.keyPressed[12] && !GameScr.gI().isRongThanMenu())
			{
				if (this.isScrolling())
				{
					return;
				}
				if (this.left.idAction > 0)
				{
					this.perform(this.left.idAction, this.left.p);
				}
				else
				{
					this.waitToPerform = 2;
				}
				SoundMn.gI().buttonClose();
			}
			else if (!GameScr.gI().isRongThanMenu() && !this.disableClose && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
			{
				if (this.isScrolling())
				{
					return;
				}
				if (!this.close)
				{
					this.close = true;
				}
				this.isClose = true;
				SoundMn.gI().buttonClose();
			}
			if (flag)
			{
				Menu.cmtoX = this.menuSelectedItem * this.menuW + this.menuW - GameCanvas.w / 2;
				if (Menu.cmtoX > Menu.cmxLim)
				{
					Menu.cmtoX = Menu.cmxLim;
				}
				if (Menu.cmtoX < 0)
				{
					Menu.cmtoX = 0;
				}
				if (this.menuSelectedItem == this.menuItems.size() - 1 || this.menuSelectedItem == 0)
				{
					Menu.cmx = Menu.cmtoX;
				}
			}
			bool flag2 = true;
			if (GameCanvas.panel.cp != null && GameCanvas.panel.cp.isClip)
			{
				if (!GameCanvas.isPointerHoldIn(GameCanvas.panel.cp.cx, 0, GameCanvas.panel.cp.sayWidth + 2, GameCanvas.panel.cp.ch))
				{
					flag2 = true;
				}
				else
				{
					flag2 = false;
					GameCanvas.panel.cp.updateKey();
				}
			}
			if (!this.disableClose && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH) && !this.pointerIsDowning && !GameScr.gI().isRongThanMenu() && flag2)
			{
				if (!this.isScrolling())
				{
					this.pointerDownTime = (this.pointerDownFirstX = 0);
					this.pointerIsDowning = false;
					GameCanvas.clearAllPointerEvent();
					Res.outz("menu select= " + this.menuSelectedItem.ToString());
					this.isClose = true;
					this.close = true;
					SoundMn.gI().buttonClose();
				}
				return;
			}
			if (GameCanvas.isPointerDown)
			{
				if (!this.pointerIsDowning && GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH))
				{
					for (int i = 0; i < this.pointerDownLastX.Length; i++)
					{
						this.pointerDownLastX[0] = GameCanvas.px;
					}
					this.pointerDownFirstX = GameCanvas.px;
					this.pointerIsDowning = true;
					this.isDownWhenRunning = (this.cmRun != 0);
					this.cmRun = 0;
				}
				else if (this.pointerIsDowning)
				{
					this.pointerDownTime++;
					if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning)
					{
						this.pointerDownFirstX = -1000;
						this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
					}
					int num = GameCanvas.px - this.pointerDownLastX[0];
					if (num != 0 && this.menuSelectedItem != -1)
					{
						this.menuSelectedItem = -1;
					}
					for (int num2 = this.pointerDownLastX.Length - 1; num2 > 0; num2--)
					{
						this.pointerDownLastX[num2] = this.pointerDownLastX[num2 - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.px;
					Menu.cmtoX -= num;
					if (Menu.cmtoX < 0)
					{
						Menu.cmtoX = 0;
					}
					if (Menu.cmtoX > Menu.cmxLim)
					{
						Menu.cmtoX = Menu.cmxLim;
					}
					if (Menu.cmx < 0 || Menu.cmx > Menu.cmxLim)
					{
						num /= 2;
					}
					Menu.cmx -= num;
					if (Menu.cmx < -(GameCanvas.h / 3))
					{
						this.wantUpdateList = true;
					}
					else
					{
						this.wantUpdateList = false;
					}
				}
			}
			if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
			{
				int i2 = GameCanvas.px - this.pointerDownLastX[0];
				GameCanvas.isPointerJustRelease = false;
				if (Res.abs(i2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
				{
					this.cmRun = 0;
					Menu.cmtoX = Menu.cmx;
					this.pointerDownFirstX = -1000;
					this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
					this.pointerDownTime = 0;
					this.waitToPerform = 10;
				}
				else if (this.menuSelectedItem != -1 && this.pointerDownTime > 5)
				{
					this.pointerDownTime = 0;
					this.waitToPerform = 1;
				}
				else if (this.menuSelectedItem == -1 && !this.isDownWhenRunning)
				{
					if (Menu.cmx < 0)
					{
						Menu.cmtoX = 0;
					}
					else if (Menu.cmx > Menu.cmxLim)
					{
						Menu.cmtoX = Menu.cmxLim;
					}
					else
					{
						int num3 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
						num3 = ((num3 > 10) ? 10 : ((num3 < -10) ? -10 : 0));
						this.cmRun = -num3 * 100;
					}
				}
				this.pointerIsDowning = false;
				this.pointerDownTime = 0;
				GameCanvas.isPointerJustRelease = false;
			}
			GameCanvas.clearKeyPressed();
			GameCanvas.clearKeyHold();
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00174F54 File Offset: 0x00173154
		public void moveCamera()
		{
			if (this.cmRun != 0 && !this.pointerIsDowning)
			{
				Menu.cmtoX += this.cmRun / 100;
				if (Menu.cmtoX < 0)
				{
					Menu.cmtoX = 0;
				}
				else if (Menu.cmtoX > Menu.cmxLim)
				{
					Menu.cmtoX = Menu.cmxLim;
				}
				else
				{
					Menu.cmx = Menu.cmtoX;
				}
				this.cmRun = this.cmRun * 9 / 10;
				if (this.cmRun < 100 && this.cmRun > -100)
				{
					this.cmRun = 0;
				}
			}
			if (Menu.cmx != Menu.cmtoX && !this.pointerIsDowning)
			{
				this.cmvx = Menu.cmtoX - Menu.cmx << 2;
				this.cmdx += this.cmvx;
				Menu.cmx += this.cmdx >> 4;
				this.cmdx &= 15;
			}
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00175040 File Offset: 0x00173240
		public void paintMenu(mGraphics g)
		{
			if (GameScr.gI().activeRongThan && GameScr.gI().isUseFreez)
			{
				return;
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.translate(-Menu.cmx, 0);
			for (int i = 0; i < this.menuItems.size(); i++)
			{
				if (i == this.menuSelectedItem)
				{
					g.drawImage(Menu.imgMenu2, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
				}
				else
				{
					g.drawImage(Menu.imgMenu1, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
				}
				Command command = (Command)this.menuItems.elementAt(i);
				string[] array = command.subCaption;
				if (array == null)
				{
					array = new string[]
					{
						((Command)this.menuItems.elementAt(i)).caption
					};
				}
				int num = Menu.menuTemY[i] + (this.menuH - array.Length * 14) / 2 + 1;
				for (int j = 0; j < array.Length; j++)
				{
					if (i == this.menuSelectedItem)
					{
						mFont.tahoma_7b_green2.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
					}
					else if (command.isDisplay)
					{
						mFont.tahoma_7b_red.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
					}
					else
					{
						mFont.tahoma_7b_dark.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
					}
				}
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0017522C File Offset: 0x0017342C
		public void doCloseMenu()
		{
			this.isClose = false;
			this.showMenu = false;
			InfoDlg.hide();
			if (this.close)
			{
				GameCanvas.panel.cp = null;
				Char.chatPopup = null;
				if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
				{
					GameCanvas.panel2.cp = null;
					return;
				}
			}
			else
			{
				if (!this.touch)
				{
					return;
				}
				GameCanvas.panel.cp = null;
				if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
				{
					GameCanvas.panel2.cp = null;
				}
				if (this.menuSelectedItem >= 0)
				{
					Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
					if (command != null)
					{
						SoundMn.gI().buttonClose();
						command.performAction();
					}
				}
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x001752E9 File Offset: 0x001734E9
		public void performSelect()
		{
			InfoDlg.hide();
			if (this.menuSelectedItem >= 0)
			{
				Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
				if (command == null)
				{
					return;
				}
				command.performAction();
			}
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0017531C File Offset: 0x0017351C
		public void updateMenu()
		{
			this.moveCamera();
			if (!this.isClose)
			{
				this.tDelay++;
				for (int i = 0; i < Menu.menuTemY.Length; i++)
				{
					if (Menu.menuTemY[i] > this.menuY)
					{
						int num = Menu.menuTemY[i] - this.menuY >> 1;
						if (num < 1)
						{
							num = 1;
						}
						if (this.tDelay > i)
						{
							Menu.menuTemY[i] -= num;
						}
					}
				}
				if (Menu.menuTemY[Menu.menuTemY.Length - 1] <= this.menuY)
				{
					this.tDelay = 0;
				}
			}
			else
			{
				this.tDelay++;
				for (int j = 0; j < Menu.menuTemY.Length; j++)
				{
					if (Menu.menuTemY[j] < GameCanvas.h)
					{
						int num2 = (GameCanvas.h - Menu.menuTemY[j] >> 1) + 2;
						if (num2 < 1)
						{
							num2 = 1;
						}
						if (this.tDelay > j)
						{
							Menu.menuTemY[j] += num2;
						}
					}
				}
				if (Menu.menuTemY[Menu.menuTemY.Length - 1] >= GameCanvas.h)
				{
					this.tDelay = 0;
					this.doCloseMenu();
				}
			}
			if (Menu.xc != 0)
			{
				Menu.xc >>= 1;
				if (Menu.xc < 0)
				{
					Menu.xc = 0;
				}
			}
			if (this.isScrolling() || this.waitToPerform <= 0)
			{
				return;
			}
			this.waitToPerform--;
			if (this.waitToPerform == 0)
			{
				if (this.menuSelectedItem >= 0 && !this.isNotClose[this.menuSelectedItem])
				{
					this.isClose = true;
					this.touch = true;
					GameCanvas.panel.cp = null;
					return;
				}
				this.performSelect();
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000034B9 File Offset: 0x000016B9
		public void perform(int idAction, object p)
		{
		}

		// Token: 0x04002DEB RID: 11755
		public bool showMenu;

		// Token: 0x04002DEC RID: 11756
		public MyVector menuItems;

		// Token: 0x04002DED RID: 11757
		public int menuSelectedItem;

		// Token: 0x04002DEE RID: 11758
		public int menuX;

		// Token: 0x04002DEF RID: 11759
		public int menuY;

		// Token: 0x04002DF0 RID: 11760
		public int menuW;

		// Token: 0x04002DF1 RID: 11761
		public int menuH;

		// Token: 0x04002DF2 RID: 11762
		public static int[] menuTemY;

		// Token: 0x04002DF3 RID: 11763
		public static int cmtoX;

		// Token: 0x04002DF4 RID: 11764
		public static int cmx;

		// Token: 0x04002DF5 RID: 11765
		public static int cmdy;

		// Token: 0x04002DF6 RID: 11766
		public static int cmvy;

		// Token: 0x04002DF7 RID: 11767
		public static int cmxLim;

		// Token: 0x04002DF8 RID: 11768
		public static int xc;

		// Token: 0x04002DF9 RID: 11769
		private Command left = new Command(mResources.SELECT, 0);

		// Token: 0x04002DFA RID: 11770
		private Command right = new Command(mResources.CLOSE, 0, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH + 1);

		// Token: 0x04002DFB RID: 11771
		private Command center;

		// Token: 0x04002DFC RID: 11772
		public static Image imgMenu1;

		// Token: 0x04002DFD RID: 11773
		public static Image imgMenu2;

		// Token: 0x04002DFE RID: 11774
		private bool disableClose;

		// Token: 0x04002DFF RID: 11775
		public int tDelay;

		// Token: 0x04002E00 RID: 11776
		public int w;

		// Token: 0x04002E01 RID: 11777
		private int pa;

		// Token: 0x04002E02 RID: 11778
		private bool trans;

		// Token: 0x04002E03 RID: 11779
		private int pointerDownTime;

		// Token: 0x04002E04 RID: 11780
		private int pointerDownFirstX;

		// Token: 0x04002E05 RID: 11781
		private int[] pointerDownLastX = new int[3];

		// Token: 0x04002E06 RID: 11782
		private bool pointerIsDowning;

		// Token: 0x04002E07 RID: 11783
		private bool isDownWhenRunning;

		// Token: 0x04002E08 RID: 11784
		private bool wantUpdateList;

		// Token: 0x04002E09 RID: 11785
		private int waitToPerform;

		// Token: 0x04002E0A RID: 11786
		private int cmRun;

		// Token: 0x04002E0B RID: 11787
		private bool touch;

		// Token: 0x04002E0C RID: 11788
		private bool close;

		// Token: 0x04002E0D RID: 11789
		private int cmvx;

		// Token: 0x04002E0E RID: 11790
		private int cmdx;

		// Token: 0x04002E0F RID: 11791
		private bool isClose;

		// Token: 0x04002E10 RID: 11792
		public bool[] isNotClose;
	}
}
