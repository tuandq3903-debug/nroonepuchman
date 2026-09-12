using System;
using System.Threading;

namespace Game1
{
	// Token: 0x0200045E RID: 1118
	public class CrackBallScr : mScreen
	{
		// Token: 0x060031D8 RID: 12760 RVA: 0x0031090C File Offset: 0x0030EB0C
		public CrackBallScr()
		{
			CrackBallScr.xSkill = new int[3];
			CrackBallScr.xSkill[0] = 16;
			CrackBallScr.ySkill = GameCanvas.h - 41;
			CrackBallScr.xSkill[1] = GameCanvas.w - 40;
			CrackBallScr.xSkill[2] = (CrackBallScr.xSkill[0] + CrackBallScr.xSkill[1]) / 2;
			CrackBallScr.fraImgKame = new FrameImage(GameCanvas.loadImage("/e/e_1.png"), 30, 30);
			CrackBallScr.fraImgKame_1 = new FrameImage(GameCanvas.loadImage("/e/e_0.png"), 68, 65);
			CrackBallScr.fraImgKame_2 = new FrameImage(GameCanvas.loadImage("/e/e_2.png"), 66, 70);
			CrackBallScr.imgReplay = GameCanvas.loadImage("/e/nut2.png");
			CrackBallScr.imgX = GameCanvas.loadImage("/e/nut3.png");
			this.wP = 230;
			this.xP = GameCanvas.hw - this.wP / 2;
			this.hP = 40;
			this.yP = -this.hP;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x00310A31 File Offset: 0x0030EC31
		public static CrackBallScr gI()
		{
			if (CrackBallScr.instance == null)
			{
				CrackBallScr.instance = new CrackBallScr();
			}
			return CrackBallScr.instance;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x00310A4C File Offset: 0x0030EC4C
		public void SetCrackBallScr(short[] idImage, byte typePrice, int price, short idTicket)
		{
			if (idImage != null && idImage.Length != 0)
			{
				this.yTo = Char.myCharz().cy - 10;
				this.setAuraItem();
				this.listBall = new BallInfo[idImage.Length];
				for (int i = 0; i < this.listBall.Length; i++)
				{
					this.listBall[i] = new BallInfo();
					this.listBall[i].idImg = (int)idImage[i];
					this.listBall[i].count = i * 25;
					this.listBall[i].yTo = -999;
					this.listBall[i].vx = Res.random(2, 5);
					this.listBall[i].dir = Res.random(-1, 2);
					this.listBall[i].SetChar();
				}
				this.isCanSkill = false;
				this.isKame = false;
				this.isSendSv = false;
				this.timeStart = GameCanvas.timeNow + (long)Res.random(1000, 2000);
				this.step = 0;
				this.indexSelect = -1;
				this.indexSkillSelect = -1;
				this.typePrice = typePrice;
				this.price = price;
				this.cost = 0;
				Char.myCharz().moveTo(470, 408, 1);
				Char.myCharz().cdir = -1;
				Char.myCharz().statusMe = 1;
				this.countFr = 0;
				this.countKame = 0;
				this.frame = 0;
				this.vp = 0;
				this.yP = -this.hP;
				this.idTicket = idTicket;
				this.numTicket = 0;
				this.checkNumTicket();
				this.switchToMe();
				SoundMn.gI().hoisinh();
			}
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x00310BEC File Offset: 0x0030EDEC
		private void setAuraItem()
		{
			this.rO = GameCanvas.hh / 3 + 10;
			if (this.rO > 50)
			{
				this.rO = 50;
			}
			this.xO = 360;
			GameScr.cmx = GameScr.cmxLim / 2;
			this.yO = GameScr.cmy + GameCanvas.hh / 3 + 30;
			this.iDot = 175;
			this.angle = 0;
			this.iAngle = 360 / this.iDot;
			this.xArg = new int[this.iDot];
			this.yArg = new int[this.iDot];
			this.xDot = new int[this.iDot];
			this.yDot = new int[this.iDot];
			this.setDotPosition();
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x00310CB8 File Offset: 0x0030EEB8
		private void setDotPosition()
		{
			if (GameCanvas.lowGraphic)
			{
				return;
			}
			for (int i = 0; i < this.yArg.Length; i++)
			{
				this.yArg[i] = Res.abs(this.rO * Res.sin(this.angle) / 1024);
				this.xArg[i] = Res.abs(this.rO * Res.cos(this.angle) / 1024);
				if (this.angle < 90)
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 90 && this.angle < 180)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 180 && this.angle < 270)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				else
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				this.angle += this.iAngle;
			}
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x00310E40 File Offset: 0x0030F040
		public override void update()
		{
			try
			{
				this.cost = this.price * (int)this.checkNum();
				this.checkNumTicket();
				GameScr.gI().update();
				if (this.timeStart - GameCanvas.timeNow > 0L)
				{
					for (int i = 0; i < this.listBall.Length; i++)
					{
						this.listBall[i].count += 2;
						if (this.listBall[i].count >= this.iDot)
						{
							this.listBall[i].count = 0;
						}
						this.listBall[i].x = this.xDot[this.listBall[i].count];
						this.listBall[i].y = this.yDot[this.listBall[i].count];
					}
				}
				else
				{
					if (this.step == 0)
					{
						this.step = 1;
					}
					if (this.step == 1)
					{
						for (int j = 0; j < this.listBall.Length; j++)
						{
							if (this.listBall[j].yTo != -999 && !this.listBall[j].isDone)
							{
								if (this.listBall[j].y < this.listBall[j].yTo)
								{
									if (this.listBall[j].vy < 0)
									{
										this.listBall[j].vy = 0;
									}
									if (this.listBall[j].y + this.listBall[j].vy > this.listBall[j].yTo)
									{
										this.listBall[j].y = this.listBall[j].yTo;
									}
									else
									{
										this.listBall[j].y += this.listBall[j].vy;
									}
									this.listBall[j].vy++;
								}
								else
								{
									if (this.listBall[j].vy > 0)
									{
										this.listBall[j].vy = 0;
									}
									this.listBall[j].y += this.listBall[j].vy;
									this.listBall[j].vy--;
								}
								if (this.listBall[j].y == this.listBall[j].yTo)
								{
									EffecMn.addEff(new Effect(19, this.listBall[j].x - 5, this.listBall[j].y + 25, 2, 1, -1));
									SoundMn.gI().charFall();
									this.listBall[j].isDone = true;
									if (!this.isCanSkill)
									{
										this.isCanSkill = true;
									}
								}
							}
						}
					}
					if (this.step == 2)
					{
						for (int k = 0; k < this.listBall.Length; k++)
						{
							if (!this.listBall[k].isDone)
							{
								if (this.listBall[k].y > -10)
								{
									if (this.listBall[k].vy > 0)
									{
										this.listBall[k].vy = 0;
									}
									this.listBall[k].y += this.listBall[k].vy;
									this.listBall[k].vy--;
									this.listBall[k].x += this.listBall[k].vx * this.listBall[k].dir;
									this.listBall[k].vx -= 3;
								}
								if (this.listBall[k].y == -10)
								{
									this.listBall[k].isPaint = false;
								}
							}
						}
						this.countFr++;
						if (this.countFr > this.fr.Length - 1)
						{
							this.countFr = this.fr.Length - 1;
							this.isKame = true;
							SoundMn.gI().newKame();
							if (!this.isSendSv && this.timeKame - GameCanvas.timeNow < 0L)
							{
								Service.gI().SendCrackBall(2, (byte)(checkTicket() + checkNum()));
								this.isSendSv = true;
							}
						}
						Char.myCharz().cf = (int)this.fr[this.countFr];
						this.countKame++;
						if (this.countKame > 5)
						{
							this.countKame = 0;
						}
						this.frame = (int)this.nFrame[this.countKame];
					}
					if (this.step == 3)
					{
						if (this.countKame <= 5)
						{
							this.countKame = 5;
						}
						this.countKame++;
						if (this.countKame > this.nFrame.Length - 1)
						{
							this.countKame = this.nFrame.Length - 1;
							this.step = 4;
							this.isKame = false;
							int num = 0;
							for (int l = 0; l < this.listBall.Length; l++)
							{
								if (this.listBall[l].isDone && !this.listBall[l].isSetImg)
								{
									this.listBall[l].idImg = (int)this.idItem[num];
									this.listBall[l].isSetImg = true;
									num++;
								}
							}
						}
						this.frame = (int)this.nFrame[this.countKame];
					}
					if (this.step == 4)
					{
						for (int m = 0; m < this.listBall.Length; m++)
						{
							if (this.listBall[m].isPaint)
							{
								this.listBall[m].xTo = Char.myCharz().cx;
							}
						}
						this.step = 5;
					}
					if (this.step == 5)
					{
						this.vp++;
						if (this.yP < GameCanvas.hh / 3)
						{
							if (this.yP + this.vp > GameCanvas.hh / 3)
							{
								this.yP = GameCanvas.hh / 3;
							}
							else
							{
								this.yP += this.vp;
							}
						}
						for (int n = 0; n < this.listBall.Length; n++)
						{
							if (this.listBall[n].isPaint)
							{
								if (this.listBall[n].x < this.listBall[n].xTo)
								{
									if (this.listBall[n].vx < 0)
									{
										this.listBall[n].vx = 0;
									}
									if (this.listBall[n].x + this.listBall[n].vx > this.listBall[n].xTo)
									{
										this.listBall[n].x = this.listBall[n].xTo;
									}
									else
									{
										this.listBall[n].x += this.listBall[n].vx;
									}
									this.listBall[n].vx++;
								}
								else
								{
									if (this.listBall[n].vx > 0)
									{
										this.listBall[n].vx = 0;
									}
									this.listBall[n].x += this.listBall[n].vx;
									this.listBall[n].vx--;
								}
								if (this.listBall[n].x == this.listBall[n].xTo)
								{
									this.listBall[n].isPaint = false;
								}
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x003115DC File Offset: 0x0030F7DC
		public override void updateKey()
		{
			if (InfoDlg.isLock)
			{
				return;
			}
			if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
			{
				this.updateKeyTouchControl();
			}
			if (CrackBallScr.isAutoCrackBall && !GameCanvas.keyPressed[0])
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			if (GameCanvas.keyPressed[0])
			{
				this.doClickSkill(2);
			}
			for (int i = 1; i < 8; i++)
			{
				if (GameCanvas.keyPressed[i])
				{
					GameCanvas.keyPressed[i] = false;
					this.doClickBall(i - 1);
				}
			}
			if (GameCanvas.keyPressed[12])
			{
				GameCanvas.keyPressed[12] = false;
				this.doClickSkill(0);
			}
			if (GameCanvas.keyPressed[13])
			{
				GameCanvas.keyPressed[13] = false;
				this.doClickSkill(1);
			}
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x003116A0 File Offset: 0x0030F8A0
		private void updateKeyTouchControl()
		{
			if (this.step == 1 && GameCanvas.isPointerClick)
			{
				for (int i = 0; i < this.listBall.Length; i++)
				{
					if (GameCanvas.isPointerHoldIn(this.listBall[i].x - 20 - GameScr.cmx, this.listBall[i].y - 10 - GameScr.cmy, 30, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						this.doClickBall(i);
					}
				}
			}
			if (!GameCanvas.isPointerClick)
			{
				return;
			}
			for (int j = 0; j < CrackBallScr.xSkill.Length; j++)
			{
				if (GameCanvas.isPointerHoldIn(CrackBallScr.xSkill[j], CrackBallScr.ySkill, 36, 36) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					if (CrackBallScr.isAutoCrackBall && j != 2)
					{
						break;
					}
					this.doClickSkill(j);
				}
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x00311770 File Offset: 0x0030F970
		private void doClickBall(int index)
		{
			if (!this.listBall[index].isDone)
			{
				SoundMn.gI().getItem();
				long num = (this.typePrice != 0) ? ((long)Char.myCharz().checkLuong()) : Char.myCharz().xu;
				if ((int)this.checkTicket() >= this.numTicket && num < (long)(this.cost + this.price))
				{
					string s = mResources.not_enough_money_1 + " " + ((this.typePrice != 0) ? mResources.LUONG : mResources.XU);
					GameScr.info1.addInfo(s, 0);
					return;
				}
				this.indexSelect = index;
				this.listBall[this.indexSelect].yTo = this.yTo + Res.random(-3, 3);
			}
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x00311834 File Offset: 0x0030FA34
		private void doClickSkill(int index)
		{
			this.indexSkillSelect = index;
			if (this.indexSkillSelect != 2)
			{
				if (index == 0)
				{
					if (this.step < 2)
					{
						if (this.checkTicket() + this.checkNum() > 0)
						{
							this.step = 2;
							SoundMn.gI().gong();
							Char.myCharz().setSkillPaint(GameScr.sks[13], 0);
							this.timeKame = GameCanvas.timeNow + (long)Res.random(2000, 3000);
							return;
						}
					}
					else if (this.yP == GameCanvas.hh / 3)
					{
						Service.gI().SendCrackBall(this.typePrice, 0);
						return;
					}
				}
				else
				{
					if (CrackBallScr.isAutoCrackBall)
					{
						this.stopAutoCrackBall();
					}
					GameScr.gI().isRongThanXuatHien = false;
					GameScr.gI().switchToMe();
				}
				return;
			}
			CrackBallScr.isAutoCrackBall = !CrackBallScr.isAutoCrackBall;
			if (CrackBallScr.isAutoCrackBall)
			{
				this.startAutoCrackBall();
				return;
			}
			CrackBallScr.isCallStop = true;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x00311918 File Offset: 0x0030FB18
		public override void paint(mGraphics g)
		{
			try
			{
				GameScr.gI().paint(g);
				g.translate(-GameScr.cmx, -GameScr.cmy);
				g.translate(0, GameCanvas.transY);
				for (int i = 0; i < this.listBall.Length; i++)
				{
					if (this.listBall[i].isPaint && this.listBall[i].y > this.listBall[i].yTo - 20)
					{
						g.drawImage(TileMap.bong, this.listBall[i].x, this.listBall[i].yTo + 7, mGraphics.VCENTER | mGraphics.HCENTER);
					}
				}
				for (int j = 0; j < this.listBall.Length; j++)
				{
					if (this.listBall[j].isPaint)
					{
						SmallImage.drawSmallImage(g, this.listBall[j].idImg, this.listBall[j].x, this.listBall[j].y, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
				}
				if (this.isKame)
				{
					if (CrackBallScr.fraImgKame != null)
					{
						int num = Char.myCharz().cx - CrackBallScr.fraImgKame.frameWidth - 28;
						for (int k = 0; k < GameCanvas.w / CrackBallScr.fraImgKame.frameWidth + 1; k++)
						{
							CrackBallScr.fraImgKame.drawFrame(this.frame, num - k * (CrackBallScr.fraImgKame.frameWidth - 1), Char.myCharz().cy - CrackBallScr.fraImgKame.frameHeight / 2 - 12 + 2, 0, 0, g);
						}
					}
					if (CrackBallScr.fraImgKame_1 != null)
					{
						int num2 = Char.myCharz().cx - CrackBallScr.fraImgKame_1.frameWidth - 10;
						CrackBallScr.fraImgKame_1.drawFrame(this.frame, num2 - 5, Char.myCharz().cy - CrackBallScr.fraImgKame_1.frameHeight / 2 - 12, 0, 0, g);
					}
				}
				GameScr.resetTranslate(g);
				int num3 = 240;
				int num4 = GameCanvas.w - num3;
				int num5 = 15;
				g.setColor(13524492);
				g.fillRect(num4, num5 - 15, num3, 15);
				g.drawImage(Panel.imgXu, num4 + 11, num5 - 7, 3);
				g.drawImage(Panel.imgLuong, num4 + 90, num5 - 8, 3);
				mFont.tahoma_7_yellow.drawString(g, Char.myCharz().xuStr + string.Empty, num4 + 24, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_yellow.drawString(g, Char.myCharz().luongStr + string.Empty, num4 + 100, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
				g.drawImage(Panel.imgLuongKhoa, num4 + 150, num5 - 7, 3);
				mFont.tahoma_7_yellow.drawString(g, Char.myCharz().luongKhoaStr + string.Empty, num4 + 160, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
				g.drawImage(Panel.imgTicket, num4 + 200, num5 - 7, 3);
				mFont.tahoma_7_yellow.drawString(g, this.numTicket.ToString() + string.Empty, num4 + 210, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
				if (this.step < 4)
				{
					int num6 = num3 / 2 + 20;
					int num7 = GameCanvas.w - num6;
					g.setColor(11837316);
					g.fillRect(num7, num5, num6, 15);
					if (this.typePrice == 0)
					{
						g.drawImage(Panel.imgXu, num7 + 21, num5 + 8, 3);
					}
					else
					{
						g.drawImage(Panel.imgLuongKhoa, num7 + 21, num5 + 7, 3);
						g.drawImage(Panel.imgLuong, num7 + 18, num5 + 7, 3);
					}
					mFont.tahoma_7_red.drawString(g, " -" + this.cost.ToString(), num7 + 30, num5 + 2, mFont.LEFT, mFont.tahoma_7_grey);
					g.drawImage(Panel.imgTicket, num7 + 80, num5 + 7, 3);
					mFont.tahoma_7_red.drawString(g, " -" + this.checkTicket().ToString(), num7 + 90, num5 + 2, mFont.LEFT, mFont.tahoma_7_grey);
				}
				g.drawImage(GameScr.imgSkill, CrackBallScr.xSkill[0], CrackBallScr.ySkill, 0);
				if (this.indexSkillSelect == 0)
				{
					g.drawImage(GameScr.imgSkill2, CrackBallScr.xSkill[0], CrackBallScr.ySkill, 0);
				}
				if (this.step < 3)
				{
					SmallImage.drawSmallImage(g, 540, CrackBallScr.xSkill[0] + 14, CrackBallScr.ySkill + 14, 0, StaticObj.VCENTER_HCENTER);
				}
				else
				{
					g.drawImage(CrackBallScr.imgReplay, CrackBallScr.xSkill[0] + 14 - 10, CrackBallScr.ySkill + 14 - 10, 0);
				}
				g.drawImage(GameScr.imgSkill, CrackBallScr.xSkill[1], CrackBallScr.ySkill, 0);
				if (this.indexSkillSelect == 1)
				{
					g.drawImage(GameScr.imgSkill2, CrackBallScr.xSkill[1], CrackBallScr.ySkill, 0);
				}
				g.drawImage(CrackBallScr.imgX, CrackBallScr.xSkill[1] + 14 - 10, CrackBallScr.ySkill + 14 - 10, 0);
				if (this.step > 3)
				{
					GameCanvas.paintz.paintFrameSimple(this.xP, this.yP, this.wP, this.hP, g);
					int num8 = GameCanvas.hw - this.idItem.Length * 30 / 2;
					for (int l = 0; l < this.idItem.Length; l++)
					{
						SmallImage.drawSmallImage(g, (int)this.idItem[l], num8 + 5 + l * 30, this.yP + 10, 0, 0);
					}
				}
				if (CrackBallScr.isAutoCrackBall)
				{
					g.drawImage(GameScr.imgSkill2, (CrackBallScr.xSkill[0] + CrackBallScr.xSkill[1]) / 2, CrackBallScr.ySkill, 0);
				}
				else
				{
					g.drawImage(GameScr.imgSkill, (CrackBallScr.xSkill[0] + CrackBallScr.xSkill[1]) / 2, CrackBallScr.ySkill, 0);
				}
				SmallImage.drawSmallImage(g, 4387, (CrackBallScr.xSkill[0] + CrackBallScr.xSkill[1]) / 2 + 14, CrackBallScr.ySkill + 14, 0, StaticObj.VCENTER_HCENTER);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00311F48 File Offset: 0x00310148
		public void DoneCrackBallScr(short[] idImage)
		{
			this.step = 3;
			this.idItem = idImage;
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x00311F58 File Offset: 0x00310158
		public override void switchToMe()
		{
			GameScr.isPaintOther = true;
			GameScr.gI().isRongThanXuatHien = true;
			base.switchToMe();
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x00311F74 File Offset: 0x00310174
		private byte checkTicket()
		{
			byte b = 0;
			for (int i = 0; i < this.listBall.Length; i++)
			{
				if (this.listBall[i].isDone)
				{
					b += 1;
				}
			}
			if ((int)b > this.numTicket)
			{
				b = (byte)this.numTicket;
			}
			return b;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x00311FBC File Offset: 0x003101BC
		private byte checkNum()
		{
			byte b = 0;
			for (int i = 0; i < this.listBall.Length; i++)
			{
				if (this.listBall[i].isDone)
				{
					b += 1;
				}
			}
			b -= this.checkTicket();
			if (b <= 0)
			{
				b = 0;
			}
			return b;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x00312004 File Offset: 0x00310204
		private void checkNumTicket()
		{
			for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
			{
				if (Char.myCharz().arrItemBag[i] != null && Char.myCharz().arrItemBag[i].template.id == this.idTicket)
				{
					this.numTicket = Char.myCharz().arrItemBag[i].quantity;
					return;
				}
			}
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x0031206C File Offset: 0x0031026C
		private void useSkillCrackBall()
		{
			if (this.step < 2)
			{
				if (this.checkTicket() + this.checkNum() > 0)
				{
					this.step = 2;
					SoundMn.gI().gong();
					Char.myCharz().setSkillPaint(GameScr.sks[13], 0);
					this.timeKame = GameCanvas.timeNow + (long)Res.random(2000, 3000);
					return;
				}
			}
			else if (this.yP == GameCanvas.hh / 3)
			{
				Service.gI().SendCrackBall(this.typePrice, 0);
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x003120F3 File Offset: 0x003102F3
		public void startAutoCrackBall()
		{
			new Thread(new ThreadStart(this.AutoCrackBall)).Start();
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x0031210B File Offset: 0x0031030B
		public void stopAutoCrackBall()
		{
			CrackBallScr.isAutoCrackBall = false;
			this.indexSkillSelect = -1;
			CrackBallScr.isCallStop = false;
			CrackBallScr.isContinue = false;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x00312128 File Offset: 0x00310328
		public void AutoCrackBall()
		{
			int num = 0;
			bool flag = false;
			try
			{
				while (CrackBallScr.isAutoCrackBall && GameCanvas.currentScreen == CrackBallScr.instance)
				{
					this.indexSkillSelect = 2;
					while (num < 7 && this.step != 5)
					{
						this.doClickBall(num);
						num++;
						Thread.Sleep(300);
					}
					if (num == 7)
					{
						Thread.Sleep(800);
						CrackBallScr.gI().useSkillCrackBall();
						Thread.Sleep(4000);
						if (CrackBallScr.isCallStop)
						{
							this.stopAutoCrackBall();
							Thread.ResetAbort();
							break;
						}
						if (this.step == 5)
						{
							CrackBallScr.gI().useSkillCrackBall();
							num = 0;
						}
						Thread.Sleep(1000);
					}
					if (this.step == 5 && num == 0 && !flag)
					{
						flag = true;
						CrackBallScr.gI().useSkillCrackBall();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04005FC0 RID: 24512
		public static CrackBallScr instance;

		// Token: 0x04005FC1 RID: 24513
		private BallInfo[] listBall;

		// Token: 0x04005FC2 RID: 24514
		private byte step;

		// Token: 0x04005FC3 RID: 24515
		private byte typePrice;

		// Token: 0x04005FC4 RID: 24516
		private int rO;

		// Token: 0x04005FC5 RID: 24517
		private int xO;

		// Token: 0x04005FC6 RID: 24518
		private int yO;

		// Token: 0x04005FC7 RID: 24519
		private int angle;

		// Token: 0x04005FC8 RID: 24520
		private int iAngle;

		// Token: 0x04005FC9 RID: 24521
		private int iDot;

		// Token: 0x04005FCA RID: 24522
		private int yTo;

		// Token: 0x04005FCB RID: 24523
		private int indexSelect;

		// Token: 0x04005FCC RID: 24524
		private int indexSkillSelect;

		// Token: 0x04005FCD RID: 24525
		private int numTicket;

		// Token: 0x04005FCE RID: 24526
		private int xP;

		// Token: 0x04005FCF RID: 24527
		private int yP;

		// Token: 0x04005FD0 RID: 24528
		private int wP;

		// Token: 0x04005FD1 RID: 24529
		private int hP;

		// Token: 0x04005FD2 RID: 24530
		private int price;

		// Token: 0x04005FD3 RID: 24531
		private int cost;

		// Token: 0x04005FD4 RID: 24532
		private int countFr;

		// Token: 0x04005FD5 RID: 24533
		private int countKame;

		// Token: 0x04005FD6 RID: 24534
		private int frame;

		// Token: 0x04005FD7 RID: 24535
		private int vp;

		// Token: 0x04005FD8 RID: 24536
		private int[] xArg;

		// Token: 0x04005FD9 RID: 24537
		private int[] yArg;

		// Token: 0x04005FDA RID: 24538
		private int[] xDot;

		// Token: 0x04005FDB RID: 24539
		private int[] yDot;

		// Token: 0x04005FDC RID: 24540
		private short[] idItem;

		// Token: 0x04005FDD RID: 24541
		private long timeStart;

		// Token: 0x04005FDE RID: 24542
		private long timeKame;

		// Token: 0x04005FDF RID: 24543
		private bool isKame;

		// Token: 0x04005FE0 RID: 24544
		private bool isCanSkill;

		// Token: 0x04005FE1 RID: 24545
		private bool isSendSv;

		// Token: 0x04005FE2 RID: 24546
		private short idTicket;

		// Token: 0x04005FE3 RID: 24547
		private static int ySkill;

		// Token: 0x04005FE4 RID: 24548
		private static int[] xSkill;

		// Token: 0x04005FE5 RID: 24549
		private static FrameImage fraImgKame;

		// Token: 0x04005FE6 RID: 24550
		private static FrameImage fraImgKame_1;

		// Token: 0x04005FE7 RID: 24551
		private static FrameImage fraImgKame_2;

		// Token: 0x04005FE8 RID: 24552
		private static Image imgX;

		// Token: 0x04005FE9 RID: 24553
		private static Image imgReplay;

		// Token: 0x04005FEA RID: 24554
		public static bool isAutoCrackBall;

		// Token: 0x04005FEB RID: 24555
		public static bool isCallStop;

		// Token: 0x04005FEC RID: 24556
		public static bool isContinue;

		// Token: 0x04005FED RID: 24557
		private byte[] fr = new byte[]
		{
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			19,
			20
		};

		// Token: 0x04005FEE RID: 24558
		private byte[] nFrame = new byte[]
		{
			0,
			0,
			0,
			1,
			1,
			1,
			2,
			2,
			2,
			3,
			3,
			3
		};
	}
}
