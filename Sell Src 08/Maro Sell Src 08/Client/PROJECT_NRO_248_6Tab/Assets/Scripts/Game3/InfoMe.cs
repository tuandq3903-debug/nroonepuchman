using System;
using System.Threading;
using Game3.Mod.XMAP;

namespace Game3
{
	// Token: 0x020002D4 RID: 724
	public class InfoMe
	{
		// Token: 0x06002075 RID: 8309 RVA: 0x0020432C File Offset: 0x0020252C
		public InfoMe()
		{
			for (int i = 0; i < this.charId.Length; i++)
			{
				this.charId[i] = new int[3];
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0020437E File Offset: 0x0020257E
		public static InfoMe gI()
		{
			if (InfoMe.me == null)
			{
				InfoMe.me = new InfoMe();
			}
			return InfoMe.me;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x00204398 File Offset: 0x00202598
		public void loadCharId()
		{
			for (int i = 0; i < this.charId.Length; i++)
			{
				this.charId[i] = new int[3];
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x002043C8 File Offset: 0x002025C8
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

		// Token: 0x06002079 RID: 8313 RVA: 0x0020457E File Offset: 0x0020277E
		public void hide()
		{
			this.info.hide();
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x0020458C File Offset: 0x0020278C
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

		// Token: 0x0600207B RID: 8315 RVA: 0x00204682 File Offset: 0x00202882
		public void doClick(int t)
		{
			this.timeDelay = t;
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0020468C File Offset: 0x0020288C
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

		// Token: 0x0600207D RID: 8317 RVA: 0x00204D27 File Offset: 0x00202F27
		public void addInfoWithChar(string s, Char c, bool isChatServer)
		{
			this.playerID = c.charID;
			this.info.addInfo(s, 3, c, isChatServer);
			this.isDone = false;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00204D4C File Offset: 0x00202F4C
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

		// Token: 0x04003EB4 RID: 16052
		public static InfoMe me;

		// Token: 0x04003EB5 RID: 16053
		public int[][] charId = new int[3][];

		// Token: 0x04003EB6 RID: 16054
		public Info info = new Info();

		// Token: 0x04003EB7 RID: 16055
		public int dir;

		// Token: 0x04003EB8 RID: 16056
		public int f;

		// Token: 0x04003EB9 RID: 16057
		public int tF;

		// Token: 0x04003EBA RID: 16058
		public int cmtoY;

		// Token: 0x04003EBB RID: 16059
		public int cmy;

		// Token: 0x04003EBC RID: 16060
		public int cmdy;

		// Token: 0x04003EBD RID: 16061
		public int cmvy;

		// Token: 0x04003EBE RID: 16062
		public int cmtoX;

		// Token: 0x04003EBF RID: 16063
		public int cmx;

		// Token: 0x04003EC0 RID: 16064
		public int cmdx;

		// Token: 0x04003EC1 RID: 16065
		public int cmvx;

		// Token: 0x04003EC2 RID: 16066
		public bool isDone;

		// Token: 0x04003EC3 RID: 16067
		public bool isUpdate = true;

		// Token: 0x04003EC4 RID: 16068
		public int timeDelay;

		// Token: 0x04003EC5 RID: 16069
		public int playerID;

		// Token: 0x04003EC6 RID: 16070
		public Command cmdChat;
	}
}
