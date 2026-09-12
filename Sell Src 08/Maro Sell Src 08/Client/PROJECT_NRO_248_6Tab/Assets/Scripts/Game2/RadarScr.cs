using System;
using UnityEngine;

namespace Game2
{
	// Token: 0x020003F6 RID: 1014
	public class RadarScr : mScreen
	{
		// Token: 0x06002D71 RID: 11633 RVA: 0x002D02DC File Offset: 0x002CE4DC
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

		// Token: 0x06002D72 RID: 11634 RVA: 0x002D070D File Offset: 0x002CE90D
		public static RadarScr gI()
		{
			if (RadarScr.instance == null)
			{
				RadarScr.instance = new RadarScr();
			}
			return RadarScr.instance;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x002D0728 File Offset: 0x002CE928
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

		// Token: 0x06002D74 RID: 11636 RVA: 0x002D07AD File Offset: 0x002CE9AD
		public static void SetNum(int num, int numMax)
		{
			RadarScr.num = num;
			RadarScr.numMax = numMax;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x002D07BC File Offset: 0x002CE9BC
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

		// Token: 0x06002D76 RID: 11638 RVA: 0x002D0818 File Offset: 0x002CEA18
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

		// Token: 0x06002D77 RID: 11639 RVA: 0x002D08A0 File Offset: 0x002CEAA0
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

		// Token: 0x06002D78 RID: 11640 RVA: 0x002D09D4 File Offset: 0x002CEBD4
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

		// Token: 0x06002D79 RID: 11641 RVA: 0x002D0B28 File Offset: 0x002CED28
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

		// Token: 0x06002D7A RID: 11642 RVA: 0x002D0BAC File Offset: 0x002CEDAC
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

		// Token: 0x06002D7B RID: 11643 RVA: 0x002D0DE8 File Offset: 0x002CEFE8
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

		// Token: 0x06002D7C RID: 11644 RVA: 0x002D0E48 File Offset: 0x002CF048
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

		// Token: 0x06002D7D RID: 11645 RVA: 0x002D0F03 File Offset: 0x002CF103
		private void doClickItem(int focus)
		{
			this.indexFocus = focus;
			this.listIndex();
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x002D0F14 File Offset: 0x002CF114
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

		// Token: 0x06002D7F RID: 11647 RVA: 0x002D0F6C File Offset: 0x002CF16C
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

		// Token: 0x06002D80 RID: 11648 RVA: 0x002D1010 File Offset: 0x002CF210
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

		// Token: 0x06002D81 RID: 11649 RVA: 0x002D166C File Offset: 0x002CF86C
		public override void switchToMe()
		{
			GameScr.isPaintOther = true;
			base.switchToMe();
		}

		// Token: 0x0400591B RID: 22811
		public static RadarScr instance;

		// Token: 0x0400591C RID: 22812
		public static bool TYPE_UI;

		// Token: 0x0400591D RID: 22813
		public static FrameImage fraImgFocus;

		// Token: 0x0400591E RID: 22814
		public static FrameImage fraImgFocusNone;

		// Token: 0x0400591F RID: 22815
		public static FrameImage fraEff;

		// Token: 0x04005920 RID: 22816
		private static Image imgUI;

		// Token: 0x04005921 RID: 22817
		private static Image imgUIText;

		// Token: 0x04005922 RID: 22818
		private static Image imgArrow_Left;

		// Token: 0x04005923 RID: 22819
		private static Image imgArrow_Right;

		// Token: 0x04005924 RID: 22820
		private static Image imgArrow_Down;

		// Token: 0x04005925 RID: 22821
		private static Image imgLock;

		// Token: 0x04005926 RID: 22822
		private static Image imgUse_0;

		// Token: 0x04005927 RID: 22823
		private static Image imgUse;

		// Token: 0x04005928 RID: 22824
		private static Image imgBack;

		// Token: 0x04005929 RID: 22825
		private static Image imgChange;

		// Token: 0x0400592A RID: 22826
		private static Image imgBar_0;

		// Token: 0x0400592B RID: 22827
		private static Image imgBar_1;

		// Token: 0x0400592C RID: 22828
		private static Image imgPro_0;

		// Token: 0x0400592D RID: 22829
		private static Image imgPro_1;

		// Token: 0x0400592E RID: 22830
		private static Image[] imgRank;

		// Token: 0x0400592F RID: 22831
		public static int xUi;

		// Token: 0x04005930 RID: 22832
		public static int yUi;

		// Token: 0x04005931 RID: 22833
		public static int wUi;

		// Token: 0x04005932 RID: 22834
		public static int hUi;

		// Token: 0x04005933 RID: 22835
		public static int xMon;

		// Token: 0x04005934 RID: 22836
		public static int yMon;

		// Token: 0x04005935 RID: 22837
		public static int xText;

		// Token: 0x04005936 RID: 22838
		public static int yText;

		// Token: 0x04005937 RID: 22839
		public static int wText;

		// Token: 0x04005938 RID: 22840
		public static int cmyText;

		// Token: 0x04005939 RID: 22841
		public static int hText;

		// Token: 0x0400593A RID: 22842
		public static int yCmd;

		// Token: 0x0400593B RID: 22843
		public static int[] xCmd = new int[0];

		// Token: 0x0400593C RID: 22844
		public static int[] dxCmd = new int[0];

		// Token: 0x0400593D RID: 22845
		private static int[][] xyArrow;

		// Token: 0x0400593E RID: 22846
		private static int[][] xyItem;

		// Token: 0x0400593F RID: 22847
		private static int[] index = new int[]
		{
			-2,
			-1,
			0,
			1,
			2
		};

		// Token: 0x04005940 RID: 22848
		private int dyArrow;

		// Token: 0x04005941 RID: 22849
		private int[] dxArrow;

		// Token: 0x04005942 RID: 22850
		private int page;

		// Token: 0x04005943 RID: 22851
		private int maxpage;

		// Token: 0x04005944 RID: 22852
		private int indexFocus;

		// Token: 0x04005945 RID: 22853
		public static MyVector list;

		// Token: 0x04005946 RID: 22854
		public static MyVector listUse;

		// Token: 0x04005947 RID: 22855
		private static int num;

		// Token: 0x04005948 RID: 22856
		private static int numMax;

		// Token: 0x04005949 RID: 22857
		private Info_RadaScr focus_card;

		// Token: 0x0400594A RID: 22858
		private int pxx;

		// Token: 0x0400594B RID: 22859
		private int pyy;

		// Token: 0x0400594C RID: 22860
		private int wClip;

		// Token: 0x0400594D RID: 22861
		private int yClip;

		// Token: 0x0400594E RID: 22862
		private int hClip;
	}
}
