using System;

namespace Game2
{
	// Token: 0x020003FF RID: 1023
	public class ServerScr : mScreen, IActionListener
	{
		// Token: 0x06002DD5 RID: 11733 RVA: 0x002D4B84 File Offset: 0x002D2D84
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

		// Token: 0x06002DD6 RID: 11734 RVA: 0x002D4BE0 File Offset: 0x002D2DE0
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

		// Token: 0x06002DD7 RID: 11735 RVA: 0x002D4C94 File Offset: 0x002D2E94
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

		// Token: 0x06002DD8 RID: 11736 RVA: 0x002D4DD8 File Offset: 0x002D2FD8
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

		// Token: 0x06002DD9 RID: 11737 RVA: 0x002D4EA4 File Offset: 0x002D30A4
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

		// Token: 0x06002DDA RID: 11738 RVA: 0x002D4EFC File Offset: 0x002D30FC
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

		// Token: 0x06002DDB RID: 11739 RVA: 0x002D503C File Offset: 0x002D323C
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

		// Token: 0x040059B0 RID: 22960
		private int mainSelect;

		// Token: 0x040059B1 RID: 22961
		private MyVector vecServer = new MyVector();

		// Token: 0x040059B2 RID: 22962
		private Command cmdCheck;

		// Token: 0x040059B3 RID: 22963
		public const int icmd = 100;

		// Token: 0x040059B4 RID: 22964
		private int wc;

		// Token: 0x040059B5 RID: 22965
		private int hc;

		// Token: 0x040059B6 RID: 22966
		private int w2c;

		// Token: 0x040059B7 RID: 22967
		private int numw;

		// Token: 0x040059B8 RID: 22968
		private int numh;

		// Token: 0x040059B9 RID: 22969
		private Command cmdGlobal;

		// Token: 0x040059BA RID: 22970
		private Command cmdVietNam;
	}
}
