using System;

namespace Game6
{
	// Token: 0x02000049 RID: 73
	public class Info : IActionListener
	{
		// Token: 0x0600037B RID: 891 RVA: 0x000444D8 File Offset: 0x000426D8
		public void hide()
		{
			this.says = null;
			this.infoWaitToShow.removeAllElements();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000444EC File Offset: 0x000426EC
		public void paint(mGraphics g, int x, int y, int dir)
		{
			if (this.infoWaitToShow.size() == 0)
			{
				return;
			}
			g.translate(x, y);
			if (this.says != null && this.says.Length != 0 && this.type != 1)
			{
				if (this.outSide)
				{
					this.cx -= GameScr.cmx;
					this.cy -= GameScr.cmy;
					this.cy += 35;
				}
				int num = (mGraphics.zoomLevel != 1) ? 10 : 0;
				if (this.info.charInfo == null)
				{
					PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H, 16777215, false);
				}
				else
				{
					mSystem.paintPopUp2(g, this.X - 23, this.Y - num / 2, this.W + 15, this.H + ((!GameCanvas.isTouch) ? 14 : 0) + num);
				}
				if (this.info.charInfo == null)
				{
					g.drawRegion(Info.gocnhon, 0, 0, 9, 8, (dir != 1) ? 2 : 0, this.cx - 3 + ((dir != 1) ? 20 : -15), this.cy - this.ch - 20 + this.sayRun + 2, mGraphics.TOP | mGraphics.HCENTER);
				}
				int num2 = -1;
				for (int i = 0; i < this.says.Length; i++)
				{
					mFont mFont2 = mFont.tahoma_7;
					string text = this.says[i];
					int num3;
					if (this.says[i].StartsWith("|"))
					{
						string[] array = Res.split(this.says[i], "|", 0);
						if (array.Length == 3)
						{
							text = array[2];
						}
						if (array.Length == 4)
						{
							text = array[3];
							int.Parse(array[2]);
						}
						num3 = int.Parse(array[1]);
						num2 = num3;
					}
					else
					{
						num3 = num2;
					}
					switch (num3)
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
					}
					if (this.info.charInfo == null)
					{
						mFont2.drawString(g, text, this.cx, this.cy - this.ch - 15 + this.sayRun + i * 12 - this.says.Length * 12 - 9, 2);
					}
					else
					{
						int num4 = this.X - 23;
						int num5 = this.Y - num / 2;
						int num6 = (mSystem.clientType != 1) ? (this.W + 25) : (this.W + 28);
						int num7 = this.H + ((!GameCanvas.isTouch) ? 14 : 0) + num;
						g.setColor(4465169);
						g.fillRect(num4, num5 + num7, num6, 2);
						int num8 = this.info.timeCount * num6 / this.info.maxTime;
						if (num8 < 0)
						{
							num8 = 0;
						}
						g.setColor(43758);
						g.fillRect(num4, num5 + num7, num8, 2);
						if (this.info.timeCount == 0)
						{
							return;
						}
						this.info.charInfo.paintCharBody(g, this.X - 7, this.Y + this.H - 3, 1, 0, true);
						if (mGraphics.zoomLevel == 1)
						{
							((!this.info.isChatServer) ? mFont.tahoma_7b_greenSmall : mFont.tahoma_7b_yellowSmall2).drawString(g, (this.info.charInfo.isTichXanh ? "     " : string.Empty) + this.info.charInfo.cName, this.X + 12, this.Y + 3, 0);
							if (this.info.charInfo.isTichXanh)
							{
								ModFunc.PaintTicks(g, this.X + 8, this.Y + 2);
							}
						}
						else
						{
							((!this.info.isChatServer) ? mFont.tahoma_7b_greenSmall : mFont.tahoma_7b_yellowSmall2).drawString(g, (this.info.charInfo.isTichXanh ? "     " : string.Empty) + this.info.charInfo.cName, this.X + 12, this.Y - 3, 0);
							if (this.info.charInfo.isTichXanh)
							{
								ModFunc.PaintTicks(g, this.X + 9, this.Y - 2);
							}
						}
						if (!GameCanvas.isTouch)
						{
							if (!TField.isQwerty)
							{
								mFont.tahoma_7b_green2Small.drawString(g, "Nhấn # để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
							}
							else
							{
								mFont.tahoma_7b_green2Small.drawString(g, "Nhấn Y để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
							}
						}
						if (mGraphics.zoomLevel == 1)
						{
							TextInfo.paint(g, text, this.X + 14, this.Y + this.H / 2 + 2, this.W - 16, this.H, mFont.tahoma_7_whiteSmall);
						}
						else
						{
							string[] array2 = mFont.tahoma_7_whiteSmall.splitFontArray(text, 120);
							for (int j = 0; j < array2.Length; j++)
							{
								mFont.tahoma_7_whiteSmall.drawString(g, array2[j], this.X + 12, this.Y + 12 + j * 12 - 3, 0);
							}
							GameCanvas.resetTrans(g);
						}
					}
				}
				Char charInfo = this.info.charInfo;
			}
			g.translate(-x, -y);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00044AB8 File Offset: 0x00042CB8
		public void update()
		{
			if (this.infoWaitToShow.size() == 0 || this.info.timeCount != 0)
			{
				return;
			}
			this.time++;
			if (this.time >= this.info.speed)
			{
				this.time = 0;
				this.infoWaitToShow.removeElementAt(0);
				if (this.infoWaitToShow.size() != 0)
				{
					InfoItem infoItem = (InfoItem)this.infoWaitToShow.firstElement();
					this.info = infoItem;
					this.getInfo();
				}
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00044B40 File Offset: 0x00042D40
		public void getInfo()
		{
			this.sayWidth = 100;
			if (GameCanvas.w == 128)
			{
				this.sayWidth = 128;
			}
			int num;
			if (this.info.charInfo != null)
			{
				this.says = new string[]
				{
					this.info.s
				};
				num = ((mGraphics.zoomLevel != 1) ? mFont.tahoma_7_whiteSmall.splitFontArray(this.info.s, 120).Length : this.says.Length);
			}
			else
			{
				this.says = mFont.tahoma_7.splitFontArray(this.info.s, this.sayWidth - 10);
				num = this.says.Length;
			}
			this.sayRun = 7;
			this.X = this.cx - this.sayWidth / 2 - 1;
			this.Y = this.cy - this.ch - 15 + this.sayRun - num * 12 - 15;
			this.W = this.sayWidth + 2 + ((this.info.charInfo != null) ? 30 : 0);
			this.H = (num + 1) * 12 + 1 + ((this.info.charInfo != null) ? 6 : 0);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00044C74 File Offset: 0x00042E74
		public void addInfo(string s, int Type, Char cInfo, bool isChatServer)
		{
			this.type = Type;
			if (GameCanvas.w == 128)
			{
				this.limLeft = 1;
			}
			if (this.infoWaitToShow.size() > 10)
			{
				this.infoWaitToShow.removeElementAt(0);
			}
			if (this.infoWaitToShow.size() > 0)
			{
				s.Equals(((InfoItem)this.infoWaitToShow.lastElement()).s);
			}
			InfoItem infoItem = new InfoItem(s);
			if (this.type == 0)
			{
				infoItem.speed = s.Length;
			}
			if (infoItem.speed < 70)
			{
				infoItem.speed = 70;
			}
			if (this.type == 1)
			{
				infoItem.speed = 10000000;
			}
			if (this.type == 3)
			{
				infoItem.speed = 300;
				infoItem.last = mSystem.currentTimeMillis();
				infoItem.maxTime = (infoItem.timeCount = 80);
			}
			if (cInfo != null)
			{
				infoItem.charInfo = cInfo;
				infoItem.isChatServer = isChatServer;
				GameCanvas.panel.addChatMessage(infoItem);
				if (GameCanvas.isTouch && GameCanvas.panel.isViewChatServer)
				{
					GameScr.info2.cmdChat = new Command(mResources.CHAT, this, 1000, infoItem);
				}
			}
			if ((cInfo != null && GameCanvas.panel.isViewChatServer) || cInfo == null)
			{
				this.infoWaitToShow.addElement(infoItem);
			}
			if (this.infoWaitToShow.size() == 1)
			{
				this.info = (InfoItem)this.infoWaitToShow.firstElement();
				this.getInfo();
			}
			if (GameCanvas.isTouch && cInfo != null && GameCanvas.panel.isViewChatServer && GameCanvas.w - 50 > 155 + this.W)
			{
				GameScr.info2.cmdChat.x = GameCanvas.w - this.W - 50;
				GameScr.info2.cmdChat.y = 35;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00044E3F File Offset: 0x0004303F
		public void perform(int idAction, object p)
		{
			if (idAction == 1000)
			{
				ChatTextField.gI().startChat(GameScr.gI(), mResources.chat_player);
			}
		}

		// Token: 0x0400070B RID: 1803
		public MyVector infoWaitToShow = new MyVector();

		// Token: 0x0400070C RID: 1804
		public InfoItem info;

		// Token: 0x0400070D RID: 1805
		public int p1 = 5;

		// Token: 0x0400070E RID: 1806
		public int p2;

		// Token: 0x0400070F RID: 1807
		public int p3;

		// Token: 0x04000710 RID: 1808
		public int x;

		// Token: 0x04000711 RID: 1809
		public int strWidth;

		// Token: 0x04000712 RID: 1810
		public int limLeft = 2;

		// Token: 0x04000713 RID: 1811
		public int hI = 20;

		// Token: 0x04000714 RID: 1812
		public int xChar;

		// Token: 0x04000715 RID: 1813
		public int yChar;

		// Token: 0x04000716 RID: 1814
		public int sayWidth = 100;

		// Token: 0x04000717 RID: 1815
		public int sayRun;

		// Token: 0x04000718 RID: 1816
		public string[] says;

		// Token: 0x04000719 RID: 1817
		public int cx;

		// Token: 0x0400071A RID: 1818
		public int cy;

		// Token: 0x0400071B RID: 1819
		public int ch;

		// Token: 0x0400071C RID: 1820
		public bool outSide;

		// Token: 0x0400071D RID: 1821
		public int f;

		// Token: 0x0400071E RID: 1822
		public int tF;

		// Token: 0x0400071F RID: 1823
		public Image img;

		// Token: 0x04000720 RID: 1824
		public static Image gocnhon = GameCanvas.loadImage("/mainImage/myTexture2dgocnhon.png");

		// Token: 0x04000721 RID: 1825
		public int time;

		// Token: 0x04000722 RID: 1826
		public int timeW;

		// Token: 0x04000723 RID: 1827
		public int type;

		// Token: 0x04000724 RID: 1828
		public int X;

		// Token: 0x04000725 RID: 1829
		public int Y;

		// Token: 0x04000726 RID: 1830
		public int W;

		// Token: 0x04000727 RID: 1831
		public int H;
	}
}
