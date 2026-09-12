using System;

namespace Game3
{
	// Token: 0x020002A3 RID: 675
	public class ChatPopup : Effect2, IActionListener
	{
		// Token: 0x06001E2B RID: 7723 RVA: 0x001D404D File Offset: 0x001D224D
		public static void addNextPopUpMultiLine(string strNext, Npc next)
		{
			ChatPopup.nextMultiChatPopUp = strNext;
			ChatPopup.nextChar = next;
			if (ChatPopup.currChatPopup == null)
			{
				ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
				ChatPopup.nextMultiChatPopUp = null;
				ChatPopup.nextChar = null;
			}
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x001D4084 File Offset: 0x001D2284
		public static void addBigMessage(string chat, int howLong, Npc c)
		{
			string[] array = new string[]
			{
				chat
			};
			if (c.charID != 5 && GameScr.info1.isDone)
			{
				GameScr.info1.isUpdate = false;
			}
			Char.isLockKey = true;
			ChatPopup.serverChatPopUp = ChatPopup.addChatPopup(array[0], howLong, c);
			ChatPopup.serverChatPopUp.strY = 5;
			ChatPopup.serverChatPopUp.cx = GameCanvas.w / 2 - ChatPopup.serverChatPopUp.sayWidth / 2 - 1;
			ChatPopup.serverChatPopUp.cy = GameCanvas.h - 20 - ChatPopup.serverChatPopUp.ch;
			ChatPopup.serverChatPopUp.currentLine = 0;
			ChatPopup.serverChatPopUp.lines = array;
			ChatPopup.scr = new Scroll();
			int nItem = ChatPopup.serverChatPopUp.says.Length;
			ChatPopup.scr.setStyle(nItem, 12, ChatPopup.serverChatPopUp.cx, ChatPopup.serverChatPopUp.cy - ChatPopup.serverChatPopUp.strY + 12, ChatPopup.serverChatPopUp.sayWidth + 2, ChatPopup.serverChatPopUp.ch - 25, true, 1);
			SoundMn.gI().openDialog();
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x001D419C File Offset: 0x001D239C
		public static void addChatPopupMultiLine(string chat, int howLong, Npc c)
		{
			string[] array = Res.split(chat, "\n", 0);
			Char.isLockKey = true;
			ChatPopup.currChatPopup = ChatPopup.addChatPopup(array[0], howLong, c);
			ChatPopup.currChatPopup.currentLine = 0;
			ChatPopup.currChatPopup.lines = array;
			string caption = mResources.CONTINUE;
			if (array.Length == 1)
			{
				caption = mResources.CLOSE;
			}
			ChatPopup.currChatPopup.cmdNextLine = new Command(caption, ChatPopup.currChatPopup, 8000, null);
			ChatPopup.currChatPopup.cmdNextLine.x = GameCanvas.w / 2 - 35;
			ChatPopup.currChatPopup.cmdNextLine.y = GameCanvas.h - 35;
			SoundMn.gI().openDialog();
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x001D4248 File Offset: 0x001D2448
		public static ChatPopup addChatPopupWithIcon(string chat, int howLong, Npc c, int idIcon)
		{
			ChatPopup.performDelay = 10;
			ChatPopup chatPopup = new ChatPopup();
			chatPopup.sayWidth = GameCanvas.w - 30 - (GameCanvas.menu.showMenu ? GameCanvas.menu.menuX : 0);
			if (chatPopup.sayWidth > 320)
			{
				chatPopup.sayWidth = 320;
			}
			if (chat.Length < 10)
			{
				chatPopup.sayWidth = 64;
			}
			if (GameCanvas.w == 128)
			{
				chatPopup.sayWidth = 128;
			}
			chatPopup.says = mFont.tahoma_7_red.splitFontArray(chat, chatPopup.sayWidth - 10);
			chatPopup.delay = howLong;
			chatPopup.c = c;
			chatPopup.iconID = idIcon;
			Char.chatPopup = chatPopup;
			chatPopup.ch = 15 - chatPopup.sayRun + chatPopup.says.Length * 12 + 10;
			if (chatPopup.ch > GameCanvas.h - 80)
			{
				chatPopup.ch = GameCanvas.h - 80;
			}
			chatPopup.mH = 10;
			if (GameCanvas.menu.showMenu)
			{
				chatPopup.mH = 0;
			}
			Effect2.vEffect2.addElement(chatPopup);
			ChatPopup.isHavePetNpc = false;
			if (c != null && c.charID == 5)
			{
				ChatPopup.isHavePetNpc = true;
				GameScr.info1.addInfo(string.Empty, 1);
			}
			ChatPopup.curr = (ChatPopup.last = mSystem.currentTimeMillis());
			chatPopup.ch += 15;
			return chatPopup;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x001D43AC File Offset: 0x001D25AC
		public static ChatPopup addChatPopup(string chat, int howLong, Npc c)
		{
			ChatPopup.performDelay = 10;
			ChatPopup chatPopup = new ChatPopup();
			chatPopup.sayWidth = GameCanvas.w - 30 - (GameCanvas.menu.showMenu ? GameCanvas.menu.menuX : 0);
			if (chatPopup.sayWidth > 320)
			{
				chatPopup.sayWidth = 320;
			}
			if (chat.Length < 10)
			{
				chatPopup.sayWidth = 64;
			}
			if (GameCanvas.w == 128)
			{
				chatPopup.sayWidth = 128;
			}
			chatPopup.says = mFont.tahoma_7_red.splitFontArray(chat, chatPopup.sayWidth - 10);
			chatPopup.delay = howLong;
			chatPopup.c = c;
			Char.chatPopup = chatPopup;
			chatPopup.ch = 15 - chatPopup.sayRun + chatPopup.says.Length * 12 + 10;
			if (chatPopup.ch > GameCanvas.h - 80)
			{
				chatPopup.ch = GameCanvas.h - 80;
			}
			chatPopup.mH = 10;
			if (GameCanvas.menu.showMenu)
			{
				chatPopup.mH = 0;
			}
			Effect2.vEffect2.addElement(chatPopup);
			ChatPopup.isHavePetNpc = false;
			if (c != null && c.charID == 5)
			{
				ChatPopup.isHavePetNpc = true;
				GameScr.info1.addInfo(string.Empty, 1);
			}
			ChatPopup.curr = (ChatPopup.last = mSystem.currentTimeMillis());
			return chatPopup;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x001D44F8 File Offset: 0x001D26F8
		public override void update()
		{
			if (ChatPopup.scr != null)
			{
				GameScr.info1.isUpdate = false;
				ChatPopup.scr.updatecm();
			}
			else
			{
				GameScr.info1.isUpdate = true;
			}
			if (GameCanvas.menu.showMenu)
			{
				this.strY = 0;
				this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
				this.cy = GameCanvas.menu.menuY - this.ch;
			}
			else
			{
				this.strY = 0;
				if (GameScr.gI().right != null || GameScr.gI().left != null || GameScr.gI().center != null || this.cmdNextLine != null || this.cmdMsg1 != null)
				{
					this.strY = 5;
					this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
					this.cy = GameCanvas.h - 20 - this.ch;
				}
				else
				{
					this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
					this.cy = GameCanvas.h - 5 - this.ch;
				}
			}
			if (this.delay > 0)
			{
				this.delay--;
			}
			if (ChatPopup.performDelay > 0)
			{
				ChatPopup.performDelay--;
			}
			else
			{
				GameScr.info1.info.time = 0;
				for (int i = 0; i < GameScr.info1.info.infoWaitToShow.size(); i++)
				{
					if (((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed != 70)
					{
						((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed = 10;
					}
				}
			}
			if (this.sayRun > 1)
			{
				this.sayRun--;
			}
			if ((this.c != null && Char.chatPopup != null && Char.chatPopup != this) || (this.c != null && Char.chatPopup == null) || this.delay <= 0)
			{
				Effect2.vEffect2Outside.removeElement(this);
				Effect2.vEffect2.removeElement(this);
			}
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x001D4710 File Offset: 0x001D2910
		public override void paint(mGraphics g)
		{
			if (ModFunc.isEditButton || (GameScr.gI().activeRongThan && GameScr.gI().isUseFreez))
			{
				return;
			}
			GameCanvas.resetTrans(g);
			int num = this.cx;
			int num2 = this.cy;
			int num3 = this.sayWidth + 2;
			int num4 = this.ch;
			if ((num <= 0 || num2 <= 0) && !GameCanvas.panel.isShow)
			{
				return;
			}
			PopUp.paintPopUp(g, num, num2, num3, num4, 16777215, false);
			if (this.c != null)
			{
				int num5 = (GameCanvas.gameTick % 10 <= 2) ? 1 : 0;
				SmallImage.drawSmallImage(g, this.c.avatar, this.cx + 14, this.cy + num5, 0, StaticObj.BOTTOM_LEFT);
			}
			if (this.iconID != 0)
			{
				int num5 = (GameCanvas.gameTick % 10 <= 2) ? 1 : 0;
				SmallImage.drawSmallImage(g, this.iconID, this.cx + num3 / 2, this.cy + this.ch - 15 + num5, 0, StaticObj.VCENTER_HCENTER);
			}
			if (ChatPopup.scr != null)
			{
				g.setClip(num, num2, num3, num4 - 16);
				g.translate(0, -ChatPopup.scr.cmy);
			}
			int tx = 0;
			int ty = 0;
			if (this.isClip)
			{
				tx = g.getTranslateX();
				ty = g.getTranslateY();
				g.setClip(num, num2 + 1, num3, num4 - 17);
				g.translate(0, -ChatPopup.cmyText);
			}
			int num6 = -1;
			for (int i = 0; i < this.says.Length; i++)
			{
				if (this.says[i].StartsWith("--"))
				{
					g.setColor(0);
					g.fillRect(num + 10, this.cy + this.sayRun + i * 12 + 6, num3 - 20, 1);
				}
				else
				{
					mFont mFont2 = mFont.tahoma_7;
					int num7 = 2;
					string st = this.says[i];
					int num8;
					if (this.says[i].StartsWith("|"))
					{
						string[] array = Res.split(this.says[i], "|", 0);
						if (array.Length == 3)
						{
							st = array[2];
						}
						if (array.Length == 4)
						{
							st = array[3];
							num7 = int.Parse(array[2]);
						}
						num8 = int.Parse(array[1]);
						num6 = num8;
					}
					else
					{
						num8 = num6;
					}
					switch (num8)
					{
					case -1:
						mFont2 = mFont.tahoma_7;
						break;
					case 0:
						mFont2 = mFont.tahoma_7b_dark;
						break;
					case 1:
						mFont2 = mFont.tahoma_7b_green;
						break;
					case 2:
						mFont2 = mFont.tahoma_7b_blue;
						break;
					case 3:
						mFont2 = mFont.tahoma_7_red;
						break;
					case 4:
						mFont2 = mFont.tahoma_7_green;
						break;
					case 5:
						mFont2 = mFont.tahoma_7_blue;
						break;
					case 7:
						mFont2 = mFont.tahoma_7b_red;
						break;
					case 8:
						mFont2 = mFont.tahoma_7b_yellow;
						break;
					}
					if (this.says[i].StartsWith("<"))
					{
						string[] array2 = Res.split(Res.split(this.says[i], "<", 0)[1], ">", 1);
						if (this.second == 0)
						{
							this.second = int.Parse(array2[1]);
						}
						else
						{
							ChatPopup.curr = mSystem.currentTimeMillis();
							if (ChatPopup.curr - ChatPopup.last >= 1000L)
							{
								ChatPopup.last = ChatPopup.curr;
								this.second--;
							}
						}
						st = this.second.ToString() + " " + array2[2];
						mFont2.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY + 12, num7);
					}
					else
					{
						if (num7 == 2)
						{
							mFont2.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY + 12, num7);
						}
						if (num7 == 1)
						{
							mFont2.drawString(g, st, this.cx + this.sayWidth - 5, this.cy + this.sayRun + i * 12 - this.strY + 12, num7);
						}
					}
				}
			}
			if (this.isClip)
			{
				GameCanvas.resetTrans(g);
				g.translate(tx, ty);
			}
			if (this.maxStarSlot > 4)
			{
				this.nMaxslot_tren = (int)((this.maxStarSlot + 1) / 2);
				this.nMaxslot_duoi = (int)this.maxStarSlot - this.nMaxslot_tren;
				for (int j = 0; j < this.nMaxslot_tren; j++)
				{
					g.drawImage(Panel.imgMaxStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + j * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 17, 3);
				}
				for (int k = 0; k < this.nMaxslot_duoi; k++)
				{
					g.drawImage(Panel.imgMaxStar, num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + k * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 8, 3);
				}
				if (this.starSlot > 0)
				{
					this.imgStar = Panel.imgStar;
					if ((int)this.starSlot >= this.nMaxslot_tren)
					{
						this.nslot_duoi = (int)this.starSlot - this.nMaxslot_tren;
						for (int l = 0; l < this.nMaxslot_tren; l++)
						{
							g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + l * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 17, 3);
						}
						for (int m = 0; m < this.nslot_duoi; m++)
						{
							if (m + this.nMaxslot_tren >= ChatPopup.numSlot)
							{
								this.imgStar = Panel.imgStar8;
							}
							g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + m * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 8, 3);
						}
					}
					else
					{
						for (int n = 0; n < (int)this.starSlot; n++)
						{
							g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + n * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 17, 3);
						}
					}
				}
			}
			else
			{
				for (int num9 = 0; num9 < (int)this.maxStarSlot; num9++)
				{
					g.drawImage(Panel.imgMaxStar, num + num3 / 2 - (int)(this.maxStarSlot * 20 / 2) + num9 * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 13, 3);
				}
				if (this.starSlot > 0)
				{
					for (int num10 = 0; num10 < (int)this.starSlot; num10++)
					{
						g.drawImage(Panel.imgStar, num + num3 / 2 - (int)(this.maxStarSlot * 20 / 2) + num10 * 20 + mGraphics.getImageWidth(Panel.imgStar), num2 + num4 - 13, 3);
					}
				}
			}
			this.paintCmd(g);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x001D4DEC File Offset: 0x001D2FEC
		public void paintRada(mGraphics g, int cmyText)
		{
			int num = this.cx;
			int num2 = this.cy;
			int num3 = this.sayWidth;
			int num9 = this.ch;
			int num4 = g.getTranslateX();
			int num5 = g.getTranslateY();
			g.translate(0, -cmyText);
			if ((num <= 0 || num2 <= 0) && !GameCanvas.panel.isShow)
			{
				return;
			}
			int num6 = -1;
			for (int i = 0; i < this.says.Length; i++)
			{
				if (this.says[i].StartsWith("--"))
				{
					g.setColor(16777215);
					g.fillRect(num + 10, this.cy + this.sayRun + i * 12 - 6, num3 - 20, 1);
				}
				else
				{
					mFont mFont2 = mFont.tahoma_7_white;
					int num7 = 2;
					string st = this.says[i];
					int num8;
					if (this.says[i].StartsWith("|"))
					{
						string[] array = Res.split(this.says[i], "|", 0);
						if (array.Length == 3)
						{
							st = array[2];
						}
						if (array.Length == 4)
						{
							st = array[3];
							num7 = int.Parse(array[2]);
						}
						num8 = int.Parse(array[1]);
						num6 = num8;
					}
					else
					{
						num8 = num6;
					}
					switch (num8)
					{
					case -1:
						mFont2 = mFont.tahoma_7_white;
						break;
					case 0:
						mFont2 = mFont.tahoma_7b_white;
						break;
					case 1:
						mFont2 = mFont.tahoma_7b_green;
						break;
					case 2:
						mFont2 = mFont.tahoma_7b_red;
						break;
					}
					if (this.says[i].StartsWith("<"))
					{
						string[] array2 = Res.split(Res.split(this.says[i], "<", 0)[1], ">", 1);
						if (this.second == 0)
						{
							this.second = int.Parse(array2[1]);
						}
						else
						{
							ChatPopup.curr = mSystem.currentTimeMillis();
							if (ChatPopup.curr - ChatPopup.last >= 1000L)
							{
								ChatPopup.last = ChatPopup.curr;
								this.second--;
							}
						}
						st = this.second.ToString() + " " + array2[2];
						mFont2.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY, num7);
					}
					else
					{
						if (num7 == 2)
						{
							mFont2.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY, num7);
						}
						if (num7 == 1)
						{
							mFont2.drawString(g, st, this.cx + this.sayWidth - 5, this.cy + this.sayRun + i * 12 - this.strY, num7);
						}
					}
				}
			}
			GameCanvas.resetTrans(g);
			g.translate(num4, num5);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x001D50C8 File Offset: 0x001D32C8
		private void doKeyText(int type)
		{
			ChatPopup.cmyText += 12 * type;
			if (ChatPopup.cmyText < 0)
			{
				ChatPopup.cmyText = 0;
			}
			if (ChatPopup.cmyText > this.lim)
			{
				ChatPopup.cmyText = this.lim;
			}
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x001D5100 File Offset: 0x001D3300
		public void updateKey()
		{
			if (this.isClip)
			{
				if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
					this.doKeyText(1);
				}
				if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
					this.doKeyText(-1);
				}
				if (GameCanvas.isPointerHoldIn(this.cx, 0, this.sayWidth + 2, this.ch))
				{
					if (GameCanvas.isPointerMove)
					{
						if (this.pyy == 0)
						{
							this.pyy = GameCanvas.py;
						}
						this.pxx = this.pyy - GameCanvas.py;
						if (this.pxx != 0)
						{
							ChatPopup.cmyText += this.pxx;
							this.pyy = GameCanvas.py;
						}
						if (ChatPopup.cmyText < 0)
						{
							ChatPopup.cmyText = 0;
						}
						if (ChatPopup.cmyText > this.lim)
						{
							ChatPopup.cmyText = this.lim;
						}
					}
					else
					{
						this.pyy = 0;
						this.pyy = 0;
					}
				}
			}
			if (ChatPopup.scr != null)
			{
				if (GameCanvas.isTouch)
				{
					ChatPopup.scr.updateKey();
				}
				if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
				{
					ChatPopup.scr.cmtoY -= 12;
					if (ChatPopup.scr.cmtoY < 0)
					{
						ChatPopup.scr.cmtoY = 0;
					}
				}
				if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
					ChatPopup.scr.cmtoY += 12;
					if (ChatPopup.scr.cmtoY > ChatPopup.scr.cmyLim)
					{
						ChatPopup.scr.cmtoY = ChatPopup.scr.cmyLim;
					}
				}
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center))
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				mScreen.keyTouch = -1;
				if (this.cmdNextLine != null)
				{
					this.cmdNextLine.performAction();
				}
				else if (this.cmdMsg1 != null)
				{
					this.cmdMsg1.performAction();
				}
				else if (this.cmdMsg2 != null)
				{
					this.cmdMsg2.performAction();
				}
			}
			if (ChatPopup.scr == null || !ChatPopup.scr.pointerIsDowning)
			{
				if (this.cmdMsg1 != null && (GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.cmdMsg1)))
				{
					GameCanvas.keyPressed[12] = false;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					GameCanvas.isPointerClick = false;
					GameCanvas.isPointerJustRelease = false;
					this.cmdMsg1.performAction();
					mScreen.keyTouch = -1;
				}
				if (this.cmdMsg2 != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.cmdMsg2)))
				{
					GameCanvas.keyPressed[13] = false;
					GameCanvas.isPointerClick = false;
					GameCanvas.isPointerJustRelease = false;
					this.cmdMsg2.performAction();
					mScreen.keyTouch = -1;
				}
			}
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x001D541C File Offset: 0x001D361C
		public void paintCmd(mGraphics g)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintz.paintTabSoft(g);
			if (this.cmdNextLine != null)
			{
				GameCanvas.paintz.paintCmdBar(g, null, this.cmdNextLine, null);
			}
			if (this.cmdMsg1 != null)
			{
				GameCanvas.paintz.paintCmdBar(g, this.cmdMsg1, null, this.cmdMsg2);
			}
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x001D5498 File Offset: 0x001D3698
		public void perform(int idAction, object p)
		{
			if (idAction == 1000)
			{
				try
				{
					string url = (string)p;
					GameMidlet.instance.platformRequest(url);
				}
				catch (Exception)
				{
				}
				if (!Main.isPC)
				{
					GameMidlet.instance.notifyDestroyed();
				}
				else
				{
					idAction = 1001;
				}
				GameCanvas.endDlg();
			}
			if (idAction == 1001)
			{
				ChatPopup.scr = null;
				Char.chatPopup = null;
				ChatPopup.serverChatPopUp = null;
				GameScr.info1.isUpdate = true;
				Char.isLockKey = false;
				if (ChatPopup.isHavePetNpc)
				{
					GameScr.info1.info.time = 0;
					GameScr.info1.info.info.speed = 10;
				}
			}
			if (idAction != 8000 || ChatPopup.performDelay > 0)
			{
				return;
			}
			int num = ChatPopup.currChatPopup.currentLine;
			num++;
			if (num < ChatPopup.currChatPopup.lines.Length)
			{
				ChatPopup chatPopup = ChatPopup.addChatPopup(ChatPopup.currChatPopup.lines[num], ChatPopup.currChatPopup.delay, ChatPopup.currChatPopup.c);
				chatPopup.currentLine = num;
				chatPopup.lines = ChatPopup.currChatPopup.lines;
				chatPopup.cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
				ChatPopup.currChatPopup = chatPopup;
				return;
			}
			Char.chatPopup = null;
			ChatPopup.currChatPopup = null;
			GameScr.info1.isUpdate = true;
			Char.isLockKey = false;
			if (ChatPopup.nextMultiChatPopUp != null)
			{
				ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
				ChatPopup.nextMultiChatPopUp = null;
				ChatPopup.nextChar = null;
				return;
			}
			if (!ChatPopup.isHavePetNpc)
			{
				return;
			}
			GameScr.info1.info.time = 0;
			for (int i = 0; i < GameScr.info1.info.infoWaitToShow.size(); i++)
			{
				if (((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed == 10000000)
				{
					((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed = 10;
				}
			}
		}

		// Token: 0x04003A2F RID: 14895
		public int sayWidth = 100;

		// Token: 0x04003A30 RID: 14896
		public int delay;

		// Token: 0x04003A31 RID: 14897
		public int sayRun;

		// Token: 0x04003A32 RID: 14898
		public string[] says;

		// Token: 0x04003A33 RID: 14899
		public int cx;

		// Token: 0x04003A34 RID: 14900
		public int cy;

		// Token: 0x04003A35 RID: 14901
		public int ch;

		// Token: 0x04003A36 RID: 14902
		public int cmx;

		// Token: 0x04003A37 RID: 14903
		public int cmy;

		// Token: 0x04003A38 RID: 14904
		public int lim;

		// Token: 0x04003A39 RID: 14905
		public Npc c;

		// Token: 0x04003A3A RID: 14906
		private bool outSide;

		// Token: 0x04003A3B RID: 14907
		public static long curr;

		// Token: 0x04003A3C RID: 14908
		public static long last;

		// Token: 0x04003A3D RID: 14909
		private int currentLine;

		// Token: 0x04003A3E RID: 14910
		private string[] lines;

		// Token: 0x04003A3F RID: 14911
		public Command cmdNextLine;

		// Token: 0x04003A40 RID: 14912
		public Command cmdMsg1;

		// Token: 0x04003A41 RID: 14913
		public Command cmdMsg2;

		// Token: 0x04003A42 RID: 14914
		public static ChatPopup currChatPopup;

		// Token: 0x04003A43 RID: 14915
		public static ChatPopup serverChatPopUp;

		// Token: 0x04003A44 RID: 14916
		public static string nextMultiChatPopUp;

		// Token: 0x04003A45 RID: 14917
		public static Npc nextChar;

		// Token: 0x04003A46 RID: 14918
		public bool isShopDetail;

		// Token: 0x04003A47 RID: 14919
		public sbyte starSlot;

		// Token: 0x04003A48 RID: 14920
		public sbyte maxStarSlot;

		// Token: 0x04003A49 RID: 14921
		public static Scroll scr;

		// Token: 0x04003A4A RID: 14922
		public static bool isHavePetNpc;

		// Token: 0x04003A4B RID: 14923
		public int mH;

		// Token: 0x04003A4C RID: 14924
		public static int performDelay;

		// Token: 0x04003A4D RID: 14925
		public int dx;

		// Token: 0x04003A4E RID: 14926
		public int dy;

		// Token: 0x04003A4F RID: 14927
		public int second;

		// Token: 0x04003A50 RID: 14928
		public static int numSlot = 7;

		// Token: 0x04003A51 RID: 14929
		private int nMaxslot_duoi;

		// Token: 0x04003A52 RID: 14930
		private int nMaxslot_tren;

		// Token: 0x04003A53 RID: 14931
		private int nslot_duoi;

		// Token: 0x04003A54 RID: 14932
		private Image imgStar;

		// Token: 0x04003A55 RID: 14933
		public int strY;

		// Token: 0x04003A56 RID: 14934
		private int iconID;

		// Token: 0x04003A57 RID: 14935
		public bool isClip;

		// Token: 0x04003A58 RID: 14936
		public static int cmyText;

		// Token: 0x04003A59 RID: 14937
		private int pxx;

		// Token: 0x04003A5A RID: 14938
		private int pyy;
	}
}
