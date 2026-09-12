using System;

namespace Game6
{
	// Token: 0x02000092 RID: 146
	public class Point
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x0007AEE6 File Offset: 0x000790E6
		public void update()
		{
			this.f++;
			this.x += this.vx;
			this.y += this.vy;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0007AF1C File Offset: 0x0007911C
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

		// Token: 0x060006C9 RID: 1737 RVA: 0x0007AF54 File Offset: 0x00079154
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

		// Token: 0x060006CA RID: 1738 RVA: 0x0007AFBC File Offset: 0x000791BC
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

		// Token: 0x060006CB RID: 1739 RVA: 0x0007B04C File Offset: 0x0007924C
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

		// Token: 0x060006CC RID: 1740 RVA: 0x0007B138 File Offset: 0x00079338
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

		// Token: 0x04000EBD RID: 3773
		public sbyte type;

		// Token: 0x04000EBE RID: 3774
		public int x;

		// Token: 0x04000EBF RID: 3775
		public int y;

		// Token: 0x04000EC0 RID: 3776
		public int g;

		// Token: 0x04000EC1 RID: 3777
		public int v;

		// Token: 0x04000EC2 RID: 3778
		public int vMax;

		// Token: 0x04000EC3 RID: 3779
		public int w;

		// Token: 0x04000EC4 RID: 3780
		public int h;

		// Token: 0x04000EC5 RID: 3781
		public int color;

		// Token: 0x04000EC6 RID: 3782
		public int limitY;

		// Token: 0x04000EC7 RID: 3783
		public int vx;

		// Token: 0x04000EC8 RID: 3784
		public int vy;

		// Token: 0x04000EC9 RID: 3785
		public int x2;

		// Token: 0x04000ECA RID: 3786
		public int y2;

		// Token: 0x04000ECB RID: 3787
		public int toX;

		// Token: 0x04000ECC RID: 3788
		public int toY;

		// Token: 0x04000ECD RID: 3789
		public int dis;

		// Token: 0x04000ECE RID: 3790
		public int f;

		// Token: 0x04000ECF RID: 3791
		public int ftam;

		// Token: 0x04000ED0 RID: 3792
		public int fRe;

		// Token: 0x04000ED1 RID: 3793
		public int frame;

		// Token: 0x04000ED2 RID: 3794
		public int maxframe;

		// Token: 0x04000ED3 RID: 3795
		public int fSmall;

		// Token: 0x04000ED4 RID: 3796
		public int goc;

		// Token: 0x04000ED5 RID: 3797
		public int gocT_Arc;

		// Token: 0x04000ED6 RID: 3798
		public int idir;

		// Token: 0x04000ED7 RID: 3799
		public int dirThrow;

		// Token: 0x04000ED8 RID: 3800
		public int dir;

		// Token: 0x04000ED9 RID: 3801
		public int dir_nguoc;

		// Token: 0x04000EDA RID: 3802
		public int idSkill;

		// Token: 0x04000EDB RID: 3803
		public int id;

		// Token: 0x04000EDC RID: 3804
		public int levelPaint;

		// Token: 0x04000EDD RID: 3805
		public int num_per_frame = 1;

		// Token: 0x04000EDE RID: 3806
		public int life;

		// Token: 0x04000EDF RID: 3807
		public int goc_Arc;

		// Token: 0x04000EE0 RID: 3808
		public int vx1000;

		// Token: 0x04000EE1 RID: 3809
		public int vy1000;

		// Token: 0x04000EE2 RID: 3810
		public int va;

		// Token: 0x04000EE3 RID: 3811
		public int x1000;

		// Token: 0x04000EE4 RID: 3812
		public int y1000;

		// Token: 0x04000EE5 RID: 3813
		public int vX1000;

		// Token: 0x04000EE6 RID: 3814
		public int vY1000;

		// Token: 0x04000EE7 RID: 3815
		public long time;

		// Token: 0x04000EE8 RID: 3816
		public long timecount;

		// Token: 0x04000EE9 RID: 3817
		public MyVector vecEffPoint;

		// Token: 0x04000EEA RID: 3818
		public string name;

		// Token: 0x04000EEB RID: 3819
		public string info;

		// Token: 0x04000EEC RID: 3820
		public bool isRemove;

		// Token: 0x04000EED RID: 3821
		public bool isSmall;

		// Token: 0x04000EEE RID: 3822
		public bool isPaint;

		// Token: 0x04000EEF RID: 3823
		public bool isChange;

		// Token: 0x04000EF0 RID: 3824
		public static FrameImage[] FraEffInMap;

		// Token: 0x04000EF1 RID: 3825
		public FrameImage fraImgEff;

		// Token: 0x04000EF2 RID: 3826
		public FrameImage fraImgEff_2;

		// Token: 0x04000EF3 RID: 3827
		public short index;

		// Token: 0x04000EF4 RID: 3828
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

		// Token: 0x04000EF5 RID: 3829
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

		// Token: 0x04000EF6 RID: 3830
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
