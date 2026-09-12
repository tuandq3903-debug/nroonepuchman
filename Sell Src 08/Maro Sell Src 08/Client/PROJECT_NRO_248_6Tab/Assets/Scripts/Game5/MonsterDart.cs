using System;

namespace Game5
{
	// Token: 0x0200014D RID: 333
	public class MonsterDart : Effect2
	{
		// Token: 0x06000EBB RID: 3771 RVA: 0x000ED224 File Offset: 0x000EB424
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

		// Token: 0x06000EBC RID: 3772 RVA: 0x000ED2CD File Offset: 0x000EB4CD
		public void setAngle(int angle)
		{
			this.angle = angle;
			this.vx = this.va * Res.cos(angle) >> 10;
			this.vy = this.va * Res.sin(angle) >> 10;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x000ED302 File Offset: 0x000EB502
		public static void addMonsterDart(int x, int y, bool isBoss, long dame, long dameMp, Char c, int dartType)
		{
			Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, c, dartType));
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000ED320 File Offset: 0x000EB520
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

		// Token: 0x06000EBF RID: 3775 RVA: 0x000ED728 File Offset: 0x000EB928
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

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000ED76C File Offset: 0x000EB96C
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

		// Token: 0x04001D66 RID: 7526
		public int va;

		// Token: 0x04001D67 RID: 7527
		private DartInfo info;

		// Token: 0x04001D68 RID: 7528
		public static MyRandom r = new MyRandom();

		// Token: 0x04001D69 RID: 7529
		public int angle;

		// Token: 0x04001D6A RID: 7530
		public int vx;

		// Token: 0x04001D6B RID: 7531
		public int vy;

		// Token: 0x04001D6C RID: 7532
		public int x;

		// Token: 0x04001D6D RID: 7533
		public int y;

		// Token: 0x04001D6E RID: 7534
		public int xTo;

		// Token: 0x04001D6F RID: 7535
		public int yTo;

		// Token: 0x04001D70 RID: 7536
		private int life;

		// Token: 0x04001D71 RID: 7537
		public bool isSpeedUp;

		// Token: 0x04001D72 RID: 7538
		public long dame;

		// Token: 0x04001D73 RID: 7539
		public long dameMp;

		// Token: 0x04001D74 RID: 7540
		public Char c;

		// Token: 0x04001D75 RID: 7541
		public bool isBoss;

		// Token: 0x04001D76 RID: 7542
		public MyVector darts = new MyVector();

		// Token: 0x04001D77 RID: 7543
		private int dx;

		// Token: 0x04001D78 RID: 7544
		private int dy;

		// Token: 0x04001D79 RID: 7545
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

		// Token: 0x04001D7A RID: 7546
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

		// Token: 0x04001D7B RID: 7547
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
