using System;

namespace Game4
{
	// Token: 0x02000242 RID: 578
	public class Point
	{
		// Token: 0x06001A0F RID: 6671 RVA: 0x001A50E2 File Offset: 0x001A32E2
		public void update()
		{
			this.f++;
			this.x += this.vx;
			this.y += this.vy;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x001A5118 File Offset: 0x001A3318
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

		// Token: 0x06001A11 RID: 6673 RVA: 0x001A5150 File Offset: 0x001A3350
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

		// Token: 0x06001A12 RID: 6674 RVA: 0x001A51B8 File Offset: 0x001A33B8
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

		// Token: 0x06001A13 RID: 6675 RVA: 0x001A5248 File Offset: 0x001A3448
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

		// Token: 0x06001A14 RID: 6676 RVA: 0x001A5334 File Offset: 0x001A3534
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

		// Token: 0x040033BB RID: 13243
		public sbyte type;

		// Token: 0x040033BC RID: 13244
		public int x;

		// Token: 0x040033BD RID: 13245
		public int y;

		// Token: 0x040033BE RID: 13246
		public int g;

		// Token: 0x040033BF RID: 13247
		public int v;

		// Token: 0x040033C0 RID: 13248
		public int vMax;

		// Token: 0x040033C1 RID: 13249
		public int w;

		// Token: 0x040033C2 RID: 13250
		public int h;

		// Token: 0x040033C3 RID: 13251
		public int color;

		// Token: 0x040033C4 RID: 13252
		public int limitY;

		// Token: 0x040033C5 RID: 13253
		public int vx;

		// Token: 0x040033C6 RID: 13254
		public int vy;

		// Token: 0x040033C7 RID: 13255
		public int x2;

		// Token: 0x040033C8 RID: 13256
		public int y2;

		// Token: 0x040033C9 RID: 13257
		public int toX;

		// Token: 0x040033CA RID: 13258
		public int toY;

		// Token: 0x040033CB RID: 13259
		public int dis;

		// Token: 0x040033CC RID: 13260
		public int f;

		// Token: 0x040033CD RID: 13261
		public int ftam;

		// Token: 0x040033CE RID: 13262
		public int fRe;

		// Token: 0x040033CF RID: 13263
		public int frame;

		// Token: 0x040033D0 RID: 13264
		public int maxframe;

		// Token: 0x040033D1 RID: 13265
		public int fSmall;

		// Token: 0x040033D2 RID: 13266
		public int goc;

		// Token: 0x040033D3 RID: 13267
		public int gocT_Arc;

		// Token: 0x040033D4 RID: 13268
		public int idir;

		// Token: 0x040033D5 RID: 13269
		public int dirThrow;

		// Token: 0x040033D6 RID: 13270
		public int dir;

		// Token: 0x040033D7 RID: 13271
		public int dir_nguoc;

		// Token: 0x040033D8 RID: 13272
		public int idSkill;

		// Token: 0x040033D9 RID: 13273
		public int id;

		// Token: 0x040033DA RID: 13274
		public int levelPaint;

		// Token: 0x040033DB RID: 13275
		public int num_per_frame = 1;

		// Token: 0x040033DC RID: 13276
		public int life;

		// Token: 0x040033DD RID: 13277
		public int goc_Arc;

		// Token: 0x040033DE RID: 13278
		public int vx1000;

		// Token: 0x040033DF RID: 13279
		public int vy1000;

		// Token: 0x040033E0 RID: 13280
		public int va;

		// Token: 0x040033E1 RID: 13281
		public int x1000;

		// Token: 0x040033E2 RID: 13282
		public int y1000;

		// Token: 0x040033E3 RID: 13283
		public int vX1000;

		// Token: 0x040033E4 RID: 13284
		public int vY1000;

		// Token: 0x040033E5 RID: 13285
		public long time;

		// Token: 0x040033E6 RID: 13286
		public long timecount;

		// Token: 0x040033E7 RID: 13287
		public MyVector vecEffPoint;

		// Token: 0x040033E8 RID: 13288
		public string name;

		// Token: 0x040033E9 RID: 13289
		public string info;

		// Token: 0x040033EA RID: 13290
		public bool isRemove;

		// Token: 0x040033EB RID: 13291
		public bool isSmall;

		// Token: 0x040033EC RID: 13292
		public bool isPaint;

		// Token: 0x040033ED RID: 13293
		public bool isChange;

		// Token: 0x040033EE RID: 13294
		public static FrameImage[] FraEffInMap;

		// Token: 0x040033EF RID: 13295
		public FrameImage fraImgEff;

		// Token: 0x040033F0 RID: 13296
		public FrameImage fraImgEff_2;

		// Token: 0x040033F1 RID: 13297
		public short index;

		// Token: 0x040033F2 RID: 13298
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

		// Token: 0x040033F3 RID: 13299
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

		// Token: 0x040033F4 RID: 13300
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
