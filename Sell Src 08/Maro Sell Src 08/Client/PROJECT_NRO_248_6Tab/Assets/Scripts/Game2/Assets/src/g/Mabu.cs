using System;

namespace Game2.Assets.src.g
{
	// Token: 0x02000438 RID: 1080
	internal class Mabu : Char
	{
		// Token: 0x0600301A RID: 12314 RVA: 0x002E7BAA File Offset: 0x002E5DAA
		public Mabu()
		{
			this.getData1();
			this.getData2();
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x002E7BC8 File Offset: 0x002E5DC8
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

		// Token: 0x0600301C RID: 12316 RVA: 0x002E7C24 File Offset: 0x002E5E24
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

		// Token: 0x0600301D RID: 12317 RVA: 0x002E7CDC File Offset: 0x002E5EDC
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

		// Token: 0x0600301E RID: 12318 RVA: 0x002E7D80 File Offset: 0x002E5F80
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

		// Token: 0x0600301F RID: 12319 RVA: 0x002E7DD8 File Offset: 0x002E5FD8
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

		// Token: 0x06003020 RID: 12320 RVA: 0x002E7E88 File Offset: 0x002E6088
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

		// Token: 0x06003021 RID: 12321 RVA: 0x002E8208 File Offset: 0x002E6408
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

		// Token: 0x04005C46 RID: 23622
		public static EffectData data1;

		// Token: 0x04005C47 RID: 23623
		public static EffectData data2;

		// Token: 0x04005C48 RID: 23624
		private new int tick;

		// Token: 0x04005C49 RID: 23625
		private int lastDir;

		// Token: 0x04005C4A RID: 23626
		private bool addFoot;

		// Token: 0x04005C4B RID: 23627
		private Effect effEat;

		// Token: 0x04005C4C RID: 23628
		private new Char focus;

		// Token: 0x04005C4D RID: 23629
		public int xTo;

		// Token: 0x04005C4E RID: 23630
		public int yTo;

		// Token: 0x04005C4F RID: 23631
		private Char[] charAttack;

		// Token: 0x04005C50 RID: 23632
		private int[] damageAttack;

		// Token: 0x04005C51 RID: 23633
		private int dx;

		// Token: 0x04005C52 RID: 23634
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

		// Token: 0x04005C53 RID: 23635
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

		// Token: 0x04005C54 RID: 23636
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

		// Token: 0x04005C55 RID: 23637
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

		// Token: 0x04005C56 RID: 23638
		public static int[][] skills = new int[][]
		{
			Mabu.skill1,
			Mabu.skill2,
			Mabu.skill3,
			Mabu.skill4
		};

		// Token: 0x04005C57 RID: 23639
		public sbyte skillID = -1;

		// Token: 0x04005C58 RID: 23640
		private int frame;

		// Token: 0x04005C59 RID: 23641
		private int pIndex;
	}
}
