using System;

namespace Game1
{
	// Token: 0x020004D7 RID: 1239
	public class ServerScr : mScreen, IActionListener
	{
		// Token: 0x06003779 RID: 14201 RVA: 0x00369C28 File Offset: 0x00367E28
		public ServerScr()
		{
			TileMap.bgID = (int)((byte)(mSystem.currentTimeMillis() % 9L));
			if (TileMap.bgID == 5 || TileMap.bgID == 6)
			{
				TileMap.bgID = 4;
			}
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameScr.cmy = 200;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x00369C84 File Offset: 0x00367E84
		public override void switchToMe()
		{
			SoundMn.gI().stopAll();
			base.switchToMe();
			this.cmdGlobal = new Command("VIỆT NAM", this, 98, null);
			this.cmdGlobal.x = 0;
			this.cmdGlobal.y = 0;
			this.cmdVietNam = new Command("GLOBAL", this, 97, null);
			this.cmdVietNam.x = 50;
			this.cmdVietNam.y = 0;
			this.vecServer = new MyVector();
			this.vecServer.addElement(this.cmdGlobal);
			this.vecServer.addElement(this.cmdVietNam);
			this.sort();
			this.cmdGlobal.performAction();
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x00369D38 File Offset: 0x00367F38
		private void sort()
		{
			this.mainSelect = ServerListScreen.ipSelect;
			this.w2c = 5;
			this.wc = 76;
			this.hc = mScreen.cmdH;
			this.numw = 2;
			if (GameCanvas.w > 3 * (this.wc + this.w2c))
			{
				this.numw = 3;
			}
			if (this.vecServer.size() < 3)
			{
				this.numw = 2;
			}
			this.numh = this.vecServer.size() / this.numw + ((this.vecServer.size() % this.numw != 0) ? 1 : 0);
			for (int i = 0; i < this.vecServer.size(); i++)
			{
				Command command = (Command)this.vecServer.elementAt(i);
				if (command != null)
				{
					int x = GameCanvas.hw - this.numw * (this.wc + this.w2c) / 2 + i % this.numw * (this.wc + this.w2c);
					int y = GameCanvas.hh - this.numh * (this.hc + this.w2c) / 2 + i / this.numw * (this.hc + this.w2c);
					command.x = x;
					command.y = y;
				}
			}
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x00369E7C File Offset: 0x0036807C
		public override void update()
		{
			GameScr.cmx++;
			if (GameScr.cmx > GameCanvas.w * 3 + 100)
			{
				GameScr.cmx = 100;
			}
			for (int i = 0; i < this.vecServer.size(); i++)
			{
				Command command = (Command)this.vecServer.elementAt(i);
				if (!GameCanvas.isTouch)
				{
					if (i == this.mainSelect)
					{
						if (GameCanvas.gameTick % 10 < 4)
						{
							command.isFocus = true;
						}
						else
						{
							command.isFocus = false;
						}
						this.cmdCheck = new Command(mResources.SELECT, this, command.idAction, null);
						this.center = this.cmdCheck;
					}
					else
					{
						command.isFocus = false;
					}
				}
				else if (command != null && command.isPointerPressInside())
				{
					command.performAction();
				}
			}
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x00369F48 File Offset: 0x00368148
		public override void paint(mGraphics g)
		{
			GameCanvas.paintBGGameScr(g);
			for (int i = 0; i < this.vecServer.size(); i++)
			{
				if (this.vecServer.elementAt(i) != null)
				{
					((Command)this.vecServer.elementAt(i)).paint(g);
				}
			}
			base.paint(g);
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x00369FA0 File Offset: 0x003681A0
		public override void updateKey()
		{
			base.updateKey();
			int num = this.mainSelect % this.numw;
			int num2 = this.mainSelect / this.numw;
			if (GameCanvas.keyPressed[4])
			{
				if (num > 0)
				{
					this.mainSelect--;
				}
				GameCanvas.keyPressed[4] = false;
			}
			else if (GameCanvas.keyPressed[6])
			{
				if (num < this.numw - 1)
				{
					this.mainSelect++;
				}
				GameCanvas.keyPressed[6] = false;
			}
			else if (GameCanvas.keyPressed[2])
			{
				if (num2 > 0)
				{
					this.mainSelect -= this.numw;
				}
				GameCanvas.keyPressed[2] = false;
			}
			else if (GameCanvas.keyPressed[8])
			{
				if (num2 < this.numh - 1)
				{
					this.mainSelect += this.numw;
				}
				GameCanvas.keyPressed[8] = false;
			}
			if (this.mainSelect < 0)
			{
				this.mainSelect = 0;
			}
			if (this.mainSelect >= this.vecServer.size())
			{
				this.mainSelect = this.vecServer.size() - 1;
			}
			if (GameCanvas.keyPressed[5])
			{
				((Command)this.vecServer.elementAt(num)).performAction();
				GameCanvas.keyPressed[5] = false;
			}
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x0036A0E0 File Offset: 0x003682E0
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 97:
				this.vecServer.removeAllElements();
				for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
				{
					if (ServerListScreen.language[i] != 0)
					{
						this.vecServer.addElement(new Command(ServerListScreen.nameServer[i], this, 100 + i, null));
					}
				}
				this.sort();
				return;
			case 98:
				this.vecServer.removeAllElements();
				for (int j = 0; j < ServerListScreen.nameServer.Length; j++)
				{
					if (ServerListScreen.language[j] == 0)
					{
						this.vecServer.addElement(new Command(ServerListScreen.nameServer[j], this, 100 + j, null));
					}
				}
				this.sort();
				return;
			case 99:
				Session_ME.gI().clearSendingMessage();
				ServerListScreen.ipSelect = this.mainSelect;
				GameCanvas.serverScreen.selectServer();
				GameCanvas.serverScreen.switchToMe();
				return;
			default:
				Session_ME.gI().clearSendingMessage();
				ServerListScreen.ipSelect = idAction - 100;
				GameCanvas.serverScreen.selectServer();
				GameCanvas.serverScreen.switchToMe();
				return;
			}
		}

		// Token: 0x04006C2F RID: 27695
		private int mainSelect;

		// Token: 0x04006C30 RID: 27696
		private MyVector vecServer = new MyVector();

		// Token: 0x04006C31 RID: 27697
		private Command cmdCheck;

		// Token: 0x04006C32 RID: 27698
		public const int icmd = 100;

		// Token: 0x04006C33 RID: 27699
		private int wc;

		// Token: 0x04006C34 RID: 27700
		private int hc;

		// Token: 0x04006C35 RID: 27701
		private int w2c;

		// Token: 0x04006C36 RID: 27702
		private int numw;

		// Token: 0x04006C37 RID: 27703
		private int numh;

		// Token: 0x04006C38 RID: 27704
		private Command cmdGlobal;

		// Token: 0x04006C39 RID: 27705
		private Command cmdVietNam;
	}
}
