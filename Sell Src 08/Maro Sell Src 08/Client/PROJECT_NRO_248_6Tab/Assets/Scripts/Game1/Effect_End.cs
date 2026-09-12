using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x0200046F RID: 1135
	public class Effect_End
	{
		// Token: 0x0600324C RID: 12876 RVA: 0x00315838 File Offset: 0x00313A38
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

		// Token: 0x0600324D RID: 12877 RVA: 0x00315A38 File Offset: 0x00313C38
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

		// Token: 0x0600324E RID: 12878 RVA: 0x00315B78 File Offset: 0x00313D78
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

		// Token: 0x0600324F RID: 12879 RVA: 0x00315CC0 File Offset: 0x00313EC0
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

		// Token: 0x06003250 RID: 12880 RVA: 0x00315D0C File Offset: 0x00313F0C
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

		// Token: 0x06003251 RID: 12881 RVA: 0x00315D60 File Offset: 0x00313F60
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

		// Token: 0x06003252 RID: 12882 RVA: 0x00315E84 File Offset: 0x00314084
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

		// Token: 0x06003253 RID: 12883 RVA: 0x00315F98 File Offset: 0x00314198
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

		// Token: 0x06003254 RID: 12884 RVA: 0x00316118 File Offset: 0x00314318
		public void removeEff()
		{
			this.isRemove = true;
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x00316124 File Offset: 0x00314324
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

		// Token: 0x06003256 RID: 12886 RVA: 0x0031619C File Offset: 0x0031439C
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

		// Token: 0x06003257 RID: 12887 RVA: 0x00316263 File Offset: 0x00314463
		private void pnt_End_String(mGraphics g)
		{
			if (this.fraImgEff != null)
			{
				this.fraImgEff.drawFrame(this.f / 5 % this.fraImgEff.nFrame, this.x, this.y, 0, 33, g);
			}
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x0031629C File Offset: 0x0031449C
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

		// Token: 0x06003259 RID: 12889 RVA: 0x0031636C File Offset: 0x0031456C
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

		// Token: 0x0600325A RID: 12890 RVA: 0x0031646C File Offset: 0x0031466C
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

		// Token: 0x0600325B RID: 12891 RVA: 0x003164F0 File Offset: 0x003146F0
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

		// Token: 0x0600325C RID: 12892 RVA: 0x0031657C File Offset: 0x0031477C
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

		// Token: 0x0600325D RID: 12893 RVA: 0x00316678 File Offset: 0x00314878
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

		// Token: 0x0600325E RID: 12894 RVA: 0x00316760 File Offset: 0x00314960
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

		// Token: 0x0600325F RID: 12895 RVA: 0x003168EC File Offset: 0x00314AEC
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

		// Token: 0x06003260 RID: 12896 RVA: 0x00316B00 File Offset: 0x00314D00
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

		// Token: 0x06003261 RID: 12897 RVA: 0x00316BB8 File Offset: 0x00314DB8
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

		// Token: 0x06003262 RID: 12898 RVA: 0x00316D54 File Offset: 0x00314F54
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

		// Token: 0x06003263 RID: 12899 RVA: 0x00316DE0 File Offset: 0x00314FE0
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

		// Token: 0x06003264 RID: 12900 RVA: 0x00316F9C File Offset: 0x0031519C
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

		// Token: 0x06003265 RID: 12901 RVA: 0x003172E0 File Offset: 0x003154E0
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

		// Token: 0x06003266 RID: 12902 RVA: 0x003173B8 File Offset: 0x003155B8
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

		// Token: 0x06003267 RID: 12903 RVA: 0x0031759C File Offset: 0x0031579C
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

		// Token: 0x06003268 RID: 12904 RVA: 0x00317618 File Offset: 0x00315818
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

		// Token: 0x06003269 RID: 12905 RVA: 0x003176D4 File Offset: 0x003158D4
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

		// Token: 0x0600326A RID: 12906 RVA: 0x0031790C File Offset: 0x00315B0C
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

		// Token: 0x0600326B RID: 12907 RVA: 0x003179E8 File Offset: 0x00315BE8
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

		// Token: 0x0600326C RID: 12908 RVA: 0x00317AEC File Offset: 0x00315CEC
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

		// Token: 0x0600326D RID: 12909 RVA: 0x00317B58 File Offset: 0x00315D58
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

		// Token: 0x0600326E RID: 12910 RVA: 0x00317BC0 File Offset: 0x00315DC0
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

		// Token: 0x0600326F RID: 12911 RVA: 0x00317CB4 File Offset: 0x00315EB4
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

		// Token: 0x06003270 RID: 12912 RVA: 0x00317D58 File Offset: 0x00315F58
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

		// Token: 0x06003271 RID: 12913 RVA: 0x00318024 File Offset: 0x00316224
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

		// Token: 0x06003272 RID: 12914 RVA: 0x00318158 File Offset: 0x00316358
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

		// Token: 0x06003273 RID: 12915 RVA: 0x00318288 File Offset: 0x00316488
		private void pnt_Gong(mGraphics g, int anchor)
		{
			if (this.fra_skill[0] != null)
			{
				this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir_nguoc, anchor, g);
			}
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x003182DC File Offset: 0x003164DC
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

		// Token: 0x06003275 RID: 12917 RVA: 0x0031833C File Offset: 0x0031653C
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

		// Token: 0x06003276 RID: 12918 RVA: 0x003183DC File Offset: 0x003165DC
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

		// Token: 0x06003277 RID: 12919 RVA: 0x0031846F File Offset: 0x0031666F
		private void set_Sub()
		{
			if (this.typeEffect == 17)
			{
				this.x += ((this.dir != 0) ? (-this.fra_skill[0].frameWidth) : 0);
			}
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x003184A4 File Offset: 0x003166A4
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

		// Token: 0x06003279 RID: 12921 RVA: 0x003184F8 File Offset: 0x003166F8
		private void pnt_Sub(mGraphics g, int anchor)
		{
			this.fra_skill[0].drawFrame(this.f / this.n_frame % this.fra_skill[0].nFrame, this.x, this.y, this.dir, anchor, g);
		}

		// Token: 0x04006089 RID: 24713
		private readonly MyVector VecEffEnd = new MyVector("EffectEnd VecEffEnd");

		// Token: 0x0400608A RID: 24714
		public FrameImage fraImgEff;

		// Token: 0x0400608B RID: 24715
		public byte[] nFrame = new byte[10];

		// Token: 0x0400608C RID: 24716
		public byte[] nFrame_2 = new byte[10];

		// Token: 0x0400608D RID: 24717
		public int typePaint;

		// Token: 0x0400608E RID: 24718
		public sbyte skillLevel;

		// Token: 0x0400608F RID: 24719
		public int typeEffect;

		// Token: 0x04006090 RID: 24720
		public int typeSub;

		// Token: 0x04006091 RID: 24721
		public int range;

		// Token: 0x04006092 RID: 24722
		public int fRemove;

		// Token: 0x04006093 RID: 24723
		public int fMove;

		// Token: 0x04006094 RID: 24724
		public int n_frame;

		// Token: 0x04006095 RID: 24725
		public int x;

		// Token: 0x04006096 RID: 24726
		public int y;

		// Token: 0x04006097 RID: 24727
		public int w;

		// Token: 0x04006098 RID: 24728
		public int h;

		// Token: 0x04006099 RID: 24729
		public int dir;

		// Token: 0x0400609A RID: 24730
		public int dir_nguoc;

		// Token: 0x0400609B RID: 24731
		public int levelPaint;

		// Token: 0x0400609C RID: 24732
		public int f;

		// Token: 0x0400609D RID: 24733
		public int frame;

		// Token: 0x0400609E RID: 24734
		public int fSpeed;

		// Token: 0x0400609F RID: 24735
		public int vy;

		// Token: 0x040060A0 RID: 24736
		public int x1000;

		// Token: 0x040060A1 RID: 24737
		public int y1000;

		// Token: 0x040060A2 RID: 24738
		public int vy1000;

		// Token: 0x040060A3 RID: 24739
		public int dy_throw;

		// Token: 0x040060A4 RID: 24740
		public int vMax;

		// Token: 0x040060A5 RID: 24741
		public int toY;

		// Token: 0x040060A6 RID: 24742
		public int stt;

		// Token: 0x040060A7 RID: 24743
		public int dy;

		// Token: 0x040060A8 RID: 24744
		public short timeRemove;

		// Token: 0x040060A9 RID: 24745
		public long time;

		// Token: 0x040060AA RID: 24746
		public bool isRemove;

		// Token: 0x040060AB RID: 24747
		public bool isAddSub;

		// Token: 0x040060AC RID: 24748
		public Char charUse;

		// Token: 0x040060AD RID: 24749
		public Point[] listObj;

		// Token: 0x040060AE RID: 24750
		public Point target;

		// Token: 0x040060AF RID: 24751
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

		// Token: 0x040060B0 RID: 24752
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

		// Token: 0x040060B1 RID: 24753
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

		// Token: 0x040060B2 RID: 24754
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

		// Token: 0x040060B3 RID: 24755
		private int rS;

		// Token: 0x040060B4 RID: 24756
		private int angleS;

		// Token: 0x040060B5 RID: 24757
		private int angleO;

		// Token: 0x040060B6 RID: 24758
		private int iAngleS;

		// Token: 0x040060B7 RID: 24759
		private int iDotS;

		// Token: 0x040060B8 RID: 24760
		private int[] xArgS;

		// Token: 0x040060B9 RID: 24761
		private int[] yArgS;

		// Token: 0x040060BA RID: 24762
		private int[] xDotS;

		// Token: 0x040060BB RID: 24763
		private int[] yDotS;

		// Token: 0x040060BC RID: 24764
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

		// Token: 0x040060BD RID: 24765
		private int[] colorpaint;

		// Token: 0x040060BE RID: 24766
		private int indexColorStar;

		// Token: 0x040060BF RID: 24767
		private int xline;

		// Token: 0x040060C0 RID: 24768
		private int yline;

		// Token: 0x040060C1 RID: 24769
		private FrameImage[] fra_skill;
	}
}
