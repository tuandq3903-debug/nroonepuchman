using System;

namespace Game2
{
	// Token: 0x020003F2 RID: 1010
	public class Point
	{
		// Token: 0x06002D57 RID: 11607 RVA: 0x002CF22A File Offset: 0x002CD42A
		public void update()
		{
			this.f++;
			this.x += this.vx;
			this.y += this.vy;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x002CF260 File Offset: 0x002CD460
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

		// Token: 0x06002D59 RID: 11609 RVA: 0x002CF298 File Offset: 0x002CD498
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

		// Token: 0x06002D5A RID: 11610 RVA: 0x002CF300 File Offset: 0x002CD500
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

		// Token: 0x06002D5B RID: 11611 RVA: 0x002CF390 File Offset: 0x002CD590
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

		// Token: 0x06002D5C RID: 11612 RVA: 0x002CF47C File Offset: 0x002CD67C
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

		// Token: 0x040058B9 RID: 22713
		public sbyte type;

		// Token: 0x040058BA RID: 22714
		public int x;

		// Token: 0x040058BB RID: 22715
		public int y;

		// Token: 0x040058BC RID: 22716
		public int g;

		// Token: 0x040058BD RID: 22717
		public int v;

		// Token: 0x040058BE RID: 22718
		public int vMax;

		// Token: 0x040058BF RID: 22719
		public int w;

		// Token: 0x040058C0 RID: 22720
		public int h;

		// Token: 0x040058C1 RID: 22721
		public int color;

		// Token: 0x040058C2 RID: 22722
		public int limitY;

		// Token: 0x040058C3 RID: 22723
		public int vx;

		// Token: 0x040058C4 RID: 22724
		public int vy;

		// Token: 0x040058C5 RID: 22725
		public int x2;

		// Token: 0x040058C6 RID: 22726
		public int y2;

		// Token: 0x040058C7 RID: 22727
		public int toX;

		// Token: 0x040058C8 RID: 22728
		public int toY;

		// Token: 0x040058C9 RID: 22729
		public int dis;

		// Token: 0x040058CA RID: 22730
		public int f;

		// Token: 0x040058CB RID: 22731
		public int ftam;

		// Token: 0x040058CC RID: 22732
		public int fRe;

		// Token: 0x040058CD RID: 22733
		public int frame;

		// Token: 0x040058CE RID: 22734
		public int maxframe;

		// Token: 0x040058CF RID: 22735
		public int fSmall;

		// Token: 0x040058D0 RID: 22736
		public int goc;

		// Token: 0x040058D1 RID: 22737
		public int gocT_Arc;

		// Token: 0x040058D2 RID: 22738
		public int idir;

		// Token: 0x040058D3 RID: 22739
		public int dirThrow;

		// Token: 0x040058D4 RID: 22740
		public int dir;

		// Token: 0x040058D5 RID: 22741
		public int dir_nguoc;

		// Token: 0x040058D6 RID: 22742
		public int idSkill;

		// Token: 0x040058D7 RID: 22743
		public int id;

		// Token: 0x040058D8 RID: 22744
		public int levelPaint;

		// Token: 0x040058D9 RID: 22745
		public int num_per_frame = 1;

		// Token: 0x040058DA RID: 22746
		public int life;

		// Token: 0x040058DB RID: 22747
		public int goc_Arc;

		// Token: 0x040058DC RID: 22748
		public int vx1000;

		// Token: 0x040058DD RID: 22749
		public int vy1000;

		// Token: 0x040058DE RID: 22750
		public int va;

		// Token: 0x040058DF RID: 22751
		public int x1000;

		// Token: 0x040058E0 RID: 22752
		public int y1000;

		// Token: 0x040058E1 RID: 22753
		public int vX1000;

		// Token: 0x040058E2 RID: 22754
		public int vY1000;

		// Token: 0x040058E3 RID: 22755
		public long time;

		// Token: 0x040058E4 RID: 22756
		public long timecount;

		// Token: 0x040058E5 RID: 22757
		public MyVector vecEffPoint;

		// Token: 0x040058E6 RID: 22758
		public string name;

		// Token: 0x040058E7 RID: 22759
		public string info;

		// Token: 0x040058E8 RID: 22760
		public bool isRemove;

		// Token: 0x040058E9 RID: 22761
		public bool isSmall;

		// Token: 0x040058EA RID: 22762
		public bool isPaint;

		// Token: 0x040058EB RID: 22763
		public bool isChange;

		// Token: 0x040058EC RID: 22764
		public static FrameImage[] FraEffInMap;

		// Token: 0x040058ED RID: 22765
		public FrameImage fraImgEff;

		// Token: 0x040058EE RID: 22766
		public FrameImage fraImgEff_2;

		// Token: 0x040058EF RID: 22767
		public short index;

		// Token: 0x040058F0 RID: 22768
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

		// Token: 0x040058F1 RID: 22769
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

		// Token: 0x040058F2 RID: 22770
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
