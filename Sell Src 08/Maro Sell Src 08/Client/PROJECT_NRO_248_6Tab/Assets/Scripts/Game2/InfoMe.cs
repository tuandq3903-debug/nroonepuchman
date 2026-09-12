using System;
using System.Threading;
using Game2.Mod.XMAP;

namespace Game2
{
	// Token: 0x020003AC RID: 940
	public class InfoMe
	{
		// Token: 0x06002A19 RID: 10777 RVA: 0x002993D0 File Offset: 0x002975D0
		public InfoMe()
		{
			for (int i = 0; i < this.charId.Length; i++)
			{
				this.charId[i] = new int[3];
			}
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x00299422 File Offset: 0x00297622
		public static InfoMe gI()
		{
			if (InfoMe.me == null)
			{
				InfoMe.me = new InfoMe();
			}
			return InfoMe.me;
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x0029943C File Offset: 0x0029763C
		public void loadCharId()
		{
			for (int i = 0; i < this.charId.Length; i++)
			{
				this.charId[i] = new int[3];
			}
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x0029946C File Offset: 0x0029766C
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

		// Token: 0x06002A1D RID: 10781 RVA: 0x00299622 File Offset: 0x00297822
		public void hide()
		{
			this.info.hide();
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x00299630 File Offset: 0x00297830
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

		// Token: 0x06002A1F RID: 10783 RVA: 0x00299726 File Offset: 0x00297926
		public void doClick(int t)
		{
			this.timeDelay = t;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00299730 File Offset: 0x00297930
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

		// Token: 0x06002A21 RID: 10785 RVA: 0x00299DCB File Offset: 0x00297FCB
		public void addInfoWithChar(string s, Char c, bool isChatServer)
		{
			this.playerID = c.charID;
			this.info.addInfo(s, 3, c, isChatServer);
			this.isDone = false;
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00299DF0 File Offset: 0x00297FF0
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

		// Token: 0x04005133 RID: 20787
		public static InfoMe me;

		// Token: 0x04005134 RID: 20788
		public int[][] charId = new int[3][];

		// Token: 0x04005135 RID: 20789
		public Info info = new Info();

		// Token: 0x04005136 RID: 20790
		public int dir;

		// Token: 0x04005137 RID: 20791
		public int f;

		// Token: 0x04005138 RID: 20792
		public int tF;

		// Token: 0x04005139 RID: 20793
		public int cmtoY;

		// Token: 0x0400513A RID: 20794
		public int cmy;

		// Token: 0x0400513B RID: 20795
		public int cmdy;

		// Token: 0x0400513C RID: 20796
		public int cmvy;

		// Token: 0x0400513D RID: 20797
		public int cmtoX;

		// Token: 0x0400513E RID: 20798
		public int cmx;

		// Token: 0x0400513F RID: 20799
		public int cmdx;

		// Token: 0x04005140 RID: 20800
		public int cmvx;

		// Token: 0x04005141 RID: 20801
		public bool isDone;

		// Token: 0x04005142 RID: 20802
		public bool isUpdate = true;

		// Token: 0x04005143 RID: 20803
		public int timeDelay;

		// Token: 0x04005144 RID: 20804
		public int playerID;

		// Token: 0x04005145 RID: 20805
		public Command cmdChat;
	}
}
