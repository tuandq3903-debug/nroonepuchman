using System;
using System.Threading;

namespace Game4
{
	// Token: 0x020001D6 RID: 470
	public class CrackBallScr : mScreen
	{
		// Token: 0x060014EC RID: 5356 RVA: 0x00151720 File Offset: 0x0014F920
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

		// Token: 0x060014ED RID: 5357 RVA: 0x00151845 File Offset: 0x0014FA45
		public static CrackBallScr gI()
		{
			if (CrackBallScr.instance == null)
			{
				CrackBallScr.instance = new CrackBallScr();
			}
			return CrackBallScr.instance;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00151860 File Offset: 0x0014FA60
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

		// Token: 0x060014EF RID: 5359 RVA: 0x00151A00 File Offset: 0x0014FC00
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

		// Token: 0x060014F0 RID: 5360 RVA: 0x00151ACC File Offset: 0x0014FCCC
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

		// Token: 0x060014F1 RID: 5361 RVA: 0x00151C54 File Offset: 0x0014FE54
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

		// Token: 0x060014F2 RID: 5362 RVA: 0x001523F0 File Offset: 0x001505F0
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

		// Token: 0x060014F3 RID: 5363 RVA: 0x001524B4 File Offset: 0x001506B4
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

		// Token: 0x060014F4 RID: 5364 RVA: 0x00152584 File Offset: 0x00150784
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

		// Token: 0x060014F5 RID: 5365 RVA: 0x00152648 File Offset: 0x00150848
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

		// Token: 0x060014F6 RID: 5366 RVA: 0x0015272C File Offset: 0x0015092C
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

		// Token: 0x060014F7 RID: 5367 RVA: 0x00152D5C File Offset: 0x00150F5C
		public void DoneCrackBallScr(short[] idImage)
		{
			this.step = 3;
			this.idItem = idImage;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00152D6C File Offset: 0x00150F6C
		public override void switchToMe()
		{
			GameScr.isPaintOther = true;
			GameScr.gI().isRongThanXuatHien = true;
			base.switchToMe();
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00152D88 File Offset: 0x00150F88
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

		// Token: 0x060014FA RID: 5370 RVA: 0x00152DD0 File Offset: 0x00150FD0
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

		// Token: 0x060014FB RID: 5371 RVA: 0x00152E18 File Offset: 0x00151018
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

		// Token: 0x060014FC RID: 5372 RVA: 0x00152E80 File Offset: 0x00151080
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

		// Token: 0x060014FD RID: 5373 RVA: 0x00152F07 File Offset: 0x00151107
		public void startAutoCrackBall()
		{
			new Thread(new ThreadStart(this.AutoCrackBall)).Start();
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00152F1F File Offset: 0x0015111F
		public void stopAutoCrackBall()
		{
			CrackBallScr.isAutoCrackBall = false;
			this.indexSkillSelect = -1;
			CrackBallScr.isCallStop = false;
			CrackBallScr.isContinue = false;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00152F3C File Offset: 0x0015113C
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

		// Token: 0x04002843 RID: 10307
		public static CrackBallScr instance;

		// Token: 0x04002844 RID: 10308
		private BallInfo[] listBall;

		// Token: 0x04002845 RID: 10309
		private byte step;

		// Token: 0x04002846 RID: 10310
		private byte typePrice;

		// Token: 0x04002847 RID: 10311
		private int rO;

		// Token: 0x04002848 RID: 10312
		private int xO;

		// Token: 0x04002849 RID: 10313
		private int yO;

		// Token: 0x0400284A RID: 10314
		private int angle;

		// Token: 0x0400284B RID: 10315
		private int iAngle;

		// Token: 0x0400284C RID: 10316
		private int iDot;

		// Token: 0x0400284D RID: 10317
		private int yTo;

		// Token: 0x0400284E RID: 10318
		private int indexSelect;

		// Token: 0x0400284F RID: 10319
		private int indexSkillSelect;

		// Token: 0x04002850 RID: 10320
		private int numTicket;

		// Token: 0x04002851 RID: 10321
		private int xP;

		// Token: 0x04002852 RID: 10322
		private int yP;

		// Token: 0x04002853 RID: 10323
		private int wP;

		// Token: 0x04002854 RID: 10324
		private int hP;

		// Token: 0x04002855 RID: 10325
		private int price;

		// Token: 0x04002856 RID: 10326
		private int cost;

		// Token: 0x04002857 RID: 10327
		private int countFr;

		// Token: 0x04002858 RID: 10328
		private int countKame;

		// Token: 0x04002859 RID: 10329
		private int frame;

		// Token: 0x0400285A RID: 10330
		private int vp;

		// Token: 0x0400285B RID: 10331
		private int[] xArg;

		// Token: 0x0400285C RID: 10332
		private int[] yArg;

		// Token: 0x0400285D RID: 10333
		private int[] xDot;

		// Token: 0x0400285E RID: 10334
		private int[] yDot;

		// Token: 0x0400285F RID: 10335
		private short[] idItem;

		// Token: 0x04002860 RID: 10336
		private long timeStart;

		// Token: 0x04002861 RID: 10337
		private long timeKame;

		// Token: 0x04002862 RID: 10338
		private bool isKame;

		// Token: 0x04002863 RID: 10339
		private bool isCanSkill;

		// Token: 0x04002864 RID: 10340
		private bool isSendSv;

		// Token: 0x04002865 RID: 10341
		private short idTicket;

		// Token: 0x04002866 RID: 10342
		private static int ySkill;

		// Token: 0x04002867 RID: 10343
		private static int[] xSkill;

		// Token: 0x04002868 RID: 10344
		private static FrameImage fraImgKame;

		// Token: 0x04002869 RID: 10345
		private static FrameImage fraImgKame_1;

		// Token: 0x0400286A RID: 10346
		private static FrameImage fraImgKame_2;

		// Token: 0x0400286B RID: 10347
		private static Image imgX;

		// Token: 0x0400286C RID: 10348
		private static Image imgReplay;

		// Token: 0x0400286D RID: 10349
		public static bool isAutoCrackBall;

		// Token: 0x0400286E RID: 10350
		public static bool isCallStop;

		// Token: 0x0400286F RID: 10351
		public static bool isContinue;

		// Token: 0x04002870 RID: 10352
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

		// Token: 0x04002871 RID: 10353
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
