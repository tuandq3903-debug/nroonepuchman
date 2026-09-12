using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x020004CE RID: 1230
	public class RadarScr : mScreen
	{
		// Token: 0x06003715 RID: 14101 RVA: 0x00365380 File Offset: 0x00363580
		public RadarScr()
		{
			RadarScr.TYPE_UI = true;
			Image img4 = mSystem.loadImage("/radar/17.png");
			Image img2 = mSystem.loadImage("/radar/3.png");
			Image img3 = mSystem.loadImage("/radar/23.png");
			RadarScr.fraImgFocus = new FrameImage(img4, 28, 28);
			RadarScr.fraImgFocusNone = new FrameImage(img2, 30, 30);
			RadarScr.fraEff = new FrameImage(img3, 11, 11);
			RadarScr.imgUI = mSystem.loadImage("/radar/0.png");
			RadarScr.imgArrow_Left = mSystem.loadImage("/radar/1.png");
			RadarScr.imgArrow_Right = mSystem.loadImage("/radar/2.png");
			RadarScr.imgUIText = mSystem.loadImage("/radar/17.png");
			RadarScr.imgArrow_Down = mSystem.loadImage("/radar/4.png");
			RadarScr.imgLock = mSystem.loadImage("/radar/5.png");
			RadarScr.imgUse_0 = mSystem.loadImage("/radar/6.png");
			RadarScr.imgRank = new Image[7];
			for (int i = 0; i < 7; i++)
			{
				RadarScr.imgRank[i] = mSystem.loadImage("/radar/" + (i + 7).ToString() + ".png");
			}
			RadarScr.imgUse = mSystem.loadImage("/radar/14.png");
			RadarScr.imgBack = mSystem.loadImage("/radar/15.png");
			RadarScr.imgChange = mSystem.loadImage("/radar/16.png");
			RadarScr.imgUIText = mSystem.loadImage("/radar/18.png");
			RadarScr.imgBar_1 = mSystem.loadImage("/radar/19.png");
			RadarScr.imgPro_0 = mSystem.loadImage("/radar/20.png");
			RadarScr.imgPro_1 = mSystem.loadImage("/radar/21.png");
			RadarScr.imgBar_0 = mSystem.loadImage("/radar/22.png");
			RadarScr.wUi = 200;
			RadarScr.hUi = 219;
			RadarScr.xUi = GameCanvas.hw - (RadarScr.wUi + 40) / 2;
			RadarScr.yUi = GameCanvas.hh - RadarScr.hUi / 2;
			RadarScr.xText = RadarScr.xUi + RadarScr.wUi - 81;
			RadarScr.yText = RadarScr.yUi + 29;
			RadarScr.wText = 120;
			RadarScr.hText = 80;
			RadarScr.xyArrow = new int[][]
			{
				new int[]
				{
					RadarScr.xUi + 34,
					RadarScr.yUi + RadarScr.hUi - 42
				},
				new int[]
				{
					RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgArrow_Down.getWidth() / 2,
					RadarScr.yUi + RadarScr.hUi / 2 + 33
				},
				new int[]
				{
					RadarScr.xUi + RadarScr.wUi - 41,
					RadarScr.yUi + RadarScr.hUi - 42
				}
			};
			RadarScr.xyItem = new int[][]
			{
				new int[]
				{
					RadarScr.xUi + 25,
					RadarScr.yUi + RadarScr.hUi - 82
				},
				new int[]
				{
					RadarScr.xUi + 57,
					RadarScr.yUi + RadarScr.hUi - 62
				},
				new int[]
				{
					RadarScr.xUi + RadarScr.wUi / 2 - 14,
					RadarScr.yUi + RadarScr.hUi - 102
				},
				new int[]
				{
					RadarScr.xUi + RadarScr.wUi - 57 - 28,
					RadarScr.yUi + RadarScr.hUi - 62
				},
				new int[]
				{
					RadarScr.xUi + RadarScr.wUi - 25 - 28,
					RadarScr.yUi + RadarScr.hUi - 82
				}
			};
			this.dxArrow = new int[2];
			this.dyArrow = 0;
			RadarScr.xMon = RadarScr.xUi + 73;
			RadarScr.yMon = RadarScr.yUi + RadarScr.hUi / 2 + 5;
			RadarScr.yCmd = RadarScr.yUi + RadarScr.hUi - 22;
			RadarScr.xCmd = new int[]
			{
				RadarScr.xUi + RadarScr.wUi / 2 - 8 - 80,
				RadarScr.xUi + RadarScr.wUi / 2 - 8,
				RadarScr.xUi + RadarScr.wUi / 2 - 8 + 80
			};
			RadarScr.dxCmd = new int[3];
			this.yClip = RadarScr.yText + 10 + 70;
			this.hClip = 0;
			RadarScr.list = new MyVector();
			RadarScr.listUse = new MyVector();
			this.page = 1;
			this.maxpage = 2;
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x003657B1 File Offset: 0x003639B1
		public static RadarScr gI()
		{
			if (RadarScr.instance == null)
			{
				RadarScr.instance = new RadarScr();
			}
			return RadarScr.instance;
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x003657CC File Offset: 0x003639CC
		public void SetRadarScr(MyVector list, int num, int numMax)
		{
			RadarScr.list = list;
			RadarScr.SetNum(num, numMax);
			this.page = 1;
			this.indexFocus = 2;
			this.listIndex();
			RadarScr.TYPE_UI = true;
			RadarScr.SetListUse();
			if (RadarScr.TYPE_UI)
			{
				this.maxpage = list.size() / 5 + ((list.size() % 5 > 0) ? 1 : 0);
				return;
			}
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 > 0) ? 1 : 0);
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x00365851 File Offset: 0x00363A51
		public static void SetNum(int num, int numMax)
		{
			RadarScr.num = num;
			RadarScr.numMax = numMax;
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x00365860 File Offset: 0x00363A60
		public static void SetListUse()
		{
			RadarScr.listUse = new MyVector(string.Empty);
			for (int i = 0; i < RadarScr.list.size(); i++)
			{
				Info_RadaScr info_RadaScr = (Info_RadaScr)RadarScr.list.elementAt(i);
				if (info_RadaScr != null && info_RadaScr.isUse == 1)
				{
					RadarScr.listUse.addElement(info_RadaScr);
				}
			}
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x003658BC File Offset: 0x00363ABC
		public void listIndex()
		{
			MyVector myVector = RadarScr.listUse;
			if (RadarScr.TYPE_UI)
			{
				myVector = RadarScr.list;
			}
			int num = (this.page - 1) * 5;
			int num2 = num + 5;
			for (int i = num; i < num2; i++)
			{
				if (i >= myVector.size())
				{
					RadarScr.index[i - num] = -1;
				}
				else
				{
					Info_RadaScr info_RadaScr = (Info_RadaScr)myVector.elementAt(i);
					if (info_RadaScr != null)
					{
						RadarScr.index[i - num] = info_RadaScr.id;
					}
				}
			}
			RadarScr.cmyText = 0;
			RadarScr.hText = 0;
			SoundMn.gI().radarItem();
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x00365944 File Offset: 0x00363B44
		public override void update()
		{
			try
			{
				if (RadarScr.hText < 80)
				{
					RadarScr.hText += 4;
					if (RadarScr.hText > 80)
					{
						RadarScr.hText = 80;
					}
				}
				this.focus_card = Info_RadaScr.GetInfo(RadarScr.listUse, RadarScr.index[this.indexFocus]);
				if (RadarScr.TYPE_UI)
				{
					this.focus_card = Info_RadaScr.GetInfo(RadarScr.list, RadarScr.index[this.indexFocus]);
				}
				GameScr.gI().update();
				if (GameCanvas.gameTick % 10 < 6)
				{
					if (GameCanvas.gameTick % 2 == 0)
					{
						this.dyArrow--;
					}
				}
				else
				{
					this.dyArrow = 0;
				}
				if (this.focus_card != null)
				{
					int num = (int)(this.focus_card.amount * 100 / this.focus_card.max_amount);
					this.hClip = num * RadarScr.imgBar_1.getHeight() / 100;
					int num2 = RadarScr.num * 100 / RadarScr.list.size();
					this.wClip = num2 * RadarScr.imgPro_1.getWidth() / 100;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("-upd-radaScr-null: " + ex.ToString());
			}
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x00365A78 File Offset: 0x00363C78
		public override void updateKey()
		{
			if (!InfoDlg.isLock)
			{
				if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
				{
					this.updateKeyTouchControl();
				}
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
				if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = false;
					this.doKeyItem(1);
				}
				if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false;
					this.doKeyItem(0);
				}
				if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					this.doClickUse(1);
				}
				if (GameCanvas.keyPressed[13])
				{
					this.doClickUse(2);
				}
				if (GameCanvas.keyPressed[12])
				{
					GameCanvas.keyPressed[12] = false;
					this.doClickUse(0);
				}
				GameCanvas.clearKeyPressed();
			}
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x00365BCC File Offset: 0x00363DCC
		private void doChangeUI()
		{
			RadarScr.TYPE_UI = !RadarScr.TYPE_UI;
			this.page = 1;
			this.indexFocus = 0;
			if (RadarScr.TYPE_UI)
			{
				this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 > 0) ? 1 : 0);
			}
			else
			{
				this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 > 0) ? 1 : 0);
			}
			this.listIndex();
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x00365C50 File Offset: 0x00363E50
		private void updateKeyTouchControl()
		{
			if (GameCanvas.isPointerClick)
			{
				for (int i = 0; i < 5; i++)
				{
					if (GameCanvas.isPointerHoldIn(RadarScr.xyItem[i][0], RadarScr.xyItem[i][1], 30, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && i != this.indexFocus)
					{
						this.doClickItem(i);
					}
				}
				if (GameCanvas.isPointerHoldIn(RadarScr.xyArrow[0][0] - 5, RadarScr.xyArrow[0][1] - 5, 20, 20))
				{
					if (GameCanvas.isPointerDown)
					{
						this.dxArrow[0] = 1;
					}
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						this.doClickArrow(0);
						this.dxArrow[0] = 0;
					}
				}
				if (GameCanvas.isPointerHoldIn(RadarScr.xyArrow[2][0] - 5, RadarScr.xyArrow[2][1] - 5, 20, 20))
				{
					if (GameCanvas.isPointerDown)
					{
						this.dxArrow[1] = 1;
					}
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						this.doClickArrow(1);
						this.dxArrow[1] = 0;
					}
				}
				for (int j = 0; j < RadarScr.xCmd.Length; j++)
				{
					if (GameCanvas.isPointerHoldIn(RadarScr.xCmd[j] - 5, RadarScr.yCmd - 5, 20, 20))
					{
						if (GameCanvas.isPointerDown)
						{
							RadarScr.dxCmd[j] = 1;
						}
						if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
						{
							this.doClickUse(j);
							RadarScr.dxCmd[j] = 0;
						}
					}
				}
			}
			else
			{
				RadarScr.dxCmd[0] = 0;
				RadarScr.dxCmd[1] = 0;
				RadarScr.dxCmd[2] = 0;
				this.dxArrow[0] = 0;
				this.dxArrow[1] = 0;
			}
			if (!GameCanvas.isPointerHoldIn(RadarScr.xText, 0, RadarScr.wText, RadarScr.yText + RadarScr.hText))
			{
				return;
			}
			if (GameCanvas.isPointerMove)
			{
				if (this.pyy == 0)
				{
					this.pyy = GameCanvas.py;
				}
				this.pxx = this.pyy - GameCanvas.py;
				if (this.pxx != 0)
				{
					RadarScr.cmyText += this.pxx;
					this.pyy = GameCanvas.py;
				}
				if (RadarScr.cmyText < 0)
				{
					RadarScr.cmyText = 0;
				}
				if (RadarScr.cmyText > this.focus_card.cp.lim)
				{
					RadarScr.cmyText = this.focus_card.cp.lim;
					return;
				}
			}
			else
			{
				this.pyy = 0;
				this.pyy = 0;
			}
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x00365E8C File Offset: 0x0036408C
		private void doClickUse(int i)
		{
			switch (i)
			{
			case 0:
				this.doChangeUI();
				break;
			case 1:
				if (this.focus_card != null)
				{
					Service.gI().SendRada(1, this.focus_card.id);
				}
				break;
			case 2:
				GameScr.gI().switchToMe();
				break;
			}
			SoundMn.gI().radarClick();
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x00365EEC File Offset: 0x003640EC
		private void doClickArrow(int dir)
		{
			if (RadarScr.TYPE_UI)
			{
				this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 > 0) ? 1 : 0);
			}
			else
			{
				this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 > 0) ? 1 : 0);
			}
			int num = this.page;
			if (dir == 0)
			{
				if (this.page == 1)
				{
					return;
				}
				num--;
				if (num < 1)
				{
					num = 1;
				}
			}
			else
			{
				if (this.page == this.maxpage)
				{
					return;
				}
				num++;
				if (num > this.maxpage)
				{
					num = this.maxpage;
				}
			}
			if (num != this.page)
			{
				this.page = num;
				this.listIndex();
			}
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x00365FA7 File Offset: 0x003641A7
		private void doClickItem(int focus)
		{
			this.indexFocus = focus;
			this.listIndex();
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x00365FB8 File Offset: 0x003641B8
		private void doKeyText(int type)
		{
			RadarScr.cmyText += 12 * type;
			if (RadarScr.cmyText < 0)
			{
				RadarScr.cmyText = 0;
			}
			if (RadarScr.cmyText > this.focus_card.cp.lim)
			{
				RadarScr.cmyText = this.focus_card.cp.lim;
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x00366010 File Offset: 0x00364210
		private void doKeyItem(int type)
		{
			int num = this.indexFocus;
			int num2 = this.page;
			num = ((type != 0) ? (num - 1) : (num + 1));
			if (num >= RadarScr.index.Length)
			{
				if (this.page < this.maxpage)
				{
					num = 0;
					num2++;
				}
				else
				{
					num = RadarScr.index.Length - 1;
				}
			}
			if (num < 0)
			{
				if (this.page > 1)
				{
					num = RadarScr.index.Length - 1;
					num2--;
				}
				else
				{
					num = 0;
				}
			}
			if (num != this.indexFocus)
			{
				this.indexFocus = num;
				RadarScr.cmyText = 0;
				RadarScr.hText = 0;
			}
			if (num2 != this.page)
			{
				this.page = num2;
				this.listIndex();
			}
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x003660B4 File Offset: 0x003642B4
		public override void paint(mGraphics g)
		{
			try
			{
				GameScr.gI().paint(g);
				g.translate(-GameScr.cmx, -GameScr.cmy);
				g.translate(0, GameCanvas.transY);
				GameScr.resetTranslate(g);
				g.drawImage(RadarScr.imgUI, RadarScr.xUi, RadarScr.yUi, 0);
				g.drawImage(RadarScr.imgPro_0, RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 - 2, 0);
				g.setClip(RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2 + 13, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 + 3, this.wClip, RadarScr.imgPro_0.getHeight());
				g.drawImage(RadarScr.imgPro_1, RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2 + 13, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 + 3, 0);
				GameScr.resetTranslate(g);
				g.drawImage(RadarScr.imgChange, RadarScr.xCmd[0], RadarScr.yCmd + RadarScr.dxCmd[0], 0);
				g.drawImage(RadarScr.imgUse_0, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
				g.drawImage(RadarScr.imgBack, RadarScr.xCmd[2], RadarScr.yCmd + RadarScr.dxCmd[2], 0);
				if (RadarScr.TYPE_UI)
				{
					g.drawRegion(RadarScr.imgUse, 0, 0, 17, 17, 0, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
				}
				else
				{
					g.drawRegion(RadarScr.imgUse, 0, 0, 17, 17, 1, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
				}
				if (this.focus_card != null)
				{
					g.setClip(RadarScr.xUi + 30, RadarScr.yUi + 13, RadarScr.wUi - 60, RadarScr.hUi / 2);
					this.focus_card.paintInfo(g, RadarScr.xMon, RadarScr.yMon);
					GameScr.resetTranslate(g);
					mFont.tahoma_7b_yellow.drawString(g, ((this.focus_card.level <= 0) ? " " : ("Lv." + this.focus_card.level.ToString() + " ")) + this.focus_card.name, RadarScr.xUi + RadarScr.wUi / 2, RadarScr.yUi + 15, 2);
					mFont.tahoma_7_white.drawString(g, "no." + this.focus_card.no.ToString(), RadarScr.xUi + 30, RadarScr.yText - 2, 0);
					g.drawImage(RadarScr.imgBar_0, RadarScr.xUi + 36, RadarScr.yText + 10, 0);
					g.setClip(RadarScr.xUi + 36, this.yClip - this.hClip, 7, this.hClip);
					g.drawImage(RadarScr.imgBar_1, RadarScr.xUi + 36, RadarScr.yText + 10, 0);
					GameScr.resetTranslate(g);
					g.drawImage(RadarScr.imgRank[(int)this.focus_card.rank], RadarScr.xUi + 39 - 5 + 14, RadarScr.yText + 12, 0);
				}
				g.setClip(RadarScr.xText, RadarScr.yText, RadarScr.wText + 5, RadarScr.hText + 8);
				if (this.focus_card != null)
				{
					g.drawImage(RadarScr.imgUIText, RadarScr.xText, RadarScr.yText, 0);
				}
				GameScr.resetTranslate(g);
				g.setClip(RadarScr.xText, RadarScr.yText + 1, RadarScr.wText, RadarScr.hText + 5);
				if (this.focus_card != null && this.focus_card.cp != null)
				{
					if (this.focus_card.cp.says == null)
					{
						return;
					}
					this.focus_card.cp.paintRada(g, RadarScr.cmyText);
				}
				GameScr.resetTranslate(g);
				if ((!RadarScr.TYPE_UI && RadarScr.listUse.size() > 5) || RadarScr.TYPE_UI)
				{
					if (this.page > 1)
					{
						g.drawImage(RadarScr.imgArrow_Left, RadarScr.xyArrow[0][0], RadarScr.xyArrow[0][1] + this.dxArrow[0], 0);
					}
					if (this.page < this.maxpage)
					{
						g.drawImage(RadarScr.imgArrow_Right, RadarScr.xyArrow[2][0], RadarScr.xyArrow[2][1] + this.dxArrow[1], 0);
					}
				}
				for (int i = 0; i < RadarScr.index.Length; i++)
				{
					int num = 0;
					int num2 = 0;
					int idx = 0;
					if (i == this.indexFocus)
					{
						num = this.dyArrow;
						num2 = -10;
						idx = 1;
						g.drawImage(RadarScr.imgArrow_Down, RadarScr.xyItem[i][0] + 10, RadarScr.xyItem[i][1] + this.dyArrow + 29 + num2, 0);
					}
					Info_RadaScr info = Info_RadaScr.GetInfo(RadarScr.listUse, RadarScr.index[i]);
					if (RadarScr.TYPE_UI)
					{
						info = Info_RadaScr.GetInfo(RadarScr.list, RadarScr.index[i]);
					}
					if (info != null)
					{
						RadarScr.fraImgFocus.drawFrame((int)info.rank, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
						SmallImage.drawSmallImage(g, info.idIcon, RadarScr.xyItem[i][0] + 14, RadarScr.xyItem[i][1] + 14 + num + num2, 0, StaticObj.VCENTER_HCENTER);
						info.paintEff(g, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2);
						if (info.level == 0)
						{
							g.drawImage(RadarScr.imgLock, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0);
						}
						if (i == this.indexFocus)
						{
							RadarScr.fraImgFocus.drawFrame(7, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
						}
						if (info.isUse == 1)
						{
							RadarScr.fraImgFocus.drawFrame(8, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
						}
					}
					else
					{
						RadarScr.fraImgFocusNone.drawFrame(idx, RadarScr.xyItem[i][0] - 1, RadarScr.xyItem[i][1] - 1 + num + num2, 0, 0, g);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("-pnt-radaScr-null: " + ex.ToString());
			}
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x00366710 File Offset: 0x00364910
		public override void switchToMe()
		{
			GameScr.isPaintOther = true;
			base.switchToMe();
		}

		// Token: 0x04006B9A RID: 27546
		public static RadarScr instance;

		// Token: 0x04006B9B RID: 27547
		public static bool TYPE_UI;

		// Token: 0x04006B9C RID: 27548
		public static FrameImage fraImgFocus;

		// Token: 0x04006B9D RID: 27549
		public static FrameImage fraImgFocusNone;

		// Token: 0x04006B9E RID: 27550
		public static FrameImage fraEff;

		// Token: 0x04006B9F RID: 27551
		private static Image imgUI;

		// Token: 0x04006BA0 RID: 27552
		private static Image imgUIText;

		// Token: 0x04006BA1 RID: 27553
		private static Image imgArrow_Left;

		// Token: 0x04006BA2 RID: 27554
		private static Image imgArrow_Right;

		// Token: 0x04006BA3 RID: 27555
		private static Image imgArrow_Down;

		// Token: 0x04006BA4 RID: 27556
		private static Image imgLock;

		// Token: 0x04006BA5 RID: 27557
		private static Image imgUse_0;

		// Token: 0x04006BA6 RID: 27558
		private static Image imgUse;

		// Token: 0x04006BA7 RID: 27559
		private static Image imgBack;

		// Token: 0x04006BA8 RID: 27560
		private static Image imgChange;

		// Token: 0x04006BA9 RID: 27561
		private static Image imgBar_0;

		// Token: 0x04006BAA RID: 27562
		private static Image imgBar_1;

		// Token: 0x04006BAB RID: 27563
		private static Image imgPro_0;

		// Token: 0x04006BAC RID: 27564
		private static Image imgPro_1;

		// Token: 0x04006BAD RID: 27565
		private static Image[] imgRank;

		// Token: 0x04006BAE RID: 27566
		public static int xUi;

		// Token: 0x04006BAF RID: 27567
		public static int yUi;

		// Token: 0x04006BB0 RID: 27568
		public static int wUi;

		// Token: 0x04006BB1 RID: 27569
		public static int hUi;

		// Token: 0x04006BB2 RID: 27570
		public static int xMon;

		// Token: 0x04006BB3 RID: 27571
		public static int yMon;

		// Token: 0x04006BB4 RID: 27572
		public static int xText;

		// Token: 0x04006BB5 RID: 27573
		public static int yText;

		// Token: 0x04006BB6 RID: 27574
		public static int wText;

		// Token: 0x04006BB7 RID: 27575
		public static int cmyText;

		// Token: 0x04006BB8 RID: 27576
		public static int hText;

		// Token: 0x04006BB9 RID: 27577
		public static int yCmd;

		// Token: 0x04006BBA RID: 27578
		public static int[] xCmd = new int[0];

		// Token: 0x04006BBB RID: 27579
		public static int[] dxCmd = new int[0];

		// Token: 0x04006BBC RID: 27580
		private static int[][] xyArrow;

		// Token: 0x04006BBD RID: 27581
		private static int[][] xyItem;

		// Token: 0x04006BBE RID: 27582
		private static int[] index = new int[]
		{
			-2,
			-1,
			0,
			1,
			2
		};

		// Token: 0x04006BBF RID: 27583
		private int dyArrow;

		// Token: 0x04006BC0 RID: 27584
		private int[] dxArrow;

		// Token: 0x04006BC1 RID: 27585
		private int page;

		// Token: 0x04006BC2 RID: 27586
		private int maxpage;

		// Token: 0x04006BC3 RID: 27587
		private int indexFocus;

		// Token: 0x04006BC4 RID: 27588
		public static MyVector list;

		// Token: 0x04006BC5 RID: 27589
		public static MyVector listUse;

		// Token: 0x04006BC6 RID: 27590
		private static int num;

		// Token: 0x04006BC7 RID: 27591
		private static int numMax;

		// Token: 0x04006BC8 RID: 27592
		private Info_RadaScr focus_card;

		// Token: 0x04006BC9 RID: 27593
		private int pxx;

		// Token: 0x04006BCA RID: 27594
		private int pyy;

		// Token: 0x04006BCB RID: 27595
		private int wClip;

		// Token: 0x04006BCC RID: 27596
		private int yClip;

		// Token: 0x04006BCD RID: 27597
		private int hClip;
	}
}
