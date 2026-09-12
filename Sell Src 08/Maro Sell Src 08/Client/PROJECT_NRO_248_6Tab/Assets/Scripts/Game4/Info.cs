using System;

namespace Game4
{
	// Token: 0x020001F9 RID: 505
	public class Info : IActionListener
	{
		// Token: 0x060016C3 RID: 5827 RVA: 0x0016E728 File Offset: 0x0016C928
		public void hide()
		{
			this.says = null;
			this.infoWaitToShow.removeAllElements();
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0016E73C File Offset: 0x0016C93C
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

		// Token: 0x060016C5 RID: 5829 RVA: 0x0016ED08 File Offset: 0x0016CF08
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

		// Token: 0x060016C6 RID: 5830 RVA: 0x0016ED90 File Offset: 0x0016CF90
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

		// Token: 0x060016C7 RID: 5831 RVA: 0x0016EEC4 File Offset: 0x0016D0C4
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

		// Token: 0x060016C8 RID: 5832 RVA: 0x0016F08F File Offset: 0x0016D28F
		public void perform(int idAction, object p)
		{
			if (idAction == 1000)
			{
				ChatTextField.gI().startChat(GameScr.gI(), mResources.chat_player);
			}
		}

		// Token: 0x04002C09 RID: 11273
		public MyVector infoWaitToShow = new MyVector();

		// Token: 0x04002C0A RID: 11274
		public InfoItem info;

		// Token: 0x04002C0B RID: 11275
		public int p1 = 5;

		// Token: 0x04002C0C RID: 11276
		public int p2;

		// Token: 0x04002C0D RID: 11277
		public int p3;

		// Token: 0x04002C0E RID: 11278
		public int x;

		// Token: 0x04002C0F RID: 11279
		public int strWidth;

		// Token: 0x04002C10 RID: 11280
		public int limLeft = 2;

		// Token: 0x04002C11 RID: 11281
		public int hI = 20;

		// Token: 0x04002C12 RID: 11282
		public int xChar;

		// Token: 0x04002C13 RID: 11283
		public int yChar;

		// Token: 0x04002C14 RID: 11284
		public int sayWidth = 100;

		// Token: 0x04002C15 RID: 11285
		public int sayRun;

		// Token: 0x04002C16 RID: 11286
		public string[] says;

		// Token: 0x04002C17 RID: 11287
		public int cx;

		// Token: 0x04002C18 RID: 11288
		public int cy;

		// Token: 0x04002C19 RID: 11289
		public int ch;

		// Token: 0x04002C1A RID: 11290
		public bool outSide;

		// Token: 0x04002C1B RID: 11291
		public int f;

		// Token: 0x04002C1C RID: 11292
		public int tF;

		// Token: 0x04002C1D RID: 11293
		public Image img;

		// Token: 0x04002C1E RID: 11294
		public static Image gocnhon = GameCanvas.loadImage("/mainImage/myTexture2dgocnhon.png");

		// Token: 0x04002C1F RID: 11295
		public int time;

		// Token: 0x04002C20 RID: 11296
		public int timeW;

		// Token: 0x04002C21 RID: 11297
		public int type;

		// Token: 0x04002C22 RID: 11298
		public int X;

		// Token: 0x04002C23 RID: 11299
		public int Y;

		// Token: 0x04002C24 RID: 11300
		public int W;

		// Token: 0x04002C25 RID: 11301
		public int H;
	}
}
