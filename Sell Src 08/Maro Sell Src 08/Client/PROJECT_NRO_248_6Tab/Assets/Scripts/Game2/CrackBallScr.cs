using System;
using System.Threading;

namespace Game2
{
	// Token: 0x02000386 RID: 902
	public class CrackBallScr : mScreen
	{
		// Token: 0x06002834 RID: 10292 RVA: 0x0027B868 File Offset: 0x00279A68
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

		// Token: 0x06002835 RID: 10293 RVA: 0x0027B98D File Offset: 0x00279B8D
		public static CrackBallScr gI()
		{
			if (CrackBallScr.instance == null)
			{
				CrackBallScr.instance = new CrackBallScr();
			}
			return CrackBallScr.instance;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0027B9A8 File Offset: 0x00279BA8
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

		// Token: 0x06002837 RID: 10295 RVA: 0x0027BB48 File Offset: 0x00279D48
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

		// Token: 0x06002838 RID: 10296 RVA: 0x0027BC14 File Offset: 0x00279E14
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

		// Token: 0x06002839 RID: 10297 RVA: 0x0027BD9C File Offset: 0x00279F9C
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

		// Token: 0x0600283A RID: 10298 RVA: 0x0027C538 File Offset: 0x0027A738
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

		// Token: 0x0600283B RID: 10299 RVA: 0x0027C5FC File Offset: 0x0027A7FC
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

		// Token: 0x0600283C RID: 10300 RVA: 0x0027C6CC File Offset: 0x0027A8CC
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

		// Token: 0x0600283D RID: 10301 RVA: 0x0027C790 File Offset: 0x0027A990
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

		// Token: 0x0600283E RID: 10302 RVA: 0x0027C874 File Offset: 0x0027AA74
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

		// Token: 0x0600283F RID: 10303 RVA: 0x0027CEA4 File Offset: 0x0027B0A4
		public void DoneCrackBallScr(short[] idImage)
		{
			this.step = 3;
			this.idItem = idImage;
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0027CEB4 File Offset: 0x0027B0B4
		public override void switchToMe()
		{
			GameScr.isPaintOther = true;
			GameScr.gI().isRongThanXuatHien = true;
			base.switchToMe();
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0027CED0 File Offset: 0x0027B0D0
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

		// Token: 0x06002842 RID: 10306 RVA: 0x0027CF18 File Offset: 0x0027B118
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

		// Token: 0x06002843 RID: 10307 RVA: 0x0027CF60 File Offset: 0x0027B160
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

		// Token: 0x06002844 RID: 10308 RVA: 0x0027CFC8 File Offset: 0x0027B1C8
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

		// Token: 0x06002845 RID: 10309 RVA: 0x0027D04F File Offset: 0x0027B24F
		public void startAutoCrackBall()
		{
			new Thread(new ThreadStart(this.AutoCrackBall)).Start();
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x0027D067 File Offset: 0x0027B267
		public void stopAutoCrackBall()
		{
			CrackBallScr.isAutoCrackBall = false;
			this.indexSkillSelect = -1;
			CrackBallScr.isCallStop = false;
			CrackBallScr.isContinue = false;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x0027D084 File Offset: 0x0027B284
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

		// Token: 0x04004D41 RID: 19777
		public static CrackBallScr instance;

		// Token: 0x04004D42 RID: 19778
		private BallInfo[] listBall;

		// Token: 0x04004D43 RID: 19779
		private byte step;

		// Token: 0x04004D44 RID: 19780
		private byte typePrice;

		// Token: 0x04004D45 RID: 19781
		private int rO;

		// Token: 0x04004D46 RID: 19782
		private int xO;

		// Token: 0x04004D47 RID: 19783
		private int yO;

		// Token: 0x04004D48 RID: 19784
		private int angle;

		// Token: 0x04004D49 RID: 19785
		private int iAngle;

		// Token: 0x04004D4A RID: 19786
		private int iDot;

		// Token: 0x04004D4B RID: 19787
		private int yTo;

		// Token: 0x04004D4C RID: 19788
		private int indexSelect;

		// Token: 0x04004D4D RID: 19789
		private int indexSkillSelect;

		// Token: 0x04004D4E RID: 19790
		private int numTicket;

		// Token: 0x04004D4F RID: 19791
		private int xP;

		// Token: 0x04004D50 RID: 19792
		private int yP;

		// Token: 0x04004D51 RID: 19793
		private int wP;

		// Token: 0x04004D52 RID: 19794
		private int hP;

		// Token: 0x04004D53 RID: 19795
		private int price;

		// Token: 0x04004D54 RID: 19796
		private int cost;

		// Token: 0x04004D55 RID: 19797
		private int countFr;

		// Token: 0x04004D56 RID: 19798
		private int countKame;

		// Token: 0x04004D57 RID: 19799
		private int frame;

		// Token: 0x04004D58 RID: 19800
		private int vp;

		// Token: 0x04004D59 RID: 19801
		private int[] xArg;

		// Token: 0x04004D5A RID: 19802
		private int[] yArg;

		// Token: 0x04004D5B RID: 19803
		private int[] xDot;

		// Token: 0x04004D5C RID: 19804
		private int[] yDot;

		// Token: 0x04004D5D RID: 19805
		private short[] idItem;

		// Token: 0x04004D5E RID: 19806
		private long timeStart;

		// Token: 0x04004D5F RID: 19807
		private long timeKame;

		// Token: 0x04004D60 RID: 19808
		private bool isKame;

		// Token: 0x04004D61 RID: 19809
		private bool isCanSkill;

		// Token: 0x04004D62 RID: 19810
		private bool isSendSv;

		// Token: 0x04004D63 RID: 19811
		private short idTicket;

		// Token: 0x04004D64 RID: 19812
		private static int ySkill;

		// Token: 0x04004D65 RID: 19813
		private static int[] xSkill;

		// Token: 0x04004D66 RID: 19814
		private static FrameImage fraImgKame;

		// Token: 0x04004D67 RID: 19815
		private static FrameImage fraImgKame_1;

		// Token: 0x04004D68 RID: 19816
		private static FrameImage fraImgKame_2;

		// Token: 0x04004D69 RID: 19817
		private static Image imgX;

		// Token: 0x04004D6A RID: 19818
		private static Image imgReplay;

		// Token: 0x04004D6B RID: 19819
		public static bool isAutoCrackBall;

		// Token: 0x04004D6C RID: 19820
		public static bool isCallStop;

		// Token: 0x04004D6D RID: 19821
		public static bool isContinue;

		// Token: 0x04004D6E RID: 19822
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

		// Token: 0x04004D6F RID: 19823
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
