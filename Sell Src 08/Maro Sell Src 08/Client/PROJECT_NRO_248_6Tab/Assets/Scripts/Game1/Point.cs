using System;

namespace Game1
{
	// Token: 0x020004CA RID: 1226
	public class Point
	{
		// Token: 0x060036FB RID: 14075 RVA: 0x003642CE File Offset: 0x003624CE
		public void update()
		{
			this.f++;
			this.x += this.vx;
			this.y += this.vy;
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x00364304 File Offset: 0x00362504
		public int setFrameAngle(int goc)
		{
			if (goc <= 15 || goc > 345)
			{
				return 12;
			}
			int num = (goc - 15) / 15 + 1;
			if (num > 24)
			{
				num = 24;
			}
			return (int)this.mpaintone_Arrow[num];
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x0036433C File Offset: 0x0036253C
		public void create_Arrow(int vMax)
		{
			this.vMax = vMax;
			int num = this.toX - this.x;
			int num2 = this.toY - this.y;
			if (this.x > this.toX)
			{
				this.dir = 2;
				this.dir_nguoc = 0;
			}
			else
			{
				this.dir = 0;
				this.dir_nguoc = 2;
			}
			this.create_Speed(num, num2);
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x003643A4 File Offset: 0x003625A4
		public void create_Speed(int dx, int dy)
		{
			int frameAngle = Res.angle(dx, dy);
			this.frame = this.setFrameAngle(frameAngle);
			int num3 = Res.getDistance(dx, dy) / this.vMax;
			if (num3 == 0)
			{
				num3 = 1;
			}
			int num4 = dx / num3;
			int num5 = dy / num3;
			if (num4 == 0 && dx < num3)
			{
				num4 = ((dx >= 0) ? 1 : -1);
			}
			if (num5 == 0 && dy < num3)
			{
				num5 = ((dy >= 0) ? 1 : -1);
			}
			if (Res.abs(num4) > Res.abs(dx))
			{
				num4 = dx;
			}
			if (Res.abs(num5) > Res.abs(dy))
			{
				num5 = dy;
			}
			this.vx = num4;
			this.vy = num5;
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x00364434 File Offset: 0x00362634
		public void moveTo_xy(int toX, int toY)
		{
			int num = toX - this.x;
			int dy = toY - this.y;
			if (num > 1)
			{
				int frameAngle = Res.angle(num, dy);
				this.frame = this.setFrameAngle(frameAngle);
			}
			if (Res.abs(this.vx) > 0)
			{
				if (Res.abs(this.x - toX) < Res.abs(this.vx))
				{
					this.x = toX;
					this.vx = 0;
				}
				else
				{
					this.x += this.vx;
				}
			}
			else
			{
				this.x = toX;
				this.vx = 0;
			}
			if (Res.abs(this.vy) <= 0)
			{
				this.y = toY;
				this.vy = 0;
				return;
			}
			if (Res.abs(this.y - toY) < Res.abs(this.vy))
			{
				this.y = toY;
				this.vy = 0;
				return;
			}
			this.y += this.vy;
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x00364520 File Offset: 0x00362720
		public void paint_Arrow(mGraphics g, FrameImage frm, int anchor, bool isCountFr)
		{
			if (frm != null)
			{
				int num = frm.nFrame / 3;
				if (num < 1)
				{
					num = 1;
				}
				int num2 = 3;
				int num3 = (frm.nFrame <= 3) ? (this.f % num) : ((this.f / num2 % 2 != 0) ? 3 : 0);
				int idx = num * (int)this.mImageArrow[this.frame] + num3;
				if (frm.nFrame < 3)
				{
					idx = this.f / num2 % frm.nFrame;
				}
				if (isCountFr)
				{
					idx = this.f / num2 % frm.nFrame;
				}
				frm.drawFrame(idx, this.x, this.y, (int)this.mXoayArrow[this.frame], anchor, g);
			}
		}

		// Token: 0x04006B38 RID: 27448
		public sbyte type;

		// Token: 0x04006B39 RID: 27449
		public int x;

		// Token: 0x04006B3A RID: 27450
		public int y;

		// Token: 0x04006B3B RID: 27451
		public int g;

		// Token: 0x04006B3C RID: 27452
		public int v;

		// Token: 0x04006B3D RID: 27453
		public int vMax;

		// Token: 0x04006B3E RID: 27454
		public int w;

		// Token: 0x04006B3F RID: 27455
		public int h;

		// Token: 0x04006B40 RID: 27456
		public int color;

		// Token: 0x04006B41 RID: 27457
		public int limitY;

		// Token: 0x04006B42 RID: 27458
		public int vx;

		// Token: 0x04006B43 RID: 27459
		public int vy;

		// Token: 0x04006B44 RID: 27460
		public int x2;

		// Token: 0x04006B45 RID: 27461
		public int y2;

		// Token: 0x04006B46 RID: 27462
		public int toX;

		// Token: 0x04006B47 RID: 27463
		public int toY;

		// Token: 0x04006B48 RID: 27464
		public int dis;

		// Token: 0x04006B49 RID: 27465
		public int f;

		// Token: 0x04006B4A RID: 27466
		public int ftam;

		// Token: 0x04006B4B RID: 27467
		public int fRe;

		// Token: 0x04006B4C RID: 27468
		public int frame;

		// Token: 0x04006B4D RID: 27469
		public int maxframe;

		// Token: 0x04006B4E RID: 27470
		public int fSmall;

		// Token: 0x04006B4F RID: 27471
		public int goc;

		// Token: 0x04006B50 RID: 27472
		public int gocT_Arc;

		// Token: 0x04006B51 RID: 27473
		public int idir;

		// Token: 0x04006B52 RID: 27474
		public int dirThrow;

		// Token: 0x04006B53 RID: 27475
		public int dir;

		// Token: 0x04006B54 RID: 27476
		public int dir_nguoc;

		// Token: 0x04006B55 RID: 27477
		public int idSkill;

		// Token: 0x04006B56 RID: 27478
		public int id;

		// Token: 0x04006B57 RID: 27479
		public int levelPaint;

		// Token: 0x04006B58 RID: 27480
		public int num_per_frame = 1;

		// Token: 0x04006B59 RID: 27481
		public int life;

		// Token: 0x04006B5A RID: 27482
		public int goc_Arc;

		// Token: 0x04006B5B RID: 27483
		public int vx1000;

		// Token: 0x04006B5C RID: 27484
		public int vy1000;

		// Token: 0x04006B5D RID: 27485
		public int va;

		// Token: 0x04006B5E RID: 27486
		public int x1000;

		// Token: 0x04006B5F RID: 27487
		public int y1000;

		// Token: 0x04006B60 RID: 27488
		public int vX1000;

		// Token: 0x04006B61 RID: 27489
		public int vY1000;

		// Token: 0x04006B62 RID: 27490
		public long time;

		// Token: 0x04006B63 RID: 27491
		public long timecount;

		// Token: 0x04006B64 RID: 27492
		public MyVector vecEffPoint;

		// Token: 0x04006B65 RID: 27493
		public string name;

		// Token: 0x04006B66 RID: 27494
		public string info;

		// Token: 0x04006B67 RID: 27495
		public bool isRemove;

		// Token: 0x04006B68 RID: 27496
		public bool isSmall;

		// Token: 0x04006B69 RID: 27497
		public bool isPaint;

		// Token: 0x04006B6A RID: 27498
		public bool isChange;

		// Token: 0x04006B6B RID: 27499
		public static FrameImage[] FraEffInMap;

		// Token: 0x04006B6C RID: 27500
		public FrameImage fraImgEff;

		// Token: 0x04006B6D RID: 27501
		public FrameImage fraImgEff_2;

		// Token: 0x04006B6E RID: 27502
		public short index;

		// Token: 0x04006B6F RID: 27503
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

		// Token: 0x04006B70 RID: 27504
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

		// Token: 0x04006B71 RID: 27505
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
	}
}
