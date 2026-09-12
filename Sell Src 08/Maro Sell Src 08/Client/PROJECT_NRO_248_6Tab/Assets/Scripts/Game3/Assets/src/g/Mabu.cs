using System;

namespace Game3.Assets.src.g
{
	// Token: 0x02000360 RID: 864
	internal class Mabu : Char
	{
		// Token: 0x06002676 RID: 9846 RVA: 0x00252B06 File Offset: 0x00250D06
		public Mabu()
		{
			this.getData1();
			this.getData2();
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x00252B24 File Offset: 0x00250D24
		public void eat(int id)
		{
			this.effEat = new Effect(105, this.cx, this.cy + 20, 2, 1, -1);
			EffecMn.addEff(this.effEat);
			if (id == Char.myCharz().charID)
			{
				this.focus = Char.myCharz();
				return;
			}
			this.focus = GameScr.findCharInMap(id);
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x00252B80 File Offset: 0x00250D80
		public new void checkFrameTick(int[] array)
		{
			if (this.skillID == 0)
			{
				if (this.tick == 11)
				{
					this.addFoot = true;
					EffecMn.addEff(new Effect(19, this.cx, this.cy + 20, 2, 1, -1));
				}
				if (this.tick >= array.Length - 1)
				{
					this.skillID = 2;
					return;
				}
			}
			if (this.skillID == 1 && this.tick == array.Length - 1)
			{
				this.skillID = 3;
				this.cy -= 15;
				return;
			}
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x00252C38 File Offset: 0x00250E38
		public void getData1()
		{
			Mabu.data1 = null;
			Mabu.data1 = new EffectData();
			string patch = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				102.ToString(),
				"/data"
			});
			try
			{
				Mabu.data1.readData2(patch);
				Mabu.data1.img = GameCanvas.loadImage("/effectdata/" + 102.ToString() + "/img.png");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x00252CDC File Offset: 0x00250EDC
		public void setSkill(sbyte id, short x, short y, Char[] charHit, int[] damageHit)
		{
			this.skillID = id;
			this.xTo = (int)x;
			this.yTo = (int)y;
			this.lastDir = this.cdir;
			this.cdir = ((this.xTo > this.cx) ? 1 : -1);
			this.charAttack = charHit;
			this.damageAttack = damageHit;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x00252D34 File Offset: 0x00250F34
		public void getData2()
		{
			Mabu.data2 = null;
			Mabu.data2 = new EffectData();
			string patch = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				103.ToString(),
				"/data"
			});
			try
			{
				Mabu.data2.readData2(patch);
				Mabu.data2.img = GameCanvas.loadImage("/effectdata/" + 103.ToString() + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x00252DE4 File Offset: 0x00250FE4
		public override void update()
		{
			if (this.focus != null)
			{
				if (this.effEat.t >= 30)
				{
					this.effEat.x += (this.cx - this.effEat.x) / 4;
					this.effEat.y += (this.cy - this.effEat.y) / 4;
					this.focus.cx = this.effEat.x;
					this.focus.cy = this.effEat.y;
					this.focus.isMabuHold = true;
				}
				else
				{
					this.effEat.trans = ((this.effEat.x > this.focus.cx) ? 1 : 0);
					this.effEat.x += (this.focus.cx - this.effEat.x) / 3;
					this.effEat.y += (this.focus.cy - this.effEat.y) / 3;
				}
			}
			if (this.skillID != -1)
			{
				if (this.skillID == 0 && this.addFoot && GameCanvas.gameTick % 2 == 0)
				{
					this.dx += ((this.xTo <= this.cx) ? -30 : 30);
					EffecMn.addEff(new Effect(103, this.cx + this.dx, this.cy + 20, 2, 1, -1)
					{
						trans = ((this.xTo <= this.cx) ? 1 : 0)
					});
					if ((this.cdir == 1 && this.cx + this.dx >= this.xTo) || (this.cdir == -1 && this.cx + this.dx <= this.xTo))
					{
						this.addFoot = false;
						this.skillID = -1;
						this.dx = 0;
						this.tick = 0;
						this.cdir = this.lastDir;
						for (int i = 0; i < this.charAttack.Length; i++)
						{
							this.charAttack[i].doInjure((long)this.damageAttack[i], 0L, false, false);
						}
					}
				}
				if (this.skillID != 3)
				{
					return;
				}
				this.xTo = this.charAttack[this.pIndex].cx;
				this.yTo = this.charAttack[this.pIndex].cy;
				this.cx += (this.xTo - this.cx) / 3;
				this.cy += (this.yTo - this.cy) / 3;
				if (GameCanvas.gameTick % 5 == 0)
				{
					EffecMn.addEff(new Effect(19, this.cx, this.cy, 2, 1, -1));
				}
				if (Res.abs(this.cx - this.xTo) <= 20 && Res.abs(this.cy - this.yTo) <= 20)
				{
					this.cx = this.xTo;
					this.cy = this.yTo;
					this.charAttack[this.pIndex].doInjure((long)this.damageAttack[this.pIndex], 0L, false, false);
					this.pIndex++;
					if (this.pIndex == this.charAttack.Length)
					{
						this.skillID = -1;
						this.pIndex = 0;
						return;
					}
				}
			}
			else
			{
				base.update();
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x00253164 File Offset: 0x00251364
		public override void paint(mGraphics g)
		{
			if (this.skillID != -1)
			{
				base.paintShadow(g);
				g.translate(0, GameCanvas.transY);
				this.checkFrameTick(Mabu.skills[(int)this.skillID]);
				if (this.skillID == 0 || this.skillID == 1)
				{
					Mabu.data1.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				else
				{
					Mabu.data2.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				g.translate(0, -GameCanvas.transY);
				return;
			}
			base.paint(g);
		}

		// Token: 0x040049C7 RID: 18887
		public static EffectData data1;

		// Token: 0x040049C8 RID: 18888
		public static EffectData data2;

		// Token: 0x040049C9 RID: 18889
		private new int tick;

		// Token: 0x040049CA RID: 18890
		private int lastDir;

		// Token: 0x040049CB RID: 18891
		private bool addFoot;

		// Token: 0x040049CC RID: 18892
		private Effect effEat;

		// Token: 0x040049CD RID: 18893
		private new Char focus;

		// Token: 0x040049CE RID: 18894
		public int xTo;

		// Token: 0x040049CF RID: 18895
		public int yTo;

		// Token: 0x040049D0 RID: 18896
		private Char[] charAttack;

		// Token: 0x040049D1 RID: 18897
		private int[] damageAttack;

		// Token: 0x040049D2 RID: 18898
		private int dx;

		// Token: 0x040049D3 RID: 18899
		public static int[] skill1 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5
		};

		// Token: 0x040049D4 RID: 18900
		public static int[] skill2 = new int[]
		{
			0,
			0,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			9,
			9,
			9,
			10,
			10
		};

		// Token: 0x040049D5 RID: 18901
		public static int[] skill3 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			10,
			10,
			11,
			11,
			12,
			12
		};

		// Token: 0x040049D6 RID: 18902
		public static int[] skill4 = new int[]
		{
			13,
			13,
			14,
			14,
			15,
			15,
			16,
			16
		};

		// Token: 0x040049D7 RID: 18903
		public static int[][] skills = new int[][]
		{
			Mabu.skill1,
			Mabu.skill2,
			Mabu.skill3,
			Mabu.skill4
		};

		// Token: 0x040049D8 RID: 18904
		public sbyte skillID = -1;

		// Token: 0x040049D9 RID: 18905
		private int frame;

		// Token: 0x040049DA RID: 18906
		private int pIndex;
	}
}
