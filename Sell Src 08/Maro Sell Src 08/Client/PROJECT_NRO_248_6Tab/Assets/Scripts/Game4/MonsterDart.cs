using System;

namespace Game4
{
	// Token: 0x02000225 RID: 549
	public class MonsterDart : Effect2
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x001822C8 File Offset: 0x001804C8
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

		// Token: 0x06001860 RID: 6240 RVA: 0x00182371 File Offset: 0x00180571
		public void setAngle(int angle)
		{
			this.angle = angle;
			this.vx = this.va * Res.cos(angle) >> 10;
			this.vy = this.va * Res.sin(angle) >> 10;
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x001823A6 File Offset: 0x001805A6
		public static void addMonsterDart(int x, int y, bool isBoss, long dame, long dameMp, Char c, int dartType)
		{
			Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, c, dartType));
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x001823C4 File Offset: 0x001805C4
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

		// Token: 0x06001863 RID: 6243 RVA: 0x001827CC File Offset: 0x001809CC
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

		// Token: 0x06001864 RID: 6244 RVA: 0x00182810 File Offset: 0x00180A10
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

		// Token: 0x04002FE5 RID: 12261
		public int va;

		// Token: 0x04002FE6 RID: 12262
		private DartInfo info;

		// Token: 0x04002FE7 RID: 12263
		public static MyRandom r = new MyRandom();

		// Token: 0x04002FE8 RID: 12264
		public int angle;

		// Token: 0x04002FE9 RID: 12265
		public int vx;

		// Token: 0x04002FEA RID: 12266
		public int vy;

		// Token: 0x04002FEB RID: 12267
		public int x;

		// Token: 0x04002FEC RID: 12268
		public int y;

		// Token: 0x04002FED RID: 12269
		public int xTo;

		// Token: 0x04002FEE RID: 12270
		public int yTo;

		// Token: 0x04002FEF RID: 12271
		private int life;

		// Token: 0x04002FF0 RID: 12272
		public bool isSpeedUp;

		// Token: 0x04002FF1 RID: 12273
		public long dame;

		// Token: 0x04002FF2 RID: 12274
		public long dameMp;

		// Token: 0x04002FF3 RID: 12275
		public Char c;

		// Token: 0x04002FF4 RID: 12276
		public bool isBoss;

		// Token: 0x04002FF5 RID: 12277
		public MyVector darts = new MyVector();

		// Token: 0x04002FF6 RID: 12278
		private int dx;

		// Token: 0x04002FF7 RID: 12279
		private int dy;

		// Token: 0x04002FF8 RID: 12280
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

		// Token: 0x04002FF9 RID: 12281
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

		// Token: 0x04002FFA RID: 12282
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
