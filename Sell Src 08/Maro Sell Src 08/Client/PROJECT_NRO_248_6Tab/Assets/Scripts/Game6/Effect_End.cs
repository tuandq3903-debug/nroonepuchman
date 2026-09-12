using System;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000037 RID: 55
	public class Effect_End
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0002C39C File Offset: 0x0002A59C
		public Effect_End(int type, int typeSub, int typePaint, Char charUse, Point target, int levelPaint, short timeRemove, short range, sbyte level)
		{
			this.f = 0;
			this.stt = 0;
			this.typeEffect = type;
			this.typeSub = typeSub;
			this.typePaint = typePaint;
			this.charUse = charUse;
			this.skillLevel = level;
			if (charUse.containsCaiTrang(1265))
			{
				if (this.typeEffect == 21 || this.typeEffect == 22 || this.typeEffect == 23)
				{
					this.charUse.cx += 10 * this.charUse.cdir;
				}
				else if (this.typeEffect == 18 || this.typeEffect == 19 || this.typeEffect == 20)
				{
					this.charUse.cx += -15 * this.charUse.cdir;
				}
				else
				{
					this.charUse.cx += 15 * this.charUse.cdir;
				}
			}
			this.x = this.charUse.cx;
			this.y = this.charUse.cy;
			this.dir = this.charUse.cdir;
			this.dir_nguoc = ((this.dir == -1) ? 2 : 0);
			this.target = target;
			this.levelPaint = levelPaint;
			this.time = mSystem.currentTimeMillis();
			this.timeRemove = timeRemove;
			this.range = (int)range;
			this.isRemove = (this.isAddSub = false);
			this.n_frame = 4;
			this.get_Img_Skill();
			this.create_Effect();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0002C59C File Offset: 0x0002A79C
		public Effect_End(int type, int typeSub, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
		{
			this.f = 0;
			this.stt = 0;
			this.typeEffect = type;
			this.typeSub = typeSub;
			this.typePaint = typePaint;
			this.x = x;
			this.y = y;
			this.levelPaint = levelPaint;
			this.dir = dir;
			this.dir_nguoc = ((dir == -1) ? 2 : 0);
			this.time = mSystem.currentTimeMillis();
			this.timeRemove = timeRemove;
			this.isRemove = (this.isAddSub = false);
			this.n_frame = 4;
			if (listObj != null)
			{
				this.listObj = new Point[listObj.Length];
				for (int i = 0; i < this.listObj.Length; i++)
				{
					this.listObj[i] = listObj[i];
				}
			}
			this.get_Img_Skill();
			this.create_Effect();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0002C6DC File Offset: 0x0002A8DC
		public Effect_End(int type, int typeSub, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj, sbyte level)
		{
			this.f = 0;
			this.stt = 0;
			this.typeEffect = type;
			this.typeSub = typeSub;
			this.typePaint = typePaint;
			this.x = x;
			this.y = y;
			this.levelPaint = levelPaint;
			this.skillLevel = level;
			this.dir = dir;
			this.dir_nguoc = ((dir == -1) ? 2 : 0);
			this.time = mSystem.currentTimeMillis();
			this.timeRemove = timeRemove;
			this.isRemove = (this.isAddSub = false);
			this.n_frame = 4;
			if (listObj != null)
			{
				this.listObj = new Point[listObj.Length];
				for (int i = 0; i < this.listObj.Length; i++)
				{
					this.listObj[i] = listObj[i];
				}
			}
			this.get_Img_Skill();
			this.create_Effect();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0002C824 File Offset: 0x0002AA24
		public static Image getImage(int id)
		{
			if (id < 0)
			{
				return null;
			}
			string path = "/e/e_" + id.ToString() + ".png";
			Image result = null;
			try
			{
				result = mSystem.loadImage(path);
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0002C870 File Offset: 0x0002AA70
		public static void setSoundSkill_END(int x, int y, int typeEffect)
		{
			try
			{
				int num = -1;
				Res.random(3);
				if (num >= 0)
				{
					SoundMn.playSound(x, y, num, SoundMn.volume);
				}
			}
			catch (Exception ex)
			{
				Res.err("ERR setSoundSkill_END: " + ex.ToString());
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0002C8C4 File Offset: 0x0002AAC4
		public void create_Effect()
		{
			try
			{
				Effect_End.setSoundSkill_END(this.x, this.y, this.typeEffect);
				switch (this.typeEffect)
				{
				case 0:
				case 1:
				case 2:
					this.set_End_String(this.typeEffect);
					break;
				case 3:
					this.set_FireWork();
					break;
				case 9:
					this.set_LINE_IN();
					break;
				case 10:
				case 11:
					this.set_End_Rock();
					break;
				case 16:
				case 17:
					this.set_Sub();
					break;
				case 18:
				case 19:
				case 20:
					this.set_Pow();
					break;
				case 21:
				case 22:
				case 23:
					this.set_Gong();
					break;
				case 24:
					this.set_Skill_Kamex10();
					break;
				case 25:
					this.set_Skill_Destroy();
					break;
				case 26:
					this.set_Skill_MaFuba();
					break;
				}
			}
			catch (Exception ex)
			{
				Res.err("ERR create_Effect: " + ex.ToString());
				this.removeEff();
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0002C9E8 File Offset: 0x0002ABE8
		public void update()
		{
			try
			{
				this.f++;
				switch (this.typeEffect)
				{
				case 0:
				case 1:
				case 2:
					this.upd_End_String();
					break;
				case 3:
					this.upd_FireWork();
					break;
				case 9:
					this.upd_LINE_IN();
					break;
				case 10:
				case 11:
					this.upd_End_Rock();
					break;
				case 16:
				case 17:
					this.upd_Sub();
					break;
				case 18:
				case 19:
				case 20:
					this.upd_Pow();
					break;
				case 21:
				case 22:
				case 23:
					this.upd_Gong();
					break;
				case 24:
					this.upd_Skill_Kamex10();
					break;
				case 25:
					this.upd_Skill_Destroy();
					break;
				case 26:
					this.upd_Skill_MaFuba();
					break;
				}
			}
			catch (Exception ex)
			{
				Res.err("ERR update: " + ex.ToString());
				this.removeEff();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0002CAFC File Offset: 0x0002ACFC
		public void paint(mGraphics g)
		{
			try
			{
				if (!this.isRemove && this.f >= 0)
				{
					switch (this.typeEffect)
					{
					case 0:
					case 1:
					case 2:
						this.pnt_End_String(g);
						break;
					case 3:
						this.pnt_FireWork(g);
						break;
					case 9:
						this.pnt_LINE_IN(g);
						break;
					case 10:
					case 11:
						this.pnt_End_Rock(g);
						break;
					case 16:
						if (this.typeSub == 0)
						{
							this.pnt_Sub(g, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
						else
						{
							this.pnt_Sub(g, mGraphics.VCENTER | mGraphics.HCENTER);
						}
						break;
					case 17:
						this.pnt_Sub(g, mGraphics.VCENTER);
						break;
					case 18:
					case 19:
					case 20:
						this.pnt_Pow(g, mGraphics.BOTTOM | mGraphics.HCENTER);
						break;
					case 21:
					case 22:
					case 23:
						this.pnt_Gong(g, mGraphics.VCENTER | mGraphics.HCENTER);
						break;
					case 24:
						this.pnt_Skill_Kamex10(g);
						break;
					case 25:
						this.pnt_Skill_Destroy(g);
						break;
					case 26:
						this.pnt_Skill_MaFuba(g);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Res.err(ex.ToString());
				this.removeEff();
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0002CC7C File Offset: 0x0002AE7C
		public void removeEff()
		{
			this.isRemove = true;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0002CC88 File Offset: 0x0002AE88
		private void set_End_String(int typeEffect)
		{
			switch (typeEffect)
			{
			case 0:
				this.fraImgEff = new FrameImage(4);
				break;
			case 1:
				this.fraImgEff = new FrameImage(5);
				break;
			case 2:
				this.fraImgEff = new FrameImage(6);
				break;
			}
			this.fRemove = 100;
			this.dy_throw = GameCanvas.h / 3 + 10;
			this.vy = 10;
			this.y1000 = 0;
			this.isAddSub = false;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0002CD00 File Offset: 0x0002AF00
		private void upd_End_String()
		{
			this.x = GameCanvas.hw;
			this.y = this.y1000;
			if (this.f > this.fRemove)
			{
				this.removeEff();
			}
			this.vy++;
			if (this.vy > 15)
			{
				this.vy = 15;
			}
			if (this.y1000 + this.vy < this.dy_throw)
			{
				this.y1000 += this.vy;
				return;
			}
			this.y1000 = this.dy_throw;
			if (!this.isAddSub)
			{
				this.isAddSub = true;
				if (this.typeSub != -1)
				{
					GameScr.addEffectEnd(this.typeSub, 0, 0, this.x, this.y, this.levelPaint, 0, -1, null);
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0002CDC7 File Offset: 0x0002AFC7
		private void pnt_End_String(mGraphics g)
		{
			if (this.fraImgEff != null)
			{
				this.fraImgEff.drawFrame(this.f / 5 % this.fraImgEff.nFrame, this.x, this.y, 0, 33, g);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0002CE00 File Offset: 0x0002B000
		private void set_FireWork()
		{
			int num = Res.random(3, 5);
			this.fRemove = 90;
			for (int i = 0; i < num; i++)
			{
				Point point = new Point();
				point.x = this.x + Res.random_Am_0(4);
				point.y = this.y + Res.random_Am_0(5);
				if (this.typeSub == 0)
				{
					point.fRe = Res.random(10);
					int num2 = 1;
					if (i % 2 == 0)
					{
						num2 = -1;
					}
					point.x = this.x + Res.random((int)(Effect_End.arrInfoEff[5][0] / 2)) * num2;
					point.y = this.y - Res.random((int)(Effect_End.arrInfoEff[5][1] / 2));
					point.fraImgEff = new FrameImage(7);
				}
				this.VecEffEnd.addElement(point);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0002CED0 File Offset: 0x0002B0D0
		private void upd_FireWork()
		{
			for (int i = 0; i < this.VecEffEnd.size(); i++)
			{
				Point point = (Point)this.VecEffEnd.elementAt(i);
				point.update();
				if (point.f == point.fRe)
				{
					SoundMn.playSound(point.x, point.y, SoundMn.FIREWORK, SoundMn.volume);
				}
				if (point.f - point.fRe > point.fraImgEff.nFrame * 3 - 1)
				{
					point.f = 0;
					if (this.typeSub == 0)
					{
						point.fRe = Res.random(10);
						int num = 1;
						if (i % 2 == 0)
						{
							num = -1;
						}
						point.x = this.x + Res.random((int)(Effect_End.arrInfoEff[5][0] / 2)) * num;
						point.y = this.y - Res.random((int)(Effect_End.arrInfoEff[5][1] / 2));
					}
				}
			}
			if (this.f >= this.fRemove)
			{
				this.removeEff();
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
		private void pnt_FireWork(mGraphics g)
		{
			for (int i = 0; i < this.VecEffEnd.size(); i++)
			{
				Point point = (Point)this.VecEffEnd.elementAt(i);
				if (point.f - point.fRe > -1 && point.fraImgEff != null)
				{
					point.fraImgEff.drawFrame((point.f - point.fRe) / 3 % point.fraImgEff.nFrame, point.x, point.y, 0, 3, g);
				}
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0002D054 File Offset: 0x0002B254
		private void set_Skill_Kamex10()
		{
			this.w = this.fra_skill[0].frameWidth;
			this.h = this.fra_skill[0].frameHeight;
			this.vMax = Res.abs(this.x - this.target.x);
			this.nFrame = new byte[]
			{
				0,
				0,
				0,
				1,
				1,
				1
			};
			this.isAddSub = false;
			SoundMn.playSound(this.x, this.y, SoundMn.KAMEX10_1, SoundMn.volume);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0002D0E0 File Offset: 0x0002B2E0
		private void upd_Skill_Kamex10()
		{
			this.fSpeed++;
			this.w += 20;
			if (this.w > this.vMax)
			{
				this.w = this.vMax;
			}
			this.x = this.charUse.cx + 10;
			this.y = this.charUse.cy - 3;
			if (this.dir == -1)
			{
				this.x = this.charUse.cx - this.w - 10;
			}
			if (!this.isAddSub && GameCanvas.timeNow - this.time >= (long)this.timeRemove)
			{
				this.f = 0;
				this.nFrame = new byte[]
				{
					2,
					2,
					2,
					3,
					3,
					3
				};
				this.isAddSub = true;
			}
			if (this.f > this.nFrame.Length - 1)
			{
				if (this.isAddSub)
				{
					this.removeEff();
					return;
				}
				this.f = 0;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0002D1DC File Offset: 0x0002B3DC
		private void pnt_Skill_Kamex10(mGraphics g)
		{
			if (this.fra_skill != null)
			{
				g.setClip(this.x, this.y - this.h / 2, this.w, this.h);
				this.Fill_Rect_Img(g, this.fra_skill[0], this.fra_skill[1], this.fra_skill[2], (int)this.nFrame[this.f], this.x, this.y, this.vMax);
				GameCanvas.resetTransGameScr(g);
				if (this.dir == -1 && this.fra_skill[0] != null)
				{
					this.fra_skill[0].drawFrame((int)this.nFrame[this.f], this.x + this.w - this.fra_skill[0].frameWidth, this.y - this.fra_skill[0].frameHeight / 2 - 1, 2, 0, g);
				}
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0002D2C4 File Offset: 0x0002B4C4
		private void set_Skill_Destroy()
		{
			this.x = this.charUse.cx + 20 * this.charUse.cdir;
			int num = 15;
			this.fMove = (int)this.timeRemove / num;
			if (this.target != null)
			{
				for (int i = 0; i < num; i++)
				{
					Point point = new Point();
					point.fraImgEff = this.fra_skill[0];
					point.fraImgEff_2 = this.fra_skill[2];
					point.x = this.x;
					point.y = this.y;
					if (this.target != null)
					{
						point.toX = this.target.x;
						point.toY = this.target.y;
						if (this.range > 0)
						{
							point.toX += Res.random_Am(0, this.range);
							point.toY += Res.random_Am(0, this.range);
						}
					}
					this.vMax = Res.random(9, 12);
					if (i == num - 1)
					{
						point.fraImgEff = this.fra_skill[1];
						point.fraImgEff_2 = this.fra_skill[3];
						point.toX = this.target.x;
						point.toY = this.target.y;
						this.vMax = 9;
					}
					point.isPaint = false;
					point.isChange = false;
					point.isRemove = false;
					point.create_Arrow(this.vMax);
					this.VecEffEnd.addElement(point);
				}
				return;
			}
			this.removeEff();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0002D450 File Offset: 0x0002B650
		private void upd_Skill_Destroy()
		{
			int num = 0;
			for (int i = 0; i < this.VecEffEnd.size(); i++)
			{
				Point point = (Point)this.VecEffEnd.elementAt(i);
				if (!point.isPaint && GameCanvas.timeNow - this.time >= (long)(i * this.fMove))
				{
					point.isPaint = true;
					GameScr.addEffectEnd(17, 0, this.typePaint, this.charUse.cx, this.charUse.cy - 3, 2, this.dir_nguoc, -1, null, this.skillLevel);
					if (i == this.VecEffEnd.size() - 1)
					{
						SoundMn.playSound(point.x, point.y, SoundMn.DESTROY_1, SoundMn.volume);
					}
					else
					{
						SoundMn.playSound(point.x, point.y, SoundMn.DESTROY_0, SoundMn.volume);
					}
				}
				if (point.isPaint && !point.isRemove)
				{
					point.f++;
					if (!point.isChange)
					{
						if (point.f < 10 && i == this.VecEffEnd.size() - 1 && this.charUse != null && !TileMap.tileTypeAt(this.charUse.cx - (this.charUse.chw + 1) * this.charUse.cdir, this.charUse.cy, (this.charUse.cdir != 1) ? 4 : 8))
						{
							this.charUse.cx -= this.charUse.cdir;
						}
						point.moveTo_xy(point.toX, point.toY);
						if (point.x == point.toX)
						{
							point.isChange = true;
							point.f = 0;
						}
					}
					if (point.isChange && point.f >= this.n_frame * point.fraImgEff_2.nFrame)
					{
						point.isRemove = true;
					}
				}
				if (point.isRemove)
				{
					num++;
				}
			}
			if (num == this.VecEffEnd.size())
			{
				this.removeEff();
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0002D664 File Offset: 0x0002B864
		private void pnt_Skill_Destroy(mGraphics g)
		{
			for (int i = 0; i < this.VecEffEnd.size(); i++)
			{
				Point point = (Point)this.VecEffEnd.elementAt(i);
				if (point.isPaint && !point.isRemove)
				{
					if (!point.isChange)
					{
						point.paint_Arrow(g, point.fraImgEff, mGraphics.VCENTER | mGraphics.HCENTER, false);
					}
					if (point.isChange)
					{
						point.fraImgEff_2.drawFrame(point.f / this.n_frame % point.fraImgEff_2.nFrame, point.x, point.y, this.dir_nguoc, mGraphics.VCENTER | mGraphics.HCENTER, g);
					}
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0002D71C File Offset: 0x0002B91C
		private void set_Skill_MaFuba()
		{
			this.nFrame = new byte[]
			{
				0,
				0,
				0,
				1,
				1,
				1,
				2,
				2,
				2
			};
			this.isAddSub = false;
			this.fMove = 10;
			this.x1000 = this.x;
			this.y1000 = this.y + 12;
			this.dy = 25;
			this.dy_throw = 19;
			if (this.typeSub == 1)
			{
				this.dy_throw = 21;
			}
			else if (this.typeSub == 2)
			{
				this.dy_throw = 31;
			}
			this.h = this.fra_skill[1].frameHeight + 50 - this.dy_throw;
			this.vy = 1;
			this.vy1000 = 1;
			this.y = this.y1000 - this.h;
			this.rS = 90;
			this.vMax = 1;
			this.angleS = (this.angleO = 25);
			this.iDotS = 1;
			if (this.listObj != null && this.listObj.Length != 0)
			{
				this.iDotS = this.listObj.Length;
			}
			this.iAngleS = 360 / this.iDotS;
			this.xArgS = new int[this.iDotS];
			this.yArgS = new int[this.iDotS];
			this.xDotS = new int[this.iDotS];
			this.yDotS = new int[this.iDotS];
			GameScr.addEffectEnd(16, 0, this.typePaint, this.x1000, this.y1000, 1, 0, -1, null, this.skillLevel);
			SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_0, SoundMn.volume);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0002D8B8 File Offset: 0x0002BAB8
		private void changeAngleStar()
		{
			if (this.vMax < 40)
			{
				this.vMax += 2;
			}
			this.angleS = this.angleO;
			this.angleS -= this.vMax;
			if (this.angleS >= 360)
			{
				this.angleS -= 360;
			}
			if (this.angleS < 0)
			{
				this.angleS = 360 + this.angleS;
			}
			this.angleO = this.angleS;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0002D944 File Offset: 0x0002BB44
		private void setDotStar()
		{
			for (int i = 0; i < this.yArgS.Length; i++)
			{
				if (this.angleS >= 360)
				{
					this.angleS -= 360;
				}
				if (this.angleS < 0)
				{
					this.angleS = 360 + this.angleS;
				}
				this.yArgS[i] = Res.abs(this.rS * Res.sin(this.angleS) / 1024);
				this.xArgS[i] = Res.abs(this.rS * Res.cos(this.angleS) / 1024);
				if (this.angleS < 90)
				{
					this.xDotS[i] = this.x + this.xArgS[i];
					this.yDotS[i] = this.y - this.yArgS[i];
				}
				else if (this.angleS >= 90 && this.angleS < 180)
				{
					this.xDotS[i] = this.x - this.xArgS[i];
					this.yDotS[i] = this.y - this.yArgS[i];
				}
				else if (this.angleS >= 180 && this.angleS < 270)
				{
					this.xDotS[i] = this.x - this.xArgS[i];
					this.yDotS[i] = this.y + this.yArgS[i];
				}
				else
				{
					this.xDotS[i] = this.x + this.xArgS[i];
					this.yDotS[i] = this.y + this.yArgS[i];
				}
				this.angleS -= this.iAngleS;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0002DB00 File Offset: 0x0002BD00
		private void upd_Skill_MaFuba()
		{
			if (this.stt == 0)
			{
				if (this.f == 3)
				{
					SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_1, SoundMn.volume);
				}
				this.frame++;
				if (this.frame > this.nFrame.Length - 1)
				{
					this.frame = this.nFrame.Length - 1;
				}
				if (this.f == this.fMove + 4)
				{
					GameScr.addEffectEnd(16, 1, this.typePaint, this.x, this.y, 3, 0, 2945, null, this.skillLevel);
				}
				if (this.f > this.fMove + 4)
				{
					this.rS--;
					if (this.rS < 0)
					{
						this.rS = 0;
						this.f = 0;
						this.fSpeed = 0;
						this.nFrame_2 = new byte[]
						{
							1,
							1,
							0,
							0,
							0,
							0,
							1,
							1,
							1,
							1,
							0,
							0,
							0,
							1,
							1,
							1,
							0,
							0,
							1,
							1,
							1,
							2
						};
						this.hideListObj_Mafuba(true);
						this.stt = 1;
						return;
					}
					this.changeAngleStar();
					this.setDotStar();
					this.updListObj_Mafuba(true);
					return;
				}
			}
			else if (this.stt == 1)
			{
				this.fSpeed++;
				if (this.fSpeed > this.nFrame_2.Length - 1)
				{
					this.fSpeed = this.nFrame_2.Length - 1;
					if (GameCanvas.gameTick % 2 == 0)
					{
						this.vy1000++;
					}
					this.vy += this.vy1000;
					if (this.vy >= this.h - this.fra_skill[0].frameHeight - this.dy + this.dy_throw)
					{
						this.vy = this.h - this.fra_skill[0].frameHeight - this.dy + this.dy_throw;
						this.f = 0;
						this.fSpeed = 0;
						this.stt = 2;
						this.nFrame_2 = new byte[]
						{
							3,
							3,
							3,
							3,
							3,
							4,
							4,
							4,
							5,
							5,
							5
						};
						return;
					}
				}
			}
			else if (this.stt == 2)
			{
				this.fSpeed++;
				if (this.fSpeed > this.nFrame_2.Length - 1)
				{
					this.stt = 3;
					this.frame = 0;
					this.nFrame = new byte[]
					{
						2,
						2,
						1,
						1,
						0,
						0,
						3,
						3,
						3,
						0,
						0,
						0,
						4,
						4,
						4,
						0,
						0
					};
					return;
				}
			}
			else if (this.stt == 3)
			{
				this.frame++;
				if (this.frame == 3)
				{
					SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_1, SoundMn.volume);
				}
				if (this.frame > this.nFrame.Length - 1)
				{
					this.frame = 0;
					this.stt = 4;
					this.nFrame = new byte[]
					{
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						3,
						3,
						3,
						0,
						0,
						0,
						4,
						4,
						4,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						3,
						3,
						0,
						0,
						4,
						4
					};
					return;
				}
			}
			else
			{
				this.frame++;
				if (this.frame > this.nFrame.Length - 1)
				{
					this.frame = 0;
				}
				if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
				{
					GameScr.addEffectEnd(16, 0, this.typePaint, this.x1000, this.y1000, 1, 0, -1, null, this.skillLevel);
					this.updListObj_Mafuba(false);
					this.removeEff();
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0002DE44 File Offset: 0x0002C044
		private void pnt_Skill_MaFuba(mGraphics g)
		{
			if (this.fra_skill == null)
			{
				return;
			}
			if (this.nFrame != null)
			{
				this.fra_skill[0].drawFrame((int)this.nFrame[this.frame], this.x1000, this.y1000, 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
			if (this.stt == 1 || this.stt == 2)
			{
				int anchor = mGraphics.BOTTOM | mGraphics.HCENTER;
				int num = this.dy;
				if (this.nFrame_2[this.fSpeed] == 0 || this.nFrame_2[this.fSpeed] == 1)
				{
					anchor = (mGraphics.VCENTER | mGraphics.HCENTER);
					num = 0;
				}
				this.fra_skill[1].drawFrame((int)this.nFrame_2[this.fSpeed], this.x, this.y + num + this.vy, 0, anchor, g);
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0002DF1C File Offset: 0x0002C11C
		private void Fill_Rect_Img(mGraphics g, FrameImage head, FrameImage body, FrameImage foot, int frame, int x, int y, int w)
		{
			int num2 = w;
			bool flag = false;
			if (head != null && foot != null)
			{
				flag = true;
				num2 = w - (head.frameWidth + foot.frameWidth);
			}
			if (num2 > 0)
			{
				int num3 = num2 / body.frameWidth;
				if (num2 % body.frameWidth > 0)
				{
					num3++;
				}
				if (this.dir == -1)
				{
					for (int i = 0; i < num3; i++)
					{
						int num4 = (i != num3 - 1) ? ((!flag) ? (x + i * body.frameWidth) : (x + foot.frameWidth + body.frameWidth + i * body.frameWidth)) : ((!flag) ? (x + w - body.frameWidth) : (x + foot.frameWidth));
						body.drawFrame(frame, num4, y - body.frameHeight / 2, 2, 0, g);
					}
				}
				else
				{
					for (int j = 0; j < num3; j++)
					{
						int num5 = (j != num3 - 1) ? ((!flag) ? (x + j * body.frameWidth) : (x + j * body.frameWidth + head.frameWidth)) : ((!flag) ? (x + w - body.frameWidth) : (x + w - (body.frameWidth + foot.frameWidth)));
						body.drawFrame(frame, num5, y - body.frameHeight / 2, 0, 0, g);
					}
				}
			}
			if (this.dir == -1)
			{
				if (head != null)
				{
					head.drawFrame(frame, x + w - head.frameWidth, y - head.frameHeight / 2, 2, 0, g);
				}
				if (foot != null)
				{
					foot.drawFrame(frame, x, y - foot.frameHeight / 2, 2, 0, g);
					return;
				}
			}
			else
			{
				if (head != null)
				{
					head.drawFrame(frame, x, y - head.frameHeight / 2, 0, 0, g);
				}
				if (foot != null)
				{
					foot.drawFrame(frame, x + w - foot.frameWidth - 1, y - foot.frameHeight / 2, 0, 0, g);
				}
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0002E100 File Offset: 0x0002C300
		private void set_LINE_IN()
		{
			this.indexColorStar = this.typeSub;
			this.x1000 = this.x * 1000;
			this.y1000 = this.y * 1000;
			this.fRemove = Res.random(4, 6);
			this.vMax = 5;
			this.xline = 10;
			this.yline = 20;
			this.create_Star_Line_In(this.vMax, this.xline, this.yline, 0);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0002E17C File Offset: 0x0002C37C
		private void upd_LINE_IN()
		{
			for (int i = 0; i < this.VecEffEnd.size(); i++)
			{
				Line line = (Line)this.VecEffEnd.elementAt(i);
				line.update();
				if (this.f >= this.fRemove)
				{
					this.VecEffEnd.removeElement(line);
					i--;
				}
			}
			if (this.f >= this.fRemove)
			{
				if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
				{
					this.VecEffEnd.removeAllElements();
					this.removeEff();
					return;
				}
				this.fRemove = Res.random(4, 6);
				this.f = 0;
				this.create_Star_Line_In(this.vMax, this.xline, this.yline, 0);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0002E238 File Offset: 0x0002C438
		private void create_Star_Line_In(int vline, int minline, int maxline, int numpoint)
		{
			if (this.f == -1)
			{
				this.VecEffEnd.removeAllElements();
			}
			int num = 4;
			this.colorpaint = new int[num];
			if (maxline <= minline)
			{
				maxline = minline + 1;
			}
			for (int i = 0; i < num; i++)
			{
				if (Res.random(2) == 0)
				{
					this.colorpaint[i] = Effect_End.colorStar[this.indexColorStar][Res.random(3)];
				}
				else
				{
					this.colorpaint[i] = Effect_End.colorStar[this.indexColorStar][2];
				}
			}
			for (int j = 0; j < num; j++)
			{
				Line line = new Line();
				int num2 = 5 + 180 / num * j;
				int num3 = 180 / num + 180 / num * j - 5;
				if (num3 <= num2)
				{
					num3 = num2 + 1;
				}
				int num4 = Res.random(minline, maxline);
				int num5 = Res.random(vline, vline + 3);
				int num6 = Res.random(num2, num3);
				int num7 = Res.random(13, 23);
				bool is2Line = Res.random(4) == 0;
				num6 = Res.fixangle(num6 % 360);
				line.setLine(this.x1000 - Res.sin(num6) * (num4 + num7), this.y1000 - Res.cos(num6) * (num4 + num7), this.x1000 - Res.sin(num6) * num7, this.y1000 - Res.cos(num6) * num7, Res.sin(num6) * num5, Res.cos(num6) * num5, is2Line);
				if (numpoint > 0)
				{
					line.type = Res.random(numpoint);
				}
				this.VecEffEnd.addElement(line);
				line = new Line();
				num6 += 180 + Res.random_Am(2, 5);
				num6 = Res.fixangle(num6 % 360);
				line.setLine(this.x1000 - Res.sin(num6) * (num4 + num7), this.y1000 - Res.cos(num6) * (num4 + num7), this.x1000 - Res.sin(num6) * num7, this.y1000 - Res.cos(num6) * num7, Res.sin(num6) * num5, Res.cos(num6) * num5, is2Line);
				if (numpoint > 0)
				{
					line.type = Res.random(numpoint);
				}
				this.VecEffEnd.addElement(line);
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0002E470 File Offset: 0x0002C670
		private void pnt_LINE_IN(mGraphics g)
		{
			for (int i = 0; i < this.VecEffEnd.size(); i++)
			{
				Line line = (Line)this.VecEffEnd.elementAt(i);
				if (line != null)
				{
					int color = 0;
					if (i / 2 < this.colorpaint.Length)
					{
						color = this.colorpaint[i / 2];
					}
					g.setColor(color);
					g.drawLine(line.x0 / 1000, line.y0 / 1000, line.x1 / 1000, line.y1 / 1000);
					if (line.is2Line)
					{
						g.drawLine(line.x0 / 1000 + 1, line.y0 / 1000, line.x1 / 1000 + 1, line.y1 / 1000);
					}
				}
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0002E54C File Offset: 0x0002C74C
		private void set_End_Rock()
		{
			this.fraImgEff = new FrameImage(8);
			this.fRemove = Res.random(23, 27);
			int num = Res.random(1, 3);
			this.toY = this.y - 40;
			for (int i = 0; i < num; i++)
			{
				Point point = new Point();
				point.x = this.x + Res.random_Am(0, 20);
				point.y = this.y + Res.random_Am_0(7);
				if (this.typeEffect == 10)
				{
					point.frame = Res.random(0, this.fraImgEff.nFrame - 2);
				}
				else if (this.typeEffect == 11)
				{
					point.frame = Res.random(2, this.fraImgEff.nFrame);
				}
				else
				{
					point.frame = Res.random(0, this.fraImgEff.nFrame);
				}
				point.dis = Res.random(2);
				point.vy = -Res.random(1, 4);
				this.VecEffEnd.addElement(point);
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0002E650 File Offset: 0x0002C850
		private void upd_End_Rock()
		{
			for (int i = 0; i < this.VecEffEnd.size(); i++)
			{
				Point point = (Point)this.VecEffEnd.elementAt(i);
				point.update();
				if (point.y < this.toY)
				{
					this.VecEffEnd.removeElementAt(i);
					i--;
				}
			}
			if (this.f >= this.fRemove)
			{
				this.removeEff();
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0002E6BC File Offset: 0x0002C8BC
		private void pnt_End_Rock(mGraphics g)
		{
			for (int i = 0; i < this.VecEffEnd.size(); i++)
			{
				Point point = (Point)this.VecEffEnd.elementAt(i);
				if (this.fraImgEff != null)
				{
					this.fraImgEff.drawFrame(point.frame, point.x, point.y, 0, mGraphics.VCENTER | mGraphics.HCENTER, g);
				}
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0002E724 File Offset: 0x0002C924
		private void updListObj_Mafuba(bool ismafuba)
		{
			if (this.listObj == null)
			{
				return;
			}
			for (int i = 0; i < this.listObj.Length; i++)
			{
				if (this.listObj[i] != null)
				{
					if (this.listObj[i].type == 0)
					{
						Mob mob = GameScr.findMobInMap(this.listObj[i].id);
						if (mob != null)
						{
							mob.isMafuba = ismafuba;
							mob.isHide = false;
							mob.xMFB = this.xDotS[i];
							mob.yMFB = this.yDotS[i];
						}
					}
					else
					{
						Char @char = (Char.myCharz().charID != this.listObj[i].id) ? GameScr.findCharInMap(this.listObj[i].id) : Char.myCharz();
						if (@char != null)
						{
							@char.isMafuba = ismafuba;
							@char.isHide = false;
							@char.xMFB = this.xDotS[i];
							@char.yMFB = this.yDotS[i];
						}
					}
				}
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0002E818 File Offset: 0x0002CA18
		private void hideListObj_Mafuba(bool ishide)
		{
			if (this.listObj == null)
			{
				return;
			}
			for (int i = 0; i < this.listObj.Length; i++)
			{
				if (this.listObj[i] != null)
				{
					if (this.listObj[i].type == 0)
					{
						Mob mob = GameScr.findMobInMap(this.listObj[i].id);
						if (mob != null)
						{
							mob.isHide = ishide;
						}
					}
					else
					{
						Char @char = (Char.myCharz().charID != this.listObj[i].id) ? GameScr.findCharInMap(this.listObj[i].id) : Char.myCharz();
						if (@char != null)
						{
							@char.isHide = ishide;
						}
					}
				}
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0002E8BC File Offset: 0x0002CABC
		private void get_Img_Skill()
		{
			int skill_id = 0;
			int[] skill_arr_ = null;
			int[] skill_arr_2 = null;
			switch (this.typeEffect)
			{
			case 16:
				skill_id = 26;
				if (this.typeSub == 0)
				{
					skill_arr_ = new int[]
					{
						7
					};
					skill_arr_2 = new int[]
					{
						28
					};
				}
				if (this.typeSub == 1)
				{
					skill_arr_ = new int[]
					{
						2
					};
					skill_arr_2 = new int[]
					{
						23
					};
				}
				break;
			case 17:
				skill_id = 25;
				skill_arr_ = new int[]
				{
					2
				};
				skill_arr_2 = new int[]
				{
					16
				};
				break;
			case 18:
				skill_id = 24;
				skill_arr_ = new int[1];
				skill_arr_2 = new int[]
				{
					9
				};
				break;
			case 19:
				skill_id = 25;
				skill_arr_ = new int[1];
				skill_arr_2 = new int[]
				{
					14
				};
				break;
			case 20:
				skill_id = 26;
				skill_arr_ = new int[1];
				skill_arr_2 = new int[]
				{
					21
				};
				break;
			case 21:
				skill_id = 24;
				skill_arr_ = new int[]
				{
					1
				};
				skill_arr_2 = new int[]
				{
					10
				};
				break;
			case 22:
				skill_id = 25;
				skill_arr_ = new int[]
				{
					1
				};
				skill_arr_2 = new int[]
				{
					15
				};
				break;
			case 23:
				skill_id = 26;
				skill_arr_ = new int[]
				{
					1
				};
				skill_arr_2 = new int[]
				{
					22
				};
				break;
			case 24:
				skill_id = 24;
				skill_arr_ = new int[]
				{
					2,
					3,
					4
				};
				skill_arr_2 = new int[]
				{
					11,
					12,
					13
				};
				break;
			case 25:
				skill_id = 25;
				skill_arr_ = new int[]
				{
					3,
					4,
					5,
					6
				};
				skill_arr_2 = new int[]
				{
					17,
					18,
					19,
					20
				};
				break;
			case 26:
			{
				skill_id = 26;
				int num2 = 0;
				int num3 = 0;
				if (this.typeSub == 0)
				{
					num2 = 4;
					num3 = 25;
				}
				else if (this.typeSub == 1)
				{
					num2 = 5;
					num3 = 26;
				}
				else if (this.typeSub == 2)
				{
					num2 = 6;
					num3 = 27;
				}
				skill_arr_ = new int[]
				{
					num2,
					3
				};
				skill_arr_2 = new int[]
				{
					num3,
					24
				};
				break;
			}
			}
			if (skill_arr_ == null || skill_arr_2 == null)
			{
				return;
			}
			this.fra_skill = new FrameImage[skill_arr_.Length];
			for (int i = 0; i < skill_arr_.Length; i++)
			{
				string nameImg = string.Concat(new string[]
				{
					"Skills_",
					skill_id.ToString(),
					"_",
					this.typePaint.ToString(),
					"_",
					skill_arr_[i].ToString()
				});
				Debug.Log("NAME: " + nameImg);
				FrameImage frameImage = mSystem.getFraImage(nameImg);
				if (frameImage == null)
				{
					frameImage = new FrameImage(skill_arr_2[i]);
				}
				if (frameImage != null)
				{
					this.fra_skill[i] = frameImage;
				}
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0002EB88 File Offset: 0x0002CD88
		private void set_Gong()
		{
			if (this.charUse != null)
			{
				if (this.typeEffect == 21)
				{
					this.x = this.charUse.cx - 3 * this.charUse.cdir;
					this.y = this.charUse.cy;
					SoundMn.playSound(this.x, this.y, SoundMn.KAMEX10_0, SoundMn.volume);
					return;
				}
				if (this.typeEffect == 22)
				{
					this.x = this.charUse.cx + 20 * this.charUse.cdir;
					this.y = this.charUse.cy - 4;
					SoundMn.playSound(this.x, this.y, SoundMn.DESTROY_2, SoundMn.volume);
					return;
				}
				if (this.typeEffect == 23)
				{
					this.x = this.charUse.cx;
					this.y = this.charUse.cy - 50;
					SoundMn.playSound(this.x, this.y, SoundMn.MAFUBA_2, SoundMn.volume);
					return;
				}
				this.x = this.charUse.cx;
				this.y = this.charUse.cy;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0002ECBC File Offset: 0x0002CEBC
		private void upd_Gong()
		{
			if (this.charUse != null)
			{
				if (this.typeEffect == 21)
				{
					this.x = this.charUse.cx - 3 * this.charUse.cdir;
					this.y = this.charUse.cy;
				}
				else if (this.typeEffect == 22)
				{
					this.x = this.charUse.cx + 20 * this.charUse.cdir;
					this.y = this.charUse.cy - 4;
				}
				else if (this.typeEffect == 23)
				{
					this.x = this.charUse.cx;
					this.y = this.charUse.cy - 50;
				}
				else
				{
					this.x = this.charUse.cx;
					this.y = this.charUse.cy;
				}
			}
			if (this.timeRemove > 0)
			{
				if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
				{
					this.removeEff();
					return;
				}
			}
			else if (this.f >= this.fra_skill[0].nFrame * this.n_frame)
			{
				this.removeEff();
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0002EDEC File Offset: 0x0002CFEC
		private void pnt_Gong(mGraphics g, int anchor)
		{
			if (this.fra_skill[0] != null)
			{
				this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir_nguoc, anchor, g);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0002EE40 File Offset: 0x0002D040
		private void set_Pow()
		{
			this.nFrame = null;
			this.n_frame = 3;
			if (this.typeEffect == 18)
			{
				if (this.typeSub == 0)
				{
					this.nFrame = new byte[]
					{
						0,
						0,
						0,
						1,
						1,
						1,
						2,
						2,
						2
					};
					return;
				}
				this.nFrame = new byte[]
				{
					3,
					3,
					3,
					4,
					4,
					4,
					5,
					5,
					5,
					6,
					6,
					6
				};
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		private void upd_Pow()
		{
			if (this.charUse != null)
			{
				this.x = this.charUse.cx;
				this.y = this.charUse.cy + 13;
			}
			if (this.timeRemove > 0)
			{
				if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
				{
					this.removeEff();
					return;
				}
			}
			else if (this.nFrame != null)
			{
				if (this.f > this.nFrame.Length)
				{
					this.removeEff();
					return;
				}
			}
			else if (this.f >= this.fra_skill[0].nFrame * this.n_frame)
			{
				this.removeEff();
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0002EF40 File Offset: 0x0002D140
		private void pnt_Pow(mGraphics g, int anchor)
		{
			if (this.fra_skill[0] != null)
			{
				if (this.nFrame != null)
				{
					this.fra_skill[0].drawFrame((int)this.nFrame[this.f % this.nFrame.Length], this.x, this.y, this.dir_nguoc, anchor, g);
					return;
				}
				this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir_nguoc, anchor, g);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0002EFD3 File Offset: 0x0002D1D3
		private void set_Sub()
		{
			if (this.typeEffect == 17)
			{
				this.x += ((this.dir != 0) ? (-this.fra_skill[0].frameWidth) : 0);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0002F008 File Offset: 0x0002D208
		private void upd_Sub()
		{
			if (this.timeRemove > 0)
			{
				if (GameCanvas.timeNow - this.time >= (long)this.timeRemove)
				{
					this.removeEff();
					return;
				}
			}
			else if (this.f >= this.fra_skill[0].nFrame * this.n_frame)
			{
				this.removeEff();
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0002F05C File Offset: 0x0002D25C
		private void pnt_Sub(mGraphics g, int anchor)
		{
			this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir, anchor, g);
		}

		// Token: 0x0400040E RID: 1038
		private readonly MyVector VecEffEnd = new MyVector("EffectEnd VecEffEnd");

		// Token: 0x0400040F RID: 1039
		public FrameImage fraImgEff;

		// Token: 0x04000410 RID: 1040
		public byte[] nFrame = new byte[10];

		// Token: 0x04000411 RID: 1041
		public byte[] nFrame_2 = new byte[10];

		// Token: 0x04000412 RID: 1042
		public int typePaint;

		// Token: 0x04000413 RID: 1043
		public sbyte skillLevel;

		// Token: 0x04000414 RID: 1044
		public int typeEffect;

		// Token: 0x04000415 RID: 1045
		public int typeSub;

		// Token: 0x04000416 RID: 1046
		public int range;

		// Token: 0x04000417 RID: 1047
		public int fRemove;

		// Token: 0x04000418 RID: 1048
		public int fMove;

		// Token: 0x04000419 RID: 1049
		public int n_frame;

		// Token: 0x0400041A RID: 1050
		public int x;

		// Token: 0x0400041B RID: 1051
		public int y;

		// Token: 0x0400041C RID: 1052
		public int w;

		// Token: 0x0400041D RID: 1053
		public int h;

		// Token: 0x0400041E RID: 1054
		public int dir;

		// Token: 0x0400041F RID: 1055
		public int dir_nguoc;

		// Token: 0x04000420 RID: 1056
		public int levelPaint;

		// Token: 0x04000421 RID: 1057
		public int f;

		// Token: 0x04000422 RID: 1058
		public int frame;

		// Token: 0x04000423 RID: 1059
		public int fSpeed;

		// Token: 0x04000424 RID: 1060
		public int vy;

		// Token: 0x04000425 RID: 1061
		public int x1000;

		// Token: 0x04000426 RID: 1062
		public int y1000;

		// Token: 0x04000427 RID: 1063
		public int vy1000;

		// Token: 0x04000428 RID: 1064
		public int dy_throw;

		// Token: 0x04000429 RID: 1065
		public int vMax;

		// Token: 0x0400042A RID: 1066
		public int toY;

		// Token: 0x0400042B RID: 1067
		public int stt;

		// Token: 0x0400042C RID: 1068
		public int dy;

		// Token: 0x0400042D RID: 1069
		public short timeRemove;

		// Token: 0x0400042E RID: 1070
		public long time;

		// Token: 0x0400042F RID: 1071
		public bool isRemove;

		// Token: 0x04000430 RID: 1072
		public bool isAddSub;

		// Token: 0x04000431 RID: 1073
		public Char charUse;

		// Token: 0x04000432 RID: 1074
		public Point[] listObj;

		// Token: 0x04000433 RID: 1075
		public Point target;

		// Token: 0x04000434 RID: 1076
		public static short[][] arrInfoEff = new short[][]
		{
			new short[]
			{
				68,
				264,
				4
			},
			new short[]
			{
				30,
				120,
				4
			},
			new short[]
			{
				66,
				280,
				4
			},
			new short[]
			{
				0,
				0,
				1
			},
			new short[]
			{
				111,
				68,
				2
			},
			new short[]
			{
				90,
				68,
				2
			},
			new short[]
			{
				125,
				68,
				2
			},
			new short[]
			{
				47,
				282,
				6
			},
			new short[]
			{
				10,
				40,
				4
			},
			new short[]
			{
				92,
				525,
				7
			},
			new short[]
			{
				62,
				372,
				6
			},
			new short[]
			{
				80,
				352,
				4
			},
			new short[]
			{
				80,
				352,
				4
			},
			new short[]
			{
				80,
				352,
				4
			},
			new short[]
			{
				72,
				240,
				3
			},
			new short[]
			{
				20,
				42,
				3
			},
			new short[]
			{
				65,
				160,
				4
			},
			new short[]
			{
				50,
				300,
				6
			},
			new short[]
			{
				84,
				168,
				2
			},
			new short[]
			{
				90,
				540,
				6
			},
			new short[]
			{
				180,
				900,
				6
			},
			new short[]
			{
				62,
				186,
				3
			},
			new short[]
			{
				34,
				80,
				4
			},
			new short[]
			{
				140,
				560,
				4
			},
			new short[]
			{
				64,
				600,
				6
			},
			new short[]
			{
				36,
				200,
				5
			},
			new short[]
			{
				35,
				200,
				5
			},
			new short[]
			{
				50,
				250,
				5
			},
			new short[]
			{
				50,
				240,
				6
			},
			new short[]
			{
				68,
				264,
				4
			},
			new short[]
			{
				30,
				120,
				4
			},
			new short[]
			{
				92,
				525,
				7
			},
			new short[]
			{
				62,
				372,
				6
			},
			new short[]
			{
				80,
				352,
				4
			},
			new short[]
			{
				80,
				352,
				4
			},
			new short[]
			{
				80,
				352,
				4
			},
			new short[]
			{
				72,
				240,
				3
			},
			new short[]
			{
				20,
				42,
				3
			},
			new short[]
			{
				65,
				160,
				4
			},
			new short[]
			{
				50,
				300,
				6
			},
			new short[]
			{
				50,
				300,
				6
			},
			new short[]
			{
				84,
				168,
				2
			},
			new short[]
			{
				84,
				168,
				2
			},
			new short[]
			{
				90,
				540,
				6
			},
			new short[]
			{
				180,
				900,
				6
			},
			new short[]
			{
				62,
				186,
				3
			},
			new short[]
			{
				34,
				80,
				4
			},
			new short[]
			{
				140,
				560,
				4
			},
			new short[]
			{
				140,
				560,
				4
			},
			new short[]
			{
				64,
				600,
				6
			},
			new short[]
			{
				35,
				200,
				5
			},
			new short[]
			{
				50,
				250,
				5
			},
			new short[]
			{
				50,
				240,
				6
			}
		};

		// Token: 0x04000435 RID: 1077
		public byte[] mpaintone_Arrow = new byte[]
		{
			12,
			11,
			10,
			9,
			8,
			7,
			6,
			5,
			4,
			3,
			2,
			1,
			0,
			23,
			22,
			21,
			20,
			19,
			18,
			17,
			16,
			15,
			14,
			13
		};

		// Token: 0x04000436 RID: 1078
		public byte[] mImageArrow = new byte[]
		{
			0,
			0,
			2,
			1,
			1,
			2,
			0,
			0,
			2,
			1,
			1,
			2,
			0,
			0,
			2,
			1,
			1,
			2,
			0,
			0,
			2,
			1,
			1,
			2
		};

		// Token: 0x04000437 RID: 1079
		public byte[] mXoayArrow = new byte[]
		{
			2,
			2,
			3,
			3,
			3,
			4,
			5,
			5,
			5,
			5,
			5,
			1,
			0,
			0,
			0,
			0,
			0,
			7,
			6,
			6,
			6,
			6,
			6,
			2
		};

		// Token: 0x04000438 RID: 1080
		private int rS;

		// Token: 0x04000439 RID: 1081
		private int angleS;

		// Token: 0x0400043A RID: 1082
		private int angleO;

		// Token: 0x0400043B RID: 1083
		private int iAngleS;

		// Token: 0x0400043C RID: 1084
		private int iDotS;

		// Token: 0x0400043D RID: 1085
		private int[] xArgS;

		// Token: 0x0400043E RID: 1086
		private int[] yArgS;

		// Token: 0x0400043F RID: 1087
		private int[] xDotS;

		// Token: 0x04000440 RID: 1088
		private int[] yDotS;

		// Token: 0x04000441 RID: 1089
		public static int[][] colorStar = new int[][]
		{
			new int[]
			{
				16310304,
				16298056,
				16777215
			},
			new int[]
			{
				7045120,
				12643960,
				16777215
			},
			new int[]
			{
				2407423,
				11987199,
				16777215
			}
		};

		// Token: 0x04000442 RID: 1090
		private int[] colorpaint;

		// Token: 0x04000443 RID: 1091
		private int indexColorStar;

		// Token: 0x04000444 RID: 1092
		private int xline;

		// Token: 0x04000445 RID: 1093
		private int yline;

		// Token: 0x04000446 RID: 1094
		private FrameImage[] fra_skill;
	}
}
