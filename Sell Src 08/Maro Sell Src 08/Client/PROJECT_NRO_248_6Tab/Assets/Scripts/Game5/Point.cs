using System;

namespace Game5
{
	// Token: 0x0200016A RID: 362
	public class Point
	{
		// Token: 0x0600106B RID: 4203 RVA: 0x0011003E File Offset: 0x0010E23E
		public void update()
		{
			this.f++;
			this.x += this.vx;
			this.y += this.vy;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00110074 File Offset: 0x0010E274
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

		// Token: 0x0600106D RID: 4205 RVA: 0x001100AC File Offset: 0x0010E2AC
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

		// Token: 0x0600106E RID: 4206 RVA: 0x00110114 File Offset: 0x0010E314
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

		// Token: 0x0600106F RID: 4207 RVA: 0x001101A4 File Offset: 0x0010E3A4
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

		// Token: 0x06001070 RID: 4208 RVA: 0x00110290 File Offset: 0x0010E490
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

		// Token: 0x0400213C RID: 8508
		public sbyte type;

		// Token: 0x0400213D RID: 8509
		public int x;

		// Token: 0x0400213E RID: 8510
		public int y;

		// Token: 0x0400213F RID: 8511
		public int g;

		// Token: 0x04002140 RID: 8512
		public int v;

		// Token: 0x04002141 RID: 8513
		public int vMax;

		// Token: 0x04002142 RID: 8514
		public int w;

		// Token: 0x04002143 RID: 8515
		public int h;

		// Token: 0x04002144 RID: 8516
		public int color;

		// Token: 0x04002145 RID: 8517
		public int limitY;

		// Token: 0x04002146 RID: 8518
		public int vx;

		// Token: 0x04002147 RID: 8519
		public int vy;

		// Token: 0x04002148 RID: 8520
		public int x2;

		// Token: 0x04002149 RID: 8521
		public int y2;

		// Token: 0x0400214A RID: 8522
		public int toX;

		// Token: 0x0400214B RID: 8523
		public int toY;

		// Token: 0x0400214C RID: 8524
		public int dis;

		// Token: 0x0400214D RID: 8525
		public int f;

		// Token: 0x0400214E RID: 8526
		public int ftam;

		// Token: 0x0400214F RID: 8527
		public int fRe;

		// Token: 0x04002150 RID: 8528
		public int frame;

		// Token: 0x04002151 RID: 8529
		public int maxframe;

		// Token: 0x04002152 RID: 8530
		public int fSmall;

		// Token: 0x04002153 RID: 8531
		public int goc;

		// Token: 0x04002154 RID: 8532
		public int gocT_Arc;

		// Token: 0x04002155 RID: 8533
		public int idir;

		// Token: 0x04002156 RID: 8534
		public int dirThrow;

		// Token: 0x04002157 RID: 8535
		public int dir;

		// Token: 0x04002158 RID: 8536
		public int dir_nguoc;

		// Token: 0x04002159 RID: 8537
		public int idSkill;

		// Token: 0x0400215A RID: 8538
		public int id;

		// Token: 0x0400215B RID: 8539
		public int levelPaint;

		// Token: 0x0400215C RID: 8540
		public int num_per_frame = 1;

		// Token: 0x0400215D RID: 8541
		public int life;

		// Token: 0x0400215E RID: 8542
		public int goc_Arc;

		// Token: 0x0400215F RID: 8543
		public int vx1000;

		// Token: 0x04002160 RID: 8544
		public int vy1000;

		// Token: 0x04002161 RID: 8545
		public int va;

		// Token: 0x04002162 RID: 8546
		public int x1000;

		// Token: 0x04002163 RID: 8547
		public int y1000;

		// Token: 0x04002164 RID: 8548
		public int vX1000;

		// Token: 0x04002165 RID: 8549
		public int vY1000;

		// Token: 0x04002166 RID: 8550
		public long time;

		// Token: 0x04002167 RID: 8551
		public long timecount;

		// Token: 0x04002168 RID: 8552
		public MyVector vecEffPoint;

		// Token: 0x04002169 RID: 8553
		public string name;

		// Token: 0x0400216A RID: 8554
		public string info;

		// Token: 0x0400216B RID: 8555
		public bool isRemove;

		// Token: 0x0400216C RID: 8556
		public bool isSmall;

		// Token: 0x0400216D RID: 8557
		public bool isPaint;

		// Token: 0x0400216E RID: 8558
		public bool isChange;

		// Token: 0x0400216F RID: 8559
		public static FrameImage[] FraEffInMap;

		// Token: 0x04002170 RID: 8560
		public FrameImage fraImgEff;

		// Token: 0x04002171 RID: 8561
		public FrameImage fraImgEff_2;

		// Token: 0x04002172 RID: 8562
		public short index;

		// Token: 0x04002173 RID: 8563
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

		// Token: 0x04002174 RID: 8564
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

		// Token: 0x04002175 RID: 8565
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
