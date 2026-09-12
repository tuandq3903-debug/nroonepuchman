using System;

namespace Game3
{
	// Token: 0x0200031A RID: 794
	public class Point
	{
		// Token: 0x060023B3 RID: 9139 RVA: 0x0023A186 File Offset: 0x00238386
		public void update()
		{
			this.f++;
			this.x += this.vx;
			this.y += this.vy;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0023A1BC File Offset: 0x002383BC
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

		// Token: 0x060023B5 RID: 9141 RVA: 0x0023A1F4 File Offset: 0x002383F4
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

		// Token: 0x060023B6 RID: 9142 RVA: 0x0023A25C File Offset: 0x0023845C
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

		// Token: 0x060023B7 RID: 9143 RVA: 0x0023A2EC File Offset: 0x002384EC
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

		// Token: 0x060023B8 RID: 9144 RVA: 0x0023A3D8 File Offset: 0x002385D8
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

		// Token: 0x0400463A RID: 17978
		public sbyte type;

		// Token: 0x0400463B RID: 17979
		public int x;

		// Token: 0x0400463C RID: 17980
		public int y;

		// Token: 0x0400463D RID: 17981
		public int g;

		// Token: 0x0400463E RID: 17982
		public int v;

		// Token: 0x0400463F RID: 17983
		public int vMax;

		// Token: 0x04004640 RID: 17984
		public int w;

		// Token: 0x04004641 RID: 17985
		public int h;

		// Token: 0x04004642 RID: 17986
		public int color;

		// Token: 0x04004643 RID: 17987
		public int limitY;

		// Token: 0x04004644 RID: 17988
		public int vx;

		// Token: 0x04004645 RID: 17989
		public int vy;

		// Token: 0x04004646 RID: 17990
		public int x2;

		// Token: 0x04004647 RID: 17991
		public int y2;

		// Token: 0x04004648 RID: 17992
		public int toX;

		// Token: 0x04004649 RID: 17993
		public int toY;

		// Token: 0x0400464A RID: 17994
		public int dis;

		// Token: 0x0400464B RID: 17995
		public int f;

		// Token: 0x0400464C RID: 17996
		public int ftam;

		// Token: 0x0400464D RID: 17997
		public int fRe;

		// Token: 0x0400464E RID: 17998
		public int frame;

		// Token: 0x0400464F RID: 17999
		public int maxframe;

		// Token: 0x04004650 RID: 18000
		public int fSmall;

		// Token: 0x04004651 RID: 18001
		public int goc;

		// Token: 0x04004652 RID: 18002
		public int gocT_Arc;

		// Token: 0x04004653 RID: 18003
		public int idir;

		// Token: 0x04004654 RID: 18004
		public int dirThrow;

		// Token: 0x04004655 RID: 18005
		public int dir;

		// Token: 0x04004656 RID: 18006
		public int dir_nguoc;

		// Token: 0x04004657 RID: 18007
		public int idSkill;

		// Token: 0x04004658 RID: 18008
		public int id;

		// Token: 0x04004659 RID: 18009
		public int levelPaint;

		// Token: 0x0400465A RID: 18010
		public int num_per_frame = 1;

		// Token: 0x0400465B RID: 18011
		public int life;

		// Token: 0x0400465C RID: 18012
		public int goc_Arc;

		// Token: 0x0400465D RID: 18013
		public int vx1000;

		// Token: 0x0400465E RID: 18014
		public int vy1000;

		// Token: 0x0400465F RID: 18015
		public int va;

		// Token: 0x04004660 RID: 18016
		public int x1000;

		// Token: 0x04004661 RID: 18017
		public int y1000;

		// Token: 0x04004662 RID: 18018
		public int vX1000;

		// Token: 0x04004663 RID: 18019
		public int vY1000;

		// Token: 0x04004664 RID: 18020
		public long time;

		// Token: 0x04004665 RID: 18021
		public long timecount;

		// Token: 0x04004666 RID: 18022
		public MyVector vecEffPoint;

		// Token: 0x04004667 RID: 18023
		public string name;

		// Token: 0x04004668 RID: 18024
		public string info;

		// Token: 0x04004669 RID: 18025
		public bool isRemove;

		// Token: 0x0400466A RID: 18026
		public bool isSmall;

		// Token: 0x0400466B RID: 18027
		public bool isPaint;

		// Token: 0x0400466C RID: 18028
		public bool isChange;

		// Token: 0x0400466D RID: 18029
		public static FrameImage[] FraEffInMap;

		// Token: 0x0400466E RID: 18030
		public FrameImage fraImgEff;

		// Token: 0x0400466F RID: 18031
		public FrameImage fraImgEff_2;

		// Token: 0x04004670 RID: 18032
		public short index;

		// Token: 0x04004671 RID: 18033
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

		// Token: 0x04004672 RID: 18034
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

		// Token: 0x04004673 RID: 18035
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
