using System;
using System.Threading;
using Game5.Mod.XMAP;

namespace Game5
{
	// Token: 0x02000124 RID: 292
	public class InfoMe
	{
		// Token: 0x06000D2D RID: 3373 RVA: 0x000DA1E4 File Offset: 0x000D83E4
		public InfoMe()
		{
			for (int i = 0; i < this.charId.Length; i++)
			{
				this.charId[i] = new int[3];
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000DA236 File Offset: 0x000D8436
		public static InfoMe gI()
		{
			if (InfoMe.me == null)
			{
				InfoMe.me = new InfoMe();
			}
			return InfoMe.me;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000DA250 File Offset: 0x000D8450
		public void loadCharId()
		{
			for (int i = 0; i < this.charId.Length; i++)
			{
				this.charId[i] = new int[3];
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000DA280 File Offset: 0x000D8480
		public void paint(mGraphics g)
		{
			if ((this.Equals(GameScr.info2) && GameScr.gI().isVS()) || (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null) || (!GameScr.isPaint || (GameCanvas.currentScreen != GameScr.gI() && GameCanvas.currentScreen != CrackBallScr.gI())) || ChatPopup.serverChatPopUp != null || !this.isUpdate || Char.ischangingMap || (GameCanvas.panel.isShow && this.Equals(GameScr.info2)))
			{
				return;
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (this.info != null)
			{
				this.info.paint(g, this.cmx, this.cmy, this.dir);
				if (this.info.info != null && this.info.info.charInfo != null && this.cmdChat == null)
				{
					bool isTouch = GameCanvas.isTouch;
				}
				if (this.info.info != null && this.info.info.charInfo != null)
				{
					Command command = this.cmdChat;
				}
			}
			if (this.info.info != null && this.info.info.charInfo == null && this.charId != null)
			{
				SmallImage.drawSmallImage(g, this.charId[Char.myCharz().cgender][this.f], this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000DA436 File Offset: 0x000D8636
		public void hide()
		{
			this.info.hide();
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000DA444 File Offset: 0x000D8644
		public void moveCamera()
		{
			if (this.cmy != this.cmtoY)
			{
				this.cmvy = this.cmtoY - this.cmy << 2;
				this.cmdy += this.cmvy;
				this.cmy += this.cmdy >> 4;
				this.cmdy &= 15;
			}
			if (this.cmx != this.cmtoX)
			{
				this.cmvx = this.cmtoX - this.cmx << 2;
				this.cmdx += this.cmvx;
				this.cmx += this.cmdx >> 4;
				this.cmdx &= 15;
			}
			this.tF++;
			if (this.tF == 5)
			{
				this.tF = 0;
				if (this.f == 0)
				{
					this.f = 1;
					return;
				}
				this.f = 0;
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000DA53A File Offset: 0x000D873A
		public void doClick(int t)
		{
			this.timeDelay = t;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000DA544 File Offset: 0x000D8744
		public void update()
		{
			if (this.info != null && this.info.infoWaitToShow != null && this.info.infoWaitToShow.size() == 0 && this.cmy != -40)
			{
				this.info.timeW--;
				if (this.info.timeW <= 0)
				{
					this.cmy = -40;
					this.info.time = 0;
					this.info.infoWaitToShow.removeAllElements();
					this.info.says = null;
					this.info.timeW = 200;
				}
			}
			if ((this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null) || !this.isUpdate)
			{
				return;
			}
			this.moveCamera();
			if (this.info == null || (this.info != null && this.info.info == null))
			{
				return;
			}
			if (!this.isDone)
			{
				if (this.timeDelay > 0)
				{
					this.timeDelay--;
					if (this.timeDelay == 0)
					{
						GameCanvas.panel.setTypeMessage();
						GameCanvas.panel.show();
					}
				}
				if (GameCanvas.gameTick % 3 == 0)
				{
					if (Char.myCharz().cdir == 1)
					{
						this.cmtoX = Char.myCharz().cx - 20 - GameScr.cmx;
					}
					if (Char.myCharz().cdir == -1)
					{
						this.cmtoX = Char.myCharz().cx + 20 - GameScr.cmx;
					}
					if (this.cmtoX <= 24)
					{
						this.cmtoX += this.info.sayWidth / 2;
					}
					if (this.cmtoX >= GameCanvas.w - 24)
					{
						this.cmtoX -= this.info.sayWidth / 2;
					}
					this.cmtoY = Char.myCharz().cy - 40 - GameScr.cmy;
					if (this.info.says != null && this.cmtoY < (this.info.says.Length + 1) * 12 + 10)
					{
						this.cmtoY = (this.info.says.Length + 1) * 12 + 10;
					}
					if (this.info.info.charInfo != null)
					{
						if (GameCanvas.w - 50 > 155 + this.info.W)
						{
							this.cmtoX = GameCanvas.w - 60 - this.info.W / 2;
							this.cmtoY = this.info.H + 10;
						}
						else
						{
							this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
							this.cmtoY = 45 + this.info.H;
							if (GameCanvas.w > GameCanvas.h || GameCanvas.w < 220)
							{
								this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
								this.cmtoY = this.info.H + 10;
							}
						}
					}
				}
				if (this.cmx > Char.myCharz().cx - GameScr.cmx)
				{
					this.dir = -1;
				}
				else
				{
					this.dir = 1;
				}
			}
			if (this.info.info == null)
			{
				return;
			}
			if (this.info.infoWaitToShow.size() > 1)
			{
				if (this.info.info.timeCount == 0)
				{
					this.info.time++;
					if (this.info.time >= this.info.info.speed)
					{
						this.info.time = 0;
						this.info.infoWaitToShow.removeElementAt(0);
						InfoItem infoItem = (InfoItem)this.info.infoWaitToShow.firstElement();
						this.info.info = infoItem;
						this.info.getInfo();
					}
					return;
				}
				this.info.info.curr = mSystem.currentTimeMillis();
				if (this.info.info.curr - this.info.info.last >= 100L)
				{
					this.info.info.last = mSystem.currentTimeMillis();
					this.info.info.timeCount--;
				}
				if (this.info.info.timeCount == 0)
				{
					this.info.infoWaitToShow.removeElementAt(0);
					if (this.info.infoWaitToShow.size() != 0)
					{
						InfoItem infoItem2 = (InfoItem)this.info.infoWaitToShow.firstElement();
						this.info.info = infoItem2;
						this.info.getInfo();
						return;
					}
				}
			}
			else
			{
				if (this.info.infoWaitToShow.size() != 1)
				{
					return;
				}
				if (this.info.info.timeCount == 0)
				{
					this.info.time++;
					if (this.info.time >= this.info.info.speed)
					{
						this.isDone = true;
					}
					if (this.info.time == this.info.info.speed)
					{
						this.cmtoY = -40;
						this.cmtoX = Char.myCharz().cx - GameScr.cmx + ((Char.myCharz().cdir != 1) ? 20 : -20);
					}
					if (this.info.time >= this.info.info.speed + 20)
					{
						this.info.time = 0;
						this.info.infoWaitToShow.removeAllElements();
						this.info.says = null;
						this.info.timeW = 200;
						return;
					}
				}
				else
				{
					this.info.info.curr = mSystem.currentTimeMillis();
					if (this.info.info.curr - this.info.info.last >= 100L)
					{
						this.info.info.last = mSystem.currentTimeMillis();
						this.info.info.timeCount--;
					}
					if (this.info.info.timeCount == 0)
					{
						this.isDone = true;
						this.cmtoY = -40;
						this.cmtoX = Char.myCharz().cx - GameScr.cmx + ((Char.myCharz().cdir != 1) ? 20 : -20);
						this.info.time = 0;
						this.info.infoWaitToShow.removeAllElements();
						this.info.says = null;
						this.cmdChat = null;
					}
				}
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000DABDF File Offset: 0x000D8DDF
		public void addInfoWithChar(string s, Char c, bool isChatServer)
		{
			this.playerID = c.charID;
			this.info.addInfo(s, 3, c, isChatServer);
			this.isDone = false;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000DAC04 File Offset: 0x000D8E04
		public void addInfo(string s, int Type)
		{
			s = Res.changeString(s);
			AutoXmap.Info(s);
			if (ModFunc.GI().isAutoVQMM)
			{
				if (s.ToLower().Contains("hành trang đã đầy"))
				{
					ModFunc.GI().isAutoVQMM = false;
					s = "Hành trang đầy, đã tắt Auto VQMM (1)";
				}
				else if (s.ToLower().Contains("rương") && s.ToLower().Contains(" đã đầy"))
				{
					new Thread(new ThreadStart(ModFunc.GI().CollectAllThuongDe)).Start();
					return;
				}
			}
			if (this.info.infoWaitToShow.size() > 0 && s.Equals(((InfoItem)this.info.infoWaitToShow.lastElement()).s))
			{
				return;
			}
			if (this.info.infoWaitToShow.size() > 10)
			{
				for (int i = 0; i < 5; i++)
				{
					this.info.infoWaitToShow.removeElementAt(0);
				}
			}
			Char cInfo = null;
			this.info.addInfo(s, Type, cInfo, false);
			if (this.info.infoWaitToShow.size() == 1)
			{
				this.cmy = 0;
				this.cmx = Char.myCharz().cx - GameScr.cmx + ((Char.myCharz().cdir != 1) ? 20 : -20);
			}
			this.isDone = false;
		}

		// Token: 0x040019B6 RID: 6582
		public static InfoMe me;

		// Token: 0x040019B7 RID: 6583
		public int[][] charId = new int[3][];

		// Token: 0x040019B8 RID: 6584
		public Info info = new Info();

		// Token: 0x040019B9 RID: 6585
		public int dir;

		// Token: 0x040019BA RID: 6586
		public int f;

		// Token: 0x040019BB RID: 6587
		public int tF;

		// Token: 0x040019BC RID: 6588
		public int cmtoY;

		// Token: 0x040019BD RID: 6589
		public int cmy;

		// Token: 0x040019BE RID: 6590
		public int cmdy;

		// Token: 0x040019BF RID: 6591
		public int cmvy;

		// Token: 0x040019C0 RID: 6592
		public int cmtoX;

		// Token: 0x040019C1 RID: 6593
		public int cmx;

		// Token: 0x040019C2 RID: 6594
		public int cmdx;

		// Token: 0x040019C3 RID: 6595
		public int cmvx;

		// Token: 0x040019C4 RID: 6596
		public bool isDone;

		// Token: 0x040019C5 RID: 6597
		public bool isUpdate = true;

		// Token: 0x040019C6 RID: 6598
		public int timeDelay;

		// Token: 0x040019C7 RID: 6599
		public int playerID;

		// Token: 0x040019C8 RID: 6600
		public Command cmdChat;
	}
}
