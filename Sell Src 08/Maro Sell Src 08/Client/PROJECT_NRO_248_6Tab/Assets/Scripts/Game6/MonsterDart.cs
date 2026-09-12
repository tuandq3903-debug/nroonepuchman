using System;

namespace Game6
{
	// Token: 0x02000075 RID: 117
	public class MonsterDart : Effect2
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x000580A8 File Offset: 0x000562A8
		public MonsterDart(int x, int y, bool isBoss, long dame, long dameMp, Char c, int dartType)
		{
			this.info = GameScr.darts[dartType];
			this.x = x;
			this.y = y;
			this.isBoss = isBoss;
			this.dame = dame;
			this.dameMp = dameMp;
			this.c = c;
			this.va = this.info.va;
			this.setAngle(Res.angle(c.cx - x, c.cy - y));
			if (x >= GameScr.cmx && x <= GameScr.cmx + GameCanvas.w)
			{
				SoundMn.gI().mobKame(dartType);
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00058151 File Offset: 0x00056351
		public void setAngle(int angle)
		{
			this.angle = angle;
			this.vx = this.va * Res.cos(angle) >> 10;
			this.vy = this.va * Res.sin(angle) >> 10;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00058186 File Offset: 0x00056386
		public static void addMonsterDart(int x, int y, bool isBoss, long dame, long dameMp, Char c, int dartType)
		{
			Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, c, dartType));
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000581A4 File Offset: 0x000563A4
		public override void update()
		{
			for (int i = 0; i < (int)this.info.nUpdate; i++)
			{
				if (this.info.tail.Length != 0)
				{
					this.darts.addElement(new SmallDart(this.x, this.y));
				}
				this.dx = ((this.c == null) ? this.xTo : this.c.cx) - this.x;
				this.dy = ((this.c == null) ? this.yTo : this.c.cy) - 10 - this.y;
				int num = 60;
				if (TileMap.mapID == 0)
				{
					num = 600;
				}
				this.life++;
				if ((this.c != null && (this.c.statusMe == 5 || this.c.statusMe == 14)) || this.c == null)
				{
					this.x += (((this.c == null) ? this.xTo : this.c.cx) - this.x) / 2;
					this.y += (((this.c == null) ? this.yTo : this.c.cy) - this.y) / 2;
				}
				if ((Res.abs(this.dx) < 16 && Res.abs(this.dy) < 16) || this.life > num)
				{
					if (this.c != null && this.c.charID >= 0 && this.dameMp != -1L)
					{
						if (this.dameMp != -100L)
						{
							this.c.doInjure(this.dame, this.dameMp, false, true);
						}
						else
						{
							ServerEffect.addServerEffect(80, this.c, 1);
						}
					}
					Effect2.vEffect2.removeElement(this);
					if (this.dameMp != -100L)
					{
						ServerEffect.addServerEffect(81, this.c, 1);
						if (this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w)
						{
							SoundMn.gI().explode_2();
						}
					}
				}
				int num2 = Res.angle(this.dx, this.dy);
				if (Math.abs(num2 - this.angle) < 90 || this.dx * this.dx + this.dy * this.dy > 4096)
				{
					if (Math.abs(num2 - this.angle) < 15)
					{
						this.angle = num2;
					}
					else if ((num2 - this.angle >= 0 && num2 - this.angle < 180) || num2 - this.angle < -180)
					{
						this.angle = Res.fixangle(this.angle + 15);
					}
					else
					{
						this.angle = Res.fixangle(this.angle - 15);
					}
				}
				if (!this.isSpeedUp && this.va < 8192)
				{
					this.va += 1024;
				}
				this.vx = this.va * Res.cos(this.angle) >> 10;
				this.vy = this.va * Res.sin(this.angle) >> 10;
				this.dx += this.vx;
				int num3 = this.dx >> 10;
				this.x += num3;
				this.dx &= 1023;
				this.dy += this.vy;
				int num4 = this.dy >> 10;
				this.y += num4;
				this.dy &= 1023;
			}
			for (int j = 0; j < this.darts.size(); j++)
			{
				SmallDart smallDart = (SmallDart)this.darts.elementAt(j);
				smallDart.index++;
				if (smallDart.index >= this.info.tail.Length)
				{
					this.darts.removeElementAt(j);
				}
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000585AC File Offset: 0x000567AC
		public static int findDirIndexFromAngle(int angle)
		{
			int i = 0;
			while (i < MonsterDart.ARROWINDEX.Length - 1)
			{
				if (angle >= MonsterDart.ARROWINDEX[i] && angle <= MonsterDart.ARROWINDEX[i + 1])
				{
					if (i >= 16)
					{
						return 0;
					}
					return i;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000585F0 File Offset: 0x000567F0
		public override void paint(mGraphics g)
		{
			int num = MonsterDart.findDirIndexFromAngle(360 - this.angle);
			int num2 = (int)MonsterDart.FRAME[num];
			int transform = MonsterDart.TRANSFORM[num];
			for (int i = this.darts.size() / 2; i < this.darts.size(); i++)
			{
				SmallDart smallDart = (SmallDart)this.darts.elementAt(i);
				SmallImage.drawSmallImage(g, (int)this.info.tailBorder[smallDart.index], smallDart.x, smallDart.y, 0, 3);
			}
			int num3 = GameCanvas.gameTick % this.info.headBorder.Length;
			SmallImage.drawSmallImage(g, (int)this.info.headBorder[num3][num2], this.x, this.y, transform, 3);
			for (int j = 0; j < this.darts.size(); j++)
			{
				SmallDart smallDart2 = (SmallDart)this.darts.elementAt(j);
				SmallImage.drawSmallImage(g, (int)this.info.tail[smallDart2.index], smallDart2.x, smallDart2.y, 0, 3);
			}
			SmallImage.drawSmallImage(g, (int)this.info.head[num3][num2], this.x, this.y, transform, 3);
			for (int k = 0; k < this.darts.size(); k++)
			{
				SmallDart smallDart3 = (SmallDart)this.darts.elementAt(k);
				if (Res.abs(MonsterDart.r.nextInt(100)) < (int)this.info.xdPercent)
				{
					SmallImage.drawSmallImage(g, (int)((GameCanvas.gameTick % 2 != 0) ? this.info.xd2[smallDart3.index] : this.info.xd1[smallDart3.index]), smallDart3.x, smallDart3.y, 0, 3);
				}
			}
		}

		// Token: 0x04000AE7 RID: 2791
		public int va;

		// Token: 0x04000AE8 RID: 2792
		private DartInfo info;

		// Token: 0x04000AE9 RID: 2793
		public static MyRandom r = new MyRandom();

		// Token: 0x04000AEA RID: 2794
		public int angle;

		// Token: 0x04000AEB RID: 2795
		public int vx;

		// Token: 0x04000AEC RID: 2796
		public int vy;

		// Token: 0x04000AED RID: 2797
		public int x;

		// Token: 0x04000AEE RID: 2798
		public int y;

		// Token: 0x04000AEF RID: 2799
		public int xTo;

		// Token: 0x04000AF0 RID: 2800
		public int yTo;

		// Token: 0x04000AF1 RID: 2801
		private int life;

		// Token: 0x04000AF2 RID: 2802
		public bool isSpeedUp;

		// Token: 0x04000AF3 RID: 2803
		public long dame;

		// Token: 0x04000AF4 RID: 2804
		public long dameMp;

		// Token: 0x04000AF5 RID: 2805
		public Char c;

		// Token: 0x04000AF6 RID: 2806
		public bool isBoss;

		// Token: 0x04000AF7 RID: 2807
		public MyVector darts = new MyVector();

		// Token: 0x04000AF8 RID: 2808
		private int dx;

		// Token: 0x04000AF9 RID: 2809
		private int dy;

		// Token: 0x04000AFA RID: 2810
		public static int[] ARROWINDEX = new int[]
		{
			0,
			15,
			37,
			52,
			75,
			105,
			127,
			142,
			165,
			195,
			217,
			232,
			255,
			285,
			307,
			322,
			345,
			370
		};

		// Token: 0x04000AFB RID: 2811
		public static int[] TRANSFORM = new int[]
		{
			0,
			0,
			0,
			7,
			6,
			6,
			6,
			2,
			2,
			3,
			3,
			4,
			5,
			5,
			5,
			1
		};

		// Token: 0x04000AFC RID: 2812
		public static sbyte[] FRAME = new sbyte[]
		{
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0,
			1,
			2,
			1,
			0
		};
	}
}
